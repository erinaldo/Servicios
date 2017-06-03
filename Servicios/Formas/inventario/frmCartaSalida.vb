Public Class frmCartaSalida
    Private _Carta As CartaSalida
    Private Property Carta As CartaSalida
        Get
            Return _Carta
        End Get
        Set(value As CartaSalida)
            _Carta = value
            dtpFecha.Value = Carta.Fecha
            txtUnidad.Text = Carta.Unidad
            txtMarca.Text = Carta.Marca
            txtModelo.Text = Carta.Modelo
            txtColor.Text = Carta.Color
            txtPlacas.Text = Carta.Placas
            txtTransportista.Text = Carta.Transportista
            txtChofer.Text = Carta.Chofer
            txtLote.Text = Carta.Lote
            txtObservaciones.Text = Carta.Observaciones
        End Set
    End Property


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim db As New dbCartasSalida(MySqlcon)
            Carta.Fecha = dtpFecha.Value
            Carta.Unidad = txtUnidad.Text
            Carta.Marca = txtMarca.Text
            Carta.Modelo = txtModelo.Text
            Carta.Color = txtColor.Text
            Carta.Placas = txtPlacas.Text
            Carta.Transportista = txtTransportista.Text
            Carta.Chofer = txtChofer.Text
            Carta.Lote = txtLote.Text
            Carta.Observaciones = txtObservaciones.Text
            db.Guardar(Carta)
            PopUp("Guardado", 90)
            Carta = New CartaSalida(0, Now.Date, "", "", "", "", "", "", "", "", "")
            Carta.Detalles.Add(New CartaSalidaDetalle(0, 0, "", 0))
            Carta.Sellos.Add(New CartaSalidaSello(0, ""))
            dgvDetalles.DataSource = Carta.Detalles
            dgvSellos.DataSource = Carta.Sellos
            dgvDetalles.Refresh()
            dgvSellos.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim f As New frmBuscarCartaSalida()
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim db As New dbCartasSalida(MySqlcon)
            Carta = db.Buscar(f.IdCarta)
            dgvDetalles.DataSource = Carta.Detalles
            dgvSellos.DataSource = Carta.Sellos
            dgvDetalles.Refresh()
            dgvSellos.Refresh()
        End If
    End Sub

    Private Sub frmCartaSalida_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        dgvDetalles.AutoGenerateColumns = False
        dgvSellos.AutoGenerateColumns = False
        Carta = New CartaSalida(0, Now.Date, "", "", "", "", "", "", "", "", "")
        Carta.Detalles.Add(New CartaSalidaDetalle(0, 0, "", 0))
        Carta.Sellos.Add(New CartaSalidaSello(0, ""))
        dgvDetalles.DataSource = Carta.Detalles
        dgvSellos.DataSource = Carta.Sellos
        dgvDetalles.Refresh()
        dgvSellos.Refresh()
    End Sub

    Private Sub dgvDetalles_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvDetalles.KeyDown
        If e.KeyCode = Keys.Enter Then
            If dgvDetalles.CurrentCell.RowIndex = dgvDetalles.RowCount - 1 Then
                Carta.Detalles.Add(New CartaSalidaDetalle(Carta.Id, 0, "", 0))
                dgvDetalles.DataSource = New ArrayList()
                dgvDetalles.DataSource = Carta.Detalles
                dgvDetalles.Refresh()
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            Dim rowIndex As Integer = dgvDetalles.CurrentCell.RowIndex
            Carta.Detalles.Remove(dgvDetalles.Rows(rowIndex).DataBoundItem)
            If Carta.Detalles.Count = 0 Then Carta.Detalles.Add(New CartaSalidaDetalle(Carta.Id, 0, "", 0))
            dgvDetalles.DataSource = New ArrayList()
            dgvDetalles.DataSource = Carta.Detalles
            If dgvDetalles.RowCount <= rowIndex Then dgvDetalles.Item(0, dgvDetalles.RowCount - 1).Selected = True
            dgvDetalles.Refresh()
        End If
    End Sub

    Private Sub dgvDetalles_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetalles.CellEndEdit
        dgvDetalles.Item(e.ColumnIndex + 1, e.RowIndex).Selected = True
        dgvDetalles.Refresh()
    End Sub

    Private Sub dgvSellos_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvSellos.KeyDown
        If e.KeyCode = Keys.Enter Then
            If dgvSellos.CurrentCell.RowIndex = dgvSellos.RowCount - 1 Then
                Carta.Sellos.Add(New CartaSalidaSello(Carta.Id, ""))
                dgvSellos.DataSource = New ArrayList()
                dgvSellos.DataSource = Carta.Sellos
                dgvSellos.Refresh()
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            Dim rowIndex As Integer = dgvSellos.CurrentCell.RowIndex
            Carta.Sellos.Remove(dgvSellos.Rows(rowIndex).DataBoundItem)
            If Carta.Sellos.Count = 0 Then Carta.Sellos.Add(New CartaSalidaSello(Carta.Id, ""))
            dgvSellos.DataSource = New ArrayList()
            dgvSellos.DataSource = Carta.Sellos
            If dgvSellos.RowCount <= rowIndex Then dgvSellos.Item(0, dgvSellos.RowCount - 1).Selected = True
            dgvSellos.Refresh()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Carta = New CartaSalida(0, Now, "", "", "", "", "", "", "", "", "")
        Carta.Detalles.Add(New CartaSalidaDetalle(0, 0, "", 0))
        Carta.Sellos.Add(New CartaSalidaSello(0, ""))
        dgvDetalles.DataSource = Carta.Detalles
        dgvSellos.DataSource = Carta.Sellos
        dgvDetalles.Refresh()
        dgvSellos.Refresh()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class