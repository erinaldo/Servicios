Public Class frmGastos

    Dim IdsSucursales As New elemento
    Dim IdsVendedores As New elemento
    Dim IdsClasificacion As New elemento
    Dim IdsClasificacion2 As New elemento
    Dim IdsClasificacion3 As New elemento
    Dim IdsCajas As New elemento
    Dim IdCajaDefault As Integer
    Dim idMovimiento As Integer
    Dim Estado As Byte
    Dim FolioAnt As Integer
    Dim idCliente As Integer
    Dim total As Double
    Dim idDetalle As Integer
    Dim Tabla As New DataTable
    Dim ConsultaOn As Boolean
    Dim idVendedorU As Integer

    Public Sub New(ByVal pidVenta As Integer, ByVal pIdCaja As Integer, ByVal pIdVendedor As Integer, ByVal pidSucursal As Integer)
        InitializeComponent()
        idMovimiento = pidVenta
        IdCajaDefault = pIdCaja

    End Sub
    Private Sub TextBox14_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComentario.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub
    Private Sub frmGastos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        LlenaCombos("tblvendedores", cmbVendedor, "nombre", "nombret", "idvendedor", IdsVendedores)
        LlenaCombos("tblgastosclasificacion", cmbClasificacion, "concat(clave,' ',nombre)", "nombret", "idClasificacion", IdsClasificacion, , , "clave")
        If cmbClasificacion.Items.Count < 1 Then
            MsgBox("Es necesario dar de alta clasificaciones.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
        Dim U As New dbUsuarios(GlobalIdUsuario, MySqlcon)
        idVendedorU = U.IdVendedor
        nuevo()
        'txtDescripcion.Text = ""
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
        Button5.Enabled = True
        Label24.Visible = False
        Button4.Enabled = True
        Button8.Enabled = True
        Button13.Enabled = False
        Button2.Enabled = False
        '----
        'ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        'ComboBox6.SelectedIndex = IdsCajas.Busca(IdCajaDefault)
        cmbCaja.Enabled = True
        txtFolio.Enabled = True
        txtSerie.Enabled = True

        dtpFecha.Value = Date.Now
        txtImporte.Text = ""
        If cmbClasificacion.Items.Count > 0 Then
            cmbClasificacion.SelectedIndex = 0
        End If
        txtComentario.Text = ""
        txtDescripcion.Text = ""
        FolioAnt = 0
        cmbVendedor.SelectedIndex = IdsVendedores.Busca(idVendedorU)
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        If Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Gastos, 0) Then
            txtSerie.Text = Sf.Serie
        Else
            txtSerie.Text = ""
        End If

        Dim V As New dbGastos(MySqlcon)
        txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
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
        Panel1.Enabled = True
        Panel2.Enabled = True
        Label14.Text = "0"

        'Dim o As New dbOpciones(MySqlcon)
        'TextBox8.Text = o.Imp.ToString
        NuevoConcepto()

        txtDescripcion.Focus()
        If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosCambiarSucursal, PermisosN.Secciones.Gastos) = False Then
            ComboBox3.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosPermitirCambioFecha, PermisosN.Secciones.Gastos) = False Then
            dtpFecha.Enabled = False
        End If
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        LlenaCombos("tblcajas", cmbCaja, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        cmbCaja.SelectedIndex = IdsCajas.Busca(IdCajaDefault)
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Gastos, 0)
        txtSerie.Text = Sf.Serie
        Dim V As New dbGastos(MySqlcon)
        txtFolio.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(txtFolio.Text) < Sf.FolioInicial Then txtFolio.Text = Sf.FolioInicial.ToString
    End Sub
    Private Sub frmGastos_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este movimiento no ha sido guardado. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbGastos(MySqlcon)
                c.Eliminar(idMovimiento)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()
    End Sub
    Private Sub BotonAgregar()
        If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosAlta, PermisosN.Secciones.Gastos) = True Then
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
            Dim errores = False
            'If idCliente <> 0 Then
            Dim C As New dbGastos(MySqlcon)
            txtFolio.BackColor = Color.White
            Label17.Visible = False

            If C.ChecaFolioRepetido(CInt(txtFolio.Text), txtSerie.Text) Then
                txtFolio.BackColor = Color.FromArgb(250, 150, 150)
                Label17.Visible = True
                FolioAnt = 0
                errores = True
            Else
                FolioAnt = CInt(txtFolio.Text)
            End If
            If errores Then
                MessageBox.Show("Folio repetido", "Pull System Soft", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                C.Guardar(idCliente, Format(dtpFecha.Value, "yyyy/MM/dd"), txtFolio.Text, total, IdsSucursales.Valor(ComboBox3.SelectedIndex), txtSerie.Text, IdsCajas.Valor(cmbCaja.SelectedIndex), IdsVendedores.Valor(cmbVendedor.SelectedIndex), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), txtComentario.Text)
                idMovimiento = C.ID
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
            
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbGastosDetalles(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            'If IdInventario = 0 Then MsgError += "Debe indicar un artículo."

            If IsNumeric(txtImporte.Text) = False Then
                MsgError += vbCrLf + "El importe debe ser un valor numérico."
                HayError = True
            Else
                If CInt(txtImporte.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += " El importe debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If
            If cmbClasificacion.Text = "" Then
                MsgError += " Es necesario dar de alta clasificacines."
                HayError = True
            End If
            Dim IdClas2 As Integer
            If cmbclasificacion2.SelectedIndex = 0 Then
                IdClas2 = 0
            Else
                IdClas2 = IdsClasificacion2.Valor(cmbclasificacion2.SelectedIndex)
            End If
            Dim IdClas3 As Integer
            If cmbclas3.SelectedIndex = 0 Then
                IdClas3 = 0
            Else
                IdClas3 = IdsClasificacion3.Valor(cmbclas3.SelectedIndex)
            End If
            If HayError = False Then

                If Button4.Text = "Agregar Concepto" Then

                    CD.Guardar(idMovimiento, txtDescripcion.Text, Double.Parse(txtImporte.Text), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), IdClas2, IdClas3) ',  IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    'aqui me quede
                    ConsultaDetalles()
                    NuevoConcepto()

                Else
                    CD.Modificar(idDetalle, idMovimiento, txtDescripcion.Text, Double.Parse(txtImporte.Text), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), IdClas2, IdClas3)

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
            Dim CD As New dbGastosDetalles(MySqlcon)
            T = CD.ConsultaReader(idMovimiento).ToTable()


            DGDetalles.DataSource = T
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGDetalles.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(3).Visible = False
            DGDetalles.Columns(1).HeaderText = "Descripción"
            DGDetalles.Columns(2).HeaderText = "Precio"

            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            ConsultaOn = True
            SacaTotal()
            ConsultaOn = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim V As New dbGastosDetalles(MySqlcon)
                V.DaTotal(idMovimiento)
                Label14.Text = Format(V.TotalVenta, "#,###,##0.00")
                total = V.TotalVenta
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub NuevoConcepto()
        'IdInventario = 0
        'IdVariante = 0
        'If cmbclas3.SelectedIndex > 0 Then
        '    txtDescripcion.Text = cmbClasificacion.Text + " " + cmbclasificacion2.Text + " " + cmbclas3.Text
        'Else
        '    If cmbclasificacion2.SelectedIndex = 0 Then
        '        txtDescripcion.Text = cmbClasificacion.Text
        '    Else
        '        txtDescripcion.Text = cmbClasificacion.Text + " " + cmbclasificacion2.Text
        '    End If
        'End If
        Button4.Text = "Agregar Concepto"
        txtImporte.Text = "0"
        Button9.Enabled = False
        txtDescripcion.Focus()
    End Sub
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NuevoConcepto()
    End Sub
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim CD As New dbGastosDetalles(MySqlcon)
                CD.Eliminar(idDetalle)
                ConsultaDetalles()
                NuevoConcepto()
                PopUp("Concepto Eliminado", 90)
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DGDetalles_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try
           
            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbGastosDetalles(idDetalle, MySqlcon)

            cmbClasificacion.SelectedIndex = IdsClasificacion.Busca(CD.IdClasificacion)
            cmbclasificacion2.SelectedIndex = IdsClasificacion2.Busca(CD.IdClasificacion2)
            cmbclas3.SelectedIndex = IdsClasificacion3.Busca(CD.IdClasificacion3)
            txtDescripcion.Text = CD.descripcion
            txtImporte.Text = CD.precio
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button9.Enabled = True
            Button4.Text = "Modificar Concepto"

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub frmGastos_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub Modificar(ByVal pEstado As Byte)
        Try
            Dim MensajeError As String = ""
            Dim C As New dbGastos(MySqlcon)
            Dim CD As New dbGastosDetalles(MySqlcon)
           

            If IsNumeric(txtFolio.Text) = False Then MensajeError = "El folio debe ser un valor numérico."
            If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosAlta, PermisosN.Secciones.Gastos) = False And pEstado <> Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosCancelar, PermisosN.Secciones.Gastos) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If

            If GlobalconEmpenios Then
                Dim Em As New dbEmpenios(MySqlcon)
                If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosPermitirCambioFecha, PermisosN.Secciones.Gastos) = False And dtpFecha.Value.ToString("yyyy/MM/dd") < Em.DaUltimaFecha() Then
                    MensajeError += " Fecha incorrecta. Revise que la fecha de su computadora este correcta."
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosPermitirCambioFecha, PermisosN.Secciones.Gastos) = False And dtpFecha.Value.ToString("yyyy/MM/dd") < C.DaUltimaFecha() Then
                    MensajeError += " Fecha incorrecta. Revise que la fecha de su computadora este correcta."
                End If
            End If

            If MensajeError = "" Then
                If C.ChecaFolioRepetido(txtFolio.Text, txtSerie.Text) Then
                    If pEstado = Estados.Guardada Then txtFolio.Text = C.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
                End If
                CD.DaTotal(idMovimiento)
                C.Modificar(idMovimiento, idCliente, Format(dtpFecha.Value, "yyyy/MM/dd"), txtFolio.Text, total, IdsSucursales.Valor(ComboBox3.SelectedIndex), txtSerie.Text, IdsCajas.Valor(cmbCaja.SelectedIndex), IdsVendedores.Valor(cmbVendedor.SelectedIndex), IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), txtComentario.Text, pEstado)
                Estado = pEstado
                If pEstado = Estados.Guardada Then
                    Dim Ca As New dbCajas(MySqlcon)
                    'If ComboBox4.SelectedIndex = 0 Then
                    '    Ca.MovimientodeCaja(C.IdCaja, C.TotalVenta)
                    'Else
                    Ca.MovimientodeCaja(C.IdCaja, total * -1)
                    'End If
                    '  Imprimir()
                    Button21.Enabled = True
                End If

                If pEstado = Estados.Cancelada Then
                    Dim Ca As New dbCajas(MySqlcon)
                    'If ComboBox4.SelectedIndex = 0 Then
                    Ca.MovimientodeCaja(C.IdCaja, total)
                    'Else
                    '    Ca.MovimientodeCaja(C.IdCaja, C.TotalVenta)
                    'End If
                    Button21.Enabled = False
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
                    Button13.Enabled = False
                    Button2.Enabled = False
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
                Dim c As New dbGastos(MySqlcon)
                c.EliminarGasto(idMovimiento)
                Nuevo()
            End If
        Else
            Nuevo()
        End If
    End Sub
    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
        Imprimir()
        nuevo()
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        BotonNuevo()
    End Sub
    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(txtComentario.Text, 1000, True)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtComentario.Text = et.Texto
        End If
    End Sub
    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbGastos(MySqlcon)
                V.ActualizaComentario(idMovimiento, txtComentario.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Modificar(Estados.Pendiente)
        nuevo()
    End Sub
    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If MsgBox("¿Cancelar este movimiento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Modificar(Estados.Cancelada)
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar este gasto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosAlta, PermisosN.Secciones.Gastos) = True Then
                Dim C As New dbGastos(MySqlcon)
                C.EliminarGasto(idMovimiento)
                PopUp("Gasto Eliminada", 90)
                nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim f As New frmGastosConsulta(ModosDeBusqueda.Secundario)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idMovimiento = f.IdCompra
            LlenaDatosVenta()
        End If
        f.Dispose()
    End Sub
    Private Sub LlenaDatosVenta() 'aqui me quede
        Dim C As New dbGastos(idMovimiento, MySqlcon)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        txtFolio.Text = C.Folio.ToString
        FolioAnt = C.Folio
        txtSerie.Text = C.Serie
        'TextBox1.Text = C.Cliente.Clave
        Estado = C.Estado
        'TextBox8.Text = C.Iva.ToString
        'If C.Desglosar = 1 Then
        '    CheckBox1.Checked = True
        'Else
        '    CheckBox1.Checked = False
        'End If

        'ComboBox4.SelectedIndex = C.Tipo
        Button2.Enabled = True
        dtpFecha.Value = C.Fecha
        txtComentario.Text = C.Comentario

        cmbCaja.SelectedIndex = IdsCajas.Busca(C.IdCaja)
        'ComboBox4.Enabled = False
        ConsultaDetalles()
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                cmbCaja.Enabled = False
                Button13.Enabled = False
                Panel1.Enabled = False
                Panel2.Enabled = False
                Button4.Enabled = False
                Button8.Enabled = False
                Button21.Enabled = True
                Button5.Enabled = False
                Button9.Enabled = False
                Button2.Enabled = False
                Button13.Enabled = False
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Panel1.Enabled = False
                Panel2.Enabled = False
                cmbCaja.Enabled = False
                Button4.Enabled = False
                Button8.Enabled = False
                Button21.Enabled = True
                Button5.Enabled = False
                Button9.Enabled = False
                Button2.Enabled = False
                Button13.Enabled = True
            Case Else
                Button9.Enabled = True
                Button5.Enabled = True
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                cmbCaja.Enabled = True
                Button4.Enabled = True
                Button8.Enabled = True
                Button13.Enabled = False
                Button2.Enabled = True
                Enabled = True
        End Select

    End Sub
    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Imprimir()
    End Sub
    Private Sub Imprimir()
        If Estado = Estados.Cancelada Or Estado = Estados.Guardada Then
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Rep = New repGastos
            Rep.SetDataSource(generaTabla())
            Dim SA As New dbSucursalesArchivos()
            Dim RutaPDF As String

            Dim Archivos As New dbSucursalesArchivos

            RutaPDF = SA.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, False)
            IO.Directory.CreateDirectory(RutaPDF + "\" + Date.Now.Year.ToString + "\") 'Modificado
            IO.Directory.CreateDirectory(RutaPDF + "\" + Date.Now.Year.ToString + "\" + Date.Now.Month.ToString("00")) 'Modificado
            RutaPDF = RutaPDF + "\" + Date.Now.Year.ToString + Date.Now.Month.ToString("00") 'Modificado
            Dim Impresora As String = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.Gastos) 'Modificado



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
            Dim PrintLayout As New CrystalDecisions.Shared.PrintLayoutSettings
            PrintLayout.Scaling = CrystalDecisions.Shared.PrintLayoutSettings.PrintScaling.Scale
            'PrintLayout.Centered = True
            Dim PS As New System.Drawing.Printing.PrinterSettings
            PS.PrinterName = Impresora
            Dim pageSettings As New System.Drawing.Printing.PageSettings(PS)
            Rep.PrintOptions.PrinterName = Impresora
            'Rep.PrintOptions.DissociatePageSizeAndPrinterPaperSize = True
            Rep.PrintToPrinter(PS, pageSettings, False, PrintLayout)
            'Dim RV As New frmReportes(Rep, False)
            'RV.Show()
        Else
            MsgBox("Debe guardar o cancelar el gasto antes de imprimir.", MsgBoxStyle.OkOnly, GlobalNombreApp)
        End If
    End Sub
    Private Function generaTabla() As DataSet
        Dim TablaFull As New DataTable
        Dim C As New dbGastos(idMovimiento, MySqlcon)
        LlenaDatosVenta()

        TablaFull.Columns.Add("fecha")
        TablaFull.Columns.Add("sucursal")
        TablaFull.Columns.Add("caja")
        TablaFull.Columns.Add("serieFolio")
        TablaFull.Columns.Add("vendedor")
        TablaFull.Columns.Add("comentario")
        TablaFull.Columns.Add("idMovimiento")
        TablaFull.Columns.Add("descripcion")
        TablaFull.Columns.Add("precio")
        TablaFull.Columns.Add("clasificacion")

        For i As Integer = 0 To DGDetalles.Rows.Count - 1
            Dim dr As DataRow

            dr = TablaFull.NewRow()

            dr("fecha") = C.Fecha
            dr("sucursal") = ComboBox3.Text
            dr("caja") = cmbCaja.Text
            dr("serieFolio") = (txtSerie.Text + txtFolio.Text)
            dr("vendedor") = cmbVendedor.Text
            dr("comentario") = txtComentario.Text
            dr("idMovimiento") = DGDetalles.Item(0, i).Value
            dr("descripcion") = DGDetalles.Item(1, i).Value
            dr("precio") = DGDetalles.Item(2, i).Value
            dr("clasificacion") = C.buscaClasificacion(Integer.Parse(DGDetalles.Item(3, i).Value))

            TablaFull.Rows.Add(dr)

        Next
        Dim dataSet As DataSet = New DataSet
        dataSet.Tables.Add(TablaFull)
        dataSet.WriteXmlSchema("tblGastos.xml")
        Return dataSet
    End Function
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If Estado = Estados.Inicio Then
            Guardar()
        End If
        Dim gastos As New frmGastosSalarioEmpleados(idMovimiento, IdsClasificacion.Valor(cmbClasificacion.SelectedIndex), IdsClasificacion2.Valor(cmbclasificacion2.SelectedIndex), IdsClasificacion3.Valor(cmbclas3.SelectedIndex))
        gastos.ShowDialog()

        ConsultaDetalles()


    End Sub
    Private Sub txtImporte_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporte.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            Button4.Focus()
            BotonAgregar()
        Else
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
                e.Handled = True
            End If
        End If

    End Sub
    Private Sub cmbClasificacion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbClasificacion.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbclasificacion2.Focus()
        End If
    End Sub
    Private Sub cmbClasificacion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbClasificacion.KeyPress

    End Sub
    Private Sub txtDescripcion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescripcion.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtImporte.Focus()
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
    Private Sub cmbClasificacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClasificacion.SelectedIndexChanged
        LlenaCombos("tblgastosclasificacion2", cmbclasificacion2, "concat(clave,' ',nombre)", "nombret", "idclasificacion", IdsClasificacion2, "idclassuperior=" + IdsClasificacion.Valor(cmbClasificacion.SelectedIndex).ToString, "GENERAL", "clave")
    End Sub
    Private Sub cmbclasificacion2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbclasificacion2.KeyDown
        
        If e.KeyCode = Keys.Enter Then
            cmbclas3.Focus()
        End If
    End Sub
    Private Sub cmbclasificacion2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbclasificacion2.SelectedIndexChanged
        LlenaCombos("tblgastosclasificacion3", cmbclas3, "concat(clave,' ',nombre)", "nombret", "idclasificacion", IdsClasificacion3, "idclassuperior=" + IdsClasificacion2.Valor(cmbclasificacion2.SelectedIndex).ToString, "GENERAL", "clave")
    End Sub
    Private Sub cmbclas3_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cmbclas3.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtDescripcion.Focus()
        End If
    End Sub
    Private Sub cmbclas3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbclas3.SelectedIndexChanged
    End Sub
End Class