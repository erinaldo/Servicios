Public Class frmContabilidadNominaCuentas
    Dim IdsTipos As New elemento
    Dim N As dbNominas
    Private Sub frmContabilidadNominaCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        cmbVariante.Items.Add("Percepción")
        cmbVariante.Items.Add("Deducción")
        N = New dbNominas(MySqlcon)
        SelectorCuentas1.C = New dbContabilidadClasificacion(MySqlcon)
        SelectorCuentas1.P = New dbContabilidadPolizas(MySqlcon)
        SelectorCuentas1.Inicializar()
        cmbVariante.SelectedIndex = 0
    End Sub

    Private Sub cmbVariante_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVariante.SelectedIndexChanged
        If cmbVariante.SelectedIndex = 0 Then
            LlenaCombos("tblpercepciones", ComboBox8, "concat(clave,' ',descripcion)", "con", "idpercepcion", IdsTipos, , , "clave")
        Else
            LlenaCombos("tbldeducciones", ComboBox8, "concat(clave,' ',descripcion)", "con", "iddeduccion", IdsTipos, , , "clave")
        End If
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim HayError As String = ""
        If SelectorCuentas1.IdCuenta >= 0 Then
            Dim Cc As New dbCContables(MySqlcon)
            If Cc.UtlimoNivel(SelectorCuentas1.IdCuenta, SelectorCuentas1.Nivel) <> 0 Then
                HayError = "Debe seleccionar una cuenta de último nivel."
            End If
        End If
        If SelectorCuentas1.IdCuenta <= 0 Then
            HayError = " Debe seleccionar una cuenta"
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.NominaConceptosAlta, PermisosN.Secciones.Contabilidad) = False Then
            HayError += " No tiene permiso para realizar esta operación."
        End If
        If HayError = "" Then
            If MsgBox("¿Guardar cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                N.ModificaNominaConcepto(IdsTipos.Valor(ComboBox8.SelectedIndex), SelectorCuentas1.IdCuenta, cmbVariante.SelectedIndex)
                PopUp("Modificado", 50)
            End If
        Else
            MsgBox(HayError, MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        If ComboBox8.SelectedIndex >= 0 Then
            SelectorCuentas1.Vacia()
            SelectorCuentas1.LlenaCuenta(N.DaIdCuentaConcepto(IdsTipos.Valor(ComboBox8.SelectedIndex), cmbVariante.SelectedIndex))
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class