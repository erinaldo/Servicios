Public Class frmRestaurantePagar
    Private listaFormasPago As New elemento
    Private idVenta As Integer
    Private idFormaPago As Integer
    Private total As Double
    Private recibido As Double
    Private cambio As Double
    Private ventasPagos As New dbRestauranteVentaPago(MySqlcon)
    Private formaPago As New dbRestauranteFormasPagos(MySqlcon)
    Private f As frmRestauranteTeclado
    Public pagado As Boolean = False
    Public Sub New(ByVal idVenta As Integer, ByVal total As Double)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.idVenta = idVenta
        Me.total = total
        LlenaCombos("tblrestauranteformaspagos", comboFormas, "nombre", "nombreN", "id", listaFormasPago)
        llenaDatos()
    End Sub
    Private Sub frmRestaurantePagar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 1000
    End Sub

    Private Sub comboFormas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboFormas.SelectedIndexChanged
        idFormaPago = listaFormasPago.Valor(comboFormas.SelectedIndex)
    End Sub

    Private Sub llenaDatos()
        txtTotal.Text = total
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If txtRecibido.Text = "" Then
            MsgBox("Debe indicar el monto recibido.")
            Exit Sub
        End If
        recibido = CDbl(txtRecibido.Text)
        ventasPagos.agregar(idVenta, idFormaPago, recibido)
        dgvPagos.DataSource = ventasPagos.buscarPorVenta(idVenta)
        recibido = ventasPagos.sumaPagos(idVenta)
        txtRecibido.Text = recibido
        calculaCambio(total, recibido)
    End Sub

    Public Declare Function BlockInput Lib "user32" (ByVal fBlock As Long) As Long

    Public Function Bloquear(Incremento As Integer, TiempoMax As Integer)
        Static contador As Integer
        contador = contador + Incremento
        If contador >= TiempoMax Then
            BlockInput(False)
            Return False
        End If
        Return True
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Bloquear(2, 10)
    End Sub

    Private Sub txtRecibido_Enter(sender As Object, e As EventArgs) Handles txtRecibido.Enter
        f = New frmRestauranteTeclado(txtRecibido)
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub txtRecibido_Leave(sender As Object, e As EventArgs) Handles txtRecibido.Leave
        f.Dispose()
    End Sub

    Private Sub calculaCambio(ByVal total As Double, ByVal recibido As Double)
        If total = recibido Then
            cambio = 0
            lblNotificacion.Visible = False
        ElseIf total > recibido Then
            cambio = total - recibido
            lblNotificacion.Visible = True
            lblNotificacion.ForeColor = Color.Red
            lblNotificacion.Text = "La cantidad recibida no cubre el total"
        Else
            cambio = recibido - total
        End If
        txtCambio.Text = cambio.ToString()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        pagado = True
        Dispose()
    End Sub

    Public Sub NumConFrac(ByVal CajaTexto As Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs)
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
 
    Private Sub txtRecibido_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRecibido.KeyPress
        NumConFrac(txtRecibido, e)
    End Sub

End Class