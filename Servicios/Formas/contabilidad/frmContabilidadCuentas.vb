Imports System.Text.RegularExpressions

Public Class frmContabilidadCuentas
    Dim Cc As dbContabilidadClasificacion
    Dim IdsAgrupador As New elemento
    Dim negrita As New Font("Arial", 8, FontStyle.Bold)
    Dim iN1 As String
    Dim iN2 As String
    Dim iN3 As String
    Dim iN4 As String
    Dim iN5 As String
    Public Sub New(pN1 As String, pN2 As String, pN3 As String, pN4 As String, pN5 As String)

        ' This call is required by the designer.
        InitializeComponent()
        iN1 = pN1
        iN2 = pN2
        iN3 = pN3
        iN4 = pN4
        iN5 = pN5
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub frmContabilidadCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Cc = New dbContabilidadClasificacion(MySqlcon)
        LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, , "SELECCIONE UNA CUENTA", "codigo")
        Nuevo()
        If iN1 <> "" Then
            Cc.IDcuenta = Cc.buscarIDlight(iN1, iN2, iN3, iN4, iN5)
            If Cc.IDcuenta <> 0 Then
                LlenaDAtos()
                Select Case Cc.Nivel
                    Case 1
                        NuevoN2(True, False)
                        TextBox2.Text = iN2
                    Case 2
                        NuevoN3(True, False)
                        TextBox3.Text = iN3
                    Case 3
                        NuevoN4(True, False)
                        TextBox4.Text = iN4
                    Case 4
                        NuevoN5(True, False)
                        TextBox5.Text = iN5
                End Select
            Else
                TextBox1.Text = iN1
            End If
        End If
    End Sub
    Private Sub Nuevo()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox1.Enabled = True
        TextBox6.Enabled = True
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
        TextBox9.Enabled = False
        TextBox10.Enabled = False
        cmbTipo1.SelectedIndex = 0
        cmbNaturaleza1.SelectedIndex = 0
        dtpFecha.Value = Date.Now
        chkDIOT.Checked = False
        'cmbTipo.SelectedIndex = 0
        'cmbSubTipo.SelectedIndex = 0
        cmbCuenta.SelectedIndex = 0
        ckAplicarSubcuentas.Checked = False
        CheckBox1.Checked = False
        btnGuardar.Text = "Guardar"
        Button1.Enabled = False
        Cc.Nivel = 1
        Cc.IdCuentaMayor = 0
        Cc.idNivel1 = 0
        Cc.idNivel2 = 0
        Cc.idNivel3 = 0
        Cc.idNivel4 = 0
        Cc.idNivel5 = 0
        Cc.IdCuentaAnt = 0
        Cc.YaTieneMovimientos = False
        Consulta()
        TextBox1.Focus()
    End Sub
    Private Sub NuevoN2(pGuardar As Boolean, pExtra As Boolean)

        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""

        TextBox2.Enabled = True
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox7.Enabled = True
        TextBox9.Enabled = False
        TextBox8.Enabled = False
        TextBox10.Enabled = False
        Cc.Nivel = 2
        Cc.idNivel2 = 0
        Cc.idNivel3 = 0
        Cc.idNivel4 = 0
        Cc.idNivel5 = 0
        If pGuardar Then
            btnGuardar.Text = "Guardar"
            Button1.Enabled = False
            TextBox1.Enabled = False
            TextBox6.Enabled = False
            If pExtra Then
                TextBox2.Text = ""
                TextBox7.Text = ""
                TextBox2.Focus()
            End If
        Else
            TextBox2.Text = ""
            TextBox7.Text = ""
            TextBox2.Focus()
        End If
    End Sub
    Private Sub NuevoN3(pGuardar As Boolean, pExtra As Boolean)


        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox2.Enabled = False
        TextBox7.Enabled = False
        TextBox1.Enabled = False
        TextBox6.Enabled = False
        TextBox3.Enabled = True
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox8.Enabled = True
        TextBox9.Enabled = False
        TextBox10.Enabled = False
        Cc.Nivel = 3
        Cc.idNivel3 = 0
        Cc.idNivel4 = 0
        Cc.idNivel5 = 0
        If pGuardar Then
            btnGuardar.Text = "Guardar"
            Button1.Enabled = False
            If pExtra Then
                TextBox3.Text = ""
                TextBox8.Text = ""
                TextBox3.Focus()
            End If
        Else
            TextBox3.Text = ""
            TextBox8.Text = ""
            TextBox3.Focus()
        End If
    End Sub
    Private Sub NuevoN4(pGuardar As Boolean, pExtra As Boolean)
        TextBox5.Text = ""
        TextBox1.Enabled = False
        TextBox6.Enabled = False
        TextBox2.Enabled = False
        TextBox7.Enabled = False
        TextBox3.Enabled = False
        TextBox8.Enabled = False
        TextBox10.Text = ""
        TextBox4.Enabled = True
        TextBox5.Enabled = False
        TextBox9.Enabled = True
        TextBox10.Enabled = False
        Cc.Nivel = 4
        Cc.idNivel4 = 0
        Cc.idNivel5 = 0
        If pGuardar Then
            btnGuardar.Text = "Guardar"
            Button1.Enabled = False
            If pExtra Then
                TextBox4.Text = ""
                TextBox9.Text = ""
                TextBox4.Focus()
            End If
        Else
            TextBox4.Text = ""
            TextBox9.Text = ""
            TextBox4.Focus()
        End If

    End Sub
    Private Sub NuevoN5(pGuardar As Boolean, pExtra As Boolean)

        TextBox1.Enabled = False
        TextBox6.Enabled = False
        TextBox2.Enabled = False
        TextBox7.Enabled = False
        TextBox3.Enabled = False
        TextBox8.Enabled = False
        TextBox4.Enabled = False
        TextBox9.Enabled = False
        TextBox5.Enabled = True
        TextBox10.Enabled = True
        Cc.Nivel = 5
        Cc.idNivel5 = 0
        If pGuardar Then
            btnGuardar.Text = "Guardar"
            Button1.Enabled = False
            If pExtra Then
                TextBox5.Text = ""
                TextBox10.Text = ""
                TextBox5.Focus()
            End If
        Else
            TextBox5.Text = ""
            TextBox10.Text = ""
            TextBox5.Focus()
        End If

    End Sub
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Botonguardar()
    End Sub
    Private Sub Botonguardar()
        Try
            Dim Errores As String = ""
            If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.CuentasAlta, PermisosN.Secciones.Contabilidad) = False And btnGuardar.Text = "Guardar" Then
                Errores += "No tiene permiso para realizar esta operación. "
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.CuentasModificar, PermisosN.Secciones.Contabilidad) = False And btnGuardar.Text <> "Guardar" Then
                Errores += "No tiene permiso para realizar esta operación. "
            End If
            If Cc.Nivel = 1 Then
                If TextBox1.Text = "" Or TextBox6.Text = "" Then
                    Errores += " Debe indicar un número y descripción a la cuenta. "
                Else
                    If IsNumeric(TextBox1.Text) = False Then
                        Errores += "El número de cuenta debe contener un valor numérico. "
                    End If
                End If
            End If
            If Cc.Nivel = 2 Then
                If TextBox2.Text = "" Or TextBox7.Text = "" Then
                    Errores += " Debe indicar un número y descripción a la cuenta. "
                Else
                    If IsNumeric(TextBox2.Text) = False Then
                        Errores += "El número de cuenta debe contener un valor numérico. "
                    End If
                End If
            End If
            If Cc.Nivel = 3 Then
                If TextBox3.Text = "" Or TextBox8.Text = "" Then
                    Errores += " Debe indicar un número y descripción a la cuenta. "
                Else
                    If IsNumeric(TextBox3.Text) = False Then
                        Errores += "El número de cuenta debe contener un valor numérico. "
                    End If
                End If
            End If
            If Cc.Nivel = 4 Then
                If TextBox4.Text = "" Or TextBox9.Text = "" Then
                    Errores += " Debe indicar un número y descripción a la cuenta. "
                Else
                    If IsNumeric(TextBox4.Text) = False Then
                        Errores += "El número de cuenta debe contener un valor numérico. "
                    End If
                End If
            End If
            If Cc.Nivel = 5 Then
                If TextBox5.Text = "" Or TextBox10.Text = "" Then
                    Errores += " Debe indicar un número y descripción a la cuenta. "
                Else
                    If IsNumeric(TextBox5.Text) = False Then
                        Errores += "El número de cuenta debe contener un valor numérico. "
                    End If
                End If
            End If
            If cmbCuenta.SelectedIndex <= 0 Then
                Errores += " Debe seleccionar un agrupador SAT válido. "
            End If
            Select Case Cc.Nivel
                Case 1
                    Cc.Descripcion = TextBox6.Text
                Case 2
                    Cc.Descripcion = TextBox7.Text
                Case 3
                    Cc.Descripcion = TextBox8.Text
                Case 4
                    Cc.Descripcion = TextBox9.Text
                Case 5
                    Cc.Descripcion = TextBox10.Text
            End Select
            Cc.Descripcion = Cc.Descripcion
            If Errores = "" Then
                If btnGuardar.Text = "Guardar" Then
                    Select Case Cc.Nivel
                        Case 1
                            If Cc.repetida(TextBox1.Text, "", "", "", "", 0, 1) Then Errores += "La cuenta ya existe. "
                        Case 2
                            If Cc.repetida(TextBox1.Text, TextBox2.Text, "", "", "", 0, 2) Then Errores += "La cuenta ya existe. "
                        Case 3
                            If Cc.repetida(TextBox1.Text, TextBox2.Text, TextBox3.Text, "", "", 0, 3) Then Errores += "La cuenta ya existe. "
                        Case 4
                            If Cc.repetida(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, "", 0, 4) Then Errores += "La cuenta ya existe. "
                        Case 5
                            If Cc.repetida(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, 0, 5) Then Errores += "La cuenta ya existe. "
                    End Select
                    If Cc.YaTieneMovimientos And Cc.Nivel > 1 Then Errores += " No se pueden agregar subcuentas a una cuenta con movimientos. "
                    If Cc.NombreRepetido(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, Cc.Descripcion, Cc.Nivel) Then Errores += "Ya existe una cuenta con esa descripción. "
                Else
                    If TextBox1.Text + TextBox2.Text + TextBox3.Text + TextBox4.Text + TextBox5.Text <> Cc.CuentaAnt Then
                        Select Case Cc.Nivel
                            Case 1
                                If Cc.repetida(TextBox1.Text, "", "", "", "", 0, 1) Then Errores += "La cuenta ya existe. "
                            Case 2
                                If Cc.repetida(TextBox1.Text, TextBox2.Text, "", "", "", 0, 2) Then Errores += "La cuenta ya existe. "
                            Case 3
                                If Cc.repetida(TextBox1.Text, TextBox2.Text, TextBox3.Text, "", "", 0, 3) Then Errores += "La cuenta ya existe. "
                            Case 4
                                If Cc.repetida(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, "", 0, 4) Then Errores += "La cuenta ya existe. "
                            Case 5
                                If Cc.repetida(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, 0, 5) Then Errores += "La cuenta ya existe. "
                        End Select
                    End If
                    If Cc.NombreAnt <> Cc.Descripcion Then
                        If Cc.NombreRepetido(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, Cc.Descripcion, Cc.Nivel) Then Errores += "Ya existe una cuenta con esa descripción. "
                    End If
                End If
            End If
            If Errores = "" Then
                If chkDIOT.Checked = True Then
                    Cc.DIOT = 1
                Else
                    Cc.DIOT = 0
                End If

                If btnGuardar.Text = "Guardar" Then
                    Cc.Guardar(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, Cc.Descripcion, Cc.Nivel, cmbTipo1.SelectedIndex.ToString, cmbNaturaleza1.SelectedIndex.ToString, dtpFecha.Value.ToString("yyyy/MM/dd"), cmbTipo.SelectedIndex, cmbSubTipo.SelectedIndex, IdsAgrupador.Valor(cmbCuenta.SelectedIndex), Cc.DIOT, Cc.IdCuentaMayor, Cc.idNivel2, Cc.idNivel3, Cc.idNivel4, Cc.idNivel5)

                    Select Case Cc.Nivel
                        Case 1
                            If CheckBox3.Checked Then
                                Nuevo()
                            Else
                                NuevoN2(False, False)
                                Consulta()
                            End If
                        Case 2
                            If CheckBox4.Checked Then
                                NuevoN2(False, False)
                            Else
                                NuevoN3(False, False)
                            End If
                            Consulta()
                        Case 3
                            If CheckBox5.Checked Then
                                NuevoN3(False, False)
                            Else
                                NuevoN4(False, False)
                            End If
                            Consulta()
                        Case 4
                            If CheckBox6.Checked Then
                                NuevoN4(False, False)
                            Else
                                NuevoN5(False, False)
                            End If
                            Consulta()
                        Case 5
                            If CheckBox7.Checked Then
                                NuevoN5(False, False)
                                Consulta()
                            Else
                                Nuevo()
                            End If
                    End Select
                    If iN1 <> "" Then
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                    End If
                    PopUp("Cuenta guardada.", 30, False)
                Else
                    If MsgBox("¿Guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If CheckBox1.Checked Then
                            Cc.Descontinuada = Descontinuar()
                        Else
                            If Cc.Descontinuada <> "" Then
                                Cc.Descontinuada = Descontinuar()
                            End If
                        End If
                        Cc.Modificar(Cc.IDcuenta, TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, Cc.Descripcion, Cc.Nivel, cmbTipo1.SelectedIndex.ToString, cmbNaturaleza1.SelectedIndex.ToString, dtpFecha.Value.ToString("yyyy/MM/dd"), cmbTipo.SelectedIndex, cmbSubTipo.SelectedIndex, IdsAgrupador.Valor(cmbCuenta.SelectedIndex), Cc.DIOT, Cc.Descontinuada)
                        If ckAplicarSubcuentas.Checked Then
                            Cc.ModificarAgrupador(Cc.IDcuenta, Cc.Nivel, IdsAgrupador.Valor(cmbCuenta.SelectedIndex))
                        End If
                        Nuevo()
                        PopUp("Cuenta modificada", 30, False)
                    End If
                    End If
            Else
                    MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub
    Private Function Descontinuar() As String
        Dim Descontinuada As String = ""
        Descontinuada = Cc.Descontinuada
        If Descontinuada.Contains(Cc.p.anio) Then
            Descontinuada = Descontinuada.Replace(Cc.p.anio + " ", "")
        Else
            Dim C As Integer = CInt(Cc.p.anio)
            Dim Temp() As String
            Dim Temp2(2) As String
            Dim Encontro As Boolean = False
            Temp = Descontinuada.Split(" ")
            For Each Str As String In Temp
                If Str > Cc.p.anio Then
                    Temp2(1) += Str + " "
                    Encontro = True
                Else
                    Temp2(0) += Str + " "
                End If
            Next
            If Encontro Then
                Descontinuada = Temp2(0)
            End If
            Dim Limite As Integer = C + 20
            While C < Limite
                Descontinuada += C.ToString + " "
                C += 1
            End While
            End If
            Return Descontinuada
    End Function
    Private Sub cmbTipo_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbTipo.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbSubTipo.Focus()
        End If
    End Sub
    Private Sub cmbTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipo.SelectedIndexChanged
        'If cmbTipo.SelectedIndex = 0 Or cmbTipo.SelectedIndex = 1 Then
        '    cmbSubTipo.Items.Clear()
        '    cmbSubTipo.Items.Add("TODOS")
        '    cmbSubTipo.Items.Add("ACTIVO A CORTO PLAZO")
        '    cmbSubTipo.Items.Add("ACTIVO A LARGO PLAZO")
        '    cmbSubTipo.SelectedIndex = 0
        '    cmbSubTipo.Enabled = True
        'Else
        '    cmbSubTipo.Items.Clear()
        '    cmbSubTipo.Enabled = False
        '    cmbCuenta.Enabled = True
        'End If
        'If cmbTipo.SelectedIndex = 2 Then
        '    cmbSubTipo.Items.Clear()
        '    LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>301 and codigo<=307 ", "SELECCIONE UNA CUENTA", "id")

        'End If
        'If cmbTipo.SelectedIndex = 3 Then
        '    cmbSubTipo.Items.Clear()
        '    LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>=400 and codigo<=404 ", "SELECCIONE UNA CUENTA", "id")
        'End If
        'If cmbTipo.SelectedIndex = 4 Then
        '    cmbSubTipo.Items.Clear()
        '    LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>=500 and codigo<=506 ", "SELECCIONE UNA CUENTA", "id")
        'End If
        'If cmbTipo.SelectedIndex = 5 Then
        '    cmbSubTipo.Items.Clear()
        '    LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>=600 and codigo<=615 ", "SELECCIONE UNA CUENTA", "id")
        'End If
        'If cmbTipo.SelectedIndex = 6 Then
        '    cmbSubTipo.Items.Clear()
        '    LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>=700 and codigo<=705 ", "SELECCIONE UNA CUENTA", "id")

        'End If
        'If cmbTipo.SelectedIndex = 7 Then
        '    cmbSubTipo.Items.Clear()
        '    LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>=800 and codigo<900 ", "SELECCIONE UNA CUENTA", "id")
        'End If
        'If cmbTipo.SelectedIndex = 8 Then
        '    cmbSubTipo.Items.Clear()
        '    LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo=0 ", "SELECCIONE UNA CUENTA", "id")
        'End If
        'cmbCuenta.SelectedIndex = 0
    End Sub

    Private Sub cmbSubTipo_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbSubTipo.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbCuenta.Focus()
        End If
    End Sub

    Private Sub cmbSubTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSubTipo.SelectedIndexChanged
        'If cmbTipo.SelectedIndex = 0 Then
        '    If cmbSubTipo.SelectedIndex = 0 Then
        '        cmbCuenta.Items.Clear()
        '        LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>=100 and codigo<=199 ", "SELECCIONE UNA CUENTA", "id")
        '    End If
        '    If cmbSubTipo.SelectedIndex = 1 Then
        '        cmbCuenta.Items.Clear()
        '        LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>=100 and codigo<=122 ", "SELECCIONE UNA CUENTA", "id")
        '    End If
        '    If cmbSubTipo.SelectedIndex = 2 Then
        '        cmbCuenta.Items.Clear()
        '        LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>=150 and codigo<=199 ", "SELECCIONE UNA CUENTA", "id")
        '    End If
        'End If
        'If cmbTipo.SelectedIndex = 1 Then
        '    If cmbSubTipo.SelectedIndex = 0 Then
        '        cmbCuenta.Items.Clear()
        '        LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>=200 and codigo<=270 ", "SELECCIONE UNA CUENTA", "id")
        '    End If
        '    If cmbSubTipo.SelectedIndex = 1 Then
        '        cmbCuenta.Items.Clear()
        '        LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>=200 and codigo<=220", "SELECCIONE UNA CUENTA", "id")
        '    End If
        '    If cmbSubTipo.SelectedIndex = 2 Then
        '        cmbCuenta.Items.Clear()
        '        LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", IdsAgrupador, "codigo>=250 and codigo<=270 ", "SELECCIONE UNA CUENTA", "id")
        '    End If
        'End If
    End Sub

    Private Sub cmbCuenta_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbCuenta.KeyDown
        If e.KeyValue = Keys.F1 Then
            Dim b As New frmContabilidadBuscarAgrupador()
            b.ShowDialog()
            If b.DialogResult = Windows.Forms.DialogResult.OK Then
                'If b.TIPO = "ACTIVO" Then
                '    cmbTipo.SelectedIndex = 0
                'Else
                '    If b.TIPO = "PASIVO" Then
                '        cmbTipo.SelectedIndex = 1
                '    Else
                '        If b.TIPO = "CAPITAL CONTABLE" Then
                '            cmbTipo.SelectedIndex = 2
                '        Else
                '            If b.TIPO = "INGRESOS" Then
                '                cmbTipo.SelectedIndex = 3
                '            Else
                '                If b.TIPO = "COSTOS" Then
                '                    cmbTipo.SelectedIndex = 4
                '                Else
                '                    If b.TIPO = "GASTOS" Then
                '                        cmbTipo.SelectedIndex = 5
                '                    Else
                '                        If b.TIPO = "RESULTADO INTEGRAL DE FINANCIAMIENTO" Then
                '                            cmbTipo.SelectedIndex = 6
                '                        Else
                '                            If b.TIPO = "CUENTAS DE ORDEN" Then
                '                                cmbTipo.SelectedIndex = 7
                '                            Else
                '                                If b.TIPO = "OTROS" Then
                '                                    cmbTipo.SelectedIndex = 8
                '                                End If
                '                            End If
                '                        End If
                '                    End If
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
                ''cuenta.Busca(b.id)
                cmbCuenta.Text = b.id
            End If
        End If
        If e.KeyCode = Keys.Enter Then
            Botonguardar()
        End If
    End Sub

    Private Sub Consulta()
        'If llenando = False Then

        Try
            'Dim cuenta As String = txtBuscar.Text
            Dim PrimerCeldaRow As Integer = -1

            Dim Palabras() As String
            Dim txtAbuscar As String = ""
            Dim txtAbuscar2 As String = ""
            Palabras = TextBox11.Text.Split(Chr(32))
            For Each palabra As String In Palabras
                If Regex.IsMatch(palabra, "[A-Z]|[a-z]") Then
                    txtAbuscar += " " + palabra
                Else
                    txtAbuscar2 += " " + palabra
                End If
            Next
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            DataGridView1.DataSource = Cc.Consulta(txtAbuscar2.Trim, 0, txtAbuscar.Trim, True, CheckBox2.Checked)

            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(1).HeaderText = "Cuenta"
            DataGridView1.Columns(2).HeaderText = "Descripción"
            DataGridView1.Columns(4).HeaderText = "Tipo"
            DataGridView1.Columns(5).HeaderText = "Nat."
            DataGridView1.Columns(6).HeaderText = "Agr."
            DataGridView1.Columns(1).Width = 140
            DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView1.Columns(4).Width = 60
            DataGridView1.Columns(5).Width = 75
            DataGridView1.Columns(6).Width = 50
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        '    End If
        'End If

    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        Consulta()
    End Sub

    Private Sub TextBox6_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox6.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox2.Enabled Then
                TextBox2.Focus()
            Else
                cmbTipo1.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox6_Leave(sender As Object, e As EventArgs) Handles TextBox6.Leave
        If btnGuardar.Text <> "Guardar" And CheckBox3.Checked = False Then
            TextBox2.Enabled = True
            TextBox7.Enabled = True
            TextBox2.Focus()
        End If
    End Sub
    
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If CheckBox3.Checked = True Or (CheckBox4.Checked = False And CheckBox5.Checked = False And CheckBox6.Checked = False And CheckBox7.Checked = False) Then
            Nuevo()
        End If
        If CheckBox4.Checked Then
            NuevoN2(True, True)
        End If
        If CheckBox5.Checked Then
            NuevoN3(True, True)
        End If
        If CheckBox6.Checked Then
            NuevoN4(True, True)
        End If
        If CheckBox7.Checked Then
            NuevoN5(True, True)
        End If
        
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox6.Focus()
        End If
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If TextBox1.Text <> "" Then TextBox1.Text = TextBox1.Text.PadLeft(Cc.p.NNiv1, "0")
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox7.Focus()
        End If
    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        If TextBox2.Text <> "" Then TextBox2.Text = TextBox2.Text.PadLeft(Cc.p.NNiv2, "0")
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If Cc.Nivel = 1 Then
            NuevoN2(True, False)
        End If
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox8.Focus()
        End If
    End Sub

    Private Sub TextBox3_Leave(sender As Object, e As EventArgs) Handles TextBox3.Leave
        If TextBox3.Text <> "" Then TextBox3.Text = TextBox3.Text.PadLeft(Cc.p.NNiv3, "0")
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If Cc.Nivel = 2 Then
            NuevoN3(True, False)
        End If
    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox9.Focus()
        End If
    End Sub

    Private Sub TextBox4_Leave(sender As Object, e As System.EventArgs) Handles TextBox4.Leave
        If TextBox4.Text <> "" Then TextBox4.Text = TextBox4.Text.PadLeft(Cc.p.NNiv4, "0")
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If Cc.Nivel = 3 Then
            NuevoN4(True, False)
        End If
    End Sub

    Private Sub TextBox5_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox10.Focus()
        End If
    End Sub

    Private Sub TextBox5_Leave(sender As Object, e As EventArgs) Handles TextBox5.Leave
        If TextBox5.Text <> "" Then TextBox5.Text = TextBox5.Text.PadLeft(Cc.p.NNiv5, "0")
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If Cc.Nivel = 4 Then
            NuevoN5(True, False)
        End If
    End Sub

    Private Sub TextBox7_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Enabled Then
                TextBox3.Focus()
            Else
                cmbTipo1.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox7_Leave(sender As Object, e As System.EventArgs) Handles TextBox7.Leave
        If btnGuardar.Text <> "Guardar" And CheckBox4.Checked = False And TextBox7.Text <> "" And TextBox2.Text <> "" Then
            TextBox3.Enabled = True
            TextBox8.Enabled = True
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub TextBox8_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox4.Enabled Then
                TextBox4.Focus()
            Else
                cmbTipo1.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox8_Leave(sender As Object, e As System.EventArgs) Handles TextBox8.Leave
        If btnGuardar.Text <> "Guardar" And CheckBox5.Checked = False And TextBox8.Text <> "" And TextBox3.Text <> "" Then
            TextBox4.Enabled = True
            TextBox9.Enabled = True
            TextBox4.Focus()
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub TextBox9_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox9.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox5.Enabled Then
                TextBox5.Focus()
            Else
                cmbTipo1.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox9_Leave(sender As Object, e As System.EventArgs) Handles TextBox9.Leave
        If btnGuardar.Text <> "Guardar" And CheckBox6.Checked = False And TextBox9.Text <> "" And TextBox4.Text <> "" Then
            TextBox5.Enabled = True
            TextBox10.Enabled = True
            TextBox5.Focus()
        End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged

    End Sub

    Private Sub TextBox10_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox10.KeyDown
        If e.KeyCode = Keys.Enter Then
                cmbTipo1.Focus()
        End If
    End Sub

    Private Sub cmbTipo1_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbTipo1.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbNaturaleza1.Focus()
        End If
    End Sub

    Private Sub cmbTipo1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipo1.SelectedIndexChanged
        cmbTipo.SelectedIndex = cmbTipo1.SelectedIndex
    End Sub

    Private Sub cmbNaturaleza1_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbNaturaleza1.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpFecha.Focus()
        End If
    End Sub

    Private Sub cmbNaturaleza1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNaturaleza1.SelectedIndexChanged

    End Sub

    Private Sub dtpFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFecha.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbTipo.Focus()
        End If
    End Sub

    Private Sub dtpFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha.ValueChanged

    End Sub

    Private Sub cmbCuenta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCuenta.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim MensajeError As String = ""
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.CuentasBaja, PermisosN.Secciones.Contabilidad) = False Then
                MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            'If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos) = True Then
            If Cc.BuscaSubcuentas(Cc.IDcuenta) > 1 Then
                MensajeError = "Esta cuenta contiene subcuentas, las cuales, se borrarán al eliminar esta." + vbCrLf
            End If

            If MsgBox(MensajeError + "¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                If Cc.TieneMovimientosLasSubcuentas(Cc.IDcuenta) Then
                    MsgBox("Esta cuenta no puede ser eliminada." + vbCrLf + "Esta o alguna(s) subcuenta(s) de la misma, está siendo siendo usada por una o más pólizas.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                Else
                    Cc.Eliminar(Cc.N1, Cc.N2, Cc.N3, Cc.N4, Cc.N5, Cc.Nivel)
                    PopUp("Eliminado", 90)
                    Nuevo()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Cc.IDcuenta = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            LlenaDAtos()
        End If
    End Sub

  
    Private Sub LlenaDAtos()
        Try
            Cc.busquedaRegistro(Cc.IDcuenta)
            Cc.IdCuentaAnt = Cc.IDcuenta
            Cc.YaTieneMovimientos = Cc.TieneMovimientos(Cc.IDcuenta)
            Cc.N1Ant = Cc.N1
            Cc.N2Ant = Cc.N2
            Cc.N3Ant = Cc.N3
            Cc.N4Ant = Cc.N4
            Cc.N5Ant = Cc.N5
            Dim niveltemp As Integer = Cc.Nivel
            Cc.Nivel = 0
            TextBox1.Text = Cc.N1.PadLeft(Cc.p.NNiv1, "0")
            If Cc.N2 <> "" Then
                TextBox2.Text = Cc.N2.PadLeft(Cc.p.NNiv2, "0")
            Else
                TextBox2.Text = ""
            End If
            If Cc.N3 <> "" Then
                TextBox3.Text = Cc.N3.PadLeft(Cc.p.NNiv3, "0")
            Else
                TextBox3.Text = ""
            End If
            If Cc.N4 <> "" Then
                TextBox4.Text = Cc.N4.PadLeft(Cc.p.NNiv4, "0")
            Else
                TextBox4.Text = ""
            End If
            If Cc.N5 <> "" Then
                TextBox5.Text = Cc.N5.PadLeft(Cc.p.NNiv5, "0")
            Else
                TextBox5.Text = ""
            End If
            TextBox6.Text = Cc.Descripcion
            TextBox7.Text = Cc.Descripcion2
            TextBox8.Text = Cc.Descripcion3
            TextBox9.Text = Cc.Descripcion4
            TextBox10.Text = Cc.Descripcion5
            If Cc.Depreciacion = 0 Then
                cmbNaturaleza1.SelectedIndex = Cc.Naturaleza
            Else
                cmbNaturaleza1.SelectedIndex = 2
            End If
            cmbTipo1.SelectedIndex = Cc.Tipo
            dtpFecha.Value = Cc.fecha
            If Cc.DIOT = 1 Then
                chkDIOT.Checked = True
            Else
                chkDIOT.Checked = False
            End If
            'If Cc.Nivel <> 1 Then
            '    cmbNaturaleza1.Enabled = False
            '    cmbTipo1.Enabled = False
            'Else
            '    cmbNaturaleza1.Enabled = True
            '    cmbTipo1.Enabled = True
            'End If
            cmbCuenta.SelectedIndex = IdsAgrupador.Busca(Cc.idContable)
            If Cc.Descontinuada.Contains(Cc.p.anio) Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
            Cc.Nivel = niveltemp
            btnGuardar.Text = "Modificar"
            Button1.Enabled = True
            Habilita(Cc.Nivel)
            Select Case Cc.Nivel
                Case 1
                    TextBox1.Focus()
                Case 2
                    TextBox2.Focus()
                Case 3
                    TextBox3.Focus()
                Case 4
                    TextBox4.Focus()
                Case 5
                    TextBox5.Focus()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub
    Private Sub Habilita(pNivel As Byte)
        TextBox1.Enabled = False
        TextBox6.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
        TextBox9.Enabled = False
        TextBox10.Enabled = False
        If pNivel = 1 Then
            TextBox1.Enabled = True
            TextBox6.Enabled = True
        End If
        If pNivel = 2 Then
            TextBox2.Enabled = True
            TextBox7.Enabled = True
        End If
        If pNivel = 3 Then
            TextBox3.Enabled = True
            TextBox8.Enabled = True
        End If
        If pNivel = 4 Then
            TextBox4.Enabled = True
            TextBox9.Enabled = True
        End If
        If pNivel = 5 Then
            TextBox5.Enabled = True
            TextBox10.Enabled = True
        End If
    End Sub
   
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If DataGridView1.Item(3, e.RowIndex).Value.ToString = "1" Then
            e.CellStyle.BackColor = Color.FromArgb(212, 212, 255)
            e.CellStyle.Font = negrita
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked Then
            CheckBox4.Checked = False
            CheckBox5.Checked = False
            CheckBox6.Checked = False
            CheckBox3.Checked = False
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            CheckBox4.Checked = False
            CheckBox5.Checked = False
            CheckBox6.Checked = False
            CheckBox7.Checked = False
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            CheckBox3.Checked = False
            CheckBox5.Checked = False
            CheckBox6.Checked = False
            CheckBox7.Checked = False
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked Then
            CheckBox4.Checked = False
            CheckBox3.Checked = False
            CheckBox6.Checked = False
            CheckBox7.Checked = False
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked Then
            CheckBox4.Checked = False
            CheckBox5.Checked = False
            CheckBox3.Checked = False
            CheckBox7.Checked = False
        End If
    End Sub

    Private Sub btnXML_Click(sender As Object, e As EventArgs) Handles btnXML.Click
        Dim B As New frmContabilidadXML()
        B.ShowDialog()
        B.Dispose()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim instru As New frmInstrucciones()
        instru.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            'OpenFileDialog1.FileName = "CUENTAS"
            'OpenFileDialog1.Filter = "*.xls|*.xlsx"
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim I As New Importador(OpenFileDialog1.FileName, MySqlcon)
                I.ImportaCuentasContables()
                I.CierraConexiones()
                Consulta()
                MsgBox("Importación exitosa.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Rep = New repContabilidadCat
        Rep.SetDataSource(Cc.Consulta("", 0, "", True, CheckBox2.Checked))
        Rep.SetParameterValue("empresa", s.Nombre)
        Rep.SetParameterValue("rfc", s.RFC)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Consulta()
    End Sub
End Class