Public Class frmSucursalesCertificados

    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Dim IdCertificado As Integer
    Dim IdSucursal As Integer
    Dim Certificado As String
    Public Sub New(ByVal pIdSucursal As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdSucursal = pIdSucursal
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox7.Text = ""
            TextBox5.Text = ""
            TextBox8.Text = "15"
            TextBox28.Text = ""
            TextBox26.Text = ""
            TextBox6.Text = ""
            CheckBox1.Checked = False
            TextBox26.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox28.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
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
                Dim P As New dbSucursalesCertificados(MySqlcon)
                DataGridView1.DataSource = P.Consulta(IdSucursal)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Serie"
                DataGridView1.Columns(2).HeaderText = "Activo"
                DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Consulta()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim P As New dbSucursalesCertificados(MySqlcon)
            Dim Archivos As New dbSucursalesArchivos
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            If IsNumeric(TextBox8.Text) = False Then
                TextBox8.Text = "15"
            End If
            If TextBox26.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Seleccione el archivo .cer."
                TextBox26.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox6.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Seleccione el archivo .key."
                TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox28.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un password."
                TextBox28.BackColor = Color.FromArgb(250, 150, 150)
            End If
            Dim en As New Encriptador
            en.CreaPFX(TextBox6.Text, TextBox28.Text, TextBox26.Text)
            If P.ChecaArchivoPfx(TextBox26.Text, TextBox28.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " Es posible que el password del archivo .key sea incorrecto."
            End If
            Dim Act As Byte = 0
            If CheckBox1.Checked Then
                Act = 1
            End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    Archivos.GuardaRutaCER(IdSucursal, TextBox26.Text, TextBox6.Text, TextBox28.Text, GlobalIdEmpresa)
                    P.Guardar(IdSucursal, TextBox1.Text, Certificado, TextBox28.Text, Act, TextBox5.Text, CInt(TextBox8.Text))
                    PopUp("Guardado", 90)
                    Nuevo()
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Archivos.GuardaRutaCER(IdSucursal, TextBox26.Text, TextBox6.Text, TextBox28.Text, GlobalIdEmpresa)
                        P.Modificar(IdCertificado, IdSucursal, TextBox1.Text, Certificado, TextBox28.Text, Act, TextBox5.Text, CInt(TextBox8.Text))
                        PopUp("Modificado", 90)
                        Nuevo()
                    End If
                End If

                TextBox1.Focus()
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
            Archivos.CierraDB()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim P As New dbSucursalesCertificados(MySqlcon)
                P.Eliminar(IdCertificado)
                PopUp("Eliminado", 90)
                Nuevo()
                TextBox1.Focus()
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este certificado debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
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
            IdCertificado = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbSucursalesCertificados(IdCertificado, MySqlcon)
            Dim Archivos As New dbSucursalesArchivos()
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            TextBox1.Text = P.NoSerie
            TextBox28.Text = P.PasswordS
            TextBox8.Text = P.Aviso.ToString
            If P.Activo = 1 Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
            Certificado = P.Certificado
            Archivos.DaRutaCER(IdSucursal, GlobalIdEmpresa, True)
            TextBox26.Text = Archivos.RutaCer
            TextBox6.Text = Archivos.RutaKey
            If IO.File.Exists(Archivos.RutaCer) Then
                LeeArchivoCer(TextBox26.Text)
            End If
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
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


    Private Sub frmClientesEquipos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Nuevo()
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        Consulta()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox26.Text = OpenFileDialog1.FileName
                LeeArchivoCer(TextBox26.Text)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox6.Text = OpenFileDialog1.FileName
        End If
    End Sub
    Private Sub LeeArchivoCer(ByVal Ruta As String)
        Try
            Dim en As New Encriptador
            If Ruta <> "" Then
                en.Leex509(Ruta)
                TextBox4.Text = en.NombreX509
                TextBox1.Text = en.Seriex509
                TextBox7.Text = en.RFCX09
                TextBox2.Text = en.EmitidoX509
                TextBox3.Text = en.FechaValidacionx509
                TextBox5.Text = en.FechaVencimientox509
                Certificado = en.Certificado64
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
        End Try

    End Sub

   
    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            TextBox28.PasswordChar = ""
        Else
            TextBox28.PasswordChar = "*"
        End If
    End Sub
End Class