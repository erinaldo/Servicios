Public Class frmBancosAddComprobantes
    Dim bancoUuid As dbBancosUuids
    Dim RFCProveedor As String
    Dim IdPagoProv As Integer
    Dim IdMoneda As Integer
    Dim IdsMonedas As New elemento
    Public Sub New(PidPagoPRov As Integer, pRFCProveedor As String, pIdMoneda As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        IdPagoProv = PidPagoPRov
        RFCProveedor = pRFCProveedor
        IdMoneda = pIdMoneda
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmBancosAddComprobantes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            bancoUuid = New dbBancosUuids(MySqlcon)
            LlenaCombos("tblmonedassat", cmbMonedaCompNac, "concat(codigo,' - ',moneda)", "nombret", "id", IdsMonedas, , , "codigo")
            Me.Show()
            Nuevo(True)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo(PConsulta As Boolean)
        cmbMonedaCompNac.SelectedIndex = IdsMonedas.Busca(IdMoneda)
        txtUUID.Text = ""
        txtMontoCompro.Text = "0.00"
        txtTipoCambioCompNac.Text = "0.00"
        btnAgregarCompro.Text = "Agregar"
        btnEliminarCompro.Enabled = False
        If PConsulta Then Consulta()
        txtUUID.Focus()
    End Sub
    Private Sub btnAgregarCompro_Click(sender As Object, e As EventArgs) Handles btnAgregarCompro.Click
        BotonGuardar()
    End Sub
    Private Sub BotonGuardar()
        Dim HayError As String = ""
        If txtUUID.Text = "" Then
            HayError = "Debe indicar el UUID."
        End If
        If txtMontoCompro.Text = "" Then
            HayError += " Debe indicar el monto."
        Else
            If IsNumeric(txtMontoCompro.Text) = False Then
                HayError += " El monto debe ser un valor numérico."
            End If
        End If
        If btnAgregarCompro.Text = "Agregar" And bancoUuid.ChecauuidRepetido(txtUUID.Text, IdPagoProv) = True Then
            HayError += "Ya se agrego este comprobante a este pago."
        End If
        If HayError = "" Then
            If btnAgregarCompro.Text = "Agregar" Then
                bancoUuid.Guardar(txtUUID.Text, IdsMonedas.Valor(cmbMonedaCompNac.SelectedIndex), CDbl(txtTipoCambioCompNac.Text), CDbl(txtMontoCompro.Text), IdPagoProv, DateTimePicker1.Value.ToString("yyyy/MM/dd"))
                Nuevo(True)
                PopUp("Comprobante agregado.", 30)
            Else
                bancoUuid.Modificar(bancoUuid.ID, txtUUID.Text, IdsMonedas.Valor(cmbMonedaCompNac.SelectedIndex), CDbl(txtTipoCambioCompNac.Text), CDbl(txtMontoCompro.Text), IdPagoProv, DateTimePicker1.Value.ToString("yyyy/MM/dd"))
                Nuevo(True)
                PopUp("Comprobante modificado.", 30)
            End If
        Else
            MsgBox(HayError, MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub btnNuevoCompro_Click(sender As Object, e As EventArgs) Handles btnNuevoCompro.Click
        Nuevo(True)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub btnEliminarCompro_Click(sender As Object, e As EventArgs) Handles btnEliminarCompro.Click
        If MsgBox("¿Eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            bancoUuid.Eliminar(bancoUuid.ID)
            Nuevo(True)
            PopUp("Eliminado", 90)
        End If
    End Sub
    Private Sub Consulta()
        DataGridView1.DataSource = bancoUuid.Consulta(IdPagoProv)
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns(1).HeaderText = "Fecha"
        DataGridView1.Columns(2).HeaderText = "UUID"
        DataGridView1.Columns(3).HeaderText = "Moneda"
        DataGridView1.Columns(4).HeaderText = "T.C."
        DataGridView1.Columns(5).HeaderText = "Importe"

    End Sub

    Private Sub txtCapturador_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCapturador.KeyDown
        If e.KeyCode = Keys.Enter And txtCapturador.Text <> "" Then
            Dim codigo As String = txtCapturador.Text
            Dim rfc As String = "", cantidad As String = "", uuid As String = ""
            Dim bandera = False
            'RFC
            Try


                For i As Integer = 2 To codigo.Length() - 1
                    If codigo.Chars(i) = "/" And i <> 2 Then
                        i = codigo.Length() - 1
                    Else
                        If bandera = True Then
                            rfc += codigo.Chars(i)
                        End If
                        If codigo.Chars(i) = "¿" Then
                            bandera = True
                        End If
                    End If
                Next
                'cantidad
                bandera = False
                For i As Integer = 35 To codigo.Length() - 1
                    If codigo.Chars(i) = "/" And i <> 35 Then
                        i = codigo.Length() - 1
                    Else
                        If bandera = True Then
                            If (codigo.Chars(i) >= "0" And codigo.Chars(i) <= "9") Or codigo.Chars(i) = "." Then
                                cantidad += codigo.Chars(i)
                            End If
                        End If
                        If codigo.Chars(i) = "¿" Then
                            bandera = True
                        End If
                    End If
                Next
                'UUID
                bandera = False
                For i As Integer = codigo.Length() - 40 To codigo.Length() - 1
                    'If codigo.Chars(i) = "/" And i <> codigo.Length() - 40 Then
                    '    Return
                    'Else
                    If bandera = True Then
                        If codigo.Chars(i) = "'" Then
                            uuid += "-"
                        Else
                            uuid += codigo.Chars(i)
                        End If

                    End If
                    If codigo.Chars(i) = "¿" Then
                        bandera = True
                    End If
                    'End If
                Next
                '_/re¿SARA771001FA0/rr¿PSS100211P40/tt¿$1,160.00/id¿5919262B'2F67'4AB1'8C9D'2861D68B6ED9
                Nuevo(False)
                txtUUID.Text = uuid
                txtMontoCompro.Text = cantidad
                If rfc = RFCProveedor Then
                    BotonGuardar()
                Else
                    MsgBox("El comprobante no pertenece al proveedor seleccionado.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
                'agregar(rfc, cantidad, uuid)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
            End Try
        End If
    End Sub

    Private Sub txtCapturador_TextChanged(sender As Object, e As EventArgs) Handles txtCapturador.TextChanged

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        OpenFileDialog1.FileName = ""
        Dim p As New dbContabilidadPolizas(MySqlcon)
        OpenFileDialog1.InitialDirectory = p.rutaUUID
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                Dim xmldoc As New Xml.XmlDocument
                xmldoc.Load(OpenFileDialog1.FileName)
                Nuevo(False)
                txtUUID.Text = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                txtMontoCompro.Text = xmldoc.Item("cfdi:Comprobante").Attributes("total").Value
                DateTimePicker1.Value = xmldoc.Item("cfdi:Comprobante").Attributes("fecha").Value.Substring(0, 10)
                Try
                    txtTipoCambioCompNac.Text = xmldoc.Item("cfdi:Comprobante").Attributes("TipoCambio").Value

                    If xmldoc.Item("cfdi:Comprobante").Attributes("Moneda").Value <> "MXN" Then
                        'Buscar moneda pendiente
                    End If
                Catch ex As Exception

                End Try
                If RFCProveedor = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value Then
                    BotonGuardar()
                Else
                    MsgBox("El comprobante no pertenece al proveedor seleccionado.", MsgBoxStyle.Information, GlobalNombreApp)
                    Nuevo(False)
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
            End Try
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        bancoUuid.ID = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
        bancoUuid.llenaDatos(bancoUuid.ID)
        btnAgregarCompro.Text = "Modificar"
        btnEliminarCompro.Enabled = True
        txtUUID.Text = bancoUuid.UUID
        txtMontoCompro.Text = bancoUuid.Monto.ToString("0.00")
        txtTipoCambioCompNac.Text = bancoUuid.TipodeCambio.ToString("0.00")
        cmbMonedaCompNac.SelectedIndex = IdsMonedas.Busca(bancoUuid.Moneda)
        DateTimePicker1.Value = bancoUuid.Fecha
        txtUUID.Focus()
    End Sub
  
    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 4 Or e.ColumnIndex = 5 Then
            e.Value = Format(e.Value, "0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub
End Class