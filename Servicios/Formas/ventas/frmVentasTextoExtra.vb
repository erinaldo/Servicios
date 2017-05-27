Public Class frmVentasTextoExtra
    Public Texto As String
    Public Sub New(ByVal pTexto As String, ByVal psize As Integer, ByVal AMayusculas As Boolean, Optional FuenteStr As String = "Lucida Console", Optional FuenteSize As Single = 8.25)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Texto = pTexto
        TextBox1.MaxLength = psize
        Dim Fuente As New Font(FuenteStr, FuenteSize)
        TextBox1.Font = Fuente
        If AMayusculas = True Then
            TextBox1.CharacterCasing = CharacterCasing.Upper
        End If
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Texto = TextBox1.Text
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub frmVentasTextoExtra_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        TextBox1.Text = Texto
    End Sub

    'Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
    '    Dim textBox As TextBox = DirectCast(sender, TextBox)
    '    Dim I As Integer
    '    textBox.Text = UCase(textBox.Text)
    '    I = Len(textBox.Text)
    '    textBox.SelectionStart = I
    'End Sub
End Class