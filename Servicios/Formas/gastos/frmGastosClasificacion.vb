Public Class frmGastosClasificacion
    Dim datos As New dbGastos(MySqlcon)
    Dim IdsTipos As New elemento
    Dim IdsTipos2 As New elemento
    Dim IdsTipos3 As New elemento
    Dim ID As Integer
    Dim IdTipo As Integer
    Dim IdTipo2 As Integer
    Dim IdTipo3 As Integer
    Dim NombreAnt1 As String
    Dim NombreAnt2 As String
    Dim NombreAnt3 As String
    Dim ClaveAnt1 As String
    Dim ClaveAnt2 As String
    Dim Claveant3 As String
    Dim Consultaon As Boolean = True
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub frmGastosClasificacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        'txtID.Text = datos.idProximo().ToString("000")
        Nuevo1()
    End Sub
    Private Sub Nuevo1()
        Try
            ConsultaOn = False
            ComboBox1.Text = ""
            Button1.Text = "Guardar"
            Button2.Enabled = False
            HabilitaArea(False)
            Panel2.Enabled = False
            LlenaCombos("tblgastosclasificacion", ComboBox1, "nombre", "nombret", "idclasificacion", IdsTipos)
            IdTipo = -1
            TextBox1.Text = datos.DaSiguienteCodigo(0, 1)
            NombreAnt1 = ""
            ClaveAnt1 = ""
            ComboBox1.Focus()
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub HabilitaArea(ByVal Habilita As Boolean)
        ComboBox2.Enabled = Habilita
        Button5.Enabled = Habilita
        Button6.Enabled = Habilita
        Button7.Enabled = Habilita
    End Sub
    Private Sub Nuevo2()
        Try
            ConsultaOn = False
            ComboBox2.Text = ""
            Button7.Text = "Guardar"
            Button6.Enabled = False
            LlenaCombos("tblgastosclasificacion2", ComboBox2, "nombre", "nombret", "idclasificacion", IdsTipos2, "idclassuperior=" + IdTipo.ToString)
            IdTipo2 = -1
            Panel2.Enabled = False
            TextBox2.Text = datos.DaSiguienteCodigo(IdTipo, 2)
            ConsultaOn = True
            ComboBox2.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    
    Private Sub Nuevo3()
        Try
            Consultaon = False
            ComboBox3.Text = ""
            Button10.Text = "Guardar"
            Button9.Enabled = False
            LlenaCombos("tblgastosclasificacion3", ComboBox3, "nombre", "nombret", "idclasificacion", IdsTipos3, "idclassuperior=" + IdTipo2.ToString)
            IdTipo3 = -1
            TextBox3.Text = datos.DaSiguienteCodigo(IdTipo2, 3)
            Consultaon = True
            ComboBox3.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex >= 0 Then
            IdTipo = IdsTipos.Valor(ComboBox1.SelectedIndex)
            'Dim IC2 As New dbDepartamentos(IdTipo, MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel1)
            datos.DaDatosClasificacion(IdTipo)
            Consultaon = False
            NombreAnt1 = datos.NombreClas
            TextBox1.Text = datos.ClaveClas
            ClaveAnt1 = datos.ClaveClas
            Button1.Text = "Modificar"
            Button2.Enabled = True
            HabilitaArea(True)
            Nuevo2()
            Consultaon = True
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex >= 0 Then
            IdTipo2 = IdsTipos2.Valor(ComboBox2.SelectedIndex)
            datos.DaDatosClasificacion2(IdTipo2)
            Consultaon = False
            NombreAnt2 = datos.NombreClas2
            ClaveAnt2 = datos.ClaveClas2
            TextBox2.Text = datos.ClaveClas2
            Panel2.Enabled = True
            Button7.Text = "Modificar"
            Button6.Enabled = True
            Nuevo3()
            Consultaon = True
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo1()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Nuevo2()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Departamentos + 3)))) <> 0 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesBajas, PermisosN.Secciones.Gastos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    datos.Eliminar(IdTipo)
                    PopUp("Eliminado", 90)
                    Nuevo1()
                    ComboBox1.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar esta clasificación debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesBajas, PermisosN.Secciones.Gastos) = True Then
                'Dim TC As New dbDepartamentos(MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel2)
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    datos.Eliminar2(IdTipo2)
                    PopUp("Eliminado", 90)
                    Nuevo2()
                    ComboBox2.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar esta clasificación debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            'Dim TC As New dbDepartamentos(MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel1)
            Dim NoError As Boolean = True
            Dim Errores As String = ""
            '---------------
            If ComboBox1.Text <> "" And TextBox1.Text <> "" Then
                If Button1.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesAltas, PermisosN.Secciones.Gastos) = True Then
                        If datos.ChecaNombreRepetido(ComboBox1.Text, 0, 1) Then
                            Errores += "Ya existe una clasificación con ese nombre."
                            NoError = False
                        End If
                        If datos.ChecaCodigoRepetido(TextBox1.Text, 0, 1) Then
                            NoError = False
                            Errores += " Ya existe una clasificación con ese código."
                        End If
                        If NoError Then
                            datos.GuardarClasificacion(ComboBox1.Text, TextBox1.Text)
                            IdTipo = datos.ID
                            Button1.Text = "Modificar"
                            Button2.Enabled = True
                            PopUp("Guardado", 90)
                            HabilitaArea(True)
                            Nuevo2()
                            'Nuevo1()
                        Else
                            MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesCambios, PermisosN.Secciones.Gastos) = True Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            If datos.ChecaNombreRepetido(ComboBox1.Text, 0, 1) And ComboBox1.Text <> NombreAnt1 Then
                                Errores += "Ya existe una clasificación con ese nombre."
                                NoError = False
                            End If
                            If datos.ChecaCodigoRepetido(TextBox1.Text, 0, 1) And TextBox1.Text <> ClaveAnt1 Then
                                Errores += "Ya existe una clasificación con ese nombre."
                                NoError = False
                            End If
                            If NoError Then
                                datos.ModificarClasificacion(IdTipo, ComboBox1.Text, TextBox1.Text)
                                PopUp("Modificado", 90)
                                ComboBox1.Focus()
                                'Nuevo1()
                            Else
                                MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                            End If
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If

                '----------------------
            Else
                MsgBox("Debe indicar un nombre y código a la clasifícación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        LlenaCombos("tblgastosclasificacion", ComboBox1, "nombre", "nombret", "idclasificacion", IdsTipos)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            'Dim TC As New dbDepartamentos(MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel2)
            Dim NoError As Boolean = True
            Dim Errores As String = ""
            '---------------
            If ComboBox2.Text <> "" And TextBox2.Text <> "" Then
                If Button7.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesAltas, PermisosN.Secciones.Gastos) = True Then
                        If datos.ChecaNombreRepetido(ComboBox2.Text, IdTipo, 2) Then
                            Errores += "Ya existe una clasificación con ese nombre."
                            NoError = False
                        End If
                        If datos.ChecaCodigoRepetido(TextBox2.Text, IdTipo, 2) Then
                            Errores += "Ya existe una clasificación con ese código."
                            NoError = False
                        End If
                        If NoError Then
                            datos.Guardar2(ComboBox2.Text, IdTipo, TextBox2.Text)
                            IdTipo2 = datos.ID2
                            PopUp("Guardado", 90)
                            Panel2.Enabled = True
                            Nuevo3()
                        Else
                            MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesCambios, PermisosN.Secciones.Gastos) = True Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            If datos.ChecaNombreRepetido(ComboBox2.Text, IdTipo, 2) And ComboBox2.Text <> NombreAnt2 Then
                                Errores += "Ya existe un sub clasificación con ese nombre."
                                NoError = False
                            End If
                            If datos.ChecaCodigoRepetido(TextBox2.Text, IdTipo, 2) And TextBox2.Text <> ClaveAnt2 Then
                                Errores += "Ya existe un sub clasificación con ese código."
                                NoError = False
                            End If
                            If NoError Then
                                datos.Modificar2(IdTipo2, ComboBox2.Text, TextBox2.Text)
                                PopUp("Modificado", 90)
                                Nuevo2()
                            Else
                                MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                            End If
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If
                ComboBox2.Focus()
                '----------------------
            Else
                MsgBox("Debe indicar un nombre y código a la clasificación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        LlenaCombos("tblgastosclasificacion2", ComboBox2, "nombre", "nombret", "idclasificacion", IdsTipos2, "idclassuperior=" + IdTipo.ToString)
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.TextChanged
        Dim textBox As ComboBox = DirectCast(sender, ComboBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub ComboBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.TextChanged
        Dim textBox As ComboBox = DirectCast(sender, ComboBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex >= 0 Then
            IdTipo3 = IdsTipos3.Valor(ComboBox3.SelectedIndex)
            datos.DaDatosClasificacion3(IdTipo3)
            Consultaon = False
            NombreAnt3 = datos.NombreClas3
            Claveant3 = datos.ClaveClas3
            TextBox3.Text = datos.ClaveClas3
            Panel2.Enabled = True
            Button10.Text = "Modificar"
            Button9.Enabled = True
            Consultaon = True
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Nuevo3()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesBajas, PermisosN.Secciones.Gastos) = True Then
                'Dim TC As New dbDepartamentos(MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel2)
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    datos.Eliminar3(IdTipo3)
                    PopUp("Eliminado", 90)
                    Nuevo3()
                    ComboBox3.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar esta clasificación debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            'Dim TC As New dbDepartamentos(MySqlcon, dbDepartamentos.TiposDepartamentos.Nivel2)
            Dim NoError As Boolean = True
            Dim Errores As String = ""
            '---------------
            If ComboBox3.Text <> "" And TextBox3.Text <> "" Then
                ComboBox3.Text = ComboBox3.Text.ToUpper
                If Button10.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesAltas, PermisosN.Secciones.Gastos) = True Then
                        If datos.ChecaNombreRepetido(ComboBox3.Text, IdTipo2, 3) Then
                            Errores += "Ya existe una clasificación con ese nombre."
                            NoError = False
                        End If
                        If datos.ChecaCodigoRepetido(TextBox3.Text, IdTipo3, 3) Then
                            Errores += "Ya existe una clasificación con ese código."
                            NoError = False
                        End If
                        If NoError Then
                            datos.Guardar3(ComboBox3.Text, IdTipo2, TextBox3.Text)
                            IdTipo3 = datos.ID3
                            PopUp("Guardado", 90)
                            'Panel2.Enabled = True
                            Nuevo3()
                        Else
                            MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesCambios, PermisosN.Secciones.Gastos) = True Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            If datos.ChecaNombreRepetido(ComboBox3.Text, IdTipo2, 3) And ComboBox3.Text <> NombreAnt3 Then
                                Errores += "Ya existe un clasificación con ese nombre."
                                NoError = False
                            End If
                            If datos.ChecaCodigoRepetido(TextBox3.Text, IdTipo2, 3) And TextBox3.Text <> Claveant3 Then
                                Errores += "Ya existe un clasificación con ese código."
                                NoError = False
                            End If
                            If NoError Then
                                datos.Modificar3(IdTipo3, ComboBox3.Text, TextBox3.Text)
                                PopUp("Modificado", 90)
                                Nuevo3()
                            Else
                                MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                            End If
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If
                ComboBox3.Focus()
                '----------------------
            Else
                MsgBox("Debe indicar un nombre y código a la clasificación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        LlenaCombos("tblgastosclasificacion3", ComboBox3, "nombre", "nombret", "idclasificacion", IdsTipos3, "idclassuperior=" + IdTipo2.ToString)
    End Sub
End Class