Public Class frmCajasMovimientosConsulta

    Public IdCompra As Integer
    Dim ConsultaOn As Boolean = True
    Dim idsSucursales As New elemento
    Dim idsCajas As New elemento
    Dim Modo As Integer
    Public Sub New(ByVal pModo As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Modo = pModo

    End Sub
    Private Sub frmComprasConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        ComboBox2.Items.Add("Todas")
        ComboBox2.Items.Add("Pendientes")
        ComboBox2.Items.Add("Guardadas")
        ComboBox2.Items.Add("Canceladas")
        ComboBox4.Items.Add("Todos")
        ComboBox4.Items.Add("Ingreso")
        ComboBox4.Items.Add("Retiro")
        ComboBox2.SelectedIndex = 0
        ComboBox4.SelectedIndex = 0
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", idsSucursales, , "Todas")

        DateTimePicker2.Value = CDate(Format(Date.Now, "yyyy") + "/01/01")
        DateTimePicker3.Value = Date.Now
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        ConsultaOn = True
        If chkTiempoReal.Checked Then Consulta()
    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim Estado As Byte

                Select Case ComboBox2.SelectedIndex
                    Case 0
                        Estado = 0
                    Case 1
                        Estado = Estados.Pendiente
                    Case 2
                        Estado = Estados.Guardada
                    Case 3
                        Estado = Estados.Cancelada
                End Select
                Dim S As New dbCajasMovimientos(MySqlcon)
                DGServicios.DataSource = S.Consulta(Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), Estado, TextBox2.Text, ComboBox4.SelectedIndex, idsSucursales.Valor(ComboBox1.SelectedIndex), idsCajas.Valor(ComboBox6.SelectedIndex))
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Folio"
                DGServicios.Columns(3).HeaderText = "Importe"
                DGServicios.Columns(4).HeaderText = "Tipo"
                DGServicios.Columns(5).HeaderText = "Caja"
                DGServicios.Columns(6).HeaderText = "Estado"
                'DGServicios.Columns(2).Width = 130
                DGServicios.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
                IdCompra = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        AbreDetalles()
    End Sub
    Private Sub AbreDetalles()
        If IdCompra <> 0 Then
            If Modo = ModosDeBusqueda.Principal Then
                Dim F As New frmCompras(DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value)
                F.ShowDialog()
            Else
                IdCompra = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker3.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        IdCompra = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        IdCompra = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
        AbreDetalles()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Consulta()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        'If ComboBox1.SelectedIndex > 0 Then
        LlenaCombos("tblcajas", ComboBox6, "nombre", "nombret", "idcaja", idsCajas, "idcaja>1 and idsucursal=" + idsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Todas")
        Consulta()
        'End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        Consulta()
    End Sub
End Class