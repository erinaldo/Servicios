Public Class frmConsultaUbicaciones
    Private idarticulo As String
    Private idsucursal As Integer
    Public Sub New(idsucursal As Integer, idarticulo As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.idsucursal = idsucursal
        Me.idarticulo = idarticulo
    End Sub
    Private Sub frmConsultaUbicaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        DataGridView1.AutoGenerateColumns = False
        Dim dba As New dbAlmacenes(MySqlcon)
        cmbAlmacen.DataSource = dba.Consulta()
        cmbAlmacen.SelectedIndex = -1
        Dim db As New dbInventario(idarticulo, MySqlcon)
        txtArticulo.Text = db.Nombre
        'If cmbAlmacen.SelectedIndex = -1 Then
        '    DataGridView1.DataSource = db.ConsultaInventarioPorUbicacion(idsucursal, 0, idarticulo, txtUbicacion.Text)
        'Else
        '    DataGridView1.DataSource = db.ConsultaInventarioPorUbicacion(idsucursal, cmbAlmacen.SelectedValue, idarticulo, txtUbicacion.Text)
        'End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim db As New dbInventario(idarticulo, MySqlcon)
        Dim r As New repInventarioAlmacenesUbicaciones
        If cmbAlmacen.SelectedIndex = -1 Then
            r.SetDataSource(db.ConsultaInventarioPorUbicacion(idsucursal, 0, idarticulo, txtUbicacion.Text))
            r.SetParameterValue("Encabezado", GlobalNombreEmpresa)
            r.SetParameterValue("Filtros", "Articulo: " + db.Clave + " Ubicación: " + txtUbicacion.Text)
            Dim f As New frmReportes(r, False)
            f.Show()
        Else
            r.SetDataSource(db.ConsultaInventarioPorUbicacion(idsucursal, cmbAlmacen.SelectedValue, idarticulo, txtUbicacion.Text))
            r.SetParameterValue("Encabezado", GlobalNombreEmpresa)
            r.SetParameterValue("Filtros", "Articulo: " + db.Clave + " Almacén: " + cmbAlmacen.Text + " Ubicación: " + txtUbicacion.Text)
            Dim f As New frmReportes(r, False)
            f.Show()
        End If
    End Sub

    Private Sub txtUbicacion_TextChanged(sender As Object, e As EventArgs) Handles txtUbicacion.TextChanged
        Dim db As New dbInventario(MySqlcon)
        If cmbAlmacen.SelectedIndex = -1 Then
            DataGridView1.DataSource = db.ConsultaInventarioPorUbicacion(idsucursal, 0, idarticulo, txtUbicacion.Text)
        Else
            DataGridView1.DataSource = db.ConsultaInventarioPorUbicacion(idsucursal, cmbAlmacen.SelectedValue, idarticulo, txtUbicacion.Text)
        End If
    End Sub

    Private Sub cmbAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlmacen.SelectedIndexChanged
        Dim db As New dbInventario(MySqlcon)
        If cmbAlmacen.SelectedIndex = -1 Then
            DataGridView1.DataSource = db.ConsultaInventarioPorUbicacion(idsucursal, 0, idarticulo, txtUbicacion.Text)
        Else
            DataGridView1.DataSource = db.ConsultaInventarioPorUbicacion(idsucursal, cmbAlmacen.SelectedValue, idarticulo, txtUbicacion.Text)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class