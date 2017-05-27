Public Class frmConsultaTimbres
    Dim ConsultaOn As Boolean = False
    Private Sub frmConsultaTimbres_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("Facturas")
        ComboBox1.Items.Add("Notas de Crédito")
        ComboBox1.Items.Add("Notas de Cargo")
        ComboBox1.Items.Add("Devoluciones")
        ComboBox1.SelectedIndex = 0
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        ConsultaOn = True

    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                If ComboBox1.SelectedIndex = 0 Then
                    Dim S As New dbVentas(MySqlcon)
                    DGServicios.DataSource = S.ConsultaTimbres(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text)
                End If
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Folio"
                DGServicios.Columns(3).HeaderText = "Cliente"
                DGServicios.Columns(4).HeaderText = "Importe"
                DGServicios.Columns(5).HeaderText = "Folio Fiscal"
                DGServicios.Columns(2).Width = 80
                DGServicios.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Consulta()
    End Sub
End Class