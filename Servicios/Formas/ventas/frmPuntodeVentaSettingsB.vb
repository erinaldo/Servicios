Public Class frmPuntodeVentaSettingsB

    Public IdSucursal As Integer
    Public IdCliente As Integer
    Public IdCaja As Integer
    Public IdVendedor As Integer
    Dim IdsSucursales As New elemento
    Dim IdsCajas As New elemento
    Dim IdsVendedores As New elemento
    Public NombreSucursal As String
    Public NombreVendedor As String
    Public NombreCaja As String
    Public NombreCliente As String
    Public IdAlmacen As Integer
    Public idListaPrecio As Integer
    Public IdConcepto As Integer
    Public Sub New(ByVal pIdsucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdCaja As Integer, ByVal pIdVendedor As Integer, ByVal pIdconcepto As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdSucursal = pIdsucursal
        IdCaja = pIdCaja
        IdVendedor = pIdVendedor
        IdCliente = pIdCliente
        IdConcepto = pIdconcepto
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmPuntodeVentaSettingsB_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        IdSucursal = IdsSucursales.Valor(ComboBox6.SelectedIndex)
        IdCaja = IdsCajas.Valor(ComboBox1.SelectedIndex)
        IdVendedor = IdsVendedores.Valor(ComboBox2.SelectedIndex)
        NombreCaja = ComboBox1.Text
        NombreSucursal = ComboBox6.Text
        NombreVendedor = ComboBox2.Text
        NombreCliente = TextBox88.Text
    End Sub

    Private Sub frmPuntodeVentaSettingsB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            LlenaCombos("tblsucursales", ComboBox6, "nombre", "nombret", "idsucursal", IdsSucursales)
            LlenaCombos("tblvendedores", ComboBox2, "nombre", "nombret", "idvendedor", IdsVendedores)
            ComboBox6.SelectedIndex = IdsSucursales.Busca(IdSucursal)
            ComboBox2.SelectedIndex = IdsVendedores.Busca(IdVendedor)
            ComboBox1.SelectedIndex = IdsCajas.Busca(IdCaja)
            Dim C As New dbClientes(IdCliente, MySqlcon)
            TextBox88.Text = C.Nombre
            idListaPrecio = C.IdLista
            Label5.Text = "Usuario Actual: " + GlobalUsuario
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CambiarSucursal, PermisosN.Secciones.PuntodeVenta) = False Then
                ComboBox6.Enabled = False
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CambioVendedor, PermisosN.Secciones.PuntodeVenta) = False Then
                ComboBox2.Enabled = False
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CambioCaja, PermisosN.Secciones.PuntodeVenta) = False Then
                ComboBox1.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub ComboBox6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox6.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox1.Focus()
        End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        LlenaCombos("tblcajas", ComboBox1, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(ComboBox6.SelectedIndex).ToString)
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox6.SelectedIndex), MySqlcon)
        IdAlmacen = S.idAlmacen
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            TextBox88.Text = B.Cliente.Nombre
            IdCliente = B.Cliente.ID
            idListaPrecio = B.Cliente.IdLista
            If B.Cliente.Credito <> 0 Or B.Cliente.CreditoDias <> 0 Then
                IdConcepto = 3
            Else
                IdConcepto = 1
            End If
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonUsuario()
    End Sub
    Private Sub BotonUsuario()
        Try
            Dim U As New dbUsuarios(MySqlcon)
            If U.ChecaLogin(TextBox1.Text, TextBox2.Text) Then
                GlobalIdUsuario = U.ID
                GlobalTipoUsuario = U.Tipo
                GlobalPermisos.PermisosCatalogos = U.PermisosCatalogos
                GlobalPermisos.PermisosCatalogos2 = U.PermisosCatalogos2
                GlobalPermisos.PermisosCompras = U.PermisosCompras
                GlobalPermisos.PermisosHerramientas = U.PermisosHerramientas
                GlobalPermisos.PermisosPuntodeVenta = U.PermisosPuntodeVenta
                GlobalPermisos.PermisosVentas = U.PermisosVentas
                GlobalPermisos.PermisosInventario = U.PermisosInventario
                GlobalUsuario = U.Nombre
                Label5.Text = "Usuario Actual: " + GlobalUsuario
                ComboBox2.SelectedIndex = IdsVendedores.Busca(U.IdVendedor)
                PopUp("Cambio de usuario exitoso", 70)
                Button1.Focus()
            Else
                'Label5.Text = "Nombre de usuario o contraseña incorrectos."
                MsgBox("Nombre de usuario o contraseña incorrectos.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub TextBox88_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox88.TextChanged

    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonUsuario()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox2.Focus()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub ComboBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button3.Focus()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class