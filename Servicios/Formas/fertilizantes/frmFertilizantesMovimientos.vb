Public Class frmFertilizantesMovimientos
    Dim FM As dbFertilizantesMovimientos
    Dim IdSucursal As Integer

    Dim IdsGenerico As New elemento
    Dim IdsAlmacenes As New elemento
    Dim IdsAlmacenes2 As New elemento
    Dim Precio As Double
    Dim PrecioU As Double
    Dim TipoImpresora As Byte
    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    Dim CadenaCFDI As String
    Dim CodigoBidimensional As Bitmap
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim CuantaY As Integer
    Dim Restante As Double
    Dim FolioAnt As Integer
    Dim SerieAnt As String
    Public Sub New(pIdPedido As Integer, pIdinventario As Integer, pIdMovimiento As Integer, pDescripcion As String, pPrecio As String, pIdSucursal As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        FM = New dbFertilizantesMovimientos(MySqlcon)
        FM.IdPedido = pIdPedido
        FM.IdInventario = pIdinventario
        FM.Id = pIdMovimiento
        Label25.Text = "Producto: " + pDescripcion
        PrecioU = pPrecio
        ' Add any initialization after the InitializeComponent() call.
        IdSucursal = pIdSucursal
    End Sub
    Private Sub frmFertilizantesMovimientos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            ComboBox1.Items.Add("Salida")
            ComboBox1.Items.Add("Envío")
            ComboBox1.Items.Add("Traspaso")
            ComboBox1.Items.Add("Devolución")
            ComboBox2.Items.Add("En tránsito")
            ComboBox2.Items.Add("Surtido")
            ComboBox1.SelectedIndex = 1
            ComboBox2.SelectedIndex = 0

            Restante = FM.ChecaTotalSurtido(FM.IdPedido, FM.IdInventario)
            Label12.Text = "Restante: " + Restante.ToString + " Kg."
            TextBox8.Text = "0"
            TextBox5.Text = "0"
            LlenaCombos("tblfertilizantesmovimientos", ComboBox3, " distinct chofer", "nombret", "idmovimiento", IdsGenerico, "chofer<>''", , "nombret", True)
            LlenaCombos("tblfertilizantesmovimientos", ComboBox4, " distinct inspector", "nombret", "idmovimiento", IdsGenerico, "inspector<>''", , "nombret", True)
            LlenaCombos("tblfertilizantesmovimientos", ComboBox5, " distinct vehiculo", "nombret", "idmovimiento", IdsGenerico, "vehiculo<>''", , "nombret", True)
            LlenaCombos("tblfertilizantesmovimientos", ComboBox6, " distinct placas", "nombret", "idmovimiento", IdsGenerico, "placas<>''", , "nombret", True)

            If FM.Id <> 0 Then
                LlenaCombos("tblalmacenes", cmbalmacenDest, "concat(nombre,' Tara=',convert(peso using utf8),' Existencia=',convert(spdainventario(" + FM.IdInventario.ToString + ",idalmacen," + IdSucursal.ToString + ") using utf8))", "nombret", "idalmacen", IdsAlmacenes2, "idalmacen<>1", , "tipo desc,nombret")
                LlenaCombos("tblalmacenes", cmbAlmacen, "concat(nombre,' Tara=',convert(peso using utf8),' Existencia=',convert(spdainventario(" + FM.IdInventario.ToString + ",idalmacen," + IdSucursal.ToString + ") using utf8))", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdSucursal.ToString, , "tipo,nombret")
                LlenaDatos()
            Else
                LlenaCombos("tblalmacenes", cmbalmacenDest, "concat(nombre,' Tara=',convert(peso using utf8),' Existencia=',convert(spdainventario(" + FM.IdInventario.ToString + ",idalmacen," + IdSucursal.ToString + ") using utf8))", "nombret", "idalmacen", IdsAlmacenes2, "idalmacen<>1 and if(ifnull((select false from tblfertilizantesmovimientos where idalmacen=tblalmacenes.idalmacen and estado=3 and estadosurtido=0 and tipo<>2 limit 1),true),ifnull((select false from tblfertilizantesmovimientos where idalmacendestino=tblalmacenes.idalmacen and estado=3 and estadosurtido=0 and tipo=2 limit 1),true),false)", , "tipo desc,nombret")
                LlenaCombos("tblalmacenes", cmbAlmacen, "concat(nombre,' Tara=',convert(peso using utf8),' Existencia=',convert(spdainventario(" + FM.IdInventario.ToString + ",idalmacen," + IdSucursal.ToString + ") using utf8))", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and if(ifnull((select false from tblfertilizantesmovimientos where idalmacen=tblalmacenes.idalmacen and estado=3 and estadosurtido=0 and tipo<>2 limit 1),true),ifnull((select false from tblfertilizantesmovimientos where idalmacendestino=tblalmacenes.idalmacen and estado=3 and estadosurtido=0 and tipo=2 limit 1),true),false) and idsucursal=" + IdSucursal.ToString, , "tipo,nombret")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        

    End Sub
    Private Sub LlenaDatos()
        FM.LlenaDatos(FM.Id)
        ComboBox1.SelectedIndex = FM.Tipo
        ComboBox2.SelectedIndex = FM.EstadoSurtido
        dtpFecha1.Value = FM.Fecha
        TextBox4.Text = FM.Serie
        SerieAnt = FM.Serie
        'TextBox4.Enabled = False
        TextBox6.Text = FM.Folio.ToString
        FolioAnt = FM.Folio
        'TextBox6.Enabled = False
        ComboBox1.Enabled = False
        cmbAlmacen.SelectedIndex = IdsAlmacenes.Busca(FM.Idalmacen)
        cmbalmacenDest.SelectedIndex = IdsAlmacenes2.Busca(FM.IdAlmacenDestino)
        cmbAlmacen.Enabled = False
        cmbalmacenDest.Enabled = False
        Precio = FM.Precio
        If FM.Surtido <> 0 Then
            PrecioU = Precio / FM.Surtido
        Else
            PrecioU = Precio / FM.PesoNeto
        End If
        TextBox5.Text = FM.PesoBruto.ToString
        'If FM.Tipo <> 3 Then TextBox5.Enabled = False
        TextBox1.Text = FM.Tara.ToString
        'If FM.Tipo <> 3 Then TextBox1.Enabled = False
        TextBox2.Text = FM.PesoNeto.ToString
        TextBox2.Enabled = False
        TextBox8.Text = FM.PesoBruto2.ToString
        TextBox3.Text = FM.Surtido.ToString
        'If FM.EstadoSurtido = 1 And FM.Tipo <> 3 Then
        '    TextBox8.Enabled = False
        '    TextBox3.Enabled = False
        'Else
        If FM.Tipo = 3 Then
            TextBox8.Enabled = True
            TextBox3.Enabled = True
        End If
        'End If
            DateTimePicker6.Value = FM.FechaCarga
            DateTimePicker1.Value = "2014/01/01 " + FM.HoraCarga
            DateTimePicker3.Value = FM.FechadeEnvio
            DateTimePicker5.Value = FM.FechaLlegada
            DateTimePicker4.Value = "2014/01/01 " + FM.HoraLlegada
            DateTimePicker2.Value = FM.FechaLlegada
            ComboBox3.Text = FM.Chofer
            ComboBox4.Text = FM.Inspector
            ComboBox5.Text = FM.Vehiculo
            ComboBox6.Text = FM.Placas
            TextBox14.Text = FM.Comentario

            Button3.Text = "Modificar"
            Button1.Enabled = True
            Button4.Enabled = True
        'If FM.EstadoSurtido = 1 And FM.Tipo <> 3 Then
        '    For Each C As Control In Me.Controls
        '        C.Enabled = False
        '    Next
        '    Button2.Enabled = True
        '    Button4.Enabled = True
        '    Button1.Enabled = True
        'End If
            If FM.Estado = Estados.Cancelada Then
                For Each C As Control In Me.Controls
                    C.Enabled = False
                Next
                Button2.Enabled = True
                Button4.Enabled = True
            End If

    End Sub
    
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim AgregaExcedendte As Boolean = False
            ComboBox3.Text = ComboBox3.Text.ToUpper
            ComboBox4.Text = ComboBox4.Text.ToUpper
            ComboBox5.Text = ComboBox5.Text.ToUpper
            ComboBox6.Text = ComboBox6.Text.ToUpper
            Dim NoErrores As Boolean = True
            Dim MsgError As String = ""
            If IsNumeric(TextBox5.Text) = False Then
                MsgError = "El peso bruto debe ser un valor numérico."
                NoErrores = False
            Else
                If CDbl(TextBox5.Text) = 0 Then
                    If MsgBox("El peso bruto esta en 0 ¿Desea continuar de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                        NoErrores = False
                        MsgError += "El peso bruto esta en 0."
                    End If
                End If
            End If
            If IsNumeric(TextBox1.Text) = False Then
                MsgError += " La tara debe ser un valor numérico."
                NoErrores = False
            End If
            If IsNumeric(TextBox2.Text) = False Then
                MsgError += " El peso neto debe ser un valor numérico."
                NoErrores = False
            End If
            If IsNumeric(TextBox8.Text) = False Then
                MsgError += " El peso bruto consumo debe ser un valor numérico."
                NoErrores = False
            End If
            If IsNumeric(TextBox5.Text) = False Then
                MsgError += " El peso bruto debe ser un valor numérico."
                NoErrores = False
            End If
            If IsNumeric(TextBox1.Text) = False Then
                MsgError += " El peso bruto de la tara debe ser un valor numérico."
                NoErrores = False
            End If
            If IsNumeric(TextBox2.Text) = False Then
                MsgError += " El peso neto debe ser un valor numérico."
                NoErrores = False
            End If
            If IsNumeric(TextBox3.Text) = False Then
                MsgError += " El peso neto consumo debe ser un valor numérico."
                NoErrores = False
            Else
                If CDbl(TextBox3.Text) < 0 Then
                    MsgError += " No se puede consumir una cantidad mayor a la surtida."
                    NoErrores = False
                End If

            End If

            If FM.ChecaFolioRepetido(CInt(TextBox6.Text), TextBox4.Text) And Button3.Text = "Guardar" Then
                'TextBox6.Text = FM.DaNuevoFolio(TextBox4.Text, IdSucursal)
                NoErrores = False
                MsgError += " Folio repetido."
            End If
            If Button3.Text <> "Guardar" And NoErrores Then
                If TextBox4.Text <> SerieAnt Or CInt(TextBox6.Text) <> FolioAnt Then
                    If FM.ChecaFolioRepetido(CInt(TextBox6.Text), TextBox4.Text) Then
                        NoErrores = False
                        MsgError += " Folio repetido."
                    End If
                End If
            End If
            If NoErrores And ComboBox2.SelectedIndex = 1 Then
                If CDbl(TextBox1.Text) > CDbl(TextBox8.Text) Then
                    TextBox8.Text = TextBox1.Text
                End If
            End If
            Dim Excedente As Double
            If ComboBox1.SelectedIndex < 3 Then
                Excedente = FM.ChecaTotalSurtido(FM.IdPedido, FM.IdInventario) - CDbl(TextBox3.Text)
                If Excedente < 0 Then
                    If MsgBox("Va a surtir una cantidad mayor a la pedida. ¿Continuar y agregar el excedente al pedido?", MsgBoxStyle.YesNo, GlobalNombreApp) <> MsgBoxResult.Yes Then
                        MsgError += " Se cancelo esta acción."
                        NoErrores = False
                    Else
                        AgregaExcedendte = True
                    End If
                End If
            End If


            If ComboBox1.SelectedIndex = 2 And Button3.Text = "Guardar" Then
                Dim Inv As New dbInventario(MySqlcon)
                Dim Existencia As Double
                Existencia = Inv.DaInventario(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), FM.IdInventario)
                If Existencia < CDbl(TextBox2.Text) Then
                    If MsgBox("El almacen origen no tiene existencia suficiente para hacer el traspaso. ¿Realizar el movimiento de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                        MsgError += " El almacen origen no tiene existencia suficiente para hacer el traspaso."
                        NoErrores = False
                    End If
                End If
                If IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex) = IdsAlmacenes2.Valor(cmbalmacenDest.SelectedIndex) Then
                    MsgError += " No se puede hacer un traspaso al mismo almacen."
                    NoErrores = False
                End If
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.MovimientosAlta, PermisosN.Secciones.Fertilizantes) = False And TextBox3.Text = "Guardar" Then
                MsgError += " No tiene permiso para realizar esta operación."
                NoErrores = False
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.MovimientosCambios, PermisosN.Secciones.Fertilizantes) = False And TextBox3.Text <> "Guardar" Then
                MsgError += " No tiene permiso para realizar esta operación."
                NoErrores = False
            End If

            If ComboBox1.SelectedIndex < 2 And Button3.Text = "Guardar" Then
                Dim Inv As New dbInventario(MySqlcon)
                Dim Existencia As Double
                Existencia = Inv.DaInventario(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), FM.IdInventario)
                If Existencia < CDbl(TextBox2.Text) Then
                    If MsgBox("No hay existencia suficiente realizar el movimiento. ¿Hacerlo de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                        MsgError += " No hay existencia suficiente."
                        NoErrores = False
                    End If
                End If
            End If
            If NoErrores Then
                If ComboBox2.SelectedIndex = 0 Then
                    Precio = CDbl(TextBox2.Text) * PrecioU
                End If
                If Button3.Text = "Guardar" Then
                    Dim IdAlmacen1 As Integer
                    Dim IdAlmacen2 As Integer
                    IdAlmacen1 = IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex)
                    If ComboBox1.SelectedIndex = 3 Then
                        IdAlmacen2 = IdAlmacen1
                    Else
                        IdAlmacen2 = IdsAlmacenes2.Valor(cmbalmacenDest.SelectedIndex)
                    End If

                    FM.Guardar(Format(dtpFecha1.Value, "yyyy/MM/dd"), TextBox4.Text.Trim, CInt(TextBox6.Text), Format(DateTimePicker3.Value, "yyyy/MM/dd"), IdAlmacen1, CDbl(TextBox5.Text), CDbl(TextBox1.Text), CDbl(TextBox2.Text), ComboBox3.Text.Trim, ComboBox4.Text.Trim, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(Date.Now, "HH:mm"), ComboBox1.SelectedIndex, IdAlmacen2, ComboBox5.Text.Trim, CDbl(TextBox3.Text), Precio, FM.IdPedido, TextBox14.Text.Trim, CDbl(TextBox8.Text), Format(DateTimePicker6.Value, "yyyy/MM/dd"), Format(DateTimePicker1.Value, "HH:mm"), Format(DateTimePicker5.Value, "yyyy/MM/dd"), Format(DateTimePicker4.Value, "HH:mm"), ComboBox6.Text.Trim, FM.IdInventario, ComboBox2.SelectedIndex)
                    If ComboBox1.SelectedIndex = 2 Then
                        FM.ModificaInventario(FM.Id, dbInventarioConceptos.Tipos.Traspaso)
                        If ComboBox2.SelectedIndex = 1 Then
                            FM.ModificaInventario(FM.Id, 5)
                        End If
                    Else
                        If ComboBox2.SelectedIndex = 1 And ComboBox1.SelectedIndex < 3 Then
                            FM.ModificaInventario(FM.Id, dbInventarioConceptos.Tipos.Salida)
                        End If
                    End If
                    If AgregaExcedendte And ComboBox2.SelectedIndex = 1 Then
                        Dim FD As New dbFertilizantesPedidoDetalles(MySqlcon)
                        Dim Inv As New dbInventario(FM.IdInventario, MySqlcon)
                        Excedente = Excedente * -1
                        FD.Guardar(FM.IdPedido, FM.IdInventario, Excedente, PrecioU * Excedente, 2, Inv.Nombre, Inv.Iva, 0, Inv.ieps, Inv.ivaRetenido, 1, Excedente, 0)
                        Dim FP As New dbFertilizantesPedido(MySqlcon)
                        FP.ModificaTotales(FM.IdPedido, 2)
                    End If

                    Try
                        Imprimir()
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                    End Try

                Else
                    If MsgBox("¿Guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Dim IdAlmacen1 As Integer
                        Dim IdAlmacen2 As Integer
                        IdAlmacen1 = IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex)
                        If ComboBox1.SelectedIndex = 3 Then
                            IdAlmacen2 = IdAlmacen1
                        Else
                            IdAlmacen2 = IdsAlmacenes2.Valor(cmbalmacenDest.SelectedIndex)
                        End If
                        FM.Modificar(FM.Id, Format(dtpFecha1.Value, "yyyy/MM/dd"), TextBox4.Text.Trim, CInt(TextBox6.Text), Format(DateTimePicker3.Value, "yyyy/MM/dd"), IdAlmacen1, CDbl(TextBox5.Text), CDbl(TextBox1.Text), CDbl(TextBox2.Text), ComboBox3.Text.Trim, ComboBox4.Text.Trim, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(Date.Now, "HH:mm"), ComboBox1.SelectedIndex, IdAlmacen2, ComboBox5.Text.Trim, Precio, FM.Estado, TextBox14.Text.Trim, CDbl(TextBox8.Text), Format(DateTimePicker6.Value, "yyyy/MM/dd"), Format(DateTimePicker1.Value, "HH:mm"), Format(DateTimePicker5.Value, "yyyy/MM/dd"), Format(DateTimePicker4.Value, "HH:mm"), ComboBox6.Text.Trim, CDbl(TextBox3.Text), ComboBox2.SelectedIndex)
                        If ComboBox1.SelectedIndex = 2 Then
                            If ComboBox2.SelectedIndex = 1 Then
                                FM.ModificaInventario(FM.Id, 5)
                            End If
                        Else
                            If ComboBox2.SelectedIndex = 1 Then
                                FM.ModificaInventario(FM.Id, dbInventarioConceptos.Tipos.Salida)
                            End If
                        End If
                        'If AgregaExcedendte And ComboBox2.SelectedIndex = 1 Then
                        '    Dim FD As New dbFertilizantesPedidoDetalles(MySqlcon)
                        '    Dim Inv As New dbInventario(FM.IdInventario, MySqlcon)
                        '    Excedente = Excedente * -1
                        '    FD.Guardar(FM.IdPedido, FM.IdInventario, Excedente, PrecioU * Excedente, 2, Inv.Nombre, Inv.Iva, 0, Inv.ieps, Inv.ivaRetenido, 1, Excedente, 0)
                        '    Dim FP As New dbFertilizantesPedido(MySqlcon)
                        '    FP.ModificaTotales(FM.IdPedido, 2)
                        'End If
                        Try
                            Imprimir()
                        Catch ex As Exception
                            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                        End Try

                    End If
                End If
                Me.Close()
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.MovimientosCancelar, PermisosN.Secciones.Fertilizantes) = False Then
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If MsgBox("¿Cancelar Movimiento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            FM.CancelarEstado(FM.Id)
            If ComboBox1.SelectedIndex = 2 Then
                FM.ModificaInventario(FM.Id, dbInventarioConceptos.Tipos.Ajuste)
                If ComboBox2.SelectedIndex = 1 Then
                    FM.ModificaInventario(FM.Id, 6)
                End If
            Else
                If ComboBox2.SelectedIndex = 1 Then
                    FM.ModificaInventario(FM.Id, dbInventarioConceptos.Tipos.Entrada)
                End If
            End If

            Me.Close()
        End If
    End Sub


    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        DibujaPaginaN(e.Graphics)
        If MasPaginas = True Or NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
        End If
        'If Estado = Estados.Cancelada Then
        '    e.Graphics.DrawString("CANCELADA", New Font("Arial", 18, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, 80, 130)
        'End If
        e.HasMorePages = MasPaginas
    End Sub

    Private Sub LlenaNodosImpresion()
        Try
            FM.LlenaDatos(FM.Id)
            Dim V As New dbFertilizantesPedido(FM.IdPedido, MySqlcon)
            Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
            'V.DaTotal(FM.Id, 2)
            Dim O As New dbOpciones(MySqlcon)
            Dim I As New dbInventario(FM.IdInventario, MySqlcon)
            Dim TotalDescuento As Double = 0
            Dim Vendedor As New dbVendedores(V.IdVendedor, MySqlcon)
            ImpND.Clear()

            ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
            ImpND.Add(New NodoImpresionN("", "direccion", Sucursal.Direccion, 0), "direccion")
            ImpND.Add(New NodoImpresionN("", "noexterior", Sucursal.NoExterior, 0), "noexterior")
            ImpND.Add(New NodoImpresionN("", "nointerior", Sucursal.NoInterior, 0), "nointerior")
            ImpND.Add(New NodoImpresionN("", "colonia", Sucursal.Colonia, 0), "colonia")
            ImpND.Add(New NodoImpresionN("", "ciudad", Sucursal.Ciudad, 0), "ciudad")
            ImpND.Add(New NodoImpresionN("", "estado", Sucursal.Estado, 0), "estado")
            ImpND.Add(New NodoImpresionN("", "cp", Sucursal.CP, 0), "cp")
            ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
            ImpND.Add(New NodoImpresionN("", "tel", Sucursal.Telefono, 0), "tel")
            ImpND.Add(New NodoImpresionN("", "email", Sucursal.Email, 0), "email")
            ImpND.Add(New NodoImpresionN("", "referencia", Sucursal.ReferenciaDomicilio, 0), "referencia")
            ImpND.Add(New NodoImpresionN("", "municipio", Sucursal.Municipio, 0), "municipio")
            ImpND.Add(New NodoImpresionN("", "vendedor", Vendedor.Nombre, 0), "vendedor")
            ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
            ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
            ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
            If V.Cliente.DireccionFiscal = 0 Then
                ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
                ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
                ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
                ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
                ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
                ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
                ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
                ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
                ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio, 0), "refcliente")
            Else
                ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion2, 0), "direccioncliente")
                ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior2, 0), "noexteriorcliente")
                ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior2, 0), "nointeriorcliente")
                ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia2, 0), "coloniacliente")
                ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP2, 0), "cpcliente")
                ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad2, 0), "ciudadcliente")
                ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado2, 0), "estadocliente")
                ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio2, 0), "municipiocliente")
                ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
            End If
            ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")

            ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")

            ImpND.Add(New NodoImpresionN("", "serie", FM.Serie, 0), "serie")
            ImpND.Add(New NodoImpresionN("", "folio", FM.Folio, 0), "folio")
            ImpND.Add(New NodoImpresionN("", "fecha", FM.Fecha, 0), "fecha")
            ImpND.Add(New NodoImpresionN("", "hora", FM.Hora, 0), "hora")

            ImpND.Add(New NodoImpresionN("", "tipomov", FM.TipoMovimientoString, 0), "tipomov")

            ImpND.Add(New NodoImpresionN("", "almacen", FM.Almacen.Nombre, 0), "almacen")
            ImpND.Add(New NodoImpresionN("", "almacendest", FM.AlmacenDestino.Nombre, 0), "almacendest")
            ImpND.Add(New NodoImpresionN("", "estados", FM.EstadoString, 0), "estados")
            ImpND.Add(New NodoImpresionN("", "estadosurtido", FM.EstadoSurtidoString, 0), "estadosurtido")
            ImpND.Add(New NodoImpresionN("", "pesobruto", FM.PesoBruto.ToString, 0), "pesobruto")
            ImpND.Add(New NodoImpresionN("", "tara", FM.Tara.ToString, 0), "tara")
            ImpND.Add(New NodoImpresionN("", "pesoneto", FM.PesoNeto.ToString, 0), "pesoneto")
            ImpND.Add(New NodoImpresionN("", "pesobruto2", FM.PesoBruto2.ToString, 0), "pesobruto2")
            ImpND.Add(New NodoImpresionN("", "consumo", FM.Surtido.ToString, 0), "consumo")
            ImpND.Add(New NodoImpresionN("", "importeneto", Format(CDbl(TextBox2.Text) * PrecioU, "#,###,##0.00"), 0), "importeneto")
            ImpND.Add(New NodoImpresionN("", "importeconsumo", FM.Precio.ToString, 0), "importeconsumo")
            ImpND.Add(New NodoImpresionN("", "fechacarga", FM.FechaCarga, 0), "fechacarga")
            ImpND.Add(New NodoImpresionN("", "horacarga", FM.HoraCarga, 0), "horacarga")
            'ImpND.Add(New NodoImpresionN("", "fechacarga", FM.FechaCarga, 0), "fechacarga")
            ImpND.Add(New NodoImpresionN("", "fecharegreso", FM.FechaLlegada, 0), "fecharegreso")
            ImpND.Add(New NodoImpresionN("", "horaregreso", FM.HoraLlegada, 0), "horaregreso")
            ImpND.Add(New NodoImpresionN("", "fechaenvio", FM.FechadeEnvio, 0), "fechaenvio")
            ImpND.Add(New NodoImpresionN("", "fechadescarga", FM.FechadeDescarga, 0), "fechadescarga")
            ImpND.Add(New NodoImpresionN("", "chofer", FM.Chofer, 0), "chofer")
            ImpND.Add(New NodoImpresionN("", "inspector", FM.Inspector, 0), "inspector")
            ImpND.Add(New NodoImpresionN("", "vehiculo", FM.Vehiculo, 0), "vehiculo")
            ImpND.Add(New NodoImpresionN("", "placas", FM.Placas, 0), "placas")
            ImpND.Add(New NodoImpresionN("", "blocklotes", FM.Comentario, 0), "blocklotes")

            ImpND.Add(New NodoImpresionN("", "cultivo", V.Cultivo, 0), "cultivo")
            ImpND.Add(New NodoImpresionN("", "pedidono", V.Serie + V.Folio.ToString("0000"), 0), "pedidono")

            ImpND.Add(New NodoImpresionN("", "producto", I.Nombre, 0), "producto")
            ImpND.Add(New NodoImpresionN("", "codigo", I.Clave, 0), "codigo")

            If FM.Estado = Estados.Cancelada Then
                ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADO", 0), "cancelado")
            Else
                ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
            End If


            ImpND.Add(New NodoImpresionN("", "nombrefiscal2", Sucursal.NombreFiscal, 0), "nombrefiscal2")
            ImpND.Add(New NodoImpresionN("", "direccion2", Sucursal.Direccion, 0), "direccion2")
            ImpND.Add(New NodoImpresionN("", "noexterior2", Sucursal.NoExterior, 0), "noexterior2")
            ImpND.Add(New NodoImpresionN("", "nointerior2", Sucursal.NoInterior, 0), "nointerior2")
            ImpND.Add(New NodoImpresionN("", "colonia2", Sucursal.Colonia, 0), "colonia2")
            ImpND.Add(New NodoImpresionN("", "ciudad2", Sucursal.Ciudad, 0), "ciudad2")
            ImpND.Add(New NodoImpresionN("", "estado2", Sucursal.Estado, 0), "estado2")
            ImpND.Add(New NodoImpresionN("", "cp2", Sucursal.CP, 0), "cp2")
            ImpND.Add(New NodoImpresionN("", "rfc2", Sucursal.RFC, 0), "rfc2")
            ImpND.Add(New NodoImpresionN("", "tel2", Sucursal.Telefono, 0), "tel2")
            ImpND.Add(New NodoImpresionN("", "email2", Sucursal.Email, 0), "email2")
            ImpND.Add(New NodoImpresionN("", "referencia2", Sucursal.ReferenciaDomicilio, 0), "referencia2")
            ImpND.Add(New NodoImpresionN("", "municipio2", Sucursal.Municipio, 0), "municipio2")
            ImpND.Add(New NodoImpresionN("", "vendedor2", Vendedor.Nombre, 0), "vendedor2")
            ImpND.Add(New NodoImpresionN("", "nombrecliente2", V.Cliente.Nombre, 0), "nombrecliente2")
            ImpND.Add(New NodoImpresionN("", "clavecliente2", V.Cliente.Clave, 0), "clavecliente2")
            ImpND.Add(New NodoImpresionN("", "contactocliente2", V.Cliente.Contacto, 0), "contactocliente2")
            If V.Cliente.DireccionFiscal = 0 Then
                ImpND.Add(New NodoImpresionN("", "direccioncliente2", V.Cliente.Direccion, 0), "direccioncliente2")
                ImpND.Add(New NodoImpresionN("", "noexteriorcliente2", V.Cliente.NoExterior, 0), "noexteriorcliente2")
                ImpND.Add(New NodoImpresionN("", "nointeriorcliente2", V.Cliente.NoInterior, 0), "nointeriorcliente2")
                ImpND.Add(New NodoImpresionN("", "coloniacliente2", V.Cliente.Colonia, 0), "coloniacliente2")
                ImpND.Add(New NodoImpresionN("", "cpcliente2", V.Cliente.CP, 0), "cpcliente2")
                ImpND.Add(New NodoImpresionN("", "ciudadcliente2", V.Cliente.Ciudad, 0), "ciudadcliente2")
                ImpND.Add(New NodoImpresionN("", "estadocliente2", V.Cliente.Estado, 0), "estadocliente2")
                ImpND.Add(New NodoImpresionN("", "municipiocliente2", V.Cliente.Municipio, 0), "municipiocliente2")
                ImpND.Add(New NodoImpresionN("", "refcliente2", V.Cliente.ReferenciaDomicilio, 0), "refcliente2")
            Else
                ImpND.Add(New NodoImpresionN("", "direccioncliente2", V.Cliente.Direccion2, 0), "direccioncliente2")
                ImpND.Add(New NodoImpresionN("", "noexteriorcliente2", V.Cliente.NoExterior2, 0), "noexteriorcliente2")
                ImpND.Add(New NodoImpresionN("", "nointeriorcliente2", V.Cliente.NoInterior2, 0), "nointeriorcliente2")
                ImpND.Add(New NodoImpresionN("", "coloniacliente2", V.Cliente.Colonia2, 0), "coloniacliente2")
                ImpND.Add(New NodoImpresionN("", "cpcliente2", V.Cliente.CP2, 0), "cpcliente2")
                ImpND.Add(New NodoImpresionN("", "ciudadcliente2", V.Cliente.Ciudad2, 0), "ciudadcliente2")
                ImpND.Add(New NodoImpresionN("", "estadocliente2", V.Cliente.Estado2, 0), "estadocliente2")
                ImpND.Add(New NodoImpresionN("", "municipiocliente2", V.Cliente.Municipio2, 0), "municipiocliente2")
                ImpND.Add(New NodoImpresionN("", "refcliente2", V.Cliente.ReferenciaDomicilio2, 0), "refcliente2")
            End If
            ImpND.Add(New NodoImpresionN("", "rfccliente2", V.Cliente.RFC, 0), "rfccliente2")

            ImpND.Add(New NodoImpresionN("", "curpcliente2", V.Cliente.CURP, 0), "curpcliente2")

            ImpND.Add(New NodoImpresionN("", "serie2", FM.Serie, 0), "serie2")
            ImpND.Add(New NodoImpresionN("", "folio2", FM.Folio, 0), "folio2")
            ImpND.Add(New NodoImpresionN("", "fecha2", FM.Fecha, 0), "fecha2")
            ImpND.Add(New NodoImpresionN("", "hora2", FM.Hora, 0), "hora2")

            ImpND.Add(New NodoImpresionN("", "tipomov2", FM.TipoMovimientoString, 0), "tipomov2")

            ImpND.Add(New NodoImpresionN("", "almacen2", FM.Almacen.Nombre, 0), "almacen2")
            ImpND.Add(New NodoImpresionN("", "almacendest2", FM.AlmacenDestino.Nombre, 0), "almacendest2")
            ImpND.Add(New NodoImpresionN("", "estados2", FM.EstadoString, 0), "estados2")
            ImpND.Add(New NodoImpresionN("", "estadosurtido2", FM.EstadoSurtidoString, 0), "estadosurtido2")
            ImpND.Add(New NodoImpresionN("", "pesobruto22", FM.PesoBruto.ToString, 0), "pesobruto22")
            ImpND.Add(New NodoImpresionN("", "tara2", FM.Tara.ToString, 0), "tara2")
            ImpND.Add(New NodoImpresionN("", "pesoneto2", FM.PesoNeto.ToString, 0), "pesoneto2")
            ImpND.Add(New NodoImpresionN("", "pesobruto222", FM.PesoBruto2.ToString, 0), "pesobruto222")
            ImpND.Add(New NodoImpresionN("", "consumo2", FM.Surtido.ToString, 0), "consumo2")
            ImpND.Add(New NodoImpresionN("", "importeneto2", Format(CDbl(TextBox2.Text) * PrecioU, "#,###,##0.00"), 0), "importeneto2")
            ImpND.Add(New NodoImpresionN("", "importeconsumo2", FM.Precio.ToString, 0), "importeconsumo2")
            ImpND.Add(New NodoImpresionN("", "fechacarga2", FM.FechaCarga, 0), "fechacarga2")
            ImpND.Add(New NodoImpresionN("", "horacarga2", FM.HoraCarga, 0), "horacarga2")
            'ImpND.Add(New NodoImpresionN("", "fechacarga", FM.FechaCarga, 0), "fechacarga")
            ImpND.Add(New NodoImpresionN("", "fecharegreso2", FM.FechaLlegada, 0), "fecharegreso2")
            ImpND.Add(New NodoImpresionN("", "horaregreso2", FM.HoraLlegada, 0), "horaregreso2")
            ImpND.Add(New NodoImpresionN("", "fechaenvio2", FM.FechadeEnvio, 0), "fechaenvio2")
            ImpND.Add(New NodoImpresionN("", "fechadescarga2", FM.FechadeDescarga, 0), "fechadescarga2")
            ImpND.Add(New NodoImpresionN("", "chofer2", FM.Chofer, 0), "chofer2")
            ImpND.Add(New NodoImpresionN("", "inspector2", FM.Inspector, 0), "inspector2")
            ImpND.Add(New NodoImpresionN("", "vehiculo2", FM.Vehiculo, 0), "vehiculo2")
            ImpND.Add(New NodoImpresionN("", "placas2", FM.Placas, 0), "placas2")
            ImpND.Add(New NodoImpresionN("", "blocklotes2", FM.Comentario, 0), "blocklotes2")

            ImpND.Add(New NodoImpresionN("", "restante", CStr(FM.PesoNeto - FM.Surtido), 0), "restante")

            ImpND.Add(New NodoImpresionN("", "cultivo2", V.Cultivo, 0), "cultivo2")
            ImpND.Add(New NodoImpresionN("", "pedidono2", V.Serie + V.Folio.ToString("0000"), 0), "pedidono2")

            ImpND.Add(New NodoImpresionN("", "producto2", I.Nombre, 0), "producto2")
            ImpND.Add(New NodoImpresionN("", "codigo2", I.Clave, 0), "codigo2")

            If FM.Estado = Estados.Cancelada Then
                ImpND.Add(New NodoImpresionN("", "cancelado2", "CANCELADO", 0), "cancelado2")
            Else
                ImpND.Add(New NodoImpresionN("", "cancelado2", "", 0), "cancelado2")
            End If
            'ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
            'ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
            Posicion = 0
            'Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
            'CodigoBidimensional = CB.Encode("?&re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, "$#,###,##0.00") + "&id=" + V.uuid, System.Text.Encoding.Default)
            NumeroPagina = 1
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

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

    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        'If TipoImpresora = 0 Then
        ImpDb.DaZonaDetalles(TiposDocumentos.FertilizantesMovimientos, IdSucursal)
        'Else
        'ImpDb.DaZonaDetalles(TiposDocumentos.FertilizantesPedidoTicket, IdSucursal)
        'End If
        'If TipoImpresora = 0 Then
        DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        'Else
        'DibujaPaginaFlujo(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        'End If

    End Sub
    Private Sub DibujaPaginaEstatico(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos

        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            'If DocAImprimir = 0 Then
            'If TipoImpresora = 0 Then
            e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FertilizantesMovimientos, IdSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'Else
            'e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FertilizantesMovimientosTicket, IdSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'End If
            'Else
            'If TipoImpresora = 0 Then
            'e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'Else
            'e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'End If
            'End If
        Catch ex As Exception

        End Try
        Dim Rec As RectangleF
        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

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
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
            YCoord = YCoord + 4 + YExtra
            '**********************************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0
            'If YCoord >= LimY And Posicion > 0 Then
            '    MasPaginas = True
            '    Exit While
            'End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
            If HayRenglon Then YCoord = YCoord + 4 + YExtra
            '********************************
            Posicion += 1
            C += 1
        End While


        'Nodos fijos
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                'If n.DataPropertyName = "iva2" Then niva2 = n
                'If n.DataPropertyName = "codigobi" Then ncb = n
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
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
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

                        If niva.ConEtiqueta = 1 Then
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
                'C = 0
                'If niva2.Visible = 1 Then
                '    YCoord = 0
                '    While C < ImpNDi2.Count
                '        'For Each n As NodoImpresionN In ImpNDi
                '        Nimp = ImpNDi2("iva" + Format(C, "00"))

                '        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                '        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                '        If niva.ConEtiqueta = 1 Then
                '            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                '        Else
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                '        End If
                '        'End If
                '        'Next
                '        YCoord += 4
                '        C += 1
                '    End While
                'End If

            End If

            If ncb.Visible = 1 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DibujaPaginaFlujo(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos
        'Dim ImpDb As New dbImpresionesN(MySqlcon)
        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        'Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            'If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FertilizantesMovimientos, IdSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FertilizantesMovimientos + 1000, IdSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
            'Else
            '    If TipoImpresora = 0 Then
            '        e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            '    Else
            '        e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            '    End If
            'End If
        Catch ex As Exception

        End Try
        Dim Rec As RectangleF


        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        'Nodos fijos principio
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                'If n.DataPropertyName = "iva2" Then niva2 = n
                'If n.DataPropertyName = "codigobi" Then ncb = n
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
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 And niva.Tipo = 0 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta = 1 Then
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
                'C = 0
                'If niva2.Visible = 1 And niva2.Tipo = 0 Then
                '    YCoord = 0
                '    While C < ImpNDi2.Count
                '        'For Each n As NodoImpresionN In ImpNDi
                '        Nimp = ImpNDi2("iva" + Format(C, "00"))

                '        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                '        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                '        If niva.ConEtiqueta = 1 Then
                '            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                '        Else
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                '        End If
                '        'End If
                '        'Next
                '        YCoord += 4
                '        C += 1
                '    End While
                'End If

            End If
            'If ncb.Visible = 1 And ncb.Tipo = 0 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

        For Each n As NodoImpresionN In ImpNDi
            If n.DataPropertyName.Contains("cancelado") Then
                e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
            End If
        Next
        'Nodos Detalles            
        XCoord = 0
        YCoord = 0
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
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 And pModo = 0 Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
            YCoord = YCoord + 4 + YExtra
            '****************************************
            Dim hayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0
            'If YCoord >= LimY And Posicion > 0 And pModo = 0 Then
            '    MasPaginas = True
            '    Exit While
            'End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    hayRenglon = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
            If hayRenglon Then YCoord = YCoord + 4 + YExtra

            '***************************************
            Posicion += 1
            C += 1
        End While


        'Nodos fijos final
        If MasPaginas = True And (pModo = 2 Or pModo = 3) Then
            NumeroPagina += 1
            Exit Sub
        End If
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                'If n.DataPropertyName = "iva2" Then niva2 = n
                'If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 2 Then
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
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(n.XL / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS
            Dim Ycoord2 As Integer
            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                Ycoord2 = 0
                If niva.Visible = 1 And niva.Tipo = 2 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                        If niva.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, Ycoord2)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        End If
                        'End If
                        'Next
                        Ycoord2 += 4
                        C += 1
                    End While
                End If

                Ycoord2 = 0
                'Ivas Sin Retenciones
                'C = 0
                'If niva2.Visible = 1 And niva2.Tipo = 2 Then
                '    YCoord = 0
                '    While C < ImpNDi2.Count
                '        'For Each n As NodoImpresionN In ImpNDi
                '        Nimp = ImpNDi2("iva" + Format(C, "00"))

                '        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                '        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva2.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                '        If niva.ConEtiqueta = 1 Then
                '            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, Ycoord2)
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                '        Else
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                '        End If
                '        'End If
                '        'Next
                '        Ycoord2 += 4
                '        C += 1
                '    End While
                'End If

            End If

            'If ncb.Visible = 1 And ncb.Tipo = 2 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(Math.Abs((ncb.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Function InsertaEnters(ByRef Cadena As String, ByVal CadaCuantos As Integer, ByVal Y As Integer, ByVal AumentoY As Integer) As Integer
        Dim C As Integer
        C = 0
        Dim CC As Integer = 0
        Dim car As String
        Dim Yx As Integer = 0
        While C < Cadena.Length
            car = Cadena.Substring(C, 1)
            If car = Chr(13) Or CC = CadaCuantos Then
                Yx += AumentoY
                CC = 0
            Else
                CC += 1
            End If
            C += 1
        End While
        Return Yx
    End Function
    Private Sub Imprimir()
        Dim en As New Encriptador
        'Dim V As New dbFertilizantesPedido(FM.Id, MySqlcon)
        Dim RutaPDF As String
        'TextBox9.Text = 
        'TextBox10.Text = 
        'en.GuardaArchivoTexto("XMLFac-" + V.Folio.ToString + ".xml", V.CreaXML(idVenta, IdMonedaG, TextBox10.Text), System.Text.Encoding.UTF8)
        'Cadena = V.CreaCadenaOriginal(idVenta, IdMonedaG)
        'Sello = en.GeneraSello(Cadena, My.Settings.rutacer, Format(CDate(V.Fecha), "yyyy"))
        'Dim Enc As New System.Text.UTF8Encoding
        'Dim Bytes() As Byte = Enc.GetBytes(V.CreaXML(idVenta, IdMonedaG, Sello))
        'Dim Bytes() As Byte = Encoder.GetBytes(pCadenaOriginal)
        Dim Archivos As New dbSucursalesArchivos

        RutaPDF = Archivos.DaRutaArchivos(IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FertilizantesPDF, False)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(dtpFecha1.Value, "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(dtpFecha1.Value, "yyyy") + "\" + Format(dtpFecha1.Value, "MM") + "\")
        RutaPDF = RutaPDF + "\" + Format(dtpFecha1.Value, "yyyy") + "\" + Format(dtpFecha1.Value, "MM")
        'IO.Directory.CreateDirectory(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        'IO.Directory.CreateDirectory(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        'en.GuardaArchivo(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFac-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
        PrintDocument1.DocumentName = "MOVIMIENTO " + TextBox4.Text.Trim + TextBox6.Text.Trim
        'Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Impresora = Archivos.DaImpresoraActiva(IdSucursal, GlobalIdEmpresa, True, 0, TiposDocumentos.FertilizantesPedido)
        TipoImpresora = 0 'SA.TipoImpresora

        Dim obj As New Bullzip.PdfWriter.PdfSettings
        obj.Init()
        obj.PrinterName = Impresora
        'obj.WriteSettings()
        If Impresora = "Bullzip PDF Printer" Then
            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            obj.SetValue("ShowSettings", "never")
            obj.SetValue("ShowPDF", "yes")
            obj.SetValue("ShowSaveAS", "nofile")
            obj.SetValue("ConfirmOverwrite", "no")
            obj.SetValue("Target", "printer")
            obj.WriteSettings()
        End If
        PrintDocument1.PrinterSettings.PrinterName = Impresora
        LlenaNodosImpresion()
        'If TipoImpresora = 0 Then
        LlenaNodos(IdSucursal, TiposDocumentos.FertilizantesMovimientos)
        'Else
        'LlenaNodos(IdSucursal, TiposDocumentos.FertilizantesMovimientosTicket)
        'End If
        PrintDocument1.Print()
        'Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\Pedido " + V.Serie + V.Folio.ToString + ".pdf", 1000)
        'If V.Cliente.Email <> "" Then
        '    Try
        '        If MsgBox("¿Enviar pedido por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '            If V.Cliente.Email <> "" Then
        '                Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
        '                Dim O As New dbOpciones(MySqlcon)
        '                Dim C As String
        '                C = "Eviado por: " + O._NombreEmpresa + vbNewLine + "RFC: " + O._RFC + vbNewLine + "PEDIDO" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Pedido enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
        '                M.send("Pedido: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\Pedido " + V.Serie + V.Folio.ToString + ".pdf", "")
        '            End If
        '        End If
        '    Catch ex As Exception
        '        MsgBox("No se puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        '    End Try
        'End If
    End Sub

    Private Sub cmbAlmacen_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbAlmacen.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cmbalmacenDest.Visible = True Then
                cmbalmacenDest.Focus()
            Else
                TextBox5.Focus()
            End If
        End If
    End Sub

    Private Sub cmbAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlmacen.SelectedIndexChanged
        If cmbAlmacen.Items.Count > 0 And (ComboBox1.SelectedIndex < 2 Or ComboBox1.SelectedIndex = 3) Then
            Dim Al As New dbAlmacenes(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), MySqlcon)
            TextBox1.Text = Al.Peso.ToString
            If ComboBox1.SelectedIndex = 0 Or ComboBox1.SelectedIndex = 3 Then
                TextBox8.Text = Al.Peso.ToString
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox8.Enabled = True Then
                TextBox8.Focus()
            Else
                DateTimePicker6.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If IsNumeric(TextBox5.Text) And IsNumeric(TextBox1.Text) Then
            TextBox2.Text = CStr(CDbl(TextBox5.Text) - CDbl(TextBox1.Text))
        Else
            TextBox2.Text = "0"
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Label23.Text = "$" + Format(CDbl(TextBox2.Text) * PrecioU, "#,###,##0.00")
    End Sub

    Private Sub TextBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox8.Enabled = True Then
                TextBox8.Focus()
            Else
                TextBox1.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If IsNumeric(TextBox5.Text) And IsNumeric(TextBox1.Text) Then
            TextBox2.Text = CStr(CDbl(TextBox5.Text) - CDbl(TextBox1.Text))
            If ComboBox2.SelectedIndex = 0 Then
                If CDbl(TextBox2.Text) > Restante Then
                    TextBox5.BackColor = Color.LightCoral
                    Label14.Visible = True
                Else
                    TextBox5.BackColor = Color.FromKnownColor(KnownColor.Window)
                    Label14.Visible = False
                End If
            End If
        Else
            TextBox2.Text = "0"
        End If
        If ComboBox1.SelectedIndex = 0 Or ComboBox1.SelectedIndex = 3 Then
            If IsNumeric(TextBox8.Text) And IsNumeric(TextBox2.Text) And ComboBox2.SelectedIndex = 1 Then
                TextBox3.Text = CStr((CDbl(TextBox8.Text) - CDbl(TextBox1.Text) - CDbl(TextBox2.Text)) * -1)
            Else
                TextBox3.Text = "0"
            End If
        End If
    End Sub

    Private Sub TextBox8_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker6.Focus()
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        If IsNumeric(TextBox8.Text) And IsNumeric(TextBox2.Text) And ComboBox2.SelectedIndex = 1 Then
            TextBox3.Text = CStr((CDbl(TextBox8.Text) - CDbl(TextBox1.Text) - CDbl(TextBox2.Text)) * -1)
        Else
            TextBox3.Text = "0"
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Precio = CDbl(TextBox3.Text) * PrecioU
        Label24.Text = "$" + Format(Precio, "#,###,##0.00")
    End Sub

    Private Sub ComboBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox4.Focus()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 2 Then
            cmbalmacenDest.Visible = True
            Label21.Visible = True
            If cmbalmacenDest.Items.Count > 0 Then
                Dim Al As New dbAlmacenes(IdsAlmacenes2.Valor(cmbalmacenDest.SelectedIndex), MySqlcon)
                TextBox1.Text = Al.Peso.ToString
            End If
        Else
            cmbalmacenDest.Visible = False
            Label21.Visible = False
            If cmbAlmacen.Items.Count > 0 Then
                Dim Al As New dbAlmacenes(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), MySqlcon)
                TextBox1.Text = Al.Peso.ToString
            End If
        End If
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        If ComboBox1.SelectedIndex = 0 Then
            If Sf.BuscaFolios(IdSucursal, dbSucursalesFolios.TipoDocumentos.FertilizantesMovimientosSalida, 0) Then
                TextBox4.Text = Sf.Serie
            Else
                TextBox4.Text = ""
            End If
            TextBox6.Text = FM.DaNuevoFolio(TextBox4.Text, IdSucursal).ToString
            If CInt(TextBox6.Text) < Sf.FolioInicial Then TextBox6.Text = Sf.FolioInicial.ToString
            TextBox8.Enabled = False
            TextBox8.Text = TextBox1.Text
            ComboBox2.SelectedIndex = 1
            ComboBox2.Enabled = False
        Else
            If ComboBox1.SelectedIndex = 3 Then
                If Sf.BuscaFolios(IdSucursal, dbSucursalesFolios.TipoDocumentos.FertilizantesDevolucion, 0) Then
                    TextBox4.Text = Sf.Serie
                Else
                    TextBox4.Text = ""
                End If
                TextBox6.Text = FM.DaNuevoFolio(TextBox4.Text, IdSucursal).ToString
                If CInt(TextBox6.Text) < Sf.FolioInicial Then TextBox6.Text = Sf.FolioInicial.ToString
                TextBox8.Enabled = False
                TextBox8.Text = TextBox1.Text
                ComboBox2.SelectedIndex = 1
                ComboBox2.Enabled = False
            Else
                TextBox8.Enabled = True
                TextBox8.Text = "0"
                ComboBox2.SelectedIndex = 0
                ComboBox2.Enabled = True
            End If
        End If

        If ComboBox1.SelectedIndex = 1 Then
            If Sf.BuscaFolios(IdSucursal, dbSucursalesFolios.TipoDocumentos.FertilizantesMovimientosEnvio, 0) Then
                TextBox4.Text = Sf.Serie
            Else
                TextBox4.Text = ""
            End If
            TextBox6.Text = FM.DaNuevoFolio(TextBox4.Text, IdSucursal).ToString
            If CInt(TextBox6.Text) < Sf.FolioInicial Then TextBox6.Text = Sf.FolioInicial.ToString
        End If
        If ComboBox1.SelectedIndex = 2 Then
            If Sf.BuscaFolios(IdSucursal, dbSucursalesFolios.TipoDocumentos.FertilizantesMovimientosTraspaso, 0) Then
                TextBox4.Text = Sf.Serie
            Else
                TextBox4.Text = ""
            End If
            TextBox6.Text = FM.DaNuevoFolio(TextBox4.Text, IdSucursal).ToString
            If CInt(TextBox6.Text) < Sf.FolioInicial Then TextBox6.Text = Sf.FolioInicial.ToString
        End If
    End Sub

    Private Sub cmbalmacenDest_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbalmacenDest.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox5.Enabled Then
                TextBox5.Focus()
            Else
                TextBox1.Focus()
            End If
        End If
    End Sub

    Private Sub cmbalmacenDest_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbalmacenDest.SelectedIndexChanged
        If cmbalmacenDest.Items.Count > 0 And ComboBox1.SelectedIndex = 2 Then
            Dim Al As New dbAlmacenes(IdsAlmacenes2.Valor(cmbalmacenDest.SelectedIndex), MySqlcon)
            TextBox1.Text = Al.Peso.ToString
        End If
    End Sub

    Private Sub ComboBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox3.Focus()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If IsNumeric(TextBox8.Text) And IsNumeric(TextBox2.Text) And ComboBox2.SelectedIndex = 1 Then
            TextBox3.Text = CStr((CDbl(TextBox8.Text) - CDbl(TextBox1.Text) - CDbl(TextBox2.Text)) * -1)
        Else
            TextBox3.Text = "0"
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Imprimir()
    End Sub

    Private Sub dtpFecha1_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFecha1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox1.Focus()
        End If
    End Sub

    Private Sub dtpFecha1_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha1.ValueChanged

    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox6.Focus()
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox6_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox6.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbAlmacen.Focus()
        End If
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub DateTimePicker6_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimePicker6.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker1.Focus()
        End If
    End Sub

    Private Sub DateTimePicker6_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker6.ValueChanged

    End Sub

    Private Sub DateTimePicker1_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimePicker1.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker3.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub DateTimePicker3_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimePicker3.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker5.Focus()
        End If
    End Sub

    Private Sub DateTimePicker3_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker3.ValueChanged

    End Sub

    Private Sub DateTimePicker5_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimePicker5.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker4.Focus()
        End If
    End Sub

    Private Sub DateTimePicker5_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker5.ValueChanged

    End Sub

    Private Sub DateTimePicker4_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimePicker4.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker2.Focus()
        End If
    End Sub

    Private Sub DateTimePicker4_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker4.ValueChanged

    End Sub

    Private Sub DateTimePicker2_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimePicker2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If ComboBox2.Enabled = True Then
                ComboBox2.Focus()
            Else
                ComboBox3.Focus()
            End If
        End If
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged

    End Sub

    Private Sub ComboBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox4.Focus()
        End If
    End Sub

    

    

   
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

    End Sub

    Private Sub ComboBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox5.Focus()
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged

    End Sub

    Private Sub ComboBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox6.Focus()
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged

    End Sub

    Private Sub ComboBox6_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox6.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox14.Focus()
        End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged

    End Sub
End Class