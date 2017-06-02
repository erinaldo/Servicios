Imports System.ComponentModel
Imports System.Reflection

Module VarGlobal
    Public MySqlcon As MySql.Data.MySqlClient.MySqlConnection
    Public MySqlcon2 As MySql.Data.MySqlClient.MySqlConnection
    Public MySqlcom As MySql.Data.MySqlClient.MySqlCommand
    Public MySqlcom2 As MySql.Data.MySqlClient.MySqlCommand
    Public MySqlcomInt As MySql.Data.MySqlClient.MySqlCommand
    Public MySqlDReader As MySql.Data.MySqlClient.MySqlDataReader
    Public ImagenPopUp As Bitmap
    Public ImagenFondo As Bitmap
    Public GlobalIcono As Icon
    'Public GlobalIconoC As Icon
    Public GlobalUsuario As String
    Public GlobalIdUsuario As Integer
    Public GlobalIdAlmacen As Integer
    Public GlobalUsuarioIdVendedor As Integer
    Public GlobalTipoUsuario As Integer
    Public GlobalConsultaTiempoReal As Boolean = False
    Public GlobalIdMoneda As Integer
    Public GlobalTipoCosteo As Byte
    Public GlobalNombreApp As String = "Pull Admin - Pull System Soft"
    Public Version As String = "0.9.037 rev 25"
    Public VersionDB As String = "00903724"
    Public GlobalTipoFacturacion As Byte
    Public GlobalDireccionTimbrado As String
    Public GlobalPacCFDI As Byte
    Public GlobalModoBusqueda As Byte
    Public GlobalEstadoPuntodeVenta As String = "Cerrado"
    Public GlobalEstadoVentanas As Integer
    Public GlobalNombreEmpresa As String
    Public GlobalIdEmpresa As Integer
    Public GlobalIdSucursalDefault As Integer
    Public GlobalBasedeDatos As String
    Public GlobalConector As Byte
    Public GlobalPermisos As New PermisosN
    Public GlobalRutaConector As String
    Public GlobalIdiomaLetras As Byte
    Public GlobalpFabricante As Boolean
    Public Const ActivaVerPunto2 As Boolean = True
    Public FechaVerPunto2 As String
    Public GlobalTipoVersion As Byte = 0
    Public ConPuntodeVenta As Boolean = True
    Public GlobalConLicencias As Boolean = True
    Public Global30Dias As Boolean = False
    Public GlobalLicenciaA As String
    Public GlobalConBancos As Boolean
    Public GlobalLicenciaSTR As String
    Public GlobalConNomina As Boolean
    Public GlobalConServicios As Boolean
    Public GlobalConGastos As Boolean
    Public GlobalconEmpenios As Boolean
    Public GlobalConServiciosInterno As Boolean
    Public GlobalConContabilidad As Boolean
    Public GlobalConFertilizantes As Boolean
    Public GlobalConValidador As Boolean
    Public GlobalSoloExistencia As Boolean
    Public GlobalconSemillas As Boolean
    Public GlobalconIntegracion As Boolean
    Public GlobalConExtra As Boolean
    Public GlobalSemillasResumida As Byte
    Public GlobalConUsuarios As Boolean
    Public GlobalConRestaurant As Boolean
    Public GlobalUsuarioActivado As Boolean
    Public GlobalUltimoDia As Boolean = False
    Public GlobaltpBanxico As String = ""
    '0 Completo
    '1 Solo Facturacion
    '2 Facturacion y Clientes
    '3 Conector Macropro
    Public Busquedas(15) As String
    Enum VentanasdeBusqueda
        Ventas = 0
        VentasRemisiones = 1
        VentasCotizaciones = 2
        VentasPedidos = 3
        VentasDevoluciones = 4
        VentasNotasCredito = 5
        VentasNotasCargo = 6
        VentasApartados = 7

        Compras = 8
        ComprasRemisiones = 9
        ComprasCotizaciones = 10
        ComprasPedidos = 11
        ComprasDevoluciones = 12
        ComprasNotasCredito = 13
        ComprasNotasCargo = 14
        Nominas = 1
        ContabilidadPolizas = 15

    End Enum

    Enum ModosDeBusqueda
        Principal = 0
        Secundario = 1
        SoloDeudas = 3
    End Enum

    'Addenda OXXo tratar de eliminar esto pronto
    Public gIdAddenda As Integer = 0
    Public gIdArticulo As Integer = 0
    Public gMessageValidation As String = ""
    Public gValidation As Boolean = True

    Public Enum estatusEmpenios
        empeniado = 0
        adjudicado = 1
        entregado = 2
        renovado = 3
    End Enum
    Public Enum Estados
        Inicio = 0
        SinGuardar = 1
        Pendiente = 2
        Guardada = 3
        Cancelada = 4
    End Enum

    Public Enum TiposDocumentos
        <Description("Venta - Factura")> Venta = 0
        <Description("Venta - Cotización")> VentaCotizacion = 1
        <Description("Venta - Pedido")> VentaPedido = 2
        <Description("Venta - Remisión")> VentaRemision = 3
        <Description("Venta - Devolución")> VentaDevolucion = 4
        <Description("Venta - Nota de crédito")> VentaNotadeCredito = 5
        <Description("Venta - Nota de cargo")> VentaNotadeCargo = 6
        <Description("Ventas - Movimiento caja")> CajasMovimientos = 32
        <Description("Ventas - Apartado")> VentasApartados = 33

        <Description("Compra - Factura")> Compra = 7
        <Description("Compra - Orden de compra")> CompraCotizacion = 8
        <Description("Compra - Pedido")> CompraPedido = 9
        <Description("Compra - Remisión")> CompraRemision = 10
        <Description("Compra - Devolución")> CompraDevolucion = 11
        <Description("Compra - Nota de crédito")> CompraNotadeCredito = 12
        <Description("Compra - Nota de cargo")> CompraNotadeCargo = 13

        <Description("Inventario - Movimientos")> MovimientosInventario = 14
        <Description("Inventario - Pedido")> InventarioPedidos = 34

        <Description("Retención")> FormatoRetencion = 15
        <Description("Contabilidad - Póliza Egresos")> ContabilidadPolizaEgresos = 35
        <Description("Contabilidad - Póliza")> ContaPoliza = 41
        <Description("Restaurante - Ticket")> RestauranteTicket = 36
        <Description("Restaurante - Comanda")> RestauranteComanda = 37
        <Description("Nómina")> Nominas = 38
        <Description("Bancos - Cheque proveedor")> BancosChequePagoProv = 39
        <Description("Empeños - Boleta")> Empenios = 40

        <Description("Fertilizantes - Pedidos")> FertilizantesPedido = 42
        <Description("Fertilizantes - Movimientos")> FertilizantesMovimientos = 43

        <Description("Codigo de barras 1")> CodigoBarras = 44
        <Description("Código de barras 2")> CodigoBarras2 = 48
        <Description("Código de barras 3")> CodigoBarras3 = 49
        <Description("Código de barras 4")> CodigoBarras4 = 50

        <Description("Semillas - Boleta")> SemillasBoleta = 45
        <Description("Semillas - Liquidación")> SemillasLiquidacion = 46
        <Description("Semillas - Comprobante")> SemillasComprobante = 47

        <Description("Gastos")> Gastos = 199
        <Description("PDF")> PDF = 200
    End Enum
    Public Class EnumDescriptor(Of T)
        Private _description As String
        Private _value As T
        Public ReadOnly Property Description() As String
            Get
                Return Me._description
            End Get
        End Property
        Public ReadOnly Property Value() As T
            Get
                Return Me._value
            End Get
        End Property

        Public Sub New(ByVal value As T)
            Me._value = value

            'Get the Description attribute.
            Dim field As FieldInfo = value.GetType().GetField(value.ToString())
            Dim attributes As DescriptionAttribute() = DirectCast(field.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())

            'Use the Description attribte if one exists, otherwise use the value itself as the description.
            Me._description = If(attributes.Length = 0, value.ToString(), attributes(0).Description)
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Description
        End Function

    End Class
    Public Class EnumDescriptorCollection(Of T)
        Inherits ObjectModel.Collection(Of EnumDescriptor(Of T))
        Public Sub New()
            For Each value As T In [Enum].GetValues(GetType(T))
                Me.Items.Add(New EnumDescriptor(Of T)(value))
            Next
        End Sub
    End Class

    Public tecladoActivo As Boolean = False
    Public GlobalEstadoRestaurante As String = "Cerrado"
    Public buscadorPorPaginas As Boolean = False

    Enum estadosPlatillos
        inicio = 0
        pagado = 1
        pendiente = 2
        servido = 3
        enProceso = 4
        enviado = 5
        sinEnviar = 6
    End Enum

    Enum EstadosMesas
        inicio = 0
        Libre = 1
        Ocupada = 2
        Reservada = 3
        Sucia = 4
    End Enum

    Public Function IniciarMySQL(ByVal DBaConectarse As String, ByVal ServidorAConectarse As String, ByVal DBUsuario As String, ByVal DBPassword As String, ByVal DBPuerto As String) As Integer
        Dim TodoBien As Integer = 1
        Try
            If DBPuerto <> "" Then
                MySqlcon = New MySql.Data.MySqlClient.MySqlConnection("Server=" + ServidorAConectarse + ";Database=" + DBaConectarse + ";Uid=" + DBUsuario + ";Pwd=" + DBPassword + ";Port=" + DBPuerto)
            Else
                MySqlcon = New MySql.Data.MySqlClient.MySqlConnection("Server=" + ServidorAConectarse + ";Database=" + DBaConectarse + ";Uid=" + DBUsuario + ";Pwd=" + DBPassword + ";Port=3306")
            End If
            MySqlcom = New MySql.Data.MySqlClient.MySqlCommand
            MySqlcomInt = New MySql.Data.MySqlClient.MySqlCommand
            MySqlcon.Open()
            MySqlcom.Connection = MySqlcon
            MySqlcomInt.Connection = MySqlcon
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, GlobalNombreApp)
            TodoBien = 0
        End Try
        Return TodoBien
    End Function
    Public Function IniciarMySQL2(ByVal DBaConectarse As String, ByVal ServidorAConectarse As String, ByVal DBUsuario As String, ByVal DBPassword As String, ByVal DBPuerto As String) As Integer
        Dim TodoBien As Integer = 1
        Try
            If DBPuerto <> "" Then
                MySqlcon2 = New MySql.Data.MySqlClient.MySqlConnection("Server=" + ServidorAConectarse + ";Database=" + DBaConectarse + ";Uid=" + DBUsuario + ";Pwd=" + DBPassword + ";Port=" + DBPuerto)
            Else
                MySqlcon2 = New MySql.Data.MySqlClient.MySqlConnection("Server=" + ServidorAConectarse + ";Database=" + DBaConectarse + ";Uid=" + DBUsuario + ";Pwd=" + DBPassword + ";Port=3306")
            End If
            MySqlcom2 = New MySql.Data.MySqlClient.MySqlCommand
            MySqlcon2.Open()
            MySqlcom2.Connection = MySqlcon
            'MySqlcomInt.Connection = MySqlcon
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Exclamation, GlobalNombreApp)
            TodoBien = 0
        End Try
        Return TodoBien
    End Function

    Public Sub LlenaCombos(ByVal Tabla As String, ByRef ComboaLlenar As ComboBox, ByVal CampoN As String, ByVal AliasN As String, ByVal CampoID As String, ByRef ListaId As elemento, Optional ByVal ValorWhere As String = "", Optional ByVal PrimerValor As String = "", Optional ByVal OrderBy As String = "", Optional pComoSimple As Boolean = False)
        'Dim SQLDataR As SqlClient.SqlDataReader
        ComboaLlenar.Items.Clear()
        ListaId.Limpiar()
        If PrimerValor <> "" Then
            ComboaLlenar.Items.Add(PrimerValor)
            ListaId.Agregar(-2)
        End If
        If OrderBy = "" Then
            OrderBy = CampoN
        End If
        If pComoSimple = False Then
            If ValorWhere = "" Then
                MySqlcomInt.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " order by " + OrderBy
            Else
                If ValorWhere.Contains("inner join") = False Then
                    MySqlcomInt.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " where " + ValorWhere + " order by " + OrderBy
                Else
                    MySqlcomInt.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " " + ValorWhere + " order by " + OrderBy
                End If
            End If
            MySqlDReader = MySqlcomInt.ExecuteReader
            While MySqlDReader.Read
                ComboaLlenar.Items.Add(MySqlDReader.Item(AliasN))
                ListaId.Agregar(MySqlDReader.Item(CampoID))
            End While
            MySqlDReader.Close()
        Else
            If ValorWhere = "" Then
                MySqlcomInt.CommandText = "select " + CampoN + " as " + AliasN + " from " + Tabla + " order by " + OrderBy
            Else
                If ValorWhere.Contains("inner join") = False Then
                    MySqlcomInt.CommandText = "select " + CampoN + " as " + AliasN + " from " + Tabla + " where " + ValorWhere + " order by " + OrderBy
                Else
                    MySqlcomInt.CommandText = "select " + CampoN + " as " + AliasN + " from " + Tabla + " " + ValorWhere + " order by " + OrderBy
                End If
            End If
            MySqlDReader = MySqlcomInt.ExecuteReader
            While MySqlDReader.Read
                ComboaLlenar.Items.Add(MySqlDReader.Item(AliasN))
                'ListaId.Agregar(MySqlDReader.Item(CampoID))
            End While
            MySqlDReader.Close()
        End If

        If ComboaLlenar.DropDownStyle = ComboBoxStyle.DropDown Then
            ComboaLlenar.Text = ""
        Else
            If ComboaLlenar.Items.Count > 0 Then
                ComboaLlenar.SelectedIndex = 0 'tenia 0
            End If
        End If
    End Sub
    Public Sub DaEstadosMexico(ByRef ComboaLlenar As ComboBox)

        ComboaLlenar.Items.Add("AGUASCALIENTES")
        ComboaLlenar.Items.Add("BAJA CALIFORNIA NORTE")
        ComboaLlenar.Items.Add("BAJA CALIFORNIA SUR")
        ComboaLlenar.Items.Add("CAMPECHE")
        ComboaLlenar.Items.Add("COAHUILA")
        ComboaLlenar.Items.Add("COLIMA")
        ComboaLlenar.Items.Add("CHIAPAS")
        ComboaLlenar.Items.Add("CHIHUAHUA")
        ComboaLlenar.Items.Add("CIUDAD DE MÉXICO")
        ComboaLlenar.Items.Add("DURANGO")
        ComboaLlenar.Items.Add("GUANAJUATO")
        ComboaLlenar.Items.Add("GUERRERO")
        ComboaLlenar.Items.Add("HIDALGO")
        ComboaLlenar.Items.Add("JALISCO")
        ComboaLlenar.Items.Add("ESTADO DE MÉXICO")
        ComboaLlenar.Items.Add("MICHOACÁN")
        ComboaLlenar.Items.Add("MORELOS")
        ComboaLlenar.Items.Add("NAYARIT")
        ComboaLlenar.Items.Add("NUEVO LEÓN")
        ComboaLlenar.Items.Add("OAXACA")
        ComboaLlenar.Items.Add("PUEBLA")
        ComboaLlenar.Items.Add("QUERÉTARO")
        ComboaLlenar.Items.Add("QUINTANA ROO")
        ComboaLlenar.Items.Add("SAN LUIS POTOSÍ")
        ComboaLlenar.Items.Add("SINALOA")
        ComboaLlenar.Items.Add("SONORA")
        ComboaLlenar.Items.Add("TABASCO")
        ComboaLlenar.Items.Add("TAMAULIPAS")
        ComboaLlenar.Items.Add("TLAXCALA")
        ComboaLlenar.Items.Add("VERACRUZ")
        ComboaLlenar.Items.Add("YUCATÁN")
        ComboaLlenar.Items.Add("ZACATECAS")
        '        1. Aguascalientes – Aguscalientes
        '2. Baja California – Mexicali
        '3. Baja California Sur – La Paz
        '4. Campeche – Campeche
        '5. Coahuila – Saltillo
        '6. Colima – Colima
        '7. Chiapas – Tuxtla Gutiérrez
        '8. Chihuahua – Chihuahua
        '9. Distrito Federal – Ciudad de México
        '10. Durango – Durango
        '11. Guanajuato – Guanajuato
        '12. Guerrero – Chilpancingo
        '13. Hidalgo – Pachuca
        '14. Jalisco – Guadalajara
        '15. Estado de México – Toluca
        '16. Michoacán – Morelia
        '17. Morelos – Cuernavaca
        '18. Nayarit – Tepic
        '19. Nuevo León – Monterrey
        '20. Oaxaca – Oaxaca
        '21. Puebla – Puebla
        '22. Querétaro – Querétaro
        '23. Quintana Roo – Chetumal
        '24. San Luis Potosí – San Luis Potosí
        '25. Sinaloa – Culiacán
        '26. Sonora – Hermosio
        '27. Tabasco – Villahermosa
        '28. Tamaulipas – Ciudad Victoria
        '29. Tlaxcala – Tlaxcal
        '30. Veracruz – Jalapa
        '31. Yucatán – Mérida
        '32. Zacatecas – Zacatecas
    End Sub
    Public Function DaClaveEstadosMexico(pEstado As String) As String
        Select Case pEstado
            Case "AGUASCALIENTES"
                Return "AGU"
            Case "BAJA CALIFORNIA NORTE"
                Return "BCN"
            Case "BAJA CALIFORNIA SUR"
                Return "BCS"
            Case "CAMPECHE"
                Return "CAM"
            Case "COAHUILA"
                Return "COA"
            Case "COLIMA"
                Return "COL"
            Case "CHIAPAS"
                Return "CHP"
            Case "CHIHUAHUA"
                Return "CHH"
            Case "CIUDAD DE MÉXICO"
                Return "DIF"
            Case "DISTRITO FEDERAL"
                Return "DIF"
            Case "DURANGO"
                Return "DUR"
            Case "GUANAJUATO"
                Return "GUA"
            Case "GUERRERO"
                Return "GUE"
            Case "HIDALGO"
                Return "HID"
            Case "JALISCO"
                Return "JAL"
            Case "ESTADO DE MÉXICO"
                Return "MEX"
            Case "MICHOACÁN"
                Return "MIC"
            Case "MORELOS"
                Return "MOR"
            Case "NAYARIT"
                Return "NAY"
            Case "NUEVO LEÓN"
                Return "NLE"
            Case "OAXACA"
                Return "OAX"
            Case "PUEBLA"
                Return "PUE"
            Case "QUERÉTARO"
                Return "QUE"
            Case "QUINTANA ROO"
                Return "ROO"
            Case "SAN LUIS POTOSÍ"
                Return "SLP"
            Case "SINALOA"
                Return "SIN"
            Case "SONORA"
                Return "SON"
            Case "TABASCO"
                Return "TAB"
            Case "TAMAULIPAS"
                Return "TAM"
            Case "TLAXCALA"
                Return "TLA"
            Case "VERACRUZ"
                Return "VER"
            Case "YUCATÁN"
                Return "YUC"
            Case "ZACATECAS"
                Return "ZAC"
        End Select
        Return "SON"
    End Function
    Public Sub PopUp(ByVal TextoAMostrar As String, ByVal TiempoAMostrar As Integer, Optional ByVal pCentrado As Boolean = False)
        Dim FormPopUp As New frmPopup
        FormPopUp.StartPosition = FormStartPosition.Manual
        FormPopUp.ShowInTaskbar = False
        FormPopUp.TiempoMostrando = TiempoAMostrar
        FormPopUp.TextoAMostrar = TextoAMostrar
        FormPopUp.Opacity = 0
        FormPopUp.TopMost = True
        If pCentrado = False Then
            FormPopUp.Left = 1
            FormPopUp.Top = Screen.PrimaryScreen.Bounds.Height - FormPopUp.Height - 30
        End If
        If pCentrado Then
            FormPopUp.Left = (Screen.PrimaryScreen.Bounds.Width / 2) - (FormPopUp.Width / 2)
            FormPopUp.Top = (Screen.PrimaryScreen.Bounds.Height / 2) - (FormPopUp.Height / 2)
        End If
        FormPopUp.Show()

    End Sub
    Public Function CuentaTimbres() As Integer
        Dim Timbres As Integer
        MySqlcomInt.CommandText = "select count(idventa) from tblventastimbrado where uuid<>'**No Timbrado**' and  uuid<>''"
        Timbres = MySqlcomInt.ExecuteScalar
        MySqlcomInt.CommandText = "select count(idcargo) from tblnotasdecargotimbrado where uuid<>'**No Timbrado**' and  uuid<>''"
        Timbres += MySqlcomInt.ExecuteScalar
        MySqlcomInt.CommandText = "select count(idnota) from tblnotasdecreditotimbrado where uuid<>'**No Timbrado**' and  uuid<>''"
        Timbres += MySqlcomInt.ExecuteScalar
        MySqlcomInt.CommandText = "select count(iddevolucion) from tbldevolucionestimbrado where uuid<>'**No Timbrado**' and  uuid<>''"
        Timbres += MySqlcomInt.ExecuteScalar
        MySqlcomInt.CommandText = "select count(idnomina) from tblnominastimbrado where uuid<>'**No Timbrado**' and  uuid<>''"
        Timbres += MySqlcomInt.ExecuteScalar
        'Cancelaciones
        MySqlcomInt.CommandText = "select count(tblventastimbrado.idventa) from tblventastimbrado inner join tblventas on tblventastimbrado.idventa=tblventas.idventa where uuid<>'**No Timbrado**' and  uuid<>'' and tblventas.estado=4 and tblventas.fecha>='2014/01/01' and tblventas.fecha<='2016/02/15'"
        Timbres += MySqlcomInt.ExecuteScalar
        MySqlcomInt.CommandText = "select count(tblnotasdecargotimbrado.idcargo) from tblnotasdecargotimbrado inner join tblnotasdecargo on tblnotasdecargotimbrado.idcargo=tblnotasdecargo.idcargo where uuid<>'**No Timbrado**' and  uuid<>'' and tblnotasdecargo.estado=4 and tblnotasdecargo.fecha>='2014/01/01' and tblnotasdecargo.fecha<='2016/02/15'"
        Timbres += MySqlcomInt.ExecuteScalar
        MySqlcomInt.CommandText = "select count(tblnotasdecreditotimbrado.idnota) from tblnotasdecreditotimbrado inner join tblnotasdecredito on tblnotasdecreditotimbrado.idnota=tblnotasdecredito.idnota where uuid<>'**No Timbrado**' and  uuid<>'' and tblnotasdecredito.estado=4 and tblnotasdecredito.fecha>='2014/01/01' and tblnotasdecredito.fecha<='2016/02/15'"
        Timbres += MySqlcomInt.ExecuteScalar
        MySqlcomInt.CommandText = "select count(tbldevolucionestimbrado.iddevolucion) from tbldevolucionestimbrado inner join tbldevoluciones on tbldevolucionestimbrado.iddevolucion=tbldevoluciones.iddevolucion where uuid<>'**No Timbrado**' and  uuid<>'' and tbldevoluciones.estado=4 and tbldevoluciones.fecha>='2014/01/01' and tbldevoluciones.fecha<='2016/02/15'"
        Timbres += MySqlcomInt.ExecuteScalar
        MySqlcomInt.CommandText = "select count(tblnominastimbrado.idnomina) from tblnominastimbrado inner join tblnominas on tblnominastimbrado.idnomina=tblnominas.idnomina where uuid<>'**No Timbrado**' and  uuid<>'' and tblnominas.estado=4 and tblnominas.fecha>='2014/01/01' and tblnominas.fecha<='2016/02/15'"
        Timbres += MySqlcomInt.ExecuteScalar
        MySqlcomInt.CommandText = "select count(uuid) from tblxmlvalidados"
        Timbres += MySqlcomInt.ExecuteScalar
        Return Timbres
    End Function


    Public Function GlobalChecarConexion() As Boolean
        'Dim Conecto As Boolean = True
        On Error Resume Next
        'Try
        MySqlcomInt.CommandText = "select 1"
        MySqlcomInt.ExecuteScalar()
        'Return True
        'Catch ex As Exception
        If MySqlcon.State = ConnectionState.Closed Then
            MySqlcon.Open()
        End If
        If MySqlcon.State = ConnectionState.Broken Then
            MySqlcon.Close()
            MySqlcon.Open()
        End If
        'Finally
        If MySqlcon.State = ConnectionState.Closed Or MySqlcon.State = ConnectionState.Broken Then
            Return False
        Else
            Return True
        End If
        'End Try
    End Function
    Public Function GlobalChecarConexion2() As Boolean
        'Dim Conecto As Boolean = True
        On Error Resume Next
        'Try
        MySqlcom2.CommandText = "select 1"
        MySqlcom2.ExecuteScalar()
        'Return True
        'Catch ex As Exception
        If MySqlcon2.State = ConnectionState.Closed Then
            MySqlcon2.Open()
        End If
        If MySqlcon2.State = ConnectionState.Broken Then
            MySqlcon2.Close()
            MySqlcon2.Open()

        End If
        'Finally
        If MySqlcon2.State = ConnectionState.Closed Or MySqlcon2.State = ConnectionState.Broken Then
            Return False
        Else
            Return True
        End If
        'End Try


    End Function
    Public Function GlobalRedondea(ByVal pCantidad As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte) As Double
        '0:              sin(redondeo)
        '1:              estandar()
        '2:              hacia arriba .5
        '3:              hacia(abajo)
        '4:              hacia(arriba)
        Select Case pTipoRedondeo
            Case 1
                pCantidad = Math.Round(pCantidad, pCantidadDecimales)
            Case 2
                Dim Temp2 As Double = 0
                Dim Temp1 As Double = 0
                Dim temp3 As Double = 0
                Dim strTemp As String
                Dim StrTemp2 As String
                If pCantidadDecimales < 2 Then
                    Temp1 = Fix(Math.Round(pCantidad, 2))
                    Temp2 = Math.Round(pCantidad - Temp1, 2)
                Else
                    Temp1 = Fix(Math.Round(pCantidad, pCantidadDecimales))
                    Temp2 = Math.Round(pCantidad - Temp1, pCantidadDecimales)
                End If


                'Temp2 = pCantidad - Temp1

                Select Case pCantidadDecimales
                    Case 1
                        temp3 = 0
                        If Temp2 <= 0.5 And Temp2 <> 0 Then
                            Temp2 = 0.5
                        Else
                            If Temp2 <> 0 Then Temp2 = 1
                        End If
                    Case 2
                        'temp3 = Math.Truncate(Temp2 * 100) / 100
                        'Temp2 = Temp2 - temp3
                        strTemp = Format(Temp2, "#0.00")
                        StrTemp2 = strTemp.Substring(strTemp.LastIndexOf(".") + 2, 1)
                        If StrTemp2 <= "5" And StrTemp2 > "0" Then
                            temp3 = (5 - CInt(StrTemp2)) / 100
                        Else
                            If StrTemp2 <> "0" Then temp3 = (10 - CInt(StrTemp2)) / 100
                        End If
                    Case 3
                        strTemp = Format(Temp2, "#0.000")
                        StrTemp2 = strTemp.Substring(strTemp.LastIndexOf(".") + 3, 1)
                        If StrTemp2 <= "5" And StrTemp2 > "0" Then
                            temp3 = (5 - CInt(StrTemp2)) / 1000
                        Else
                            If StrTemp2 <> "0" Then temp3 = (10 - CInt(StrTemp2)) / 1000
                        End If
                    Case 4
                        strTemp = Format(Temp2, "#0.0000")
                        StrTemp2 = strTemp.Substring(strTemp.LastIndexOf(".") + 4, 1)
                        If StrTemp2 <= "5" And StrTemp2 < "0" Then
                            temp3 = (5 - CInt(StrTemp2)) / 10000
                        Else
                            If StrTemp2 <> "0" Then temp3 = (10 - CInt(StrTemp2)) / 10000
                        End If
                    Case 5
                        strTemp = Format(Temp2, "#0.00000")
                        StrTemp2 = strTemp.Substring(strTemp.LastIndexOf(".") + 5, 1)
                        If StrTemp2 <= "5" And StrTemp2 > "0" Then
                            temp3 = (5 - CInt(StrTemp2)) / 100000
                        Else
                            If StrTemp2 <> "0" Then temp3 = (10 - CInt(StrTemp2)) / 100000
                        End If
                End Select
                pCantidad = Temp1 + temp3 + Temp2
            Case 3
                Dim Temp2 As Double
                Dim Temp1 As Double
                Dim temp3 As Double
                Temp1 = Fix(pCantidad)
                Temp2 = pCantidad - Temp1
                Select Case pCantidadDecimales
                    Case 1
                        temp3 = 0
                        Temp2 = 0
                    Case 2
                        temp3 = Math.Truncate(Temp2 * 100) / 100
                        'Temp2 = Temp2 - temp3
                        'If Temp2 <= 0.05 Then Temp2 = 0.05 - Temp2
                        'If Temp2 > 0.05 Then Temp2 = 0.1 - Temp2
                    Case 3
                        temp3 = Math.Truncate(Temp2 * 1000) / 1000
                        'Temp2 = Temp2 - temp3
                        'If Temp2 <= 0.005 Then Temp2 = 0.005
                        'If Temp2 > 0.005 Then Temp2 = 0.01
                    Case 4
                        temp3 = Math.Truncate(Temp2 * 10000) / 10000
                        'Temp2 = Temp2 - temp3
                        'If Temp2 <= 0.0005 Then Temp2 = 0.0005
                        'If Temp2 > 0.0005 Then Temp2 = 0.001
                    Case 5
                        temp3 = Math.Truncate(Temp2 * 100000) / 100000
                        'Temp2 = Temp2 - temp3
                        'If Temp2 <= 0.00005 Then Temp2 = 0.00005
                        'If Temp2 > 0.00005 Then Temp2 = 0.0001
                End Select
                pCantidad = Temp1 + temp3
            Case 4
                Dim Temp2 As Double
                Dim Temp1 As Double
                Dim temp3 As Double
                Temp1 = Fix(pCantidad)
                Temp2 = pCantidad - Temp1
                Select Case pCantidadDecimales
                    Case 1
                        If Temp2 <> 0 Then
                            Temp2 = 1
                        Else
                            Temp2 = 0
                        End If
                    Case 2
                        temp3 = Math.Truncate(Temp2 * 100) / 100
                        Temp2 = Temp2 - temp3
                        'If Temp2 <> 0.05 Then Temp2 = 0.05 - Temp2
                        If Temp2 <> 0 Then Temp2 = 0.1 - Temp2
                    Case 3
                        temp3 = Math.Truncate(Temp2 * 1000) / 1000
                        'Temp2 = Temp2 - temp3
                        'If Temp2 <= 0.005 Then Temp2 = 0.005
                        'If Temp2 > 0.005 Then Temp2 = 0.01
                    Case 4
                        temp3 = Math.Truncate(Temp2 * 10000) / 10000
                        Temp2 = Temp2 - temp3
                        'If Temp2 <= 0.0005 Then Temp2 = 0.0005
                        If Temp2 <> 0 Then Temp2 = 0.001
                    Case 5
                        temp3 = Math.Truncate(Temp2 * 100000) / 100000
                        Temp2 = Temp2 - temp3
                        'If Temp2 <= 0.00005 Then Temp2 = 0.00005
                        If Temp2 <> 0 Then Temp2 = 0.0001
                End Select
                pCantidad = Temp1 + temp3 + Temp2
        End Select
        Return pCantidad
    End Function

    Public Function ChecaDB() As Boolean
        MySqlcomInt.CommandText = "select max(versionchk) from tblactualizaciones"
        If MySqlcomInt.ExecuteScalar <> VersionDB Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Sub AddError(pDescripcion As String, pDondeFue As String, pFecha As String, pHora As String, pIdMovimiento As Integer)
        If pDescripcion.Length > 2000 Then pDescripcion = pDescripcion.Substring(1, 2000)
        MySqlcomInt.CommandText = "insert into tbllogdeerrores(descripcion,dondefue,fecha,hora,idmovimiento) values('" + Replace(Replace(pDescripcion, "\", "\\"), "'", "''") + "','" + pDondeFue + "','" + pFecha + "','" + pHora + "'," + pIdMovimiento.ToString + ")"
        MySqlcomInt.ExecuteNonQuery()
    End Sub
    Public Sub AddErrorTimbrado(pDescripcion As String, pDondeFue As String, pFecha As String, pHora As String, pIdMovimiento As Integer)
        If pDescripcion.Length > 2000 Then pDescripcion = pDescripcion.Substring(1, 2000)
        MySqlcomInt.CommandText = "insert into tbllogdeerrorest(descripcion,dondefue,fecha,hora,idmovimiento) values('" + Replace(Replace(pDescripcion, "\", "\\"), "'", "''") + "','" + pDondeFue + "','" + pFecha + "','" + pHora + "'," + pIdMovimiento.ToString + ")"
        MySqlcomInt.ExecuteNonQuery()
    End Sub
    Public Function InicioRapido() As Boolean
        Try
            MySqlcomInt.CommandText = "select folioinicial from tblopciones;"
            If MySqlcomInt.ExecuteScalar = 1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function AcuseCancelacion(pUUID As String, pApikey As String, pRFC As String, pDoc As String, pIdMov As Integer) As String
        Dim Cadena As String
        Dim XmlTimbrado As String = ""
        Try
            Cadena = pRFC + "~" + pApikey + "~" + pUUID
            Dim FF As New facturafielacusecan.server
            FF.Url = "http://www.facturafiel.com/websrv/sist_acuse_cancelacion.php?wsdl"
            XmlTimbrado = FF.sist_acuse_cancelacion_service(Cadena)
            FF.Dispose()
            Return XmlTimbrado
        Catch ex As Exception
            AddErrorTimbrado(ex.Message, "Acuse en " + pDoc, Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), pIdMov)
            Return "ERROR"
        End Try
        Return True
    End Function
    Public Function Timbrar33(ByVal pRFC As String, ByVal pXML As String, ByVal pRutaSalida As String, ByVal pAPIKEY As String, ByVal pConMsgbox As Boolean, ByVal pFolio As Integer, ByVal pSerie As String, pDocumento As String, pId As Integer) As String
        Dim Cadena As String
        Dim XmlTimbrado As String = ""
        Try
            Dim Pruebas As String = "NO"
            MySqlcomInt.CommandText = "select codigopostal from tblopciones limit 1"
            Pruebas = MySqlcomInt.ExecuteScalar
            Cadena = pRFC + "~" + pAPIKEY + "~" + Pruebas + "~" + pDocumento + "~" + pXML
            Dim FF As New facturafiel33.server
            FF.Url = "https://www.facturafiel.com/websrv/servicio_timbrado_xml_33.php?wsdl"
            XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
            FF.Dispose()
            Dim Checa As String
            Checa = XmlTimbrado
            Checa = Checa.ToUpper
        Catch ex As Exception
            Dim en As New Encriptador
            IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
            If IO.File.Exists(Application.StartupPath + "\temp\ferror.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\ferror.txt")
            End If
            en.GuardaArchivoTexto(Application.StartupPath + "\temp\ferror.txt", ex.Message, System.Text.Encoding.Default)
            AddErrorTimbrado(Replace(ex.Message, "'", "''"), pDocumento, Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), pId)
            'XmlTimbrado = "Recuperar"
            XmlTimbrado = "ERROR"
            'End If
        End Try
        Return XmlTimbrado
    End Function

End Module
