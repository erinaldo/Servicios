Public Class frmClientesDescuentos
    Private clientes As dbClientes
    Private descuentos As dbClientesDescuentos
    Private idCliente As Integer
    Private idClas1 As Integer = -1
    Private idClas2 As Integer = -1
    Private idClas3 As Integer = -1
    Private idsClas1 As New elemento
    Private idsClas2 As New elemento
    Private idsClas3 As New elemento
    Private nuevo As Boolean = True
    Private guardado As Boolean = False

    Public Sub New(ByVal idCliente As Integer, pNombre As String)
        InitializeComponent()
        Me.idCliente = idCliente
        txtCliente.Text = pNombre
    End Sub
    Private Sub frmClientesDescuentos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception
           
        End Try
        If idCliente > 0 Then
            clientes = New dbClientes(idCliente, MySqlcon)
        Else
            clientes = New dbClientes(MySqlcon)
        End If
        descuentos = New dbClientesDescuentos(MySqlcon)

        LlenaCombos("tblinventarioclasificaciones", comboClas1, "nombre", "nombret", "idclasificacion", idsClas1, "idclasificacion>1", , "nombre")
        llenaGrid()
    End Sub

    Private Sub comboClas1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboClas1.SelectedIndexChanged
        idClas1 = idsClas1.Valor(comboClas1.SelectedIndex)
        'If idClas1 > 0 Then
        LlenaCombos("tblinventarioclasificaciones2", comboClas2, "nombre", "nombret", "idclasificacion", idsClas2, "idclasificacion>1 and idnivelsuperior=" + idClas1.ToString, "General", "nombre")
        'End If
    End Sub

    Private Sub comboClas2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboClas2.SelectedIndexChanged
        idClas2 = idsClas2.Valor(comboClas2.SelectedIndex)
        'If idClas2 > 0 Then
        LlenaCombos("tblinventarioclasificaciones3", comboClas3, "nombre", "nombret", "idclasificacion", idsClas3, "idclasificacion>1 and idnivelsuperior=" + idClas2.ToString, "General", "nombre")
        'c'omboClas3.Enabled = True
        'End If
    End Sub

    Private Sub comboClas3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboClas3.SelectedIndexChanged
        idClas3 = idsClas3.Valor(comboClas3.SelectedIndex)
    End Sub

    Private Sub llenaGrid()
        dgvDescuentos.DataSource = descuentos.buscaFiltrado(clientes.ID)
        dgvDescuentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvDescuentos.Columns(0).Visible = False
        dgvDescuentos.Columns(4).HeaderText = "Descuento"
        dgvDescuentos.Columns(4).Width = 80
        dgvDescuentos.ClearSelection()

    End Sub
   

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If IsNumeric(txtDescuento.Text) Then
            If idClas2 < 0 Then idClas2 = 0
            If idClas3 < 0 Then idClas3 = 0
            If descuentos.buscaDescuento(idCliente, idClas1, idClas2, idClas3) = -1000 Then
                descuentos.guardar(clientes.ID, idClas1, idClas2, idClas3, CDbl(txtDescuento.Text), 0)
                PopUp("Guardado", 30)
                guardado = True
                llenaGrid()
                txtDescuento.Text = ""
                comboClas1.Focus()
            Else
                MsgBox("Ya hay un descuento asignado a esa clasificación.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Else
            MsgBox("El descuento debe ser un valor numérico.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub txtDescuento_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDescuento.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If InStr("0123456789.", Chr(KeyAscii)) = 0 Then
            If KeyAscii <> 8 Then
                KeyAscii = 0
            End If
            e.KeyChar = Chr(KeyAscii)
            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvDescuentos_DoubleClick(sender As Object, e As EventArgs) Handles dgvDescuentos.DoubleClick
        Try
            Dim id As Integer = CInt(dgvDescuentos.CurrentRow.Cells(0).Value.ToString())
            Dim result = MsgBox("¿Desea eliminar el descuento seleccionado?", MsgBoxStyle.YesNo)
            If result = DialogResult.Yes Then
                descuentos.eliminar(id)
                llenaGrid()
                PopUp("Eliminado", 30)
            End If
        Catch ex As Exception

        End Try
    End Sub

   
End Class