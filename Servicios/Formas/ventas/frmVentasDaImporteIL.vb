Public Class frmVentasDaImporteIL
    Dim IdVenta As Integer

    Dim idsTipos As New elemento
    Dim V As dbVentas
    Dim idTipo As Integer = -1
    Public Sub New(ByVal pIdVenta As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        IdVenta = pIdVenta
    End Sub
    Private Sub frmCantidades_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        V = New dbVentas(MySqlcon)
        Nuevo()
    End Sub
    Private Sub Nuevo()
        Try
            ComboBox1.Text = ""
            Button1.Text = "Guardar"
            'Button2.Enabled = False
            'TextBox1.Text = ""
            LlenaCombos("tblventasimpuestos", ComboBox1, "nombre", "nombret", "idimpuesto", idsTipos, "idventa=" + IdVenta.ToString + " and tasa=0")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        BotonGuardar()
    End Sub
    Private Sub BotonGuardar()
        Try
            Dim TC As New dbClientesCuentas(MySqlcon)
            If IsNumeric(TextBox9.Text) Then
                V.ActualizaImporteImpuesto(idTipo, CDbl(TextBox9.Text))
                'PopUp("Guardado", 90)
                Me.Close()
            Else
                MsgBox("Debe de indicar un nombre a la medida.", MsgBoxStyle.Exclamation, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
   

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        'If ComboBox1.SelectedIndex >= 0 Then
        idTipo = idsTipos.Valor(ComboBox1.SelectedIndex)
        TextBox9.Text = V.DaImporteImpuesto(idTipo).ToString
        TextBox9.Focus()
        'Dim TC As New dbTiposCantidades(idTipo, MySqlcon)
        'TextBox1.Text = TC.Abreviatura
        'Button1.Text = "Modificar"
        'Button2.Enabled = True
        'End If
    End Sub

    Private Sub TextBox9_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox9.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonGuardar()
        End If
    End Sub

End Class