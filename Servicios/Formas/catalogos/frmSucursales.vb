Public Class frmSucursales
    Dim IdSucursal As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Public CodigoSucursal As String = ""
    Dim IdsAlmacenes As New elemento
    Dim IdsAlmacenesC As New elemento
    Dim IdsAlmacenesM As New elemento
    Dim IdsTiposSuc As New elemento
    Dim IdsRegimen As New elemento
    Dim Csat As dbCatalogosSat
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            TextBox6.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox8.Text = ""
            TextBox7.Text = ""
            TextBox18.Text = ""
            TextBox11.Text = ""
            TextBox10.Text = ""
            TextBox12.Text = ""
            TextBox5.Text = ""
            TextBox9.Text = ""
            TextBox21.Text = ""
            TextBox20.Text = ""
            TextBox19.Text = ""
            TextBox22.Text = "MÉXICO"
            TextBox23.Text = ""
            TextBox16.Text = ""
            TextBox15.Text = ""
            TextBox17.Text = ""
            TextBox13.Text = ""
            TextBox14.Text = ""
            TextBox26.Text = ""
            TextBox25.Text = ""
            TextBox24.Text = ""
            TextBox27.Text = "MÉXICO"
            TextBox28.Text = ""
            TextBox32.Text = "0"
            TextBox33.Text = ""
            TextBox35.Text = ""
            TextBox36.Text = ""
            TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
            
            TextBox32.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox5.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox9.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox22.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox20.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox8.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox18.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox19.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
            Button5.Visible = False
            Button6.Visible = False
            Button7.Visible = False
            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("No Asignado")
            ComboBox2.SelectedIndex = 0
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("No Asignado")
            ComboBox1.SelectedIndex = 0
            ComboBox3.Items.Clear()
            ComboBox3.Items.Add("No Asignado")
            ComboBox3.SelectedIndex = 0
            ComboBox4.SelectedIndex = 0

            TextBox29.Text = ""
            TextBox31.Text = ""
            TextBox30.Text = ""
            TextBox37.Text = ""
            TextBox38.Text = "MEX"

            ConsultaOn = True
            Consulta()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim P As New dbSucursales(MySqlcon)
                DataGridView1.DataSource = P.Consulta(TextBox34.Text, True)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Código"
                DataGridView1.Columns(2).HeaderText = "Nombre"
                DataGridView1.Columns(3).HeaderText = "Teléfono"
                DataGridView1.Columns(2).Width = 200
                DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo()
    End Sub

   

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim P As New dbSucursales(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            If TextBox5.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar el municipio a la sucursal."
                TextBox5.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox8.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar RFC a la sucursal."
                TextBox5.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox33.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar el nombre fiscal a la sucursal."
                TextBox5.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox9.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar la ciudad a la sucursal."
                TextBox9.BackColor = Color.FromArgb(250, 150, 150)
            End If

            If TextBox20.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar el estado a la sucursal."
                TextBox20.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox22.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar el pais a la sucursal."
                TextBox22.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un nombre a la sucursal."
                TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox18.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar la calle a la sucursal."
                TextBox18.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox19.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar el código postal a la sucursal."
                TextBox19.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox6.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un código a la sucursal."
                TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If
            
            If IsNumeric(TextBox32.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " Impuesto debe ser un valor numérico."
                TextBox32.BackColor = Color.FromArgb(250, 150, 150)
            End If
            TextBox8.Text = Trim(Replace(TextBox8.Text, "-", ""))
            While TextBox8.Text.IndexOf(" ") <> -1
                TextBox8.Text = Replace(TextBox8.Text, " ", "")
            End While
            If TextBox8.Text.Length > 13 Or TextBox8.Text.Length < 12 Then
                NoErrores = False
                MensajeError += vbCrLf + " El RFC debe contener al menos 12 caracteres o ser menor o igual a 13 caracteres."
                TextBox8.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If Button1.Text = "Guardar" Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.SucursalesAlta, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.SucursalesCambio, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then

                    If P.ChecaClaveRepetida(TextBox6.Text) = False Then

                        'P.Guardar(TextBox1.Text, TextBox5.Text, TextBox3.Text, TextBox4.Text, TextBox2.Text, TextBox6.Text, TextBox8.Text, TextBox7.Text, TextBox12.Text, TextBox9.Text, TextBox10.Text, TextBox11.Text, TextBox17.Text, TextBox16.Text, TextBox13.Text, TextBox14.Text, TextBox15.Text)
                        P.Guardar(Trim(TextBox1.Text), Trim(TextBox18.Text), Trim(TextBox3.Text), Trim(TextBox4.Text), Trim(TextBox2.Text), Trim(TextBox6.Text), Trim(TextBox8.Text), Trim(TextBox7.Text), Trim(TextBox9.Text), Trim(TextBox19.Text), Trim(TextBox20.Text), Trim(TextBox22.Text), Trim(TextBox23.Text), Trim(TextBox14.Text), Trim(TextBox24.Text), Trim(TextBox25.Text), Trim(TextBox27.Text), Trim(TextBox11.Text), Trim(TextBox10.Text), Trim(TextBox12.Text), Trim(TextBox5.Text), Trim(TextBox21.Text), Trim(TextBox16.Text), Trim(TextBox15.Text), Trim(TextBox17.Text), Trim(TextBox13.Text), Trim(TextBox26.Text), "", 1, 1, "", CDbl(TextBox32.Text), Trim(TextBox33.Text), TextBox35.Text, TextBox36.Text, IdsTiposSuc.Valor(ComboBox4.SelectedIndex), TextBox28.Text, IdsRegimen.Valor(ComboBox5.SelectedIndex), TextBox29.Text, TextBox30.Text, TextBox31.Text, TextBox37.Text, TextBox38.Text, IdsRegimen.Valor(ComboBox5.SelectedIndex))
                        Dim dbP As New dbImpresionesN(MySqlcon)
                        dbP.GuardaZonaDetalles(P.ID)
                        PopUp("Guardado", 60)
                        Nuevo()
                    Else
                        MsgBox("Ya existe una sucursal con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                        TextBox6.BackColor = Color.FromArgb(250, 150, 150)
                    End If
                
                Else

                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Dim ClaveRepetida As Boolean = False
                        If TextBox6.Text <> ClaveAnterior Then
                            ClaveRepetida = P.ChecaClaveRepetida(TextBox6.Text)
                        End If
                        If ClaveRepetida = False Then
                            Dim IdA As Integer
                            IdA = IdsAlmacenes.Valor(ComboBox2.SelectedIndex)
                            If IdA < 0 Then IdA = 0
                            Dim IdAc As Integer
                            IdAc = IdsAlmacenesC.Valor(ComboBox1.SelectedIndex)
                            If IdAc < 0 Then IdAc = 0
                            Dim IdAm As Integer
                            IdAm = IdsAlmacenesM.Valor(ComboBox3.SelectedIndex)
                            If IdAm < 0 Then IdAm = 0
                            P.Modificar(IdSucursal, Trim(TextBox1.Text), Trim(TextBox18.Text), Trim(TextBox3.Text), Trim(TextBox4.Text), Trim(TextBox2.Text), Trim(TextBox6.Text), Trim(TextBox8.Text), Trim(TextBox7.Text), Trim(TextBox9.Text), Trim(TextBox19.Text), Trim(TextBox20.Text), Trim(TextBox22.Text), Trim(TextBox23.Text), Trim(TextBox14.Text), Trim(TextBox24.Text), Trim(TextBox25.Text), Trim(TextBox27.Text), Trim(TextBox11.Text), Trim(TextBox10.Text), Trim(TextBox12.Text), Trim(TextBox5.Text), Trim(TextBox21.Text), Trim(TextBox16.Text), Trim(TextBox15.Text), Trim(TextBox17.Text), Trim(TextBox13.Text), Trim(TextBox26.Text), "", 1, 1, "", CDbl(TextBox32.Text), IdA, Trim(TextBox33.Text), TextBox35.Text, TextBox36.Text, IdAc, IdAm, IdsTiposSuc.Valor(ComboBox4.SelectedIndex), TextBox28.Text, IdsRegimen.Valor(ComboBox5.SelectedIndex), TextBox29.Text, TextBox30.Text, TextBox31.Text, TextBox37.Text, TextBox38.Text, IdsRegimen.Valor(ComboBox5.SelectedIndex))
                            PopUp("Modificado", 60)
                            Nuevo()
                        Else
                            MsgBox("Ya existe una sucursal con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                            TextBox6.BackColor = Color.FromArgb(250, 150, 150)
                        End If
                    End If
                    
                End If
            TextBox6.Focus()
            Else
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.SucursalesBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                    Dim P As New dbSucursales(MySqlcon)
                    P.Eliminar(IdSucursal)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    TextBox6.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este cliente debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        LlenaDatos()
    End Sub

    Private Sub LlenaDatos()
        Try
            IdSucursal = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbSucursales(IdSucursal, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            Button5.Visible = True
            Button6.Visible = True
            Button7.Visible = True
            TextBox6.Text = P.Clave
            TextBox1.Text = P.Nombre
            TextBox2.Text = P.Contacto
            TextBox3.Text = P.Telefono
            TextBox4.Text = P.Email
            TextBox8.Text = P.RFC
            TextBox7.Text = P.Giro
            TextBox18.Text = P.Direccion
            TextBox11.Text = P.NoExterior
            TextBox10.Text = P.NoInterior
            TextBox12.Text = P.Colonia
            TextBox5.Text = P.Municipio
            TextBox9.Text = P.Ciudad
            TextBox21.Text = P.ReferenciaDomicilio
            TextBox20.Text = P.Estado
            TextBox19.Text = P.CP
            TextBox22.Text = P.Pais

            TextBox23.Text = P.Direccion2
            TextBox16.Text = P.NoExterior2
            TextBox15.Text = P.NoInterior2
            TextBox17.Text = P.Colonia2
            TextBox13.Text = P.Municipio2
            TextBox14.Text = P.Ciudad2
            TextBox26.Text = P.ReferenciaDomicilio2
            TextBox25.Text = P.Estado2
            TextBox24.Text = P.CP2
            TextBox27.Text = P.Pais2
            TextBox32.Text = P.Impuesto
            TextBox33.Text = P.NombreFiscal
            TextBox35.Text = P.RegimenFiscal
            TextBox36.Text = P.CURP
            TextBox28.Text = P.RegistroPatronal

            TextBox29.Text = P.cColonia
            TextBox31.Text = P.cMunicipio
            TextBox30.Text = P.cLocalidad
            TextBox37.Text = P.cEstado
            TextBox38.Text = P.cPais


            LlenaCombos("tblalmacenes", ComboBox2, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idsucursal=" + IdSucursal.ToString, "No Asignado")
            ComboBox2.SelectedIndex = IdsAlmacenes.Busca(P.idAlmacen)
            LlenaCombos("tblalmacenes", ComboBox1, "nombre", "nombret", "idalmacen", IdsAlmacenesC, "idsucursal=" + IdSucursal.ToString, "No Asignado")
            ComboBox1.SelectedIndex = IdsAlmacenesC.Busca(P.IdAlmacenC)
            LlenaCombos("tblalmacenes", ComboBox3, "nombre", "nombret", "idalmacen", IdsAlmacenesM, "idsucursal=" + IdSucursal.ToString, "No Asignado")
            ComboBox3.SelectedIndex = IdsAlmacenesM.Busca(P.IdAlmacenM)
            ClaveAnterior = P.Clave
            ComboBox4.SelectedIndex = IdsTiposSuc.Busca(P.idTipo)
            ComboBox5.SelectedIndex = IdsRegimen.Busca(P.ClaveRegimen)
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            LlenaDatos()
            TextBox6.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatos()
        End If
    End Sub

    Private Sub frmSucursales_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Csat.Con.Close()
    End Sub

    Private Sub frmClientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblsucursalestipos", ComboBox4, "nombre", "nombrec", "idtipo", IdsTiposSuc)
        Csat = New dbCatalogosSat
        Csat.IniciarMySQL(My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
        Nuevo()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.SucursalesCertificados, PermisosN.Secciones.Catalagos) = True Then
            Dim Fi As New frmSucursalesCertificados(IdSucursal)
            Fi.ShowDialog()
            Fi.Dispose()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.SucursalesFolio, PermisosN.Secciones.Catalagos) = True Then
            Dim Fi As New frmSucursalesFolios(IdSucursal)
            Fi.ShowDialog()
            Fi.Dispose()
        End If
    End Sub

    Private Sub TextBox34_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox34.TextChanged
        Consulta()
    End Sub

    Private Sub TextBox22_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox22.TextChanged
        If TextBox22.Text = "" Then
            TextBox38.Text = ""
        End If
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox35_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox35.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox36_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox36.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox33_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox33.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox18_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox18.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox11_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox11.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox10_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox12_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox12.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox9_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox9.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox21_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox21.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox20_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox20.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox22_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox22.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox23_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox23.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox16_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox16.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox15_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox15.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox17_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox17.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox13_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox13.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox14_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox14.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox26_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox26.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox25_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox25.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox27_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox27.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox19_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox19.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox24_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox24.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox34_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox34.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.SucursalesCambio, PermisosN.Secciones.Catalagos) = True Then
            Dim f As New frmClientesEquipos(IdSucursal, 0, 2)
            f.ShowDialog()
            f.Dispose()
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        If TextBox8.Text.Length = 13 Then LlenaCombos("tblregimenfiscales", ComboBox5, "concat(convert(clave using utf8),' ',descripcion)", "nombrec", "clave", IdsRegimen, "fisica=1")
        If TextBox8.Text.Length = 12 Then LlenaCombos("tblregimenfiscales", ComboBox5, "concat(convert(clave using utf8),' ',descripcion)", "nombrec", "clave", IdsRegimen, "moral=1")
    End Sub

    Private Sub TextBox19_TextChanged(sender As Object, e As EventArgs) Handles TextBox19.TextChanged
        If ConsultaOn And TextBox19.Text.Length = 5 Then
            CSat.DaDatosCP(TextBox19.Text)
            TextBox5.Text = Csat.Municipio
            TextBox31.Text = Csat.cMunicipio
            TextBox9.Text = Csat.Localidad
            TextBox30.Text = Csat.cLocalidad
            TextBox20.Text = Csat.Estado
            TextBox37.Text = Csat.cEstado
            'TextBox12.AutoCompleteCustomSource = CSat.txtSource
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim fbc As New frmBuscadorCatalogosSat(1, TextBox19.Text)
        fbc.Cat = Csat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox29.Text = fbc.Clave
            TextBox12.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim fbc As New frmBuscadorCatalogosSat(2)
        fbc.Cat = Csat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox31.Text = fbc.Clave
            TextBox5.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim fbc As New frmBuscadorCatalogosSat(3)
        fbc.Cat = Csat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox30.Text = fbc.Clave
            TextBox9.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim fbc As New frmBuscadorCatalogosSat(4)
        fbc.Cat = Csat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox37.Text = fbc.Clave
            TextBox20.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim fbc As New frmBuscadorCatalogosSat(5)
        fbc.Cat = Csat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox38.Text = fbc.Clave
            TextBox22.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub

    Private Sub TextBox24_TextChanged(sender As Object, e As EventArgs) Handles TextBox24.TextChanged
        If ConsultaOn And TextBox24.Text.Length = 5 Then
            Csat.DaDatosCP(TextBox24.Text)
            TextBox13.Text = Csat.Municipio
            TextBox14.Text = Csat.Localidad
            TextBox25.Text = Csat.Estado
        End If
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        If TextBox12.Text = "" Then
            TextBox29.Text = ""
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text = "" Then
            TextBox31.Text = ""
        End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If TextBox9.Text = "" Then
            TextBox30.Text = ""
        End If
    End Sub

    Private Sub TextBox20_TextChanged(sender As Object, e As EventArgs) Handles TextBox20.TextChanged
        If TextBox20.Text = "" Then
            TextBox37.Text = ""
        End If
    End Sub
End Class