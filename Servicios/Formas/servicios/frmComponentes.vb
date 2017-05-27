Public Class frmComponentes
    Public idEquipo As Integer
    Dim TipoEquipo As Integer
    Public Sub New(ByVal pIdEquipo As Integer, ByVal pIdTipoEquipo As Integer)
        InitializeComponent()
        idEquipo = pIdEquipo
        TipoEquipo = pIdEquipo
    End Sub
    Private Sub frmComponentes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Dim P As New dbServiciosEquipos(MySqlcon)
        P.Llenadatos(idEquipo)
        lblNombre.Text = P.nombre
        ' lblColor.Text = P.color
        ' lblKilometraje.Text = P.kilometraje
        lblmarca.Text = P.marca
        lblMatricula.Text = P.matricula
        lblModelo.Text = P.modelo
        lblMotor.Text = P.motor
        lblSerie.Text = P.serie
        llenaDatos()
    End Sub
    Private Sub llenaDatos()
        Try
            Dim PrimerCeldaRow As Integer = -1
            Dim P As New dbDetallesEquipo(MySqlcon)
            If dtgComponentes.RowCount > 0 Then PrimerCeldaRow = dtgComponentes.FirstDisplayedCell.RowIndex
            Dim Dt As New DataTable
            Dim TablaFull As New DataTable
            TablaFull.Columns.Add("idDetalle")
            TablaFull.Columns.Add("idEquipo")
            TablaFull.Columns.Add("Cantidad")
            TablaFull.Columns.Add("Código")
            TablaFull.Columns.Add("Descripcion")
            TablaFull.Columns.Add("Tiempo vida")
            If TipoEquipo = 0 Then
                Dt = P.detallesEquipo(idEquipo)
            Else
                Dt = P.detallesEquiposuc(idEquipo)
            End If


            For i As Integer = 0 To Dt.Rows.Count() - 1
                Dim dr1 As DataRow
                dr1 = TablaFull.NewRow()
                dr1("idDetalle") = Dt.Rows(i)(0).ToString
                dr1("idEquipo") = Dt.Rows(i)(1).ToString
                dr1("Cantidad") = Dt.Rows(i)(2).ToString
                P.buscarArticulo(Dt.Rows(i)(3).ToString)
                dr1("Código") = P.codigo
                dr1("Descripcion") = P.descripcion
                dr1("Tiempo vida") = Dt.Rows(i)(4).ToString

                TablaFull.Rows.Add(dr1)
            Next

            dtgComponentes.DataSource = TablaFull
            dtgComponentes.Columns(0).Visible = False
            dtgComponentes.Columns(1).HeaderText = "idEquipo"
            dtgComponentes.Columns(1).Visible = False
            dtgComponentes.Columns(2).HeaderText = "Cantidad"
            dtgComponentes.Columns(3).HeaderText = "Codigo"
            dtgComponentes.Columns(4).HeaderText = "Descripcion"
            dtgComponentes.Columns(5).HeaderText = "Tiempo vida"
            dtgComponentes.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            If dtgComponentes.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then dtgComponentes.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Me.Close()
    End Sub
End Class