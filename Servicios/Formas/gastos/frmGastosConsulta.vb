Public Class frmGastosConsulta
    Public IdCompra As Integer
    Dim ConsultaOn As Boolean = True
    Dim idsSucursales As New elemento
    Dim idsCajas As New elemento
    Dim Modo As Integer
    Dim idsClasificacion As New elemento
    Public Sub New(ByVal pModo As Integer)
        InitializeComponent()
        Modo = pModo
    End Sub
    Private Sub frmGastosConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        cmbEstado.Items.Add("Todas")
        cmbEstado.Items.Add("Pendientes")
        cmbEstado.Items.Add("Guardadas")
        cmbEstado.Items.Add("Canceladas")
        cmbEstado.SelectedIndex = 0
        LlenaCombos("tblsucursales", cmbSucursal, "nombre", "nombret", "idsucursal", idsSucursales, , "Todas")
        '  LlenaCombos("tblgastosclasificacion", cmbClasificacion, "nombre", "nombret", "idClasificacion", idsClasificacion)
        dtpFecha1.Value = CDate(Format(Date.Now, "yyyy") + "/01/01")
        dtpFecha2.Value = Date.Now
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        ConsultaOn = True
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub cmbSucursal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged
        LlenaCombos("tblcajas", cmbCaja, "nombre", "nombret", "idcaja", idsCajas, "idcaja>1 and idsucursal=" + idsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, "Todas")
        Consulta()
    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim Estado As Byte

                Select Case cmbEstado.SelectedIndex
                    Case 0
                        Estado = 0
                    Case 1
                        Estado = Estados.Pendiente
                    Case 2
                        Estado = Estados.Guardada
                    Case 3
                        Estado = Estados.Cancelada
                End Select
                Dim S As New dbGastos(MySqlcon)
                DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), Estado, txtBusqueda.Text, idsSucursales.Valor(cmbSucursal.SelectedIndex), idsCajas.Valor(cmbCaja.SelectedIndex))
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Folio"
                DGServicios.Columns(3).HeaderText = "Total"
                DGServicios.Columns(4).HeaderText = "Caja"
                DGServicios.Columns(5).HeaderText = "Estado"

                DGServicios.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
                IdCompra = 0

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub txtBusqueda_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBusqueda.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dtpFecha1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha1.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub dtpFecha2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha2.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        IdCompra = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        IdCompra = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
        AbreDetalles()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Consulta()
    End Sub

    Private Sub cmbClasificacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub cmbCaja_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCaja.SelectedIndexChanged
        Consulta()
    End Sub

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionar.Click
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
End Class