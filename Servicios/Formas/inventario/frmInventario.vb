Public Class frmInventario
    Dim IdsTC1 As New elemento
    Dim IdsTC2 As New elemento
    Dim IdsClas As New elemento
    Dim IdsClas2 As New elemento
    Dim IdsClas3 As New elemento
    Public IdInventario As Integer
    Dim IdsMonedas As New elemento
    Dim IdsMonedas2 As New elemento
    Dim IdsAlmacenes As New elemento
    Dim ImpND As New Collection
    Dim ImpNDi As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi2 As New Collection

    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Dim IdPrecio As Integer
    Dim EncontroClas As Boolean = True
    Dim Costo As Double
    Dim CSat As dbCatalogosSat
    Public Codigo As String
    Public Descripcion As String
    Dim TipoAlta As Byte
    Dim CodigoBidimensional As Bitmap
    Public Sub New(Optional pTipoAlta As Integer = 0)

        ' This call is required by the designer.
        InitializeComponent()
        TipoAlta = pTipoAlta
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmInventario_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CSat.Con.Close()
    End Sub
    Private Sub frmInventario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim HuboError As Boolean = False
        Try
            Me.Icon = GlobalIcono
            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
                For Each c As Control In Me.Controls
                    c.Enabled = False
                Next
                Button4.Enabled = True
                Exit Sub
            End If
            If My.Settings.repultimocosto Then
                CheckBox1.Checked = True
            End If
            LlenaCombos("tbltiposcantidades", ComboBox1, "nombre", "nombret", "idtipocantidad", IdsTC1, "idtipocantidad>1")
            LlenaCombos("tbltiposcantidades", ComboBox2, "nombre", "nombret", "idtipocantidad", IdsTC2, "idtipocantidad>1")
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1", "Todos")
            ComboBox6.Items.Add("General")
            ComboBox7.Items.Add("General")
            ComboBox9.Items.Add("No Usa")
            ComboBox9.Items.Add("AxBxCxD/12")
            LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", , "nombre")
            Csat = New dbCatalogosSat
            Dim X As New elemento
            Csat.IniciarMySQL(My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
            CSat.LlenaCombos("tblunidadaduana", ComboBox10, "concat(clave,' ',descripcion)", "nombrem", "clave", X, , , , True)
            'LlenaCombos("tblmonedas", ComboBox5, "nombre", "nombrem", "idmoneda", IdsMonedas, "idmoneda>1")
            LlenaCombos("tblmonedas", ComboBox4, "nombre", "nombrem", "idmoneda", IdsMonedas2, "idmoneda>1")
            If ComboBox1.Items.Count <= 0 Or ComboBox2.Items.Count <= 0 Then
                MsgBox("Debe dar alta primero los tipos de medida para poder registrar artículos en el inventario.", MsgBoxStyle.Critical, GlobalNombreApp)
                Button1.Enabled = False
                Button3.Enabled = False
                HuboError = True
            End If
            If ComboBox3.Items.Count <= 0 Then
                MsgBox("Debe dar alta primero las clasificaciones para poder registrar artículos en el inventario.", MsgBoxStyle.Critical, GlobalNombreApp)
                Button1.Enabled = False
                Button3.Enabled = False
                HuboError = True
            End If
            If ComboBox8.Items.Count <= 1 Then
                MsgBox("Debe dar alta primero los almacenes para poder registrar artículos en el inventario.", MsgBoxStyle.Critical, GlobalNombreApp)
                Button1.Enabled = False
                Button3.Enabled = False
                HuboError = True
            End If
            'If ComboBox5.Items.Count <= 0 Then
            '    MsgBox("Debe dar alta primero los tipo de moneda para poder registrar artículos en el inventario.", MsgBoxStyle.Critical, GlobalNombreApp)
            '    Button1.Enabled = False
            '    Button3.Enabled = False
            '    HuboError = True
            'End If
            ConsultaOn = False
            ComboBox8.SelectedIndex = IdsAlmacenes.Busca(GlobalIdAlmacen)
            If Not HuboError Then Nuevo()
            If TipoAlta = 1 Then
                If Codigo <> "" Then txtCodigo1.Text = Codigo
                TextBox1.Text = Descripcion
            End If

            cmbCodigoBarras1.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras))
            cmbCodigoBarras1.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras2))
            cmbCodigoBarras1.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras3))
            cmbCodigoBarras1.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras4))
            cmbCodigoBarras1.SelectedIndex = 0
            cmbCodigoBarras2.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras))
            cmbCodigoBarras2.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras2))
            cmbCodigoBarras2.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras3))
            cmbCodigoBarras2.Items.Add(New EnumDescriptor(Of TiposDocumentos)(TiposDocumentos.CodigoBarras4))
            cmbCodigoBarras2.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Nuevo()
        Try
            Dim I As New dbInventario(MySqlcon)
            txtCodigo1.Text = I.DaMaximo(False)
            TextBox1.Text = ""
            txtCodigo2.Text = ""
            'txtZona.Text = ""
            Button9.Visible = False
            txtExaminar.Text = ""
            cargarImagenLeave()
            btnCodigoBarras1.Enabled = False
            btnCodigoBarras2.Enabled = False
            cmbCodigoBarras1.Enabled = False
            cmbCodigoBarras2.Enabled = False
            CheckBox8.Checked = False
            If CheckBox6.Checked = False Then
                ConsultaOn = False
                txtIEPS.Text = ""
                txtIvaRetenido.Text = ""
                Costo = 0
                HabilitaClase3(False)
                Button8.Visible = False
                Label33.Visible = False
                TextBox2.Text = "0"
                TextBox3.Text = "1"
                TextBox4.Text = "0"
                TextBox5.Text = ""
                TextBox7.Text = "0"
                TextBox11.Text = ""
                TextBox14.Text = "16"
                TextBox17.Text = ""
                TextBox18.Text = "0"
                TextBox19.Text = "0"
                TextBox20.Text = "0"
                TextBox22.Text = ""
                TextBox23.Text = ""
                TextBox24.Text = ""
                Label40.Text = ""
                Label41.Text = ""
                ComboBox10.Text = "06 PIEZA"
                CheckBox12.Checked = False
                CheckBox13.Checked = False
                txtIEPS.Text = "0"
                txtIvaRetenido.Text = "0"
                CheckBox9.Checked = False
                CheckBox7.Checked = False
                TextBox8.Text = ""
                CheckBox4.Checked = False
                CheckBox5.Checked = False
                TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
                TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
                TextBox3.BackColor = Color.FromKnownColor(KnownColor.Window)
                txtCodigo1.BackColor = Color.FromKnownColor(KnownColor.Window)
                TextBox7.BackColor = Color.FromKnownColor(KnownColor.Window)
                TextBox4.BackColor = Color.FromKnownColor(KnownColor.Window)
                ComboBox1.SelectedIndex = 0
                ComboBox2.SelectedIndex = 0
                ComboBox3.SelectedIndex = 0
                ComboBox6.SelectedIndex = 0
                ComboBox7.SelectedIndex = 0
                ComboBox9.SelectedIndex = 0
                CheckBox10.Checked = False
                CheckBox2.Checked = False
                CheckBox15.Checked = False
                CheckBox3.Checked = True
                CheckBox16.Checked = False
                CheckBox17.Checked = False
                CheckBox18.Checked = False
                chkUsaUbicacion.Checked = False
                'Panel1.Visible = False
                'Label12.Visible = False
                'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Articulos + 2)))) = 0 Then
                'Button8.Enabled = False
                'Panel1.Enabled = False
                'End If
            End If
            CheckBox14.Checked = False
            CheckBox11.Checked = False
            Button5.Text = "Guardar"
            NuevoPrecio()
            Button1.Text = "Guardar"
            Button2.Enabled = False
            ConsultaOn = True
            DataGridView2.DataSource = Nothing
            Consulta()
            txtCodigo1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then

                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex

                Dim I As New dbInventario(MySqlcon)
                DataGridView1.DataSource = I.Consulta(0, TextBox16.Text, txtCodigo1.Text, , , , , txtCodigo2.Text, , GlobalpFabricante, GlobalModoBusqueda, 0, 0)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Código"
                DataGridView1.Columns(2).HeaderText = "Descripción"
                DataGridView1.Columns(3).HeaderText = "Cantidad"
                DataGridView1.Columns(3).DefaultCellStyle.Format = "0.####"
                DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo()
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodigo1.TextChanged
        'Consulta()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        'Consulta()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TipoAlta = 0 Then
            Guardar(False)
        Else
            Guardar(True)
        End If
    End Sub
    Private Sub Guardar(ByVal NoNuevo As Boolean)
        Try
            Dim I As New dbInventario(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            Dim idClas2 As Integer
            Dim idClas3 As Integer
            Dim ManejaSeries As Byte = 0
            Dim Inventariable As Byte = 0
            Dim RetieneIva As Byte = 0
            Dim PrecioNeto As Byte = 0
            Dim EsKit As Byte = 0
            Dim Esamortizacion As Byte = 0
            Dim SeparaKit As Byte = 0
            Dim PorLotes As Byte = 0
            Dim EsDevRev As Byte = 0
            Dim Aduana As Byte = 0
            Dim Semillas As Byte = 0
            Dim Descontinuado As Byte = 0
            Dim Rest As Byte = 0
            Dim SV As Byte = 0
            Dim SC As Byte = 0
            Dim SI As Byte = 0
            If CheckBox15.Checked Then Rest = 1
            If CheckBox5.Checked Then PrecioNeto = 1
            If CheckBox9.Checked Then SeparaKit = 1
            If CheckBox4.Checked Then RetieneIva = 1
            If CheckBox3.Checked Then Inventariable = 1
            If CheckBox7.Checked Then Esamortizacion = 1
            If CheckBox8.Checked Then EsKit = 1
            If CheckBox10.Checked Then PorLotes = 1
            If CheckBox13.Checked Then Aduana = 1
            If CheckBox12.Checked Then Semillas = 1
            If CheckBox14.Checked Then Descontinuado = 1
            If CheckBox11.Checked Then EsDevRev = 1
            If CheckBox2.Checked Then ManejaSeries = 1
            If CheckBox16.Checked Then SV = 1
            If CheckBox17.Checked Then SC = 1
            If CheckBox18.Checked Then SI = 1
            If ComboBox6.SelectedIndex = 0 Then
                idClas2 = 1
            Else
                idClas2 = IdsClas2.Valor(ComboBox6.SelectedIndex)
            End If
            If ComboBox7.SelectedIndex = 0 Then
                idClas3 = 1
            Else
                idClas3 = IdsClas3.Valor(ComboBox7.SelectedIndex)
            End If
            If IsNumeric(TextBox2.Text) = False Then
                NoErrores = False
                MensajeError = "La Cantidad debe ser un valor numérico."
                TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox3.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El contenido debe ser un valor numérico."
                TextBox3.BackColor = Color.FromArgb(250, 150, 150)
            Else
                If CDbl(TextBox3.Text) <= 0 Then
                    NoErrores = False
                    MensajeError += vbCrLf + " El contenido debe ser un valor mayor a cero."
                    TextBox3.BackColor = Color.FromArgb(250, 150, 150)
                End If
            End If
            If IsNumeric(TextBox4.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El punto de reorden debe ser un valor numérico."
                TextBox4.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un nombre al artículo."
                TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If txtCodigo1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un código al artículo."
                txtCodigo1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox7.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El costo base debe ser un valor numérico."
                TextBox7.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox18.Text) = False Then
                NoErrores = False
                MensajeError = "El peso debe ser un valor numérico."
                'TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox19.Text) = False Then
                NoErrores = False
                MensajeError = "El Mínimo debe ser un valor numérico."
                'TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox20.Text) = False Then
                NoErrores = False
                MensajeError = "La Máximo debe ser un valor numérico."
                'TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            End If
            'IEPS y RETE
            If IsNumeric(txtIEPS.Text) = False Then
                NoErrores = False
                MensajeError = "La Cantidad del IEPS debe ser un valor numérico."
                txtIEPS.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(txtIvaRetenido.Text) = False Then
                NoErrores = False
                MensajeError = "La Cantidad del IVA Retenido debe ser un valor numérico."
                txtIvaRetenido.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If Button1.Text = "Guardar" Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.InventarioAlta, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.InventarioCambio, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If

            End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    If I.ChecaClaveRepetida(txtCodigo1.Text) = False Then
                        I.Guardar(TextBox1.Text, 0, IdsTC1.Valor(ComboBox1.SelectedIndex), CDbl(TextBox3.Text), IdsTC2.Valor(ComboBox2.SelectedIndex), IdsClas.Valor(ComboBox3.SelectedIndex), TextBox5.Text, txtCodigo1.Text, CDbl(TextBox4.Text), CDbl(TextBox7.Text), 2, TextBox11.Text, idClas2, idClas3, ManejaSeries, Inventariable, CDbl(TextBox14.Text), RetieneIva, txtCodigo2.Text, TextBox17.Text, PrecioNeto, TextBox8.Text, ComboBox9.SelectedIndex, Esamortizacion, CDbl(TextBox18.Text), CDbl(TextBox20.Text), CDbl(TextBox19.Text), EsKit, SeparaKit, PorLotes, Double.Parse(txtIEPS.Text), Double.Parse(txtIvaRetenido.Text), txtExaminar.Text, EsDevRev, Aduana, Semillas, Rest, TextBox22.Text, ComboBox10.Text, TextBox23.Text, TextBox24.Text, SV, SC, SI, chkUsaUbicacion.Checked)
                        IdInventario = I.ID
                        Dim IP As New dbInventarioPrecios(MySqlcon)
                        IP.AsignaListas(IdInventario)
                        PopUp("Guardado", 60)
                        If TipoAlta = 1 Then
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                            Me.Close()
                        End If
                        If NoNuevo = False Then Nuevo()
                    Else
                        MsgBox("Ya existe un artículo con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                        txtCodigo1.BackColor = Color.FromArgb(250, 150, 150)
                    End If
                Else

                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Dim ClaveRepetida As Boolean = False
                        If txtCodigo1.Text <> ClaveAnterior Then
                            ClaveRepetida = I.ChecaClaveRepetida(txtCodigo1.Text)
                        End If
                        If ClaveRepetida = False Then
                            I.Modificar(IdInventario, TextBox1.Text, 0, IdsTC1.Valor(ComboBox1.SelectedIndex), CDbl(TextBox3.Text), IdsTC2.Valor(ComboBox2.SelectedIndex), IdsClas.Valor(ComboBox3.SelectedIndex), TextBox5.Text, txtCodigo1.Text, CDbl(TextBox4.Text), CDbl(TextBox7.Text), 2, TextBox11.Text, idClas2, idClas3, ManejaSeries, Inventariable, CDbl(TextBox14.Text), RetieneIva, txtCodigo2.Text, TextBox17.Text, PrecioNeto, TextBox8.Text, ComboBox9.SelectedIndex, Esamortizacion, CDbl(TextBox18.Text), CDbl(TextBox20.Text), CDbl(TextBox19.Text), EsKit, SeparaKit, PorLotes, Double.Parse(txtIEPS.Text), Double.Parse(txtIvaRetenido.Text), txtExaminar.Text, EsDevRev, Aduana, Semillas, Descontinuado, Rest, TextBox22.Text, ComboBox10.Text, TextBox23.Text, TextBox24.Text, SV, SC, SI, chkUsaUbicacion.Checked)
                            PopUp("Modificado", 60)
                            If TipoAlta = 1 Then
                                Me.DialogResult = Windows.Forms.DialogResult.OK
                                Me.Close()
                            End If
                            Nuevo()
                        Else
                            MsgBox("Ya existe un artículo con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                            txtCodigo1.BackColor = Color.FromArgb(250, 150, 150)
                        End If
                    End If
                End If
                txtCodigo1.Focus()
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.InventarioBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim I As New dbInventario(MySqlcon)
                    I.Eliminar(IdInventario)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    txtCodigo1.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este artículo debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        LlenaDatos(DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value)
    End Sub

    Private Sub LlenaDatos(ByVal pId As Integer)
        Try
            IdInventario = pId
            Dim I As New dbInventario(IdInventario, MySqlcon)
            Button1.Text = "Modificar"
            Button5.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            txtCodigo1.Text = I.Clave
            txtCodigo2.Text = I.Clave2
            TextBox1.Text = I.Nombre
            Button9.Visible = True
            Costo = I.CostoBase

            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.InventarioCambio, PermisosN.Secciones.Catalagos) = True Then
                btnCodigoBarras2.Enabled = True
                btnCodigoBarras1.Enabled = True
                cmbCodigoBarras1.Enabled = True
                cmbCodigoBarras2.Enabled = True
            Else
                btnCodigoBarras2.Enabled = False
                btnCodigoBarras1.Enabled = False
                cmbCodigoBarras1.Enabled = False
                cmbCodigoBarras2.Enabled = False
            End If

            If ComboBox8.SelectedIndex <> 0 Then
                TextBox2.Text = Format(I.DaInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario), "0.00")
            Else
                TextBox2.Text = Format(I.DaInventarioTodos(IdInventario), "0.00")
            End If
            TextBox3.Text = I.Contenido.ToString
            If I.Contenido <> 0 And I.Contenido <> 1 Then
                Label21.Text = CStr(CDbl(TextBox2.Text) * CDbl(I.Contenido))
                Label21.Visible = True
            Else
                Label21.Visible = False
            End If
            'txtZona.Text = I.zona
            txtExaminar.Text = I.urlImagen
            cargarImagenLeave()
            TextBox4.Text = I.PuntodeReorden.ToString
            TextBox5.Text = I.Descripcion
            ClaveAnterior = I.Clave
            TextBox7.Text = Format(I.CostoBase, "0.00")
            TextBox11.Text = I.NoParte
            TextBox14.Text = I.Iva.ToString
            txtIEPS.Text = I.ieps
            txtIvaRetenido.Text = I.ivaRetenido
            TextBox17.Text = I.Fabricante
            TextBox8.Text = I.Ubicacion
            If I.ManejaSeries = 1 Then
                CheckBox2.Checked = True
            Else
                CheckBox2.Checked = False
            End If
            If I.SepararKit = 1 Then
                CheckBox9.Checked = True
            Else
                CheckBox9.Checked = False
            End If
            If I.RetieneIva = 1 Then
                CheckBox4.Checked = True
            Else
                CheckBox4.Checked = False
            End If
            If I.PrecioNeto = 1 Then
                CheckBox5.Checked = True
            Else
                CheckBox5.Checked = False
            End If
            If I.EsAmortizacion = 1 Then
                CheckBox7.Checked = True
            Else
                CheckBox7.Checked = False
            End If
            If I.EsKit = 1 Then
                CheckBox8.Checked = True
            Else
                CheckBox8.Checked = False
            End If
            If I.PorLotes = 1 Then
                CheckBox10.Checked = True
            Else
                CheckBox10.Checked = False
            End If
            If I.esRetDev = 1 Then
                CheckBox11.Checked = True
            Else
                CheckBox11.Checked = False
            End If
            If I.Aduana = 1 Then
                CheckBox13.Checked = True
            Else
                CheckBox13.Checked = False
            End If
            If I.Semillas = 1 Then
                CheckBox12.Checked = True
            Else
                CheckBox12.Checked = False
            End If
            If I.Restaurante = 1 Then
                CheckBox15.Checked = True
            Else
                CheckBox15.Checked = False
            End If
            If I.Descontinuado = 1 Then
                CheckBox14.Checked = True
            Else
                CheckBox14.Checked = False
            End If
            If I.SoloVentas = 1 Then
                CheckBox16.Checked = True
            Else
                CheckBox16.Checked = False
            End If
            If I.SoloCompras = 1 Then
                CheckBox17.Checked = True
            Else
                CheckBox17.Checked = False
            End If
            If I.SoloInventario = 1 Then
                CheckBox18.Checked = True
            Else
                CheckBox18.Checked = False
            End If
            TextBox18.Text = I.Peso.ToString
            TextBox19.Text = I.Minimo.ToString
            TextBox20.Text = I.Maximo.ToString
            ComboBox1.SelectedIndex = IdsTC1.Busca(I.TipoCantidad.ID)
            ComboBox2.SelectedIndex = IdsTC2.Busca(I.TipoContenido.ID)
            ComboBox3.SelectedIndex = IdsClas.Busca(I.Clasificacion.ID)
            ComboBox9.SelectedIndex = I.UsaFormula
            If I.Clasificacion2.ID <= 1 Then
                ComboBox6.SelectedIndex = 0
            Else
                ComboBox6.SelectedIndex = IdsClas2.Busca(I.Clasificacion2.ID)
            End If
            If I.Clasificacion3.ID <= 1 Then
                ComboBox7.SelectedIndex = 0
            Else
                ComboBox7.SelectedIndex = IdsClas3.Busca(I.Clasificacion3.ID)
            End If
            'ComboBox5.SelectedIndex = IdsMonedas.Busca(I.CostoBaseMoneda.ID)
            ConsultaOn = True
            If I.Inventariable = 0 Then
                CheckBox3.Checked = False
            Else
                CheckBox3.Checked = True
            End If
            If CheckBox1.Checked Then
                TextBox7.Text = Format(I.DaUltimoCosto(IdInventario), "0.00")
            End If
            TextBox22.Text = I.FraccionArancel
            ComboBox10.Text = I.UnidadAduana
            TextBox23.Text = I.cProdServ
            Label40.Text = CSat.DaProductoServicio(I.cProdServ)
            TextBox24.Text = I.cUnidad
            Label41.Text = CSat.DaUnidadMedida(I.cUnidad)
            chkUsaUbicacion.Checked = I.UsaUbicacion
            ConsultaPrecios()
            If CheckBox8.Checked Then
                Label33.Visible = True
                Label33.Text = "Cantidad de kits: " + Format(I.CuantosKits(IdInventario, IdsAlmacenes.Valor(ComboBox8.SelectedIndex)), "0.00")
            Else
                Label33.Visible = False
            End If

            'Panel1.Visible = True
            'Label12.Visible = True
            'NuevoPrecio()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            LlenaDatos(DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value)
            txtCodigo1.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatos(DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value)
        End If
    End Sub
    Private Sub NuevoPrecio()
        txtPrecio.Text = "0"
        txtPrecio.BackColor = Color.FromKnownColor(KnownColor.Window)
        TextBox9.Text = ""
        txtUtilidad.Text = "0"
        TextBox21.Text = "0"
        ConsultaPrecios()
    End Sub
    Private Sub ConsultaPrecios()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then

                If DataGridView2.RowCount > 0 Then PrimerCeldaRow = DataGridView2.FirstDisplayedCell.RowIndex
                Dim I As New dbInventarioPrecios(MySqlcon)
                DataGridView2.DataSource = I.Consulta(IdInventario)
                DataGridView2.Columns(0).Visible = False
                DataGridView2.Columns(1).HeaderText = "Lista precio"
                DataGridView2.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DataGridView2.Columns(2).HeaderText = "Precio"
                DataGridView2.Columns(2).DefaultCellStyle.Format = "C2"
                DataGridView2.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridView2.Columns(2).Width = 80
                DataGridView2.Columns(3).HeaderText = "Utilidad"
                DataGridView2.Columns(3).Width = 80
                DataGridView2.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridView2.Columns(4).HeaderText = "Descuento"
                DataGridView2.Columns(4).Width = 80
                DataGridView2.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridView2.Columns(5).HeaderText = "Comentario"
                'DataGridView2.Columns(5).Width = 80
                If DataGridView2.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView2.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                DataGridView2.ClearSelection()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaDatosPrecios()
        Try
            IdPrecio = DataGridView2.Item(0, DataGridView2.CurrentCell.RowIndex).Value
            Dim IP As New dbInventarioPrecios(IdPrecio, MySqlcon)
            txtPrecio.Text = Format(IP.Precio, "0.00###")
            TextBox9.Text = IP.Comentario
            txtUtilidad.Text = Format(IP.utilidad, "0.00###")
            TextBox21.Text = Format(IP.Descuento, "0.00###")
            ComboBox4.SelectedIndex = IdsMonedas2.Busca(IP.IdMoneda)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim EsNuevo As Boolean = False
            Dim O As New dbOpciones(MySqlcon)
            Dim Con As New dbMonedasConversiones(1, MySqlcon)
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.InventarioPrecios, PermisosN.Secciones.Catalagos2) = True Then
                If Button1.Text = "Guardar" Then
                    Guardar(True)
                    EsNuevo = True
                    LlenaDatos(IdInventario)
                End If
                Dim IP As New dbInventarioPrecios(IdPrecio, MySqlcon)
                If IsNumeric(txtPrecio.Text) And IsNumeric(txtUtilidad.Text) Then
                    If O._MetodoUtilidad <> "0" And CDbl(txtUtilidad.Text) = 100 Then
                        MsgBox("La utilidad no puede ser 100.", MsgBoxStyle.Information, GlobalNombreApp)
                        Exit Sub
                    End If
                    'If Button5.Text = "Agregar" Then
                    '    IP.Guardar(CDbl(txtPrecio.Text), IdInventario, CDbl(txtUtilidad.Text), TextBox9.Text, IdsMonedas2.Valor(ComboBox4.SelectedIndex))
                    '    PopUp("Precio Agregado", 90)
                    '    NuevoPrecio()
                    'Else
                    If EsNuevo = False Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            If CDbl(TextBox21.Text) <> 0 Then
                                txtPrecio.Text = IP.DaPrecioListaUno(IdInventario).ToString
                                txtPrecio.Text = CStr(CDbl(txtPrecio.Text) - (CDbl(txtPrecio.Text) * CDbl(TextBox21.Text) / 100))
                            End If
                            Dim CostoCalculo As Double
                            If O.TipoCostoPrecios = 0 Then
                                CostoCalculo = CDbl(TextBox7.Text)
                            Else
                                Dim I As New dbInventario(MySqlcon)
                                CostoCalculo = I.DaUltimoCosto(IdInventario)
                            End If
                            If CDbl(txtUtilidad.Text) <> 0 Then
                                If O._MetodoUtilidad = "0" Then
                                    If IdsMonedas2.Valor(ComboBox4.SelectedIndex) = 2 Then
                                        txtPrecio.Text = CStr(CostoCalculo * (1 + CDbl(txtUtilidad.Text) / 100))
                                    Else
                                        txtPrecio.Text = CStr((CostoCalculo * (1 + CDbl(txtUtilidad.Text) / 100)) / Con.Cantidad)
                                    End If
                                Else
                                    If IdsMonedas2.Valor(ComboBox4.SelectedIndex) = 2 Then
                                        txtPrecio.Text = CStr(CostoCalculo / (1 - CDbl(txtUtilidad.Text) / 100))
                                    Else
                                        txtPrecio.Text = CStr((CostoCalculo / (1 - CDbl(txtUtilidad.Text) / 100)) / Con.Cantidad)
                                    End If
                                End If
                                If CheckBox5.Checked Then
                                    If CDbl(TextBox14.Text) <> 0 Then
                                        txtPrecio.Text = CStr(CDbl(txtPrecio.Text) * (1 + (CDbl(TextBox14.Text) + CDbl(txtIEPS.Text) - CDbl(txtIvaRetenido.Text)) / 100))
                                    End If
                                End If
                            End If
                            IP.Modificar(IdPrecio, CDbl(txtPrecio.Text), CDbl(txtUtilidad.Text), TextBox9.Text, IdsMonedas2.Valor(ComboBox4.SelectedIndex), CInt(TextBox21.Text), False)
                            PopUp("Precio Modificado", 90)
                            NuevoPrecio()
                        End If
                    Else
                        IP.BuscaPrecio(IdInventario, 1)
                        IdPrecio = IP.ID
                        If CDbl(txtUtilidad.Text) <> 0 Then
                            If O._MetodoUtilidad = "0" Then
                                If IdsMonedas2.Valor(ComboBox4.SelectedIndex) = 2 Then
                                    txtPrecio.Text = CStr(CDbl(TextBox7.Text) * (1 + CDbl(txtUtilidad.Text) / 100))
                                Else
                                    txtPrecio.Text = CStr((CDbl(TextBox7.Text) * (1 + CDbl(txtUtilidad.Text) / 100)) / Con.Cantidad)
                                End If
                            Else
                                If IdsMonedas2.Valor(ComboBox4.SelectedIndex) = 2 Then
                                    txtPrecio.Text = CStr(CDbl(TextBox7.Text) / (1 - CDbl(txtUtilidad.Text) / 100))
                                Else
                                    txtPrecio.Text = CStr((CDbl(TextBox7.Text) / (1 - CDbl(txtUtilidad.Text) / 100)) / Con.Cantidad)
                                End If
                            End If
                            If CheckBox5.Checked Then
                                If CDbl(TextBox14.Text) <> 0 Then
                                    txtPrecio.Text = CStr(CDbl(txtPrecio.Text) * (1 + (CDbl(TextBox14.Text) + CDbl(txtIEPS.Text) - CDbl(txtIvaRetenido.Text)) / 100))
                                End If
                            End If
                            ' txtPrecio.Text = CStr(CDbl(TextBox7.Text) * (1 + CDbl(txtUtilidad.Text) / 100))

                        End If
                        IP.Modificar(IdPrecio, CDbl(txtPrecio.Text), CDbl(txtUtilidad.Text), TextBox9.Text, IdsMonedas2.Valor(ComboBox4.SelectedIndex), CInt(TextBox21.Text), False)
                        'PopUp("Precio Modificado", 90)
                        NuevoPrecio()
                    End If
                    ' End If
                Else
                    txtPrecio.BackColor = Color.FromArgb(250, 150, 150)
                    MsgBox("El precio y utlidad deben ser un valor numérico.", MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub


    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        NuevoPrecio()
    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        LlenaDatosPrecios()
    End Sub



    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If MsgBox("¿Desea eliminar este precio?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim IP As New dbInventarioPrecios(MySqlcon)
                IP.Eliminar(IdPrecio)
                PopUp("Precio Removido", 90)
                NuevoPrecio()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DataGridView2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView2.KeyUp
        If e.KeyCode = Keys.Enter Then
            LlenaDatosPrecios()
            txtPrecio.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatosPrecios()
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If Button1.Text <> "Guardar" Then
            Dim F As New frmInventarioConsultaSeries(IdInventario)
            F.ShowDialog()
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim IdClas As Integer
        IdClas = IdsClas.Valor(ComboBox3.SelectedIndex)
        Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        ConsultaOn = False
        TextBox10.Text = IC2.Codigo
        ConsultaOn = True
        LlenaCombos("tblinventarioclasificaciones2", ComboBox6, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "General", "nombre")
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas2.Valor(ComboBox6.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            ConsultaOn = False
            TextBox12.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones3", ComboBox7, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "General", "nombre")
            HabilitaClase3(True)
        Else
            HabilitaClase3(False)
            ComboBox7.SelectedIndex = 0
            ConsultaOn = False
            TextBox12.Text = ""
            ConsultaOn = True
        End If

    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox7.SelectedIndexChanged
        If ComboBox7.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas3.Valor(ComboBox7.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            ConsultaOn = False
            TextBox13.Text = IC2.Codigo
            ConsultaOn = True
        Else
            ConsultaOn = False
            TextBox13.Text = ""
            ConsultaOn = True
        End If
    End Sub

    Private Sub TextBox10_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox10.LostFocus
        If EncontroClas = False Then
            ComboBox3.SelectedIndex = 0
            EncontroClas = True
        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            If IC.BuscaClasificacion(TextBox10.Text) Then
                EncontroClas = True
                ComboBox3.SelectedIndex = IdsClas.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub TextBox12_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox12.LostFocus
        If EncontroClas = False Then
            ComboBox6.SelectedIndex = 0
            EncontroClas = True
        End If
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            If IC.BuscaClasificacion(TextBox12.Text, IdsClas.Valor(ComboBox3.SelectedIndex)) Then
                EncontroClas = True
                ComboBox6.SelectedIndex = IdsClas2.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub TextBox13_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox13.LostFocus
        If EncontroClas = False Then
            ComboBox7.SelectedIndex = 0
            EncontroClas = True
        End If
    End Sub

    Private Sub TextBox13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox13.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            If IC.BuscaClasificacion(TextBox13.Text, IdsClas2.Valor(ComboBox6.SelectedIndex)) Then
                EncontroClas = True
                ComboBox7.SelectedIndex = IdsClas3.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub
    Private Sub HabilitaClase2(ByVal Habilita As Boolean)
        TextBox12.Enabled = Habilita
        ComboBox6.Enabled = Habilita
    End Sub
    Private Sub HabilitaClase3(ByVal Habilita As Boolean)
        TextBox13.Enabled = Habilita
        ComboBox7.Enabled = Habilita
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            Button8.Visible = True
        Else
            Button8.Visible = False
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged
        If ConsultaOn Then
            Dim I As New dbInventario(MySqlcon)
            If ComboBox8.SelectedIndex <> 0 Then
                TextBox2.Text = I.DaInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario)
            Else
                TextBox2.Text = I.DaInventarioTodos(IdInventario)
            End If
        End If
    End Sub

    Private Sub TextBox15_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodigo2.TextChanged
        'Consulta()
    End Sub

    Private Sub TextBox16_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox16.TextChanged
        Consulta()
    End Sub

    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.InventarioPrecios, PermisosN.Secciones.Catalagos2) = True Then
                If MsgBox("¿Actualizar precios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim IP As New dbInventarioPrecios(MySqlcon)
                    Dim o As New dbOpciones(MySqlcon)
                    If o.TipoCostoPrecios = 0 Then
                        IP.ActualizaPrecios(IdInventario, CDbl(TextBox7.Text), CheckBox5.Checked, CDbl(TextBox14.Text), o._MetodoUtilidad, CDbl(txtIEPS.Text), CDbl(txtIvaRetenido.Text))
                    Else
                        Dim UCosto As Double
                        Dim I As New dbInventario(MySqlcon)
                        UCosto = I.DaUltimoCosto(IdInventario)
                        IP.ActualizaPrecios(IdInventario, UCosto, CheckBox5.Checked, CDbl(TextBox14.Text), o._MetodoUtilidad, CDbl(txtIEPS.Text), CDbl(txtIvaRetenido.Text))
                    End If
                    NuevoPrecio()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If IsNumeric(TextBox3.Text) Then
            If CDbl(TextBox3.Text) <> 1 And CDbl(TextBox3.Text) <> 0 Then
                Label21.Visible = True
                Label21.Text = CStr(CDbl(TextBox2.Text) * CDbl(TextBox3.Text))
            Else
                Label21.Visible = False
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            My.Settings.repultimocosto = True
        Else
            My.Settings.repultimocosto = False
        End If
    End Sub

    Private Sub TextBox18_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked Then
            CheckBox2.Checked = False
            CheckBox2.Enabled = False
            CheckBox3.Checked = False
            CheckBox3.Enabled = False
        Else
            CheckBox2.Enabled = True
            CheckBox3.Enabled = True
        End If
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked Then
            Button7.Visible = True
            'Label34.Visible = True
            CheckBox9.Visible = True
        Else
            Button7.Visible = False
            'Label34.Visible = False
            CheckBox9.Visible = False
        End If
    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If Button1.Text = "Guardar" Then
            Guardar(True)
            'EsNuevo = True
            LlenaDatos(IdInventario)
        End If
        Dim IDe As New frmInventarioDetalles(IdInventario, 0, 0, 0)
        IDe.ShowDialog()
        IDe.Dispose()
    End Sub

    Private Sub btnExaminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExaminar.Click
        If txtExaminar.Text = "" Then
            OpenFileDialog1.InitialDirectory = "C:\"
        Else
            OpenFileDialog1.InitialDirectory = txtExaminar.Text
        End If
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtExaminar.Text = OpenFileDialog1.FileName
            pctImagenProducto.Image = Image.FromFile(OpenFileDialog1.FileName)

            ' Get the scale factor.
            ' Dim scale_factor As Single = Single.Parse(txtScale.Text)

            ' Get the source bitmap.
            Dim bm_source As New Bitmap(pctImagenProducto.Image)

            ' Make a bitmap for the result.
            Dim bm_dest As New Bitmap( _
                CInt(150), _
                CInt(120))

            ' Make a Graphics object for the result Bitmap.
            Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)

            ' Copy the source image into the destination bitmap.
            gr_dest.DrawImage(bm_source, 0, 0, _
                bm_dest.Width + 1, _
                bm_dest.Height + 1)

            ' Display the result.
            pctImagenProducto.Image = bm_dest




        End If

    End Sub

    Private Sub txtExaminar_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExaminar.Leave
        cargarImagenLeave()
    End Sub
    Private Sub cargarImagenLeave()
        Dim aux As String
        Try


            If txtExaminar.Text <> "" Then
                'txtExaminar.Text = OpenFileDialog1.FileName
                pctImagenProducto.Image = Image.FromFile(txtExaminar.Text)

                ' Get the scale factor.
                ' Dim scale_factor As Single = Single.Parse(txtScale.Text)

                ' Get the source bitmap.
                Dim bm_source As New Bitmap(pctImagenProducto.Image)

                ' Make a bitmap for the result.
                Dim bm_dest As New Bitmap( _
                    CInt(150), _
                    CInt(120))

                ' Make a Graphics object for the result Bitmap.
                Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)

                ' Copy the source image into the destination bitmap.
                gr_dest.DrawImage(bm_source, 0, 0, _
                    bm_dest.Width + 1, _
                    bm_dest.Height + 1)

                ' Display the result.
                pctImagenProducto.Image = bm_dest
            Else
                pctImagenProducto.Image = Nothing


            End If

        Catch ex As Exception
            aux = txtExaminar.Text
            txtExaminar.Text = ""
            pctImagenProducto.Image = Nothing
            MsgBox("No se puede cargar la imagen """ + aux + """ compruebe que la dirección esté bien escrita.")
        End Try
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodigo1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox15_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodigo2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        'If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
        '    e.Handled = True
        'End If

    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox11_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox11.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox17_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox17.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim IR As New frmInventarioConsultaRelaciones(IdInventario)
        IR.ShowDialog()
        IR.Dispose()
    End Sub

    Private Sub btnCrearCB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCodigoBarras2.Click
        'Dim B As New frmCodigodeBarras(IdInventario, TextBox6.Text)
        'B.ShowDialog()
        'If B.DialogResult = Windows.Forms.DialogResult.OK Then
        '    TextBox15.Text = B.Codigo
        '    LlenaDatos(IdInventario)
        'End If
        'B.Dispose()
        Dim precio As Double
        If txtPrecio.Text <> "" Then
            precio = Double.Parse(txtPrecio.Text)
        Else
            precio = 0
        End If
        'Dim frmImprimeCodigo As New frmInventarioImpresionCodigo(TextBox15.Text, IdInventario, DataGridView2.DataSource)
        'frmImprimeCodigo.Show()

        Imprimir(txtCodigo2.Text, precio, cmbCodigoBarras2.SelectedItem.Value)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles btnCodigoBarras1.Click
        Dim precio As Double
        If txtPrecio.Text <> "" Then
            precio = Double.Parse(txtPrecio.Text)
        Else
            precio = 0
        End If
        'Dim frmImprimeCodigo As New frmInventarioImpresionCodigo(TextBox6.Text, IdInventario, DataGridView2.DataSource)
        'frmImprimeCodigo.Show()

        Imprimir(txtCodigo1.Text, precio, cmbCodigoBarras1.SelectedItem.Value)
    End Sub

    Private Sub CheckBox10_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox10.CheckedChanged
        If CheckBox10.Checked Then
            Button11.Visible = True
        Else
            Button11.Visible = False
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim IR As New frmInventarioLotesConsulta(IdInventario)
        IR.ShowDialog()
        IR.Dispose()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim IR As New frmInventarioAduanaConsulta(IdInventario)
        IR.ShowDialog()
        IR.Dispose()
    End Sub

    Private Sub CheckBox13_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox13.CheckedChanged
        If CheckBox13.Checked Then
            Button12.Visible = True
        Else
            Button12.Visible = False
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.Configuracion, PermisosN.Secciones.Semillas) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If IdInventario <> 0 Then
            Dim SemConfig As New frmSemillasConfiguracion(MySqlcon, IdInventario)
            SemConfig.ShowDialog()
            SemConfig.Dispose()
        End If
    End Sub

    Private Sub CheckBox12_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox12.CheckedChanged
        If CheckBox12.Checked Then
            Button13.Visible = True
        Else
            Button13.Visible = False
        End If
    End Sub
    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim fbc As New frmBuscadorCatalogosSat(0)
        fbc.Cat = CSat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox22.Text = fbc.Clave
        End If
        fbc.Dispose()
    End Sub
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim fbc As New frmBuscadorCatalogosSat(6)
        fbc.Cat = CSat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox23.Text = fbc.Clave
            Label40.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub
    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim fbc As New frmBuscadorCatalogosSat(7)
        fbc.Cat = CSat
        fbc.CerrarCon = False
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox24.Text = fbc.Clave
            Label41.Text = fbc.Nombre
        End If
        fbc.Dispose()
    End Sub

    Private Sub Imprimir(pCodigo As String, precio As Double, documento As Integer)
        Codigo = pCodigo
        Dim printDialog1 As New PrintDialog()
        If printDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try

                Dim code As iTextSharp.text.pdf.Barcode128 = New iTextSharp.text.pdf.Barcode128
                code.Code = Codigo
                Dim image As Image = code.CreateDrawingImage(Color.Black, Color.White)
                CodigoBidimensional = New Bitmap(image)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            Dim suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)

            Dim SA As New dbSucursalesArchivos
            LlenaNodosImpresion()
            LlenaNodos(suc.ID, documento)

            PrintDocument1.PrinterSettings = printDialog1.PrinterSettings
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub LlenaNodosImpresion()
        Dim O As New dbOpciones(MySqlcon)

        ImpND.Clear()

        'Dim IdPrecio As Integer = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
        Dim IP As New dbInventarioPrecios(MySqlcon)
        Dim i As New dbInventario(idInventario, MySqlcon)
        ImpND.Add(New NodoImpresionN("", "codigo", codigo, 0), "codigo")
        ImpND.Add(New NodoImpresionN("", "nombreArticulo", i.Nombre, 0), "nombreArticulo")
        ImpND.Add(New NodoImpresionN("", "precio", Format(IP.DaPrecioListaUno(idInventario), "$#,###,##0.00"), 0), "precio")


    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        DibujaPaginaN(e.Graphics)
    End Sub

    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        ImpDb.DaZonaDetalles(TiposDocumentos.codigoBarras, GlobalIdSucursalDefault)

        DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)

    End Sub


    Private Sub DibujaPaginaEstatico(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)

        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos

        Dim MasPaginas As Boolean = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        Dim codigos As New Collection
        Dim NumeroPagina As Integer

        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try

            e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.codigoBarras, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))

        Catch ex As Exception

        End Try
        Dim Rec As RectangleF
        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer
        Dim CuantaY As Integer
        Dim Posicion As Integer
        Dim CuantosRenglones As Integer
        'Nodos Detalles            

        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim LimY As Integer
        LimY = (pZonaY + pZonaYL) / 40 * 10
        C = Posicion
        YCoord = 0
        'Calcula páginas
        CuantaY = 0
        For Each n As NodoImpresionN In ImpNDD
            If n.DataPropertyName.Contains("descripcion") Then
                CuantaY = CuantaY + 4 + InsertaEnters(n.Valor, pZonaRG, YCoord, pZonaAlt)
            End If
        Next
        If LimY > 0 Then CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 Then
                MasPaginas = True
                Exit While
            End If
            'For Each n As NodoImpresionN In ImpNDi
            '    Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
            '    If n.DataPropertyName.Contains("descripcion") And n.Renglon = 1 Then

            '    End If
            'Next
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then
                                        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        'YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, Rec, strF)
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra
            '*************Segundoo Renglon***************
            YExtra = 0
            YExtra2 = 0
            Dim HayRenglon2 As Boolean = False
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    HayRenglon2 = True
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then
                                        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        'YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            If HayRenglon2 Then YCoord = YCoord + 4 + YExtra
            '**************************************
            Posicion += 1
            C += 1
        End While


        'Nodos fijos
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "codigobi" Then
                    ncb = n
                    codigos.Add(ncb)
                End If

                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 *, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, Rec, strF)
                            End Select
                        End If
                    End If
                End If
            Next

            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName.Contains("cancelado") Then
                    e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
                End If
            Next

            'e.DrawString(ImpND("cancelado").Valor, ImpNDi("cancelado").Fuente, Brushes.Red, ImpNDi("cancelado").X / 40 * 10, ImpNDi("cancelado").Y / 40 * 10)
            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

                YCoord = 0
                'Ivas Sin Retenciones
                C = 0
                If niva2.Visible = 1 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                        If niva2.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

            End If
            For Each n As NodoImpresionN In codigos

                If n.Visible = 1 Then e.DrawImage(CodigoBidimensional, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
            Next
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Function InsertaEnters(ByVal Cadena As String, ByVal CadaCuantos As Integer, ByVal Y As Integer, ByVal AumentoY As Integer) As Integer
        Dim C As Integer
        C = 0
        Dim CC As Integer = 0
        Dim car As String
        Dim Yx As Integer = 0
        While C < Cadena.Length
            car = Cadena.Substring(C, 1)
            If car = Chr(13) Or CC = CadaCuantos Or car = Chr(10) Then
                If car = Chr(13) Then C += 1
                Yx += AumentoY
                CC = 0
            Else
                CC += 1
            End If
            C += 1
        End While
        Return Yx
    End Function

    Private Sub LlenaNodos(ByVal pidSucursal As Integer, ByVal pDocumento As Integer)
        Dim I As New dbImpresionesN(MySqlcon)
        Dim Fs As FontStyle
        ImpNDi.Clear()
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
                ImpNDi.Add(New NodoImpresionN(dr("id"), dr("x"), dr("y"), dr("xl"), dr("yl"), dr("texto"), dr("datapropertyname"), New Font(dr("fuente").ToString, CSng(dr("fuentesize")), Fs, GraphicsUnit.Point), dr("alineacion"), dr("tipo"), dr("tipodato"), dr("visible"), dr("documento"), dr("tiponodo"), dr("idsucursal"), dr("conetiqueta"), dr("nombre"), dr("renglon"), dr("clasificacion")))
            End While
            dr.Close()
        Catch ex As Exception

        End Try

    End Sub
End Class