Public Class frmRestauranteCambioMesa
    Private idSucursal As Integer
    Private idVenta As Integer
    Private idSeccion As Integer

    Public Sub New(ByVal pIdVenta As Integer, pIdSucursal As Integer)
        InitializeComponent()
        idVenta = pIdVenta
        idSucursal = pIdSucursal
    End Sub
    Private Sub frmRestauranteCambioMesa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
            Dim configuracion As New dbRestauranteConfiguracion(MySqlcon)
            Me.BackColor = Color.FromArgb(configuracion.colorVentanas)
        Catch ex As Exception

        End Try

        Dim secciones As New dbRestauranteSecciones(MySqlcon)
        For Each r As RadioButton In secciones.ListaSecciones(GlobalIdSucursalDefault)
            AddHandler r.CheckedChanged, AddressOf rdbSeccion_CheckedChanged
            pnlSecciones.Controls.Add(r)
        Next
        If pnlSecciones.Controls.Count > 0 Then
            DirectCast(pnlSecciones.Controls(0), RadioButton).Checked = True
        End If
    End Sub

    Private Sub rdbSeccion_CheckedChanged(sender As Object, e As EventArgs)
        If DirectCast(sender, RadioButton).Checked Then
            Try
                Dim secciones As New dbRestauranteSecciones(MySqlcon)
                pnlMesasFijo.BackgroundImage = Image.FromFile(secciones.rutaMapa)
            Catch

            End Try
            Dim mesasController As New dbRestauranteMesas(MySqlcon, idSucursal)

            pnlMesasFijo.Controls.Clear()
            For Each m As RestauranteMesa In mesasController.Mesas(sender.Id, -1)
                AddHandler m.Click, AddressOf RestauranteMesa_Click
                m.Enabled = m.Estado = EstadosMesas.Libre
                pnlMesasFijo.Controls.Add(m)
            Next
        End If
    End Sub

    Private Sub RestauranteMesa_Click(sender As Object, e As EventArgs)
        If MsgBox("¿Cambiar a esta mesa?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Dim ventas As New dbRestauranteVentas(MySqlcon)
            ventas.CambiarMesa(idVenta, sender.Id)
            DialogResult = Windows.Forms.DialogResult.OK
            Close()
        End If
    End Sub
End Class