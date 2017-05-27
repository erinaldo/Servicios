Public Class frmTrabajadores
    Dim IdTrabajador As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Public NumeroEmpleado As String
    Dim Tipo As Byte
    Dim IdsBancos As New elemento
    Dim IdCuenta As Integer
    Dim IdCuenta2 As Integer
    Dim IdCuenta3 As Integer
    Dim IdCuenta4 As Integer
    Dim RegistroPatronal As String
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Dim clavesContratos() As String = {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "99"}
    Dim clavesPeridiocidad() As String = {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10"}
    Dim clavesJornadas() As String = {"01", "02", "03", "04", "05", "06", "07", "08", "99"}

    Public Sub New(ByVal pTipo As Byte, ByVal pIdTrabajador As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Tipo = pTipo
        IdTrabajador = pIdTrabajador

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            TextBox6.Text = RegistroPatronal
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox10.Text = ""
            TextBox9.Text = ""
            TextBox8.SelectedIndex = 0
            TextBox7.SelectedIndex = 0
            ComboBox2.SelectedIndex = 0
            TextBox15.Text = ""
            TextBox14.SelectedIndex = 0
            TextBox5.Text = "0"
            TextBox12.Text = "0"
            TextBox13.Text = "0"
            Txtcalle.Text = ""
            txtrfc.Text = ""
            txtciudad.Text = ""
            txtcodigopostal.Text = ""
            txtcolonia.Text = ""
            txtemail.Text = ""
            cmbEstadomx2.Text = ""
            txtmunicipio.Text = ""
            txtnoexterior.Text = ""
            txtnointerior.Text = ""
            txtpais.Text = "MÉXICO"
            txttelefono.Text = ""
            txtreferencia.Text = ""
            ComboBox1.SelectedIndex = 0
            ComboBox8.SelectedIndex = 0
            'TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            'TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
            ConsultaOn = True
            Consulta()
            'Dim C As New dbTecnicos(MySqlcon)
            'ConsultaOn = False
            'TextBox6.Text = Format(C.DaMaximo, "0000")
            'ConsultaOn = True
            IdCuenta = 0
            IdCuenta2 = 0
            IdCuenta3 = 0
            IdCuenta4 = 0
            Button9.BackColor = ColorRojo
            Button10.BackColor = ColorRojo
            Button11.BackColor = ColorRojo
            Button12.BackColor = ColorRojo
            TextBox1.Focus()
            CheckBox1.Checked = False

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim P As New dbTrabajadores(MySqlcon)
                DataGridView1.DataSource = P.Consulta(TextBox11.Text)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Nombre"
                DataGridView1.Columns(2).HeaderText = "No. Empleado"
                DataGridView1.Columns(3).HeaderText = "R. Patronal"
                DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Tipo = 0 Then
            Me.Close()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo()
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim P As New dbTrabajadores(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            Dim Sind As Byte = 0
            If CheckBox1.Checked Then Sind = 1
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox14.Text = "" Then
                NoErrores = False
                MensajeError = vbCrLf + " Debe llenar todos los datos con un * marcado para poder dar de alta un trabajador."
                'TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox5.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " La antigüedad debe ser un valor numérico."
                'TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox12.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El salario base debe ser un valor numérico."
                'TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox13.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El salario diario debe ser un valor numérico."
                'TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If ComboBox3.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar el estado donde labora."
            End If
            If (TextBox6.Text = "" Or TextBox4.Text = "") And TextBox7.SelectedIndex < 8 Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar el registro patronal y Número de seguro social."
            End If
            txtrfc.Text = Trim(Replace(txtrfc.Text, "-", ""))
            While txtrfc.Text.IndexOf(" ") <> -1
                txtrfc.Text = Replace(txtrfc.Text, " ", "")
            End While
            If txtrfc.Text.Length > 13 Or txtrfc.Text.Length < 12 Then
                NoErrores = False
                MensajeError += vbCrLf + " El RFC debe contener al menos 12 caracteres o ser menor o igual a 13 caracteres."
                'TextBox8.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox16.Text <> "" Then
                If TextBox16.Text.Length > 13 Or TextBox16.Text.Length < 12 Then
                    NoErrores = False
                    MensajeError += vbCrLf + " El RFC del patrón tercero contener al menos 12 caracteres o ser menor o igual a 13 caracteres."
                    'TextBox8.BackColor = Color.FromArgb(250, 150, 150)
                End If
            End If
            If TextBox3.Text.Length > 18 Or TextBox3.Text.Length < 18 Then
                NoErrores = False
                MensajeError += vbCrLf + " La CURP se compone de 18 caracteres."
                'TextBox8.BackColor = Color.FromArgb(250, 150, 150)
            End If

            If Button1.Text = "Guardar" Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.TrabajadoresAlta, PermisosN.Secciones.Nomina) = False Then
                    MensajeError += " No tiene permiso para realizar esta operación."
                    NoErrores = False
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.TrabajadoresCambios, PermisosN.Secciones.Nomina) = False Then
                    MensajeError += " No tiene permiso para realizar esta operación."
                    NoErrores = False
                End If
            End If

            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    'If (PermisosCatalogos And CULng((Math.Pow(2, percatalogos1.Tecnicos + 1)))) <> 0 Then
                    'If P.ChecaClaveRepetida(TextBox6.Text) = False Then
                    P.Guardar(TextBox1.Text, TextBox6.Text, TextBox2.Text, TextBox3.Text, ComboBox8.SelectedIndex + 2, TextBox4.Text, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox5.Text), TextBox10.Text, TextBox9.Text, TextBox8.Text, TextBox7.Text, TextBox14.Text, CDbl(TextBox12.Text), ComboBox1.SelectedIndex + 1, CDbl(TextBox13.Text), Txtcalle.Text, txttelefono.Text, txtemail.Text, txtrfc.Text, txtciudad.Text, txtcodigopostal.Text, cmbEstadomx2.Text, txtpais.Text, txtnoexterior.Text, txtnointerior.Text, txtcolonia.Text, txtmunicipio.Text, txtreferencia.Text, IdsBancos.Valor(ComboBox2.SelectedIndex), TextBox15.Text, IdCuenta, IdCuenta2, IdCuenta3, IdCuenta4, Sind, TextBox16.Text, ComboBox3.Text)
                    PopUp("Guardado", 90)
                    'Nuevo()
                    'Else
                    '   MsgBox("Ya existe un técnico con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                    '   TextBox6.BackColor = Color.FromArgb(250, 150, 150)
                    ' End If
                    'Else
                    '   MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    'End If
                Else
                    'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Tecnicos + 2)))) <> 0 Then
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                        P.Modificar(IdTrabajador, TextBox1.Text, TextBox6.Text, TextBox2.Text, TextBox3.Text, ComboBox8.SelectedIndex + 2, TextBox4.Text, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox5.Text), TextBox10.Text, TextBox9.Text, TextBox8.Text, TextBox7.Text, TextBox14.Text, CDbl(TextBox12.Text), ComboBox1.SelectedIndex + 1, CDbl(TextBox13.Text), Txtcalle.Text, txttelefono.Text, txtemail.Text, txtrfc.Text, txtciudad.Text, txtcodigopostal.Text, cmbEstadomx2.Text, txtpais.Text, txtnoexterior.Text, txtnointerior.Text, txtcolonia.Text, txtmunicipio.Text, txtreferencia.Text, IdsBancos.Valor(ComboBox2.SelectedIndex), TextBox15.Text, IdCuenta, IdCuenta2, IdCuenta3, IdCuenta4, Sind, TextBox16.Text, ComboBox3.Text)
                        PopUp("Modificado", 90)
                        'Nuevo()
                    End If
                    'Else
                    'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    'End If
                End If
                If Tipo = 0 Then
                    Nuevo()
                    TextBox1.Focus()
                Else
                    NumeroEmpleado = TextBox2.Text
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                End If
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.TrabajadoresBaja, PermisosN.Secciones.Nomina) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbTrabajadores(MySqlcon)
                    P.Eliminar(IdTrabajador)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    TextBox6.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este técnico debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        IdTrabajador = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
        LlenaDatos()
    End Sub

    Private Sub LlenaDatos()
        Try
            Dim P As New dbTrabajadores(IdTrabajador, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            TextBox1.Text = P.Nombre
            TextBox6.Text = P.RegistroPatronal
            TextBox2.Text = P.NumeroEmpleado
            TextBox3.Text = P.Curp
            ComboBox8.SelectedIndex = P.TipoRegimen - 2
            TextBox4.Text = P.NumeroSeguroSocial
            DateTimePicker1.Value = P.FechaInicioLaboral
            TextBox5.Text = P.Antiguedad.ToString
            TextBox10.Text = P.Departamento
            TextBox9.Text = P.Puesto
            TextBox8.Text = P.TipoJornada
            TextBox7.Text = P.TipoContrato
            TextBox14.Text = P.Periodicidad
            ComboBox1.SelectedIndex = P.RiesgoPuesto - 1
            TextBox12.Text = P.SalarioBaseCotApor.ToString
            TextBox13.Text = P.SalarioDiarioIntegrado.ToString

            Txtcalle.Text = P.Direccion
            txtrfc.Text = P.RFC
            txtciudad.Text = P.Ciudad
            txtcodigopostal.Text = P.CP
            txtcolonia.Text = P.Colonia
            txtemail.Text = P.Email
            cmbEstadomx2.Text = P.Estado
            txtmunicipio.Text = P.Municipio
            txtnoexterior.Text = P.NoExterior
            txtnointerior.Text = P.NoInterior
            txtpais.Text = P.Pais
            txttelefono.Text = P.Telefono
            txtreferencia.Text = P.ReferenciaDomicilio
            ComboBox2.SelectedIndex = IdsBancos.Busca(P.Banco)
            TextBox15.Text = P.CLABE
            IdCuenta = P.IdCuenta
            IdCuenta2 = P.IdCuenta2
            IdCuenta3 = P.IdCuenta3
            IdCuenta4 = P.IdCuenta4
            ComboBox3.Text = P.EstadoLabora
            If IdCuenta <> 0 Then
                Button9.BackColor = ColorVerde
            Else
                Button9.BackColor = ColorRojo
            End If
            If IdCuenta2 <> 0 Then
                Button10.BackColor = ColorVerde
            Else
                Button10.BackColor = ColorRojo
            End If
            If IdCuenta3 <> 0 Then
                Button11.BackColor = ColorVerde
            Else
                Button11.BackColor = ColorRojo
            End If
            If IdCuenta4 <> 0 Then
                Button12.BackColor = ColorVerde
            Else
                Button12.BackColor = ColorRojo
            End If
            If P.sindicalizado = 0 Then
                CheckBox1.Checked = False
            Else
                CheckBox1.Checked = True
            End If
            TextBox16.Text = P.RFCPatronOrigen
            'CheckBox1.Checked = P.sindicalizado
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub frmTecnicos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        DaEstadosMexico(cmbEstadomx2)
        DaEstadosMexico(ComboBox3)
        ComboBox8.Items.Add("02 Sueldos.")
        ComboBox8.Items.Add("03 Jubilados.")
        ComboBox8.Items.Add("04 Pensionados.")
        ComboBox8.Items.Add("05 Asimilados Miembros Sociedades Cooperativas Producción.")
        ComboBox8.Items.Add("06 Asimilados Integrantes Sociedades Asociaciones Civiles.")
        ComboBox8.Items.Add("07 Asimilados Miembros consejos.")
        ComboBox8.Items.Add("08 Asimilados Comisionistas.")
        ComboBox8.Items.Add("09 Asimilados Honorarios.")
        ComboBox8.Items.Add("10 Asimilados acciones.")
        ComboBox8.Items.Add("11 Asimilados otros.")
        ComboBox8.Items.Add("99 Otro regimen.")
        ComboBox1.Items.Add("1 Clase I")
        ComboBox1.Items.Add("2 Clase II")
        ComboBox1.Items.Add("3 Clase III")
        ComboBox1.Items.Add("4 Clase IV")
        ComboBox1.Items.Add("5 Clase V")
        ComboBox1.Items.Add("No aplica")
        TextBox7.Items.Add("01 Contrato de trabajo por tiempo indeterminado")
        TextBox7.Items.Add("02 Contrato de trabajo para obra determinada")
        TextBox7.Items.Add("03 Contrato de trabajo por tiempo determinado")
        TextBox7.Items.Add("04 Contrato de trabajo por temporada")
        TextBox7.Items.Add("05 Contrato de trabajo sujeto a prueba")
        TextBox7.Items.Add("06 Contrato de trabajo con capacitación inicial")
        TextBox7.Items.Add("07 Modalidad de contratación por pago de hora laborada")
        TextBox7.Items.Add("08 Modalidad de trabajo por comisión laboral")
        TextBox7.Items.Add("09 Modalidades de contratación donde no existe relación de trabajo")
        TextBox7.Items.Add("10 Jubilación, pensión, retiro.")
        TextBox7.Items.Add("99 Otro contrato")
        TextBox14.Items.Add("01 Diario")
        TextBox14.Items.Add("02 Semanal")
        TextBox14.Items.Add("03 Catorcenal")
        TextBox14.Items.Add("04 Quincenal")
        TextBox14.Items.Add("05 Mensual")
        TextBox14.Items.Add("06 Bimestral")
        TextBox14.Items.Add("07 Unidad Obra")
        TextBox14.Items.Add("08 Comisión")
        TextBox14.Items.Add("09 Precio alzado")
        TextBox14.Items.Add("10 Decenal")
        TextBox14.Items.Add("99 Otra peridiocidad")
        TextBox8.Items.Add("No aplica")
        TextBox8.Items.Add("01 Diurna")
        TextBox8.Items.Add("02 Nocturna")
        TextBox8.Items.Add("03 Mixta")
        TextBox8.Items.Add("04 Por hora")
        TextBox8.Items.Add("05 Reducida")
        TextBox8.Items.Add("06 Continuada")
        TextBox8.Items.Add("07 Partida")
        TextBox8.Items.Add("08 Por turnos")
        TextBox8.Items.Add("99 Otra Jornada")
        TextBox7.SelectedIndex = 0
        TextBox8.SelectedIndex = 0
        TextBox14.SelectedIndex = 0
        LlenaCombos("tblbancoscatalogo", ComboBox2, "concat(clave,' ',nombre)", "nombreb", "clave", IdsBancos, , "SIN BANCO", "clave")
        Dim Suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        RegistroPatronal = Suc.RegistroPatronal
        If IdTrabajador <> 0 Then
            LlenaDatos()
        Else
            Nuevo()
        End If
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            LlenaDatos()
            TextBox1.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatos()
        End If
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged
        Consulta()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        TextBox5.Text = CStr(CInt((DateDiff(DateInterval.Day, DateTimePicker1.Value, Date.Now) + 1) / 7))
    End Sub

  
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta = fsc.IdCuenta
            If IdCuenta <> 0 Then
                Button9.BackColor = ColorVerde
            Else
                Button9.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta2)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta2 = fsc.IdCuenta
            If IdCuenta2 <> 0 Then
                Button10.BackColor = ColorVerde
            Else
                Button10.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta3)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta3 = fsc.IdCuenta
            If IdCuenta3 <> 0 Then
                Button11.BackColor = ColorVerde
            Else
                Button11.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta4)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta4 = fsc.IdCuenta
            If IdCuenta4 <> 0 Then
                Button12.BackColor = ColorVerde
            Else
                Button12.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If IdTrabajador <> 0 Then
            Dim f As New frmNominaDatosN(IdTrabajador)
            f.ShowDialog()
        End If
    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim grfc As New frmGeneradorRFC
        If grfc.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtrfc.Text = grfc.RFC
        End If
        grfc.Dispose()
    End Sub
End Class