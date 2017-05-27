Public Class frmRestauranteBuscaVenta
    Private config As New dbRestauranteConfiguracion(1, MySqlcon)
    Public ventas As New dbRestauranteVentas(MySqlcon)
    Public pedidos As New dbRestaurantePedidos(MySqlcon)
    Public idventa As Integer = -1
    Private idSucursal As Integer = -1
    Public idPedido As Integer = -1
    Private IdsSucursales As New elemento
    Private desde As String
    Private hasta As String
    Private tipo As tipoBusqueda

    Public Enum tipoBusqueda
        ventas = 1
        pedidos = 2
        muestraVenta = 3
    End Enum

    Public Sub New(ByVal tipo As tipoBusqueda, Optional ByVal idPedido As Integer = -1)
        Me.tipo = tipo
        If idPedido > 0 Then
            Me.idPedido = idPedido
            pedidos.buscar(idPedido)
            ventas.buscar(pedidos.idVenta)
            txtFolio.Text = pedidos.folio
        End If
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmRestauranteBuscaVenta_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        'panelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub frmRestauranteBuscaVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.BackColor = Color.FromArgb(config.colorVentanas)
        Catch ex As Exception

        End Try
        CheckBox1.Checked = GlobalModoBusqueda
        LlenaCombos("tblsucursales", comboSucursal, "nombre", "nombret", "idsucursal", IdsSucursales)
        hora1.CustomFormat = "HH:mm:ss"
        hora2.CustomFormat = "HH:mm:ss"
        hora1.Visible = False
        hora2.Visible = False
        Label4.Visible = False
        If tipo = tipoBusqueda.pedidos Then
            btnGuardar.Visible = True
        End If
        If tipo = tipoBusqueda.muestraVenta Then
            txtFolio.Enabled = False
            Label2.Visible = False
            Label3.Visible = False
            Label4.Visible = False
            dtp2.Visible = False
            hora2.Visible = False
            btnBuscar.Visible = False
            comboSucursal.Visible = False
            CheckBox1.Visible = False
            configuraVentas()
        End If
        'panelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub comboSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboSucursal.SelectedIndexChanged
        idSucursal = IdsSucursales.Valor(comboSucursal.SelectedIndex)
        If CheckBox1.Checked Then
            buscar()
        End If
    End Sub

    Private Sub buscar()
        If tipo = tipoBusqueda.ventas Then
            dgvVentas.DataSource = ventas.buscar(IdsSucursales.Valor(comboSucursal.SelectedIndex), dtp1.Value.ToString("yyyy/MM/dd"), dtp2.Value.ToString("yyyy/MM/dd"))
            configuragrid()
        End If
        If tipo = tipoBusqueda.pedidos Then
            dgvVentas.DataSource = pedidos.vistaPedidos(dtp1.Value.ToString("yyyy/MM/dd"), dtp2.Value.ToString("yyyy/MM/dd"))
            configuraGridPedidos()
            For Each r As DataGridViewRow In dgvVentas.Rows
                If r.Cells("estado").Value = CInt(Estados.Pendiente) Then
                    r.DefaultCellStyle.BackColor = Color.Yellow
                End If
            Next
        End If
        If tipo = tipoBusqueda.muestraVenta Then
            dgvVentas.DataSource = ventas.vistaDetalles(ventas.idVenta, estadosPlatillos.pendiente, Estados.Pendiente)
        End If
        dgvVentas.ClearSelection()
    End Sub

    Private Sub dtp1_ValueChanged(sender As Object, e As EventArgs) Handles dtp1.ValueChanged
        If CheckBox1.Checked Then
            buscar()
        End If
    End Sub

    Private Sub dtp2_ValueChanged(sender As Object, e As EventArgs) Handles dtp2.ValueChanged
        If CheckBox1.Checked Then
            buscar()
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        buscar()
    End Sub
    Private Sub configuragrid()
        dgvVentas.Columns(0).Visible = False
        dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvVentas.MultiSelect = False
    End Sub

    Private Sub configuraGridPedidos()
        dgvVentas.Columns(0).Visible = False
        dgvVentas.Columns(1).Visible = False
        dgvVentas.Columns(2).Visible = False
        dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvVentas.MultiSelect = True
    End Sub

    Private Sub dgvVentas_Click(sender As Object, e As EventArgs) Handles dgvVentas.Click
        Try
            If tipo = tipoBusqueda.ventas Then
                idventa = CInt(dgvVentas.CurrentRow.Cells(0).Value.ToString())
            End If
            If tipo = tipoBusqueda.pedidos Then
                idPedido = CInt(dgvVentas.CurrentRow.Cells(0).Value.ToString())
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvVentas_DoubleClick(sender As Object, e As EventArgs) Handles dgvVentas.DoubleClick
        idventa = CInt(dgvVentas.CurrentRow.Cells(0).Value.ToString())
        Dispose()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Dispose()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        For i As Integer = 0 To dgvVentas.Rows.Count - 1
            If dgvVentas.Rows(i).Selected Then
                pedidos.buscar(CInt(dgvVentas.Rows(i).Cells(0).Value.ToString))
                Dim v As Integer = pedidos.idVenta
                pedidos.modificar(pedidos.idPedido, pedidos.idVenta, pedidos.fecha, pedidos.hora, pedidos.serie, pedidos.folio, CInt(Estados.Guardada), pedidos.idVendedor, 0)
                ventas.buscar(v)
                ventas.modificar(ventas.idVenta, ventas.idCliente, ventas.descuento, ventas.total, ventas.totalapagar, CInt(Estados.Guardada), ventas.fecha, ventas.idSucursal, GlobalUsuarioIdVendedor, 0, "")
            End If
        Next
        buscar()
        idPedido = -1
    End Sub

    Private Sub configuraVentas()
        dgvVentas.Columns(0).Visible = False
        dgvVentas.Columns(1).HeaderText = "Cantidad"
        dgvVentas.Columns(2).HeaderText = "Producto"
        dgvVentas.Columns(3).HeaderText = "Precio Unit."
        dgvVentas.Columns(4).HeaderText = "Total"
    End Sub
End Class