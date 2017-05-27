Public Class frmBuscaDocumentoVenta
    Dim ConsultaOn As Boolean = True
    Dim Modo As Integer
    Public Tipo As Integer
    Dim BuscaCompras As Boolean
    Dim DocumentoDefault As Integer
    Dim IdsSucursales As New elemento
    Dim IdSucursal As Integer
    Dim PorSurtirIndex As Integer
    Dim porSurtirNotodos As Boolean
    Dim PermitirCambiarDoc As Boolean
    Dim PermitirCambiarFormadePAgo As Boolean
    Dim indexTipoPago As Integer
    Dim SoloGuardados As Boolean
    Dim ProvCliente As String
    Dim PermitirCambiarSucursal As Boolean
    Dim iTipoG As Byte
    Public Sub New(ByVal pModo As Integer, ByVal pBuscaCompras As Boolean, ByVal pDocumentoDefault As Integer, ByVal pidsucursal As Integer, ByVal pPorSurtirIndex As Integer, ByVal pPorSurtirNotodos As Boolean, pPermitirCambiarDoc As Boolean, pPermitirCambiarFormadePago As Boolean, pIndexTipoPago As Integer, pSoloGuardados As Boolean, pProvcliente As String, pPermitirCambiarSuc As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Modo = pModo
        BuscaCompras = pBuscaCompras
        DocumentoDefault = pDocumentoDefault
        IdSucursal = pidsucursal
        PorSurtirIndex = pPorSurtirIndex
        porSurtirNotodos = pPorSurtirNotodos
        PermitirCambiarDoc = pPermitirCambiarDoc
        PermitirCambiarFormadePAgo = pPermitirCambiarFormadePago
        PermitirCambiarSucursal = pPermitirCambiarSuc
        indexTipoPago = pIndexTipoPago
        SoloGuardados = pSoloGuardados
        ProvCliente = pProvcliente
    End Sub

    Public ReadOnly Property id() As Integer()
        Get
            Dim arr As Integer() = {}
            Dim r As DataGridViewRow
            For Each r In DGServicios.SelectedRows
                ReDim Preserve arr(arr.Length)
                arr(arr.Length - 1) = r.Cells(0).Value
            Next
            Return arr
        End Get
    End Property
    Public ReadOnly Property Folios() As String()
        Get
            Dim arr As String() = {}
            Dim r As DataGridViewRow
            For Each r In DGServicios.SelectedRows
                ReDim Preserve arr(arr.Length)
                arr(arr.Length - 1) = r.Cells(2).Value
            Next
            Return arr
        End Get
    End Property

    Private Sub Consulta()
        If BuscaCompras = False Then
            Try
                If ConsultaOn Then
                    Dim Estado As Byte
                    Select Case cmbEstado.SelectedIndex
                        Case 0
                            Estado = 0
                        Case 1
                            Estado = Estados.Pendiente
                        Case 2
                            Estado = Estados.Guardada
                        Case 3
                            Estado = Estados.Cancelada
                    End Select
                    Dim iTipo As Byte
                    If Modo = 0 Then
                        iTipo = ComboBox1.SelectedIndex
                    Else
                        If ComboBox1.SelectedIndex = 0 Then
                            iTipo = 2
                        Else
                            iTipo = 3
                        End If
                    End If
                    iTipoG = iTipo
                    Select Case iTipo
                        Case 0
                            Select Case cmbEstado.SelectedIndex
                                Case 0
                                    Estado = 0
                                Case 1
                                    Estado = Estados.Pendiente
                                Case 2
                                    Estado = Estados.Pendiente
                                Case 3
                                    Estado = Estados.Cancelada
                            End Select
                            Dim S As New dbVentasCotizaciones(MySqlcon)
                            DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, Estado, txtFolio.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                        Case 1
                            Dim S As New dbVentasPedidos(MySqlcon)
                            DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, Estado, txtFolio.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                        Case 2
                            Dim Oc As New dbOpcionesOc(MySqlcon)
                            Oc.LlenaDatos(0, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                            Dim S As New dbVentasRemisiones(MySqlcon)
                            If porSurtirNotodos = False Then
                                DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, txtFolio.Text, Estado, 1, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, Oc.SeriesOc, Oc.OcultarOc, ComboBox4.SelectedIndex)
                            Else
                                DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, txtFolio.Text, Estado, 1, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, Oc.SeriesOc, Oc.OcultarOc, ComboBox4.SelectedIndex + 1)
                            End If
                        Case 3
                            Dim Credito As Byte
                            Select Case ComboBox2.SelectedIndex
                                Case 0
                                    Credito = 200
                                Case 1
                                    Credito = 1
                                Case 2
                                    Credito = 0
                            End Select
                            Dim Semillas As Byte = 0
                            If Modo = 3 Then
                                Semillas = 1
                            End If
                            Dim S As New dbVentas(MySqlcon)
                            If porSurtirNotodos = False Then
                                DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, txtFolio.Text, Estado, Credito, 0, ComboBox4.SelectedIndex, IdsSucursales.Valor(ComboBox3.SelectedIndex), Semillas)
                            Else
                                DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, txtFolio.Text, Estado, Credito, 0, ComboBox4.SelectedIndex + 1, IdsSucursales.Valor(ComboBox3.SelectedIndex), Semillas)
                            End If
                        Case 4
                            ConsultaFertilizantesPEdidos()
                    End Select
                    If iTipo <= 3 Then
                        DGServicios.Columns(0).Visible = False
                        DGServicios.Columns(1).HeaderText = "Fecha"
                        DGServicios.Columns(2).HeaderText = "Folio"
                        DGServicios.Columns(3).HeaderText = "C. Cliente"
                        DGServicios.Columns(4).HeaderText = "Cliente"
                        DGServicios.Columns(5).HeaderText = "Importe"
                        DGServicios.Columns(6).HeaderText = "Estado"
                        DGServicios.Columns(2).Width = 80
                        DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    Else
                        If iTipo <> 4 Then
                            DGServicios.Columns(0).Visible = False
                            DGServicios.Columns(1).HeaderText = "Fecha"
                            DGServicios.Columns(2).HeaderText = "C. Cliente"
                            DGServicios.Columns(3).HeaderText = "Nombre Cliente"
                            DGServicios.Columns(6).HeaderText = "Importe"
                            DGServicios.Columns(7).HeaderText = "Estado"
                            DGServicios.Columns(2).Width = 80
                            DGServicios.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        End If
                    End If
                        DGServicios.ClearSelection()
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        Else
            ConsultaCompras()
        End If
    End Sub

    Private Sub ConsultaCompras()
        Try
            If ConsultaOn Then
                Dim Estado As Byte
                Select Case cmbEstado.SelectedIndex
                    Case 0
                        Estado = 0
                    Case 1
                        Estado = Estados.Pendiente
                    Case 2
                        Estado = Estados.Guardada
                    Case 3
                        Estado = Estados.Cancelada
                End Select
                Dim iTipo As Byte
                If Modo = 0 Then
                    iTipo = ComboBox1.SelectedIndex
                Else
                    If ComboBox1.SelectedIndex = 0 Then
                        iTipo = 2
                    Else
                        iTipo = 3
                    End If
                End If
                iTipoG = iTipo
                Select Case iTipo
                    Case 0
                        Dim S As New dbComprasCotizacionesb(MySqlcon)
                        DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, 0, txtFolio.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                    Case 1
                        Dim S As New dbComprasPedidos(MySqlcon)
                        Dim ps As Boolean = False
                        If ComboBox4.SelectedIndex > 0 Then ps = True
                        DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, Estado, txtFolio.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), ps)
                    Case 2
                        Dim S As New dbComprasRemisiones(MySqlcon)
                        DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, Estado, txtFolio.Text, 1, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                    Case 3
                        Dim Credito As Byte
                        Select Case ComboBox2.SelectedIndex
                            Case 0
                                Credito = 200
                            Case 1
                                Credito = 1
                            Case 2
                                Credito = 0
                        End Select
                        Dim S As New dbCompras(MySqlcon)
                        DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, txtFolio.Text, Estado, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                End Select
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Folio"
                DGServicios.Columns(3).HeaderText = "C. Prov"
                DGServicios.Columns(4).HeaderText = "Proveedor"
                DGServicios.Columns(5).HeaderText = "Importe"
                DGServicios.Columns(6).HeaderText = "Estado"
                DGServicios.Columns(2).Width = 80
                DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub ConsultaFertilizantesPEdidos()
        Try
            If ConsultaOn Then
                Dim Estado As Byte = Estados.Guardada
                'Select Case cmbEstado.SelectedIndex
                '    Case 0
                '        Estado = 0
                '    Case 1
                '        Estado = Estados.Pendiente
                '    Case 2
                '        Estado = Estados.Guardada
                '    Case 3
                '        Estado = Estados.Cancelada
                'End Select
                Dim Credito As Byte = 200
                'Select Case ComboBox1.SelectedIndex
                '    Case 0
                '        Credito = 200
                '    Case 1
                '        Credito = 1
                '    Case 2
                '        Credito = 0
                'End Select
                Dim S As New dbFertilizantesPedido(MySqlcon)
                DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, Estado, txtFolio.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), 2, True)
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Folio"
                DGServicios.Columns(3).HeaderText = "C. Cliente"
                DGServicios.Columns(4).HeaderText = "Cliente"
                DGServicios.Columns(5).HeaderText = "Importe"
                DGServicios.Columns(6).HeaderText = "Estado"
                DGServicios.Columns(7).HeaderText = "Estado P."
                DGServicios.Columns(2).Width = 80
                DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        AbreDetalles()
    End Sub
    Private Sub AbreDetalles()
        'If Modo = ModosDeBusqueda.Principal Then
        If id.Length <> 0 Then
            '0 cotizacion
            '1 pedido
            '2 remision
            '3 ventas
            If Modo = 0 Then
                Tipo = ComboBox1.SelectedIndex
            Else
                If ComboBox1.SelectedIndex = 0 Then
                    Tipo = 2
                Else
                    Tipo = 3
                End If

            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
            'Me.Close()
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCliente.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha1.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha2.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick

    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        AbreDetalles()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolio.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub frmVentasConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        cmbEstado.Items.Add("Todos")
        cmbEstado.Items.Add("Pendiente")
        cmbEstado.Items.Add("Guardado")
        cmbEstado.Items.Add("Cancelado")
        cmbEstado.SelectedIndex = 2
        If SoloGuardados Then
            cmbEstado.Enabled = False
        End If
        If porSurtirNotodos = False Then
            ComboBox4.Items.Add("Todos")
        End If
        ComboBox4.Items.Add("Normal")
        ComboBox4.Items.Add("Por Surtir")
        ComboBox4.SelectedIndex = PorSurtirIndex
        txtCliente.Text = ProvCliente
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        ComboBox3.SelectedIndex = IdsSucursales.Busca(IdSucursal)
        ComboBox3.Enabled = PermitirCambiarSucursal
        If Modo = 0 Then

            If BuscaCompras Then
                ComboBox1.Items.Add("Pre-orden")
                ComboBox1.Items.Add("Orden de Compra")
                ComboBox1.Items.Add("Remisión")
                ComboBox1.Items.Add("Compra")
            Else
                ComboBox1.Items.Add("Cotización")
                ComboBox1.Items.Add("Pedido")
                ComboBox1.Items.Add("Remisión")
                ComboBox1.Items.Add("Factura")
                If GlobalConFertilizantes Then ComboBox1.Items.Add("Fertilizantes")
            End If

            ComboBox1.SelectedIndex = DocumentoDefault
        Else
            ComboBox1.Items.Add("Remisión")
            If BuscaCompras Then
                ComboBox1.Items.Add("Compra")
            Else
                ComboBox1.Items.Add("Factura")
            End If
            ComboBox4.Enabled = False
            cmbEstado.SelectedIndex = 2
            cmbEstado.Enabled = False
            ComboBox1.SelectedIndex = DocumentoDefault
            If Modo = 3 Then
                ComboBox1.Enabled = False
            End If
        End If
        ComboBox2.Items.Add("Todos")
        ComboBox2.Items.Add("Contado")
        ComboBox2.Items.Add("Crédito")
        ComboBox2.SelectedIndex = indexTipoPago
        ComboBox1.Enabled = PermitirCambiarDoc
        ComboBox2.Enabled = PermitirCambiarFormadePAgo
        dtpFecha1.Value = CDate(Format(Date.Now, "yyyy") + "/01/01")
        dtpFecha2.Value = Date.Now
        ConsultaOn = True
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Consulta()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "Pre-orden" Or ComboBox1.Text = "Orden de Compra" Or ComboBox1.Text = "Cotización" Or ComboBox1.Text = "Pedido" Then
            ComboBox3.Enabled = True
        Else
            ComboBox3.Enabled = PermitirCambiarSucursal
        End If
        
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_SelectionChanged(sender As Object, e As System.EventArgs) Handles DGServicios.SelectionChanged
        SumaSeleccionados()
    End Sub
    Private Sub SumaSeleccionados()
        Dim T As Double = 0
        If BuscaCompras = False Then
            Dim iTipo As Byte
            If Modo = 0 Then
                iTipo = ComboBox1.SelectedIndex
            Else
                If ComboBox1.SelectedIndex = 0 Then
                    iTipo = 2
                Else
                    iTipo = 3
                End If
            End If
            For Each r As DataGridViewRow In DGServicios.SelectedRows
                Select Case iTipo
                    Case 0
                        T += r.Cells(5).Value
                    Case 1
                        T += r.Cells(5).Value
                    Case 2
                        T += r.Cells(5).Value
                    Case 3
                        T += r.Cells(5).Value
                    Case 4
                        T += r.Cells(5).Value
                End Select
            Next
        Else
            Dim iTipo As Byte
            If Modo = 0 Then
                iTipo = ComboBox1.SelectedIndex
            Else
                If ComboBox1.SelectedIndex = 0 Then
                    iTipo = 2
                Else
                    iTipo = 3
                End If
            End If
            For Each r As DataGridViewRow In DGServicios.SelectedRows
                Select Case iTipo
                    Case 0
                        T += r.Cells(5).Value
                    Case 1
                        T += r.Cells(5).Value
                    Case 2
                        T += r.Cells(5).Value
                    Case 3
                        T += r.Cells(5).Value
                End Select
            Next
        End If
        Label10.Text = "Total seleccionado= " + T.ToString("###,###,##0.00")
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        If iTipoG <= 3 And e.ColumnIndex = 5 Then
            e.Value = Format(e.Value, "c2")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub
End Class