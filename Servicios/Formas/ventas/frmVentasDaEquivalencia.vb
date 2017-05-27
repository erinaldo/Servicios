Public Class frmVentasDaEquivalencia
    Dim IdInventario As Integer
    Public Cantidad As Double
    Public CantidadM As Double
    Public PrecioM As Double
    Public TipoCantidadM As Integer
    Dim IdsTC2 As New elemento
    Dim IdsTC1 As New elemento
    Public Sub New(ByVal pIdInventario As Integer, ByVal pCantidad As Double, ByVal pCantidadM As Double, ByVal pIdTipoCantidadM As Integer, ByVal pPrecio As Double, pHabilitado As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdInventario = pIdInventario
        Cantidad = pCantidad
        CantidadM = pCantidadM
        PrecioM = pPrecio
        TipoCantidadM = pIdTipoCantidadM
        If pHabilitado = False Then
            Button7.Enabled = False
        End If
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmVentasDaEquivalencia_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception
        End Try
        LlenaCombos("tbltiposcantidades", ComboBox1, "nombre", "nombret", "idtipocantidad", IdsTC1, "idtipocantidad>1")
        LlenaCombos("tbltiposcantidades", ComboBox2, "nombre", "nombret", "idtipocantidad", IdsTC2, "idtipocantidad>1")
        Dim i As New dbInventario(IdInventario, MySqlcon)
        TextBox5.Text = Cantidad.ToString
        ComboBox1.SelectedIndex = IdsTC1.Busca(i.TipoCantidad.ID)
        If CantidadM <> 0 Then
            TextBox1.Text = CantidadM.ToString
        Else
            TextBox1.Text = Cantidad.ToString
        End If
        If TipoCantidadM = 0 Then
            ComboBox2.SelectedIndex = IdsTC2.Busca(i.TipoContenido.ID)
        Else
            ComboBox2.SelectedIndex = IdsTC2.Busca(TipoCantidadM)
        End If
        TextBox2.Text = PrecioM.ToString
        ComboBox1.Enabled = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
    Private Sub Aceptar()
        If IsNumeric(TextBox1.Text) And IsNumeric(TextBox5.Text) And IsNumeric(TextBox2.Text) Then
            CantidadM = CDbl(TextBox1.Text)
            Cantidad = CDbl(TextBox5.Text)
            TipoCantidadM = IdsTC2.Valor(ComboBox2.SelectedIndex)
            PrecioM = CDbl(TextBox2.Text)
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            MsgBox("Las cantidades deben ser un valor numérico.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Aceptar()
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox2.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub ComboBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Aceptar()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class