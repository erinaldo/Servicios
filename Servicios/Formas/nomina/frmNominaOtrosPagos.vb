Public Class frmNominaOtrosPagos
    Private clavesTipos() As String = {"01", "02", "03", "04", "99"}
    Private otrosPagos As New dbNominaOtrosPagos(MySqlcon)
    Private nuevo As Boolean = True
    Private nuevoPago As Boolean = True
    Private idNomina As Integer
    Private nomina As dbNominas
    Dim Estado As Byte
    Public Sub New(ByVal pidNomina As Integer, pEstado As Byte)
        ' This call is required by the designer.
        InitializeComponent()
        Estado = pEstado
        idNomina = pidNomina
        ' Add any initialization after the InitializeComponent() call.
        nomina = New dbNominas(idNomina, MySqlcon)
        llenaDatos()
        If Estado = Estados.Guardada Or Estado = Estados.Cancelada Then
            btnAgregar.Enabled = False
        End If
    End Sub

    Private Sub llenaDatos()
        llenaGrid()
    End Sub
    Private Sub frmNominaOtrosPagos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        comboTipo.Items.Add("001 Reintegro de ISR pagado en exceso siempre que no haya sido enterado al SAT")
        comboTipo.Items.Add("002 Subsidio para el empleo efectivamente entregado al trabajador")
        comboTipo.Items.Add("003 Viáticos entregados al trabajador")
        comboTipo.Items.Add("004 Aplicación de saldo a favor por compensación anual")
        comboTipo.Items.Add("999 Pagos distintos a los listados y que no deben considerarse como ingreso por sueldos, salarios o ingresos asimilados")
        nuevoOtroPago()
    End Sub
    Public Sub NumConFrac(ByVal CajaTexto As Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporte.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False

        ElseIf e.KeyChar = "." And Not CajaTexto.Text.IndexOf(".") Then
            e.Handled = True
        ElseIf e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If comboTipo.SelectedIndex = 1 Then
            If CDbl(txtSubsidio.Text) < CDbl(txtImporte.Text) Then
                MsgBox("Subsicio causado debe ser mayor o igual al importe.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
        End If
        If nuevoPago Then
            If txtClave.Text <> "" And txtImporte.Text <> "" Then
                otrosPagos.agregar(comboTipo.SelectedIndex, txtClave.Text, comboTipo.Text, CDbl(txtImporte.Text), idNomina, CDbl(txtSubsidio.Text), CDbl(txtSaldo.Text), CDbl(txtAnhos.Text), CDbl(txtRemanente.Text))
                llenaGrid()
                nuevoOtroPago()
            End If

        Else
            If txtClave.Text <> "" And txtImporte.Text <> "" Then
                otrosPagos.modificar(otrosPagos.idPago, comboTipo.SelectedIndex, txtClave.Text, comboTipo.Text, CDbl(txtImporte.Text), idNomina, CDbl(txtSubsidio.Text), CDbl(txtSaldo.Text), CDbl(txtAnhos.Text), CDbl(txtRemanente.Text))
                llenaGrid()
                nuevoOtroPago()
            End If
        End If
    End Sub

    Private Sub llenaGrid()
        dgvPagos.DataSource = otrosPagos.vistaPagos(idNomina)
        dgvPagos.Columns(0).Visible = False
        dgvPagos.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvPagos.Columns(1).HeaderText = "Clave"
        dgvPagos.Columns(2).HeaderText = "Concepto"
        dgvPagos.Columns(3).HeaderText = "Importe"
        'dgvPagos.Columns(0).Visible = False
    End Sub

    Private Sub MuestraPago(ByVal idPago)
        otrosPagos.buscar(idPago)
        comboTipo.SelectedIndex = otrosPagos.tipoPago
        txtClave.Text = otrosPagos.clave
        txtImporte.Text = otrosPagos.importe.ToString
        txtSubsidio.Text = otrosPagos.subsidio.ToString
        txtSaldo.Text = otrosPagos.saldoAFavor.ToString
        txtAnhos.Text = otrosPagos.anhos.ToString
        txtRemanente.Text = otrosPagos.remanente.ToString
        comboTipo.Focus()
    End Sub

    Private Sub dgvPagos_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPagos.CellClick
        Try
            Dim idpago As Integer = CInt(dgvPagos.CurrentRow.Cells(0).Value.ToString())
            MuestraPago(idpago)
            nuevoPago = False
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then
                Button2.Enabled = True
            End If
            btnAgregar.Text = "Modificar"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles dgvPagos.Click
        
    End Sub

    Private Sub nuevoOtroPago()
        nuevoPago = True
        txtClave.Text = ""
        txtImporte.Text = "0"
        txtRemanente.Text = "0"
        txtAnhos.Text = "0"
        txtSaldo.Text = "0"
        txtSubsidio.Text = "0"
        comboTipo.SelectedIndex = 0
        Button2.Enabled = False
        btnAgregar.Text = "Agregar"
        comboTipo.Focus()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dispose()
    End Sub

    Private Sub txtSubsidio_TextChanged(sender As Object, e As EventArgs) Handles txtSubsidio.TextChanged

    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        nuevoOtroPago()
    End Sub

    Private Sub comboTipo_KeyDown(sender As Object, e As KeyEventArgs) Handles comboTipo.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtClave.Focus()
        End If
    End Sub

    Private Sub comboTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboTipo.SelectedIndexChanged
        txtSubsidio.Enabled = False
        txtSaldo.Enabled = False
        txtAnhos.Enabled = False
        txtRemanente.Enabled = False
        Select Case comboTipo.SelectedIndex
            Case 1
                txtSubsidio.Enabled = True
            Case 3
                txtSaldo.Enabled = True
                txtAnhos.Enabled = True
                txtRemanente.Enabled = True
        End Select
    End Sub

    Private Sub dgvPagos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPagos.CellContentClick

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar este pago?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            otrosPagos.eliminar(otrosPagos.idPago)
            nuevoOtroPago()
            llenaGrid()
        End If
    End Sub
End Class