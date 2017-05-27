Public Class frmConfiguracionCorte
    Private conceptos As String() = {"FACTURAS CONTADO:",
"FACTURAS CRÉDITO:",
"COBRANZA FACTURAS:",
"REMISIONES CONTADO EFECTIVO:",
"REMISIONES CONTADO NO EFECTIVO:",
"REMISIONES CRÉDITO:",
"COBRANZA REMISIONES:",
"DEVOLUCIONES FACTURAS:",
"DEVOLUCIONES REMISIONES:",
"NOTAS DE CRÉDITO VENTAS:",
"NOTAS DE CARGO VENTAS:",
"APARTADOS:",
"COBRANZA APARTADOS:",
"GASTOS:",
"DOCUMENTOS CLIENTES:"}
    Private seleccion As List(Of String)
    Private configuracion As List(Of String)
    Private aux As List(Of String)
    Private v As dbVentas
    Dim Op As dbOpciones
    Private Sub frmConfiguracionCorte_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        v = New dbVentas(MySqlcon)
        Op = New dbOpciones(MySqlcon)
        configuracion = v.listaConfiguracion()
        llenaGrid(dgvCorte, conceptos)
        seleccion = New List(Of String)
        aux = New List(Of String)
        checaConfiguracion()
        If Op.VentasCorteRemTodas = 1 Then chkRemCorte.Checked = True
        If Op.VentasCorteRemxMetodo = 1 Then CheckBox1.Checked = True
    End Sub

    Private Sub llenaGrid(ByVal dgv As DataGridView, ByVal datos As String())
        dgv.DataSource = Nothing
        Dim t As New DataTable
        t.Columns.Add("Concepto")
        For Each s As String In datos
            Dim r As DataRow = t.NewRow
            r("Concepto") = s
            t.Rows.Add(r)
        Next

        dgv.DataSource = t
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.ClearSelection()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For i As Integer = 0 To dgvCorte.Rows.Count - 1
            If dgvCorte.Rows(i).Selected Then
                configuracion.Add(dgvCorte.Rows(i).Cells(0).Value.ToString())
            End If
        Next
        checaConfiguracion()
    End Sub
    Private Sub llenaGrid2()
        dgvCorte2.DataSource = Nothing
        'configuracion.AddRange(seleccion)
        Dim t As New DataTable
        t.Columns.Add("Concepto")
        For Each s As String In configuracion
            Dim r As DataRow = t.NewRow
            r("Concepto") = s
            t.Rows.Add(r)
        Next
        dgvCorte2.DataSource = t
        dgvCorte2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For i As Integer = 0 To dgvCorte.Rows.Count - 1
            configuracion.Add(dgvCorte.Rows(i).Cells(0).Value.ToString())
        Next
        checaConfiguracion()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For i As Integer = 0 To dgvCorte2.Rows.Count - 1
            If dgvCorte2.Rows(i).Selected Then
                configuracion.Remove(dgvCorte2.Rows(i).Cells(0).Value.ToString())
            End If
        Next
        checaConfiguracion()
    End Sub

    Private Sub checaConfiguracion()
        aux = New List(Of String)
        aux.AddRange(conceptos)
        For i As Integer = 0 To configuracion.Count - 1
            aux.Remove(configuracion(i))
        Next
        llenaGrid(dgvCorte, aux.ToArray)
        llenaGrid2()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        For i As Integer = 0 To dgvCorte2.Rows.Count - 1
            configuracion.Remove(dgvCorte2.Rows(i).Cells(0).Value.ToString())
        Next
        checaConfiguracion()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        v.configuracionDeCorte(configuracion)
        Dim rt As Byte = 0
        Dim rm As Byte = 0
        If chkRemCorte.Checked Then rt = 1
        If CheckBox1.Checked Then rm = 1
        Op.GuardaOpcionCorte(rt, rm)
        PopUp("Guardado.", 30)
        Dispose()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Dispose()
    End Sub
End Class