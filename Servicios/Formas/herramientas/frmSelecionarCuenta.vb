Public Class frmSelecionarCuenta
    Public IdCuenta As Integer
    Public Sub New(pidCuenta As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        IdCuenta = pidCuenta
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        IdCuenta = SelectorCuentas1.IdCuenta
        If SelectorCuentas1.IdCuenta >= 0 Then
            Dim Cc As New dbCContables(MySqlcon)
            If Cc.UtlimoNivel(SelectorCuentas1.IdCuenta, SelectorCuentas1.Nivel) <> 0 Then
                MsgBox("Debe seleccionar una cuenta de último nivel.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
    
    Private Sub frmSelecionarCuenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        SelectorCuentas1.P = New dbContabilidadPolizas(MySqlcon)
        SelectorCuentas1.C = New dbContabilidadClasificacion(MySqlcon)
        SelectorCuentas1.Inicializar()
        If IdCuenta <> 0 Then SelectorCuentas1.LlenaCuenta(IdCuenta)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SelectorCuentas1.Vacia()
    End Sub
End Class