Public Class frmComprasReportesN
    Dim IdsClas As New elemento
    Dim IdsClas2 As New elemento
    Dim IdsClas3 As New elemento
    Dim IdsSucursales As New elemento
    Dim IdsVendedores As New elemento
    Dim IdsMonedas As New elemento
    Dim IdInventario As Integer
    Dim IdVariante As Integer
    Dim IdCliente As Integer
    Dim ConsultaOn As Boolean
    Dim EncontroClas As Boolean = True
    Dim idsConceptos As New elemento
    Dim IdsTiposSuc As New elemento
    Dim IdsAlmacenes As New elemento
    Dim IdsTiposProv As New elemento
    Private Sub frmComprasReportesN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ListBox1.Items.Add("Compras.")
        ListBox1.Items.Add("Compras por artículo.")
        ListBox1.Items.Add("Compras Lotes.")
        ListBox1.Items.Add("Compras Lotes Remisiones.")
        ListBox1.Items.Add("Compras Importaciones.")
        ListBox1.Items.Add("Compras Importaciones Remisiones.")
        ListBox1.Items.Add("Compras concentrado por proveedor.")
        ListBox1.Items.Add("Compras por artículo mensual por proveedor.")
        ListBox1.Items.Add("Saldos.")
        ListBox1.Items.Add("Analítica de proveedores.")
        ListBox1.Items.Add("Antigüedad de saldos.")
        'ListBox1.Items.Add("Antigüedad de saldos a fecha.")
        ListBox1.Items.Add("Relación de pagos.")
        ListBox1.Items.Add("Documentos proveedores.")
        ListBox1.Items.Add("Notas de crédito.")
        ListBox1.Items.Add("Notas de cargo.")
        ListBox1.Items.Add("Devoluciones.")
        ListBox1.Items.Add("Remisiones.")
        ListBox1.Items.Add("Remisiones por artículo.")
        ListBox1.Items.Add("Pre-Ordenes de compra.")
        ListBox1.Items.Add("Ordenes de compra.")
        ListBox1.Items.Add("Catálogo de proveedores.")
        ListBox1.Items.Add("Directorio de proveedores.")
        ConsultaOn = False
        ComboBox5.Items.Add("Pesos")
        ComboBox5.Items.Add("Moneda Original")
        ComboBox5.SelectedIndex = 0
        LlenaCombos("tblsucursalestipos", ComboBox15, "nombre", "nombrec", "idtipo", IdsTiposSuc, , "Todos")
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        LlenaCombos("tblmonedas", ComboBox4, "nombre", "nombrem", "idmoneda", IdsMonedas, "idmoneda>1", "Todas")
        ComboBox1.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", "Todas", "nombre")
        LlenaCombos("tblvendedores", ComboBox2, "nombre", "nombret", "idvendedor", IdsVendedores, , "Todos")
        LlenaCombos("tblproveedorestipos", ComboBox8, "nombre", "nombret", "idtipo", IdsTiposProv, , "Todos")
        Dim CM As New dbMonedasConversiones(1, MySqlcon)
        TextBox5.Text = CM.Cantidad.ToString
        ConsultaOn = True
        cmbIpoGrafica.SelectedIndex = 0
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

    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 0, False, False, True, False) Then
                    IdInventario = p.ID
                    TextBox4.Text = p.Nombre

                Else
                    IdInventario = 0
                    TextBox4.Text = ""
                    IdVariante = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbproveedores(MySqlcon)
                If c.BuscaProveedor(TextBox1.Text) Then
                    TextBox2.Text = c.Nombre
                    IdCliente = c.ID
                Else
                    IdCliente = 0
                    TextBox2.Text = ""
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub HabilitaClase2(ByVal Habilita As Boolean)
        TextBox12.Enabled = Habilita
        ComboBox6.Enabled = Habilita
    End Sub
    Private Sub HabilitaClase3(ByVal Habilita As Boolean)
        TextBox13.Enabled = Habilita
        ComboBox7.Enabled = Habilita
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = B.Proveedor.Nombre
            IdCliente = B.Proveedor.ID
            ConsultaOn = False
            TextBox1.Text = B.Proveedor.Clave
            ConsultaOn = True
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloProducto
        Dim op As New dbOpciones(MySqlcon)
        If op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, 0, False, True, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        IdInventario = B.Inventario.ID
                        IdVariante = 0
                        TextBox4.Text = B.Inventario.Nombre
                        TextBox3.Text = B.Inventario.Clave
                        ConsultaOn = True
                        TextBox4.Text = B.Inventario.Nombre
                End Select
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(TipodeBusqueda, 0, False, True, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        IdInventario = B.Inventario.ID
                        IdVariante = 0
                        TextBox4.Text = B.Inventario.Nombre
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

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaCliente()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.Text = "Compras." Or ListBox1.Text = "Cobranza." Then
            btnGraficas.Enabled = True
            grpGraficas.Enabled = True
        Else
            btnGraficas.Enabled = False
            grpGraficas.Enabled = False
        End If
        If ListBox1.Text = "Notas de crédito." Then
            LlenaCombos("tblconceptosnotascompras", cmbConcepto, "nombre", "nombret", "idconceptonotacompra", idsConceptos, " tipo=1 and idconceptonotacompra>0", "Todos")
            Label8.Visible = True
            cmbConcepto.Visible = True
        End If
        If ListBox1.Text = "Notas de cargo." Then
            LlenaCombos("tblconceptosnotascompras", cmbConcepto, "nombre", "nombret", "idconceptonotacompra", idsConceptos, " tipo=0 and idconceptonotacompra>0", "Todos")
            Label8.Visible = True
            cmbConcepto.Visible = True
        End If
        If ListBox1.Text = "Relación de pagos." Then
            LlenaCombos("tblconceptosnotascompras", cmbConcepto, "nombre", "nombret", "idconceptonotacompra", idsConceptos, " tipo=2 and idconceptonotacompra>0", "Todos")
            Label8.Visible = True
            cmbConcepto.Visible = True
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'ListBox1.Items.Add("Compras.")
        'ListBox1.Items.Add("Compras por artículo.")
        'ListBox1.Items.Add("Reporte de saldos.")
        'ListBox1.Items.Add("Antigüedad de saldos.")
        'ListBox1.Items.Add("Antigüedad de saldos a fecha.")
        'ListBox1.Items.Add("Cobranza")
        'ListBox1.Items.Add("Documentos proveedores.")
        'ListBox1.Items.Add("Notas de crédito.")
        'ListBox1.Items.Add("Notas de cargo.")
        'ListBox1.Items.Add("Devoluciones.")
        'ListBox1.Items.Add("Remisiones.")
        'ListBox1.Items.Add("Remisiones por artículo.")
        'ListBox1.Items.Add("Pre-Ordenes de compra.")
        'ListBox1.Items.Add("Ordenes de compra.")
        'ListBox1.Items.Add("Catálogo de proveedores.")
        'ListBox1.Items.Add("Directorio de proveedores.")
        Try
            Dim V As New dbCompras(MySqlcon)
            'Dim O As New dbOpciones(MySqlcon)
            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim idClas As Integer
            Dim idClas2 As Integer
            Dim idClas3 As Integer
            Dim TipoCosteo As Byte
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
            If RadioButton1.Checked Then TipoCosteo = 1
            If RadioButton2.Checked Then TipoCosteo = 2
            If RadioButton3.Checked Then TipoCosteo = 3
            Select Case ListBox1.Text
                Case "Compras."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    Rep = New repComprasNormal
                    Rep.SetDataSource(V.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox14.Checked, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim Filtros As String
                    Filtros = " Fechas: Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If TextBox2.Text = "" Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    'If TextBox4.Text = "" Then
                    '    Filtros += " Articulo: Todos"
                    'Else
                    '    Filtros += " Articulo: " + TextBox4.Text
                    'End If
                    'If ComboBox2.SelectedIndex > 0 Then
                    '    Filtros += " *Vendedor: " + ComboBox2.Text
                    'End If
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += "Moneda: " + ComboBox4.Text
                    End If
                    'If chkFacturadas.Checked = True Then
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    'End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Compras concentrado por proveedor."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    Rep = New repComprasProveedores
                    Rep.SetDataSource(V.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox14.Checked, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), True, IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim Filtros As String
                    Filtros = " Fechas: Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If TextBox2.Text = "" Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    'If TextBox4.Text = "" Then
                    '    Filtros += " Articulo: Todos"
                    'Else
                    '    Filtros += " Articulo: " + TextBox4.Text
                    'End If
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += "Moneda: " + ComboBox4.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Compras por artículo."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.ReporteComprasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    Rep = New repComprasArticulos
                    Rep.SetDataSource(V.ReporteComprasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsTiposProv.Valor(ComboBox8.SelectedIndex), IdsAlmacenes.Valor(ComboBox9.SelectedIndex)))
                    Dim Filtros As String
                    Filtros = " Fechas: Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacén: " + ComboBox9.Text + vbCr
                    If TextBox2.Text = "" Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    If TextBox4.Text = "" Then
                        Filtros += " Articulo: Todos"
                    Else
                        Filtros += " Articulo: " + TextBox4.Text
                    End If
                    'If ComboBox2.SelectedIndex > 0 Then
                    '    Filtros += " *Vendedor: " + ComboBox2.Text
                    'End If
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += " Moneda: " + ComboBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Saldos."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.ReporteProveedoresDeudas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    Rep = New repComprasProveedoresDeudas
                    Rep.SetDataSource(V.ReporteProveedoresDeudas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Dim Filtros As String
                    Filtros = " Fechas: Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If TextBox2.Text = "" Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += " Moneda: " + ComboBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Analítica de proveedores."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Rep = New repClientesAna
                    Dim P As New dbproveedores(MySqlcon)
                    Rep.SetDataSource(P.ReporteAnalitico(IdCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("titulo", "Analítico de Proveedores")
                    Dim Filtros As String
                    Filtros = " Fechas: Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If TextBox2.Text = "" Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Rep.SetParameterValue("noceros", CheckBox9.Checked)
                    'Rep.SetParameterValue("titulo01", "Compras")
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Antigüedad de saldos."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.ReporteViejosSaldos(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    Rep = New repComprasViejosSaldosH
                    If Format(DateTimePicker2.Value, "yyyyMMdd") = Format(Date.Now, "yyyyMMdd") Then
                        '    Rep.SetDataSource(V.ReporteViejosSaldos(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, 1))
                        Rep.SetDataSource(V.ReporteViejosSaldosH(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), 1, True, IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Else
                        Rep.SetDataSource(V.ReporteViejosSaldosH(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), 1, False, IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    End If
                    Dim Filtros As String = ""
                    Filtros = " Sucursal: " + ComboBox1.Text
                    If TextBox2.Text = "" Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += " Moneda: " + ComboBox4.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("fecha", Format(DateTimePicker2.Value, "yyyy/MM/dd"))
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                    'Case "Antigüedad de saldos a fecha."
                    '   Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    '  Rep = New repComprasViejosSaldosH
                Case "Relación de pagos."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VP As New dbComprasPagos(MySqlcon)
                    'VP.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    Rep = New repComprasPagos
                    Rep.SetDataSource(VP.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, idsConceptos.Valor(cmbConcepto.SelectedIndex), CheckBox8.Checked, IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim Filtros As String
                    Filtros = " Fechas: Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If TextBox2.Text = "" Then
                        Filtros += " Proveedor: Todos"
                        Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    Rep.SetParameterValue("filtro", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Documentos proveedores."
                    Dim Rep As New repDocumentosProveedores
                    Dim db As New dbCapturaDocumentosProveedores(MySqlcon)
                    'db.Reporte(IdCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"))
                    Rep.SetDataSource(db.Reporte(IdCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Tipo Proveedor: " + ComboBox8.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Notas de crédito."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repNotasCreditoConceptoC
                    Rep.SetDataSource(V.ReporteNotasDeCreditoPorConcepto(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, idsConceptos.Valor(cmbConcepto.SelectedIndex), IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Dim Filtros As String
                    Filtros = " Fechas: Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If TextBox2.Text = "" Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += " Moneda: " + ComboBox4.Text
                    End If
                    'If CheckBox14.Checked Then
                    '    Filtros += " *SOLO CANCELADAS*"
                    'End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Notas de cargo."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repNotasCargoConceptoC
                    Rep.SetDataSource(V.ReporteNotasDeCargoPorConcepto(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, idsConceptos.Valor(cmbConcepto.SelectedIndex), IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Dim Filtros As String
                    Filtros = " Fechas: Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If TextBox2.Text = "" Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += " Moneda: " + ComboBox4.Text
                    End If
                    'If CheckBox14.Checked Then
                    '    Filtros += " *SOLO CANCELADAS*"
                    'End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Devoluciones."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Dim D As New dbDevolucionesCompras(MySqlcon)
                    Rep = New repDevolucionesC
                    Rep.SetDataSource(D.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, CheckBox14.Checked, ComboBox5.SelectedIndex, IdInventario, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    If TextBox4.Text = "" Then
                        Filtros += " Articulo: Todos"
                    Else
                        Filtros += " Articulo: " + TextBox4.Text
                    End If
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += " Moneda: " + ComboBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbComprasRemisiones(MySqlcon)
                    Rep = New RepVentasRPC
                    Rep.SetDataSource(VR.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), chkFacturadas.Checked, IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("TipoRep", "Reporte de Remisiones de compra.")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += " Moneda: " + ComboBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones por artículo."
                    Dim VR As New dbComprasRemisiones(MySqlcon)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repComprasArticulosRemisiones
                    Rep.SetDataSource(VR.ReporteComprasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), chkFacturadas.Checked, IdsTiposProv.Valor(ComboBox8.SelectedIndex), IdsAlmacenes.Valor(ComboBox9.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Articulo: Todos"
                    Else
                        Filtros += " Articulo: " + TextBox4.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    Filtros += " Almacén: " + ComboBox9.Text
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += " Moneda: " + ComboBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Pre-Ordenes de compra."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbComprasCotizacionesb(MySqlcon)
                    Rep = New RepVentasRPC
                    Rep.SetDataSource(VR.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("TipoRep", "Reporte de pre-ordenes de compra.")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    'If TextBox4.Text = "" Then
                    '    Filtros += " Articulo: Todos"
                    'Else
                    '    Filtros += " Articulo: " + TextBox4.Text
                    'End If
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += " Moneda: " + ComboBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ordenes de compra."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbComprasPedidos(MySqlcon)
                    Rep = New RepVentasRPC
                    Rep.SetDataSource(VR.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("TipoRep", "Reporte de ordenes de compra.")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Proveedor: Todos"
                    Else
                        Filtros += " Proveedor: " + TextBox2.Text
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    If CheckBox14.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Catálogo de proveedores."
                    Dim db As New dbproveedores(MySqlcon)
                    Dim r As New repCatalogoProveedores
                    r.SetDataSource(db.Reporte(IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    r.SetParameterValue("Encabezado", S.Nombre)
                    Dim RV As New frmReportes(r, False)
                    RV.Show()
                Case "Directorio de proveedores."
                    Dim db As New dbproveedores(MySqlcon)
                    Dim r As New repDirectorioProveedores
                    r.SetDataSource(db.Reporte(IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    r.SetParameterValue("Encabezado", S.Nombre)
                    Dim RV As New frmReportes(r, False)
                    RV.Show()
                Case "Compras Lotes."
                    Dim CL As New dblotes(MySqlcon)
                    CL.reporteComprasLotesFactura(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsTiposProv.Valor(ComboBox8.SelectedIndex), ComboBox8.Text)
                Case "Compras Lotes Remisiones."
                    Dim CL As New dblotes(MySqlcon)
                    CL.reporteComprasLotesRemisiones(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsTiposProv.Valor(ComboBox8.SelectedIndex), ComboBox8.Text)
                Case "Compras Importaciones."
                    Dim VL As New dbInventarioAduana(MySqlcon)
                    VL.reporteComprasAduanas(IdsSucursales.Valor(ComboBox1.SelectedIndex), DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsTiposProv.Valor(ComboBox8.SelectedIndex), ComboBox8.Text)
                Case "Compras Importaciones Remisiones."
                    Dim VL As New dbInventarioAduana(MySqlcon)
                    VL.reporteComprasRemisionesAduanas(IdsSucursales.Valor(ComboBox1.SelectedIndex), DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsTiposProv.Valor(ComboBox8.SelectedIndex), ComboBox8.Text)
                Case "Compras por artículo mensual por proveedor."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim P As New dbproveedores(MySqlcon)
                    'P.ReporteProveedoresAxM(Format(DateTimePicker1.Value, "yyyy/MM/01"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario)
                    Rep = New repCompraVentaMensualxPC
                    Rep.SetDataSource(P.ReporteProveedoresAxM(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("Titulo", "Compras por artículo mensual por proveedor.")
                    Dim Filtros As String
                    Filtros = "Compras del " + Format(DateTimePicker1.Value, "yyyy/MM/01") + " al " + Format(DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Year, 1, DateTimePicker1.Value)), "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente <> 0 Then
                        Filtros += " Proveedor: " + TextBox2.Text
                    Else
                        Filtros += " Proveedor: Todos"
                    End If
                    Filtros += " Tipo Proveedor: " + ComboBox8.Text
                    If IdInventario <> 0 Then
                        Filtros += " Artículo: " + TextBox4.Text
                    Else
                        Filtros += " Artículo: Todos"
                    End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Rep.SetParameterValue("col1", P.FechasMes(0))
                    Rep.SetParameterValue("col2", P.FechasMes(1))
                    Rep.SetParameterValue("col3", P.FechasMes(2))
                    Rep.SetParameterValue("col4", P.FechasMes(3))
                    Rep.SetParameterValue("col5", P.FechasMes(4))
                    Rep.SetParameterValue("col6", P.FechasMes(5))
                    Rep.SetParameterValue("col7", P.FechasMes(6))
                    Rep.SetParameterValue("col8", P.FechasMes(7))
                    Rep.SetParameterValue("col9", P.FechasMes(8))
                    Rep.SetParameterValue("col10", P.FechasMes(9))
                    Rep.SetParameterValue("col11", P.FechasMes(10))
                    Rep.SetParameterValue("col12", P.FechasMes(11))
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

    Private Sub btnGraficas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficas.Click
        Try
            Dim V As New dbCompras(MySqlcon)
            'Dim O As New dbOpciones(MySqlcon)
            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim idClas As Integer
            Dim idClas2 As Integer
            Dim idClas3 As Integer
            Dim TipoCosteo As Byte
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
            If RadioButton1.Checked Then TipoCosteo = 1
            If RadioButton2.Checked Then TipoCosteo = 2
            If RadioButton3.Checked Then TipoCosteo = 3
            Select ListBox1.Text
                Case "Compras."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    If cmbIpoGrafica.SelectedIndex = 0 Then
                        Rep = New repGraficasComprasNormal
                    Else
                        Rep = New repGraficasComprasNBarra
                    End If

                    Rep.SetDataSource(V.ReporteGrafica(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox14.Checked))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim Filtros As String
                    Filtros = " del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd")
                    If CheckBox14.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    Rep.SetParameterValue("fechas", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Cobranza."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VP As New dbComprasPagos(MySqlcon)
                    'VP.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    If cmbIpoGrafica.SelectedIndex = 0 Then
                        Rep = New repGraficasComprasPagos
                    Else
                        Rep = New repGraficasComprasPagosB
                    End If

                    Rep.SetDataSource(VP.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, idsConceptos.Valor(cmbConcepto.SelectedIndex), CheckBox8.Checked, IdsTiposProv.Valor(ComboBox8.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        LlenaCombos("tblalmacenes", ComboBox9, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Todos")
    End Sub
End Class