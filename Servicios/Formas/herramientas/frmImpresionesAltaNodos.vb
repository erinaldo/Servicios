Public Class frmImpresionesAltaNodos
    Dim IdsSucursales As New elemento
    Dim F As Font
    Private Sub frmImpresionesAltaNodos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox3.Items.Add("Ventas - Factura")
        ComboBox3.Items.Add("Ventas - Cotización")
        ComboBox3.Items.Add("Ventas - Pedido")
        ComboBox3.Items.Add("Ventas - Remisión")
        ComboBox3.Items.Add("Ventas - Devolución")
        ComboBox3.Items.Add("Ventas - Nota de Crédito")
        ComboBox3.Items.Add("Ventas - Nota de Cargo")
        'ComboBox3.Items.Add("Ventas - Lista de Series")
        ComboBox3.Items.Add("Compras - Factura")
        ComboBox3.Items.Add("Compras - Cotización")
        ComboBox3.Items.Add("Compras - Pedido")
        ComboBox3.Items.Add("Compras - Remisión")
        ComboBox3.Items.Add("Compras - Devolución")
        ComboBox3.Items.Add("Compras - Nota de Crédito")
        ComboBox3.Items.Add("Compras - Nota de Cargo")
        ComboBox3.Items.Add("Movimientos de Inventario")
        ComboBox3.SelectedIndex = 0
        ComboBox1.Items.Add("Estátido")
        ComboBox1.Items.Add("Detalles")
        ComboBox1.SelectedIndex = 0
        ComboBox4.Items.Add("Texto")
        ComboBox4.Items.Add("Numérico")
        ComboBox4.SelectedIndex = 0
        ComboBox5.Items.Add("No")
        ComboBox5.Items.Add("Si")
        ComboBox5.SelectedIndex = 0
        ComboBox7.Items.Add("No")
        ComboBox7.Items.Add("Si")
        ComboBox7.SelectedIndex = 0
        ComboBox6.Items.Add("Caja de texto")
        ComboBox6.Items.Add("Línea")
        ComboBox6.Items.Add("Etiqueta")
        ComboBox6.SelectedIndex = 0
        LlenaCombos("tblsucursales", ComboBox2, "nombre", "nombret", "idsucursal", IdsSucursales)
        Nuevo()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If FontDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            F = FontDialog1.Font
            'For Each n As NodoImpresionN In Nodos
            'If n.Seleccionado Then
            'n.Fuente = FontDialog1.Font
            TextBox7.Text = F.FontFamily.Name + "," + F.Size.ToString + "pt"
            'Panel1.Refresh()
            'Exit For
            'End If
            'Next
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim dbI As New dbImpresionesN(MySqlcon)
        Dim no As New NodoImpresionN(1, CInt(TextBox3.Text), CInt(TextBox1.Text), CInt(TextBox6.Text), CInt(TextBox5.Text), TextBox10.Text, TextBox4.Text, F, NodoImpresionN.Alineaciones.Izquierda, ComboBox1.SelectedIndex, ComboBox4.SelectedIndex, ComboBox5.SelectedIndex, ComboBox3.SelectedIndex, ComboBox6.SelectedIndex, IdsSucursales.Valor(ComboBox2.SelectedIndex), ComboBox7.SelectedIndex, TextBox2.Text, 0, 0)
        TextBox11.Text = dbI.GuardaNodo(no)
        PopUp("Nodo Agregado", 90)
        Nuevo()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Nuevo()
        TextBox3.Text = "100"
        TextBox1.Text = "100"
        TextBox6.Text = "120"
        TextBox5.Text = "20"
        TextBox2.Text = ""
        TextBox4.Text = ""
        TextBox7.Text = ""
        TextBox10.Text = ""
        ComboBox1.SelectedIndex = 0
        ComboBox4.SelectedIndex = 0
        ComboBox5.SelectedIndex = 0
        ComboBox6.SelectedIndex = 0
        ComboBox7.SelectedIndex = 0
        TextBox8.Text = ""
        TextBox9.Text = "0"
        TextBox3.Focus()
    End Sub
End Class