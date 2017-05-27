Public Class frmGastosReportes
    Dim IdsSucursales As New elemento
    Dim IdsVendedores As New elemento
    Dim IdsClasificacion As New elemento
    Dim IdsClasificacion2 As New elemento
    Dim IdsClasificacion3 As New elemento
    Dim sa As New dbSucursalesArchivos
    Dim IdsCajas As New elemento
    Dim idCaja As Integer = -1
    Dim idSucursal As Integer = -1
    Dim idClasificacion As Integer = -1
    Dim idVendedor As Integer = -1
    Dim fechaInicial As String = ""
    Dim fechaFinal As String = ""
    Dim Co As New dbGastos(MySqlcon)

    Private Sub frmGastosReportes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ComboBox3.Items.Add("Todos")
        ComboBox3.Items.Add("Depósito")
        ComboBox3.Items.Add("Retiro")
        ComboBox3.SelectedIndex = 0
        LlenaCombos("tblsucursales", cmbSucursal, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        LlenaCombos("tblvendedores", cmbVendedor, "nombre", "nombret", "idvendedor", IdsVendedores, , "Todos")
        LlenaCombos("tblgastosclasificacion", cmbClasificacion, "concat(clave,' ',nombre)", "nombret", "idClasificacion", IdsClasificacion, , "Todas", "clave")
        dtpFechaHasta.MinDate = dtpFecha.Value

        'sa.DaOpciones(GlobalIdEmpresa, False)
        'idCaja = sa.idCaja
    End Sub

    Private Sub cmbSucursal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged
        LlenaCombos("tblcajas", cmbCaja, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, "Todas")
        idSucursal = IdsSucursales.Valor(cmbSucursal.SelectedIndex)
    End Sub

    Private Sub cmbClasificacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClasificacion.SelectedIndexChanged
        If cmbClasificacion.SelectedIndex <> 0 Then
            idClasificacion = IdsClasificacion.Valor(cmbClasificacion.SelectedIndex)
        Else
            idClasificacion = -1
        End If
        LlenaCombos("tblgastosclasificacion2", cmbclasificacion2, "concat(clave,' ',nombre)", "nombret", "idclasificacion", IdsClasificacion2, "idclassuperior=" + IdsClasificacion.Valor(cmbClasificacion.SelectedIndex).ToString, "Todas", "clave")
    End Sub

    Private Sub cmbVendedor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendedor.SelectedIndexChanged
        If cmbVendedor.SelectedIndex = 0 Then
            idVendedor = -1
        Else
            idVendedor = IdsVendedores.Valor(cmbVendedor.SelectedIndex)
        End If
    End Sub

    Private Sub dtpFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        dtpFechaHasta.MinDate = dtpFecha.Value
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Dim sRenglon As String = ""
        If chkFecha.Checked = True Then
            fechaInicial = dtpFecha.Value.ToString("yyyy/MM/dd")
            fechaFinal = dtpFechaHasta.Value.ToString("yyyy/MM/dd")
        Else
            fechaFinal = ""
            fechaInicial = ""
        End If
        idSucursal = IdsSucursales.Valor(cmbSucursal.SelectedIndex)
        If cmbCaja.Items.Count > 0 Then
            idCaja = IdsCajas.Valor(cmbCaja.SelectedIndex)
        Else
            idCaja = -1
        End If
        idVendedor = IdsVendedores.Valor(cmbVendedor.SelectedIndex)
        idClasificacion = IdsClasificacion.Valor(cmbClasificacion.SelectedIndex)
        If lstTipos.SelectedIndex = 0 Then

            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            Rep = New repGastosTodos
            Rep.SetDataSource(Co.filtroTodos(fechaInicial, fechaFinal, idSucursal, idCaja, idVendedor))
            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", S.Telefono)
            If fechaFinal <> "" Then
                Rep.SetParameterValue("fechas", "GASTOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
            Else
                Rep.SetParameterValue("fechas", " ")
            End If
            If idSucursal > 0 Then
                sRenglon += "SUCURSAL: " + cmbSucursal.Text
            End If
            If idCaja > 0 Then
                sRenglon += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor > 0 Then
                sRenglon += " VENDEDOR: " + cmbVendedor.Text
            End If
            Rep.SetParameterValue("sucursal", sRenglon)


            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
        If lstTipos.SelectedIndex = 1 Then

            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            Rep = New repGastosDetalles
            Rep.SetDataSource(Co.filtroDetalles(fechaInicial, fechaFinal, idSucursal, idCaja, idVendedor, idClasificacion, IdsClasificacion2.Valor(cmbclasificacion2.SelectedIndex), IdsClasificacion3.Valor(cmbClasificacion3.SelectedIndex)))
            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", S.Telefono)
            If fechaFinal <> "" Then
                Rep.SetParameterValue("fechas", "GASTOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
            Else
                Rep.SetParameterValue("fechas", " ")
            End If
            If idSucursal > 0 Then
                sRenglon += "SUCURSAL: " + cmbSucursal.Text
            End If
            If idCaja > 0 Then
                sRenglon += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor > 0 Then
                sRenglon += " VENDEDOR: " + cmbVendedor.Text
            End If
            Rep.SetParameterValue("sucursal", sRenglon)


            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
        If lstTipos.SelectedIndex = 2 Then

            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            Rep = New repGastosDetallesClas
            Rep.SetDataSource(Co.filtroDetalles(fechaInicial, fechaFinal, idSucursal, idCaja, idVendedor, idClasificacion, IdsClasificacion2.Valor(cmbclasificacion2.SelectedIndex), IdsClasificacion3.Valor(cmbClasificacion3.SelectedIndex)))
            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", S.Telefono)
            If fechaFinal <> "" Then
                Rep.SetParameterValue("fechas", "GASTOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
            Else
                Rep.SetParameterValue("fechas", " ")
            End If
            If idSucursal > 0 Then
                sRenglon += "SUCURSAL: " + cmbSucursal.Text
            End If
            If idCaja > 0 Then
                sRenglon += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor > 0 Then
                sRenglon += " VENDEDOR: " + cmbVendedor.Text
            End If
            Rep.SetParameterValue("sucursal", sRenglon)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If

        If lstTipos.Text = "Movimientos de caja." Then
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CM As New dbCajasMovimientos(MySqlcon)
            Dim OC As New dbOpcionesOc(MySqlcon)
            OC.LlenaDatos(0, GlobalIdSucursalDefault)
            Rep = New repCajasMovimientos
            Rep.SetDataSource(CM.Reporte(Format(dtpFecha.Value, "yyyy/MM/dd"), Format(dtpFechaHasta.Value, "yyyy/MM/dd"), IdsSucursales.Valor(cmbSucursal.SelectedIndex), 0, 0, False, IdsVendedores.Valor(cmbVendedor.SelectedIndex), "", IdsCajas.Valor(cmbCaja.SelectedIndex), ComboBox3.SelectedIndex, OC.OcultarOc, OC.SeriesOc))
            Rep.SetParameterValue("Encabezado", S.Nombre)
            Dim Filtros As String
            Filtros = "Fechas: " + Format(dtpFecha.Value, "yyyy/MM/dd") + " al " + Format(dtpFechaHasta.Value, "yyyy/MM/dd") + " Sucursal: " + cmbSucursal.Text + " Vendedor: " + cmbVendedor.Text + " Caja: " + cmbCaja.Text + " Tipo Mov: " + ComboBox3.Text
            Rep.SetParameterValue("Filtros", Filtros)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub cmbclasificacion2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbclasificacion2.SelectedIndexChanged
        LlenaCombos("tblgastosclasificacion3", cmbClasificacion3, "concat(clave,' ',nombre)", "nombret", "idclasificacion", IdsClasificacion3, "idclassuperior=" + IdsClasificacion2.Valor(cmbclasificacion2.SelectedIndex).ToString, "Todas", "clave")
    End Sub

    Private Sub cmbClasificacion3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClasificacion3.SelectedIndexChanged

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class