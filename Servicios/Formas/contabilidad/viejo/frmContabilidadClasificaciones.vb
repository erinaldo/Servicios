Imports System.Text.RegularExpressions

Public Class frmContabilidadClasificaciones
    Public Nivel As Integer
    Dim elemNiv1 As New elemento
    Dim elemNiv2 As New elemento
    Dim elemNiv3 As New elemento
    Dim elemNiv4 As New elemento
    Dim elemNiv5 As New elemento
    Dim elemNiv1nom As New elemento
    Dim elemNiv2nom As New elemento
    Dim elemNiv3nom As New elemento
    Dim elemNiv4nom As New elemento
    Dim elemNiv5nom As New elemento
    Dim cuenta As New elemento
    Public P As New dbContabilidadClasificacion(MySqlcon)
    Dim C As New dbContabilidadPolizas(MySqlcon)
    Public idNivel1 As Integer
    Public idNivel2 As Integer
    Public idNivel3 As Integer
    Public idNivel4 As Integer
    Public idNivel5 As Integer
    Public anterior As String
    'Elementos cuenta
    Dim N1 As String
    Dim N2 As String
    Dim N3 As String
    Dim N4 As String
    Dim N5 As String
    Dim Descripcion As String
    Dim tipoCuenta As Integer
    Dim Naturaleza As Integer
    Dim NivelCuenta As Integer
    Dim fechaCuenta As String
    'Fin Elementos de la cuenta
    Dim DIOT As Integer
    Dim IDGlogal As Integer
    Dim llenando As Boolean = False
    Dim negrita As New Font("Arial", 9, FontStyle.Bold)
    Dim q As New dbPagosProveedores(MySqlcon)
    'Dim nombreEmpresa As String = q.nombre(GlobalIdSucursalDefault)
    'Dim rfc As String = q.RFC(GlobalIdSucursalDefault)
    Dim N1aux As Integer
    Dim N2aux As Integer
    Dim N3aux As Integer
    Dim N4aux As Integer
    Dim N5aux As Integer
    Dim NAux As String = ""
    Dim ConsultaOn As Boolean = True
    Dim Descontinuada As Byte
    Public Sub New(Optional ByVal pNive1 As Integer = -1, Optional ByVal pNivel2 As Integer = -1, Optional ByVal pNivel3 As Integer = -1, Optional ByVal pNivel4 As Integer = -1, Optional ByVal pNivel5 As Integer = -1, Optional ByVal pAux As String = "")

        ' This call is required by the designer.
        InitializeComponent()
        N1aux = pNive1
        N2aux = pNivel2
        N3aux = pNivel3
        N4aux = pNivel4
        N5aux = pNivel5
        NAux = pAux

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmContabilidadClasificaciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        cmbTipo1.SelectedIndex = 0
        cmbNaturaleza1.SelectedIndex = 0
        C.llenaDatosConfig()
        NuevoNivel1()
        llenando = True

        'cmbBuscarNivel.SelectedIndex = 0
        llenando = False

        cmbTipo.SelectedIndex = 0
        P.cosas()
        ckAplicarSubcuentas.Enabled = False
        cmbNivel1.MaxLength = C.NNiv1
        cmbNivel2.MaxLength = C.NNiv2
        cmbNivel3.MaxLength = C.NNiv3
        cmbNivel4.MaxLength = C.NNiv4
        cmbNivel5.MaxLength = C.NNiv5
        Filtro()
        grpNiveles.Focus()

        If N1aux <> -1 Then
            llenarDatosAUX()
        End If
    End Sub
    Public Sub llenarDatosAUX()
        grpNiveles.Focus()
        If N1aux <> -1 Then
            P.conocerID(N1aux.ToString, "", "", "", "", 1)
            If P.IDcuenta <> 0 Then
                idNivel1 = P.IDcuenta
                IDGlogal = P.IDcuenta
                Nivel = 1
                P.busquedaRegistro(idNivel1)
                N1 = P.N1
                N2 = ""
                N3 = ""
                N4 = ""
                N5 = ""
                cmbNivel1.Text = P.N1.PadLeft(C.NNiv1, "0")

                Descripcion = P.Descripcion
                cmbNomNivel1.Text = Descripcion
                cmbTipo1.SelectedIndex = P.Tipo
                tipoCuenta = cmbTipo1.SelectedIndex
                cmbNaturaleza1.SelectedIndex = P.Naturaleza
                Naturaleza = cmbNaturaleza1.SelectedIndex
                dtpFecha.Value = P.fecha
                fechaCuenta = dtpFecha.Value.Year.ToString + "/" + dtpFecha.Value.Month.ToString("00") + "/" + dtpFecha.Value.Day.ToString("00")
                NivelCuenta = Nivel
                NuevoNivel2()
                cmbNivel2.Focus()
            Else
                NuevoNivel1()
                Nivel = 1
                cmbNivel1.Text = N1aux.ToString.PadLeft(C.NNiv1, "0")
                ' grpNiveles.Focus()
                cmbNomNivel1.Focus()


            End If
        End If
        If N2aux <> -1 Then
            P.conocerID(N1aux.ToString, N2aux.ToString, "", "", "", 2)
            If P.IDcuenta <> 0 Then
                Nivel = 2
                idNivel2 = P.IDcuenta
                IDGlogal = idNivel2
                P.busquedaRegistro(idNivel2)
                N1 = P.N1
                N2 = P.N2
                N3 = ""
                N4 = ""
                N5 = ""
                cmbNivel2.Text = P.N2.PadLeft(C.NNiv2, "0")
                Descripcion = P.Descripcion
                cmbNomNivel2.Text = Descripcion
                NuevoNivel3()
            Else
                NuevoNivel2()
                Nivel = 2
                cmbNivel2.Text = N2aux.ToString.PadLeft(C.NNiv2, "0")
                'grpNiveles.Focus()
                cmbNivel2.Focus()

            End If
        End If
        If N3aux <> -1 Then
            P.conocerID(N1aux.ToString, N2aux.ToString, N3aux.ToString, "", "", 3)
            If P.IDcuenta <> 0 Then
                Nivel = 3
                idNivel3 = P.IDcuenta
                IDGlogal = idNivel3
                P.busquedaRegistro(idNivel3)
                N1 = P.N1
                N2 = P.N2
                N3 = P.N3
                N4 = ""
                N5 = ""
                cmbNivel3.Text = P.N3.PadLeft(C.NNiv3, "0")
                Descripcion = P.Descripcion
                cmbNomNivel3.Text = Descripcion
                NuevoNivel4()
            Else
                NuevoNivel3()
                Nivel = 3
                cmbNivel3.Text = N3aux.ToString.PadLeft(C.NNiv3, "0")
                'grpNiveles.Focus()
                cmbNomNivel3.Focus()
            End If
        End If
        If N4aux <> -1 Then
            P.conocerID(N1aux.ToString, N2aux.ToString, N3aux.ToString, N4aux.ToString, "", 4)
            If P.IDcuenta <> 0 Then
                Nivel = 4
                idNivel4 = N4aux
                IDGlogal = idNivel4
                P.busquedaRegistro(idNivel4)
                N1 = P.N1
                N2 = P.N2
                N3 = P.N3
                N4 = P.N4
                N5 = ""
                cmbNivel4.Text = P.N4.PadLeft(C.NNiv4, "0")
                Descripcion = P.Descripcion
                cmbNomNivel4.Text = Descripcion
                NuevoNivel5()
            Else
                NuevoNivel4()
                Nivel = 4
                cmbNivel4.Text = N4aux.ToString.PadLeft(C.NNiv4, "0")
                'grpNiveles.Focus()
                cmbNomNivel4.Focus()
            End If
        End If
        If N5aux <> -1 Then
            P.conocerID(N1aux.ToString, N2aux.ToString, N3aux.ToString, N4aux.ToString, N5aux.ToString, 5)
            If P.IDcuenta <> 0 Then
                Nivel = 5
                idNivel5 = N5aux
                IDGlogal = idNivel5
                P.busquedaRegistro(idNivel5)
                N1 = P.N1
                N2 = P.N2
                N3 = P.N3
                N4 = P.N4
                N5 = P.N5
                cmbNivel5.Text = P.N5.PadLeft(C.NNiv5, "0")
                Descripcion = P.Descripcion
                cmbNomNivel5.Text = Descripcion
            Else
                NuevoNivel5()
                Nivel = 5
                cmbNivel5.Text = N5aux.ToString.PadLeft(C.NNiv5, "0")
                'grpNiveles.Focus()
                cmbNomNivel5.Focus()
            End If
        End If
        dtpFecha.Value = Date.Now
        btnGuardar.Enabled = True

    End Sub
    Private Sub limpiar()
        chkDIOT.Checked = False
        If Nivel = 1 Then
            cmbNivel2.Text = ""
            cmbNivel3.Text = ""
            cmbNivel4.Text = ""
            cmbNivel5.Text = ""
            cmbNomNivel2.Text = ""
            cmbNomNivel3.Text = ""
            cmbNomNivel4.Text = ""
            cmbNomNivel5.Text = ""

            cmbNivel2.Enabled = False
            cmbNivel3.Enabled = False
            cmbNivel4.Enabled = False
            cmbNivel5.Enabled = False
            cmbNomNivel2.Enabled = False
            cmbNomNivel3.Enabled = False
            cmbNomNivel4.Enabled = False
            cmbNomNivel5.Enabled = False
            If cmbNomNivel1.SelectedIndex >= 0 Then
                cmbNomNivel1.Text = ""
            End If
        End If
        If Nivel = 2 Then

            cmbNivel3.Text = ""
            cmbNivel4.Text = ""
            cmbNivel5.Text = ""
            If cmbNomNivel2.SelectedIndex >= 0 Then
                cmbNomNivel2.Text = ""
            End If
            cmbNomNivel3.Text = ""
            cmbNomNivel4.Text = ""
            cmbNomNivel5.Text = ""

            cmbNivel3.Enabled = False
            cmbNivel4.Enabled = False
            cmbNivel5.Enabled = False
            cmbNomNivel3.Enabled = False
            cmbNomNivel4.Enabled = False
            cmbNomNivel5.Enabled = False
        End If
        If Nivel = 3 Then

            cmbNivel4.Text = ""
            cmbNivel5.Text = ""
            cmbNomNivel4.Text = ""
            cmbNomNivel5.Text = ""
            If cmbNomNivel3.SelectedIndex >= 0 Then
                cmbNomNivel3.Text = ""
            End If
            cmbNivel4.Enabled = False
            cmbNivel5.Enabled = False
            cmbNomNivel4.Enabled = False
            cmbNomNivel5.Enabled = False
        End If
        If Nivel = 4 Then
            cmbNivel5.Text = ""
            cmbNomNivel5.Text = ""
            If cmbNomNivel4.SelectedIndex >= 0 Then
                cmbNomNivel4.Text = ""
            End If
            cmbNivel5.Enabled = False
            cmbNomNivel5.Enabled = False
        End If
        If Nivel = 5 Then
            If cmbNomNivel5.SelectedIndex >= 0 Then
                cmbNomNivel5.Text = ""
            End If
        End If
    End Sub
    Private Sub NuevoNivel1()
        cmbNivel1.Text = ""
        cmbNivel2.Text = ""
        cmbNivel3.Text = ""
        cmbNivel4.Text = ""
        cmbNivel5.Text = ""

        cmbNivel2.Enabled = False
        cmbNivel3.Enabled = False
        cmbNivel4.Enabled = False
        cmbNivel5.Enabled = False
        cmbNomNivel2.Enabled = False
        cmbNomNivel3.Enabled = False
        cmbNomNivel4.Enabled = False
        cmbNomNivel5.Enabled = False

        cmbNomNivel1.Text = ""
        cmbNomNivel2.Text = ""
        cmbNomNivel3.Text = ""
        cmbNomNivel4.Text = ""
        cmbNomNivel5.Text = ""

        ' Nivel = 1

        cmbNivel1.Focus()
        cmbTipo.SelectedIndex = 0
        cmbCuenta.SelectedIndex = 0
        LlenaCombos("tblccontables", cmbNivel1, "LPAD(tblccontables.Cuenta," + C.NNiv1.ToString + ",'0')", "nombret", "idCContable", elemNiv1, "Nivel='1'", , " idCContable")
        LlenaCombos("tblccontables", cmbNomNivel1, "Descripcion", "nombret", "idCContable", elemNiv1nom, "Nivel='1'", , " idCContable")
        idNivel1 = -1
        idNivel2 = -1
        idNivel3 = -1
        idNivel4 = -1
        idNivel5 = -1
        grpCuenta.Enabled = True
        cmbNaturaleza1.Enabled = True
        cmbTipo1.Enabled = True

    End Sub
    Private Sub NuevoNivel2()

        cmbNivel2.Text = ""
        cmbNivel3.Text = ""
        cmbNivel4.Text = ""
        cmbNivel5.Text = ""

        cmbNivel1.Enabled = True
        cmbNivel2.Enabled = True
        cmbNivel3.Enabled = False
        cmbNivel4.Enabled = False
        cmbNivel5.Enabled = False
        cmbNomNivel1.Enabled = True
        cmbNomNivel2.Enabled = True
        cmbNomNivel3.Enabled = False
        cmbNomNivel4.Enabled = False
        cmbNomNivel5.Enabled = False

        cmbNomNivel2.Text = ""
        cmbNomNivel3.Text = ""
        cmbNomNivel4.Text = ""
        cmbNomNivel5.Text = ""

        ' Nivel = 2

        cmbNivel2.Focus()

        idNivel2 = -1
        idNivel3 = -1
        idNivel4 = -1
        idNivel5 = -1
        LlenaCombos("tblccontables", cmbNivel2, "LPAD(N2," + C.NNiv1.ToString + ",'0')", "nombret", "idCContable", elemNiv2, "Nivel='2' and Cuenta=" + cmbNivel1.Text, , " idCContable")
        LlenaCombos("tblccontables", cmbNomNivel2, "Descripcion", "nombret", "idCContable", elemNiv2nom, "Nivel='2' and Cuenta=" + cmbNivel1.Text, , " idCContable")
        'grpCuenta.Enabled = False
        cmbNaturaleza1.Enabled = False
        cmbTipo1.Enabled = False

    End Sub
    Private Sub NuevoNivel3()

        cmbNivel3.Text = ""
        cmbNivel4.Text = ""
        cmbNivel5.Text = ""

        cmbNivel1.Enabled = True
        cmbNivel2.Enabled = True
        cmbNivel3.Enabled = True
        cmbNivel4.Enabled = False
        cmbNivel5.Enabled = False
        cmbNomNivel1.Enabled = True
        cmbNomNivel2.Enabled = True
        cmbNomNivel3.Enabled = True
        cmbNomNivel4.Enabled = False
        cmbNomNivel5.Enabled = False

        cmbNomNivel3.Text = ""
        cmbNomNivel4.Text = ""
        cmbNomNivel5.Text = ""

        ' Nivel = 3

        cmbNivel3.Focus()

        idNivel3 = -1
        idNivel4 = -1
        idNivel5 = -1
        LlenaCombos("tblccontables", cmbNivel3, "LPAD(N3," + C.NNiv1.ToString + ",'0')", "nombret", "idCContable", elemNiv3, "Nivel='3' and Cuenta=" + cmbNivel1.Text + " and N2=" + cmbNivel2.Text, , " idCContable")
        LlenaCombos("tblccontables", cmbNomNivel3, "Descripcion", "nombret", "idCContable", elemNiv3nom, "Nivel='3' and Cuenta=" + cmbNivel1.Text + " and N2=" + cmbNivel2.Text, , " idCContable")
        ' grpCuenta.Enabled = False
        cmbNaturaleza1.Enabled = False
        cmbTipo1.Enabled = False
    End Sub
    Private Sub NuevoNivel4()

        cmbNivel4.Text = ""
        cmbNivel5.Text = ""

        cmbNivel1.Enabled = True
        cmbNivel2.Enabled = True
        cmbNivel3.Enabled = True
        cmbNivel4.Enabled = True
        cmbNivel5.Enabled = False
        cmbNomNivel1.Enabled = True
        cmbNomNivel2.Enabled = True
        cmbNomNivel3.Enabled = True
        cmbNomNivel4.Enabled = True
        cmbNomNivel5.Enabled = False

        cmbNomNivel4.Text = ""
        cmbNomNivel5.Text = ""

        ' Nivel = 4

        cmbNivel4.Focus()

        idNivel4 = -1
        idNivel5 = -1
        LlenaCombos("tblccontables", cmbNivel4, "LPAD(N4," + C.NNiv1.ToString + ",'0')", "nombret", "idCContable", elemNiv4, "Nivel='4' and Cuenta=" + cmbNivel1.Text + " and N2=" + cmbNivel2.Text + " and N3=" + cmbNivel3.Text, , " idCContable")
        LlenaCombos("tblccontables", cmbNomNivel4, "Descripcion", "nombret", "idCContable", elemNiv4nom, "Nivel='4' and Cuenta=" + cmbNivel1.Text + " and N2=" + cmbNivel2.Text + " and N3=" + cmbNivel3.Text, , " idCContable")
        'grpCuenta.Enabled = False
        cmbNaturaleza1.Enabled = False
        cmbTipo1.Enabled = False
    End Sub
    Private Sub NuevoNivel5()

        cmbNivel5.Text = ""

        cmbNivel1.Enabled = True
        cmbNivel2.Enabled = True
        cmbNivel3.Enabled = True
        cmbNivel4.Enabled = True
        cmbNivel5.Enabled = True
        cmbNomNivel1.Enabled = True
        cmbNomNivel2.Enabled = True
        cmbNomNivel3.Enabled = True
        cmbNomNivel4.Enabled = True
        cmbNomNivel5.Enabled = True

        cmbNomNivel5.Text = ""

        ' Nivel = 5

        cmbNivel5.Focus()

        idNivel5 = -1

        LlenaCombos("tblccontables", cmbNivel5, "LPAD(N5," + C.NNiv1.ToString + ",'0')", "nombret", "idCContable", elemNiv5, "Nivel='5' and Cuenta=" + cmbNivel1.Text + " and N2=" + cmbNivel2.Text + " and N3=" + cmbNivel3.Text + " and N4=" + cmbNivel4.Text, , " idCContable")
        LlenaCombos("tblccontables", cmbNomNivel5, "Descripcion", "nombret", "idCContable", elemNiv5nom, "Nivel='5' and Cuenta=" + cmbNivel1.Text + " and N2=" + cmbNivel2.Text + " and N3=" + cmbNivel3.Text + " and N4=" + cmbNivel4.Text, , " idCContable")
        'grpCuenta.Enabled = False
        cmbNaturaleza1.Enabled = False
        cmbTipo1.Enabled = False
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoTodo.Click
        NuevoNivel1()
        chkDIOT.Checked = False
        ckAplicarSubcuentas.Checked = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGuardar.Enabled = False
        CheckBox1.Checked = False
    End Sub
    Private Sub elegirTipo(ByVal combo As ComboBox)
        If combo.Text.Chars(0) = "1" Then
            cmbTipo1.SelectedIndex = 0
        End If
        If combo.Text.Chars(0) = "2" Then
            cmbTipo1.SelectedIndex = 1
        End If
        If combo.Text.Chars(0) = "3" Then
            cmbTipo1.SelectedIndex = 2
        End If
        If combo.Text.Chars(0) = "4" Then
            cmbTipo1.SelectedIndex = 4
        End If
        If combo.Text.Chars(0) = "5" Then
            cmbTipo1.SelectedIndex = 5
        End If
        If combo.Text.Chars(0) = "6" Then
            cmbTipo1.SelectedIndex = 6
        End If
    End Sub

    Private Sub cmbNivel1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNivel1.KeyPress
      
        'If Char.IsDigit(e.KeyChar) Then
        '    e.Handled = False
        'ElseIf Char.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub
    Private Sub nivel1enter()

        cmbNivel1.Text = cmbNivel1.Text.PadLeft(C.NNiv1, "0")
        If P.existe(quitarCeros(cmbNivel1.Text), "", 1, quitarCeros(cmbNivel1.Text), "", "", "", "") Then
            idNivel1 = P.IDcuenta
            P.datosCombo(idNivel1, 1)
            cmbTipo.SelectedIndex = P.cmb1
            cmbSubTipo.SelectedIndex = P.cmb2
            cmbCuenta.SelectedIndex = cuenta.Busca(P.idContable)
            cmbNomNivel1.Text = P.nombre
            Nivel = 1
            llenaDatosCuentaGuardados()
            NuevoNivel2()
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
            btnGuardar.Enabled = False
            cmbNaturaleza1.Enabled = True
            cmbTipo1.Enabled = True

        Else
            'Nueva Cuenta nivel 1 cod

            Nivel = 1
            limpiar()

            cmbNomNivel1.Focus()
            cmbNomNivel1.SelectAll()
            btnGuardar.Text = "Guardar"
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGuardar.Enabled = True
            idNivel1 = -1
            elegirTipo(cmbNivel1)

        End If
    End Sub
    Private Sub cmbNomNivel1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNomNivel1.KeyPress
       
            e.KeyChar = e.KeyChar.ToString.ToUpper

    End Sub
    Private Sub Nivel1enter2()
        If P.existe("", cmbNomNivel1.Text, 1, "", "", "", "", "") Then
            'idNivel1 = elemNiv1nom.Valor(cmbNomNivel1.SelectedIndex)
            idNivel1 = P.IDcuenta
            P.datosCombo(idNivel1, 1)
            cmbTipo.SelectedIndex = P.cmb1
            cmbSubTipo.SelectedIndex = P.cmb2
            cmbCuenta.SelectedIndex = cuenta.Busca(P.idContable)
            cmbNivel1.Text = P.cuenta
            Nivel = 1
            llenaDatosCuentaGuardados()
            NuevoNivel2()
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
            btnGuardar.Enabled = False
            cmbNaturaleza1.Enabled = True
            cmbTipo1.Enabled = True

        Else
            'Nueva cuenta nivel 1 desc
            Nivel = 1
            limpiar()
            'cmbNivel1.Text = ""
            If cmbNivel1.Text <> "" Then
                cmbNivel1.Focus()
                cmbNivel1.SelectAll()
            Else
                dtpFecha.Focus()
            End If

            btnGuardar.Text = "Guardar"
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGuardar.Enabled = True
            idNivel1 = -1
            If cmbNivel1.Text <> "" Then
                grpCuenta.Focus()
                cmbTipo1.Focus()
            End If
        End If
    End Sub
    Private Sub cmbNivel2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNivel2.KeyPress

       
        '    If Char.IsDigit(e.KeyChar) Then
        '        e.Handled = False
        '    ElseIf Char.IsControl(e.KeyChar) Then
        '        e.Handled = False
        '    Else
        '        e.Handled = True
        'End If
    End Sub
    Private Sub nivel2enter()
        cmbNivel2.Text = cmbNivel2.Text.PadLeft(C.NNiv2, "0")
        If P.existe(quitarCeros(cmbNivel1.Text), "", 2, quitarCeros(cmbNivel1.Text), quitarCeros(cmbNivel2.Text), "", "", "") Then
            ' idNivel2 = elemNiv2.Valor(cmbNivel2.SelectedIndex)
            idNivel2 = P.IDcuenta
            P.datosCombo(idNivel2, 2)
            cmbTipo.SelectedIndex = P.cmb1
            cmbSubTipo.SelectedIndex = P.cmb2
            cmbCuenta.SelectedIndex = cuenta.Busca(P.idContable)
            cmbNomNivel2.Text = P.nombre
            Nivel = 2
            llenaDatosCuentaGuardados()
            NuevoNivel3()
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
            btnGuardar.Enabled = False
            cmbNaturaleza1.Enabled = False
            cmbTipo1.Enabled = False

        Else
            'nueva cuenta nivel 2 cod
            Nivel = 2
            limpiar()
            'cmbNomNivel2.Text = ""
            cmbNomNivel2.Focus()
            cmbNomNivel2.SelectAll()
            btnGuardar.Text = "Guardar"
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGuardar.Enabled = True
            cmbNaturaleza1.Enabled = False
            cmbTipo1.Enabled = False
            idNivel2 = -1
            elegirTipo(cmbNivel2)
        End If
    End Sub
    Private Sub cmbNomNivel2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNomNivel2.KeyPress
       
            e.KeyChar = e.KeyChar.ToString.ToUpper

    End Sub
    Private Sub Nivel2Enter2()
        If P.existe("", cmbNomNivel2.Text, 2, cmbNivel1.Text, "", "", "", "") Then
            'idNivel2 = elemNiv2nom.Valor(cmbNomNivel2.SelectedIndex)
            idNivel2 = P.IDcuenta
            P.datosCombo(idNivel2, 2)
            cmbTipo.SelectedIndex = P.cmb1
            cmbSubTipo.SelectedIndex = P.cmb2
            cmbCuenta.SelectedIndex = cuenta.Busca(P.idContable)
            cmbNivel2.Text = P.cuenta
            Nivel = 2
            llenaDatosCuentaGuardados()
            NuevoNivel3()
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
            btnGuardar.Enabled = False
            cmbNaturaleza1.Enabled = False
            cmbTipo1.Enabled = False

        Else
            'nueva cuenta nivel 2 desc
            Nivel = 2
            limpiar()
            ' cmbNivel2.Text = ""
            If cmbNivel2.Text <> "" Then
                cmbNivel2.Focus()
                cmbNivel2.SelectAll()
            Else
                dtpFecha.Focus()
            End If

            btnGuardar.Text = "Guardar"
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGuardar.Enabled = True
            cmbNaturaleza1.Enabled = False
            cmbTipo1.Enabled = False
            idNivel2 = -1
            If cmbNivel2.Text <> "" Then
                GroupBox1.Focus()
                cmbTipo.Focus()
            End If
        End If
    End Sub
    'Private Sub cmbNivel3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNivel3.KeyPress
    '    If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
    '        Nivel3Enter()
    '    Else
    '        If Char.IsDigit(e.KeyChar) Then
    '            e.Handled = False
    '        ElseIf Char.IsControl(e.KeyChar) Then
    '            e.Handled = False
    '        Else
    '            e.Handled = True
    '        End If
    '    End If
    'End Sub
    Private Sub Nivel3Enter()
        cmbNivel3.Text = cmbNivel3.Text.PadLeft(C.NNiv3, "0")
        If P.existe(quitarCeros(cmbNivel1.Text), "", 3, quitarCeros(cmbNivel1.Text), quitarCeros(cmbNivel2.Text), quitarCeros(cmbNivel3.Text), "", "") Then
            'idNivel3 = elemNiv3.Valor(cmbNivel3.SelectedIndex)
            idNivel3 = P.IDcuenta
            P.datosCombo(idNivel3, 3)
            cmbTipo.SelectedIndex = P.cmb1
            cmbSubTipo.SelectedIndex = P.cmb2
            cmbCuenta.SelectedIndex = cuenta.Busca(P.idContable)
            cmbNomNivel3.Text = P.nombre
            Nivel = 3
            llenaDatosCuentaGuardados()
            NuevoNivel4()
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
            btnGuardar.Enabled = False

        Else
            'nueva cuenta nivel 3 cod
            Nivel = 3
            limpiar()
            ' cmbNomNivel3.Text = ""
            cmbNomNivel3.Focus()
            cmbNomNivel3.SelectAll()
            btnGuardar.Text = "Guardar"
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGuardar.Enabled = True
            idNivel3 = -1
            elegirTipo(cmbNivel3)
        End If
    End Sub
    Private Sub cmbNomNivel3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNomNivel3.KeyPress
       
            e.KeyChar = e.KeyChar.ToString.ToUpper

    End Sub
    Private Sub Nivel3Enter2()
        If P.existe("", cmbNomNivel3.Text, 3, cmbNivel1.Text, cmbNivel2.Text, "", "", "") Then
            ' idNivel3 = elemNiv3nom.Valor(cmbNomNivel3.SelectedIndex)
            idNivel3 = P.IDcuenta
            P.datosCombo(idNivel3, 3)
            cmbTipo.SelectedIndex = P.cmb1
            cmbSubTipo.SelectedIndex = P.cmb2
            cmbCuenta.SelectedIndex = cuenta.Busca(P.idContable)
            cmbNivel3.Text = P.cuenta
            Nivel = 3
            llenaDatosCuentaGuardados()
            NuevoNivel4()
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
            btnGuardar.Enabled = False

        Else
            'nueva cuenta nivel 3 des
            Nivel = 3
            limpiar()
            'cmbNivel3.Text = ""
            If cmbNivel3.Text <> "" Then
                cmbNivel3.Focus()
                cmbNivel3.SelectAll()
            Else
                dtpFecha.Focus()
            End If

            btnGuardar.Text = "Guardar"
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGuardar.Enabled = True
            idNivel3 = -1
            If cmbNivel3.Text <> "" Then
                GroupBox1.Focus()
                cmbTipo.Focus()
            End If
        End If
    End Sub
    'Private Sub cmbNivel4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNivel4.KeyPress
    '    If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
    '        Nivel4Enter()
    '    Else
    '        If Char.IsDigit(e.KeyChar) Then
    '            e.Handled = False
    '        ElseIf Char.IsControl(e.KeyChar) Then
    '            e.Handled = False
    '        Else
    '            e.Handled = True
    '        End If
    '    End If
    'End Sub
    Private Sub Nivel4Enter()
        cmbNivel4.Text = cmbNivel4.Text.PadLeft(C.NNiv4, "0")
        If P.existe(quitarCeros(cmbNivel1.Text), "", 4, quitarCeros(cmbNivel1.Text), quitarCeros(cmbNivel2.Text), quitarCeros(cmbNivel3.Text), quitarCeros(cmbNivel4.Text), "") Then
            ' idNivel4 = elemNiv4.Valor(cmbNivel4.SelectedIndex)
            idNivel4 = P.IDcuenta
            P.datosCombo(idNivel4, 4)
            cmbTipo.SelectedIndex = P.cmb1
            cmbSubTipo.SelectedIndex = P.cmb2
            cmbCuenta.SelectedIndex = cuenta.Busca(P.idContable)
            cmbNomNivel4.Text = P.nombre
            Nivel = 4
            llenaDatosCuentaGuardados()
            NuevoNivel5()
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
            btnGuardar.Enabled = False

        Else
            'nueva cuenta nivel 4 cod
            Nivel = 4
            limpiar()
            'cmbNomNivel4.Text = ""
            cmbNomNivel4.Focus()
            cmbNomNivel4.SelectAll()
            btnGuardar.Text = "Guardar"
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGuardar.Enabled = True
            idNivel4 = -1
            elegirTipo(cmbNivel4)
        End If
    End Sub
    Private Sub cmbNomNivel4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNomNivel4.KeyPress
        'If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
        '    nivel4Enter2()
        'Else
        e.KeyChar = e.KeyChar.ToString.ToUpper
        'End If
    End Sub
    Private Sub nivel4Enter2()
        If P.existe("", cmbNomNivel4.Text, 4, cmbNivel1.Text, cmbNivel2.Text, cmbNivel3.Text, "", "") Then
            'idNivel4 = elemNiv4nom.Valor(cmbNomNivel4.SelectedIndex)
            idNivel4 = P.IDcuenta
            P.datosCombo(idNivel4, 4)
            cmbTipo.SelectedIndex = P.cmb1
            cmbSubTipo.SelectedIndex = P.cmb2
            cmbCuenta.SelectedIndex = cuenta.Busca(P.idContable)
            cmbNivel4.Text = P.cuenta
            Nivel = 4
            llenaDatosCuentaGuardados()
            NuevoNivel5()
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
            btnGuardar.Enabled = False

        Else
            'nueva cuenta nivel 4 desc
            Nivel = 4
            limpiar()
            'cmbNivel4.Text = ""
            If cmbNivel4.Text <> "" Then
                cmbNivel4.Focus()
                cmbNivel4.SelectAll()
            Else
                dtpFecha.Focus()
            End If


            btnGuardar.Text = "Guardar"
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGuardar.Enabled = True
            idNivel4 = -1
            If cmbNivel4.Text <> "" Then
                GroupBox1.Focus()
                cmbTipo.Focus()
            End If
        End If
    End Sub
    'Private Sub cmbNivel5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNivel5.KeyPress
    '    If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
    '        Nivel5Enter()
    '    Else
    '        If Char.IsDigit(e.KeyChar) Then
    '            e.Handled = False
    '        ElseIf Char.IsControl(e.KeyChar) Then
    '            e.Handled = False
    '        Else
    '            e.Handled = True
    '        End If

    '    End If
    'End Sub
    Private Sub Nivel5Enter()
        cmbNivel5.Text = cmbNivel5.Text.PadLeft(C.NNiv5, "0")
        If P.existe(quitarCeros(cmbNivel1.Text), "", 5, quitarCeros(cmbNivel1.Text), quitarCeros(cmbNivel2.Text), quitarCeros(cmbNivel3.Text), quitarCeros(cmbNivel4.Text), quitarCeros(cmbNivel5.Text)) Then
            'idNivel5 = elemNiv5.Valor(cmbNivel5.SelectedIndex)
            idNivel5 = P.IDcuenta
            P.datosCombo(idNivel5, 5)
            cmbTipo.SelectedIndex = P.cmb1
            cmbSubTipo.SelectedIndex = P.cmb2
            cmbCuenta.SelectedIndex = cuenta.Busca(P.idContable)
            cmbNomNivel5.Text = P.nombre
            Nivel = 5
            llenaDatosCuentaGuardados()
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
            btnGuardar.Enabled = False

        Else
            'nueva cuenta nivel 5 cod
            Nivel = 5
            limpiar()
            'cmbNomNivel5.Text = ""
            cmbNomNivel5.Focus()
            cmbNomNivel5.SelectAll()
            btnGuardar.Text = "Guardar"
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGuardar.Enabled = True
            idNivel5 = -1
            elegirTipo(cmbNivel5)
        End If
    End Sub
    Private Sub cmbNomNivel5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNomNivel5.KeyPress
        'If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
        '    Nivel3Enter2()
        'Else
        e.KeyChar = e.KeyChar.ToString.ToUpper
        'End If
    End Sub
    Private Sub Nivel5Enter2()
        If P.existe("", cmbNomNivel5.Text, 5, cmbNivel1.Text, cmbNivel2.Text, cmbNivel3.Text, cmbNivel4.Text, "") Then
            'idNivel5 = elemNiv5nom.Valor(cmbNomNivel5.SelectedIndex)
            idNivel5 = P.IDcuenta
            P.datosCombo(idNivel5, 5)
            cmbTipo.SelectedIndex = P.cmb1
            cmbSubTipo.SelectedIndex = P.cmb2
            cmbCuenta.SelectedIndex = cuenta.Busca(P.idContable)
            Nivel = 5
            llenaDatosCuentaGuardados()
            cmbNivel5.Text = P.cuenta
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
            btnGuardar.Enabled = False

        Else
            'Nueva cuenta desc
            Nivel = 5
            limpiar()
            'cmbNivel5.Text = ""
            If cmbNivel5.Text <> "" Then
                cmbNivel5.Focus()
                cmbNivel5.SelectAll()
            Else
                dtpFecha.Focus()
            End If

            btnGuardar.Text = "Guardar"
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGuardar.Enabled = True
            idNivel5 = -1
            If cmbNivel5.Text <> "" Then
                GroupBox1.Focus()
                cmbTipo.Focus()
            End If
        End If

    End Sub
    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        BotonGuardar()
    End Sub
    Private Sub BotonGuardar()
        Try


            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            If cmbNivel1.Text <> "" Then
                If Not IsNumeric(cmbNivel1.Text) Then
                    NoErrores = False
                    MensajeError += vbCrLf + "El número de cuenta debe ser un valor nimérico."
                End If
            End If
            If cmbNivel2.Text <> "" Then
                If Not IsNumeric(cmbNivel2.Text) Then
                    NoErrores = False
                    MensajeError += vbCrLf + "El número de cuenta debe ser un valor nimérico."
                End If
            End If
            If cmbNivel3.Text <> "" Then
                If Not IsNumeric(cmbNivel3.Text) Then
                    NoErrores = False
                    MensajeError += vbCrLf + "El número de cuenta debe ser un valor nimérico."
                End If
            End If
            If cmbNivel4.Text <> "" Then
                If Not IsNumeric(cmbNivel4.Text) Then
                    NoErrores = False
                    MensajeError += vbCrLf + "El número de cuenta debe ser un valor nimérico."
                End If
            End If
            If cmbNivel5.Text <> "" Then
                If Not IsNumeric(cmbNivel5.Text) Then
                    NoErrores = False
                    MensajeError += vbCrLf + "El número de cuenta debe ser un valor nimérico."
                End If
            End If
            If Nivel = 1 Then
                If cmbNivel1.Text = "" Or cmbNomNivel1.Text = "" Then
                    NoErrores = False
                    MensajeError += vbCrLf + "Debe llenar todos los campos requerídos."
                    Colorear(False)
                Else
                    If P.repetida(quitarCeros(cmbNivel1.Text), "", "", "", "", idNivel1, 1) Then
                        NoErrores = False
                        MensajeError += vbCrLf + "La cuenta ya existe."
                        Colorear(False)
                    End If

                End If
            End If
            If Nivel = 2 Then
                If cmbNivel2.Text = "" Or cmbNomNivel2.Text = "" Then
                    NoErrores = False
                    MensajeError += vbCrLf + "Debe llenar todos los campos requerídos."
                    Colorear(False)
                Else
                    If P.repetida(P.N1, quitarCeros(cmbNivel2.Text), "", "", "", idNivel2, 2) Then
                        NoErrores = False
                        MensajeError += vbCrLf + "La cuenta ya existe."
                        Colorear(False)
                    End If
                End If
                If P.TieneMovimientos(idNivel1) > 0 Then
                    NoErrores = False
                    MensajeError += vbCrLf + " La cuenta tiene movimientos en el nivel anterior."
                    Colorear(False)
                End If
            End If
            If Nivel = 3 Then
                If cmbNivel3.Text = "" Or cmbNomNivel3.Text = "" Then
                    NoErrores = False
                    MensajeError += vbCrLf + "Debe llenar todos los campos requerídos."
                    Colorear(False)
                Else
                    If P.repetida(P.N1, P.N2, quitarCeros(cmbNivel3.Text), "", "", idNivel3, 3) Then
                        NoErrores = False
                        MensajeError += vbCrLf + "La cuenta ya existe."
                        Colorear(False)

                    End If
                    If P.TieneMovimientos(idNivel2) > 0 Then
                        NoErrores = False
                        MensajeError += vbCrLf + " La cuenta tiene movimientos en el nivel anterior."
                        Colorear(False)
                    End If
                End If
            End If
            If Nivel = 4 Then
                If cmbNivel4.Text = "" Or cmbNomNivel4.Text = "" Then
                    NoErrores = False
                    MensajeError += vbCrLf + "Debe llenar todos los campos requerídos."
                    Colorear(False)
                Else
                    If P.repetida(P.N1, P.N2, P.N3, quitarCeros(cmbNivel4.Text), "", idNivel4, 4) Then
                        NoErrores = False
                        MensajeError += vbCrLf + "La cuenta ya existe."
                        Colorear(False)

                    End If
                End If
                If P.TieneMovimientos(idNivel3) > 0 Then
                    NoErrores = False
                    MensajeError += vbCrLf + " La cuenta tiene movimientos en el nivel anterior."
                    Colorear(False)
                End If
            End If
            If Nivel = 5 Then
                If cmbNivel5.Text = "" Or cmbNomNivel5.Text = "" Then
                    NoErrores = False
                    MensajeError += vbCrLf + "Debe llenar todos los campos requerídos."
                    Colorear(False)
                Else
                    If P.repetida(P.N1, P.N2, P.N3, P.N4, quitarCeros(cmbNivel5.Text), idNivel5, 5) Then
                        NoErrores = False
                        MensajeError += vbCrLf + "La cuenta ya existe."
                        Colorear(False)

                    End If
                    If P.TieneMovimientos(idNivel4) > 0 Then
                        NoErrores = False
                        MensajeError += vbCrLf + " La cuenta tiene movimientos en el nivel anterior."
                        Colorear(False)
                    End If
                End If
            End If
            If cmbCuenta.SelectedIndex <= 0 Then
                NoErrores = False
                MensajeError += vbCrLf + "Debe seleccionar un agrupador SAT válido"
            End If
            If NoErrores = True Then
                Colorear(True)
                llenaDatosCuenta()
                If chkDIOT.Checked = True Then
                    DIOT = 1
                Else
                    DIOT = 0
                End If
                P.Guardar(N1, N2, N3, N4, N5, Descripcion, NivelCuenta, tipoCuenta, Naturaleza, fechaCuenta, cmbTipo.SelectedIndex, cmbSubTipo.SelectedIndex, cuenta.Valor(cmbCuenta.SelectedIndex), DIOT, 0)
                P.busquedaRegistro(P.IDcuenta)
                PopUp("Guardado", 90)
                If Nivel = 1 Then
                    NuevoNivel2()
                    idNivel1 = P.IDcuenta
                    btnEliminar.Enabled = True
                    btnGuardar.Enabled = False
                    btnModificar.Enabled = True
                End If
                If Nivel = 2 Then
                    NuevoNivel3()
                    idNivel2 = P.IDcuenta
                    btnEliminar.Enabled = True
                    btnGuardar.Enabled = False
                    btnModificar.Enabled = True
                End If
                If Nivel = 3 Then
                    NuevoNivel4()
                    idNivel3 = P.IDcuenta
                    btnEliminar.Enabled = True
                    btnGuardar.Enabled = False
                    btnModificar.Enabled = True
                End If
                If Nivel = 4 Then
                    NuevoNivel5()
                    idNivel4 = P.IDcuenta
                    btnEliminar.Enabled = True
                    btnGuardar.Enabled = False
                    btnModificar.Enabled = True
                End If
                If Nivel = 5 Then
                    idNivel5 = P.IDcuenta
                    btnEliminar.Enabled = True
                    btnGuardar.Enabled = False
                    btnModificar.Enabled = True
                End If
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Filtro()
            If NoErrores = False Then
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Pull System Soft")
        End Try
    End Sub
    Private Function quitarCeros(ByVal pTexto As String)
        Dim texto As String = ""
        If pTexto.Chars(0) = "0" Then
            For i As Integer = 0 To pTexto.Length.ToString - 1
                If pTexto.Chars(i) <> "0" Then
                    For j As Integer = i To pTexto.Length - 1
                        texto += pTexto.Chars(j)
                    Next
                    If texto = "" Then
                        texto = "0"
                    End If
                    Return texto
                End If
            Next
        Else
            texto = pTexto
        End If
        If texto = "" Then
            texto = "0"
        End If

        Return texto
    End Function

    Private Sub Colorear(ByVal ptipo As Boolean)
        If ptipo Then
            cmbNivel1.BackColor = Color.White
            cmbNivel2.BackColor = Color.White
            cmbNivel3.BackColor = Color.White
            cmbNivel4.BackColor = Color.White
            cmbNivel5.BackColor = Color.White

            cmbNomNivel1.BackColor = Color.White
            cmbNomNivel2.BackColor = Color.White
            cmbNomNivel3.BackColor = Color.White
            cmbNomNivel4.BackColor = Color.White
            cmbNomNivel5.BackColor = Color.White
        Else
            If Nivel = 1 Then
                If cmbNivel1.Text = "" Then
                    cmbNivel1.BackColor = Color.Tomato
                End If
                If cmbNomNivel1.Text = "" Then
                    cmbNomNivel1.BackColor = Color.Tomato
                End If
            End If
            If Nivel = 2 Then
                If cmbNivel2.Text = "" Then
                    cmbNivel2.BackColor = Color.Tomato
                End If
                If cmbNomNivel2.Text = "" Then
                    cmbNomNivel2.BackColor = Color.Tomato
                End If
            End If
            If Nivel = 3 Then
                If cmbNivel3.Text = "" Then
                    cmbNivel3.BackColor = Color.Tomato
                End If
                If cmbNomNivel3.Text = "" Then
                    cmbNomNivel3.BackColor = Color.Tomato
                End If
            End If
            If Nivel = 4 Then
                If cmbNivel4.Text = "" Then
                    cmbNivel4.BackColor = Color.Tomato
                End If
                If cmbNomNivel4.Text = "" Then
                    cmbNomNivel4.BackColor = Color.Tomato
                End If
            End If
            If Nivel = 5 Then
                If cmbNivel5.Text = "" Then
                    cmbNivel5.BackColor = Color.Tomato
                End If
                If cmbNomNivel5.Text = "" Then
                    cmbNomNivel5.BackColor = Color.Tomato
                End If
            End If
        End If

    End Sub
    Private Sub llenaDatosCuenta()
        If Nivel = 1 Then
            N1 = quitarCeros(cmbNivel1.Text)
            N2 = ""
            N3 = ""
            N4 = ""
            N5 = ""
            Descripcion = cmbNomNivel1.Text
            NivelCuenta = 1

        End If
        If Nivel = 2 Then
            P.busquedaRegistro(idNivel1)
            N1 = P.N1
            N2 = quitarCeros(cmbNivel2.Text)
            N3 = ""
            N4 = ""
            N5 = ""
            Descripcion = cmbNomNivel2.Text
            NivelCuenta = 2

        End If
        If Nivel = 3 Then
            P.busquedaRegistro(idNivel2)
            N1 = P.N1
            N2 = P.N2
            N3 = quitarCeros(cmbNivel3.Text)
            N4 = ""
            N5 = ""
            Descripcion = cmbNomNivel3.Text
            NivelCuenta = 3
        End If
        If Nivel = 4 Then
            P.busquedaRegistro(idNivel3)
            N1 = P.N1
            N2 = P.N2
            N3 = P.N3
            N4 = quitarCeros(cmbNivel4.Text)
            N5 = ""
            Descripcion = cmbNomNivel4.Text
            NivelCuenta = 4
        End If
        If Nivel = 5 Then
            P.busquedaRegistro(idNivel4)
            N1 = P.N1
            N2 = P.N2
            N3 = P.N3
            N4 = P.N4
            N5 = quitarCeros(cmbNivel5.Text)
            Descripcion = cmbNomNivel5.Text
            NivelCuenta = 5
        End If

        Naturaleza = cmbNaturaleza1.SelectedIndex
        tipoCuenta = cmbTipo1.SelectedIndex
        fechaCuenta = dtpFecha.Value.Year.ToString + "/" + dtpFecha.Value.Month.ToString("00") + "/" + dtpFecha.Value.Day.ToString("00")
    End Sub
    Private Sub llenaDatosCuentaGuardados()
        If Nivel = 1 Then
            P.busquedaRegistro(idNivel1)
            cmbNivel1.Text = P.N1.PadLeft(C.NNiv1, "0")
            cmbNomNivel1.Text = P.Descripcion
            cmbNaturaleza1.SelectedIndex = P.Naturaleza
            cmbTipo1.SelectedIndex = P.Tipo
            dtpFecha.Value = fechaFormato(P.fecha)
            
        End If
        If Nivel = 2 Then
            P.busquedaRegistro(idNivel2)
            cmbNivel2.Text = P.N2.PadLeft(C.NNiv2, "0")
            cmbNomNivel2.Text = P.Descripcion
            cmbNaturaleza1.SelectedIndex = P.Naturaleza
            cmbTipo1.SelectedIndex = P.Tipo
            dtpFecha.Value = fechaFormato(P.fecha)
        End If
        If Nivel = 3 Then
            P.busquedaRegistro(idNivel3)
            cmbNivel3.Text = P.N3.PadLeft(C.NNiv3, "0")
            cmbNomNivel3.Text = P.Descripcion
            cmbNaturaleza1.SelectedIndex = P.Naturaleza
            cmbTipo1.SelectedIndex = P.Tipo
            dtpFecha.Value = fechaFormato(P.fecha)
        End If
        If Nivel = 4 Then
            P.busquedaRegistro(idNivel4)
            cmbNivel4.Text = P.N4.PadLeft(C.NNiv4, "0")
            cmbNomNivel4.Text = P.Descripcion
            cmbNaturaleza1.SelectedIndex = P.Naturaleza
            cmbTipo1.SelectedIndex = P.Tipo
            dtpFecha.Value = fechaFormato(P.fecha)
        End If
        If Nivel = 5 Then
            P.busquedaRegistro(idNivel5)
            cmbNivel5.Text = P.N5.PadLeft(C.NNiv5, "0")
            cmbNomNivel5.Text = P.Descripcion
            cmbNaturaleza1.SelectedIndex = P.Naturaleza
            cmbTipo1.SelectedIndex = P.Tipo
            dtpFecha.Value = fechaFormato(P.fecha)
        End If
        If P.Descontinuada = 1 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If

    End Sub
    Private Function fechaFormato(ByVal pFecha As String) As String
        Dim year As String = ""
        Dim month As String = ""
        Dim day As String = ""


        year = pFecha.Chars(0) + pFecha.Chars(1) + pFecha.Chars(2) + pFecha.Chars(3)
        month = pFecha.Chars(5) + pFecha.Chars(6)
        day += pFecha.Chars(8) + pFecha.Chars(9)



        Return day + "/" + month + "/" + year
    End Function
    Private Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        
        BotonModificar()
    End Sub
    Private Sub BotonModificar()
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""

        If Nivel = 1 Then
            If cmbNivel1.Text = "" Or cmbNomNivel1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + "Debe llenar todos los campos requerídos."
                Colorear(False)
            Else
                If P.repetida(quitarCeros(cmbNivel1.Text), "", "", "", "", idNivel1, 1) Then
                    NoErrores = False
                    MensajeError += vbCrLf + "La cuenta ya existe."
                    Colorear(False)
                End If
            End If
        End If
        If Nivel = 2 Then
            If cmbNivel2.Text = "" Or cmbNomNivel2.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + "Debe llenar todos los campos requerídos."
                Colorear(False)
            Else
                If P.repetida(P.N1, quitarCeros(cmbNivel2.Text), "", "", "", idNivel2, 2) Then
                    NoErrores = False
                    MensajeError += vbCrLf + "La cuenta ya existe."
                    Colorear(False)
                End If
            End If
        End If
        If Nivel = 3 Then
            If cmbNivel3.Text = "" Or cmbNomNivel3.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + "Debe llenar todos los campos requerídos."
                Colorear(False)

            Else
                If P.repetida(P.N1, P.N2, quitarCeros(cmbNivel3.Text), "", "", idNivel3, 3) Then
                    NoErrores = False
                    MensajeError += vbCrLf + "La cuenta ya existe."
                    Colorear(False)

                End If
            End If
        End If
        If Nivel = 4 Then
            If cmbNivel4.Text = "" Or cmbNomNivel4.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + "Debe llenar todos los campos requerídos."
                Colorear(False)


            Else
                If P.repetida(P.N1, P.N2, P.N3, quitarCeros(cmbNivel4.Text), "", idNivel4, 4) Then
                    NoErrores = False
                    MensajeError += vbCrLf + "La cuenta ya existe."
                    Colorear(False)

                End If
            End If
        End If
        If Nivel = 5 Then
            If cmbNivel5.Text = "" Or cmbNomNivel5.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + "Debe llenar todos los campos requerídos."
                Colorear(False)

            Else
                If P.repetida(P.N1, P.N2, P.N3, P.N4, quitarCeros(cmbNivel5.Text), idNivel5, 5) Then
                    NoErrores = False
                    MensajeError += vbCrLf + "La cuenta ya existe."
                    Colorear(False)

                End If
            End If
        End If
        If cmbCuenta.SelectedIndex <= 0 Then
            NoErrores = False
            MensajeError += vbCrLf + "Debe seleccionar un agrupador SAT válido"
        End If
        If NoErrores = True Then
            If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Colorear(True)
                llenarDatosPrevios()
                If chkDIOT.Checked = True Then
                    DIOT = 1
                Else
                    DIOT = 0
                End If
                Dim Descon As Byte = 0
                If CheckBox1.Checked = True Then
                    Descon = 1
                End If
                Dim subTipo As Integer
                If cmbSubTipo.SelectedIndex = -1 Then
                    subTipo = 1000
                Else
                    subTipo = cmbSubTipo.SelectedIndex
                End If
                P.Modificar(IDGlogal, N1, N2, N3, N4, N5, Descripcion, NivelCuenta, tipoCuenta, Naturaleza, fechaCuenta, cmbTipo.SelectedIndex, subTipo, cuenta.Valor(cmbCuenta.SelectedIndex), DIOT, Descon)
                P.ModificarCuentas(anterior, N1, N2, N3, N4, N5, NivelCuenta)
                If ckAplicarSubcuentas.Checked = True Then
                    P.ModificarCODSAT(anterior, N1, N2, N3, N4, N5, NivelCuenta, cuenta.Valor(cmbCuenta.SelectedIndex), cmbTipo.SelectedIndex, subTipo)
                End If
                P.busquedaRegistro(P.IDcuenta)

                If Nivel = 1 Then
                    P.ModificarTipoSubC(N1, tipoCuenta, Naturaleza)
                    NuevoNivel2()
                    idNivel1 = P.IDcuenta
                    btnEliminar.Enabled = True
                    btnGuardar.Enabled = False
                    btnModificar.Enabled = True
                    cmbNaturaleza1.Enabled = True
                    cmbTipo1.Enabled = True

                End If
                If Nivel = 2 Then
                    NuevoNivel3()
                    idNivel2 = P.IDcuenta
                    btnEliminar.Enabled = True
                    btnGuardar.Enabled = False
                    btnModificar.Enabled = True
                End If
                If Nivel = 3 Then
                    NuevoNivel4()
                    idNivel3 = P.IDcuenta
                    btnEliminar.Enabled = True
                    btnGuardar.Enabled = False
                    btnModificar.Enabled = True
                End If
                If Nivel = 4 Then
                    NuevoNivel5()
                    idNivel4 = P.IDcuenta
                    btnEliminar.Enabled = True
                    btnGuardar.Enabled = False
                    btnModificar.Enabled = True
                End If
                If Nivel = 5 Then
                    idNivel5 = P.IDcuenta
                    btnEliminar.Enabled = True
                    btnGuardar.Enabled = False
                    btnModificar.Enabled = True
                End If
                ' Me.DialogResult = Windows.Forms.DialogResult.OK
                PopUp("Modificado", 90)
                Filtro()
            End If

        End If
        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub
    Public Sub llenarDatosPrevios()
        If Nivel = 1 Then
            IDGlogal = idNivel1
            N1 = quitarCeros(cmbNivel1.Text)
            N2 = ""
            N3 = ""
            N4 = ""
            N5 = ""
            Descripcion = cmbNomNivel1.Text
            anterior = P.N1
        End If
        If Nivel = 2 Then
            IDGlogal = idNivel2
            P.busquedaRegistro(idNivel2)
            N1 = P.N1
            N2 = quitarCeros(cmbNivel2.Text)
            N3 = ""
            N4 = ""
            N5 = ""
            Descripcion = cmbNomNivel2.Text
            anterior = P.N2
        End If
        If Nivel = 3 Then
            IDGlogal = idNivel3
            P.busquedaRegistro(idNivel3)
            N1 = P.N1
            N2 = P.N2
            N3 = quitarCeros(cmbNivel3.Text)
            N4 = ""
            N5 = ""
            Descripcion = cmbNomNivel3.Text
            anterior = P.N3
        End If
        If Nivel = 4 Then
            IDGlogal = idNivel4
            P.busquedaRegistro(idNivel4)
            N1 = P.N1
            N2 = P.N2
            N3 = P.N3
            N4 = quitarCeros(cmbNivel4.Text)
            N5 = ""
            Descripcion = cmbNomNivel4.Text
            anterior = P.N4
        End If
        If Nivel = 5 Then
            IDGlogal = idNivel5
            P.busquedaRegistro(idNivel5)
            N1 = P.N1
            N2 = P.N2
            N3 = P.N3
            N4 = P.N4
            N5 = quitarCeros(cmbNivel5.Text)
            anterior = P.N5
            Descripcion = cmbNomNivel5.Text
        End If
        tipoCuenta = cmbTipo1.SelectedIndex
        Naturaleza = cmbNaturaleza1.SelectedIndex
        fechaCuenta = dtpFecha.Value.Year.ToString + "/" + dtpFecha.Value.Month.ToString("00") + "/" + dtpFecha.Value.Day.ToString("00")
        NivelCuenta = Nivel
    End Sub
    Public Sub llenarDatosOriginales()
        If Nivel = 1 Then
            IDGlogal = idNivel1
            P.busquedaRegistro(idNivel1)
            N1 = P.N1
            N2 = ""
            N3 = ""
            N4 = ""
            N5 = ""
            Descripcion = P.Descripcion
        End If
        If Nivel = 2 Then
            IDGlogal = idNivel2
            P.busquedaRegistro(idNivel2)
            N1 = P.N1
            N2 = P.N2
            N3 = ""
            N4 = ""
            N5 = ""
            Descripcion = P.Descripcion
        End If
        If Nivel = 3 Then
            IDGlogal = idNivel3
            P.busquedaRegistro(idNivel3)
            N1 = P.N1
            N2 = P.N2
            N3 = P.N3
            N4 = ""
            N5 = ""
            Descripcion = P.Descripcion

        End If
        If Nivel = 4 Then
            IDGlogal = idNivel4
            P.busquedaRegistro(idNivel4)
            N1 = P.N1
            N2 = P.N2
            N3 = P.N3
            N4 = P.N4
            N5 = ""
            Descripcion = P.Descripcion
        End If
        If Nivel = 5 Then
            IDGlogal = idNivel5
            P.busquedaRegistro(idNivel5)
            N1 = P.N1
            N2 = P.N2
            N3 = P.N3
            N4 = P.N4
            N5 = P.N5
            Descripcion = P.Descripcion
        End If
        tipoCuenta = cmbTipo1.SelectedIndex
        Naturaleza = cmbNaturaleza1.SelectedIndex
        fechaCuenta = dtpFecha.Value.Year.ToString + "/" + dtpFecha.Value.Month.ToString("00") + "/" + dtpFecha.Value.Day.ToString("00")
        NivelCuenta = Nivel
    End Sub
    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim mensaje As String = ""
        Try
            'If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos) = True Then
            If tieneregitros() Then
                mensaje = "Esta cuenta contiene subcuentas, las cuales, se borrarán al eliminar esta." + vbCrLf
            End If

            If MsgBox(mensaje + "¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                If P.TieneMovEliminar(P.N1, P.N2, P.N3, P.N4, P.N5, Nivel) Then
                    MsgBox("Esta cuenta no puede ser eliminada." + vbCrLf + "Esta o alguna(s) subcuenta(s) de la misma, está siendo siendo usada por una o más pólizas.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                Else
                    P.Eliminar(P.N1, P.N2, P.N3, P.N4, P.N5, Nivel)
                    PopUp("Eliminado", 90)

                    limpiar()
                    If Nivel = 1 Then
                        NuevoNivel1()
                    End If
                    If Nivel = 2 Then
                        NuevoNivel2()
                        Nivel = 1
                    End If
                    If Nivel = 3 Then
                        NuevoNivel3()
                        Nivel = 2
                    End If
                    If Nivel = 4 Then
                        NuevoNivel4()
                        Nivel = 3
                    End If
                    If Nivel = 5 Then
                        NuevoNivel5()
                        Nivel = 4
                    End If
                    Filtro()
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Function tieneregitros() As Boolean
        llenarDatosOriginales()

        If P.subcategorias(P.N1, P.N2, P.N3, P.N4, P.N5, Nivel) > 1 Then
            Return True
        Else
            Return False
        End If


    End Function

    Private Sub Filtro()
        If llenando = False Then

            Try
                'Dim cuenta As String = txtBuscar.Text
                Dim PrimerCeldaRow As Integer = -1

                Dim Palabras() As String
                Dim txtAbuscar As String = ""
                Dim txtAbuscar2 As String = ""
                Palabras = txtBuscar.Text.Split(Chr(32))
                For Each palabra As String In Palabras
                    If Regex.IsMatch(palabra, "[A-Z]|[a-z]") Then
                        txtAbuscar += " " + palabra
                    Else
                        txtAbuscar2 += " " + palabra
                    End If
                Next
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                'If Regex.IsMatch(cuenta, "[A-Z]|[a-z]") Then
                'DataGridView1.DataSource = P.filtro("", 0, cuenta)
                'Else
                DataGridView1.DataSource = P.Consulta(txtAbuscar2.Trim, 0, txtAbuscar.Trim, True, CheckBox2.Checked)
                'End If
                'DataGridView1.Columns(0).HeaderText = "id"
                'DataGridView1.Columns(1).HeaderText = "Cuenta"
                'DataGridView1.Columns(2).HeaderText = "Descripción"
                'DataGridView1.Columns(3).HeaderText = "Nivel"
                'DataGridView1.Columns(4).HeaderText = "Tipo"
                'DataGridView1.Columns(5).HeaderText = "Naturaleza"
                'DataGridView1.Columns(6).HeaderText = "Agrupador SAT"
                'DataGridView1.Columns(0).Visible = False
                'DataGridView1.Columns(3).Visible = False
                'DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                'DataGridView1.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                'DataGridView1.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                'DataGridView1.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                'DataGridView1.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                'DataGridView1.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                'DataGridView1.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                'DataGridView1.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Dim column As DataGridViewColumn = DataGridView1.Columns(6)
                'column.Width = 10
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
            '    End If
        End If

    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If DataGridView1.Item(3, e.RowIndex).Value.ToString = "1" Then
            e.CellStyle.Font = negrita
        End If
    End Sub

    Private Sub txtBuscarCuenta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Filtro()
    End Sub

    Private Sub txtBuscarDescripicion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscar.TextChanged
        Filtro()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        llenaDatos()
    End Sub
    Private Sub llenaDatos()
        Try
            Dim pID As Integer
            Dim encontrado As Boolean
            pID = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            P.busquedaRegistro(pID)
            cmbTipo.SelectedIndex = P.cmb1
            If P.cmb2 = 1000 Then

            Else

                cmbSubTipo.SelectedIndex = P.cmb2
            End If

            cmbCuenta.SelectedIndex = cuenta.Busca(P.idContable)
            Nivel = P.Nivel
            Naturaleza = P.Naturaleza
            tipoCuenta = P.Tipo
            fechaCuenta = fechaFormato(P.fecha)
            Descripcion = P.Descripcion
            If P.Descontinuada = 0 Then
                CheckBox1.Checked = False
            Else
                CheckBox1.Checked = True
            End If
            N1 = P.N1
            N2 = P.N2
            N3 = P.N3
            N4 = P.N4
            N5 = P.N5
            If P.DIOT = 1 Then
                chkDIOT.Checked = True
            Else
                chkDIOT.Checked = False
            End If
            encontrado = False

            If Nivel = 1 Then
                cmbNivel1.Text = N1.PadLeft(C.NNiv1, "0")
                cmbNomNivel1.Text = Descripcion
                encontrado = True
                idNivel1 = pID
                NuevoNivel2()
            Else
                If encontrado = False Then
                    P.conocerID(N1, "", "", "", "", 1)
                    cmbNivel1.Text = P.N1.PadLeft(C.NNiv1, "0")
                    idNivel1 = P.IDcuenta
                    cmbNomNivel1.Text = P.Descripcion
                End If
            End If
            If Nivel = 2 Then
                cmbNivel2.Text = N2.PadLeft(C.NNiv2, "0")
                cmbNomNivel2.Text = Descripcion
                encontrado = True
                idNivel2 = pID
                NuevoNivel3()
            Else
                If encontrado = False Then
                    P.conocerID(N1, N2, "", "", "", 2)
                    cmbNivel2.Text = P.N2.PadLeft(C.NNiv2, "0")
                    idNivel2 = P.IDcuenta
                    cmbNomNivel2.Text = P.Descripcion
                End If

            End If
            If Nivel = 3 Then
                cmbNivel3.Text = N3.PadLeft(C.NNiv3, "0")
                cmbNomNivel3.Text = Descripcion
                encontrado = True
                idNivel3 = pID
                NuevoNivel4()
            Else
                If encontrado = False Then
                    P.conocerID(N1, N2, N3, "", "", 3)
                    cmbNivel3.Text = P.N3.PadLeft(C.NNiv3, "0")
                    idNivel3 = P.IDcuenta
                    cmbNomNivel3.Text = P.Descripcion
                End If

            End If
            If Nivel = 4 Then
                cmbNivel4.Text = N4.PadLeft(C.NNiv4, "0")
                cmbNomNivel4.Text = Descripcion
                encontrado = True
                idNivel4 = pID
                NuevoNivel5()
            Else
                If encontrado = False Then
                    P.conocerID(N1, N2, N3, N4, "", 4)
                    cmbNivel4.Text = P.N4.PadLeft(C.NNiv4, "0")
                    idNivel4 = P.IDcuenta
                    cmbNomNivel4.Text = P.Descripcion
                End If
            End If
            If Nivel = 5 Then
                NuevoNivel5()
                cmbNivel5.Text = N5.PadLeft(C.NNiv5, "0")
                cmbNomNivel5.Text = Descripcion
                idNivel5 = pID
                encontrado = True

            Else
                If encontrado = False Then
                    P.conocerID(N1, N2, N3, N4, N5, 5)
                    cmbNivel5.Text = P.N5.PadLeft(C.NNiv5, "0")
                    idNivel5 = P.IDcuenta
                    cmbNomNivel5.Text = P.Descripcion
                End If

            End If

            cmbNaturaleza1.SelectedIndex = Naturaleza
            cmbTipo1.SelectedIndex = tipoCuenta
            dtpFecha.Value = fechaCuenta
            If Nivel <> 1 Then
                cmbNaturaleza1.Enabled = False
                cmbTipo1.Enabled = False
            Else
                cmbNaturaleza1.Enabled = True
                cmbTipo1.Enabled = True
            End If
            btnModificar.Enabled = True
            btnEliminar.Enabled = True
            btnGuardar.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub cmbTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged
        If cmbTipo.SelectedIndex = 0 Then
            cmbSubTipo.Items.Clear()
            cmbSubTipo.Items.Add("TODOS")
            cmbSubTipo.Items.Add("ACTIVO A CORTO PLAZO")
            cmbSubTipo.Items.Add("ACTIVO A LARGO PLAZO")
            cmbSubTipo.SelectedIndex = 0
            cmbSubTipo.Enabled = True

        Else
            If cmbTipo.SelectedIndex = 1 Then
                cmbSubTipo.Items.Clear()
                cmbSubTipo.Items.Add("TODOS")
                cmbSubTipo.Items.Add("PASIVOS A CORTO PLAZO")
                cmbSubTipo.Items.Add("PASIVOS A LARGO PLAZO")
                cmbSubTipo.SelectedIndex = 0
                cmbSubTipo.Enabled = True

            Else
                cmbSubTipo.Items.Clear()
                cmbSubTipo.Enabled = False
                cmbCuenta.Enabled = True
            End If
        End If
        If cmbTipo.SelectedIndex = 2 Then
            cmbSubTipo.Items.Clear()
            LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>301 and codigo<=307 ", "SELECCIONE UNA CUENTA", "id")

        End If
        If cmbTipo.SelectedIndex = 3 Then
            cmbSubTipo.Items.Clear()
            LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>=400 and codigo<=404 ", "SELECCIONE UNA CUENTA", "id")
        End If
        If cmbTipo.SelectedIndex = 4 Then
            cmbSubTipo.Items.Clear()
            LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>=500 and codigo<=506 ", "SELECCIONE UNA CUENTA", "id")
        End If
        If cmbTipo.SelectedIndex = 5 Then
            cmbSubTipo.Items.Clear()
            LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>=600 and codigo<=615 ", "SELECCIONE UNA CUENTA", "id")
        End If
        If cmbTipo.SelectedIndex = 6 Then
            cmbSubTipo.Items.Clear()
            LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>=700 and codigo<=705 ", "SELECCIONE UNA CUENTA", "id")

        End If
        If cmbTipo.SelectedIndex = 7 Then
            cmbSubTipo.Items.Clear()
            LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>=800 and codigo<900 ", "SELECCIONE UNA CUENTA", "id")

        End If
        If cmbTipo.SelectedIndex = 8 Then
            cmbSubTipo.Items.Clear()
            LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo=0 ", "SELECCIONE UNA CUENTA", "id")

        End If
        cmbCuenta.SelectedIndex = 0
    End Sub

    Private Sub cmbSubTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSubTipo.SelectedIndexChanged
        If cmbTipo.SelectedIndex = 0 Then
            If cmbSubTipo.SelectedIndex = 0 Then
                cmbCuenta.Items.Clear()
                LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>=100 and codigo<=199 ", "SELECCIONE UNA CUENTA", "id")
            End If
            If cmbSubTipo.SelectedIndex = 1 Then
                cmbCuenta.Items.Clear()
                LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>=100 and codigo<=122 ", "SELECCIONE UNA CUENTA", "id")
            End If
            If cmbSubTipo.SelectedIndex = 2 Then
                cmbCuenta.Items.Clear()
                LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>=150 and codigo<=199 ", "SELECCIONE UNA CUENTA", "id")
            End If
        End If
        If cmbTipo.SelectedIndex = 1 Then
            If cmbSubTipo.SelectedIndex = 0 Then
                cmbCuenta.Items.Clear()
                LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>=200 and codigo<=270 ", "SELECCIONE UNA CUENTA", "id")
            End If
            If cmbSubTipo.SelectedIndex = 1 Then
                cmbCuenta.Items.Clear()
                LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>=200 and codigo<=220", "SELECCIONE UNA CUENTA", "id")
            End If
            If cmbSubTipo.SelectedIndex = 2 Then
                cmbCuenta.Items.Clear()
                LlenaCombos("tblagrupadorcuentas", cmbCuenta, "concat(codigo,' - ',Nombre)", "nombret", "id", cuenta, "codigo>=250 and codigo<=270 ", "SELECCIONE UNA CUENTA", "id")
            End If
        End If
    End Sub

    Private Sub cmbBuscarNivel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Filtro()
    End Sub

    Private Sub btnGuardar_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.EnabledChanged
        If btnGuardar.Enabled = True Then
            ckAplicarSubcuentas.Enabled = False

        Else
            ckAplicarSubcuentas.Enabled = True
        End If
    End Sub

    Private Sub btnModificar_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.EnabledChanged
        If btnModificar.Enabled = False Then
            ckAplicarSubcuentas.Enabled = False

        Else
            ckAplicarSubcuentas.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            OpenFileDialog1.FileName = "CUENTAS"
            OpenFileDialog1.Filter = "*.xls|*.xlsx"
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim I As New Importador(OpenFileDialog1.FileName, MySqlcon)

                I.ImportaCuentasContables()

                I.CierraConexiones()
                Filtro()
                MsgBox("Importación exitosa.")

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim instru As New frmInstrucciones()
        instru.Show()
    End Sub

    Private Sub cmbTipo1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbTipo1.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            cmbNaturaleza1.Focus()
        End If
    End Sub

    Private Sub cmbNaturaleza1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNaturaleza1.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            dtpFecha.Focus()
        End If
    End Sub

    Private Sub dtpFecha_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpFecha.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            cmbTipo.Focus()
        End If
    End Sub

    Private Sub cmbTipo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbTipo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            cmbSubTipo.Focus()
        End If
    End Sub

    Private Sub cmbSubTipo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbSubTipo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            cmbCuenta.Focus()
        End If
    End Sub

    Private Sub cmbCuenta_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            If ckAplicarSubcuentas.Enabled = True Then
                ckAplicarSubcuentas.Focus()
            Else
                If btnGuardar.Enabled = True Then
                    btnGuardar.Focus()
                Else
                    btnModificar.Focus()
                End If
            End If

        End If
    End Sub

    Private Sub ckAplicarSubcuentas_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ckAplicarSubcuentas.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            If btnGuardar.Enabled = True Then
                btnGuardar.Focus()
            Else
                btnModificar.Focus()
            End If
        End If
    End Sub

    Private Sub btnGuardar_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnGuardar.KeyPress

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Rep = New repContabilidadCat
        'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
        Rep.SetDataSource(P.Consulta("", 0, "", True, CheckBox2.Checked))

        Rep.SetParameterValue("empresa", s.Nombre)
        Rep.SetParameterValue("rfc", s.RFC)


        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub cmbNomNivel5_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNomNivel5.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbNivel5.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            Nivel5Enter2()
        End If
    End Sub

    Private Sub cmbNomNivel4_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNomNivel4.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbNivel4.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            nivel4Enter2()
        End If
    End Sub

    Private Sub cmbNomNivel3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNomNivel3.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbNivel3.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            Nivel3Enter2()
        End If
    End Sub

    Private Sub cmbNomNivel2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNomNivel2.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbNivel2.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            Nivel2Enter2()
        End If
    End Sub

    Private Sub cmbNomNivel1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNomNivel1.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbNivel1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            Nivel1enter2()
        End If
    End Sub

    Private Sub cmbNivel5_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNivel5.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbNomNivel4.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            Nivel5Enter()
        End If
    End Sub

    Private Sub cmbNivel4_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNivel4.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbNomNivel3.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            Nivel4Enter()
        End If
    End Sub

    Private Sub cmbNivel3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNivel3.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbNomNivel2.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            Nivel3Enter()
        End If
    End Sub

    Private Sub cmbNivel2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNivel2.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbNomNivel1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            nivel2enter()
        End If
    End Sub

    Private Sub btnNuevoTodo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnNuevoTodo.KeyDown
        If btnEliminar.Enabled = True Then
            btnEliminar.Focus()
        Else
            If btnModificar.Enabled = True Then
                btnModificar.Focus()
            Else
                If btnGuardar.Enabled = True Then
                    btnGuardar.Focus()
                Else
                    GroupBox1.Focus()
                    If ckAplicarSubcuentas.Enabled = True Then
                        ckAplicarSubcuentas.Focus()
                    Else
                        cmbCuenta.Focus()
                    End If
                End If

            End If
        End If
    End Sub

    Private Sub btnEliminar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnEliminar.KeyDown
        If btnModificar.Enabled = True Then
            btnModificar.Focus()
        Else
            btnGuardar.Focus()
        End If
    End Sub

    Private Sub btnModificar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnModificar.KeyDown
        If e.KeyCode = Keys.Escape Then
            GroupBox1.Focus()
            ckAplicarSubcuentas.Focus()
        End If
    End Sub

    Private Sub btnGuardar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnGuardar.KeyDown
        If e.KeyCode = Keys.Escape Then
            GroupBox1.Focus()
            ckAplicarSubcuentas.Focus()
        End If
    End Sub

    Private Sub ckAplicarSubcuentas_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ckAplicarSubcuentas.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbCuenta.Focus()
        End If
    End Sub

    Private Sub cmbCuenta_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            If cmbSubTipo.Enabled = True Then
                cmbSubTipo.Focus()
            Else
                cmbTipo.Focus()
            End If
        End If

    End Sub

    Private Sub cmbSubTipo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSubTipo.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbTipo.Focus()
        End If

    End Sub

    Private Sub cmbTipo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbTipo.KeyDown
        If e.KeyCode = Keys.Escape Then
            grpCuenta.Focus()
            chkDIOT.Focus()
        End If
    End Sub

    Private Sub chkDIOT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles chkDIOT.KeyDown
        If e.KeyCode = Keys.Escape Then
            dtpFecha.Focus()
        End If
    End Sub

    Private Sub dtpFecha_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpFecha.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbNaturaleza1.Focus()
        End If
    End Sub

    Private Sub cmbNaturaleza1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNaturaleza1.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbTipo1.Focus()
        End If
    End Sub

    Private Sub cmbTipo1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbTipo1.KeyDown
        If e.KeyCode = Keys.Escape Then
            grpNiveles.Focus()
            If cmbNomNivel5.Enabled = True Then
                cmbNomNivel5.Focus()
            Else
                If cmbNomNivel4.Enabled = True Then
                    cmbNomNivel4.Focus()
                Else
                    If cmbNomNivel3.Enabled = True Then
                        cmbNomNivel3.Focus()
                    Else
                        If cmbNomNivel2.Enabled = True Then
                            cmbNomNivel2.Focus()
                        Else
                            cmbNomNivel1.Focus()
                        End If
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub btnCerrar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnCerrar.KeyDown
        If e.KeyCode = Keys.Escape Then
            Button2.Focus()
        End If
    End Sub

    Private Sub Button2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Button2.KeyDown
        If e.KeyCode = Keys.Escape Then
            Button1.Focus()
        End If
    End Sub

    Private Sub Button1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Button1.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnImprimir.Focus()
        End If
    End Sub

    Private Sub btnImprimir_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnImprimir.KeyDown
        If e.KeyCode = Keys.Escape Then
            grpBuscar.Focus()
            DataGridView1.Focus()
        End If
    End Sub

   

    Private Sub btnLimpiar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            txtBuscar.Focus()
        End If
    End Sub
 

    Private Sub txtBuscarCuenta_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            btnNuevoTodo.Focus()
        End If
    End Sub

    Private Sub cmbNivel1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNivel1.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnCerrar.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            nivel1enter()
        End If
    End Sub

    Private Sub frmContabilidadClasificaciones_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If Nivel = 1 Then
            cmbNomNivel1.Focus()
        Else
            If Nivel = 2 Then
                cmbNomNivel2.Focus()
            Else
                If Nivel = 3 Then
                    cmbNomNivel3.Focus()
                Else
                    If Nivel = 4 Then
                        cmbNomNivel4.Focus()
                    Else
                        cmbNomNivel5.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnXML_Click(sender As Object, e As EventArgs) Handles btnXML.Click
        Dim B As New frmContabilidadXML()
        B.ShowDialog()
        B.Dispose()
    End Sub

    Private Sub cmbCuenta_KeyDown_1(sender As Object, e As KeyEventArgs) Handles cmbCuenta.KeyDown
        If e.KeyValue = Keys.F1 Then
            Dim b As New frmContabilidadBuscarAgrupador()
            b.ShowDialog()
            If b.DialogResult = Windows.Forms.DialogResult.OK Then
                If b.TIPO = "ACTIVO" Then
                    cmbTipo.SelectedIndex = 0
                Else
                    If b.TIPO = "PASIVO" Then
                        cmbTipo.SelectedIndex = 1
                    Else
                        If b.TIPO = "CAPITAL CONTABLE" Then
                            cmbTipo.SelectedIndex = 2
                        Else
                            If b.TIPO = "INGRESOS" Then
                                cmbTipo.SelectedIndex = 3
                            Else
                                If b.TIPO = "COSTOS" Then
                                    cmbTipo.SelectedIndex = 4
                                Else
                                    If b.TIPO = "GASTOS" Then
                                        cmbTipo.SelectedIndex = 5
                                    Else
                                        If b.TIPO = "RESULTADO INTEGRAL DE FINANCIAMIENTO" Then
                                            cmbTipo.SelectedIndex = 6
                                        Else
                                            If b.TIPO = "CUENTAS DE ORDEN" Then
                                                cmbTipo.SelectedIndex = 7
                                            Else
                                                If b.TIPO = "OTROS" Then
                                                    cmbTipo.SelectedIndex = 8
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                'cuenta.Busca(b.id)
                cmbCuenta.Text = b.id

            End If
        End If
        If e.KeyCode = Keys.Enter Then
            If btnGuardar.Enabled Then
                BotonGuardar()
            Else
                BotonModificar()
            End If
        End If
    End Sub

  

    
    Private Sub cmbNivel1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNivel1.SelectedIndexChanged
        If ConsultaOn Then
            ConsultaOn = False
            cmbNomNivel1.SelectedIndex = cmbNivel1.SelectedIndex
            ConsultaOn = True
        End If
    End Sub

    Private Sub cmbNivel1_Leave(sender As Object, e As EventArgs) Handles cmbNivel1.Leave
        'If cmbNivel1.Text <> "" Then
        '    nivel1enter()
        'End If
    End Sub

    Private Sub cmbNivel2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNivel2.SelectedIndexChanged
        If ConsultaOn Then
            ConsultaOn = False
            cmbNomNivel2.SelectedIndex = cmbNivel2.SelectedIndex
            ConsultaOn = True
        End If
    End Sub

    Private Sub cmbNivel3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNivel3.SelectedIndexChanged
        If ConsultaOn Then
            ConsultaOn = False
            cmbNomNivel3.SelectedIndex = cmbNivel3.SelectedIndex
            ConsultaOn = True
        End If
    End Sub

    Private Sub cmbNivel4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNivel4.SelectedIndexChanged
        If ConsultaOn Then
            ConsultaOn = False
            cmbNomNivel3.SelectedIndex = cmbNivel3.SelectedIndex
            ConsultaOn = True
        End If
    End Sub

    Private Sub cmbNivel5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNivel5.SelectedIndexChanged
        If ConsultaOn Then
            ConsultaOn = False
            cmbNomNivel5.SelectedIndex = cmbNivel5.SelectedIndex
            ConsultaOn = True
        End If
    End Sub

    Private Sub cmbNivel2_Leave(sender As Object, e As EventArgs) Handles cmbNivel2.Leave
        'If cmbNivel2.Text <> "" Then
        '    nivel2enter()
        'End If
    End Sub

    Private Sub cmbNivel3_Leave(sender As Object, e As EventArgs) Handles cmbNivel3.Leave
        'If cmbNivel3.Text <> "" Then
        '    Nivel3Enter()
        'End If
    End Sub

    Private Sub cmbNivel4_Leave(sender As Object, e As EventArgs) Handles cmbNivel4.Leave
        'If cmbNivel4.Text <> "" Then
        '    Nivel4Enter()
        'End If
    End Sub

    Private Sub cmbNivel5_Leave(sender As Object, e As EventArgs) Handles cmbNivel5.Leave
        'If cmbNivel5.Text <> "" Then
        '    Nivel5Enter()
        'End If
    End Sub

    Private Sub cmbCuenta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCuenta.SelectedIndexChanged

    End Sub

    Private Sub cmbNomNivel1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNomNivel1.SelectedIndexChanged
        If ConsultaOn Then
            ConsultaOn = False
            cmbNivel1.SelectedIndex = cmbNomNivel1.SelectedIndex
            ConsultaOn = True
        End If
    End Sub

    Private Sub cmbNomNivel1_Leave(sender As Object, e As EventArgs) Handles cmbNomNivel1.Leave
        'If cmbNomNivel1.Text <> "" Then
        '    Nivel1enter2()
        'End If
    End Sub

    Private Sub cmbNomNivel2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNomNivel2.SelectedIndexChanged
        If ConsultaOn Then
            ConsultaOn = False
            cmbNivel2.SelectedIndex = cmbNomNivel2.SelectedIndex
            ConsultaOn = True
        End If
    End Sub

    Private Sub cmbNomNivel2_Leave(sender As Object, e As EventArgs) Handles cmbNomNivel2.Leave
        'If cmbNomNivel2.Text <> "" Then
        '    Nivel2Enter2()
        'End If
    End Sub

    Private Sub cmbNomNivel3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNomNivel3.SelectedIndexChanged
        If ConsultaOn Then
            ConsultaOn = False
            cmbNivel3.SelectedIndex = cmbNomNivel3.SelectedIndex
            ConsultaOn = True
        End If
    End Sub

    Private Sub cmbNomNivel3_Leave(sender As Object, e As EventArgs) Handles cmbNomNivel3.Leave
        'If cmbNomNivel3.Text <> "" Then
        '    Nivel3Enter2()
        'End If
    End Sub

    Private Sub cmbNomNivel4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNomNivel4.SelectedIndexChanged
        If ConsultaOn Then
            ConsultaOn = False
            cmbNivel4.SelectedIndex = cmbNomNivel4.SelectedIndex
            ConsultaOn = True
        End If
    End Sub

    Private Sub cmbNomNivel4_Leave(sender As Object, e As EventArgs) Handles cmbNomNivel4.Leave
        'If cmbNomNivel4.Text <> "" Then
        '    nivel4Enter2()
        'End If
    End Sub

    Private Sub cmbNomNivel5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNomNivel5.SelectedIndexChanged
        If ConsultaOn Then
            ConsultaOn = False
            cmbNivel5.SelectedIndex = cmbNomNivel5.SelectedIndex
            ConsultaOn = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Filtro()
    End Sub

   
End Class