Public Class frmInventarioRCostos

    Dim IdsClas As New elemento
    Dim IdsClas2 As New elemento
    Dim IdsClas3 As New elemento
    Dim IdInventario As Integer
    Dim Ids As New Collection
    Dim IdsNeg As New Collection
    Dim ConsultaOn As Boolean
    Dim EncontroClas As Boolean = True
    Dim TipoDeCambio As Double
    Dim TipoProceso As Byte
    Dim IdsSucursales As New elemento
    Dim IdsAlmacenes As New elemento
    Private Structure Inventario
        Dim Id As Integer
        Dim Cantidad As Double
        Dim IdAlmacen As Integer
        Dim Costo As Double
        Dim Nombre As String
    End Structure
    Public Sub New(ByVal pTipo As Byte)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        TipoProceso = pTipo
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmVentasReportes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        DateTimePicker1.Value = Date.Now
        DateTimePicker2.Value = Date.Now
        'LlenaCombos("tblalmacenes", ComboBox5, "nombre", "nombret", "idalmacen", IdsAlmacenes2, "idalmacen<>1", "Todos")
        'LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        'ComboBox1.SelectedIndex = IdsSucursales.Busca(My.Settings.idsucursal)
        LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", "Todas", "nombre")
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")

        If TipoProceso = 1 Then
            CheckBox1.Visible = False
            CheckBox2.Visible = False
            Me.Text = "Recalcular inventario"
        End If
        If TipoProceso = 2 Then
            Label22.Visible = True
            Label18.Visible = True
            ComboBox1.Visible = True
            ComboBox8.Visible = True
            'CheckBox1.Visible = False
            CheckBox2.Text = "Recalcular inventario al inicio."
            Me.Text = "Ajustar inventario a cero"
        End If
        Dim CM As New dbMonedasConversiones(1, MySqlcon)
        TipoDeCambio = CM.Cantidad
        ConsultaOn = True
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas.Valor(ComboBox3.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            ConsultaOn = False
            TextBox10.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones2", ComboBox6, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "Todas", "nombre")
            HabilitaClase2(True)
        Else
            HabilitaClase2(False)
            HabilitaClase3(False)
            ComboBox6.SelectedIndex = -1
            ConsultaOn = False
            TextBox10.Text = ""
            ConsultaOn = True
        End If

    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas2.Valor(ComboBox6.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            ConsultaOn = False
            TextBox12.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones3", ComboBox7, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "Todas", "nombre")
            HabilitaClase3(True)
        Else
            HabilitaClase3(False)
            ComboBox7.SelectedIndex = -1
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
    Private Sub HabilitaClase2(ByVal Habilita As Boolean)
        TextBox12.Enabled = Habilita
        ComboBox6.Enabled = Habilita
    End Sub
    Private Sub HabilitaClase3(ByVal Habilita As Boolean)
        TextBox13.Enabled = Habilita
        ComboBox7.Enabled = Habilita
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
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TipoProceso = 0 Or TipoProceso = 1 Then
            RecalculaInvCosto(True)
        Else
            AjustaaCero()
        End If
    End Sub

    Private Sub RecalculaInvCosto(pPreguntar As Boolean)
        Button4.Enabled = False
        Button1.Enabled = True
        Dim IdTrono As Integer
        If pPreguntar Then
            If TipoProceso = 0 Then
                If MsgBox("¿Recalcular Costos? Este proceso puede durar varios minutos.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                    Button1.Enabled = False
                    Button4.Enabled = True
                    Exit Sub
                End If
            End If

            If TipoProceso = 1 Then
                If MsgBox("¿Recalcular Inventario? Este proceso puede durar varios minutos.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                    Button1.Enabled = False
                    Button4.Enabled = True
                    Exit Sub
                End If
            End If
        End If
        Try
            Dim I As New dbInventario(MySqlcon)
            Dim idClas As Integer
            Dim idClas2 As Integer
            Dim idClas3 As Integer

            If ComboBox3.SelectedIndex <= 0 Then
                idClas = 0
            Else
                idClas = IdsClas.Valor(ComboBox3.SelectedIndex)
            End If
            If ComboBox6.SelectedIndex <= 0 Then
                idClas2 = 0
            Else
                idClas2 = IdsClas2.Valor(ComboBox6.SelectedIndex)
            End If
            If ComboBox7.SelectedIndex <= 0 Then
                idClas3 = 0
            Else
                idClas3 = IdsClas3.Valor(ComboBox7.SelectedIndex)
            End If
            Ids.Clear()
            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            DR = I.ConsultaIds(idClas, idClas2, idClas3, IdInventario, False)
            While DR.Read
                Ids.Add(DR("idinventario"))
            End While
            DR.Close()

            If TipoProceso = 0 Then
                If CheckBox1.Checked Then
                    Label1.Text = "Procesando 1 de " + Ids.Count.ToString
                    ProgressBar1.Maximum = Ids.Count
                    ProgressBar1.Value = ProgressBar1.Minimum
                    ConsultaOn = True
                    Application.DoEvents()
                        For Each id As Integer In Ids
                        IdTrono = id
                        If ProgressBar1.Value >= 0 Then
                            I.ReCalculaCostosB(id, GlobalTipoCosteo, Format(DateTimePicker1.Value, "yyyy/MM/dd"))
                            Label1.Text = "Procesando " + ProgressBar1.Value.ToString + " de " + Ids.Count.ToString
                        End If
                        ProgressBar1.Value += 1
                        Application.DoEvents()
                        If ConsultaOn = False Then Exit For
                    Next
                    End If
                    If CheckBox2.Checked Then
                        Label1.Text = "Procesando salidas 1 de " + Ids.Count.ToString
                        ProgressBar1.Maximum = Ids.Count
                        ProgressBar1.Value = ProgressBar1.Minimum
                        ConsultaOn = True
                        Application.DoEvents()
                        For Each id As Integer In Ids
                            I.AjustaCostoSalidas(id, Format(DateTimePicker1.Value, "yyyy/MM/dd"))
                            Label1.Text = "Procesando salidas " + ProgressBar1.Value.ToString + " de " + Ids.Count.ToString
                            ProgressBar1.Value += 1
                            Application.DoEvents()
                            If ConsultaOn = False Then Exit For
                        Next
                    End If
                End If
                If TipoProceso = 1 Then
                    Label1.Text = "Procesando 1 de " + Ids.Count.ToString
                    ProgressBar1.Maximum = Ids.Count
                    ProgressBar1.Value = ProgressBar1.Minimum
                    ConsultaOn = True
                    Application.DoEvents()
                    For Each id As Integer In Ids
                        I.RecalculaInventario(id)
                        Label1.Text = "Procesando " + ProgressBar1.Value.ToString + " de " + Ids.Count.ToString
                        ProgressBar1.Value += 1
                        Application.DoEvents()
                        If ConsultaOn = False Then Exit For
                    Next
                End If

                If ConsultaOn = True Then Label1.Text = "Proceso terminado."

        Catch ex As Exception
            MsgBox(ex.Message + " " + IdTrono.ToString, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        Button1.Enabled = False
        Button4.Enabled = True
    End Sub
    Private Sub AjustaaCero()
        Button4.Enabled = False
        Button1.Enabled = True
        If MsgBox("¿Ajustar Inventarios a Cero? Este proceso puede durar varios minutos.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
            Button1.Enabled = False
            Button4.Enabled = True
            Exit Sub
        End If
        If CheckBox2.Checked Then
            TipoProceso = 1
            RecalculaInvCosto(False)
        End If
        TipoProceso = 2
        Dim idClas As Integer
        Dim idClas2 As Integer
        Dim idClas3 As Integer
        Dim Contador As Long = 0
        If ComboBox3.SelectedIndex <= 0 Then
            idClas = 0
        Else
            idClas = IdsClas.Valor(ComboBox3.SelectedIndex)
        End If
        If ComboBox6.SelectedIndex <= 0 Then
            idClas2 = 0
        Else
            idClas2 = IdsClas2.Valor(ComboBox6.SelectedIndex)
        End If
        If ComboBox7.SelectedIndex <= 0 Then
            idClas3 = 0
        Else
            idClas3 = IdsClas3.Valor(ComboBox7.SelectedIndex)
        End If
        Try

            Dim I As New dbInventario(MySqlcon)
            Dim Ids As New Collection
            Dim Mov As New dbMovimientos(MySqlcon)
            Dim MovDetalles As New dbMovimientosDetalles(MySqlcon)
            Dim o As New dbOpciones(MySqlcon)
            Dim C As Integer
            Dim Ca As Integer
            Dim UnArticulo As Inventario
            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            Dim Concepto As New dbInventarioConceptos(MySqlcon)
            'Entradas
            C = 1
            Contador = 1
            ProgressBar1.Maximum = 1
            ProgressBar1.Value = ProgressBar1.Minimum
            Concepto.DaPrimerConcepto(0)
            While C <= ComboBox1.Items.Count - 1
                ComboBox1.SelectedIndex = C
                Ca = 1
                Ids.Clear()
                While Ca <= ComboBox8.Items.Count - 1
                    ComboBox8.SelectedIndex = Ca
                    Label1.Text = "Recopilando información para entradas. Puede tardar varios minutos."
                    Application.DoEvents()
                    DR = I.DaIds(1, IdsAlmacenes.Valor(Ca), IdInventario, idClas, idClas2, idClas3, DateTimePicker1.Value.ToString("yyyy/MM/dd"))
                    While DR.Read
                        UnArticulo.Cantidad = DR("cantidad") * -1
                        UnArticulo.Id = DR("idinventario")
                        UnArticulo.IdAlmacen = IdsAlmacenes.Valor(Ca)
                        UnArticulo.Costo = DR("costo")
                        UnArticulo.Nombre = DR("nombre")
                        Ids.Add(UnArticulo)
                        Label1.Text = "Recopilando información: " + Contador.ToString
                        Contador += 1
                        Application.DoEvents()
                    End While
                    DR.Close()
                    Ca += 1
                End While
                C += 1
                Contador = 0
                Mov.Guardar(Mov.DaNuevoFolio(Concepto.Serie, IdsSucursales.Valor(ComboBox1.SelectedIndex), Concepto.ID), DateTimePicker1.Value.ToString("yyyy/MM/dd"), Concepto.ID, Concepto.Serie, IdsSucursales.Valor(ComboBox1.SelectedIndex), 1, 2, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0)
                For Each Art As Inventario In Ids
                    MovDetalles.Guardar(Mov.ID, Art.Id, Art.Cantidad, Art.Costo * Art.Cantidad, 2, Art.Nombre, Art.IdAlmacen, Art.IdAlmacen, 1, 1, 0, "", "", "", "")
                    Contador += 1
                    Label1.Text = "No movimientos procesados: " + Contador.ToString
                    Application.DoEvents()
                Next
                If Ids.Count > 0 Then
                    Mov.DaTotal(Mov.ID, 2)
                    Mov.Modificar(Mov.ID, Mov.Folio, 3, "Movimiento generado automáticamente por el sistema para ajustar el inventario a cero", Mov.Serie, Mov.Subtotal, Mov.TotalVenta, 1, 2, Mov.Fecha, 0, 0, 0, 0, 0)
                    Mov.ModificaInventario(Mov.ID, GlobalTipoCosteo, 1)
                Else
                    Mov.Eliminar(Mov.ID, GlobalTipoCosteo, 1, o.CostoTiempoReal)
                End If
                'Mov.ReCalculaCostos(Mov.ID, GlobalTipoCosteo, o.CostoTiempoReal, 1)
            End While

            'Salidas
            C = 1
            Concepto.DaPrimerConcepto(1)
            While C <= ComboBox1.Items.Count - 1
                ComboBox1.SelectedIndex = C

                Ca = 1
                Ids.Clear()
                Contador = 1
                While Ca <= ComboBox8.Items.Count - 1
                    ComboBox8.SelectedIndex = Ca
                    Label1.Text = "Recopilando información para salidas. Puede tardar varios minutos."
                    Application.DoEvents()
                    DR = I.DaIds(0, IdsAlmacenes.Valor(Ca), IdInventario, idClas, idClas2, idClas3, DateTimePicker1.Value.ToString("yyyy/MM/dd"))
                    While DR.Read
                        UnArticulo.Cantidad = DR("cantidad")
                        UnArticulo.Id = DR("idinventario")
                        UnArticulo.IdAlmacen = IdsAlmacenes.Valor(Ca)
                        UnArticulo.Costo = DR("costo")
                        UnArticulo.Nombre = DR("nombre")
                        Ids.Add(UnArticulo)
                        Label1.Text = "Recopilando información: " + Contador.ToString
                        Contador += 1
                        Application.DoEvents()
                    End While
                    DR.Close()
                    Ca += 1
                End While
                C += 1
                Contador = 0
                Mov.Guardar(Mov.DaNuevoFolio(Concepto.Serie, IdsSucursales.Valor(ComboBox1.SelectedIndex), Concepto.ID), DateTimePicker1.Value.ToString("yyyy/MM/dd"), Concepto.ID, Concepto.Serie, IdsSucursales.Valor(ComboBox1.SelectedIndex), 1, 2, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0)
                For Each Art As Inventario In Ids
                    MovDetalles.Guardar(Mov.ID, Art.Id, Art.Cantidad, Art.Costo * Art.Cantidad, 2, Art.Nombre, Art.IdAlmacen, Art.IdAlmacen, 1, 1, 0, "", "", "", "")
                    Contador += 1
                    Label1.Text = "No. movimientos procesados: " + Contador.ToString
                    Application.DoEvents()
                Next
                If Ids.Count > 0 Then
                    Mov.DaTotal(Mov.ID, 2)
                    Mov.Modificar(Mov.ID, Mov.Folio, 3, "Movimiento generado automáticamente por el sistema para ajustar el inventario a cero", Mov.Serie, Mov.Total, Mov.TotalaPagar, 1, 2, Mov.Fecha, 0, 0, 0, 0, 0)
                    Mov.ModificaInventario(Mov.ID, GlobalTipoCosteo, 1)
                Else
                    Mov.Eliminar(Mov.ID, GlobalTipoCosteo, 1, o.CostoTiempoReal)
                End If
                'Mov.ReCalculaCostos(Mov.ID, GlobalTipoCosteo, o.CostoTiempoReal, 1)
            End While
            ProgressBar1.Value = 1
            If CheckBox1.Checked Then
                TipoProceso = 0
                RecalculaInvCosto(False)
            End If
            ComboBox1.SelectedIndex = 0
            Label1.Text = "Proceso Terminado"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        Button1.Enabled = False
        Button4.Enabled = True
    End Sub
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 1, "") Then
                    IdInventario = p.ID
                    TextBox4.Text = p.Nombre
                Else
                    IdInventario = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub


    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloInv
        Dim Op As New dbOpciones(MySqlcon)
        If Op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, 0, False, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        IdInventario = B.Inventario.ID
                        ConsultaOn = False
                        TextBox3.Text = B.Inventario.Clave
                        ConsultaOn = True
                        TextBox4.Text = B.Inventario.Nombre
                End Select
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(TipodeBusqueda, 0, False, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        IdInventario = B.Inventario.ID
                        ConsultaOn = False
                        TextBox3.Text = B.Inventario.Clave
                        ConsultaOn = True
                        TextBox4.Text = B.Inventario.Nombre
                End Select
            End If
            B.Dispose()
        End If
    End Sub


    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub
   
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ConsultaOn = False
        Label1.Text += " Proceso Detenido."
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Todos")
    End Sub
End Class