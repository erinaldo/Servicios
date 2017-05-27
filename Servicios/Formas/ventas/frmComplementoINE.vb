Public Class frmComplementoINE
    Private comites() As String = {"Ejecutivo Nacional", "Ejecutivo Estatal", "Directivo Estatal"}
    Private ambitos() As String = {"Local", "Federal"}
    Private entidades() As String = {"AGU", "BCN", "BCS", "CAM", "CHP", "CHH", "COA", "COL", "DIF", "DUR", "GUA", "GRO",
                                     "HID", "JAL", "MEX", "MIC", "MOR", "NAY", "NLE", "OAX", "PUE", "QTO", "ROO", "SLP",
                                     "SIN", "SON", "TAB", "TAM", "TLA", "VER", "YUC", "ZAC"}
    Private procesos() As String = {"Ordinario", "Precampaña", "Campaña"}
    Private complemento As dbComplementoIne
    Private entidadesINE As dbComplementoIneEntidad
    Private clavesConta As dbComplementoIneContabilidad
    Public XML As String = ""
    Public cadenaOriginal As String = ""
    Private idFactura As Integer
    Private idClave As Integer
    Private nueva As Boolean = True

    Public Sub New(ByVal idFactura As Integer)
        InitializeComponent()
        Me.idFactura = idFactura

    End Sub
    Private Sub frmComplementoINE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        complemento = New dbComplementoIne(MySqlcon)
        entidadesINE = New dbComplementoIneEntidad(MySqlcon)
        clavesConta = New dbComplementoIneContabilidad(MySqlcon)
        llenaListas(comboAmbito, ambitos)
        llenaListas(comboComite, comites)
        llenaListas(comboEntidad, entidades)
        llenaListas(comboProcesos, procesos)
        If complemento.buscar(idFactura) Then
            llenaDatosComplemento()
        Else
            complemento.guardar(comboComite.Text, comboProcesos.Text, idFactura, "")
            complemento.buscar(idFactura)
        End If
    End Sub

    Private Sub llenaListas(ByRef combo As ComboBox, ByVal datos As String())
        combo.Items.Clear()
        For Each s As String In datos
            combo.Items.Add(s)
        Next
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If TextBox2.Visible = True Then
            If IsNumeric(TextBox2.Text) = False Then
                MsgBox("El Id de contabilidad debe ser un valor numérico.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
        End If
        complemento.actualizar(complemento.id, comboComite.Text, comboProcesos.Text, idFactura, TextBox2.Text)
        complemento.buscar(idFactura)
        If entidadesINE.buscaComplementoEntidad(complemento.id, comboEntidad.Text) Then
            entidadesINE.actualizar(entidadesINE.id, comboEntidad.Text, comboAmbito.Text, complemento.id)
        Else
            entidadesINE.guardar(comboEntidad.Text, comboAmbito.Text, complemento.id)
            entidadesINE.buscaComplementoEntidad(complemento.id, comboEntidad.Text)
        End If
        If TextBox1.Text <> "" Then
            If clavesConta.buscaEntidadClave(entidadesINE.id, CInt(TextBox1.Text)) Then
                clavesConta.modificar(clavesConta.id, CInt(TextBox1.Text), entidadesINE.id)
            Else
                clavesConta.guardar(CInt(TextBox1.Text), entidadesINE.id)
            End If
        End If
        complemento.version = "1.1"
        XML = complemento.crearXML
        cadenaOriginal = complemento.cadenaOriginal
        Dispose()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Dispose()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnClave.Click
        If TextBox1.Text <> "" Then
            If nueva Then
                Dim idEntidad As Integer = -1
                If entidadesINE.buscaComplementoEntidad(complemento.id, comboEntidad.Text) Then
                    clavesConta.guardar(CInt(TextBox1.Text), entidadesINE.id)
                    TextBox1.Text = ""
                    llenaGrid()
                Else
                    entidadesINE.guardar(comboEntidad.Text, comboAmbito.Text, complemento.id)
                    entidadesINE.buscaComplementoEntidad(complemento.id, comboEntidad.Text)
                    clavesConta.guardar(CInt(TextBox1.Text), entidadesINE.id)
                    TextBox1.Text = ""
                    llenaGrid()
                End If
            Else
                clavesConta.modificar(idClave, CInt(TextBox1.Text), entidadesINE.id)
                nueva = True
                btnClave.Text = "Agregar Clave"
                TextBox1.Text = ""
                llenaGrid()
            End If
        Else
            MsgBox("Debe indicar la clave de Contabilidad.")
        End If
    End Sub

    Private Sub llenaGrid()
        If entidadesINE.buscaComplementoEntidad(complemento.id, comboEntidad.Text) Then
            dgvClaves.DataSource = clavesConta.buscaClaves(entidadesINE.id)
            dgvClaves.Columns(0).Visible = False
            dgvClaves.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End If
    End Sub

    Private Sub dgvClaves_Click(sender As Object, e As EventArgs) Handles dgvClaves.Click
        Try
            idClave = CInt(dgvClaves.CurrentRow.Cells(0).Value.ToString)
            TextBox1.Text = dgvClaves.CurrentRow.Cells(1).Value
            btnClave.Text = "Modificar Clave"
            nueva = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub comboEntidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboEntidad.SelectedIndexChanged
        If entidadesINE.buscaComplementoEntidad(complemento.id, comboEntidad.Text) Then
            llenaGrid()
        Else
            dgvClaves.DataSource = Nothing
        End If
    End Sub

    Private Sub comboProcesos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboProcesos.SelectedIndexChanged
        If comboProcesos.Text = "Ordinario" Then
            comboAmbito.Enabled = False
        Else
            comboAmbito.Enabled = True
        End If
        If comboComite.Text = "Ejecutivo Nacional" And comboProcesos.Text = "Ordinario" Then
            Label7.Visible = True
            TextBox2.Visible = True
            comboEntidad.Enabled = False
            TextBox1.Enabled = False
            btnClave.Enabled = False
        Else
            Label7.Visible = False
            TextBox2.Visible = False
            comboEntidad.Enabled = True
            TextBox1.Enabled = True
            btnClave.Enabled = True
        End If
    End Sub

    Private Sub llenaDatosComplemento()
        comboProcesos.SelectedIndex = comboProcesos.Items.IndexOf(complemento.proceso)
        comboComite.SelectedIndex = comboComite.Items.IndexOf(complemento.comite)
        comboAmbito.SelectedIndex = comboAmbito.Items.IndexOf(complemento.ambito)
        TextBox2.Text = complemento.idContabilidadP
        For Each s As String In comboEntidad.Items
            If entidadesINE.buscaComplementoEntidad(complemento.id, s) Then
                comboEntidad.SelectedIndex = comboEntidad.Items.IndexOf(s)
                llenaGrid()
                Exit For
            End If
        Next
    End Sub

    Private Sub comboComite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboComite.SelectedIndexChanged
        If comboComite.Text = "Ejecutivo Nacional" And comboProcesos.Text = "Ordinario" Then
            Label7.Visible = True
            TextBox2.Visible = True
            comboEntidad.Enabled = False
            TextBox1.Enabled = False
            btnClave.Enabled = False
        Else
            Label7.Visible = False
            TextBox2.Visible = False
            comboEntidad.Enabled = True
            TextBox1.Enabled = True
            btnClave.Enabled = True
        End If
    End Sub
End Class