Public Class frmServiciosArticulosHistorial
    Public idEquipo As Integer
    Dim tipoEquipo As Integer
    Public Sub New(ByVal pIdEquipo As Integer, ByVal ptipoEquipo As Integer)
        InitializeComponent()
        idEquipo = pIdEquipo
        tipoEquipo = ptipoEquipo
    End Sub
    Private Sub frmServiciosArticulosHistorial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        filtros()
        'Dim CE As dbClientesEquipos
        Dim SE As New dbServiciosEquipos(MySqlcon)

    End Sub
    Private Sub filtros()
        Try
            Dim P As New dbServiciosEventos(MySqlcon)


            Dim TablaFull As New DataTable
            Dim Tabla As New DataTable
            TablaFull.Columns.Add("fecha")
            TablaFull.Columns.Add("idEvento")
            TablaFull.Columns.Add("codigo")
            TablaFull.Columns.Add("nombre")
            TablaFull.Columns.Add("cantidad")
            TablaFull.Columns.Add("precio")
            If tipoEquipo = 0 Then
                Tabla = P.filtroHistorialArticulos(idEquipo)
            Else
                Tabla = P.filtroHistorialArticulossuc(idEquipo)
            End If


            For i As Integer = 0 To Tabla.Rows.Count() - 1
                Dim dr As DataRow

                dr = TablaFull.NewRow()
                dr("fecha") = Tabla.Rows(i)(0).ToString
                dr("idEvento") = Tabla.Rows(i)(1).ToString
                P.InventarioUtilizado2(Integer.Parse(Tabla.Rows(i)(2).ToString))
                dr("codigo") = P.codigoInventario
                dr("nombre") = P.nombreInvenario
                dr("cantidad") = Tabla.Rows(i)(3).ToString
                dr("precio") = "$" + Format(Double.Parse(Tabla.Rows(i)(4)), "0.00")
                TablaFull.Rows.Add(dr)
            Next

            DGInventario.DataSource = TablaFull
            DGInventario.Columns(0).HeaderText = "Fecha"
            DGInventario.Columns(1).Visible = False
            DGInventario.Columns(2).HeaderText = "Código"
            DGInventario.Columns(3).HeaderText = "Nombre"
            DGInventario.Columns(4).HeaderText = "Cantidad"
            DGInventario.Columns(5).HeaderText = "Precio"
            DGInventario.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
End Class