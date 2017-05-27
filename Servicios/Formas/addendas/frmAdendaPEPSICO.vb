Public Class frmAdendaPEPSICO
    Public strXML As String
    Dim idVenta As Integer
    Dim Addenda As dbAdendaPEPSICO
    Dim Tipo As String = "AddendaPCO"
    Dim version As Double = 2
    Dim tabla As New DataTable
    Dim idAddenda As Integer
    Dim Venta As dbVentas
    Dim NoMostrar As Boolean



    Public Sub New(ByVal pidVenta As Integer, ByVal pNomostrar As Boolean)
        InitializeComponent()
        Dim Ad As New dbAdendaPEPSICO(MySqlcon)
        Dim Tabla2 As DataTable
        Dim Precio As Double
        idVenta = pidVenta
        Venta = New dbVentas(idVenta, MySqlcon, "0")
        tabla.Columns.Add("idRetencion")
        tabla.Columns.Add("Cantidad")
        tabla.Columns.Add("Unidad")
        tabla.Columns.Add("Descripcion")
        tabla.Columns.Add("valorUnitario")
        tabla.Columns.Add("Importe")
        tabla.Columns.Add("idVentasInventario")
        Tabla2 = Ad.llenaTabla(idVenta)

        For i As Integer = 0 To Tabla2.Rows.Count - 1
            Dim dr As DataRow
            Precio = (Double.Parse(Tabla2.Rows(i)(3).ToString)) / (Double.Parse(Tabla2.Rows(i)(0).ToString))
            dr = tabla.NewRow()
            dr("idRetencion") = ""
            dr("Cantidad") = Format(Double.Parse(Tabla2.Rows(i)(0).ToString), "0.00")
            dr("Unidad") = Tabla2.Rows(i)(1).ToString
            dr("Descripcion") = Tabla2.Rows(i)(2).ToString
            dr("valorUnitario") = Format(Precio, "0.00")
            dr("Importe") = Format(Double.Parse(Tabla2.Rows(i)(3).ToString), "0.00")
            dr("idVentasInventario") = Tabla2.Rows(i)(4).ToString
            tabla.Rows.Add(dr)
        Next
        DataGridView1.DataSource = tabla

        DataGridView1.Columns(4).HeaderText = "Valor Unitario"
        DataGridView1.Columns(0).HeaderText = "ID Recepción"
        DataGridView1.Columns(1).Width = 60
        DataGridView1.Columns(2).Width = 70
        DataGridView1.Columns(4).Width = 90
        DataGridView1.Columns(5).Width = 90
        DataGridView1.Columns(1).ReadOnly = True
        DataGridView1.Columns(2).ReadOnly = True
        DataGridView1.Columns(3).ReadOnly = True
        DataGridView1.Columns(4).ReadOnly = True
        DataGridView1.Columns(5).ReadOnly = True
        DataGridView1.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns(6).Visible = False
        If NoMostrar Then BotonGuardar()
    End Sub


    Private Sub frmAdendaPEPSICO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            Dim tablaAux As DataTable

            Addenda = New dbAdendaPEPSICO(MySqlcon)
            Addenda.LlenaDatos(idVenta)
            Dim aux1 As String
            Dim aux2 As String

            If Addenda.idAddenda <> 0 Then

                txtIDPedido.Text = Addenda.idPedido
                txtIDSolicitudPago.Text = Addenda.idSolicitudPago
                txtReferencia.Text = Addenda.referencia
                txtSerie.Text = Addenda.serie
                txtFolio.Text = Addenda.folio
                txtFolioUUID.Text = Addenda.folioUUID
                txtTipoDoc.Text = Addenda.tipoDoc.ToString()
                txtProveedor.Text = Addenda.idProveedor
                idAddenda = Addenda.idAddenda
                ' txtRecepcion.Text = Addenda.idRecepcion.ToString()
                tablaAux = Addenda.llenaTablaRecepciones(idAddenda)
                For i As Integer = 0 To tablaAux.Rows.Count - 1

                    For j As Integer = 0 To DataGridView1.Rows.Count - 1

                        aux1 = tablaAux.Rows(i)(2).ToString
                        aux2 = DataGridView1(6, j).Value.ToString()

                        If aux1 = aux2 Then
                            DataGridView1(0, j).Value = tablaAux.Rows(i)(3).ToString
                        End If
                    Next


                Next
            Else
                txtFolio.Text = Venta.Folio.ToString
                txtSerie.Text = Venta.Serie
                Venta.DaDatosTimbrado(idVenta)
                txtFolioUUID.Text = Venta.uuid
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub txtIDPedido_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIDPedido.KeyPress
        'If InStr(1, "0123456789" & Chr(8), e.KeyChar) = 0 Then
        '    e.KeyChar = ""
        'End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTipoDoc.KeyPress
        If InStr(1, "0123456789" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProveedor.KeyPress
        If InStr(1, "0123456789" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        

        BotonGuardar()
    End Sub
    Private Sub BotonGuardar()
        txtIDSolicitudPago.BackColor = Color.White
        txtTipoDoc.BackColor = Color.White
        txtProveedor.BackColor = Color.White
        'txtRecepcion.BackColor = Color.White
        If txtIDPedido.Text = "" And txtIDSolicitudPago.Text = "" Then
            MsgBox("Debe indicar un número de pedido o una solicitud de pago.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If txtProveedor.Text = "" Then
            MsgBox("Debe indicar el proveedor.", MsgBoxStyle.Information, GlobalNombreApp)
            'txtIDSolicitudPago.BackColor = Color.FromArgb(250, 150, 150)
            'txtTipoDoc.BackColor = Color.FromArgb(250, 150, 150)
            txtProveedor.BackColor = Color.FromArgb(250, 150, 150)
            '  txtRecepcion.BackColor = Color.FromArgb(250, 150, 150)

            Exit Sub


        End If

        For i As Integer = 0 To DataGridView1.RowCount() - 1
            If DataGridView1(0, i).Value = "" Then
                MsgBox("Debe indicar un número de recepción para cada artículo.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub

            End If
        Next

        If Addenda.idAddenda = 0 Then
            Addenda.Guardar(Tipo, version, txtIDPedido.Text, txtIDSolicitudPago.Text, txtReferencia.Text, txtSerie.Text, txtFolio.Text, txtFolioUUID.Text, txtTipoDoc.Text, txtProveedor.Text, idVenta)
            idAddenda = Addenda.idAddenda
            For i As Integer = 0 To DataGridView1.RowCount() - 1
                'guardar todos los renglonres
                Addenda.GuardarRecepcion(idAddenda, DataGridView1(6, i).Value.ToString(), DataGridView1(0, i).Value.ToString())
            Next


        Else
            'que se cargue el idaddenda
            Addenda.Modificar(Tipo, version, txtIDPedido.Text, txtIDSolicitudPago.Text, txtReferencia.Text, txtSerie.Text, txtFolio.Text, txtFolioUUID.Text, txtTipoDoc.Text, txtProveedor.Text, idVenta)
            'que se eliminen todos los registros y lkuego se guarden
            Addenda.eliminarRecepcion(idAddenda)
            For i As Integer = 0 To DataGridView1.RowCount() - 1
                'guardar todos los renglonres
                Addenda.GuardarRecepcion(idAddenda, DataGridView1(6, i).Value.ToString, DataGridView1(0, i).Value.ToString)
            Next

        End If


        strXML = Addenda.CreaXML(idVenta)
        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub
    Private Sub DataGridView1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGridView1.KeyPress

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "." Then
            e.Handled = True
        End If
    End Sub
  

    Private Sub DataGridView1_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If e.ColumnIndex = 0 Then
            If Not e.FormattedValue.ToString = String.Empty Then

                If Not IsNumeric(e.FormattedValue.ToString) Then
                    If Not e.FormattedValue.ToString = "," Or Not e.FormattedValue.ToString = "." Then
                        MsgBox("El valor de recepción debe ser numérico. Favor de cambiarlo.", MsgBoxStyle.Information, GlobalNombreApp)
                        e.Cancel = True ' BLOQUEMOS PARA QUE NO SALGA DE LA CELDA 
                    End If

                End If
                Dim aux As String = e.FormattedValue.ToString
                If aux.Length <> 10 Then
                    MsgBox("El valor de recepción debe contener 10 dígitos. Favor de corregirlo.", MsgBoxStyle.Information, GlobalNombreApp)
                    e.Cancel = True ' BLOQUEMOS PARA QUE NO SALGA DE LA CELDA 
                End If
            End If
        End If
    End Sub

    Private Sub txtIDPedido_TextChanged(sender As Object, e As EventArgs) Handles txtIDPedido.TextChanged

    End Sub
End Class