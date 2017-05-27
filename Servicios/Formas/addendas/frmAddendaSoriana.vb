Public Class frmAddendaSoriana
    Private IdVenta As Integer
    Public Property addenda As AddendaSoriana

    Public Sub New(idventa As Integer)
        InitializeComponent()
        Me.IdVenta = idventa
    End Sub
    Private Sub frmAddendaSorianaReverse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvPedidos.AutoGenerateColumns = False
        dgvArticulos.AutoGenerateColumns = False

        Dim db As New dbAddendaSoriana(MySqlcon)

        addenda = db.Buscar(IdVenta)
        txtProveedor.Text = addenda.Proveedor
        txtRemision.Text = addenda.Remision
        dtpFecha.Value = addenda.FechaRemision
        txtTienda.Text = addenda.Tienda
        cmbMoneda.SelectedIndex = addenda.TipoMoneda
        cmbTipoBulto.SelectedIndex = addenda.TipoBulto
        txtSubtotal.Text = Format(addenda.Subtotal, "C2")
        txtIVA.Text = Format(addenda.IVA, "C2")
        txtIEPS.Text = Format(addenda.IEPS, "C2")
        txtOtrosImpuestos.Text = Format(addenda.OtrosImpuestos, "C2")
        txtTotal.Text = Format(addenda.Total, "C2")
        cmbEntregaMercancia.Text = addenda.EntregaMercancia
        txtCantidadBultos.Text = addenda.CantidadBultos
        dtpFechaEntrega.Value = addenda.FechaEntrega
        txtCita.Text = addenda.Cita
        If addenda.FolioNotaEntrada IsNot Nothing Then
            txtFolioNotaEntrada.Text = addenda.FolioNotaEntrada
        End If

        If addenda.Pedimento IsNot Nothing Then
            txtPedimento.Text = addenda.Pedimento.Pedimento
            txtAduana.Text = addenda.Pedimento.Aduana
            txtAgenteAduanal.Text = addenda.Pedimento.AgenteAduanal
            cmbTipoPedimento.Text = addenda.Pedimento.TipoPedimento
            dtpFechaPedimento.Value = addenda.Pedimento.FechaPedimento
            dtpFechaRecibo.Value = addenda.Pedimento.FechaReciboLaredo
            dtpFechaBillOflanding.Value = addenda.Pedimento.FechaBillOfLanding
        End If

        If addenda.Pedidos.Count = 0 Then addenda.Pedidos.Add(New AddendaSorianaPedido(addenda.IdVenta, 0, 0))
        dgvPedidos.DataSource = addenda.Pedidos

    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            ValidateChildren()

            If Not IsNumeric(txtTienda.Text) Then
                txtTienda.Focus()
            ElseIf cmbTipoBulto.Text = "" Then
                cmbTipoBulto.Focus()
            ElseIf cmbEntregaMercancia.Text = "" Then
                cmbEntregaMercancia.Focus()
            ElseIf Not IsNumeric(txtCantidadBultos.Text) Then
                txtCantidadBultos.Focus()
            ElseIf Not IsNumeric(txtCita.Text) Then
                txtCita.Focus()
            ElseIf Not IsNumeric(txtFolioNotaEntrada.Text) And txtFolioNotaEntrada.Text <> "" Then
                txtFolioNotaEntrada.Focus()
            Else
                Dim db As New dbAddendaSoriana(MySqlcon)
                addenda.Tienda = txtTienda.Text
                addenda.TipoMoneda = cmbMoneda.SelectedIndex
                addenda.TipoBulto = cmbTipoBulto.SelectedIndex
                addenda.EntregaMercancia = cmbEntregaMercancia.Text
                addenda.CantidadBultos = txtCantidadBultos.Text
                addenda.FechaEntrega = dtpFechaEntrega.Value
                addenda.Cita = txtCita.Text
                If txtFolioNotaEntrada.Text = "" Then
                    addenda.FolioNotaEntrada = Nothing
                Else
                    addenda.FolioNotaEntrada = txtFolioNotaEntrada.Text
                End If
                If txtPedimento.Text <> "" Then
                    addenda.Pedimento = New AddendaSorianaPedimento()
                    addenda.Pedimento.Pedimento = txtPedimento.Text
                    addenda.Pedimento.Aduana = txtAduana.Text
                    addenda.Pedimento.AgenteAduanal = txtAgenteAduanal.Text
                    addenda.Pedimento.TipoPedimento = cmbTipoPedimento.Text
                    addenda.Pedimento.FechaPedimento = dtpFechaPedimento.Value
                    addenda.Pedimento.FechaReciboLaredo = dtpFechaRecibo.Value
                    addenda.Pedimento.FechaBillOfLanding = dtpFechaBillOflanding.Value
                End If

                db.Guardar(addenda)
                Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgvPedidos_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPedidos.SelectionChanged
        If dgvPedidos.SelectedRows.Count > 0 Then
            dgvArticulos.DataSource = DirectCast(dgvPedidos.SelectedRows(0).DataBoundItem, AddendaSorianaPedido).Articulos
        End If
    End Sub

    Private Sub dgvPedidos_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvPedidos.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim p As New AddendaSorianaPedido(addenda.IdVenta, 0, 0)
            p.Articulos.Add(New AddendaSorianaArticulo(p.IdVenta, 0, 0, 0, 0, 0, 0))
            addenda.Pedidos.Add(p)
            dgvPedidos.DataSource = Nothing
            dgvPedidos.DataSource = addenda.Pedidos
        ElseIf e.KeyCode = Keys.Delete Then
            addenda.Pedidos.Remove(dgvPedidos.SelectedRows(0).DataBoundItem)
            dgvPedidos.DataSource = Nothing
            dgvPedidos.DataSource = addenda.Pedidos
        End If
    End Sub

    Private Sub dgvArticulos_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvArticulos.KeyDown
        Dim p As AddendaSorianaPedido = dgvPedidos.SelectedRows(0).DataBoundItem
        If e.KeyCode = Keys.Enter Then
            Dim addenda As New AddendaSorianaArticulo(p.IdVenta, p.FolioPedido, 0, 0, 0, 0, 0)
            p.Articulos.Add(addenda)
            dgvArticulos.DataSource = Nothing
            dgvArticulos.DataSource = p.Articulos
        ElseIf e.KeyCode = Keys.Delete Then
            p.Articulos.Remove(dgvArticulos.SelectedRows(0).DataBoundItem)
            dgvArticulos.DataSource = Nothing
            dgvArticulos.DataSource = p.Articulos
        End If
    End Sub

    Private Sub dgvPedidos_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvPedidos.DataError
        MsgBox("Proporcione un valor numérico.")
    End Sub

    Private Sub dgvArticulos_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvArticulos.DataError
        MsgBox("Proporcione un valor numérico.")
    End Sub

    Private Sub txtProveedor_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtProveedor.Validating, txtTotal.Validating, txtTienda.Validating, txtSubtotal.Validating, txtOtrosImpuestos.Validating, txtIVA.Validating, txtIEPS.Validating, txtCita.Validating, txtCantidadBultos.Validating, cmbEntregaMercancia.Validating
        If Not IsNumeric(sender.Text) Then
            ErrorProvider1.SetError(sender, "Proporcione un valor válido.")
        Else
            ErrorProvider1.SetError(sender, "")
        End If
    End Sub

    Private Sub txtFolioNotaEntrada_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFolioNotaEntrada.Validating
        If Not IsNumeric(sender.Text) And sender.Text <> "" Then
            ErrorProvider1.SetError(sender, "Proporcione un valor válido.")
        Else
            ErrorProvider1.SetError(sender, "")
        End If
    End Sub

    Private Sub txtRemision_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtRemision.Validating, cmbMoneda.Validating, cmbTipoBulto.Validating
        If sender.Text = "" Then
            ErrorProvider1.SetError(sender, "Proporcione un valor válido.")
        Else
            ErrorProvider1.SetError(sender, "")
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub
End Class

