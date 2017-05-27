Public Class frmSemillasReportes
    Public Enum tiporeporte
        boletas = 0
        liquidacion = 1
        comprobante = 2
    End Enum
    Private op As tiporeporte = 0
    Private generaReporte As dbSemillasgenerarReporte
    Private productores As dbproveedores
    Private productos As dbInventario
    Private clientes As dbClientes
    Private tipoBoleta As Char
    Private tipos As String() = {"Todas", "Entrada", "Salida"}


    Public Sub New(pTipo As Byte)

        ' This call is required by the designer.
        InitializeComponent()
        generaReporte = New dbSemillasgenerarReporte(MySqlcon)
        ' Add any initialization after the InitializeComponent() call.
        productores = New dbproveedores(MySqlcon)
        productos = New dbInventario(MySqlcon)
        clientes = New dbClientes(MySqlcon)
        'chkTiempo.Checked = GlobalConsultaTiempoReal
        ListBox1.Items.Add("Boletas")
        If pTipo = 0 Then
            ListBox1.Items.Add("Liquidaciones")
            ListBox1.Items.Add("Comprobantes")
        End If
        ListBox1.SelectedIndex = 0
        ComboBox1.Items.AddRange(tipos)
    End Sub


    Private Sub btnProductor_Click(sender As Object, e As EventArgs) Handles btnProductor.Click
        If op = tiporeporte.boletas Or op = tiporeporte.liquidacion Then
            If tipoBoleta = "E" Then
                Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 1, False, False, False)
                frm.ShowDialog()
                If Not frm.Proveedor Is Nothing Then
                    productores = frm.Proveedor
                    txtClaveProductor.Text = productores.Clave
                    txtProductor.Text = productores.Nombre
                End If
            Else
                Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 1, False, False, False)
                frm.ShowDialog()
                If Not frm.Cliente Is Nothing Then
                    clientes = frm.Cliente
                    txtClaveProductor.Text = clientes.Clave
                    txtProductor.Text = clientes.Nombre
                End If
            End If
        Else
            Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 1, False, False, False)
            frm.ShowDialog()
            If Not frm.Cliente Is Nothing Then
                clientes = frm.Cliente
                txtClaveProductor.Text = clientes.Clave
                txtProductor.Text = clientes.Nombre
            End If
        End If
    End Sub

    Private Sub txtClaveProductor_TextChanged(sender As Object, e As EventArgs) Handles txtClaveProductor.TextChanged
        'If chkTiempo.Checked Then
        If op = tiporeporte.comprobante Then
            Dim clave As String = txtClaveProductor.Text
            If clientes.BuscaCliente(clave) Then
                txtProductor.Text = clientes.Nombre
            Else
                txtProductor.Text = ""
            End If
        End If
        If op = tiporeporte.boletas Then
            If tipoBoleta = "S" Then
                Dim clave As String = txtClaveProductor.Text
                If clientes.BuscaCliente(clave) Then
                    txtProductor.Text = clientes.Nombre
                End If
            Else
                Dim clave As String = txtClaveProductor.Text
                If productores.BuscaProveedor(clave) Then
                    txtProductor.Text = productores.Nombre
                Else
                    txtProductor.Text = ""
                End If
            End If
        End If
        If op = tiporeporte.liquidacion Then
            Dim clave As String = txtClaveProductor.Text
            If productores.BuscaProveedor(clave) Then
                txtProductor.Text = productores.Nombre
            Else
                txtProductor.Text = ""
            End If
        End If
        If txtClaveProductor.Text = "" Then
            clientes = New dbClientes(MySqlcon)
            productores = New dbproveedores(MySqlcon)
        End If
        'End If
    End Sub

    Private Sub txtClaveProducto_TextChanged(sender As Object, e As EventArgs) Handles txtClaveProducto.TextChanged
        'If chkTiempo.Checked Then
        Dim clave As String = txtClaveProducto.Text
        If productos.BuscaArticulo(clave, 1) Then
            txtProducto.Text = productos.Nombre
        Else
            productos = New dbInventario(MySqlcon)
            txtProducto.Text = ""
        End If
        'End If
    End Sub

    Private Sub btnProducto_Click(sender As Object, e As EventArgs) Handles btnProducto.Click
        Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.ArticuloInv, 0, False, False, False)
        frm.ShowDialog()
        If Not frm.Inventario Is Nothing Then
            productos = frm.Inventario
            txtClaveProducto.Text = productos.Clave
            txtProducto.Text = productos.Nombre
        End If
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Select Case op
            Case tiporeporte.boletas
                If dtpDesde.Value.ToString("yyyy/MM/dd") <> dtpHasta.Value.ToString("yyyy/MM/dd") Then
                    If dtpDesde.Value.ToString("yyyy/MM/dd") < dtpHasta.Value.ToString("yyyy/MM/dd") Then
                        generaReporte.reporteProductoProductor(productos.ID, productores.ID, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), tipoBoleta, clientes.ID)
                    Else
                        MsgBox("las fechas son incorrectas, la primera debe ser menor que la segunda")
                    End If
                Else
                    generaReporte.reporteProductoProductor(productos.ID, productores.ID, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), tipoBoleta, clientes.ID)
                End If
            Case tiporeporte.liquidacion
                If dtpDesde.Value.ToString("yyyy/MM/dd") <> dtpHasta.Value.ToString("yyyy/MM/dd") Then
                    If dtpDesde.Value.ToString("yyyy/MM/dd") < dtpHasta.Value.ToString("yyyy/MM/dd") Then
                        generaReporte.reporteLiquidaciones(productores.ID, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                    Else
                        MsgBox("las fechas son incorrectas, la primera debe ser menor que la segunda")
                    End If
                Else
                    generaReporte.reporteLiquidaciones(productores.ID, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                End If
            Case tiporeporte.comprobante
                If dtpDesde.Value.ToString("yyyy/MM/dd") <> dtpHasta.Value.ToString("yyyy/MM/dd") Then
                    If dtpDesde.Value.ToString("yyyy/MM/dd") < dtpHasta.Value.ToString("yyyy/MM/dd") Then
                        generaReporte.reporteComprobantes(clientes.ID, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                    Else
                        MsgBox("las fechas son incorrectas, la primera debe ser menor que la segunda")
                    End If
                Else
                    generaReporte.reporteComprobantes(clientes.ID, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                End If
        End Select
    End Sub

    Private Sub frmSemillasReportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = GlobalIcono
        ComboBox1.SelectedIndex = 1
        configura()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs)

    End Sub



    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Select Case ListBox1.SelectedIndex
            Case 0
                op = tiporeporte.boletas
                configura()
                nuevaConsulta()
                ComboBox1.Visible = True
                Label5.Visible = True
            Case 1
                op = tiporeporte.liquidacion
                configura()
                nuevaConsulta()
                ComboBox1.Visible = False
                Label5.Visible = False
            Case 2
                op = tiporeporte.comprobante
                configura()
                nuevaConsulta()
                ComboBox1.Visible = False
                Label5.Visible = False
        End Select
    End Sub

    Private Sub configura()
        If op = 0 Then
            If tipoBoleta = "S" Then
                Label1.Text = "Cliente:"
                nuevaConsulta()
            Else
                txtClaveProducto.Enabled = True
                txtClaveProductor.Enabled = True
                btnProducto.Enabled = True
                btnProductor.Enabled = True
                Label1.Text = "Productor:"
                nuevaConsulta()
            End If
            If tipoBoleta = "T" Then
                btnProductor.Enabled = False
                txtClaveProductor.Enabled = False
            End If
            If tipoBoleta = "E" Then
                btnProductor.Enabled = True
                txtClaveProductor.Enabled = True
                nuevaConsulta()
            End If
        ElseIf op = 1 Then
            txtClaveProducto.Enabled = False
            btnProducto.Enabled = False
            Label1.Text = "Productor:"
        ElseIf op = 2 Then
            txtClaveProducto.Enabled = False
            btnProducto.Enabled = False
            Label1.Text = "Cliente:"

        End If
    End Sub

    Private Sub nuevaConsulta()
        txtClaveProducto.Text = ""
        txtClaveProductor.Text = ""
        txtProducto.Text = ""
        txtProductor.Text = ""
        clientes = New dbClientes(MySqlcon)
        productores = New dbproveedores(MySqlcon)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        tipoBoleta = ComboBox1.SelectedItem.ToString().Chars(0)
        configura()
    End Sub
End Class