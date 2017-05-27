Public Class frmDeposito
    Dim idCuentas As New elemento
    Dim idDeposito As Integer
    Dim id As Integer
    Dim idCuenta As Integer
    Dim idBanco As Integer
    Dim Tabla As New DataTable
    Dim CantidadPago As Double
    Dim IDaux As Integer
    Dim Dt As DataTable
    Dim Dt2 As DataTable
    Dim idModificar As Integer
    Dim Cuenta, Descripcion, Cargo, Abono As String
    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim CuantaY As Integer
    Dim TipoImpresora As Byte
    Dim inicial As Integer = 1
    Dim p As New dbContabilidadPolizas(MySqlcon)
    Dim Intedrado As Byte
    Dim FacturasAfectadas As String
    Dim FormadePago As String
    Dim ProveedorInt As String
    Dim CantidadInt As String
    Dim idCliente As Integer
    Dim WhereStr As String
    Dim FechaInt As String
    Dim IdsCuentas As New elemento
    Dim IdsBancos As New elemento
    Dim ConsultaOn As Boolean
    Dim TipoLigue As Byte
    Dim IdPagoProv As Integer
    Dim Ids As Integer()
    Dim v As dbVentas
    Dim dep As dbDeposito
    Public Sub New(ByVal pIntegrado As Byte, ByVal pFacturasAfectadas As String, ByVal pFormadepago As String, ByVal pNproveedor As String, ByVal pCantidadInt As String, ByVal pIdCliente As Integer, ByVal pWherestr As String, ByVal pFechaInt As String, pTipoLigue As Byte)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Intedrado = pIntegrado
        FacturasAfectadas = pFacturasAfectadas
        FormadePago = pFormadepago
        ProveedorInt = pNproveedor
        CantidadInt = pCantidadInt
        idCliente = pIdCliente
        WhereStr = pWherestr
        FechaInt = pFechaInt
        TipoLigue = pTipoLigue
        '0 no ligue
        '1 ligue con pagos
        '2 ligue con facturas
        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub frmDeposito_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' llenarCuenta()
        'Dim year As String
        'Dim month As String
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            btnImprimirDeposito.Image = Image.FromFile(Application.StartupPath + "\iconos\printer.png")
        Catch ex As Exception

        End Try
        v = New dbVentas(MySqlcon)
        dep = New dbDeposito(MySqlcon)
        ConsultaOn = False
        LlenaCombos("tblcuentas", cmbCuenta, "concat(numero,' ',(select nombre from tblbancoscatalogo where idbanco=banco))", "cuentaN", "idcuenta", IdsCuentas)
        'LlenaCombos("tblbancoscatalogo", cmbBancos, "nombre", "nombren", "idbanco", IdsBancos)
        'year = Date.Now.Year.ToString
        'month = Format(Date.Now.Month, "00")
        dtpDesde.Value = "01/" + Date.Now.ToString("MM") + "/" + Date.Now.ToString("yyyy")
        chkFecha.Checked = True
        Tablacrear()
        ConsultaOn = True
        Nuevo(False)
        If Intedrado = 1 Then
            txtReferencia.Text = FormadePago
            txtComentario.Text = FacturasAfectadas
            txtCantidad.Text = Format(CDbl(CantidadInt), "0.00")
            dtpFecha.Value = FechaInt
        End If
        If TipoLigue <> 0 Then
            Button1.Visible = False
            Button3.Visible = False
        End If
        'filtros()
    End Sub
    'Private Sub llenarCuenta()

    '    Dim P As New dbPagosProveedores(MySqlcon)
    '    Dt = P.buscarCuenta()
    '    If Dt.Rows.Count > 0 Then

    '        With cmbCuenta
    '            .DataSource = Dt
    '            .DisplayMember = "Nombre"
    '            .ValueMember = "Nombre"
    '        End With

    '    Else
    '        MsgBox("No hay cuentas de Banco registradas, favor de registrar una.", MsgBoxStyle.Critical, GlobalNombreApp)
    '    End If

    'End Sub
    'Private Sub llenarBancos()

    '    Dim P As New dbPagosProveedores(MySqlcon)
    '     Dt2 = P.buscarCuenta()
    '    Dt2 = P.buscarBancos()
    '    If Dt2.Rows.Count > 0 Then

    '        With cmbBancos
    '            .DataSource = Dt2
    '            .DisplayMember = "Nombre"
    '            .ValueMember = "Nombre"
    '        End With
    '    Else
    '    End If

    'End Sub
    
    Private Sub Nuevo(ByVal ResetParametros As Boolean)

        Label7.Visible = False
        btnGuardarPoliza.Text = "Guardar"
        txtReferencia.BackColor = Color.White
        txtComentario.Text = ""
        txtCantidad.Text = "0.00"
        txtReferencia.Text = ""
        btnEliminar.Enabled = False
        FiltroTodos()
        btnImprimirDeposito.Enabled = False
        ReDim Ids(-1)
        ConsultaOn = False
        NuevaPoliza()
        If chkFecha.Checked = False Then
            dtpFecha.Value = Date.Today
        End If
        txtReferencia.Focus()
        IdPagoProv = 0
        lblDescripcion.Text = ""
        If ResetParametros Then
            FacturasAfectadas = ""
            WhereStr = ""
        End If
        ConsultaOn = True
        Label1.Text = "Total venta contado: " + v.DaTotalContadoConta(DateTimePicker1.Value.ToString("yyyy/MM/dd"), 0).ToString("###,###,##0.00") + " Total depósitos no ligados: " + dep.DatotalDepositosConta(DateTimePicker1.Value.ToString("yyyy/MM/dd"), "").ToString("###,###,##0.00")
    End Sub
    'Public Function fechaFormato(ByVal pfecha As DateTimePicker) As String
    '    Dim Dia As String
    '    Dim Mes As String
    '    Dim year As String
    '    Dim fechita As String


    '    Dia = Format(pfecha.Value.Date.Day, "00")
    '    Mes = Format(pfecha.Value.Date.Month, "00")
    '    year = pfecha.Value.Date.Year

    '    fechita = year + "-" + Mes + "-" + Dia
    '    Return fechita

    'End Function

    Private Sub txtCantidad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.Click
        txtCantidad.SelectAll()
    End Sub

    Private Sub txtCantidad_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.DoubleClick
        txtCantidad.DeselectAll()
    End Sub

    Private Sub txtCantidad_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.Enter
        txtCantidad.SelectAll()
    End Sub

    Private Sub txtCantidad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyDown
        If txtCantidad.Text = "0.00" Then
            txtCantidad.Text = ""
        End If
    End Sub

    Private Sub txtCantidad_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCantidad_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.Leave
        Dim x As Double
        If txtCantidad.Text = "." Then
            txtCantidad.Text = "0.00"
        End If
        If txtCantidad.Text = "" Then
            txtCantidad.Text = "0.00"
        Else
            x = Double.Parse(txtCantidad.Text)
            txtCantidad.Text = Format(x, "0.00")
        End If
        Cuentas()
    End Sub

    Public Sub FiltroTodos()
        Try
            If ConsultaOn Then
                Dim PrimerCeldaRow As Integer = -1
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim P As New dbDeposito(MySqlcon)
                'Dim tabla As DataTable
                'Dim TablaFull As New DataTable
                'TablaFull.Columns.Add("id")
                'TablaFull.Columns.Add("Fecha")
                'TablaFull.Columns.Add("Referencia")
                'TablaFull.Columns.Add("Banco")
                'TablaFull.Columns.Add("Cantidad")
                'TablaFull.Columns.Add("N. Cuenta")
                'TablaFull.Columns.Add("Banco2")
                'tabla = P.filtroTodos().ToTable()

                'For i As Integer = 0 To tabla.Rows.Count() - 1
                '    Dim dr As DataRow

                '    dr = TablaFull.NewRow()
                '    dr("id") = tabla.Rows(i)(0).ToString
                '    dr("Fecha") = tabla.Rows(i)(1).ToString
                '    dr("Referencia") = tabla.Rows(i)(2).ToString
                '    dr("Banco") = P.buscarBanco(tabla.Rows(i)(3).ToString)
                '    dr("Cantidad") = tabla.Rows(i)(4).ToString
                '    dr("N. Cuenta") = P.buscarNumeroCuenta(tabla.Rows(i)(6).ToString)
                '    dr("Banco2") = P.buscarBancoCuenta(tabla.Rows(i)(6).ToString)
                '    TablaFull.Rows.Add(dr)
                'Next
                DataGridView1.DataSource = P.filtroTodos(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), txtBuscRef.Text, IdsCuentas.Valor(cmbCuenta.SelectedIndex), TextBox1.Text)
                DataGridView1.Columns(0).HeaderText = "Folio"
                DataGridView1.Columns(1).HeaderText = "Fecha"
                DataGridView1.Columns(2).HeaderText = "Referencia"
                'DataGridView1.Columns(3).HeaderText = "Banco"
                DataGridView1.Columns(3).HeaderText = "Cantidad"
                DataGridView1.Columns(4).HeaderText = "N. Cuenta"
                DataGridView1.Columns(5).HeaderText = "Banco"
                'DataGridView1.Columns(0).Visible = False
                'DataGridView1.Columns(5).Width = 150
                DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            If IdPagoProv <> 0 Then
                MsgBox("No puede eliminar un deposito que viene de un traspaso. Lo puede hacer desde el Pago que generó el traspaso.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.DepositosCancelar, PermisosN.Secciones.Bancos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbDeposito(MySqlcon)
                    If P.LigadoABancos(idDeposito) Then
                        MsgBox("Para poder modificar este movimiento tiene que eliminar todos los pagos relacionados a este.", MsgBoxStyle.Information, GlobalNombreApp)
                        Exit Sub
                    End If
                    P.Eliminar(idDeposito)
                    P.EliminarPoliza(idDeposito)
                    P.DesligarDeposito(idDeposito)
                    'Dim nFilas As Integer = DataGridView2.Rows.Count
                    ' If nFilas > 0 Then
                    'eliminar poliza
                    'P.EliminarTodaPoliza(txtPoliza.Text)
                    'End If
                    PopUp("Eliminado", 90)
                    Nuevo(True)
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 3 Then
            e.Value = Format(e.Value, "#,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub DataGridView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        If DataGridView1.CurrentCell.RowIndex >= 0 Then
            idDeposito = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            LlenarDatos()
            btnImprimirDeposito.Enabled = True
            btnAgregar.Enabled = True
            btnAgregar.Text = "Agregar"
            CantidadPago = Double.Parse(txtCantidad.Text)
            'tienePoliza()
        End If
    End Sub
    Private Sub LlenarDatos()

        Try
            id = idDeposito
            Dim P As New dbDeposito(MySqlcon)
            P.Buscar(id)
            Label7.Text = "Folio: " + id.ToString("00000")
            Label7.Visible = True
            txtReferencia.Text = P.Referencia
            txtComentario.Text = P.comentario
            txtCantidad.Text = P.Cantidad.ToString("0.00")
            cmbCuenta.SelectedIndex = IdsCuentas.Busca(P.Banco2)
            idCuenta = P.Banco2
            IdPagoProv = P.IdPagoProv
            'txtCodigo.Text = P.buscarNumeroCuenta(P.Banco2)
            'Dim date1 As Date = Date.Parse(P.Fecha.ToString, System.Globalization.CultureInfo.InvariantCulture)
            dtpFecha.Value = CDate(P.Fecha)
            DateTimePicker1.Value = CDate(P.FechaConta)
            idDeposito = id
            ReDim Ids(-1)
            btnGuardarPoliza.Text = "Modificar"
            btnEliminar.Enabled = True
            txtReferencia.Focus()
            'DataGridView1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    'Private Sub LlenarDatos2()

    '    Try
    '        id = idDeposito
    '        Dim P As New dbDeposito(MySqlcon)
    '        P.Buscar(id)
    '        txtReferencia.Text = P.Referencia
    '        cmbBancos.Text = P.buscarBanco(P.Banco)
    '        txtCantidad.Text = P.Cantidad
    '        txtCodigo.Text = P.cuenta
    '        cmbCuenta.Text = P.buscarBanco(P.Banco2)
    '        Dim date1 As Date = Date.Parse(P.Fecha.ToString, System.Globalization.CultureInfo.InvariantCulture)
    '        dtpFecha.Value = date1
    '        idDeposito = id
    '        'btnGuardar.Text = "Modificar"
    '        btnEliminar.Enabled = True
    '        txtReferencia.Focus()
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try

    'End Sub
    'Public Sub FiltroFecha()

    '    Try
    '        Dim PrimerCeldaRow As Integer = -1
    '        If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
    '        Dim P As New dbDeposito(MySqlcon)
    '        Dim tabla As DataTable
    '        Dim TablaFull As New DataTable
    '        TablaFull.Columns.Add("id")
    '        TablaFull.Columns.Add("Fecha")
    '        TablaFull.Columns.Add("Referencia")
    '        TablaFull.Columns.Add("Banco")
    '        TablaFull.Columns.Add("Cantidad")
    '        TablaFull.Columns.Add("N. Cuenta")
    '        TablaFull.Columns.Add("Banco2")
    '        tabla = P.filtroFecha(fechaFormato(dtpDesde), fechaFormato(dtpHasta)).ToTable()
    '        For i As Integer = 0 To tabla.Rows.Count() - 1
    '            Dim dr As DataRow

    '            dr = TablaFull.NewRow()
    '            dr("id") = tabla.Rows(i)(0).ToString
    '            dr("Fecha") = tabla.Rows(i)(1).ToString
    '            dr("Referencia") = tabla.Rows(i)(2).ToString
    '            dr("Banco") = P.buscarBanco(tabla.Rows(i)(3).ToString)
    '            dr("Cantidad") = tabla.Rows(i)(4).ToString
    '            dr("N. Cuenta") = P.buscarNumeroCuenta(tabla.Rows(i)(6).ToString)
    '            dr("Banco2") = P.buscarBancoCuenta(tabla.Rows(i)(6).ToString)
    '            TablaFull.Rows.Add(dr)
    '        Next
    '        DataGridView1.DataSource = TablaFull
    '        DataGridView1.Columns(0).HeaderText = "id"
    '        DataGridView1.Columns(1).HeaderText = "Fecha"
    '        DataGridView1.Columns(2).HeaderText = "Referencia"
    '        DataGridView1.Columns(3).HeaderText = "Banco"
    '        DataGridView1.Columns(4).HeaderText = "Cantidad"
    '        DataGridView1.Columns(5).HeaderText = "N. Cuenta"
    '        DataGridView1.Columns(6).HeaderText = "Banco"
    '        DataGridView1.Columns(0).Visible = False
    '        DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

    '        If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try

    'End Sub

    'Public Sub FiltroRef()
    '    Try
    '        Dim PrimerCeldaRow As Integer = -1
    '        If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex

    '        Dim P As New dbDeposito(MySqlcon)
    '        Dim tabla As DataTable
    '        Dim TablaFull As New DataTable
    '        TablaFull.Columns.Add("id")
    '        TablaFull.Columns.Add("Fecha")
    '        TablaFull.Columns.Add("Referencia")
    '        TablaFull.Columns.Add("Banco")
    '        TablaFull.Columns.Add("Cantidad")
    '        TablaFull.Columns.Add("N. Cuenta")
    '        TablaFull.Columns.Add("Banco2")
    '        tabla = P.filtroProv(txtBuscRef.Text).ToTable()

    '        For i As Integer = 0 To tabla.Rows.Count() - 1
    '            Dim dr As DataRow

    '            dr = TablaFull.NewRow()
    '            dr("id") = tabla.Rows(i)(0).ToString
    '            dr("Fecha") = tabla.Rows(i)(1).ToString
    '            dr("Referencia") = tabla.Rows(i)(2).ToString
    '            dr("Banco") = P.buscarBanco(tabla.Rows(i)(3).ToString)
    '            dr("Cantidad") = tabla.Rows(i)(4).ToString
    '            dr("N. Cuenta") = P.buscarNumeroCuenta(tabla.Rows(i)(6).ToString)
    '            dr("Banco2") = P.buscarBancoCuenta(tabla.Rows(i)(6).ToString)

    '            TablaFull.Rows.Add(dr)
    '        Next
    '        DataGridView1.DataSource = TablaFull
    '        DataGridView1.Columns(0).HeaderText = "id"
    '        DataGridView1.Columns(1).HeaderText = "Fecha"
    '        DataGridView1.Columns(2).HeaderText = "Referencia"
    '        DataGridView1.Columns(3).HeaderText = "Banco"
    '        DataGridView1.Columns(4).HeaderText = "Cantidad"
    '        DataGridView1.Columns(5).HeaderText = "N. Cuenta"
    '        DataGridView1.Columns(6).HeaderText = "Banco"
    '        DataGridView1.Columns(0).Visible = False
    '        DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

    '        If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try
    'End Sub

    'Public Sub FiltroAmbos()
    '    Try
    '        Dim PrimerCeldaRow As Integer = -1
    '        If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
    '        Dim P As New dbDeposito(MySqlcon)
    '        Dim tabla As DataTable
    '        Dim TablaFull As New DataTable
    '        TablaFull.Columns.Add("id")
    '        TablaFull.Columns.Add("Fecha")
    '        TablaFull.Columns.Add("Referencia")
    '        TablaFull.Columns.Add("Banco")
    '        TablaFull.Columns.Add("Cantidad")
    '        TablaFull.Columns.Add("N. Cuenta")
    '        TablaFull.Columns.Add("Banco2")

    '        tabla = P.filtroAmbos(txtBuscRef.Text, fechaFormato(dtpDesde), fechaFormato(dtpHasta)).ToTable()

    '        For i As Integer = 0 To tabla.Rows.Count() - 1
    '            Dim dr As DataRow

    '            dr = TablaFull.NewRow()
    '            dr("id") = tabla.Rows(i)(0).ToString
    '            dr("Fecha") = tabla.Rows(i)(1).ToString
    '            dr("Referencia") = tabla.Rows(i)(2).ToString
    '            dr("Banco") = P.buscarBanco(tabla.Rows(i)(3).ToString)
    '            dr("Cantidad") = tabla.Rows(i)(4).ToString
    '            dr("N. Cuenta") = P.buscarNumeroCuenta(tabla.Rows(i)(6).ToString)
    '            dr("Banco2") = P.buscarBancoCuenta(tabla.Rows(i)(6).ToString)
    '            TablaFull.Rows.Add(dr)
    '        Next
    '        DataGridView1.DataSource = TablaFull
    '        DataGridView1.Columns(0).HeaderText = "id"
    '        DataGridView1.Columns(1).HeaderText = "Fecha"
    '        DataGridView1.Columns(2).HeaderText = "Referencia"
    '        DataGridView1.Columns(3).HeaderText = "Banco"
    '        DataGridView1.Columns(4).HeaderText = "Cantidad"
    '        DataGridView1.Columns(5).HeaderText = "N. Cuenta"
    '        DataGridView1.Columns(6).HeaderText = "Banco"
    '        DataGridView1.Columns(0).Visible = False
    '        DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

    '        If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try
    'End Sub

    Private Sub ckbFecha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbFecha.CheckedChanged

    End Sub

    'Private Sub filtros()
    '    If ckbFecha.Checked = False And ckbProvedor.Checked = False Then
    '        FiltroTodos()
    '    End If
    '    If ckbFecha.Checked = True And ckbProvedor.Checked = False Then
    '        FiltroFecha()
    '    End If
    '    If ckbFecha.Checked = False And ckbProvedor.Checked = True Then
    '        FiltroRef()
    '    End If
    '    If ckbFecha.Checked = True And ckbProvedor.Checked = True Then
    '        FiltroAmbos()
    '    End If

    'End Sub

    Private Sub ckbProvedor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbProvedor.CheckedChanged

    End Sub

    Private Sub txtBuscProve_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscRef.TextChanged
        FiltroTodos()
    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Nuevo(True)
    End Sub

    Private Sub dtpDesde_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDesde.ValueChanged
        Dim tmpc As Boolean = ConsultaOn
        ConsultaOn = False
        dtpHasta.Value = (Date.Parse("01/" + dtpDesde.Value.Month.ToString + "/" + dtpDesde.Value.Year.ToString).AddMonths(1)).AddDays(-1)
        ConsultaOn = tmpc
        FiltroTodos()
    End Sub

    Private Sub dtpHasta_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpHasta.ValueChanged
        'ckbFecha.Checked = True
        FiltroTodos()
    End Sub

    Private Sub btnLimpiarBusqueda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiarBusqueda.Click
        LimpiarBusq()
    End Sub
    Private Sub LimpiarBusq()
        Dim year As String
        Dim month As String

        year = Date.Now.Year.ToString
        month = Format(Date.Now.Month, "00")
        dtpDesde.Value = "01/" + month + "/" + year.ToString
        dtpHasta.Value = Today
        txtBuscRef.Text = ""
        ckbFecha.Checked = True
        ckbProvedor.Checked = False
        TextBox1.Text = ""
        FiltroTodos()
    End Sub

    Private Sub btnBuscarCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCC.Click
        'Dim B As New frmBuscadorCC()
        'B.ShowDialog()
        'If B.DialogResult = Windows.Forms.DialogResult.OK Then
        '    txtCuenta.Text = B.Cuenta.ToString()
        '    txtDesc.Text = B.descripcion.ToString()
        '    'txtCargo.Focus()

        'End If
    End Sub

    Private Sub txtCuenta_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCuenta.Leave
        'Dim aux As String
        'Dim tamano As Integer


        'Dim P As New dbCContables(MySqlcon)
        'txtCuenta.BackColor = Color.White
        'If txtCuenta.Text = " " Then
        '    txtCuenta.Text = ""
        'End If
        'If txtCuenta.Text = "" Then
        'Else
        '    tamano = txtCuenta.Text.Length
        '    If txtCuenta.Text.Chars(tamano - 1) = " " Then
        '        aux = ""
        '        For i As Integer = 0 To tamano - 2
        '            aux = aux + txtCuenta.Text.Chars(i)
        '        Next
        '        txtCuenta.Text = aux
        '    End If
        '    'Comprobar existencia cuenta
        '    If P.existeCuenta(txtCuenta.Text) = 0 Then
        '        'No existe la cuenta
        '        MsgBox("La cuenta no existe, escriba correctamente la cuenta.", MsgBoxStyle.Critical, GlobalNombreApp)
        '        txtCuenta.BackColor = Color.FromArgb(250, 150, 150)
        '        txtCuenta.Text = ""
        '        txtCuenta.Focus()
        '    Else
        '        'Comprobar si es de último Nivel
        '        P.PreUltimo(txtCuenta.Text)
        '        If P.UtlimoNivel(P.ID.ToString(), P.Nivel.ToString()) = 0 Then
        '            txtDesc.Focus()
        '        Else
        '            MsgBox("Debe seleccionar cuentas de último nivel.", MsgBoxStyle.Critical, GlobalNombreApp)
        '            txtCuenta.BackColor = Color.FromArgb(250, 150, 150)
        '            txtCuenta.Focus()
        '            txtCuenta.Text = ""
        '        End If
        '    End If
        'End If
        txtCuenta.BackColor = Color.White
        verificaCuenta()
        If txtCuenta.Text <> "" Then
            If p.existeCuenta(idCuenta) = False Then
                MsgBox("La cuenta proporcionada no exite.", MsgBoxStyle.OkOnly, "Pull System Soft")
                txtCuenta.BackColor = Color.Tomato
                txtCuenta.Focus()
            End If
        End If
    End Sub

    Private Sub txtCuenta_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCuenta.KeyPress
        'Dim textBox As TextBox = DirectCast(sender, TextBox)
        'If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
        '    e.Handled = True
        'Else
        '    If e.KeyChar = Convert.ToChar(8) Then
        '        'que no sea backspace
        '    Else
        '        Dim aux As String

        '        If txtCuenta.Text = "" Then

        '        Else
        '            If txtCuenta.Text.Length() = 5 Then
        '                aux = txtCuenta.Text + " "
        '                txtCuenta.Text = aux
        '                txtCuenta.Select(txtCuenta.Text.Length, 0)
        '            End If
        '            If txtCuenta.Text.Length() = 11 Then
        '                aux = txtCuenta.Text + " "
        '                txtCuenta.Text = aux
        '                txtCuenta.Select(txtCuenta.Text.Length, 0)
        '            End If
        '            If txtCuenta.Text.Length() = 17 Then
        '                aux = txtCuenta.Text + " "
        '                txtCuenta.Text = aux
        '                txtCuenta.Select(txtCuenta.Text.Length, 0)
        '            End If
        '            If txtCuenta.Text.Length() = 23 Then
        '                aux = txtCuenta.Text + " "
        '                txtCuenta.Text = aux
        '                txtCuenta.Select(txtCuenta.Text.Length, 0)
        '            End If
        '        End If
        '    End If
        'End If
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtCuenta.Text <> "" Then
            If txtCuenta.Text = "+" Then
                'Dim B As New frmBuscadorCC()
                'B.ShowDialog()
                'If B.DialogResult = Windows.Forms.DialogResult.OK Then
                '    txtCuenta.Text = B.Cuenta.ToString()
                '    ' txtDesc.Text = B.descripcion.ToString()

                '    lblDescripcion.Text = B.descripcion
                '    idCuenta = B.ID
                '    txtDesc.Focus()
                'End If
            Else
                verificaCuenta()
            End If

        Else
            Dim textBox As TextBox = DirectCast(sender, TextBox)
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = " ") Or (e.KeyChar = "+")) Then
                e.Handled = True
            End If




        End If
    End Sub
    Private Sub verificaCuenta()
        Dim N1 As String = ""
        Dim N2 As String = ""
        Dim N3 As String = ""
        Dim N4 As String = ""
        Dim N5 As String = ""
        Dim niv As Integer = 1
        Dim aux As String = ""
        For i As Integer = 0 To txtCuenta.Text.Length() - 1
            If txtCuenta.Text.Chars(i) <> " " Then
                aux += txtCuenta.Text.Chars(i)
            Else
                If txtCuenta.Text.Chars(i) = " " And i <> txtCuenta.Text.Length() - 1 Then
                    If niv = 1 Then
                        N1 = aux
                    End If
                    If niv = 2 Then
                        N2 = aux
                    End If
                    If niv = 3 Then
                        N3 = aux
                    End If
                    If niv = 4 Then
                        N4 = aux
                    End If
                    aux = ""
                    niv += 1
                End If

            End If

        Next
        If niv = 1 Then
            N1 = aux
        End If
        If niv = 2 Then
            N2 = aux
        End If
        If niv = 3 Then
            N3 = aux
        End If
        If niv = 4 Then
            N4 = aux
        End If
        If niv = 5 Then
            N5 = aux
        End If
        idCuenta = p.BuscarIdCuenta(niv, N1, N2, N4, N4, N5)

        If p.ultimoNivel(N1, N2, N3, N4, N5, niv) = 0 Then
            lblDescripcion.Text = p.descripcionCuenta
            txtDesc.Focus()
        Else
            MsgBox("Debe seleccionar cuentas de último nivel.", MsgBoxStyle.Critical, GlobalNombreApp)
            txtCuenta.Focus()
        End If



    End Sub
    '****************Tabla DataGrid2
    Private Sub Tablacrear()

        Tabla.Columns.Add("ID")
        Tabla.Columns.Add("Cuenta")
        Tabla.Columns.Add("Descripción")
        Tabla.Columns.Add("Cargo")
        Tabla.Columns.Add("Abono")
        Tabla.Columns.Add("Poliza")

        DataGridView2.DataSource = Tabla
        DataGridView2.Columns(0).HeaderText = "ID"
        DataGridView2.Columns(1).HeaderText = "Cuenta"
        DataGridView2.Columns(2).HeaderText = "Descripción"
        DataGridView2.Columns(3).HeaderText = "Cargo"
        DataGridView2.Columns(4).HeaderText = "Abono"
        DataGridView2.Columns(5).HeaderText = "Poliza"
        DataGridView2.Columns(0).Visible = False
        DataGridView2.Columns(5).Visible = False
        DataGridView2.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        AgregarPoliza()
    End Sub
    Private Sub AgregarPoliza()
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""
        txtCuenta.BackColor = Color.White
        txtDesc.BackColor = Color.White
        'txtAbono.BackColor = Color.White
        'txtCargo.BackColor = Color.White
        txtPoliza.BackColor = Color.White
        'Verificar si ya existe el numero de Poliza
        If txtCuenta.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + "Debe indicar un número de cuenta."
            txtCuenta.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtDesc.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + "Debe indicar una descripción."
            txtDesc.BackColor = Color.FromArgb(250, 150, 150)
        End If
        'If txtAbono.obText = "" Then
        '    NoErrores = False
        '    MensajeError += vbCrLf + "Debe indicar un valor al abono."
        '    txtAbono.BackColor = Color.FromArgb(250, 150, 150)
        'End If
        'If txtCargo.obText = "" Then
        '    NoErrores = False
        '    MensajeError += vbCrLf + "Debe indicar un valor al cargo."
        '    txtCargo.BackColor = Color.FromArgb(250, 150, 150)
        'End If
        If txtPoliza.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar un número de póliza."
            txtPoliza.BackColor = Color.FromArgb(250, 150, 150)
        End If

        If NoErrores = True Then
            If btnAgregar.Text = "Agregar" Then
                IDaux = Tabla.Rows.Count
                agregarFila()
                txtCuenta.Text = ""
                txtDesc.Text = ""
                'txtCargo.insText("0.00")
                'txtAbono.insText("0.00")
                btnGuardarPoliza.Enabled = True
                txtCuenta.Focus()
                PopUp("Agregado", 90)
            Else 'Modificar

                IDaux = Tabla.Rows.Count
                eliminarFila()
                agregarFila()
                ' Next
                Cuentas()
                txtCuenta.Text = ""
                txtDesc.Text = ""
                'txtCargo.insText("0.00")
                'txtAbono.insText("0.00")
                btnAgregar.Text = "Agregar"
                btnEliminar2.Enabled = False
                PopUp("Renglón modificado", 90)
                txtCuenta.Focus()
                'End If
            End If
        End If


        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

        End If
    End Sub

    Private Sub agregarFila()
        Dim nFilas As Integer

        nFilas = Tabla.Rows.Count
        Dim dr5 As DataRow
        dr5 = Tabla.NewRow()
        dr5("ID") = IDaux + 1.ToString()
        dr5("Cuenta") = txtCuenta.Text
        dr5("Descripción") = txtDesc.Text
        'dr5("Cargo") = txtCargo.obText
        'dr5("Abono") = txtAbono.obText
        dr5("Poliza") = txtPoliza.Text
        Tabla.Rows.Add(dr5)

        'Agregar tabla
        DataGridView2.DataSource = Tabla
        DataGridView2.Columns(0).HeaderText = "ID"
        DataGridView2.Columns(1).HeaderText = "Cuenta"
        DataGridView2.Columns(2).HeaderText = "Descripción"
        DataGridView2.Columns(3).HeaderText = "Cargo"
        DataGridView2.Columns(4).HeaderText = "Abono"
        DataGridView2.Columns(5).HeaderText = "Poliza"
        DataGridView2.Columns(0).Visible = False
        DataGridView2.Columns(5).Visible = False
        DataGridView2.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        Cuentas()

    End Sub

    Private Sub Cuentas()
        Dim nFilas As Integer
        Dim Cargos As Double = 0
        Dim Abonos As Double = 0
        Dim dif As Double = 0
        Dim tot As Double = 0
        nFilas = DataGridView2.Rows.Count
        For i As Integer = 0 To nFilas - 1
            Cargos = Cargos + Double.Parse(DataGridView2.Item(3, i).Value())
            Abonos = Abonos + Double.Parse(DataGridView2.Item(4, i).Value())
        Next
        tot = Cargos - Abonos
        lblDiferencia.Text = Format(tot, "0.00")
        If Double.Parse(lblDiferencia.Text) <> 0.0 Then
            lblDiferencia.ForeColor = Color.Red
        Else
            lblDiferencia.ForeColor = Color.Black

            lblDiferencia.Text = "0.00"
        End If
        lblCargos.Text = Format(Cargos, "0.00")
        lblAbonos.Text = Format(Abonos, "0.00")
    End Sub
    Private Sub eliminarFila()
        Dim nfilas As Integer
        Dim nFilas2 As Integer
        nFilas2 = Tabla.Rows.Count
        For i As Integer = nFilas2 - 1 To 0 Step -1
            id = Tabla.Rows(i)(0)
            If id = idModificar Then
                Tabla.Rows.RemoveAt(i)
                i = 0
            End If
        Next
        nfilas = DataGridView2.Rows.Count()
        DataGridView2.Refresh()
        DataGridView2.DataSource = Tabla
        DataGridView2.Columns(0).HeaderText = "ID"
        DataGridView2.Columns(1).HeaderText = "Cuenta"
        DataGridView2.Columns(2).HeaderText = "Descripción"
        DataGridView2.Columns(3).HeaderText = "Cargo"
        DataGridView2.Columns(4).HeaderText = "Abono"
        DataGridView2.Columns(5).HeaderText = "Poliza"
        DataGridView2.Columns(0).Visible = False
        DataGridView2.Columns(5).Visible = False
        DataGridView2.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill


    End Sub

    Private Sub btnEliminar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar2.Click
        If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim nFilas As Integer
            nFilas = DataGridView2.Rows.Count
            For i As Integer = 0 To nFilas - 1
                id = DataGridView2.Item(0, i).Value
                If id = idModificar Then
                    i = nFilas - 1
                    DataGridView2.Rows.RemoveAt(i)
                End If
            Next
            'nFilas = DataGridView2.Rows.Count
            nFilas = Tabla.Rows.Count
            For i As Integer = 0 To nFilas - 1
                id = Tabla.Rows(i)(0)
                If id = idModificar Then
                    i = nFilas - 1
                    Tabla.Rows.RemoveAt(i)
                End If
            Next
            'limpieza
            txtCuenta.Text = ""
            txtDesc.Text = ""
            'txtCargo.insText("0.00")
            'txtAbono.insText("0.00")
            btnAgregar.Text = "Agregar"
            btnEliminar2.Enabled = False
            PopUp("Eliminado", 90)
            Cuentas()
            txtCuenta.Focus()
        End If

    End Sub

    Private Sub DataGridView2_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        ' llenaDatos()
    End Sub
    Private Sub llenaDatos()
        Try
            idModificar = Integer.Parse(DataGridView2.Item(0, DataGridView2.CurrentCell.RowIndex).Value)
            Cuenta = DataGridView2.Item(1, DataGridView2.CurrentCell.RowIndex).Value
            Descripcion = DataGridView2.Item(2, DataGridView2.CurrentCell.RowIndex).Value
            Cargo = DataGridView2.Item(3, DataGridView2.CurrentCell.RowIndex).Value
            Abono = DataGridView2.Item(4, DataGridView2.CurrentCell.RowIndex).Value
            btnAgregar.Text = "Modificar"
            btnEliminar2.Enabled = True
            txtCuenta.Text = cuenta
            txtDesc.Text = descripcion
            'txtCargo.insText(Cargo)
            'txtAbono.insText(Abono)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    '*******************************Hacer tabla y dbDeposito lo siguiente solo es un copyPaste
    Private Sub NuevaPoliza()
        Dim P As New dbPagosProveedores(MySqlcon)
        Dim q As New dbDeposito(MySqlcon)
        Dim nFilas As Integer = 0
        txtPoliza.Text = q.FolioPoliza()
        txtCuenta.BackColor = Color.White
        txtDesc.BackColor = Color.White
        txtCuenta.Text = ""
        txtDesc.Text = ""
        'txtCargo.insText("0.00")
        'txtAbono.insText("0.00")
        nFilas = DataGridView2.Rows.Count()
        If nFilas > 0 Then
            'eliminar todo
            For i As Integer = nFilas - 1 To 0 Step -1
                DataGridView2.Rows.RemoveAt(i)
            Next

        End If
        nFilas = Tabla.Rows.Count
        If nFilas > 0 Then
            'eliminar todo
            For i As Integer = nFilas - 1 To 0 Step -1
                Tabla.Rows.RemoveAt(i)
            Next

        End If
        Cuentas()
        lblCargos.Text = "0.00"
        lblAbonos.Text = "0.00"
        lblDiferencia.Text = "0.00"
        buscarEncargado()
        ' btnGuardarPoliza.Text = "Guardar"
        txtPoliza.ReadOnly = False
    End Sub
    Private Sub buscarEncargado()
        Dim P As New dbPagosProveedores(MySqlcon)
        P.buscarEncargado()
        txtAutoriza.Text = P.Autoriza
        txtElabora.Text = P.Elabora
        txtRegistra.Text = P.Registra
    End Sub

    Private Sub txtElabora_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtElabora.Leave
        Dim P As New dbPagosProveedores(MySqlcon)
        P.guardarEncargado(txtElabora.Text, txtAutoriza.Text, txtRegistra.Text)
    End Sub

    Private Sub txtAutoriza_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAutoriza.Leave
        Dim P As New dbPagosProveedores(MySqlcon)
        P.guardarEncargado(txtElabora.Text, txtAutoriza.Text, txtRegistra.Text)
    End Sub

    Private Sub txtRegistra_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRegistra.Leave
        Dim P As New dbPagosProveedores(MySqlcon)
        P.guardarEncargado(txtElabora.Text, txtAutoriza.Text, txtRegistra.Text)
    End Sub
    Private Sub btnGuardarPoliza_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardarPoliza.Click
        guardar()

    End Sub

    Private Sub tienePoliza()
        Dim P As New dbDeposito(MySqlcon)
        If P.tienePoliza(idDeposito) > 0 Then
            'ya tiene poliza
            txtCuenta.BackColor = Color.White
            txtDesc.BackColor = Color.White
            txtCuenta.Text = ""
            txtDesc.Text = ""
            'txtCargo.insText("0.00")
            'txtAbono.insText("0.00")
            DataGridView2.DataSource = P.buscarPoliza(idDeposito)
            DataGridView2.Columns(0).Visible = False
            DataGridView2.Columns(5).Visible = False
            DataGridView2.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            Cuentas()
            btnGuardarPoliza.Text = "Modificar"
            btnGuardarPoliza.Enabled = True
            P.buscarDatos(idDeposito)
            txtElabora.Text = P.Elabora
            txtAutoriza.Text = P.Autoriza
            txtRegistra.Text = P.Registra
            txtPoliza.Text = P.NumPoliza
            txtPoliza.ReadOnly = True
            Dim nFilas As Integer
            nFilas = Tabla.Rows.Count
            If nFilas <> 0 Then


                For i As Integer = nFilas - 1 To 0 Step -1
                    Tabla.Rows(i).Delete()
                Next
            End If
            nFilas = DataGridView2.Rows.Count
            For i As Integer = 0 To nFilas - 1
                Dim dr5 As DataRow
                dr5 = Tabla.NewRow()
                dr5("ID") = DataGridView2.Item(0, i).Value
                dr5("Cuenta") = DataGridView2.Item(1, i).Value
                dr5("Descripción") = DataGridView2.Item(2, i).Value
                dr5("Cargo") = DataGridView2.Item(3, i).Value
                dr5("Abono") = DataGridView2.Item(4, i).Value
                dr5("Poliza") = DataGridView2.Item(5, i).Value
                Tabla.Rows.Add(dr5)
            Next

        Else
            NuevaPoliza()
        End If

    End Sub

    'Private Sub txtCargo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCargo.KeyPress
    '    If e.KeyChar = Chr(13) Then
    '        AgregarPoliza()
    '    End If
    'End Sub

    'Private Sub txtAbono_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAbono.KeyPress
    '    If e.KeyChar = Chr(13) Then
    '        AgregarPoliza()
    '    End If
    'End Sub

    'Private Sub cmbCuenta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCuenta.SelectedIndexChanged
    '    'barcosCombo()
    'End Sub
    'Private Sub barcosCombo()
    '    If Dt.Rows.Count > 0 Then
    '        Dim i As Integer = 0

    '        i = cmbCuenta.SelectedIndex()
    '        idCuenta = Integer.Parse(Dt.Rows(i)(0).ToString())
    '        txtCodigo.Text = Dt.Rows(i)(1).ToString()
    '    End If
    'End Sub
    Private Sub imprimirPoliza()
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""

        If btnGuardarPoliza.Text = "Guardar" Then
            NoErrores = False
            MensajeError += vbCrLf + "El pago no tiene poliza o no esta guardada, guarda la póliza antes de imprimir."

        End If

        If NoErrores = True Then
            LlenarDatos()
            btnAgregar.Enabled = True
            CantidadPago = Double.Parse(txtCantidad.Text)
            tienePoliza()
            Imprimir()
        End If


        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

        End If
    End Sub
    
    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        'If TipoImpresora = 0 Then
        '    ImpDb.DaZonaDetalles(TiposDocumentos.PDF, GlobalIdSucursalDefault)
        'Else
        '    ImpDb.DaZonaDetalles(TiposDocumentos.BancosPolizaDepositos, GlobalIdSucursalDefault)
        'End If
        If TipoImpresora = 0 Then
            DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujo(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub
    Private Sub DibujaPaginaEstatico(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos

        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CajasMovimientos, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CajasMovimientos + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If

        Catch ex As Exception

        End Try
        Dim Rec As RectangleF
        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer


        Dim LimY As Integer
        LimY = (pZonaY + pZonaYL) / 40 * 10
        C = Posicion
        YCoord = 0
        'Calcula páginas
        CuantaY = 0
        For Each n As NodoImpresionN In ImpNDD
            If n.DataPropertyName.Contains("descripcion") Then
                CuantaY = CuantaY + 4 + InsertaEnters(n.Valor, pZonaRG, YCoord, pZonaAlt)
            End If
        Next
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra

            '***********************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0

            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            If HayRenglon Then YCoord = YCoord + 4 + YExtra
            '***********************

            Posicion += 1
            C += 1
        End While


        'Nodos fijos
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n

                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then

                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            End Select
                        End If
                    End If
                End If
            Next

            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName.Contains("cancelado") Then
                    e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
                End If
            Next
            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

                YCoord = 0

            End If
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DibujaPaginaFlujo(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos
        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            'If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CajasMovimientos, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CajasMovimientos + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
        Catch ex As Exception

        End Try
        Dim Rec As RectangleF


        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then

                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If
                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 And niva.Tipo = 0 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

                YCoord = 0

            End If
            'If ncb.Visible = 1 And ncb.Tipo = 0 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

        For Each n As NodoImpresionN In ImpNDi
            If n.DataPropertyName.Contains("cancelado") Then
                e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
            End If
        Next
        'Nodos Detalles            
        XCoord = 0
        YCoord = 0
        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim LimY As Integer
        LimY = (pZonaY + pZonaYL) / 40 * 10
        C = Posicion
        YCoord = 0
        'Calcula páginas
        CuantaY = 0
        For Each n As NodoImpresionN In ImpNDD
            If n.DataPropertyName.Contains("descripcion") Then
                CuantaY = CuantaY + 4 + InsertaEnters(n.Valor, pZonaRG, YCoord, pZonaAlt)
            End If
        Next
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 And pModo = 0 Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra
            '*****************************************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0

            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            If HayRenglon Then YCoord = YCoord + 4 + YExtra
            '********************************

            Posicion += 1
            C += 1
        End While


        'Nodos fijos final
        If MasPaginas = True And (pModo = 2 Or pModo = 3) Then
            NumeroPagina += 1
            Exit Sub
        End If
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                'If n.DataPropertyName = "iva2" Then niva2 = n
                'If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 2 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(n.XL / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS
            Dim Ycoord2 As Integer
            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                Ycoord2 = 0
                If niva.Visible = 1 And niva.Tipo = 2 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                        If niva.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, Ycoord2)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        End If
                        'End If
                        'Next
                        Ycoord2 += 4
                        C += 1
                    End While
                End If

                Ycoord2 = 0

            End If

            'If ncb.Visible = 1 And ncb.Tipo = 2 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(Math.Abs((ncb.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub LlenaNodos(ByVal pidSucursal As Integer, ByVal pDocumento As Integer)
        Dim I As New dbImpresionesN(MySqlcon)
        Dim Fs As FontStyle
        ImpNDi.Clear()
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader
        Try
            dr = I.DaNodos(pDocumento, pidSucursal)
            While dr.Read
                Select Case dr("fuentestilo")
                    Case 1
                        Fs = FontStyle.Bold
                    Case 2
                        Fs = FontStyle.Italic
                    Case 0
                        Fs = FontStyle.Regular
                    Case 8
                        Fs = FontStyle.Strikeout
                    Case 4
                        Fs = FontStyle.Underline
                End Select
                ImpNDi.Add(New NodoImpresionN(dr("id"), dr("x"), dr("y"), dr("xl"), dr("yl"), dr("texto"), dr("datapropertyname"), New Font(dr("fuente").ToString, CSng(dr("fuentesize")), Fs, GraphicsUnit.Point), dr("alineacion"), dr("tipo"), dr("tipodato"), dr("visible"), dr("documento"), dr("tiponodo"), dr("idsucursal"), dr("conetiqueta"), dr("nombre"), dr("renglon"), dr("clasificacion")))
            End While
            dr.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LlenaNodosImpresion()

        ImpND.Clear()
        ImpNDD.Clear()
        Dim firmaChequeRecibido As String = "Firma cheque recibido"
        ImpND.Add(New NodoImpresionN("", "concepto", txtReferencia.Text, 0), "concepto")
        ImpND.Add(New NodoImpresionN("", "firmaChequeRecibido", firmaChequeRecibido, 0), "firmaChequeRecibido")
        Dim nFilas As Integer
        nFilas = DataGridView2.Rows.Count
        CuantosRenglones = 0
        For j As Integer = 0 To nfilas - 1
            ImpNDD.Add(New NodoImpresionN("", "Cuenta", DataGridView2.Item(1, j).Value, 0), "Cuenta" + Format(j, "000"))
            ImpNDD.Add(New NodoImpresionN("", "descripcion", DataGridView2.Item(2, j).Value, 0), "descripcion" + Format(j, "000"))

            If Double.Parse(DataGridView2.Item(3, j).Value) = 0 Then
                ImpNDD.Add(New NodoImpresionN("", "debe", " ", 0), "debe" + Format(j, "000"))
            Else
                ImpNDD.Add(New NodoImpresionN("", "debe", "$" + DataGridView2.Item(3, j).Value, 0), "debe" + Format(j, "000"))
            End If

            If Double.Parse(DataGridView2.Item(4, j).Value) = 0 Then
                ImpNDD.Add(New NodoImpresionN("", "haber", " ", 0), "haber" + Format(j, "000"))
            Else
                ImpNDD.Add(New NodoImpresionN("", "haber", "$" + DataGridView2.Item(4, j).Value, 0), "haber" + Format(j, "000"))
            End If


            CuantosRenglones += 1
        Next
        ImpND.Add(New NodoImpresionN("", "totalDebe", "$" + lblCargos.Text, 0), "totalDebe")
        ImpND.Add(New NodoImpresionN("", "totalHacer", "$" + lblAbonos.Text, 0), "totalHacer")
        ImpND.Add(New NodoImpresionN("", "numeroPoliza", txtPoliza.Text, 0), "numeroPoliza")
        ImpND.Add(New NodoImpresionN("", "hechaPor", txtElabora.Text, 0), "hechaPor")
        ImpND.Add(New NodoImpresionN("", "autorizadaPor", txtAutoriza.Text, 0), "autorizadaPor")
        ImpND.Add(New NodoImpresionN("", "auxiliares", txtRegistra.Text, 0), "auxiliares")
        Dim fecha As String = dtpFecha.Value.ToString("yyyy/MM/dd")
        Dim C As New dbCuentas(IdsCuentas.Valor(cmbCuenta.SelectedIndex), MySqlcon)

        ImpND.Add(New NodoImpresionN("", "nCuenta", C.Numero, 0), "nCuenta")
        ImpND.Add(New NodoImpresionN("", "banco", C.nombreBanco(C.Banco), 0), "banco")
        ImpND.Add(New NodoImpresionN("", "fecha", fecha, 0), "fecha") 'darle fornato
        ImpND.Add(New NodoImpresionN("", "referencia", txtReferencia.Text, 0), "referencia")
        ImpND.Add(New NodoImpresionN("", "bancoCliente", "", 0), "bancoCliente")
        ImpND.Add(New NodoImpresionN("", "cantidad", "$" + txtCantidad.Text, 0), "cantidad")

        Posicion = 0
        NumeroPagina = 1
    End Sub


    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        DibujaPaginaN(e.Graphics)
        If MasPaginas = True Or NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
        End If

        e.HasMorePages = MasPaginas
    End Sub

    Private Function InsertaEnters(ByRef Cadena As String, ByVal CadaCuantos As Integer, ByVal Y As Integer, ByVal AumentoY As Integer) As Integer
        Dim C As Integer
        C = 0
        Dim CC As Integer = 0
        Dim car As String
        Dim Yx As Integer = 0
        While C < Cadena.Length
            car = Cadena.Substring(C, 1)
            If car = Chr(13) Or CC = CadaCuantos Then
                Yx += AumentoY
                CC = 0
            Else
                CC += 1
            End If
            C += 1
        End While
        Return Yx
    End Function


    Private Sub Imprimir()
        Dim en As New Encriptador
        Dim P As New dbDeposito(MySqlcon) 'Modificado
        P.Buscar(idDeposito) 'Modificado
        Dim RutaPDF As String

        Dim Archivos As New dbSucursalesArchivos

        RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(P.Fecha, "yyyy") + "\") 'Modificado
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(P.Fecha, "yyyy") + "\" + Format(P.Fecha, "MM") + "\") 'Modificado
        RutaPDF = RutaPDF + "\" + Format(P.Fecha, "yyyy") + "\" + Format(P.Fecha, "MM") 'Modificado

        PrintDocument1.DocumentName = "Póliza Depósito - " + P.Referencia + " " + P.Banco 'Modificado
        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Impresora = SA.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.CajasMovimientos) 'Modificado
        TipoImpresora = SA.TipoImpresora



        'obj.WriteSettings()
        If Impresora = "Bullzip PDF Printer" Then
            Dim obj As New Bullzip.PdfWriter.PdfSettings
            obj.Init()
            obj.PrinterName = Impresora
            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            obj.SetValue("ShowSettings", "never")
            obj.SetValue("ShowPDF", "yes")
            obj.SetValue("ShowSaveAS", "nofile")
            obj.SetValue("ConfirmOverwrite", "no")
            obj.SetValue("Target", "printer")
            obj.WriteSettings()
        End If
        PrintDocument1.PrinterSettings.PrinterName = Impresora
        LlenaNodosImpresion()
        'If TipoImpresora = 0 Then
        '    LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.BancosPolizaDepositos) 'Modificado
        'Else
        '    LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.BancosPolizaDepositos) 'Modificado
        'End If
        PrintDocument1.Print()

    End Sub

    Private Sub cmbBancos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Dt2.Rows.Count > 0 Then
        '    Dim i As Integer = 0
        '    i = cmbBancos.SelectedIndex()
        '    idBanco = Integer.Parse(Dt2.Rows(i)(0).ToString())

        '    ' txtCodigo.Text = Dt.Rows(idCuenta)(2).ToString()
        'End If
    End Sub
    'Guardar global
    Private Sub guardar()
        'guardar Depósito
        Try
            Dim P As New dbDeposito(MySqlcon)
            Dim fecha As String = dtpFecha.Value.ToString("yyyy/MM/dd")
            Dim nFilas As Integer = 0
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            ' Dim nFilas As Integer
            nFilas = DataGridView2.RowCount()
            txtPoliza.BackColor = Color.White
            txtReferencia.BackColor = Color.White

            'If nFilas > 0 Then
            '    If Double.Parse(txtCantidad.Text) <> Double.Parse(lblCargos.Text) Or Double.Parse(txtCantidad.Text) <> Double.Parse(lblAbonos.Text) Then
            '        NoErrores = False
            '        MensajeError += vbCrLf + "La póliza no coincide con el pago a proveedor."
            '        'txtCo.BackColor = Color.FromArgb(250, 150, 150)
            '    End If
            '    If lblDiferencia.Text <> "0.00" Then
            '        NoErrores = False
            '        MensajeError += vbCrLf + "La poliza no esta balanceada."

            '    End If
            'End If

            'If txtCodigo.Text = "" Then
            '    NoErrores = False
            '    MensajeError += vbCrLf + "Debe indicar una cuena de Banco."
            '    cmbCuenta.BackColor = Color.FromArgb(250, 150, 150)
            'End If
            If txtReferencia.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + "Debe indicar una referencia."
                txtReferencia.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If txtCantidad.Text = "" Or IsNumeric(txtCantidad.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + "La cantidad debe ser un valor numérico."
            End If


            If P.Repetida(fecha, txtReferencia.Text, IdsCuentas.Valor(cmbCuenta.SelectedIndex).ToString, txtCantidad.Text) > 0 Then
                NoErrores = False
                MensajeError += vbCrLf + "Los datos proporcionados coinciden con un depósito ya almacenado, favor de cambiar los datos."
            End If

            If btnGuardarPoliza.Text = "Modificar" And IdPagoProv <> 0 Then
                NoErrores = False
                MensajeError += " No puede modificar un deposito que viene de un traspaso. Lo puede hacer desde el Pago que generó el traspaso."
            End If
            'If nFilas < 1 Then
            '    NoErrores = False
            '    MensajeError += vbCrLf + "No hay datos agregados en la Póliza"
            'End If

            If NoErrores = True Then

                If btnGuardarPoliza.Text = "Guardar" Then
                    If P.folioPolizaRepetida(txtPoliza.Text) > 0 And nFilas > 0 Then
                        MsgBox("Ya existe una póliza con la misma clave, favor de cambiarla.", MsgBoxStyle.Critical, GlobalNombreApp)
                        txtPoliza.BackColor = Color.FromArgb(250, 150, 150)
                        NoErrores = False
                    Else

                        'guardar
                        'deposito
                        idDeposito = P.Guardar(fecha, txtReferencia.Text, "1", txtCantidad.Text, IdsCuentas.Valor(cmbCuenta.SelectedIndex), txtComentario.Text, DateTimePicker1.Value.ToString("yyyy/MM/dd"))
                        If Intedrado = 1 And FacturasAfectadas <> "" And TipoLigue = 1 Then
                            P.LigarAComprasPagos(idDeposito, WhereStr)
                        End If
                        If Intedrado = 1 And FacturasAfectadas <> "" And TipoLigue = 2 Then
                            P.LigarDeposito(WhereStr, idDeposito)
                        End If
                        If Ids.Length > 0 Then
                            For Each Idc As Integer In Ids
                                P.LigarDeposito(Idc.ToString, idDeposito)
                            Next
                        End If
                        CantidadPago = Double.Parse(txtCantidad.Text)

                        'poliza
                        nFilas = DataGridView2.Rows.Count
                        For i As Integer = 0 To nFilas - 1
                            P.guardarPoliza(idDeposito, txtPoliza.Text, DataGridView2.Item(0, i).Value, DataGridView2.Item(1, i).Value, DataGridView2.Item(2, i).Value, DataGridView2.Item(3, i).Value, DataGridView2.Item(4, i).Value, txtElabora.Text, txtAutoriza.Text, txtRegistra.Text)
                        Next
                        btnGuardarPoliza.Text = "Modificar"

                        txtPoliza.ReadOnly = True
                        If MsgBox("¿Imprimir depósito?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            ImprimirDeposito()
                        End If
                        'LimpiarBusq()
                        '   Dim aux As String
                        ' aux = txtCodigo.Text
                        ' cmbBancos.Text = auxbanco
                        GeneraPoliza(idDeposito)
                        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = False Then PopUp("Guardado", 90)
                        Nuevo(True)
                        btnImprimirDeposito.Enabled = True
                        'barcosCombo()

                        If Intedrado = 1 Then Me.DialogResult = Windows.Forms.DialogResult.OK
                        'txtCodigo.Text = aux
                    End If
                Else 'Modificar
                    If P.LigadoABancos(idDeposito) Then
                        MsgBox("Para poder modificar este movimiento tiene que eliminar todos los pagos relacionados a este.", MsgBoxStyle.Information, GlobalNombreApp)
                        Exit Sub
                    End If
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        'modificar
                        'depósito
                        P.Modificar(fecha, txtReferencia.Text, "1", txtCantidad.Text, idDeposito, IdsCuentas.Valor(cmbCuenta.SelectedIndex).ToString, txtComentario.Text, DateTimePicker1.Value.ToString("yyyy/MM/dd"))
                        P.EliminarPoliza(idDeposito)
                        nFilas = DataGridView2.Rows.Count
                        For i As Integer = 0 To nFilas - 1
                            P.guardarPoliza(idDeposito, txtPoliza.Text, DataGridView2.Item(0, i).Value, DataGridView2.Item(1, i).Value, DataGridView2.Item(2, i).Value, DataGridView2.Item(3, i).Value, DataGridView2.Item(4, i).Value, txtElabora.Text, txtAutoriza.Text, txtRegistra.Text)
                        Next
                        If Ids.Length > 0 Then
                            For Each Idc As Integer In Ids
                                P.LigarDeposito(Idc.ToString, idDeposito)
                            Next
                        End If
                        btnGuardarPoliza.Text = "Modificar"
                        PopUp("Modificado", 90)
                        ' Dim aux As String
                        ' aux = txtCodigo.Text
                        If MsgBox("¿Imprimir depósito?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            ImprimirDeposito()
                        End If
                        ' txtCodigo.Text = aux
                        'LimpiarBusq()
                        'barcosCombo()
                        Nuevo(True)
                    End If
                End If
            End If


            If NoErrores = False Then
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
        End Try
        



    End Sub
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        llenaDatos()
    End Sub

    Private Sub btnLimpiarRenglon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiarRenglon.Click
        txtCuenta.Text = ""
        txtDesc.Text = ""
        lblDescripcion.Text = ""
        '  txtAbono.Text = "0.00"
        'txtAbono.insText("0.00")
        'txtCargo.insText("0.00")
        ' txtCargo.Text = "0.00"
        btnAgregar.Text = "Agregar"
        btnEliminar2.Enabled = False
        txtCuenta.Focus()
    End Sub

    Private Sub btnImprimirDatos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnImprimirDeposito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimirDeposito.Click
        ImprimirDeposito()
    End Sub
    Private Sub ImprimirDeposito()
        Dim P As New dbDeposito(MySqlcon)
        Dim Suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        P.Buscar(idDeposito)
        idCuenta = Integer.Parse(P.Banco2)
        'idDeposito = id
        'Dim q As New dbPagosProveedores(MySqlcon)
        'Dim nombreEmpresa As String = q.nombre()
        'Dim rfc As String = q.RFC()
        'Dim direccion As String = q.Calles(nombreEmpresa)
        'Dim direccion2 As String = q.direccion2(nombreEmpresa)
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Rep = New repDepositoSolo
        'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
        'Rep.SetDataSource(P.Reporte1())
        Rep.SetParameterValue("encabezado", Suc.Nombre)
        Rep.SetParameterValue("direccion", Suc.Direccion + " " + Suc.NoExterior + " " + Suc.NoInterior)
        Rep.SetParameterValue("rfc", Suc.RFC)
        Rep.SetParameterValue("direccion2", Suc.Ciudad + " " + Suc.Estado + " " + Suc.CP)

        Rep.SetParameterValue("noCuenta", cmbCuenta.Text)
        Rep.SetParameterValue("bancoCuenta", "")
        Rep.SetParameterValue("fecha", P.Fecha)
        Rep.SetParameterValue("referencia", P.Referencia)
        Rep.SetParameterValue("banco", "")
        Rep.SetParameterValue("cantidad", Double.Parse(P.Cantidad))
        Rep.SetParameterValue("comentario", P.comentario)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub grpBusqueda_Enter(sender As Object, e As EventArgs) Handles grpBusqueda.Enter

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Forma As New frmBuscaDocumentoVenta(0, False, 3, GlobalIdSucursalDefault, 0, False, False, False, 1, True, "", True)
        If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Ids = Forma.id
            If txtComentario.Text = "" Then txtComentario.Text = "FACTURAS:"
            For Each Str As String In Forma.Folios
                txtComentario.Text += " " + Str
            Next

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If idDeposito <> 0 Then
            Dim f As New frmVentasConsulta(ModosDeBusqueda.Principal, idDeposito, False)
            f.ShowDialog()
            f.Dispose()
        Else
            MsgBox("Debe seleccionar un deposito.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub GeneraPoliza(pIdDeposito As Integer)
        Try
            Dim Op As New dbOpciones(MySqlcon)
            If Op.IntegrarContabilidad = 1 And GlobalconIntegracion Then
                Dim M As New dbContabilidadMascaras(MySqlcon)
                Dim V As New dbDeposito(MySqlcon)
                V.ID = pidDeposito
                V.Buscar(pidDeposito)
                Dim Canceladas As Byte = 0
                Dim credito As Byte = 2
                Dim cuantas As Integer
                'If V.Estado = Estados.Cancelada Then
                '    Canceladas = 1
                'End If
                'Dim FP As New dbFormasdePago(V.IdFormadePago, MySqlcon)
                'If FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
                '    credito = 0
                'Else
                '    credito = 1
                'End If
                cuantas = M.CuantasHay(4, Canceladas, credito)
                If cuantas > 0 Then
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(4, Canceladas, credito)
                    Else
                        Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 4)
                        f.ShowDialog()
                        If f.DialogResult = Windows.Forms.DialogResult.OK Then
                            M.ID = f.IdMascara
                        Else
                            Exit Sub
                        End If
                    End If
                    M.LlenaDatos()
                    Dim GP As dbContabilidadGeneraPolizas
                    'If Canceladas = 0 Then
                    GP = New dbContabilidadGeneraPolizas(M, V.FechaConta, V.FechaConta, V.FechaConta)
                    'Else
                    '   GP = New dbContabilidadGeneraPolizas(M, V.FechaCancelado, V.FechaCancelado, V.FechaCancelado)
                    'End If
                    GP.GeneraPolizaGeneral(V.ID, V.Banco2, 3, 0, 0, 0, 0)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = True Then
                        If GP.Exito Then
                            Dim frmp As New frmContabilidadPolizasN(GP.IdPoliza)
                            frmp.ShowDialog()
                            frmp.Dispose()
                        Else
                            MsgBox("No se generó la póliza.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    Else
                        PopUp("Póliza Generada", 90)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub cmbCuenta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCuenta.SelectedIndexChanged
        If ConsultaOn Then FiltroTodos()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If ConsultaOn Then FiltroTodos()
    End Sub

    Private Sub dtpFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha.ValueChanged
        DateTimePicker1.Value = dtpFecha.Value
    End Sub
    
    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If ConsultaOn Then Label1.Text = "Total venta contado: " + v.DaTotalContadoConta(DateTimePicker1.Value.ToString("yyyy/MM/dd"), 0).ToString("###,###,##0.00") + " Total depósitos no ligados: " + dep.DatotalDepositosConta(DateTimePicker1.Value.ToString("yyyy/MM/dd"), "").ToString("###,###,##0.00")
    End Sub

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged

    End Sub
End Class