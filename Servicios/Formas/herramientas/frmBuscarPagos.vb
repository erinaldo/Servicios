Public Class frmBuscarPagos
    Dim ConsultaOn As Boolean
    Dim TVP As dbVentasPagosRemisiones
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
    Public ReadOnly Property idremisiones() As Integer()
        Get
            Dim arr As Integer() = {}
            Dim r As DataGridViewRow
            For Each r In DGServicios.SelectedRows
                ReDim Preserve arr(arr.Length)
                arr(arr.Length - 1) = r.Cells(6).Value
            Next
            Return arr
        End Get
    End Property
    Private Sub frmBuscarPagos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        TVP = New dbVentasPagosRemisiones(MySqlcon)
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        ConsultaOn = True
    End Sub

    Private Sub txtdatoscliente_TextChanged(sender As Object, e As EventArgs) Handles txtdatoscliente.TextChanged

    End Sub

    Private Sub btnBuscarCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarCliente.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtdatoscliente.Text = B.Cliente.Nombre + " | " + B.Cliente.RFC
            TVP.IdCliente = B.Cliente.ID
            ConsultaOn = False
            txtcliente.Text = B.Cliente.Clave
            ConsultaOn = True
            Consulta()
        End If
    End Sub

    Private Sub txtcliente_TextChanged(sender As Object, e As EventArgs) Handles txtcliente.TextChanged
        BuscaCliente()
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbClientes(MySqlcon)
                If c.BuscaCliente(txtcliente.Text) Then
                    txtdatoscliente.Text = c.Nombre + " | " + c.RFC
                    TVP.IdCliente = c.ID
                    Consulta()
                Else
                    txtdatoscliente.Text = ""
                    TVP.IdCliente = 0
                    Consulta()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        If ConsultaOn Then
            Try
                DGServicios.DataSource = TVP.ConsultaParaFacturar(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), TVP.IdCliente, txtFolio.Text, 0)
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(6).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Folio"
                DGServicios.Columns(3).HeaderText = "RFC"
                DGServicios.Columns(4).HeaderText = "Cliente"
                DGServicios.Columns(5).HeaderText = "Importe"
                DGServicios.Columns(2).Width = 80
                DGServicios.Columns(3).Width = 150
                DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        End If
    End Sub
    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub dtpFecha1_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha1.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub dtpFecha2_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha2.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub txtFolio_TextChanged(sender As Object, e As EventArgs) Handles txtFolio.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Consulta()
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ConsultaOn = False
        txtcliente.Text = ""
        txtFolio.Text = "XAXX010101000"
        ConsultaOn = True
        Consulta()
    End Sub

    Private Sub DGServicios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub DGServicios_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
    Private Sub SumaSeleccionados()
        Dim T As Double = 0
            For Each r As DataGridViewRow In DGServicios.SelectedRows
            T += r.Cells(5).Value
            Next
        Label10.Text = "Total seleccionado= " + T.ToString("###,###,##0.00")
    End Sub

    Private Sub DGServicios_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        If e.ColumnIndex = 5 Then
            e.Value = Format(e.Value, "0.00")
        End If
    End Sub

    Private Sub DGServicios_SelectionChanged(sender As Object, e As EventArgs) Handles DGServicios.SelectionChanged
        SumaSeleccionados()
    End Sub
End Class