Public Class frmConciliarInventario

    Public IdCompra As Integer
    Dim IdInventario As Integer
    Dim IdAlmacenes As New elemento
    Dim ConsultaOn As Boolean = True
    Dim Modo As Integer
    Dim IdsListas As New elemento
    Dim IdsSucursales As New elemento
    Public Sub New(ByVal pIdInventario As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdInventario = pIdInventario

    End Sub

    Private Sub frmConciliarInventario_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If CheckBox2.Checked Then
            My.Settings.rfc = "1"
        Else
            My.Settings.rfc = "0"
        End If
        'My.Settings.Save()
    End Sub
    Private Sub frmComprasConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If My.Settings.rfc = "1" Then
            CheckBox2.Checked = True
        End If
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        LlenaCombos("tbllistasprecios", ComboBox2, "descripcion", "nombret", "idlista", IdsListas)
        ConsultaOn = False
        DateTimePicker2.Value = Date.Now
        'chkTiempoReal.Checked = ConsultaTiempoRealG
        DGServicios.AutoGenerateColumns = False
        ConsultaOn = True
        Consulta()
    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim S As New dbInventario(MySqlcon)
                DGServicios.DataSource = S.conciliarInventario(IdAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex))
                DGServicios.ClearSelection()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub CalculaSaldos(ByVal pSaldoInicial As Double)
        Dim C As Integer = 0
        Dim Cc As New dbClientes(MySqlcon)
        pSaldoInicial = Cc.DaSaldoAFecha(IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"))
        While C < DGServicios.RowCount
            pSaldoInicial = pSaldoInicial + DGServicios.Item(6, C).Value - DGServicios.Item(7, C).Value
            DGServicios.Item(8, C).Value = pSaldoInicial
            C += 1
        End While

    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Articulo, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = B.Inventario.Nombre
            IdInventario = B.Inventario.ID
            ConsultaOn = False
            TextBox1.Text = B.Inventario.Clave
            ConsultaOn = True
            'Consulta()
        End If
    End Sub
    Private Sub BuscaInventario()
        Try
            If ConsultaOn Then
                Dim c As New dbInventario(MySqlcon)
                If c.BuscaArticulo(TextBox1.Text, 1, "") Then
                    TextBox2.Text = c.Nombre
                    IdInventario = c.ID
                    Consulta()
                Else
                    IdInventario = 0
                    TextBox2.Text = ""
                    If TextBox1.Text = "" Then Consulta()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaInventario()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'DGServicios.DataSource.writexmlschema("tblconciliarinventario.xml")
        Dim r As New repConciliarInventario
        r.SetDataSource(DGServicios.DataSource)
        Dim Su As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        r.SetParameterValue("Empresa", Su.Nombre)
        r.SetParameterValue("Fechas", "Fecha: " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal:" + ComboBox1.Text + " Almancen: " + ComboBox8.Text)
        Dim f As New frmReportes(r, False)
        f.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try

            Dim I As New dbInventario(MySqlcon)
            I.LlenaTablaConciliacion(Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdAlmacenes.Valor(ComboBox8.SelectedIndex), IdsListas.Valor(ComboBox2.SelectedIndex), CheckBox2.Checked)
            Consulta()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ConsultaOn = False
        LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Todos")
        ConsultaOn = True
        If ConsultaOn Then
            Consulta()
        End If
    End Sub

    Private Sub DGServicios_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DGServicios.DataError
        If e.Exception IsNot Nothing AndAlso e.Context = DataGridViewDataErrorContexts.Commit Then MsgBox(e.Exception.Message)
    End Sub

    Private Sub DGServicios_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellEndEdit
        If e.ColumnIndex = 7 Then
            DGServicios.Item(8, e.RowIndex).Value = DGServicios.Item(7, e.RowIndex).Value - DGServicios.Item(6, e.RowIndex).Value
            DGServicios.Item(10, e.RowIndex).Value = DGServicios.Item(9, e.RowIndex).Value * DGServicios.Item(8, e.RowIndex).Value
        End If
        'DGServicios.Refresh()
    End Sub

    Private Sub DateTimePicker2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown, TextBox1.KeyDown, DateTimePicker2.KeyDown, ComboBox8.KeyDown, ComboBox1.KeyDown, Button5.KeyDown, Button3.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{Tab}")
    End Sub

    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            MySqlcom.Transaction = MySqlcom.Connection.BeginTransaction()
            Dim r As DataGridViewRow
            For Each r In DGServicios.Rows
                MySqlcom.CommandText = "update tblinventarioconciliaciones set existencia=" + r.Cells(7).Value.ToString + ",diferencia=" + r.Cells(8).Value.ToString + ",precio=" + r.Cells(9).Value.ToString + ",importedif=" + r.Cells(10).Value.ToString + " where fecha='" + Format(DateTimePicker2.Value, "yyyy/MM/dd") + "' and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString + " and idalmacen=" + IdAlmacenes.Valor(ComboBox8.SelectedIndex).ToString + " and idinventario=" + r.Cells(3).Value.ToString + ";"
                MySqlcom.ExecuteNonQuery()
            Next
            MySqlcom.Transaction.Commit()
            PopUp("Conciliación Guardada.", 90)
        Catch ex As Exception
            MySqlcom.Transaction.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        If ConsultaOn Then
            Consulta()
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged
        If ConsultaOn Then
            Consulta()
        End If
    End Sub
End Class