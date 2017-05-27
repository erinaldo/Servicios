Public Class frmDescuentosConsulta
    Dim P As New dbDescuentos(MySqlcon)
    Dim O As dbOpciones
    'Dim Opc As dbOpcionesOc
    Dim Tipo As Byte
    Private Sub frmDescuentosConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try

        O = New dbOpciones(MySqlcon)
        'Opc = New dbOpcionesOc(MySqlcon)
        'Opc.LlenaDatos(Tipo, )
        ' TipoRedondeo = O.TipoRedondeo
        Me.BackColor = Color.FromArgb(O.PVRojo, O.PVVerde, O.PVAzul)
        cargarDatos()
    End Sub
    Private Sub cargarDatos()
        Dim tabla As DataTable
        Dim aux As Double
        Dim aux2 As String
        tabla = P.consultaDescuentos(Format(Date.Now, "yyyy/MM/dd HH:mm:ss"), txtProducto.Text())
        For i As Integer = 0 To tabla.Rows.Count() - 1
            If tabla.Rows(i)(2).ToString() = "Porcentaje" Then
                aux = Double.Parse(tabla.Rows(i)(3).ToString())
                aux2 = "-" + aux.ToString() + "%"
                tabla.Rows(i)(3) = aux2
            End If
            If tabla.Rows(i)(2).ToString() = "Efectivo" Then
                aux = Double.Parse(tabla.Rows(i)(3).ToString())
                aux2 = "-$" + aux.ToString()
                tabla.Rows(i)(3) = aux2
            End If
        Next
        DataGridView1.DataSource = tabla
        DataGridView1.Columns(0).HeaderText = "id"
        DataGridView1.Columns(1).HeaderText = "Producto"
        DataGridView1.Columns(2).HeaderText = "Tipo Descuento"
        DataGridView1.Columns(3).HeaderText = "Descuento"
        DataGridView1.Columns(4).HeaderText = "Promoción"
        DataGridView1.Columns(5).HeaderText = "Fecha inicial"
        DataGridView1.Columns(6).HeaderText = "fecha final"
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

    End Sub
    

    Public Function fechaFormato() As String
        Dim fechita As Date = Date.Now()
        fechita = fechita.ToString("dd/MM/yyyy")
        Return fechita
    End Function

    Private Sub txtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducto.TextChanged
        cargarDatos()
    End Sub
End Class