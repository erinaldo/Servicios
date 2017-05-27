Public Class frmAddendaFemsa
    Dim Tipo As Byte
    Dim Moneda As String
    Dim Estado As Byte
    Dim IdVenta As Integer
    Dim Eslectronica As Byte
    Dim Afuezas As Boolean = False
    Public XMLAdenda As String
    Dim Email As String
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub
    Public Sub New(ByVal PTipo As Byte, ByVal pMoneda As String, ByVal pidVenta As Integer, ByVal pEstado As Byte, ByVal pEselectronica As Byte, ByVal pEmail As String, ByVal pAfuerzas As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        '0 Factura 
        '1 Nota de Cargo
        '2 Nota de Crédito
        Tipo = PTipo
        Moneda = pMoneda
        Estado = pEstado
        IdVenta = pidVenta
        Eslectronica = pEselectronica
        Email = pEmail
        Afuezas = pAfuerzas
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmAddendaFemsa_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Afuezas Then
            MsgBox("Solo puede cerrar esta ventana dando click en guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            e.Cancel = True
        End If
    End Sub
    Private Sub frmAddendaFemsa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try

        ComboBox1.Items.Add("1.- Factura")
        ComboBox1.Items.Add("2.- Consignación")
        ComboBox1.Items.Add("3.- Retención")
        ComboBox1.SelectedIndex = 0

        If Tipo = 1 Or Tipo = 2 Then
            ComboBox1.Visible = False
            Label1.Visible = False
        End If


        ComboBox2.Items.Add("0051")
        ComboBox2.Items.Add("0055")
        ComboBox2.Items.Add("0059")
        ComboBox2.Items.Add("0065")
        ComboBox2.Items.Add("0066")
        ComboBox2.Items.Add("0067")
        ComboBox2.Items.Add("0068")
        ComboBox2.Items.Add("0069")
        ComboBox2.Items.Add("0072")
        ComboBox2.Items.Add("0083")
        ComboBox2.Items.Add("0085")
        ComboBox2.Items.Add("0086")
        ComboBox2.Items.Add("0087")
        ComboBox2.Items.Add("0090")
        ComboBox2.Items.Add("0117")
        ComboBox2.Items.Add("0169")
        ComboBox2.Items.Add("0174")
        ComboBox2.Items.Add("0188")
        ComboBox2.Items.Add("0189")
        ComboBox2.Items.Add("0195")
        ComboBox2.Items.Add("0265")
        ComboBox2.SelectedIndex = 0

        Dim Adenda As New dbAdendasFemsa(MySqlcon)
        If Adenda.BuscaAddenda(IdVenta, Tipo) Then
            If Adenda.claveDoc < 3 Then
                ComboBox1.SelectedIndex = Adenda.claveDoc - 1
            End If
            ComboBox2.Text = Adenda.NoSociedad
            TextBox1.Text = Adenda.NoProveedor
            TextBox2.Text = Adenda.NoPedido
            TextBox3.Text = Adenda.NoEntrada
            TextBox4.Text = Adenda.NoRemision
            TextBox5.Text = Adenda.Centro
            TextBox6.Text = Adenda.Retencion1
            TextBox7.Text = Adenda.Retencion2
            If Adenda.IniPerLiq <> "" Then DateTimePicker1.Value = Adenda.IniPerLiq
            If Adenda.FinPerLiq <> "" Then DateTimePicker2.Value = Adenda.FinPerLiq
        End If
        If Afuezas Then
            Button2.Visible = False
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 1 Then
            TextBox5.Enabled = True
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        Else
            TextBox5.Enabled = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        End If
        If ComboBox1.SelectedIndex = 3 Then
            TextBox6.Enabled = True
            TextBox7.Enabled = True
        Else
            TextBox6.Enabled = False
            TextBox7.Enabled = False
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Select Case ComboBox2.SelectedIndex
            Case 0
                Label12.Text = "Cervecería Cuauhtémoc Moctezuma S.A. de C.V."
            Case 1
                Label12.Text = "Sistema Ambiental Industral S.A. de C.V."
            Case 2
                Label12.Text = "Bienes Raíces CERMOC S.A. de C.V."
            Case 3
                Label12.Text = "Carta Blanca de Occidente S.A. de C.V."
            Case 4
                Label12.Text = "Servicios Industriales y Comerciales S.A. de C.V."
            Case 5
                Label12.Text = "Distribuidora de Cervezas de Sonora S.A de C.V."
            Case 6
                Label12.Text = "Comerdis del Norte S.A. de C.V."
            Case 7
                Label12.Text = "Distribuidora de Cervezas de Sinaloa S.A de C.V."
            Case 8
                Label12.Text = "Cía. Cervecera Chihuahua"
            Case 9
                Label12.Text = "Cervezas Cuauhtémoc Moctezuma S.A de C.V."
            Case 10
                Label12.Text = "Codicome Caribe S.A. de C.V."
            Case 11
                Label12.Text = "Codicome Sureste S.A. de C.V."
            Case 12
                Label12.Text = "Codicome Centro S.A. de C.V."
            Case 13
                Label12.Text = "Cervezas Cuauhtémoc Moctezuma del Norte S.A. de C.V."
            Case 14
                Label12.Text = "Distribuidora Tecate del Mar de Cortez"
            Case 15
                Label12.Text = "Control Negocios Comerciales"
            Case 16
                Label12.Text = "DCM de Tamps S.A. de C.V."
            Case 17
                Label12.Text = "Servicios de Administración de Mercados S.A de C.V."
            Case 18
                Label12.Text = "Inmobiliaria BRC en Occidente S.A. de C.V."
            Case 19
                Label12.Text = "Distrubuidora Cuauhtémoc Moctezuma en San Lius S.A. de C.V."
            Case 20
                Label12.Text = "Grupo Cuauhtémoc Moctezuma S.A de C.V."
        End Select
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Debe llenar al menos los datos de No. Proveedor, No. Pedido, No.Entrada ", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If TextBox1.Text.Length < 10 Then
                TextBox1.Text = TextBox1.Text.PadLeft(10, "0")
            End If
            Dim Adenda As New dbAdendasFemsa(MySqlcon)
            If Adenda.BuscaAddenda(IdVenta, Tipo) = False Then
                If ComboBox1.SelectedIndex = 1 Then
                    Adenda.Guardar(IdVenta, 1, ComboBox1.SelectedIndex + 1, ComboBox2.Text, TextBox1.Text, TextBox2.Text, Moneda, TextBox3.Text, TextBox4.Text, "", TextBox5.Text, Format(DateTimePicker1.Value, "yyyy.MM.dd"), Format(DateTimePicker2.Value, "yyyy.MM.dd"), TextBox6.Text, TextBox7.Text, "", "", "", Tipo)
                Else
                    Adenda.Guardar(IdVenta, 1, ComboBox1.SelectedIndex + 1, ComboBox2.Text, TextBox1.Text, TextBox2.Text, Moneda, TextBox3.Text, TextBox4.Text, "", "", "", "", TextBox6.Text, TextBox7.Text, "", "", "", Tipo)
                End If
            Else
                If ComboBox1.SelectedIndex = 1 Then
                    Adenda.Modificar(Adenda.ID, 1, ComboBox1.SelectedIndex + 1, ComboBox2.Text, TextBox1.Text, TextBox2.Text, Moneda, TextBox3.Text, TextBox4.Text, "", TextBox5.Text, Format(DateTimePicker1.Value, "yyyy.MM.dd"), Format(DateTimePicker2.Value, "yyyy.MM.dd"), TextBox6.Text, TextBox7.Text, "", "", "")
                Else
                    Adenda.Modificar(Adenda.ID, 1, ComboBox1.SelectedIndex + 1, ComboBox2.Text, TextBox1.Text, TextBox2.Text, Moneda, TextBox3.Text, TextBox4.Text, "", "", "", "", TextBox6.Text, TextBox7.Text, "", "", "")
                End If
            End If
            XMLAdenda = Adenda.CreaXMLCFDI(Eslectronica, Email)
            Afuezas = False
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class