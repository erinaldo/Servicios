Public Class frmInventarioLotes
    Dim IdAlmacen As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Dim CantidadAsignado As Double
    Dim CantidadAnterior As Double
    Dim Lote As New dblotes(MySqlcon)
    Dim Cantidad As Double
    Dim Existencia As Double
    Dim Modo As Byte
    Dim LoteSeleccionado As Integer
    Dim IdDocumento As Integer
    Dim QueDocumento As Byte
    'Dim IdsSucursales As New elemento
    Private Sub frmAlmacenes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            Nuevo()

            'Label3.Text = Cantidad.ToString + " de  artículos por asignar."
            If Lote.IdDetalleVenta <> 0 Or Lote.IdDetalleRemisionV <> 0 Then
                TextBox6.Enabled = False
                DateTimePicker1.Enabled = False
                Label5.Text = "Lotes asignados a la venta."
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Public Sub New(ByVal pIddetalleVenta As Integer, ByVal pIdDetalleRemisionV As Integer, ByVal pIdDetalleCompra As Integer, ByVal pIdDetalleRemisionC As Integer, ByVal pCantidad As Double, ByVal pIdInventario As Integer, ByVal pModo As Byte, pIdDetalleDevolucion As Integer, pIdDetalleDevolucionC As Integer, pIdDetalleMovimiento As Integer, pIdAlmacen As Integer, pAlmacenStr As String, pTipoMov As Byte, pIdDocumento As Integer, pQueDocumento As Byte)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Lote.IdDetalleRemisionV = pIdDetalleRemisionV
        Lote.IdDetalleCompra = pIdDetalleCompra
        Lote.IdDetalleVenta = pIddetalleVenta
        Lote.idDetalleRemisionC = pIdDetalleRemisionC
        Lote.idDetalleDevolucionC = pIdDetalleDevolucionC
        Lote.idDetalleDevolucionV = pIdDetalleDevolucion
        Lote.idDetalleMovimiento = pIdDetalleMovimiento
        Lote.IdInventario = pIdInventario
        Lote.TipoMov = pTipoMov
        Lote.IdAlamacen = pIdAlmacen
        Cantidad = pCantidad
        Modo = pModo
        IdDocumento = pIdDocumento
        QueDocumento = pQueDocumento
        Label8.Text = "Almacén: " + pAlmacenStr
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            TextBox6.Text = ""
            TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
            ConsultaOn = True
            Consulta()
            TextBox2.Text = CStr(Cantidad - CantidadAsignado)
            CantidadAnterior = 0
            ConsultaLotes()
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
                DataGridView1.DataSource = Lote.Consulta(Lote.IdInventario, "")
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).Visible = False
                DataGridView1.Columns(2).HeaderText = "Lote"
                DataGridView1.Columns(3).HeaderText = "Caducidad"
                DataGridView1.Columns(4).HeaderText = "Cantidad"
                DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                CantidadAsignado = Lote.CantidadAsignados()
                Label3.Text = CStr(Cantidad - CantidadAsignado) + " de " + Cantidad.ToString + " artículos por asignar."
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ConsultaLotes()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView2.RowCount > 0 Then PrimerCeldaRow = DataGridView2.FirstDisplayedCell.RowIndex
                'Dim P As New dblotes(MySqlcon)
                If IdDocumento = 0 Then
                    DataGridView2.DataSource = Lote.ConsultaLotes(Lote.IdInventario, TextBox3.Text, True, Lote.IdAlamacen)
                Else
                    DataGridView2.DataSource = Lote.ConsultaLotesDev(Lote.IdInventario, TextBox3.Text, Lote.IdAlamacen, IdDocumento, QueDocumento)
                End If
                DataGridView2.Columns(0).Visible = False
                'DataGridView2.Columns(1).Visible = False
                DataGridView2.Columns(1).HeaderText = "Lote"
                DataGridView2.Columns(2).HeaderText = "Caducidad"
                DataGridView2.Columns(3).HeaderText = "Cantidad"
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
        botonGuardar()
    End Sub
    Private Sub BotonGuardar()
        Try
            'Dim P As New dbAlmacenes(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""

            If TextBox6.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un número al lote."
                TextBox6.BackColor = Color.FromArgb(250, 150, 150)
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
                    If Lote.CantidadAsignados() + CDbl(TextBox2.Text) > Cantidad And (Lote.IdDetalleCompra <> 0 Or Lote.idDetalleRemisionC <> 0) Then
                        MensajeError += vbCrLf + " Está tratando de asignar mayor cantidad a la comprada."
                        NoErrores = False
                    End If
                    If Lote.CantidadAsignados() + CDbl(TextBox2.Text) > Cantidad And (Lote.IdDetalleRemisionV <> 0 Or Lote.IdDetalleVenta <> 0) Then
                        MensajeError += vbCrLf + " Está tratando de asignar cantidad mayor a la que se va a vender."
                        NoErrores = False
                    End If
                    If CDbl(TextBox2.Text) > Existencia And (Lote.IdDetalleRemisionV <> 0 Or Lote.IdDetalleVenta <> 0) Then
                        MensajeError += vbCrLf + " Lote no tiene suficiente existencia."
                        NoErrores = False
                    End If
                Else
                    If Lote.CantidadAsignados() - CantidadAnterior + CDbl(TextBox2.Text) > Cantidad And (Lote.IdDetalleCompra <> 0 Or Lote.idDetalleRemisionC <> 0) Then
                        MensajeError += vbCrLf + " Está tratando de asignar mayor cantidad a la comprada."
                        NoErrores = False
                    End If
                    If Lote.CantidadAsignados() - CantidadAnterior + CDbl(TextBox2.Text) > Cantidad And (Lote.IdDetalleRemisionV <> 0 Or Lote.IdDetalleVenta <> 0) Then
                        MensajeError += vbCrLf + " Está tratando de asignar cantidad mayor a la que se va a vender."
                        NoErrores = False
                    End If
                    If CDbl(TextBox2.Text) > Existencia And (Lote.IdDetalleRemisionV <> 0 Or Lote.IdDetalleVenta <> 0) Then
                        MensajeError += vbCrLf + " Lote no tiene suficiente existencia."
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
                    If Lote.ChecaLoteRepetido(TextBox6.Text.ToUpper, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Lote.IdInventario) = 0 Then
                        If Lote.IdDetalleCompra <> 0 Or Lote.idDetalleRemisionC <> 0 Or (Lote.idDetalleMovimiento <> 0 And (Lote.TipoMov = dbInventarioConceptos.Tipos.Entrada Or Lote.TipoMov = dbInventarioConceptos.Tipos.InventarioInicial)) Then Lote.Guardar(TextBox6.Text.ToUpper, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CDbl(TextBox2.Text), Lote.IdInventario)
                        If Lote.IdDetalleCompra <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.IdDetalleCompra, CDbl(TextBox2.Text), "tblcompraslotes")
                        If Lote.idDetalleRemisionC <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.idDetalleRemisionC, CDbl(TextBox2.Text), "tblcomprasremisioneslotes")
                        If Lote.IdDetalleVenta <> 0 Then Lote.AsignaLoteADocumento(LoteSeleccionado, Lote.IdDetalleVenta, CDbl(TextBox2.Text), "tblventaslotes")
                        If Lote.IdDetalleRemisionV <> 0 Then Lote.AsignaLoteADocumento(LoteSeleccionado, Lote.IdDetalleRemisionV, CDbl(TextBox2.Text), "tblventasremisioneslotes")
                        If Lote.idDetalleDevolucionC <> 0 Then Lote.AsignaLoteADocumento(LoteSeleccionado, Lote.idDetalleDevolucionC, CDbl(TextBox2.Text), "tbldevolucionesclotes")
                        If Lote.idDetalleDevolucionV <> 0 Then Lote.AsignaLoteADocumento(LoteSeleccionado, Lote.idDetalleDevolucionV, CDbl(TextBox2.Text), "tbldevolucioneslotes")
                        If Lote.idDetalleMovimiento <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.idDetalleMovimiento, CDbl(TextBox2.Text), "tblmovimientoslotes")
                        PopUp("Guardado", 20)
                        Nuevo()
                    Else
                        'MsgBox("Ya existe un almacen con ese número, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                        'TextBox6.BackColor = Color.FromArgb(250, 150, 150)
                        If Lote.IdDetalleCompra <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.IdDetalleCompra, CDbl(TextBox2.Text), "tblcompraslotes")
                        If Lote.idDetalleRemisionC <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.idDetalleRemisionC, CDbl(TextBox2.Text), "tblcomprasremisioneslotes")
                        If Lote.IdDetalleVenta <> 0 Then Lote.AsignaLoteADocumento(LoteSeleccionado, Lote.IdDetalleVenta, CDbl(TextBox2.Text), "tblventaslotes")
                        If Lote.IdDetalleRemisionV <> 0 Then Lote.AsignaLoteADocumento(LoteSeleccionado, Lote.IdDetalleRemisionV, CDbl(TextBox2.Text), "tblventasremisioneslotes")
                        If Lote.idDetalleDevolucionC <> 0 Then Lote.AsignaLoteADocumento(LoteSeleccionado, Lote.idDetalleDevolucionC, CDbl(TextBox2.Text), "tbldevolucionesclotes")
                        If Lote.idDetalleDevolucionV <> 0 Then Lote.AsignaLoteADocumento(LoteSeleccionado, Lote.idDetalleDevolucionV, CDbl(TextBox2.Text), "tbldevolucioneslotes")
                        If Lote.idDetalleMovimiento <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.idDetalleMovimiento, CDbl(TextBox2.Text), "tblmovimientoslotes")
                        PopUp("Guardado", 20)
                        Nuevo()
                    End If
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Dim ClaveRepetida As Integer = 0
                        If TextBox6.Text.ToUpper <> ClaveAnterior Then
                            ClaveRepetida = Lote.ChecaLoteRepetido(TextBox6.Text.ToUpper, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Lote.IdInventario)
                        End If
                        If Lote.IdDetalleCompra <> 0 Or Lote.idDetalleRemisionC <> 0 Then ClaveRepetida = 0
                        If ClaveRepetida = 0 Then
                            If Lote.IdDetalleCompra <> 0 Or Lote.idDetalleRemisionC <> 0 Or (Lote.idDetalleMovimiento <> 0 And (Lote.TipoMov = dbInventarioConceptos.Tipos.Entrada Or Lote.TipoMov = dbInventarioConceptos.Tipos.InventarioInicial)) Then Lote.Modificar(Lote.ID, TextBox6.Text.ToUpper, Format(DateTimePicker1.Value, "yyyy/MM/dd"))
                            If Lote.IdDetalleCompra <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.IdDetalleCompra, CDbl(TextBox2.Text), "tblcompraslotes")
                            If Lote.idDetalleRemisionC <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.idDetalleRemisionC, CDbl(TextBox2.Text), "tblcomprasremisioneslotes")
                            If Lote.IdDetalleVenta <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.IdDetalleVenta, CDbl(TextBox2.Text), "tblventaslotes")
                            If Lote.IdDetalleRemisionV <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.IdDetalleRemisionV, CDbl(TextBox2.Text), "tblventasremisioneslotes")
                            If Lote.idDetalleDevolucionC <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.idDetalleDevolucionC, CDbl(TextBox2.Text), "tbldevolucionesclotes")
                            If Lote.idDetalleDevolucionV <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.idDetalleDevolucionV, CDbl(TextBox2.Text), "tbldevolucioneslotes")
                            If Lote.idDetalleMovimiento <> 0 Then Lote.AsignaLoteADocumento(Lote.ID, Lote.idDetalleMovimiento, CDbl(TextBox2.Text), "tblmovimientoslotes")
                            PopUp("Modificado", 20)
                            Nuevo()
                        Else
                            MsgBox("Ya existe un lote con ese número y fecha de caducidad, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                            TextBox6.BackColor = Color.FromArgb(250, 150, 150)
                        End If
                    End If
                End If
                TextBox6.Focus()
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
                    Lote.EliminarDetalle(Lote.IdDetalleLote)
                    PopUp("Eliminado", 20)
                    Nuevo()
                    TextBox6.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este lote debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
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
            Lote.ID = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            'Dim P As New dbAlmacenes(IdAlmacen, MySqlcon)

            If Modo < 2 Then
                Lote.LlenaDatosDetalle(Lote.ID)
            Else
                Lote.LlenaDatos()
            End If
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            ClaveAnterior = Lote.Lote
            TextBox6.Text = Lote.Lote
            Existencia = Lote.Existencia
            'TextBox1.Text = P.Nombre
            TextBox2.Text = Lote.Cantidad
            CantidadAnterior = Lote.Cantidad
            DateTimePicker1.Value = Lote.FechaCaducidad
            'ComboBox1.SelectedIndex = IdsSucursales.Busca(P.IdSucursal)
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub




    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            LlenaDatos()
            TextBox6.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatos()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        ConsultaLotes()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        LoteSeleccionado = DataGridView2.Item(0, DataGridView2.CurrentCell.RowIndex).Value
        'If Lote.IdDetalleVenta <> 0 Or Lote.IdDetalleRemisionV <> 0 Then
        'Dim Lote As New dblotes(LoteSeleccionado, MySqlcon)
        Lote.ID = LoteSeleccionado
        Lote.LlenaDatos()
        ClaveAnterior = Lote.Lote
        TextBox6.Text = Lote.Lote
        Existencia = Lote.Cantidad
        'TextBox1.Text = P.Nombre
        DateTimePicker1.Value = CDate(Lote.FechaCaducidad)
        TextBox2.Focus()
        'End If
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Lote.Eliminar(LoteSeleccionado)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    TextBox6.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este lote debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox6_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox6.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker1.Focus()
        End If
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

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