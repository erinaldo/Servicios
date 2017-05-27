Public Class frmContabilidadContra
    Dim cont As dbContabilidadPolizas

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Dim p As New dbContabilidadPolizas(MySqlcon)
        cont = p
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Dim actual As String = txtPassActual.Text
        Dim nueva As String = txtPassNuevo.Text
        Dim repetir As String = txtRepetirPass.Text
        If nueva <> repetir Then
            MsgBox("las contraseñas no coinciden")
            Return
        Else
            If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim res As Boolean = cont.cambiarContra(actual, nueva)
                If res Then
                    PopUp("contraseña modificada", 90)
                Else
                    MsgBox("error al cambiar la contraseña, introduza los datos corretamente")
                End If
            Else
                Return
            End If
        End If
    End Sub

    Private Sub frmContabilidadContra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
    End Sub
End Class