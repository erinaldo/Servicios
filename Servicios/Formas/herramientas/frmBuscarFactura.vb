Public Class frmBuscarFactura
    Private consulta As dbConsultaFacturas
    Private tablaReporte As DataTable
    Private tablaFolio As DataTable
    Private ruta As String

    Public Sub New(ByVal rutaValidadas As String)

        ' This call is required by the designer.
        InitializeComponent()
        Me.ruta = rutaValidadas
        ' Add any initialization after the InitializeComponent() call.
        tablaFolio = New DataTable
    End Sub
    Private Sub frmBuscarFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono

        Catch ex As Exception

        End Try
        consulta = New dbConsultaFacturas(MySqlcon)
        checkReal.Checked = GlobalConsultaTiempoReal
        dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        configuraTablaFolio()
    End Sub

    Private Sub buscar()
        If txtFolio.Text <> "" Then
            dgvFacturas.DataSource = consulta.buscaFolio(txtFolio.Text, tablaFolio, ruta)
        Else
            dgvFacturas.DataSource = consulta.filtrar(txtRFC.Text, txtNombre.Text, txtFolio.Text, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), txtUUDI.Text)
        End If
        If dgvFacturas.Rows.Count > 0 Then
            dgvFacturas.CurrentRow.Selected = False
        End If
    End Sub

    Private Sub imprimir()
        tablaReporte = New DataTable
        configuraTablaReporte()
        tablaReporte.TableName = "Facturas"
        Dim ds As New DataSet
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repFacturasValidadas
        If dgvFacturas.SelectedRows.Count > 0 Then
            For i As Integer = 0 To dgvFacturas.Rows.Count - 1
                If dgvFacturas.Rows(i).Selected Then
                    consulta.imprimir(dgvFacturas.Rows(i).Cells("UUID").Value.ToString(), tablaReporte, "xml Validado", ruta)
                End If
            Next
        Else
            For i As Integer = 0 To dgvFacturas.Rows.Count - 1
                consulta.imprimir(dgvFacturas.Rows(i).Cells("UUID").Value.ToString(), tablaReporte, "xml Validado", ruta)
            Next
        End If
        ds.Tables.Add(tablaReporte)
        rep.SetDataSource(ds)
        rep.SetParameterValue("nombreEmpresa", GlobalNombreEmpresa)
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Dispose()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        imprimir()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        buscar()
    End Sub

    Private Sub txtFolio_TextChanged(sender As Object, e As EventArgs) Handles txtFolio.TextChanged
        If checkReal.Checked Then
            buscar()
        End If
    End Sub

    Private Sub txtRFC_TextChanged(sender As Object, e As EventArgs) Handles txtRFC.TextChanged
        If checkReal.Checked Then
            buscar()
        End If
    End Sub

    Private Sub txtNombre_TextChanged(sender As Object, e As EventArgs) Handles txtNombre.TextChanged
        txtFolio.Text = ""
        If checkReal.Checked Then
            buscar()
        End If
    End Sub

    Private Sub txtUUDI_TextChanged(sender As Object, e As EventArgs) Handles txtUUDI.TextChanged
        txtFolio.Text = ""
        If checkReal.Checked Then
            buscar()
        End If
    End Sub

    Private Sub dtpDesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpDesde.ValueChanged
        If checkReal.Checked Then
            buscar()
        End If
    End Sub

    Private Sub dtpHasta_ValueChanged(sender As Object, e As EventArgs) Handles dtpHasta.ValueChanged
        If checkReal.Checked Then
            buscar()
        End If
    End Sub

    Private Sub configuraTablaReporte()
        tablaReporte.Columns.Add("Versión", GetType(String))
        tablaReporte.Columns.Add("Tipo Comprobante", GetType(String))
        tablaReporte.Columns.Add("Certificado SAT", GetType(String))
        tablaReporte.Columns.Add("Certificado Emisor", GetType(String))
        tablaReporte.Columns.Add("Fecha Emisión", GetType(String))
        tablaReporte.Columns.Add("Fecha certificación", GetType(String))
        tablaReporte.Columns.Add("UUID", GetType(String))
        tablaReporte.Columns.Add("Importe Total", GetType(String))
        tablaReporte.Columns.Add("RFC Emisor", GetType(String))
        tablaReporte.Columns.Add("Nombre Emisor", GetType(String))
        tablaReporte.Columns.Add("RFC Receptor", GetType(String))
        tablaReporte.Columns.Add("Nombre Receptor", GetType(String))
        tablaReporte.Columns.Add("Resultado de Validación", GetType(String))
        tablaReporte.Columns.Add("Folio", GetType(String))
    End Sub

    Private Sub configuraTablaFolio()
        tablaFolio.Columns.Add("Fecha", GetType(String))
        tablaFolio.Columns.Add("UUID", GetType(String))
        tablaFolio.Columns.Add("RFC", GetType(String))
        tablaFolio.Columns.Add("Total", GetType(String))
    End Sub
End Class