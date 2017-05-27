Public Class frmEquiposHistorial
    Dim idEquipo As Integer
    Dim inicio As Boolean
    Dim tipoEquipo As Integer

    Public Sub New(ByVal pIdEquipo As Integer, ByVal pTipoEquipo As Integer)
        InitializeComponent()
        idEquipo = pIdEquipo
        tipoEquipo = pTipoEquipo
    End Sub

    Private Sub frmEquiposHistorial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' filtroTodos()
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        inicio = True
        llenarCuenta()
        inicio = False

    End Sub

    Private Sub filtroTodos()
        Try
            Dim SE As New dbServiciosEventos(MySqlcon)
            If tipoEquipo = 0 Then
                DGEventos.DataSource = SE.ConsultaTodosServicios(idEquipo, 0)
            Else
                DGEventos.DataSource = SE.ConsultaTodosServiciossuc(idEquipo, 0)
            End If

            DGEventos.Columns(0).Visible = False
            DGEventos.Columns(1).HeaderText = "Servicio"
            DGEventos.Columns(2).HeaderText = "Clasificación"
            DGEventos.Columns(3).HeaderText = "Subclasificación"
            DGEventos.Columns(4).HeaderText = "Comentario"
            DGEventos.Columns(5).HeaderText = "Precio"
            DGEventos.Columns(6).HeaderText = "Minutos"
            DGEventos.Columns(6).Visible = False
            DGEventos.Columns(2).Width = 130
            DGEventos.Columns(3).Width = 130
            DGEventos.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            'Componentes

            ''  totalConsumidos = 0
            Dim P As New dbServiciosEventos(MySqlcon)
            Dim TablaFull As New DataTable
            Dim Tabla As New DataTable
            TablaFull.Columns.Add("ID")
            TablaFull.Columns.Add("idEvento")
            TablaFull.Columns.Add("idInventario")
            TablaFull.Columns.Add("Codigo")
            TablaFull.Columns.Add("Nombre")
            TablaFull.Columns.Add("Precio")
            TablaFull.Columns.Add("Cantidad")
            TablaFull.Columns.Add("Total")
            If tipoEquipo = 0 Then
                Tabla = P.filtroArticulosConsumidos(idEquipo, 0)

            Else
                Tabla = P.filtroArticulosConsumidossuc(idEquipo, 0)

            End If
           
            For i As Integer = 0 To Tabla.Rows.Count() - 1
                Dim dr As DataRow

                dr = TablaFull.NewRow()
                dr("ID") = Tabla.Rows(i)(0).ToString
                dr("idEvento") = Tabla.Rows(i)(1).ToString
                dr("idInventario") = Tabla.Rows(i)(2).ToString
                P.InventarioUtilizado2(Integer.Parse(Tabla.Rows(i)(2).ToString))
                dr("Codigo") = P.codigoInventario
                dr("Nombre") = P.nombreInvenario
                dr("Precio") = "$" + Format(Tabla.Rows(i)(3), "0.00")
                dr("Cantidad") = Tabla.Rows(i)(4).ToString
                dr("Total") = "$" + Format(Tabla.Rows(i)(5), "0.00")
                TablaFull.Rows.Add(dr)
                ''  totalConsumidos = totalConsumidos + Double.Parse(Tabla.Rows(i)(5).ToString)
            Next

            ''  lblTotalArticulos.Text = Format(totalConsumidos, "0.00")

            DGInventario.DataSource = TablaFull
            DGInventario.Columns(0).Visible = False
            ''DGInventario.Columns(1).Visible = False
            DGInventario.Columns(2).Visible = False
            DGInventario.Columns(1).HeaderText = "Servicio"
            DGInventario.Columns(3).HeaderText = "Código"
            DGInventario.Columns(4).HeaderText = "Nombre"
            DGInventario.Columns(5).HeaderText = "Precio"
            DGInventario.Columns(6).HeaderText = "Cantidad"
            DGInventario.Columns(7).HeaderText = "Total"
            DGInventario.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGInventario.Refresh()
        

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub llenarCuenta()

        If tipoEquipo = 0 Then
            Dim P As New dbServiciosEventos(MySqlcon)
            Dim Dt2 As DataTable
            Dim Dt As New DataTable

            Dt.Columns.Add("idservicio")


            Dt2 = P.ConsultaServiciosId(idEquipo).ToTable
            'agregar nueva row Todos
            Dim dr2 As DataRow
            dr2 = Dt.NewRow()
            dr2("idservicio") = "Todos"
            Dt.Rows.Add(dr2)

            If Dt2.Rows.Count() > 0 Then
                For i As Integer = 0 To Dt2.Rows.Count() - 1
                    Dim dr As DataRow
                    dr = Dt.NewRow()
                    dr("idservicio") = Dt2.Rows(i)(0).ToString
                    Dt.Rows.Add(dr)
                Next
            End If

            If Dt.Rows.Count() > 1 Then
                With cmbEvento
                    .DataSource = Dt
                    .DisplayMember = "idservicio"
                    .ValueMember = "idservicio"
                End With
                filtros()
            Else
                MsgBox("No hay servicios registrados.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Else
            Dim P As New dbServiciosEventos(MySqlcon)
            Dim Dt2 As DataTable
            Dim Dt As New DataTable

            Dt.Columns.Add("idservicio")


            Dt2 = P.ConsultaServiciosIdSuc(idEquipo).ToTable
            'agregar nueva row Todos
            Dim dr2 As DataRow
            dr2 = Dt.NewRow()
            dr2("idservicio") = "Todos"
            Dt.Rows.Add(dr2)

            If Dt2.Rows.Count() > 0 Then
                For i As Integer = 0 To Dt2.Rows.Count() - 1
                    Dim dr As DataRow
                    dr = Dt.NewRow()
                    dr("idservicio") = Dt2.Rows(i)(0).ToString
                    Dt.Rows.Add(dr)
                Next
            End If

            If Dt.Rows.Count() > 1 Then
                With cmbEvento
                    .DataSource = Dt
                    .DisplayMember = "idservicio"
                    .ValueMember = "idservicio"
                End With
                filtros()
            Else
                MsgBox("No hay servicios registrados.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
        
    End Sub
    Private Sub filtros()
        If cmbEvento.Text = "Todos" Then
            filtroTodos()
        Else
            filtroEspecifico(Integer.Parse(cmbEvento.Text))
        End If

    End Sub

    Private Sub filtroEspecifico(ByVal pidServicio As Integer)
        Try
            Dim SE As New dbServiciosEventos(MySqlcon)
            If tipoEquipo = 0 Then
                DGEventos.DataSource = SE.ConsultaTodosServicios(idEquipo, pidServicio)
            Else
                DGEventos.DataSource = SE.ConsultaTodosServiciossuc(idEquipo, pidServicio)
            End If

            DGEventos.Columns(0).Visible = False
            DGEventos.Columns(1).HeaderText = "Servicio"
            DGEventos.Columns(2).HeaderText = "Clasificación"
            DGEventos.Columns(3).HeaderText = "Subclasificación"
            DGEventos.Columns(4).HeaderText = "Comentario"
            DGEventos.Columns(5).HeaderText = "Precio"
            DGEventos.Columns(6).HeaderText = "Minutos"
            DGEventos.Columns(6).Visible = False
            DGEventos.Columns(2).Width = 130
            DGEventos.Columns(3).Width = 130
            DGEventos.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            'Componentes

            ''  totalConsumidos = 0
            Dim P As New dbServiciosEventos(MySqlcon)
            Dim TablaFull As New DataTable
            Dim Tabla As New DataTable
            TablaFull.Columns.Add("ID")
            TablaFull.Columns.Add("idEvento")
            TablaFull.Columns.Add("idInventario")
            TablaFull.Columns.Add("Codigo")
            TablaFull.Columns.Add("Nombre")
            TablaFull.Columns.Add("Precio")
            TablaFull.Columns.Add("Cantidad")
            TablaFull.Columns.Add("Total")
            If tipoEquipo = 0 Then
                Tabla = P.filtroArticulosConsumidos(idEquipo, pidServicio)

            Else
                Tabla = P.filtroArticulosConsumidossuc(idEquipo, pidServicio)

            End If
           
            For i As Integer = 0 To Tabla.Rows.Count() - 1
                Dim dr As DataRow

                dr = TablaFull.NewRow()
                dr("ID") = Tabla.Rows(i)(0).ToString
                dr("idEvento") = Tabla.Rows(i)(1).ToString
                dr("idInventario") = Tabla.Rows(i)(2).ToString

                P.InventarioUtilizado2(Integer.Parse(Tabla.Rows(i)(2).ToString))
                dr("Codigo") = P.codigoInventario
                dr("Nombre") = P.nombreInvenario
                dr("Precio") = "$" + Format(Tabla.Rows(i)(3), "0.00")
                dr("Cantidad") = Tabla.Rows(i)(4).ToString
                dr("Total") = "$" + Format(Tabla.Rows(i)(5), "0.00")
                TablaFull.Rows.Add(dr)
                ''  totalConsumidos = totalConsumidos + Double.Parse(Tabla.Rows(i)(5).ToString)
            Next

            ''  lblTotalArticulos.Text = Format(totalConsumidos, "0.00")

            DGInventario.DataSource = TablaFull
            DGInventario.Columns(0).Visible = False
            ''DGInventario.Columns(1).Visible = False
            DGInventario.Columns(2).Visible = False
            DGInventario.Columns(1).HeaderText = "Servicio"
            DGInventario.Columns(3).HeaderText = "Código"
            DGInventario.Columns(4).HeaderText = "Nombre"
            DGInventario.Columns(5).HeaderText = "Precio"
            DGInventario.Columns(6).HeaderText = "Cantidad"
            DGInventario.Columns(7).HeaderText = "Total"
            DGInventario.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGInventario.Refresh()


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub cmbEvento_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEvento.SelectedIndexChanged
        If inicio = False Then
            filtros()
        End If
    End Sub
End Class