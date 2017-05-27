Public Class frmAddendaLey
    Public strXML As String
    Dim IdVenta As Integer
    Dim Addenda As dbAddendaLey
    Dim Descuento As Double
    Dim Eselectronica As Byte
    Dim Fecha As String
    Dim Email As String
    Private Sub frmAddendaLey_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            ComboBox1.Items.Add("Mercaderias")
            ComboBox1.Items.Add("Activos Fijos")
            ComboBox1.Items.Add("Servicios")
            ComboBox1.SelectedIndex = 0
            Addenda = New dbAddendaLey(MySqlcon)
            Addenda.LlenaDatos(IdVenta)
            DateTimePicker2.Value = Fecha
            If Addenda.Idaddenda <> 0 Then
                ComboBox1.SelectedIndex = Addenda.Clasificacion
                ComboBox2.SelectedIndex = Addenda.Tipo
                TextBox2.Text = Addenda.Proveedor
                TextBox1.Text = Addenda.Centro
                TextBox3.Text = Addenda.NumeroEntrada
                DateTimePicker2.Value = CDate(Addenda.FechaEntrada)
                TextBox5.Text = Addenda.NoRemision
                TextBox6.Text = Addenda.Pedido
                TextBox4.Text = Addenda.ProveededorSap
                TextBox7.Text = Addenda.NombreProveedorSap
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Public Sub New(ByVal pIdVenta As Integer, ByVal pDescuento As Double, ByVal pEsElectronica As Byte, ByVal pFecha As String, ByVal pEmail As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdVenta = pIdVenta
        Descuento = pDescuento
        Fecha = pFecha
        Eselectronica = pEsElectronica
        Email = pEmail
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("Pedido directo a tienda.")
            ComboBox2.Items.Add("Pedido centralizado.")
            ComboBox2.Items.Add("Pedido abierto.")
            ComboBox2.Items.Add("Recepción manual de perecederos.")
            ComboBox2.Items.Add("Centro de distribución.")
            ComboBox2.SelectedIndex = 0
            TextBox1.Enabled = True
            Label12.Visible = True
            TextBox2.Enabled = True
            Label11.Visible = True
            TextBox3.Enabled = True
            Label13.Visible = True
            TextBox4.Enabled = True
            Label16.Visible = True
            TextBox5.Enabled = True
            Label14.Visible = False
            TextBox6.Enabled = False
            Label15.Visible = False
            TextBox7.Enabled = False
            Label17.Visible = False
            DateTimePicker2.Enabled = True

        End If
        If ComboBox1.SelectedIndex = 1 Then
            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("Pedido Sistema")
            ComboBox2.Items.Add("Pedido Manual")
            ComboBox2.SelectedIndex = 0


        End If
        If ComboBox1.SelectedIndex = 2 Then
            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("Pedido Sistema")
            ComboBox2.Items.Add("Pedido Manual")
            ComboBox2.SelectedIndex = 0
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox1.SelectedIndex >= 1 And ComboBox2.SelectedIndex = 0 Then
            TextBox1.Enabled = True
            Label12.Visible = True
            TextBox2.Enabled = False
            Label11.Visible = False
            TextBox3.Enabled = False
            Label13.Visible = False
            TextBox4.Enabled = True
            Label16.Visible = True
            TextBox5.Enabled = False
            Label14.Visible = False
            TextBox6.Enabled = True
            Label15.Visible = True
            TextBox7.Enabled = True
            Label17.Visible = False
            DateTimePicker2.Enabled = False
        End If
        If ComboBox1.SelectedIndex >= 1 And ComboBox2.SelectedIndex = 0 Then
            TextBox1.Enabled = True
            Label12.Visible = True
            TextBox2.Enabled = False
            Label11.Visible = False
            TextBox3.Enabled = False
            Label13.Visible = False
            TextBox4.Enabled = True
            Label16.Visible = True
            TextBox5.Enabled = True
            Label14.Visible = True
            TextBox6.Enabled = False
            Label15.Visible = False
            TextBox7.Enabled = True
            Label17.Visible = True
            DateTimePicker2.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            If ComboBox1.SelectedIndex = 0 Then
                If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                    MsgBox("Debe llenar todos los datos que tienen un asterisco.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
            End If

            If Addenda.Idaddenda = 0 Then
                Addenda.Guardar(IdVenta, ComboBox1.SelectedIndex, ComboBox2.SelectedIndex, TextBox2.Text, TextBox1.Text, TextBox3.Text, TextBox5.Text, TextBox4.Text, TextBox7.Text, Format(DateTimePicker2.Value, "yyyy-MM-dd"), Descuento, TextBox6.Text)
            Else
                Addenda.Modificar(IdVenta, ComboBox1.SelectedIndex, ComboBox2.SelectedIndex, TextBox2.Text, TextBox1.Text, TextBox3.Text, TextBox5.Text, TextBox4.Text, TextBox7.Text, Format(DateTimePicker2.Value, "yyyy-MM-dd"), Descuento, TextBox6.Text)
            End If

            strXML = Addenda.CreaXML(IdVenta, Eselectronica, Email)
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        strXML = ""
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class