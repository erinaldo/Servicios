Public Class frmPagare
    Dim idCliente As Integer
    Dim ConsultaOn As Boolean = True
    Public ReadOnly Property id() As Integer()
        Get
            Dim arr As Integer() = {}
            Dim r As DataGridViewRow
            For Each r In DGServicios.SelectedRows
                ReDim Preserve arr(arr.Length)
                arr(arr.Length - 1) = r.Cells(0).Value
            Next
            Return arr
        End Get
    End Property

    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim S As New dbVentas(MySqlcon)
                DGServicios.DataSource = S.Pagare(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), idCliente, txtFolio.Text)
                DGServicios.ClearSelection()
                DGServicios.Columns(2).HeaderText = "Folio"
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            Dim r As New repPagare
            r.SetDataSource(DGServicios.DataSource)
            r.SetParameterValue("fechapago", DateTimePicker1.Value)
            r.SetParameterValue("interes", NumericUpDown1.Value)
            r.SetParameterValue("empresa", GlobalNombreEmpresa)
            Dim f As New frmReportes(r, False)
            f.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCliente.TextChanged
        BuscaCliente()
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbClientes(MySqlcon)
                If c.BuscaCliente(txtcliente.Text) Then
                    txtdatoscliente.Text = c.Nombre + " | " + c.RFC
                    IdCliente = c.ID
                    Consulta()
                Else
                    txtdatoscliente.Text = ""
                    idCliente = 0
                    DGServicios.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha1.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha2.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolio.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub frmVentasConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        DGServicios.AutoGenerateColumns = False
        ConsultaOn = False
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        dtpFecha1.Value = Date.Now.AddDays(1 - Now.DayOfYear)
        dtpFecha2.Value = Date.Now
        ConsultaOn = True
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Consulta()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtdatoscliente.Text = B.Cliente.Nombre + " | " + B.Cliente.RFC
            IdCliente = B.Cliente.ID
            consultaOn = False
            txtCliente.Text = B.Cliente.Clave
            ConsultaOn = True
            Consulta()
        End If
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        If DGServicios.Item(1, e.RowIndex).Value = 1 Then
            DGServicios.Item(1, e.RowIndex).Value = 0
        Else
            DGServicios.Item(1, e.RowIndex).Value = 1
        End If
    End Sub

    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub
End Class