Imports MySql.Data.MySqlClient
Public Class dbSemillasgenerarReporte
    Private command As New MySqlCommand
    Private sucursal As dbSucursales

    Public Sub New(ByVal conexion As MySqlConnection)
        command.Connection = conexion
    End Sub

    Public Sub reporteGeneral()
        Dim ds As New DataSet
        command.CommandText = "select b.folio as folio, b.fecha as fecha, b.humedad as humedad, b.impurezas as impurezas, b.granoquebrado as granoQuebrado," +
            "b.granodanado as granoDanado,b.castigoHumedad as castigoHumedad,b.castigoimpurezas as castigoImpurezas,b.castigoGranoQ as castigoGranoQ, " +
            "b.castigoGranoD as castigoGranoD,b.pesoanalizado as pesoanalizado,b.hora as hora, b.tipoboleta as tipoboleta, prov.nombre as nombreProductor, inv.nombre as nombreProducto, b.peso as peso " +
     "from tblsemillasboletas as b inner join tblproveedores as prov on b.productor=prov.idproveedor inner join tblinventario as inv on b.producto=inv.idinventario "
        Dim da As New MySqlDataAdapter(command)
        da.Fill(ds, "Table")
        ' ds.WriteXml(CurDir() & "\repGeneral.xml", XmlWriteMode.WriteSchema)
        'Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        'rep = New reportePrueba
        'Dim fecha As New Date()
        'rep.SetParameterValue("fecha", fecha.ToString("yyyy/MM/dd"))
        'rep.SetDataSource(ds.Tables("Table"))
        'Dim RV As New frmReportes(rep, False)
        'RV.Show()
    End Sub

    Public Sub reporteProductos(ByVal producto As dbInventario)
        Dim ds As New DataSet
        command.CommandText = "select b.folio as folio, b.fecha as fecha, prov.nombre as nombreProductor, inv.nombre as nombreProducto, b.peso as peso " +
     "from tblsemillasboletas as b inner join tblproveedores as prov on b.productor=prov.idproveedor inner join tblinventario as inv on b.producto=inv.idinventario " +
     "where b.producto=" + producto.ID.ToString() + ";"
        Dim da As New MySqlDataAdapter(command)
        da.Fill(ds, "tblRepSemillasProductos")
        'ds.WriteXml(CurDir() & "\repSemillasProductos.xml", XmlWriteMode.WriteSchema)
        'Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        'rep = New repSemillasProductos
        'rep.SetDataSource(ds.Tables("tblSemillasProductos"))
        'Dim RV As New frmReportes(rep, False)
        'RV.Show()
    End Sub

    Public Sub reporteProductores(ByVal proveedor As dbproveedores)
        Dim ds As New DataSet
        command.CommandText = "select b.folio as folio, b.fecha as fecha, b.humedad, as humedad, b.impurezas as impurezas, b.granoquebrado as granoQuebrado," +
            "b.granodanado as granoDañado,b.castigoHumedad as castigoHumedad,b.castigoimpurezas as castigoImpurezas,b.castigoGranoQ as castigoGranoQ, " +
            "b.castigoGranoD as castigoGranoD,b.pesoanalizado as pesoanalizado,b.hora as hora, b.tipoboleta as tipoboleta, prov.nombre as nombreProductor, inv.nombre as nombreProducto, b.peso as peso " +
     "from tblsemillasboletas as b inner join tblproveedores as prov on b.productor=prov.idproveedor inner join tblinventario as inv on b.producto=inv.idinventario " +
     "where b.productor=" + proveedor.ID.ToString() + ";"
        Dim da As New MySqlDataAdapter(command)
        da.Fill(ds, "tblRepSemillasProductores")
        'ds.WriteXml(CurDir() & "\repSemillasProductos.xml", XmlWriteMode.WriteSchema)
        'Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        'rep = New repSemillasProductos
        'rep.SetDataSource(ds.Tables("tblSemillasProductores"))
        'Dim RV As New frmReportes(rep, False)
        'RV.Show()
    End Sub

    Public Sub reporteFechasRango(ByVal desde As String, ByVal hasta As String)
        Dim ds As New DataSet
        command.CommandText = "select b.folio as folio, b.fecha as fecha, prov.nombre as nombreProductor, inv.nombre as nombreProducto, b.peso as peso " +
     "from tblsemillasboletas as b inner join tblproveedores as prov on b.productor=prov.idproveedor inner join tblinventario as inv on b.producto=inv.idinventario " +
     "where b.fecha>='" + desde + "' and b.fecha<='" + hasta + "';"
        Dim da As New MySqlDataAdapter(command)
        da.Fill(ds, "tblRepSemillasRangoFechas")
        ds.WriteXml(CurDir() & "\repSemillasRangoFechas.xml", XmlWriteMode.WriteSchema)
    End Sub

    Public Sub reportefecha(ByVal fecha As String)
        Dim ds As New DataSet
        command.CommandText = "select b.folio as folio, b.fecha as fecha, prov.nombre as nombreProductor, inv.nombre as nombreProducto, b.peso as peso " +
     "from tblsemillasboletas as b inner join tblproveedores as prov on b.productor=prov.idproveedor inner join tblinventario as inv on b.producto=inv.idinventario " +
     "where b.fecha='" + fecha + "';"
        Dim da As New MySqlDataAdapter(command)
        da.Fill(ds, "tblRepSemillasFecha")
        ds.WriteXml(CurDir() & "\repSemillasFecha.xml", XmlWriteMode.WriteSchema)
    End Sub

    Public Sub reporteProductoProductor(Optional ByVal idProducto As Integer = 0, Optional ByVal idProductor As Integer = 0, Optional ByVal desde As String = "", Optional ByVal hasta As String = "", Optional ByVal tipo As String = "", Optional ByVal idCliente As Integer = 0)
        Dim prod As dbInventario
        Dim prov As dbproveedores
        Dim ds As New DataSet
        Dim cli As dbClientes
        command.CommandText = "select b.folio as folio,b.serie, b.fecha as fecha, b.humedad as humedad, b.impurezas as impurezas, b.granoquebrado as granoQuebrado," +
            "b.granodanado as granoDañado,b.castigoHumedad as castigoHumedad,b.castigoimpurezas as castigoImpurezas,b.castigoGranoQ as castigoGranoQ, " +
            "b.castigoGranoD as castigoGranoD,b.pesoanalizado as pesoanalizado,b.hora as hora, b.tipoboleta as tipoboleta,"
        If tipo = "E" Then
            command.CommandText += "prov.nombre as nombreProductor,"
        End If
        If tipo = "S" Then
            command.CommandText += "cli.nombre as nombreProductor,"
        End If
        If tipo = "T" Then
            command.CommandText += "if(tipoboleta='E',ifnull((select nombre from tblproveedores where tblproveedores.idproveedor=b.productor),''),ifnull((select nombre from tblclientes where tblclientes.idcliente=b.idcliente),'')) as nombreProductor,"
        End If
        command.CommandText += "inv.nombre as nombreProducto, b.peso as peso " +
     "from tblsemillasboletas as b inner join "
        If tipo = "S" Then
            command.CommandText += "tblclientes as cli on b.idcliente=cli.idcliente inner join tblinventario as inv on b.producto=inv.idinventario "
        End If
        If tipo = "E" Then
            command.CommandText += "tblproveedores as prov on b.productor=prov.idproveedor inner join tblinventario as inv on b.producto=inv.idinventario "
        End If
        If tipo = "T" Then
            command.CommandText += " tblinventario as inv on b.producto=inv.idinventario "
        End If
        If tipo = "T" And idProducto > 0 Then
            command.CommandText += " where b.producto=" + idProducto.ToString()
        End If
        If idProducto > 0 And idProductor > 0 Then
            command.CommandText += " where b.productor=" + idProductor.ToString() + " and b.producto=" + idProducto.ToString()
        End If
        If tipo = "E" Then
            If idProducto > 0 And idProductor <= 0 Then
                command.CommandText += " where b.producto=" + idProducto.ToString()
            End If
        End If
        If idProducto <= 0 And idProductor > 0 Then
            command.CommandText += " where b.productor=" + idProductor.ToString()
        End If
        If tipo = "S" Then
            If idProducto > 0 And idCliente <= 0 Then
                command.CommandText += " where b.producto=" + idProducto.ToString()
            End If
        End If
        If idProducto <= 0 And idCliente > 0 Then
            command.CommandText += " where b.idcliente=" + idCliente.ToString()
        End If
        If desde <> "" And hasta = "" Then
            command.CommandText += " and b.fecha='" + desde + "';"
        End If
        If tipo <> "T" Then
            command.CommandText += " and b.tipoboleta='" + tipo + "'"
        End If
        If idProducto <= 0 And idProductor <= 0 And idCliente <= 0 Then
            command.CommandText += " where"
        Else
            command.CommandText += " and"
        End If
        If desde <> "" And hasta <> "" Then
            command.CommandText += " b.fecha>='" + desde + "' and b.fecha<='" + hasta + "' "
        End If
        command.CommandText += "order by b.fecha,b.folio"
        Dim da As New MySqlDataAdapter(command)
        da.Fill(ds, "tblRepSemillasProductores")
        'ds.WriteXmlSchema(CurDir() & "\repSemillasProductoProductor.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repSemillasProductoProductor
        rep.SetDataSource(ds)
        sucursal = New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        rep.SetParameterValue("Empresa", sucursal.Nombre)
        rep.SetParameterValue("desde", desde)
        rep.SetParameterValue("hasta", hasta)
        If idProducto > 0 Then
            prod = New dbInventario(idProducto, MySqlcon)
            Dim nombreProducto As String = prod.Nombre
            rep.SetParameterValue("Producto", nombreProducto)
        Else
            rep.SetParameterValue("Producto", "Todos")
        End If
        If idProductor > 0 Then
            prov = New dbproveedores(idProductor, MySqlcon)
            Dim nombreProveedor As String = prov.Nombre
            rep.SetParameterValue("Productor", nombreProveedor)
        Else
            If tipo = "E" Or tipo = "T" Then
                rep.SetParameterValue("Productor", "Todos")
            Else
                rep.SetParameterValue("Productor", "Ninguno")
            End If
        End If
        If idCliente > 0 Then
            cli = New dbClientes(idCliente, MySqlcon)
            Dim nombreCliente As String = cli.Nombre
            rep.SetParameterValue("cliente", nombreCliente)
        Else
            If tipo = "S" Or tipo = "T" Then
                rep.SetParameterValue("cliente", "Todos")
            Else
                rep.SetParameterValue("cliente", "Ninguno")
            End If
        End If
        If tipo = "E" Then
            rep.SetParameterValue("tipoBoletas", "Entrada")
        End If
        If tipo = "S" Then
            rep.SetParameterValue("tipoBoletas", "Salida")
        End If
        If tipo = "T" Then
            rep.SetParameterValue("tipoBoletas", "Todas")
        End If
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub

    Public Sub reporteLiquidaciones(Optional ByVal idProveedor As Integer = 0, Optional ByVal desde As String = "", Optional ByVal hasta As String = "")
        'Dim prod As dbInventario
        Dim prov As dbproveedores
        command.CommandText = "select concat(l.serie,l.folio) as folio, l.fecha as fecha,prov.nombre as productor, l.subtotal,(select ifnull(sum(a.importe),0) from tblsemillasanticipos as a where a.idliquidacion=l.id) as totalAnticipos,(select ifnull(sum(f.importe),0) from tblsemillasanticiposfacturas as f where f.idliquidacion=l.id) as totalFacturas, l.total from tblsemillasliquidacion as l inner join tblproveedores as prov on l.idproveedor=prov.idproveedor where "
        If idProveedor > 0 Then
            command.CommandText += "l.idproveedor=" + idProveedor.ToString() + " and "
        End If
        command.CommandText += "l.fecha>='" + desde + "' and l.fecha<='" + hasta + "';"
        Dim da As New MySqlDataAdapter(command)
        Dim ds As New DataSet
        da.Fill(ds, "tblLiquidaciones")
        ds.WriteXmlSchema(CurDir() & "\repSemillasLiquidaciones.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repSemillasLiquidacion
        rep.SetDataSource(ds)
        sucursal = New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        rep.SetParameterValue("Empresa", sucursal.Nombre)
        If idProveedor > 0 Then
            prov = New dbproveedores(idProveedor, MySqlcon)
            Dim nombreProductor As String = prov.Nombre
            rep.SetParameterValue("Productor", nombreProductor)
        Else
            rep.SetParameterValue("Productor", "Todos")
        End If
        rep.SetParameterValue("desde", desde)
        rep.SetParameterValue("hasta", hasta)
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub

    Public Sub reporteComprobantes(Optional ByVal idCliente As Integer = 0, Optional ByVal desde As String = "", Optional ByVal hasta As String = "")
        Dim cliente As dbClientes
        command.CommandText = "select concat(c.serie,folio) as folio, c.fecha as fecha, c.superficie as superficie, com.referencia as folioCompra,cli.nombre as nombre,concat(c.nombreRepresentante,' ',c.apellidoP,' ',c.apellidoM) as nombreRepLegal,c.sociopersonamoral as socio, " +
"c.volumen as volumen, com.totalapagar as totalFactura from tblsemillascomprobante as c inner join tblclientes as cli on c.idcliente=cli.idcliente inner join tblcompras as com on c.idcompra=com.idcompra where "
        If idCliente > 0 Then
            command.CommandText += "c.idcliente=" + idCliente.ToString() + " and "
        End If
        command.CommandText += "c.fecha>='" + desde + "' and c.fecha<='" + hasta + "';"
        Dim da As New MySqlDataAdapter(command)
        Dim ds As New DataSet
        da.Fill(ds, "tblcomprobantes")
        ds.WriteXmlSchema("repSemillasComprobantes.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repSemillasComprobantes
        rep.SetDataSource(ds)
        sucursal = New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        rep.SetParameterValue("Empresa", sucursal.Nombre)
        rep.SetParameterValue("desde", desde)
        rep.SetParameterValue("hasta", hasta)
        If idCliente > 0 Then
            cliente = New dbClientes(idCliente, MySqlcon)
            Dim nombrecliente As String = cliente.Nombre
            rep.SetParameterValue("Cliente", nombrecliente)
        Else
            rep.SetParameterValue("Cliente", "Todos")
        End If
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub
End Class
