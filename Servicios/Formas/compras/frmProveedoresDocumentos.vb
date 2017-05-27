Imports System.Windows.Forms

Public Class frmProveedoresDocumentos

    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim idProveedor As Integer

    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim IdMoneda As Integer

    Dim IdsSucursales As New elemento

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub frmProveedoressDocumentos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F10 And btnGuardar.Enabled = True Then
            CerrarPendientes(Estados.Guardada)
            LimpiarTodo()
            PopUp("Documentos Guardados", 90)
        End If
    End Sub

    Private Sub frmCapturaDocumentosProveedores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception
        End Try
        dgvDocumentos.AutoGenerateColumns = False
        ConsultaOn = False
        cmbTipo.Items.Add("Saldo Inicial")
        cmbTipo.Items.Add("Documento")
        cmbTipo.SelectedIndex = 1
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

    Private Sub Nuevo(ByVal pProveedorNuevo As Boolean)
        txtSerieReferencia.Text = ""
        txtImporte.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = "16"
        TextBox3.Text = "0"
        TextBox4.Text = "0"
        dtpFecha.Value = Date.Now
        cmbMoneda.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        btnAgregar.Text = "Agregar Documento"
        pnlProveedor.Enabled = True
        pnlDatos.Enabled = True
        btnAgregar.Enabled = True
        btnEliminar.Enabled = False
        If cmbTipo.SelectedIndex = 0 Then
            txtFolioReferencia.Text = "0"
            If pProveedorNuevo Then
                txtProveedor.Text = ""
                txtProveedor.Focus()
            End If
        Else
            txtFolioReferencia.Text = ""
            dtpFecha.Focus()
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CambioSucursal, PermisosN.Secciones.Compras) = False Then
            cmbSucursal.Enabled = False
        End If
    End Sub

    Public Sub LimpiarTodo()
        dtpFecha.Value = Date.Now
        txtProveedor.Text = ""
        dtpFecha.Value = Date.Now
        dgvDocumentos.DataSource = Nothing
        txtFolioReferencia.BackColor = Color.FromKnownColor(KnownColor.Window)
        txtFolioReferencia.Text = ""
        Dim CM As New dbMonedasConversiones(1, MySqlcon)
        lblTotal.Text = "0"
        txtTipoCambio.Text = CM.Cantidad.ToString
        txtImporte.Text = ""
        txtFolioReferencia.Text = ""
        pnlProveedor.Enabled = True
        btnEliminar.Enabled = False
        cmbMoneda.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        txtTipoCambio.Text = CM.Cantidad.ToString
        btnAgregar.Text = "Agregar Documento"
        btnGuardar.Enabled = False
        pnlDatos.Enabled = False
        btnAgregar.Enabled = False
        txtProveedor.Focus()
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
    Private Sub txtProveedor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProveedor.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbTipo.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            BotonProveedor()
        End If
    End Sub

    Private Sub txtProveedor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProveedor.TextChanged
        BuscaProveedor()
        'Consulta()
    End Sub

    Private Sub BuscaProveedor()
        Try
            If ConsultaOn Then
                Dim c As New dbproveedores(MySqlcon)
                If c.BuscaProveedor(txtProveedor.Text) Then
                    txtDatosProveedor.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                    txtDatosProveedor2.Text = "Límite: " + Format(c.LimiteCredito, "#,##0.00") + vbCrLf + "Días: " + c.DiasCredito.ToString
                    idProveedor = c.ID
                    Consulta()
                    pnlDatos.Enabled = True
                    btnAgregar.Enabled = True
                Else
                    txtDatosProveedor.Text = ""
                    txtDatosProveedor2.Text = ""
                    idProveedor = 0
                    Nuevo(False)
                    dgvDocumentos.DataSource = Nothing
                    btnGuardar.Enabled = False
                    pnlDatos.Enabled = False
                    btnAgregar.Enabled = False
                End If
                
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub CerrarPendientes(ByVal pEstado As Byte)
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DocumentosProveedoresAlta, PermisosN.Secciones.Compras) = False Then Throw New Exception("No tiene permiso para realizar esta operación.")
            Dim C As New dbCapturaDocumentosProveedores(MySqlcon)
            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            Dim Ids As New Collection
            DR = C.DaPendientes(idProveedor)
            While DR.Read
                Ids.Add(DR("iddocumento"))
            End While
            DR.Close()
            For Each id As Integer In Ids
                C.CerrarPendiente(id)
                GeneraPoliza(id)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Consulta()
        Try
            If idProveedor = 0 Then
                dgvDocumentos.DataSource = Nothing
            Else
                Dim CD As New dbCapturaDocumentosProveedores(MySqlcon)
                dgvDocumentos.DataSource = CD.Consulta(idProveedor, "", If(chkMostrarGuardades.Checked, 0, Estados.Pendiente))
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
            Dim CD As New dbCapturaDocumentosProveedores(MySqlcon)
            If IsNumeric(txtFolioReferencia.Text) = False And cmbTipo.SelectedIndex = 1 Then Throw New Exception("El folio debe ser un valor numérico.")
            If Not IsNumeric(txtImporte.Text) Then Throw New Exception("El importe debe ser un valor numérico.")
            If CDbl(txtImporte.Text) <= 0 Then Throw New Exception("El importe debe ser un valor mayor a 0.")
            If Not IsNumeric(TextBox2.Text) Then Throw New Exception("El iva debe ser un valor numérico.")
            If Not IsNumeric(TextBox3.Text) Then Throw New Exception("El ieps debe ser un valor numérico.")
            If Not IsNumeric(TextBox4.Text) Then Throw New Exception("El iva retenico debe ser un valor numérico.")

            If btnAgregar.Text = "Agregar Documento" Then
                If cmbTipo.SelectedIndex = 0 Then
                    CD.ChecaFolioRepetido(txtFolioReferencia.Text, txtSerieReferencia.Text, cmbTipo.SelectedIndex, idProveedor)
                    CD.Guardar(idProveedor, Format(dtpFecha.Value, "yyyy/MM/dd"), Estados.Guardada, txtImporte.Text, IdsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, txtTipoCambio.Text, IDsMonedas.Valor(cmbMoneda.SelectedIndex), txtFolioReferencia.Text, txtSerieReferencia.Text, 2, cmbTipo.SelectedIndex, TextBox1.Text, CDbl(TextBox2.Text), CDbl(TextBox3.Text), CDbl(TextBox4.Text))
                Else
                    CD.ChecaFolioRepetido(txtFolioReferencia.Text, txtSerieReferencia.Text, cmbTipo.SelectedIndex, idProveedor)
                    CD.Guardar(idProveedor, Format(dtpFecha.Value, "yyyy/MM/dd"), Estados.Pendiente, txtImporte.Text, IdsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, txtTipoCambio.Text, IDsMonedas.Valor(cmbMoneda.SelectedIndex), txtFolioReferencia.Text, txtSerieReferencia.Text, 2, cmbTipo.SelectedIndex, TextBox1.Text, CDbl(TextBox2.Text), CDbl(TextBox3.Text), CDbl(TextBox4.Text))
                End If  
            Else
                CD.ChecaFolioRepetido(txtFolioReferencia.Text, txtSerieReferencia.Text, cmbTipo.SelectedIndex, idProveedor, dgvDocumentos.Item(colId.Index, dgvDocumentos.CurrentCell.RowIndex).Value)
                CD.Modificar(dgvDocumentos.Item(colId.Index, dgvDocumentos.CurrentCell.RowIndex).Value, idProveedor, Estados.Pendiente, Format(dtpFecha.Value, "yyyy/MM/dd"), txtImporte.Text, IdsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, txtTipoCambio.Text, IDsMonedas.Valor(cmbMoneda.SelectedIndex), txtFolioReferencia.Text, txtSerieReferencia.Text, 2, cmbTipo.SelectedIndex, TextBox1.Text, CDbl(TextBox2.Text), CDbl(TextBox3.Text), CDbl(TextBox4.Text))
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
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DocumentosProveedoresAlta, PermisosN.Secciones.Compras) = True Then
            AgregaArticulo()
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            txtFolioReferencia.Focus()
        End If
    End Sub
    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDocumentos.CellClick
        LlenaDatosDocumento()
    End Sub

    Private Sub LlenaDatosDocumento()
        Try
            Dim CD As New dbCapturaDocumentosProveedores(dgvDocumentos.Item(0, dgvDocumentos.CurrentCell.RowIndex).Value, MySqlcon)
            'txtProveedor.Text = CD.Proveedor.Clave
            'BuscaProveedor()
            cmbTipo.SelectedIndex = CD.TipoSaldo
            dtpFecha.Value = CD.Fecha
            txtSerieReferencia.Text = CD.SerieReferencia
            txtFolioReferencia.Text = CD.FolioReferencia
            txtImporte.Text = CD.TotalaPagar.ToString
            cmbMoneda.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            TextBox1.Text = CD.Concepto
            TextBox2.Text = CD.Iva.ToString
            TextBox3.Text = CD.IEPS.ToString
            TextBox4.Text = CD.IvaRet.ToString
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
            txtImporte.Focus()
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
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DocumentosProveedoresAlta, PermisosN.Secciones.Compras) = False Then Throw New Exception("No tiene permiso para realizar esta operación.")
                If MsgBox("¿Desea eliminar este documento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbCapturaDocumentosProveedores(MySqlcon)
                    CD.Eliminar(dgvDocumentos.Item(colId.Index, dgvDocumentos.CurrentCell.RowIndex).Value)
                    Consulta()
                    Nuevo(True)
                    PopUp("Documento Eliminado", 90)
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DocumentosProveedoresCancelar, PermisosN.Secciones.Compras) = False Then Throw New Exception("No tiene permiso para realizar esta operación.")
                If MsgBox("¿Desea cancelar este documento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

                    Dim VP As New dbComprasPagos(MySqlcon)
                    If VP.HayPagosDocumentos(dgvDocumentos.Item(colId.Index, dgvDocumentos.CurrentCell.RowIndex).Value) = 0 Then
                        Dim CD As New dbCapturaDocumentosProveedores(MySqlcon)
                        CD.Modificar(dgvDocumentos.Item(colId.Index, dgvDocumentos.CurrentCell.RowIndex).Value, idProveedor, Estados.Cancelada, Format(dtpFecha.Value, "yyyy/MM/dd"), txtImporte.Text, IdsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, txtTipoCambio.Text, IDsMonedas.Valor(cmbMoneda.SelectedIndex), txtFolioReferencia.Text, txtSerieReferencia.Text, 2, cmbTipo.SelectedIndex, TextBox1.Text, CDbl(TextBox2.Text), CDbl(TextBox3.Text), CDbl(TextBox4.Text))
                        Consulta()
                        Nuevo(True)
                        PopUp("Documento Cancelado", 90)
                    Else
                        MsgBox("Para poder cancelar esta nota de cargo primero debe cancelar todos los pagos.", MsgBoxStyle.Information, GlobalNombreApp)
                    End If


                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnBuscarProveedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarProveedor.Click
        BotonProveedor()
    End Sub
    Private Sub BotonProveedor()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtDatosProveedor.Text = B.Proveedor.Nombre + vbCrLf + B.Proveedor.RFC + vbCrLf + B.Proveedor.Direccion + " " + B.Proveedor.NoExterior + " " + B.Proveedor.Ciudad + " " + B.Proveedor.CP

            txtDatosProveedor2.Text = "Límite: " + Format(B.Proveedor.LimiteCredito, "N2") + vbCrLf + "Días: " + B.Proveedor.DiasCredito.ToString + vbCrLf + "Saldo: " + Format(B.Proveedor.DaSaldoAFecha(B.Proveedor.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")

            idProveedor = B.Proveedor.ID
            ConsultaOn = False
            txtProveedor.Text = B.Proveedor.Clave
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
            txtProveedor.Focus()
        End If
    End Sub

    Private Sub cmbTipo_KeyDown1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbTipo.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpFecha.Focus()
        End If
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

    Private Sub cmbSucursal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged

    End Sub

    Private Sub txtTipoCambio_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTipoCambio.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtProveedor.Focus()
        End If
    End Sub

    Private Sub txtTipoCambio_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTipoCambio.TextChanged

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

    Private Sub txtImporte_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtImporte.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub txtImporte_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImporte.TextChanged

    End Sub

    Private Sub cmbMoneda_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbMoneda.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cmbTipo.SelectedIndex = 1 Then
                BotonAgregar()
            Else
                btnAgregar.Focus()
            End If
        End If
    End Sub


    Private Sub GeneraPoliza(pIdDocumento As Integer)
        Try
            Dim Op As New dbOpciones(MySqlcon)
            If Op.IntegrarContabilidad = 1 And GlobalconIntegracion Then
                Dim M As New dbContabilidadMascaras(MySqlcon)
                Dim V As New dbCapturaDocumentosProveedores(pIdDocumento, MySqlcon)
                Dim Canceladas As Byte = 0
                Dim cuantas As Integer
                If V.Estado = Estados.Cancelada Then
                    Canceladas = 1
                End If
                cuantas = M.CuantasHay(12, Canceladas, 3)
                If cuantas > 0 Then
                    'If cuantas = 1 Then
                    '    M.ID = M.DaMascaraActiva(12, Canceladas, 3)
                    'Else
                    'cuantas = M.CuantasHay(12, Canceladas, 3)
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(12, Canceladas, 3)
                    Else
                        Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 12)
                        f.ShowDialog()
                        If f.DialogResult = Windows.Forms.DialogResult.OK Then
                            M.ID = f.IdMascara
                        Else
                            Exit Sub
                        End If
                    End If
                    'End If
                    M.LlenaDatos()
                    Dim GP As dbContabilidadGeneraPolizas
                    If Canceladas = 0 Then
                        GP = New dbContabilidadGeneraPolizas(M, V.Fecha, V.Fecha, V.Fecha)
                    Else
                        GP = New dbContabilidadGeneraPolizas(M, V.FechaCancelado, V.FechaCancelado, V.FechaCancelado)
                    End If
                    GP.GeneraPolizaGeneral(V.ID, V.idproveedor, 1, 0, 0, 0, 0, , , V.Concepto)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = True Then
                        If GP.Exito Then
                            Dim frmp As New frmContabilidadPolizasN(GP.IdPoliza)
                            frmp.ShowDialog()
                            frmp.Dispose()
                        Else
                            MsgBox("No se generó la póliza", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
End Class
