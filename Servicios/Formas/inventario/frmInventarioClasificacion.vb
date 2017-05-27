Public Class frmInventarioClasificacion
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
    Dim Clas1 As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
    Dim Clas2 As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
    Dim Clas3 As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
    Private Sub Nuevo1()
        Try
            ComboBox1.Text = ""

            Button1.Text = "Guardar"
            Button2.Enabled = False
            Panel1.Enabled = False
            Panel2.Enabled = False
            LlenaCombos("tblinventarioclasificaciones", ComboBox1, "nombre", "nombret", "idclasificacion", idsTipos, "idclasificacion>1", , "nombre")
            TextBox1.Text = Clas1.DaSiguienteCodigo
            'TreeView1.Nodes.Add("Calando")
            'TreeView1.Nodes.Add("Calando B")
            'TreeView1.Nodes(0).Nodes.Add("Calando2")
            idTipo = -1
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    
    Private Sub Nuevo2()
        Try
            ComboBox2.Text = ""
            TextBox2.Text = Clas2.DaSiguienteCodigo(idTipo)
            Button7.Text = "Guardar"
            Button6.Enabled = False
            Panel2.Enabled = False
            LlenaCombos("tblinventarioclasificaciones2", ComboBox2, "nombre", "nombret", "idclasificacion", idsTipos2, "idclasificacion>1 and idnivelsuperior=" + idTipo.ToString, , "nombre")
            idTipo2 = -1

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo3()
        Try
            ComboBox3.Text = ""
            TextBox3.Text = Clas3.DaSiguienteCodigo(idTipo2)
            Button10.Text = "Guardar"
            Button9.Enabled = False
            LlenaCombos("tblinventarioclasificaciones3", ComboBox3, "nombre", "nombret", "idclasificacion", idsTipos3, "idclasificacion>1 and idnivelsuperior=" + idTipo2.ToString, , "nombre")
            idTipo3 = -1
            LLenaArbol()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            Dim NoError As Boolean = True
            Dim Errores As String = ""
            '---------------
            If ComboBox1.Text <> "" And TextBox1.Text <> "" Then
                If Button1.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioAlta, PermisosN.Secciones.Catalagos) = True Then
                        If TC.ChecaNombreRepetido(ComboBox1.Text) Then
                            Errores += "Ya existe una clasificación con ese nombre."
                            NoError = False
                        End If
                        If TC.ChecaCodigoRepetido(TextBox1.Text) Then
                            NoError = False
                            Errores += " Ya existe una clasificación con ese código."
                        End If
                        If NoError Then
                            TC.Guardar(ComboBox1.Text, TextBox1.Text)
                            PopUp("Guardado", 90)
                            Button1.Text = "Modificar"
                            Button2.Enabled = True
                            idTipo = TC.ID
                            NombreAnt1 = TC.Nombre
                            CodigoAnt1 = TC.Codigo
                            Panel1.Enabled = True
                            LLenaArbol()
                            TextBox2.Focus()
                            'Nuevo1()
                        Else
                            MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioCambios, PermisosN.Secciones.Catalagos) = True Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            If TC.ChecaNombreRepetido(ComboBox1.Text) And ComboBox1.Text <> NombreAnt1 Then
                                Errores += "Ya existe una clasificación con ese nombre."
                                NoError = False
                            End If
                            If TC.ChecaCodigoRepetido(TextBox1.Text) And TextBox1.Text <> CodigoAnt1 Then
                                NoError = False
                                Errores += " Ya existe una clasificación con ese código."
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
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If
                '----------------------
            Else
                MsgBox("Debe indicar un nombre y código a la clasificación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioBaja, PermisosN.Secciones.Catalagos) = True Then
                Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    TC.Eliminar(idTipo)
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

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo1()
        Nuevo2()
        Nuevo3()
        TextBox1.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex >= 0 Then
            idTipo = idsTipos.Valor(ComboBox1.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(idTipo, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            TextBox1.Text = IC2.Codigo
            NombreAnt1 = IC2.Nombre
            CodigoAnt1 = IC2.Codigo
            Button1.Text = "Modificar"
            Button2.Enabled = True
            Nuevo2()
            Panel1.Enabled = True
        End If
    End Sub

    Private Sub frmInventarioClasificacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Nuevo1()
        Nuevo2()
        Nuevo3()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Nuevo2()
        Nuevo3()
        TextBox2.Focus()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Nuevo3()
        TextBox3.Focus()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            Dim NoError As Boolean = True
            Dim Errores As String = ""
            '---------------
            If ComboBox2.Text <> "" And TextBox2.Text <> "" Then
                If Button7.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioAlta, PermisosN.Secciones.Catalagos) = True Then
                        If TC.ChecaNombreRepetido(ComboBox2.Text, idTipo) Then
                            Errores += "Ya existe una clasificación con ese nombre."
                            NoError = False
                        End If
                        If TC.ChecaCodigoRepetido(TextBox2.Text, idTipo) Then
                            NoError = False
                            Errores += " Ya existe una clasificación con ese código."
                        End If
                        If NoError Then
                            TC.Guardar(ComboBox2.Text, TextBox2.Text, idTipo)
                            PopUp("Guardado", 90)
                            Button7.Text = "Modificar"
                            Button6.Enabled = True
                            idTipo2 = TC.ID
                            NombreAnt2 = TC.Nombre
                            CodigoAnt2 = TC.Codigo
                            Panel2.Enabled = True
                            LLenaArbol()
                            TextBox3.Focus()
                            'Nuevo2()
                        Else
                            MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If

                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioCambios, PermisosN.Secciones.Catalagos) = True Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            If TC.ChecaNombreRepetido(ComboBox2.Text, idTipo) And ComboBox2.Text <> NombreAnt2 Then
                                Errores += "Ya existe una clasificación con ese nombre."
                                NoError = False
                            End If
                            If TC.ChecaCodigoRepetido(TextBox2.Text, idTipo) And TextBox2.Text <> CodigoAnt2 Then
                                NoError = False
                                Errores += " Ya existe una clasificación con ese código."
                            End If
                            If NoError Then
                                TC.Modificar(idTipo2, ComboBox2.Text, TextBox2.Text)
                                PopUp("Modificado", 90)
                                TextBox2.Focus()
                                'Nuevo2()
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
                MsgBox("Debe indicar un nombre y código a la clasificación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioBaja, PermisosN.Secciones.Catalagos) = True Then
                Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    TC.Eliminar(idTipo2)
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

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Try
            Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            Dim NoError As Boolean = True
            Dim Errores As String = ""
            '---------------
            If ComboBox3.Text <> "" And TextBox3.Text <> "" Then
                If Button10.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioAlta, PermisosN.Secciones.Catalagos) = True Then
                        If TC.ChecaNombreRepetido(ComboBox3.Text, idTipo2) Then
                            Errores += "Ya existe una clasificación con ese nombre."
                            NoError = False
                        End If
                        If TC.ChecaCodigoRepetido(TextBox3.Text, idTipo2) Then
                            NoError = False
                            Errores += " Ya existe una clasificación con ese código."
                        End If
                        If NoError Then
                            TC.Guardar(ComboBox3.Text, TextBox3.Text, idTipo2)
                            PopUp("Guardado", 90)
                            Nuevo3()
                        Else
                            MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioCambios, PermisosN.Secciones.Catalagos) = True Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            If TC.ChecaNombreRepetido(ComboBox3.Text, idTipo2) And ComboBox3.Text <> NombreAnt3 Then
                                Errores += "Ya existe una clasificación con ese nombre."
                                NoError = False
                            End If
                            If TC.ChecaCodigoRepetido(TextBox3.Text, idTipo2) And TextBox3.Text <> CodigoAnt3 Then
                                NoError = False
                                Errores += " Ya existe una clasificación con ese código."
                            End If
                            If NoError Then
                                TC.Modificar(idTipo3, ComboBox3.Text, TextBox3.Text)
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
                TextBox3.Focus()
                '----------------------
            Else
                MsgBox("Debe indicar un nombre y código a la clasificación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioBaja, PermisosN.Secciones.Catalagos) = True Then
                Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    TC.Eliminar(idTipo3)
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

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
       
        If ComboBox2.SelectedIndex >= 0 Then
            idTipo2 = idsTipos2.Valor(ComboBox2.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(idTipo2, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            TextBox2.Text = IC2.Codigo
            NombreAnt2 = IC2.Nombre
            CodigoAnt2 = IC2.Codigo
            Button7.Text = "Modificar"
            Button6.Enabled = True
            Nuevo3()
            Panel2.Enabled = True
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex >= 0 Then
            idTipo3 = idsTipos3.Valor(ComboBox3.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(idTipo3, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            TextBox3.Text = IC2.Codigo
            NombreAnt3 = IC2.Nombre
            CodigoAnt3 = IC2.Codigo
            Button10.Text = "Modificar"
            Button9.Enabled = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        'Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        'If IC.BuscaClasificacion(TextBox1.Text) Then
        '    ComboBox1.SelectedIndex = idsTipos.Busca(IC.ID)
        'End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        'Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
        'If IC.BuscaClasificacion(TextBox2.Text, idTipo) Then
        '    ComboBox2.SelectedIndex = idsTipos2.Busca(IC.ID)
        'End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        'Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
        'If IC.BuscaClasificacion(TextBox3.Text, idTipo2) Then
        '    ComboBox3.SelectedIndex = idsTipos3.Busca(IC.ID)
        'End If
    End Sub
    Private Sub LLenaArbol()
        'Dim Clas As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        'Dim Clases As Collection
        'Dim Cont As Integer = 0
        'Dim Cont2 As Integer
        'TreeView1.Nodes.Clear()
        'Clases = Clas.DaColeccion(dbInventarioClasificaciones.TiposClasificacion.Nivel1, 0)
        'For Each N As ClasificacionBase In Clases
        '    TreeView1.Nodes.Add(N.idClasificacion.ToString, N.Nombre)
        '    Cont2 = 0
        '    Dim Clases2 As Collection
        '    Clases2 = Clas.DaColeccion(dbInventarioClasificaciones.TiposClasificacion.Nivel2, N.idClasificacion)
        '    For Each N2 As ClasificacionBase In Clases2
        '        TreeView1.Nodes(Cont).Nodes.Add(N2.idClasificacion, N2.Nombre)
        '        Dim Clases3 As Collection
        '        Clases3 = Clas.DaColeccion(dbInventarioClasificaciones.TiposClasificacion.Nivel3, N2.idClasificacion)
        '        For Each N3 As ClasificacionBase In Clases3
        '            TreeView1.Nodes(Cont).Nodes(Cont2).Nodes.Add(N3.idClasificacion, N3.Nombre)
        '        Next
        '        Cont2 += 1
        '    Next
        '    Cont += 1
        'Next
    End Sub
End Class