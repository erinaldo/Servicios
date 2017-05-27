Public Class frmCambioUsuario
    Dim IdsUsuarios As New elemento
    Dim Tipo As Byte
    Dim PermisoAChecar As Byte
    Public Sub New(ByVal pTipo As Byte, ByVal pPermisoaChecar As Byte)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Tipo = pTipo
        PermisoAChecar = pPermisoaChecar
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim U As New dbUsuarios(MySqlcon)
            If Tipo = 0 Then
                If U.ChecaLogin(ComboBox1.Text, TextBox2.Text) Then
                    GlobalIdUsuario = U.ID
                    GlobalTipoUsuario = U.Tipo
                    GlobalUsuarioIdVendedor = U.IdVendedor
                    GlobalPermisos.PermisosCatalogos = U.PermisosCatalogos
                    GlobalPermisos.PermisosCatalogos2 = U.PermisosCatalogos2
                    GlobalPermisos.PermisosCompras = U.PermisosCompras
                    GlobalPermisos.PermisosHerramientas = U.PermisosHerramientas
                    GlobalPermisos.PermisosPuntodeVenta = U.PermisosPuntodeVenta
                    GlobalPermisos.PermisosVentas = U.PermisosVentas
                    GlobalPermisos.PermisosInventario = U.PermisosInventario
                    GlobalPermisos.PermisosBancos = U.PermisosBancos
                    GlobalPermisos.PermisosServicios = U.PermisosServicios
                    GlobalPermisos.PermisosNomina = U.PermisosNomina
                    GlobalPermisos.PermisosGastos = U.PermisosGastos
                    GlobalPermisos.PermisosEmpenios = U.PermisosEmpenios
                    GlobalPermisos.PermisosServicios = U.PermisosServicios
                    GlobalPermisos.PermisosFertilizantes = U.PermisosFertilizantes
                    GlobalPermisos.PermisosSemillas = U.PermisosSemillas
                    GlobalPermisos.PermisosContabilidad = U.PermisosContabilidad
                    GlobalUsuario = U.NombreUsuario
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    'Label5.Text = "Nombre de usuario o contraseña incorrectos."
                    MsgBox("Nombre de usuario o contraseña incorrectos.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
            End If
            If Tipo = 1 Then
                If U.ChecaLogin(ComboBox1.Text, TextBox2.Text) Then
                    Dim CP As New PermisosN
                    CP.PermisosEmpenios = U.PermisosEmpenios
                    CP.PermisosPuntodeVenta = U.PermisosPuntodeVenta
                    If PermisoAChecar = 0 Then 'Checa cambio de vendedor
                        If CP.ChecaPermiso(PermisosN.Empenios.EmpeniosPermitirCambiarVendedor, PermisosN.Secciones.Empenios) = True Then
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        Else
                            MsgBox("No tiene permiso para realzar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                    If PermisoAChecar = 1 Then 'Checa Sobre evaluo
                        If CP.ChecaPermiso(PermisosN.Empenios.EmpeniosSobreValouPermitir, PermisosN.Secciones.Empenios) = True Or CP.ChecaPermiso(PermisosN.Empenios.NoLimitarEvaluo, PermisosN.Secciones.Empenios) = True Then
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        Else
                            MsgBox("No tiene permiso para realzar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                    If PermisoAChecar = 2 Then 'Checa Sobre evaluo sin limite
                        If CP.ChecaPermiso(PermisosN.Empenios.NoLimitarEvaluo, PermisosN.Secciones.Empenios) = True Then
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        Else
                            MsgBox("No tiene permiso para realzar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                    If PermisoAChecar = 2 Then 'Checa Sobre evaluo sin limite
                        If CP.ChecaPermiso(PermisosN.Empenios.NoLimitarEvaluo, PermisosN.Secciones.Empenios) = True Then
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        Else
                            MsgBox("No tiene permiso para realzar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                    '3 fecha cambio
                    If PermisoAChecar = 3 Then 'Checa Sobre evaluo sin limite
                        If CP.ChecaPermiso(PermisosN.Empenios.PermitirCambioFecha, PermisosN.Secciones.Empenios) = True Then
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        Else
                            MsgBox("No tiene permiso para realzar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                    '4 Eliminar Artículos punto de venta
                    If PermisoAChecar = 4 Then
                        If CP.ChecaPermiso(PermisosN.PuntodeVentas.VentasBaja, PermisosN.Secciones.PuntodeVenta) = True Then
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        Else
                            MsgBox("No tiene permiso para realzar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                    '4 Asignar cantidad punto de venta
                    If PermisoAChecar = 5 Then
                        If CP.ChecaPermiso(PermisosN.PuntodeVentas.AsignarCantidad, PermisosN.Secciones.PuntodeVenta) = True Then
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        Else
                            MsgBox("No tiene permiso para realzar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                Else
                    'Label5.Text = "Nombre de usuario o contraseña incorrectos."
                    MsgBox("Nombre de usuario o contraseña incorrectos.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub frmCambioUsuario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblusuarios", ComboBox1, "nombreusuario", "NombreU", "idusuario", IdsUsuarios)
        If Tipo = 1 Then
            Me.Text = "Autorizar"
            Button4.Text = "Autorizar"
        End If
    End Sub
End Class