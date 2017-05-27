Public Class txtCantidades

    Private Sub txtCantidad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.Click
        txtCantidad.SelectAll()
    End Sub

    Private Sub txtCantidad_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.Enter
        txtCantidad.SelectAll()
    End Sub

    Private Sub txtCantidad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyDown
        If txtCantidad.Text = "0.00" Then
            txtCantidad.Text = ""
        End If
    End Sub

    Private Sub txtCantidad_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCantidad_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.Leave
        Dim x As Double
        If txtCantidad.Text = "." Then
            txtCantidad.Text = "0.00"
        End If
        If txtCantidad.Text = "" Then
            txtCantidad.Text = "0.00"
        Else
            x = Double.Parse(txtCantidad.Text)
            If x >= 2000.0 Then
                'Leyenda.Checked = True
            Else
                '  Leyenda.Checked = False
            End If

            txtCantidad.Text = Format(x, "0.00")
        End If

    End Sub

    Private Sub txtCantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.TextChanged

    End Sub
    Function obText() As String
        Return txtCantidad.Text
    End Function

    Public Sub insText(ByVal dato As String)
        txtCantidad.Text = dato
    End Sub

    Public Sub txtCantidad_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.DoubleClick
        txtCantidad.DeselectAll()
    End Sub
End Class
