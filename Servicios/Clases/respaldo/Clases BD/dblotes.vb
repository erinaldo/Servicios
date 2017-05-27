Public Class dblotes
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    'Public idInventario As Integer
    Public Lote As String
    Public FechaCaducidad As String
    Public Cantidad As Double
    Public Existencia As Double
    Public IdInventario As Integer
    Public IdDetalleVenta As Integer
    Public IdDetalleCompra As Integer
    Public IdDetalleRemisionV As Integer
    Public idDetalleRemisionC As Integer

    Public IdDetalleLote As Integer
    Public CantidadDetalle As Integer
    Public Idlote As Integer
    'Public FechaGarantia As String
    'Public idCompra As Integer
    'Public idVenta As Integer
    'Public idServicio As Integer
    'Public idRemision As Integer
    'Public idMovimiento As Integer
    'Public IdRemisionC As Integer
    'Public IdCotizacion As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        FechaCaducidad = ""
        Lote = ""
        Cantidad = 0
        IdDetalleCompra = 0
        IdDetalleVenta = 0
        idDetalleRemisionC = 0
        IdDetalleRemisionV = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblinventariolotes where idlote=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            FechaCaducidad = DReader("fechacaducidad")
            Lote = DReader("lote")
            Cantidad = DReader("cantidad")
            Existencia = DReader("cantidad")
            IdInventario = DReader("idinventario")
        End If
        DReader.Close()
    End Sub

    Public Sub LlenaDatosDetalle(ByVal pid As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        IdDetalleLote = pid
        If IdDetalleCompra <> 0 Then Comm.CommandText = "select tblinventariolotes.idlote,tblinventariolotes.fechacaducidad,tblinventariolotes.lote,tblcompraslotes.cantidad,tblinventariolotes.cantidad as ext from tblcompraslotes inner join tblinventariolotes on tblcompraslotes.idlote=tblinventariolotes.idlote where id=" + IdDetalleLote.ToString
        If idDetalleRemisionC <> 0 Then Comm.CommandText = "select tblinventariolotes.idlote,tblinventariolotes.fechacaducidad,tblinventariolotes.lote,tblcomprasremisioneslotes.cantidad,tblinventariolotes.cantidad as ext from tblcomprasremisioneslotes inner join tblinventariolotes on tblcomprasremisioneslotes.idlote=tblinventariolotes.idlote where id=" + IdDetalleLote.ToString
        If IdDetalleVenta <> 0 Then Comm.CommandText = "select tblinventariolotes.idlote,tblinventariolotes.fechacaducidad,tblinventariolotes.lote,tblventaslotes.cantidad,tblinventariolotes.cantidad as ext from tblventaslotes inner join tblinventariolotes on tblventaslotes.idlote=tblinventariolotes.idlote where id=" + IdDetalleLote.ToString
        If IdDetalleRemisionV <> 0 Then Comm.CommandText = "select tblinventariolotes.idlote,tblinventariolotes.fechacaducidad,tblinventariolotes.lote,tblventasremisioneslotes.cantidad,tblinventariolotes.cantidad as ext from tblventasremisioneslotes inner join tblinventariolotes on tblventasremisioneslotes.idlote=tblinventariolotes.idlote where id=" + IdDetalleLote.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            FechaCaducidad = DReader("fechacaducidad")
            Lote = DReader("lote")
            Cantidad = DReader("cantidad")
            ID = DReader("idlote")
            Existencia = DReader("ext")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pLote As String, ByVal pFechaCaducidad As String, ByVal pCantidad As Double, ByVal pIdInventario As Integer)
        FechaCaducidad = pFechaCaducidad
        Lote = pLote
        Cantidad = pCantidad
        IdInventario = pIdInventario
        Comm.CommandText = "insert into tblinventariolotes(lote,fechacaducidad,cantidad,idinventario) values('" + Replace(Lote, "'", "''") + "','" + FechaCaducidad + "',0," + IdInventario.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull((select max(idlote) from tblinventariolotes),0)"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pLote As String, ByVal pFechaCaducidad As String)
        ID = pID
        FechaCaducidad = pFechaCaducidad
        Lote = pLote
        Comm.CommandText = "update tblinventariolotes set lote='" + Replace(Lote, "'", "''") + "',fechacaducidad='" + FechaCaducidad + "' where idlote=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub AsignaLoteADocumento(ByVal pidLote As Integer, ByVal pidDetalle As Integer, ByVal pCantidad As Double, ByVal pTabla As String)
        Dim iId As Integer

        Comm.CommandText = "select count(id) from " + pTabla + " where iddetalle=" + pidDetalle.ToString + " and idlote=" + pidLote.ToString
        iId = Comm.ExecuteScalar
        If iId = 0 Then
            Comm.CommandText = "insert into " + pTabla + "(idlote,iddetalle,cantidad,surtido) values(" + pidLote.ToString + "," + pidDetalle.ToString + "," + pCantidad.ToString + ",0)"
            Comm.ExecuteNonQuery()
        Else
            Comm.CommandText = "update " + pTabla + " set cantidad=" + pCantidad.ToString + " where idlote=" + pidLote.ToString + " and iddetalle=" + pidDetalle.ToString
            Comm.ExecuteNonQuery()
        End If
        'Comm.CommandText = "update tblinventarioseries set idventa=" + idVenta.ToString + " where idserie=" + ID.ToString
        'Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitaLoteADocumento(ByVal pidLote As Integer, ByVal pidDetalle As Integer, ByVal pTabla As String)
        Comm.CommandText = "delete from " + pTabla + " where idlote=" + pidLote.ToString + " and iddetalle=" + pidDetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblinventariolotes where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub EliminarDetalle(ByVal pID As Integer)
        If IdDetalleCompra <> 0 Then Comm.CommandText = "delete from tblcompraslotes where id=" + pID.ToString
        If idDetalleRemisionC <> 0 Then Comm.CommandText = "delete from tblcomprasremisioneslotes where id=" + pID.ToString
        If IdDetalleVenta <> 0 Then Comm.CommandText = "delete from tblventaslotes where id=" + pID.ToString
        If IdDetalleRemisionV <> 0 Then Comm.CommandText = "delete from tblventasremisioneslotes where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function Consulta(ByVal pIdInventario As Integer, ByVal pNoserie As String) As DataView
        Dim DS As New DataSet
        If IdDetalleCompra <> 0 Then
            Comm.CommandText = "select tblcompraslotes.id,tblinventariolotes.idlote,tblinventariolotes.lote,tblinventariolotes.fechacaducidad,tblcompraslotes.cantidad  from tblinventariolotes inner join tblcompraslotes on tblinventariolotes.idlote=tblcompraslotes.idlote inner join tblcomprasdetalles on tblcomprasdetalles.iddetalle=tblcompraslotes.iddetalle where lote like '%" + pNoserie + "%' and tblinventariolotes.idinventario=" + pIdInventario.ToString + " and tblcompraslotes.iddetalle=" + IdDetalleCompra.ToString
        End If
        If idDetalleRemisionC <> 0 Then
            Comm.CommandText = "select tblcomprasremisioneslotes.id,tblinventariolotes.idlote,tblinventariolotes.lote,tblinventariolotes.fechacaducidad,tblcomprasremisioneslotes.cantidad  from tblinventariolotes inner join tblcomprasremisioneslotes on tblinventariolotes.idlote=tblcomprasremisioneslotes.idlote inner join tblcomprasremisionesdetalles on tblcomprasremisionesdetalles.iddetalle=tblcomprasremisioneslotes.iddetalle where lote like '%" + pNoserie + "%' and tblinventariolotes.idinventario=" + pIdInventario.ToString + " and tblcomprasremisioneslotes.iddetalle=" + idDetalleRemisionC.ToString
        End If
        If IdDetalleVenta <> 0 Then
            Comm.CommandText = "select tblventaslotes.id,tblinventariolotes.idlote,tblinventariolotes.lote,tblinventariolotes.fechacaducidad,tblventaslotes.cantidad  from tblinventariolotes inner join tblventaslotes on tblinventariolotes.idlote=tblventaslotes.idlote inner join tblventasinventario on tblventasinventario.idventasinventario=tblventaslotes.iddetalle where lote like '%" + pNoserie + "%' and tblinventariolotes.idinventario=" + pIdInventario.ToString + " and tblventaslotes.iddetalle=" + IdDetalleVenta.ToString
        End If
        If IdDetalleRemisionV <> 0 Then
            Comm.CommandText = "select tblventasremisioneslotes.id,tblinventariolotes.idlote,tblinventariolotes.lote,tblinventariolotes.fechacaducidad,tblventasremisioneslotes.cantidad  from tblinventariolotes inner join tblventasremisioneslotes on tblinventariolotes.idlote=tblventasremisioneslotes.idlote inner join tblventasremisionesinventario on tblventasremisionesinventario.iddetalle=tblventasremisioneslotes.iddetalle where lote like '%" + pNoserie + "%' and tblinventariolotes.idinventario=" + pIdInventario.ToString + " and tblventasremisioneslotes.iddetalle=" + IdDetalleRemisionV.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventariolotes")
        Return DS.Tables("tblinventariolotes").DefaultView
    End Function
    Public Function ConsultaLotes(ByVal pIdInventario As Integer, ByVal pNoSerie As String, pSinInventgarioNo As Boolean) As DataView
        Dim DS As New DataSet
        'If IdDetalleRemisionV <> 0 Then
        Comm.CommandText = "select idlote,lote,fechacaducidad,cantidad  from tblinventariolotes where lote like '%" + pNoSerie + "%' and tblinventariolotes.idinventario=" + pIdInventario.ToString
        If pSinInventgarioNo Then
            Comm.CommandText += " and tblinventariolotes.cantidad>0"
        End If
        Comm.CommandText += " order by fechacaducidad,cantidad"
        'End If 
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventariolotes")
        Return DS.Tables("tblinventariolotes").DefaultView
    End Function
    Public Function ChecaLoteRepetido(ByVal pNoSerie As String, ByVal pFechaCaducidad As String, ByVal pIdInventario As Integer) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select ifnull((select idlote from tblinventariolotes where lote='" + Replace(pNoSerie, "'", "''") + "' and fechacaducidad='" + pFechaCaducidad + "' and idinventario=" + pIdInventario.ToString + "),0)"
        ID = Comm.ExecuteScalar
        Return ID
    End Function
  
    Public Sub AgregaCantidadALote(ByVal pLote As String, ByVal pFechadeCaducidad As String, ByVal pCantidad As Double, ByVal pIdinventario As Integer)
        Comm.CommandText = "update tblinventariolotes set cantidad=cantidad+" + pCantidad.ToString + " where lote='" + Replace(pLote, "'", "''") + "' and fechacaducidad='" + pFechadeCaducidad + "' and idinventario=" + pIdinventario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function CantidadAsignados() As Double
        Dim Res As Double
        If IdDetalleCompra Then Comm.CommandText = "select ifnull((select sum(cantidad) from tblcompraslotes where iddetalle=" + IdDetalleCompra.ToString + "),0)"
        If idDetalleRemisionC Then Comm.CommandText = "select ifnull((select sum(cantidad) from tblcomprasremisioneslotes where iddetalle=" + idDetalleRemisionC.ToString + "),0)"
        If IdDetalleVenta Then Comm.CommandText = "select ifnull((select sum(cantidad) from tblventaslotes where iddetalle=" + IdDetalleVenta.ToString + "),0)"
        If IdDetalleRemisionV Then Comm.CommandText = "select ifnull((select sum(cantidad) from tblventasremisioneslotes where iddetalle=" + IdDetalleRemisionV.ToString + "),0)"
        Res = Comm.ExecuteScalar
        Return Res
    End Function

    Public Function ConsultaMovimientosLotes(pId As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "delete from tblinventariolotesconsulta where idlote=" + pId.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblinventariolotesconsulta(idlote,lote,caducidad,iddocumento,folio,fecha,tipodoc,cantidad) select l.idlote,l.lote,l.fechacaducidad,c.idcompra,concat(c.serie,lpad(convert(c.folioi using utf8),4,'0'),' ',c.referencia),c.fecha,'COMPRA',cl.cantidad from tblinventariolotes as l inner join  tblcompraslotes cl on l.idlote=cl.idlote inner join tblcomprasdetalles on cl.iddetalle=tblcomprasdetalles.iddetalle inner join tblcompras c on c.idcompra=tblcomprasdetalles.idcompra where l.idlote=" + pId.ToString + " and c.estado=3;"
        Comm.CommandText += "insert into tblinventariolotesconsulta(idlote,lote,caducidad,iddocumento,folio,fecha,tipodoc,cantidad) select l.idlote,l.lote,l.fechacaducidad,c.idremision,concat(c.serie,lpad(convert(c.folioi using utf8),4,'0'),' ',c.folio),c.fecha,'REM. COMPRA',cl.cantidad from tblinventariolotes as l inner join  tblcomprasremisioneslotes cl on l.idlote=cl.idlote inner join tblcomprasremisionesdetalles on cl.iddetalle=tblcomprasremisionesdetalles.iddetalle inner join tblcomprasremisiones c on c.idremision=tblcomprasremisionesdetalles.idremision where l.idlote=" + pId.ToString + " and c.estado=3;"
        Comm.CommandText += "insert into tblinventariolotesconsulta(idlote,lote,caducidad,iddocumento,folio,fecha,tipodoc,cantidad) select l.idlote,l.lote,l.fechacaducidad,c.idventa,concat(c.serie,lpad(convert(c.folio using utf8),4,'0')),c.fecha,'FACTURA',cl.cantidad from tblinventariolotes as l inner join  tblventaslotes cl on l.idlote=cl.idlote inner join tblventasinventario on cl.iddetalle=tblventasinventario.idventasinventario inner join tblventas c on c.idventa=tblventasinventario.idventa where l.idlote=" + pId.ToString + " and c.estado=3;"
        Comm.CommandText += "insert into tblinventariolotesconsulta(idlote,lote,caducidad,iddocumento,folio,fecha,tipodoc,cantidad) select l.idlote,l.lote,l.fechacaducidad,c.idremision,concat(c.serie,lpad(convert(c.folio using utf8),4,'0')),c.fecha,'REMISIÓN',cl.cantidad from tblinventariolotes as l inner join  tblventasremisioneslotes cl on l.idlote=cl.idlote inner join tblventasremisionesinventario on cl.iddetalle=tblventasremisionesinventario.iddetalle inner join tblventasremisiones c on c.idremision=tblventasremisionesinventario.idremision where l.idlote=" + pId.ToString + " and c.estado=3;"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select iddocumento,tipodoc,fecha,folio,cantidad from tblinventariolotesconsulta where idlote=" + pId.ToString + " order by fecha,folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbllotes")
        'DS.WriteXmlSchema("tblclientesana.xml")
        Return DS.Tables("tbllotes").DefaultView
    End Function
    Public Sub reporteComprasLotesFactura(ByVal idSucursal As Integer, ByVal proveedor As Integer, ByVal idInventario As Integer, ByVal idClasificacion As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer, ByVal desde As String, ByVal hasta As String)
        Comm.CommandText = "select c.fecha,prov.nombre as nombreProveedor,i.nombre,i.clave,l.lote,concat(c.serie,c.folioi) as folio, l.fechacaducidad,cl.cantidad from tblcompraslotes as cl "
        Comm.CommandText += "inner join tblinventariolotes as l on cl.idlote=l.idlote inner join tblcomprasdetalles as cd on cl.iddetalle=cd.iddetalle "
        Comm.CommandText += "inner join tblcompras as c on cd.idcompra=c.idcompra inner join tblinventario as i on l.idinventario=i.idinventario "
        Comm.CommandText += "inner join tblproveedores as prov on c.idproveedor=prov.idproveedor where c.estado=3 "
        If idInventario > 0 Then
            Comm.CommandText += " and l.idinventario=" + idInventario.ToString() + " "
        End If
        Comm.CommandText += " and c.fecha>='" + desde + "' and c.fecha<='" + hasta + "' "
        If idSucursal > 0 Then
            Comm.CommandText += " and c.idsucursal=" + idSucursal.ToString()
        End If
        If proveedor > 0 Then
            Comm.CommandText += " and c.idproveedor=" + proveedor.ToString()
        End If
        If idClasificacion > 0 Then
            Comm.CommandText += " and i.idclasificacion=" + idClasificacion.ToString()
        End If
        If idClas2 > 0 Then
            Comm.CommandText += " and i.idclasificacion2=" + idClas2.ToString()
        End If
        If idClas3 > 0 Then
            Comm.CommandText += " and i.idclasificacion3=" + idClas3.ToString()
        End If
        Comm.CommandText += " order by i.idinventario,l.lote,c.fecha,c.serie,c.folioi;"
        Dim ds As New DataSet
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tbllotes")
        ds.WriteXmlSchema("tblComprasLotes.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repComprasLotes
        rep.SetDataSource(ds)
        Dim suc As New dbSucursales(idSucursal, MySqlcon)
        If proveedor > 0 Then
            Dim p As New dbproveedores(proveedor, MySqlcon)
            rep.SetParameterValue("proveedor", p.Nombre)
        Else
            rep.SetParameterValue("proveedor", "Todos")
        End If
        rep.SetParameterValue("nombreEmpresa", suc.Nombre)
        rep.SetParameterValue("desde", desde)
        rep.SetParameterValue("hasta", hasta)
        rep.SetParameterValue("tipoReporte", "Facturas")
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub



    Public Sub reporteComprasLotesRemisiones(ByVal idSucursal As Integer, ByVal proveedor As Integer, ByVal idInventario As Integer, ByVal idClasificacion As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer, ByVal desde As String, ByVal hasta As String)
        Comm.CommandText = "select c.fecha,prov.nombre as nombreProveedor,i.nombre,i.clave,l.lote,concat(c.serie,c.folioi) as folio, l.fechacaducidad,cl.cantidad from tblcomprasremisioneslotes as cl "
        Comm.CommandText += "inner join tblinventariolotes as l on cl.idlote=l.idlote inner join tblcomprasremisionesdetalles as cd on cl.iddetalle=cd.iddetalle "
        Comm.CommandText += "inner join tblcomprasremisiones as c on cd.idremision=c.idremision inner join tblinventario as i on l.idinventario=i.idinventario "
        Comm.CommandText += "inner join tblproveedores as prov on c.idproveedor=prov.idproveedor where c.estado=3 "
        If idInventario > 0 Then
            Comm.CommandText += " and l.idinventario=" + idInventario.ToString() + " "
        End If
        Comm.CommandText += " and c.fecha>='" + desde + "' and c.fecha<='" + hasta + "' "
        If idSucursal > 0 Then
            Comm.CommandText += " and c.idsucursal=" + idSucursal.ToString()
        End If
        If proveedor > 0 Then
            Comm.CommandText += " and c.idproveedor=" + proveedor.ToString()
        End If
        If idClasificacion > 0 Then
            Comm.CommandText += " and i.idclasificacion=" + idClasificacion.ToString()
        End If
        If idClas2 > 0 Then
            Comm.CommandText += " and i.idclasificacion2=" + idClas2.ToString()
        End If
        If idClas3 > 0 Then
            Comm.CommandText += " and i.idclasificacion3=" + idClas3.ToString()
        End If
        Comm.CommandText += " order by i.idinventario,l.lote,c.fecha,c.serie,c.folioi;"
        Dim ds As New DataSet
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tbllotes")
        'ds.WriteXmlSchema("tblComprasLotes.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repComprasLotes
        rep.SetDataSource(ds)
        Dim suc As New dbSucursales(idSucursal, MySqlcon)
        If proveedor > 0 Then
            Dim p As New dbproveedores(proveedor, MySqlcon)
            rep.SetParameterValue("proveedor", p.Nombre)
        Else
            rep.SetParameterValue("proveedor", "Todos")
        End If
        rep.SetParameterValue("nombreEmpresa", suc.Nombre)
        rep.SetParameterValue("desde", desde)
        rep.SetParameterValue("hasta", hasta)
        rep.SetParameterValue("tipoReporte", "Remisiones")
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub




    Public Sub reporteVentasLotesFactura(ByVal idSucursal As Integer, ByVal cliente As Integer, ByVal idInventario As Integer, ByVal idClasificacion As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer, ByVal desde As String, ByVal hasta As String, ByVal idVendedor As Integer)
        Dim DS As New DataSet

        Comm.CommandText = "select v.fecha,concat(v.serie,v.folio) as folio,vl.cantidad,l.fechacaducidad,l.lote,cli.nombre as cliente,i.nombre as producto,i.clave,vendedor.nombre as nombreVendedor from tblventaslotes as vl inner join tblinventariolotes as l on l.idlote=vl.idlote inner join tblventasinventario as vi on vl.iddetalle=vi.idventasinventario inner join tblventas as v on vi.idventa=v.idventa inner join tblclientes as cli on v.idcliente=cli.idcliente inner join tblinventario as i on vi.idinventario=i.idinventario inner join tblvendedores as vendedor on v.idvendedor=vendedor.idvendedor "
        Comm.CommandText += "where v.fecha>='" + desde + "' and v.fecha<='" + hasta + "' and v.estado=3 "
        If idInventario > 0 Then
            Comm.CommandText += "and l.idinventario=" + idInventario.ToString()
        End If
        If idSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + idSucursal.ToString()
        End If
        If cliente > 0 Then
            Comm.CommandText += " and v.idcliente=" + cliente.ToString()
        End If
        If idClasificacion > 0 Then
            Comm.CommandText += " and i.idclasificacion=" + idClasificacion.ToString()
        End If
        If idClas2 > 0 Then
            Comm.CommandText += " and i.idclasificacion2=" + idClas2.ToString()
        End If
        If idClas3 > 0 Then
            Comm.CommandText += " and i.idclasificacion3=" + idClas3.ToString()
        End If
        If idVendedor > 0 Then
            Comm.CommandText += " and v.idVendedor=" + idVendedor.ToString()
        End If
        Comm.CommandText += " order by i.idinventario,l.lote,v.fecha,v.serie,v.folio"
        'Dim ds As New DataSet
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tbllotes")
        'ds.WriteXmlSchema("tblVentasLotes.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repVentasLotes
        rep.SetDataSource(ds)
        Dim suc As New dbSucursales(idSucursal, MySqlcon)
        If idVendedor > 0 Then
            Dim v As New dbVendedores(idVendedor, MySqlcon)
            rep.SetParameterValue("vendedor", v.Nombre)
        Else
            rep.SetParameterValue("vendedor", "Todos")
        End If
        If cliente > 0 Then
            Dim c As New dbClientes(cliente, MySqlcon)
            rep.SetParameterValue("cliente", c.Nombre)
        Else
            rep.SetParameterValue("cliente", "Todos")
        End If
        rep.SetParameterValue("nombreEmpresa", suc.Nombre)
        rep.SetParameterValue("desde", desde)
        rep.SetParameterValue("hasta", hasta)
        rep.SetParameterValue("tipoReporte", "Facturas")
        rep.SetParameterValue("dequees", "Reporte de ventas por lotes facturas")
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub




    Public Sub reporteVentasLotesRemisiones(ByVal idSucursal As Integer, ByVal cliente As Integer, ByVal idInventario As Integer, ByVal idClasificacion As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer, ByVal desde As String, ByVal hasta As String, ByVal idVendedor As Integer)
        Dim DS As New DataSet

        Comm.CommandText = "select v.fecha,concat(v.serie,v.folio) as folio,vl.cantidad,l.fechacaducidad,l.lote,cli.nombre as cliente,i.nombre as producto,i.clave,vendedor.nombre as nombreVendedor from tblventasremisioneslotes as vl inner join tblinventariolotes as l on l.idlote=vl.idlote inner join tblventasremisionesinventario as vi on vl.iddetalle=vi.iddetalle inner join tblventasremisiones as v on vi.idremision=v.idremision inner join tblclientes as cli on v.idcliente=cli.idcliente inner join tblinventario as i on vi.idinventario=i.idinventario inner join tblvendedores as vendedor on v.idvendedor=vendedor.idvendedor "
        Comm.CommandText += "where v.fecha>='" + desde + "' and v.fecha<='" + hasta + "' and v.estado=3 "
        If idInventario > 0 Then
            Comm.CommandText += "and l.idinventario=" + idInventario.ToString()
        End If
        If idSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + idSucursal.ToString()
        End If
        If cliente > 0 Then
            Comm.CommandText += " and v.idcliente=" + cliente.ToString()
        End If
        If idClasificacion > 0 Then
            Comm.CommandText += " and i.idclasificacion=" + idClasificacion.ToString()
        End If
        If idClas2 > 0 Then
            Comm.CommandText += " and i.idclasificacion2=" + idClas2.ToString()
        End If
        If idClas3 > 0 Then
            Comm.CommandText += " and i.idclasificacion3=" + idClas3.ToString()
        End If
        If idVendedor > 0 Then
            Comm.CommandText += " and v.idVendedor=" + idVendedor.ToString()
        End If
        Comm.CommandText += " order by i.idinventario,l.lote,v.fecha,v.serie,v.folio"
        'Dim ds As New DataSet
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(DS, "tbllotes")
        'DS.WriteXmlSchema("tblVentasLotes.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repVentasLotes
        rep.SetDataSource(DS)
        Dim suc As New dbSucursales(idSucursal, MySqlcon)
        If idVendedor > 0 Then
            Dim v As New dbVendedores(idVendedor, MySqlcon)
            rep.SetParameterValue("vendedor", v.Nombre)
        Else
            rep.SetParameterValue("vendedor", "Todos")
        End If
        If cliente > 0 Then
            Dim c As New dbClientes(cliente, MySqlcon)
            rep.SetParameterValue("cliente", c.Nombre)
        Else
            rep.SetParameterValue("cliente", "Todos")
        End If
        rep.SetParameterValue("nombreEmpresa", suc.Nombre)
        rep.SetParameterValue("desde", desde)
        rep.SetParameterValue("hasta", hasta)
        rep.SetParameterValue("tipoReporte", "Remisiones")
        rep.SetParameterValue("dequees", "Reporte de ventas por lotes remisiones")
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub
    Public Sub reporteInventarioLotes(ByVal idInventario As Integer, ByVal idClas As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer, ByVal existencia As Boolean, pDescon As Byte)
        Dim ds As New DataSet
        Dim clasificacion As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        Dim cla As String = ""
        Comm.CommandText = "select i.clave,i.nombre,l.lote,l.fechacaducidad,l.cantidad from tblinventario as i inner join tblinventariolotes as l on i.idinventario=l.idinventario"
        If existencia Then
            Comm.CommandText += " where l.cantidad>0 "
        Else
            Comm.CommandText += " where l.cantidad>=0 "
        End If
        If idInventario > 0 Then
            Comm.CommandText += " and i.idinventario=" + idInventario.ToString()
        End If
        If idClas > 0 Then
            Comm.CommandText += " and i.idclasificacion=" + idClas.ToString()
            clasificacion = New dbInventarioClasificaciones(idClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            cla += clasificacion.Nombre + " - "
        End If
        If idClas2 > 0 Then
            Comm.CommandText += " and i.idclasificacion2=" + idClas2.ToString()
            clasificacion = New dbInventarioClasificaciones(idClas2, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            cla += clasificacion.Nombre + " - "
        End If
        If idClas3 > 0 Then
            Comm.CommandText += " and i.idclasificacion3=" + idClas3.ToString()
            clasificacion = New dbInventarioClasificaciones(idClas3, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            cla += clasificacion.Nombre
        End If
        If pDescon = 0 Then
            Comm.CommandText += " and i.descontinuado=0"
        End If
        Comm.CommandText += " order by i.idinventario,l.lote"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tblinventario")
        'ds.WriteXmlSchema("tblInventarioLotes.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repInventarioLotes
        rep.SetDataSource(ds)
        rep.SetParameterValue("nombreEmpresa", GlobalNombreEmpresa)
        If cla <> "" Then
            rep.SetParameterValue("clasificacion", cla)
        Else
            rep.SetParameterValue("clasificacion", "Todas")
        End If
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub
End Class
