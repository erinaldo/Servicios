Public Class frmPagosProveedores
    Dim IdsConceptos As New elemento
    Dim esCheque As Integer = 0
    'Dim id As Integer = 0
    Dim idModificar As Integer = 0
    Dim letras As Integer = 0
    Dim Tabla As New DataTable
    Dim cuenta As String
    Dim descripcion As String
    Dim cargo As String
    Dim abono As String
    Dim CantidadPago As Double = 0
    Dim idPagoProv As Integer
    Dim temporarSolicitud, temporalFolio As String
    Dim IDaux As Integer
    Dim Dt As DataTable
    Dim idCuenta As Integer
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
    Dim Cheque As Boolean = False
    Dim Intedrado As Byte
    Dim FacturasAfectadas As String
    Dim FormadePago As String
    Dim ProveedorInt As String
    Dim CantidadInt As String
    Dim idProveedor As Integer
    Dim idProveedorAux As Integer
    Dim WhereStr As String
    Dim FechaInt As String
    Dim IdsCuentas As New elemento
    Dim ConsultaOn As Boolean
    Dim TipoLigue As Byte
    Dim Ids As Integer()
    Dim IdsBancos As New elemento
    Dim IdsMonedas As New elemento
    Dim RFC As String
    Dim IdsCuentas2 As New elemento
    Dim IdBancoProv As Integer
    Dim CuentaProv As String
    Public Sub New(ByVal pIntegrado As Byte, ByVal pFacturasAfectadas As String, ByVal pFormadepago As String, ByVal pNproveedor As String, ByVal pCantidadInt As String, ByVal pIdProveedor As Integer, ByVal pWherestr As String, ByVal pFechaInt As String, PTipoLigue As Byte)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Intedrado = pIntegrado
        FacturasAfectadas = pFacturasAfectadas
        FormadePago = pFormadepago
        ProveedorInt = pNproveedor
        CantidadInt = pCantidadInt
        idProveedorAux = pIdProveedor
        WhereStr = pWherestr
        FechaInt = pFechaInt
        TipoLigue = PTipoLigue
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmPagosProveedores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        Try
            btnImprimirPago.Image = Image.FromFile(Application.StartupPath + "\iconos\printer.png")
        Catch ex As Exception

        End Try
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        LlenaCombos("tblcuentas", cmbCuenta, "concat(numero,' ',(select nombre from tblbancoscatalogo where idbanco=banco))", "cuentaN", "idcuenta", IdsCuentas)
        LlenaCombos("tblcuentas", ComboBox1, "concat(numero,' ',(select nombre from tblbancoscatalogo where idbanco=banco))", "cuentaN", "idcuenta", IdsCuentas2)
        LlenaCombos("tblbancoscatalogo", cmbBancos, "nombre", "nombren", "idbanco", IdsBancos)
        LlenaCombos("tblmonedassat", cmbMonedaTrans, "concat(codigo,' - ',moneda)", "nombret", "id", IdsMonedas, , , "codigo")
        'Year = Date.Now.Year.ToString
        'Month = Format(Date.Now.Month, "00")

        dtpDesde.Value = "01/" + Date.Now.ToString("MM") + "/" + Date.Now.ToString("yyyy")
        chkFecha.Checked = True
        'llenarCuenta()
        LlenaCombos("tblformasdepago", cmbTipoPago, "nombre", "nombrec", "idforma", IdsConceptos, "tipo=1 and parabancos=1")
        Tablacrear()
        ConsultaOn = True
        Nuevo(False)
        'FiltroAmbos()
        If Intedrado = 1 Then
            cmbProvedor.Text = ProveedorInt
            txtReferencia.Text = FacturasAfectadas
            idProveedor = idProveedorAux
            txtCantidad.Text = Format(CDbl(CantidadInt), "0.00")
            dtpFecha.Value = FechaInt
            dtpFechaCobro.Value = FechaInt
            Dim prov As New dbproveedores(idProveedor, MySqlcon)
            RFC = prov.RFC
        End If
        If TipoLigue <> 0 Then
            Button1.Visible = False
            Button4.Visible = False
            CheckBox1.Visible = False
        End If
        Me.Show()
        My.Application.DoEvents()
        txtFolio.Focus()
    End Sub

    Private Sub Nuevo(ByVal ResetParametros As Boolean)
        Dim year As String
        Dim month As String
        IdBancoProv = 0
        CuentaProv = ""
        cmbTipoPago.BackColor = Color.White
        txtFolio.BackColor = Color.White
        txtReferencia.BackColor = Color.White
        cmbProvedor.BackColor = Color.White
        txtBancoOriExTrans.Text = ""
        cmbTipoPago.Text = ""
        txtFolio.Text = ""
        txtReferencia.Text = ""
        cmbProvedor.Text = ""
        txtCantidad.Text = "0.00"
        TextBox1.Text = "0.00"
        TextBox2.Text = "0.00"
        TextBox5.Text = "0.00"
        cmbIVA.Text = "16"
        Leyenda.Checked = False
        cmbMonedaTrans.SelectedIndex = 100
        cmbBancos.SelectedIndex = 0
        cmbTipoPago.Focus()
        esCheque = 0
        btnGuardarPoliza.Text = "Guardar"
        If ResetParametros Then
            FacturasAfectadas = ""
            WhereStr = ""
        End If
        btnEliminar2.Enabled = False
        year = Date.Now.Year.ToString
        month = Format(Date.Now.Month, "00")
        ConsultaOn = False
        CheckBox1.Checked = False
        ConsultaOn = True
        FiltroTodos()
        ReDim Ids(-1)
        idPagoProv = 0
        idProveedor = 0
        'buscarEncargado()
        If chkFecha.Checked = False Then
            dtpFecha.Value = Date.Today
            dtpFechaCobro.Value = Date.Today
        End If
        txtCuentaDestinoTrans.Text = ""
        txtBancoDesExTrans.Text = ""
        txtTipoCambioTran.Text = "0"
        CheckBox1.Checked = False
        CheckBox1.Enabled = True
        Label40.Visible = False
        NuevaPoliza()
    End Sub
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
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""
        txtCuenta.BackColor = Color.White
        txtDesc.BackColor = Color.White
        txtAbono.BackColor = Color.White
        txtCargo.BackColor = Color.White
        txtPoliza.BackColor = Color.White
        'Verificar si ya existe el numero de Poliza
        If txtCuenta.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar un número de cuenta."
            txtCuenta.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtDesc.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar una descripción."
            txtDesc.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtAbono.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar un valor al abono."
            txtAbono.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtCargo.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar un valor al cargo."
            txtCargo.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtPoliza.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar un número a la póliza."
            txtPoliza.BackColor = Color.FromArgb(250, 150, 150)
        End If

        If NoErrores = True Then
            If btnAgregar.Text = "Agregar" Then
                IDaux = Tabla.Rows.Count
                agregarFila()
                txtCuenta.Text = ""
                txtDesc.Text = ""
                txtCargo.Text = "0.00"
                txtAbono.Text = "0.00"
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
                txtCargo.Text = "0.00"
                txtAbono.Text = "0.00"
                btnAgregar.Text = "Agregar"
                btnEliminar2.Enabled = False
                PopUp("Modificado", 90)
                txtCuenta.Focus()
                'End If
            End If
            End If


            If NoErrores = False Then
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

            End If


    End Sub
    Private Sub eliminarFila()
        Dim nfilas As Integer
        Dim nFilas2 As Integer
        nFilas2 = Tabla.Rows.Count
        Dim id As Integer
        For i As Integer = nFilas2 - 1 To 0 Step -1
            id = Tabla.Rows(i)(0)
            If id = idModificar Then
                Tabla.Rows.RemoveAt(i)
                i = 0
            End If
        Next
        nfilas = DataGridView2.Rows.Count()

        'Agregar tabla
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
        ' tot = Cargos + Abonos
        tot = Cargos - Abonos
        'lblDiferencia.Text = Format(txtCantidad.Text - tot.ToString(), "0.00")
        lblDiferencia.Text = Format(tot, "0.00")
        If Double.Parse(lblDiferencia.Text) <> 0.0 Then
            lblDiferencia.ForeColor = Color.Red
        Else
            lblDiferencia.ForeColor = Color.Black

            lblDiferencia.Text = "0.00"
        End If
        lblCargos.Text = Format(Cargos, "0.00")
        lblAbonos.Text = Format(Abonos, "0.00")
        'End If
    End Sub
    Private Sub Folio()
        Dim P As New dbPagosProveedores(MySqlcon)
        txtFolio.Text = P.Folio().ToString()
        cmbProvedor.Focus()
        Leyenda.Checked = False
    End Sub
    Private Sub FolioEfectivo()
        Dim P As New dbPagosProveedores(MySqlcon)
        txtFolio.Text = P.FolioEfectivo().ToString()
        cmbProvedor.Focus()
        Leyenda.Checked = False
    End Sub
  
    Private Sub cmbTipoPago_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipoPago.SelectedIndexChanged
        Dim des As String = ""
        If cmbTipoPago.Text.ToUpper.Contains("CHEQUE") Then
            txtBancoOriExTrans.Enabled = True
            txtCuentaDestinoTrans.Enabled = False
            txtCuentaDestinoTrans.Text = ""
            cmbBancos.Enabled = False
        Else
            txtBancoOriExTrans.Enabled = False
            txtBancoOriExTrans.Text = ""
            txtCuentaDestinoTrans.Enabled = True
            cmbBancos.Enabled = True
            txtCuentaDestinoTrans.Text = CuentaProv
            cmbBancos.SelectedIndex = IdsBancos.Busca(IdBancoProv)
        End If
        If cmbTipoPago.SelectedIndex >= 0 Then
            des = cmbTipoPago.SelectedItem
            If des = "CHEQUE NOMINATIVO" Then
                Folio()
                esCheque = 1
            Else
                If des = "CHEQUE" Then
                    Folio()
                    esCheque = 1
                Else
                    If des = "CHEQUES" Then
                        Folio()
                    End If
                    If des = "EFECTIVO" Then
                        FolioEfectivo()
                    Else
                        txtFolio.Text = ""
                        txtFolio.Focus()
                        esCheque = False
                    End If
                End If
            End If

        End If

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

    Private Sub txtCantidad_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.Enter
        txtCantidad.SelectAll()
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
            If x >= 2000.0 Then
                Leyenda.Checked = True
            Else
                Leyenda.Checked = False
            End If

            txtCantidad.Text = Format(x, "0.00")
        End If
        Cuentas()
    End Sub

    Private Sub txtCantidad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.Click
        txtCantidad.SelectAll()
    End Sub

   
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click

        Nuevo(True)
        NuevaPoliza()
       
        
    End Sub

    

    'Public Function fechaFormato(ByVal pfecha As DateTimePicker) As String
    '    Dim Dia As String
    '    Dim Mes As String
    '    Dim year As String
    '    Dim fechita As String


    '    Dia = Format(pfecha.Value.Date.Day, "00")
    '    Mes = Format(pfecha.Value.Date.Month, "00")
    '    year = pfecha.Value.Date.Year

    '    fechita = year + "/" + Mes + "/" + Dia
    '    Return fechita
    'End Function

    Public Sub FiltroTodos()
        Try
            If ConsultaOn Then
                Dim PrimerCeldaRow As Integer = -1
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex

                Dim P As New dbPagosProveedores(MySqlcon)
                'Dim tabla As DataTable
                'Dim TablaFull As New DataTable
                'Dim Q As New dbDeposito(MySqlcon)
                'TablaFull.Columns.Add("id")
                'TablaFull.Columns.Add("Folio")
                'TablaFull.Columns.Add("Proveedor")
                'TablaFull.Columns.Add("Fecha")
                'TablaFull.Columns.Add("FechaCobro")
                'TablaFull.Columns.Add("Referencia")
                'TablaFull.Columns.Add("Cantidad")
                'TablaFull.Columns.Add("Tipo")
                'TablaFull.Columns.Add("IVA")
                'TablaFull.Columns.Add("Leyenda")
                'TablaFull.Columns.Add("esCheque")
                'TablaFull.Columns.Add("Estado")
                'TablaFull.Columns.Add("numCuenta")
                'TablaFull.Columns.Add("Banco")

                'tabla = P.filtroTodos().ToTable()

                'For i As Integer = 0 To tabla.Rows.Count() - 1
                '    Dim dr1 As DataRow
                '    dr1 = TablaFull.NewRow()
                '    dr1("id") = tabla.Rows(i)(0).ToString
                '    dr1("Folio") = tabla.Rows(i)(1).ToString
                '    dr1("Proveedor") = tabla.Rows(i)(2).ToString
                '    dr1("Fecha") = tabla.Rows(i)(3).ToString
                '    dr1("FechaCobro") = tabla.Rows(i)(4).ToString
                '    dr1("Referencia") = tabla.Rows(i)(5).ToString
                '    dr1("Cantidad") = tabla.Rows(i)(6).ToString
                '    dr1("Tipo") = tabla.Rows(i)(7).ToString
                '    dr1("IVA") = tabla.Rows(i)(8).ToString
                '    dr1("Leyenda") = tabla.Rows(i)(9).ToString
                '    dr1("esCheque") = tabla.Rows(i)(10).ToString
                '    dr1("Estado") = tabla.Rows(i)(11).ToString
                '    dr1("numCuenta") = Q.buscarNumeroCuenta(tabla.Rows(i)(12).ToString)
                '    dr1("Banco") = Q.buscarBancoCuenta(tabla.Rows(i)(12).ToString)


                '    TablaFull.Rows.Add(dr1)
                'Next
                DataGridView1.DataSource = P.filtroTodos(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), txtBuscProve.Text, IdsCuentas.Valor(cmbCuenta.SelectedIndex), TextBox3.Text, TextBox4.Text)
                DataGridView1.Columns(0).HeaderText = "F. Int."
                DataGridView1.Columns(1).HeaderText = "Folio"
                DataGridView1.Columns(2).HeaderText = "Fecha"
                DataGridView1.Columns(3).HeaderText = "Importe"
                DataGridView1.Columns(4).HeaderText = "Proveedor"
                DataGridView1.Columns(5).HeaderText = "Fecha Cobro"
                DataGridView1.Columns(6).HeaderText = "Referencia"
                DataGridView1.Columns(7).HeaderText = "N. Cuenta"
                DataGridView1.Columns(8).HeaderText = "Banco"

                'DataGridView1.Columns(0).Visible = False
                'DataGridView1.Columns(11).Visible = False
                'DataGridView1.Columns(10).Visible = False
                'DataGridView1.Columns(9).Visible = False
                'DataGridView1.Columns(8).Visible = False

                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    'Public Sub FiltroFecha()
    '    Try
    '        Dim PrimerCeldaRow As Integer = -1
    '        If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
    '        Dim P As New dbPagosProveedores(MySqlcon)
    '        Dim tabla As DataTable
    '        Dim TablaFull As New DataTable
    '        Dim Q As New dbDeposito(MySqlcon)
    '        TablaFull.Columns.Add("id")
    '        TablaFull.Columns.Add("Folio")
    '        TablaFull.Columns.Add("Proveedor")
    '        TablaFull.Columns.Add("Fecha")
    '        TablaFull.Columns.Add("FechaCobro")
    '        TablaFull.Columns.Add("Referencia")
    '        TablaFull.Columns.Add("Cantidad")
    '        TablaFull.Columns.Add("Tipo")
    '        TablaFull.Columns.Add("IVA")
    '        TablaFull.Columns.Add("Leyenda")
    '        TablaFull.Columns.Add("esCheque")
    '        TablaFull.Columns.Add("Estado")
    '        TablaFull.Columns.Add("numCuenta")
    '        TablaFull.Columns.Add("Banco")

    '        'tabla = P.filtroFecha(fechaFormato(dtpDesde), fechaFormato(dtpHasta)).ToTable()

    '        For i As Integer = 0 To tabla.Rows.Count() - 1
    '            Dim dr As DataRow
    '            dr = TablaFull.NewRow()
    '            dr("id") = tabla.Rows(i)(0).ToString
    '            dr("Folio") = tabla.Rows(i)(1).ToString
    '            dr("Proveedor") = tabla.Rows(i)(2).ToString
    '            dr("Fecha") = tabla.Rows(i)(3).ToString
    '            dr("FechaCobro") = tabla.Rows(i)(4).ToString
    '            dr("Referencia") = tabla.Rows(i)(5).ToString
    '            dr("Cantidad") = tabla.Rows(i)(6).ToString
    '            dr("Tipo") = tabla.Rows(i)(7).ToString
    '            dr("IVA") = tabla.Rows(i)(8).ToString
    '            dr("Leyenda") = tabla.Rows(i)(9).ToString
    '            dr("esCheque") = tabla.Rows(i)(10).ToString
    '            dr("Estado") = tabla.Rows(i)(11).ToString
    '            dr("numCuenta") = Q.buscarNumeroCuenta(tabla.Rows(i)(12).ToString)
    '            dr("Banco") = Q.buscarBancoCuenta(tabla.Rows(i)(12).ToString)


    '            TablaFull.Rows.Add(dr)
    '        Next
    '        DataGridView1.DataSource = TablaFull
    '        DataGridView1.Columns(0).HeaderText = "id"
    '        DataGridView1.Columns(1).HeaderText = "Folio"
    '        DataGridView1.Columns(2).HeaderText = "Proveedor"
    '        DataGridView1.Columns(3).HeaderText = "Fecha"
    '        DataGridView1.Columns(4).HeaderText = "Fecha Cobro"
    '        DataGridView1.Columns(5).HeaderText = "Referencia"
    '        DataGridView1.Columns(6).HeaderText = "Cantidad"
    '        DataGridView1.Columns(7).HeaderText = "Tipo"
    '        DataGridView1.Columns(8).HeaderText = "IVA"
    '        DataGridView1.Columns(9).HeaderText = "Leyenda"
    '        DataGridView1.Columns(10).HeaderText = "EsCheque"
    '        DataGridView1.Columns(11).HeaderText = "Estado"
    '        DataGridView1.Columns(12).HeaderText = "N. Cuenta"
    '        DataGridView1.Columns(13).HeaderText = "Banco"

    '        DataGridView1.Columns(0).Visible = False
    '        DataGridView1.Columns(11).Visible = False
    '        DataGridView1.Columns(10).Visible = False
    '        DataGridView1.Columns(9).Visible = False
    '        DataGridView1.Columns(8).Visible = False

    '        If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try
    'End Sub
    'Public Sub FiltroProv()
    '    Try
    '        Dim PrimerCeldaRow As Integer = -1
    '        If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
    '        Dim P As New dbPagosProveedores(MySqlcon)
    '        Dim tabla As DataTable
    '        Dim TablaFull As New DataTable
    '        Dim Q As New dbDeposito(MySqlcon)
    '        TablaFull.Columns.Add("id")
    '        TablaFull.Columns.Add("Folio")
    '        TablaFull.Columns.Add("Proveedor")
    '        TablaFull.Columns.Add("Fecha")
    '        TablaFull.Columns.Add("FechaCobro")
    '        TablaFull.Columns.Add("Referencia")
    '        TablaFull.Columns.Add("Cantidad")
    '        TablaFull.Columns.Add("Tipo")
    '        TablaFull.Columns.Add("IVA")
    '        TablaFull.Columns.Add("Leyenda")
    '        TablaFull.Columns.Add("esCheque")
    '        TablaFull.Columns.Add("Estado")
    '        TablaFull.Columns.Add("numCuenta")
    '        TablaFull.Columns.Add("Banco")

    '        tabla = P.filtroProv(txtBuscProve.Text).ToTable()

    '        For i As Integer = 0 To tabla.Rows.Count() - 1
    '            Dim dr As DataRow
    '            dr = TablaFull.NewRow()
    '            dr("id") = tabla.Rows(i)(0).ToString
    '            dr("Folio") = tabla.Rows(i)(1).ToString
    '            dr("Proveedor") = tabla.Rows(i)(2).ToString
    '            dr("Fecha") = tabla.Rows(i)(3).ToString
    '            dr("FechaCobro") = tabla.Rows(i)(4).ToString
    '            dr("Referencia") = tabla.Rows(i)(5).ToString
    '            dr("Cantidad") = tabla.Rows(i)(6).ToString
    '            dr("Tipo") = tabla.Rows(i)(7).ToString
    '            dr("IVA") = tabla.Rows(i)(8).ToString
    '            dr("Leyenda") = tabla.Rows(i)(9).ToString
    '            dr("esCheque") = tabla.Rows(i)(10).ToString
    '            dr("Estado") = tabla.Rows(i)(11).ToString
    '            dr("numCuenta") = Q.buscarNumeroCuenta(tabla.Rows(i)(12).ToString)
    '            dr("Banco") = Q.buscarBancoCuenta(tabla.Rows(i)(12).ToString)


    '            TablaFull.Rows.Add(dr)
    '        Next
    '        DataGridView1.DataSource = TablaFull
    '        DataGridView1.Columns(0).HeaderText = "id"
    '        DataGridView1.Columns(1).HeaderText = "Folio"
    '        DataGridView1.Columns(2).HeaderText = "Proveedor"
    '        DataGridView1.Columns(3).HeaderText = "Fecha"
    '        DataGridView1.Columns(4).HeaderText = "Fecha Cobro"
    '        DataGridView1.Columns(5).HeaderText = "Referencia"
    '        DataGridView1.Columns(6).HeaderText = "Cantidad"
    '        DataGridView1.Columns(7).HeaderText = "Tipo"
    '        DataGridView1.Columns(8).HeaderText = "IVA"
    '        DataGridView1.Columns(9).HeaderText = "Leyenda"
    '        DataGridView1.Columns(10).HeaderText = "EsCheque"
    '        DataGridView1.Columns(11).HeaderText = "Estado"
    '        DataGridView1.Columns(12).HeaderText = "N. Cuenta"
    '        DataGridView1.Columns(13).HeaderText = "Banco"

    '        DataGridView1.Columns(0).Visible = False
    '        DataGridView1.Columns(11).Visible = False
    '        DataGridView1.Columns(10).Visible = False
    '        DataGridView1.Columns(9).Visible = False
    '        DataGridView1.Columns(8).Visible = False

    '        If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try
    'End Sub
    'Public Sub FiltroAmbos()
    '    Try
    '        Dim PrimerCeldaRow As Integer = -1
    '        If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
    '        Dim P As New dbPagosProveedores(MySqlcon)
    '        Dim tabla As DataTable
    '        Dim TablaFull As New DataTable
    '        Dim Q As New dbDeposito(MySqlcon)
    '        TablaFull.Columns.Add("id")
    '        TablaFull.Columns.Add("Folio")
    '        TablaFull.Columns.Add("Proveedor")
    '        TablaFull.Columns.Add("Fecha")
    '        TablaFull.Columns.Add("FechaCobro")
    '        TablaFull.Columns.Add("Referencia")
    '        TablaFull.Columns.Add("Cantidad")
    '        TablaFull.Columns.Add("Tipo")
    '        TablaFull.Columns.Add("IVA")
    '        TablaFull.Columns.Add("Leyenda")
    '        TablaFull.Columns.Add("esCheque")
    '        TablaFull.Columns.Add("Estado")
    '        TablaFull.Columns.Add("numCuenta")
    '        TablaFull.Columns.Add("Banco")

    '        tabla = P.filtroAmbos(txtBuscProve.Text, fechaFormato(dtpDesde), fechaFormato(dtpHasta)).ToTable()

    '        For i As Integer = 0 To tabla.Rows.Count() - 1
    '            Dim dr As DataRow
    '            dr = TablaFull.NewRow()
    '            dr("id") = tabla.Rows(i)(0).ToString
    '            dr("Folio") = tabla.Rows(i)(1).ToString
    '            dr("Proveedor") = tabla.Rows(i)(2).ToString
    '            dr("Fecha") = tabla.Rows(i)(3).ToString
    '            dr("FechaCobro") = tabla.Rows(i)(4).ToString
    '            dr("Referencia") = tabla.Rows(i)(5).ToString
    '            dr("Cantidad") = tabla.Rows(i)(6).ToString
    '            dr("Tipo") = tabla.Rows(i)(7).ToString
    '            dr("IVA") = tabla.Rows(i)(8).ToString
    '            dr("Leyenda") = tabla.Rows(i)(9).ToString
    '            dr("esCheque") = tabla.Rows(i)(10).ToString
    '            dr("Estado") = tabla.Rows(i)(11).ToString
    '            dr("numCuenta") = Q.buscarNumeroCuenta(tabla.Rows(i)(12).ToString)
    '            dr("Banco") = Q.buscarBancoCuenta(tabla.Rows(i)(12).ToString)


    '            TablaFull.Rows.Add(dr)
    '        Next
    '        DataGridView1.DataSource = TablaFull
    '        DataGridView1.Columns(0).HeaderText = "id"
    '        DataGridView1.Columns(1).HeaderText = "Folio"
    '        DataGridView1.Columns(2).HeaderText = "Proveedor"
    '        DataGridView1.Columns(3).HeaderText = "Fecha"
    '        DataGridView1.Columns(4).HeaderText = "Fecha Cobro"
    '        DataGridView1.Columns(5).HeaderText = "Referencia"
    '        DataGridView1.Columns(6).HeaderText = "Cantidad"
    '        DataGridView1.Columns(7).HeaderText = "Tipo"
    '        DataGridView1.Columns(8).HeaderText = "IVA"
    '        DataGridView1.Columns(9).HeaderText = "Leyenda"
    '        DataGridView1.Columns(10).HeaderText = "EsCheque"
    '        DataGridView1.Columns(11).HeaderText = "Estado"
    '        DataGridView1.Columns(12).HeaderText = "N. Cuenta"
    '        DataGridView1.Columns(13).HeaderText = "Banco"

    '        DataGridView1.Columns(0).Visible = False
    '        DataGridView1.Columns(11).Visible = False
    '        DataGridView1.Columns(10).Visible = False
    '        DataGridView1.Columns(9).Visible = False
    '        DataGridView1.Columns(8).Visible = False

    '        If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try
    'End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        'Eliminar Nivel 1
        Try
            
            If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.PagoProveedoresCancelar, PermisosN.Secciones.Bancos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbPagosProveedores(MySqlcon)
                    If P.LigadoABancos(idPagoProv) Then
                        MsgBox("Para poder modificar este movimiento tiene que eliminar todos los pagos relacionados a este.", MsgBoxStyle.Information, GlobalNombreApp)
                        Exit Sub
                    End If
                    P.Eliminar(idPagoProv)
                    P.EliminarPoliza(idPagoProv)
                    P.DesligarRetiros(idPagoProv)
                    PopUp("Eliminado", 90)
                    Nuevo(True)
                    'FiltroTodos()
                    NuevaPoliza()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub dtpDesde_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDesde.ValueChanged
        Dim tmpc As Boolean = ConsultaOn
        ConsultaOn = False
        dtpHasta.Value = (Date.Parse("01/" + dtpDesde.Value.Month.ToString + "/" + dtpDesde.Value.Year.ToString).AddMonths(1)).AddDays(-1)
        ConsultaOn = tmpc
        FiltroTodos()
    End Sub

    Private Sub dtpHasta_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpHasta.ValueChanged

        If dtpDesde.Value > dtpHasta.Value Then
            dtpDesde.Value = dtpHasta.Value
        End If
        FiltroTodos()
    End Sub

    Private Sub ckbFecha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'filtros()
    End Sub


    Private Sub txtBuscProve_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscProve.TextChanged
        FiltroTodos()
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 3 Then
            e.Value = Format(e.Value, "0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub
    'Private Sub filtros()
    '    If ckbProvedor.Checked = False And ckbFecha.Checked = True Then
    '        'Fecha
    '        FiltroFecha()
    '    End If
    '    If ckbFecha.Checked = False And ckbProvedor.Checked = False Then
    '        'Todos
    '        FiltroTodos()
    '    End If
    '    If ckbFecha.Checked = False And ckbProvedor.Checked = True Then
    '        'Proveedor
    '        FiltroProv()
    '    End If
    '    If ckbFecha.Checked = True And ckbProvedor.Checked = True Then
    '        'Ambos
    '        FiltroAmbos()
    '    End If
    'End Sub

    'Private Sub ckbProvedor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbProvedor.CheckedChanged
    '    filtros()
    'End Sub

    'Private Sub btnLimpiarBusqueda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiarBusqueda.Click

    '    dtpDesde.Value = Today
    '    dtpHasta.Value = Today
    '    txtBuscProve.Text = ""
    '    ckbFecha.Checked = False
    '    ckbProvedor.Checked = False
    '    FiltroTodos()
    'End Sub

    Private Sub DataGridView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        LlenarDatos()
        btnAgregar.Enabled = True
        btnAgregar.Text = "Agregar"
        CantidadPago = Double.Parse(txtCantidad.Text)
        tienePoliza()
        btnGuardarPoliza.Text = "Modificar"
        DataGridView1.Focus()
        btnImprimirPago.Enabled = True
    End Sub
    Private Sub LlenarDatos()
        Try
            idPagoProv = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value

            Dim P As New dbPagosProveedores(MySqlcon)

            'Dim Q As New dbDeposito(MySqlcon)
            P.Buscar(idPagoProv)
            Dim Prov As New dbproveedores(P.IdProveedor, MySqlcon)
            cmbTipoPago.Text = P.Tipoo
            txtFolio.Text = P.Folioo
            RFC = Prov.RFC
            cmbProvedor.Text = Prov.Nombre
            cmbCuenta.SelectedIndex = IdsCuentas.Busca(P.Banco)
            ConsultaOn = False
            ComboBox1.SelectedIndex = IdsCuentas2.Busca(P.IdCuentaDest)
            If P.EsTraspaso = 1 Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
            ConsultaOn = True
            idProveedor = P.IdProveedor
            idCuenta = P.Banco
            txtCuentaDestinoTrans.Text = P.CuentaDestino
            txtBancoOriExTrans.Text = P.BancoOrigenEx
            cmbBancos.SelectedIndex = IdsBancos.Busca(P.IdBancoD)
            txtBancoDesExTrans.Text = P.BancoDestinoEx
            cmbMonedaTrans.SelectedIndex = IdsMonedas.Busca(P.IdMoneda)
            txtTipoCambioTran.Text = P.TipodeCambio.ToString
            ReDim Ids(-1)
            'Dim date1 As Date = Date.Parse(P.Fecha.ToString, System.Globalization.CultureInfo.InvariantCulture)
            dtpFecha.Value = CDate(P.Fecha)
            'date1 = Date.Parse(P.FechaCobro.ToString, System.Globalization.CultureInfo.InvariantCulture)
            dtpFechaCobro.Value = CDate(P.FechaCobro)
            txtReferencia.Text = P.Referencia
            txtCantidad.Text = P.Cantidad.ToString("0.00")
            cmbIVA.Text = P.IVA.ToString("0.00")
            Leyenda.Checked = P.Leyenda
            TextBox1.Text = P.IvaRet.ToString()
            TextBox2.Text = P.IEPS.ToString
            TextBox5.Text = P.ISR.ToString
            Label40.Visible = True
            Label40.Text = "Folio Interno: " + P.ID.ToString("00000")
            CheckBox1.Enabled = False
            'idPagoProv = id
            'btnGuardar.Text = "Modificar"
            btnEliminar.Enabled = True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    'Private Sub LlenarDatos2()
    '    Try
    '        'id = idPagoProv
    '        Dim P As New dbPagosProveedores(MySqlcon)
    '        Dim Q As New dbDeposito(MySqlcon)
    '        P.Buscar(idPagoProv)
    '        cmbTipoPago.Text = P.Tipoo
    '        txtFolio.Text = P.Folioo
    '        cmbProvedor.Text = P.Proveedor
    '        'txtCodigo.Text = P.nCuenta
    '        'cmbCuenta.Text = P.Banco
    '        cmbCuenta.Text = Q.buscarBancoCuenta(P.Banco)
    '        txtCodigo.Text = Q.buscarNumeroCuenta(P.Banco)
    '        Dim date1 As Date = Date.Parse(P.Fecha.ToString, System.Globalization.CultureInfo.InvariantCulture)
    '        dtpFecha.Value = date1
    '        date1 = Date.Parse(P.FechaCobro.ToString, System.Globalization.CultureInfo.InvariantCulture)
    '        dtpFechaCobro.Value = date1
    '        txtReferencia.Text = P.Referencia
    '        txtCantidad.Text = P.Cantidad
    '        cmbIVA.Text = P.IVA
    '        Leyenda.Checked = P.Leyenda
    '        'idPagoProv = id
    '        'btnGuardar.Text = "Modificar"
    '        btnEliminar.Enabled = True

    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try
    'End Sub

    Private Sub txtCargo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCargo.KeyDown
        If txtCargo.Text = "0.00" Then
            txtCargo.Text = ""
        End If
    End Sub

    Private Sub txtAbono_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAbono.KeyDown
        If txtAbono.Text = "0.00" Then
            txtAbono.Text = ""
        End If
    End Sub

    Private Sub txtCargo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCargo.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0) Or (e.KeyChar = "-" And textBox.Text.Length = 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAbono_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAbono.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0) Or (e.KeyChar = "-" And textBox.Text.Length = 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCargo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCargo.Leave
        Dim x As Double
        If txtCargo.Text = "" Then
            txtCargo.Text = "0.00"
        Else
            x = Double.Parse(txtCargo.Text)
            If x >= 2000.0 Then

            Else

            End If

            txtCargo.Text = Format(x, "0.00")
        End If
    End Sub

    Private Sub txtAbono_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAbono.Leave
        Dim x As Double
        If txtAbono.Text = "" Then
            txtAbono.Text = "0.00"
        Else
            x = Double.Parse(txtAbono.Text)
            If x >= 2000.0 Then

            Else

            End If

            txtAbono.Text = Format(x, "0.00")
        End If
    End Sub

    Private Sub txtCargo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCargo.Enter
        txtCargo.SelectAll()
    End Sub

    Private Sub txtAbono_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAbono.Enter
        txtAbono.SelectAll()
    End Sub

    Private Sub txtCargo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCargo.Click
        txtCargo.SelectAll()
    End Sub

    Private Sub txtAbono_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAbono.Click
        txtAbono.SelectAll()
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        BotonBuscar()
    End Sub
    Private Sub Botonbuscar()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            cmbProvedor.Text = B.Proveedor.Nombre.ToString()
            idProveedor = B.Proveedor.ID
            RFC = B.Proveedor.RFC
            B.Proveedor.DaPrimerCuenta()

            IdBancoProv = B.Proveedor.Cuenta.IdBanco
            TextBox1.Text = B.Proveedor.IvaRet.ToString
            TextBox2.Text = B.Proveedor.Ieps.ToString
            TextBox5.Text = B.Proveedor.ISR.ToString
            If B.Proveedor.Cuenta.Cuenta <> "" Then
                CuentaProv = B.Proveedor.Cuenta.Cuenta
            Else
                CuentaProv = B.Proveedor.Cuenta.Clabe
            End If
            If cmbTipoPago.Text.ToUpper.Contains("CHEQUE") = False Then
                txtCuentaDestinoTrans.Text = CuentaProv
                cmbBancos.SelectedIndex = IdsBancos.Busca(IdBancoProv)
            End If
            dtpFecha.Focus()
        End If
        B.Dispose()
    End Sub

    Private Sub btnNuevoProv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoProv.Click
        Dim FC As New frmProveedores(1, 0, "")
        If FC.ShowDialog() = Windows.Forms.DialogResult.OK Then
            cmbProvedor.Text = FC.Nombre
            RFC = FC.RFC
            IdProveedor = FC.IdProveedor
            Dim Prov As New dbproveedores(idProveedor, MySqlcon)
            Prov.DaPrimerCuenta()
            IdBancoProv = Prov.Cuenta.IdBanco
            TextBox1.Text = Prov.IvaRet.ToString
            TextBox2.Text = Prov.Ieps.ToString
            TextBox3.Text = Prov.ISR.ToString
            If Prov.Cuenta.Cuenta <> "" Then
                CuentaProv = Prov.Cuenta.Cuenta
            Else
                CuentaProv = Prov.Cuenta.Clabe
            End If
            If cmbTipoPago.Text.ToUpper.Contains("CHEQUE") = False Then
                txtCuentaDestinoTrans.Text = CuentaProv
                cmbBancos.SelectedIndex = IdsBancos.Busca(IdBancoProv)
            End If
        End If
    End Sub

    Private Sub cmbIVA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    'Private Sub btnBuscarCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCC.Click
    '    Dim B As New frmBuscadorCC()
    '    B.ShowDialog()
    '    If B.DialogResult = Windows.Forms.DialogResult.OK Then
    '        txtCuenta.Text = B.Cuenta.ToString()
    '        txtDesc.Text = B.descripcion.ToString()

    '        txtCargo.Focus()

    '    End If
    'End Sub

    Private Sub txtCuenta_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCuenta.Leave
        Dim aux As String
        Dim tamano As Integer


        Dim P As New dbCContables(MySqlcon)
        txtCuenta.BackColor = Color.White
        If txtCuenta.Text = " " Then
            txtCuenta.Text = ""
        End If
        If txtCuenta.Text = "" Then
        Else
            tamano = txtCuenta.Text.Length
            If txtCuenta.Text.Chars(tamano - 1) = " " Then
                aux = ""
                For i As Integer = 0 To tamano - 2
                    aux = aux + txtCuenta.Text.Chars(i)
                Next
                txtCuenta.Text = aux
            End If
            'Comprobar existencia cuenta
            If P.existeCuenta(txtCuenta.Text) = 0 Then
                'No existe la cuenta
                MsgBox("La cuenta no existe, escriba correctamente la cuenta.", MsgBoxStyle.Critical, GlobalNombreApp)
                txtCuenta.BackColor = Color.FromArgb(250, 150, 150)
                txtCuenta.Text = ""
                txtCuenta.Focus()
            Else
                'Comprobar si es de último Nivel
                P.PreUltimo(txtCuenta.Text)
                If P.UtlimoNivel(P.ID.ToString(), P.Nivel.ToString()) = 0 Then
                    txtDesc.Focus()
                Else
                    MsgBox("Debe seleccionar cuentas de último nivel.", MsgBoxStyle.Critical, GlobalNombreApp)
                    txtCuenta.BackColor = Color.FromArgb(250, 150, 150)
                    txtCuenta.Focus()
                    txtCuenta.Text = ""
                End If
            End If
        End If
    End Sub

    Private Sub txtCuenta_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCuenta.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        Else
            If e.KeyChar = Convert.ToChar(8) Then
                'que no sea backspace
            Else
                Dim aux As String

                If txtCuenta.Text = "" Then

                Else
                    If txtCuenta.Text.Length() = 5 Then
                        aux = txtCuenta.Text + " "
                        txtCuenta.Text = aux
                        txtCuenta.Select(txtCuenta.Text.Length, 0)
                    End If
                    If txtCuenta.Text.Length() = 11 Then
                        aux = txtCuenta.Text + " "
                        txtCuenta.Text = aux
                        txtCuenta.Select(txtCuenta.Text.Length, 0)
                    End If
                    If txtCuenta.Text.Length() = 17 Then
                        aux = txtCuenta.Text + " "
                        txtCuenta.Text = aux
                        txtCuenta.Select(txtCuenta.Text.Length, 0)
                    End If
                    If txtCuenta.Text.Length() = 23 Then
                        aux = txtCuenta.Text + " "
                        txtCuenta.Text = aux
                        txtCuenta.Select(txtCuenta.Text.Length, 0)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtCuenta_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCuenta.Enter
        If txtCuenta.Text = "" Then
        Else
            txtCuenta.SelectAll()
        End If
    End Sub

    Private Sub txtCuenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCuenta.Click
        If txtCuenta.Text = "" Then
        Else
            txtCuenta.SelectAll()
        End If
    End Sub

    Private Sub DataGridView2_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        'Double clic
        ' llenaDatos()
    End Sub
    Private Sub llenaDatos()
        Try
            idModificar = Integer.Parse(DataGridView2.Item(0, DataGridView2.CurrentCell.RowIndex).Value)
            cuenta = DataGridView2.Item(1, DataGridView2.CurrentCell.RowIndex).Value
            descripcion = DataGridView2.Item(2, DataGridView2.CurrentCell.RowIndex).Value
            cargo = DataGridView2.Item(3, DataGridView2.CurrentCell.RowIndex).Value
            abono = DataGridView2.Item(4, DataGridView2.CurrentCell.RowIndex).Value
            btnAgregar.Text = "Modificar"
            btnEliminar2.Enabled = True
            txtCuenta.Text = cuenta
            txtDesc.Text = descripcion
            txtCargo.Text = cargo
            txtAbono.Text = abono

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnEliminar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar2.Click
        If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim nFilas As Integer
            nFilas = DataGridView2.Rows.Count
            Dim id As Integer = 0
            For i As Integer = 0 To nFilas - 1
                id = DataGridView2.Item(0, i).Value
                If id = idModificar Then
                    i = nFilas - 1
                    DataGridView2.Rows.RemoveAt(i)
                End If
            Next
            nFilas = DataGridView2.Rows.Count
            'If nFilas = 0 Then
            '    btnGuardarPoliza.Enabled = False
            'End If
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
            txtCargo.Text = "0.00"
            txtAbono.Text = "0.00"
            btnAgregar.Text = "Agregar"
            btnEliminar2.Enabled = False
            PopUp("Eliminado", 90)
            Cuentas()
            txtCuenta.Focus()
        End If

    End Sub

    Private Sub txtPoliza_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPoliza.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtSolicitud_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSolicitud.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPoliza_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPoliza.Leave
        If txtPoliza.Text = "" Then
            Dim P As New dbPagosProveedores(MySqlcon)
            txtPoliza.Text = P.FolioPoliza()
        Else
            Dim x As String
            Dim y As Integer
            y = Integer.Parse(txtPoliza.Text)
            x = Format(y, "00000")
            txtPoliza.Text = x
        End If
        
    End Sub

    Private Sub txtSolicitud_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSolicitud.Leave
        If txtSolicitud.Text = "" Then
            txtSolicitud.Text = "00001"
        Else
            Dim x As String
            Dim y As Integer
            y = Integer.Parse(txtSolicitud.Text)
            x = Format(y, "00000")
            txtSolicitud.Text = x
        End If
        
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
        guardar(False)
    End Sub

    Private Sub NuevaPoliza()
        Dim P As New dbPagosProveedores(MySqlcon)
        Dim nFilas As Integer = 0
        btnAgregar.Text = "Agregar"
        txtCuenta.BackColor = Color.White
        txtDesc.BackColor = Color.White
        txtCuenta.Text = ""
        txtDesc.Text = ""
        'btnImprimirPago.Enabled = False
        txtPoliza.Text = P.FolioPoliza()
        txtCuenta.Text = ""
        txtDesc.Text = ""
        txtAbono.Text = "0.00"
        txtCargo.Text = "0.00"
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
        btnGuardarPoliza.Text = "Guardar"
        txtPoliza.ReadOnly = False
        txtSolicitud.ReadOnly = False
    End Sub

    Private Sub tienePoliza()
        Dim P As New dbPagosProveedores(MySqlcon)
        If P.tienePoliza(idPagoProv) > 0 Then
            'ya tiene poliza

            txtCuenta.BackColor = Color.White
            txtDesc.BackColor = Color.White
            txtCuenta.Text = ""
            txtDesc.Text = ""
            DataGridView2.DataSource = P.buscarPoliza(idPagoProv)
            DataGridView2.Columns(0).Visible = False
            DataGridView2.Columns(5).Visible = False
            DataGridView2.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            Cuentas()
            btnGuardarPoliza.Text = "Modificar"
            btnGuardarPoliza.Enabled = True
            P.buscarDatos(idPagoProv)
            txtElabora.Text = P.Elabora
            txtAutoriza.Text = P.Autoriza
            txtRegistra.Text = P.Registra
            txtSolicitud.Text = P.Solicitud
            TxtGravada1.insText(P.CantidadGravadaISR)
            TxtGravada2.insText(P.CantidadGravadaIVA)
            TxtGravada3.insText(P.CantidadGravadaIEPS)
            TxtRetenida1.insText(P.CantidadRetenidaISR)
            TxtRetenida2.insText(P.CantidadRetenidaIVA)
            TxtRetenida3.insText(P.CantidadRetenidaIEPS)
            txtPoliza.Text = P.NumPoliza
            txtPoliza.ReadOnly = True
            txtSolicitud.ReadOnly = True
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

    Private Sub txtSolicitud_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSolicitud.Enter

    End Sub

    Private Sub agregarFila()
        Dim nFilas As Integer

        nFilas = Tabla.Rows.Count
        Dim dr5 As DataRow
        dr5 = Tabla.NewRow()
        dr5("ID") = IDaux + 1.ToString()
        dr5("Cuenta") = txtCuenta.Text
        dr5("Descripción") = txtDesc.Text
        dr5("Cargo") = txtCargo.Text
        dr5("Abono") = txtAbono.Text
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



    Private Sub txtCantidad_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.DoubleClick
        txtCantidad.DeselectAll()
    End Sub

    Private Sub txtCargo_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCargo.DoubleClick
        txtCargo.DeselectAll()
    End Sub

    Private Sub txtAbono_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAbono.DoubleClick
        txtAbono.DeselectAll()
    End Sub
    'Private Sub llenarCuenta()

    '    Dim P As New dbPagosProveedores(MySqlcon)
    '    Dt = P.buscarCuenta()
    '    If Dt.Rows.Count() > 0 Then
    '        With cmbCuenta
    '            .DataSource = Dt
    '            .DisplayMember = "nombre"
    '            .ValueMember = "Numero"
    '        End With
    '    Else
    '        MsgBox("No hay cuentas de Banco registradas, favor de registrar una.", MsgBoxStyle.Critical, GlobalNombreApp)
    '    End If
    'End Sub
    'Private Sub selectedindex()
    '    If Dt.Rows.Count > 0 Then
    '        Dim i As Integer = 0
    '        i = cmbCuenta.SelectedIndex()
    '        idCuenta = Integer.Parse(Dt.Rows(i)(0).ToString())
    '        txtCodigo.Text = Dt.Rows(i)(1).ToString()
    '    End If
    'End Sub
   

    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.BancosChequePagoProv, GlobalIdSucursalDefault)
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.BancosChequePagoProv, GlobalIdSucursalDefault)
        End If
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
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.BancosChequePagoProv, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.BancosChequePagoProv, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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
        'Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
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

        'Dim V As New dbCajasMovimientos(idMovimiento, MySqlcon)
        Dim P As New dbPagosProveedores(MySqlcon) 'Modificado
        P.Buscar(idPagoProv)
        ' Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        ' V.DaTotal(idMovimiento, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
        Dim O As New dbOpciones(MySqlcon)
        Dim TotalDescuento As Double = 0
        ' Dim Vendedor As New dbVendedores(V.IdVendedor, MySqlcon)
        Dim cantidadLetra As String 'modificado
        Dim nfilas As Integer
        ImpND.Clear()
        ImpNDD.Clear()
        Dim cantidad1 As Double
        'Dim centavos As Integer
        cantidad1 = CDbl(P.Cantidad)
        'centavos = Integer.Parse((Double.Parse(P.Cantidad) - Double.Parse(cantidad1)) * 100)
        'cantidadLetra = "SON: (" + Num2Text(Double.Parse(Double.Parse(cantidad1))) + " PESOS " + Format(centavos, "00") + "/100)"
        Dim CL As New CLetras
        If cantidad1 >= 0 Then
            cantidadLetra = "SON: (" + CL.LetrasM(cantidad1, 2, GlobalIdiomaLetras) + ")"
        Else
            cantidadLetra = "SON: (MENOS " + CL.LetrasM(cantidad1 * -1, 2, GlobalIdiomaLetras) + ")"
        End If
        ImpND.Add(New NodoImpresionN("", "fechaCheque", P.Fecha, 0), "fechaCheque")
        ImpND.Add(New NodoImpresionN("", "nombreCheque", P.Proveedor, 0), "nombreCheque")
        ImpND.Add(New NodoImpresionN("", "cantidadLetra", cantidadLetra, 0), "cantidadLetra") 'falta cnvertir a letra
        ImpND.Add(New NodoImpresionN("", "cantidad", "$" + P.Cantidad, 0), "cantidad")
        ImpND.Add(New NodoImpresionN("", "concepto", P.Referencia, 0), "concepto")
        ImpND.Add(New NodoImpresionN("", "firmaChequeRecibido", P.Proveedor, 0), "firmaChequeRecibido")
        'Poliza
        CuantosRenglones = 0
        nfilas = DataGridView2.Rows.Count
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

        Posicion = 0
        NumeroPagina = 1
    End Sub
    Private Sub LlenaNodosImpresionCheque()

        'Dim V As New dbCajasMovimientos(idMovimiento, MySqlcon)
        Dim P As New dbPagosProveedores(MySqlcon) 'Modificado
        P.Buscar(idPagoProv)
        Dim Pro As New dbproveedores(P.IdProveedor, MySqlcon)
        Dim Cuenta As New dbCuentas(P.Banco, MySqlcon)
        ' Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        ' V.DaTotal(idMovimiento, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
        Dim O As New dbOpciones(MySqlcon)
        Dim TotalDescuento As Double = 0
        ' Dim Vendedor As New dbVendedores(V.IdVendedor, MySqlcon)
        Dim cantidadLetra As String 'modificado
        'Dim nfilas As Integer
        ImpND.Clear()
        ImpNDD.Clear()
        Dim cantidad1 As Double
        'Dim centavos As Integer
        cantidad1 = CDbl(P.Cantidad)
        'centavos = Integer.Parse((Double.Parse(P.Cantidad) - Double.Parse(cantidad1)) * 100)
        Dim CL As New CLetras
        If cantidad1 >= 0 Then
            cantidadLetra = "SON: (" + CL.LetrasM(cantidad1, 2, GlobalIdiomaLetras) + ")"
        Else
            cantidadLetra = "SON: (MENOS " + CL.LetrasM(cantidad1 * -1, 2, GlobalIdiomaLetras) + ")"
        End If

        ImpND.Add(New NodoImpresionN("", "fechaCheque", P.Fecha, 0), "fechaCheque")
        ImpND.Add(New NodoImpresionN("", "nombreCheque", Pro.Nombre, 0), "nombreCheque")
        ImpND.Add(New NodoImpresionN("", "cantidadLetra", cantidadLetra, 0), "cantidadLetra") 'falta cnvertir a letra
        ImpND.Add(New NodoImpresionN("", "cantidad", P.Cantidad.ToString(O._formatoTotal), 0), "cantidad")

        ImpND.Add(New NodoImpresionN("", "nocuenta", Cuenta.Numero, 0), "nocuenta")
        ImpND.Add(New NodoImpresionN("", "bancocuenta", Cuenta.BancoNombre, 0), "bancocuenta")
        ImpND.Add(New NodoImpresionN("", "folio", P.Folioo, 0), "folio")
        ImpND.Add(New NodoImpresionN("", "fechacobro", P.FechaCobro, 0), "fechacobro")
        ImpND.Add(New NodoImpresionN("", "referencia", P.Referencia, 0), "referencia")
        ImpND.Add(New NodoImpresionN("", "ivapor", P.IVA.ToString(O._formatoIva), 0), "ivapor")
        ImpND.Add(New NodoImpresionN("", "ivaretpor", P.IvaRet.ToString(O._formatoIva), 0), "ivaretpor")
        ImpND.Add(New NodoImpresionN("", "iepspor", P.IEPS.ToString(O._formatoIva), 0), "iepspor")
        ImpND.Add(New NodoImpresionN("", "cuentadest", P.CuentaDestino, 0), "cuentadest")
        ImpND.Add(New NodoImpresionN("", "bancodest", P.DaNombreBanco(P.IdBancoD), 0), "bancodest")
        ImpND.Add(New NodoImpresionN("", "bancodestex", P.BancoDestinoEx, 0), "bancodestex")
        ImpND.Add(New NodoImpresionN("", "moneda", P.DaNombreMoneda(P.IdMoneda), 0), "moneda")
        ImpND.Add(New NodoImpresionN("", "tipodecambio", P.TipodeCambio.ToString("0.00"), 0), "tipodecambio")
        If P.EsTraspaso = 1 Then
            ImpND.Add(New NodoImpresionN("", "estraspaso", "ES TRASPASO", 0), "estraspaso")
            Dim Cuenta2 As New dbCuentas(P.IdCuentaDest, MySqlcon)
            ImpND.Add(New NodoImpresionN("", "cuentadesttras", Cuenta2.Numero + " " + Cuenta2.BancoNombre, 0), "cuentadesttras")
        Else
            ImpND.Add(New NodoImpresionN("", "estraspaso", "", 0), "estraspaso")
            ImpND.Add(New NodoImpresionN("", "cuentadesttras", "", 0), "cuentadesttras")
        End If
        If P.Leyenda Then
            ImpND.Add(New NodoImpresionN("", "leyendacheque", "PARA ABONO EN CUENTA DE BENEFICIARIO", 0), "leyendacheque")
        Else
            ImpND.Add(New NodoImpresionN("", "leyendacheque", "", 0), "leyendacheque")
        End If
        Dim CantidadDesglosada As Double
        '(cantidad/(1+(tblpagoprov.iva-tblpagoprov.ivaret+tblpagoprov.ieps)/100)
        CantidadDesglosada = P.Cantidad / (1 + (P.IVA - P.IvaRet + P.IEPS) / 100)
        ImpND.Add(New NodoImpresionN("", "ivaimp", Format(CantidadDesglosada * P.IVA / 100, O._formatoIva), 0), "ivaimp")
        ImpND.Add(New NodoImpresionN("", "ivaretimp", Format(CantidadDesglosada * P.IvaRet / 100, O._formatoIva), 0), "ivaretimp")
        ImpND.Add(New NodoImpresionN("", "iepsimp", Format(CantidadDesglosada * P.IEPS / 100, O._formatoIva), 0), "iepsimp")
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
        Dim P As New dbPagosProveedores(MySqlcon) 'Modificado
        P.Buscar(idPagoProv) 'Modificado
        Dim RutaPDF As String

        Dim Archivos As New dbSucursalesArchivos

        RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(P.FechaCobro, "yyyy") + "\") 'Modificado
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(P.FechaCobro, "yyyy") + "\" + Format(P.FechaCobro, "MM") + "\") 'Modificado
        RutaPDF = RutaPDF + "\" + Format(P.FechaCobro, "yyyy") + "\" + Format(P.FechaCobro, "MM") 'Modificado
        If Cheque Then
            PrintDocument1.DocumentName = "Cheque Pago Proveedor - " + P.Proveedor + " " + P.Tipoo + " " + P.Folioo 'Modificado
        Else
            PrintDocument1.DocumentName = "Poliza Pago Proveedor - " + P.Proveedor + " " + P.Tipoo + " " + P.Folioo 'Modificado
        End If

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
        If Cheque Then
            LlenaNodosImpresionCheque()
        Else
            LlenaNodosImpresion()
        End If

        If TipoImpresora = 0 Then
            If Cheque Then
                LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.BancosChequePagoProv) 'Modificado
            Else
                LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.ContabilidadPolizaEgresos) 'Modificado
            End If

        Else
            If Cheque Then
                LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.BancosChequePagoProv) 'Modificado
            Else
                LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.ContabilidadPolizaEgresos) 'Modificado
            End If
        End If
        PrintDocument1.Print()
 
    End Sub

    Public Function Num2Text(ByVal value As Double) As String
        Select Case value
            Case 0 : Num2Text = "CERO"
            Case 1 : Num2Text = "UN"
            Case 2 : Num2Text = "DOS"
            Case 3 : Num2Text = "TRES"
            Case 4 : Num2Text = "CUATRO"
            Case 5 : Num2Text = "CINCO"
            Case 6 : Num2Text = "SEIS"
            Case 7 : Num2Text = "SIETE"
            Case 8 : Num2Text = "OCHO"
            Case 9 : Num2Text = "NUEVE"
            Case 10 : Num2Text = "DIEZ"
            Case 11 : Num2Text = "ONCE"
            Case 12 : Num2Text = "DOCE"
            Case 13 : Num2Text = "TRECE"
            Case 14 : Num2Text = "CATORCE"
            Case 15 : Num2Text = "QUINCE"
            Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
            Case 20 : Num2Text = "VEINTE"
            Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
            Case 30 : Num2Text = "TREINTA"
            Case 40 : Num2Text = "CUARENTA"
            Case 50 : Num2Text = "CINCUENTA"
            Case 60 : Num2Text = "SESENTA"
            Case 70 : Num2Text = "SETENTA"
            Case 80 : Num2Text = "OCHENTA"
            Case 90 : Num2Text = "NOVENTA"
            Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
            Case 100 : Num2Text = "CIEN"
            Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
            Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
            Case 500 : Num2Text = "QUINIENTOS"
            Case 700 : Num2Text = "SETECIENTOS"
            Case 900 : Num2Text = "NOVECIENTOS"
            Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
            Case 1000 : Num2Text = "MIL"
            Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
            Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
            Case 1000000 : Num2Text = "UN MILLON"
            Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
            Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES "
                If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : Num2Text = "UN BILLON"
            Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
        End Select
    End Function

    Private Sub imprimirPoliza()
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""


        'If 'btnGuardar.Text = "Guardar" Then
        '    NoErrores = False
        '    MensajeError += vbCrLf + "El pago no esta guardado, guardalo antes de imprimir."


        'End If
        'If btnGuardarPoliza.Text = "Guardar" Then
        '    NoErrores = False
        '    MensajeError += vbCrLf + "El pago no tiene poliza o no esta guardada, guarda la póliza antes de imprimir."

        'End If

        If NoErrores = True Then
            'LlenarDatos2()
            btnAgregar.Enabled = True
            CantidadPago = Double.Parse(txtCantidad.Text)
            tienePoliza()
            Imprimir()
        End If


        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

        End If
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        imprimirPoliza()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""

        'If btnGuardar.Text = "Guardar" Then
        '    NoErrores = False
        '    MensajeError += vbCrLf + "El pago no esta guardado, guardalo antes de imprimir."

        'End If
        If btnGuardarPoliza.Text = "Guardar" Then
            NoErrores = False
            MensajeError += vbCrLf + "El pago no tiene poliza o no esta guardada, guarda la póliza antes de imprimir."

        End If

        If NoErrores = True Then
            'LlenarDatos2()
            btnAgregar.Enabled = True
            CantidadPago = Double.Parse(txtCantidad.Text)
            tienePoliza()
            Imprimir2()
        End If


        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

        End If
    End Sub

    Private Sub Imprimir2()
        Dim en As New Encriptador
        Dim P As New dbPagosProveedores(MySqlcon) 'Modificado
        P.Buscar(idPagoProv) 'Modificado
        Dim RutaPDF As String

        Dim Archivos As New dbSucursalesArchivos

        RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(P.FechaCobro, "yyyy") + "\") 'Modificado
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(P.FechaCobro, "yyyy") + "\" + Format(P.FechaCobro, "MM") + "\") 'Modificado
        RutaPDF = RutaPDF + "\" + Format(P.FechaCobro, "yyyy") + "\" + Format(P.FechaCobro, "MM") 'Modificado

        PrintDocument2.DocumentName = "Retencion - " + P.Proveedor + " " + P.Tipoo + " " + P.Folioo 'Modificado
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
        PrintDocument2.PrinterSettings.PrinterName = Impresora
        LlenaNodosImpresion2()
        'If TipoImpresora = 0 Then
        '    LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.BancosRetencion) 'Modificado
        'Else
        '    LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.BancosRetencion) 'Modificado
        'End If
        PrintDocument2.Print()

    End Sub
    Private Sub LlenaNodosImpresion2()

        Dim P As New dbPagosProveedores(MySqlcon) 'Modificado
        Dim Suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Dim Prov As New dbproveedores(idProveedor, MySqlcon)
        P.Buscar(idPagoProv)
        Dim mesI As String
        Dim mesF As String
        Dim anio As String
        'Dim rfc As String
        'Dim rfcEmpresa As String
        'Dim nombreEmpresa As String
        'rfcEmpresa = s
        'n'ombreEmpresa = P.nombre()
        mesI = dtpFecha.Value.Date.Month.ToString()
        mesF = dtpFechaCobro.Value.Date.Month.ToString()
        anio = dtpFechaCobro.Value.Date.Year.ToString()
        'rfc = P.ConsultaRFC(cmbProvedor.Text)
        ImpND.Clear()
        ImpND.Add(New NodoImpresionN("", "mesInicial", mesI, 0), "mesInicial")
        ImpND.Add(New NodoImpresionN("", "mesFinal", mesF, 0), "mesFinal")
        ImpND.Add(New NodoImpresionN("", "ejercico", anio, 0), "ejercico")
        ImpND.Add(New NodoImpresionN("", "rfc", Prov.RFC, 0), "rfc")
        ImpND.Add(New NodoImpresionN("", "curp", " ", 0), "curp") 'CURP auxiliar
        ImpND.Add(New NodoImpresionN("", "nombre", P.Proveedor, 0), "nombre")
        ImpND.Add(New NodoImpresionN("", "CGISR", "$" + TxtGravada1.obText, 0), "CGISR")
        ImpND.Add(New NodoImpresionN("", "CGIVA", "$" + TxtGravada2.obText, 0), "CGIVA")
        ImpND.Add(New NodoImpresionN("", "CGIEPS", "$" + TxtGravada3.obText, 0), "CGIEPS")
        ImpND.Add(New NodoImpresionN("", "CRISR", "$" + TxtRetenida1.obText, 0), "CRISR")
        ImpND.Add(New NodoImpresionN("", "CRIVA", "$" + TxtRetenida2.obText, 0), "CRIVA")
        ImpND.Add(New NodoImpresionN("", "CRIEPS", "$" + TxtRetenida3.obText, 0), "CRIEPS") 'AQUI ME QUEDE
        'FALTA VOLVER A EJECUTAR EL SCRIPT P YA LO PUSE VISIBLE
        'TERMINAR ESTO Y PROBAR LA BUSQUEDA DE RFC
        'Datos del retenedor ??
        'If rfcEmpresa <> "" Then
        ImpND.Add(New NodoImpresionN("", "retenedorRFC", Suc.RFC, 0), "retenedorRFC")
        'Else
        'ImpND.Add(New NodoImpresionN("", "retenedorRFC", " ", 0), "retenedorRFC")
        'End If
        'I'f nombreEmpresa <> "" Then
        ImpND.Add(New NodoImpresionN("", "retenedorNombre", Suc.Nombre, 0), "retenedorNombre")
        'Else
        'ImpND.Add(New NodoImpresionN("", "retenedorNombre", " ", 0), "retenedorNombre")
        'End If

        ImpND.Add(New NodoImpresionN("", "retenedorRepresentante", txtAutoriza.Text, 0), "retenedorRepresentante")
        ImpND.Add(New NodoImpresionN("", "retenedorRFCRepresentante", " ", 0), "retenedorRFCRepresentante")
        'frm arriba es provisional

        ImpND.Add(New NodoImpresionN("", "firmaRetenedor", txtAutoriza.Text, 0), "firmaRetenedor")
        ImpND.Add(New NodoImpresionN("", "firmaRecibido", cmbProvedor.Text, 0), "firmaRecibido")

        Posicion = 0
        NumeroPagina = 1
    End Sub
    Private Sub DibujaPaginaN2(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        'If TipoImpresora = 0 Then
        '    ImpDb.DaZonaDetalles(TiposDocumentos.BancosRetencion, GlobalIdSucursalDefault)
        'Else
        '    ImpDb.DaZonaDetalles(TiposDocumentos.BancosRetencion, GlobalIdSucursalDefault)
        'End If
        If TipoImpresora = 0 Then
            DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujo(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub

    Private Sub PrintDocument2_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage
        DibujaPaginaN2(e.Graphics)
        If MasPaginas = True Or NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
        End If

        e.HasMorePages = MasPaginas
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""


       '
        'If btnGuardarPoliza.Text = "Guardar" Then
        '    NoErrores = False
        '    MensajeError += vbCrLf + "El pago no tiene poliza o no esta guardada, guarda la póliza antes de imprimir."

        'End If

        If NoErrores = True Then
            'LlenarDatos2()
            btnAgregar.Enabled = True
            CantidadPago = Double.Parse(txtCantidad.Text)
            tienePoliza()
            'Imprimir3()
        End If


        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

        End If
    End Sub

    'Private Sub PrintDocument3_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument3.PrintPage
    '    DibujaPaginaN3(e.Graphics)
    '    If MasPaginas = True Or NumeroPagina > 2 Then
    '        e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
    '    End If

    '    e.HasMorePages = MasPaginas
    'End Sub
    'Private Sub Imprimir3()
    '    Dim en As New Encriptador
    '    Dim P As New dbPagosProveedores(MySqlcon) 'Modificado
    '    P.Buscar(idPagoProv) 'Modificado
    '    Dim RutaPDF As String

    '    Dim Archivos As New dbSucursalesArchivos

    '    RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, True)
    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(P.FechaCobro, "yyyy") + "\") 'Modificado
    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(P.FechaCobro, "yyyy") + "\" + Format(P.FechaCobro, "MM") + "\") 'Modificado
    '    RutaPDF = RutaPDF + "\" + Format(P.FechaCobro, "yyyy") + "\" + Format(P.FechaCobro, "MM") 'Modificado

    '    PrintDocument3.DocumentName = "Solicitud - " + P.Proveedor + " " + P.Tipoo + " " + P.Folioo 'Modificado
    '    Dim SA As New dbSucursalesArchivos
    '    Dim Impresora As String
    '    Impresora = SA.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.CajasMovimientos) 'Modificado
    '    TipoImpresora = SA.TipoImpresora



    '    'obj.WriteSettings()
    '    If Impresora = "Bullzip PDF Printer" Then
    '        Dim obj As New Bullzip.PdfWriter.PdfSettings
    '        obj.Init()
    '        obj.PrinterName = Impresora
    '        obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
    '        obj.SetValue("ShowSettings", "never")
    '        obj.SetValue("ShowPDF", "yes")
    '        obj.SetValue("ShowSaveAS", "nofile")
    '        obj.SetValue("ConfirmOverwrite", "no")
    '        obj.SetValue("Target", "printer")
    '        obj.WriteSettings()
    '    End If
    '    PrintDocument3.PrinterSettings.PrinterName = Impresora
    '    LlenaNodosImpresion3()
    '    If TipoImpresora = 0 Then
    '        LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.BancosSolicitud) 'Modificado
    '    Else
    '        LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.BancosSolicitud) 'Modificado
    '    End If
    '    PrintDocument3.Print()

    'End Sub
    Private Sub LlenaNodosImpresion3()

        Dim P As New dbPagosProveedores(MySqlcon) 'Modificado
        Dim Suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        P.Buscar(idPagoProv)
        Dim dia As String
        Dim mes As String
        Dim anio As String
        Dim costo As Double
        Dim iva As Double
        Dim formato As String = "SOLADSERV"
        Dim areaSolicitante As String = "DEPTO. DE SERVICIOS ADMINISTRATIVOS"
        iva = (Double.Parse(txtCantidad.Text) * Double.Parse(cmbIVA.Text)) / 100
        costo = Double.Parse(txtCantidad.Text) - iva
        dia = dtpFecha.Value.Date.Day.ToString()
        mes = dtpFecha.Value.Date.Month.ToString()
        anio = dtpFecha.Value.Date.Year.ToString()

        ImpND.Clear()
        ImpND.Add(New NodoImpresionN("", "formatoClave", formato, 0), "formatoClave") 'Checar
        ImpND.Add(New NodoImpresionN("", "numero", txtPoliza.Text, 0), "numero")
        ImpND.Add(New NodoImpresionN("", "dia", dia, 0), "dia")
        ImpND.Add(New NodoImpresionN("", "mes", mes, 0), "mes")
        ImpND.Add(New NodoImpresionN("", "anio", anio, 0), "anio")
        ImpND.Add(New NodoImpresionN("", "URClave", Suc.RFC, 0), "URClave") 'clave? rfc?
        ImpND.Add(New NodoImpresionN("", "URDenominacion", Suc.Nombre, 0), "URDenominacion")
        ImpND.Add(New NodoImpresionN("", "solicitante", areaSolicitante, 0), "solicitante") 'area solicitante?
        ImpND.Add(New NodoImpresionN("", "descripcion", txtReferencia.Text, 0), "descripcion")
        ImpND.Add(New NodoImpresionN("", "proveedorNombre", cmbProvedor.Text, 0), "proveedorNombre")
        ImpND.Add(New NodoImpresionN("", "proveedorRFC", P.ConsultaRFC(cmbProvedor.Text), 0), "proveedorRFC")
        ImpND.Add(New NodoImpresionN("", "proveedorDomicilio", P.Consultadireccion(cmbProvedor.Text), 0), "proveedorDomicilio")
        ImpND.Add(New NodoImpresionN("", "partida", txtSolicitud.Text, 0), "partida")
        ImpND.Add(New NodoImpresionN("", "formaPago", cmbTipoPago.Text + " " + txtFolio.Text, 0), "formaPago") 'AQUI ME QUEDE
        ImpND.Add(New NodoImpresionN("", "costo", "$" + Format(costo, "0.00"), 0), "costo")
        ImpND.Add(New NodoImpresionN("", "porcientoIVA", cmbIVA.Text, 0), "porcientoIVA")
        ImpND.Add(New NodoImpresionN("", "importeIVA", "$" + Format(iva, "0.00"), 0), "importeIVA")
        ImpND.Add(New NodoImpresionN("", "importeTotal", "$" + txtCantidad.Text, 0), "importeTotal")

        ImpND.Add(New NodoImpresionN("", "firmaProveedor", cmbProvedor.Text, 0), "firmaProveedor")
        If txtRegistra.Text <> "" Then
            ImpND.Add(New NodoImpresionN("", "firmaServicios", txtRegistra.Text, 0), "firmaServicios")
        Else
            ImpND.Add(New NodoImpresionN("", "firmaServicios", " ", 0), "firmaServicios")
        End If
        If txtAutoriza.Text <> "" Then
            ImpND.Add(New NodoImpresionN("", "firmaTitular", txtAutoriza.Text, 0), "firmaTitular")
        Else
            ImpND.Add(New NodoImpresionN("", "firmaTitular", " ", 0), "firmaTitular")
        End If
      
        Posicion = 0
        NumeroPagina = 1
    End Sub
    'Private Sub DibujaPaginaN3(ByRef e As System.Drawing.Graphics)
    '    Dim ImpDb As New dbImpresionesN(MySqlcon)
    '    If TipoImpresora = 0 Then
    '        ImpDb.DaZonaDetalles(TiposDocumentos.BancosSolicitud, GlobalIdSucursalDefault)
    '    Else
    '        ImpDb.DaZonaDetalles(TiposDocumentos.BancosSolicitud, GlobalIdSucursalDefault)
    '    End If
    '    If TipoImpresora = 0 Then
    '        DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
    '    Else
    '        DibujaPaginaFlujo(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
    '    End If

    'End Sub

    Private Sub guardar(pConCompro As Boolean)
        Dim P As New dbPagosProveedores(MySqlcon)
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""
        Dim auxFolio As String = ""
        Dim auxCantidad As Double = 0.0
        Dim auxtot As Double = 0.0
        Dim nFilas As Integer = 0
        txtPoliza.BackColor = Color.White
        txtSolicitud.BackColor = Color.White
        cmbTipoPago.BackColor = Color.White
        txtFolio.BackColor = Color.White
        txtReferencia.BackColor = Color.White
        cmbProvedor.BackColor = Color.White


        nFilas = DataGridView2.Rows.Count

        'If nFilas > 0 Then
        '    If lblDiferencia.Text <> "0.00" Then
        '        NoErrores = False
        '        MensajeError += vbCrLf + "La poliza no esta balanceada."

        '    End If

        '    If Double.Parse(txtCantidad.Text) <> Double.Parse(lblCargos.Text) Or Double.Parse(txtCantidad.Text) <> Double.Parse(lblAbonos.Text) Then
        '        NoErrores = False
        '        MensajeError += vbCrLf + "El total de la póliza no coincide con el pago a proveedor."
        '        'txtCo.BackColor = Color.FromArgb(250, 150, 150)
        '    End If
        'End If
        If txtCantidad.Text = "" Or IsNumeric(txtCantidad.Text) = False Then
            NoErrores = False
            MensajeError += vbCrLf + "La cantidad debe ser un valor numérico."
        End If

        If idProveedor = 0 Then
            NoErrores = False
            MensajeError += vbCrLf + "Debe indicar un proveedor."
            'txtCodigo.BackColor = Color.FromArgb(250, 150, 150)
        End If

        'If P.Existe(cmbProvedor.Text) = 0 Then
        '    NoErrores = False
        '    MensajeError += vbCrLf + " Debe indicar un Proveedor Registrado."
        '    cmbProvedor.BackColor = Color.FromArgb(250, 150, 150)
        'End If

        If cmbTipoPago.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar un tipo de pago."
            cmbTipoPago.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtFolio.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar un número de folio."
            txtFolio.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtReferencia.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar una referencia al pago."
            txtReferencia.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If cmbProvedor.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe seleccionar un proveedor"
            cmbProvedor.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.PagoProveedoresAlta, PermisosN.Secciones.Bancos) = False Then
            NoErrores = False
            MensajeError += " No tiene permiso para realizar esta operación."
        End If
        If IsNumeric(txtTipoCambioTran.Text) = False Then
            NoErrores = False
            MensajeError += " El tipo de cambio debe ser un valor numérico."
        End If
        If TextBox1.Text = "" Then TextBox1.Text = "0"
        If IsNumeric(TextBox1.Text) = False Then
            NoErrores = False
            MensajeError += " El IVA retenido debe ser un valor numérico."
        End If
        If TextBox2.Text = "" Then TextBox2.Text = "0"
        If IsNumeric(TextBox2.Text) = False Then
            NoErrores = False
            MensajeError += " El IEPS retenido debe ser un valor numérico."
        End If
        If TextBox5.Text = "" Then TextBox5.Text = "0"
        If IsNumeric(TextBox5.Text) = False Then
            NoErrores = False
            MensajeError += " El ISR retenido debe ser un valor numérico."
        End If
        Dim EsTras As Byte
        If CheckBox1.Checked = True Then
            EsTras = 1
            If IdsCuentas.Valor(cmbCuenta.SelectedIndex) = IdsCuentas2.Valor(ComboBox1.SelectedIndex) Then
                NoErrores = False
                MensajeError += " No se puede hacer un traspaso entre la misma cuenta."
            End If
        Else
            EsTras = 0
        End If
        If NoErrores = True Then
            If btnGuardarPoliza.Text = "Guardar" Then

                If P.folioPolizaRepetida(txtPoliza.Text) > 0 Then

                    MsgBox("Ya existe una póliza con la misma clave, favor de cambiarla.", MsgBoxStyle.Critical, GlobalNombreApp)
                    txtPoliza.BackColor = Color.FromArgb(250, 150, 150)
                    NoErrores = False
                End If
                'Else
                If P.folioSolicitudRepetida(txtPoliza.Text, txtSolicitud.Text) > 0 Then
                    'NoErrores = False
                    MsgBox("Ya existe el numero de solicitud asignado a la misma póliza, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                    txtSolicitud.BackColor = Color.FromArgb(250, 150, 150)
                    'Else
                    'guardar 
                    NoErrores = False
                End If
                If NoErrores = True Then
                    Dim fecha As String = dtpFecha.Value.ToString("yyyy/MM/dd")
                    Dim fecha2 As String = dtpFechaCobro.Value.ToString("yyyy/MM/dd")
                    Dim leyenda2 As Integer
                    If Leyenda.Checked = True Then
                        leyenda2 = 1
                    Else
                        leyenda2 = 0
                    End If
                    
                    idPagoProv = P.Guardar(cmbTipoPago.Text, txtFolio.Text, idProveedor.ToString, fecha, fecha2, txtReferencia.Text, txtCantidad.Text, cmbIVA.Text, leyenda2, esCheque, "Activo", IdsCuentas.Valor(cmbCuenta.SelectedIndex).ToString, txtCuentaDestinoTrans.Text, IdsBancos.Valor(cmbBancos.SelectedIndex), txtBancoDesExTrans.Text, IdsMonedas.Valor(cmbMonedaTrans.SelectedIndex), CDbl(txtTipoCambioTran.Text), txtBancoOriExTrans.Text, CDbl(TextBox1.Text), CDbl(TextBox2.Text), CDbl(TextBox5.Text), EsTras, IdsCuentas2.Valor(ComboBox1.SelectedIndex))
                    If Intedrado = 1 And FacturasAfectadas <> "" And TipoLigue = 1 Then
                        P.LigarAComprasPagos(idPagoProv, WhereStr)
                    End If
                    If Intedrado = 1 And FacturasAfectadas <> "" And TipoLigue = 2 Then
                        P.LigarRetiro(WhereStr, idPagoProv)
                    End If
                    If Ids.Length > 0 Then
                        For Each Idc As Integer In Ids
                            P.LigarRetiro(Idc.ToString, idPagoProv)
                        Next
                    End If
                    ' btnGuardar.Text = "Modificar"
                    'filtros(
                    'GuardarPoliza

                    For i As Integer = 0 To nFilas - 1
                        P.guardarPoliza(idPagoProv, txtPoliza.Text, DataGridView2.Item(0, i).Value, DataGridView2.Item(1, i).Value, DataGridView2.Item(2, i).Value, DataGridView2.Item(3, i).Value, DataGridView2.Item(4, i).Value, txtElabora.Text, txtAutoriza.Text, txtRegistra.Text, txtSolicitud.Text, TxtGravada1.obText, TxtGravada2.obText, TxtGravada3.obText, TxtRetenida1.obText, TxtRetenida2.obText, TxtRetenida3.obText)
                    Next
                    'btnGuardarPoliza.Text = "Modificar"

                    txtPoliza.ReadOnly = True
                    txtSolicitud.ReadOnly = True
                    If pConCompro Then
                        Dim f As New frmBancosAddComprobantes(idPagoProv, RFC, IdsMonedas.Valor(cmbMonedaTrans.SelectedIndex))
                        f.ShowDialog()
                        f.Dispose()
                    End If
                    If MsgBox("¿Desea Imprimir el cheque?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Cheque = True
                        imprimirPoliza()
                    End If
                    GeneraPoliza(idPagoProv)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = False Then
                        PopUp("Pago Guardado", 90)
                    End If
                    Nuevo(True)
                    NuevaPoliza()
                    If Intedrado = 1 Then Me.DialogResult = Windows.Forms.DialogResult.OK
                    '  selectedindex()
                    'fin guardar
                    '    End If
                End If
                '------------------------
            Else 'Modificar
                If P.LigadoABancos(idPagoProv) Then
                    MsgBox("Para poder modificar este movimiento tiene que eliminar todos los pagos relacionados a este.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim fecha As String = dtpFecha.Value.ToString("yyyy/MM/dd")
                    Dim fecha2 As String = dtpFechaCobro.Value.ToString("yyyy/MM/dd")
                    Dim leyenda2 As Integer
                    'Dim aux As String
                    If Leyenda.Checked = True Then
                        leyenda2 = 1
                    Else
                        leyenda2 = 0
                    End If
                    P.Modificar(idPagoProv, cmbTipoPago.Text, txtFolio.Text, idProveedor.ToString, fecha, fecha2, txtReferencia.Text, txtCantidad.Text, cmbIVA.Text, leyenda2, esCheque, "Activo", IdsCuentas.Valor(cmbCuenta.SelectedIndex).ToString, txtCuentaDestinoTrans.Text, IdsBancos.Valor(cmbBancos.SelectedIndex), txtBancoDesExTrans.Text, IdsMonedas.Valor(cmbMonedaTrans.SelectedIndex), CDbl(txtTipoCambioTran.Text), txtBancoOriExTrans.Text, CDbl(TextBox1.Text), CDbl(TextBox2.Text), CDbl(TextBox5.Text), IdsCuentas2.Valor(ComboBox1.SelectedIndex), EsTras)
                    '  btnGuardar.Text = "Modificar"
                    'filtros()
                    'poliza
                    If Ids.Length > 0 Then
                        For Each Idc As Integer In Ids
                            P.LigarRetiro(Idc.ToString, idPagoProv)
                        Next
                    End If
                    P.EliminarPoliza(idPagoProv)
                    For i As Integer = 0 To nFilas - 1
                        P.guardarPoliza(idPagoProv, txtPoliza.Text, DataGridView2.Item(0, i).Value, DataGridView2.Item(1, i).Value, DataGridView2.Item(2, i).Value, DataGridView2.Item(3, i).Value, DataGridView2.Item(4, i).Value, txtElabora.Text, txtAutoriza.Text, txtRegistra.Text, txtSolicitud.Text, TxtGravada1.obText, TxtGravada2.obText, TxtGravada3.obText, TxtRetenida1.obText, TxtRetenida2.obText, TxtRetenida3.obText)
                    Next
                    btnGuardarPoliza.Text = "Modificar"
                    If pConCompro Then
                        Dim f As New frmBancosAddComprobantes(idPagoProv, RFC, IdsMonedas.Valor(cmbMonedaTrans.SelectedIndex))
                        f.ShowDialog()
                        f.Dispose()
                    End If
                    PopUp("Pago Modificado", 90)
                    'aux = txtCodigo.Text
                    If MsgBox("¿Desea Imprimir el cheque?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Cheque = True
                        imprimirPoliza()
                    End If

                    Nuevo(True)
                    NuevaPoliza()
                    'txtCodigo.Text = aux
                End If
            End If
        End If


        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
        Else
            If Intedrado = 1 Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        If Intedrado = 1 Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Else
            Me.Close()
        End If

    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        llenaDatos()
    End Sub

    Private Sub btnLimpiarRenglon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiarRenglon.Click
        txtCuenta.Text = ""
        txtDesc.Text = ""
        ' txtAbono.
        txtAbono.Text = "0.00"
        txtCargo.Text = "0.00"
        btnAgregar.Text = "Agregar"
        btnEliminar2.Enabled = False
        txtCuenta.Focus()
    End Sub

    Private Sub grpPagos_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpPagos.Enter
   
    End Sub

    Private Sub btnImprimirPago_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimirPago.Click
        Dim P As New dbPagosProveedores(MySqlcon)
        P.Buscar(idPagoProv)
        idCuenta = Integer.Parse(P.Banco)
        'idPagoProv = id
        Dim Q1 As New dbDeposito(MySqlcon)
        Dim Suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Dim q As New dbPagosProveedores(MySqlcon)
        'Dim nombreEmpresa As String = q.nombre()
        'Dim rfc As String = q.RFC()
        'Dim direccion As String = q.Calles(nombreEmpresa)
        'D() 'im direccion2 As String = q.direccion2(nombreEmpresa)
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument


        Rep = New rptPagoProveedorSolo
        'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
        'Rep.SetDataSource(P.Reporte1())
        Rep.SetParameterValue("encabezado", Suc.Nombre)
        Rep.SetParameterValue("direccion", Suc.Direccion + " " + Suc.NoExterior + " " + Suc.NoInterior)
        Rep.SetParameterValue("rfc", Suc.RFC)
        Rep.SetParameterValue("direccion2", Suc.Ciudad + " " + Suc.Estado + " " + Suc.CP)

        Rep.SetParameterValue("tipoPago", P.Tipoo)
        Rep.SetParameterValue("folio", P.Folioo)
        Rep.SetParameterValue("fecha1", P.Fecha.ToString)
        Rep.SetParameterValue("fecha2", P.FechaCobro)
        Rep.SetParameterValue("noCuenta", Q1.buscarNumeroCuenta(P.Banco))
        Rep.SetParameterValue("bancoCuenta", Q1.buscarBancoCuenta(P.Banco))
        Rep.SetParameterValue("proveedor", P.Proveedor)

        Rep.SetParameterValue("referencia", P.Referencia)
        Rep.SetParameterValue("cantidad", P.Cantidad)
        Rep.SetParameterValue("iva", P.IVA + "%")

        If P.Leyenda Then
            Rep.SetParameterValue("leyenda", "Para abono en cuenta del beneficiario.")
        Else
            Rep.SetParameterValue("leyenda", " ")
        End If
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub btnImprimirCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimirCheque.Click
        Cheque = True
        imprimirPoliza()
        Cheque = False
    End Sub

    Private Sub cmbProvedor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProvedor.TextChanged

    End Sub

    Private Sub dtpFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha.ValueChanged
        dtpFechaCobro.Value = dtpFecha.Value
    End Sub

    Private Sub cmbIVA_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbIVA.KeyDown
        If e.KeyCode = Keys.Enter Then
            guardar(False)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Forma As New frmBuscaDocumentoVenta(0, True, 3, GlobalIdSucursalDefault, 0, False, False, False, 1, True, cmbProvedor.Text, True)
        If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Ids = Forma.id
            If txtReferencia.Text = "" Then txtReferencia.Text = "COMPRAS:"
            For Each Str As String In Forma.Folios
                txtReferencia.Text += " " + Str
            Next
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If idPagoProv <> 0 Then
            Dim f As New frmComprasConsulta(ModosDeBusqueda.Principal, idPagoProv, 0)
            f.ShowDialog()
            f.Dispose()
        Else
            MsgBox("Debe seleccionar un pago.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub GeneraPoliza(pIdDeposito As Integer)
        Try
            Dim Op As New dbOpciones(MySqlcon)
            If Op.IntegrarContabilidad = 1 And GlobalconIntegracion Then
                Dim M As New dbContabilidadMascaras(MySqlcon)
                Dim V As New dbPagosProveedores(MySqlcon)
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
                cuantas = M.CuantasHay(5, Canceladas, credito)
                If cuantas > 0 Then
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(5, Canceladas, credito)
                    Else
                        Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 5)
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
                    GP = New dbContabilidadGeneraPolizas(M, V.Fecha, V.Fecha, V.Fecha)
                    'Else
                    '   GP = New dbContabilidadGeneraPolizas(M, V.FechaCancelado, V.FechaCancelado, V.FechaCancelado)
                    'End If
                    Dim L As Byte = 0
                    If V.Leyenda Then L = 1
                    GP.GeneraPolizaGeneral(V.ID, V.Banco, 3, V.IdProveedor, 1, V.IdCuentaDest, 4, V.Cantidad, L)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = True Then
                        If GP.Exito Then
                            Dim frmp As New frmContabilidadPolizasN(GP.IdPoliza)
                            frmp.ShowDialog()
                            frmp.Dispose()
                        Else
                            MsgBox("No se generó la póliza.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = False Then PopUp("Póliza Generada", 90)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        guardar(True)
    End Sub

    Private Sub txtFolio_TextChanged(sender As Object, e As EventArgs) Handles txtFolio.TextChanged
        If txtBancoOriExTrans.Enabled = True Then
            txtBancoOriExTrans.Text = txtFolio.Text
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Label22.Visible = CheckBox1.Checked
        ComboBox1.Visible = CheckBox1.Checked
        If CheckBox1.Checked And ConsultaOn = True Then
            Dim cuenta As New dbCuentas(IdsCuentas2.Valor(ComboBox1.SelectedIndex), MySqlcon)
            txtCuentaDestinoTrans.Text = cuenta.Numero
            cmbBancos.SelectedIndex = IdsBancos.Busca(cuenta.Banco)
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If CheckBox1.Checked And ConsultaOn = True Then
            Dim cuenta As New dbCuentas(IdsCuentas2.Valor(ComboBox1.SelectedIndex), MySqlcon)
            txtCuentaDestinoTrans.Text = cuenta.Numero
            cmbBancos.SelectedIndex = IdsBancos.Busca(cuenta.Banco)
        End If
    End Sub

    Private Sub cmbCuenta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCuenta.SelectedIndexChanged
        If ConsultaOn Then FiltroTodos()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        txtBancoDesExTrans.Enabled = CheckBox2.Checked
        cmbMonedaTrans.Enabled = CheckBox2.Checked
        txtTipoCambioTran.Enabled = CheckBox2.Checked
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If ConsultaOn Then FiltroTodos()
    End Sub

    Private Sub btnLimpiarBusqueda_Click(sender As Object, e As EventArgs) Handles btnLimpiarBusqueda.Click

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If ConsultaOn Then FiltroTodos()
    End Sub

    Private Sub Leyenda_CheckedChanged(sender As Object, e As EventArgs) Handles Leyenda.CheckedChanged

    End Sub

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged

    End Sub

    Private Sub chkFecha_CheckedChanged(sender As Object, e As EventArgs) Handles chkFecha.CheckedChanged

    End Sub
End Class