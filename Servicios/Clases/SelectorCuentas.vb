Public Class SelectorCuentas
    Public C As dbContabilidadClasificacion
    Public P As dbContabilidadPolizas
    Private IdC As Integer
    Public ConsultaOn As Boolean
    Public Nivel As Integer
    Public EsDiot As Byte
    Public ActivaBuscador As Boolean
    Public SoloUltimoNivel As Boolean = True
    Public Property IdCuenta() As Integer
        Get
            Return IdC
        End Get
        Set(value As Integer)
            IdC = value
            RaiseEvent CambiaID()
        End Set
    End Property
    Public Event CambiaID()
    Public Sub Inicializar()
        'txtCuenta.MaxLength = P.NNiv1
        'txtN2.MaxLength = P.NNiv2
        'txtN3.MaxLength = P.NNiv3
        'txtN4.MaxLength = P.NNiv4
        'txtN5.MaxLength = P.NNiv5
        ConsultaOn = True
        Try
            PictureBox1.Image = Image.FromFile(Application.StartupPath + "\iconos\imgtextBox.png")
        Catch ex As Exception

        End Try

    End Sub
    Private Sub BuscarCuentaBoton()

        Dim buscador As String = ""
        If txtCuenta.Text <> "" Then
            buscador += txtCuenta.Text
        End If
        If txtN2.Text <> "" Then
            buscador += " " + txtN2.Text
        End If
        If txtN3.Text <> "" Then
            buscador += " " + txtN3.Text
        End If
        If txtN4.Text <> "" Then
            buscador += " " + txtN4.Text
        End If
        If txtN5.Text <> "" Then
            buscador += " " + txtN5.Text
        End If

        Dim B As New frmBuscadorCC(buscador, SoloUltimoNivel)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            'txtCuenta.Text = B.Cuenta.ToString()
            '' txtDesc.Text = B.descripcion.ToString()

            'lblDescripcion.Text = B.descripcion
            txtN2.Text = ""
            txtN2.Enabled = False
            txtN3.Text = ""
            txtN3.Enabled = False
            txtN4.Text = ""
            txtN4.Enabled = False
            txtN5.Text = ""
            txtN5.Enabled = False
            C.busquedaRegistro(B.ID)
            Nivel = C.Nivel
            txtCuenta.Text = C.N1.PadLeft(p.NNiv1, "0")
            If C.Nivel >= 2 Then
                txtN2.Text = C.N2.PadLeft(p.NNiv2, "0")
                txtN2.Enabled = True
            End If
            If C.Nivel >= 3 Then
                txtN3.Text = C.N3.PadLeft(p.NNiv3, "0")
                txtN3.Enabled = True
            End If
            If C.Nivel >= 4 Then
                txtN4.Text = C.N4.PadLeft(p.NNiv4, "0")
                txtN4.Enabled = True
            End If
            If C.Nivel >= 5 Then
                txtN5.Text = C.N5.PadLeft(p.NNiv5, "0")
                txtN5.Enabled = True
            End If
            txtDesc.Text = C.Descripcion
            EsDiot = C.DIOT
            IdCuenta = B.ID
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If txtN5.Enabled Then
            txtN5.Focus()
        Else
            If txtN4.Enabled Then
                txtN4.Focus()
            Else
                If txtN3.Enabled Then
                    txtN3.Focus()
                Else
                    If txtN2.Enabled Then
                        txtN2.Focus()
                    Else
                        txtCuenta.Focus()
                    End If
                End If
            End If
        End If
        'txtCuenta.Focus()
    End Sub

    Private Sub txtCuenta_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCuenta.KeyDown
        If e.KeyCode = Keys.F1 And ActivaBuscador = False Then
            BuscarCuentaBoton()
        End If
        If (e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down) And ActivaBuscador = False Then
            txtN2.Focus()
        End If
        If e.KeyCode = Keys.Escape And ActivaBuscador Then
            ActivaBuscador = False
        End If
        Me.OnKeyDown(e)
    End Sub

    Private Sub txtCuenta_Leave(sender As Object, e As EventArgs) Handles txtCuenta.Leave
        ConsultaOn = False
        If txtCuenta.Text <> "" Then txtCuenta.Text = txtCuenta.Text.PadLeft(P.NNiv1, "0")
        ConsultaOn = True
    End Sub

    Private Sub txtCuenta_TextChanged(sender As Object, e As EventArgs) Handles txtCuenta.TextChanged
        txtN2.Text = ""
        txtN3.Text = ""
        txtN4.Text = ""
        txtN5.Text = ""
        If IsNumeric(txtCuenta.Text) = False And txtCuenta.Text.Trim <> "" Then
            ActivaBuscador = True
        Else
            ActivaBuscador = False
            If txtCuenta.Text.Length >= P.NNiv1 And ConsultaOn Then
                BuscaCuenta()
                txtN2.Enabled = True
                txtN2.Focus()
            Else
                BuscaCuenta()
                If IdCuenta <> 0 Then
                    txtN2.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub txtN2_Click(sender As Object, e As EventArgs) Handles txtN2.Click
        If txtN2.Enabled = False Then txtCuenta.Focus()
    End Sub

    Private Sub txtN2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtN2.KeyDown
        
        If e.KeyCode = Keys.F1 Then
            BuscarCuentaBoton()
        End If
        If e.KeyCode = Keys.Back Then
            If txtN2.Text = "" Then
                txtCuenta.Focus()
                txtCuenta.Select(txtCuenta.Text.Length, 0)
                txtN2.Enabled = False
            End If
        End If
        If (e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down) And ActivaBuscador = False Then
            txtN3.Focus()
        End If
        If e.KeyCode = Keys.Escape And ActivaBuscador Then
            ActivaBuscador = False
        End If
        Me.OnKeyDown(e)
        If (e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up) And ActivaBuscador = False Then
            txtCuenta.Focus()
        End If
    End Sub

    Private Sub txtN2_Leave(sender As Object, e As EventArgs) Handles txtN2.Leave
        ConsultaOn = False
        If txtN2.Text <> "" Then txtN2.Text = txtN2.Text.PadLeft(P.NNiv2, "0")
        ConsultaOn = True
    End Sub

    Private Sub txtN2_TextChanged(sender As Object, e As EventArgs) Handles txtN2.TextChanged
        txtN3.Text = ""
        txtN4.Text = ""
        txtN5.Text = ""
        If IsNumeric(txtN2.Text) = False And txtN2.Text.Trim <> "" Then
            ActivaBuscador = True
        Else
            ActivaBuscador = False
            If txtN2.Text.Length >= P.NNiv2 And ConsultaOn Then
                BuscaCuenta()
                txtN3.Enabled = True
                txtN3.Focus()
            Else
                BuscaCuenta()
                If IdCuenta <> 0 Then
                    txtN3.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub txtN3_Click(sender As Object, e As EventArgs) Handles txtN3.Click
        If txtN3.Enabled = False Then txtCuenta.Focus()
    End Sub

    Private Sub txtN3_KeyDown(sender As Object, e As KeyEventArgs) Handles txtN3.KeyDown
        If e.KeyCode = Keys.F1 Then
            BuscarCuentaBoton()
        End If
        If e.KeyCode = Keys.Back Then
            If txtN3.Text = "" Then
                txtN2.Focus()
                txtN2.Select(txtN2.Text.Length, 0)
                txtN3.Enabled = False
            End If
        End If
        If (e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down) And ActivaBuscador = False Then
            txtN4.Focus()
        End If
        If (e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up) And ActivaBuscador = False Then
            txtN2.Focus()
        End If
        If e.KeyCode = Keys.Escape And ActivaBuscador Then
            ActivaBuscador = False
        End If
        Me.OnKeyDown(e)
    End Sub

    Private Sub txtN3_Leave(sender As Object, e As EventArgs) Handles txtN3.Leave
        ConsultaOn = False
        If txtN3.Text <> "" Then txtN3.Text = txtN3.Text.PadLeft(P.NNiv2, "0")
        ConsultaOn = True
    End Sub

    Private Sub txtN3_TextChanged(sender As Object, e As EventArgs) Handles txtN3.TextChanged
        txtN4.Text = ""
        txtN5.Text = ""
        If IsNumeric(txtN3.Text) = False And txtN3.Text.Trim <> "" Then
            ActivaBuscador = True
        Else
            ActivaBuscador = False
            If txtN3.Text.Length >= P.NNiv3 And ConsultaOn Then
                BuscaCuenta()
                txtN4.Enabled = True
                txtN4.Focus()
            Else
                BuscaCuenta()
                If IdCuenta <> 0 Then
                    txtN4.Enabled = True
                End If
            End If
        End If

    End Sub


    Private Sub txtN4_Click(sender As Object, e As EventArgs) Handles txtN4.Click
        If txtN4.Enabled = False Then txtCuenta.Focus()
    End Sub

    Private Sub txtN4_KeyDown(sender As Object, e As KeyEventArgs) Handles txtN4.KeyDown
        
        If e.KeyCode = Keys.F1 Then
            BuscarCuentaBoton()
        End If
        If e.KeyCode = Keys.Back Then
            If txtN4.Text = "" Then
                txtN3.Focus()
                txtN3.Select(txtN3.Text.Length, 0)
                txtN4.Enabled = False
            End If
        End If
        If (e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down) And ActivaBuscador = False Then
            txtN5.Focus()
        End If
        If (e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up) And ActivaBuscador = False Then
            txtN3.Focus()
        End If
        If e.KeyCode = Keys.Escape And ActivaBuscador Then
            ActivaBuscador = False
        End If
        Me.OnKeyDown(e)
    End Sub

    Private Sub txtN4_Leave(sender As Object, e As EventArgs) Handles txtN4.Leave
        ConsultaOn = False
        If txtN4.Text <> "" Then txtN4.Text = txtN4.Text.PadLeft(P.NNiv4, "0")
        ConsultaOn = True
    End Sub

    Private Sub txtN4_TextChanged(sender As Object, e As EventArgs) Handles txtN4.TextChanged
        txtN5.Text = ""
        If IsNumeric(txtN4.Text) = False And txtN4.Text.Trim <> "" Then
            ActivaBuscador = True
        Else
            ActivaBuscador = False
            If txtN4.Text.Length >= P.NNiv4 And ConsultaOn Then
                BuscaCuenta()
                txtN5.Enabled = True
                txtN5.Focus()
            Else
                BuscaCuenta()
                If IdCuenta <> 0 Then
                    txtN5.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub txtN5_Click(sender As Object, e As EventArgs) Handles txtN5.Click
        If txtN5.Enabled = False Then txtCuenta.Focus()
    End Sub


    Private Sub txtN5_KeyDown(sender As Object, e As KeyEventArgs) Handles txtN5.KeyDown
        If e.KeyCode = Keys.Back Then
            If txtN5.Text = "" Then
                txtN4.Focus()
                txtN4.Select(txtN4.Text.Length, 0)
                txtN5.Enabled = False
            End If
        End If
        If (e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up) And ActivaBuscador = False Then
            txtN4.Focus()
        End If
        If e.KeyCode = Keys.Escape And ActivaBuscador Then
            ActivaBuscador = False
        End If
        Me.OnKeyDown(e)
    End Sub

    Private Sub txtN5_Leave(sender As Object, e As EventArgs) Handles txtN5.Leave
        ConsultaOn = False
        If txtN5.Text <> "" Then txtN5.Text = txtN5.Text.PadLeft(P.NNiv5, "0")
        ConsultaOn = True
    End Sub

    Private Sub txtN5_TextChanged(sender As Object, e As EventArgs) Handles txtN5.TextChanged

        If IsNumeric(txtN5.Text) = False And txtN5.Text.Trim <> "" Then
            ActivaBuscador = True
        Else
            ActivaBuscador = False
            BuscaCuenta()
        End If
    End Sub

    Private Function BuscaCuenta() As Boolean
        If ConsultaOn Then
            Dim N1 As String = ""
            Dim N2 As String = ""
            Dim N3 As String = ""
            Dim N4 As String = ""
            Dim N5 As String = ""
            Dim IdTemp As Integer = 0
            If IsNumeric(txtCuenta.Text) Then
                N1 = CStr(CInt(txtCuenta.Text))
            Else
                If txtCuenta.Text <> "" Then N1 = "F"
            End If
            If IsNumeric(txtN2.Text) Then
                N2 = CStr(CInt(txtN2.Text))
            Else
                If txtN2.Text <> "" Then N2 = "F"
            End If
            If IsNumeric(txtN3.Text) Then
                N3 = CStr(CInt(txtN3.Text))
            Else
                If txtN3.Text <> "" Then N3 = "F"
            End If
            If IsNumeric(txtN4.Text) Then
                N4 = CStr(CInt(txtN4.Text))
            Else
                If txtN4.Text <> "" Then N4 = "F"
            End If
            If IsNumeric(txtN5.Text) Then
                N5 = CStr(CInt(txtN5.Text))
            Else
                If txtN5.Text <> "" Then N5 = "F"
            End If
            If N1 <> "F" And N2 <> "F" And N3 <> "F" And N4 <> "F" And N5 <> "F" Then
                IdTemp = P.BuscarIdCuenta(5, N1, N2, N3, N4, N5)
                If IdTemp <> 0 Then
                    txtDesc.Text = P.descripcionCuenta
                    Nivel = C.DaNivel(IdTemp)
                    EsDiot = C.DaDiot(IdTemp)
                Else
                    txtDesc.Text = ""
                    Nivel = 0
                    EsDiot = 0
                End If
                IdCuenta = IdTemp
            End If
        End If
        Return False
    End Function

    Public Sub LlenaCuenta(pIdCuenta As Integer)
        If C.busquedaRegistro(pIdCuenta) Then
            txtDesc.Text = C.Descripcion
            P.DNiv1 = C.Descripcion
            P.DNiv2 = C.Descripcion2
            P.DNiv3 = C.Descripcion3
            P.DNiv4 = C.Descripcion4
            P.DNiv5 = C.Descripcion5
            ConsultaOn = False
            txtCuenta.Text = C.N1.PadLeft(P.NNiv1, "0")
            If C.Nivel >= 2 Then
                txtN2.Text = C.N2.PadLeft(P.NNiv2, "0")
                txtN2.Enabled = True
                txtN3.Enabled = True
            End If
            If C.Nivel >= 3 Then
                txtN3.Text = C.N3.PadLeft(P.NNiv3, "0")
                txtN3.Enabled = True
                txtN4.Enabled = True
            End If
            If C.Nivel >= 4 Then
                txtN4.Text = C.N4.PadLeft(P.NNiv4, "0")
                txtN4.Enabled = True
                txtN5.Enabled = True
            End If
            If C.Nivel >= 5 Then
                txtN5.Text = C.N5.PadLeft(P.NNiv5, "0")
                txtN5.Enabled = True
            End If
            ConsultaOn = True
            'BuscaCuenta()
            'Nivel = C.DaNivel(IdCuenta)
            Nivel = C.Nivel
            EsDiot = C.DaDiot(pIdCuenta)
            IdCuenta = pIdCuenta
        Else
            IdCuenta = 0
            EsDiot = 0
        End If
    End Sub
    Public Sub Vacia()
        Nivel = 0
        IdCuenta = 0
        EsDiot = 0
        ConsultaOn = False
        txtCuenta.Text = ""
        txtN2.Text = ""
        txtN3.Text = ""
        txtN4.Text = ""
        txtN5.Text = ""
        txtDesc.Text = ""
        ConsultaOn = True
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        BuscarCuentaBoton()
    End Sub

    Public Function DaCuentatxt() As String
        Return txtCuenta.Text + txtN2.Text + txtN3.Text + txtN4.Text + txtN5.Text
    End Function

    Private Sub SelectorCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
