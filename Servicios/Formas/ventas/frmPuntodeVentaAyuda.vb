Public Class frmPuntodeVentaAyuda

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frmPuntodeVentaAyuda_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        TextBox1.Text = "Funciones:" + vbCrLf + vbCrLf
        TextBox1.Text += """ + "" Agrega uno al último artículo agregado o al artículo seleccionado." + vbCrLf + vbCrLf
        TextBox1.Text += """ +N "" Agrega la cantidad indicada al último artículo agregado o al artículo seleccionado." + vbCrLf + vbCrLf
        TextBox1.Text += """ - "" Resta uno al último artículo agregado o al artículo seleccionado. Si la cantidad queda en cero el artículo se elimina." + vbCrLf + vbCrLf
        TextBox1.Text += """ -N "" Resta la cantidad indicada al último artículo agregado o al artículo seleccionado. Si la cantidad queda menor o igual a cero el artículo se elimina." + vbCrLf + vbCrLf
        TextBox1.Text += """ /N "" o ""=N"" Asigna la cantidad indicada al último artículo agregado o al artículo seleccionado." + vbCrLf + vbCrLf
        TextBox1.Text += """ *(Código) "" Al cobrar sirve para indicar una forma de pago." + vbCrLf
        TextBox1.Text += """ ** "" Al cobrar sirve para indicar varias formas de pago." + vbCrLf
        TextBox1.Text += """ **CR "" Al cobrar sirve para indicar varias forma de pago de crédito." + vbCrLf + vbCrLf + vbCrLf
        TextBox1.Text += "Teclas especiales:" + vbCrLf + vbCrLf
        TextBox1.Text += """ F2 "" Muestra la búsqueda de artículos al estar agregando artículos." + vbCrLf + vbCrLf
        TextBox1.Text += """ F2 "" Muestra la lista de precios cuando el sistema pide precio." + vbCrLf + vbCrLf
        TextBox1.Text += """ F3 "" Cambiar precio." + vbCrLf + vbCrLf
        TextBox1.Text += """ F4 "" Cancelar una venta." + vbCrLf + vbCrLf
        TextBox1.Text += """ F5 "" Limpia la pantalla para comenzar una nueva venta." + vbCrLf + vbCrLf
        TextBox1.Text += """ F6 "" Lee báscula y asigna la cantidad leida al último artículo agregado o al artículo seleccionado." + vbCrLf + vbCrLf
        TextBox1.Text += """ F7 "" Hacer un descuento." + vbCrLf + vbCrLf
        TextBox1.Text += """ F8 "" Busca una venta pendiente." + vbCrLf + vbCrLf
        TextBox1.Text += """ F9 "" Se guarda una venta como pendiente." + vbCrLf + vbCrLf
        TextBox1.Text += """ F10 "" Movimientos de caja." + vbCrLf + vbCrLf
        TextBox1.Text += """ F11 "" Muestra el efectivo actual en caja." + vbCrLf + vbCrLf
        TextBox1.Text += """ F12 "" Muestra la ventana de reportes." '+ vbCrLf + vbCrLf
    End Sub
End Class