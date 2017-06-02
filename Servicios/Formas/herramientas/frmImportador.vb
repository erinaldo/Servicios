Public Class frmImportador
    Dim IdsSucursales As New elemento
    Dim IdsMovimientos As New elemento
    Dim IdsAlmacenes As New elemento
    Dim WithEvents Imp As Importador
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            ' OpenFileDialog1.Filter = "Archivos Excel|*.xlsx;*.xls"
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                ''TextBox26.Text = OpenFileDialog1.FileName
                Imp = New Importador(OpenFileDialog1.FileName, MySqlcon)
                ''DataGridView1.DataSource = I.ConsultaExcel()
                If IdsSucursales.Valor(ComboBox3.SelectedIndex) <= 0 Then
                    MsgBox("Debe dar de alta sucursales.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                If IdsMovimientos.Valor(ComboBox6.SelectedIndex) <= 0 Then
                    MsgBox("Debe dar de alta conceptos de entrada.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                If IdsAlmacenes.Valor(ComboBox8.SelectedIndex) <= 0 Then
                    MsgBox("Debe dar de alta almacenes.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                Imp.ImportaArticulos(CheckBox1.Checked, IdsSucursales.Valor(ComboBox3.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdsMovimientos.Valor(ComboBox6.SelectedIndex), CheckBox2.Checked)
                Imp.CierraConexiones()
                MsgBox("Listo")
                'Label40.Text = I.ConsultaExcelReader
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button52_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button52.Click
        Try

            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                ''TextBox26.Text = OpenFileDialog1.FileName
                Imp = New Importador(OpenFileDialog1.FileName, MySqlcon)
                Imp.ImportaClientes()
                Imp.CierraConexiones()
                MsgBox("Listo")
                'Label40.Text = I.ConsultaExcelReader


            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button53_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button53.Click
        Try

            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                ''TextBox26.Text = OpenFileDialog1.FileName
                Imp = New Importador(OpenFileDialog1.FileName, MySqlcon)
                Imp.ImportaProveedores()
                Imp.CierraConexiones()
                MsgBox("Listo")
                'Label40.Text = I.ConsultaExcelReader
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frmImportador_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        LlenaCombos("tblinventarioconceptos", ComboBox6, "nombre", "nombret", "idconcepto", IdsMovimientos, "idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString + " and tipo=0")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            'OpenFileDialog1.Filter = "Archivos Excel|*.xlsx;*.xls"
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                ''TextBox26.Text = OpenFileDialog1.FileName
                Imp = New Importador(OpenFileDialog1.FileName, MySqlcon)
                ''DataGridView1.DataSource = I.ConsultaExcel()
                If IdsSucursales.Valor(ComboBox3.SelectedIndex) <= 0 Then
                    MsgBox("Debe dar de alta sucursales.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                If IdsMovimientos.Valor(ComboBox6.SelectedIndex) <= 0 Then
                    MsgBox("Debe dar de alta conceptos de entrada.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                If IdsAlmacenes.Valor(ComboBox8.SelectedIndex) <= 0 Then
                    MsgBox("Debe dar de alta almacenes.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                Imp.ImportaExistencia(CheckBox1.Checked, IdsSucursales.Valor(ComboBox3.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdsMovimientos.Valor(ComboBox6.SelectedIndex), CheckBox2.Checked)
                Imp.CierraConexiones()
                MsgBox("Listo")
                'Label40.Text = I.ConsultaExcelReader
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try

            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                ''TextBox26.Text = OpenFileDialog1.FileName
                Imp = New Importador(OpenFileDialog1.FileName, MySqlcon)
                Imp.ImportaDocumentosClientes()
                Imp.CierraConexiones()
                MsgBox("Listo")
                'Label40.Text = I.ConsultaExcelReader
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                ''TextBox26.Text = OpenFileDialog1.FileName
                Imp = New Importador(OpenFileDialog1.FileName, MySqlcon)
                Imp.ImportaDocumentosProv()
                Imp.CierraConexiones()
                MsgBox("Listo")
                'Label40.Text = I.ConsultaExcelReader
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ActualizaRenglones() Handles Imp.RenglonLeido
        Label1.Text = "Registros leidos: " + Imp.RenglonCont.ToString
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                ''TextBox26.Text = OpenFileDialog1.FileName
                Dim En As New Encriptador()
                ProgressBar1.Visible = True
                Imp = New Importador(OpenFileDialog1.FileName, MySqlcon)
                En.GuardaArchivoTexto("unidades.txt", Imp.UnidadesScrip(), System.Text.Encoding.Default)
                Imp.CierraConexiones()
                MsgBox("Listo")
                ProgressBar1.Visible = False
                'Label40.Text = I.ConsultaExcelReader
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
End Class