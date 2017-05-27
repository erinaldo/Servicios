Public Class frmValidacion
    Dim val As New dbValidacionXML(MySqlcon)
    Dim consulta As Integer = 0
    Dim RutaValidador As String
    Dim RutaAValidar As String
    Dim RutaValidadas As String
    Dim tablaNoValidadas As New DataTable
    Dim tablaValidadas As New DataTable
    Dim tablaReporte As DataTable
    Dim tablaReporteAux As DataTable
    Dim consultaFacturas As dbConsultaFacturas
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'txtDireccionCarpeta.Text = My.Settings.direccionValid
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        consultaFacturas = New dbConsultaFacturas(MySqlcon)
        configuraTabla(tablaValidadas)
        configuraTabla(tablaNoValidadas)
        tablaReporte = New DataTable()
        tablaReporteAux = New DataTable()
        configuraTablaReporte(tablaReporte)
        configuraTablaReporte(tablaReporteAux)
        dgvValidadas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvNoValidadas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Dim SA As New dbSucursalesArchivos()
        RutaValidador = SA.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.Validador, False)
        RutaAValidar = SA.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.AValidadar, False)
        RutaValidadas = SA.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.Validadas, False)
        If RutaValidador = "" Then
            RutaValidador = Application.StartupPath
        End If
        If RutaAValidar = "" Then
            RutaAValidar = "C:\SIS_FE\RECEPCION"
        End If
        If RutaValidadas = "" Then
            RutaValidadas = "C:\SIS_FE\RECEPCION"
        End If
        If My.Computer.FileSystem.DirectoryExists(RutaAValidar) = False Then
            My.Computer.FileSystem.CreateDirectory(RutaAValidar)
        End If
        Label1.Text += RutaAValidar
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            dgvNoValidadas.DataSource = Nothing
            dgvValidadas.DataSource = Nothing
            tablaValidadas = New DataTable
            tablaNoValidadas = New DataTable
            tablaReporte = New DataTable
            tablaReporteAux = New DataTable
            configuraTabla(tablaValidadas)
            configuraTabla(tablaNoValidadas)
            configuraTablaReporte(tablaReporte)
            configuraTablaReporte(tablaReporteAux)
            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            'tablaReporte = New DataTable
            'configuraTablaReporte()
            Dim O As New dbOpciones(MySqlcon)
            Dim RutaXML As String = ""
            Dim RutaPDF As String = ""
            Dim extension As String
            Dim Resultado As String
            Dim ProcesoInfo As New ProcessStartInfo()
            Dim totalValidadas As Integer = 0
            Dim totalNoValidadas As Integer = 0

            Dim xmldoc As New Xml.XmlDocument
            Dim UUID As String = ""
            Dim RFC As String = ""
            Dim Monto As Double = 0
            Dim Nombre As String = ""
            Dim Serie As String = ""
            Dim Folio As String = ""
            Dim FechaCFDI As Date
            Dim Archivos() As String
            Dim ArchivosNV() As String
            Dim Cont As Integer = 1
            btnBuscar.Enabled = False
            Dim nombreArchivo As String = ""
            'My.Settings.direccionValid = txtDireccionCarpeta.Text
            'txtResultados.Text = "---------------------------------------------------------------------------------------------------------------------------------------------------------" + vbCrLf + "                                                                     Resultados" + vbCrLf + "---------------------------------------------------------------------------------------------------------------------------------------------------------" + vbCrLf
            If RutaAValidar <> "" Then
                Dim aux As New List(Of String)

                If My.Computer.FileSystem.DirectoryExists(RutaValidadas + "\NO VALIDADAS") Then
                    ArchivosNV = IO.Directory.GetFiles(RutaValidadas + "\NO VALIDADAS", "*.xml", IO.SearchOption.AllDirectories)
                    For Each a As String In ArchivosNV
                        nombreArchivo = My.Computer.FileSystem.GetName(a)
                        My.Computer.FileSystem.MoveFile(a, RutaAValidar + "\" + nombreArchivo, True)
                    Next
                End If
                If My.Computer.FileSystem.DirectoryExists(RutaValidadas + "\NO VALIDADAS") Then
                    ArchivosNV = IO.Directory.GetFiles(RutaValidadas + "\NO VALIDADAS", "*.pdf", IO.SearchOption.AllDirectories)
                    For Each a As String In ArchivosNV
                        nombreArchivo = My.Computer.FileSystem.GetName(a)
                        My.Computer.FileSystem.MoveFile(a, RutaAValidar + "\" + nombreArchivo, True)
                    Next
                End If
                'aux.AddRange(IO.Directory.GetFiles(RutaAValidar, "*.xml"))
                Archivos = IO.Directory.GetFiles(RutaAValidar, "*.xml")
                'Archivos = aux.ToArray
                If ProgressBar1.Value > 0 Then
                    ProgressBar1.Value = 0
                End If
                ProgressBar1.Maximum = Archivos.Length
                Label3.Text = "XML Procesados: 0 de " + ProgressBar1.Maximum.ToString
                'My.Computer.FileSystem.GetFiles(RutaAValidar, FileIO.SearchOption.SearchTopLevelOnly, {"*.xml"})
                'Dim i As Integer = 1
                For Each foundFile As String In Archivos
                    Dim rr = tablaReporte.NewRow()
                    RutaXML = foundFile
                    extension = RutaXML.Substring(RutaXML.Length - 3, 3)
                    If extension.ToUpper = "XML" Then
                        RutaPDF = RutaXML.Remove(RutaXML.Length() - 3, 3)
                        RutaPDF += "pdf"
                        If My.Computer.FileSystem.FileExists(RutaPDF) = False Then
                            RutaPDF = ""
                            extension = "XML           "
                        Else
                            extension = "PDF - XML "
                        End If
                        Dim P As New System.Diagnostics.Process()
                        P.StartInfo.FileName = RutaValidador + "\ValidaSAT_MSDOS.exe"
                        P.StartInfo.RedirectStandardInput = False
                        P.StartInfo.RedirectStandardOutput = True
                        P.StartInfo.UseShellExecute = False
                        P.StartInfo.CreateNoWindow = True
                        If RutaPDF <> "" Then
                            P.StartInfo.Arguments = S.RFC + " " + O._ApiKey + " " + RutaXML + " " + RutaPDF
                        Else
                            P.StartInfo.Arguments = S.RFC + " " + O._ApiKey + " " + RutaXML
                        End If
                        P.Start()
                        Resultado = P.StandardOutput.ReadToEnd()
                        P.WaitForExit()
                        P.Close()
                        P.Dispose()
                        'If i Mod 2 = 0 Then
                        '    Resultado = "N"
                        'Else
                        '    Resultado = "S"
                        'End If
                        'obtener datos XML
                        xmldoc.Load(RutaXML)
                        Try
                            UUID = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                            Monto = xmldoc.Item("cfdi:Comprobante").Attributes("total").Value
                            RFC = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value
                            Nombre = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("nombre").Value
                            FechaCFDI = CDate(Replace(Replace(xmldoc.Item("cfdi:Comprobante").Attributes("fecha").Value, "T", " "), "-", "/"))
                            rr("UUID") = UUID
                            rr("Importe Total") = Format(CDbl(Monto), O._formatoTotal)
                            rr("RFC Emisor") = RFC
                            rr("Nombre Emisor") = Nombre
                            rr("Fecha Emisión") = FechaCFDI.ToString
                            rr("Versión") = xmldoc.Item("cfdi:Comprobante").Attributes("version").Value
                            rr("Tipo Comprobante") = xmldoc.Item("cfdi:Comprobante").Attributes("tipoDeComprobante").Value
                            rr("Certificado SAT") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                            rr("Certificado Emisor") = xmldoc.Item("cfdi:Comprobante").Attributes("noCertificado").Value
                            rr("RFC Receptor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Receptor").Attributes("rfc").Value
                            rr("Nombre Receptor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Receptor").Attributes("nombre").Value
                            rr("Fecha Certificación") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                            rr("Resultado de Validación") = Resultado

                        Catch ex As Exception
                            Resultado = "N"
                        End Try

                        Try
                            Serie = xmldoc.Item("cfdi:Comprobante").Attributes("serie").Value
                        Catch ex As Exception
                            Serie = ""
                        End Try
                        Try
                            Folio = xmldoc.Item("cfdi:Comprobante").Attributes("folio").Value.ToString
                        Catch ex As Exception
                            Folio = ""
                        End Try
                        rr("Folio") = Serie + Folio
                        tablaReporte.Rows.Add(rr("Versión"), rr("Tipo Comprobante"), rr("Certificado SAT"), rr("Certificado Emisor"), rr("Fecha Emisión"), rr("Fecha Certificación"), rr("UUID"), rr("Importe Total"), rr("RFC Emisor"), rr("Nombre Emisor"), rr("RFC Receptor"), rr("Nombre Receptor"), rr("Resultado de Validación"), rr("Folio"))
                        Dim r As DataRow
                        'r.Cells.Add(New DataGridViewCell)


                        'txtResultados.Text += "Validando archivo: " + RutaXML + vbCrLf

                        'txtResultados.Text += "COMPROBANTE: " + UUID + " Folio: " + Serie + Folio + " EMISOR: " + RFC + " " + Nombre + vbCrLf
                        'txtResultados.Text += Resultado + vbCrLf
                        If Resultado.Contains("Este CFDI ya fue procesado") Then
                            r = tablaValidadas.NewRow
                            r("UUID") = UUID
                            r("Folio") = Serie + Folio
                            r("Emisor") = RFC
                            r("Nombre") = Nombre
                            r("Resultado") = Resultado
                            tablaValidadas.Rows.Add(r)
                            If tablaValidadas.Rows.Count > 0 Then
                                dgvValidadas.DataSource = tablaValidadas
                                dgvValidadas.CurrentRow.Selected = False
                            End If

                            If My.Computer.FileSystem.DirectoryExists(RutaValidadas + "\VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00")) = False Then
                                My.Computer.FileSystem.CreateDirectory(RutaValidadas + "\VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00"))
                            End If

                            'Mover XML
                            nombreArchivo = My.Computer.FileSystem.GetName(RutaXML)
                            My.Computer.FileSystem.MoveFile(RutaXML, RutaValidadas + "\VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00") + "\" + nombreArchivo, True)

                            If RutaPDF <> "" Then
                                nombreArchivo = My.Computer.FileSystem.GetName(RutaPDF)
                                My.Computer.FileSystem.MoveFile(RutaPDF, RutaValidadas + "\VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00") + "\" + nombreArchivo, True)
                            End If
                            totalValidadas += 1

                            'Continue For
                            'End If
                        ElseIf Resultado.Chars(0) = "N" Then
                            'No validado
                            totalNoValidadas += 1

                            'Mover a no validadas
                            If My.Computer.FileSystem.DirectoryExists(RutaValidadas + "\NO VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00")) = False Then
                                My.Computer.FileSystem.CreateDirectory(RutaValidadas + "\NO VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00"))
                            End If

                            'Mover XML
                            nombreArchivo = My.Computer.FileSystem.GetName(RutaXML)
                            My.Computer.FileSystem.MoveFile(RutaXML, RutaValidadas + "\NO VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00") + "\" + nombreArchivo, True)

                            If RutaPDF <> "" Then
                                nombreArchivo = My.Computer.FileSystem.GetName(RutaPDF)
                                My.Computer.FileSystem.MoveFile(RutaPDF, RutaValidadas + "\NO VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00") + "\" + nombreArchivo, True)
                            End If
                            r = tablaNoValidadas.NewRow
                            r("UUID") = UUID
                            r("Folio") = Serie + Folio
                            r("Emisor") = RFC
                            r("Nombre") = Nombre
                            r("Resultado") = Resultado
                            tablaNoValidadas.Rows.Add(r)
                            If tablaNoValidadas.Rows.Count > 0 Then
                                dgvNoValidadas.DataSource = tablaNoValidadas
                                dgvNoValidadas.CurrentRow.Selected = False
                                dgvNoValidadas.Columns("Folio").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                                dgvNoValidadas.Columns("Resultado").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                            End If
                        Else

                            'validados
                            'Guardarlo en la base de datos
                            val.guardar(UUID, RFC, Monto)

                            r = tablaValidadas.NewRow
                            r("UUID") = UUID
                            r("Folio") = Serie + Folio
                            r("Emisor") = RFC
                            r("Nombre") = Nombre
                            r("Resultado") = Resultado
                            tablaValidadas.Rows.Add(r)
                            If tablaValidadas.Rows.Count > 0 Then
                                dgvValidadas.DataSource = tablaValidadas
                                dgvValidadas.CurrentRow.Selected = False
                                dgvValidadas.Columns("Folio").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                                dgvValidadas.Columns("Resultado").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                            End If

                            If My.Computer.FileSystem.DirectoryExists(RutaValidadas + "\VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00")) = False Then
                                My.Computer.FileSystem.CreateDirectory(RutaValidadas + "\VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00"))
                            End If

                            'Mover XML
                            nombreArchivo = My.Computer.FileSystem.GetName(RutaXML)
                            My.Computer.FileSystem.MoveFile(RutaXML, RutaValidadas + "\VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00") + "\" + nombreArchivo, True)

                            If RutaPDF <> "" Then
                                nombreArchivo = My.Computer.FileSystem.GetName(RutaPDF)
                                My.Computer.FileSystem.MoveFile(RutaPDF, RutaValidadas + "\VALIDADAS\" + Nombre + "\" + FechaCFDI.Year.ToString + "\" + FechaCFDI.Month.ToString("00") + "\" + nombreArchivo, True)
                            End If
                            totalValidadas += 1
                        End If
                    End If
                    ProgressBar1.Value += 1
                    Label3.Text = "XML Procesados " + ProgressBar1.Value.ToString + " de " + ProgressBar1.Maximum.ToString
                    Application.DoEvents()
                    'Threading.Thread.Sleep(2000)
                    'i += 1
                Next
                'txtResultados.Text += vbCrLf + vbCrLf + "TOTAL VALIDADAS:       " + totalValidadas.ToString + vbCrLf + "TOTAL NO VALIDADAS: " + totalNoValidadas.ToString + vbCrLf + vbCrLf + "TOTAL:                            " + (totalNoValidadas + totalValidadas).ToString
            End If
            btnBuscar.Enabled = True
            lblValidadas.Text = totalValidadas
            lblValidadas.Visible = True
            lblNoValidadas.Text = totalNoValidadas
            lblNoValidadas.Visible = True
        Catch ex As Exception
            Label3.Text = "Hubo error."
            ProgressBar1.Value = ProgressBar1.Maximum
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
            btnBuscar.Enabled = True
        End Try

    End Sub



    Private Sub btnConsulta_Click(sender As Object, e As EventArgs) Handles btnConsulta.Click
        Try
            If consulta = 0 Then
                Process.Start(RutaValidador + "\ValidaSAT_Screen.exe")
                consulta = 1
                btnConsulta.Text = "Cerrar Consulta"
            Else
                'cerrar proceso
                Dim Validasat() As Process = Process.GetProcessesByName("ValidaSAT_Screen")
                For Each Proc As Process In Validasat
                    Proc.Kill()
                Next

                btnConsulta.Text = "Consulta"
                consulta = 0
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub configuraTabla()

        dgvValidadas.Columns.Add("UUID", "UUID")
        dgvValidadas.Columns.Add("Folio", "Folio")
        dgvValidadas.Columns.Add("Emisor", "Emisor")
        dgvValidadas.Columns.Add("Nombre", "Nombre")

    End Sub

    Private Sub configuraTabla(ByRef tabla As DataTable)

        tabla.Columns.Add("UUID", GetType(String))
        tabla.Columns.Add("Folio", GetType(String))
        tabla.Columns.Add("Emisor", GetType(String))
        tabla.Columns.Add("Nombre", GetType(String))
        tabla.Columns.Add("Resultado", GetType(String))

    End Sub

    Private Function buscaSubCarpetas(ruta As String, path As String) As String()

        Dim lista As New List(Of String)
        lista.AddRange(IO.Directory.GetFiles(ruta, path))
        Dim aux As String() = IO.Directory.GetDirectories(ruta)
        If aux.Length > 0 Then
            For Each f As String In aux
                lista.AddRange(buscaSubCarpetas(f, path))
            Next
        End If
        Return lista.ToArray()
    End Function

    Private Sub configuraTablaReporte(ByRef tabla As DataTable)
        tabla.Columns.Add("Versión", GetType(String))
        tabla.Columns.Add("Tipo Comprobante", GetType(String))
        tabla.Columns.Add("Certificado SAT", GetType(String))
        tabla.Columns.Add("Certificado Emisor", GetType(String))
        tabla.Columns.Add("Fecha Emisión", GetType(String))
        tabla.Columns.Add("Fecha certificación", GetType(String))
        tabla.Columns.Add("UUID", GetType(String))
        tabla.Columns.Add("Importe Total", GetType(String))
        tabla.Columns.Add("RFC Emisor", GetType(String))
        tabla.Columns.Add("Nombre Emisor", GetType(String))
        tabla.Columns.Add("RFC Receptor", GetType(String))
        tabla.Columns.Add("Nombre Receptor", GetType(String))
        tabla.Columns.Add("Resultado de Validación", GetType(String))
        tabla.Columns.Add("Folio", GetType(String))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ds As New DataSet
        If dgvValidadas.SelectedRows.Count = 0 And dgvNoValidadas.SelectedRows.Count = 0 Then
            tablaReporte.TableName = "Facturas"
            ds.Tables.Add(tablaReporte)
            'tablaReporte.WriteXmlSchema(CurDir() & "\repFacturas.xml")
            Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rep = New repFacturasValidadas
            rep.SetDataSource(ds)
            rep.SetParameterValue("nombreEmpresa", GlobalNombreEmpresa)
            Dim RV As New frmReportes(rep, False)
            RV.Show()
            ds.Tables.Remove(tablaReporte)
        Else
            llenaTablaReporteAux()
            tablaReporteAux.TableName = "Facturas"
            ds.Tables.Add(tablaReporteAux)
            'tablaReporte.WriteXmlSchema(CurDir() & "\repFacturas.xml")
            Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rep = New repFacturasValidadas
            rep.SetDataSource(ds)
            rep.SetParameterValue("nombreEmpresa", GlobalNombreEmpresa)
            Dim RV As New frmReportes(rep, False)
            RV.Show()
            ds.Tables.Remove(tablaReporteAux)
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim f As New frmBuscarFactura(RutaValidadas)
        f.ShowDialog()
    End Sub

    Private Sub llenaTablaReporteAux()
        Dim Archivos() As String
        Dim ArchivosNV() As String = {}
        Dim aux As New List(Of String)
        Dim nombreArchivo As String = ""
        Dim uuid As String = ""
        tablaReporteAux = New DataTable
        configuraTablaReporte(tablaReporteAux)
        Dim rr As DataRow = tablaReporteAux.NewRow
        Dim xmlDoc As New System.Xml.XmlDocument
        Dim resultado As String = ""


        If My.Computer.FileSystem.DirectoryExists(RutaValidadas + "\NO VALIDADAS") Then
            'ArchivosNV = IO.Directory.GetFiles(RutaValidadas + "\NO VALIDADAS", "*.xml", IO.SearchOption.AllDirectories)
            aux.AddRange(IO.Directory.GetFiles(RutaValidadas + "\NO VALIDADAS", "*.xml", IO.SearchOption.AllDirectories))
        End If
        If My.Computer.FileSystem.DirectoryExists(RutaValidadas + "\VALIDADAS") Then
            'ArchivosNV = IO.Directory.GetFiles(RutaValidadas + "\VALIDADAS", "*.xml", IO.SearchOption.AllDirectories)
            aux.AddRange(IO.Directory.GetFiles(RutaValidadas + "\VALIDADAS", "*.xml", IO.SearchOption.AllDirectories))
        End If
        aux.AddRange(ArchivosNV)
        aux.AddRange(IO.Directory.GetFiles(RutaAValidar, "*.xml"))
        Archivos = aux.ToArray()
        For i As Integer = 0 To dgvValidadas.Rows.Count - 1
            If dgvValidadas.Rows(i).Selected Then
                consultaFacturas.imprimir(dgvValidadas.Rows(i).Cells("UUID").Value, tablaReporteAux, dgvValidadas.Rows(i).Cells("Resultado").Value, "", Archivos)
            End If
        Next
        For i As Integer = 0 To dgvNoValidadas.Rows.Count - 1
            If dgvNoValidadas.Rows(i).Selected Then
                consultaFacturas.imprimir(dgvNoValidadas.Rows(i).Cells("UUID").Value, tablaReporteAux, dgvNoValidadas.Rows(i).Cells("Resultado").Value, "", Archivos)
            End If
        Next
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If dgvValidadas.Rows.Count > 0 Then
            For i As Integer = 0 To dgvValidadas.Rows.Count - 1
                dgvValidadas.Rows(i).Selected = False
            Next
        End If
        If dgvNoValidadas.Rows.Count > 0 Then
            For i As Integer = 0 To dgvNoValidadas.Rows.Count - 1
                dgvNoValidadas.Rows(i).Selected = False
            Next
        End If
    End Sub
End Class