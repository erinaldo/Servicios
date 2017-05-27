Public Class frmContabilidadAuxiliarCuentas
    Dim Fecha1 As String
    Dim Fecha2 As String
    Dim IdCuenta As Integer
    Public Sub New(pFecha1 As String, pFecha2 As String, pIdCuenta As Integer, pCuentastr As String, pSaldoInicial As String, pSaldoFinal As String, pTotalCargos As String, ptotalAbonos As String)

        ' This call is required by the designer.
        InitializeComponent()
        Fecha1 = pFecha1
        Fecha2 = pFecha2
        IdCuenta = pIdCuenta
        lblDescripcion.Text = pCuentastr
        Label4.Text = pSaldoInicial
        Label7.Text = pSaldoFinal
        Label5.Text = pTotalCargos
        Label6.Text = ptotalAbonos
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmContabilidadAuxiliarCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Consulta()
    End Sub
    Private Sub Consulta()
        Try
            Dim P As New dbContabilidadPolizas(MySqlcon)
            DGServicios.DataSource = P.auxiliarCuentasPantalla(Fecha1, Fecha2, IdCuenta)
            DGServicios.Columns(0).Visible = False
            DGServicios.Columns(1).HeaderText = "Fecha"
            DGServicios.Columns(2).HeaderText = "Tipo"
            DGServicios.Columns(3).HeaderText = "Num."
            DGServicios.Columns(4).HeaderText = "Cuenta"
            DGServicios.Columns(5).HeaderText = "Descripción"
            DGServicios.Columns(6).HeaderText = "Cargo"
            DGServicios.Columns(7).HeaderText = "Abono"
            DGServicios.Columns(1).Width = 80
            DGServicios.Columns(2).Width = 40
            DGServicios.Columns(3).Width = 40
            DGServicios.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                DGServicios.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub DGServicios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub DGServicios_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        If DGServicios.CurrentCell.RowIndex >= 0 Then
            Dim Poliza As New frmContabilidadPolizasN(DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value)
            Poliza.ShowDialog()
            Poliza.Dispose()
        End If
    End Sub

    Private Sub DGServicios_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        If e.ColumnIndex = 6 Or e.ColumnIndex = 7 Then
            e.Value = Format(e.Value, "###,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub
End Class