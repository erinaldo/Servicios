Public Class frmServiciosDetalles
    Dim IdServicio As Integer
    Dim IdEvento As Integer
    Dim idEquipo As Integer
    Dim IdsTecnicos As New elemento
    Dim IdsTecnicos2 As New elemento
    Dim IdsClas As New elemento
    Dim IdsEstado As New elemento
    Dim IDsClas2 As New elemento
    Dim IdInventario As Integer
    Dim IdServiciosInventario As Integer
    Dim ConsultaOn As Boolean = True
    Dim totalAnadidos As Double
    Dim totalConsumidos As Double
    Dim ivaManoObra As Double = 0
    Dim TipoEquipo As Integer
    Dim llenando As Boolean
    Public Sub New(ByVal pIdServicio As Integer, ByVal pidEquipo As String, ByVal pTipoEquipo As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        idEquipo = Integer.Parse(pidEquipo)
        TipoEquipo = pTipoEquipo
        'TipoEquipo=0 clientes
        'TipoEquipo=1 Sucursales

        ' Add any initialization after the InitializeComponent() call.
        IdServicio = pIdServicio
        If My.Settings.pulgadasServicios = 0 Then
            RadioButton1.Checked = True
        Else
            RadioButton1.Checked = False
        End If
        txtImpresora.Text = My.Settings.impresoraServicios
    End Sub
    Private Sub frmServiciosDetalles_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        llenando = True
        LlenaCombos("tbltecnicos", ComboBox3, "nombre", "nombret", "idtecnico", IdsTecnicos)
        LlenaCombos("tbltecnicos", ComboBox6, "nombre", "nombret", "idtecnico", IdsTecnicos2)
        LlenaCombos("tblserviciosclasificaciones", ComboBox4, "nombre", "nombrec", "idclasificacion", IdsClas)
        LlenaCombos("tblserviciosestados", ComboBox2, "estado", "estadoc", "idEstado", IdsEstado)
        'ComboBox2.Items.Add("En taller")
        'ComboBox2.Items.Add("Listo")
        ComboBox1.Items.Add("Abierto")
        ComboBox1.Items.Add("Cerrado")
        llenando = False
        If ComboBox4.Items.Count <= 0 Then
            MsgBox("Necesita dar de alta clasificaciónes de servicios.", MsgBoxStyle.Critical, GlobalNombreApp)
            Me.Close()
        End If
        If ComboBox2.Items.Count > 0 Then
            ComboBox2.SelectedIndex = 0 'ihguhgu
            ComboBox1.SelectedIndex = 0
            LlenaDatosServicios()
            filtroArticulosAnadidos2()
            daIVA()
            ComboBox1.Focus()
        Else
            MsgBox("Es necesario de alta los estados de los Equipos." + vbCrLf + "Vuelva cuando los haya dado de alta.", MsgBoxStyle.Critical, GlobalNombreApp)
            Me.Close()
        End If
        Dim P As New dbServiciosEventos(MySqlcon)
        If P.esTerminal(IdsEstado.Valor(ComboBox2.SelectedIndex)) Then
            ' ComboBox1.SelectedIndex = 1
            cerrado()
        End If

    End Sub
    Private Sub Limpia()
        DGEventos.DataSource = Nothing
        TextBox2.Text = ""
        IdServicio = 0
        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 0
        NuevoEvento()
    End Sub
    Private Function filtroArticulosAnadidos2() As DataSet
        'filtro todos
        Try
            totalConsumidos = 0
            Dim P As New dbServiciosEventos(MySqlcon)
            Dim TablaFull As New DataTable
            Dim Tabla As New DataTable
            TablaFull.Columns.Add("ID")
            TablaFull.Columns.Add("idEvento")
            TablaFull.Columns.Add("idInventario")
            TablaFull.Columns.Add("Codigo")
            TablaFull.Columns.Add("Nombre")
            TablaFull.Columns.Add("Precio")
            TablaFull.Columns.Add("Cantidad")
            TablaFull.Columns.Add("Total")
            TablaFull.Columns.Add("Fecha")
            If TipoEquipo = 0 Then
                'Equipos de clientes
                Tabla = P.filtroArticulosConsumidos(IdServicio)
            Else
                'Equipos de sucursales
                Tabla = P.filtroArticulosConsumidossuc(IdServicio)
            End If

            For i As Integer = 0 To Tabla.Rows.Count() - 1
                Dim dr As DataRow

                dr = TablaFull.NewRow()
                dr("ID") = Tabla.Rows(i)(0).ToString
                dr("idEvento") = Tabla.Rows(i)(1).ToString
                dr("idInventario") = Tabla.Rows(i)(2).ToString
                P.InventarioUtilizado2(Integer.Parse(Tabla.Rows(i)(2).ToString))
                dr("Codigo") = P.codigoInventario
                dr("Nombre") = P.nombreInvenario
                dr("Precio") = Double.Parse(Tabla.Rows(i)(3)).ToString("c2")
                dr("Cantidad") = Tabla.Rows(i)(4).ToString
                dr("Total") = Double.Parse(Tabla.Rows(i)(5)).ToString("c2")
                dr("Fecha") = Format(Tabla.Rows(i)(7))
                TablaFull.Rows.Add(dr)
                totalConsumidos = totalConsumidos + Double.Parse(Tabla.Rows(i)(5).ToString)
            Next

            lblArticulos.Text = Format(totalConsumidos, "0.00")

            DGInventario.DataSource = TablaFull
            DGInventario.Columns(0).Visible = False
            DGInventario.Columns(1).Visible = False
            DGInventario.Columns(2).Visible = False
            DGInventario.Columns(3).HeaderText = "Código"
            DGInventario.Columns(4).HeaderText = "Nombre"
            DGInventario.Columns(5).HeaderText = "Precio"
            DGInventario.Columns(6).HeaderText = "Cant."
            DGInventario.Columns(7).HeaderText = "Total"
            DGInventario.Columns(3).Width = 50
            DGInventario.Columns(6).Width = 50
            DGInventario.Columns(8).Width = 70
            DGInventario.Columns(5).Width = 70
            DGInventario.Columns(7).Width = 70
            DGInventario.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGInventario.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DGInventario.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGInventario.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGInventario.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGInventario.Refresh()


            Dim dataSet As DataSet = New DataSet
            dataSet.Tables.Add(TablaFull)
            ' dataSet.WriteXmlSchema("tblServiciosArticulosAnadidos.xml")
            Return dataSet
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Return Nothing
        End Try
    End Function
    Private Sub NuevoEvento()
        TextBox3.Text = ""
        TextBox4.Text = "0"
        TextBox5.Text = "0"
        Button2.Text = "Agregar"
        lblArticulos.Text = Format(filtroArticulosAnadidos(), "0.00")
        Button3.Enabled = False
        'Button5.Enabled = False
        'Button6.Enabled = False
        ComboBox4.SelectedIndex = 0
        Dim SC As New dbServiciosClasificaciones2(IDsClas2.Valor(ComboBox5.SelectedIndex), MySqlcon)
        TextBox5.Text = SC.Precio.ToString
        'DGInventario.DataSource = Nothing
        TextBox3.Focus()
    End Sub
   
    Private Sub LlenaDatosServicios()
        Try
            If TipoEquipo = 0 Then
                Dim S As New dbServicios(IdServicio, MySqlcon)
                ComboBox2.SelectedIndex = IdsEstado.Busca(S.Estado)
                ComboBox1.SelectedIndex = S.Cerrado
                TextBox2.Text = S.Detalles
                '  Label14.Text = Format(S.Folio, "0000")
                DateTimePicker1.Value = S.FechaEntrada
                Label1.Text = "Cliente:"
                Dim C As New dbClientes(S.IdCliente, MySqlcon)
                TextBox1.Text = C.Clave + " - " + C.Nombre + vbCrLf + " Tel: " + C.Telefono + " Email: " + C.Email
                TextBox1.Text += vbCrLf + "Dirección: " + C.Direccion + ", " + C.Ciudad + ", " + C.CP + ", " + C.Estado + ", " + C.Pais
                ' TextBox1.Text += vbCrLf + "Dirección 2: " + C.Direccion2 + ", " + C.Ciudad2 + ", " + C.CP2 + ", " + C.Estado2 + ", " + C.Pais2
                Dim CE As dbClientesEquipos
                Dim SE As New dbServiciosEquipos(MySqlcon)
                CE = SE.BuscaEquipo(S.ID)
                If CE.ID <> 0 Then
                    TextBox7.Text = "Descripción: " + CE.Nombre + vbCrLf + "Matrícula: " + CE.Matricula + vbCrLf + "No. Serie: " + CE.NoSerie + vbCrLf + "Modelo: " + CE.Modelo
                End If
                txtFolio.Text = Format(S.Folio, "0000")
                txtSerie.Text = S.Serie
                ComboBox6.SelectedIndex = IdsTecnicos2.Busca(S.IdTecnico)
                ComboBox3.SelectedIndex = IdsTecnicos.Busca(S.IdTecnico)
                ConsultaEventos()
                NuevoEvento()
                ConsultaTotales()
            Else
                Dim S As New dbServicios(MySqlcon)
                S.LlenadatosSucursales(IdServicio)
                If S.Estado = 0 Then
                    ComboBox2.SelectedIndex = 0
                Else
                    ComboBox2.SelectedIndex = IdsEstado.Busca(S.Estado)
                End If

                ComboBox1.SelectedIndex = S.Cerrado
                TextBox2.Text = S.Detalles
                '  Label14.Text = Format(S.Folio, "0000")
                DateTimePicker1.Value = S.FechaEntrada
                Label1.Text = "Sucursal:"
                Dim C As New dbSucursales(S.IdCliente, MySqlcon)
                TextBox1.Text = C.Clave + " - " + C.Nombre + vbCrLf + " Tel: " + C.Telefono + " Email: " + C.Email
                TextBox1.Text += vbCrLf + "Dirección: " + C.Direccion + ", " + C.Ciudad + ", " + C.CP + ", " + C.Estado + ", " + C.Pais
                ' TextBox1.Text += vbCrLf + "Dirección 2: " + C.Direccion2 + ", " + C.Ciudad2 + ", " + C.CP2 + ", " + C.Estado2 + ", " + C.Pais2
                'Dim CE As dbClientesEquipos
                Dim CE As New dbServiciosEquipos(MySqlcon)
                CE.LlenadatosSucursales(S.idEquipo)
                If CE.ID <> 0 Then
                    TextBox7.Text = "Descripción: " + CE.nombre + vbCrLf + "Matrícula: " + CE.matricula + vbCrLf + "No. Serie: " + CE.serie + vbCrLf + "Modelo: " + CE.modelo
                End If
                txtFolio.Text = Format(S.Folio, "0000")
                txtSerie.Text = S.Serie
                ComboBox6.SelectedIndex = IdsTecnicos2.Busca(S.IdTecnico)
                ComboBox3.SelectedIndex = IdsTecnicos.Busca(S.IdTecnico)
                ConsultaEventossuc()
                NuevoEvento()
                ConsultaTotalesSuc()
            End If
           
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        LlenaCombos("tblserviciosclasificaciones2", ComboBox5, "nombre", "nombrec", "idclasificacion2", IDsClas2, "idclasificacion=" + IdsClas.Valor(ComboBox3.SelectedIndex).ToString, "GENERAL")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim errores As Boolean = False
            Dim er As String = ""
            Dim S As New dbServicios(MySqlcon)
            Dim idEvento1 As Integer
            If txtFolio.Text = "" Then
                errores = True
                er = "Indique un número de folio"

            End If
            If ComboBox2.SelectedIndex <= 0 Then
                idEvento1 = 0
            Else
                idEvento1 = IdsEstado.Valor(ComboBox2.SelectedIndex)
            End If
            If errores = False Then
                Dim P As New dbServiciosEventos(MySqlcon)
                If P.esTerminal(IdsEstado.Valor(ComboBox2.SelectedIndex)) Then

                    If MsgBox("Esta seleccionando un estado terminal, una vez modificado no podrá hacer ajustes. ¿Desea modificar este estado?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If TipoEquipo = 0 Then
                            S.Modificar(IdServicio, TextBox2.Text, Format(Date.Now, "yyyy/MM/dd"), Format(Date.Now, "HH:mm"), IdsEstado.Valor(ComboBox2.SelectedIndex), 0, ComboBox1.SelectedIndex, txtSerie.Text, Integer.Parse(txtFolio.Text), IdsTecnicos2.Valor(ComboBox6.SelectedIndex))
                        Else
                            S.ModificarSuc(IdServicio, TextBox2.Text, Format(Date.Now, "yyyy/MM/dd"), Format(Date.Now, "HH:mm"), IdsEstado.Valor(ComboBox2.SelectedIndex), 0, ComboBox1.SelectedIndex, txtSerie.Text, Integer.Parse(txtFolio.Text), IdsTecnicos2.Valor(ComboBox6.SelectedIndex))
                        End If
                        PopUp("Modificado", 90)
                        cerrado()
                    End If
                Else
                    If TipoEquipo = 0 Then
                        S.Modificar(IdServicio, TextBox2.Text, Format(Date.Now, "yyyy/MM/dd"), Format(Date.Now, "HH:mm"), IdsEstado.Valor(ComboBox2.SelectedIndex), 0, ComboBox1.SelectedIndex, txtSerie.Text, Integer.Parse(txtFolio.Text), IdsTecnicos2.Valor(ComboBox6.SelectedIndex))
                    Else
                        S.ModificarSuc(IdServicio, TextBox2.Text, Format(Date.Now, "yyyy/MM/dd"), Format(Date.Now, "HH:mm"), IdsEstado.Valor(ComboBox2.SelectedIndex), 0, ComboBox1.SelectedIndex, txtSerie.Text, Integer.Parse(txtFolio.Text), IdsTecnicos2.Valor(ComboBox6.SelectedIndex))
                    End If
                    PopUp("Modificado", 90)
                End If
               

            Else
                MsgBox(er, MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            Dim SE As New dbServiciosEventos(MySqlcon)
            If Button2.Text = "Agregar" Then
                If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosDetallesAgregarEstatus, PermisosN.Secciones.Servicios) = True) Then
                  
                    If TipoEquipo = 0 Then
                        SE.Guardar(IdServicio, IdsClas.Valor(ComboBox4.SelectedIndex), IDsClas2.Valor(ComboBox5.SelectedIndex), TextBox3.Text, CDbl(TextBox4.Text), CDbl(TextBox5.Text), IdsTecnicos.Valor(ComboBox3.SelectedIndex), idEquipo, fecha(), Double.Parse(txtIVA.Text))
                    Else
                        SE.Guardarsuc(IdServicio, IdsClas.Valor(ComboBox4.SelectedIndex), IDsClas2.Valor(ComboBox5.SelectedIndex), TextBox3.Text, CDbl(TextBox4.Text), CDbl(TextBox5.Text), IdsTecnicos.Valor(ComboBox3.SelectedIndex), idEquipo, fecha(), Double.Parse(txtIVA.Text))
                    End If

                    IdEvento = SE.ID
                    PopUp("Evento Agregado", 90)
                    Button2.Text = "Modificar"
                    Button3.Enabled = True
                    'Button5.Enabled = True
                    'Button6.Enabled = True
                    ConsultaEventos()
                    ConsultaTotales()
                    daIVA()
                    NuevoEvento()
                Else
                    MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            Else
                If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosDetallesModificarEstatus, PermisosN.Secciones.Servicios) = True) Then

                    If TipoEquipo = 0 Then
                        SE.Modificar(IdEvento, IdsClas.Valor(ComboBox4.SelectedIndex), IDsClas2.Valor(ComboBox5.SelectedIndex), TextBox3.Text, CDbl(TextBox4.Text), CDbl(TextBox5.Text), IdsTecnicos.Valor(ComboBox3.SelectedIndex), fecha(), Double.Parse(txtIVA.Text))
                    Else
                        SE.Modificarsuc(IdEvento, IdsClas.Valor(ComboBox4.SelectedIndex), IDsClas2.Valor(ComboBox5.SelectedIndex), TextBox3.Text, CDbl(TextBox4.Text), CDbl(TextBox5.Text), IdsTecnicos.Valor(ComboBox3.SelectedIndex), fecha(), Double.Parse(txtIVA.Text))
                    End If

                    PopUp("Evento Modificado", 90)
                    NuevoEvento()
                    ConsultaEventos()
                    ConsultaTotales()
                    daIVA()
                    NuevoEvento()
                Else
                    MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Function fecha() As String
        Dim fechita As String
        fechita = dtpFecha.Value.ToString("yyyy/MM/dd")
        Return fechita
    End Function
    Private Function ConsultaEventos() As DataSet
         Dim dataSet As DataSet = New DataSet
        Try
            Dim SE As New dbServiciosEventos(MySqlcon)
            If TipoEquipo = 0 Then
                DGEventos.DataSource = SE.Consulta(IdServicio, idEquipo)
            Else
                DGEventos.DataSource = SE.ConsultaSuc(IdServicio, idEquipo)
            End If

            DGEventos.Columns(0).Visible = False
            DGEventos.Columns(1).HeaderText = "Clasificación"
            DGEventos.Columns(2).HeaderText = "Subclasificación"
            DGEventos.Columns(3).HeaderText = "Comentario"
            DGEventos.Columns(4).HeaderText = "Precio"
            DGEventos.Columns(5).HeaderText = "Minutos"
            DGEventos.Columns(6).HeaderText = "Fecha"
            DGEventos.Columns(7).HeaderText = "IVA"
            DGEventos.Columns(5).Visible = False
            DGEventos.Columns(7).Visible = False
            DGEventos.Columns(1).Width = 130
            DGEventos.Columns(2).Width = 130
            DGEventos.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            If TipoEquipo = 0 Then
                dataSet.Tables.Add(SE.Consulta(IdServicio, idEquipo).ToTable())
            Else
                dataSet.Tables.Add(SE.ConsultaSuc(IdServicio, idEquipo).ToTable())
            End If

            ' DataSet.WriteXmlSchema("tblServicioEventos.xml")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        Return dataSet
    End Function
    Private Function ConsultaEventossuc() As DataSet
        Dim dataSet As DataSet = New DataSet
        Try
            Dim SE As New dbServiciosEventos(MySqlcon)
            DGEventos.DataSource = SE.ConsultaSuc(IdServicio, idEquipo)
            DGEventos.Columns(0).Visible = False
            DGEventos.Columns(1).HeaderText = "Clasificación"
            DGEventos.Columns(2).HeaderText = "Subclasificación"
            DGEventos.Columns(3).HeaderText = "Comentario"
            DGEventos.Columns(4).HeaderText = "Precio"
            DGEventos.Columns(5).HeaderText = "Minutos"
            DGEventos.Columns(6).HeaderText = "Fecha"
            DGEventos.Columns(7).HeaderText = "IVA"
            DGEventos.Columns(5).Visible = False
            DGEventos.Columns(7).Visible = False
            DGEventos.Columns(1).Width = 130
            DGEventos.Columns(2).Width = 130
            DGEventos.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill


            dataSet.Tables.Add(SE.ConsultaSuc(IdServicio, idEquipo).ToTable())
            ' DataSet.WriteXmlSchema("tblServicioEventos.xml")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        Return dataSet
    End Function
    Private Sub LlenaDatosEventos()
        Try
            ConsultaOn = False
            
            IdEvento = DGEventos.Item(0, DGEventos.CurrentCell.RowIndex).Value
            Dim S As New dbServiciosEventos(IdEvento, MySqlcon)
            If TipoEquipo <> 0 Then
                S.llenaDatosSuc(IdEvento)
            End If

            ComboBox4.SelectedIndex = IdsClas.Busca(S.IdClasificacion)
            ComboBox5.SelectedIndex = IDsClas2.Busca(S.IdClasificacion2)
            ComboBox3.SelectedIndex = IdsTecnicos.Busca(S.IdTecnico)
            TextBox3.Text = S.Comentario
            TextBox4.Text = S.Tiempo.ToString
            TextBox5.Text = S.Precio.ToString
            Button2.Text = "Modificar"
            Button3.Enabled = True
            dtpFecha.Value = S.fecha
            txtIVA.Text = S.iva
            'Button5.Enabled = True
            ' Button6.Enabled = True
            ConsultaOn = True
            ' ConsultaInventarioUtilizado()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        NuevoEvento()
    End Sub

    Private Sub DGEventos_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGEventos.CellClick
        LlenaDatosEventos()
    End Sub

    Private Sub DGEventos_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGEventos.CellContentClick

    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        Try
            'If ConsultaOn Then
            Dim SC As New dbServiciosClasificaciones2(IDsClas2.Valor(ComboBox5.SelectedIndex), MySqlcon)
            TextBox5.Text = SC.Precio.ToString
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub ConsultaTotales()
    
        lblManoObra.Text = Format(filtroManoObra(), "0.00")
        filtroArticulosAnadidos()
        lblTotalTotales.Text = Format((Double.Parse(lblManoObra.Text) + Double.Parse(lblArticulos.Text)), "0.00")
    End Sub
    Private Sub ConsultaTotalesSuc()

        lblManoObra.Text = Format(filtroManoObraSuc(), "0.00")
        filtroArticulosAnadidossuc()
        lblTotalTotales.Text = Format((Double.Parse(lblManoObra.Text) + Double.Parse(lblArticulos.Text)), "0.00")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            ' If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos) = True Then
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosDetallesEliminarEstatus, PermisosN.Secciones.Servicios) = True) Then
                    Dim P As New dbServiciosEventos(MySqlcon)
                    If TipoEquipo = 0 Then
                        P.Eliminar(IdEvento)
                    Else
                        P.EliminarSuc(IdEvento)
                    End If

                    NuevoEvento()
                    ConsultaEventos()
                    ConsultaTotales()
                    daIVA()
                    PopUp("Eliminado", 90)
                Else
                    MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnAgregarArticulo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarArticulo.Click
        cargarVentanaArticulos()
    End Sub

    Private Sub cargarVentanaArticulos()
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosAgregarArticuloVer, PermisosN.Secciones.Servicios) = True) Then
            Dim B As New fmrAgregarArticulo(IdServicio, idEquipo, TipoEquipo)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                lblArticulos.Text = Format(filtroArticulosAnadidos(), "0.00")
                ConsultaTotales()

            End If
            filtroArticulosAnadidos2()
            daIVA()
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Function filtroArticulosAnadidos() As Double
        'filtro todos
        totalAnadidos = 0
        Dim P As New dbServiciosEventos(MySqlcon)
        Dim Tabla As DataTable
        If TipoEquipo = 0 Then
            Tabla = P.filtroArticulosConsumidos(IdServicio)
        Else
            Tabla = P.filtroArticulosConsumidossuc(IdServicio)
        End If

        For i As Integer = 0 To Tabla.Rows.Count() - 1
            totalAnadidos = totalAnadidos + Double.Parse(Tabla.Rows(i)(5).ToString)
        Next
        Return totalAnadidos
    End Function
    Private Function filtroArticulosAnadidossuc() As Double
        'filtro todos
        totalAnadidos = 0
        Dim P As New dbServiciosEventos(MySqlcon)
        Dim Tabla As DataTable
        Tabla = P.filtroArticulosConsumidossuc(IdServicio)
        For i As Integer = 0 To Tabla.Rows.Count() - 1
            totalAnadidos = totalAnadidos + Double.Parse(Tabla.Rows(i)(5).ToString)
        Next
        Return totalAnadidos
    End Function
    Private Function filtroManoObra() As Double
        'filtro todos
        Dim totalMano = 0
        Dim P As New dbServiciosEventos(MySqlcon)
        Dim Tabla As DataTable
        If TipoEquipo = 0 Then
            Tabla = P.Consulta(IdServicio, idEquipo).ToTable()
        Else
            Tabla = P.ConsultaSuc(IdServicio, idEquipo).ToTable()
        End If

        For i As Integer = 0 To Tabla.Rows.Count() - 1
            totalMano = totalMano + Double.Parse(Tabla.Rows(i)(4).ToString)
        Next
        Return totalMano
    End Function
    Private Function filtroManoObraSuc() As Double
        'filtro todos
        Dim totalMano = 0
        Dim P As New dbServiciosEventos(MySqlcon)
        Dim Tabla As DataTable
        Tabla = P.ConsultaSuc(IdServicio, idEquipo).ToTable()
        For i As Integer = 0 To Tabla.Rows.Count() - 1
            totalMano = totalMano + Double.Parse(Tabla.Rows(i)(4).ToString)
        Next
        Return totalMano
    End Function

    Private Sub frmServiciosDetalles_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

 

    Private Sub txtFolio_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFolio.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub DGInventario_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGInventario.CellContentDoubleClick

    End Sub

    Private Sub DGInventario_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGInventario.DoubleClick
        cargarVentanaArticulos()
    End Sub
    '**************************************pendiente
    Private Sub btnFacturar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFacturar.Click
        'Facturar
        Dim Op As New dbOpciones(MySqlcon)
        Dim C As New dbVentas(MySqlcon)
        Dim S As New dbServicios(IdServicio, MySqlcon)
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Dim CD As New dbVentasInventario(MySqlcon)
        Dim V As New dbVentas(MySqlcon)
        Dim inv As New dbInventario(MySqlcon)
        Dim serie As String
        Dim Folio As String
        Dim idVenta As Integer


        Sf.BuscaFolios(GlobalIdSucursalDefault, dbSucursalesFolios.TipoDocumentos.Factura, GlobalTipoFacturacion)
        serie = Sf.Serie
        Folio = V.DaNuevoFolio(serie, GlobalIdSucursalDefault, GlobalTipoFacturacion, Op._ModoFoliosB).ToString
        Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
        'Guardar Remision
        C.Guardar(S.IdCliente, Date.Now.ToString("yyyy/MM/dd"), Folio, 0, 0, serie, Sf.NoAprobacion, Sc.NoSerie, Sf.YearAprobacion, GlobalTipoFacturacion, GlobalIdSucursalDefault, 1, 1, 2, 0, 0, 1, 0, 0, 0, "G01", "", "")
        idVenta = C.ID
        'Guardar detalles Remisión
        '  * Guardar Eventos *

        For i As Integer = 0 To DGEventos.RowCount - 1
            inv.ID = inv.buscarNoInventariable()
            inv.LlenaDatos()
            CD.Guardar(idVenta, inv.ID, 1, DGEventos.Item(4, i).Value, 2, DGEventos.Item(3, i).Value, 1, DGEventos.Item(7, i).Value, 0, 1, 0, 0, 1, inv.TipoContenido.ID, 0, 0, "", 0, "")
        Next
        For i As Integer = 0 To DGInventario.RowCount - 1
            inv.ID = DGInventario.Item(2, i).Value
            inv.LlenaDatos()
            CD.Guardar(idVenta, DGInventario.Item(2, i).Value, 1, DGInventario.Item(5, i).Value, 2, DGInventario.Item(4, i).Value, 1, inv.Iva, 0, 1, 0, 0, 1, inv.TipoContenido.ID, inv.ieps, inv.ivaRetenido, "", 0, "")
        Next

        Dim remi As New frmVentasN(idVenta, 0, Double.Parse(lblTotalTotales.Text), S.IdCliente)
        remi.Show()

    End Sub
    'end pendiente
    'Pendiente
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim S As New dbServicios(IdServicio, MySqlcon)
        If TipoEquipo <> 0 Then
            S.LlenadatosSucursales(IdServicio)
        End If
        Dim C As New dbClientes(S.IdCliente, MySqlcon)
        Dim CE As dbClientesEquipos
        Dim SE As New dbServiciosEquipos(MySqlcon)
        Dim datos As String = ""
        CE = SE.BuscaEquipo(S.ID)

        Rep = New repServiciosDetalles

        'Rep.SetDataSource(filtroArticulosAnadidos2())
        Rep.SetDataSource(ConsultaEventos())
        Rep.Subreports(0).SetDataSource(filtroArticulosAnadidos2())


        '  Rep.SetParameterValue("serie", "CO")
        Rep.SetParameterValue("folio", S.Serie + S.Folio.ToString("000"))

        If TipoEquipo = 0 Then
            Rep.SetParameterValue("datos", "DATOS DEL CLIENTE")
        Else
            Rep.SetParameterValue("datos", "DATOS SUCURSAL")
        End If
        If TipoEquipo = 0 Then
            Rep.SetParameterValue("clienteNombre", C.Nombre)
            Rep.SetParameterValue("clienteTelefono", "TEL: " + C.Telefono)
            Rep.SetParameterValue("clienteDireccion", "DIRECCIÓN: " + C.Direccion + ", " + C.Ciudad + ", " + C.CP)
            Rep.SetParameterValue("clienteDireccion2", C.Estado + ", " + C.Pais)

            Rep.SetParameterValue("clienteCodigo", C.Clave)
            Rep.SetParameterValue("equipoNombre", CE.Nombre)
            Rep.SetParameterValue("equipoMatricula", "MATRÍCULA: " + CE.Matricula)
            Rep.SetParameterValue("equipoNoSerie", "No. SERIE: " + CE.NoSerie)
            Rep.SetParameterValue("equipoSerie", "MODELO: " + CE.Modelo)
        Else
            Dim C2 As New dbSucursales(S.IdCliente, MySqlcon)
            Rep.SetParameterValue("clienteNombre", C2.Nombre)
            Rep.SetParameterValue("clienteTelefono", "TEL: " + C2.Telefono)
            Rep.SetParameterValue("clienteDireccion", "DIRECCIÓN: " + C2.Direccion + ", " + C2.Ciudad + ", " + C2.CP)
            Rep.SetParameterValue("clienteDireccion2", C2.Estado + ", " + C2.Pais)

            Rep.SetParameterValue("clienteCodigo", C2.Clave)
            SE.LlenadatosSucursales(S.idEquipo)
            Rep.SetParameterValue("equipoNombre", SE.nombre)
            Rep.SetParameterValue("equipoMatricula", "MATRÍCULA: " + SE.matricula)
            Rep.SetParameterValue("equipoNoSerie", "No. SERIE: " + SE.serie)
            Rep.SetParameterValue("equipoSerie", "MODELO: " + SE.modelo)
        End If
        
        

        Rep.SetParameterValue("totalArticulos", Double.Parse(lblArticulos.Text))

        Rep.SetParameterValue("totaltoales", Double.Parse(lblTotalTotales.Text))






        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub
    'end pendiente
    'pendiente
    Private Sub btnRemision_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemision.Click
        Dim C As New dbVentasRemisiones(MySqlcon)
        Dim S As New dbServicios(IdServicio, MySqlcon)
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Dim CD As New dbVentasRemisionesInventario(MySqlcon)
        Dim V As New dbVentasRemisiones(MySqlcon)
        Dim inv As New dbInventario(MySqlcon)
        Dim serie As String
        Dim Folio As String
        Dim idRemision As Integer

        Sf.BuscaFolios(GlobalIdSucursalDefault, dbSucursalesFolios.TipoDocumentos.Remision, 0)
        serie = Sf.Serie
        Folio = V.DaNuevoFolio(serie, GlobalIdSucursalDefault).ToString
        'Guardar Remision
        C.Guardar(S.IdCliente, Date.Now.ToString("yyyy/MM/dd"), Folio, 0, 0, GlobalIdSucursalDefault, serie, 1, 2, 1, "")
        idRemision = C.ID
        'Guardar detalles Remisión
        '  * Guardar Eventos *
        For i As Integer = 0 To DGEventos.RowCount - 1
            inv.ID = inv.buscarNoInventariable()
            inv.LlenaDatos()
            CD.Guardar(idRemision, inv.ID, 1, DGEventos.Item(4, i).Value, 2, DGEventos.Item(3, i).Value, 1, DGEventos.Item(7, i).Value, 0, 1, 0, 0, 0, 1, inv.TipoContenido.ID, 0, "")
        Next
        For i As Integer = 0 To DGInventario.RowCount - 1
            inv.ID = DGInventario.Item(2, i).Value
            inv.LlenaDatos()
            CD.Guardar(idRemision, DGInventario.Item(2, i).Value, 1, DGInventario.Item(5, i).Value, 2, DGInventario.Item(4, i).Value, 1, inv.Iva, 0, 1, 0, inv.ieps, inv.ivaRetenido, 1, inv.TipoContenido.ID, 0, "")
        Next

        Dim remi As New frmVentasRemisiones(idRemision)
        remi.Show()

    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIVA.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
    'pendiente

    Private Sub txtIVA_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIVA.Leave
        If txtIVA.Text = "" Then
            txtIVA.Text = "0"
        End If
    End Sub

    Private Sub daIVA()
        Dim inv As New dbInventario(MySqlcon)
        Dim iva As Double = 0
        Dim aux As Double = 0
        Dim ieps As Double = 0
        Dim ivaRet As Double = 0
        Dim precio As Double = 0

        For i As Integer = 0 To DGEventos.RowCount - 1
            aux = (Double.Parse(DGEventos.Item(7, i).Value) * Double.Parse(DGEventos.Item(4, i).Value)) / 100
            iva += aux
        Next

        For i As Integer = 0 To DGInventario.RowCount - 1
            inv.ID = DGInventario.Item(2, i).Value
            inv.LlenaDatos()
            Dim numero As String = ""
            Dim numCom As String = DGInventario.Item(7, i).Value.ToString
            For k As Integer = 1 To numCom.Length() - 1
                numero += numCom.Chars(k)
            Next

            precio = Double.Parse(numero)
            aux = Double.Parse(inv.Iva)
            aux = (Double.Parse(numero) * Double.Parse(inv.Iva)) / 100
            ieps = (Double.Parse(numero) * Double.Parse(inv.ieps)) / 100
            ivaRet = (Double.Parse(numero) * Double.Parse(inv.ivaRetenido)) / 100


            iva += aux
            iva += ieps
            iva -= ivaRet

            lblIVA.Text = iva.ToString("0.00")
            lblTotalTotales.Text = (Double.Parse(lblArticulos.Text) + Double.Parse(lblManoObra.Text) + iva).ToString("0.00")
        Next

    End Sub

    Private Sub imprimirTicket()
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim S As New dbServicios(IdServicio, MySqlcon)
        If TipoEquipo <> 0 Then
            S.LlenadatosSucursales(IdServicio)
        End If
        Dim C As New dbClientes(S.IdCliente, MySqlcon)
        Dim CE As dbClientesEquipos
        Dim SE As New dbServiciosEquipos(MySqlcon)
        CE = SE.BuscaEquipo(S.ID)
        If RadioButton1.Checked = True Then
            Rep = New repServicioDetalleTicket
        Else
            Rep = New repServicioDetallesTicket2
        End If


        'Rep.SetDataSource(filtroArticulosAnadidos2())

        Rep.SetDataSource(ConsultaEventos())
        Rep.Subreports(0).SetDataSource(filtroArticulosAnadidos2())

        Rep.SetParameterValue("folio", S.Serie + S.Folio.ToString("000"))
        Rep.SetParameterValue("serie", "")
        ' Rep.SetParameterValue("folio", "0001")

        'Rep.SetParameterValue("clienteCodigo", C.Clave)
        'Rep.SetParameterValue("clienteNombre", C.Nombre)
        'Rep.SetParameterValue("clienteTelefono", "Tel: " + C.Telefono)
        'Rep.SetParameterValue("clienteDireccion", "Dirección: " + C.Direccion + ", " + C.Ciudad + ", " + C.CP)
        'Rep.SetParameterValue("clienteDireccion2", C.Estado + ", " + C.Pais)
        'Rep.SetParameterValue("equipoNombre", CE.Nombre)
        'Rep.SetParameterValue("equipoMatricula", "Matrícula: " + CE.Matricula)
        'Rep.SetParameterValue("equipoNoSerie", "No. Serie: " + CE.NoSerie)
        'Rep.SetParameterValue("equipoSerie", "Modelo: " + CE.Modelo)
        If TipoEquipo = 0 Then
            Rep.SetParameterValue("datos", "DATOS DEL CLIENTE")
            Rep.SetParameterValue("clienteNombre", C.Nombre)
            Rep.SetParameterValue("clienteTelefono", "TEL: " + C.Telefono)
            Rep.SetParameterValue("clienteDireccion", "DIRECCIÓN: " + C.Direccion + ", " + C.Ciudad + ", " + C.CP)
            Rep.SetParameterValue("clienteDireccion2", C.Estado + ", " + C.Pais)

            'Rep.SetParameterValue("clienteCodigo", C.Clave)
            Rep.SetParameterValue("equipoNombre", CE.Nombre)
            Rep.SetParameterValue("equipoMatricula", "MATRÍCULA: " + CE.Matricula)
            Rep.SetParameterValue("equipoNoSerie", "No. SERIE: " + CE.NoSerie)
            Rep.SetParameterValue("equipoSerie", "MODELO: " + CE.Modelo)
        Else
            Rep.SetParameterValue("datos", "DATOS SUCURSA")
            Dim C2 As New dbSucursales(S.IdCliente, MySqlcon)
            Rep.SetParameterValue("clienteNombre", C2.Nombre)
            Rep.SetParameterValue("clienteTelefono", "TEL: " + C2.Telefono)
            Rep.SetParameterValue("clienteDireccion", "DIRECCIÓN: " + C2.Direccion + ", " + C2.Ciudad + ", " + C2.CP)
            Rep.SetParameterValue("clienteDireccion2", C2.Estado + ", " + C2.Pais)
            ' Rep.SetParameterValue("clienteCodigo", C.Clave)
            '  Rep.SetParameterValue("clienteCodigo", C2.Clave)
            SE.LlenadatosSucursales(S.idEquipo)
            Rep.SetParameterValue("equipoNombre", SE.nombre)
            Rep.SetParameterValue("equipoMatricula", "MATRÍCULA: " + SE.matricula)
            Rep.SetParameterValue("equipoNoSerie", "No. SERIE: " + SE.serie)
            Rep.SetParameterValue("equipoSerie", "MODELO: " + SE.modelo)
        End If
        Rep.SetParameterValue("totalArticulos", Double.Parse(lblArticulos.Text))

        Rep.SetParameterValue("totaltoales", Double.Parse(lblTotalTotales.Text))
        Rep.SetParameterValue("iva", Double.Parse(lblIVA.Text))


        Dim PrintLayout As New CrystalDecisions.Shared.PrintLayoutSettings
        PrintLayout.Scaling = CrystalDecisions.Shared.PrintLayoutSettings.PrintScaling.Scale
        Dim PS As New System.Drawing.Printing.PrinterSettings
        PS.PrinterName = txtImpresora.Text
        Dim pageSettings As New System.Drawing.Printing.PageSettings(PS)
        Rep.PrintOptions.PrinterName = txtImpresora.Text
        Rep.PrintOptions.DissociatePageSizeAndPrinterPaperSize = True
        'Rep.PrintToPrinter(1, False, 0, 0)
        Rep.PrintToPrinter(PS, pageSettings, False, PrintLayout)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If txtImpresora.Text <> "" Then

            imprimirTicket()

        Else
            MsgBox("Debe seleccionar una impresora.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If


    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtImpresora.Text = PrintDialog1.PrinterSettings.PrinterName
            My.Settings.impresoraServicios = txtImpresora.Text
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.pulgadasServicios = 0
        
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            My.Settings.pulgadasServicios = 1
        
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If llenando = False Then
            Dim P As New dbServiciosEventos(MySqlcon)
            If P.esTerminal(IdsEstado.Valor(ComboBox2.SelectedIndex)) Then
                ComboBox1.SelectedIndex = 1
            Else
                ComboBox1.SelectedIndex = 0
            End If
        End If
        
    End Sub
    Private Sub cerrado()
        TextBox2.Enabled = False
        ComboBox2.Enabled = False
        ComboBox1.Enabled = False
        DateTimePicker1.Enabled = False
        Button1.Enabled = False

        GroupBox1.Enabled = False

        txtSerie.Enabled = False
        txtFolio.Enabled = False
    End Sub

End Class