Public Class frmVentasSelectorMetodosPago
    Dim Tipo As Byte
    Dim IdMov As Integer
    Dim Total As Double
    Dim TipoForma As Byte
    Dim IdsFormasdePAgo As New elemento
    Dim MP As dbVentasAddMetodos
    Public Sub New(pTipo As Byte, pIdMov As Integer, pTotal As Double, pTipoForma As Byte, pSoloConsulta As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'tipos
        '0 facturas
        '1 remisiones
        '2 nominas
        IdMov = pIdMov
        Tipo = pTipo
        Total = pTotal
        TipoForma = pTipoForma
        If TipoForma = 0 Then
            Label2.Text = "CRÉDITO"
        Else
            Label2.Text = "CONTADO"
        End If
        If pSoloConsulta Then
            Button15.Enabled = False
            Button1.Enabled = False
        End If
    End Sub

    Private Sub frmVentasSelectorMetodosPago_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If TodoAgregado() = False Then
            MsgBox("Debe asignar el total del documento.", MsgBoxStyle.Information, GlobalNombreApp)
            e.Cancel = True
        End If
    End Sub
    Private Sub frmVentasSelectorMetodosPago_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        MP = New dbVentasAddMetodos(MySqlcon)
        If Tipo = 0 Or Tipo = 2 Then
            LlenaCombos("tblformasdepago", ComboBox4, "concat(lpad(convert(clavesat using utf8),2,'0'),' ',nombre)", "nombret", "idforma", IdsFormasdePAgo, "tipo=" + TipoForma.ToString, , "idforma")
        Else
            If TipoForma = 1 Then
                LlenaCombos("tblformasdepagoremisiones", ComboBox4, "nombre", "nombrec", "idforma", IdsFormasdePAgo, "tipo=1 or tipo=2", , "idforma")
            Else
                LlenaCombos("tblformasdepagoremisiones", ComboBox4, "nombre", "nombrec", "idforma", IdsFormasdePAgo, "tipo=3", , "idforma")
            End If
        End If
        Consulta()
    End Sub
    Private Sub Consulta()
        DGDetalles.DataSource = MP.Consulta(Tipo, IdMov)
        DGDetalles.Columns(0).Visible = False
        DGDetalles.Columns(1).HeaderText = "Clave"
        DGDetalles.Columns(2).HeaderText = "Descripción"
        DGDetalles.Columns(3).HeaderText = "Importe"
        DGDetalles.Columns(1).Width = 45
        DGDetalles.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGDetalles.ClearSelection()
        Dim TotalAgregado As Double = MP.TotalAgregado(Tipo, IdMov)
        TextBox18.Text = (Total - TotalAgregado).ToString("###,###,##0.00")
        Label1.Text = "Total: " + TotalAgregado.ToString("###,###,##0.00") + " restan " + TextBox18.Text
    End Sub

    Private Sub DGDetalles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub DGDetalles_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGDetalles.CellFormatting
        If e.ColumnIndex = 1 Then
            e.Value = Format(e.Value, "00")
        End If
        If e.ColumnIndex = 3 Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            e.Value = Format(e.Value, "#0.00")
        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        BotonAgregar()
    End Sub
    Private Sub BotonAgregar()
        Dim Err As String = ""
        If IsNumeric(TextBox18.Text) = False Then
            Err = "Debe indicar una cantidad."
        End If
        If Err = "" Then
            If CDbl(TextBox18.Text) <= 0 Then
                Err = "El importe debe ser mayor a cero."
            End If
            If MP.TotalAgregado(Tipo, IdMov) + CDbl(TextBox18.Text) > Math.Round(Total, 2) Then
                Err += "No puede pasarse del total del documento."
            End If
        End If
        If Err = "" Then
            MP.Guardar(Tipo, IdsFormasdePAgo.Valor(ComboBox4.SelectedIndex), CDbl(TextBox18.Text), IdMov)
            TextBox18.Text = ""
            ComboBox4.Focus()
            Consulta()
        Else
            MsgBox(Err, MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim idMetodo As Integer = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
        If idMetodo > 0 Then
            If MsgBox("¿Remover el método de pago seleccionado?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                MP.Eliminar(Tipo, idMetodo)
                TextBox18.Text = ""
                ComboBox4.Focus()
                Consulta()
            End If
        End If
    End Sub
    Private Function TodoAgregado() As Boolean
        If Math.Round(MP.TotalAgregado(Tipo, IdMov), 2) = Math.Round(Total, 2) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ComboBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox18.Focus()
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged

    End Sub

    Private Sub TextBox18_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox18.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub TextBox18_TextChanged(sender As Object, e As EventArgs) Handles TextBox18.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class