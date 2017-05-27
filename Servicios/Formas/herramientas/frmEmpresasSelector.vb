Public Class frmEmpresasSelector
    Dim Em As New dbEmpresas
    Dim Ids As New elemento
    Dim IdEmpresa As Integer
    Public ChecaDefault As Boolean = True
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        'ChecaDefault = pChecadefault
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmEmpresasSelector_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Em.MySqlconE.Close()
    End Sub
    Private Sub frmEmpresasSelector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = "Seleccionar Empresa - " + Version
            GlobalIcono = New Icon(Application.StartupPath + "\iconos\iconochico.ico")
            'GlobalIconoC = New Icon(Application.StartupPath + "\iconos\iconochico.ico")
            Me.Icon = GlobalIcono
        Catch ex As Exception
        End Try
        
        Try
            ' ''Licencia
            Dim Lic As New Licencia("localhost", False)
            If My.Settings.serial = "" Then
                Dim frmI As New frmInput("Licencia:", frmInput.TipoDatos.Texto, True, False, 10)
                If frmI.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Lic.Licencia = frmI.Valor
                Else
                    MsgBox("Debe intruducir una licencia.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End
                End If
                frmI.Dispose()
            Else
                Lic.Licencia = My.Settings.serial
            End If
            If Lic.Validar(Lic.Licencia, False) Then
                GlobalTipoVersion = Lic.TipoVerFinal
                ConPuntodeVenta = Lic.ConPV
                GlobalConLicencias = Lic.ConLic
                Global30Dias = Lic.Lic30dias
                GlobalLicenciaA = Lic.NombreCliente
                GlobalConBancos = Lic.Bancos
                GlobalLicenciaSTR = Lic.Licencia
                GlobalConNomina = Lic.Nomina
                GlobalConServicios = Lic.Servicios
                GlobalConGastos = Lic.Gastos
                GlobalconEmpenios = Lic.Empenios
                GlobalConContabilidad = Lic.Contabilidad
                GlobalConFertilizantes = Lic.Fertilizantes
                GlobalConServiciosInterno = Lic.ServiciosInterno
                GlobalConValidador = Lic.Validador
                GlobalconSemillas = Lic.Semillas
                GlobalConExtra = Lic.Extra
                GlobalconIntegracion = Lic.Integracion
                GlobalConUsuarios = Lic.Usuarios
                GlobalConRestaurant = Lic.Restaurant
                Dim FechaUE As String
                If Global30Dias Then
                    If My.Settings.ultimaejecucion > Format(Date.Now, "yyyy/MM/dd") Or Lic.FechaInstalacion > Format(Date.Now, "yyyy/MM/dd") Then
                        MsgBox("Es posible que haya un problema con la fecha de su computadora, favor de verificarla.", MsgBoxStyle.Critical, GlobalNombreApp)
                        End
                    Else
                        FechaUE = Format(DateAdd("d", 30, CDate(Lic.FechaInstalacion)), "yyyy/MM/dd")
                        If FechaUE < Format(Date.Now, "yyyy/MM/dd") Then
                            If MsgBox("Su periodo de 30 días de prueba se ha terminado. " + vbCrLf + " ¿Desea introducir una nueva licencia?" + vbCrLf + "", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                                Lic.BorrarLicenciadeSistema()
                                MsgBox("El sistema se cerrará, al abrirlo de nuevo pedirá su nueva licencia.", MsgBoxStyle.Information, GlobalNombreApp)
                            Else
                                MsgBox("El sistema se cerrará.", MsgBoxStyle.Information, GlobalNombreApp)
                            End If
                            End
                        End If
                    End If
                End If
            Else
                MsgBox(Lic.MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
                End
            End If
            'Termina licencia
            'para calar conector descomentariar la siguiente linea
            'GlobalTipoVersion = 3
            PideSettings()
            Em.IniciarMySQLE(My.Settings.BasedeDatos, My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
            If ChecaDefault Then
                IdEmpresa = My.Settings.empresadefault
            Else
                IdEmpresa = 0
            End If

            If IdEmpresa <> 0 Then
                Em.LlenaDatos(IdEmpresa)
                GlobalNombreEmpresa = Em.NombreEmpresa
                GlobalIdEmpresa = Em.IdEmpresa
                GlobalBasedeDatos = Em.NombreDB
                Dim Fl As New frmLogIn(Em.Servidor, Em.NombreDB, Em.Usuario, Em.PasswordS, My.Settings.puertodb)
                Fl.Show()
                Me.Close()
            Else
                Consulta()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim Dreader As MySql.Data.MySqlClient.MySqlDataReader
            Dreader = Em.ConsultaReader()
            While Dreader.Read
                ListBox1.Items.Add(Dreader("nombreempresa"))
                Ids.Agregar(Dreader("idempresa"))
            End While
            Dreader.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

    Private Sub PideSettings()
        If My.Settings.Servidor = "" Or My.Settings.PideDatos Then
            Dim dialog As New frmInput("Servidor:", frmInput.TipoDatos.Texto, True, , , "localhost")
            dialog.ShowDialog()
            My.Settings.Servidor = dialog.Valor
        End If
        If My.Settings.BasedeDatos = "" Or My.Settings.PideDatos Then
            Dim dialog As New frmInput("Base de Datos:", frmInput.TipoDatos.Texto, True, , , "db_servicesempresas")
            dialog.ShowDialog()
            My.Settings.BasedeDatos = dialog.Valor
        End If
        If My.Settings.DBUsuario = "" Or My.Settings.PideDatos Then
            Dim dialog As New frmInput("Usuario:", frmInput.TipoDatos.Texto, True, , , "root")
            dialog.ShowDialog()
            My.Settings.DBUsuario = dialog.Valor
        End If
        If My.Settings.DBPassword = "" Or My.Settings.PideDatos Then
            Dim dialog As New frmInput("Password:", frmInput.TipoDatos.Texto, True, True)
            dialog.ShowDialog()
            My.Settings.DBPassword = dialog.Valor
        End If
        If My.Settings.PideDatos Then
            Dim dialog As New frmInput("Puerto:", frmInput.TipoDatos.Texto, False, , , "3306")
            dialog.ShowDialog()
            My.Settings.puertodb = dialog.Valor
        End If
        If My.Settings.PideDatos Then My.Settings.PideDatos = False
    End Sub
    Private Sub ResetearSettings()
        Dim dialog As New frmInput("Clave:", frmInput.TipoDatos.Texto, False, True)
        dialog.ShowDialog()
        If dialog.Valor = "Reset" Then
            My.Settings.PideDatos = True
        Else
            PopUp("Clave Incorrecta", 90)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ResetearSettings()
        Me.Close()
    End Sub

    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        Seleccionar()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex >= 0 Then
            IdEmpresa = Ids.Valor(ListBox1.SelectedIndex)
        Else
            IdEmpresa = 0
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Seleccionar()
    End Sub
    Private Sub Seleccionar()
        If IdEmpresa > 0 Then
            If CheckBox1.Checked Then
                My.Settings.empresadefault = IdEmpresa
                'My.Settings.Save()
                'Em.MarcarDefault(IdEmpresa)
            End If
            Em.LlenaDatos(IdEmpresa)
            GlobalNombreEmpresa = Em.NombreEmpresa
            GlobalIdEmpresa = IdEmpresa
            GlobalBasedeDatos = Em.NombreDB
            Dim Fl As New frmLogIn(Em.Servidor, Em.NombreDB, Em.Usuario, Em.PasswordS, My.Settings.puertodb)
            Fl.Show()
            Me.Close()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class