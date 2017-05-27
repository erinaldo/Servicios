Public Class frmConvesionesMonedas
    Dim IdsMonedas1 As New elemento
    Dim IdsMonedas2 As New elemento
    Private Sub frmConvesionesMonedas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IdsMonedas1, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IdsMonedas2, "idmoneda>1")
        Consulta()
    End Sub

    Private Sub Consulta()
        Try
            Dim C As New dbMonedasConversiones(MySqlcon)
            DataGridView1.DataSource = C.Consulta
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "Moneda 1"
            DataGridView1.Columns(2).HeaderText = "Moneda 2"
            DataGridView1.Columns(3).HeaderText = "Valor"
            DataGridView1.Columns(1).Width = 150
            DataGridView1.Columns(2).Width = 150
            DataGridView1.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Monedas_Conversiones + 1)))) <> 0 Then
            Dim C As New dbMonedasConversiones(MySqlcon)
            C.Guardar(IdsMonedas1.Valor(ComboBox1.SelectedIndex), IdsMonedas2.Valor(ComboBox2.SelectedIndex), CDbl(TextBox1.Text))
            PopUp("Guardado", 90)
            Consulta()
            TextBox1.Text = "0"
            ComboBox2.SelectedIndex = 0
            'Else
            'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Monedas_Conversiones + 2)))) <> 0 Then
            Dim Renglon As Integer = 0
            Dim IdConversion As Integer
            Dim Cantidad As Double
            Dim HayError As Boolean = False
            Dim I As New dbMonedasConversiones(MySqlcon)
            While Renglon < DataGridView1.RowCount
                If IsNumeric(DataGridView1.Item(3, Renglon).Value) = False Then
                    HayError = True
                End If
                Renglon += 1
            End While
            Renglon = 0
            If Not HayError Then
                While Renglon < DataGridView1.RowCount
                    IdConversion = DataGridView1.Item(0, Renglon).Value
                    Cantidad = DataGridView1.Item(3, Renglon).Value
                    I.Modificar(IdConversion, Cantidad)
                    Renglon += 1
                End While
            Else
                MsgBox("Debe indicar solamente cantidades numéricas", MsgBoxStyle.Critical, "Restaurante")
            End If
            'Else
            'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Restaurante")
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Monedas_Conversiones + 3)))) <> 0 Then
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim C As New dbMonedasConversiones(MySqlcon)
                C.Eliminar(DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value)
                Consulta()
                PopUp("Registro Eliminado", 90)
            End If
            'Else
            'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class