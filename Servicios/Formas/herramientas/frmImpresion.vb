Public Class frmImpresion
    Dim Mueve As Boolean
    Dim MouseX As Integer
    Dim MouseY As Integer
    Dim Fuente As Font = New Font("Arial", 10, FontStyle.Regular)
    Dim FuenteChica As Font = New Font("Arial", 7, FontStyle.Regular)
    Dim Nodos As New Collection
    Dim ConsultaOn As Boolean = False
    Dim TipoDocumento As Integer
    Dim idSucursal As Integer
    Dim IdsSucursales As New elemento
    Dim dbImp As dbImpresionesN
    Dim Fondo As Bitmap
    Dim SeMovio As Boolean = False
    Dim PlumaDetalles As New Pen(Color.Aquamarine, 3)
    Dim Rectangulo As New Rectangle
    Dim Rectangulo2 As New Rectangle
    Dim Flujo01 As Bitmap
    Dim Flujo02 As Bitmap
    Dim Fijo01 As Bitmap
    Dim Fijo02 As Bitmap

    Private Sub frmImpresion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If TextBox8.Enabled = False Then
            If e.KeyCode = Keys.T Then
                SplitContainer1.Panel1.Enabled = True
            End If
            If e.KeyCode = Keys.G Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas) = True Then
                    GuardaNodos()
                    PopUp("Guardado", 90)
                Else
                    PopUp("No tiene permiso para realizar esta operación.", 90)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
            If e.KeyCode = Keys.Up Then
                For Each n As NodoImpresionN In Nodos
                    If n.Seleccionado Then
                        n.Y -= 1
                        ConsultaOn = False
                        TextBox3.Text = n.Y.ToString
                        ConsultaOn = True
                        Exit For
                    End If
                Next
                PanelDoubleBuffer1.Refresh()
            End If
            If e.KeyCode = Keys.Down Then
                For Each n As NodoImpresionN In Nodos
                    If n.Seleccionado Then
                        n.Y += 1
                        ConsultaOn = False
                        TextBox3.Text = n.Y.ToString
                        ConsultaOn = True
                        Exit For
                    End If
                Next
                PanelDoubleBuffer1.Refresh()
            End If
            If e.KeyCode = Keys.Left Then
                For Each n As NodoImpresionN In Nodos
                    If n.Seleccionado Then
                        n.X -= 1
                        ConsultaOn = False
                        TextBox2.Text = n.X.ToString
                        ConsultaOn = True
                        Exit For
                    End If
                Next
                PanelDoubleBuffer1.Refresh()
            End If
            If e.KeyCode = Keys.Right Then
                For Each n As NodoImpresionN In Nodos
                    If n.Seleccionado Then
                        n.X += 1
                        ConsultaOn = False
                        TextBox2.Text = n.X.ToString
                        ConsultaOn = True
                        Exit For
                    End If
                Next
                PanelDoubleBuffer1.Refresh()
            End If
        End If
    End Sub

    Private Sub frmImpresion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            'GlobalIcono = New Icon(Application.StartupPath + "\iconos\iconoico.ico")
            Flujo01 = Image.FromFile(Application.StartupPath + "\iconos\flujo01.png")
            Flujo02 = Image.FromFile(Application.StartupPath + "\iconos\flujo02.png")
            Fijo01 = Image.FromFile(Application.StartupPath + "\iconos\fijo01.png")
            Fijo02 = Image.FromFile(Application.StartupPath + "\iconos\fijo02.png")
            RadioButton1.BackgroundImage = Fijo01
            RadioButton2.BackgroundImage = Flujo02
        Catch ex As Exception

        End Try


        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        'SplitContainer1.PanelDoubleBuffer1.BackgroundImageLayout = ImageLayout.Stretch
        'SplitContainer1.PanelDoubleBuffer1.BackgroundImage = ImagenFondo
        '    Public Enum TiposDocumentos
        '    Venta = 0
        '    VentaCotizacion = 1
        '    VentaPedido = 2
        '    VentaRemision = 3
        '    VentaDevolucion = 4
        '    VentaNotadeCredito = 5
        '    VentaNotadeCargo = 6
        '    Compra = 7
        '    CompraCotizacion = 8
        '    CompraPedido = 9
        '    CompraRemision = 10
        '    CompraDevolucion = 11
        '    CompraNotadeCredito = 12
        '    CompraNotadeCargo = 13
        'End Enum
        dbImp = New dbImpresionesN(MySqlcon)
        cmbDocumento.DataSource = New EnumDescriptorCollection(Of TiposDocumentos)
        'cmbDocumento.Items.Add("Ventas - Factura")
        'cmbDocumento.Items.Add("Ventas - Cotización")
        'cmbDocumento.Items.Add("Ventas - Pedido")
        'cmbDocumento.Items.Add("Ventas - Remisión")
        'cmbDocumento.Items.Add("Ventas - Devolución")
        'cmbDocumento.Items.Add("Ventas - Nota de Crédito")
        'cmbDocumento.Items.Add("Ventas - Nota de Cargo")
        'cmbDocumento.Items.Add("Compras - Factura")
        'cmbDocumento.Items.Add("Compras - Pre-orden de compra")
        'cmbDocumento.Items.Add("Compras - Orden de compra")
        'cmbDocumento.Items.Add("Compras - Remisión")
        'cmbDocumento.Items.Add("Compras - Devolución")
        'cmbDocumento.Items.Add("Compras - Nota de Crédito")
        'cmbDocumento.Items.Add("Compras - Nota de Cargo")
        'cmbDocumento.Items.Add("Movimientos de Inventario")
        'cmbDocumento.Items.Add("Formato de Retención")
        'cmbDocumento.Items.Add("Movimientos de Caja")
        'cmbDocumento.Items.Add("Apartados")
        'cmbDocumento.Items.Add("Inventario - Pedidos.")
        'cmbDocumento.Items.Add("Contabilidad - Pólizas Egresos.")
        'cmbDocumento.Items.Add("Descontinuado.")
        'cmbDocumento.Items.Add("Descontinuado.")
        'cmbDocumento.Items.Add("Nómina")
        'cmbDocumento.Items.Add("Bancos - Pagos Cheque")
        'cmbDocumento.Items.Add("Empeños")
        'cmbDocumento.Items.Add("Contabilidad - Pólizas")
        'cmbDocumento.Items.Add("Fertilizantes - Pedidos")
        'cmbDocumento.Items.Add("Fertilizantes - Movimientos")
        'cmbDocumento.Items.Add("Código de Barras")
        'cmbDocumento.Items.Add("Semillas - Boleta")
        'cmbDocumento.Items.Add("Semillas - Liquidación")
        ComboBox1.Items.Add("No")
        ComboBox1.Items.Add("Si")
        ComboBox1.SelectedIndex = 1
        ComboBox4.Items.Add("No")
        ComboBox4.Items.Add("Si")
        ComboBox4.Items.Add("Si Extra")
        ComboBox4.SelectedIndex = 1
        cmbDocumento.SelectedIndex = 0
        LlenaCombos("tblsucursales", ComboBox2, "nombre", "nombret", "idsucursal", IdsSucursales)
        ComboBox2.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        PoneImagenDB()
        PoneDatosZona()
        LlenaNodos(IdsSucursales.Valor(ComboBox2.SelectedIndex), cmbDocumento.SelectedIndex)

        'Nodos.Add(New NodoImpresionN(1, 100, 100, 100, 20, "Nombre", "clientenombre", New Font("Arial", 10, FontStyle.Regular), NodoImpresionN.Alineaciones.Izquierda, 0, 0, 1, TiposDocumentos.Venta, 0, 1))
        'Nodos.Add(New NodoImpresionN(2, 100, 130, 100, 20, "Direccion", "clientedir", New Font("Arial", 10, FontStyle.Regular), NodoImpresionN.Alineaciones.Izquierda, 0, 0, 1, TiposDocumentos.Venta, 0, 1))
        'Nodos.Add(New NodoImpresionN(3, 100, 160, 100, 20, "RFC", "clienterfc", New Font("Arial", 10, FontStyle.Regular), NodoImpresionN.Alineaciones.Izquierda, 0, 0, 1, TiposDocumentos.Venta, 0, 1))
        'Nodos.Add(New NodoImpresionN(4, 100, 190, 100, 20, "Fecha", "fecha", New Font("Arial", 10, FontStyle.Regular), NodoImpresionN.Alineaciones.Izquierda, 0, 0, 1, TiposDocumentos.Venta, 0, 1))
        'Nodos.Add(New NodoImpresionN(5, 100, 220, 600, 2, "Linea", "ln", New Font("Arial", 10, FontStyle.Regular), NodoImpresionN.Alineaciones.Izquierda, 0, 0, 1, TiposDocumentos.Venta, 1, 1))

        ConsultaOn = True
    End Sub

    Private Sub PanelDoubleBuffer1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'PanelDoubleBuffer1.Focus()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If ConsultaOn Then
            If IsNumeric(TextBox2.Text) Then
                For Each n As NodoImpresionN In Nodos
                    If n.Seleccionado Then
                        n.X = CInt(TextBox2.Text)
                        PanelDoubleBuffer1.Refresh()
                        Exit For
                    End If
                Next

            End If
        End If
        Try
            Label9.Text = CStr(CInt(TextBox2.Text) / 40) + "cm."
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If ConsultaOn Then
            If IsNumeric(TextBox3.Text) Then
                For Each n As NodoImpresionN In Nodos
                    If n.Seleccionado Then
                        n.Y = CInt(TextBox3.Text)
                        PanelDoubleBuffer1.Refresh()
                        Exit For
                    End If
                Next

            End If
        End If
        Try
            Label10.Text = CStr(CInt(TextBox3.Text) / 40) + "cm."
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        If ConsultaOn Then
            If IsNumeric(TextBox4.Text) Then
                For Each n As NodoImpresionN In Nodos
                    If n.Seleccionado Then
                        n.XL = CInt(TextBox4.Text)
                        PanelDoubleBuffer1.Refresh()
                        Exit For
                    End If
                Next
            End If
        End If
        Try
            Label12.Text = CStr(CInt(TextBox4.Text) / 40) + "cm."
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If ConsultaOn Then
            If IsNumeric(TextBox5.Text) Then
                For Each n As NodoImpresionN In Nodos
                    If n.Seleccionado Then
                        n.YL = CInt(TextBox5.Text)
                        PanelDoubleBuffer1.Refresh()
                        Exit For
                    End If
                Next
            End If
        End If
        Try
            Label13.Text = CStr(CInt(TextBox5.Text) / 40) + "cm."
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ConsultaOn Then
            If FontDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                For Each n As NodoImpresionN In Nodos
                    If n.Seleccionado Then
                        n.Fuente = FontDialog1.Font
                        TextBox6.Text = n.Fuente.FontFamily.Name + "," + n.Fuente.Size.ToString + "pt"
                        PanelDoubleBuffer1.Refresh()
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ConsultaOn Then
            For Each n As NodoImpresionN In Nodos
                If n.Seleccionado Then
                    n.Visible = ComboBox1.SelectedIndex
                    PanelDoubleBuffer1.Refresh()
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            OpenFileDialog1.Filter = "Archivos de imagen(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'Dim Imagen As Bitmap
                Fondo = Image.FromFile(OpenFileDialog1.FileName)
                TextBox7.Text = OpenFileDialog1.FileName
                PanelDoubleBuffer1.BackgroundImage = Fondo
                Dim Index As Integer = cmbDocumento.SelectedIndex
                If Index > 15 Then Index += 16
                Dim SA As New dbSucursalesArchivos
                If RadioButton1.Checked Then
                    SA.GuardaRuta(Index, IdsSucursales.Valor(ComboBox2.SelectedIndex), OpenFileDialog1.FileName, GlobalIdEmpresa)
                Else
                    SA.GuardaRuta(Index + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex), OpenFileDialog1.FileName, GlobalIdEmpresa)
                End If

            End If
        Catch ex As Exception
            MsgBox("No se pudo cargar la imagen", MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub PoneImagenDB()
        Try
            Dim SA As New dbSucursalesArchivos
            'Dim Imagen As Bitmap
            Dim Index As Integer = cmbDocumento.SelectedIndex
            If Index > 15 Then Index += 16
            Dim R As String
            If RadioButton1.Checked Then
                R = SA.DaRuta(Index, IdsSucursales.Valor(ComboBox2.SelectedIndex), GlobalIdEmpresa, True)
            Else
                R = SA.DaRuta(Index + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex), GlobalIdEmpresa, True)
            End If
            If R <> "" Then
                Fondo = Image.FromFile(R)
                TextBox7.Text = R
                PanelDoubleBuffer1.BackgroundImage = Fondo
            Else
                TextBox7.Text = ""
                PanelDoubleBuffer1.BackgroundImage = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub PoneDatosZona()
        'Dim Index As Integer = ComboBox3.SelectedIndex
        'If Index > 15 Then Index += 16
        If RadioButton1.Checked Then
            dbImp.DaZonaDetalles(cmbDocumento.SelectedItem.Value, IdsSucursales.Valor(ComboBox2.SelectedIndex))
            CheckBox4.Visible = False
            CheckBox5.Visible = False
        Else
            dbImp.DaZonaDetalles(cmbDocumento.SelectedItem.Value + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex))
            CheckBox4.Visible = True
            CheckBox5.Visible = True
        End If
        TextBox9.Text = dbImp.Y.ToString
        TextBox8.Text = dbImp.YL.ToString
        TextBox11.Text = dbImp.RG.ToString
        TextBox10.Text = dbImp.Alt.ToString
        TextBox12.Text = dbImp.Ancho.ToString
        Select Case dbImp.Modo
            Case 0
                CheckBox4.Checked = 0
                CheckBox5.Checked = 0
            Case 1
                CheckBox4.Checked = True
                CheckBox5.Checked = False
            Case 2
                CheckBox4.Checked = False
                CheckBox5.Checked = True
            Case 3
                CheckBox4.Checked = True
                CheckBox5.Checked = True
        End Select

    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ConsultaOn Then
            PoneImagenDB()
            PoneDatosZona()
            Dim Index As Integer
            Index = cmbDocumento.SelectedIndex
            If Index > 15 Then Index += 16
            If RadioButton1.Checked Then
                LlenaNodos(IdsSucursales.Valor(ComboBox2.SelectedIndex), Index)
            Else
                LlenaNodos(IdsSucursales.Valor(ComboBox2.SelectedIndex), Index + 1000)
            End If
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDocumento.SelectedIndexChanged
        If ConsultaOn Then
            PoneImagenDB()
            PoneDatosZona()
            Dim Index As Integer
            Index = cmbDocumento.SelectedIndex
            If Index > 15 Then Index += 16
            If RadioButton1.Checked Then
                LlenaNodos(IdsSucursales.Valor(ComboBox2.SelectedIndex), Index)
            Else
                LlenaNodos(IdsSucursales.Valor(ComboBox2.SelectedIndex), Index + 1000)
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox7.Text = ""
        PanelDoubleBuffer1.BackgroundImage = Nothing
        Fondo = Nothing
        Dim SA As New dbSucursalesArchivos
        If RadioButton1.Checked Then
            SA.GuardaRuta(cmbDocumento.SelectedIndex, IdsSucursales.Valor(ComboBox2.SelectedIndex), "", GlobalIdEmpresa)
        Else
            SA.GuardaRuta(cmbDocumento.SelectedIndex + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex), "", GlobalIdEmpresa)
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim Cuantas As Integer = 1
        For Each n As NodoImpresionN In Nodos
            If n.TipoNodo = 1 Then
                Cuantas += 1
            End If
        Next
        Dim no As New NodoImpresionN("", "", "", 0)
        Dim dbI As New dbImpresionesN(MySqlcon)
        Dim Index As Integer = cmbDocumento.SelectedIndex
        If Index > 15 Then Index += 16
        If RadioButton1.Checked Then
            no = New NodoImpresionN(6, 1, 100, 864, 2, "Línea" + Format(Cuantas, "00"), "ln" + Format(Cuantas, "00"), New Font("Arial", 10, FontStyle.Regular), NodoImpresionN.Alineaciones.Izquierda, 0, 0, 1, Index, 1, IdsSucursales.Valor(ComboBox2.SelectedIndex), 0, "Línea" + Format(Cuantas, "00"), 0, 8)
        Else
            no = New NodoImpresionN(6, 1, 100, 864, 2, "Línea" + Format(Cuantas, "00"), "ln" + Format(Cuantas, "00"), New Font("Arial", 10, FontStyle.Regular), NodoImpresionN.Alineaciones.Izquierda, 0, 0, 1, Index + 1000, 1, IdsSucursales.Valor(ComboBox2.SelectedIndex), 0, "Línea" + Format(Cuantas, "00"), 0, 8)
        End If
        dbI.GuardaNodo(no)
        no.id = dbI.ID
        Nodos.Add(no, "ln" + Format(Cuantas, "00"))
        PanelDoubleBuffer1.Refresh()
        PopUp("Linea Agregada", 90)
    End Sub
    Private Sub LlenaNodos(ByVal pidSucursal As Integer, ByVal pDocumento As Integer)
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
        Dim I As New dbImpresionesN(MySqlcon)
        Dim Fs As FontStyle
        Dim Cnodos As Integer = 0
        Nodos.Clear()
        ComboBox5.Items.Clear()
        ComboBox5.Items.Add("Sin seleccionar")

        Dim dr As MySql.Data.MySqlClient.MySqlDataReader
        Try
            dr = I.DaNodos(pDocumento, pidSucursal)
            While dr.Read
                Select Case dr("fuentestilo")
                    Case 1
                        Fs = FontStyle.Bold
                    Case 2
                        Fs = FontStyle.Italic
                    Case 0
                        Fs = FontStyle.Regular
                    Case 8
                        Fs = FontStyle.Strikeout
                    Case 4
                        Fs = FontStyle.Underline
                End Select
                If dr("tiponodo") <> 2 Then Cnodos += 1
                If dr("tiponodo") = 0 Then
                    Nodos.Add(New NodoImpresionN(dr("id"), dr("x"), dr("y"), dr("xl"), dr("yl"), dr("texto"), dr("datapropertyname"), New Font(dr("fuente").ToString, CSng(dr("fuentesize")), Fs), dr("alineacion"), dr("tipo"), dr("tipodato"), dr("visible"), dr("documento"), dr("tiponodo"), dr("idsucursal"), dr("conetiqueta"), dr("nombre"), dr("renglon"), dr("clasificacion")))
                    ComboBox5.Items.Add(dr("nombre"))
                Else
                    Nodos.Add(New NodoImpresionN(dr("id"), dr("x"), dr("y"), dr("xl"), dr("yl"), dr("texto"), dr("datapropertyname"), New Font(dr("fuente").ToString, CSng(dr("fuentesize")), Fs), dr("alineacion"), dr("tipo"), dr("tipodato"), dr("visible"), dr("documento"), dr("tiponodo"), dr("idsucursal"), dr("conetiqueta"), dr("nombre"), dr("renglon"), dr("clasificacion")), dr("datapropertyname"))
                    ComboBox5.Items.Add(dr("nombre"))
                End If
            End While
            dr.Close()
            ConsultaOn = False
            ComboBox5.SelectedIndex = 0
            ConsultaOn = True
            Me.Text = "Configuración de impresiones " + Cnodos.ToString
            Me.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub GuardaNodos()
        Try
            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
            If RadioButton2.Checked Then ReasignaTipos()
            Dim dbN As New dbImpresionesN(MySqlcon)
            For Each n As NodoImpresionN In Nodos
                dbN.ActualizaNodo(n)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        If IsNumeric(TextBox9.Text) And IsNumeric(TextBox8.Text) And ConsultaOn And IsNumeric(TextBox10.Text) And IsNumeric(TextBox11.Text) And IsNumeric(TextBox12.Text) Then
            Dim I As New dbImpresionesN(MySqlcon)
            Dim M As Byte = 0
            If CheckBox4.Checked = True And CheckBox5.Checked = False Then M = 1
            If CheckBox4.Checked = False And CheckBox5.Checked = True Then M = 2
            If CheckBox4.Checked = True And CheckBox5.Checked = True Then M = 3
            Dim Index As Integer = cmbDocumento.SelectedIndex
            If Index > 15 Then Index += 16
            If RadioButton1.Checked Then
                I.ActualizaZonaDetalles(Index, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            Else
                I.ActualizaZonaDetalles(Index + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            End If

        End If
        'If RadioButton2.Checked = True Then
        '    ReasignaTipos()
        'End If
        Try
            Label16.Text = CStr(CInt(TextBox9.Text) / 40) + "cm."
        Catch ex As Exception

        End Try
        'For Each n As NodoImpresionN In Nodos
        '    If n.Y > CInt(TextBox9.Text) And n.Y < CInt(TextBox9.Text) + CInt(TextBox8.Text) Then
        '        n.Tipo = 1
        '    Else
        '        n.Tipo = 0
        '    End If
        'Next
        PanelDoubleBuffer1.Refresh()
        'End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
        If IsNumeric(TextBox9.Text) And IsNumeric(TextBox8.Text) And ConsultaOn And IsNumeric(TextBox10.Text) And IsNumeric(TextBox11.Text) And IsNumeric(TextBox12.Text) Then
            Dim I As New dbImpresionesN(MySqlcon)
            Dim M As Byte = 0
            If CheckBox4.Checked = True And CheckBox5.Checked = False Then M = 1
            If CheckBox4.Checked = False And CheckBox5.Checked = True Then M = 2
            If CheckBox4.Checked = True And CheckBox5.Checked = True Then M = 3
            Dim Index As Integer = cmbDocumento.SelectedIndex
            If Index > 15 Then Index += 16
            If RadioButton1.Checked Then
                I.ActualizaZonaDetalles(Index, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            Else
                I.ActualizaZonaDetalles(Index + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            End If

        End If
        'If RadioButton2.Checked = True Then
        '    ReasignaTipos()
        'End If
        Try
            Label15.Text = CStr(CInt(TextBox8.Text) / 40) + "cm."
        Catch ex As Exception

        End Try
        'For Each n As NodoImpresionN In Nodos
        '    If n.Y > CInt(TextBox9.Text) And n.Y < CInt(TextBox9.Text) + CInt(TextBox8.Text) Then
        '        n.Tipo = 1
        '    Else
        '        n.Tipo = 0
        '    End If
        'Next
        PanelDoubleBuffer1.Refresh()
        'End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim Cual As String = ""
        Dim Id As Integer = 0
        For Each n As NodoImpresionN In Nodos
            If n.TipoNodo = 1 And n.Seleccionado Then
                Cual = n.DataPropertyName
                Id = n.id
                Exit For
            End If
        Next
        If Cual <> "" Then
            Nodos.Remove(Cual)
            Dim dbn As New dbImpresionesN(MySqlcon)
            dbn.EliminaNodo(Id)
            PanelDoubleBuffer1.Refresh()
            PopUp("Línea Removida", 90)
        End If

    End Sub



    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas) = True Then
            GuardaNodos()
            PopUp("Guardado", 90)
        Else
            MsgBox("No tiene que permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ConsultaOn Then
            For Each n As NodoImpresionN In Nodos
                If n.Seleccionado Then
                    n.ConEtiqueta = ComboBox4.SelectedIndex
                    'PanelDoubleBuffer1.Refresh()
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If ConsultaOn Then
            For Each n As NodoImpresionN In Nodos
                If n.Seleccionado Then
                    n.Texto = TextBox1.Text
                    PanelDoubleBuffer1.Refresh()
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim Cuantas As Integer = 1
        For Each n As NodoImpresionN In Nodos
            If n.TipoNodo = 2 Then
                Cuantas += 1
            End If
        Next
        Dim dbI As New dbImpresionesN(MySqlcon)
        Dim no As New NodoImpresionN("", "", "", 0)
        Dim Index As Integer = cmbDocumento.SelectedIndex
        If Index > 15 Then Index += 16
        If RadioButton1.Checked Then
            no = New NodoImpresionN(10, 10, 10, 100, 20, "Etiqueta" + Format(Cuantas, "00"), "et" + Format(Cuantas, "00"), New Font("Lucida Console", 8, FontStyle.Regular), NodoImpresionN.Alineaciones.Izquierda, 0, 0, 1, Index, 2, IdsSucursales.Valor(ComboBox2.SelectedIndex), 0, "Etiqueta" + Format(Cuantas, "00"), 0, 7)
        Else
            no = New NodoImpresionN(10, 10, 10, 100, 20, "Etiqueta" + Format(Cuantas, "00"), "et" + Format(Cuantas, "00"), New Font("Lucida Console", 8, FontStyle.Regular), NodoImpresionN.Alineaciones.Izquierda, 0, 0, 1, Index + 1000, 2, IdsSucursales.Valor(ComboBox2.SelectedIndex), 0, "Etiqueta" + Format(Cuantas, "00"), 0, 7)
        End If
        dbI.GuardaNodo(no)
        no.id = dbI.ID
        Nodos.Add(no, "et" + Format(Cuantas, "00"))
        PanelDoubleBuffer1.Refresh()
        PopUp("Etiqueta Agregada", 90)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim Cual As String = ""
        Dim Id As Integer = 0
        For Each n As NodoImpresionN In Nodos
            If n.TipoNodo = 2 And n.Seleccionado Then
                Cual = n.DataPropertyName
                Id = n.id
                Exit For
            End If
        Next
        If Cual <> "" Then
            Nodos.Remove(Cual)
            Dim dbn As New dbImpresionesN(MySqlcon)
            dbn.EliminaNodo(Id)
            PanelDoubleBuffer1.Refresh()
            PopUp("Etiqueda Removida", 90)
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim en As New Encriptador
            Dim dbI As New dbImpresionesN(MySqlcon)
            Dim Index As Integer = cmbDocumento.SelectedIndex
            If Index > 15 Then Index += 16
            If RadioButton1.Checked Then
                en.GuardaArchivoTexto(SaveFileDialog1.FileName, dbI.PasaATexto(Index, IdsSucursales.Valor(ComboBox2.SelectedIndex)), System.Text.Encoding.Default)
            Else
                en.GuardaArchivoTexto(SaveFileDialog1.FileName, dbI.PasaATexto(Index + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex)), System.Text.Encoding.Default)
            End If
            PopUp("Archivo Guardado", 90)
        End If
    End Sub


    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If TextBox8.Enabled = True Then
            SplitContainer1.Panel1.Enabled = False
            'For Each c As Control In Me.Controls
            '    c.Enabled = False
            'Next
            'PanelDoubleBuffer1.Enabled = True
        End If
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged
        If IsNumeric(TextBox9.Text) And IsNumeric(TextBox8.Text) And ConsultaOn And IsNumeric(TextBox10.Text) And IsNumeric(TextBox11.Text) And IsNumeric(TextBox12.Text) Then
            Dim I As New dbImpresionesN(MySqlcon)
            Dim M As Byte = 0
            If CheckBox4.Checked = True And CheckBox5.Checked = False Then M = 1
            If CheckBox4.Checked = False And CheckBox5.Checked = True Then M = 2
            If CheckBox4.Checked = True And CheckBox5.Checked = True Then M = 3
            Dim Index As Integer = cmbDocumento.SelectedIndex
            If Index > 15 Then Index += 16
            If RadioButton1.Checked Then
                I.ActualizaZonaDetalles(Index, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            Else
                I.ActualizaZonaDetalles(Index + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            End If

        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        If IsNumeric(TextBox9.Text) And IsNumeric(TextBox8.Text) And ConsultaOn And IsNumeric(TextBox10.Text) And IsNumeric(TextBox11.Text) And IsNumeric(TextBox12.Text) Then
            Dim I As New dbImpresionesN(MySqlcon)
            Dim M As Byte = 0
            If CheckBox4.Checked = True And CheckBox5.Checked = False Then M = 1
            If CheckBox4.Checked = False And CheckBox5.Checked = True Then M = 2
            If CheckBox4.Checked = True And CheckBox5.Checked = True Then M = 3
            Dim Index As Integer = cmbDocumento.SelectedIndex
            If Index > 15 Then Index += 16
            If RadioButton1.Checked Then
                I.ActualizaZonaDetalles(Index, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            Else
                I.ActualizaZonaDetalles(Index + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            End If


        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        PanelDoubleBuffer1.Refresh()
    End Sub
    Private Sub ReasignaTipos()
        'If RadioButton1.Checked = True Then
        '    For Each n As NodoImpresionN In Nodos
        '        If n.Tipo = 2 Then
        '            n.Tipo = 0
        '        End If
        '    Next
        'Else
        For Each n As NodoImpresionN In Nodos
            If n.Y < CInt(TextBox9.Text) And n.Tipo <> 1 Then
                n.Tipo = 0
            End If
            If n.Y > CInt(TextBox9.Text) + CInt(TextBox8.Text) And n.Tipo <> 1 Then
                n.Tipo = 2
            End If
        Next
        'End If
    End Sub
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim fan As New frmImpresionesAltaNodos
        fan.Show()
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        If ConsultaOn Then
            For Each n As NodoImpresionN In Nodos
                n.Seleccionado = False
            Next
            If ComboBox5.SelectedIndex > 0 Then
                ConsultaOn = False
                TextBox1.Text = ""
                TextBox2.Text = "0"
                TextBox3.Text = "0"
                TextBox4.Text = "0"
                TextBox5.Text = "0"
                TextBox6.Text = ""
                'Label22.Text = ""
                ' ComboBox5.SelectedIndex = 0
                ComboBox1.SelectedIndex = 1
                ComboBox4.SelectedIndex = 1
                ConsultaOn = True
                For Each n As NodoImpresionN In Nodos
                    If n.TipoNodo = 0 Then
                        If n.Nombre = ComboBox5.Text Then
                            If n.Visible = 0 And CheckBox1.Checked Then
                            Else
                                n.Seleccionado = True
                                'n.MouseX = e.X - n.X
                                'n.MouseY = e.Y - n.Y
                                ConsultaOn = False
                                TextBox1.Text = n.Texto
                                TextBox2.Text = n.X.ToString
                                TextBox3.Text = n.Y.ToString
                                TextBox4.Text = n.XL.ToString
                                TextBox5.Text = n.YL.ToString
                                TextBox6.Text = n.Fuente.Name + "," + n.Fuente.Size.ToString + "pt"
                                'ComboBox5.Text = n.Nombre
                                ComboBox1.SelectedIndex = n.Visible
                                ComboBox4.SelectedIndex = n.ConEtiqueta
                                ConsultaOn = True
                                Exit For
                            End If

                        End If
                    Else
                        If n.Nombre = ComboBox5.Text Then
                            If n.Visible = 0 And CheckBox1.Checked Then
                            Else
                                n.Seleccionado = True
                                'n.MouseX = e.X - n.X
                                'n.MouseY = e.Y - n.Y
                                ConsultaOn = False
                                TextBox1.Text = n.Texto
                                TextBox2.Text = n.X.ToString
                                TextBox3.Text = n.Y.ToString
                                TextBox4.Text = n.XL.ToString
                                TextBox5.Text = n.YL.ToString
                                TextBox6.Text = n.Fuente.Name + "," + n.Fuente.Size.ToString + "pt"
                                'ComboBox5.Text = n.Nombre
                                ComboBox1.SelectedIndex = n.Visible
                                ComboBox4.SelectedIndex = n.ConEtiqueta
                                ConsultaOn = True
                                Exit For
                            End If
                        End If
                    End If
                Next
            End If
            PanelDoubleBuffer1.Refresh()
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked And ConsultaOn Then
            PoneImagenDB()
            PoneDatosZona()
            Dim Index As Integer
            Index = cmbDocumento.SelectedIndex
            If Index > 15 Then Index += 16
            If RadioButton1.Checked Then
                LlenaNodos(IdsSucursales.Valor(ComboBox2.SelectedIndex), Index)
            Else
                LlenaNodos(IdsSucursales.Valor(ComboBox2.SelectedIndex), Index + 1000)
            End If
            RadioButton1.BackgroundImage = Fijo01
            RadioButton2.BackgroundImage = Flujo02
            ' ReasignaTipos()
        End If
        'If IsNumeric(TextBox9.Text) And IsNumeric(TextBox8.Text) And ConsultaOn And IsNumeric(TextBox10.Text) And IsNumeric(TextBox11.Text) And IsNumeric(TextBox12.Text) Then
        '    Dim I As New dbImpresionesN(MySqlcon)
        '    Dim M As Byte
        '    If RadioButton1.Checked Then
        '        M = 0
        '    Else
        '        M = 1
        '    End If
        '    'I.ActualizaZonaDetalles(ComboBox3.SelectedIndex, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text))
        'End If

    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked And ConsultaOn Then
            ConsultaOn = False
            PoneImagenDB()
            PoneDatosZona()
            Dim Index As Integer
            Index = cmbDocumento.SelectedIndex
            If Index > 15 Then Index += 16
            If RadioButton1.Checked Then
                LlenaNodos(IdsSucursales.Valor(ComboBox2.SelectedIndex), Index)
            Else
                LlenaNodos(IdsSucursales.Valor(ComboBox2.SelectedIndex), Index + 1000)
            End If
            RadioButton1.BackgroundImage = Fijo02
            RadioButton2.BackgroundImage = Flujo01
            'ReasignaTipos()
            ConsultaOn = True
        End If
        'If IsNumeric(TextBox9.Text) And IsNumeric(TextBox8.Text) And ConsultaOn And IsNumeric(TextBox10.Text) And IsNumeric(TextBox11.Text) And IsNumeric(TextBox12.Text) Then
        '    Dim I As New dbImpresionesN(MySqlcon)
        '    Dim M As Byte
        '    If RadioButton1.Checked Then
        '        M = 0
        '    Else
        '        M = 1
        '    End If
        '    I.ActualizaZonaDetalles(ComboBox3.SelectedIndex, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text))
        'End If
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        If IsNumeric(TextBox9.Text) And IsNumeric(TextBox8.Text) And ConsultaOn And IsNumeric(TextBox10.Text) And IsNumeric(TextBox11.Text) And IsNumeric(TextBox12.Text) Then
            Dim I As New dbImpresionesN(MySqlcon)
            Dim M As Byte = 0
            If CheckBox4.Checked = True And CheckBox5.Checked = False Then M = 1
            If CheckBox4.Checked = False And CheckBox5.Checked = True Then M = 2
            If CheckBox4.Checked = True And CheckBox5.Checked = True Then M = 3
            Dim Index As Integer = cmbDocumento.SelectedIndex
            If Index > 15 Then Index += 16
            If RadioButton1.Checked Then
                I.ActualizaZonaDetalles(Index, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            Else
                I.ActualizaZonaDetalles(Index + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            End If

        End If
        Try
            Label28.Text = CStr(CInt(TextBox12.Text) / 40) + "cm."
        Catch ex As Exception

        End Try
        PanelDoubleBuffer1.Refresh()
    End Sub

    Private Sub PanelDoubleBuffer1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelDoubleBuffer1.MouseDown
        If Mueve = False Then
            For Each n As NodoImpresionN In Nodos
                n.Seleccionado = False
            Next
            ConsultaOn = False
            TextBox1.Text = ""
            TextBox2.Text = "0"
            TextBox3.Text = "0"
            TextBox4.Text = "0"
            TextBox5.Text = "0"
            TextBox6.Text = ""
            'Label22.Text = ""
            ComboBox5.SelectedIndex = 0
            ComboBox1.SelectedIndex = 1
            ComboBox4.SelectedIndex = 1
            ConsultaOn = True
            For Each n As NodoImpresionN In Nodos
                If n.TipoNodo = 0 Then
                    If e.X > n.X And e.X < n.X + n.XL And e.Y > n.Y And e.Y < n.Y + n.YL Then
                        If n.Visible = 0 And CheckBox1.Checked Then
                        Else
                            n.Seleccionado = True
                            n.MouseX = e.X - n.X
                            n.MouseY = e.Y - n.Y
                            ConsultaOn = False
                            TextBox1.Text = n.Texto
                            TextBox2.Text = n.X.ToString
                            TextBox3.Text = n.Y.ToString
                            TextBox4.Text = n.XL.ToString
                            TextBox5.Text = n.YL.ToString
                            TextBox6.Text = n.Fuente.Name + "," + n.Fuente.Size.ToString + "pt"
                            ComboBox5.Text = n.Nombre
                            ComboBox1.SelectedIndex = n.Visible
                            ComboBox4.SelectedIndex = n.ConEtiqueta
                            If n.Renglon = 0 Then
                                CheckBox6.Checked = False
                            Else
                                CheckBox6.Checked = True
                            End If
                            ConsultaOn = True
                            Exit For
                        End If

                    End If
                Else
                    If e.X >= n.X And e.X <= n.X + n.XL And e.Y >= n.Y - 1 And e.Y <= n.Y + n.YL + 1 Then
                        If n.Visible = 0 And CheckBox1.Checked Then
                        Else
                            n.Seleccionado = True
                            n.MouseX = e.X - n.X
                            n.MouseY = e.Y - n.Y
                            ConsultaOn = False
                            TextBox1.Text = n.Texto
                            TextBox2.Text = n.X.ToString
                            TextBox3.Text = n.Y.ToString
                            TextBox4.Text = n.XL.ToString
                            TextBox5.Text = n.YL.ToString
                            TextBox6.Text = n.Fuente.Name + "," + n.Fuente.Size.ToString + "pt"
                            ComboBox5.Text = n.Nombre
                            ComboBox1.SelectedIndex = n.Visible
                            ComboBox4.SelectedIndex = n.ConEtiqueta
                            If n.Renglon = 0 Then
                                CheckBox6.Checked = False
                            Else
                                CheckBox6.Checked = True
                            End If
                            ConsultaOn = True
                            Exit For
                        End If
                    End If
                End If
            Next
            If CheckBox2.Checked Then PanelDoubleBuffer1.BackgroundImage = Nothing
            PanelDoubleBuffer1.Refresh()
        End If
        Mueve = True
    End Sub

    Private Sub PanelDoubleBuffer1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelDoubleBuffer1.MouseMove
        If Mueve Then
            If CheckBox3.Checked Then
                PanelDoubleBuffer1.Invalidate(Rectangulo2)
            End If
            For Each n As NodoImpresionN In Nodos
                If n.Seleccionado Then
                    n.X = e.X - n.MouseX
                    If n.X < 0 Then n.X = 0
                    If n.Y < 0 Then n.Y = 0
                    n.Y = e.Y - n.MouseY
                    ConsultaOn = False
                    TextBox2.Text = n.X.ToString
                    TextBox3.Text = n.Y.ToString
                    Rectangulo2 = Rectangulo
                    Rectangulo.X = n.X
                    Rectangulo.Y = n.Y - 10
                    Rectangulo.Width = n.XL + 2
                    Rectangulo.Height = n.YL + 12
                    ConsultaOn = True
                    Exit For
                End If
            Next
            If CheckBox3.Checked Then
                PanelDoubleBuffer1.Invalidate(Rectangulo)
            Else
                PanelDoubleBuffer1.Refresh()
            End If
        End If
    End Sub

    Private Sub PanelDoubleBuffer1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelDoubleBuffer1.MouseUp
        'For Each n As NodoImpresionN In Nodos
        '    If n.Seleccionado Then
        '        If n.Y > CInt(TextBox9.Text) And n.Y < CInt(TextBox9.Text) + CInt(TextBox8.Text) Then
        '            n.Tipo = 1
        '        Else
        '            n.Tipo = 0
        '        End If

        '        Exit For
        '    End If
        'Next
        If CheckBox2.Checked And Mueve Then
            PanelDoubleBuffer1.BackgroundImage = Fondo
            'Dim SA As New dbSucursalesArchivos
        Else
            If Mueve Then
                PanelDoubleBuffer1.Refresh()
            End If
        End If

        Mueve = False

    End Sub

    Private Sub PanelDoubleBuffer1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelDoubleBuffer1.Paint
        For Each n As NodoImpresionN In Nodos
            Select Case n.TipoNodo
                Case 0
                    If n.Seleccionado Then
                        If n.Visible = 1 Then
                            e.Graphics.DrawRectangle(Pens.Blue, n.X, n.Y, n.XL, n.YL)
                            e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Blue, n.X + 2, n.Y + 2)
                            e.Graphics.DrawString(CStr(n.X / 40) + "-" + CStr(n.Y / 40), FuenteChica, Brushes.Blue, n.X, n.Y - 10)
                        Else
                            If CheckBox1.Checked = False Then
                                e.Graphics.DrawRectangle(Pens.Salmon, n.X, n.Y, n.XL, n.YL)
                                e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Salmon, n.X + 2, n.Y + 2)
                                e.Graphics.DrawString(CStr(n.X / 40) + "-" + CStr(n.Y / 40), FuenteChica, Brushes.Salmon, n.X, n.Y - 10)
                            End If
                        End If
                    Else
                        If n.Visible = 1 Then
                            Select Case n.Tipo
                                Case 0
                                    e.Graphics.DrawRectangle(Pens.Black, n.X, n.Y, n.XL, n.YL)
                                    e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X + 2, n.Y + 2)
                                Case 1
                                    e.Graphics.DrawRectangle(Pens.Green, n.X, n.Y, n.XL, n.YL)
                                    e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Green, n.X + 2, n.Y + 2)
                                Case 2
                                    e.Graphics.DrawRectangle(Pens.DarkViolet, n.X, n.Y, n.XL, n.YL)
                                    e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.DarkViolet, n.X + 2, n.Y + 2)
                            End Select

                        Else
                            If CheckBox1.Checked = False Then
                                e.Graphics.DrawRectangle(Pens.Gray, n.X, n.Y, n.XL, n.YL)
                                e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Gray, n.X + 2, n.Y + 2)
                            End If
                        End If
                    End If
                Case 1
                    If n.Seleccionado Then
                        If n.Visible = 1 Then
                            e.Graphics.DrawLine(New Pen(Color.Blue, n.YL), n.X, n.Y, n.XL, n.Y)
                            e.Graphics.DrawString(CStr(n.X / 40) + "-" + CStr(n.Y / 40), FuenteChica, Brushes.Blue, n.X, n.Y - 11)
                        Else
                            If CheckBox1.Checked = False Then
                                e.Graphics.DrawLine(New Pen(Color.Salmon, n.YL), n.X, n.Y, n.XL, n.Y)
                                e.Graphics.DrawString(CStr(n.X / 40) + "-" + CStr(n.Y / 40), FuenteChica, Brushes.Blue, n.X, n.Y - 11)
                            End If
                        End If
                    Else
                        If n.Visible = 1 Then
                            e.Graphics.DrawLine(New Pen(Color.Black, n.YL), n.X, n.Y, n.XL, n.Y)
                        Else
                            If CheckBox1.Checked = False Then
                                e.Graphics.DrawLine(New Pen(Color.Gray, n.YL), n.X, n.Y, n.XL, n.Y)
                            End If
                        End If
                    End If
                Case 2
                    If n.Seleccionado Then
                        If n.Visible = 1 Then
                            e.Graphics.DrawRectangle(Pens.Blue, n.X, n.Y, n.XL, n.YL)
                            e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Blue, n.X + 2, n.Y + 2)
                            e.Graphics.DrawString(CStr(n.X / 40) + "-" + CStr(n.Y / 40), FuenteChica, Brushes.Blue, n.X, n.Y - 10)
                        Else
                            If CheckBox1.Checked = False Then
                                e.Graphics.DrawRectangle(Pens.Salmon, n.X, n.Y, n.XL, n.YL)
                                e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Salmon, n.X + 2, n.Y + 2)
                                e.Graphics.DrawString(CStr(n.X / 40) + "-" + CStr(n.Y / 40), FuenteChica, Brushes.Salmon, n.X, n.Y - 10)
                            End If
                        End If
                    Else
                        If n.Visible = 1 Then
                            'e.Graphics.DrawRectangle(Pens.Black, n.X, n.Y, n.XL, n.YL)
                            'e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X + 2, n.Y + 2)
                            Select Case n.Tipo
                                Case 0
                                    e.Graphics.DrawRectangle(Pens.Black, n.X, n.Y, n.XL, n.YL)
                                    e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X + 2, n.Y + 2)
                                Case 1
                                    e.Graphics.DrawRectangle(Pens.Green, n.X, n.Y, n.XL, n.YL)
                                    e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Green, n.X + 2, n.Y + 2)
                                Case 2
                                    e.Graphics.DrawRectangle(Pens.DarkViolet, n.X, n.Y, n.XL, n.YL)
                                    e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.DarkViolet, n.X + 2, n.Y + 2)
                            End Select
                        Else
                            If CheckBox1.Checked = False Then
                                e.Graphics.DrawRectangle(Pens.Gray, n.X, n.Y, n.XL, n.YL)
                                e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Gray, n.X + 2, n.Y + 2)
                            End If
                        End If
                    End If
                Case 3
                    If n.Seleccionado Then
                        If n.Visible = 1 Then
                            e.Graphics.DrawRectangle(Pens.Blue, n.X, n.Y, n.XL, n.YL)
                            e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Blue, n.X + 2, n.Y + 2)
                            e.Graphics.DrawString(CStr(n.X / 40) + "-" + CStr(n.Y / 40), FuenteChica, Brushes.Blue, n.X, n.Y - 10)
                        Else
                            If CheckBox1.Checked = False Then
                                e.Graphics.DrawRectangle(Pens.Salmon, n.X, n.Y, n.XL, n.YL)
                                e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Salmon, n.X + 2, n.Y + 2)
                                e.Graphics.DrawString(CStr(n.X / 40) + "-" + CStr(n.Y / 40), FuenteChica, Brushes.Salmon, n.X, n.Y - 10)
                            End If
                        End If
                    Else
                        If n.Visible = 1 Then
                            'e.Graphics.DrawRectangle(Pens.Black, n.X, n.Y, n.XL, n.YL)
                            'e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X + 2, n.Y + 2)
                            Select Case n.Tipo
                                Case 0
                                    e.Graphics.DrawRectangle(Pens.Black, n.X, n.Y, n.XL, n.YL)
                                    e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X + 2, n.Y + 2)
                                Case 1
                                    e.Graphics.DrawRectangle(Pens.Green, n.X, n.Y, n.XL, n.YL)
                                    e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Green, n.X + 2, n.Y + 2)
                                Case 2
                                    e.Graphics.DrawRectangle(Pens.DarkViolet, n.X, n.Y, n.XL, n.YL)
                                    e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.DarkViolet, n.X + 2, n.Y + 2)
                            End Select
                        Else
                            If CheckBox1.Checked = False Then
                                e.Graphics.DrawRectangle(Pens.Gray, n.X, n.Y, n.XL, n.YL)
                                e.Graphics.DrawString(n.Texto, n.Fuente, Brushes.Gray, n.X + 2, n.Y + 2)
                            End If
                        End If
                    End If
            End Select
        Next
        If IsNumeric(TextBox9.Text) And IsNumeric(TextBox8.Text) Then
            e.Graphics.DrawLine(PlumaDetalles, 1, CInt(TextBox9.Text), 865, CInt(TextBox9.Text))
            e.Graphics.DrawLine(PlumaDetalles, 1, CInt(TextBox9.Text) + CInt(TextBox8.Text), 865, CInt(TextBox9.Text) + CInt(TextBox8.Text))
        End If
        If IsNumeric(TextBox12.Text) Then
            e.Graphics.DrawLine(PlumaDetalles, CInt(TextBox12.Text), 1, CInt(TextBox12.Text), 1116)
        End If
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If IsNumeric(TextBox9.Text) And IsNumeric(TextBox8.Text) And ConsultaOn And IsNumeric(TextBox10.Text) And IsNumeric(TextBox11.Text) And IsNumeric(TextBox12.Text) Then
            Dim I As New dbImpresionesN(MySqlcon)
            Dim M As Byte = 0
            If CheckBox4.Checked = True And CheckBox5.Checked = False Then M = 1
            If CheckBox4.Checked = False And CheckBox5.Checked = True Then M = 2
            If CheckBox4.Checked = True And CheckBox5.Checked = True Then M = 3
            If RadioButton1.Checked Then
                I.ActualizaZonaDetalles(cmbDocumento.SelectedIndex, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            Else
                I.ActualizaZonaDetalles(cmbDocumento.SelectedIndex + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            End If

        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        If IsNumeric(TextBox9.Text) And IsNumeric(TextBox8.Text) And ConsultaOn And IsNumeric(TextBox10.Text) And IsNumeric(TextBox11.Text) And IsNumeric(TextBox12.Text) Then
            Dim I As New dbImpresionesN(MySqlcon)
            Dim M As Byte = 0
            If CheckBox4.Checked = True And CheckBox5.Checked = False Then M = 1
            If CheckBox4.Checked = False And CheckBox5.Checked = True Then M = 2
            If CheckBox4.Checked = True And CheckBox5.Checked = True Then M = 3
            If RadioButton1.Checked Then
                I.ActualizaZonaDetalles(cmbDocumento.SelectedIndex, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            Else
                I.ActualizaZonaDetalles(cmbDocumento.SelectedIndex + 1000, IdsSucursales.Valor(ComboBox2.SelectedIndex), CInt(TextBox9.Text), CInt(TextBox8.Text), CInt(TextBox11.Text), CInt(TextBox10.Text), M, CInt(TextBox12.Text), GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas))
            End If

        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If ConsultaOn Then
            For Each n As NodoImpresionN In Nodos
                If n.Seleccionado Then
                    If CheckBox6.Checked = True Then
                        n.Renglon = 1
                    Else
                        n.Renglon = 0
                    End If
                    'PanelDoubleBuffer1.Refresh()
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Try
            If MsgBox("¿Copiar el formato del documento seleccionado de la sucursal default a la seleccionada? Este proceso no se puede deshacer.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                If GlobalIdSucursalDefault = IdsSucursales.Valor(ComboBox2.SelectedIndex) Then
                    MsgBox("Debe seleccionar una sucursal diferente a la que este por default.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                Dim DocumentoI As Integer = cmbDocumento.SelectedIndex
                If DocumentoI > 15 Then DocumentoI += 1000
                Dim ImpN As New dbImpresionesN(MySqlcon)
                If RadioButton1.Checked Then
                    'SA.GuardaRuta(Index, IdsSucursales.Valor(ComboBox2.SelectedIndex), OpenFileDialog1.FileName, GlobalIdEmpresa)
                    ImpN.CopiaFormato(GlobalIdSucursalDefault, IdsSucursales.Valor(ComboBox2.SelectedIndex), DocumentoI)
                Else
                    ImpN.CopiaFormato(GlobalIdSucursalDefault, IdsSucursales.Valor(ComboBox2.SelectedIndex), DocumentoI + 1000)
                End If
                If ConsultaOn Then
                    PoneImagenDB()
                    PoneDatosZona()
                    Dim Index As Integer
                    Index = cmbDocumento.SelectedIndex
                    If Index > 15 Then Index += 16
                    If RadioButton1.Checked Then
                        LlenaNodos(IdsSucursales.Valor(ComboBox2.SelectedIndex), Index)
                    Else
                        LlenaNodos(IdsSucursales.Valor(ComboBox2.SelectedIndex), Index + 1000)
                    End If
                End If
                PopUp("Listo", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, GlobalNombreApp)
        End Try
    End Sub
End Class