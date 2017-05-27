Public Class frmGastosProgramables
    Dim tipo As Integer
    Dim tipoBusqueda As Integer
    Dim nombre As String
    Dim fecha As String
    Dim dias As Integer
    Dim frecuencia As String
    Dim horas As Integer
    Dim minutos As Integer
    Dim inicio As String
    Dim fin As String
    Dim ID As Integer
    Dim consulta As Boolean = True
    Dim estado As Boolean
    Dim datos As New dbGastosProgramables(MySqlcon)

    Private Sub frmGastosProgramables_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        consulta = False
        'cmbFecuenciaHoras.SelectedIndex = 4
        cmbTipo.SelectedIndex = 0
        cmbDia.SelectedIndex = 1
        consulta = True
        busqueda()
    End Sub
    Private Sub rdbPagoConstante_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPagoConstante.CheckedChanged
        If rdbPagoConstante.Checked = True Then
            pnlPagoConstante.Visible = True
            pnlPagoUnico.Visible = False
            grpPeriodo.Visible = False
            GroupBox2.Visible = False
        Else
            pnlPagoConstante.Visible = False
            pnlPagoUnico.Visible = True
            grpPeriodo.Visible = False
            GroupBox2.Visible = False
        End If
    End Sub
    Private Sub chkPeriodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPeriodo.CheckedChanged
        If chkPeriodo.Checked = True Then
            dtpInicio.Enabled = False
            dtpFinal.Enabled = False
        Else
            dtpInicio.Enabled = True
            dtpFinal.Enabled = True
        End If
    End Sub
    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""
        txtDescripcion.BackColor = Color.White

        If chkEstado.Checked = True Then
            estado = True
        Else
            estado = False
        End If
        If rdbPagoConstante.Checked = True Then
            tipo = 0
            fecha = cmbDia.SelectedIndex
            If cmbDia.SelectedIndex.ToString.Length = 1 Then
                fecha = "0" + fecha
            End If
        Else
            tipo = 1
            fecha = dtpFecha.Value.ToString("yyyy/MM/dd")

        End If
        If txtDescripcion.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + "Debe indicar un nombre para el gasto."
            txtDescripcion.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If chkPeriodo.Checked = True Then
            inicio = "0000/00/00"
            fin = "9999/12/30"
        Else
            inicio = dtpInicio.Value.ToString("yyyy/MM/dd")
            fin = dtpFinal.Value.ToString("yyyy/MM/dd")
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosProgramarAlta, PermisosN.Secciones.Gastos) = False And btnGuardar.Text = "Guardar" Then
            NoErrores = False
            MensajeError += " No tiene permiso para realizar esta operación."
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosProgramarCambios, PermisosN.Secciones.Gastos) = False And btnGuardar.Text <> "Guardar" Then
            NoErrores = False
            MensajeError += " No tiene permiso para realizar esta operación."
        End If
        If NoErrores = True Then
            If btnGuardar.Text = "Guardar" Then
                datos.Guardar(tipo, txtDescripcion.Text, fecha, nmrDias.Value, inicio, fin, estado)
                ID = datos.ID
                btnGuardar.Text = "Modificar"
                btnEliminar.Enabled = True
                nuevo()
                PopUp("Guardado", 90)
            Else
                If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    'modificar
                    datos.Modificar(ID, tipo, txtDescripcion.Text, fecha, nmrDias.Value, inicio, fin, estado)
                    nuevo()
                    PopUp("Modificado", 90)
                End If
            End If
        End If
        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub
    Private Sub busqueda()
        If consulta Then


            Dim tabla As DataTable
            If cmbTipo.SelectedIndex = 0 Then
                tipoBusqueda = 3
            End If
            If cmbTipo.SelectedIndex = 1 Then
                tipoBusqueda = 0
            End If
            If cmbTipo.SelectedIndex = 2 Then
                tipoBusqueda = 1
            End If

            tabla = datos.busqueda(txtBusqueda.Text, tipoBusqueda)
            DGDetalles.DataSource = tabla
            DGDetalles.Columns(1).HeaderText = "Nombre"
            DGDetalles.Columns(2).HeaderText = "Tipo"
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'DGDetalles.Rows(0).Visible = False

        End If
    End Sub
    Private Sub txtBusqueda_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBusqueda.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
        busqueda()
    End Sub
    Private Sub cmbTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged
        busqueda()
    End Sub
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevo()
    End Sub
    Private Sub nuevo()
        If chkMantenerDatos.Checked = False Then
            txtDescripcion.Text = ""
            chkPeriodo.Checked = True
            dtpFecha.Value = Date.Now
            btnGuardar.Text = "Guardar"
            txtDescripcion.Focus()
            cmbDia.SelectedIndex = 1
            nmrDias.Value = 0
            chkEstado.Checked = True
        Else
            btnGuardar.Text = "Guardar"
            txtDescripcion.Text = ""
            txtDescripcion.Focus()
        End If
        btnEliminar.Enabled = False
        busqueda()

    End Sub
    Private Sub DGDetalles_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        llenaDatos()
    End Sub
    Private Sub llenaDatos()
        ID = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
        datos.LlenaDatos(ID)
        If datos.tipo = 0 Then
            rdbPagoConstante.Checked = True
            cmbDia.SelectedIndex = Integer.Parse(datos.fecha)
            grpPeriodo.Visible = False
            'If datos.inicio = "0000/00/00" Then
            '    chkPeriodo.Checked = True
            'Else
            '    dtpInicio.Value = datos.inicio
            '    dtpFinal.Value = datos.fin
            '    chkPeriodo.Checked = False
            'End If
        Else
            rdbPagoUnico.Checked = True
            dtpFecha.Value = datos.fecha
        End If
        txtDescripcion.Text = datos.nombre
        nmrDias.Value = datos.dias
        'cmbFecuenciaHoras.SelectedIndex = datos.frecuencia
        btnGuardar.Text = "Modificar"
        btnEliminar.Enabled = True
        If datos.estado = True Then
            chkEstado.Checked = True
        Else
            chkEstado.Checked = False
        End If
    End Sub
    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosProgramarBajas, PermisosN.Secciones.Gastos) = True Then

                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    datos.Eliminar(ID)
                    PopUp("Eliminado", 90)
                    busqueda()
                    nuevo()

                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Private Sub txtDescripcion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescripcion.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub
End Class