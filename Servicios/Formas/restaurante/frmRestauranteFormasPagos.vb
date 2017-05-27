Public Class frmRestauranteFormasPagos
    Private id As Integer
    Private clave As String
    Private nombre As String
    Private FP As New dbRestauranteFormasPagos(MySqlcon)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        llenaGrid()
    End Sub
    Private Sub frmRestauranteFormasPagos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvFormasPago.Columns(0).Visible = False
    End Sub

    Private Sub llenaGrid()
        dgvFormasPago.DataSource = FP.vistaFormas()
    End Sub

    Private Sub llenaDatos(ByVal id As Integer)
        If FP.buscar(id) Then
            id = FP.id
            clave = FP.clave
            nombre = FP.nombre
            txtClave.Text = clave
            txtNombre.Text = nombre
        End If
    End Sub

    Private Sub Nuevo()
        id = -1
        clave = ""
        nombre = ""
        txtClave.Text = clave
        txtNombre.Text = nombre
        btnEliminar.Enabled = False
        btnGuardar.Text = "Guardar"
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        FP.eliminar(id)
        Nuevo()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Nuevo()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If id < 0 Then
            FP.agregar(nombre, clave)
            PopUp("Guardado", 30)
            Nuevo()
        Else
            FP.modificar(id, nombre, clave)
            PopUp("Modificado", 30)
            Nuevo()
        End If
    End Sub
End Class