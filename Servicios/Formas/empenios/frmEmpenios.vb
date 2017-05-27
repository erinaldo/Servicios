Public Class frmEmpenios
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
    Private Sub frmEmpenios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = Me.Text + " " + GlobalUsuario
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

        TipoEmpeño = 0
    End Sub
    Private Sub nuevo()
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        cmbCaja.SelectedIndex = IdsCajas.Busca(IdCajaDefault)
        Estado = Estados.Inicio
        Button1.Enabled = False
        Button14.Enabled = False
        ' Button13.Enabled = False
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
        'TextBox13.Text = ""
        TextBox1.Enabled = True
        TextBox7.Enabled = True
        'TextBox13.Enabled = True
        Label12.Text = ""
        ComboBox4.SelectedIndex = 0
        '----
        'ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        'ComboBox6.SelectedIndex = IdsCajas.Busca(IdCajaDefault)
        cmbCaja.Enabled = False
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
        nuevoConceptoTe()
        'grpVEhiculos.Enabled = True
        nuevoFolio()

        idMovimiento = 0
        Label24.Visible = False
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        ' Button13.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False

        txtFolio.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        grpJoyas.Enabled = True
        Panel2.Enabled = True
        Label14.Text = 0.ToString("C2")

        NuevoConcepto()
        nuevoConceptoTe()
        NuevoConceptov()
        Dim usuario As New dbUsuarios(GlobalIdUsuario, MySqlcon)
        cmbVendedor.SelectedIndex = IdsVendedores.Busca(usuario.IdVendedor)
        lblTotalAux.Visible = False
        lblTotalAux2.Visible = False
        TextBox1.Focus()
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosCambiarSucursal, PermisosN.Secciones.Empenios) = False Then
            ComboBox3.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosPermitirCambiarVendedor, PermisosN.Secciones.Empenios) = False Then
            cmbVendedor.Enabled = False
            btnHabilita.Visible = True
        Else
            btnHabilita.Visible = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.PermitirCambioFecha, PermisosN.Secciones.Empenios) = False Then
            dtpFecha.Enabled = False
            
        End If
    End Sub
    Private Sub nuevoFolio()
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        If Co.folio = 0 Then
            Dim anio As String = Date.Now.Year.ToString.Chars(2) + Date.Now.Year.ToString.Chars(3)
            txtSerie.Text = Date.Now.Day.ToString("00") + Date.Now.Month.ToString("00") + anio
            Dim V As New dbEmpenios(MySqlcon)
            txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
        Else
            If Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.MovimientosCaja, 0) Then
                txtSerie.Text = Sf.Serie
            Else
                txtSerie.Text = ""
            End If

            Dim V As New dbEmpenios(MySqlcon)
            txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
        End If
    End Sub

    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbCaja.Focus()
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
            Dim V As New dbEmpenios(MySqlcon)
            txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
        Else
            If Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.MovimientosCaja, 0) Then
                txtSerie.Text = Sf.Serie
            Else
                txtSerie.Text = ""
            End If

            Dim V As New dbEmpenios(MySqlcon)
            txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
        End If

    End Sub

    Private Sub frmEmpenios_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este movimiento no ha sido guardado. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbEmpenios(MySqlcon)
                c.Eliminar(idMovimiento)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub BotonAgregar()
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosAlta, PermisosN.Secciones.Empenios) = True Then
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                AgregaArticulo()
            End If
            ActualizaDiasPrestamo()
        Else
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub Guardar()
        Try
            If idCliente <> 0 Then
                Dim errores = False
                Dim C As New dbEmpenios(MySqlcon)
                txtFolio.BackColor = Color.White
                Label17.Visible = False

                If C.ChecaFolioRepetido(CInt(txtFolio.Text), txtSerie.Text) Then
                     nuevoFolio()
                End If
                If errores Then
                    MessageBox.Show("Folio repetido", "Pull System Soft", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    C.Guardar(idCliente, Format(dtpFecha.Value, "yyyy/MM/dd"), txtFolio.Text, total, IdsSucursales.Valor(ComboBox3.SelectedIndex), txtSerie.Text, IdsCajas.Valor(cmbCaja.SelectedIndex), IdsVendedores.Valor(cmbVendedor.SelectedIndex), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), txtComentario.Text, Integer.Parse(txtDiasPrestamo.Text), IdsForma.Valor(ComboBox4.SelectedIndex), TipoEmpeño)
                    idMovimiento = C.ID
                    Estado = 1
                    txtFolio.Enabled = False
                    txtSerie.Enabled = False
                    Button1.Enabled = True
                    Button14.Enabled = True
                    Button21.Enabled = True
                    Button1.Enabled = True
                    Button14.Enabled = True
                    '  Button13.Enabled = True
                    ' Button2.Enabled = True
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
            Dim CD As New dbEmpeniosDetalles(MySqlcon)
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
                If CInt(txtEvaluo.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += " El Evalúo debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If
            If cmbClasificacion.Text = "" Then
                MsgError += " Es necesario dar de alta clasificacines."
                HayError = True
            End If
            If IsNumeric(txtPeso.Text) = False Then
                MsgError += vbCrLf + "El Peso debe ser un valor numérico."
                HayError = True

            End If
            Dim str As String
            str = checarPrestamo()
           
            If CDbl(txtEvaluo.Text) < CDbl(txtImporte.Text) And str = "" Then
                If MsgBox("¿Autorizar Sobrevaluo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim frmAuto As New frmCambioUsuario(1, 1)
                    If frmAuto.ShowDialog <> Windows.Forms.DialogResult.OK Then
                        MsgError += vbCrLf + " Sin Autorización para realizar este movimiento."
                        HayError = True
                    End If
                Else
                    MsgError += vbCrLf + " No se puede prestar arriba del evalúo."
                    HayError = True
                End If
            End If

            If CDbl(txtEvaluo.Text) < CDbl(txtImporte.Text) And str <> "" Then
                If MsgBox("¿Autorizar Sobrevaluo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim frmAuto As New frmCambioUsuario(1, 2)
                    If frmAuto.ShowDialog <> Windows.Forms.DialogResult.OK Then
                        MsgError += vbCrLf + " Sin Autorización para realizar este movimiento."
                        HayError = True
                    End If
                Else
                    MsgError += vbCrLf + str
                    HayError = True
                End If
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
            Dim CD As New dbEmpeniosDetalles(MySqlcon)
            T = CD.ConsultaReader(idMovimiento).ToTable()


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
            Dim CD As New dbEmpeniosDetallesV(MySqlcon)
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
                Dim V As New dbEmpeniosDetalles(MySqlcon)
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
                Dim V As New dbEmpeniosDetallesV(MySqlcon)
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
    Private Sub SacaTotalT()
        Try
            If ConsultaOn Then
                Dim V As New dbEmpeniosDetallesT(MySqlcon)
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
        cmbKilates.SelectedIndex = 1
        txtPeso.Text = "0.00"
        txtEvaluo.Text = "0.00"
        txtDescripcion.Text = cmbClasificacion.Text
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
   

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                If TipoEmpeño = 0 Then
                    Dim CD As New dbEmpeniosDetalles(MySqlcon)
                    CD.Eliminar(idDetalle)
                    ConsultaDetalles()
                    NuevoConcepto()
                End If
                If TipoEmpeño = 1 Then
                    Dim CD As New dbEmpeniosDetallesV(MySqlcon)
                    CD.Eliminar(idDetalle)
                    ConsultaDetallesv()
                    NuevoConceptov()
                End If
                If TipoEmpeño = 2 Then
                    Dim CD As New dbEmpeniosDetallesT(MySqlcon)
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
    Private Sub LlenaDatosDetallesA()
        Try

            idDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbEmpeniosDetalles(idDetalle, MySqlcon)

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
            Dim CD As New dbEmpeniosDetallesV(idDetalle, MySqlcon)

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
    Private Sub LlenaDatosDetallesT()
        Try

            idDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbEmpeniosDetallesT(idDetalle, MySqlcon)

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

    Private Sub frmEmpenios_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F10 Then
            If Button14.Enabled = True Then
                Modificar(Estados.Guardada)
            End If
        End If
        'If e.KeyCode = Keys.F9 Then
        '    If Button1.Enabled = True Then
        '        Modificar(Estados.Pendiente)
        '    End If
        'End If
        If e.KeyCode = Keys.F5 Then
            BotonNuevo()
        End If
    End Sub
    Private Sub Modificar(ByVal pEstado As Byte)
        Try
            Dim MensajeError As String = ""
            Dim C As New dbEmpenios(MySqlcon)
            Dim CD As New dbEmpeniosDetalles(MySqlcon)


            If IsNumeric(txtFolio.Text) = False Then MensajeError = "El folio debe ser un valor numérico."
            If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosAlta, PermisosN.Secciones.Empenios) = False And pEstado <> Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosCancelar, PermisosN.Secciones.Empenios) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If pEstado = Estados.Guardada Then
                If C.ChecaFolioRepetido(CInt(txtFolio.Text), txtSerie.Text) Then
                    nuevoFolio()
                End If
            End If
            If DGDetalles.RowCount <= 0 Then
                MensajeError += " No se puede guardar un empeño sin conceptos."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.PermitirCambioFecha, PermisosN.Secciones.Empenios) = False And dtpFecha.Value.ToString("yyyy/MM/dd") < C.DaUltimaFecha() Then
                MensajeError += " Fecha incorrecta. Revise que la fecha de su computadora este correcta."
            End If
            If MensajeError = "" Then
                'If C.ChecaFolioRepetido(txtFolio.Text, txtSerie.Text) Then
                '    If pEstado = Estados.Guardada Then txtFolio.Text = C.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
                'End If


                CD.DaTotal(idMovimiento)
                C.Modificar(idMovimiento, idCliente, Format(dtpFecha.Value, "yyyy/MM/dd"), txtFolio.Text, total, IdsSucursales.Valor(ComboBox3.SelectedIndex), txtSerie.Text, IdsCajas.Valor(cmbCaja.SelectedIndex), IdsVendedores.Valor(cmbVendedor.SelectedIndex), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), txtComentario.Text, pEstado, Integer.Parse(txtDiasPrestamo.Text), IdsForma.Valor(ComboBox4.SelectedIndex), TipoEmpeño)
                If pEstado = Estados.Guardada Then ActualizarConfig()
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
                    'Button13.Enabled = True
                    Button21.Enabled = True
                    ActualizarConfig()
                    ActualizaDiasPrestamo()
                    Imprimir()
                End If

                If pEstado = Estados.Cancelada Then
                    Button21.Enabled = False
                    ' Button13.Enabled = False
                    Button2.Enabled = False
                    ActualizarConfig()
                    ActualizaDiasPrestamo()
                    Imprimir()
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
                    Button2.Enabled = False
                    Button4.Enabled = False
                    Button8.Enabled = False
                End If
                If pEstado = Estados.Pendiente Then
                    ActualizaDiasPrestamo()
                End If
                nuevo()
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
                Dim c As New dbEmpenios(MySqlcon)
                c.Eliminar(idMovimiento)
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

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        BotonNuevo()
        Dim Sf As New dbSucursalesFolios(MySqlcon)

        Dim anio As String = Date.Now.Year.ToString.Chars(2) + Date.Now.Year.ToString.Chars(3)
        txtSerie.Text = Date.Now.Day.ToString("00") + Date.Now.Month.ToString("00") + anio
        Dim V As New dbEmpenios(MySqlcon)
        txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
        

    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(txtComentario.Text, 2000, True)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtComentario.Text = et.Texto
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbEmpenios(MySqlcon)
                V.ActualizaComentario(idMovimiento, txtComentario.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Modificar(Estados.Pendiente)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim em As New dbEmpenios(MySqlcon)
        If em.TienePagos(idMovimiento) = False Then
            If MsgBox("¿Cancelar este movimiento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Modificar(Estados.Cancelada)
            End If
        Else
            MsgBox("Este empeño tiene pagos. Cancele primero los pagos para poder cancelar el empeño.", MsgBoxStyle.Information, GlobalNombreApp)
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar este empeño?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosAlta, PermisosN.Secciones.Empenios) = True Then
                Dim C As New dbEmpenios(MySqlcon)
                C.Eliminar(idMovimiento)
                PopUp("Empeño Eliminado", 90)
                nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este empeño no ha sido guardado. ¿Desea continuar? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbEmpenios(MySqlcon)
                c.Eliminar(idMovimiento)
                nuevo()
                nuevoConceptoTe()
                NuevoConceptov()
            Else
                Exit Sub
            End If
        End If
        Dim f As New frmEmpeniosConsulta(ModosDeBusqueda.Secundario)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idMovimiento = f.IdCompra
            LlenaDatosVenta()
        End If
        f.Dispose()
    End Sub
    Private Sub LlenaDatosVenta() 'aqui me quede
        Dim C As New dbEmpenios(idMovimiento, MySqlcon)
        Estado = C.Estado
        If fechaNueva = "" Then
            dtpFecha.Value = C.fechaContrato
        Else
            dtpFecha.Value = fechaNueva
        End If
        If C.renovado > 0 Then
            lblTotalAux.Visible = True
            lblTotalAux2.Visible = True
        Else
            lblTotalAux.Visible = False
            lblTotalAux2.Visible = False
        End If
        lblTotalAux.Text = C.TotalAux.ToString("C2")
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        txtFolio.Text = C.Folio.ToString
        FolioAnt = C.Folio
        txtSerie.Text = C.Serie
        'TextBox1.Text = C.Cliente.Clave

        TextBox1.Text = C.Cliente.Clave
        txtDiasPrestamo.Text = C.diasPrestamo
        BuscaClienteBoton2()

        '  Button2.Enabled = True
        txtComentario.Text = C.Comentario
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
                'cmbCaja.Enabled = False
                'Button13.Enabled = False
                grpJoyas.Enabled = False
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
                'TextBox13.Enabled = False
                dtpFecha.Enabled = False
                ComboBox3.Enabled = False
                cmbVendedor.Enabled = False
                Button5.Enabled = False
                Button19.Enabled = False
                txtDiasPrestamo.Enabled = False
                btnHabilita.Visible = False
                Button2.Enabled = False
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                ' Button13.Enabled = True
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                grpJoyas.Enabled = False
                grpTerrenos.Enabled = False
                Panel2.Enabled = False
                grpVEhiculos.Enabled = False
                'cmbCaja.Enabled = False
                Button4.Enabled = False
                Button8.Enabled = False
                Button21.Enabled = True
                txtSerie.Enabled = False
                txtFolio.Enabled = False
                TextBox1.Enabled = False
                TextBox7.Enabled = False
                'TextBox13.Enabled = False
                dtpFecha.Enabled = False
                ComboBox3.Enabled = False
                cmbVendedor.Enabled = False
                Button5.Enabled = False
                Button19.Enabled = False
                btnHabilita.Visible = False
                txtDiasPrestamo.Enabled = False
                Button2.Enabled = False
            Case Else
                Label24.Visible = False
                ' Button13.Enabled = True
                grpJoyas.Enabled = True
                Panel2.Enabled = True
                grpVEhiculos.Enabled = True
                grpTerrenos.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                'cmbCaja.Enabled = True
                Button4.Enabled = True
                Button8.Enabled = True
                Button2.Enabled = True
                Enabled = True
                txtSerie.Enabled = True
                txtFolio.Enabled = True
                TextBox1.Enabled = True
                TextBox7.Enabled = True
                'TextBox13.Enabled = True
                dtpFecha.Enabled = True
                ComboBox3.Enabled = False
                nuevoFolio()
                If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosPermitirCambiarVendedor, PermisosN.Secciones.Empenios) = False Then
                    cmbVendedor.Enabled = False
                    btnHabilita.Visible = True
                Else
                    cmbVendedor.Enabled = True
                    btnHabilita.Visible = False
                End If
                Button5.Enabled = True
                Button19.Enabled = True
                txtDiasPrestamo.Enabled = True
        End Select


    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If Estado = Estados.Guardada Then
            'MsgBox("El recibo no está guardado.", MsgBoxStyle.OkOnly, GlobalNombreApp)
            'Else
            Imprimir()
        Else
            MsgBox("Solo se pueden imprimir empeños guardados.", MsgBoxStyle.Information, GlobalNombreApp)
        End If


    End Sub
#Region "Imprimir"
    Private Sub Imprimir(Optional ByVal pSalir As Boolean = False)
        Dim en As New Encriptador

        Dim C As New dbEmpenios(idMovimiento, MySqlcon)
        Dim RutaPDF As String

        Dim Archivos As New dbSucursalesArchivos

        RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Date.Now.Year.ToString + "\") 'Modificado
        IO.Directory.CreateDirectory(RutaPDF + "\" + Date.Now.Year.ToString + "\" + Date.Now.Month.ToString("00") + "\") 'Modificado
        RutaPDF = RutaPDF + "\" + Date.Now.Year.ToString + "\" + Date.Now.Month.ToString("00") 'Modificado

        PrintDocument1.DocumentName = "Empeño Recibo - " + C.Serie + C.Folio.ToString + " " + C.Cliente.Nombre    'Modificado
        ' End If

        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.Empenios) 'Modificado
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
        If TipoEmpeño = 0 Then
            LlenaNodosImpresion()
        End If
        If TipoEmpeño = 1 Then
            LlenaNodosImpresionV()
        End If
        If TipoEmpeño = 2 Then
            LlenaNodosImpresionT()

        End If


        If TipoImpresora = 0 Then
            LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.Empenios) 'Modificado
        Else
            LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.Empenios + 1000) 'Modificado
        End If
        PrintDocument1.Print()
        If My.Settings.empenioscantimpresion = 2 Then


            PrintDocument1.DocumentName = "Empeño Recibo - " + C.Serie + C.Folio.ToString + " " + C.Cliente.Nombre + "- COPIA"    'Modificado
            PrintDocument1.PrinterSettings.PrinterName = Impresora
            If TipoEmpeño = 0 Then
                LlenaNodosImpresion()
            End If
            If TipoEmpeño = 1 Then
                LlenaNodosImpresionV()
            End If
            If TipoEmpeño = 2 Then
                LlenaNodosImpresionT()
            End If
            If TipoImpresora = 0 Then
                LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.Empenios) 'Modificado
            Else
                LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.Empenios + 1000) 'Modificado
            End If
            PrintDocument1.Print()
        End If
        If pSalir Then Me.Close()
    End Sub
    Private Sub LlenaNodosImpresion()

        Dim C As New dbEmpenios(idMovimiento, MySqlcon)
        Dim Cd As New dbEmpeniosDetalles(idMovimiento, MySqlcon)
        Dim Ec As New dbEmpeniosConfiguracion(MySqlcon)
        'Ec.LlenaDatos()
        ' Dim P As New dbPagosProveedores(MySqlcon) 'Modificado
        Dim O As New dbOpciones(MySqlcon)
        Dim TotalDescuento As Double = 0
        Dim tabla As DataTable
        Dim cantidadLetra As String 'modificado
        ImpND.Clear()
        ImpNDD.Clear()
        'Dim cantidad1 As Double
        'Dim centavos As Integer
        Dim plazo As String = ""
        Dim fechita As Date
        Dim aux As Double
        Dim evaluo As Double = 0
        Dim Cl As New CLetras
        Dim dec As Decimal
        fechita = C.Fecha.ToString
        If TotalNuevo = -1 Then
            'cantidad1 = Int(Double.Parse(C.Total))
            'centavos = Integer.Parse((Double.Parse(C.Total) - Double.Parse(cantidad1)) * 100)
            If C.Total >= 0 Then
                cantidadLetra = Cl.LetrasM(C.Total, 2, GlobalIdiomaLetras) 'Num2Text(Double.Parse(Double.Parse(cantidad1))) + " Y " + Format(centavos, "00") + "/100  PESOS M.N."
            Else
                cantidadLetra = "MENOS " + Cl.LetrasM(C.Total * -1, 2, GlobalIdiomaLetras) 'Num2Text(Double.Parse(Double.Parse(cantidad1))) + " Y " + Format(centavos, "00") + "/100  PESOS M.N."
            End If
        Else
            'cantidad1 = Int(Double.Parse(C.Total))
            'Dim x As Double = Double.Parse((TotalNuevo - Double.Parse(cantidad1)).ToString("0.00")) * 100

            'centavos = Integer.Parse(x)
            If TotalNuevo >= 0 Then
                cantidadLetra = Cl.LetrasM(TotalNuevo, 2, GlobalIdiomaLetras) 'Num2Text(Double.Parse(Double.Parse(cantidad1))) + " Y " + Format(centavos, "00") + "/100  PESOS M.N."
            Else
                cantidadLetra = "MENOS " + Cl.LetrasM(TotalNuevo * -1, 2, GlobalIdiomaLetras) 'Num2Text(Double.Parse(Double.Parse(cantidad1))) + " Y " + Format(centavos, "00") + "/100  PESOS M.N."
            End If
        End If
        Dim Sucursal As New dbSucursales(C.IdSucursal, MySqlcon)

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
        ImpND.Add(New NodoImpresionN("", "comentario", C.Comentario, 0), "comentario")

        ImpND.Add(New NodoImpresionN("", "docFormaPago", C.formaPago, 0), "docFormaPago")
        ImpND.Add(New NodoImpresionN("", "docFolio", Format(C.Folio, "000"), 0), "docFolio")
        ImpND.Add(New NodoImpresionN("", "serie", C.Serie, 0), "serie")
        If fechaNueva = "" Then
            ImpND.Add(New NodoImpresionN("", "docFecha", C.Fecha.ToString, 0), "docFecha")
        Else
            ImpND.Add(New NodoImpresionN("", "docFecha", fechaNueva, 0), "docFecha")
        End If

        ImpND.Add(New NodoImpresionN("", "clienteNombre", C.Cliente.Nombre.ToString, 0), "clienteNombre") 'falta cnvertir a letra
        ImpND.Add(New NodoImpresionN("", "clienteDireccion", C.Cliente.Direccion.ToString + " " + C.Cliente.Colonia + " " + C.Cliente.Ciudad, 0), "clienteDireccion")
        ImpND.Add(New NodoImpresionN("", "clienteNumero", C.Cliente.Clave, 0), "clienteNumero")
        ImpND.Add(New NodoImpresionN("", "docEvaluador", C.buscaVendedor.ToString, 0), "docEvaluador")
        If TotalNuevo = -1 Then
            ImpND.Add(New NodoImpresionN("", "docPrestamoNumero", C.Total.ToString("C2"), 0), "docPrestamoNumero")
        Else
            ImpND.Add(New NodoImpresionN("", "docPrestamoNumero", TotalNuevo.ToString("C2"), 0), "docPrestamoNumero")
        End If
        If fechaNueva = "" Then
            ImpND.Add(New NodoImpresionN("", "docFechaVen", Format(DateAdd(DateInterval.Day, 120, fechita), "dd/MM/yyyy"), 0), "docFechaVen")
            ImpND.Add(New NodoImpresionN("", "docPlazoDesempenio", Format(DateAdd(DateInterval.Day, 120, fechita), "dd/MM/yyyy"), 0), "docPlazoDesempenio")
            ImpND.Add(New NodoImpresionN("", "docFechaLimiteFini", Format(DateAdd(DateInterval.Day, 120 + Co.diasProrroga, fechita), "dd/MM/yyyy"), 0), "docFechaLimiteFini")
            ImpND.Add(New NodoImpresionN("", "docInicioComerciali", Format(DateAdd(DateInterval.Day, 121 + Co.diasProrroga, fechita), "dd/MM/yyyy"), 0), "docInicioComerciali")
        Else
            ImpND.Add(New NodoImpresionN("", "docFechaVen", Format(DateAdd(DateInterval.Day, 120, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docFechaVen")
            ImpND.Add(New NodoImpresionN("", "docPlazoDesempenio", Format(DateAdd(DateInterval.Day, 120, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docPlazoDesempenio")
            ImpND.Add(New NodoImpresionN("", "docFechaLimiteFini", Format(DateAdd(DateInterval.Day, 120 + Co.diasProrroga, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docFechaLimiteFini")
            ImpND.Add(New NodoImpresionN("", "docInicioComerciali", Format(DateAdd(DateInterval.Day, 121 + Co.diasProrroga, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docInicioComerciali")
        End If

        ImpND.Add(New NodoImpresionN("", "docPrestamo", cantidadLetra, 0), "docPrestamo")
        '  ImpND.Add(New NodoImpresionN("", "docEvaluador", C.buscaVendedor.ToString, 0), "docEvaluador")
        aux = C.TotalAux * ((Co.interes + Co.almacenaje) / 100)
        ImpND.Add(New NodoImpresionN("", "docInteresdoc", "$" + (aux).ToString("0.00"), 0), "docInteresdoc")
        '  ImpND.Add(New NodoImpresionN("", "docAlmacenajedoc", "00", 0), "docAlmacenajedoc")
        ' ImpND.Add(New NodoImpresionN("", "docGasto", "00", 0), "docGasto")




        'Poliza
        Dim Cont As Integer = 0
        Dim kilat As String

        CuantosRenglones = 0
        tabla = Cd.ConsultaReader(idMovimiento).ToTable
        ' kilat = devuelveKilate(tabla.Rows(0)(2).ToString())


        If tabla.Rows.Count > 0 Then kilat = tabla.Rows(0)(2).ToString()


        For i As Integer = 0 To tabla.Rows.Count - 1
            ImpNDD.Add(New NodoImpresionN("", "ArticuloDescripcionCompleta", tabla.Rows(i)(2).ToString() + "-" + tabla.Rows(i)(1).ToString() + " " + tabla.Rows(i)(4).ToString() + " gr. " + tabla.Rows(i)(3).ToString(), 0), "ArticuloDescripcionCompleta" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "ArticuloDescripcion", tabla.Rows(i)(2).ToString() + "-" + tabla.Rows(i)(1).ToString(), 0), "ArticuloDescripcion" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "ArticuloPeso", tabla.Rows(i)(4).ToString() + " gr.", 0), "ArticuloPeso" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "ArticuloKilates", tabla.Rows(i)(3).ToString(), 0), "ArticuloKilates" + Format(i, "000"))
            evaluo = evaluo + Double.Parse(tabla.Rows(i)(6).ToString).ToString()
            ImpNDD.Add(New NodoImpresionN("", "ArticuloClasificacionV", "", 0), "ArticuloClasificacionV" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "ArticuloMarca", "", 0), "ArticuloMarca" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "ArticuloModelo", "", 0), "ArticuloModelo" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "ArticuloColor", "", 0), "ArticuloColor" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "ArticuloNoSerie", "", 0), "ArticuloNoSerie" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "ArticuloPlacas", "", 0), "ArticuloPlacas" + Format(i, "000"))
            ' CuantosRenglones += 1
            ImpNDD.Add(New NodoImpresionN("", "ArticuloDescripcionT", "", 0), "ArticuloDescripcionT" + Format(i, "000"))

        Next
        CuantosRenglones = tabla.Rows.Count - 1
        ImpND.Add(New NodoImpresionN("", "ArticuloAvaluo", evaluo.ToString("C2"), 0), "ArticuloAvaluo")
        ' ImpND.Add(New NodoImpresionN("", "docPorcentajeAlmacenaje", Co.almacenaje.ToString, 0), "docPorcentajeAlmacenaje")
        ImpND.Add(New NodoImpresionN("", "docPorcentajeInteres", Format(Co.interes + Co.almacenaje, "0.00") + "%", 0), "docPorcentajeInteres")
        '  ImpND.Add(New NodoImpresionN("", "docPorcentajeAvaluo", Co.gasto.ToString, 0), "docPorcentajeAvaluo")
        ImpND.Add(New NodoImpresionN("", "docNumerodeRegistro", Co.numRegistro, 0), "docNumerodeRegistro")

        Dim interes As Double = 0
        Dim Almacenaje As Double = 0
        Dim desempenio As Double
        Dim refrendo As Double = 0

        interes = C.TotalAux * Co.interes * 30
        interes = interes / 100
        Almacenaje = C.TotalAux * Co.almacenaje * 30
        Almacenaje = Almacenaje / 100
        refrendo = interes + Almacenaje
        dec = Decimal.Parse(refrendo)
        refrendo = Double.Parse(Decimal.Round(dec, 0))
        desempenio = C.TotalAux + refrendo



        ImpND.Add(New NodoImpresionN("", "ArticuloPlazo30", "30 Días", 0), "ArticuloPlazo30")
        If fechaNueva = "" Then
            ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago30", Format(DateAdd(DateInterval.Day, 30, fechita), "dd/MM/yyyy"), 0), "ArticuloFechaPago30")
        Else
            ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago30", Format(DateAdd(DateInterval.Day, 30, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "ArticuloFechaPago30")
        End If

        ImpND.Add(New NodoImpresionN("", "ArticuloInteres30", interes.ToString("C2"), 0), "ArticuloInteres30")
        ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje30", Almacenaje.ToString("C2"), 0), "ArticuloAlmacenaje30")
        ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo30", refrendo.ToString("C2"), 0), "ArticuloRefrendo30")
        ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio30", desempenio.ToString("C2"), 0), "ArticuloDesempenio30")

        interes = interes + ((C.TotalAux * Co.A31a60 * 30) / 100)
        Almacenaje = Almacenaje + ((C.TotalAux * Co.B31a60 * 30) / 100)
        refrendo = interes + Almacenaje
        dec = Decimal.Parse(refrendo)
        refrendo = Double.Parse(Decimal.Round(dec, 0))
        desempenio = C.TotalAux + refrendo

        ImpND.Add(New NodoImpresionN("", "ArticuloPlazo60", "60 Días", 0), "ArticuloPlazo60")
        If fechaNueva = "" Then
            ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago60", Format(DateAdd(DateInterval.Day, 60, fechita), "dd/MM/yyyy"), 0), "ArticuloFechaPago60")
        Else
            ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago60", Format(DateAdd(DateInterval.Day, 60, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "ArticuloFechaPago60")
        End If

        ImpND.Add(New NodoImpresionN("", "ArticuloInteres60", interes.ToString("C2"), 0), "ArticuloInteres60")
        ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje60", Almacenaje.ToString("C2"), 0), "ArticuloAlmacenaje60")
        ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo60", refrendo.ToString("C2"), 0), "ArticuloRefrendo60")
        ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio60", desempenio.ToString("C2"), 0), "ArticuloDesempenio60")


        interes = interes + ((C.TotalAux * Co.A61a90 * 30) / 100)
        Almacenaje = Almacenaje + ((C.TotalAux * Co.B61a90 * 30) / 100)
        refrendo = interes + Almacenaje
        dec = Decimal.Parse(refrendo)
        refrendo = Double.Parse(Decimal.Round(dec, 0))
        desempenio = C.TotalAux + refrendo

        ImpND.Add(New NodoImpresionN("", "ArticuloPlazo90", "90 Días", 0), "ArticuloPlazo90")
        If fechaNueva = "" Then
            ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago90", Format(DateAdd(DateInterval.Day, 90, fechita), "dd/MM/yyyy"), 0), "ArticuloFechaPago90")
        Else
            ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago90", Format(DateAdd(DateInterval.Day, 90, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "ArticuloFechaPago90")
        End If

        ImpND.Add(New NodoImpresionN("", "ArticuloInteres90", interes.ToString("C2"), 0), "ArticuloInteres90")
        ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje90", Almacenaje.ToString("C2"), 0), "ArticuloAlmacenaje90")
        ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo90", refrendo.ToString("C2"), 0), "ArticuloRefrendo90")
        ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio90", desempenio.ToString("C2"), 0), "ArticuloDesempenio90")


        interes = interes + ((C.TotalAux * Co.A90mas * 30) / 100)
        Almacenaje = Almacenaje + ((C.TotalAux * Co.B90mas * 30) / 100)
        refrendo = interes + Almacenaje
        dec = Decimal.Parse(refrendo)
        refrendo = Double.Parse(Decimal.Round(dec, 0))
        desempenio = C.TotalAux + refrendo

        ImpND.Add(New NodoImpresionN("", "ArticuloPlazo120", "120 Días", 0), "ArticuloPlazo120")
        If fechaNueva = "" Then
            ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago120", Format(DateAdd(DateInterval.Day, 120, fechita), "dd/MM/yyyy"), 0), "ArticuloFechaPago120")
        Else
            ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago120", Format(DateAdd(DateInterval.Day, 120, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "ArticuloFechaPago120")
        End If

        ImpND.Add(New NodoImpresionN("", "ArticuloInteres120", interes.ToString("C2"), 0), "ArticuloInteres120")
        ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje120", Almacenaje.ToString("C2"), 0), "ArticuloAlmacenaje120")
        ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo120", refrendo.ToString("C2"), 0), "ArticuloRefrendo120")
        ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio120", desempenio.ToString("C2"), 0), "ArticuloDesempenio120")



        CuantosRenglones += 1
        Cont += 1
        'Next
        ' Next


        Posicion = 0
        NumeroPagina = 1
    End Sub
    Private Sub LlenaNodosImpresionV()

        Dim C As New dbEmpenios(idMovimiento, MySqlcon)
        Dim Cd As New dbEmpeniosDetallesV(idMovimiento, MySqlcon)
        ' Dim P As New dbPagosProveedores(MySqlcon) 'Modificado
        Dim O As New dbOpciones(MySqlcon)
        Dim TotalDescuento As Double = 0
        Dim tabla As DataTable
        Dim cantidadLetra As String 'modificado
        ImpND.Clear()
        ImpNDD.Clear()
        'Dim cantidad1 As Double
        'Dim centavos As Integer
        Dim plazo As String = ""
        Dim fechita As Date
        Dim aux As Double
        Dim evaluo As Double = 0
        Dim Cl As New CLetras
        Dim dec As Decimal
        fechita = C.Fecha.ToString
        If TotalNuevo = -1 Then
            'cantidad1 = Int(Double.Parse(C.Total))
            'centavos = Integer.Parse((Double.Parse(C.Total) - Double.Parse(cantidad1)) * 100)
            If C.Total >= 0 Then
                cantidadLetra = Cl.LetrasM(C.Total, 2, GlobalIdiomaLetras) 'Num2Text(Double.Parse(Double.Parse(cantidad1))) + " Y " + Format(centavos, "00") + "/100  PESOS M.N."
            Else
                cantidadLetra = "MENOS " + Cl.LetrasM(C.Total * -1, 2, GlobalIdiomaLetras)
            End If
        Else
            'cantidad1 = Int(Double.Parse(C.Total))
            'Dim x As Double = Double.Parse((TotalNuevo - Double.Parse(cantidad1)).ToString("0.00")) * 100

            'centavos = Integer.Parse(x)
            If TotalNuevo >= 0 Then
                cantidadLetra = Cl.LetrasM(TotalNuevo, 2, GlobalIdiomaLetras) 'Num2Text(Double.Parse(Double.Parse(cantidad1))) + " Y " + Format(centavos, "00") + "/100  PESOS M.N."
            Else
                cantidadLetra = "MENOS " + Cl.LetrasM(TotalNuevo * -1, 2, GlobalIdiomaLetras) 'Num2Text(Double.Parse(Double.Parse(cantidad1))) + " Y " + Format(centavos, "00") + "/100  PESOS M.N."
            End If
            End If
            Dim Sucursal As New dbSucursales(C.IdSucursal, MySqlcon)

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
            ImpND.Add(New NodoImpresionN("", "comentario", C.Comentario, 0), "comentario")
            ImpND.Add(New NodoImpresionN("", "docFormaPago", C.formaPago, 0), "docFormaPago")
            ImpND.Add(New NodoImpresionN("", "docFolio", Format(C.Folio, "000"), 0), "docFolio")
            ImpND.Add(New NodoImpresionN("", "serie", C.Serie, 0), "serie")
            If fechaNueva = "" Then
                ImpND.Add(New NodoImpresionN("", "docFecha", Date.Parse(C.Fecha.ToString).ToString("dd/MM/yyyy"), 0), "docFecha")
            Else
                ImpND.Add(New NodoImpresionN("", "docFecha", fechaNueva, 0), "docFecha")
            End If

            ImpND.Add(New NodoImpresionN("", "clienteNombre", C.Cliente.Nombre.ToString, 0), "clienteNombre") 'falta cnvertir a letra
            ImpND.Add(New NodoImpresionN("", "clienteDireccion", C.Cliente.Direccion.ToString + " " + C.Cliente.Colonia + " " + C.Cliente.Ciudad, 0), "clienteDireccion")
            ImpND.Add(New NodoImpresionN("", "clienteNumero", C.Cliente.Clave, 0), "clienteNumero")
            ImpND.Add(New NodoImpresionN("", "docEvaluador", C.buscaVendedor.ToString, 0), "docEvaluador")
            If TotalNuevo = -1 Then
                ImpND.Add(New NodoImpresionN("", "docPrestamoNumero", C.Total.ToString("C2"), 0), "docPrestamoNumero")
            Else
                ImpND.Add(New NodoImpresionN("", "docPrestamoNumero", TotalNuevo.ToString("C2"), 0), "docPrestamoNumero")
            End If
            If fechaNueva = "" Then
            ImpND.Add(New NodoImpresionN("", "docFechaVen", Format(DateAdd(DateInterval.Day, 60, fechita), "dd/MM/yyyy"), 0), "docFechaVen")
            ImpND.Add(New NodoImpresionN("", "docPlazoDesempenio", Format(DateAdd(DateInterval.Day, 60, fechita), "dd/MM/yyyy"), 0), "docPlazoDesempenio")
            ImpND.Add(New NodoImpresionN("", "docFechaLimiteFini", Format(DateAdd(DateInterval.Day, 60 + Co.diasProrrogaV, fechita), "dd/MM/yyyy"), 0), "docFechaLimiteFini")
            ImpND.Add(New NodoImpresionN("", "docInicioComerciali", Format(DateAdd(DateInterval.Day, 61 + Co.diasProrrogaV, fechita), "dd/MM/yyyy"), 0), "docInicioComerciali")
            Else
            ImpND.Add(New NodoImpresionN("", "docFechaVen", Format(DateAdd(DateInterval.Day, 120, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docFechaVen")
            ImpND.Add(New NodoImpresionN("", "docPlazoDesempenio", Format(DateAdd(DateInterval.Day, 120, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docPlazoDesempenio")
            ImpND.Add(New NodoImpresionN("", "docFechaLimiteFini", Format(DateAdd(DateInterval.Day, 120 + Co.diasProrrogaV, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docFechaLimiteFini")
            ImpND.Add(New NodoImpresionN("", "docInicioComerciali", Format(DateAdd(DateInterval.Day, 121 + Co.diasProrrogaV, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docInicioComerciali")
            End If

            ImpND.Add(New NodoImpresionN("", "docPrestamo", cantidadLetra, 0), "docPrestamo")
            '  ImpND.Add(New NodoImpresionN("", "docEvaluador", C.buscaVendedor.ToString, 0), "docEvaluador")
            aux = C.TotalAux * ((Co.interes1a15V + Co.almacenaje1a15V) / 100)
            ImpND.Add(New NodoImpresionN("", "docInteresdoc", "$" + (aux).ToString("0.00"), 0), "docInteresdoc")
            '  ImpND.Add(New NodoImpresionN("", "docAlmacenajedoc", "00", 0), "docAlmacenajedoc")
            ' ImpND.Add(New NodoImpresionN("", "docGasto", "00", 0), "docGasto")




            'Poliza
            Dim Cont As Integer = 0
            Dim kilat As String

            CuantosRenglones = 0
            tabla = Cd.ConsultaReader(idMovimiento).ToTable
            ' kilat = devuelveKilate(tabla.Rows(0)(2).ToString())



            kilat = tabla.Rows(0)(2).ToString()
            Dim DescCompleta As String

            For i As Integer = 0 To tabla.Rows.Count - 1
                ImpNDD.Add(New NodoImpresionN("", "ArticuloDescripcion", "", 0), "ArticuloDescripcion" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloPeso", "", 0), "ArticuloPeso" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloKilates", "", 0), "ArticuloKilates" + Format(i, "000"))
                DescCompleta = ""
                If tabla.Rows(i)(3) <> "" Then
                    DescCompleta += "Marca:" + tabla.Rows(i)(3)
                End If
                If tabla.Rows(i)(4) <> "" Then
                    DescCompleta += " Modelo:" + tabla.Rows(i)(4)
                End If
                If tabla.Rows(i)(5) <> "" Then
                    DescCompleta += " Color:" + tabla.Rows(i)(5)
                End If
                If tabla.Rows(i)(6) <> "" Then
                    DescCompleta += " N/S:" + tabla.Rows(i)(6)
                End If
                If tabla.Rows(i)(7) <> "" Then
                    DescCompleta += " Placas:" + tabla.Rows(i)(7)
                End If
                '" Modelo:" + tabla.Rows(i)(4).ToString() + " Color:" + tabla.Rows(i)(5).ToString() + " No. Serie:" + tabla.Rows(i)(6).ToString() + " Placas:" + tabla.Rows(i)(6).ToString()
                ImpNDD.Add(New NodoImpresionN("", "ArticuloDescripcionCompleta", DescCompleta, 0), "ArticuloDescripcionCompleta" + Format(i, "000"))

                ImpNDD.Add(New NodoImpresionN("", "ArticuloClasificacionV", tabla.Rows(i)(2).ToString(), 0), "ArticuloClasificacionV" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloMarca", tabla.Rows(i)(3).ToString(), 0), "ArticuloMarca" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloModelo", tabla.Rows(i)(4).ToString(), 0), "ArticuloModelo" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloColor", tabla.Rows(i)(5).ToString(), 0), "ArticuloColor" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloNoSerie", tabla.Rows(i)(6).ToString(), 0), "ArticuloNoSerie" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloPlacas", tabla.Rows(i)(7).ToString(), 0), "ArticuloPlacas" + Format(i, "000"))

                ImpNDD.Add(New NodoImpresionN("", "ArticuloDescripcionT", "", 0), "ArticuloDescripcionT" + Format(i, "000"))


                evaluo = evaluo + Double.Parse(tabla.Rows(i)(8).ToString).ToString()
                ' CuantosRenglones += 1
            Next
            CuantosRenglones = tabla.Rows.Count - 1
            ImpND.Add(New NodoImpresionN("", "ArticuloAvaluo", evaluo.ToString("C2"), 0), "ArticuloAvaluo")
            ' ImpND.Add(New NodoImpresionN("", "docPorcentajeAlmacenaje", Co.almacenaje.ToString, 0), "docPorcentajeAlmacenaje")
            ImpND.Add(New NodoImpresionN("", "docPorcentajeInteres", Format(Co.interes1a15V + Co.almacenaje1a15V, "0.00") + "%", 0), "docPorcentajeInteres")
            '  ImpND.Add(New NodoImpresionN("", "docPorcentajeAvaluo", Co.gasto.ToString, 0), "docPorcentajeAvaluo")
            ImpND.Add(New NodoImpresionN("", "docNumerodeRegistro", Co.numRegistro, 0), "docNumerodeRegistro")

            Dim interes As Double = 0
            Dim Almacenaje As Double = 0
            Dim desempenio As Double
            Dim refrendo As Double = 0

            interes = C.TotalAux * Co.interes1a15V * 15
            interes = interes / 100
            Almacenaje = C.TotalAux * Co.almacenaje1a15V * 15
            Almacenaje = Almacenaje / 100
            refrendo = interes + Almacenaje
            dec = Decimal.Parse(refrendo)
            refrendo = Double.Parse(Decimal.Round(dec, 0))
            desempenio = C.TotalAux + refrendo

            ImpND.Add(New NodoImpresionN("", "ArticuloPlazo30", "15 Días", 0), "ArticuloPlazo30")
            If fechaNueva = "" Then
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago30", Format(DateAdd(DateInterval.Day, 15, fechita), "dd/MM/yyyy"), 0), "ArticuloFechaPago30")
            Else
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago30", Format(DateAdd(DateInterval.Day, 15, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "ArticuloFechaPago30")
            End If

            ImpND.Add(New NodoImpresionN("", "ArticuloInteres30", interes.ToString("C2"), 0), "ArticuloInteres30")
            ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje30", Almacenaje.ToString("C2"), 0), "ArticuloAlmacenaje30")
            ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo30", refrendo.ToString("C2"), 0), "ArticuloRefrendo30")
            ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio30", desempenio.ToString("C2"), 0), "ArticuloDesempenio30")

            interes = interes + ((C.TotalAux * Co.interes16a30V * 15) / 100)
            Almacenaje = Almacenaje + ((C.TotalAux * Co.almacenaje16a30V * 15) / 100)
            refrendo = interes + Almacenaje
            dec = Decimal.Parse(refrendo)
            refrendo = Double.Parse(Decimal.Round(dec, 0))
            desempenio = C.TotalAux + refrendo

            ImpND.Add(New NodoImpresionN("", "ArticuloPlazo60", "30 Días", 0), "ArticuloPlazo60")
            If fechaNueva = "" Then
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago60", Format(DateAdd(DateInterval.Day, 30, fechita), "dd/MM/yyyy"), 0), "ArticuloFechaPago60")
            Else
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago60", Format(DateAdd(DateInterval.Day, 30, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "ArticuloFechaPago60")
            End If

            ImpND.Add(New NodoImpresionN("", "ArticuloInteres60", interes.ToString("C2"), 0), "ArticuloInteres60")
            ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje60", Almacenaje.ToString("C2"), 0), "ArticuloAlmacenaje60")
            ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo60", refrendo.ToString("C2"), 0), "ArticuloRefrendo60")
            ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio60", desempenio.ToString("C2"), 0), "ArticuloDesempenio60")


            interes = interes + ((C.TotalAux * Co.interes31a60V * 30) / 100)
            Almacenaje = Almacenaje + ((C.TotalAux * Co.almacenaje31a60V * 30) / 100)
            refrendo = interes + Almacenaje
            dec = Decimal.Parse(refrendo)
            refrendo = Double.Parse(Decimal.Round(dec, 0))
            desempenio = C.TotalAux + refrendo

            ImpND.Add(New NodoImpresionN("", "ArticuloPlazo90", "60 Días", 0), "ArticuloPlazo90")
            If fechaNueva = "" Then
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago90", Format(DateAdd(DateInterval.Day, 60, fechita), "dd/MM/yyyy"), 0), "ArticuloFechaPago90")
            Else
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago90", Format(DateAdd(DateInterval.Day, 60, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "ArticuloFechaPago90")
            End If

            ImpND.Add(New NodoImpresionN("", "ArticuloInteres90", interes.ToString("C2"), 0), "ArticuloInteres90")
            ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje90", Almacenaje.ToString("C2"), 0), "ArticuloAlmacenaje90")
            ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo90", refrendo.ToString("C2"), 0), "ArticuloRefrendo90")
            ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio90", desempenio.ToString("C2"), 0), "ArticuloDesempenio90")


            interes = interes + ((C.TotalAux * Co.A90mas * 30) / 100)
            Almacenaje = Almacenaje + ((C.TotalAux * Co.B90mas * 30) / 100)
            refrendo = interes + Almacenaje
            dec = Decimal.Parse(refrendo)
            refrendo = Double.Parse(Decimal.Round(dec, 0))
            desempenio = C.TotalAux + refrendo

            ImpND.Add(New NodoImpresionN("", "ArticuloPlazo120", " ", 0), "ArticuloPlazo120")
            If fechaNueva = "" Then
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago120", "", 0), "ArticuloFechaPago120")
            Else
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago120", "", 0), "ArticuloFechaPago120")
            End If

            ImpND.Add(New NodoImpresionN("", "ArticuloInteres120", "", 0), "ArticuloInteres120")
            ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje120", "", 0), "ArticuloAlmacenaje120")
            ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo120", "", 0), "ArticuloRefrendo120")
            ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio120", "", 0), "ArticuloDesempenio120")



            CuantosRenglones += 1
            Cont += 1
            'Next
            ' Next


            Posicion = 0
            NumeroPagina = 1
    End Sub
    Private Sub LlenaNodosImpresionT()

        Dim C As New dbEmpenios(idMovimiento, MySqlcon)
        Dim Cd As New dbEmpeniosDetallesT(idMovimiento, MySqlcon)
        ' Dim P As New dbPagosProveedores(MySqlcon) 'Modificado
        Dim O As New dbOpciones(MySqlcon)
        Dim TotalDescuento As Double = 0
        Dim tabla As DataTable
        Dim cantidadLetra As String 'modificado
        ImpND.Clear()
        ImpNDD.Clear()
        'Dim cantidad1 As Double
        'Dim centavos As Integer
        Dim plazo As String = ""
        Dim fechita As Date
        Dim aux As Double
        Dim evaluo As Double = 0
        Dim Cl As New CLetras
        Dim dec As Decimal
        fechita = C.Fecha.ToString
        If TotalNuevo = -1 Then
            'cantidad1 = Int(Double.Parse(C.Total))
            'centavos = Integer.Parse((Double.Parse(C.Total) - Double.Parse(cantidad1)) * 100)
            If C.Total >= 0 Then
                cantidadLetra = Cl.LetrasM(C.Total, 2, GlobalIdiomaLetras) 'Num2Text(Double.Parse(Double.Parse(cantidad1))) + " Y " + Format(centavos, "00") + "/100  PESOS M.N."
            Else
                cantidadLetra = "MENOS " + Cl.LetrasM(C.Total * -1, 2, GlobalIdiomaLetras) 'Num2Text(Double.Parse(Double.Parse(cantidad1))) + " Y " + Format(centavos, "00") + "/100  PESOS M.N."
            End If
        Else
            'cantidad1 = Int(Double.Parse(C.Total))
            'Dim x As Double = Double.Parse((TotalNuevo - Double.Parse(cantidad1)).ToString("0.00")) * 100

            'centavos = Integer.Parse(x)
            If TotalNuevo >= 0 Then
                cantidadLetra = Cl.LetrasM(TotalNuevo, 2, GlobalIdiomaLetras) 'Num2Text(Double.Parse(Double.Parse(cantidad1))) + " Y " + Format(centavos, "00") + "/100  PESOS M.N."
            Else
                cantidadLetra = "MENOS " + Cl.LetrasM(TotalNuevo * -1, 2, GlobalIdiomaLetras) 'Num2Text(Double.Parse(Double.Parse(cantidad1))) + " Y " + Format(centavos, "00") + "/100  PESOS M.N."
            End If
            End If
            Dim Sucursal As New dbSucursales(C.IdSucursal, MySqlcon)

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
            ImpND.Add(New NodoImpresionN("", "comentario", C.Comentario, 0), "comentario")
            ImpND.Add(New NodoImpresionN("", "docFormaPago", C.formaPago, 0), "docFormaPago")
            ImpND.Add(New NodoImpresionN("", "docFolio", Format(C.Folio, "000"), 0), "docFolio")
            ImpND.Add(New NodoImpresionN("", "serie", C.Serie, 0), "serie")
            If fechaNueva = "" Then
                ImpND.Add(New NodoImpresionN("", "docFecha", Date.Parse(C.Fecha.ToString).ToString("dd/MM/yyyy"), 0), "docFecha")
            Else
                ImpND.Add(New NodoImpresionN("", "docFecha", fechaNueva, 0), "docFecha")
            End If

            ImpND.Add(New NodoImpresionN("", "clienteNombre", C.Cliente.Nombre.ToString, 0), "clienteNombre") 'falta cnvertir a letra
            ImpND.Add(New NodoImpresionN("", "clienteDireccion", C.Cliente.Direccion.ToString + " " + C.Cliente.Colonia + " " + C.Cliente.Ciudad, 0), "clienteDireccion")
            ImpND.Add(New NodoImpresionN("", "clienteNumero", C.Cliente.Clave, 0), "clienteNumero")
            ImpND.Add(New NodoImpresionN("", "docEvaluador", C.buscaVendedor.ToString, 0), "docEvaluador")
            If TotalNuevo = -1 Then
                ImpND.Add(New NodoImpresionN("", "docPrestamoNumero", C.Total.ToString("C2"), 0), "docPrestamoNumero")
            Else
                ImpND.Add(New NodoImpresionN("", "docPrestamoNumero", TotalNuevo.ToString("C2"), 0), "docPrestamoNumero")
            End If
            If fechaNueva = "" Then
            ImpND.Add(New NodoImpresionN("", "docFechaVen", Format(DateAdd(DateInterval.Day, 60, fechita), "dd/MM/yyyy"), 0), "docFechaVen")
            ImpND.Add(New NodoImpresionN("", "docPlazoDesempenio", Format(DateAdd(DateInterval.Day, 60, fechita), "dd/MM/yyyy"), 0), "docPlazoDesempenio")
            ImpND.Add(New NodoImpresionN("", "docFechaLimiteFini", Format(DateAdd(DateInterval.Day, 60 + Co.diasProrrogaT, fechita), "dd/MM/yyyy"), 0), "docFechaLimiteFini")
            ImpND.Add(New NodoImpresionN("", "docInicioComerciali", Format(DateAdd(DateInterval.Day, 61 + Co.diasProrrogaT, fechita), "dd/MM/yyyy"), 0), "docInicioComerciali")
            Else
            ImpND.Add(New NodoImpresionN("", "docFechaVen", Format(DateAdd(DateInterval.Day, 120, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docFechaVen")
            ImpND.Add(New NodoImpresionN("", "docPlazoDesempenio", Format(DateAdd(DateInterval.Day, 120, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docPlazoDesempenio")
            ImpND.Add(New NodoImpresionN("", "docFechaLimiteFini", Format(DateAdd(DateInterval.Day, 120 + Co.diasProrrogaT, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docFechaLimiteFini")
            ImpND.Add(New NodoImpresionN("", "docInicioComerciali", Format(DateAdd(DateInterval.Day, 121 + Co.diasProrrogaT, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "docInicioComerciali")
            End If

            ImpND.Add(New NodoImpresionN("", "docPrestamo", cantidadLetra, 0), "docPrestamo")
            '  ImpND.Add(New NodoImpresionN("", "docEvaluador", C.buscaVendedor.ToString, 0), "docEvaluador")
            aux = C.TotalAux * ((Co.interes1a15T + Co.almacenaje1a15T) / 100)
            ImpND.Add(New NodoImpresionN("", "docInteresdoc", "$" + (aux).ToString("0.00"), 0), "docInteresdoc")
            '  ImpND.Add(New NodoImpresionN("", "docAlmacenajedoc", "00", 0), "docAlmacenajedoc")
            ' ImpND.Add(New NodoImpresionN("", "docGasto", "00", 0), "docGasto")




            'Poliza
            Dim Cont As Integer = 0
            Dim kilat As String

            CuantosRenglones = 0
            tabla = Cd.ConsultaReader(idMovimiento).ToTable
            ' kilat = devuelveKilate(tabla.Rows(0)(2).ToString())



            kilat = tabla.Rows(0)(2).ToString()


            For i As Integer = 0 To tabla.Rows.Count - 1
                ImpNDD.Add(New NodoImpresionN("", "ArticuloDescripcion", "", 0), "ArticuloDescripcion" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloPeso", "", 0), "ArticuloPeso" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloKilates", "", 0), "ArticuloKilates" + Format(i, "000"))

                ImpNDD.Add(New NodoImpresionN("", "ArticuloClasificacionV", tabla.Rows(i)(2).ToString(), 0), "ArticuloClasificacionV" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloMarca", "", 0), "ArticuloMarca" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloModelo", "", 0), "ArticuloModelo" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloColor", "", 0), "ArticuloColor" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloNoSerie", "", 0), "ArticuloNoSerie" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloPlacas", "", 0), "ArticuloPlacas" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloDescripcionCompleta", tabla.Rows(i)(3).ToString(), 0), "ArticuloDescripcionCompleta" + Format(i, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ArticuloDescripcionT", tabla.Rows(i)(3).ToString(), 0), "ArticuloDescripcionT" + Format(i, "000"))
                '  ImpNDD.Add(New NodoImpresionN("", "ArticuloEvaluoT", tabla.Rows(i)(4).ToString("C2"), 0), "ArticuloEvaluoT" + Format(i, "000"))
                ' ImpNDD.Add(New NodoImpresionN("", "ArticuloImporteT", tabla.Rows(i)(5).ToString("C2"), 0), "ArticuloImporteT" + Format(i, "000"))


                evaluo = evaluo + Double.Parse(tabla.Rows(i)(4).ToString).ToString()
                ' CuantosRenglones += 1
            Next
            CuantosRenglones = tabla.Rows.Count - 1
            ImpND.Add(New NodoImpresionN("", "ArticuloAvaluo", evaluo.ToString("C2"), 0), "ArticuloAvaluo")
            ' ImpND.Add(New NodoImpresionN("", "docPorcentajeAlmacenaje", Co.almacenaje.ToString, 0), "docPorcentajeAlmacenaje")
            ImpND.Add(New NodoImpresionN("", "docPorcentajeInteres", Format(Co.interes1a15T + Co.almacenaje1a15T, "0.00") + "%", 0), "docPorcentajeInteres")
            '  ImpND.Add(New NodoImpresionN("", "docPorcentajeAvaluo", Co.gasto.ToString, 0), "docPorcentajeAvaluo")
            ImpND.Add(New NodoImpresionN("", "docNumerodeRegistro", Co.numRegistro, 0), "docNumerodeRegistro")

            Dim interes As Double = 0
            Dim Almacenaje As Double = 0
            Dim desempenio As Double
            Dim refrendo As Double = 0

            interes = C.TotalAux * Co.interes1a15T * 15
            interes = interes / 100
            Almacenaje = C.TotalAux * Co.almacenaje1a15T * 15
            Almacenaje = Almacenaje / 100
            refrendo = interes + Almacenaje
            dec = Decimal.Parse(refrendo)
            refrendo = Double.Parse(Decimal.Round(dec, 0))
            desempenio = C.TotalAux + refrendo

            ImpND.Add(New NodoImpresionN("", "ArticuloPlazo30", "15 Días", 0), "ArticuloPlazo30")
            If fechaNueva = "" Then
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago30", Format(DateAdd(DateInterval.Day, 15, fechita), "dd/MM/yyyy"), 0), "ArticuloFechaPago30")
            Else
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago30", Format(DateAdd(DateInterval.Day, 15, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "ArticuloFechaPago30")
            End If

            ImpND.Add(New NodoImpresionN("", "ArticuloInteres30", interes.ToString("C2"), 0), "ArticuloInteres30")
            ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje30", Almacenaje.ToString("C2"), 0), "ArticuloAlmacenaje30")
            ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo30", refrendo.ToString("C2"), 0), "ArticuloRefrendo30")
            ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio30", desempenio.ToString("C2"), 0), "ArticuloDesempenio30")

            interes = interes + ((C.TotalAux * Co.interes16a30T * 15) / 100)
            Almacenaje = Almacenaje + ((C.TotalAux * Co.almacenaje16a30T * 15) / 100)
            refrendo = interes + Almacenaje
            dec = Decimal.Parse(refrendo)
            refrendo = Double.Parse(Decimal.Round(dec, 0))
            desempenio = C.TotalAux + refrendo

            ImpND.Add(New NodoImpresionN("", "ArticuloPlazo60", "30 Días", 0), "ArticuloPlazo60")
            If fechaNueva = "" Then
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago60", Format(DateAdd(DateInterval.Day, 30, fechita), "dd/MM/yyyy"), 0), "ArticuloFechaPago60")
            Else
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago60", Format(DateAdd(DateInterval.Day, 30, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "ArticuloFechaPago60")
            End If

            ImpND.Add(New NodoImpresionN("", "ArticuloInteres60", interes.ToString("C2"), 0), "ArticuloInteres60")
            ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje60", Almacenaje.ToString("C2"), 0), "ArticuloAlmacenaje60")
            ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo60", refrendo.ToString("C2"), 0), "ArticuloRefrendo60")
            ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio60", desempenio.ToString("C2"), 0), "ArticuloDesempenio60")


            interes = interes + ((C.TotalAux * Co.interes31a60T * 30) / 100)
            Almacenaje = Almacenaje + ((C.TotalAux * Co.almacenaje31a60T * 30) / 100)
            refrendo = interes + Almacenaje
            dec = Decimal.Parse(refrendo)
            refrendo = Double.Parse(Decimal.Round(dec, 0))
            desempenio = C.TotalAux + refrendo

            ImpND.Add(New NodoImpresionN("", "ArticuloPlazo90", "60 Días", 0), "ArticuloPlazo90")
            If fechaNueva = "" Then
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago90", Format(DateAdd(DateInterval.Day, 60, fechita), "dd/MM/yyyy"), 0), "ArticuloFechaPago90")
            Else
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago90", Format(DateAdd(DateInterval.Day, 60, Date.Parse(fechaNueva)), "dd/MM/yyyy"), 0), "ArticuloFechaPago90")
            End If

            ImpND.Add(New NodoImpresionN("", "ArticuloInteres90", interes.ToString("C2"), 0), "ArticuloInteres90")
            ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje90", Almacenaje.ToString("C2"), 0), "ArticuloAlmacenaje90")
            ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo90", refrendo.ToString("C2"), 0), "ArticuloRefrendo90")
            ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio90", desempenio.ToString("C2"), 0), "ArticuloDesempenio90")


            interes = interes + ((C.TotalAux * Co.A90mas * 30) / 100)
            Almacenaje = Almacenaje + ((C.TotalAux * Co.B90mas * 30) / 100)
            refrendo = interes + Almacenaje
            dec = Decimal.Parse(refrendo)
            refrendo = Double.Parse(Decimal.Round(dec, 0))
            desempenio = C.TotalAux + refrendo

            ImpND.Add(New NodoImpresionN("", "ArticuloPlazo120", " ", 0), "ArticuloPlazo120")
            If fechaNueva = "" Then
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago120", "", 0), "ArticuloFechaPago120")
            Else
                ImpND.Add(New NodoImpresionN("", "ArticuloFechaPago120", "", 0), "ArticuloFechaPago120")
            End If

            ImpND.Add(New NodoImpresionN("", "ArticuloInteres120", "", 0), "ArticuloInteres120")
            ImpND.Add(New NodoImpresionN("", "ArticuloAlmacenaje120", "", 0), "ArticuloAlmacenaje120")
            ImpND.Add(New NodoImpresionN("", "ArticuloRefrendo120", "", 0), "ArticuloRefrendo120")
            ImpND.Add(New NodoImpresionN("", "ArticuloDesempenio120", "", 0), "ArticuloDesempenio120")



            CuantosRenglones += 1
            Cont += 1
            'Next
            ' Next


            Posicion = 0
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
    Public Function Num2Text(ByVal value As Double) As String
        Select Case value
            Case 0 : Num2Text = "CERO"
            Case 1 : Num2Text = "UN"
            Case 2 : Num2Text = "DOS"
            Case 3 : Num2Text = "TRES"
            Case 4 : Num2Text = "CUATRO"
            Case 5 : Num2Text = "CINCO"
            Case 6 : Num2Text = "SEIS"
            Case 7 : Num2Text = "SIETE"
            Case 8 : Num2Text = "OCHO"
            Case 9 : Num2Text = "NUEVE"
            Case 10 : Num2Text = "DIEZ"
            Case 11 : Num2Text = "ONCE"
            Case 12 : Num2Text = "DOCE"
            Case 13 : Num2Text = "TRECE"
            Case 14 : Num2Text = "CATORCE"
            Case 15 : Num2Text = "QUINCE"
            Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
            Case 20 : Num2Text = "VEINTE"
            Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
            Case 30 : Num2Text = "TREINTA"
            Case 40 : Num2Text = "CUARENTA"
            Case 50 : Num2Text = "CINCUENTA"
            Case 60 : Num2Text = "SESENTA"
            Case 70 : Num2Text = "SETENTA"
            Case 80 : Num2Text = "OCHENTA"
            Case 90 : Num2Text = "NOVENTA"
            Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
            Case 100 : Num2Text = "CIEN"
            Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
            Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
            Case 500 : Num2Text = "QUINIENTOS"
            Case 700 : Num2Text = "SETECIENTOS"
            Case 900 : Num2Text = "NOVECIENTOS"
            Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
            Case 1000 : Num2Text = "MIL"
            Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
            Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
            Case 1000000 : Num2Text = "UN MILLON"
            Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
            Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES "
                If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : Num2Text = "UN BILLON"
            Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
        End Select
    End Function

#End Region

    Private Sub txtPeso_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtPeso.Text = "" Then
            txtPeso.Text = "0"
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

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()

    End Sub

    Private Sub Button8_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
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

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbClasificacion.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaClienteBoton2()
    End Sub

    Private Sub txtEvaluo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEvaluo.TextChanged

    End Sub

    Private Sub txtEvaluo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEvaluo.Leave
        Dim x As Double
        If txtEvaluo.Text = "." Then
            txtEvaluo.Text = "0.00"
        End If
        If txtEvaluo.Text = "" Then
            txtEvaluo.Text = "0.00"
        Else
            x = Double.Parse(txtEvaluo.Text)
            txtEvaluo.Text = Format(x, "0.00####")
        End If
        If txtEvaluo.Text = "" Then
            txtEvaluo.Text = "0"
        End If
    End Sub

    Private Sub Button16_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim et As New frmVentasTextoExtra(txtDescripcion.Text, 45, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtDescripcion.Text = et.Texto
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        DibujaPaginaN(e.Graphics)
        If MasPaginas = True Or NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
        End If
        If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
            e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
        End If
        e.HasMorePages = MasPaginas
    End Sub
    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.Empenios, GlobalIdSucursalDefault)
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.Empenios + 1000, GlobalIdSucursalDefault)
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
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Empenios, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Empenios + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If

        Catch ex As Exception

        End Try
        Dim Rec As RectangleF
        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer


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

                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then

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

            End If
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DibujaPaginaFlujo(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos
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
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Empenios, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Empenios + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
        Catch ex As Exception

        End Try
        Dim Rec As RectangleF


        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then

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

            End If

            'If ncb.Visible = 1 And ncb.Tipo = 2 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(Math.Abs((ncb.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
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

    Private Sub txtPeso_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPeso.Leave
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
    Private Sub ActualizaDiasPrestamo()
        If txtDiasPrestamo.Text <> "" Then
            Dim C As New dbEmpenios(MySqlcon)
            C.ActualizarDiasPrestamo(idMovimiento, Integer.Parse(txtDiasPrestamo.Text))
        End If
    End Sub

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

    Private Sub txtEvaluo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEvaluo.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtImporte_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporte.KeyPress

        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            BotonAgregar()
            ' ActualizaDiasPrestamo()

        Else
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
                e.Handled = True
            End If
        End If
        
    End Sub

    Private Sub txtDiasPrestamo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiasPrestamo.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
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
        
        'checarPrestamo()
        'END CHEHAR PERMISOS ****************************************************************
    End Sub

    Private Sub txtDiasPrestamo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiasPrestamo.Leave
        If txtDiasPrestamo.Text = "" Then
            txtDiasPrestamo.Text = "0"
        End If
        If Estado <> Estados.Inicio Then
            ActualizaDiasPrestamo()
        End If
    End Sub
    Private Sub ActualizarConfig()
        Dim C As New dbEmpenios(MySqlcon)
        If TipoEmpeño = 0 Then
            C.ModificarConfiguracion(idMovimiento, Co.A1a30, Co.A31a60, Co.A61a90, Co.A90mas, Co.B1a30, Co.B31a60, Co.B61a90, Co.B90mas)
        End If
        If TipoEmpeño = 1 Then
            C.ModificarConfiguracion(idMovimiento, Co.interes1a15V, Co.interes16a30V, Co.interes31a60V, 0, Co.almacenaje1a15V, Co.almacenaje16a30V, Co.almacenaje31a60V, 0)
        End If
        If TipoEmpeño = 2 Then
            C.ModificarConfiguracion(idMovimiento, Co.interes1a15T, Co.interes16a30T, Co.interes31a60T, 0, Co.almacenaje1a15T, Co.almacenaje16a30T, Co.almacenaje31a60T, 0)
        End If


    End Sub

    Private Sub btnActualizarConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizarConfig.Click
        If Estado <> Estados.Inicio Then
            ActualizarConfig()
            PopUp("Configuración actualizada", 90)
        End If
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

    Private Sub cmbClasificacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClasificacion.SelectedIndexChanged
        txtDescripcion.Text = cmbClasificacion.Text
    End Sub

    Private Sub txtImporte_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporte.TextChanged

    End Sub

    Private Sub cmbCaja_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCaja.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtSerie.Focus()
        End If
    End Sub

    Private Sub cmbCaja_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCaja.SelectedIndexChanged
        
    End Sub

    Private Sub txtSerie_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSerie.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFolio.Focus()
        End If
        
    End Sub

    Private Sub txtSerie_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerie.TextChanged

    End Sub

    Private Sub txtFolio_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFolio.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
        If e.KeyCode = Keys.F5 Then
            nuevoFolio()
        End If
    End Sub

    Private Sub txtFolio_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolio.TextChanged

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
    Private Function checarPrestamo() As String
        Dim x As Double
        x = Double.Parse(txtImporte.Text)
        Dim porcentaje As Double
        If tipoJoya = "Oro" Then
            porcentaje = (Double.Parse(txtEvaluo.Text) * Co.aumentoPrenda) / 100
            porcentaje = porcentaje + Double.Parse(txtEvaluo.Text)
            If x > porcentaje Then
                'txtImporte.Text = porcentaje.ToString("0.00")
                Return "El Importe no puede ser más alto de " + Co.aumentoPrenda.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo."
                'If MsgBox("El Importe no puede ser más alto de " + Co.aumentoPrenda.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo.", MsgBoxStyle.OkOnly, "Pull System Soft") = MsgBoxResult.Ok Then
                '    txtImporte.Focus()
                'End If
            End If
        Else
            porcentaje = (Double.Parse(txtEvaluo.Text) * Co.aumentoPlata) / 100
            porcentaje = porcentaje + Double.Parse(txtEvaluo.Text)
            If x > porcentaje Then
                'txtImporte.Text = porcentaje.ToString("0.00")
                Return "El Importe no puede ser más alto de " + Co.aumentoPlata.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo."
                'If MsgBox("El Importe no puede ser más alto de " + Co.aumentoPlata.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo.", MsgBoxStyle.OkOnly, "Pull System Soft") = MsgBoxResult.Ok Then
                '    txtImporte.Focus()
                'End If
            End If
        End If
        Return ""
    End Function
    Private Function checarPrestamoV() As String
        Dim x As Double
        x = Double.Parse(txtImporteV.Text)
        Dim porcentaje As Double
        'If tipoJoya = "Oro" Then
        '  Dim CD As New dbEmpeniosDetallesV(idMovimiento, MySqlcon)

        porcentaje = (Double.Parse(txtEvaluoV.Text) * Co.aumentoValorPrendaV) / 100
        porcentaje = porcentaje + Double.Parse(txtEvaluoV.Text)
        If x > porcentaje Then
            'txtImporteV.Text = porcentaje.ToString("0.00")
            'If MsgBox("El Importe no puede ser más alto de " + Co.aumentoValorPrendaV.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo.", MsgBoxStyle.OkOnly, "Pull System Soft") = MsgBoxResult.Ok Then
            '    txtImporteV.Focus()
            'End If
            Return "El Importe no puede ser más alto de " + Co.aumentoValorPrendaV.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo."
        End If
        Return ""
    End Function
    Private Function checarPrestamoT() As String
        Dim x As Double
        x = Double.Parse(txtImporteT.Text)
        Dim porcentaje As Double
        'If tipoJoya = "Oro" Then
        '  Dim CD As New dbEmpeniosDetallesV(idMovimiento, MySqlcon)

        porcentaje = (Double.Parse(txtEvaluoT.Text) * Co.aumentoValorPrendaT) / 100
        porcentaje = porcentaje + Double.Parse(txtEvaluoT.Text)
        If x > porcentaje Then
            'txtImporteT.Text = porcentaje.ToString("0.00")
            'If MsgBox("El Importe no puede ser más alto de " + Co.aumentoValorPrendaT.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo.", MsgBoxStyle.OkOnly, "Pull System Soft") = MsgBoxResult.Ok Then
            '    txtImporteT.Focus()
            'End If
            Return "El Importe no puede ser más alto de " + Co.aumentoValorPrendaT.ToString + "% (" + porcentaje.ToString("C2") + ") al evalúo."
        End If

        Return ""
    End Function

    Private Sub cmbTipoJoya_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbTipoJoya.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            If tipoJoya = "Oro" Then
                cmbKilates.Focus()
            Else
                txtPeso.Focus()
            End If
        End If
        
    End Sub

    Private Sub txtDescripcion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescripcion.TextChanged
        
    End Sub

    Private Sub rdbVehiculos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbVehiculos.CheckedChanged
        If rdbVehiculos.Checked = True Then
            grpJoyas.Visible = False
            grpTerrenos.Visible = False
            grpVEhiculos.Top = 184
            grpVEhiculos.Left = 15
            grpVEhiculos.Visible = True
            TipoEmpeño = 1
            If Estado <> Estados.Inicio Then
                ConsultaDetallesv()
            End If
        End If
    End Sub

    Private Sub rdbJoyería_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbJoyería.CheckedChanged
        If rdbJoyería.Checked = True Then
            grpVEhiculos.Visible = False
            grpJoyas.Visible = True
            grpTerrenos.Visible = False
            TipoEmpeño = 0
            If Estado <> Estados.Inicio Then
                ConsultaDetalles()
            End If
        End If
    End Sub
    '************************VEHUCULOS*******************************

    Private Sub btnAgregarV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarV.Click
        'If Estado = 0 Then
        '    Guardar()
        'End If
        'If Estado <> 0 Then
        '    AgregaArticuloV()
        'End If
        BotonAgregar3()
    End Sub
    Private Sub BotonAgregar3()
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosAlta, PermisosN.Secciones.Empenios) = True Then
            If Estado = 0 Then
                Guardar()
                ActualizarConfig()
            End If
            If Estado <> 0 Then
                AgregaArticuloV()
            End If
            'ActualizaDiasPrestamo()
        Else
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
        End If

    End Sub
    Private Sub AgregaArticuloV()
        Try
            Dim CD As New dbEmpeniosDetallesV(MySqlcon)
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
                If CInt(txtEvaluoV.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += " El Evalúo debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If
            If cmbClasificacion.Text = "" Then
                MsgError += " Es necesario dar de alta clasificaciones."
                HayError = True
            End If
            
            'If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosSobreValouPermitir, PermisosN.Secciones.Empenios) = False And CDbl(txtEvaluoV.Text) < CDbl(txtImporteV.Text) Then
            '    If MsgBox("¿Autorizar Sobrevaluo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            '        Dim EC As New dbEmpeniosConfiguracion(1, MySqlcon)
            '        Dim Pass As String
            '        Dim dialog As New frmInput("Clave Autorización:", frmInput.TipoDatos.Texto, False, True, 45)
            '        dialog.ShowDialog()
            '        Pass = dialog.Valor
            '        If Pass <> EC.ClaveAutorizacion Then
            '            MsgError += vbCrLf + " Clave incorrecta."
            '            HayError = True
            '        End If
            '    Else
            '        MsgError += vbCrLf + " No se puede prestar arriba del evalúo."
            '        HayError = True
            '    End If
            'End If
            'Dim str As String
            'str = checarPrestamoV()
            'If str <> "" And HayError = False Then
            '    MsgError += vbCrLf + str
            '    HayError = True
            'End If

            Dim str As String
            str = checarPrestamoV()

            If CDbl(txtEvaluoV.Text) < CDbl(txtImporteV.Text) And str = "" Then
                If MsgBox("¿Autorizar Sobrevaluo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim frmAuto As New frmCambioUsuario(1, 1)
                    If frmAuto.ShowDialog <> Windows.Forms.DialogResult.OK Then
                        MsgError += vbCrLf + " Sin Autorización para realizar este movimiento."
                        HayError = True
                    End If
                Else
                    MsgError += vbCrLf + " No se puede prestar arriba del evalúo."
                    HayError = True
                End If
            End If

            If CDbl(txtEvaluoV.Text) < CDbl(txtImporteV.Text) And str <> "" Then
                If MsgBox("¿Autorizar Sobrevaluo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim frmAuto As New frmCambioUsuario(1, 2)
                    If frmAuto.ShowDialog <> Windows.Forms.DialogResult.OK Then
                        MsgError += vbCrLf + " Sin Autorización para realizar este movimiento."
                        HayError = True
                    End If
                Else
                    MsgError += vbCrLf + str
                    HayError = True
                End If
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

    Private Sub txtEvaluoV_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEvaluoV.Leave
        Dim x As Double
        If txtEvaluoV.Text = "." Then
            txtEvaluoV.Text = "0.00"
        End If
        If txtEvaluoV.Text = "" Then
            txtEvaluoV.Text = "0.00"
        Else
            x = Double.Parse(txtEvaluoV.Text)
            txtEvaluoV.Text = Format(x, "0.00####")
        End If
        If txtEvaluoV.Text = "" Then
            txtEvaluoV.Text = "0"
        End If
    End Sub

    Private Sub txtImporteV_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporteV.Leave
        Dim x As Double
        If txtImporteV.Text = "." Then
            txtImporteV.Text = "0.00"
        End If
        If txtImporteV.Text = "" Then
            txtImporteV.Text = "0.00"
        Else
            x = Double.Parse(txtImporteV.Text)
            txtImporteV.Text = Format(x, "0.00####")
        End If
        If txtImporteV.Text = "" Then
            txtImporteV.Text = "0"
        End If

        'checarPrestamoV()
    End Sub

    Private Sub txtImporteV_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporteV.KeyPress


        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then

            'If Estado = 0 Then
            '    Guardar()
            'End If
            'If Estado <> 0 Then
            '    AgregaArticuloV()
            'End If
            BotonAgregar3()
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
        Else
            If Char.IsLower(e.KeyChar) And txtPlacasV.Text.Length <= 45 Then
                txtPlacasV.SelectedText = Char.ToUpper(e.KeyChar)
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnNuevoV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoV.Click
        NuevoConceptov()
    End Sub

    Private Sub txtPlacasV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlacasV.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub rdbTerrenos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTerrenos.CheckedChanged
        If rdbTerrenos.Checked = True Then
            grpTerrenos.Left = 15
            grpTerrenos.Top = 184
            grpTerrenos.Visible = True
            grpJoyas.Visible = False
            grpVEhiculos.Visible = False
            TipoEmpeño = 2
            If Estado <> Estados.Inicio Then
                ConsultaDetallest()
            End If
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim et As New frmVentasTextoExtra(txtDescripcionT.Text, 400, True)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtDescripcionT.Text = et.Texto
        End If
    End Sub

    Private Sub agregarConceptoT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles agregarConceptoT.Click
        'If Estado = 0 Then
        '    Guardar()
        '    ActualizarConfig()
        'End If
        'If Estado <> 0 Then
        '    AgregaArticuloT()
        'End If
        BotonAgregar2()
    End Sub
    Private Sub BotonAgregar2()
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosAlta, PermisosN.Secciones.Empenios) = True Then
            If Estado = 0 Then
                Guardar()
                ActualizarConfig()
            End If
            If Estado <> 0 Then
                AgregaArticuloT()
            End If
            'ActualizaDiasPrestamo()
        Else
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
        End If

    End Sub
    Private Sub nuevoConceptoT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nuevoConceptoT.Click
        nuevoConceptoTe()
    End Sub
    Private Sub AgregaArticuloT()
        Try
            Dim CD As New dbEmpeniosDetallesT(MySqlcon)
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
                If CInt(txtEvaluoT.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += " El Evalúo debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If
            If cmbClasificacion.Text = "" Then
                MsgError += " Es necesario dar de alta clasificaciones."
                HayError = True
            End If
            If txtDescripcionT.Text = "" Then
                MsgError += " Es necesario dar una descripción."
                HayError = True
            End If

            'If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosSobreValouPermitir, PermisosN.Secciones.Empenios) = False And CDbl(txtEvaluoT.Text) < CDbl(txtImporteT.Text) Then
            '    If MsgBox("¿Autorizar Sobrevaluo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            '        Dim EC As New dbEmpeniosConfiguracion(1, MySqlcon)
            '        Dim Pass As String
            '        Dim dialog As New frmInput("Clave Autorización:", frmInput.TipoDatos.Texto, False, True, 45)
            '        dialog.ShowDialog()
            '        Pass = dialog.Valor
            '        If Pass <> EC.ClaveAutorizacion Then
            '            MsgError += vbCrLf + " Clave incorrecta."
            '            HayError = True
            '        End If
            '    Else
            '        MsgError += vbCrLf + " No se puede prestar arriba del evalúo."
            '        HayError = True
            '    End If
            'End If
            'Dim str As String
            'str = checarPrestamoT()
            'If str <> "" And HayError = False Then
            '    MsgError += vbCrLf + str
            '    HayError = True
            'End If

            Dim str As String
            str = checarPrestamoT()

            If CDbl(txtEvaluoT.Text) < CDbl(txtImporteT.Text) And str = "" Then
                If MsgBox("¿Autorizar Sobrevaluo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim frmAuto As New frmCambioUsuario(1, 1)
                    If frmAuto.ShowDialog <> Windows.Forms.DialogResult.OK Then
                        MsgError += vbCrLf + " Sin Autorización para realizar este movimiento."
                        HayError = True
                    End If
                Else
                    MsgError += vbCrLf + " No se puede prestar arriba del evalúo."
                    HayError = True
                End If
            End If

            If CDbl(txtEvaluoT.Text) < CDbl(txtImporteT.Text) And str <> "" Then
                If MsgBox("¿Autorizar Sobrevaluo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim frmAuto As New frmCambioUsuario(1, 2)
                    If frmAuto.ShowDialog <> Windows.Forms.DialogResult.OK Then
                        MsgError += vbCrLf + " Sin Autorización para realizar este movimiento."
                        HayError = True
                    End If
                Else
                    MsgError += vbCrLf + str
                    HayError = True
                End If
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
    Private Sub ConsultaDetallest()
        Try

            Tabla.Rows.Clear()
            Dim T As DataTable
            Dim CD As New dbEmpeniosDetallesT(MySqlcon)
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

    Private Sub txtImporteT_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporteT.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.00"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.00")
        End If
        'checarPrestamoT()
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

    Private Sub txtImporteT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporteT.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            'If Estado = 0 Then
            '    Guardar()
            'End If
            'If Estado <> 0 Then
            '    AgregaArticuloT()
            'End If
            BotonAgregar2()
        Else
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtDescripcionT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescripcionT.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtEvaluoT.Focus()
        End If
    End Sub

    Private Sub cmbClasificacionT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbClasificacionT.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtDescripcionT.Focus()
        End If
    End Sub

    Private Sub grpTerrenos_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpTerrenos.Enter

    End Sub

    Private Sub grpVEhiculos_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpVEhiculos.Enter

    End Sub

    Private Sub Label29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label29.Click

    End Sub

    Private Sub txtImporteT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporteT.TextChanged

    End Sub

    Private Sub txtImporteV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporteV.TextChanged

    End Sub

    Private Sub txtMarcav_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMarcav.TextChanged
      
    End Sub

    Private Sub txtModelo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtModelo.TextChanged
       
    End Sub

    Private Sub txtColor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtColor.TextChanged
       
    End Sub

    Private Sub txtNoSerie_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoSerie.TextChanged
       
    End Sub

    Private Sub txtDescripcionT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescripcionT.TextChanged
      
    End Sub

    Private Sub txtComentario_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComentario.TextChanged
   
    End Sub

    Private Sub txtEvaluoV_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEvaluoV.TextChanged

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHabilita.Click
        'Dim EC As New dbEmpeniosConfiguracion(1, MySqlcon)
        'Dim Pass As String
        'Dim dialog As New frmInput("Autorización:", frmInput.TipoDatos.Texto, False, True, 45)
        'dialog.ShowDialog()
        'Pass = dialog.Valor
        Dim frmAuto As New frmCambioUsuario(1, 0)
        If frmAuto.ShowDialog = Windows.Forms.DialogResult.OK Then
            'btnHabilita.Enabled = True
            cmbVendedor.Enabled = True
        Else
            MsgBox("Clave incorrecta", MsgBoxStyle.OkOnly, GlobalNombreApp)
            'btnHabilita.Enabled = False
            cmbVendedor.Enabled = False
        End If
    End Sub

    Private Sub txtSerie_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtSerie.MouseDown
        
    End Sub

    Private Sub dtpFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha.ValueChanged
        If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then
            Dim Sf As New dbSucursalesFolios(MySqlcon)
            If Co.folio = 0 Then
                'Dim anio As String = dtpFecha.Value.Year.ToString.Chars(2) + dtpFecha.Value.Year.ToString.Chars(3)
                'txtSerie.Text = dtpFecha.Value.Day.ToString("00") + dtpFecha.Value.Month.ToString("00") + anio
                txtSerie.Text = dtpFecha.Value.ToString("ddMMyy")
                Dim V As New dbEmpenios(MySqlcon)
                txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
                If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
            Else
                If Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.MovimientosCaja, 0) Then
                    txtSerie.Text = Sf.Serie
                Else
                    txtSerie.Text = ""
                End If

                Dim V As New dbEmpenios(MySqlcon)
                txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
                If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
            End If
        End If
    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Dim frmAuto As New frmCambioUsuario(1, 3)
        If frmAuto.ShowDialog = Windows.Forms.DialogResult.OK Then
            'btnHabilita.Enabled = True
            dtpFecha.Enabled = True
        Else
            MsgBox("Clave incorrecta", MsgBoxStyle.OkOnly, GlobalNombreApp)
            'btnHabilita.Enabled = False
            dtpFecha.Enabled = False
        End If
    End Sub
End Class