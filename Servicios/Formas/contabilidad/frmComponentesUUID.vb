Public Class frmComponentesUUID
    Dim p As New dbContabilidadPolizas(MySqlcon)
    Public tabla As New DataTable
    Private RFC As String = ""
    Dim Ruta As String
    Dim Tipo As String
    Dim prov As New dbproveedores(MySqlcon)
    Dim Clientes As New dbClientes(MySqlcon)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    'Private List1 As ListBox
    Public Sub New(pRutaXmls As String, PTipoPoliza As String)

        ' This call is required by the designer.
        InitializeComponent()
        Ruta = pRutaXmls
        Tipo = PTipoPoliza
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmComponentesUUID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try

        p.llenaDatosConfig()
        tabla.Columns.Add("Selec")
        tabla.Columns.Add("UUID")
        tabla.Columns.Add("Monto")
        tabla.Columns.Add("RFC")
        tabla.Columns.Add("Fecha")
        tabla.Columns.Add("Fecha2")
        tabla.Columns.Add("usado")
        If Ruta = "" Then
            MsgBox("No ha especificado ninguna ruta hacia la carpeta contenedora de los UUID." + vbCrLf + "Diríjase a Configuración para determinar una carpeta", MsgBoxStyle.OkOnly, GlobalNombreApp)
        Else
            'buscar("", Ruta, Tipo)
            carpetas(0)
        End If
    End Sub
    Private Sub carpetas(ByVal pID As Integer)
        Dim carpeta As New IO.DirectoryInfo(Ruta)
        Dim nombre3 As String = ""
        For Each D As IO.DirectoryInfo In carpeta.GetDirectories
            If txtFolioCliente.Text = "" Then
                buscar("", Ruta + "\" + D.Name, Tipo)
            Else
                buscar(RFC, Ruta + "\" + D.Name, Tipo)
            End If

            Dim carpeta2 As New IO.DirectoryInfo(Ruta + "\" + D.Name)
            Try
                For Each D2 As IO.DirectoryInfo In carpeta2.GetDirectories
                    If pID = 0 Then
                        buscar("", Ruta + "\" + D.Name + "\" + D2.Name, Tipo)
                    Else
                        buscar(RFC, Ruta + "\" + D.Name + "\" + D2.Name, Tipo)
                    End If
                    nombre3 = D2.Name
                Next

                Dim carpeta3 As New IO.DirectoryInfo(Ruta + "\" + D.Name + "\" + nombre3)

                For Each D3 As IO.DirectoryInfo In carpeta3.GetDirectories
                    If pID = 0 Then
                        buscar("", Ruta + "\" + D.Name + "\" + nombre3 + "\" + D3.Name, Tipo)
                    Else
                        buscar(RFC, Ruta + "\" + D.Name + "\" + nombre3 + "\" + D3.Name, Tipo)
                    End If
                Next
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try

        Next
        Dim view As DataView = New DataView(tabla)
        view.Sort = "Fecha2"
        dgvCompro.DataSource = view


    End Sub
    Private Sub buscar(ByVal pFolio As String, ByVal pRuta As String, pTipoPoliza As String)
        Dim Files As String(), File As String, files0(1) As String
        Try
            Files = IO.Directory.GetFiles(pRuta, "*.xml")
        Catch ex As Exception
            Files = files0
        End Try

        Dim UUID As String
        If pRuta = Ruta Then
            tabla.Clear()
            dgvCompro.DataSource = tabla
        End If

        Try
            If pFolio = "" Then
                For Each File In Files
                    If File <> "" Then
                        Try
                            Dim xmldoc As New Xml.XmlDocument
                            xmldoc.Load(File)
                            UUID = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value

                            Dim dr As DataRow
                            dr = tabla.NewRow()
                            dr("Selec") = 0
                            dr("UUID") = UUID
                            dr("Monto") = xmldoc.Item("cfdi:Comprobante").Attributes("total").Value
                            dr("RFC") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value
                            dr("Fecha") = Date.Parse(xmldoc.Item("cfdi:Comprobante").Attributes("fecha").Value).ToString("dd/MM/yyyy")
                            dr("Fecha2") = Date.Parse(xmldoc.Item("cfdi:Comprobante").Attributes("fecha").Value).ToString("yyyy/MM/dd")
                            If p.contadorUUID(UUID) Then
                                dr("usado") = 1
                            Else
                                dr("usado") = 0
                            End If
                            tabla.Rows.Add(dr)
                            'End If
                        Catch ex As Exception

                        End Try

                    End If
                Next
            Else
                Dim RFCTemp As String
                For Each File In Files
                    If File <> "" Then
                        Try
                            Dim xmldoc As New Xml.XmlDocument
                            xmldoc.Load(File)
                            UUID = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                            If pTipoPoliza = "E" Then
                                RFCTemp = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value
                            Else
                                RFCTemp = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Receptor").Attributes("rfc").Value
                            End If
                            If RFC = RFCTemp Then
                                Dim dr As DataRow
                                dr = tabla.NewRow()
                                dr("Selec") = 0
                                dr("UUID") = UUID
                                dr("Monto") = xmldoc.Item("cfdi:Comprobante").Attributes("total").Value
                                If pTipoPoliza = "E" Then
                                    dr("RFC") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value
                                Else
                                    dr("RFC") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Receptor").Attributes("rfc").Value
                                End If
                                dr("Fecha") = Date.Parse(xmldoc.Item("cfdi:Comprobante").Attributes("fecha").Value).ToString("dd/MM/yyyy")
                                dr("Fecha2") = Date.Parse(xmldoc.Item("cfdi:Comprobante").Attributes("fecha").Value).ToString("yyyy/MM/dd")
                                If p.contadorUUID(UUID) Then
                                    dr("usado") = 1
                                Else
                                    dr("usado") = 0
                                End If
                                tabla.Rows.Add(dr)
                            End If
                        Catch ex As Exception

                        End Try

                    End If
                Next

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
        End Try

        dgvCompro.DataSource = tabla
        'dgvCompro.Columns("usado").Visible = False
    End Sub
    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub dgvCompro_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompro.CellClick
        If e.ColumnIndex <> 0 Then Exit Sub
        Try
            If dgvCompro.Rows(e.RowIndex).Cells(0).Value = 0 Then
                dgvCompro.Rows(e.RowIndex).Cells(0).Value = 1
            Else
                dgvCompro.Rows(e.RowIndex).Cells(0).Value = 0
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        BuscaClienteBoton()
       
    End Sub
    Private Sub BuscaClienteBoton()
        If Tipo = "E" Then
            Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.ProveedorDiot, 0, False, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                txtFolioCliente.Text = B.Proveedor.Clave
                txtNombre.Text = B.Proveedor.Nombre
                RFC = B.Proveedor.RFC
                B.Dispose()
                buscar(RFC, Ruta, Tipo)
                carpetas(B.Proveedor.ID)
                txtFolioCliente.Focus()
            End If
        Else
            Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                txtFolioCliente.Text = B.Cliente.Clave
                txtNombre.Text = B.Cliente.Nombre
                RFC = B.Cliente.RFC
                B.Dispose()
                buscar(RFC, Ruta, Tipo)
                carpetas(B.Cliente.ID)
                txtFolioCliente.Focus()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        BotonBuscar()
    End Sub
    Private Sub BotonBuscar()
        If Tipo = "E" Then
            prov.BuscaProveedorDIOT(txtFolioCliente.Text)
            If prov.Nombre = "" Then
                'prov.ID = 0
                txtNombre.Text = prov.Nombre
                RFC = prov.RFC
                'buscar(RFC, Ruta, Tipo)
                carpetas(prov.ID)
            Else
                txtNombre.Text = ""
                RFC = ""
                'buscar("", Ruta, Tipo)
                carpetas(0)
            End If
        Else
            If Clientes.BuscaCliente(txtFolioCliente.Text) Then
                txtNombre.Text = Clientes.Nombre
                RFC = Clientes.RFC
                'buscar(RFC, Ruta, Tipo)
                carpetas(Clientes.ID)
            Else
                txtNombre.Text = ""
                RFC = ""
                'buscar("", Ruta, Tipo)
                carpetas(0)
            End If
        End If
        txtFolioCliente.Focus()
    End Sub
    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        txtFolioCliente.Text = ""
        txtNombre.Text = ""
        prov.ID = 0
        'buscar("", Ruta, Tipo)
        carpetas(0)
        txtFolioCliente.Focus()
    End Sub

    Private Sub txtFolioCliente_TextChanged(sender As Object, e As EventArgs) Handles txtFolioCliente.TextChanged
        BotonBuscar()
    End Sub

    Private Sub dgvCompro_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompro.CellContentClick

    End Sub

    Private Sub dgvCompro_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvCompro.CellFormatting
        If dgvCompro.Item("usado", e.RowIndex).Value = 1 Then
            e.CellStyle.BackColor = ColorVerde
        End If
    End Sub
End Class