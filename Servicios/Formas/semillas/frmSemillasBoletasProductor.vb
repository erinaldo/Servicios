Public Class frmSemillasBoletasProductor
    Private boletas As dbSemillasBoleta
    Private proveedores As dbproveedores
    Private detalles As dbSemillasDetalleLiquidacion
    Private liquidaciones As dbSemillasLiquidacion
    Private proveedor As dbproveedores
    Public producto As dbInventario
    Private boleta As dbSemillasBoleta
    Private liquidacion As dbSemillasLiquidacion

    Public Sub New(ByRef proveedor As dbproveedores)
        InitializeComponent()
        Me.proveedor = proveedor
        boletas = New dbSemillasBoleta(MySqlcon)
        detalles = New dbSemillasDetalleLiquidacion(MySqlcon)
        liquidaciones = New dbSemillasLiquidacion(MySqlcon)
        muestraBoletasProductor()
    End Sub
    Public Sub New(ByRef liquidacion As dbSemillasLiquidacion)
        InitializeComponent()
        Me.Icon = GlobalIcono
        Me.liquidacion = liquidacion
        boletas = New dbSemillasBoleta(MySqlcon)
        detalles = New dbSemillasDetalleLiquidacion(MySqlcon)
        liquidaciones = New dbSemillasLiquidacion(MySqlcon)
        Me.proveedor = liquidacion.proveedor
        'muestraBoletasProductor()
        muestraBoletas()
    End Sub

    Private Sub muestraBoletas()
        dgvBoletas.DataSource = boletas.consultaProductorLiquidadas(liquidacion.proveedor, False)
        'txtTotalPendientes.Text = detalles.sumaBoletas(liquidacion, False)
        configuraGrid()
    End Sub

    Private Sub muestraBoletasProductor()
        Dim dt As New DataTable
        dt.Columns.Add("id", GetType(Integer))
        dt.Columns.Add("folio", GetType(String))
        dt.Columns.Add("fecha", GetType(String))
        dt.Columns.Add("producto", GetType(String))
        dt.Columns.Add("productor", GetType(String))
        dt.Columns.Add("importe", GetType(Double))

        For Each b As dbSemillasBoleta In boletas.boletasProductorLiquidadas(proveedor, False)
            If boletas.checarBoletaPendiente(b.id) Then
                dt.Rows.Add(b.id, b.folio, b.fecha, b.producto.Nombre, b.productor.Nombre, b.importe)
            End If
        Next
        dgvBoletas.DataSource = dt
        configuraGrid()
    End Sub

    Private Sub liquidaBoleta(ByVal index As Integer)

        Dim id As Integer = Convert.ToInt32(dgvBoletas.Rows(index).Cells("id").Value)
        'Dim liquidar As Boolean = Convert.ToBoolean(dgvBoletas.Rows(index).Cells("liquidada").Value)
        boletas.liquidar(id, True)
        boleta = boletas.buscar(New dbSemillasBoleta(id))
        producto = New dbInventario(boleta.producto.ID, MySqlcon)
    End Sub

    Private Sub btnLiquidar_Click(sender As Object, e As EventArgs) Handles btnLiquidar.Click
        Dim aux As Object
        For i As Integer = 0 To dgvBoletas.Rows.Count - 1
            aux = dgvBoletas.Rows(i).Cells("seleccion").Value
            If Convert.ToBoolean(aux) = True Then
                liquidaBoleta(i)
                agregarBoleta(i)
            End If
        Next
        Dispose()
    End Sub

    Private Sub configuraGrid()
        dgvBoletas.Columns(1).Visible = False
        dgvBoletas.Columns(0).ReadOnly = False
        dgvBoletas.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub

    Private Sub agregarBoleta(ByVal index As Integer)
        Dim idBoleta As Integer = Convert.ToInt32(dgvBoletas.Rows(index).Cells("id").Value)
        Dim b As New dbSemillasBoleta(idBoleta)
        boleta = boletas.buscar(b)
        Dim d As New dbSemillasDetalleLiquidacion(boleta, liquidacion, boleta.importe, False)
        detalles.agregar(d)
    End Sub

    Private Sub dgvBoletas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBoletas.CellClick
        If dgvBoletas.Item(0, e.RowIndex).Value = 0 Then
            dgvBoletas.Item(0, e.RowIndex).Value = 1
        Else
            dgvBoletas.Item(0, e.RowIndex).Value = 0
        End If
    End Sub

    Private Sub frmSemillasBoletasProductor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        dgvBoletas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class