Public Class frmContabilidadAyudaCompro
    Dim indice As Integer
    Public Sub New(ByVal pIndice As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        indice = pIndice
    End Sub
    Private Sub frmContabilidadAyudaCompro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        tbPrincipal.SelectedIndex = indice
    End Sub

    Private Sub frmContabilidadAyudaCompro_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class