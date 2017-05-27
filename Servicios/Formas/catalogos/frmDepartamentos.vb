Public Class frmDepartamentos
    Dim idsTipos As New elemento
    Dim idsTipos2 As New elemento
    Dim idsTipos3 As New elemento
    Dim idTipo As Integer = -1
    Dim idTipo2 As Integer = -1
    Dim idTipo3 As Integer = -1
    Dim NombreAnt1 As String
    Dim CodigoAnt1 As String
    Dim NombreAnt2 As String
    Dim CodigoAnt2 As String
    Dim NombreAnt3 As String
    Dim CodigoAnt3 As String
    Dim ConsultaOn As Boolean = True

    Private Sub Nuevo1()
        Try
            ConsultaOn = False
            ComboBox1.Text = ""
            TextBox1.Text = ""
            Button1.Text = "Guardar"
            Button2.Enabled = False
            HabilitaArea(False)
            LlenaCombos("tbldepartamentos", ComboBox1, "nombre", "nombret", "iddepartamento", idsTipos)
            idTipo = -1
            TextBox1.Focus()
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo2()
        Try
            ConsultaOn = False
            ComboBox2.Text = ""
            TextBox2.Text = ""
            Button7.Text = "Guardar"
            Button6.Enabled = False
            LlenaCombos("tbldepartamentosareas", ComboBox2, "nombre", "nombret", "iddepartamento", idsTipos2, "iddepartamentos=" + idTipo.ToString)
            idTipo2 = -1
            ConsultaOn = True
            TextBox2.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub frmDepartamentos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Nuevo1()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim TC As New dbDepartamentos(MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel1)
            Dim NoError As Boolean = True
            Dim Errores As String = ""
            '---------------
            If ComboBox1.Text <> "" And TextBox1.Text <> "" Then
                If Button1.Text = "Guardar" Then
                    'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Departamentos + 1)))) <> 0 Then
                    If TC.ChecaNombreRepetido(ComboBox1.Text) Then
                        Errores += "Ya existe un departamento con ese nombre."
                        NoError = False
                    End If
                    If TC.ChecaCodigoRepetido(TextBox1.Text) Then
                        NoError = False
                        Errores += " Ya existe un departamento con ese código."
                    End If
                    If NoError Then
                        TC.Guardar(ComboBox1.Text, TextBox1.Text)
                        PopUp("Guardado", 90)
                        idTipo = TC.ID
                        Button1.Text = "Modificar"
                        Button2.Enabled = True
                        HabilitaArea(True)
                        Nuevo2()
                        'Nuevo1()
                    Else
                        MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                    'Else
                    '   MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    'End If
                Else
                    'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Departamentos + 2)))) <> 0 Then
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If TC.ChecaNombreRepetido(ComboBox1.Text) And ComboBox1.Text <> NombreAnt1 Then
                            Errores += "Ya existe un departamento con ese nombre."
                            NoError = False
                        End If
                        If TC.ChecaCodigoRepetido(TextBox1.Text) And TextBox1.Text <> CodigoAnt1 Then
                            NoError = False
                            Errores += " Ya existe un departamento con ese código."
                        End If
                        If NoError Then
                            TC.Modificar(idTipo, ComboBox1.Text, TextBox1.Text)
                            PopUp("Modificado", 90)
                            TextBox1.Focus()
                            'Nuevo1()
                        Else
                            MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    End If
                    'Else
                    'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    'End If
                End If

            '----------------------
            Else
            MsgBox("Debe indicar un nombre y código al departamento.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Departamentos + 3)))) <> 0 Then
            Dim TC As New dbDepartamentos(MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel1)
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                TC.Eliminar(idTipo)
                PopUp("Eliminado", 90)
                Nuevo1()
                ComboBox1.Focus()
            End If
            'Else
            'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            'End If
        Catch exm As MySql.Data.MySqlClient.MySqlException

            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este departamento debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo1()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex >= 0 Then
            idTipo = idsTipos.Valor(ComboBox1.SelectedIndex)
            Dim IC2 As New dbDepartamentos(idTipo, MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel1)
            ConsultaOn = False
            TextBox1.Text = IC2.Codigo
            NombreAnt1 = IC2.Nombre
            CodigoAnt1 = IC2.Codigo
            Button1.Text = "Modificar"
            Button2.Enabled = True
            HabilitaArea(True)
            Nuevo2()
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Nuevo2()
    End Sub



    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            Dim TC As New dbDepartamentos(MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel2)
            Dim NoError As Boolean = True
            Dim Errores As String = ""
            '---------------
            If ComboBox2.Text <> "" And TextBox2.Text <> "" Then
                If Button7.Text = "Guardar" Then
                    'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Departamentos + 1)))) <> 0 Then
                    If TC.ChecaNombreRepetido(ComboBox2.Text, idTipo) Then
                        Errores += "Ya existe un área con ese nombre."
                        NoError = False
                    End If
                    If TC.ChecaCodigoRepetido(TextBox2.Text, idTipo) Then
                        NoError = False
                        Errores += " Ya existe un área con ese código."
                    End If
                    If NoError Then
                        TC.Guardar(ComboBox2.Text, TextBox2.Text, idTipo)
                        PopUp("Guardado", 90)
                        Nuevo2()
                    Else
                        MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                    'Else
                    '   MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    '  E'nd If
                Else
                    'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Departamentos + 1)))) <> 0 Then
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If TC.ChecaNombreRepetido(ComboBox2.Text, idTipo) And ComboBox2.Text <> NombreAnt2 Then
                            Errores += "Ya existe un área con ese nombre."
                            NoError = False
                        End If
                        If TC.ChecaCodigoRepetido(TextBox2.Text, idTipo) And TextBox2.Text <> CodigoAnt2 Then
                            NoError = False
                            Errores += " Ya existe un área con ese código."
                        End If
                        If NoError Then
                            TC.Modificar(idTipo2, ComboBox2.Text, TextBox2.Text)
                            PopUp("Modificado", 90)
                            Nuevo2()
                        Else
                            MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    End If
                    'Else
                    'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    'End If
                End If
            TextBox2.Focus()
            '----------------------
            Else
            MsgBox("Debe indicar un nombre y código del área.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Departamentos + 3)))) <> 0 Then
            Dim TC As New dbDepartamentos(MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel2)
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                TC.Eliminar(idTipo2)
                PopUp("Eliminado", 90)
                Nuevo2()
                ComboBox2.Focus()
            End If
            'Else
            'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            'End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar el área debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged

        If ComboBox2.SelectedIndex >= 0 Then
            idTipo2 = idsTipos2.Valor(ComboBox2.SelectedIndex)
            Dim IC2 As New dbDepartamentos(idTipo2, MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel2)
            ConsultaOn = False
            TextBox2.Text = IC2.Codigo
            NombreAnt2 = IC2.Nombre
            CodigoAnt2 = IC2.Codigo
            Button7.Text = "Modificar"
            Button6.Enabled = True
            ConsultaOn = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        'If ConsultaOn Then
        '    Dim IC As New dbDepartamentos(MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel1)
        '    If IC.BuscaDepartamento(TextBox1.Text) Then
        '        ComboBox1.SelectedIndex = idsTipos.Busca(IC.ID)
        '    Else
        '        ComboBox1.Text = ""
        '        'Nuevo2()
        '    End If
        'End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        'If ConsultaOn Then
        '    Dim IC As New dbDepartamentos(MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel2)
        '    If IC.BuscaDepartamento(TextBox2.Text, idTipo) Then
        '        ComboBox2.SelectedIndex = idsTipos2.Busca(IC.ID)
        '    Else
        '        ComboBox2.Text = ""
        '        'Nuevo2()
        '    End If
        'End If
    End Sub
    Private Sub HabilitaArea(ByVal Habilita As Boolean)
        TextBox2.Enabled = Habilita
        ComboBox2.Enabled = Habilita
        Button5.Enabled = Habilita
        Button6.Enabled = Habilita
        Button7.Enabled = Habilita
    End Sub
End Class