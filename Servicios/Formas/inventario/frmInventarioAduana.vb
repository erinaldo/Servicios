Public Class frmInventarioAduana
    Dim IdAlmacen As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Dim CantidadAsignado As Double
    Dim CantidadAnterior As Double
    Dim Aduana As New dbInventarioAduana(MySqlcon)
    Dim Cantidad As Double
    Dim Existencia As Double
    Dim Modo As Byte
    Dim AduanaSeleccionado As Integer
    Dim IdDocumento As Integer
    Dim QueDocumento As Byte
    Dim CSat As dbCatalogosSat
    Dim xSat As New elemento
    'Dim IdsSucursales As New elemento
    Private Sub frmAlmacenes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            CSat = New dbCatalogosSat
            CSat.IniciarMySQL(My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
            CSat.LlenaCombos("tbladuanas", ComboBox8, "concat(clave,' ',descripcion)", "nombrem", "clave", xSat, , , , True)
            CSat.Con.Close()
            Nuevo()
            'Label3.Text = Cantidad.ToString + " de  artículos por asignar."
            If Aduana.IdDetalleVenta <> 0 Or Aduana.IdDetalleRemisionV <> 0 Then
                ComboBox8.Enabled = False
                TextBox1.Enabled = False
                DateTimePicker1.Enabled = False
                Label5.Text = "Importaciones asignadas a la venta."
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Public Sub New(ByVal pIddetalleVenta As Integer, ByVal pIdDetalleRemisionV As Integer, ByVal pIdDetalleCompra As Integer, ByVal pIdDetalleRemisionC As Integer, ByVal pCantidad As Double, ByVal pIdInventario As Integer, ByVal pModo As Byte, pIdDetalleDevolucionC As Integer, pIdDetalleDevolucionV As Integer, pIdDetalleMovimiento As Integer, pIdAlmacen As Integer, pAlmacenStr As String, pTipoMov As Byte, pIdDocumento As Integer, pQueDocumento As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Aduana.IdDetalleRemisionV = pIdDetalleRemisionV
        Aduana.IdDetalleCompra = pIdDetalleCompra
        Aduana.IdDetalleVenta = pIddetalleVenta
        Aduana.idDetalleRemisionC = pIdDetalleRemisionC
        Aduana.IdDetalleDevolucioC = pIdDetalleDevolucionC
        Aduana.idDetalleDevolucionV = pIdDetalleDevolucionV
        Aduana.idDetalleMovimiento = pIdDetalleMovimiento
        Aduana.IdAlmacen = pIdAlmacen
        Aduana.IdInventario = pIdInventario
        Aduana.TipoMov = pTipoMov
        Cantidad = pCantidad
        Modo = pModo
        IdDocumento = pIdDocumento
        QueDocumento = pQueDocumento
        Label9.Text = "Almacén: " + pAlmacenStr
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            TextBox4.Text = ""
            TextBox1.Text = ""
            TextBox4.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            CantidadAnterior = 0
            Button2.Enabled = False
            ConsultaOn = True
            Consulta()
            TextBox2.Text = CStr(Cantidad - CantidadAsignado)
            ConsultaAduanas()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                'Dim P As New dblotes(MySqlcon)
                DataGridView1.DataSource = Aduana.Consulta(Aduana.IdInventario, "")
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).Visible = False
                DataGridView1.Columns(2).HeaderText = "Aduana"
                DataGridView1.Columns(3).HeaderText = "Número"
                DataGridView1.Columns(4).HeaderText = "Fecha"
                DataGridView1.Columns(5).HeaderText = "Cantidad"
                DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                CantidadAsignado = Aduana.CantidadAsignados()
                Label3.Text = CStr(Cantidad - CantidadAsignado) + " de " + Cantidad.ToString + " artículos por asignar."
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ConsultaAduanas()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView2.RowCount > 0 Then PrimerCeldaRow = DataGridView2.FirstDisplayedCell.RowIndex
                'Dim P As New dblotes(MySqlcon)
                If IdDocumento = 0 Then
                    DataGridView2.DataSource = Aduana.ConsultaAduanas(Aduana.IdInventario, TextBox3.Text, True, Aduana.IdAlmacen)
                Else
                    DataGridView2.DataSource = Aduana.ConsultaAduanasDev(Aduana.IdInventario, TextBox3.Text, Aduana.IdAlmacen, IdDocumento, QueDocumento)
                End If
                DataGridView2.Columns(0).Visible = False
                DataGridView2.Columns(1).HeaderText = "Aduana"
                DataGridView2.Columns(2).HeaderText = "Número"
                DataGridView2.Columns(3).HeaderText = "Fecha"
                DataGridView2.Columns(4).HeaderText = "Cantidad"
                DataGridView2.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView2.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView2.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                'Label3.Text = CStr(Cantidad - Lote.CantidadAsignados()) + " de " + Cantidad.ToString + " artículos por asignar."
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        BotonGuardar()
    End Sub
    Private Sub BotonGuardar()
        Try
            'Dim P As New dbAlmacenes(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""

            If TextBox1.Text.Length < 7 Then
                NoErrores = False
                MensajeError += vbCrLf + " El número de importación debe ser de 7 dígitos."
                TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox4.Text.Length < 4 Then
                NoErrores = False
                MensajeError += vbCrLf + " La patente debe ser de 4 dígitos."
                TextBox4.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox2.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " La cantidad debe ser un valor numérico."
                'TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            Else
                If CDbl(TextBox2.Text) < 0 Then
                    NoErrores = False
                    MensajeError += vbCrLf + " Solo se permiten cantidades positivas."
                End If
                If Button1.Text = "Guardar" Then
                    If Aduana.CantidadAsignados() + CDbl(TextBox2.Text) > Cantidad And (Aduana.IdDetalleCompra <> 0 Or Aduana.idDetalleRemisionC <> 0) Then
                        MensajeError += vbCrLf + " Está tratando de asignar mayor cantidad a la comprada."
                        NoErrores = False
                    End If
                    If Aduana.CantidadAsignados() + CDbl(TextBox2.Text) > Cantidad And (Aduana.IdDetalleRemisionV <> 0 Or Aduana.IdDetalleVenta <> 0) Then
                        MensajeError += vbCrLf + " Está tratando de asignar cantidad mayor a la que se va a vender."
                        NoErrores = False
                    End If
                    If CDbl(TextBox2.Text) > Existencia And (Aduana.IdDetalleRemisionV <> 0 Or Aduana.IdDetalleVenta <> 0) Then
                        MensajeError += vbCrLf + " La importación seleccionada no tiene existencia suficiente."
                        NoErrores = False
                    End If
                Else
                    If Aduana.CantidadAsignados() - CantidadAnterior + CDbl(TextBox2.Text) > Cantidad And (Aduana.IdDetalleCompra <> 0 Or Aduana.idDetalleRemisionC <> 0) Then
                        MensajeError += vbCrLf + " Está tratando de asignar mayor cantidad a la comprada."
                        NoErrores = False
                    End If
                    If Aduana.CantidadAsignados() - CantidadAnterior + CDbl(TextBox2.Text) > Cantidad And (Aduana.IdDetalleRemisionV <> 0 Or Aduana.IdDetalleVenta <> 0) Then
                        MensajeError += vbCrLf + " Está tratando de asignar cantidad mayor a la que se va a vender."
                        NoErrores = False
                    End If
                    If CDbl(TextBox2.Text) > Existencia And (Aduana.IdDetalleRemisionV <> 0 Or Aduana.IdDetalleVenta <> 0) Then
                        MensajeError += vbCrLf + " La importación seleccionada no tiene existencia suficiente."
                        NoErrores = False
                    End If
                End If
            End If

            'If Button1.Text = "Guardar" Then
            '    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesAlta, PermisosN.Secciones.Catalagos) = False Then
            '        NoErrores = False
            '        MensajeError += " No tiene permiso para realizar esta operación."
            '    End If
            'Else
            '    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesCambio, PermisosN.Secciones.Catalagos) = False Then
            '        NoErrores = False
            '        MensajeError += " No tiene permiso para realizar esta operación."
            '    End If
            'End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    If Aduana.ChecaAduanaRepetida(TextBox1.Text.ToUpper, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Aduana.IdInventario) = 0 Then
                        If Aduana.IdDetalleCompra <> 0 Or Aduana.idDetalleRemisionC <> 0 Or (Aduana.idDetalleMovimiento <> 0 And (Aduana.TipoMov = dbInventarioConceptos.Tipos.Entrada Or Aduana.TipoMov = dbInventarioConceptos.Tipos.InventarioInicial)) Then Aduana.Guardar(ComboBox8.Text, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CDbl(TextBox2.Text), Aduana.IdInventario, TextBox1.Text.ToUpper, DateTimePicker1.Value.ToString("yy"), ComboBox8.Text.Substring(0, 2), TextBox4.Text)
                        If Aduana.IdDetalleCompra <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.IdDetalleCompra, CDbl(TextBox2.Text), "tblcomprasaduana")
                        If Aduana.idDetalleRemisionC <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.idDetalleRemisionC, CDbl(TextBox2.Text), "tblcomprasremisionesaduana")
                        If Aduana.IdDetalleVenta <> 0 Then Aduana.AsignaAduanaADocumento(AduanaSeleccionado, Aduana.IdDetalleVenta, CDbl(TextBox2.Text), "tblventasaduanan")
                        If Aduana.IdDetalleRemisionV <> 0 Then Aduana.AsignaAduanaADocumento(AduanaSeleccionado, Aduana.IdDetalleRemisionV, CDbl(TextBox2.Text), "tblventasremisionesaduana")
                        If Aduana.IdDetalleDevolucioC <> 0 Then Aduana.AsignaAduanaADocumento(AduanaSeleccionado, Aduana.IdDetalleDevolucioC, CDbl(TextBox2.Text), "tbldevolucionescaduana")
                        If Aduana.idDetalleDevolucionV <> 0 Then Aduana.AsignaAduanaADocumento(AduanaSeleccionado, Aduana.idDetalleDevolucionV, CDbl(TextBox2.Text), "tbldevolucionesaduana")
                        If Aduana.idDetalleMovimiento <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.idDetalleMovimiento, CDbl(TextBox2.Text), "tblmovimientosaduana")
                        PopUp("Guardado", 20)
                        Nuevo()
                    Else
                        'MsgBox("Ya existe un almacen con ese número, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                        'TextBox6.BackColor = Color.FromArgb(250, 150, 150)
                        If Aduana.IdDetalleCompra <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.IdDetalleCompra, CDbl(TextBox2.Text), "tblcomprasaduana")
                        If Aduana.idDetalleRemisionC <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.idDetalleRemisionC, CDbl(TextBox2.Text), "tblcomprasremisionesaduana")
                        If Aduana.IdDetalleVenta <> 0 Then Aduana.AsignaAduanaADocumento(AduanaSeleccionado, Aduana.IdDetalleVenta, CDbl(TextBox2.Text), "tblventasaduanan")
                        If Aduana.IdDetalleRemisionV <> 0 Then Aduana.AsignaAduanaADocumento(AduanaSeleccionado, Aduana.IdDetalleRemisionV, CDbl(TextBox2.Text), "tblventasremisionesaduana")
                        If Aduana.IdDetalleDevolucioC <> 0 Then Aduana.AsignaAduanaADocumento(AduanaSeleccionado, Aduana.IdDetalleDevolucioC, CDbl(TextBox2.Text), "tbldevolucionescaduana")
                        If Aduana.idDetalleDevolucionV <> 0 Then Aduana.AsignaAduanaADocumento(AduanaSeleccionado, Aduana.idDetalleDevolucionV, CDbl(TextBox2.Text), "tbldevolucionesaduana")
                        If Aduana.idDetalleMovimiento <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.idDetalleMovimiento, CDbl(TextBox2.Text), "tblmovimientosaduana")
                        PopUp("Guardado", 20)
                        Nuevo()
                    End If
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                        Dim ClaveRepetida As Integer = 0
                        If TextBox1.Text <> ClaveAnterior Then
                            ClaveRepetida = Aduana.ChecaAduanaRepetida(TextBox1.Text.ToUpper, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Aduana.IdInventario)
                        End If
                        If Aduana.IdDetalleCompra <> 0 Or Aduana.idDetalleRemisionC <> 0 Then ClaveRepetida = 0
                        If ClaveRepetida = 0 Then
                            If Aduana.IdDetalleCompra <> 0 Or Aduana.idDetalleRemisionC <> 0 Or (Aduana.idDetalleMovimiento <> 0 And (Aduana.TipoMov = dbInventarioConceptos.Tipos.Entrada Or Aduana.TipoMov = dbInventarioConceptos.Tipos.InventarioInicial)) Then Aduana.Modificar(Aduana.ID, ComboBox8.Text, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox1.Text.ToUpper, DateTimePicker1.Value.ToString("yy"), ComboBox8.Text.Substring(0, 2), TextBox4.Text)
                            If Aduana.IdDetalleCompra <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.IdDetalleCompra, CDbl(TextBox2.Text), "tblcomprasaduana")
                            If Aduana.idDetalleRemisionC <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.idDetalleRemisionC, CDbl(TextBox2.Text), "tblcomprasremisionesaduana")
                            If Aduana.IdDetalleVenta <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.IdDetalleVenta, CDbl(TextBox2.Text), "tblventasaduanan")
                            If Aduana.IdDetalleRemisionV <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.IdDetalleRemisionV, CDbl(TextBox2.Text), "tblventasremisionesaduana")
                            If Aduana.IdDetalleDevolucioC <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.IdDetalleDevolucioC, CDbl(TextBox2.Text), "tbldevolucionescaduana")
                            If Aduana.idDetalleDevolucionV <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.idDetalleDevolucionV, CDbl(TextBox2.Text), "tbldevolucionesaduana")
                            If Aduana.idDetalleMovimiento <> 0 Then Aduana.AsignaAduanaADocumento(Aduana.ID, Aduana.idDetalleMovimiento, CDbl(TextBox2.Text), "tblmovimientosaduana")
                            PopUp("Modificado", 20)
                            Nuevo()
                        Else
                            MsgBox("Ya existe una importación con ese número, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                            TextBox1.BackColor = Color.FromArgb(250, 150, 150)
                        End If
                    End If
                End If
                ComboBox8.Focus()
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    'Dim P As New dbAlmacenes(MySqlcon)
                    Aduana.EliminarDetalle(Aduana.IdDetalleAduana)
                    PopUp("Eliminado", 20)
                    Nuevo()
                    ComboBox8.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar esta importación debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'If Modo < 2 Then
        LlenaDatos()
        'End If
    End Sub

    Private Sub LlenaDatos()
        Try
            Aduana.ID = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            'Dim P As New dbAlmacenes(IdAlmacen, MySqlcon)
            If Modo < 2 Then
                Aduana.LlenaDatosDetalle(Aduana.ID)
            Else
                Aduana.LlenaDatos()
            End If
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            ClaveAnterior = Aduana.Aduana + Aduana.Numero
            ComboBox8.Text = Aduana.Aduana
            TextBox1.Text = Aduana.Numero
            TextBox2.Text = Aduana.Cantidad
            TextBox4.Text = Aduana.Patente
            CantidadAnterior = Aduana.Cantidad
            Existencia = Aduana.Existencia
            DateTimePicker1.Value = Aduana.Fecha
            'ComboBox1.SelectedIndex = IdsSucursales.Busca(P.IdSucursal)
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub




    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            LlenaDatos()
            ComboBox8.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatos()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        ConsultaAduanas()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        AduanaSeleccionado = DataGridView2.Item(0, DataGridView2.CurrentCell.RowIndex).Value
        'If Lote.IdDetalleVenta <> 0 Or Lote.IdDetalleRemisionV <> 0 Then
        Dim L As New dbInventarioAduana(AduanaSeleccionado, MySqlcon)
        ClaveAnterior = L.Numero
        ComboBox8.Text = L.Aduana
        TextBox1.Text = L.Numero
        Existencia = L.Cantidad
        TextBox4.Text = L.Patente
        DateTimePicker1.Value = CDate(L.Fecha)
        TextBox2.Focus()
        'End If
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    'Dim P As New dbAlmacenes(MySqlcon)
                    Aduana.Eliminar(AduanaSeleccionado)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    ComboBox8.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar esta importación debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Public Function HayViejaAduana(pidDetalle) As Boolean
        Return Aduana.HayViejaAduana(pidDetalle)
    End Function

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker1.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub DateTimePicker1_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimePicker1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonGuardar()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class