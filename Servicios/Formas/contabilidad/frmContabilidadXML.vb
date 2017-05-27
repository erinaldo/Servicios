Imports System.IO
Imports System.IO.Compression

Public Class frmContabilidadXML
    Dim p As New dbContabilidadPolizas(MySqlcon)
    Dim C As New dbContabilidadClasificacion(MySqlcon)
    Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
    Dim periodo As String

    Private Sub frmContabilidadXML_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        periodo = p.buscarPeriodo()
        'dtpDesde.MinDate = "01/01/" + periodo.ToString
        'dtpHasta.MinDate = "01/01/" + periodo.ToString
        'dtpDesde.MaxDate = "31/12/" + periodo.ToString
        'dtpHasta.MaxDate = "31/12/" + periodo.ToString
        dtpDesde.Value = "01/" + Date.Now.Month.ToString + "/" + periodo
        dtpHasta.Value = Date.Now.Day.ToString + "/" + Date.Now.Month.ToString + "/" + periodo

    End Sub

    Private Sub lstTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTipo.SelectedIndexChanged
        btnImprimir.Enabled = True
        If lstTipo.SelectedIndex >= 0 Then
            dtpDesde.Enabled = True
            dtpHasta.Enabled = True
            btnVerificar.Enabled = True
        End If
        If lstTipo.SelectedIndex <> 0 Then
            dtpDesde.Value = "01/" + dtpDesde.Value.Month.ToString("00") + "/" + periodo.ToString
            dtpHasta.Value = Date.Parse("01/" + dtpDesde.Value.Month.ToString("00") + "/" + periodo.ToString).AddMonths(1).AddDays(-1)
            btnVerificar.Enabled = False
        End If
        If lstTipo.SelectedIndex = 0 Then
            'dtpDesde.MinDate = "01/01/" + "1990"
            'dtpHasta.MinDate = "01/01/" + "1990"
            'dtpDesde.MaxDate = "31/12/" + periodo.ToString
            'dtpHasta.MaxDate = "31/12/" + periodo.ToString
            dtpDesde.Value = "01/" + Date.Now.Month.ToString + "/" + periodo
            dtpHasta.Value = Date.Now.Day.ToString + "/" + Date.Now.Month.ToString + "/" + periodo
        Else
            'dtpDesde.MinDate = "01/01/" + periodo.ToString
            'dtpHasta.MinDate = "01/01/" + periodo.ToString
            'dtpDesde.MaxDate = "31/12/" + periodo.ToString
            'dtpHasta.MaxDate = "31/12/" + periodo.ToString
            dtpDesde.Value = "01/" + Date.Now.Month.ToString + "/" + periodo
            dtpHasta.Value = Date.Now.Day.ToString + "/" + Date.Now.Month.ToString + "/" + periodo
        End If
        If lstTipo.SelectedIndex = 0 Then
            'CATALOGO
            pnlTipoSolicitud.Enabled = False
            pnlBalanza.Enabled = False
        End If
        If lstTipo.SelectedIndex = 1 Then
            'BALANZA
            pnlTipoSolicitud.Enabled = False
            pnlBalanza.Enabled = True
            cmbTipoEnvio.SelectedIndex = 0
        End If
        If lstTipo.SelectedIndex = 2 Then
            'POLIZAS
            pnlTipoSolicitud.Enabled = True
            pnlBalanza.Enabled = False
            cmbTipoSolicitud.SelectedIndex = 0
        End If
        If lstTipo.SelectedIndex = 3 Then
            'AUXILIAR DE FOLIOS
            pnlTipoSolicitud.Enabled = True
            pnlBalanza.Enabled = False
            cmbTipoSolicitud.SelectedIndex = 0
        End If
        If lstTipo.SelectedIndex = 4 Then
            'AUXILIAR DE CUENTAS
            pnlTipoSolicitud.Enabled = True
            pnlBalanza.Enabled = False
            cmbTipoSolicitud.SelectedIndex = 0
        End If
    End Sub
    Public Sub ComprimirArchivo(ByVal archivoEntrada As String, ByVal archivoSalida As String)
        Dim zipPath As String = ""

        'Guardar archivo en zip
        zipPath = ""
        For i As Integer = 0 To archivoEntrada.Length - 4
            zipPath += archivoEntrada(i)
        Next
        zipPath += "zip"
        archivoSalida = zipPath

        Using sourceFile As FileStream = File.OpenRead(archivoEntrada)
            Using destFile As FileStream = File.Create(archivoSalida)
                Using compStream As GZipStream = New GZipStream(destFile, CompressionMode.Compress)

                    Dim data(sourceFile.Length) As Byte
                    sourceFile.Read(data, 0, data.Length)
                    compStream.Write(data, 0, data.Length)
                End Using
            End Using
        End Using

        'Eliminar XML
        My.Computer.FileSystem.DeleteFile(archivoEntrada)
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        btnImprimir.Enabled = False
        txtNumOrden.BackColor = Color.White
        txtNumTramite.BackColor = Color.White
        Me.Cursor = Cursors.WaitCursor
        Dim mes As String = MonthName(dtpDesde.Value.Month, True).ToUpper
        Dim nombreArchivo As String = ""
        If My.Computer.FileSystem.DirectoryExists("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes) = False Then
            My.Computer.FileSystem.CreateDirectory("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes)
        End If
        Try

            If lstTipo.SelectedIndex = 0 Then
                'CATALOGO
                Dim contados As Integer = p.verificarCuentas(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                Dim sinagrupar As Integer = p.CuentasSinAgrupar
                If sinagrupar <> 0 Then
                    MsgBox("Hay " + sinagrupar.ToString + " cuentas sin agrupar.", MsgBoxStyle.Critical, GlobalNombreApp)
                    Exit Sub
                End If
                If MsgBox("Del " + dtpDesde.Value.ToString("dd/MM/yyyy") + " al " + dtpHasta.Value.ToString("dd/MM/yyyy") + vbCrLf + contados.ToString + " cuentas han sido agregadas." + vbCrLf + "¿Desea generar el XML?", MsgBoxStyle.YesNo, GlobalNombreApp) = DialogResult.Yes Then
                    nombreArchivo = s.RFC + dtpDesde.Value.Year.ToString + dtpDesde.Value.Month.ToString("00") + "CT.xml"
                    Dim xmldoc As New System.Xml.XmlDocument
                    xmldoc = C.generaXML(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                    xmldoc.Save("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes + "\" + nombreArchivo)
                    'xmldoc.Save("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes + "\" + nombreArchivo + ".gz")
                    'ComprimirArchivo("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes + "\" + nombreArchivo + ".gz", "")
                    PopUp("Guardado", 90)
                End If
            End If


            If lstTipo.SelectedIndex = 1 Then
                'BALANZA
                Dim fechamod As String = ""

                If cmbTipoEnvio.SelectedIndex = 1 Then
                    fechamod = dtpFechaModificacion.Value.Year.ToString + "-" + dtpFechaModificacion.Value.Month.ToString("00") + "-" + dtpFechaModificacion.Value.Day.ToString("00")
                    'SaveFileDialog1.FileName = s.RFC + "_" + dtpDesde.Value.Year.ToString + "_" + dtpDesde.Value.Month.ToString("00") + "_" + "BC"
                    If CheckBox1.Checked = False Then
                        nombreArchivo = s.RFC + dtpDesde.Value.Year.ToString + dtpDesde.Value.Month.ToString("00") + "BC.xml"
                    Else
                        nombreArchivo = s.RFC + dtpDesde.Value.Year.ToString + "13" + "BC.xml"
                    End If
                Else
                    ' SaveFileDialog1.FileName = s.RFC + "_" + dtpDesde.Value.Year.ToString + "_" + dtpDesde.Value.Month.ToString("00") + "_" + "BN"
                    If CheckBox1.Checked = False Then
                        nombreArchivo = s.RFC + dtpDesde.Value.Year.ToString + dtpDesde.Value.Month.ToString("00") + "BN.xml"
                    Else
                        nombreArchivo = s.RFC + dtpDesde.Value.Year.ToString + "13" + "BN.xml"
                    End If
                    End If

                    ' SaveFileDialog1.Filter = "XML files (*.xml)|*.xml"
                    ' Dim savRes As DialogResult = SaveFileDialog1.ShowDialog()
                    ' If (savRes = DialogResult.OK) Then
                    Dim xmldoc As New System.Xml.XmlDocument
                    xmldoc = p.generaXMLBalanza(dtpDesde.Value.Month.ToString("00"), cmbTipoEnvio.Text.Chars(0), fechamod, "", "", "", CheckBox1.Checked)
                    xmldoc.Save("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes + "\" + nombreArchivo)
                    'ComprimirArchivo("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes + "\" + nombreArchivo, "")
                    PopUp("Guardado", 90)
                    'End If

            End If
            If lstTipo.SelectedIndex = 2 Then
                'POLIZAS
                Dim errores As Boolean = False
                Dim er As String = ""
                If (cmbTipoSolicitud.SelectedIndex = 0 Or cmbTipoSolicitud.SelectedIndex = 1) And (txtNumOrden.Text = "" Or txtNumOrden.TextLength <> 13) Then
                    errores = True
                    If txtNumOrden.TextLength <> 13 Then
                        er += "El Número de Orden debe contener 13 caractéres."
                    Else
                        er += "Debe indicar un Número de Orden."
                    End If
                End If
                If (cmbTipoSolicitud.SelectedIndex = 2 Or cmbTipoSolicitud.SelectedIndex = 3) And (txtNumTramite.Text = "" Or txtNumTramite.TextLength <> 10) Then
                    errores = True
                    If txtNumTramite.TextLength <> 10 Then
                        er += "El Número de Trámite debe contener 10 caractéres."
                    Else
                        er += "Debe indicar un Número de Trámite."
                    End If
                End If
                If errores = False Then


                    ' SaveFileDialog1.FileName = s.RFC + "_" + dtpDesde.Value.Year.ToString + "_" + dtpDesde.Value.Month.ToString("00") + "_" + "PL"
                    nombreArchivo = s.RFC + dtpDesde.Value.Year.ToString + dtpDesde.Value.Month.ToString("00") + "PL.xml"
                    '  SaveFileDialog1.Filter = "XML files (*.xml)|*.xml"
                    '  Dim savRes As DialogResult = SaveFileDialog1.ShowDialog()
                    ' If (savRes = DialogResult.OK) Then
                    Dim xmldoc As New System.Xml.XmlDocument
                    xmldoc = p.GeneraxmlPolizas(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), cmbTipoSolicitud.Text.Chars(0) + cmbTipoSolicitud.Text.Chars(1), txtNumOrden.Text, txtNumTramite.Text, "", "", "")
                    xmldoc.Save("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes + "\" + nombreArchivo)
                    'ComprimirArchivo("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes + "\" + nombreArchivo, "")
                    PopUp("Guardado", 90)
                    'End If
                Else
                    MsgBox(er, MsgBoxStyle.Critical, GlobalNombreApp)
                    If cmbTipoSolicitud.SelectedIndex = 0 Or cmbTipoSolicitud.SelectedIndex = 1 Then
                        txtNumOrden.BackColor = Color.Coral
                    Else
                        txtNumTramite.BackColor = Color.Coral

                    End If

                End If
            End If

            If lstTipo.SelectedIndex = 4 Then
                'Auxiliar de cuentas
                Dim errores As Boolean = False
                Dim er As String = ""
                If (cmbTipoSolicitud.SelectedIndex = 0 Or cmbTipoSolicitud.SelectedIndex = 1) And (txtNumOrden.Text = "" Or txtNumOrden.TextLength <> 13) Then
                    errores = True
                    If txtNumOrden.TextLength <> 13 Then
                        er += "El Número de Orden debe contener 13 caractéres."
                    Else
                        er += "Debe indicar un Número de Orden."
                    End If

                End If
                If (cmbTipoSolicitud.SelectedIndex = 2 Or cmbTipoSolicitud.SelectedIndex = 3) And (txtNumTramite.Text = "" Or txtNumTramite.TextLength <> 10) Then
                    errores = True
                    If txtNumTramite.TextLength <> 10 Then
                        er += "El Número de Trámite debe contener 10 caractéres."
                    Else
                        er += "Debe indicar un Número de Trámite."
                    End If

                End If
                If errores = False Then


                    'SaveFileDialog1.FileName = s.RFC + "_" + dtpDesde.Value.Year.ToString + "_" + dtpDesde.Value.Month.ToString("00") + "_" + "XC"
                    nombreArchivo = s.RFC + dtpDesde.Value.Year.ToString + dtpDesde.Value.Month.ToString("00") + "XC.xml"
                    ' SaveFileDialog1.Filter = "XML files (*.xml)|*.xml"
                    ' Dim savRes As DialogResult = SaveFileDialog1.ShowDialog()
                    ' If (savRes = DialogResult.OK) Then
                    Dim xmldoc As New System.Xml.XmlDocument
                    xmldoc = p.xmlAuxiliarcuentas(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), cmbTipoSolicitud.Text.Chars(0) + cmbTipoSolicitud.Text.Chars(1), txtNumOrden.Text, txtNumTramite.Text, "", "", "")
                    xmldoc.Save("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes + "\" + nombreArchivo)
                    'ComprimirArchivo("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes + "\" + nombreArchivo, "")
                    PopUp("Guardado", 90)
                    'End If
                Else
                    MsgBox(er, MsgBoxStyle.Critical, GlobalNombreApp)
                    If cmbTipoSolicitud.SelectedIndex = 0 Or cmbTipoSolicitud.SelectedIndex = 1 Then
                        txtNumOrden.BackColor = Color.Coral
                    Else
                        txtNumTramite.BackColor = Color.Coral

                    End If

                End If
            End If

            If lstTipo.SelectedIndex = 3 Then
                'Auxiliar de folios
                Dim errores As Boolean = False
                Dim er As String = ""
                If (cmbTipoSolicitud.SelectedIndex = 0 Or cmbTipoSolicitud.SelectedIndex = 1) And (txtNumOrden.Text = "" Or txtNumOrden.TextLength <> 13) Then
                    errores = True
                    If txtNumOrden.TextLength <> 13 Then
                        er += "El Número de Orden debe contener 13 caractéres."
                    Else
                        er += "Debe indicar un Número de Orden."
                    End If

                End If
                If (cmbTipoSolicitud.SelectedIndex = 2 Or cmbTipoSolicitud.SelectedIndex = 3) And (txtNumTramite.Text = "" Or txtNumTramite.TextLength <> 10) Then
                    errores = True
                    If txtNumTramite.TextLength <> 10 Then
                        er += "El Número de Trámite debe contener 10 caractéres."
                    Else
                        er += "Debe indicar un Número de Trámite."
                    End If

                End If
                If errores = False Then


                    'SaveFileDialog1.FileName = s.RFC + "_" + dtpDesde.Value.Year.ToString + "_" + dtpDesde.Value.Month.ToString("00") + "_" + "XF"
                    nombreArchivo = s.RFC + dtpDesde.Value.Year.ToString + dtpDesde.Value.Month.ToString("00") + "XF.xml"
                    ' SaveFileDialog1.Filter = "XML files (*.xml)|*.xml"
                    ' Dim savRes As DialogResult = SaveFileDialog1.ShowDialog()
                    ' If (savRes = DialogResult.OK) Then
                    Dim xmldoc As New System.Xml.XmlDocument
                    xmldoc = p.xmlAuxiliarFolios(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), cmbTipoSolicitud.Text.Chars(0) + cmbTipoSolicitud.Text.Chars(1), txtNumOrden.Text, txtNumTramite.Text, "", "", "")
                    xmldoc.Save("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes + "\" + nombreArchivo)
                    'ComprimirArchivo("C:\SIS_FE\REPORTES XML" + dtpDesde.Value.Year.ToString + "\" + mes + "\" + nombreArchivo, "")
                    PopUp("Guardado", 90)
                    'End If
                Else
                    MsgBox(er, MsgBoxStyle.Critical, GlobalNombreApp)
                    If cmbTipoSolicitud.SelectedIndex = 0 Or cmbTipoSolicitud.SelectedIndex = 1 Then
                        txtNumOrden.BackColor = Color.Coral
                    Else
                        txtNumTramite.BackColor = Color.Coral

                    End If

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
        End Try
        btnImprimir.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub dtpDesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpDesde.ValueChanged
        'If dtpHasta.Value < dtpDesde.Value Then
        '    dtpHasta.Value = dtpDesde.Value
        'End If
        ' dtpHasta.Value = Date.Parse("01" + "/" + dtpDesde.Value.Month.ToString("00") + dtpDesde.Value.Year.ToString).AddMonths(+1).AddDays(-1)
        ' If lstTipo.SelectedIndex <> 0 Then
        'dtpDesde.Value = "01/" + dtpDesde.Value.Month.ToString("00") + "/" + periodo.ToString
        dtpHasta.Value = Date.Parse("01/" + dtpDesde.Value.Month.ToString("00") + "/" + dtpDesde.Value.Year.ToString).AddMonths(1).AddDays(-1)
        '  End If
    End Sub

    Private Sub dtpHasta_ValueChanged(sender As Object, e As EventArgs) Handles dtpHasta.ValueChanged
        'If dtpHasta.Value < dtpDesde.Value Then
        '    dtpHasta.Value = dtpDesde.Value
        'End If
        'dtpHasta.Value = Date.Parse("01" + "/" + dtpDesde.Value.Month.ToString("00") + dtpDesde.Value.Year.ToString).AddMonths(+1).AddDays(-1)
        ' If lstTipo.SelectedIndex <> 0 Then
        dtpHasta.Value = Date.Parse("01/" + dtpHasta.Value.Month.ToString("00") + "/" + dtpHasta.Value.Year.ToString).AddMonths(1).AddDays(-1)
        'dtpDesde.Value = Date.Parse("01/" + dtpHasta.Value.Month.ToString("00") + "/" + periodo.ToString)
        ' End If
    End Sub

    Private Sub cmbTipoSolicitud_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoSolicitud.SelectedIndexChanged
        If cmbTipoSolicitud.SelectedIndex = 0 Or cmbTipoSolicitud.SelectedIndex = 1 Then
            lblNumOrden.Enabled = True
            txtNumOrden.Enabled = True
            lblNumTramite.Enabled = False
            txtNumTramite.Enabled = False
        Else
            lblNumOrden.Enabled = False
            txtNumOrden.Enabled = False
            lblNumTramite.Enabled = True
            txtNumTramite.Enabled = True
        End If
    End Sub

    Private Sub txtNumTramite_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNumTramite.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbTipoEnvio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoEnvio.SelectedIndexChanged
        If cmbTipoEnvio.SelectedIndex = 0 Then
            lblFechaBalanza.Enabled = False
            dtpFechaModificacion.Enabled = False
        Else
            lblFechaBalanza.Enabled = True
            dtpFechaModificacion.Enabled = True
        End If
    End Sub

    Private Sub btnVerificar_Click(sender As Object, e As EventArgs) Handles btnVerificar.Click
        Dim contados As Integer = p.verificarCuentas(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        MsgBox("Del " + dtpDesde.Value.ToString("dd/MM/yyyy") + " al " + dtpHasta.Value.ToString("dd/MM/yyyy") + vbCrLf + contados.ToString + " cuentas han sido agregadas.", MsgBoxStyle.OkOnly, GlobalNombreApp)
    End Sub

    Private Sub txtNumOrden_Leave(sender As Object, e As EventArgs) Handles txtNumOrden.Leave
        If txtNumOrden.Text <> "" And txtNumOrden.Text.Length <> 13 Then
            MsgBox("El número de orden debe contener 13 caracteres.", MsgBoxStyle.OkOnly, GlobalNombreApp)
            txtNumOrden.Focus()
        End If
    End Sub

    Private Sub txtNumTramite_Leave(sender As Object, e As EventArgs) Handles txtNumTramite.Leave
        If txtNumTramite.Text <> "" And txtNumTramite.Text.Length <> 10 Then
            MsgBox("El número de trámite debe contener 10 caracteres.", MsgBoxStyle.OkOnly, GlobalNombreApp)
            txtNumTramite.Focus()
        End If
    End Sub

    Private Sub pnlTipoSolicitud_Paint(sender As Object, e As PaintEventArgs) Handles pnlTipoSolicitud.Paint

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class