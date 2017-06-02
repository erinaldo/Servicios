Public Class frmLogIn
    Dim BasedeDatos As String
    Dim Servidor As String
    Dim DBUsuario As String
    Dim DBPassword As String
    Dim DBPuerto As String
    Dim IdsUsuarios As New elemento
    Public Sub New(ByVal pServidor As String, ByVal pBasededatos As String, ByVal pDBusuario As String, ByVal pdbPassword As String, ByVal pDbPuerto As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        BasedeDatos = pBasededatos
        Servidor = pServidor
        DBUsuario = pDBusuario
        DBPassword = pdbPassword
        DBPuerto = pDbPuerto
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Button1.Enabled = False
            Button2.Enabled = False
            Dim U As New dbUsuarios(MySqlcon)
            If U.ChecaLogin(ComboBox1.Text, TextBox2.Text) Then
                GlobalIdUsuario = U.ID
                GlobalTipoUsuario = U.Tipo
                GlobalUsuarioIdVendedor = U.IdVendedor
                'permisoscatalogos1 = U.PermisosCatalogos
                'PermisosCatalogos2 = U.PermisosCatalogos12
                'PermisosVentas = U.PermisosVentas
                'PermisosCompras = U.PermisosCompras
                'PermisosInventario = U.PermisosInventario
                'PermisosOtros = U.PermisosHerramientas
                GlobalPermisos.PermisosCatalogos = U.PermisosCatalogos
                GlobalPermisos.PermisosCatalogos2 = U.PermisosCatalogos2
                GlobalPermisos.PermisosCompras = U.PermisosCompras
                GlobalPermisos.PermisosHerramientas = U.PermisosHerramientas
                GlobalPermisos.PermisosPuntodeVenta = U.PermisosPuntodeVenta
                GlobalPermisos.PermisosVentas = U.PermisosVentas
                GlobalPermisos.PermisosInventario = U.PermisosInventario
                GlobalPermisos.PermisosBancos = U.PermisosBancos
                GlobalPermisos.PermisosNomina = U.PermisosNomina
                GlobalPermisos.PermisosServicios = U.PermisosServicios
                GlobalPermisos.PermisosGastos = U.PermisosGastos
                GlobalPermisos.PermisosEmpenios = U.PermisosEmpenios
                GlobalPermisos.PermisosFertilizantes = U.PermisosFertilizantes
                GlobalPermisos.PermisosContabilidad = U.PermisosContabilidad
                GlobalPermisos.PermisosSemillas = U.PermisosSemillas
                GlobalUsuario = U.NombreUsuario
                Label3.Visible = True
                My.Application.DoEvents()
                If InicioRapido() = False Then
                    Try
                        Dim Btp As New banxico.DgieWS
                        Dim doc As New System.Xml.XmlDocument()
                        doc.LoadXml(Btp.tiposDeCambioBanxico)
                        GlobaltpBanxico = ""
                        For Each n As System.Xml.XmlNode In doc.GetElementsByTagName("bm:Series")
                            If n.Attributes("IDSERIE").Value = "SF43718" Then
                                GlobaltpBanxico = n.FirstChild.Attributes("OBS_VALUE").Value
                            End If
                        Next
                        If GlobaltpBanxico = "" Then
                            For Each n As System.Xml.XmlNode In doc.GetElementsByTagName("bm:Series")
                                If n.Attributes("IDSERIE").Value = "SF60653" Then
                                    GlobaltpBanxico = n.FirstChild.Attributes("OBS_VALUE").Value
                                End If
                            Next
                        End If
                    Catch ex As Exception
                        GlobaltpBanxico = "Error"
                    End Try
                    Try
                        Dim HC As New dbClientes(MySqlcon)
                        Dim r As New repCatalogoClientes
                        r.SetDataSource(HC.Reporte("xxxzzzzyyy"))
                        r.Load()
                        Dim fr As New frmReportes(r, True)
                        fr.Opacity = 0
                        fr.Show()
                    Catch ex As Exception
                        MsgBox("A ocurrido un error inicializando Crystal Reports. " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                    End Try
                Else
                    GlobaltpBanxico = "Error"
                End If
                If GlobalLicenciaSTR = "RG1Q5LG1KH" Then
                    If MsgBox("¿Normal?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        Dim f As New frmPrincipal
                        f.Show()
                        Me.Close()
                    Else
                        Dim f As New frmPrincipalN
                        f.Show()
                        Me.Close()
                    End If
                Else
                    Dim f As New frmPrincipal
                    f.Show()
                    Me.Close()
                End If

            Else
                Button1.Enabled = True
                Button2.Enabled = True
                MsgBox("Nombre de usuario o contraseña incorrectos.", MsgBoxStyle.Information, GlobalNombreApp)
            End If

        Catch ex As Exception
            Button1.Enabled = True
            Button2.Enabled = True
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        'Dim f As New frmPrincipal
        'f.Show()
        'Me.Close()
    End Sub


    Private Sub frmLogIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Entrada - " + Version
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            'GlobalIcono = New Icon(Application.StartupPath + "\iconos\iconoico.ico")
            ImagenPopUp = Image.FromFile("fondopopup.png")
            ImagenFondo = Image.FromFile("fondopullb.jpg")
            Me.BackgroundImageLayout = ImageLayout.Stretch
            Me.BackgroundImage = ImagenFondo
            'Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            'PideSettings()
            If IniciarMySQL(BasedeDatos, Servidor, DBUsuario, DBPassword, DBPuerto) = 0 Then
                MsgBox("Ha habido un problema de conexión a la base de datos, verifique sus conexiones de red y vuelva a intentarlo. Si el problema continua consulte al proveedor del sistema.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                If MsgBox("¿Desea modificar la configuración de conexión?" + vbCrLf + "(Necesita de una clave especial.)", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim F As New frmEmpresas
                    frmEmpresas.ShowDialog()
                    frmEmpresas.Dispose()
                End If
                'ResetearSettings()
                MsgBox("El sistema se cerrara.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                Me.Close()
            End If
            If ChecaDB() = False Then
                MsgBox("Este ejecutable no es compatible con la base de datos a la que se intenta conectar. El sistema se cerrara.", MsgBoxStyle.Critical, GlobalNombreApp)
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, GlobalNombreApp)
        End Try
        If GlobalTipoVersion = 3 Then
            Try
                Dim U As New dbUsuarios(1, MySqlcon)
                If U.ID <> 0 Then
                    GlobalIdUsuario = U.ID
                    GlobalTipoUsuario = U.Tipo
                    GlobalPermisos.PermisosCatalogos = U.PermisosCatalogos
                    GlobalPermisos.PermisosCatalogos2 = U.PermisosCatalogos2
                    GlobalPermisos.PermisosCompras = U.PermisosCompras
                    GlobalPermisos.PermisosHerramientas = U.PermisosHerramientas
                    GlobalPermisos.PermisosInventario = U.PermisosInventario
                    GlobalPermisos.PermisosPuntodeVenta = U.PermisosPuntodeVenta
                    GlobalPermisos.PermisosVentas = U.PermisosVentas
                    'PermisosCatalogos1 = U.PermisosCatalogos
                    'PermisosCatalogos2 = U.PermisosCatalogos12
                    'PermisosVentas = U.PermisosVentas
                    'PermisosCompras = U.PermisosCompras
                    'PermisosInventario = U.PermisosInventario
                    'PermisosOtros = U.PermisosHerramientas
                    GlobalUsuario = U.Nombre
                    Dim f As New frmPrincipal
                    f.Show()
                    Me.Close()
                Else
                    MsgBox("Error de usuario.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        Else
            LlenaCombos("tblusuarios", ComboBox1, "nombreusuario", "NombreU", "idusuario", IdsUsuarios)
        End If
    End Sub
    'Private Sub PideSettings()
    '    If My.Settings.Servidor = "" Or My.Settings.PideDatos Then
    '        Dim dialog As New frmInput("Servidor:", frmInput.TipoDatos.Texto, True)
    '        dialog.ShowDialog()
    '        My.Settings.Servidor = dialog.Valor

    '    End If
    '    If My.Settings.BasedeDatos = "" Or My.Settings.PideDatos Then
    '        Dim dialog As New frmInput("Base de Datos:", frmInput.TipoDatos.Texto, True)
    '        dialog.ShowDialog()
    '        My.Settings.BasedeDatos = dialog.Valor
    '    End If
    '    If My.Settings.DBUsuario = "" Or My.Settings.PideDatos Then
    '        Dim dialog As New frmInput("Usuario:", frmInput.TipoDatos.Texto, True)
    '        dialog.ShowDialog()
    '        My.Settings.DBUsuario = dialog.Valor
    '    End If
    '    If My.Settings.DBPassword = "" Or My.Settings.PideDatos Then
    '        Dim dialog As New frmInput("Password:", frmInput.TipoDatos.Texto, True, True)
    '        dialog.ShowDialog()
    '        My.Settings.DBPassword = dialog.Valor
    '    End If
    '    If My.Settings.PideDatos Then
    '        Dim dialog As New frmInput("Puerto:", frmInput.TipoDatos.Texto, False)
    '        dialog.ShowDialog()
    '        My.Settings.puertodb = dialog.Valor
    '    End If
    '    If My.Settings.PideDatos Then My.Settings.PideDatos = False
    'End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Dim down As New Actualizador
        'If down.Descarga = 0 Then
        '    MsgBox("Trono " + down.MensajeError, MsgBoxStyle.OkOnly, "-")
        'End If
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ResetearSettings()
        Me.Close()
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
End Class
