Public Class frmEmpeniosCompras
    Dim IdsSucursales As New elemento
    Dim IdsVendedores As New elemento
    Dim IdsClasificacion As New elemento
    Dim IdsClasificacionv As New elemento
    Dim IdsClasificaciont As New elemento
    Dim IdsCajas As New elemento
    Dim IdCajaDefault As Integer
    Dim idMovimiento As Integer
    Dim Estado As Byte
    Dim FolioAnt As Integer
    Dim idCliente As Integer
    Dim total As Double
    Dim tasa As Double
    Dim idDetalle As Integer
    Dim Tabla As New DataTable
    Dim ConsultaOn As Boolean
    Dim Saldo As Double
    Dim CreditoCliente As Double
    Dim SaldoaFavor As Double
    Dim TipoImpresora As Byte
    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim CuantaY As Integer
    Dim Co As New dbEmpeniosConfiguracion(MySqlcon)
    Dim sa As New dbSucursalesArchivos
    Dim idCaja As Integer
    Dim IdsForma As New elemento
    Dim fechaNueva As String
    Dim TotalNuevo As Double
    Dim Salir As Boolean
    Dim tipoJoya As String
    Dim TipoEmpeño As Integer

    Public Sub New(ByVal pidVenta As Integer, ByVal pIdCaja As Integer, ByVal pIdVendedor As Integer, ByVal pidSucursal As Integer, ByVal pFechaNueva As String, ByVal pTotalNuevo As Double, ByVal Psalir As Boolean)
        InitializeComponent()
        idMovimiento = pidVenta
        IdCajaDefault = pIdCaja
        Salir = Psalir
        fechaNueva = pFechaNueva
        TotalNuevo = pTotalNuevo

    End Sub

    Private Sub frmEmpeniosCompras_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Co.LlenaDatos()
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        LlenaCombos("tblvendedores", cmbVendedor, "nombre", "nombret", "idvendedor", IdsVendedores)
        LlenaCombos("tblempeniosclasificacion", cmbClasificacion, "nombre", "nombret", "idClasificacion", IdsClasificacion)
        LlenaCombos("tblformasdepagoremisiones", ComboBox4, "nombre", "nombrec", "idforma", IdsForma, "tipo=1 or tipo=2", , "idforma")
        LlenaCombos("tblempeniosclasificacion", cmbClasificacionV, "nombre", "nombret", "idClasificacion", IdsClasificacionv)
        LlenaCombos("tblempeniosclasificacion", cmbClasificacionT, "nombre", "nombret", "idClasificacion", IdsClasificaciont)
        If cmbClasificacion.Items.Count < 1 Then
            MsgBox("Es necesario dar de alta clasificaciones.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
        cmbKilates.SelectedIndex = 0
        cmbTipoJoya.SelectedIndex = 0
        If idMovimiento <> 0 Then
            LlenaDatosVenta()
            If fechaNueva <> "" And TotalNuevo <> -1 Then
                Imprimir(Salir)
            End If
        Else
            nuevo()
        End If
        Dim usuario As New dbUsuarios(GlobalIdUsuario, MySqlcon)
        cmbVendedor.SelectedIndex = IdsVendedores.Busca(usuario.IdVendedor)
        TipoEmpeño = 0
    End Sub

    Private Sub frmEmpeniosCompras_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este movimiento no ha sido guardado. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbEmpeniosCompras(MySqlcon)
                c.EliminarGasto(idMovimiento)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmEmpeniosCompras_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    End Sub
    Private Sub nuevo()
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        cmbCaja.SelectedIndex = IdsCajas.Busca(IdCajaDefault)
        Estado = Estados.Inicio
        Button1.Enabled = False
        Button14.Enabled = False
        Button13.Enabled = False
        Button2.Enabled = False
        Button21.Enabled = False
        idCliente = 0
        Label24.Visible = False
        Button4.Enabled = True
        Button8.Enabled = True
        txtDiasPrestamo.Enabled = True
        txtDiasPrestamo.Text = "0"
        TextBox1.Text = ""
        TextBox7.Text = ""
        TextBox1.Enabled = True
        TextBox7.Enabled = True
        Label12.Text = ""
        ComboBox4.SelectedIndex = 0
        cmbCaja.Enabled = True
        txtFolio.Enabled = True
        txtSerie.Enabled = True
        dtpFecha.Enabled = True
        ComboBox3.Enabled = True
        Button5.Enabled = True
        Button19.Enabled = True
        cmbVendedor.Enabled = True
        dtpFecha.Value = Date.Now
        txtImporte.Text = ""
        If cmbClasificacion.Items.Count > 0 Then
            cmbClasificacion.SelectedIndex = 0
        End If
        txtComentario.Text = ""
        txtDescripcion.Text = ""
        FolioAnt = 0
        cmbKilates.SelectedIndex = 0
        cmbVendedor.SelectedIndex = 0
        NuevoConceptov()
        grpVEhiculos.Enabled = True
        Button2.Enabled = False
        nuevoFolio()

        idMovimiento = 0
        Label24.Visible = False
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False

        txtFolio.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        grpJoyas.Enabled = True
        Panel2.Enabled = True
        Label14.Text = 0.ToString("C2")

        NuevoConcepto()

        TextBox1.Focus()
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosCambiarSucursal, PermisosN.Secciones.Empenios) = False Then
            ComboBox3.Enabled = False
        End If
    End Sub
    Private Sub nuevoFolio()
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        If Co.folio = 0 Then
            Dim anio As String = Date.Now.Year.ToString.Chars(2) + Date.Now.Year.ToString.Chars(3)
            txtSerie.Text = Date.Now.Day.ToString("00") + Date.Now.Month.ToString("00") + anio
            Dim V As New dbEmpeniosCompras(MySqlcon)
            txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
        Else
            If Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.MovimientosCaja, 0) Then
                txtSerie.Text = Sf.Serie
            Else
                txtSerie.Text = ""
            End If

            Dim V As New dbEmpeniosCompras(MySqlcon)
            txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        sa.DaOpciones(GlobalIdEmpresa, False)
        idCaja = sa.idCaja
        LlenaCombos("tblcajas", cmbCaja, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        cmbCaja.SelectedIndex = IdsCajas.Busca(idCaja)

        Dim Sf As New dbSucursalesFolios(MySqlcon)


        If Co.folio = 0 Then
            Dim anio As String = Date.Now.Year.ToString.Chars(2) + Date.Now.Year.ToString.Chars(3)
            txtSerie.Text = Date.Now.Day.ToString("00") + Date.Now.Month.ToString("00") + anio
            Dim V As New dbEmpeniosCompras(MySqlcon)
            txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
        Else
            If Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.MovimientosCaja, 0) Then
                txtSerie.Text = Sf.Serie
            Else
                txtSerie.Text = ""
            End If

            Dim V As New dbEmpeniosCompras(MySqlcon)
            txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
        End If

    End Sub
    Private Sub BotonAgregar()
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasAlta, PermisosN.Secciones.Empenios) = True Then
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                AgregaArticulo()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub Guardar()
        Try
            If idCliente <> 0 Then


                Dim errores = False
                Dim C As New dbEmpeniosCompras(MySqlcon)
                txtFolio.BackColor = Color.White
                Label17.Visible = False

                If C.ChecaFolioRepetido(CInt(txtFolio.Text), txtSerie.Text) Then
                   nuevoFolio()
                Else
                    FolioAnt = CInt(txtFolio.Text)
                End If
                If errores Then
                    MessageBox.Show("Folio repetido", "Pull System Soft", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    C.Guardar(idCliente, Format(dtpFecha.Value, "yyyy/MM/dd"), txtFolio.Text, total, IdsSucursales.Valor(ComboBox3.SelectedIndex), txtSerie.Text, IdsCajas.Valor(cmbCaja.SelectedIndex), IdsVendedores.Valor(cmbVendedor.SelectedIndex), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), txtComentario.Text, Integer.Parse(txtDiasPrestamo.Text), IdsForma.Valor(ComboBox4.SelectedIndex), TipoEmpeño)
                    idMovimiento = C.Idmovimiento
                    Estado = 1
                    txtFolio.Enabled = False
                    txtSerie.Enabled = False
                    Button1.Enabled = True
                    Button14.Enabled = True
                    Button21.Enabled = True
                    Button1.Enabled = True
                    Button14.Enabled = True
                    Button13.Enabled = True
                    Button2.Enabled = True
                End If
            Else
                MsgBox("Debe indicar un cliente", MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbEmpeniosComprasDetalles(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            Dim tipo As Integer
            'If IdInventario = 0 Then MsgError += "Debe indicar un artículo."
            If cmbTipoJoya.SelectedIndex = 0 Then
                tipo = 0
            Else
                tipo = 1
            End If
            If IsNumeric(txtImporte.Text) = False Then
                MsgError += vbCrLf + "El importe debe ser un valor numérico."
                HayError = True
            Else
                If CInt(txtImporte.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += " El importe debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If
            'If IsNumeric(txtKilates.Text) = False Then
            '    MsgError += vbCrLf + "La tasa debe ser un valor numérico."
            '    HayError = True

            'End If
            If IsNumeric(txtEvaluo.Text) = False Then
                MsgError += vbCrLf + "El Evalúo debe ser un valor numérico."
                HayError = True
            Else
                'If CInt(txtEvaluo.Text) <= 0 And My.Settings.conceptocero = False Then
                '    MsgError += " El Evalúo debe ser un valor mayor a 0."
                '    HayError = True
                'End If
            End If
            If cmbClasificacion.Text = "" Then
                MsgError += " Es necesario dar de alta clasificacines."
                HayError = True
            End If
            If IsNumeric(txtPeso.Text) = False Then
                MsgError += vbCrLf + "El Peso debe ser un valor numérico."
                HayError = True

            End If

            If HayError = False Then

                If Button4.Text = "Agregar Concepto" Then
                    If tipo = 0 Then
                        CD.Guardar(idMovimiento, txtDescripcion.Text, Double.Parse(txtImporte.Text), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), cmbKilates.SelectedIndex, Double.Parse(txtPeso.Text), Double.Parse(txtEvaluo.Text), tipo) ',  IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    Else
                        CD.Guardar(idMovimiento, txtDescripcion.Text, Double.Parse(txtImporte.Text), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), 0, Double.Parse(txtPeso.Text), Double.Parse(txtEvaluo.Text), tipo) ',  IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    End If

                    'aqui me quede
                    ConsultaDetalles()
                    NuevoConcepto()

                Else
                    If tipo = 0 Then
                        CD.Modificar(idDetalle, idMovimiento, txtDescripcion.Text, Double.Parse(txtImporte.Text), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), cmbKilates.SelectedIndex, Double.Parse(txtPeso.Text), Double.Parse(txtEvaluo.Text), tipo)
                    Else
                        CD.Modificar(idDetalle, idMovimiento, txtDescripcion.Text, Double.Parse(txtImporte.Text), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), 0, Double.Parse(txtPeso.Text), Double.Parse(txtEvaluo.Text), tipo)

                    End If


                    ConsultaDetalles()
                    NuevoConcepto()

                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ConsultaDetalles()
        Try

            Tabla.Rows.Clear()
            Dim T As DataTable
            Dim CD As New dbEmpeniosComprasDetalles(MySqlcon)
            T = CD.ConsultaReader(idMovimiento).ToTable()
            CD.impresionRecibo(idMovimiento)

            DGDetalles.DataSource = T
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGDetalles.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(5).Visible = False
            DGDetalles.Columns(8).Visible = False
            DGDetalles.Columns(1).HeaderText = "Descripción"
            DGDetalles.Columns(2).HeaderText = "Tipo"
            DGDetalles.Columns(3).HeaderText = "Kilates"
            DGDetalles.Columns(4).HeaderText = "Peso"
            DGDetalles.Columns(5).HeaderText = "Clasificación"
            DGDetalles.Columns(6).HeaderText = "Evalúo"
            DGDetalles.Columns(7).HeaderText = "Importe"
            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            ConsultaOn = True
            SacaTotal()
            ConsultaOn = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub ConsultaDetallesv()
        Try

            Tabla.Rows.Clear()
            Dim T As DataTable
            Dim CD As New dbEmpeniosComprasDetallesV(MySqlcon)
            T = CD.ConsultaReader(idMovimiento).ToTable()


            DGDetalles.DataSource = T
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).Visible = False
            DGDetalles.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'DGDetalles.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable

            DGDetalles.Columns(2).HeaderText = "Clasificación"
            DGDetalles.Columns(3).HeaderText = "Marca"
            DGDetalles.Columns(4).HeaderText = "Modelo"
            DGDetalles.Columns(5).HeaderText = "Color"
            DGDetalles.Columns(6).HeaderText = "N° Serie"
            DGDetalles.Columns(7).HeaderText = "Placas"
            DGDetalles.Columns(8).HeaderText = "Evalúo"
            DGDetalles.Columns(9).HeaderText = "Importe"
            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            ConsultaOn = True
            SacaTotalv()
            ConsultaOn = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim V As New dbEmpeniosComprasDetalles(MySqlcon)
                V.DaTotal(idMovimiento)
                If TotalNuevo = -1 Then
                    Label14.Text = V.TotalVenta.ToString("C2")
                    total = V.TotalVenta
                Else
                    Label14.Text = TotalNuevo.ToString("C2")
                    total = TotalNuevo
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub SacaTotalv()
        Try
            If ConsultaOn Then
                Dim V As New dbEmpeniosComprasDetallesV(MySqlcon)
                V.DaTotal(idMovimiento)
                If TotalNuevo = -1 Then
                    Label14.Text = V.TotalVenta.ToString("C2")
                    total = V.TotalVenta
                Else
                    Label14.Text = TotalNuevo.ToString("C2")
                    total = TotalNuevo
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub NuevoConcepto()
        'IdInventario = 0
        'IdVariante = 0
        txtDescripcion.Text = ""
        Button4.Text = "Agregar Concepto"
        txtImporte.Text = "0.00"
        txtDescripcion.Text = cmbClasificacion.Text
        cmbKilates.SelectedIndex = 1
        txtPeso.Text = "0.00"
        txtEvaluo.Text = "0.00"
        txtDescripcion.Focus()
        Button9.Enabled = False
    End Sub
    Private Sub NuevoConceptov()
        'IdInventario = 0
        'IdVariante = 0
        txtMarcav.Text = ""
        txtModelo.Text = ""
        txtColor.Text = ""
        txtNoSerie.Text = ""
        txtPlacasV.Text = ""
        txtEvaluoV.Text = "0.00"
        txtImporteV.Text = "0.00"
        btnAgregarV.Text = "Agregar Concepto"
        txtMarcav.Focus()
        btnAgregarV.Enabled = True
        grpVEhiculos.Enabled = True
        Button9.Enabled = False
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                If TipoEmpeño = 0 Then
                    Dim CD As New dbEmpeniosComprasDetalles(MySqlcon)
                    CD.Eliminar(idDetalle)
                    ConsultaDetalles()
                    NuevoConcepto()
                End If
                If TipoEmpeño = 1 Then
                    Dim CD As New dbEmpeniosComprasDetallesV(MySqlcon)
                    CD.Eliminar(idDetalle)
                    ConsultaDetallesv()
                    NuevoConceptov()
                End If
                If TipoEmpeño = 2 Then
                    Dim CD As New dbEmpeniosComprasDetallest(MySqlcon)
                    CD.Eliminar(idDetalle)
                    ConsultaDetallest()
                    nuevoConceptoTe()
                End If
                PopUp("Concepto Eliminado", 90)
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGDetalles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGDetalles.Click

    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        If TipoEmpeño = 0 Then
            LlenaDatosDetallesA()
        End If
        If TipoEmpeño = 1 Then
            LlenaDatosDetallesV()
        End If
        If TipoEmpeño = 2 Then
            LlenaDatosDetallesT()
        End If
    End Sub
    Private Sub LlenaDatosDetallesT()
        Try

            idDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbEmpeniosComprasDetallest(idDetalle, MySqlcon)

            cmbClasificacionT.SelectedIndex = IdsClasificaciont.Busca(CD.clasificacion)
            txtDescripcionT.Text = CD.descripcion
            txtEvaluoT.Text = CD.evaluo.ToString("0.00")
            txtImporteT.Text = CD.importe.ToString("0.00")
            agregarConceptoT.Text = "Modificar Concepto"
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then
                Button9.Enabled = True
            End If
            txtDescripcionT.Focus()


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub LlenaDatosDetallesA()
        Try

            idDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbEmpeniosComprasDetalles(idDetalle, MySqlcon)

            cmbClasificacion.SelectedIndex = IdsClasificacion.Busca(CD.clasificacion)
            txtDescripcion.Text = CD.descripcion
            txtImporte.Text = CD.precio.ToString("0.00")
            cmbKilates.SelectedIndex = CD.kilates
            txtPeso.Text = CD.peso.ToString("0.00")
            txtEvaluo.Text = CD.evaluo.ToString("0.00")
            Button4.Text = "Modificar Concepto"
            cmbTipoJoya.SelectedIndex = CD.tipo
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then
                Button9.Enabled = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaDatosDetallesV()
        Try

            idDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbEmpeniosComprasDetallesV(idDetalle, MySqlcon)

            cmbClasificacionV.SelectedIndex = IdsClasificacionv.Busca(CD.clasificacion)
            txtMarcav.Text = CD.marca
            txtModelo.Text = CD.modelo
            txtColor.Text = CD.color
            txtNoSerie.Text = CD.noSerie
            txtPlacasV.Text = CD.placas
            txtEvaluoV.Text = CD.evaluo.ToString("0.00")
            txtImporteV.Text = CD.importe.ToString("0.00")
            btnAgregarV.Text = "Modificar Concepto"
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then
                Button9.Enabled = True
            End If
            txtMarcav.Focus()


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Modificar(ByVal pEstado As Byte)
        Try
            Dim MensajeError As String = ""
            Dim C As New dbEmpeniosCompras(MySqlcon)
            Dim CD As New dbEmpeniosComprasDetalles(MySqlcon)


            If IsNumeric(txtFolio.Text) = False Then MensajeError = "El folio debe ser un valor numérico."
            If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasAlta, PermisosN.Secciones.Empenios) = False And pEstado <> Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasCancelar, PermisosN.Secciones.Empenios) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If pEstado = Estados.Guardada Then
                nuevoFolio()
            End If
            If MensajeError = "" Then
                If C.ChecaFolioRepetido(txtFolio.Text, txtSerie.Text) Then
                    If pEstado = Estados.Guardada Then txtFolio.Text = C.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
                End If


                CD.DaTotal(idMovimiento)
                C.Modificar(idMovimiento, idCliente, Format(dtpFecha.Value, "yyyy/MM/dd"), txtFolio.Text, total, IdsSucursales.Valor(ComboBox3.SelectedIndex), txtSerie.Text, IdsCajas.Valor(cmbCaja.SelectedIndex), IdsVendedores.Valor(cmbVendedor.SelectedIndex), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), txtComentario.Text, Integer.Parse(txtDiasPrestamo.Text), IdsForma.Valor(ComboBox4.SelectedIndex), TipoEmpeño, pEstado)
                Estado = pEstado
                Dim Forma As New dbFormasdePagoRemisiones(IdsForma.Valor(ComboBox4.SelectedIndex), MySqlcon)
                If Forma.Tipo = 1 Then
                    If pEstado = Estados.Guardada Then
                        Dim Ca As New dbCajas(MySqlcon)
                        Ca.MovimientodeCaja(C.IdCaja, total * -1)
                    End If
                    If pEstado = Estados.Cancelada Then
                        Dim Ca As New dbCajas(MySqlcon)
                        Ca.MovimientodeCaja(C.IdCaja, total)
                    End If
                End If
                If pEstado = Estados.Guardada Then
                    Button2.Enabled = True
                    Button13.Enabled = True
                    Button21.Enabled = True
                End If

                If pEstado = Estados.Cancelada Then
                    Button21.Enabled = False
                    Button13.Enabled = False
                    Button2.Enabled = False
                End If
                If pEstado <> Estados.SinGuardar Then
                    'nuevo()
                    If pEstado = Estados.Cancelada Then
                        Label24.Visible = True
                        Label24.Text = "Cancelada"
                        Label24.ForeColor = Color.Red
                    End If
                    If pEstado = Estados.Guardada Then
                        Label24.Visible = True
                        Label24.Text = "Guardada"
                        Label24.ForeColor = Color.FromArgb(50, 255, 50)
                    End If
                    Button14.Enabled = False
                    Button1.Enabled = False

                    Button4.Enabled = False
                    Button8.Enabled = False
                End If
            Else
                MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub BotonNuevo()
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este movimiento no ha sido guardado. ¿Desea iniciar un nuevo movimiento? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbEmpeniosCompras(MySqlcon)
                c.EliminarGasto(idMovimiento)
                nuevo()
                nuevoConceptoTe()
                NuevoConceptov()
            End If
        Else
            nuevo()
            nuevoConceptoTe()
            NuevoConceptov()
        End If
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
        ' ActualizarConfig()
        ' ActualizaDiasPrestamo()
        Imprimir()
        nuevo()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        BotonNuevo()
        Dim Sf As New dbSucursalesFolios(MySqlcon)

        Dim anio As String = Date.Now.Year.ToString.Chars(2) + Date.Now.Year.ToString.Chars(3)
        txtSerie.Text = Date.Now.Day.ToString("00") + Date.Now.Month.ToString("00") + anio
        Dim V As New dbEmpeniosCompras(MySqlcon)
        txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString

    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(txtComentario.Text, 2000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtComentario.Text = et.Texto
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbEmpeniosCompras(MySqlcon)
                V.ActualizaComentario(idMovimiento, txtComentario.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Modificar(Estados.Pendiente)
        '  ActualizaDiasPrestamo()
        nuevo()
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim em As New dbEmpeniosCompras(MySqlcon)
        '  If em.TienePagos(idMovimiento) = False Then
        If MsgBox("¿Cancelar este movimiento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

            Modificar(Estados.Cancelada)
        End If
        '  Else
        'MsgBox("Este empeño tiene pagos. Cancele primero los pagos para poder cancelar el empeño.", MsgBoxStyle.Information, GlobalNombreApp)
        'End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar esta compra?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasAlta, PermisosN.Secciones.Empenios) = True Then
                Dim C As New dbEmpeniosCompras(MySqlcon)
                C.EliminarGasto(idMovimiento)
                PopUp("Compra Eliminada", 90)
                nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim f As New frmEmpeniosGastosConsulta(ModosDeBusqueda.Secundario)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idMovimiento = f.IdCompra
            LlenaDatosVenta()
        End If
        f.Dispose()
    End Sub
    Private Sub LlenaDatosVenta() 'aqui me quede
        Dim C As New dbEmpeniosCompras(idMovimiento, MySqlcon)


        txtFolio.Text = C.Folio.ToString
        FolioAnt = C.Folio
        txtSerie.Text = C.Serie
        'TextBox1.Text = C.Cliente.Clave
        Estado = C.Estado
        TextBox1.Text = C.Cliente.Clave
        BuscaClienteBoton2()

        Button2.Enabled = True
        If fechaNueva = "" Then
            dtpFecha.Value = C.Fecha
        Else
            dtpFecha.Value = fechaNueva
        End If

        txtComentario.Text = C.Comentario
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        cmbCaja.SelectedIndex = IdsCajas.Busca(C.IdCaja)
        ComboBox4.SelectedIndex = IdsForma.Busca(C.IdForma)
        'ComboBox4.Enabled = False
        If C.tipoEmpenio = 0 Then
            TipoEmpeño = 0
            rdbJoyería.Checked = True
            ConsultaDetalles()
        End If
        If C.tipoEmpenio = 1 Then
            TipoEmpeño = 1
            rdbVehiculos.Checked = True
            ConsultaDetallesv()
        End If
        If C.tipoEmpenio = 2 Then
            TipoEmpeño = 2
            rdbTerrenos.Checked = True
            ConsultaDetallest()
        End If
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                cmbCaja.Enabled = False
                Button13.Enabled = False
                grpJoyas.Enabled = False
                Button21.Enabled = False
                Panel2.Enabled = False
                grpVEhiculos.Enabled = False
                grpTerrenos.Enabled = False
                Button4.Enabled = False
                Button8.Enabled = False
                Button21.Enabled = True
                txtSerie.Enabled = False
                txtFolio.Enabled = False
                TextBox1.Enabled = False
                TextBox7.Enabled = False
                dtpFecha.Enabled = False
                ComboBox3.Enabled = False
                cmbVendedor.Enabled = False
                Button5.Enabled = False
                Button19.Enabled = False
                txtDiasPrestamo.Enabled = False
                Button2.Enabled = False
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                grpJoyas.Enabled = False
                Button21.Enabled = True
                grpTerrenos.Enabled = False
                Panel2.Enabled = False
                grpVEhiculos.Enabled = False
                cmbCaja.Enabled = False
                Button4.Enabled = False
                Button8.Enabled = False
                Button21.Enabled = True
                txtSerie.Enabled = False
                txtFolio.Enabled = False
                TextBox1.Enabled = False
                TextBox7.Enabled = False
                dtpFecha.Enabled = False
                ComboBox3.Enabled = False
                cmbVendedor.Enabled = False
                Button5.Enabled = False
                Button19.Enabled = False
                txtDiasPrestamo.Enabled = False
                Button2.Enabled = False
            Case Else

                Label24.Visible = False
                Button13.Enabled = True
                grpJoyas.Enabled = True
                Button21.Enabled = True
                nuevoFolio()
                Panel2.Enabled = True
                grpVEhiculos.Enabled = True
                grpTerrenos.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                cmbCaja.Enabled = True
                Button4.Enabled = True
                Button8.Enabled = True
                Enabled = True
                txtSerie.Enabled = True
                txtFolio.Enabled = True
                TextBox1.Enabled = True
                TextBox7.Enabled = True
                dtpFecha.Enabled = True
                ComboBox3.Enabled = True
                cmbVendedor.Enabled = True
                Button5.Enabled = True
                Button19.Enabled = True
                txtDiasPrestamo.Enabled = True
                Button2.Enabled = True

        End Select


    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If Estado <> Estados.Inicio Then
            Imprimir()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        BuscaClienteBoton()
        cmbClasificacion.Focus()
    End Sub
    Private Sub BuscaClienteBoton()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            'Saldo = B.Cliente.DaSaldoAFecha(B.Cliente.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
            'CreditoCliente = B.Cliente.Credito
            'SaldoaFavor = CDbl(Format(B.Cliente.DaSaldoAFavor(B.Cliente.ID), "0.00"))
            If B.Cliente.DireccionFiscal = 0 Then
                TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.CP
            Else
                TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion2 + " " + B.Cliente.NoExterior2 + " " + B.Cliente.Ciudad2 + " " + B.Cliente.CP2
            End If

            Dim Ident As New dbIdentificacion(MySqlcon)
            Ident.LlenaDatos(B.Cliente.buscarUltimoID(B.Cliente.ID))
            Label12.Text = "Identificacion: " + Ident.Nombre + vbCrLf + " Número: " + B.Cliente.obtenerIdentificacion(B.Cliente.ID, Ident.Ididentificacion)

            'TextBox13.Text = "Días/Lím: " + B.Cliente.CreditoDias.ToString + "/" + Format(B.Cliente.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(Saldo, "#,##0.00") + vbCrLf + "A Favor: " + Format(SaldoaFavor, "#,##0.00")

            idCliente = B.Cliente.ID

            ConsultaOn = False
            TextBox1.Text = B.Cliente.Clave
            ConsultaOn = True
            B.Dispose()

        End If
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim FC As New frmClientes(1, idCliente)
        If FC.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FC.CodigoCliente
        End If
        FC.Dispose()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()
        'ActualizaDiasPrestamo()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NuevoConcepto()
    End Sub
    Private Sub BuscaClienteBoton2()
        Try
            Dim c As New dbClientes(MySqlcon)

            If c.BuscaCliente(TextBox1.Text) Then
                If c.DireccionFiscal = 0 Then
                    TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                Else
                    TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion2 + " " + c.NoExterior2 + " " + c.Ciudad2 + " " + c.CP2
                End If
                'Saldo = c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
                'CreditoCliente = c.Credito

                'SaldoaFavor = CDbl(Format(c.DaSaldoAFavor(c.ID), "0.00"))
                'TextBox13.Text = "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(Saldo, "#,##0.00") + vbCrLf + "A Favor: " + Format(SaldoaFavor, "#,##0.00")
                Dim Ident As New dbIdentificacion(MySqlcon)
                Ident.LlenaDatos(c.buscarUltimoID(c.ID))
                Label12.Text = "Identificacion: " + Ident.Nombre + vbCrLf + " Número: " + c.obtenerIdentificacion(c.ID, Ident.Ididentificacion)
                idCliente = c.ID
            Else
                idCliente = 0
                TextBox7.Text = ""
                Label12.Text = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim et As New frmVentasTextoExtra(txtDescripcion.Text, 45, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtDescripcion.Text = et.Texto
        End If
    End Sub

    Private Sub calcularEvaluo()
        If cmbKilates.SelectedIndex <> 0 Then
            Dim evaluo As Double
            If cmbKilates.SelectedIndex = 1 Then
                '8K
                evaluo = 8 * Co.factor
                evaluo = evaluo * Double.Parse(txtPeso.Text)
                txtEvaluo.Text = evaluo.ToString("0.00")
            End If
            If cmbKilates.SelectedIndex = 2 Then
                '10k
                evaluo = 10 * Co.factor
                evaluo = evaluo * Double.Parse(txtPeso.Text)
                txtEvaluo.Text = evaluo.ToString("0.00")
            End If
            If cmbKilates.SelectedIndex = 3 Then
                '14k
                evaluo = 14 * Co.factor
                evaluo = evaluo * Double.Parse(txtPeso.Text)
                txtEvaluo.Text = evaluo.ToString("0.00")
            End If
            If cmbKilates.SelectedIndex = 4 Then
                '18k
                evaluo = 18 * Co.factor
                evaluo = evaluo * Double.Parse(txtPeso.Text)
                txtEvaluo.Text = evaluo.ToString("0.00")
            End If
            If cmbKilates.SelectedIndex = 5 Then
                '24k
                evaluo = 24 * Co.factor
                evaluo = evaluo * Double.Parse(txtPeso.Text)
                txtEvaluo.Text = evaluo.ToString("0.00")
            End If
        End If
    End Sub
    Private Sub calcularEvaluoPlata()
        Dim evaluo As Double
        evaluo = Co.valorPlata * Double.Parse(txtPeso.Text)
        txtEvaluo.Text = evaluo.ToString("0.00")
    End Sub


    Private Sub cmbKilates_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbKilates.SelectedIndexChanged
        If tipoJoya = "Oro" Then
            calcularEvaluo()
        Else
            calcularEvaluoPlata()
        End If
    End Sub

    Private Sub txtPeso_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPeso.Leave
        Dim x As Double
        If txtPeso.Text = "." Then
            txtPeso.Text = "0.00"
        End If
        If txtPeso.Text = "" Then
            txtPeso.Text = "0.00"
        Else
            x = Double.Parse(txtPeso.Text)
            txtPeso.Text = Format(x, "0.00####")
        End If
        If txtPeso.Text <> "" Then
            If tipoJoya = "Oro" Then
                calcularEvaluo()
            Else
                calcularEvaluoPlata()
            End If

        End If
    End Sub

    Private Sub txtPeso_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPeso.TextChanged
        If txtPeso.Text <> "" Then


            If txtPeso.Text = "." Then
                txtPeso.Text = "0."
            End If
            If tipoJoya = "Oro" Then
                calcularEvaluo()
            Else
                calcularEvaluoPlata()
            End If

        End If
    End Sub
    Private Function devuelveKilate(ByVal linea As String) As String
        Dim kilat As String = " "

        If linea = 0 Then
            kilat = " "
        End If
        If linea = "1" Then
            kilat = "7 K"
        End If
        If linea = "2" Then
            kilat = "8 K"
        End If
        If linea = "3" Then
            kilat = "9 K"
        End If
        If linea = "4" Then
            kilat = "12 K"
        End If
        If linea = "5" Then
            kilat = "24 K"
        End If
        Return kilat
    End Function

    Private Sub txtPeso_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPeso.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtImporte.Focus()
        Else
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtImporte_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporte.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            BotonAgregar()
            'ActualizaDiasPrestamo()
        Else
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub txtImporte_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtImporte.KeyUp

    End Sub

    Private Sub txtImporte_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporte.Leave
        Dim x As Double
        If txtImporte.Text = "." Then
            txtImporte.Text = "0.00"
        End If
        If txtImporte.Text = "" Then
            txtImporte.Text = "0.00"
        Else
            x = Double.Parse(txtImporte.Text)
            txtImporte.Text = Format(x, "0.00###")
        End If
        'CHECAR PERMISOS********************************************************************

        checarPrestamo()
        'END CHEHAR PERMISOS ****************************************************************
    End Sub


    Private Sub cmbClasificacion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbClasificacion.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtDescripcion.Focus()
        End If
    End Sub

    Private Sub txtDescripcion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescripcion.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then

            cmbTipoJoya.Focus()

        End If
    End Sub

    Private Sub cmbKilates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbKilates.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtPeso.Focus()
        End If
    End Sub

    Private Sub cmbTipoJoya_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbTipoJoya.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            If tipoJoya = "Oro" Then
                cmbKilates.Focus()
            Else
                txtPeso.Focus()
            End If
        End If
    End Sub

    Private Sub cmbCaja_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbCaja.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtSerie.Focus()
        End If
    End Sub

    Private Sub txtSerie_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerie.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtFolio.Focus()
        End If
    End Sub

    Private Sub txtFolio_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFolio.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub cmbTipoJoya_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipoJoya.SelectedIndexChanged
        If cmbTipoJoya.SelectedIndex = 0 Then
            tipoJoya = "Oro"
            cmbKilates.Enabled = True
            calcularEvaluo()

            checarPrestamo()
        Else
            tipoJoya = "Plata"
            cmbKilates.Enabled = False
            calcularEvaluoPlata()
            txtPeso.Focus()
            checarPrestamo()
        End If
    End Sub
    Private Sub checarPrestamo()
        Dim x As Double
        x = Double.Parse(txtImporte.Text)
        Dim porcentaje As Double
        If tipoJoya = "Oro" Then
            porcentaje = (Double.Parse(txtEvaluo.Text) * Co.aumentoPrenda) / 100
            porcentaje = porcentaje + Double.Parse(txtEvaluo.Text)
            If x > porcentaje Then
                txtImporte.Text = porcentaje.ToString("0.00")
                If MsgBox("El Importe no puede ser más alto de " + Co.aumentoPrenda.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo.", MsgBoxStyle.OkOnly, "Pull System Soft") = MsgBoxResult.Ok Then
                    txtImporte.Focus()
                End If
            End If
        Else
            porcentaje = (Double.Parse(txtEvaluo.Text) * Co.aumentoPlata) / 100
            porcentaje = porcentaje + Double.Parse(txtEvaluo.Text)
            If x > porcentaje Then
                txtImporte.Text = porcentaje.ToString("0.00")
                If MsgBox("El Importe no puede ser más alto de " + Co.aumentoPlata.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo.", MsgBoxStyle.OkOnly, "Pull System Soft") = MsgBoxResult.Ok Then
                    txtImporte.Focus()
                End If


            End If
        End If

    End Sub


    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaClienteBoton2()
    End Sub

    Private Sub rdbJoyería_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbJoyería.CheckedChanged
        If rdbJoyería.Checked = True Then
            grpVEhiculos.Visible = False
            grpTerrenos.Visible = False
            grpJoyas.Visible = True

            TipoEmpeño = 0
            If Estado <> Estados.Inicio Then
                ConsultaDetalles()
            End If
            TextBox1.Focus()
        End If
    End Sub

    Private Sub rdbVehiculos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbVehiculos.CheckedChanged
        If rdbVehiculos.Checked = True Then
            grpVEhiculos.Top = 178
            grpVEhiculos.Left = 10
            grpVEhiculos.Visible = True
            grpJoyas.Visible = False
            grpTerrenos.Visible = False
            TipoEmpeño = 1
            If Estado <> Estados.Inicio Then
                ConsultaDetallesv()
            End If
            TextBox1.Focus()
        End If
    End Sub

    Private Sub rdbTerrenos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTerrenos.CheckedChanged
        If rdbTerrenos.Checked = True Then
            grpTerrenos.Top = 178
            grpTerrenos.Left = 10
            grpTerrenos.Visible = True
            grpJoyas.Visible = False
            grpVEhiculos.Visible = False
            TipoEmpeño = 2
            If Estado <> Estados.Inicio Then
                ConsultaDetallest()
            End If
            TextBox1.Focus()
        End If
    End Sub

    Private Sub cmbClasificacionV_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbClasificacionV.KeyPress

        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtMarcav.Focus()
        End If
    End Sub

    Private Sub txtEvaluoV_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEvaluoV.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtImporteV.Focus()
        Else
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtMarcav_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMarcav.KeyPress

        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtModelo.Focus()
        End If
    End Sub

    Private Sub txtModelo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtModelo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtColor.Focus()
        End If
    End Sub

    Private Sub txtColor_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtColor.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtNoSerie.Focus()
        End If
    End Sub

    Private Sub txtNoSerie_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNoSerie.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtPlacasV.Focus()
        End If
    End Sub

    Private Sub txtPlacasV_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPlacasV.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtEvaluoV.Focus()
        End If
    End Sub

    Private Sub txtImporteV_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporteV.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            BotonAgregarV()
        Else
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtImporteV_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporteV.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.00"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.00")
        End If
        checarPrestamoV()
    End Sub

    Private Sub txtEvaluoV_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEvaluoV.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.00"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.00")
        End If
        checarPrestamoV()
    End Sub

    Private Sub btnAgregarV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarV.Click

        BotonAgregarV()


    End Sub
    Private Sub BotonAgregarV()
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasAlta, PermisosN.Secciones.Empenios) = True Then
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                AgregaArticuloV()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub btnNuevoV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoV.Click
        NuevoConceptov()
    End Sub
    Private Sub checarPrestamoV()
        Dim x As Double
        x = Double.Parse(txtImporteV.Text)
        Dim porcentaje As Double
        'If tipoJoya = "Oro" Then
        '  Dim CD As New dbEmpeniosDetallesV(idMovimiento, MySqlcon)

        porcentaje = (Double.Parse(txtEvaluoV.Text) * Co.aumentoValorPrendaV) / 100
        porcentaje = porcentaje + Double.Parse(txtEvaluoV.Text)
        If x > porcentaje Then
            txtImporteV.Text = porcentaje.ToString("0.00")
            If MsgBox("El Importe no puede ser más alto de " + Co.aumentoValorPrendaV.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo.", MsgBoxStyle.OkOnly, "Pull System Soft") = MsgBoxResult.Ok Then
                txtImporteV.Focus()
            End If
        End If


    End Sub
    Private Sub AgregaArticuloV()
        Try
            Dim CD As New dbEmpeniosComprasDetallesV(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""


            If IsNumeric(txtImporteV.Text) = False Then
                MsgError += vbCrLf + "El importe debe ser un valor numérico."
                HayError = True
            Else
                If CInt(txtImporteV.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += " El importe debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If

            If IsNumeric(txtEvaluoV.Text) = False Then
                MsgError += vbCrLf + "El Evalúo debe ser un valor numérico."
                HayError = True
            Else
                'If CInt(txtEvaluoV.Text) <= 0 And My.Settings.conceptocero = False Then
                '    MsgError += " El Evalúo debe ser un valor mayor a 0."
                '    HayError = True
                'End If
            End If
            If cmbClasificacion.Text = "" Then
                MsgError += " Es necesario dar de alta clasificaciones."
                HayError = True
            End If


            If HayError = False Then

                If btnAgregarV.Text = "Agregar Concepto" Then

                    CD.Guardar(idMovimiento, IdsClasificacionv.Valor(cmbClasificacionV.SelectedIndex), txtMarcav.Text, txtModelo.Text, txtColor.Text, txtNoSerie.Text, txtPlacasV.Text, txtEvaluoV.Text, txtImporteV.Text)


                    'aqui me quede
                    ConsultaDetallesv()
                    NuevoConceptov()

                Else

                    CD.Modificar(idDetalle, idMovimiento, IdsClasificacionv.Valor(cmbClasificacionV.SelectedIndex), txtMarcav.Text, txtModelo.Text, txtColor.Text, txtNoSerie.Text, txtPlacasV.Text, txtEvaluoV.Text, txtImporteV.Text)




                    ConsultaDetallesv()
                    NuevoConceptov()

                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub cmbClasificacionT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbClasificacionT.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtDescripcionT.Focus()
        End If
    End Sub

    Private Sub txtDescripcionT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescripcionT.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtEvaluoT.Focus()
        End If
    End Sub

    Private Sub txtEvaluoT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEvaluoT.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtImporteT.Focus()
        Else
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtEvaluoT_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEvaluoT.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.00"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.00")
        End If
    End Sub

    Private Sub txtImporteT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporteT.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            BotonAgregarT()
        Else
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtImporteT_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporteT.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.00"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.00")
        End If
        checarPrestamoT()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim et As New frmVentasTextoExtra(txtDescripcionT.Text, 400, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtDescripcionT.Text = et.Texto
        End If
    End Sub

    Private Sub agregarConceptoT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles agregarConceptoT.Click
        BotonAgregarT()
    End Sub
    Private Sub BotonAgregarT()
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasAlta, PermisosN.Secciones.Empenios) = True Then
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                AgregaArticuloT()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub nuevoConceptoT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nuevoConceptoT.Click
        nuevoConceptoTe()
    End Sub
    Private Sub AgregaArticuloT()
        Try
            Dim CD As New dbEmpeniosComprasDetallest(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""


            If IsNumeric(txtImporteT.Text) = False Then
                MsgError += vbCrLf + "El importe debe ser un valor numérico."
                HayError = True
            Else
                If CInt(txtImporteT.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += " El importe debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If

            If IsNumeric(txtEvaluoT.Text) = False Then
                MsgError += vbCrLf + "El Evalúo debe ser un valor numérico."
                HayError = True
            Else
                'If CInt(txtEvaluoT.Text) <= 0 And My.Settings.conceptocero = False Then
                '    MsgError += " El Evalúo debe ser un valor mayor a 0."
                '    HayError = True
                'End If
            End If
            If cmbClasificacion.Text = "" Then
                MsgError += " Es necesario dar de alta clasificaciones."
                HayError = True
            End If
            If txtDescripcionT.Text = "" Then
                MsgError += " Es necesario dar una descripción."
                HayError = True
            End If

            If HayError = False Then

                If agregarConceptoT.Text = "Agregar Concepto" Then

                    CD.Guardar(idMovimiento, IdsClasificaciont.Valor(cmbClasificacionT.SelectedIndex), txtDescripcionT.Text, txtEvaluoT.Text, txtImporteT.Text)


                    'aqui me quede
                    ConsultaDetallest()
                    nuevoConceptoTe()

                Else

                    CD.Modificar(idDetalle, idMovimiento, IdsClasificaciont.Valor(cmbClasificacionT.SelectedIndex), txtDescripcionT.Text, txtEvaluoT.Text, txtImporteT.Text)




                    ConsultaDetallest()
                    nuevoConceptoTe()

                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub checarPrestamoT()
        Dim x As Double
        x = Double.Parse(txtImporteT.Text)
        Dim porcentaje As Double
        'If tipoJoya = "Oro" Then
        '  Dim CD As New dbEmpeniosDetallesV(idMovimiento, MySqlcon)

        porcentaje = (Double.Parse(txtEvaluoT.Text) * Co.aumentoValorPrendaT) / 100
        porcentaje = porcentaje + Double.Parse(txtEvaluoT.Text)
        If x > porcentaje Then
            txtImporteT.Text = porcentaje.ToString("0.00")
            If MsgBox("El Importe no puede ser más alto de " + Co.aumentoValorPrendaT.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo.", MsgBoxStyle.OkOnly, "Pull System Soft") = MsgBoxResult.Ok Then
                txtImporteT.Focus()
            End If
        End If


    End Sub
    Private Sub ConsultaDetallest()
        Try

            Tabla.Rows.Clear()
            Dim T As DataTable
            Dim CD As New dbEmpeniosComprasDetallest(MySqlcon)
            T = CD.ConsultaReader(idMovimiento).ToTable()


            DGDetalles.DataSource = T
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).Visible = False
            DGDetalles.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'DGDetalles.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable

            DGDetalles.Columns(2).HeaderText = "Clasificación"
            DGDetalles.Columns(3).HeaderText = "Descripción"
            DGDetalles.Columns(4).HeaderText = "Evalúo"
            DGDetalles.Columns(5).HeaderText = "Importe"
            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            ConsultaOn = True
            SacaTotalT()
            ConsultaOn = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub SacaTotalT()
        Try
            If ConsultaOn Then
                Dim V As New dbEmpeniosComprasDetallest(MySqlcon)
                V.DaTotal(idMovimiento)
                If TotalNuevo = -1 Then
                    Label14.Text = V.TotalVenta.ToString("C2")
                    total = V.TotalVenta
                Else
                    Label14.Text = TotalNuevo.ToString("C2")
                    total = TotalNuevo
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub nuevoConceptoTe()
        'IdInventario = 0
        'IdVariante = 0
        txtDescripcionT.Text = ""
        txtEvaluoT.Text = "0.00"
        txtImporteT.Text = "0.00"
        agregarConceptoT.Text = "Agregar Concepto"
        txtDescripcionT.Focus()
        agregarConceptoT.Enabled = True
        grpTerrenos.Enabled = True
        Button9.Enabled = False
    End Sub
    Private Sub Imprimir(Optional ByVal pSalir As Boolean = False)
        Dim em As New dbEmpeniosComprasDetalles(MySqlcon)
        Dim c As New dbEmpeniosCompras(idMovimiento, MySqlcon)
        Dim emv As New dbEmpeniosComprasDetallesV(MySqlcon)
        Dim emt As New dbEmpeniosComprasDetallest(MySqlcon)
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Rep = New repEmpeniosCompras()

        If TipoEmpeño = 0 Then

            Rep.SetDataSource(em.impresionRecibo(idMovimiento))

        End If
        If TipoEmpeño = 1 Then
            Rep.SetDataSource(emv.impresionRecibo(idMovimiento))
        End If
        If TipoEmpeño = 2 Then
            Rep.SetDataSource(emt.impresionRecibo(idMovimiento))
        End If
        Rep.SetParameterValue("fechaPago", c.Fecha)
        Rep.SetParameterValue("clienteNombre", c.Cliente.Nombre)
        Rep.SetParameterValue("clienteRFC", c.Cliente.RFC)
        Rep.SetParameterValue("tipoPago", c.formaPago)
        Rep.SetParameterValue("cantidadNumero", c.Total.ToString("C2"))
        Rep.SetParameterValue("folio", txtSerie.Text + txtFolio.Text)
        Rep.SetParameterValue("Encabezado", ComboBox3.Text)
        Dim CL As New CLetras
        If c.Total >= 0 Then
            Rep.SetParameterValue("cantidadLetra", "SON: " + CL.LetrasM(c.Total, 2, GlobalIdiomaLetras))
        Else
            Rep.SetParameterValue("cantidadLetra", "SON: MENOS " + CL.LetrasM(c.Total * -1, 2, GlobalIdiomaLetras))
        End If
        'Rep.PrintOptions.PrinterName = My.Settings.impresoraempenios
        '  Rep.PrintToPrinter(1, False, 0, 0)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
        If pSalir Then Me.Close()
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox19_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtImporteT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporteT.TextChanged

    End Sub

    Private Sub txtImporte_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporte.TextChanged

    End Sub

    Private Sub grpJoyas_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpJoyas.Enter

    End Sub

    Private Sub txtEvaluo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEvaluo.TextChanged

    End Sub

    Private Sub txtEvaluoT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEvaluoT.TextChanged

    End Sub

    Private Sub txtDescripcionT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescripcionT.TextChanged

    End Sub

    Private Sub txtDescripcion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescripcion.TextChanged

    End Sub

    Private Sub txtMarcav_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMarcav.TextChanged

    End Sub

    Private Sub txtModelo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtModelo.TextChanged

    End Sub

    Private Sub txtColor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtColor.TextChanged

    End Sub

    Private Sub txtNoSerie_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoSerie.TextChanged

    End Sub

    Private Sub txtPlacasV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlacasV.TextChanged

    End Sub

    Private Sub txtComentario_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComentario.TextChanged

    End Sub

    Private Sub cmbClasificacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClasificacion.SelectedIndexChanged
        txtDescripcion.Text = cmbClasificacion.Text
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If grpJoyas.Visible Then
                cmbClasificacion.Focus()
            End If
            If grpTerrenos.Visible Then
                cmbClasificacionT.Focus()
            End If
            If grpVEhiculos.Visible Then
                cmbClasificacionV.Focus()
            End If
        End If
    End Sub
End Class