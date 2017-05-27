Public Class frmInventarioFormula01
    Public Resultado As Double
    Public FormulaString As String
    Dim Descripcion As String
    Public Sub New(pDescripcion As String)

        ' This call is required by the designer.
        InitializeComponent()
        Descripcion = pDescripcion
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Aceptar()
    End Sub
    Private Sub Aceptar()
        If IsNumeric(TextBox1.Text) And IsNumeric(TextBox2.Text) And IsNumeric(TextBox3.Text) And IsNumeric(TextBox5.Text) Then
            Resultado = CDbl(TextBox5.Text) * CDbl(TextBox1.Text) * CDbl(TextBox2.Text) * CDbl(TextBox3.Text) / 12
            FormulaString = Descripcion + " " + TextBox5.Text + " Piezas " + TextBox1.Text + "x" + TextBox2.Text + "x" + TextBox3.Text
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            MsgBox("Debe introducir solo números.", MsgBoxStyle.Information, GlobalNombreApp)
        End If

    End Sub
    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

   
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            Aceptar()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub frmInventarioFormula01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
    End Sub
End Class