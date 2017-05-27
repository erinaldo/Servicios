Public Class frmClientesDocumentos
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim idCliente As Integer

    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim IdMoneda As Integer
  
    Dim IdsSucursales As New elemento

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub frmClientesDocumentos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F10 And btnGuardar.Enabled = True Then
            CerrarPendientes(Estados.Guardada)
            LimpiarTodo()
            PopUp("Documentos Guardados", 90)
        End If
    End Sub

    Private Sub frmCapturaDocumentosCliente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.Icon = GlobalIcono
        Catch ex As Exception
        End Try
        dgvDocumentos.AutoGenerateColumns = False
        ConsultaOn = False
        cmbTipo.Items.Add("Saldo Inicial")
        cmbTipo.Items.Add("Documento")
        cmbTipo.SelectedIndex = 0
        LlenaCombos("tblsucursales", cmbSucursal, "nombre", "nombret", "idsucursal", IdsSucursales)
        cmbSucursal.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        LlenaCombos("tblmonedas", cmbMoneda, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", cmbMonedaGral, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
        Dim CM As New dbMonedasConversiones(1, MySqlcon)
        txtTipoCambio.Text = CM.Cantidad.ToString
        ConsultaOn = True
        Nuevo(True)
        pnlDatos.Enabled = False
        btnAgregar.Enabled = False
        'Consulta()

    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim r As New DataGridViewRow
                Dim suma As Double = 0
                For Each r In dgvDocumentos.Rows
                    suma += r.Cells(colImporte.Index).Value
                Next
                lblTotal.Text = Format(suma, "N2")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Nuevo(ByVal pClienteNuevo As Boolean)
        txtSerieReferencia.Text = ""
        txtImporte.Text = ""
        cmbMoneda.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        btnAgregar.Text = "Agregar Documento"
        pnlCliente.Enabled = True
        pnlDatos.Enabled = True
        btnAgregar.Enabled = True
        btnEliminar.Enabled = False
        dtpFecha.Value = Date.Now
        If cmbTipo.SelectedIndex = 0 Then
            txtFolioReferencia.Text = "0"
            If pClienteNuevo Then
                txtCliente.Text = ""
                txtCliente.Focus()
            End If
        Else
            txtFolioReferencia.Text = ""
            dtpFecha.Focus()
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
            cmbSucursal.Enabled = False
        End If
    End Sub

    Public Sub LimpiarTodo()
        dtpFecha.Value = Date.Now
        txtCliente.Text = ""
        dtpFecha.Value = Date.Now
        dgvDocumentos.DataSource = Nothing
        txtFolioReferencia.BackColor = Color.FromKnownColor(KnownColor.Window)
        txtFolioReferencia.Text = ""
        Dim CM As New dbMonedasConversiones(1, MySqlcon)
        lblTotal.Text = "0"
        txtTipoCambio.Text = CM.Cantidad.ToString
        txtImporte.Text = ""
        txtFolioReferencia.Text = ""
        pnlCliente.Enabled = True
        btnEliminar.Enabled = False
        cmbMoneda.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        txtTipoCambio.Text = CM.Cantidad.ToString
        btnAgregar.Text = "Agregar Documento"
        btnGuardar.Enabled = False
        pnlDatos.Enabled = False
        btnAgregar.Enabled = False
        txtCliente.Focus()
    End Sub
    Private Sub ChecaPendientes()
        Dim R As Integer = 0
        While R < dgvDocumentos.RowCount
            If dgvDocumentos.Item("colSestado", R).Value = "Pendiente" Then
                btnGuardar.Enabled = True
                Exit While
            End If
            R += 1
        End While
    End Sub
    Private Sub txtCliente_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCliente.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbTipo.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            BotonCliente()
        End If
    End Sub

    Private Sub txtCliente_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCliente.TextChanged
        BuscaCliente()
        'Consulta()
    End Sub

    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbClientes(MySqlcon)
                If c.BuscaCliente(txtCliente.Text) Then
                    txtDatosCliente.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                    txtDatosCliente2.Text = "Límite: " + Format(c.Credito, "#,##0.00") + vbCrLf + "Días: " + c.CreditoDias.ToString + vbCrLf + "Saldo: " + Format(c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
                    idCliente = c.ID
                    Consulta()
                    pnlDatos.Enabled = True
                    btnAgregar.Enabled = True
                Else
                    txtDatosCliente.Text = ""
                    txtDatosCliente2.Text = ""
                    idCliente = 0
                    Nuevo(False)
                    dgvDocumentos.DataSource = Nothing
                    btnGuardar.Enabled = False
                    pnlDatos.Enabled = False
                    btnAgregar.Enabled = False
                End If

                'btnEliminar.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub CerrarPendientes(ByVal pEstado As Byte)
        Try
            Dim C As New dbCapturaDocumentosClientes(MySqlcon)
            C.CerrarPendientes(idCliente)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Consulta()
        Try
            If idCliente = 0 Then
                dgvDocumentos.DataSource = Nothing
            Else
                Dim CD As New dbCapturaDocumentosClientes(MySqlcon)
                dgvDocumentos.DataSource = CD.Consulta(idCliente, "", If(chkMostrarGuardades.Checked, 0, Estados.Pendiente))
            End If
            If dgvDocumentos.RowCount > dgvDocumentos.DisplayedRowCount(False) Then dgvDocumentos.FirstDisplayedScrollingRowIndex = dgvDocumentos.RowCount - dgvDocumentos.DisplayedRowCount(False)
            ChecaPendientes()
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbCapturaDocumentosClientes(MySqlcon)
            If IsNumeric(txtFolioReferencia.Text) = False And cmbTipo.SelectedIndex = 1 Then Throw New Exception("El folio debe ser un valor numérico.")
            If Not IsNumeric(txtImporte.Text) Then Throw New Exception("El importe debe ser un valor numérico.")
            If CDbl(txtImporte.Text) <= 0 Then Throw New Exception("El importe debe ser un valor mayor a 0.")
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.DocumentosClientesAlta, PermisosN.Secciones.Ventas) = False Then
                Throw New Exception("No tiene permiso para realizar esta operación.")
            End If
            If btnAgregar.Text = "Agregar Documento" Then
                If cmbTipo.SelectedIndex = 0 Then
                    CD.ChecaFolioRepetido(txtFolioReferencia.Text, txtSerieReferencia.Text, cmbTipo.SelectedIndex, idCliente)
                    CD.Guardar(idCliente, Format(dtpFecha.Value, "yyyy/MM/dd"), Estados.Guardada, txtImporte.Text, IdsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, CDbl(txtTipoCambio.Text), IDsMonedas.Valor(cmbMoneda.SelectedIndex), txtFolioReferencia.Text, txtSerieReferencia.Text, 2, cmbTipo.SelectedIndex)
                Else
                    CD.ChecaFolioRepetido(txtFolioReferencia.Text, txtSerieReferencia.Text, cmbTipo.SelectedIndex, idCliente)
                    CD.Guardar(idCliente, Format(dtpFecha.Value, "yyyy/MM/dd"), Estados.Pendiente, txtImporte.Text, IdsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, CDbl(txtTipoCambio.Text), IDsMonedas.Valor(cmbMoneda.SelectedIndex), txtFolioReferencia.Text, txtSerieReferencia.Text, 2, cmbTipo.SelectedIndex)
                End If
            Else
                CD.ChecaFolioRepetido(txtFolioReferencia.Text, txtSerieReferencia.Text, cmbTipo.SelectedIndex, idCliente, dgvDocumentos.Item(colId.Index, dgvDocumentos.CurrentCell.RowIndex).Value)
                CD.Modificar(dgvDocumentos.Item(colId.Index, dgvDocumentos.CurrentCell.RowIndex).Value, idCliente, Estados.Pendiente, Format(dtpFecha.Value, "yyyy/MM/dd"), txtImporte.Text, IdsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, txtTipoCambio.Text, IDsMonedas.Valor(cmbMoneda.SelectedIndex), txtFolioReferencia.Text, txtSerieReferencia.Text, 2, cmbTipo.SelectedIndex)
            End If
            Consulta()
            Nuevo(True)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        BotonAgregar()
    End Sub
    Private Sub BotonAgregar()

        AgregaArticulo()
    End Sub
    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDocumentos.CellClick
        LlenaDatosDocumento()
    End Sub

    Private Sub LlenaDatosDocumento()
        Try
            Dim CD As New dbCapturaDocumentosClientes(dgvDocumentos.Item(0, dgvDocumentos.CurrentCell.RowIndex).Value, MySqlcon)
            'txtCliente.Text = CD.Cliente.Clave
            'BuscaCliente()
            cmbTipo.SelectedIndex = CD.TipoSaldo
            dtpFecha.Value = CD.Fecha
            txtSerieReferencia.Text = CD.SerieReferencia
            txtFolioReferencia.Text = CD.FolioReferencia
            txtImporte.Text = CD.TotalaPagar.ToString
            cmbMoneda.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            If CD.Estado = 2 Then
                btnEliminar.Text = "Eliminar Documento"
                btnAgregar.Enabled = True
                pnlDatos.Enabled = True
            Else
                btnEliminar.Text = "Cancelar Documento"
                btnAgregar.Enabled = False
                pnlDatos.Enabled = False
            End If
            btnEliminar.Enabled = True
            btnAgregar.Text = "Modificar Documento"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Nuevo(True)
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            
            If btnEliminar.Text = "Eliminar Documento" Then
                If MsgBox("¿Desea eliminar este documento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbCapturaDocumentosClientes(MySqlcon)
                    CD.Eliminar(dgvDocumentos.Item(colId.Index, dgvDocumentos.CurrentCell.RowIndex).Value)
                    Consulta()
                    Nuevo(True)
                    PopUp("Documento Eliminado", 90)
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.DocumentosClientesCancelar, PermisosN.Secciones.Ventas) = False Then
                    Throw New Exception("No tiene permisos para realizar esta operación.")
                End If
                If MsgBox("¿Desea cancelar este documento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

                    Dim VP As New dbVentasPagos(MySqlcon)
                    If VP.HayPagosDocumentos(dgvDocumentos.Item(colId.Index, dgvDocumentos.CurrentCell.RowIndex).Value) = 0 Then
                        Dim CD As New dbCapturaDocumentosClientes(MySqlcon)
                        CD.Modificar(dgvDocumentos.Item(colId.Index, dgvDocumentos.CurrentCell.RowIndex).Value, idCliente, Estados.Cancelada, Format(dtpFecha.Value, "yyyy/MM/dd"), txtImporte.Text, IdsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, txtTipoCambio.Text, IDsMonedas.Valor(cmbMoneda.SelectedIndex), txtFolioReferencia.Text, txtSerieReferencia.Text, 2, cmbTipo.SelectedIndex)
                        Consulta()
                        Nuevo(True)
                        PopUp("Documento Cancelado", 90)
                    Else
                        MsgBox("Para poder cancelar este documento primero debe cancelar todos los pagos.", MsgBoxStyle.Information, GlobalNombreApp)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        BotonCliente()
    End Sub
    Private Sub BotonCliente()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtDatosCliente.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.CP

            txtDatosCliente2.Text = "Límite: " + Format(B.Cliente.Credito, "N2") + vbCrLf + "Días: " + B.Cliente.CreditoDias.ToString + vbCrLf + "Saldo: " + Format(B.Cliente.DaSaldoAFecha(B.Cliente.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")

            idCliente = B.Cliente.ID
            ConsultaOn = False
            txtCliente.Text = B.Cliente.Clave
            ConsultaOn = True
            pnlDatos.Enabled = True
            btnAgregar.Enabled = True
            Consulta()
            cmbTipo.Focus()
        End If
    End Sub
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub cmbMonedaGral_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMonedaGral.SelectedIndexChanged
        SacaTotal()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        CerrarPendientes(Estados.Guardada)
        LimpiarTodo()
        PopUp("Documentos Guardados", 90)
    End Sub

    Private Sub cmbSucursal_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSucursal.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCliente.Focus()
        End If
    End Sub

    Private Sub cmbTipo_KeyDown1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbTipo.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpFecha.Focus()
        End If
    End Sub

    Private Sub cmbTipo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipo.LostFocus
        cmbTipo.BackColor = Color.FromKnownColor(KnownColor.Window)
    End Sub

    Private Sub cmbTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged
        If cmbTipo.SelectedIndex = 0 Then
            lblSerieReferencia.Visible = False
            lblFolioReferencia.Visible = False
            lblReferencia.Visible = False
            txtSerieReferencia.Visible = False
            txtFolioReferencia.Visible = False
            txtSerieReferencia.Text = ""
            txtFolioReferencia.Text = 0
        Else
            lblSerieReferencia.Visible = True
            lblFolioReferencia.Visible = True
            lblReferencia.Visible = True
            txtSerieReferencia.Visible = True
            txtFolioReferencia.Visible = True
            txtSerieReferencia.Text = ""
            txtFolioReferencia.Text = ""
        End If
    End Sub


    Private Sub chkMostrarGuardades_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMostrarGuardades.CheckedChanged
        Consulta()
    End Sub

    Private Sub txtImporte_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtImporte.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cmbTipo.SelectedIndex = 1 Then
                BotonAgregar()
            Else
                btnAgregar.Focus()
            End If
        End If
    End Sub

    Private Sub txtImporte_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporte.TextChanged

    End Sub

    Private Sub cmbMoneda_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbMoneda.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cmbTipo.SelectedIndex = 1 Then
                BotonAgregar()
            Else
                btnAgregar.Focus()
            End If
        End If
    End Sub

    Private Sub cmbMoneda_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMoneda.SelectedIndexChanged

    End Sub

    Private Sub dtpFecha_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpFecha.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cmbTipo.SelectedIndex = 1 Then
                txtSerieReferencia.Focus()
            Else
                txtImporte.Focus()
            End If
        End If
    End Sub

    Private Sub dtpFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged

    End Sub

    Private Sub txtSerieReferencia_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSerieReferencia.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFolioReferencia.Focus()
        End If
    End Sub

    Private Sub txtSerieReferencia_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerieReferencia.TextChanged

    End Sub

    Private Sub txtFolioReferencia_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFolioReferencia.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtImporte.Focus()
        End If
    End Sub

    Private Sub txtFolioReferencia_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolioReferencia.TextChanged
       
    End Sub

    Private Sub txtTipoCambio_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTipoCambio.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCliente.Focus()
        End If
    End Sub

    Private Sub txtTipoCambio_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTipoCambio.TextChanged

    End Sub

    Private Sub dgvDocumentos_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDocumentos.CellContentClick

    End Sub
End Class