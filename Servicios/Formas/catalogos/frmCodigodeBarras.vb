Public Class frmCodigodeBarras
    Dim B As New dbCodigodeBarras(MySqlcon)
    Dim ID As Integer
    Dim upc As UPCA
    Public Codigo As String
    Dim tipoProducto As Integer
    Dim Ean13 As dbEAN13
    Dim imgFinal As Image
    Dim IDInventario As Integer
    Public CodCom As String = ""

    Public Sub New(ByVal pIDInv As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IDInventario = pIDInv

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Buscar()

        B.buscar(IDInventario)
        If B.ID <> 0 Then
            ID = B.ID
            llenarDatos()
            generarImagen()

        Else
            nuevo()
        End If


    End Sub
    Private Sub frmCodigodeBarras_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        cmbEscala.SelectedIndex = 5
        cmbTipo.SelectedIndex = 0
        nuevo()
        cmbCodigoPais.SelectedIndex = 0
        cmbTipoCodigo.SelectedIndex = 0
        Buscar()
    End Sub

    Private Sub btnGenerar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        generarImagen()

    End Sub
    Private Sub generarImagen()
        If cmbTipoCodigo.SelectedIndex = 3 Then
            If txtCodigo.Text.Length() <> txtCodigo.MaxLength() Then
                MsgBox("Se requieren " + txtCodigo.MaxLength().ToString + " caracteres.", MsgBoxStyle.OkOnly, "Pull System Soft")
            Else

                If cmbTipoCodigo.SelectedIndex = 0 Or cmbTipoCodigo.SelectedIndex = 1 Then
                    Try
                        imgFinal = PrintEANBarCode(txtCodigo.Text, Me.PictureBox1, 10, 10, Me.PictureBox1.Width - 20, Me.PictureBox1.Height - 20)

                    Catch
                        MsgBox(Err.Description, MsgBoxStyle.Exclamation, Application.ProductName)
                    End Try
                    btnGaurdarImagen.Enabled = True
                    btnGuardarCodigo.Enabled = True
                    btnImprimirCodigo.Enabled = True
                End If
            End If
        Else
            'CODIGO PARA UPC-A
            If cmbTipoCodigo.SelectedIndex = 1 Then
                If txtManufactura.Text <> "" And txtProducto.Text <> "" Then
                    txtManufactura.BackColor = Color.White
                    txtProducto.BackColor = Color.White
                    Dim g As System.Drawing.Graphics = PictureBox1.CreateGraphics()

                    g.FillRectangle(New System.Drawing.SolidBrush(Color.White), New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height))

                    CreateUPC()
                    upc.Scale = Double.Parse(Convert.ToDecimal(cmbEscala.Items(cmbEscala.SelectedIndex)))
                    imgFinal = upc.DrawUpcaBarcode(g, New System.Drawing.Point(0, 0), PictureBox1)
                    txtCheckSum.Text = upc.ChecksumDigit
                    g.Dispose()
                    Codigo = upc.ProductType + upc.ManufacturerCode + upc.ProductCode + upc.ChecksumDigit
                    Try
                        ' Call PrintEANBarCode(Codigo, Me.PictureBox1, 10, 10, Me.PictureBox1.Width - 20, Me.PictureBox1.Height - 20)

                    Catch
                        MsgBox(Err.Description, MsgBoxStyle.Exclamation, Application.ProductName)
                    End Try
                    btnGaurdarImagen.Enabled = True
                    btnGuardarCodigo.Enabled = True
                    btnImprimirCodigo.Enabled = True
                Else
                    MsgBox("Faltan campos por llenar", MsgBoxStyle.OkOnly, "Pull System Soft")
                    If txtManufactura.Text = "" Then
                        txtManufactura.BackColor = Color.Coral
                    End If
                    If txtProducto.Text = "" Then
                        txtProducto.BackColor = Color.Coral
                    End If
                End If


            Else
                If txtManufactura.Text <> "" And txtProducto.Text <> "" And txtCodigoPais.Text <> "" And txtProducto.Text.Length = 5 And txtManufactura.Text.Length >= 4 Then
                    txtManufactura.BackColor = Color.White
                    txtProducto.BackColor = Color.White
                    txtCodigoPais.BackColor = Color.White

                    Dim g As System.Drawing.Graphics = PictureBox1.CreateGraphics()
                    g.FillRectangle(New System.Drawing.SolidBrush(Color.White), New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height))
                    CreateEan13()
                    Ean13.Scale = Double.Parse(Convert.ToDecimal(cmbEscala.Items(cmbEscala.SelectedIndex)))
                    imgFinal = Ean13.DrawEan13Barcode(g, New System.Drawing.Point(0, 0), PictureBox1)
                    txtCheckSum.Text = Ean13.ChecksumDigit
                    g.Dispose()
                    btnGaurdarImagen.Enabled = True
                    btnGuardarCodigo.Enabled = True
                    btnImprimirCodigo.Enabled = True
                Else
                    MsgBox("Faltan campos por llenar o caractéres en los campos.", MsgBoxStyle.OkOnly, "Pull System Soft")
                    If txtManufactura.Text = "" Then
                        txtManufactura.BackColor = Color.Coral
                    End If
                    If txtProducto.Text = "" Then
                        txtProducto.BackColor = Color.Coral
                    End If
                    If txtCodigoPais.Text = "" Then
                        txtCodigoPais.BackColor = Color.Coral
                    End If
                    If txtManufactura.Text.Length < 4 Then
                        txtManufactura.BackColor = Color.Coral
                    End If
                    If txtProducto.Text.Length <> 5 Then
                        txtProducto.BackColor = Color.Coral
                    End If
                End If

            End If

        End If

    End Sub


    Private Sub cmbTipoCodigo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipoCodigo.SelectedIndexChanged
        'If cmbTipoCodigo.SelectedIndex = 0 Then
        '    'EAN8
        '    txtCodigo.MaxLength = 8
        '    pnlUPCA.Visible = False
        '    pnlEAN13.Visible = False
        '    btnGenerar.Location = New Point(198, 102)
        '    Label3.Location = New Point(114, 78)
        '    txtNombre.Location = New Point(177, 76)
        '    PictureBox1.Location = New Point(109, 131)
        '    PictureBox1.Size = New Size(254, 119)
        '    btnGuardarCodigo.Location = New Point(90, 256)
        '    btnGaurdarImagen.Location = New Point(169, 256)
        '    btnImprimirCodigo.Location = New Point(246, 256)
        '    btnNuevo.Location = New Point(325, 256)
        'End If
        If cmbTipoCodigo.SelectedIndex = 0 Then
            'EAN13
            txtCodigo.MaxLength = 13
            pnlUPCA.Visible = True
            pnlEAN13.Visible = True
            cmbTipo.Visible = False
            Label4.Visible = False
            'btnGenerar.Location = New Point(198, 196)
            ' Label3.Location = New Point(114, 174)
            ' txtNombre.Location = New Point(177, 172)
            '  PictureBox1.Location = New Point(90, 226)
            ' PictureBox1.Size = New Size(302, 219)
            '  PictureBox1.Size = New Size(291, 189)
            'btnGuardarCodigo.Location = New Point(90, 451)
            'btnGaurdarImagen.Location = New Point(169, 451)
            'btnImprimirCodigo.Location = New Point(246, 451)
            'btnNuevo.Location = New Point(325, 451)
        End If
        If cmbTipoCodigo.SelectedIndex = 1 Then
            pnlUPCA.Visible = True
            pnlEAN13.Visible = False
            cmbTipo.Visible = True
            Label4.Visible = True
            '  btnGenerar.Location = New Point(198, 196)
            'Label3.Location = New Point(114, 174)
            'txtNombre.Location = New Point(177, 172)
            '  PictureBox1.Location = New Point(90, 226)
            'PictureBox1.Size = New Size(291, 189)

            'btnGuardarCodigo.Location = New Point(90, 451)
            'btnGaurdarImagen.Location = New Point(169, 451)
            'btnImprimirCodigo.Location = New Point(246, 451)
            'btnNuevo.Location = New Point(325, 451)
        End If

    End Sub

    Private Sub btnGaurdarImagen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGaurdarImagen.Click
       
        If PictureBox1.Image IsNot Nothing Then
            Dim saveImage As New SaveFileDialog 'Este es el SaveFileDialog
            Dim ruta As String = "" 'Para tener la ruta de la imagen

            saveImage.Title = "Guardar imagen como..." 'Título de la ventana
            saveImage.Filter = "Imagen BMP (*.bmp)|*.bmp|Imagen JPG (*.jpg)|*.jpg|Imagen PNG (*.png)|*.png" 'Los formatos en que se guardará la imagen
            If saveImage.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'Recuperar la ruta de la imagen si no está vacía
                If Not String.IsNullOrEmpty(saveImage.FileName) Then ruta = saveImage.FileName
                Dim mimag As Graphics = Graphics.FromImage(PictureBox1.Image) 'Objeto Image para guardar la imagen del Picture
                Dim extension As String = ruta.Substring(ruta.Length - 3, 3) 'Recuperar los ultimos 3 caracteres de la extensión
                Dim myImg As Image
                myImg = PictureBox1.Image 'Guardar la imagen del PictureBox en el objeto Image

                'ESTO SOLO FUNCIONA EN VISUAL BASIC 2008
                Select Case extension
                    Case "bmp"
                        PictureBox1.Image.Save(ruta, System.Drawing.Imaging.ImageFormat.Bmp)
                        'myImg.Save(ruta, Imaging.ImageFormat.Bmp) 'Guardar en formato BMP
                    Case "jpg"
                        myImg.Save(ruta, Imaging.ImageFormat.Jpeg) 'Guardar en formato JPG
                    Case "png"
                        myImg.Save(ruta, Imaging.ImageFormat.Png) 'Guardar en formato PNG
                End Select
                PopUp("Imagen guardada", 90)
            End If

        End If

    End Sub

    Private Sub btnGuardarCodigo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardarCodigo.Click
        'If cmbTipoCodigo.SelectedIndex <> 0 Then

        txtCodigoPais.BackColor = Color.White
        txtManufactura.BackColor = Color.White
        txtProducto.BackColor = Color.White
        'txtNombre.BackColor = Color.White
        If txtManufactura.Text <> "" And txtProducto.Text <> "" Then
            If txtCodigoPais.Text <> "" Then
                'EAN13
                If btnGuardarCodigo.Text = "Guardar Código" Then

                    If cmbTipoCodigo.SelectedIndex = 0 Then
                        Codigo = txtCodigoPais.Text + txtManufactura.Text + txtProducto.Text + txtCheckSum.Text
                    Else
                        Codigo = tipoProducto.ToString + txtManufactura.Text + txtProducto.Text + txtCheckSum.Text
                    End If


                    B.Guardar("", Codigo, cmbTipoCodigo.SelectedIndex, IDInventario)
                    PopUp("Guardado", 90)
                    nuevo()
                    Dim I As New dbInventario(MySqlcon)
                    I.ModificarCodigo2(IDInventario, Codigo)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    Dim Codigo As String
                    If cmbTipoCodigo.SelectedIndex = 0 Then
                        Codigo = txtCodigoPais.Text + txtManufactura.Text + txtProducto.Text + txtCheckSum.Text
                    Else
                        Codigo = tipoProducto.ToString + txtManufactura.Text + txtProducto.Text + txtCheckSum.Text
                    End If
                    B.Modificar(ID, "", Codigo, cmbTipoCodigo.SelectedIndex)
                    PopUp("Modificado", 90)
                    nuevo()
                    Dim I As New dbInventario(MySqlcon)
                    I.ModificarCodigo2(IDInventario, Codigo)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                End If




            Else
                txtCodigoPais.BackColor = Color.Coral
                MsgBox("Faltan campos por llenar.", MsgBoxStyle.OkOnly, "Pull System Soft")
            End If



        Else
            txtManufactura.BackColor = Color.Coral
            txtProducto.BackColor = Color.Coral
            'txtNombre.BackColor = Color.Coral
            MsgBox("Faltan campos por llenar.", MsgBoxStyle.OkOnly, "Pull System Soft")
        End If

        'Else
        ''EAN
        'If txtCodigo.Text <> "" And txtNombre.Text <> "" Then
        '    If btnGuardarCodigo.Text = "Guardar Código" Then
        '        B.Guardar(txtNombre.Text, txtCodigo.Text, cmbTipoCodigo.SelectedIndex, IDInventario)
        '        PopUp("Guardado", 90)
        '        nuevo()
        '        Me.DialogResult = Windows.Forms.DialogResult.OK
        '    Else
        '        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '            'modificar

        '            B.Modificar(ID, txtNombre.Text, txtCodigo.Text, cmbTipoCodigo.SelectedIndex)
        '            PopUp("Modificado", 90)
        '            nuevo()
        '            Me.DialogResult = Windows.Forms.DialogResult.OK
        '        End If

        '    End If
        'Else
        '    MsgBox("Faltan campos por llenar.", MsgBoxStyle.OkOnly, "Pull System Soft")
        'End If
        'End If
       
    End Sub
    Private Sub nuevo()
        'txtNombre.Text = ""
        txtCodigo.Text = ""
        PictureBox1.Image = Nothing
        btnGuardarCodigo.Text = "Guardar Código"
        btnGaurdarImagen.Enabled = False
        btnGuardarCodigo.Enabled = False
        btnImprimirCodigo.Enabled = False
        btnEliminar.Visible = False
        'dgvCodigos.DataSource = B.Consulta(txtBuscarNombre.Text)
        'dgvCodigos.Columns(0).Visible = False
        'dgvCodigos.Columns(2).Visible = False
        'dgvCodigos.Columns(1).HeaderText = "Nombre"
        'dgvCodigos.Columns(3).HeaderText = "Código"
        'dgvCodigos.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        cmbCodigoPais.SelectedIndex = 0
        cmbTipo.SelectedIndex = 0
        txtCodigoPais.Text = "750"
        txtManufactura.Text = ""
        txtProducto.Text = ""
        txtCheckSum.Text = ""
        ' If cmbTipoCodigo.SelectedIndex <> 0 Then
        txtManufactura.Focus()
        ' Else
        ' txtCodigo.Focus()
        ' End If


    End Sub

    'Private Sub txtBuscarNombre_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    dgvCodigos.DataSource = B.Consulta(txtBuscarNombre.Text)
    '    dgvCodigos.Columns(0).Visible = False
    '    dgvCodigos.Columns(2).Visible = False
    '    dgvCodigos.Columns(1).HeaderText = "Nombre"
    '    dgvCodigos.Columns(3).HeaderText = "Código"
    '    dgvCodigos.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    'End Sub

    Private Sub txtCodigo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodigo.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub llenarDatos()
        B.LlenaDatos(ID)
        cmbTipoCodigo.SelectedIndex = B.tipo
        If cmbTipoCodigo.SelectedIndex = 1 Then
            pnlUPCA.Visible = True
            tipoProducto = Integer.Parse(B.Codigo.Chars(0).ToString)
            If tipoProducto = 0 Then
                cmbTipo.SelectedIndex = 0
            End If
            If tipoProducto = 2 Then
                cmbTipo.SelectedIndex = 1
            End If
            If tipoProducto = 3 Then
                cmbTipo.SelectedIndex = 2
            End If
            If tipoProducto = 4 Then
                cmbTipo.SelectedIndex = 3
            End If
            If tipoProducto = 5 Then
                cmbTipo.SelectedIndex = 4
            End If
            If tipoProducto = 7 Then
                cmbTipo.SelectedIndex = 5
            End If
            txtManufactura.Text = B.Codigo.Chars(1) + B.Codigo.Chars(2) + B.Codigo.Chars(3) + B.Codigo.Chars(4) + B.Codigo.Chars(5)
            txtProducto.Text = B.Codigo.Chars(6) + B.Codigo.Chars(7) + B.Codigo.Chars(8) + B.Codigo.Chars(9) + B.Codigo.Chars(10)
            txtCheckSum.Text = B.Codigo.Chars(11)
        Else
            If cmbTipoCodigo.SelectedIndex = 0 Then
                Dim cara As Integer = B.Codigo.Length - 11
                Dim cadena As String = ""
                ' txtCodigoPais.Text = B.Codigo.Chars(1) + B.Codigo.Chars(1) + B.Codigo.Chars(1)
                txtCheckSum.Text = B.Codigo.Chars(B.Codigo.Length - 1)
                txtManufactura.Text = B.Codigo.Chars(0 + cara) + B.Codigo.Chars(1 + cara) + B.Codigo.Chars(2 + cara) + B.Codigo.Chars(3 + cara) + B.Codigo.Chars(4 + cara)
                txtProducto.Text = B.Codigo.Chars(5 + cara) + B.Codigo.Chars(6 + cara) + B.Codigo.Chars(7 + cara) + B.Codigo.Chars(8 + cara) + B.Codigo.Chars(9 + cara)

                For i As Integer = 0 To cara - 1
                    cadena = cadena + B.Codigo.Chars(i)
                Next
                txtCodigoPais.Text = cadena
                'Else
                '    txtCodigo.Text = B.Codigo
            End If


        End If
        ' txtNombre.Text = B.Nombre
        btnGuardarCodigo.Text = "Modificar Código"
        btnGaurdarImagen.Enabled = True
        btnGuardarCodigo.Enabled = True
        btnImprimirCodigo.Enabled = True
        btnEliminar.Visible = True
    End Sub

    'Private Sub dgvCodigos_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    Try
    '        ID = dgvCodigos.Item(0, dgvCodigos.CurrentCell.RowIndex).Value
    '        llenarDatos()
    '        generarImagen()
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try

    'End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try

            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                B.Eliminar(ID)
                PopUp("Eliminado", 90)
                nuevo()
                Codigo = ""
                Dim I As New dbInventario(MySqlcon)
                I.ModificarCodigo2(IDInventario, Codigo)
                Me.DialogResult = Windows.Forms.DialogResult.OK

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevo()
    End Sub

    Private Sub btnImprimirCodigo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimirCodigo.Click
        If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings

            PrintDocument1.Print()
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        If cmbTipoCodigo.SelectedIndex = 0 Then
            e.Graphics.DrawImage(imgFinal, 0, 0)

        Else
            e.Graphics.DrawImage(imgFinal, 0, 0)
         
            
        End If
       
    End Sub

			
#Region "UPCA"
    Private Sub CreateUPC()
        upc = New UPCA()
        upc.ProductType = tipoProducto.ToString()

        upc.ManufacturerCode = txtManufactura.Text
        upc.ProductCode = txtProducto.Text
        If (txtCheckSum.Text.Length > 0) Then
            upc.ChecksumDigit = txtCheckSum.Text
        End If

    End Sub

#End Region

    Private Sub cmbTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged
        If cmbTipo.SelectedIndex = 0 Then
            tipoProducto = 0
        End If
        If cmbTipo.SelectedIndex = 1 Then
            tipoProducto = 2
        End If
        If cmbTipo.SelectedIndex = 2 Then
            tipoProducto = 3
        End If
        If cmbTipo.SelectedIndex = 3 Then
            tipoProducto = 4
        End If
        If cmbTipo.SelectedIndex = 4 Then
            tipoProducto = 5
        End If
        If cmbTipo.SelectedIndex = 5 Then
            tipoProducto = 7
        End If

    End Sub

    Private Sub txtManufactura_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtProducto_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub



    Private Sub txtCodigoPais_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbCodigoPais_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCodigoPais.SelectedIndexChanged
        If cmbCodigoPais.SelectedIndex = 0 Then
            txtCodigoPais.Text = "750"
        End If
        If cmbCodigoPais.SelectedIndex = 1 Then
            txtCodigoPais.Text = "00"
        End If
        If cmbCodigoPais.SelectedIndex = 2 Then
            txtCodigoPais.Text = ""
            txtCodigo.Focus()
        End If
    End Sub
    Private Sub CreateEan13()
        Ean13 = New dbEAN13()
        Ean13.CountryCode = txtCodigoPais.Text
        Ean13.ManufacturerCode = txtManufactura.Text
        Ean13.ProductCode = txtProducto.Text
        If txtCheckSum.Text.Length > 0 Then
            Ean13.ChecksumDigit = txtCheckSum.Text
        End If
    End Sub

    Private Sub txtCodigoPais_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodigoPais.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtManufactura_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtManufactura.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtProducto_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProducto.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub
End Class