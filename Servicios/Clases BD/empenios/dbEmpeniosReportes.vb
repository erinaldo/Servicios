Imports MySql.Data.MySqlClient
Public Class dbEmpeniosReportes
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Dim tabla As New DataTable
    Public prestamosF As Double = 0
    Public capitalF As Double = 0
    Public pagosF As Double = 0
    Public interesF As Double = 0
    Public comprasF As Double = 0
    Public sueldosF As Double = 0
    Public variosF As Double = 0
    Public ingresosF As Double = 0
    Public depositosF As Double = 0
    Public saldoF As Double = 0
    Public saldoInicialF As Double = 0
    Public saldoTotalF As Double = 0
    '***
    Public CanprestamosF As Double = 0
    Public CancapitalF As Double = 0
    Public CanpagosF As Double = 0
    Public CaninteresF As Double = 0
    Public CancomprasF As Double = 0
    Public CansueldosF As Double = 0
    Public CanvariosF As Double = 0
    Public CaningresosF As Double = 0
    Public CandepositosF As Double = 0
    Public CansaldoF As Double = 0
    Public CansaldoInicialF As Double = 0
    Public CansaldoTotalF As Double = 0

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)

        Comm.Connection = Conexion
        tabla.Columns.Add("fecha")
        tabla.Columns.Add("prestamos")
        tabla.Columns.Add("capital")
        tabla.Columns.Add("pagos")
        tabla.Columns.Add("interes")

        tabla.Columns.Add("compras")
        tabla.Columns.Add("sueldos")
        tabla.Columns.Add("varios")
        tabla.Columns.Add("ingresos")

        tabla.Columns.Add("depositos")
        tabla.Columns.Add("saldo")
        tabla.Columns.Add("saldoInicial")

        
    End Sub

    Public Function filtroTodos(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, PPendientes As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblempenios.idmovimiento, tblempenios.fecha, CONCAT(tblempenios.serie,' ',tblempenios.folio), tblempenios.total, tblclientes.nombre as cliente, tblvendedores.nombre as vendedor from tblempenios inner join tblclientes on tblempenios.idcliente =tblclientes.idcliente  inner join tblvendedores on tblempenios.idvendedor = tblvendedores.idvendedor where tblempenios.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblempenios.fecha>='" + pFechaI + "' and tblempenios.fecha<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenios.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenios.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenios.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If PPendientes Then
            Comm.CommandText += " and tblempenios.pagado=0"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempenios")
        'DS.WriteXmlSchema("tblImpresionEnajenante.xml")
        Return DS.Tables("tblempenios").DefaultView
    End Function

    Public Function filtroEmpeniosDetalles(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pidClasificacion As Integer, pPendientes As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select  tblclientes.nombre as cliente, tblempenios.fecha, CONCAT(tblempenios.serie,' ',tblempenios.folio) as folio,tblempeniosclasificacion.nombre,tblempeniosdetalles.descripcion,tblempeniosdetalles.kilates, tblempeniosdetalles.peso, tblempeniosdetalles.evaluo, tblempeniosdetalles.precio, tblempenios.total from tblempeniosdetalles inner join tblempenios on tblempeniosdetalles.idMovimiento =tblempenios.idmovimiento  inner join tblempeniosclasificacion on tblempeniosdetalles.clasificacion = tblempeniosclasificacion.idClasificacion inner join tblclientes on tblempenios.idcliente =tblclientes.idcliente where tblempenios.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblempenios.fecha>='" + pFechaI + "' and tblempenios.fecha<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenios.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenios.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenios.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pidClasificacion <> -1 Then
            Comm.CommandText += " and tblempeniosdetalles.clasificacion=" + pidClasificacion.ToString
        End If
        If pPendientes Then
            Comm.CommandText += " and tblempenios.pagado=0"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosDetalles")
        'DS.WriteXmlSchema("tblEmpeniosDetalles.xml")
        Return DS.Tables("tblempeniosDetalles").DefaultView
    End Function
    Public Function filtroEmpeniosDetallesV(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pidClasificacion As Integer, pPendientes As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select  tblclientes.nombre as cliente, tblempenios.fecha, CONCAT(tblempenios.serie,' ',tblempenios.folio) as folio,tblempeniosclasificacion.nombre,tblempeniosdetallesv.marca,tblempeniosdetallesv.modelo,tblempeniosdetallesv.color,tblempeniosdetallesv.noSerie,tblempeniosdetallesv.placas,tblempeniosdetallesv.evaluo, tblempeniosdetallesv.importe, tblempenios.total from tblempeniosdetallesv inner join tblempenios on tblempeniosdetallesv.idMovimiento =tblempenios.idmovimiento  inner join tblempeniosclasificacion on tblempeniosdetallesv.clasificacion = tblempeniosclasificacion.idClasificacion inner join tblclientes on tblempenios.idcliente =tblclientes.idcliente where tblempenios.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblempenios.fecha>='" + pFechaI + "' and tblempenios.fecha<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenios.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenios.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenios.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pidClasificacion <> -1 Then
            Comm.CommandText += " and tblempeniosdetallesv.clasificacion=" + pidClasificacion.ToString
        End If
        If pPendientes Then
            Comm.CommandText += " and tblempenios.pagado=0"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosdetallesv")
        DS.WriteXmlSchema("tblempeniosdetallesv.xml")
        Return DS.Tables("tblempeniosdetallesv").DefaultView
    End Function
    Public Function filtroEmpeniosDetallesT(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pidClasificacion As Integer, pPendientes As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select  tblclientes.nombre as cliente, tblempenios.fecha, CONCAT(tblempenios.serie,' ',tblempenios.folio) as folio,tblempeniosclasificacion.nombre,tblempeniosdetallest.descripcion,tblempeniosdetallest.evaluo, tblempeniosdetallest.importe, tblempenios.total from tblempeniosdetallest inner join tblempenios on tblempeniosdetallest.idMovimiento =tblempenios.idmovimiento  inner join tblempeniosclasificacion on tblempeniosdetallest.clasificacion = tblempeniosclasificacion.idClasificacion inner join tblclientes on tblempenios.idcliente =tblclientes.idcliente where tblempenios.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblempenios.fecha>='" + pFechaI + "' and tblempenios.fecha<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenios.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenios.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenios.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pidClasificacion <> -1 Then
            Comm.CommandText += " and tblempeniosdetallest.clasificacion=" + pidClasificacion.ToString
        End If
        If pPendientes Then
            Comm.CommandText += " and tblempenios.pagado=0"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosdetallest")
        'DS.WriteXmlSchema("tblempeniosdetallest.xml")
        Return DS.Tables("tblempeniosdetallest").DefaultView
    End Function
    'pagos
    Public Function filtroPagos(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, pVis As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select  CONCAT(tblempenios.serie,' ',tblempenios.folio), tblempenios.fecha, tblempeniosabono.fecha,IF(tblempeniosabono.tipoPago=1,0,(tblempeniosabono.cantidad-tblempeniosabono.totalRefrendo)) as interes,IF(tblempeniosabono.tipoPago=1,tblempeniosabono.cantidad,tblempeniosabono.totalRefrendo) as capital,tblempeniosabono.cantidad, tblvendedores.nombre as vendedor, IF(tblempeniosabono.cantidad=(tblempeniosabono.totalEmepnio + tblempeniosabono.totalRefrendo),'SÍ','NO')  from tblempeniosabono inner join tblempenios on tblempeniosabono.idEmpenio =tblempenios.idmovimiento inner join tblvendedores on tblempenios.idvendedor = tblvendedores.idvendedor   where tblempenios.estado=3 and tblempeniosabono.descuento=0 and tblempeniosabono.tipopago<>3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblempeniosabono.fecha>='" + pFechaI + "' and tblempeniosabono.fecha<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenios.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenios.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenios.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pVis <> 0 Then
            Comm.CommandText += " and tblempeniosabono.vis=1"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosPagos")
        'DS.WriteXmlSchema("tblempeniosPagos.xml")
        Return DS.Tables("tblempeniosPagos").DefaultView
    End Function
    Public Function ReporteEmpeniosIntereses(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, pVis As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblempeniosabono.folio, tblempeniosabono.fecha, tblclientes.nombre,tblempeniosabono.cantidad from tblempeniosabono inner join tblempenios on tblempeniosabono.idEmpenio=tblempenios.idmovimiento inner join tblvendedores on tblempenios.idvendedor = tblvendedores.idvendedor inner join tblclientes on tblempenios.idcliente=tblclientes.idcliente where tblempenios.estado=3 and tblempeniosabono.descuento=0 and tblempeniosabono.tipopago=1 "
        If pFechaF <> "" Then
            Comm.CommandText += " and tblempeniosabono.fecha>='" + pFechaI + "' and tblempeniosabono.fecha<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenios.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenios.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenios.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pVis <> 0 Then
            Comm.CommandText += " and tblempeniosabono.vis=1"
        End If
        Comm.CommandText += " order by tblempeniosabono.folio,tblempeniosabono.fecha"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosint")
        'DS.WriteXmlSchema("tblempeniosint.xml")
        Return DS.Tables("tblempeniosint").DefaultView
    End Function
    Public Function filtroRMEmpenios(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, pVis As Byte) As DataTable
        Dim fecha1 As Date = Date.Parse(pFechaI)
        Dim fechaAntes As Date
        Dim fecha As String
        ' Dim tabla As DataTable
        Dim dias As Integer = DateDiff(DateInterval.Day, Date.Parse(pFechaI), Date.Parse(pFechaF))
        fechaAntes = DateAdd(DateInterval.Day, -1, fecha1)
        'fechaAntes.AddDays(-1)
        'variables
        'Inicializar el cero
        prestamosF = 0
        capitalF = 0
        pagosF = 0
        interesF = 0
        comprasF = 0
        sueldosF = 0
        variosF = 0
        ingresosF = 0
        depositosF = 0
        saldoF = 0
        saldoInicialF = 0
        saldoTotalF = 0

        Dim prestamos As Double = 0
        Dim capital As Double = 0
        Dim pagos As Double = 0
        Dim interes As Double = 0
        Dim compras As Double = 0
        Dim sueldos As Double = 0
        Dim varios As Double = 0
        Dim ingresos As Double = 0 'ingresos de que?
        Dim depositos As Double = 0
        Dim saldo As Double = 0
        Dim saldoInicial As Double = 0
        Dim saldoTotal = 0
        tabla.Rows.Clear()
        saldoInicial = csaldoInicial(fechaAntes.ToString("yyyy/MM/dd"), pidSucursal, pidCaja)
        fecha1 = DateAdd(DateInterval.Day, -1, fecha1)
        For i As Integer = 0 To dias
            fecha1 = DateAdd(DateInterval.Day, 1, fecha1)
            fecha = fecha1.ToString("yyyy/MM/dd")

            If hayMovimientos(fecha, pidSucursal) <> 0 Then


                '****Prestamos***
                Comm.CommandText = "select  COALESCE(sum(tblempenios.total),0) from tblempenios where tblempenios.estado=3 "
                Comm.CommandText += " and tblempenios.fecha='" + fecha + "'"
                If pidSucursal <> -1 Then
                    Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
                End If
                If pidCaja <> -1 Then
                    Comm.CommandText += " and tblempenios.idcaja=" + pidCaja.ToString
                End If
                prestamos = Comm.ExecuteScalar
                prestamosF += prestamos
                '****Pagos*****
                Comm.CommandText = "select  COALESCE(sum(tblempeniosabono.cantidad),0) from tblempeniosabono inner join tblempenios on tblempeniosabono.idEmpenio=tblempenios.idmovimiento  where descuento=0 "
                Comm.CommandText += " and tblempeniosabono.fecha='" + fecha + "'"
                If pidSucursal <> -1 Then
                    Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
                End If
                If pidCaja <> -1 Then
                    Comm.CommandText += " and tblempeniosabono.caja=" + pidCaja.ToString
                End If
                If pVis <> 0 Then
                    Comm.CommandText += " and tblempeniosabono.vis=1"
                End If
                pagos = Comm.ExecuteScalar
                pagosF += pagos
                '******interes**********
                Comm.CommandText = "select  COALESCE(sum(tblempeniosabono.cantidad),0) from tblempeniosabono inner join tblempenios on tblempeniosabono.idempenio=tblempenios.idmovimiento  where tblempeniosabono.tipoPago=1  and tblempeniosabono.descuento=0 "
                Comm.CommandText += "and tblempeniosabono.fecha='" + fecha + "'"
                If pidSucursal <> -1 Then
                    Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
                End If
                If pidCaja <> -1 Then
                    Comm.CommandText += " and tblempeniosabono.caja=" + pidCaja.ToString
                End If
                If pVis <> 0 Then
                    Comm.CommandText += " and tblempeniosabono.vis=1"
                End If
                interes = Comm.ExecuteScalar
                interesF += interes
                '*******capital*****
                Comm.CommandText = "select  COALESCE(sum(tblempeniosabono.cantidad),0) from tblempeniosabono inner join tblempenios on tblempeniosabono.idempenio=tblempenios.idmovimiento  where (tblempeniosabono.tipoPago=0 or tblempeniosabono.tipoPago=3) and tblempeniosabono.descuento=0 "
                Comm.CommandText += "and tblempeniosabono.fecha='" + fecha + "'"
                If pidSucursal <> -1 Then
                    Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
                End If
                If pidCaja <> -1 Then
                    Comm.CommandText += " and tblempeniosabono.caja=" + pidCaja.ToString
                End If
                If pVis <> 0 Then
                    Comm.CommandText += " and tblempeniosabono.vis=1"
                End If
                capital = Comm.ExecuteScalar
                capitalF += capital
                '********interes del capital
                Comm.CommandText = "select  COALESCE(sum(tblempeniosabono.totalRefrendo),0) from tblempeniosabono inner join tblempenios on tblempeniosabono.idempenio=tblempenios.idmovimiento  where (tblempeniosabono.tipoPago=0 or tblempeniosabono.tipoPago=3) and tblempeniosabono.descuento=0 "
                Comm.CommandText += "and tblempeniosabono.fecha='" + fecha + "'"
                If pidSucursal <> -1 Then
                    Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
                End If
                If pidCaja <> -1 Then
                    Comm.CommandText += " and tblempeniosabono.caja=" + pidCaja.ToString
                End If
                If pVis <> 0 Then
                    Comm.CommandText += " and tblempeniosabono.vis=1"
                End If
                interes += Comm.ExecuteScalar
                'interesF += interes
                '**************compras************
                Comm.CommandText = "select  COALESCE(sum(tblempenioscompras.total),0) from tblempenioscompras where tblempenioscompras.estado=3 "
                Comm.CommandText += " and tblempenioscompras.fecha='" + fecha + "'"
                If pidSucursal <> -1 Then
                    Comm.CommandText += " and tblempenioscompras.idsucursal=" + pidSucursal.ToString
                End If
                If pidCaja <> -1 Then
                    Comm.CommandText += " and tblempenioscompras.idcaja=" + pidCaja.ToString
                End If
                compras = Comm.ExecuteScalar
                comprasF += compras
                '********sueldos********
                Comm.CommandText = "select  COALESCE(sum(tblgastosdetalles.precio),0) from tblgastosdetalles inner join tblgastos on tblgastosdetalles.idMovimiento=tblgastos.idmovimiento  where tblgastosdetalles.esSalario=1 and estado=3 "
                Comm.CommandText += "and tblgastos.fecha='" + fecha + "'"
                If pidSucursal <> -1 Then
                    Comm.CommandText += " and tblgastos.idsucursal=" + pidSucursal.ToString
                End If
                If pidCaja <> -1 Then
                    Comm.CommandText += " and tblgastos.idcaja=" + pidCaja.ToString
                End If
                sueldos = Comm.ExecuteScalar
                sueldosF += sueldos
                '************varios**********
                Comm.CommandText = "select  COALESCE(sum(tblgastosdetalles.precio),0) from tblgastosdetalles inner join tblgastos on tblgastosdetalles.idMovimiento=tblgastos.idmovimiento  where tblgastosdetalles.esSalario=0 and estado=3 "
                Comm.CommandText += " and tblgastos.fecha='" + fecha + "'"
                If pidSucursal <> -1 Then
                    Comm.CommandText += " and tblgastos.idsucursal=" + pidSucursal.ToString
                End If
                If pidCaja <> -1 Then
                    Comm.CommandText += " and tblgastos.idcaja=" + pidCaja.ToString
                End If
                varios = Comm.ExecuteScalar
                variosF += varios
                '******ingresos*********
                Comm.CommandText = "select  COALESCE(sum(tblcajasmovimientos.total),0) from tblcajasmovimientos where tipo=0 and estado=3"
                Comm.CommandText += " and tblcajasmovimientos.fecha='" + fecha + "'"
                If pidSucursal <> -1 Then
                    Comm.CommandText += " and tblcajasmovimientos.idsucursal=" + pidSucursal.ToString
                End If
                If pidCaja <> -1 Then
                    Comm.CommandText += " and tblcajasmovimientos.idcaja=" + pidCaja.ToString
                End If

                ingresos = Comm.ExecuteScalar
                ingresosF += ingresos
                '******depositos los que salen de la caja 1*********
                Comm.CommandText = "select  COALESCE(sum(tblcajasmovimientos.total),0) from tblcajasmovimientos where tipo=1 and estado=3 "
                Comm.CommandText += " and tblcajasmovimientos.fecha='" + fecha + "'"
                If pidSucursal <> -1 Then
                    Comm.CommandText += " and tblcajasmovimientos.idsucursal=" + pidSucursal.ToString
                End If
                If pidCaja <> -1 Then
                    Comm.CommandText += " and tblcajasmovimientos.idcaja=" + pidCaja.ToString
                End If
                depositos = Comm.ExecuteScalar
                depositosF += depositos
                saldo = (-prestamos) + capital + interes - compras - sueldos - varios + ingresos - depositos


                Dim dr As DataRow
                dr = tabla.NewRow()
                dr("fecha") = fecha
                dr("prestamos") = prestamos
                dr("capital") = capital
                dr("pagos") = pagos
                dr("interes") = interes

                dr("compras") = compras
                dr("sueldos") = sueldos
                dr("varios") = varios
                dr("ingresos") = ingresos

                dr("depositos") = depositos
                saldoTotal += saldo + saldoInicial
                saldoTotalF = saldoTotal
                dr("saldo") = saldoTotal
                dr("saldoInicial") = saldoInicial
                saldoInicial = 0
                tabla.Rows.Add(dr)
            End If
        Next
        'Dim dataSet As DataSet = New DataSet
        'dataSet.Tables.Add(tabla)
        'dataSet.WriteXmlSchema("ReporteMensual.xml")
        Return tabla

    End Function
    Private Function hayMovimientos(ByVal fecha As String, ByVal pidSucursal As Integer)
        Dim contador As Integer = 0

        '****Prestamos***
        Comm.CommandText = "select  COUNT(tblempenios.total) from tblempenios where tblempenios.estado=3 "
        Comm.CommandText += " and tblempenios.fecha='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        contador += Comm.ExecuteScalar
        '****Pagos*****
        Comm.CommandText = "select  COUNT(tblempeniosabono.cantidad) from tblempeniosabono inner join tblempenios on tblempeniosabono.idEmpenio=tblempenios.idmovimiento  where "
        Comm.CommandText += " tblempeniosabono.fecha='" + fecha + "'"
        contador += Comm.ExecuteScalar
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        '**************compras************
        Comm.CommandText = "select  COUNT(tblempenioscompras.total) from tblempenioscompras where tblempenioscompras.estado=3 "
        Comm.CommandText += " and tblempenioscompras.fecha='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idsucursal=" + pidSucursal.ToString
        End If
        contador += Comm.ExecuteScalar

        '********sueldos********
        Comm.CommandText = "select  COUNT(tblgastosdetalles.precio) from tblgastosdetalles inner join tblgastos on tblgastosdetalles.idMovimiento=tblgastos.idmovimiento  where tblgastosdetalles.esSalario=1 "
        Comm.CommandText += "and tblgastos.fecha='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblgastos.idsucursal=" + pidSucursal.ToString
        End If
        contador += Comm.ExecuteScalar
        '************varios**********
        Comm.CommandText = "select  COUNT(tblgastosdetalles.precio)  from tblgastosdetalles inner join tblgastos on tblgastosdetalles.idMovimiento=tblgastos.idmovimiento  where tblgastosdetalles.esSalario=0 "
        Comm.CommandText += " and tblgastos.fecha='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblgastos.idsucursal=" + pidSucursal.ToString
        End If
        contador += Comm.ExecuteScalar
        '******ingresos*********
        Comm.CommandText = "select  COUNT(tblcajasmovimientos.total) from tblcajasmovimientos where tipo=0 "
        Comm.CommandText += "and tblcajasmovimientos.fecha='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblcajasmovimientos.idsucursal=" + pidSucursal.ToString
        End If
        contador += Comm.ExecuteScalar
        '******depositos los que salen de la caja 1*********
        Comm.CommandText = "select COUNT(tblcajasmovimientos.total) from tblcajasmovimientos where tipo=1 "
        Comm.CommandText += " and tblcajasmovimientos.fecha='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblcajasmovimientos.idsucursal=" + pidSucursal.ToString
        End If
        contador += Comm.ExecuteScalar
        Return contador


    End Function
    Public Function filtroRMECantidad(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, pVis As Byte) As DataTable
        Dim fecha1 As Date = Date.Parse(pFechaI)
        'Dim fecha As String
        ' Dim tabla As DataTable
        Dim dias As Integer = DateDiff(DateInterval.Day, Date.Parse(pFechaI), Date.Parse(pFechaF))
        'variables
        'Inicializar el cero
        CanprestamosF = 0
        CancapitalF = 0
        CanpagosF = 0
        CaninteresF = 0
        CancomprasF = 0
        CansueldosF = 0
        CanvariosF = 0
        CaningresosF = 0
        CandepositosF = 0
        CansaldoF = 0
        CansaldoInicialF = 0
        CansaldoTotalF = 0

        'Dim prestamos As Double = 0
        'Dim capital As Double = 0
        'Dim pagos As Double = 0
        'Dim interes As Double = 0
        'Dim compras As Double = 0
        'Dim sueldos As Double = 0
        'Dim varios As Double = 0
        'Dim ingresos As Double = 0 'ingresos de que?
        'Dim depositos As Double = 0
        'Dim saldo As Double = 0
        'Dim saldoInicial As Double = 0
        'Dim saldoTotal = 0
        'tabla.Rows.Clear()
        'saldoInicial = csaldoInicial(fecha1.ToString("yyyy/MM/dd"), pidSucursal)
        fecha1 = DateAdd(DateInterval.Day, -1, fecha1)
        '  For i As Integer = 0 To dias
        ' fecha1 = DateAdd(DateInterval.Day, 1, fecha1)
        ' fecha = fecha1.ToString("yyyy/MM/dd")

        ' If hayMovimientos(fecha, pidSucursal) <> 0 Then


        '****Prestamos***
        Comm.CommandText = "select  count(tblempenios.total) from tblempenios where tblempenios.estado=3 "
        Comm.CommandText += " and tblempenios.fechaContrato>='" + pFechaI + "' and tblempenios.fechaContrato<='" + pFechaF + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenios.idcaja=" + pidCaja.ToString
        End If
        CanprestamosF += Comm.ExecuteScalar
        'CanprestamosF += prestamos
        '****Pagos*****
        Comm.CommandText = "select  count(tblempeniosabono.cantidad) from tblempeniosabono inner join tblempenios on tblempeniosabono.idEmpenio=tblempenios.idmovimiento  where tblempeniosabono.descuento=0  and"
        Comm.CommandText += " tblempeniosabono.fecha>='" + pFechaI + "' and tblempeniosabono.fecha<='" + pFechaF + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempeniosabono.caja=" + pidCaja.ToString
        End If
        If pVis <> 0 Then
            Comm.CommandText += " and tblempeniosabono.vis=1"
        End If
        CanpagosF += Comm.ExecuteScalar
        ' counpagosF += pagos
        '******interes**********
        Comm.CommandText = "select count(tblempeniosabono.cantidad) from tblempeniosabono inner join tblempenios on tblempeniosabono.idempenio=tblempenios.idmovimiento  where tblempeniosabono.tipoPago=1 and tblempeniosabono.descuento=0  "
        Comm.CommandText += "and tblempeniosabono.fecha>='" + pFechaI + "' and tblempeniosabono.fecha<='" + pFechaF + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempeniosabono.caja=" + pidCaja.ToString
        End If
        If pVis <> 0 Then
            Comm.CommandText += " and tblempeniosabono.vis=1"
        End If
        CaninteresF += Comm.ExecuteScalar
        'interesF += interes
        '*******capital*****
        Comm.CommandText = "select  count(tblempeniosabono.cantidad) from tblempeniosabono inner join tblempenios on tblempeniosabono.idempenio=tblempenios.idmovimiento  where (tblempeniosabono.tipoPago=0 or tblempeniosabono.tipoPago=3) and tblempeniosabono.descuento=0 "
        Comm.CommandText += "and tblempeniosabono.fecha>='" + pFechaI + "' and tblempeniosabono.fecha<='" + pFechaF + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempeniosabono.caja=" + pidCaja.ToString
        End If
        If pVis <> 0 Then
            Comm.CommandText += " and tblempeniosabono.vis=1"
        End If
        CancapitalF += Comm.ExecuteScalar
        'capitalF += capital
        '********interes del capital
        Comm.CommandText = "select  count(tblempeniosabono.totalRefrendo) from tblempeniosabono inner join tblempenios on tblempeniosabono.idempenio=tblempenios.idmovimiento  where (tblempeniosabono.tipoPago=0 or tblempeniosabono.tipoPago=3) and tblempeniosabono.descuento=0 "
        Comm.CommandText += "and tblempeniosabono.fecha>='" + pFechaI + "' and tblempeniosabono.fecha<='" + pFechaF + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempeniosabono.caja=" + pidCaja.ToString
        End If
        If pVis <> 0 Then
            Comm.CommandText += " and tblempeniosabono.vis=1"
        End If
        'interes += Comm.ExecuteScalar
        'interesF += interes
        '**************compras************
        Comm.CommandText = "select count(tblempenioscompras.total) from tblempenioscompras where tblempenioscompras.estado=3 "
        Comm.CommandText += " and tblempenioscompras.fecha>='" + pFechaI + "' and tblempenioscompras.fecha<='" + pFechaF + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idsucursal=" + pidSucursal.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idcaja=" + pidCaja.ToString
        End If
        CancomprasF = Comm.ExecuteScalar
        'comprasF += compras
        '********sueldos********
        Comm.CommandText = "select count(tblgastosdetalles.precio) from tblgastosdetalles inner join tblgastos on tblgastosdetalles.idMovimiento=tblgastos.idmovimiento  where tblgastosdetalles.esSalario=1 and estado=3"
        Comm.CommandText += " and tblgastos.fecha>='" + pFechaI + "' and tblgastos.fecha<='" + pFechaF + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblgastos.idsucursal=" + pidSucursal.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblgastos.idcaja=" + pidCaja.ToString
        End If
        CansueldosF = Comm.ExecuteScalar
        'sueldosF += sueldos
        '************varios**********
        Comm.CommandText = "select  count(tblgastosdetalles.precio) from tblgastosdetalles inner join tblgastos on tblgastosdetalles.idMovimiento=tblgastos.idmovimiento  where tblgastosdetalles.esSalario=0 and estado=3"
        Comm.CommandText += " and tblgastos.fecha>='" + pFechaI + "' and tblgastos.fecha<='" + pFechaF + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblgastos.idsucursal=" + pidSucursal.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblgastos.idcaja=" + pidCaja.ToString
        End If
        CanvariosF = Comm.ExecuteScalar
        'variosF += varios
        '******ingresos*********
        Comm.CommandText = "select  count(tblcajasmovimientos.total) from tblcajasmovimientos where tipo=0 "
        Comm.CommandText += "and tblcajasmovimientos.fecha>='" + pFechaI + "' and tblcajasmovimientos.fecha<='" + pFechaF + "' and estado=3"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblcajasmovimientos.idsucursal=" + pidSucursal.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblcajasmovimientos.idcaja=" + pidCaja.ToString
        End If

        CaningresosF = Comm.ExecuteScalar
        ' ingresosF += ingresos
        '******depositos los que salen de la caja 1*********
        Comm.CommandText = "select  count(tblcajasmovimientos.total) from tblcajasmovimientos where tipo=1 "
        Comm.CommandText += " and tblcajasmovimientos.fecha>='" + pFechaI + "' and tblcajasmovimientos.fecha<='" + pFechaF + "' and estado=3"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblcajasmovimientos.idsucursal=" + pidSucursal.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblcajasmovimientos.idcaja=" + pidCaja.ToString
        End If
        CandepositosF = Comm.ExecuteScalar
        'depositosF += depositos
        ' saldo = (-prestamos) + capital + interes - compras - sueldos - varios + ingresos - depositos



        '  End If
        ' Next
        'Dim dataSet As DataSet = New DataSet
        'dataSet.Tables.Add(tabla)
        'dataSet.WriteXmlSchema("ReporteMensual.xml")
        Return tabla

    End Function
    Public Function csaldoInicial(ByVal fecha As String, ByVal pidSucursal As Integer, ByVal pIDCaja As Integer) As Double
        Dim prestamos As Double = 0
        Dim capital As Double = 0
        Dim pagos As Double = 0
        Dim interes As Double = 0
        Dim compras As Double = 0
        Dim sueldos As Double = 0
        Dim varios As Double = 0
        Dim ingresos As Double = 0 'ingresos de que?
        Dim depositos As Double = 0
        Dim saldo As Double = 0


        '****Prestamos***
        Comm.CommandText = "select  COALESCE(sum(tblempenios.total),0) from tblempenios where tblempenios.estado=3 "
        Comm.CommandText += " and tblempenios.fecha<='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pIDCaja <> -1 Then
            Comm.CommandText += " and tblempenios.idcaja=" + pIDCaja.ToString
        End If
        prestamos = Comm.ExecuteScalar
        '****Pagos*****
        Comm.CommandText = "select  COALESCE(sum(tblempeniosabono.cantidad),0) from tblempeniosabono inner join tblempenios on tblempeniosabono.idEmpenio=tblempenios.idmovimiento  where descuento=0"
        Comm.CommandText += " and tblempeniosabono.fecha<='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pIDCaja <> -1 Then
            Comm.CommandText += " and tblempeniosabono.caja=" + pIDCaja.ToString
        End If
        pagos = Comm.ExecuteScalar
        '******interes**********
        Comm.CommandText = "select  COALESCE(sum(tblempeniosabono.cantidad),0) from tblempeniosabono inner join tblempenios on tblempeniosabono.idempenio=tblempenios.idmovimiento  where tblempeniosabono.tipoPago=1 and  descuento=0 "
        Comm.CommandText += "and tblempeniosabono.fecha<='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pIDCaja <> -1 Then
            Comm.CommandText += " and tblempeniosabono.caja=" + pIDCaja.ToString
        End If
        interes = Comm.ExecuteScalar
        '*******capital*****
        Comm.CommandText = "select  COALESCE(sum(tblempeniosabono.cantidad-tblempeniosabono.totalRefrendo),0) from tblempeniosabono inner join tblempenios on tblempeniosabono.idempenio=tblempenios.idmovimiento  where (tblempeniosabono.tipoPago=0 or tblempeniosabono.tipoPago=3) and  descuento=0"
        Comm.CommandText += " and tblempeniosabono.fecha<='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pIDCaja <> -1 Then
            Comm.CommandText += " and tblempeniosabono.caja=" + pIDCaja.ToString
        End If
        capital = Comm.ExecuteScalar
        '********interes del capital
        Comm.CommandText = "select  COALESCE(sum(tblempeniosabono.totalRefrendo),0) from tblempeniosabono inner join tblempenios on tblempeniosabono.idempenio=tblempenios.idmovimiento  where (tblempeniosabono.tipoPago=0 or tblempeniosabono.tipoPago=3) and  tblempeniosabono.descuento=0"
        Comm.CommandText += " and tblempeniosabono.fecha<='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        If pIDCaja <> -1 Then
            Comm.CommandText += " and tblempeniosabono.caja=" + pIDCaja.ToString
        End If
        interes += Comm.ExecuteScalar
        '**************compras************
        Comm.CommandText = "select  COALESCE(sum(tblempenioscompras.total),0) from tblempenioscompras where tblempenioscompras.estado=3 "
        Comm.CommandText += " and tblempenioscompras.fecha<='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idsucursal=" + pidSucursal.ToString
        End If
        If pIDCaja <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idcaja=" + pIDCaja.ToString
        End If
        compras = Comm.ExecuteScalar
        '********sueldos********
        Comm.CommandText = "select  COALESCE(sum(tblgastosdetalles.precio),0) from tblgastosdetalles inner join tblgastos on tblgastosdetalles.idMovimiento=tblgastos.idmovimiento  where tblgastosdetalles.esSalario=1 and tblgastos.estado=3 "
        Comm.CommandText += "and tblgastos.fecha<='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblgastos.idsucursal=" + pidSucursal.ToString
        End If
        If pIDCaja <> -1 Then
            Comm.CommandText += " and tblgastos.idcaja=" + pIDCaja.ToString
        End If
        sueldos = Comm.ExecuteScalar
        '************varios**********
        Comm.CommandText = "select  COALESCE(sum(tblgastosdetalles.precio),0) from tblgastosdetalles inner join tblgastos on tblgastosdetalles.idMovimiento=tblgastos.idmovimiento  where tblgastosdetalles.esSalario=0 and tblgastos.estado=3 "
        Comm.CommandText += " and tblgastos.fecha<='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblgastos.idsucursal=" + pidSucursal.ToString
        End If
        If pIDCaja <> -1 Then
            Comm.CommandText += " and tblgastos.idcaja=" + pIDCaja.ToString
        End If
        varios = Comm.ExecuteScalar
        '******ingresos*********
        Comm.CommandText = "select  COALESCE(sum(tblcajasmovimientos.total),0) from tblcajasmovimientos where tipo=0 and estado=3 "
        Comm.CommandText += "and tblcajasmovimientos.fecha<='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblcajasmovimientos.idsucursal=" + pidSucursal.ToString
        End If
        If pIDCaja <> -1 Then
            Comm.CommandText += " and tblcajasmovimientos.idcaja=" + pIDCaja.ToString
        End If
        ingresos = Comm.ExecuteScalar
        '******depositos los que salen de la caja 1*********
        Comm.CommandText = "select  COALESCE(sum(tblcajasmovimientos.total),0) from tblcajasmovimientos where tipo=1 and estado=3 "
        Comm.CommandText += " and tblcajasmovimientos.fecha<='" + fecha + "'"
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblcajasmovimientos.idsucursal=" + pidSucursal.ToString
        End If
        If pIDCaja <> -1 Then
            Comm.CommandText += " and tblcajasmovimientos.idcaja=" + pIDCaja.ToString
        End If
        depositos = Comm.ExecuteScalar
        saldo = (-prestamos) + capital + interes - compras - sueldos - varios + ingresos - depositos
        saldoInicialF = saldo
        Return saldo
    End Function

    Public Function filtroAdjudicaciones(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblempenios.fecha, CONCAT(tblempenios.serie,' ',tblempenios.folio), tblempeniosadjudicaciones.fechaAdjudicacion,tblclientes.nombre as cliente,tblempenios.TotalAux   from tblempeniosadjudicaciones inner join tblempenios on tblempeniosadjudicaciones.idEmpenio =tblempenios.idmovimiento inner join tblclientes on tblempenios.idcliente =tblclientes.idcliente   where tblempenios.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblempeniosadjudicaciones.fechaAdjudicacion>='" + pFechaI + "' and tblempeniosadjudicaciones.fechaAdjudicacion<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenios.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenios.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenios.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosAdjudicaciones")
        DS.WriteXmlSchema("tblempeniosAdjudicaciones.xml")
        Return DS.Tables("tblempeniosAdjudicaciones").DefaultView
    End Function
    Public Function filtroTodosCompras(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblempenioscompras.idmovimiento, tblempenioscompras.fecha, CONCAT(tblempenioscompras.serie,' ',tblempenioscompras.folio), tblempenioscompras.total, tblclientes.nombre as cliente, tblvendedores.nombre as vendedor from tblempenioscompras inner join tblclientes on tblempenioscompras.idcliente =tblclientes.idcliente  inner join tblvendedores on tblempenioscompras.idvendedor = tblvendedores.idvendedor where tblempenioscompras.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblempenioscompras.fecha>='" + pFechaI + "' and tblempenioscompras.fecha<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenioscompras.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idsucursal=" + pidSucursal.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempenioscompras")
        DS.WriteXmlSchema("tblImpresionCompras.xml")
        Return DS.Tables("tblempenioscompras").DefaultView
    End Function

    Public Function filtroComprasDetalles(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pidClasificacion As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select  tblclientes.nombre as cliente, tblempenioscompras.fecha, CONCAT(tblempenioscompras.serie,' ',tblempenioscompras.folio) as folio,tblempeniosclasificacion.nombre,tblempeniosgastosdetalles.descripcion,tblempeniosgastosdetalles.kilates, tblempeniosgastosdetalles.peso, tblempeniosgastosdetalles.evaluo, tblempeniosgastosdetalles.precio, tblempenioscompras.total from tblempeniosgastosdetalles inner join tblempenioscompras on tblempeniosgastosdetalles.idMovimiento =tblempenioscompras.idmovimiento  inner join tblempeniosclasificacion on tblempeniosgastosdetalles.clasificacion = tblempeniosclasificacion.idClasificacion inner join tblclientes on tblempenioscompras.idcliente =tblclientes.idcliente where tblempenioscompras.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblempenioscompras.fecha>='" + pFechaI + "' and tblempenioscompras.fecha<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenioscompras.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idsucursal=" + pidSucursal.ToString
        End If
        If pidClasificacion <> -1 Then
            Comm.CommandText += " and tblempeniosgastosdetalles.clasificacion=" + pidClasificacion.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosgastosdetalles")
        DS.WriteXmlSchema("tblempeniosComprasdetalles.xml")
        Return DS.Tables("tblempeniosgastosdetalles").DefaultView
    End Function
    Public Function filtroComprasDetallesV(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pidClasificacion As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select  tblclientes.nombre as cliente, tblempenioscompras.fecha, CONCAT(tblempenioscompras.serie,' ',tblempenioscompras.folio) as folio,tblempeniosclasificacion.nombre,tblempenioscomprasdetallesv.marca,tblempenioscomprasdetallesv.modelo,tblempenioscomprasdetallesv.color,tblempenioscomprasdetallesv.noSerie,tblempenioscomprasdetallesv.placas,tblempenioscomprasdetallesv.evaluo, tblempenioscomprasdetallesv.importe, tblempenioscompras.total from tblempenioscomprasdetallesv inner join tblempenioscompras on tblempenioscomprasdetallesv.idMovimiento =tblempenioscompras.idmovimiento  inner join tblempeniosclasificacion on tblempenioscomprasdetallesv.clasificacion = tblempeniosclasificacion.idClasificacion inner join tblclientes on tblempenioscompras.idcliente =tblclientes.idcliente where tblempenioscompras.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblempenioscompras.fecha>='" + pFechaI + "' and tblempenioscompras.fecha<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenioscompras.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idsucursal=" + pidSucursal.ToString
        End If
        If pidClasificacion <> -1 Then
            Comm.CommandText += " and tblempenioscomprasdetallesv.clasificacion=" + pidClasificacion.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosgastosdetalles")
        DS.WriteXmlSchema("tblempeniosComprasdetallesV.xml")
        Return DS.Tables("tblempeniosgastosdetalles").DefaultView
    End Function
    Public Function filtroComprasDetallesT(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pidClasificacion As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select  tblclientes.nombre as cliente, tblempenioscompras.fecha, CONCAT(tblempenioscompras.serie,' ',tblempenioscompras.folio) as folio,tblempeniosclasificacion.nombre,tblempenioscomprasdetallest.descripcion, tblempenioscomprasdetallest.evaluo, tblempenioscomprasdetallest.importe, tblempenioscompras.total from tblempenioscomprasdetallest inner join tblempenioscompras on tblempenioscomprasdetallest.idMovimiento =tblempenioscompras.idmovimiento  inner join tblempeniosclasificacion on tblempenioscomprasdetallest.clasificacion = tblempeniosclasificacion.idClasificacion inner join tblclientes on tblempenioscompras.idcliente =tblclientes.idcliente where tblempenioscompras.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblempenioscompras.fecha>='" + pFechaI + "' and tblempenioscompras.fecha<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenioscompras.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenioscompras.idsucursal=" + pidSucursal.ToString
        End If
        If pidClasificacion <> -1 Then
            Comm.CommandText += " and tblempenioscomprasdetallest.clasificacion=" + pidClasificacion.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosgastosdetalles")
        DS.WriteXmlSchema("tblempeniosComprasdetallesT.xml")
        Return DS.Tables("tblempeniosgastosdetalles").DefaultView
    End Function
End Class
