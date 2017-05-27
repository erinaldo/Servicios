Public Class frmNotariosPublicos
    Dim IdsInmueble As New elemento
    Dim IdsEstado As New elemento
    Dim IdsEntidadFed As New elemento
    Dim IdsPais As New elemento
    Dim dt As New DataTable
    Dim dt2 As New DataTable
    Dim I As New dbNotariosPublicos(MySqlcon)
    Dim EnajenandteRow As Integer
    Dim AdquirienteRow As Integer
    Dim ID As Integer
    Dim resultado As String
    Dim idFactura As Integer
    Dim tblImuebles As New DataTable
    Dim idInmueble As Integer
    Dim inmuebleRow As Integer
    Dim cadenaOriginal As String

    Public Sub New(ByVal pidFactura As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        idFactura = pidFactura
        
    End Sub

    Private Sub frmNotariosPublicos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        cmbCoPropiedad.SelectedIndex = 0
        cmbCoPropiedadAdquiriente.SelectedIndex = 0
        LlenaCombos("tbltipoinmueble", cmbTipoInmueble, "nombre", "nombret", "ID", IdsInmueble)
        LlenaCombos("tblestados", cmbEstado, "estado", "nombret", "ID", IdsEstado)
        With cmbPais
            .DataSource = I.llenarPais()
            .DisplayMember = "nombre"
            .ValueMember = "clave"
            .SelectedIndex = 141
        End With

        LlenaCombos("tblestados", cmbEntidadFederativa, "estado", "nombret", "ID", IdsEntidadFed)
        cmbEstado.SelectedIndex = 25
        cmbEntidadFederativa.SelectedIndex = 25
        dt.Columns.Add("Nombre")
        dt.Columns.Add("A. Paterno")
        dt.Columns.Add("A. Materno")
        dt.Columns.Add("RFC")
        dt.Columns.Add("CURP")
        dt.Columns.Add("Porcenaje")

        dgvEnajenantes.DataSource = dt
        dt2.Columns.Add("Nombre")
        dt2.Columns.Add("A. Paterno")
        dt2.Columns.Add("A. Materno")
        dt2.Columns.Add("RFC")
        dt2.Columns.Add("CURP")
        dt2.Columns.Add("Porcenaje")
        dgvAdquirietes.DataSource = dt2

        'tblImuebles.Columns.Add("TipoInmueble")
        'tblImuebles.Columns.Add("Estado")
        tblImuebles.Columns.Add("Tipo Inmueble")
        tblImuebles.Columns.Add("Calle")
        tblImuebles.Columns.Add("No. Exterior")
        tblImuebles.Columns.Add("no. Interior")
        tblImuebles.Columns.Add("Colonia")
        tblImuebles.Columns.Add("Localidad")
        tblImuebles.Columns.Add("Referencia")
        tblImuebles.Columns.Add("Municipio")
        tblImuebles.Columns.Add("Estado")
        tblImuebles.Columns.Add("País")
        tblImuebles.Columns.Add("Código Postal")
        tblImuebles.Columns.Add("tipoInmueble")
        tblImuebles.Columns.Add("valorEstado")
        tblImuebles.Columns.Add("valorPais")
        dgvInmuebles.DataSource = tblImuebles
        'LlenaCombos("tblpais", cmbPais, "nombre", "nombret", "clave", IdsPais)
        txtCalle.Focus()
        ID = I.HayDatosNotarios(idFactura)
        If ID <> 0 Then
            llenarTODOSdatos()
            txtCalle.Focus()
        End If
        ' estados()
    End Sub

    Private Sub txtCP_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCP.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            'tpDatosEnajenante.SelectedIndex = 1
            'txtNumInstrumNota.Focus()
            GuardarInmueble()
            nuevoInmueble()
        Else
            Dim textBox As TextBox = DirectCast(sender, TextBox)
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
                e.Handled = True
            End If
        End If
        
    End Sub

    Private Sub txtNumInstrumNota_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumInstrumNota.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMonto_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMonto.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMonto_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMonto.Leave
        Dim x As Double
        If txtMonto.Text = "." Then
            txtMonto.Text = "0.00"
        End If
        If txtMonto.Text <> "" Then
          
            x = Double.Parse(txtMonto.Text)
            txtMonto.Text = Format(x, "0.00")
        End If
    End Sub

    Private Sub txtSubTotal_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubTotal.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtSubTotal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSubTotal.Leave
        Dim x As Double
        If txtSubTotal.Text = "." Then
            txtSubTotal.Text = "0.00"
        End If
        If txtSubTotal.Text <> "" Then
          
            x = Double.Parse(txtSubTotal.Text)
            txtSubTotal.Text = Format(x, "0.00")
        End If
    End Sub

    Private Sub txtIVA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIVA.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            tpDatosEnajenante.SelectedIndex = 2
            txtCURP.Focus()
        Else
            Dim textBox As TextBox = DirectCast(sender, TextBox)
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
                e.Handled = True
            End If
        End If
       
    End Sub

    Private Sub txtIVA_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIVA.Leave
        Dim x As Double
        If txtIVA.Text = "." Then
            txtIVA.Text = "0.00"
        End If
        If txtIVA.Text <> "" Then
          
            x = Double.Parse(txtIVA.Text)
            txtIVA.Text = Format(x, "0.00")
        End If
    End Sub

    Private Sub txtCURP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCURP.KeyPress
        If Char.IsLower(e.KeyChar) And txtCURP.Text.Length <= 17 Then
            txtCURP.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
      
    End Sub

    Private Sub txtNumNotaria_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumNotaria.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtNumNotaria_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumNotaria.Leave
        If txtNumNotaria.Text <> "" Then
            If Integer.Parse(txtNumNotaria.Text) = 0 Then
                MsgBox("El valor mínimo para No. Notaría es: 1.", MsgBoxStyle.OkOnly, "Pull System Soft")
                txtNumNotaria.Focus()
                txtNumNotaria.SelectAll()
            End If
        End If
       
    End Sub

    Private Sub txtNumInstrumNota_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumInstrumNota.Leave
        If txtNumInstrumNota.Text <> "" Then
            If Integer.Parse(txtNumInstrumNota.Text) = 0 Then
                MsgBox("El valor mínimo para No. Inst. Notarial es: 1.", MsgBoxStyle.OkOnly, "Pull System Soft")
                txtNumInstrumNota.Focus()
                txtNumInstrumNota.SelectAll()
            End If
        End If
    End Sub

    Private Sub txtRFCUno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsLower(e.KeyChar) And txtRFCUno.Text.Length <= 12 Then
            txtRFCUno.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtRFCUno_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ValidarRFC(txtRFCUno.Text) = 1 And txtRFCUno.Text <> "" Then
            MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRFCUno.Focus()
            txtRFCUno.SelectAll()
        End If
        'If txtRFC.Text = "" Then
        '    txtRFC.Text = "XXXXXXXXXXXXX"
        'End If
    End Sub

    Public Function ValidarRFC(ByVal cadena As String) As Integer
        Dim i As Integer = 0
        Dim confirmacion As Boolean = True
        If cadena.Length > 11 And cadena.Length < 14 Then
            If cadena.Length = 12 Then
                cadena = "-" + cadena
                i = 1
            End If

            For j As Integer = i To 3
                If Char.IsLetter(cadena(j)) = False Then 'creo que aqui tiene que ser verdadero
                    confirmacion = False
                End If
            Next
            For j As Integer = 4 To 9
                If Char.IsDigit(cadena(j)) = False Then
                    confirmacion = False

                End If
            Next
            For j As Integer = 9 To 12
                If Char.IsLetterOrDigit(cadena(j)) = False Then
                    confirmacion = False

                End If

            Next


        Else
            confirmacion = False
        End If
        If (confirmacion = False) Then
            'no es valido
            Return 1
        Else
            ' si es valido
            Return 0
        End If
    End Function


    Private Sub txtPorcentaje_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorcentaje.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPorcentaje_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPorcentaje.Leave
        Dim x As Double
        If txtPorcentaje.Text = "." Then
            txtPorcentaje.Text = "0.00"
        End If
        If txtPorcentaje.Text = "" Then
            txtPorcentaje.Text = "0.00"
        Else
            x = Double.Parse(txtPorcentaje.Text)
            txtPorcentaje.Text = Format(x, "0.00")
        End If

        If txtPorcentaje.Text <> "" Then
            If Double.Parse(txtPorcentaje.Text) > 100 Then
                MsgBox("El valor máximo para el porcentaje es: 100.", MsgBoxStyle.OkOnly, "Pull System Soft")
                txtPorcentaje.Focus()
                txtPorcentaje.SelectAll()
            End If
        End If
    End Sub

    Private Sub cmbCoPropiedad_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCoPropiedad.SelectedIndexChanged
        If cmbCoPropiedad.SelectedIndex = 0 Then
            pnlEnajenantesCop.Visible = False
            pnlUnEnajenante.Visible = True
            txtNombreEnajenante.Focus()
        Else
            pnlEnajenantesCop.Visible = True
            pnlUnEnajenante.Visible = False
            txtNombreCOP.Focus()
        End If
    End Sub


    Private Sub txtRFCUnAdqui_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRFCUnAdqui.KeyPress
        If Char.IsLower(e.KeyChar) And txtRFCUnAdqui.Text.Length <= 12 Then
            txtRFCUnAdqui.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtRFCUnAdqui_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRFCUnAdqui.Leave
        If ValidarRFC(txtRFCUnAdqui.Text) = 1 And txtRFCUnAdqui.Text <> "" Then
            MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRFCUnAdqui.Focus()
            txtRFCUnAdqui.SelectAll()
        End If
    End Sub

    Private Sub txtPorcentajeAdquirienteCop_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorcentajeAdquirienteCop.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPorcentajeAdquirienteCop_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPorcentajeAdquirienteCop.Leave
        Dim x As Double
        If txtPorcentajeAdquirienteCop.Text = "." Then
            txtPorcentajeAdquirienteCop.Text = "0.00"
        End If
        If txtPorcentajeAdquirienteCop.Text = "" Then
            txtPorcentajeAdquirienteCop.Text = "0.00"
        Else
            x = Double.Parse(txtPorcentajeAdquirienteCop.Text)
            txtPorcentajeAdquirienteCop.Text = Format(x, "0.00")
        End If

        If txtPorcentajeAdquirienteCop.Text <> "" Then
            If Double.Parse(txtPorcentajeAdquirienteCop.Text) > 100 Then
                MsgBox("El valor máximo para el porcentaje es: 100.", MsgBoxStyle.OkOnly, "Pull System Soft")
                txtPorcentajeAdquirienteCop.Focus()
                txtPorcentajeAdquirienteCop.SelectAll()
            End If
        End If
    End Sub

    Private Sub cmbCoPropiedadAdquiriente_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCoPropiedadAdquiriente.SelectedIndexChanged
        If cmbCoPropiedadAdquiriente.SelectedIndex = 0 Then
            pnlUnAdquiriente.Visible = True
            pnlAdquirienteCop.Visible = False
            txtNombreUnAdqui.Focus()
        Else
            pnlUnAdquiriente.Visible = False
            pnlAdquirienteCop.Visible = True
            txtNombreAdquirienteCop.Focus()
        End If
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If checaErrores() = False Then
            Dim adquiriente As Boolean
            Dim enajenante As Boolean

            If cmbCoPropiedad.SelectedIndex = 0 Then
                enajenante = False
            Else
                enajenante = True
            End If

            If cmbCoPropiedadAdquiriente.SelectedIndex = 0 Then
                adquiriente = False
            Else
                adquiriente = True
            End If
            If btnGuardar.Text = "Guardar" Then
                Dim x As String = cmbPais.SelectedValue

                I.Guardar(Integer.Parse(txtNumInstrumNota.Text), dtpFecha.Value.ToString("dd/MM/yyyy"), Double.Parse(txtMonto.Text), Double.Parse(txtSubTotal.Text), Double.Parse(txtIVA.Text), txtCURP.Text, Integer.Parse(txtNumNotaria.Text), IdsEntidadFed.Valor(cmbEntidadFederativa.SelectedIndex), txtAdscripcion.Text, enajenante, adquiriente, idFactura, txtNombreNotario.Text)
                ID = I.ID

                For j As Integer = 0 To dgvInmuebles.RowCount - 1
                    I.guardarInmuebles(ID, dgvInmuebles.Item(11, j).Value(), dgvInmuebles.Item(1, j).Value(), dgvInmuebles.Item(2, j).Value(), dgvInmuebles.Item(3, j).Value(), dgvInmuebles.Item(4, j).Value(), dgvInmuebles.Item(5, j).Value(), dgvInmuebles.Item(6, j).Value(), dgvInmuebles.Item(7, j).Value(), dgvInmuebles.Item(12, j).Value(), dgvInmuebles.Item(9, j).Value(), dgvInmuebles.Item(10, j).Value())

                Next
                If enajenante = False Then
                    'sing

                    I.GuardarEnajenante(txtNombreEnajenante.Text, txtApellidoPEnajenante.Text, txtApellidoMaternoEnajenante.Text, txtRFCEnajenante.Text, txtCURPEnajenante.Text, ID)
                Else
                    'pklu
                    For j As Integer = 0 To dt.Rows.Count - 1
                        I.GuardarEnajenantes(dt.Rows(j)(0).ToString(), dt.Rows(j)(1).ToString(), dt.Rows(j)(2).ToString(), dt.Rows(j)(3).ToString(), dt.Rows(j)(4).ToString(), Double.Parse(dt.Rows(j)(5).ToString()), ID)
                    Next

                End If

                If adquiriente = False Then
                    'sing

                    I.GuardarAdquiriente(txtNombreUnAdqui.Text, txtApellidoPaternoUnAdqui.Text, txtApellidoMaternoUnAdqui.Text, txtRFCUnAdqui.Text, txtCURPUnAdqui.Text, ID)
                Else
                    'plu
                    For j As Integer = 0 To dt2.Rows.Count - 1
                        I.GuardarAdquirientes(dt2.Rows(j)(0).ToString(), dt2.Rows(j)(1).ToString(), dt2.Rows(j)(2).ToString(), dt2.Rows(j)(3).ToString(), dt2.Rows(j)(4).ToString(), Double.Parse(dt2.Rows(j)(5).ToString()), ID)
                    Next

                End If
                btnGuardar.Text = "Modificar"
                'resultado = I.Impresion(ID)
                'cadenaOriginal = I.cadenaOriginal(ID)
                'llenardatos()

            Else
                'MODIFICAR
                I.Modificar(ID, Integer.Parse(txtNumInstrumNota.Text), dtpFecha.Value.ToString("dd/MM/yyyy"), Double.Parse(txtMonto.Text), Double.Parse(txtSubTotal.Text), Double.Parse(txtIVA.Text), txtCURP.Text, Integer.Parse(txtNumNotaria.Text), IdsEntidadFed.Valor(cmbEntidadFederativa.SelectedIndex), txtAdscripcion.Text, enajenante, adquiriente, txtNombreNotario.Text)
                I.Eliminarinmuebles(ID)

                For j As Integer = 0 To dgvInmuebles.RowCount - 1
                    I.guardarInmuebles(ID, dgvInmuebles.Item(11, j).Value().ToString, dgvInmuebles.Item(1, j).Value(), dgvInmuebles.Item(2, j).Value(), dgvInmuebles.Item(3, j).Value(), dgvInmuebles.Item(4, j).Value(), dgvInmuebles.Item(5, j).Value(), dgvInmuebles.Item(6, j).Value(), dgvInmuebles.Item(7, j).Value(), dgvInmuebles.Item(12, j).Value(), dgvInmuebles.Item(9, j).Value(), dgvInmuebles.Item(10, j).Value())

                Next
                If enajenante = False Then
                    'plural
                    I.EliminarEnajenante(ID)
                    I.GuardarEnajenante(txtNombreEnajenante.Text, txtApellidoPEnajenante.Text, txtApellidoMaternoEnajenante.Text, txtRFCEnajenante.Text, txtCURPEnajenante.Text, ID)
                Else
                    'singular
                    I.EliminarEnajenantes(ID)
                    For j As Integer = 0 To dt.Rows.Count - 1
                        I.GuardarEnajenantes(dt.Rows(j)(0).ToString(), dt.Rows(j)(1).ToString(), dt.Rows(j)(2).ToString(), dt.Rows(j)(3).ToString(), dt.Rows(j)(4).ToString(), Double.Parse(dt.Rows(j)(5).ToString()), ID)
                    Next

                End If

                If adquiriente = False Then
                    'plural
                    I.EliminarAdquiriente(ID)
                    I.GuardarAdquiriente(txtNombreUnAdqui.Text, txtApellidoPaternoUnAdqui.Text, txtApellidoMaternoUnAdqui.Text, txtRFCUnAdqui.Text, txtCURPUnAdqui.Text, ID)
                Else
                    'singular
                    I.EliminarAdquirientes(ID)
                    For j As Integer = 0 To dt2.Rows.Count - 1
                        I.GuardarAdquirientes(dt2.Rows(j)(0).ToString(), dt2.Rows(j)(1).ToString(), dt2.Rows(j)(2).ToString(), dt2.Rows(j)(3).ToString(), dt2.Rows(j)(4).ToString(), Double.Parse(dt2.Rows(j)(5).ToString()), ID)
                    Next

                End If

            End If
            ' llenardatos()
            'resultado = I.Impresion(ID)
            'cadenaOriginal = I.cadenaOriginal(ID)
            'If MsgBox("¿Desea imprimir el reporte?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            '    imprimirReporte()
            'End If
            Me.Close()

        End If

    End Sub
    Private Function checaErrores() As Boolean
        Dim HayError As Boolean = False
        Dim MsgError As String = ""

       
        txtNumInstrumNota.BackColor = Color.White
        txtMonto.BackColor = Color.White
        txtSubTotal.BackColor = Color.White
        txtIVA.BackColor = Color.White
        txtCURP.BackColor = Color.White
        txtNumNotaria.BackColor = Color.White
        txtNombreEnajenante.BackColor = Color.White
        txtApellidoPEnajenante.BackColor = Color.White
        txtRFCEnajenante.BackColor = Color.White
        txtCURPEnajenante.BackColor = Color.White
        txtNombreUnAdqui.BackColor = Color.White
        txtRFCUnAdqui.BackColor = Color.White

        If dgvInmuebles.RowCount <= 0 Then
            MsgError += vbCrLf + "No se han registrado Inmuebles en 'Descripción Inmueble'."
            HayError = True
        End If
        If txtNumInstrumNota.Text = "" Then
            MsgError += vbCrLf + "El campo Número de instrumento notarial en 'Datos Operación' es requerido."
            HayError = True
            txtNumInstrumNota.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtMonto.Text = "" Then
            MsgError += vbCrLf + "El campo Monto en 'Datos Operación' es requerido."
            HayError = True
            txtMonto.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtSubTotal.Text = "" Then
            MsgError += vbCrLf + "El campo SubTotal en 'Datos Operación' es requerido."
            HayError = True
            txtSubTotal.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtIVA.Text = "" Then
            MsgError += vbCrLf + "El campo IVA en 'Datos Operación' es requerido."
            HayError = True
            txtIVA.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtCURP.Text = "" Then
            MsgError += vbCrLf + "El campo CURP en 'Datos Notario' es requerido."
            HayError = True
            txtCURP.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtNumNotaria.Text = "" Then
            MsgError += vbCrLf + "El campo Número Notaría en 'Datos Notario' es requerido."
            HayError = True
            txtNumNotaria.BackColor = Color.FromArgb(250, 150, 150)
        End If

        If cmbCoPropiedad.SelectedIndex = 0 And txtNombreEnajenante.Text = "" Then
            MsgError += vbCrLf + "El campo Nombre en 'Datos Enajenante' es requerido."
            HayError = True
            txtNombreEnajenante.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If cmbCoPropiedad.SelectedIndex = 0 And txtApellidoPEnajenante.Text = "" Then
            MsgError += vbCrLf + "El campo Apellido en 'Datos Enajenante' es requerido."
            HayError = True
            txtApellidoPEnajenante.BackColor = Color.FromArgb(250, 150, 150)
        End If

        If cmbCoPropiedad.SelectedIndex = 0 And txtRFCEnajenante.Text = "" Then
            MsgError += vbCrLf + "El campo RFC en 'Datos Enajenante' es requerido."
            HayError = True
            txtRFCEnajenante.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If cmbCoPropiedad.SelectedIndex = 1 And dgvEnajenantes.RowCount() <= 0 Then
            MsgError += vbCrLf + "No se han registrado enajenantes en 'Datos Enajenante'."
            HayError = True
        End If


        If cmbCoPropiedad.SelectedIndex = 0 And txtCURPEnajenante.Text = "" Then
            MsgError += vbCrLf + "El campo CURP en 'Datos Enajenante' es requerido."
            HayError = True
            txtCURPEnajenante.BackColor = Color.FromArgb(250, 150, 150)
        End If

        If cmbCoPropiedadAdquiriente.SelectedIndex = 0 And txtNombreUnAdqui.Text = "" Then
            MsgError += vbCrLf + "El campo Nombre en 'Datos Adquiriente' es requerido."
            HayError = True
            txtNombreUnAdqui.BackColor = Color.FromArgb(250, 150, 150)
        End If

        If cmbCoPropiedadAdquiriente.SelectedIndex = 0 And txtRFCUnAdqui.Text = "" Then
            MsgError += vbCrLf + "El campo RFC en 'Datos Adquiriente' es requerido."
            HayError = True
            txtRFCUnAdqui.BackColor = Color.FromArgb(250, 150, 150)
        End If

        If cmbCoPropiedadAdquiriente.SelectedIndex = 1 And dgvAdquirietes.RowCount <= 0 Then
            MsgError += vbCrLf + "No se han registrado enajenantes en 'Datos Adquiriente'."
            HayError = True

        End If
      
        If HayError = True Then
            MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
        End If
        Return HayError
    End Function

    Private Sub btnAgregarAdquirientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarAdquirientes.Click
        Dim HayError As Boolean = False
        Dim MsgError As String = ""
        txtNombreAdquirienteCop.BackColor = Color.White
        txtRFCAdquirienteCop.BackColor = Color.White
        If txtNombreAdquirienteCop.Text = "" Then
            MsgError += vbCrLf + "El campo Nombre en 'Datos Adquiriente' es requerido."
            HayError = True
            txtNombreAdquirienteCop.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtRFCAdquirienteCop.Text = "" Then
            MsgError += vbCrLf + "El campo RFC en 'Datos Adquiriente' es requerido."
            HayError = True
            txtRFCAdquirienteCop.BackColor = Color.FromArgb(250, 150, 150)
        End If

        If HayError = False Then


            If btnAgregarAdquirientes.Text = "Agregar" Then

                Dim dr As DataRow
                dr = dt2.NewRow()
                dr("Nombre") = txtNombreAdquirienteCop.Text
                dr("A. Paterno") = txtApellidoPaternoAdquirienteCop.Text
                dr("A. Materno") = txtApellidoMaternoAdquirienteCop.Text
                dr("RFC") = txtRFCAdquirienteCop.Text
                dr("CURP") = txtCURPadquirienteCOp.Text
                dr("Porcenaje") = txtPorcentajeAdquirienteCop.Text

                dt2.Rows.Add(dr)
                NuevoAdquiriente()
            Else
                dt2.Rows.Remove(dt2.Rows(AdquirienteRow))
                Dim dr As DataRow
                dr = dt2.NewRow()
                dr("Nombre") = txtNombreAdquirienteCop.Text
                dr("A. Paterno") = txtApellidoPaternoAdquirienteCop.Text
                dr("A. Materno") = txtApellidoMaternoAdquirienteCop.Text
                dr("RFC") = txtRFCAdquirienteCop.Text
                dr("CURP") = txtCURPadquirienteCOp.Text
                dr("Porcenaje") = txtPorcentajeAdquirienteCop.Text

                dt2.Rows.Add(dr)
                NuevoAdquiriente()
                'MODIFICAR


            End If
            txtNombreAdquirienteCop.Focus()

        Else
            MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
        End If


    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim HayError As Boolean = False
        Dim MsgError As String = ""
        txtNombreCOP.BackColor = Color.White
        txtRFCCop.BackColor = Color.White

        If txtNombreCOP.Text = "" Then
            MsgError += vbCrLf + "El campo Nombre en 'Datos Enajenate' es requerido."
            HayError = True
            txtNombreCOP.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtRFCCop.Text = "" Then
            MsgError += vbCrLf + "El campo RFC en 'Datos Enajenate' es requerido."
            HayError = True
            txtRFCCop.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If HayError = False Then
            If btnAgregar.Text = "Agregar" Then
                Dim dr As DataRow
                dr = dt.NewRow()
                dr("Nombre") = txtNombreCOP.Text
                dr("A. Paterno") = txtApellidoCop.Text
                dr("A. Materno") = txtApellidoMaterno.Text
                dr("RFC") = txtRFCCop.Text
                dr("CURP") = txtCURPCop.Text
                dr("Porcenaje") = txtPorcentaje.Text

                dt.Rows.Add(dr)

                NuevoEnajenate()

            Else
                'MODIFICAR
                dt.Rows.Remove(dt.Rows(EnajenandteRow))
                Dim dr As DataRow
                dr = dt.NewRow()
                dr("Nombre") = txtNombreCOP.Text
                dr("A. Paterno") = txtApellidoCop.Text
                dr("A. Materno") = txtApellidoMaterno.Text
                dr("RFC") = txtRFCCop.Text
                dr("CURP") = txtCURPCop.Text
                dr("Porcenaje") = txtPorcentaje.Text

                dt.Rows.Add(dr)

                NuevoEnajenate()
            End If




            txtNombreCOP.Focus()
        Else
            MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
        End If
        'despues de guardar

    End Sub

    Private Sub txtRFCAdquirienteCop_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRFCAdquirienteCop.Leave
        If ValidarRFC(txtRFCAdquirienteCop.Text) = 1 And txtRFCAdquirienteCop.Text <> "" Then
            MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRFCAdquirienteCop.Focus()
            txtRFCAdquirienteCop.SelectAll()
        End If
    End Sub

    Private Sub txtRFCAdquirienteCop_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRFCAdquirienteCop.KeyPress
        If Char.IsLower(e.KeyChar) And txtRFCAdquirienteCop.Text.Length <= 12 Then
            txtRFCAdquirienteCop.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtCURPUnAdqui_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCURPUnAdqui.KeyPress
        If Char.IsLower(e.KeyChar) And txtCURPUnAdqui.Text.Length <= 17 Then
            txtCURPUnAdqui.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtCURPadquirienteCOp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCURPadquirienteCOp.KeyPress
        If Char.IsLower(e.KeyChar) And txtCURPadquirienteCOp.Text.Length <= 17 Then
            txtCURPadquirienteCOp.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtRFCEnajenante_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRFCEnajenante.KeyPress
        If Char.IsLower(e.KeyChar) And txtRFCEnajenante.Text.Length <= 12 Then
            txtRFCEnajenante.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtRFCEnajenante_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRFCEnajenante.Leave
        If ValidarRFC(txtRFCEnajenante.Text) = 1 And txtRFCEnajenante.Text <> "" Then
            MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRFCEnajenante.Focus()
            txtRFCEnajenante.SelectAll()
        End If
    End Sub

    Private Sub txtCURPEnajenante_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCURPEnajenante.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then

            tpDatosEnajenante.SelectedIndex = 4
            cmbCoPropiedadAdquiriente.Focus()
        Else
            If Char.IsLower(e.KeyChar) And txtCURPEnajenante.Text.Length <= 17 Then
                txtCURPEnajenante.SelectedText = Char.ToUpper(e.KeyChar)
                e.Handled = True
            End If
        End If
        
    End Sub

    Private Sub txtRFCCop_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRFCCop.KeyPress
        If Char.IsLower(e.KeyChar) And txtRFCCop.Text.Length <= 12 Then
            txtRFCCop.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtRFCCop_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRFCCop.Leave
        If ValidarRFC(txtRFCCop.Text) = 1 And txtRFCCop.Text <> "" Then
            MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRFCCop.Focus()
            txtRFCCop.SelectAll()
        End If
    End Sub

    Private Sub txtCURPCop_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCURPCop.KeyPress
        If Char.IsLower(e.KeyChar) And txtCURPCop.Text.Length <= 17 Then
            txtCURPCop.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub cmbEntidadFederativa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEntidadFederativa.SelectedIndexChanged
        txtAdscripcion.Focus()
    End Sub

    Private Sub cmbEntidadFederativa_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbEntidadFederativa.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtAdscripcion.Focus()
        End If
    End Sub

    Private Sub dtpFecha_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpFecha.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtMonto.Focus()
        End If
    End Sub

    Private Sub cmbTipoInmueble_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbTipoInmueble.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtCalle.Focus()
        End If
    End Sub

    Private Sub cmbEstado_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbEstado.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            cmbPais.Focus()
        End If
    End Sub

    Private Sub cmbPais_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbPais.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            txtCP.Focus()
        End If
    End Sub

    Private Sub txtAdscripcion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdscripcion.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            tpDatosEnajenante.SelectedIndex = 3
            cmbCoPropiedad.Focus()
        End If
    End Sub

    Private Sub btnNuevoAdquiriente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoAdquiriente.Click
        NuevoAdquiriente()
    End Sub

    Private Sub btnNuevoEnajenate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoEnajenate.Click
        NuevoEnajenate()
    End Sub
    Private Sub NuevoAdquiriente()
        txtNombreAdquirienteCop.Text = ""
        txtApellidoPaternoAdquirienteCop.Text = ""
        txtApellidoMaternoAdquirienteCop.Text = ""
        txtRFCAdquirienteCop.Text = ""
        txtCURPadquirienteCOp.Text = ""
        txtPorcentajeAdquirienteCop.Text = "0.00"
        btnEliminarAdquiriente.Enabled = False
        btnAgregarAdquirientes.Text = "Agregar"
        txtNombreAdquirienteCop.Focus()
    End Sub
    Private Sub NuevoEnajenate()
        txtNombreCOP.Text = ""
        txtApellidoCop.Text = ""
        txtApellidoMaterno.Text = ""
        txtRFCCop.Text = ""
        txtCURPCop.Text = ""
        txtPorcentaje.Text = "0.00"
        btnEliminarenajenante.Enabled = False
        btnAgregar.Text = "Agregar"
        txtNombreCOP.Focus()
    End Sub

    Private Sub dgvEnajenantes_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEnajenantes.CellClick

        NuevoEnajenate()
        EnajenandteRow = dgvEnajenantes.CurrentCell.RowIndex
        txtNombreCOP.Text = dgvEnajenantes.Item(0, dgvEnajenantes.CurrentCell.RowIndex).Value()
        txtApellidoCop.Text = dgvEnajenantes.Item(1, dgvEnajenantes.CurrentCell.RowIndex).Value()
        txtApellidoMaterno.Text = dgvEnajenantes.Item(2, dgvEnajenantes.CurrentCell.RowIndex).Value()
        txtRFCCop.Text = dgvEnajenantes.Item(3, dgvEnajenantes.CurrentCell.RowIndex).Value()
        txtCURPCop.Text = dgvEnajenantes.Item(4, dgvEnajenantes.CurrentCell.RowIndex).Value()
        txtPorcentaje.Text = dgvEnajenantes.Item(5, dgvEnajenantes.CurrentCell.RowIndex).Value()
        btnEliminarenajenante.Enabled = True
        btnAgregar.Text = "Modificar"
    End Sub

    Private Sub btnEliminarenajenante_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarenajenante.Click
        Try
            If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

                dt.Rows.Remove(dt.Rows(EnajenandteRow))
                dgvEnajenantes.DataSource = dt
                NuevoEnajenate()
                PopUp("Enajenante Eliminado", 90)
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

    Private Sub dgvAdquirietes_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAdquirietes.CellClick
        NuevoAdquiriente()
        AdquirienteRow = dgvAdquirietes.CurrentCell.RowIndex
        txtNombreAdquirienteCop.Text = dgvAdquirietes.Item(0, dgvAdquirietes.CurrentCell.RowIndex).Value()
        txtApellidoPaternoAdquirienteCop.Text = dgvAdquirietes.Item(1, dgvAdquirietes.CurrentCell.RowIndex).Value()
        txtApellidoMaternoAdquirienteCop.Text = dgvAdquirietes.Item(2, dgvAdquirietes.CurrentCell.RowIndex).Value()
        txtRFCAdquirienteCop.Text = dgvAdquirietes.Item(3, dgvAdquirietes.CurrentCell.RowIndex).Value()
        txtCURPadquirienteCOp.Text = dgvAdquirietes.Item(4, dgvAdquirietes.CurrentCell.RowIndex).Value()
        txtPorcentajeAdquirienteCop.Text = dgvAdquirietes.Item(5, dgvAdquirietes.CurrentCell.RowIndex).Value()
        btnEliminarAdquiriente.Enabled = False
        btnAgregarAdquirientes.Text = "Modificar"
    End Sub

    Private Sub btnEliminarAdquiriente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarAdquiriente.Click



        Try
            If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

                dt2.Rows.Remove(dt2.Rows(AdquirienteRow))
                dgvAdquirietes.DataSource = dt2

                NuevoAdquiriente()
                PopUp("Adquiriente Eliminado", 90)
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub llenardatos()
        Dim aux As String
        resultado = ""
        I.LlenaDatos(ID)


        resultado += "<NotariosPublicos Version:""1.0"" xmlns:notariospublicos=""http://www.sat.gob.mx/notariospublicos"">" + vbCrLf
        'DESCRIPCION INMUEBLE
        resultado += "<notariospublicos:DescInmuebles TipoInmueble=""" + Integer.Parse(I.TipoInmueble).ToString("00") + """ Calle=""" + I.Calle + """  "
        If I.NoExterior <> "" Then
            resultado += " NoExterior=""" + I.NoExterior + """"
        End If
        If I.NoInterior <> "" Then
            resultado += " NoInterior=""" + I.NoInterior + """"
        End If
        If I.Colonia <> "" Then
            resultado += " Colonia=""" + I.Colonia + """"
        End If
        If I.Localidad <> "" Then
            resultado += " Localidad=""" + I.Localidad + """"
        End If
        If I.Referencia <> "" Then
            resultado += " Referencia=""" + I.Referencia + """"
        End If
        resultado += " Municipio=""" + I.Municipio + """ Estado=""" + I.encontrarEstado(I.Estado) + """ Pais=""" + I.Pais + """ CodigoPostal=""" + I.CodigoPostal + """/>" + vbCrLf
        'resultado += "</notariospublicos:DescInmuebles>" + vbCrLf
        'FIN DESCRIPCION INMUEBLE
        ' DatosOperacion

        resultado += "<notariospublicos:DatosOperacion NumInstrumentoNotaria=""" + I.NumInstrumentoNotarial.ToString + """ FechaInstNotarial=""" + I.FechaInstNotarial + """ MontoOperacion=""" + I.MontoOperacion.ToString("0.00") + """ Subtotal=""" + I.Subtotal.ToString("0.00") + """ IVA=""" + I.IVA.ToString("0.00") + """/>" + vbCrLf
        'resultado += "</notariospublicos:DatosOperacion >" + vbCrLf
        'fin  DatosOperacion

        'INICIA DATOS NOTARIO
        resultado += "<notariospublicos:DatosNotario CURP=""" + I.CURP + """ NumNotaria=""" + I.NumNotaria.ToString + """ EntidadFederativa=""" + I.encontrarEstado(I.EntidadFederativa) + """"
        If I.Adscripcion <> "" Then
            resultado += " Adscripcion=""" + I.Adscripcion + """"
        End If
        resultado += "/>" + vbCrLf
        'FIN DE DATOS NOTARIO

        'DATOS ENAJENANTE
        If I.CoproSocConyugalE = False Then
            aux = "No"
        Else
            aux = "Si"
        End If
        resultado += "<notariospublicos:DatosEnajenante CoproSocConyugalE=""" + aux + """>" + vbCrLf
        If I.CoproSocConyugalE = False Then
            'un enajenante
            resultado += "<notariospublicos:DatosUnEnajenante Nombre=""" + I.dtEnajenante.Rows(0)(1).ToString + """ ApellidoPaterno=""" + I.dtEnajenante.Rows(0)(2).ToString + """"
            If I.dtEnajenante.Rows(0)(3).ToString <> "" Then
                resultado += " ApellidoMaterno=""" + I.dtEnajenante.Rows(0)(3).ToString + """"
            End If
            resultado += " RFC=""" + I.dtEnajenante.Rows(0)(4).ToString + """ CURP=""" + I.dtEnajenante.Rows(0)(5).ToString + """ />" + vbCrLf
        Else
            For j As Integer = 0 To I.dtEnajenante.Rows.Count() - 1
                resultado += "<notariospublicos:DatosEnajenantesCopSC Nombre=""" + I.dtEnajenante.Rows(j)(1).ToString + """"
                If I.dtEnajenante.Rows(j)(2).ToString <> "" Then
                    resultado += " ApellidoPaterno=""" + I.dtEnajenante.Rows(0)(2).ToString + """"
                End If
                If I.dtEnajenante.Rows(j)(3).ToString <> "" Then
                    resultado += " ApellidoMaterno=""" + I.dtEnajenante.Rows(0)(3).ToString + """"
                End If
                resultado += " RFC=""" + I.dtEnajenante.Rows(j)(4).ToString + """"
                If I.dtEnajenante.Rows(j)(5).ToString <> "" Then
                    resultado += " CURP=""" + I.dtEnajenante.Rows(0)(5).ToString + """"
                End If
                resultado += " Porcentaje=""" + Double.Parse(I.dtEnajenante.Rows(j)(6).ToString).ToString("0.00") + """/>" + vbCrLf
            Next






        End If



        resultado += "</notariospublicos:DatosEnajenante> " + vbCrLf

        'TERMINA DATOS ENAJENANTE
        'EMPIEZA : DatosAdquiriente

        If I.CoproSocConyugalE2 = False Then
            aux = "No"
        Else
            aux = "Si"
        End If
        resultado += "<notariospublicos:DatosAdquiriente CoproSocConyugalE=""" + aux + """>" + vbCrLf
        If I.CoproSocConyugalE2 = False Then

            resultado += "<notariospublicos:DatosUnAdquiriente Nombre=""" + I.dtAdquiriente.Rows(0)(1).ToString + """"
            If I.dtAdquiriente.Rows(0)(2).ToString <> "" Then
                resultado += "ApellidoPaterno=""" + I.dtAdquiriente.Rows(0)(2).ToString + """"
            End If

            If I.dtAdquiriente.Rows(0)(3).ToString <> "" Then
                resultado += " ApellidoMaterno=""" + I.dtAdquiriente.Rows(0)(3).ToString + """"
            End If
            resultado += " RFC=""" + I.dtAdquiriente.Rows(0)(4).ToString + """"
            If I.dtAdquiriente.Rows(0)(5).ToString <> "" Then
                resultado += " CURP=""" + I.dtAdquiriente.Rows(0)(5).ToString + """"
            End If
            resultado += " />" + vbCrLf

        Else

            For j As Integer = 0 To I.dtAdquiriente.Rows.Count() - 1
                resultado += "<notariospublicos:DatosAdquirientesCopSC Nombre=""" + I.dtAdquiriente.Rows(j)(1).ToString + """"
                If I.dtAdquiriente.Rows(j)(2).ToString <> "" Then
                    resultado += " ApellidoPaterno=""" + I.dtAdquiriente.Rows(j)(2).ToString + """"
                End If
                If I.dtAdquiriente.Rows(j)(3).ToString <> "" Then
                    resultado += " ApellidoMaterno=""" + I.dtAdquiriente.Rows(j)(3).ToString + """"
                End If
                resultado += " RFC=""" + I.dtAdquiriente.Rows(j)(4).ToString + """"
                If I.dtAdquiriente.Rows(j)(5).ToString <> "" Then
                    resultado += " CURP=""" + I.dtAdquiriente.Rows(j)(5).ToString + """"
                End If
                resultado += " Porcentaje=""" + Double.Parse(I.dtAdquiriente.Rows(j)(6).ToString).ToString("0.00") + """/>" + vbCrLf
            Next

        End If


        resultado += "</notariospublicos:DatosAdquiriente> " + vbCrLf


        resultado += "</NotariosPublicos>"

    End Sub
    Private Sub llenarTODOSdatos()
        I.LlenaDatos(ID)
        txtNombreNotario.Text = I.NombreNotario
        txtNumInstrumNota.Text = I.NumInstrumentoNotarial.ToString
        dtpFecha.Value = I.FechaInstNotarial
        txtMonto.Text = I.MontoOperacion.ToString("0.00")
        txtSubTotal.Text = I.Subtotal.ToString("0.00")
        txtIVA.Text = I.IVA.ToString("0.00")
        txtCURP.Text = I.CURP
        txtNumNotaria.Text = I.NumNotaria
        cmbEntidadFederativa.SelectedIndex = IdsEntidadFed.Busca(I.EntidadFederativa)
        txtAdscripcion.Text = I.Adscripcion
        If I.CoproSocConyugalE = False Then
            cmbCoPropiedad.SelectedIndex = 0
            txtNombreEnajenante.Text = I.dtEnajenante.Rows(0)(1).ToString
            txtApellidoPEnajenante.Text = I.dtEnajenante.Rows(0)(2).ToString
            txtApellidoMaternoEnajenante.Text = I.dtEnajenante.Rows(0)(3).ToString
            txtRFCEnajenante.Text = I.dtEnajenante.Rows(0)(4).ToString
            txtCURPEnajenante.Text = I.dtEnajenante.Rows(0)(5).ToString
        Else
            cmbCoPropiedad.SelectedIndex = 1
            For j As Integer = 0 To I.dtEnajenante.Rows.Count - 1
                Dim dr As DataRow
                dr = dt.NewRow()
                dr("Nombre") = I.dtEnajenante.Rows(j)(1).ToString
                dr("A. Paterno") = I.dtEnajenante.Rows(j)(2).ToString
                dr("A. Materno") = I.dtEnajenante.Rows(j)(3).ToString
                dr("RFC") = I.dtEnajenante.Rows(j)(4).ToString
                dr("CURP") = I.dtEnajenante.Rows(j)(5).ToString
                dr("Porcenaje") = I.dtEnajenante.Rows(j)(6).ToString

                dt.Rows.Add(dr)
            Next
            dgvEnajenantes.DataSource = dt


        End If
        If I.CoproSocConyugalE2 = False Then
            cmbCoPropiedadAdquiriente.SelectedIndex = 0
            txtNombreUnAdqui.Text = I.dtAdquiriente.Rows(0)(1).ToString
            txtApellidoPaternoUnAdqui.Text = I.dtAdquiriente.Rows(0)(2).ToString
            txtApellidoMaternoUnAdqui.Text = I.dtAdquiriente.Rows(0)(3).ToString
            txtRFCUnAdqui.Text = I.dtAdquiriente.Rows(0)(4).ToString
            txtCURPUnAdqui.Text = I.dtAdquiriente.Rows(0)(5).ToString

        Else
            cmbCoPropiedadAdquiriente.SelectedIndex = 1
            For j As Integer = 0 To I.dtAdquiriente.Rows.Count - 1
                Dim dr As DataRow
                dr = dt2.NewRow()
                dr("Nombre") = I.dtAdquiriente.Rows(j)(1).ToString
                dr("A. Paterno") = I.dtAdquiriente.Rows(j)(2).ToString
                dr("A. Materno") = I.dtAdquiriente.Rows(j)(3).ToString
                dr("RFC") = I.dtAdquiriente.Rows(j)(4).ToString
                dr("CURP") = I.dtAdquiriente.Rows(j)(5).ToString
                dr("Porcenaje") = I.dtAdquiriente.Rows(j)(6).ToString

                dt2.Rows.Add(dr)
            Next
            dgvAdquirietes.DataSource = dt2
        End If
        nuevoInmueble()
        tblImuebles.Clear()
        'tblImuebles = I.dtInmuebles
        For j As Integer = 0 To I.dtInmuebles.Rows.Count - 1
            Dim dr As DataRow
            dr = tblImuebles.NewRow()
            dr("Tipo Inmueble") = I.encontrartipoInmueble(I.dtInmuebles.Rows(j)(2).ToString)
            dr("Calle") = I.dtInmuebles.Rows(j)(3).ToString
            dr("No. Exterior") = I.dtInmuebles.Rows(j)(4).ToString
            dr("no. Interior") = I.dtInmuebles.Rows(j)(5).ToString
            dr("Colonia") = I.dtInmuebles.Rows(j)(6).ToString
            dr("Localidad") = I.dtInmuebles.Rows(j)(7).ToString
            dr("Referencia") = I.dtInmuebles.Rows(j)(8).ToString
            dr("Municipio") = I.dtInmuebles.Rows(j)(9).ToString
            dr("Estado") = I.encontrarEstado(I.dtInmuebles.Rows(j)(10) - 1)
            dr("País") = I.dtInmuebles.Rows(j)(11).ToString
            dr("Código Postal") = I.dtInmuebles.Rows(j)(12).ToString
            dr("tipoInmueble") = I.dtInmuebles.Rows(j)(2)
            dr("valorEstado") = I.dtInmuebles.Rows(j)(10).ToString
            dr("valorPais") = I.dtInmuebles.Rows(j)(11)
            tblImuebles.Rows.Add(dr)
        Next
        dgvInmuebles.DataSource = tblImuebles
        dgvInmuebles.Columns(11).Visible = False
        dgvInmuebles.Columns(12).Visible = False
        dgvInmuebles.Columns(13).Visible = False
        btnGuardar.Text = "Modificar"

    End Sub


 

    Private Sub btnNuevoInmueble_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoInmueble.Click
        nuevoInmueble()
    End Sub
    Private Sub nuevoInmueble()
        txtCalle.Text = ""
        txtNoExt.Text = ""
        txtNoInt.Text = ""
        txtColonia.Text = ""
        txtLocalidad.Text = ""
        txtReferencia.Text = ""
        txtMunicipio.Text = ""
        txtCP.Text = ""
        btnGuardarInmueble.Text = "Agregar"
        btnEliminarInmueble.Enabled = False
        txtCalle.Focus()
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        txtNumNotaria.Text = ""
        txtMonto.Text = ""
        dtpFecha.Value = Date.Now
        txtMonto.Text = ""
        txtSubTotal.Text = ""
        txtIVA.Text = ""
        txtCURP.Text = ""
        txtNumNotaria.Text = ""
        txtAdscripcion.Text = ""
        txtNombreEnajenante.Text = ""
        txtApellidoPEnajenante.Text = ""
        txtApellidoMaternoEnajenante.Text = ""
        txtRFCEnajenante.Text = ""
        txtCURPEnajenante.Text = ""
        txtNombreCOP.Text = ""
        txtApellidoCop.Text = ""
        txtApellidoMaterno.Text = ""
        txtRFCCop.Text = ""
        txtCURPCop.Text = ""
        txtPorcentaje.Text = "0.00"
        btnEliminarenajenante.Enabled = False
        btnAgregar.Text = "Agregar"
        dt.Clear()
        dgvEnajenantes.DataSource = Nothing
        cmbCoPropiedad.SelectedIndex = 0
        txtNombreAdquirienteCop.Text = ""
        txtApellidoPaternoAdquirienteCop.Text = ""
        txtApellidoMaternoAdquirienteCop.Text = ""
        txtRFCAdquirienteCop.Text = ""
        txtCURPadquirienteCOp.Text = ""
        txtPorcentajeAdquirienteCop.Text = "0.00"
        btnEliminarAdquiriente.Enabled = False
        btnAgregarAdquirientes.Text = "Agregar"
        dt2.Clear()
        dgvAdquirietes.DataSource = Nothing
        txtNombreUnAdqui.Text = ""
        txtApellidoPaternoUnAdqui.Text = ""
        txtApellidoMaternoUnAdqui.Text = ""
        txtRFCUnAdqui.Text = ""
        txtCURPUnAdqui.Text = ""
        cmbCoPropiedadAdquiriente.SelectedIndex = 0


        tblImuebles.Clear()
        dgvInmuebles.DataSource = Nothing
        txtCalle.Text = ""
        txtNoExt.Text = ""
        txtNoInt.Text = ""
        txtColonia.Text = ""
        txtLocalidad.Text = ""
        txtReferencia.Text = ""
        txtMunicipio.Text = ""
        txtCP.Text = ""
        btnGuardarInmueble.Text = "Guardar"
        btnEliminarInmueble.Enabled = False
        txtCalle.Focus()

    End Sub

    Private Sub btnGuardarInmueble_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardarInmueble.Click
        GuardarInmueble()

    End Sub
    Private Sub GuardarInmueble()
        Dim HayError As Boolean = False
        Dim MsgError As String = ""


        txtCalle.BackColor = Color.White
        txtMunicipio.BackColor = Color.White
        txtCP.BackColor = Color.White

        If txtCalle.Text = "" Then
            MsgError += vbCrLf + "El campo Calle en 'Descripción Inmueble' es requerido."
            HayError = True
            txtCalle.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtMunicipio.Text = "" Then
            MsgError += vbCrLf + "El campo Municipio en 'Descripción Inmueble' es requerido."
            HayError = True
            txtMunicipio.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtCP.Text = "" Then
            MsgError += vbCrLf + "El campo Código Postal en 'Descripción Inmueble' es requerido."
            HayError = True
            txtCP.BackColor = Color.FromArgb(250, 150, 150)
        Else
            If txtCP.Text.Length() <> 5 Then
                MsgError += vbCrLf + "El campo Código Postal en 'Descripción Inmueble' está incompleto."
                HayError = True
                txtCP.BackColor = Color.FromArgb(250, 150, 150)
            End If
        End If
        If HayError = False Then
            If btnGuardarInmueble.Text = "Agregar" Then
                Dim dr As DataRow
                dr = tblImuebles.NewRow()
                dr("Tipo Inmueble") = cmbTipoInmueble.Text
                dr("Calle") = txtCalle.Text
                dr("No. Exterior") = txtNoExt.Text
                dr("no. Interior") = txtNoInt.Text
                dr("Colonia") = txtColonia.Text
                dr("Localidad") = txtLocalidad.Text
                dr("Referencia") = txtReferencia.Text
                dr("Municipio") = txtMunicipio.Text
                dr("Estado") = cmbEstado.Text
                dr("País") = cmbPais.SelectedValue
                dr("Código Postal") = txtCP.Text
                dr("tipoInmueble") = IdsInmueble.Valor(cmbTipoInmueble.SelectedIndex)
                dr("valorEstado") = IdsEstado.Valor(cmbEstado.SelectedIndex)
                dr("valorPais") = cmbPais.SelectedValue
                tblImuebles.Rows.Add(dr)


                ' I.guardarInmuebles(ID, IdsInmueble.Valor(cmbTipoInmueble.SelectedIndex), txtCalle.Text, txtNoExt.Text, txtNoInt.Text, txtColonia.Text, txtLocalidad.Text, txtReferencia.Text, txtMunicipio.Text, IdsEstado.Valor(cmbEstado.SelectedIndex), cmbPais.SelectedValue, txtCP.Text)
                nuevoInmueble()
                ' tblImuebles = I.consultaInmuebles(ID)
                dgvInmuebles.DataSource = tblImuebles
                dgvInmuebles.Columns(11).Visible = False
                dgvInmuebles.Columns(12).Visible = False
                dgvInmuebles.Columns(13).Visible = False
                PopUp("Inmueble Guardado", 90)
            Else
                tblImuebles.Rows.Remove(tblImuebles.Rows(inmuebleRow))
                Dim dr As DataRow
                dr = tblImuebles.NewRow()
                dr("Tipo Inmueble") = cmbTipoInmueble.Text
                dr("Calle") = txtCalle.Text
                dr("No. Exterior") = txtNoExt.Text
                dr("no. Interior") = txtNoInt.Text
                dr("Colonia") = txtColonia.Text
                dr("Localidad") = txtLocalidad.Text
                dr("Referencia") = txtReferencia.Text
                dr("Municipio") = txtMunicipio.Text
                dr("Estado") = cmbEstado.Text
                dr("País") = cmbPais.SelectedValue
                dr("Código Postal") = txtCP.Text
                dr("tipoInmueble") = IdsInmueble.Valor(cmbTipoInmueble.SelectedIndex)
                dr("valorEstado") = IdsEstado.Valor(cmbEstado.SelectedIndex)
                dr("valorPais") = cmbPais.SelectedValue
                tblImuebles.Rows.Add(dr)
                ' I.ModificarInmuebles(idInmueble, IdsInmueble.Valor(cmbTipoInmueble.SelectedIndex), txtCalle.Text, txtNoExt.Text, txtNoInt.Text, txtColonia.Text, txtLocalidad.Text, txtReferencia.Text, txtMunicipio.Text, IdsEstado.Valor(cmbEstado.SelectedIndex), cmbPais.SelectedValue, txtCP.Text)
                nuevoInmueble()
                ' tblImuebles = I.consultaInmuebles(ID)
                dgvInmuebles.DataSource = tblImuebles
                dgvInmuebles.Columns(11).Visible = False
                dgvInmuebles.Columns(12).Visible = False
                dgvInmuebles.Columns(13).Visible = False
                PopUp("Inmueble Modificado", 90)
            End If

        Else
            MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)

        End If


    End Sub
    Private Sub dgvInmuebles_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInmuebles.CellClick
        If dgvInmuebles.RowCount > 0 Then




            nuevoInmueble()
            inmuebleRow = dgvInmuebles.CurrentCell.RowIndex
            'idInmueble = dgvInmuebles.Item(0, dgvInmuebles.CurrentCell.RowIndex).Value()
            ' I.LlenaDatosInmueble(idInmueble)
            cmbTipoInmueble.SelectedIndex = IdsInmueble.Busca(dgvInmuebles.Item(11, dgvInmuebles.CurrentCell.RowIndex).Value())
            txtCalle.Text = dgvInmuebles.Item(1, dgvInmuebles.CurrentCell.RowIndex).Value()
            txtNoExt.Text = dgvInmuebles.Item(2, dgvInmuebles.CurrentCell.RowIndex).Value()
            txtNoInt.Text = dgvInmuebles.Item(3, dgvInmuebles.CurrentCell.RowIndex).Value()
            txtColonia.Text = dgvInmuebles.Item(4, dgvInmuebles.CurrentCell.RowIndex).Value()
            txtLocalidad.Text = dgvInmuebles.Item(5, dgvInmuebles.CurrentCell.RowIndex).Value()
            txtReferencia.Text = dgvInmuebles.Item(6, dgvInmuebles.CurrentCell.RowIndex).Value()
            txtMunicipio.Text = dgvInmuebles.Item(7, dgvInmuebles.CurrentCell.RowIndex).Value()
            cmbEstado.SelectedIndex = IdsEstado.Busca(dgvInmuebles.Item(12, dgvInmuebles.CurrentCell.RowIndex).Value())
            cmbPais.SelectedValue = dgvInmuebles.Item(13, dgvInmuebles.CurrentCell.RowIndex).Value
            txtCP.Text = dgvInmuebles.Item(10, dgvInmuebles.CurrentCell.RowIndex).Value()
            btnEliminarInmueble.Enabled = True
            btnGuardarInmueble.Text = "Modificar"

        End If
    End Sub

    Private Sub btnEliminarInmueble_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarInmueble.Click

        Try
            If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

                tblImuebles.Rows.Remove(tblImuebles.Rows(inmuebleRow))
                dgvInmuebles.DataSource = tblImuebles
                dgvInmuebles.Columns(11).Visible = False
                dgvInmuebles.Columns(12).Visible = False
                dgvInmuebles.Columns(13).Visible = False

                nuevoInmueble()
                PopUp("Inmueble Eliminado", 90)
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub imprimirReporte()
        I.LlenaDatos(ID)
        Dim ventas As New dbVentas(idFactura, MySqlcon, "")
        ventas.DaDatosTimbrado(idFactura)

        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

        Rep = New repNotariosPublicos
        Rep.Subreports.Item("repInmuebles").SetDataSource(I.seleccionarInmueble(ID))
        Rep.Subreports.Item("repEnajenante").SetDataSource(I.dtImpresionEnajenante)
        Rep.Subreports.Item("repAdquiriente").SetDataSource(I.dtimpresionAdquiriente)

        Rep.SetParameterValue("serie", ventas.Serie)
        Rep.SetParameterValue("folio", ventas.Folio)
        Rep.SetParameterValue("folioFiscal", ventas.uuid)
        Rep.SetParameterValue("nombreNotario", I.NombreNotario)
        Rep.SetParameterValue("noNotaria", I.NumNotaria)
        Rep.SetParameterValue("CURPNotario", I.CURP)
        Rep.SetParameterValue("entidadFNotario", Integer.Parse(I.EntidadFederativa).ToString("00") + " - " + I.encontrarEstado(I.EntidadFederativa))
        Rep.SetParameterValue("noInstrumento", I.NumInstrumentoNotarial)
        Rep.SetParameterValue("montoOperacion", I.MontoOperacion.ToString("C2"))
        Rep.SetParameterValue("fechaFirma", I.FechaInstNotarial)
        Rep.SetParameterValue("subTotal", I.Subtotal.ToString("C2"))
        Rep.SetParameterValue("IVA", I.IVA.ToString("C2"))



        'rpt.Subreports.Item("nombredelreporte.rpt").SetDataSource("SELECT * FROM Tabla")
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    'private Sub estados()
    '    Dim x As String = "01 AGUASCALIENTES-02 BAJA CALIFORNIA NORTE-03 BAJA CALIFORNIA SUR-04 CAMPECHE-05 COAHUILA-06 COLIMA-07 CHIAPAS-08 CHIHUAHUA-09 DISTRITO FEDERAL-10 DURANGO-11 GUANAJUATO-12 GUERRERO-13 HIDALGO-14 JALISCO-15 MEXICO-16 MICHOACAN-17 MORELOS-18 NAYARIT-19 NUEVO LEON-20 OAXACA-21 PUEBLA-22 QUERETARO-23 QUINTANA ROO-24 SAN LUIS POTOSI-25 SINALOA-26 SONORA-27 TABASCO-28 TAMAULIPAS-29 TLAXCALA-30 VERACRUZ-31 YUCATAN-32 ZACATECAS"
    '    Dim num As String = ""
    '    Dim estado As String = ""
    '    Dim resultado As String = ""
    '    Dim contador As Integer = 0
    '    Dim asc As Boolean = True
    '    For j As Integer = 0 To x.Length() - 1
    '        asc = True

    '        If contador > 2 Then
    '            If x.Chars(j) = "-" Then
    '                resultado += "if(toNumber({tblInmuebles.Estado})=" + num + ") then" + vbCrLf
    '                resultado += """" + num + " - " + estado + ".""" + vbCrLf
    '                resultado += "else" + vbCrLf
    '                contador = 0
    '                num = ""
    '                estado = ""
    '                asc = False
    '            Else
    '                estado += x.Chars(j)
    '            End If

    '        End If
    '        If contador = 2 Then
    '            contador += 1
    '        End If
    '        If contador < 2 And asc Then
    '            num += x.Chars(j)
    '            contador += 1
    '        End If
    '    Next
    'End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If btnGuardar.Text <> "Guardar" Then
            imprimirReporte()
        Else
            MsgBox("Es necesario guardar antes de imprimir.", MsgBoxStyle.OkOnly, GlobalNombreApp)
        End If

    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub txtCURP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCURP.TextChanged

    End Sub
End Class