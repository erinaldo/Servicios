Public Class frmClientes

    Dim IdCliente As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Dim IdsVendedores As New elemento
    Dim IdsIdentificacion As New elemento
    Dim IdsListas As New elemento
    Public CodigoCliente As String = ""
    Public TipoDeAlta As Integer
    Dim IdsVendedores2 As New elemento
    Dim IdsVendedores3 As New elemento
    Dim IdsTipos As New elemento
    Dim webcam As New WebCam
    Dim imagen As String = ""
    Dim consulta1 As Boolean
    Dim NoRepetir As Boolean = False
    Dim NombreAnterior As String
    Dim IdCuenta As Integer
    Dim IdCuenta2 As Integer
    Dim IdCuenta3 As Integer
    Dim IdCuenta4 As Integer
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Dim IdsFormaF As New elemento
    Dim IdsFormaR As New elemento
    Dim CSat As dbCatalogosSat
    Dim X As New elemento
    Public Sub New(ByVal pTipodeAlta As Integer, ByVal pIdCliente As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        TipoDeAlta = pTipodeAlta
        ' Add any initialization after the InitializeComponent() call.
        IdCliente = pIdCliente
    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            ComboBox1.SelectedIndex = 0
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox34.Text = ""
            TextBox4.Text = ""
            TextBox8.Text = "XAXX010101000"
            TextBox7.Text = ""
            TextBox18.Text = ""
            TextBox35.Text = "0"
            TextBox11.Text = ""
            TextBox10.Text = ""
            TextBox12.Text = ""
            TextBox5.Text = ""
            TextBox9.Text = ""
            TextBox21.Text = ""
            NombreAnterior = ""
            cmbEstadomx.Text = ""
            TextBox19.Text = ""
            IdCuenta = 0
            IdCuenta2 = 0
            IdCuenta3 = 0
            IdCuenta4 = 0
            Button9.BackColor = ColorRojo
            Button10.BackColor = ColorRojo
            Button11.BackColor = ColorRojo
            Button12.BackColor = ColorRojo
            TextBox22.Text = "MÉXICO"
            TextBox41.Text = ""
            TextBox39.Text = ""
            TextBox40.Text = ""
            TextBox25.Text = ""
            TextBox20.Text = "MEX"
            ' txtZona.Text = ""
            ' txtZona2.Text = ""
            cmbIdentificacion.SelectedIndex = 0
            txtNumeroID.Text = ""
            imagen = ""
            ComboBox3.SelectedIndex = 0
            ComboBox4.SelectedIndex = 0
            pctImagen.Image = Nothing
            btnEliminarWebCam.Text = "Eliminar"
            btnNuevoWebCam.Text = "Nuevo"
            webcam.Detener(Timer1)
            CheckBox5.Checked = False
            TextBox23.Text = ""
            TextBox16.Text = ""
            TextBox15.Text = ""
            TextBox17.Text = ""
            TextBox13.Text = ""
            TextBox14.Text = ""
            TextBox26.Text = ""
            cmbEstadomx2.Text = ""
            TextBox24.Text = ""
            TextBox27.Text = ""
            TextBox28.Text = "0"
            TextBox29.Text = "0"
            TextBox30.Text = "0"
            TextBox31.Text = "0"
            TextBox32.Text = "0"
            TextBox36.Text = ""
            TextBox37.Text = ""
            TextBox38.Text = ""
            TextBox42.Text = ""
            ComboBox6.Text = "G01 Adquisición de mercancias"
            TextBox6.Text = ""
            CheckBox3.Checked = False
            CheckBox3.Visible = False
            CheckBox1.Checked = False
            ComboBox5.SelectedIndex = 0
            cmbListaPrecio.SelectedIndex = 0
            RadioButton1.Checked = True
            TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox28.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox29.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox31.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox32.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox35.BackColor = Color.FromKnownColor(KnownColor.Window)
            cmbZona.SelectedIndex = 0
            cmbZona2.SelectedIndex = 0
            ComboBox2.SelectedIndex = 0
            Button1.Text = "Guardar"
            Button2.Enabled = False
            Button5.Visible = False
            Button6.Visible = False
            Button7.Visible = False
            ConsultaOn = True
            Consulta()
            Dim C As New dbClientes(MySqlcon)
            ConsultaOn = False
            If CheckBox4.Checked Then
                TextBox6.Text = C.DaMaximo(True).ToString
            Else
                TextBox6.Text = Format(C.DaMaximo(False), "0000")
            End If
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim P As New dbClientes(MySqlcon)
                DataGridView1.DataSource = P.Consulta(TextBox33.Text)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Código"
                DataGridView1.Columns(2).HeaderText = "Nombre"
                DataGridView1.Columns(3).HeaderText = "RFC"
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

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        'Consulta()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        'Consulta()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        BotonGuardar(False)
    End Sub
    Private Sub BotonGuardar(ByVal pLlenaDatos As Boolean)
        Try
            Dim P As New dbClientes(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            Dim DirFiscal As Byte = 0
            Dim Esconde As Byte = 0
            Dim Impuesto As Byte = 0
            Dim idClas As Integer
            Dim idClas2 As Integer
            Dim idClasID As Integer
            Dim NoChecar As Byte
            If CheckBox5.Checked Then
                NoChecar = 1
            Else
                NoChecar = 0
            End If
            If cmbIdentificacion.SelectedIndex <= 0 Then
                idClasID = 0
            Else
                idClasID = IdsIdentificacion.Valor(cmbIdentificacion.SelectedIndex)
            End If
            'If cmbIdentificacion.SelectedIndex > 0 And txtNumeroID.Text = "" Then
            '    MensajeError += vbCrLf + " No ha asignado un número a la identificación."
            '    txtNumeroID.BackColor = Color.FromArgb(250, 150, 150)
            'End If

            If cmbZona.SelectedIndex <= 0 Then
                idClas = 0
            Else
                idClas = IdsVendedores2.Valor(cmbZona.SelectedIndex)
            End If
            If cmbZona2.SelectedIndex <= 0 Then
                idClas2 = 0
            Else
                idClas2 = IdsVendedores3.Valor(cmbZona2.SelectedIndex)
            End If
            If RadioButton2.Checked Then DirFiscal = 1
            TextBox8.Text = Trim(Replace(TextBox8.Text, "-", ""))
            While TextBox8.Text.IndexOf(" ") <> -1
                TextBox8.Text = Replace(TextBox8.Text, " ", "")
            End While
            If TextBox8.Text.Length > 13 Or TextBox8.Text.Length < 12 Then
                NoErrores = False
                MensajeError += vbCrLf + " El RFC debe contener al menos 12 caracteres o ser menor o igual a 13 caracteres."
                TextBox8.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un nombre al cliente."
                TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox6.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un código al cliente."
                TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If


            If TextBox28.Text = "" Then TextBox28.Text = "0"
            If TextBox29.Text = "" Then TextBox28.Text = "0"
            If TextBox31.Text = "" Then TextBox28.Text = "0"
            If TextBox32.Text = "" Then TextBox28.Text = "0"
            If IsNumeric(TextBox28.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El crédito debe ser un valor numérico."
                TextBox28.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox29.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " Los días de crédito deben ser un valor numérico."
                TextBox29.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox32.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El ISR debe ser un valor numérico."
                TextBox32.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox31.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El Iva retenido deben ser un valor numérico."
                TextBox31.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox35.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El iva debe ser un valor numérico."
                TextBox35.BackColor = Color.FromArgb(250, 150, 150)
            End If
            Dim SObre As Byte
            If CheckBox1.Checked Then
                SObre = 1
            Else
                SObre = 0
            End If
            If CheckBox2.Checked Then
                Esconde = 1
            Else
                Esconde = 0
            End If
            If CheckBox3.Checked Then
                Impuesto = 1
            Else
                Impuesto = 0
            End If
            
            If Button1.Text = "Guardar" Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClientesAlta, PermisosN.Secciones.Catalagos) = False Then
                    MensajeError += " No tiene permiso para realizar esta operación."
                    NoErrores = False
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClientesCambio, PermisosN.Secciones.Catalagos) = False Then
                    MensajeError += " No tiene permiso para realizar esta operación."
                    NoErrores = False
                End If
            End If
            If Button1.Text = "Guardar" And P.ChecaNombreRepetido(TextBox1.Text) And NoRepetir Then
                MensajeError += " Ya existe un cliente con ese nombre"
                NoErrores = False
            End If
            If Button1.Text = "Modificar" And P.ChecaNombreRepetido(TextBox1.Text) And NombreAnterior <> TextBox1.Text And NoRepetir Then
                MensajeError += " Ya existe un cliente con ese nombre"
                NoErrores = False
            End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    If P.ChecaClaveRepetida(TextBox6.Text) = False Then
                        'P.Guardar(TextBox1.Text, TextBox5.Text, TextBox3.Text, TextBox4.Text, TextBox2.Text, TextBox6.Text, TextBox8.Text, TextBox7.Text, TextBox12.Text, TextBox9.Text, TextBox10.Text, TextBox11.Text, TextBox17.Text, TextBox16.Text, TextBox13.Text, TextBox14.Text, TextBox15.Text)
                        P.Guardar(Trim(TextBox1.Text), Trim(TextBox18.Text), Trim(TextBox3.Text), Trim(TextBox4.Text), Trim(TextBox2.Text), Trim(TextBox6.Text), Trim(TextBox8.Text), Trim(TextBox7.Text), Trim(TextBox9.Text), Trim(TextBox19.Text), Trim(cmbEstadomx.Text), Trim(TextBox22.Text), Trim(TextBox23.Text), Trim(TextBox14.Text), Trim(TextBox24.Text), Trim(cmbEstadomx2.Text), Trim(TextBox27.Text), Trim(TextBox11.Text), Trim(TextBox10.Text), Trim(TextBox12.Text), Trim(TextBox5.Text), Trim(TextBox21.Text), Trim(TextBox16.Text), Trim(TextBox15.Text), Trim(TextBox17.Text), Trim(TextBox13.Text), Trim(TextBox26.Text), DirFiscal, CDbl(TextBox28.Text), CDbl(TextBox29.Text), CDbl(TextBox30.Text), CDbl(TextBox32.Text), CDbl(TextBox31.Text), IdsVendedores.Valor(ComboBox5.SelectedIndex), IdsListas.Valor(cmbListaPrecio.SelectedIndex), TextBox34.Text, SObre, CDbl(TextBox35.Text), Esconde, TextBox36.Text, TextBox37.Text, TextBox38.Text, ComboBox1.SelectedIndex, Impuesto, idClas, idClas2, imagen, idClasID, txtNumeroID.Text, IdCuenta, IdsTipos.Valor(ComboBox2.SelectedIndex), IdCuenta2, IdCuenta3, IdCuenta4, IdsFormaF.Valor(ComboBox3.SelectedIndex), IdsFormaR.Valor(ComboBox4.SelectedIndex), NoChecar, TextBox41.Text, TextBox40.Text, TextBox39.Text, TextBox25.Text, TextBox20.Text, TextBox42.Text, ComboBox6.Text.Substring(0, 3))
                        IdCliente = P.ID
                        'If txtNumeroID.Text <> "" Then
                        '    P.GuardarIdentificacion(P.ID, idClasID, txtNumeroID.Text)
                        'End If
                        If BotonGuardarIdent(True) = False Then
                            MsgBox("La identificación no se guardó.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                        PopUp("Guardado", 90)
                        If TipoDeAlta = 1 Then
                            CodigoCliente = TextBox6.Text
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        End If
                        If pLlenaDatos = True Then
                            LlenaDatos()
                        Else
                            Nuevo()
                        End If

                    Else
                        MsgBox("Ya existe un cliente con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                        TextBox6.BackColor = Color.FromArgb(250, 150, 150)
                    End If
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Dim ClaveRepetida As Boolean = False
                        If TextBox6.Text <> ClaveAnterior Then
                            ClaveRepetida = P.ChecaClaveRepetida(TextBox6.Text)
                        End If
                        If ClaveRepetida = False Then
                            'P.Modificar(IdCliente, TextBox1.Text, TextBox5.Text, TextBox3.Text, TextBox4.Text, TextBox2.Text, TextBox6.Text, TextBox8.Text, TextBox7.Text, TextBox12.Text, TextBox9.Text, TextBox10.Text, TextBox11.Text, TextBox17.Text, TextBox16.Text, TextBox13.Text, TextBox14.Text, TextBox15.Text)
                            P.Modificar(IdCliente, Trim(TextBox1.Text), Trim(TextBox18.Text), Trim(TextBox3.Text), Trim(TextBox4.Text), Trim(TextBox2.Text), Trim(TextBox6.Text), Trim(TextBox8.Text), Trim(TextBox7.Text), Trim(TextBox9.Text), Trim(TextBox19.Text), Trim(cmbEstadomx.Text), Trim(TextBox22.Text), Trim(TextBox23.Text), Trim(TextBox14.Text), Trim(TextBox24.Text), Trim(cmbEstadomx2.Text), Trim(TextBox27.Text), Trim(TextBox11.Text), Trim(TextBox10.Text), Trim(TextBox12.Text), Trim(TextBox5.Text), Trim(TextBox21.Text), Trim(TextBox16.Text), Trim(TextBox15.Text), Trim(TextBox17.Text), Trim(TextBox13.Text), Trim(TextBox26.Text), DirFiscal, CDbl(TextBox28.Text), CDbl(TextBox29.Text), CDbl(TextBox32.Text), CDbl(TextBox31.Text), IdsVendedores.Valor(ComboBox5.SelectedIndex), IdsListas.Valor(cmbListaPrecio.SelectedIndex), TextBox34.Text, SObre, CDbl(TextBox35.Text), Esconde, TextBox36.Text, TextBox37.Text, TextBox38.Text, ComboBox1.SelectedIndex, Impuesto, idClas, idClas2, imagen, idClasID, txtNumeroID.Text, IdCuenta, IdsTipos.Valor(ComboBox2.SelectedIndex), IdCuenta2, IdCuenta3, IdCuenta4, IdsFormaF.Valor(ComboBox3.SelectedIndex), IdsFormaR.Valor(ComboBox4.SelectedIndex), NoChecar, TextBox41.Text, TextBox40.Text, TextBox39.Text, TextBox25.Text, TextBox20.Text, TextBox42.Text, ComboBox6.Text.Substring(0, 3))
                            'If P.existeIdentificacion(IdCliente, idClasID) > 0 Then
                            '    If txtNumeroID.Text = "" Then
                            '        P.EliminarIdentificacion(IdCliente, idClasID)
                            '    Else
                            '        P.EliminarIdentificacion(IdCliente, idClasID)
                            '        P.GuardarIdentificacion(P.ID, idClasID, txtNumeroID.Text)
                            '        ' P.ModificarIdentificacion(IdCliente, idClasID, txtNumeroID.Text)
                            '    End If
                            'Else
                            '    If txtNumeroID.Text <> "" Then
                            '        P.GuardarIdentificacion(IdCliente, idClasID, txtNumeroID.Text)
                            '    End If
                            'End If

                            PopUp("Modificado", 90)
                            If TipoDeAlta = 1 Then
                                CodigoCliente = TextBox6.Text
                                Me.DialogResult = Windows.Forms.DialogResult.OK
                            End If
                            Nuevo()
                        Else
                            MsgBox("Ya existe un cliente con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                            TextBox6.BackColor = Color.FromArgb(250, 150, 150)
                        End If
                    End If
                End If
                TextBox6.Focus()
            Else
                MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClientesBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbClientes(MySqlcon)
                    P.Eliminar(IdCliente)
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
        IdCliente = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
        LlenaDatos()
    End Sub

    Private Sub LlenaDatos()
        Try
            Dim P As New dbClientes(IdCliente, MySqlcon)
            'If pNochecarident Then
            cmbIdentificacion.SelectedIndex = 0
            txtNumeroID.Text = ""
            If P.buscarUltimoID(IdCliente) <> -100 Then
                cmbIdentificacion.SelectedIndex = IdsIdentificacion.Busca(P.buscarUltimoID(IdCliente))
                btnGuardarID.Text = "Modificar"
                btnEliminarID.Enabled = True
                btnGuardarID.Enabled = True
            Else
                cmbIdentificacion.SelectedIndex = 0
                btnGuardarID.Text = "Guardar"
                btnEliminarID.Enabled = False
                btnGuardarID.Enabled = False
            End If
            'End If
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            Button5.Visible = True
            Button6.Visible = True
            Button7.Visible = True
            CheckBox3.Visible = True
            TextBox6.Text = P.Clave
            TextBox1.Text = P.Nombre
            NombreAnterior = P.Nombre
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
            cmbEstadomx.Text = P.Estado
            TextBox19.Text = P.CP
            TextBox22.Text = P.Pais
            TextBox41.Text = P.cColonia
            TextBox39.Text = P.cMunicipio
            TextBox40.Text = P.cLocalidad
            TextBox25.Text = P.cEstado
            TextBox20.Text = P.cPais
            TextBox42.Text = P.RegIdTrib

            IdCuenta = P.IdCuenta
            IdCuenta2 = P.IdCuenta2
            IdCuenta3 = P.IdCuenta3
            IdCuenta4 = P.idCuenta4
            ComboBox3.SelectedIndex = IdsFormaF.Busca(P.IdFormaF)
            ComboBox4.SelectedIndex = IdsFormaR.Busca(P.IdFormaR)
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
            TextBox23.Text = P.Direccion2
            TextBox16.Text = P.NoExterior2
            TextBox15.Text = P.NoInterior2
            TextBox17.Text = P.Colonia2
            TextBox13.Text = P.Municipio2
            TextBox14.Text = P.Ciudad2
            TextBox26.Text = P.ReferenciaDomicilio2
            cmbEstadomx2.Text = P.Estado2
            TextBox24.Text = P.CP2
            TextBox27.Text = P.Pais2
            TextBox28.Text = P.Credito.ToString
            TextBox29.Text = P.CreditoDias.ToString
            TextBox30.Text = Format(P.DaSaldoAFecha(IdCliente, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
            TextBox32.Text = P.ISR.ToString
            TextBox31.Text = P.IvaRetenido.ToString
            TextBox34.Text = P.CURP
            TextBox36.Text = P.Representante
            TextBox37.Text = P.RepresentanteRFC
            TextBox38.Text = P.RepresentanteRegistro
            cmbZona.SelectedIndex = IdsVendedores2.Busca(P.zona)
            cmbZona2.SelectedIndex = IdsVendedores3.Busca(P.zona2)
            ComboBox2.SelectedIndex = IdsTipos.Busca(P.IdTipo)
            ComboBox6.Text = CSat.DaUsoCFDI(P.UsoCFDI)
            'txtZona.Text = P.zona
            'txtZona2.Text = P.zona2
            '  cmbIdentificacion.SelectedIndex = IdsIdentificacion.Busca(P.identificacion)
            'txtNumeroID.Text = P.numeroID
            If P.DireccionFiscal = 0 Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If
            If P.EscondeIva = 0 Then
                CheckBox2.Checked = False
            Else
                CheckBox2.Checked = True
            End If
            TextBox35.Text = P.IVA
            If P.SobreescribeIVA = 0 Then
                CheckBox1.Checked = False
            Else
                CheckBox1.Checked = True
            End If
            If P.ActivarImpuestos = 0 Then
                CheckBox3.Checked = False
            Else
                CheckBox3.Checked = True
            End If
            If P.NoChecarCr = 0 Then
                CheckBox5.Checked = False
            Else
                CheckBox5.Checked = True
            End If
            ComboBox5.SelectedIndex = IdsVendedores.Busca(P.IdVendedor)
            cmbListaPrecio.SelectedIndex = IdsListas.Busca(P.IdLista)
            ComboBox1.SelectedIndex = P.UsaAdenda
            ClaveAnterior = P.Clave
            ConsultaOn = True

            imagen = P.imagen
            mostrarImagen(P.imagen)

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
            IdCliente = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            LlenaDatos()
            TextBox6.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            IdCliente = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            LlenaDatos()
        End If
    End Sub

    Private Sub frmClientes_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CSat.Con.Close()
    End Sub

    Private Sub frmClientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        consulta1 = True

        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            For Each c As Control In Me.Controls
                c.Enabled = False
            Next
            Button4.Enabled = True
            Exit Sub
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ClientesMontoCredito, PermisosN.Secciones.Catalagos2) = False Then
            TextBox28.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ClientesDiasCredito, PermisosN.Secciones.Catalagos2) = False Then
            TextBox29.Enabled = False
        End If
        CSat = New dbCatalogosSat
        Csat.IniciarMySQL(My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)

        'Dim Suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        'If Suc.RFC.Length = 13 Then
        'CSat.LlenaCombos("tblusoscfdi", ComboBox6, "concat(clave,' ',descripcion)", "nombrem", "clave", X, , , , True)
        'Else
        'CSat.LlenaCombos("tblusoscfdi", ComboBox6, "concat(clave,' ',descripcion)", "nombrem", "clave", X, "moral='Sí'", , , True)
        'End If
        Dim op As New dbOpciones(MySqlcon)
        If op.ClienteBloquearCodigo = 1 Then TextBox6.Enabled = False
        If op.ClientesMayusculas = 1 Then
            CheckBox4.Checked = True
        Else
            CheckBox4.Checked = False
        End If
        If GlobalIdUsuario <> 1000 Then
            CheckBox5.Visible = False
        End If
        If op.ClientesSinRepetir = 1 Then NoRepetir = True
        Dim T As TextBox
        'If op.ClientesMayusculas = 1 Then
        For Each c As Control In Me.Controls
            If TypeOf c Is TextBox Then
                T = c
                T.CharacterCasing = CharacterCasing.Upper
                c = T
            End If
        Next
        For Each c As Control In Me.TabPage1.Controls
            If TypeOf c Is TextBox Then
                T = c
                T.CharacterCasing = CharacterCasing.Upper
                c = T
            End If
        Next
        For Each c As Control In Me.TabPage2.Controls
            If TypeOf c Is TextBox Then
                T = c
                T.CharacterCasing = CharacterCasing.Upper
                c = T
            End If
        Next
        TextBox36.CharacterCasing = CharacterCasing.Upper
        TextBox37.CharacterCasing = CharacterCasing.Upper
        TextBox38.CharacterCasing = CharacterCasing.Upper
        txtNumeroID.CharacterCasing = CharacterCasing.Upper
        TextBox4.CharacterCasing = CharacterCasing.Normal
        'End If
        LlenaCombos("tblvendedores", ComboBox5, "nombre", "nombret", "idvendedor", IdsVendedores)
        LlenaCombos("tbllistasprecios", cmbListaPrecio, "descripcion", "descripciont", "idlista", IdsListas)
        LlenaCombos("tblzona", cmbZona, "zona", "zonat", "idZona", IdsVendedores2, , "Todos")
        LlenaCombos("tblzona", cmbZona2, "zona", "zonat", "idZona", IdsVendedores3, , "Todos")
        LlenaCombos("tblclientestipos", ComboBox2, "nombre", "nombret", "idtipo", IdsTipos)
        LlenaCombos("tblformasdepago", ComboBox3, "concat(convert(if(tipo=0,'CRÉDITO',if(tipo=1,'CONTADO','PARCIALIDAD')) using utf8),'-',nombre)", "nombret", "idforma", IdsFormaF, , , "idforma")
        LlenaCombos("tblformasdepagoremisiones", ComboBox4, "concat(convert(if(tipo=1,'CONTADO',if(tipo=2,'CONTADO','CRÉDITO')) using utf8),'-',nombre)", "nombrec", "idforma", IdsFormaR, , , "idforma")
        LlenaCombos("tblidentificacion", cmbIdentificacion, "nombre", "nombret", "idIdentificacion", IdsIdentificacion, , "Ninguno")
        DaEstadosMexico(cmbEstadomx)
        DaEstadosMexico(cmbEstadomx2)

        ComboBox1.Items.Add("No Usa")
        ComboBox1.Items.Add("Femsa")
        ComboBox1.Items.Add("Oxxo")
        ComboBox1.Items.Add("Casa Ley")
        ComboBox1.Items.Add("PEPSICO")
        ComboBox1.Items.Add("Grupo Modelo")
        ComboBox1.Items.Add("Complemento INE")
        ComboBox1.Items.Add("Comercio Exterior")
        ComboBox1.Items.Add("Soriana")
        ComboBox1.Items.Add("Walmart")
        If IdCliente = 0 Then
            Nuevo()
        Else
            LlenaDatos()
        End If
        consulta1 = False
        If cmbIdentificacion.Items.Count > 0 Then
            cmbIdentificacion.SelectedIndex = 0
        End If


    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClientesCambio, PermisosN.Secciones.Catalagos) = True Then
            Dim f As New frmClientesEquipos(IdCliente, 0, 1)
            f.ShowDialog()
            f.Dispose()
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub TextBox33_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox33.TextChanged
        Consulta()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClientesCambio, PermisosN.Secciones.Catalagos) = True Then
            Dim fCC As New frmClientesCuentas(IdCliente)
            fCC.ShowDialog()
            fCC.Dispose()
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            RadioButton2.Checked = False
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            RadioButton1.Checked = False
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClientesCambio, PermisosN.Secciones.Catalagos) = True Then
            Dim fCC As New frmClientesImpuestos(IdCliente)
            fCC.ShowDialog()
            fCC.Dispose()
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        webcam.timer_tick(pctImagen)
    End Sub


    Private Sub btnNuevoWebCam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoWebCam.Click
        If btnNuevoWebCam.Text = "Nuevo" Then
            webcam.Iniciar(Timer1, Me)
            btnNuevoWebCam.Text = "Capturar"
            btnEliminarWebCam.Text = "Detener"


        Else
            Dim ruta As String

            ruta = webcam.Capturar(pctImagen, IdCliente)
            imagen = ruta
            webcam.Detener(Timer1)
            mostrarImagen(ruta)
            btnNuevoWebCam.Text = "Nuevo"
            btnEliminarWebCam.Text = "Eliminar"
        End If

    End Sub

    Private Sub btnEliminarWebCam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarWebCam.Click
        detenerEliminarCam()
    End Sub
    Private Sub detenerEliminarCam()
        If btnEliminarWebCam.Text = "Detener" Then
            webcam.Detener(Timer1)
            mostrarImagen("")
            btnNuevoWebCam.Text = "Nuevo"
            btnEliminarWebCam.Text = "Eliminar"
        Else
           

            mostrarImagen("")
            imagen = ""
            btnNuevoWebCam.Text = "Nuevo"
            btnEliminarWebCam.Text = "Eliminar"
        End If
    End Sub

    Private Sub mostrarImagen(ByVal ruta As String)
        
        If ruta <> "" Then


            pctImagen.Image = Image.FromFile(ruta)

            ' Get the scale factor.
            ' Dim scale_factor As Single = Single.Parse(txtScale.Text)

            ' Get the source bitmap.
            Dim bm_source As New Bitmap(pctImagen.Image)

            ' Make a bitmap for the result.
            Dim bm_dest As New Bitmap( _
                CInt(188), _
                CInt(165))

            ' Make a Graphics object for the result Bitmap.
            Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)

            ' Copy the source image into the destination bitmap.
            gr_dest.DrawImage(bm_source, 0, 0, _
                bm_dest.Width + 1, _
                bm_dest.Height + 1)

            ' Display the result.
            pctImagen.Image = bm_dest
        Else
            pctImagen.Image = Nothing

        End If




    End Sub

    Private Sub cmbIdentificacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbIdentificacion.SelectedIndexChanged
        If consulta1 = False Then
            Dim P As New dbClientes(MySqlcon)
            Dim idClasID As Integer

            If cmbIdentificacion.SelectedIndex <= 0 Then
                idClasID = 0
                btnEliminarID.Enabled = False
                btnGuardarID.Enabled = False
                'btnGuardarID.Text = "Guardar"
            Else
                idClasID = IdsIdentificacion.Valor(cmbIdentificacion.SelectedIndex)
                btnGuardarID.Enabled = True
            End If
            If P.existeIdentificacion(IdCliente, idClasID) > 0 Then
                btnGuardarID.Text = "Modificar"
                btnEliminarID.Enabled = True
                btnGuardarID.Enabled = True
            Else
                btnGuardarID.Text = "Guardar"
                btnEliminarID.Enabled = False
                ' btnGuardarID.Enabled = True
            End If
            txtNumeroID.Text = P.obtenerIdentificacion(IdCliente, idClasID)

        End If
    End Sub

    Private Sub btnGuardarID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardarID.Click

        'If Button1.Text = "Guardar" Then
        '    BotonGuardar(True)
        'End If
        BotonGuardarIdent(False)
        
    End Sub
    Private Function BotonGuardarIdent(ByVal EnGuardar As Boolean) As Boolean

        Dim idClasID As Integer
        Dim P As New dbClientes(MySqlcon)

        If cmbIdentificacion.SelectedIndex <= 0 Then
            idClasID = 0
        Else
            idClasID = IdsIdentificacion.Valor(cmbIdentificacion.SelectedIndex)
        End If
        If idClasID = 0 And EnGuardar = True Then
            Return True
        End If
        If btnGuardarID.Text = "Guardar" Then
            If txtNumeroID.Text <> "" Then
                P.GuardarIdentificacion(IdCliente, idClasID, txtNumeroID.Text)
                btnGuardarID.Text = "Modificar"
                btnEliminarID.Enabled = True
                PopUp("Identificación guardada", 90)
                Return True
            Else
                MsgBox("No se ha proporcionado un número de identificación.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                Return False
            End If
        Else
            P.EliminarIdentificacion(IdCliente, idClasID)
            P.GuardarIdentificacion(IdCliente, idClasID, txtNumeroID.Text)
            PopUp("Identificación modificada", 90)
            Return True
        End If
    End Function
    Private Sub btnEliminarID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarID.Click
        Dim idClasID As Integer
        Dim P As New dbClientes(MySqlcon)
        Dim ultimo As Integer

        If cmbIdentificacion.SelectedIndex <= 0 Then
            idClasID = 0
        Else
            idClasID = IdsIdentificacion.Valor(cmbIdentificacion.SelectedIndex)
        End If

        If MsgBox("¿Desea eliminar esta identificacion?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            P.EliminarIdentificacion(IdCliente, idClasID)
            ultimo = P.buscarUltimoID(IdCliente)
            If ultimo = -100 Then
                cmbIdentificacion.SelectedIndex = 0
            Else
                cmbIdentificacion.SelectedIndex = IdsIdentificacion.Busca(ultimo)
            End If
            PopUp("Identificación modificada", 90)
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim C As New dbClientes(MySqlcon)
        ConsultaOn = False
        If CheckBox4.Checked Then
            TextBox6.Text = C.DaMaximo(True).ToString
        Else
            TextBox6.Text = Format(C.DaMaximo(False), "0000")
        End If
        ConsultaOn = True
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

    Private Sub Label52_Click(sender As Object, e As EventArgs) Handles Label52.Click

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If IdCliente <> 0 Then
            Dim f As New frmClientesDescuentos(IdCliente, TextBox1.Text)
            f.ShowDialog()
            f.Dispose()
        End If
    End Sub

    Private Sub TextBox19_TextChanged(sender As Object, e As EventArgs) Handles TextBox19.TextChanged
        If ConsultaOn And TextBox19.Text.Length = 5 Then
            CSat.DaDatosCP(TextBox19.Text)
            TextBox5.Text = CSat.Municipio
            TextBox9.Text = CSat.Localidad
            cmbEstadomx.Text = CSat.Estado
            TextBox39.Text = CSat.cMunicipio
            TextBox40.Text = CSat.cLocalidad
            TextBox25.Text = CSat.cEstado
            'TextBox12.AutoCompleteCustomSource = CSat.txtSource
        End If
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Dim fbc As New frmBuscadorCatalogosSat(1, TextBox19.Text)
        fbc.Cat = CSat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox41.Text = fbc.Clave
            TextBox12.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim fbc As New frmBuscadorCatalogosSat(2)
        fbc.Cat = CSat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox39.Text = fbc.Clave
            TextBox5.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim fbc As New frmBuscadorCatalogosSat(3)
        fbc.Cat = CSat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox40.Text = fbc.Clave
            TextBox9.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim fbc As New frmBuscadorCatalogosSat(4)
        fbc.Cat = CSat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox25.Text = fbc.Clave
            cmbEstadomx.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim fbc As New frmBuscadorCatalogosSat(5)
        fbc.Cat = CSat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox20.Text = fbc.Clave
            TextBox22.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        If TextBox12.Text = "" Then TextBox41.Text = ""
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text = "" Then TextBox39.Text = ""
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If TextBox9.Text = "" Then TextBox40.Text = ""
    End Sub

    Private Sub cmbEstadomx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEstadomx.SelectedIndexChanged
        If cmbEstadomx.Text = "" Then TextBox25.Text = ""
    End Sub

    Private Sub TextBox22_TextChanged(sender As Object, e As EventArgs) Handles TextBox22.TextChanged
        If TextBox22.Text = "" Then TextBox20.Text = ""
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        If TextBox8.Text.Length = 13 Then CSat.LlenaCombos("tblusoscfdi", ComboBox6, "concat(clave,' ',descripcion)", "nombrem", "clave", X, , , , True)
        If TextBox8.Text.Length = 12 Then CSat.LlenaCombos("tblusoscfdi", ComboBox6, "concat(clave,' ',descripcion)", "nombrem", "clave", X, "moral='Sí'", , , True)
    End Sub
End Class