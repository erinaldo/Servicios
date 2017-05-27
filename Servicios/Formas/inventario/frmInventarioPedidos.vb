Public Class frmInventarioPedidos
    Dim Pedido As dbInventarioPedidos
    Dim Detalles As dbInventarioPedidosDetalles
    Dim IdsSucursalesA As New elemento
    Dim IdsSucursalesB As New elemento
    Dim Consultaon As Boolean = True
    Dim IdPedidoC As Integer
    Dim ImpDoc As ImprimirDocumento
    Dim Op As dbOpciones
    Public Sub New(pIdPedido As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        IdPedidoC = pIdPedido
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmInventarioPedidos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Pedido.Estado = Estados.SinGuardar Then
            If MsgBox("Este pedido no ha sido guardado. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Pedido.Eliminar(Pedido.IdPedido)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmInventarioPedidos_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            If Pedido.Estado = Estados.SinGuardar Then
                If MsgBox("Este pedido no ha sido guardado. ¿Desea iniciar un nuevo pedido? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Pedido.Eliminar(Pedido.IdPedido)
                    Nuevo()
                End If
            Else
                Nuevo()
            End If
        End If
        If e.KeyCode = Keys.F9 And Button1.Enabled Then
            Modificar(False, Estados.Pendiente, True)
        End If
        If e.KeyCode = Keys.F10 And Button14.Enabled Then
            If Pedido.Estado = 3 Then
                Modificar(False, 5, True)
            Else
                Modificar(False, Estados.Guardada, True)
            End If
        End If
    End Sub
    Private Sub frmInventarioPedidos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            Pedido = New dbInventarioPedidos(MySqlcon)
            Detalles = New dbInventarioPedidosDetalles(MySqlcon)
            ImpDoc = New ImprimirDocumento()
            Op = New dbOpciones(MySqlcon)
            LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursalesA)
            LlenaCombos("tblsucursales", ComboBox6, "nombre", "nombret", "idsucursal", IdsSucursalesB)
            ComboBox1.Items.Add("Normal")
            ComboBox1.Items.Add("Urgente")
            Nuevo()
            If IdPedidoC <> 0 Then
                LlenaDatos(IdPedidoC)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub
    Private Sub Nuevo()
        Pedido.Inicializar()
        Label24.Visible = False
        Panel1.Enabled = True
        Button14.Text = "Guardar (F10)"
        ComboBox3.SelectedIndex = IdsSucursalesA.Busca(GlobalIdSucursalDefault)
        ComboBox6.SelectedIndex = 0
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursalesA.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.InventarioPedidos, 0)
        TextBox11.Text = Sf.Serie
        TextBox2.Text = Pedido.DaNuevoFolio(TextBox11.Text).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        Button1.Enabled = False
        Button14.Enabled = False
        DateTimePicker1.Value = Date.Now
        Button13.Enabled = False
        Button2.Enabled = False
        Button15.Enabled = False
        Button5.Visible = False
        Button11.Visible = False
        ComboBox1.SelectedIndex = 0
        ComboBox1.Enabled = True
        ComboBox3.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.CambioSucursal, PermisosN.Secciones.Inventario)
        ComboBox6.Enabled = True
        DateTimePicker1.Enabled = True
        TextBox2.Enabled = True
        TextBox11.Enabled = True

        TextBox14.Text = ""
        NuevoConcepto()
    End Sub
    Private Sub NuevoConcepto()
        TextBox5.Text = "1"
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox1.Text = "1"
        TextBox12.Text = "0"
        TextBox6.Text = "0"
        Label2.Visible = False
        TextBox1.Visible = False
        Button4.Text = "Agregar Concepto"
        Button9.Enabled = False
        Label14.Text = Pedido.Datotal(Pedido.IdPedido).ToString("#,###,##0.00")
        Detalles.Inicializa()
        ConsultaDetalles
    End Sub
    Private Sub Guardar()
        Dim Errores As String = ""
        If IdsSucursalesA.Valor(ComboBox3.SelectedIndex) = IdsSucursalesB.Valor(ComboBox6.SelectedIndex) Then
            Errores += "No se puede hacer un pedido a la misma sucursal que solicita. "
        End If
        If IsNumeric(TextBox2.Text) = False Then
            Errores += " El folio debe ser un valor numérico. "
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosAlta, PermisosN.Secciones.Inventario) = False Then
            Errores += " No tiene permiso para realizar esta operación."
        End If

        If Errores = "" Then
            Pedido.Guardar(DateTimePicker1.Value.ToString("yyyy/MM/dd"), TextBox11.Text, CInt(TextBox2.Text), IdsSucursalesA.Valor(ComboBox3.SelectedIndex), IdsSucursalesB.Valor(ComboBox6.SelectedIndex), ComboBox1.SelectedIndex)
            Pedido.Estado = Estados.SinGuardar
            Button1.Enabled = True
            Button2.Enabled = True
            Button14.Enabled = True
            Button15.Enabled = True
        Else
            MsgBox(Errores, MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Public Sub AgregaConcepto()
        Dim Errores As String = ""
        If Detalles.IdInventario <= 0 Then
            Errores += "Debe seleccionar un artículo."
        End If
        If IsNumeric(TextBox5.Text) = False Then
            Errores += "La cantidad debe ser un valor numérico"
        End If
        If IsNumeric(TextBox1.Text) = False Then
            Errores += "La cantidad a autorizar debe ser un valor numérico"
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosAlta, PermisosN.Secciones.Inventario) = False And Pedido.Estado < 3 Then
            Errores += " No tiene permiso para realizar esta operación."
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosAutorizar, PermisosN.Secciones.Inventario) = False And Pedido.Estado >= 3 Then
            Errores += " No tiene permiso para realizar esta operación."
        End If
        If Errores = "" Then
            If Button4.Text = "Agregar Concepto" Then
                Detalles.Guardar(Pedido.IdPedido, Detalles.IdInventario, CDbl(TextBox5.Text), CDbl(TextBox12.Text))
                NuevoConcepto()
            Else
                Detalles.Modificar(Detalles.IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox12.Text), CDbl(TextBox1.Text))
                If Detalles.Autorizado = 1 Then
                    Detalles.Autorizar(Detalles.IdDetalle, 1)
                End If
                NuevoConcepto()
            End If
            TextBox5.Focus()
        Else
            MsgBox(Errores, MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub Modificar(pRechazar As Boolean, pEstado As Byte, pNuevo As Boolean)
        Dim Errores As String = ""
        If IdsSucursalesA.Valor(ComboBox3.SelectedIndex) = IdsSucursalesB.Valor(ComboBox6.SelectedIndex) Then
            Errores += "No se puede hacer un pedido a la misma sucursal que solicita. "
        End If
        If IsNumeric(TextBox2.Text) = False Then
            Errores += " El folio debe ser un valor numérico. "
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosAlta, PermisosN.Secciones.Inventario) = False And pEstado <= 3 Then
            Errores += " No tiene permiso para realizar esta operación."
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosCancelar, PermisosN.Secciones.Inventario) = False And pEstado = 4 Then
            Errores += " No tiene permiso para realizar esta operación."
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosAutorizar, PermisosN.Secciones.Inventario) = False And pEstado = 5 Then
            Errores += " No tiene permiso para realizar esta operación."
        End If
        If Errores = "" Then
            Pedido.Estado = pEstado
            If pRechazar Then
                Pedido.Estado = 6
                Dim R As Integer = 0
                While R < DGDetalles.RowCount
                    Detalles.Autorizar(DGDetalles.Item(0, R).Value, 0)
                    R += 1
                End While
            End If
            If Pedido.Estado = 5 Then
                Dim R As Integer = 0
                While R < DGDetalles.RowCount
                    If DGDetalles.Item(1, R).Value = 1 Then
                        Detalles.Autorizar(DGDetalles.Item(0, R).Value, 1)
                    Else
                        Detalles.Autorizar(DGDetalles.Item(0, R).Value, 0)
                    End If
                    R += 1
                End While
            End If
            Pedido.Modificar(Pedido.IdPedido, DateTimePicker1.Value.ToString("yyyy/MM/dd"), TextBox11.Text, CInt(TextBox2.Text), IdsSucursalesA.Valor(ComboBox3.SelectedIndex), IdsSucursalesB.Valor(ComboBox6.SelectedIndex), 0, TextBox14.Text, Pedido.Estado, ComboBox1.SelectedIndex)

            If Pedido.Estado >= 3 Then
                If MsgBox("¿Imprimir el pedido?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Imprimir(Pedido.IdPedido)
                End If
            End If
            If Pedido.Estado = 5 Then
                If MsgBox("¿Surtir el pedido?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Pedido.LlenaDatos(Pedido.IdPedido)
                    Dim PC As New frmInventarioMovimientosN(Pedido.IdPedido, Pedido.DaAlmacenMovsSucursal(Pedido.IdSucursalA), Pedido.DaAlmacenMovsSucursal(Pedido.IdSucursalB), Pedido.IdSucursalB, Pedido.DaConceptoTraspaso(Pedido.IdSucursalB))
                    PC.ShowDialog()
                    PC.Dispose()
                End If
            End If
            If IdPedidoC <> 0 Then Me.DialogResult = Windows.Forms.DialogResult.OK
            If pNuevo Then Nuevo()
        Else
            MsgBox(Errores, MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
    Private Sub ConsultaDetalles()
        DGDetalles.DataSource = Detalles.Consulta(Pedido.IdPedido)
        DGDetalles.ClearSelection()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If Pedido.Estado = Estados.SinGuardar Then
            If MsgBox("Este pedido no ha sido guardado. ¿Desea iniciar un nuevo pedido? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Pedido.Eliminar(Pedido.IdPedido)
                Nuevo()
            End If
        Else
            Nuevo()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        BotonAgregar()
    End Sub
    Private Sub BotonAgregar()
        If Pedido.Estado = 0 Then Guardar()
        If Pedido.Estado > 0 Then
            AgregaConcepto()
        End If
    End Sub
    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If Pedido.Estado = 3 Then
            Modificar(False, 5, True)
        Else
            Modificar(False, Estados.Guardada, True)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Modificar(False, Estados.Pendiente, True)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim R As Integer = 0
        While R < DGDetalles.RowCount
            DGDetalles.Item(1, R).Value = 1
            R += 1
        End While
    End Sub
    Private Sub LlenaDatos(pIdPedido As Integer)
        Pedido.LlenaDatos(pIdPedido)
        ComboBox3.SelectedIndex = IdsSucursalesA.Busca(Pedido.IdSucursalA)
        ComboBox6.SelectedIndex = IdsSucursalesB.Busca(Pedido.IdSucursalB)
        DateTimePicker1.Value = Pedido.Fecha
        TextBox11.Text = Pedido.Serie
        TextBox2.Text = Pedido.Folio.ToString
        TextBox14.Text = Pedido.Comentario
        ComboBox1.SelectedIndex = Pedido.Tipo
        NuevoConcepto()
        Select Case Pedido.Estado
            Case 3
                DateTimePicker1.Enabled = False
                ComboBox3.Enabled = False
                ComboBox6.Enabled = False
                TextBox2.Enabled = False
                TextBox11.Enabled = False
                Button1.Enabled = False
                Button14.Enabled = True
                Button2.Enabled = False
                Button13.Enabled = True
                Button14.Text = "Autorizar (F10)"
                Label24.Visible = True
                Label24.ForeColor = Color.FromArgb(225, 195, 70)
                Label24.Text = "SIN AUTORIZAR"
                Button5.Visible = True
                Panel1.Enabled = True
                Button5.Visible = True
                Button11.Visible = True
                ComboBox1.Enabled = False
                Button15.Enabled = True
            Case 4
                DateTimePicker1.Enabled = False
                ComboBox3.Enabled = False
                ComboBox6.Enabled = False
                TextBox2.Enabled = False
                TextBox11.Enabled = False
                Button1.Enabled = False
                Button14.Enabled = False
                Button2.Enabled = False
                Button13.Enabled = False
                Label24.Visible = True
                Label24.Text = "CANCELADA"
                Label24.ForeColor = Color.Red
                Button5.Visible = False
                Panel1.Enabled = False
                Button5.Visible = False
                Button11.Visible = False
                Button14.Text = "Autorizar (F10)"
                ComboBox1.Enabled = False
                Button15.Enabled = True
            Case 5
                DateTimePicker1.Enabled = False
                ComboBox3.Enabled = False
                ComboBox6.Enabled = False
                TextBox2.Enabled = False
                TextBox11.Enabled = False
                Button1.Enabled = False
                Button14.Enabled = False
                Button2.Enabled = False
                Button13.Enabled = True
                Label24.Visible = True
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Label24.Text = "AUTORIZADO"
                Button5.Visible = False
                Panel1.Enabled = False
                Button5.Visible = False
                Button11.Visible = False
                ComboBox1.Enabled = False
                Button14.Text = "Autorizar (F10)"
                Button15.Enabled = True
            Case Else
                DateTimePicker1.Enabled = True
                ComboBox3.Enabled = True
                ComboBox6.Enabled = True
                TextBox2.Enabled = True
                TextBox11.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button2.Enabled = True
                Button13.Enabled = False
                Label24.Visible = False
                Button5.Visible = False
                Panel1.Enabled = True
                Button5.Visible = False
                Button11.Visible = False
                ComboBox1.Enabled = True
                Button15.Enabled = True
                Button14.Text = "Guardar (F10)"
        End Select
    End Sub
   
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar este pedido?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Pedido.Eliminar(Pedido.IdPedido)
            Nuevo()
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim PC As New frmInventarioPedidosConsulta(1, False, 0)
        If PC.ShowDialog = Windows.Forms.DialogResult.OK Then
            LlenaDatos(PC.IdPedido)
        End If
        PC.Dispose()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        BuscaArticuloBoton()
    End Sub
    Private Sub BuscaArticuloBoton()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloProductoInv
        Dim op As New dbOpciones(MySqlcon)
        If op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, 0, False, False, True)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                        LlenaDatosArticulo(B.Inventario)
                        TextBox3.Focus()
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(TipodeBusqueda, 0, False, False, True)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                        LlenaDatosArticulo(B.Inventario)
                        TextBox3.Focus()
            End If
            B.Dispose()
        End If
    End Sub
    Private Sub LlenaDatosArticulo(pInventario As dbInventario)
        Detalles.IdInventario = pInventario.ID
        consultaon = False
        TextBox3.Text = pInventario.Clave
        TextBox3.Select(TextBox3.Text.Length, 0)
        consultaon = True
        TextBox4.Text = pInventario.Nombre
        TextBox12.Text = Math.Round(pInventario.DaUltimoCosto(pInventario.ID), 2).ToString
        If IsNumeric(TextBox1.Text) Then
            TextBox6.Text = CDbl(TextBox12.Text) * CDbl(TextBox1.Text)
        Else
            TextBox6.Text = "0"
        End If

    End Sub
    Private Sub LlenaDatosDetalle(pidDetalle As Integer)
        Detalles.LlenaDatos(pidDetalle)
        Consultaon = False
        TextBox3.Text = Detalles.Inventario.Clave
        Consultaon = True
        TextBox4.Text = Detalles.Inventario.Nombre
        TextBox5.Text = Detalles.Cantidad.ToString
        If Detalles.Cantidad <> 0 Then
            TextBox1.Text = Detalles.CantidadAut.ToString
            TextBox12.Text = Format(Detalles.Precio / Detalles.Cantidad, "0.00##")
            TextBox6.Text = Detalles.Precio
        Else
            TextBox1.Text = Detalles.CantidadAut.ToString
            TextBox12.Text = 0
        End If
        Button4.Text = "Modificar Concepto"
        Label2.Visible = True
        TextBox1.Visible = True
        If Pedido.Estado > 3 Then
            Button9.Enabled = False
        Else
            Button9.Enabled = True
        End If
        If CheckScroll.Checked Then TextBox5.Focus()
    End Sub
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 1, False, False, False, True) Then
                    LlenaDatosArticulo(p)
                Else
                    Detalles.IdInventario = 0
                    TextBox4.Text = ""
                    TextBox6.Text = "0"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub

    Private Sub DGDetalles_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        If DGDetalles.CurrentCell.RowIndex >= 0 Then
            LlenaDatosDetalle(DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value)
        End If
    End Sub

    Private Sub DGDetalles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        TextBox1.Text = TextBox5.Text
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If IsNumeric(TextBox1.Text) And IsNumeric(TextBox12.Text) Then
            TextBox6.Text = Format(CDbl(TextBox1.Text) * CDbl(TextBox12.Text), "0.00##")
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim R As Integer = 0
        While R < DGDetalles.RowCount
            DGDetalles.Item(1, R).Value = 0
            R += 1
        End While
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 1000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
        End If
        et.Dispose()
    End Sub

    Private Sub DateTimePicker1_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimePicker1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox3.Focus()
        End If
    End Sub

    Private Sub ComboBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox6.Focus()
        End If
    End Sub

  
    
    Private Sub ComboBox6_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox6.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox11.Focus()
        End If
    End Sub

    Private Sub TextBox11_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox11.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox1.Focus()
        End If
    End Sub

    Private Sub TextBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Visible = True Then
                TextBox1.Focus()
            Else
                BotonAgregar()
            End If
        End If
        If e.KeyCode = Keys.F1 Then
            BuscaArticuloBoton()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    
    Private Sub ComboBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox5.Focus()
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

    End Sub
    Private Sub Imprimir(pIdPedido As Integer)
        Try
            Pedido.LlenaDatos(pIdPedido)
            ImpDoc.IdSucursal = Pedido.IdSucursalA
            ImpDoc.TipoDocumento = TiposDocumentos.InventarioPedidos
            ImpDoc.TipoDocumentoT = TiposDocumentos.InventarioPedidos + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.InventarioPedidos
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.OtrosPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresion()
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "PEDIDO-" + Pedido.Serie + Pedido.Folio.ToString("0000")
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaNodosImpresion()
        Dim SucursalA As New dbSucursales(Pedido.IdSucursalA, MySqlcon)
        Dim SucursalB As New dbSucursales(Pedido.IdSucursalB, MySqlcon)

        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrefiscal", SucursalA.NombreFiscal, 0), "nombrefiscal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "direccion", SucursalA.Direccion, 0), "direccion")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "noexterior", SucursalA.NoExterior, 0), "noexterior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeior", SucursalA.NoInterior, 0), "nointerior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "colonia", SucursalA.Colonia, 0), "colonia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudad", SucursalA.Ciudad, 0), "ciudad")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "estado", SucursalA.Estado, 0), "estado")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cp", SucursalA.CP, 0), "cp")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfc", SucursalA.RFC, 0), "rfc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "tel", SucursalA.Telefono, 0), "tel")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "email", SucursalA.Email, 0), "email")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "referencia", SucursalA.ReferenciaDomicilio, 0), "referencia")

        If Pedido.Tipo = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "tipo", "NORMAL", 0), "tipo")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "tipo", "URGENTE", 0), "tipo")
        End If

        ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", Pedido.Serie, 0), "serie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "folio", Pedido.Folio, 0), "folio")


        ImpDoc.ImpND.Add(New NodoImpresionN("", "fecha", Replace(Pedido.Fecha, "/", "-"), 0), "fecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "hora", Pedido.Hora, 0), "hora")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "solicitante", SucursalA.Nombre, 0), "solicitante")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "solicita", SucursalB.Nombre, 0), "solicita")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", Pedido.Comentario, 0), "comentario")
        Select Case Pedido.Estado
            Case 3
                ImpDoc.ImpND.Add(New NodoImpresionN("", "estadopedido", "SIN AUTORIZAR", 0), "estadopedido")
            Case 4
                ImpDoc.ImpND.Add(New NodoImpresionN("", "estadopedido", "CANCELADO", 0), "estadopedido")
            Case 5
                ImpDoc.ImpND.Add(New NodoImpresionN("", "estadopedido", "AUTORIZADO", 0), "estadopedido")
            Case Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "estadopedido", "", 0), "estadopedido")
        End Select
        ImpDoc.ImpND.Add(New NodoImpresionN("", "total", Pedido.Datotal(Pedido.IdPedido).ToString("#,###,##0.00").PadLeft(13), 0), "total")
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        DR = Detalles.ConsultaReader(Pedido.IdPedido)
        Dim Cont As Integer = 0
        While DR.Read
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8), 0), "cantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("abreviatura"), 0), "tipocantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidadaut", Format(DR("cantidadaut"), "#,##0.00").PadLeft(8), 0), "cantidadaut" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "autorizado", DR("autorizado"), 0), "autorizado" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("pu"), "$#,###,##0.00").PadLeft(13), 0), "preciou" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), "$#,###,##0.00").PadLeft(13), 0), "importe" + Format(Cont, "000"))
            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()
        If Pedido.Estado = Estados.Cancelada Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ImpDoc.DibujaPaginaN(e.Graphics)
        If ImpDoc.MasPaginas = True Or ImpDoc.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(ImpDoc.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If
        If Pedido.Estado = Estados.Inicio Or Pedido.Estado = Estados.SinGuardar Or Pedido.Estado = Estados.Pendiente Then
            e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
        End If
        e.HasMorePages = ImpDoc.MasPaginas
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        If Pedido.Estado < 3 Then
            Modificar(False, Pedido.Estado, False)
        End If
        Imprimir(Pedido.IdPedido)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosCancelar, PermisosN.Secciones.Inventario) = False Then
            MsgBox("No tiene permisos para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If MsgBox("¿Cancelar este pedido?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If Pedido.TieneMovimientos(Pedido.IdPedido) = False Then
                Modificar(False, Estados.Cancelada, True)
            Else
                MsgBox("Este pedido ya tiene movimientos de inventario, no se puede cancelar.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosAlta, PermisosN.Secciones.Inventario) = False And Pedido.Estado < 3 Then
            MsgBox(" No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosAutorizar, PermisosN.Secciones.Inventario) = False And Pedido.Estado >= 3 Then
            MsgBox(" No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If MsgBox("¿Eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Detalles.Eliminar(Detalles.IdDetalle)
            NuevoConcepto()
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        NuevoConcepto()
    End Sub
    
End Class