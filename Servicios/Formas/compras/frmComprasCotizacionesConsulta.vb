Public Class frmComprasCotizacionesConsulta
    Public IdCotizacion As Integer
    Dim ConsultaOn As Boolean = True
    Dim Modo As Integer
    Dim TipoCot As Byte
    Dim IdsSucursales As New elemento
    Public Sub New(ByVal pModo As Integer, ByVal pTipoCot As Byte)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Modo = pModo
        TipoCot = pTipoCot
    End Sub

    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim Estado As Byte
                Select Case ComboBox2.SelectedIndex
                    Case 0
                        Estado = 0
                    Case 1
                        Estado = Estados.Pendiente
                    Case 2
                        Estado = Estados.Guardada
                    Case 3
                        Estado = Estados.Cancelada
                End Select
                'If TipoCot = 0 Then
                '    Dim S As New dbComprasCotizaciones(MySqlcon)
                '    DGServicios.DataSource = S.Consulta(Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), ComboBox2.SelectedIndex, TextBox2.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                '    DGServicios.Columns(0).Visible = False
                '    DGServicios.Columns(1).HeaderText = "Fecha"
                '    DGServicios.Columns(2).HeaderText = "Referencia"
                '    'DGServicios.Columns(3).HeaderText = "C. Proveedor"
                '    'DGServicios.Columns(4).HeaderText = "Proveedor"
                '    'DGServicios.Columns(2).Width = 200
                '    DGServicios.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                '    'Else
                Dim S As New dbComprasCotizacionesb(MySqlcon)
                DGServicios.DataSource = S.Consulta(Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), TextBox1.Text, Estado, TextBox2.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Folio"
                DGServicios.Columns(3).HeaderText = "Clave P."
                DGServicios.Columns(4).HeaderText = "Proveedor"
                DGServicios.Columns(5).HeaderText = "Importe"
                DGServicios.Columns(6).HeaderText = "Estado"
                DGServicios.Columns(3).Width = 80
                DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
                IdCotizacion = 0
                'End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        AbreDetalles()
    End Sub
    Private Sub AbreDetalles()
        If IdCotizacion <> 0 Then
            If Modo = ModosDeBusqueda.Principal Then
                    Dim F As New frmComprasCotizacionesNB(DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value)
                    F.ShowDialog()
            Else
                IdCotizacion = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker3.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        IdCotizacion = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        IdCotizacion = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
        AbreDetalles()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub
    Private Sub frmComprasCotizacionesConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ComboBox2.Items.Add("Todos")
        ComboBox2.Items.Add("Guardados")
        ComboBox2.Items.Add("Pendientes")
        ConsultaOn = False
        ComboBox2.SelectedIndex = 0
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CambioSucursal, PermisosN.Secciones.Compras) = False Then
            ComboBox3.Enabled = False
            ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        End If
        DateTimePicker2.Value = CDate(Format(Date.Now, "yyyy") + "/01/01")
        DateTimePicker3.Value = Date.Now
        ConsultaOn = True
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Consulta()
    End Sub
    'Dim x As Integer = 100
    'Dim y As Integer = 500
    'Dim t As Double = 0
    'Private Sub frmComprasCotizacionesConsulta_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    'x=Vo*cosA*t 
    '    'y=Vo*senA*t-(1/2)*g*t^2 
    '    e.Graphics.DrawLine(Pens.Black, x, y, x + 3, y + 3)
    '    e.Graphics.DrawLine(Pens.Black, 100, 500, 300, 500)
    'End Sub

    'Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
    '    x += 80 * Math.Cos(70) * t
    '    y -= 80 * Math.Sin(70) * t - (t * t * 98 / 2)
    '    t += 0.01
    '    Me.Refresh()
    'End Sub

    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        If e.ColumnIndex = 5 Then
            e.Value = Format(e.Value, "c2")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub
End Class