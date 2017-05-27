Public Class frmSioNo
    Public Sub New(pTextoaMostrar As String, pEncabezado As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Label8.Text = pTextoaMostrar
        Label1.Text = pTextoaMostrar
        Me.Text = pEncabezado
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnGuardar_KeyDown(sender As Object, e As KeyEventArgs) Handles btnGuardar.KeyDown
        If e.KeyCode = Keys.Right Then
            Button1.Focus()
        End If
    End Sub

    Private Sub Button1_KeyDown(sender As Object, e As KeyEventArgs) Handles Button1.KeyDown
        If e.KeyCode = Keys.Left Then
            btnGuardar.Focus()
        End If
    End Sub

    Private Sub frmSioNo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If Label8.Width >= 390 Then
            Me.Width = Label8.Size.Width + 80
            Label8.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
            Label1.Visible = False
        Else
            Label8.Visible = False
            Label1.Visible = True
            'Label8.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 120)
        End If

        Button1.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 198)
        btnGuardar.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 80)

    End Sub
End Class