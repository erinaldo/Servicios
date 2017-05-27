Public Class frmTiposCP

    Dim IdConcepto As Integer
    Dim IdsConceptos As New elemento
    Dim DeQueTipo As Byte
    Public Sub New(pDequetipo As Byte)

        ' This call is required by the designer.
        InitializeComponent()
        DeQueTipo = pDequetipo
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmInvnetarioConceptos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ComboBox1.Items.Add("Proveedor")
        ComboBox1.Items.Add("Acreedor")
        'ComboBox1.Items.Add("Parcialidad")
        Select Case DeQueTipo
            Case 0
                ComboBox1.Visible = False
                Label3.Visible = False
                Me.Text = "Tipos de Clientes"
            Case 1
                Me.Text = "Tipos de Proveedores"
            Case 2
                ComboBox1.Visible = False
                Label3.Visible = False
                Me.Text = "Tipos de Sucursales"
        End Select
        NuevoConcepto()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub NuevoConcepto()
        Try
            Button5.Text = "Guardar"
            Button6.Enabled = False
            Select Case DeQueTipo
                Case 0
                    LlenaCombos("tblclientestipos", ComboBox2, "nombre", "nombrec", "idtipo", IdsConceptos)
                Case 1
                    LlenaCombos("tblproveedorestipos", ComboBox2, "nombre", "nombrec", "idtipo", IdsConceptos)
                Case 2
                    LlenaCombos("tblsucursalestipos", ComboBox2, "nombre", "nombrec", "idtipo", IdsConceptos)
            End Select
            Button5.Enabled = True
            Button7.Enabled = True
            ComboBox1.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            ComboBox2.Text = ComboBox2.Text.ToUpper()
            If ComboBox2.Text <> "" Then
                Dim SC As New dbTiposCP(MySqlcon, DeQueTipo)
                If Button5.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ClientesTiposAlta, PermisosN.Secciones.Catalagos2) = False And DeQueTipo = 0 Then
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                        Exit Sub
                    End If
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ProvTiposAlta, PermisosN.Secciones.Catalagos2) = False And DeQueTipo = 1 Then
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                        Exit Sub
                    End If
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.SucTiposAlta, PermisosN.Secciones.Catalagos2) = False And DeQueTipo = 2 Then
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                        Exit Sub
                    End If
                    SC.Guardar(ComboBox2.Text, ComboBox1.SelectedIndex)
                    PopUp("Guardado", 90)
                    NuevoConcepto()
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ClientesTiposCambio, PermisosN.Secciones.Catalagos2) = False And DeQueTipo = 0 Then
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                        Exit Sub
                    End If
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ProvTiposCambio, PermisosN.Secciones.Catalagos2) = False And DeQueTipo = 1 Then
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                        Exit Sub
                    End If
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.SucTiposCambio, PermisosN.Secciones.Catalagos2) = False And DeQueTipo = 2 Then
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                        Exit Sub
                    End If
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        SC.Modificar(IdConcepto, ComboBox2.Text, ComboBox1.SelectedIndex)
                        PopUp("Modificado", 90)
                        NuevoConcepto()
                    End If
                End If
            Else
                MsgBox("Debe indicar un nombre a la forma de pago.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ClientesTiposBaja, PermisosN.Secciones.Catalagos2) = False And DeQueTipo = 0 Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ProvTiposBaja, PermisosN.Secciones.Catalagos2) = False And DeQueTipo = 1 Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.SucTiposBaja, PermisosN.Secciones.Catalagos2) = False And DeQueTipo = 2 Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If IdConcepto <> 1 Then
                If MsgBox("¿Desea Eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim SC As New dbTiposCP(MySqlcon, DeQueTipo)
                    SC.Eliminar(IdConcepto)
                    PopUp("Eliminado", 90)
                    NuevoConcepto()
                End If
            Else
                MsgBox("No se puede eliminar esa forma de pago.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        NuevoConcepto()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            If ComboBox1.SelectedIndex >= 0 Then
                IdConcepto = IdsConceptos.Valor(ComboBox2.SelectedIndex)
                Dim C2 As New dbTiposCP(IdConcepto, MySqlcon, DeQueTipo)
                Button6.Enabled = True
                Button5.Text = "Modificar"
                ComboBox1.SelectedIndex = C2.Tipo
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
End Class