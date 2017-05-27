Public Class frmAddendaWalmart
    Private IdVenta As Integer
    Public addenda As AddendaWalmart
    Public Sub New(idventa As Integer)
        InitializeComponent()
        Me.IdVenta = idventa
    End Sub
    Private Sub frmAddendaWalmart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvArticulos.AutoGenerateColumns = False

        Dim db As New dbAddendaWalmart(MySqlcon)
        addenda = db.Buscar(IdVenta)

        txtFolio.Text = addenda.Folio
        txtSerie.Text = addenda.Serie
        dtpFecha.Value = addenda.Fecha
        txtUUID.Text = addenda.UUID
        If addenda.FolioOrden IsNot Nothing Then txtFolioOrdenCompra.Text = addenda.FolioOrden
        dtpFechaOrdenCompra.Value = addenda.FechaOrden
        If addenda.FolioRecibo IsNot Nothing Then txtFolioRecibo.Text = addenda.FolioRecibo
        dtpFechaRecibo.Value = addenda.FechaRecibo
        txtSubtotal.Text = Format(addenda.SubTotal, "C2")
        txtIva.Text = Format(addenda.Iva, "C2")
        txtIeps.Text = Format(addenda.Ieps, "C2")
        txtTotal.Text = Format(addenda.Total, "C2")

        txtNumeroProveedor.Text = addenda.NumeroProveedor
        txtNombreProveedor.Text = addenda.NombreProveedor
        txtCalleYNumeroProveedor.Text = addenda.CalleYNumeroProveedor
        txtColoniaProveedor.Text = addenda.ColoniaProveedor
        txtLocalidadProveedor.Text = addenda.LocalidadProveedor
        txtEstadoProveedor.Text = addenda.EstadoProveedor
        txtCodigoPostalProveedor.Text = addenda.CodigoPostalProveedor
        txtRfcProveedor.Text = addenda.RfcProveedor
        txtCedulaIEPS.Text = addenda.CedulaIEPS

        txtClaveComprador.Text = addenda.ClaveComprador
        txtNombreComprador.Text = addenda.NombreComprador
        txtCalleYNumeroComprador.Text = addenda.CalleYNumeroComprador
        txtColoniaComprador.Text = addenda.ColoniaComprador
        txtLocalidadComprador.Text = addenda.LocalidadComprador
        txtEstadoComprador.Text = addenda.EstadoComprador
        txtCodigoPostalComprador.Text = addenda.CodigoPostalComprador
        txtRfcComprador.Text = addenda.RfcComprador
        txtMoneda.Text = addenda.Moneda
        txtTasaIva.Text = addenda.TasaIva
        txtDiasCredito.Text = addenda.DiasCredito

        txtClaveLugarEntrega.Text = addenda.ClaveLugarEntrega
        txtNombreLugarEntrega.Text = addenda.NombreLugarEntrega
        txtCalleYNumeroLugarEntrega.Text = addenda.CalleYNumeroLugarEntrega
        txtColoniaLugarEntrega.Text = addenda.ColoniaLugarEntrega
        txtLocalidadLugarEntrega.Text = addenda.LocalidadLugarEntrega
        txtEstadoLugarEntrega.Text = addenda.EstadoLugarEntrega
        txtCodigoPostalLugarEntrega.Text = addenda.CodigoPostalLugarEntrega

        dgvArticulos.DataSource = addenda.Articulos
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            Me.ValidateChildren()
            If Not IsNumeric(txtFolioOrdenCompra.Text) And txtFolioOrdenCompra.Text <> "" Then
                TabControl1.SelectedIndex = 0
                txtFolioOrdenCompra.Focus()
            ElseIf Not IsNumeric(txtFolioRecibo.Text) And txtFolioRecibo.Text <> "" Then
                TabControl1.SelectedIndex = 0
                txtFolioRecibo.Focus()
            ElseIf txtFolioOrdenCompra.Text = "" And txtFolioRecibo.Text = "" Then
                TabControl1.SelectedIndex = 0
                txtFolioOrdenCompra.Focus()
            ElseIf txtFolioOrdenCompra.Text <> "" And txtFolioRecibo.Text <> "" Then
                TabControl1.SelectedIndex = 0
                txtFolioOrdenCompra.Focus()
            ElseIf txtUUID.Text = "" Then
                TabControl1.SelectedIndex = 0
                txtUUID.Focus()
            ElseIf txtClaveLugarEntrega.Text = "" Then
                TabControl1.SelectedIndex = 3
                txtClaveLugarEntrega.Focus()
            ElseIf txtNombreLugarEntrega.Text = "" Then
                TabControl1.SelectedIndex = 3
                txtNombreLugarEntrega.Focus()
            Else
                If IsNumeric(txtFolioOrdenCompra.Text) Then addenda.FolioOrden = txtFolioOrdenCompra.Text
                addenda.FechaOrden = dtpFechaOrdenCompra.Value
                If IsNumeric(txtFolioRecibo.Text) Then addenda.FolioRecibo = txtFolioRecibo.Text
                addenda.FechaRecibo = dtpFechaRecibo.Value
                addenda.ClaveLugarEntrega = txtClaveLugarEntrega.Text
                addenda.NombreLugarEntrega = txtNombreLugarEntrega.Text
                addenda.CedulaIEPS = txtCedulaIEPS.Text

                Dim db As New dbAddendaWalmart(MySqlcon)
                db.Guardar(addenda)

                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtClaveLugarEntrega_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtClaveLugarEntrega.Validating, txtUUID.Validating, txtTotal.Validating, txtTasaIva.Validating, txtSubtotal.Validating, txtSerie.Validating, txtRfcProveedor.Validating, txtRfcComprador.Validating, txtNumeroProveedor.Validating, txtNombreProveedor.Validating, txtNombreLugarEntrega.Validating, txtNombreComprador.Validating, txtMoneda.Validating, txtLocalidadProveedor.Validating, txtLocalidadLugarEntrega.Validating, txtLocalidadComprador.Validating, txtIva.Validating, txtFolio.Validating, txtEstadoProveedor.Validating, txtEstadoLugarEntrega.Validating, txtEstadoComprador.Validating, txtDiasCredito.Validating, txtColoniaProveedor.Validating, txtColoniaLugarEntrega.Validating, txtColoniaComprador.Validating, txtCodigoPostalProveedor.Validating, txtCodigoPostalComprador.Validating, txtCodigoPostalLugarEntrega.Validating, txtClaveComprador.Validating, txtCalleYNumeroProveedor.Validating, txtCalleYNumeroLugarEntrega.Validating, txtCalleYNumeroComprador.Validating
        If sender.Text = "" Then
            ErrorProvider1.SetError(sender, "Proporcione un valor válido.")
        Else
            ErrorProvider1.SetError(sender, "")
        End If
    End Sub

    Private Sub txtFolioOrdenCompra_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFolioOrdenCompra.Validating
        If Not IsNumeric(sender.Text) And sender.Text <> "" Then
            ErrorProvider1.SetError(sender, "Proporcione un valor válido.")
        ElseIf sender.Text = "" And txtFolioRecibo.Text = "" Then
            ErrorProvider1.SetError(sender, "Proporcione un folio de orden de compra o folio de recibo.")
        ElseIf sender.Text <> "" And txtFolioRecibo.Text <> "" Then
            ErrorProvider1.SetError(sender, "Proporcione solo un folio de orden o folio de recibo.")
        Else
            ErrorProvider1.SetError(sender, "")
        End If
    End Sub

   
    Private Sub txtFolioRecibo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFolioRecibo.Validating
        If Not IsNumeric(sender.Text) And sender.Text <> "" Then
            ErrorProvider1.SetError(sender, "Proporcione un valor válido.")
        Else
            ErrorProvider1.SetError(sender, "")
        End If
    End Sub
End Class