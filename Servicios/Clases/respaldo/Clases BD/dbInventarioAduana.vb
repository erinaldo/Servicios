Public Class dbInventarioAduana
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    'Public idInventario As Integer
    'Public Lote As String
    'Public fecha As String
    Public Numero As String
    Public Fecha As String
    Public Aduana As String
    Public Cantidad As Double
    Public IdAduana As Integer
    Public IdInventario As Integer
    Public IdDetalleVenta As Integer
    Public IdDetalleCompra As Integer
    Public IdDetalleRemisionV As Integer
    Public idDetalleRemisionC As Integer
    Public Existencia As Double
    Public IdDetalleAduana As Integer
    Public CantidadDetalle As Integer
    'Public Idaduana As Integer
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
        Fecha = ""
        Aduana = ""
        Numero = ""
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
        Comm.CommandText = "select * from tblinventarioaduana where idaduana=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Fecha = DReader("fecha")
            Aduana = DReader("aduana")
            Cantidad = DReader("cantidad")
            Existencia = DReader("cantidad")
            Numero = DReader("numero")
            IdInventario = DReader("idinventario")
        End If
        DReader.Close()
    End Sub

    Public Sub LlenaDatosDetalle(ByVal pid As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        IdDetalleAduana = pid
        If IdDetalleCompra <> 0 Then Comm.CommandText = "select tblinventarioaduana.idaduana,tblinventarioaduana.fecha,tblinventarioaduana.aduana,tblinventarioaduana.numero,tblcomprasaduana.cantidad,tblinventarioaduana.cantidad as ext from tblcomprasaduana inner join tblinventarioaduana on tblcomprasaduana.idaduana=tblinventarioaduana.idaduana where id=" + IdDetalleAduana.ToString
        If idDetalleRemisionC <> 0 Then Comm.CommandText = "select tblinventarioaduana.idaduana,tblinventarioaduana.fecha,tblinventarioaduana.aduana,tblinventarioaduana.numero,tblcomprasremisionesaduana.cantidad,tblinventarioaduana.cantidad as ext from tblcomprasremisionesaduana inner join tblinventarioaduana on tblcomprasremisionesaduana.idaduana=tblinventarioaduana.idaduana where id=" + IdDetalleAduana.ToString
        If IdDetalleVenta <> 0 Then Comm.CommandText = "select tblinventarioaduana.idaduana,tblinventarioaduana.fecha,tblinventarioaduana.aduana,tblinventarioaduana.numero,tblventasaduanan.cantidad,tblinventarioaduana.cantidad as ext from tblventasaduanan inner join tblinventarioaduana on tblventasaduanan.idaduana=tblinventarioaduana.idaduana where id=" + IdDetalleAduana.ToString
        If IdDetalleRemisionV <> 0 Then Comm.CommandText = "select tblinventarioaduana.idaduana,tblinventarioaduana.fecha,tblinventarioaduana.aduana,tblinventarioaduana.numero,tblventasremisionesaduana.cantidad,tblinventarioaduana.cantidad as ext from tblventasremisionesaduana inner join tblinventarioaduana on tblventasremisionesaduana.idaduana=tblinventarioaduana.idaduana where id=" + IdDetalleAduana.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            fecha = DReader("fecha")
            Aduana = DReader("aduana")
            Numero = DReader("numero")
            Cantidad = DReader("cantidad")
            Existencia = DReader("ext")
            ID = DReader("idaduana")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pAduana As String, ByVal pfecha As String, ByVal pCantidad As Double, ByVal pIdInventario As Integer, pNumero As String)
        Fecha = pfecha
        Aduana = pAduana
        Numero = pNumero
        Cantidad = pCantidad
        IdInventario = pIdInventario
        Comm.CommandText = "insert into tblinventarioaduana(aduana,fecha,numero,cantidad,idinventario) values('" + Replace(Aduana.Trim, "'", "''") + "','" + Fecha + "','" + Replace(Numero.Trim, "'", "''") + "',0," + IdInventario.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull((select max(idaduana) from tblinventarioaduana),0)"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pAduana As String, ByVal pfecha As String, pNumero As String)
        ID = pID
        Fecha = pfecha
        Aduana = pAduana
        Numero = pNumero
        Comm.CommandText = "update tblinventarioaduana set aduana='" + Replace(Aduana.Trim, "'", "''") + "',fecha='" + Fecha + "',numero='" + Replace(Numero.Trim, "'", "''") + "' where idaduana=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub AsignaAduanaADocumento(ByVal pidaduana As Integer, ByVal pidDetalle As Integer, ByVal pCantidad As Double, ByVal pTabla As String)
        Dim iId As Integer

        Comm.CommandText = "select count(id) from " + pTabla + " where iddetalle=" + pidDetalle.ToString + " and idaduana=" + pidaduana.ToString
        iId = Comm.ExecuteScalar
        If iId = 0 Then
            Comm.CommandText = "insert into " + pTabla + "(idaduana,iddetalle,cantidad,surtido) values(" + pidaduana.ToString + "," + pidDetalle.ToString + "," + pCantidad.ToString + ",0)"
            Comm.ExecuteNonQuery()
        Else
            Comm.CommandText = "update " + pTabla + " set cantidad=" + pCantidad.ToString + " where idaduana=" + pidaduana.ToString + " and iddetalle=" + pidDetalle.ToString
            Comm.ExecuteNonQuery()
        End If
        'Comm.CommandText = "update tblinventarioseries set idventa=" + idVenta.ToString + " where idserie=" + ID.ToString
        'Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitaAduanaADocumento(ByVal pidaduana As Integer, ByVal pidDetalle As Integer, ByVal pTabla As String)
        Comm.CommandText = "delete from " + pTabla + " where idaduana=" + pidaduana.ToString + " and iddetalle=" + pidDetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblinventarioaduana where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub EliminarDetalle(ByVal pID As Integer)
        If IdDetalleCompra <> 0 Then Comm.CommandText = "delete from tblcomprasaduana where id=" + pID.ToString
        If idDetalleRemisionC <> 0 Then Comm.CommandText = "delete from tblcomprasremisionesaduana where id=" + pID.ToString
        If IdDetalleVenta <> 0 Then Comm.CommandText = "delete from tblventasaduanan where id=" + pID.ToString
        If IdDetalleRemisionV <> 0 Then Comm.CommandText = "delete from tblventasremisionesaduana where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function Consulta(ByVal pIdInventario As Integer, ByVal pNoserie As String) As DataView
        Dim DS As New DataSet
        If IdDetalleCompra <> 0 Then
            Comm.CommandText = "select tblcomprasaduana.id,tblinventarioaduana.idaduana,tblinventarioaduana.aduana,tblinventarioaduana.numero,tblinventarioaduana.fecha,tblcomprasaduana.cantidad  from tblinventarioaduana inner join tblcomprasaduana on tblinventarioaduana.idaduana=tblcomprasaduana.idaduana inner join tblcomprasdetalles on tblcomprasdetalles.iddetalle=tblcomprasaduana.iddetalle where concat(aduana,numero) like '%" + pNoserie + "%' and tblinventarioaduana.idinventario=" + pIdInventario.ToString + " and tblcomprasaduana.iddetalle=" + IdDetalleCompra.ToString
        End If
        If idDetalleRemisionC <> 0 Then
            Comm.CommandText = "select tblcomprasremisionesaduana.id,tblinventarioaduana.idaduana,tblinventarioaduana.aduana,tblinventarioaduana.numero,tblinventarioaduana.fecha,tblcomprasremisionesaduana.cantidad  from tblinventarioaduana inner join tblcomprasremisionesaduana on tblinventarioaduana.idaduana=tblcomprasremisionesaduana.idaduana inner join tblcomprasremisionesdetalles on tblcomprasremisionesdetalles.iddetalle=tblcomprasremisionesaduana.iddetalle where concat(aduana,numero) like '%" + pNoserie + "%' and tblinventarioaduana.idinventario=" + pIdInventario.ToString + " and tblcomprasremisionesaduana.iddetalle=" + idDetalleRemisionC.ToString
        End If
        If IdDetalleVenta <> 0 Then
            Comm.CommandText = "select tblventasaduanan.id,tblinventarioaduana.idaduana,tblinventarioaduana.aduana,tblinventarioaduana.numero,tblinventarioaduana.fecha,tblventasaduanan.cantidad  from tblinventarioaduana inner join tblventasaduanan on tblinventarioaduana.idaduana=tblventasaduanan.idaduana inner join tblventasinventario on tblventasinventario.idventasinventario=tblventasaduanan.iddetalle where concat(aduana,numero) like '%" + pNoserie + "%' and tblinventarioaduana.idinventario=" + pIdInventario.ToString + " and tblventasaduanan.iddetalle=" + IdDetalleVenta.ToString
        End If
        If IdDetalleRemisionV <> 0 Then
            Comm.CommandText = "select tblventasremisionesaduana.id,tblinventarioaduana.idaduana,tblinventarioaduana.aduana,tblinventarioaduana.numero,tblinventarioaduana.fecha,tblventasremisionesaduana.cantidad  from tblinventarioaduana inner join tblventasremisionesaduana on tblinventarioaduana.idaduana=tblventasremisionesaduana.idaduana inner join tblventasremisionesinventario on tblventasremisionesinventario.iddetalle=tblventasremisionesaduana.iddetalle where concat(aduana,numero) like '%" + pNoserie + "%' and tblinventarioaduana.idinventario=" + pIdInventario.ToString + " and tblventasremisionesaduana.iddetalle=" + IdDetalleRemisionV.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioaduana")
        Return DS.Tables("tblinventarioaduana").DefaultView
    End Function
    Public Function ConsultaAduanas(ByVal pIdInventario As Integer, ByVal pNoSerie As String, pSinInventarioNo As Boolean) As DataView
        Dim DS As New DataSet
        'If IdDetalleRemisionV <> 0 Then
        Comm.CommandText = "select idaduana,aduana,numero,fecha,cantidad  from tblinventarioaduana where concat(aduana,numero) like '%" + pNoSerie + "%' and tblinventarioaduana.idinventario=" + pIdInventario.ToString
        If pSinInventarioNo Then
            Comm.CommandText += " and tblinventarioaduana.cantidad>0"
        End If
        Comm.CommandText += " order by fecha,cantidad"
        'End If 
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioaduana")
        Return DS.Tables("tblinventarioaduana").DefaultView
    End Function
    Public Function ChecaAduanaRepetida(ByVal pNoSerie As String, ByVal pfecha As String, ByVal pIdInventario As Integer) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select ifnull((select idaduana from tblinventarioaduana where concat(aduana,numero)='" + Replace(pNoSerie, "'", "''") + "' and fecha='" + pfecha + "' and idinventario=" + pIdInventario.ToString + "),0)"
        ID = Comm.ExecuteScalar
        Return ID
    End Function

    Public Sub AgregaCantidadAAduana(ByVal pidAduana As Integer, pCantidad As Double)
        Comm.CommandText = "update tblinventarioaduana set cantidad=cantidad+" + pCantidad.ToString + " where idaduana='" + pidAduana.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function CantidadAsignados() As Double
        Dim Res As Double
        If IdDetalleCompra Then Comm.CommandText = "select ifnull((select sum(cantidad) from tblcomprasaduana where iddetalle=" + IdDetalleCompra.ToString + "),0)"
        If idDetalleRemisionC Then Comm.CommandText = "select ifnull((select sum(cantidad) from tblcomprasremisionesaduana where iddetalle=" + idDetalleRemisionC.ToString + "),0)"
        If IdDetalleVenta Then Comm.CommandText = "select ifnull((select sum(cantidad) from tblventasaduanan where iddetalle=" + IdDetalleVenta.ToString + "),0)"
        If IdDetalleRemisionV Then Comm.CommandText = "select ifnull((select sum(cantidad) from tblventasremisionesaduana where iddetalle=" + IdDetalleRemisionV.ToString + "),0)"
        Res = Comm.ExecuteScalar
        Return Res
    End Function

    Public Function ConsultaMovimientosAduana(pId As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "delete from tblinventarioaduanaconsulta where idaduana=" + pId.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblinventarioaduanaconsulta(idaduana,iddocumento,folio,fecha,tipodoc,cantidad) select l.idaduana,c.idcompra,concat(c.serie,lpad(convert(c.folioi using utf8),4,'0'),' ',c.referencia),c.fecha,'COMPRA',cl.cantidad from tblinventarioaduana as l inner join  tblcomprasaduana cl on l.idaduana=cl.idaduana inner join tblcomprasdetalles on cl.iddetalle=tblcomprasdetalles.iddetalle inner join tblcompras c on c.idcompra=tblcomprasdetalles.idcompra where l.idaduana=" + pId.ToString + " and c.estado=3;"
        Comm.CommandText += "insert into tblinventarioaduanaconsulta(idaduana,iddocumento,folio,fecha,tipodoc,cantidad) select l.idaduana,c.idremision,concat(c.serie,lpad(convert(c.folioi using utf8),4,'0'),' ',c.folio),c.fecha,'REM. COMPRA',cl.cantidad from tblinventarioaduana as l inner join  tblcomprasremisionesaduana cl on l.idaduana=cl.idaduana inner join tblcomprasremisionesdetalles on cl.iddetalle=tblcomprasremisionesdetalles.iddetalle inner join tblcomprasremisiones c on c.idremision=tblcomprasremisionesdetalles.idremision where l.idaduana=" + pId.ToString + " and c.estado=3;"
        Comm.CommandText += "insert into tblinventarioaduanaconsulta(idaduana,iddocumento,folio,fecha,tipodoc,cantidad) select l.idaduana,c.idventa,concat(c.serie,lpad(convert(c.folio using utf8),4,'0')),c.fecha,'FACTURA',cl.cantidad from tblinventarioaduana as l inner join  tblventasaduanan cl on l.idaduana=cl.idaduana inner join tblventasinventario on cl.iddetalle=tblventasinventario.idventasinventario inner join tblventas c on c.idventa=tblventasinventario.idventa where l.idaduana=" + pId.ToString + " and c.estado=3;"
        Comm.CommandText += "insert into tblinventarioaduanaconsulta(idaduana,iddocumento,folio,fecha,tipodoc,cantidad) select l.idaduana,c.idremision,concat(c.serie,lpad(convert(c.folio using utf8),4,'0')),c.fecha,'REMISIÓN',cl.cantidad from tblinventarioaduana as l inner join  tblventasremisionesaduana cl on l.idaduana=cl.idaduana inner join tblventasremisionesinventario on cl.iddetalle=tblventasremisionesinventario.iddetalle inner join tblventasremisiones c on c.idremision=tblventasremisionesinventario.idremision where l.idaduana=" + pId.ToString + " and c.estado=3;"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select iddocumento,tipodoc,fecha,folio,cantidad from tblinventarioaduanaconsulta where idaduana=" + pId.ToString + " order by fecha,folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbladuanas")
        'DS.WriteXmlSchema("tblclientesana.xml")
        Return DS.Tables("tbladuanas").DefaultView
    End Function
    Public Function HayViejaAduana(pIddetalle As Integer) As Boolean
        Comm.CommandText = "select count(idaduana) from tblventasaduana where iddetalle=" + pIddetalle.ToString
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function HayViejaAduanaGlobal(pIdVenta As Integer) As Boolean
        Comm.CommandText = "select count(idaduana) from tblventasaduana inner join tblventasinventario on tblventasaduana.iddetalle=tblventasinventario.idventasinventario where idventa=" + pIdVenta.ToString
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    'Public Function ConsultaMovimientosAduana(pId As Integer) As DataView
    '    Dim DS As New DataSet
    '    Comm.CommandText = "delete from tblinventarioaduanaconsulta where idaduana=" + pId.ToString
    '    Comm.ExecuteNonQuery()
    '    Comm.CommandText = "insert into tblinventarioaduanaconsulta(idaduana,iddocumento,folio,fecha,tipodoc,cantidad) select l.idaduana,c.idcompra,concat(c.serie,lpad(convert(c.folioi using utf8),4,'0'),' ',c.referencia),c.fecha,'COMPRA',cl.cantidad from tblinventarioaduana as l inner join  tblcomprasaduana cl on l.idaduana=cl.idaduana inner join tblcomprasdetalles on cl.iddetalle=tblcomprasdetalles.iddetalle inner join tblcompras c on c.idcompra=tblcomprasdetalles.idcompra where l.idaduana=" + pId.ToString + " and c.estado=3;"
    '    Comm.CommandText += "insert into tblinventarioaduanaconsulta(idaduana,iddocumento,folio,fecha,tipodoc,cantidad) select l.idaduana,c.idremision,concat(c.serie,lpad(convert(c.folioi using utf8),4,'0'),' ',c.folio),c.fecha,'REM. COMPRA',cl.cantidad from tblinventarioaduana as l inner join  tblcomprasremisionesaduana cl on l.idaduana=cl.idaduana inner join tblcomprasremisionesdetalles on cl.iddetalle=tblcomprasremisionesdetalles.iddetalle inner join tblcomprasremisiones c on c.idremision=tblcomprasremisionesdetalles.idremision where l.idaduana=" + pId.ToString + " and c.estado=3;"
    '    Comm.CommandText += "insert into tblinventarioaduanaconsulta(idaduana,iddocumento,folio,fecha,tipodoc,cantidad) select l.idaduana,c.idventa,concat(c.serie,lpad(convert(c.folio using utf8),4,'0')),c.fecha,'FACTURA',cl.cantidad from tblinventarioaduana as l inner join  tblventasaduanan cl on l.idaduana=cl.idaduana inner join tblventasinventario on cl.iddetalle=tblventasinventario.idventasinventario inner join tblventas c on c.idventa=tblventasinventario.idventa where l.idaduana=" + pId.ToString + " and c.estado=3;"
    '    Comm.CommandText += "insert into tblinventarioaduanaconsulta(idaduana,iddocumento,folio,fecha,tipodoc,cantidad) select l.idaduana,c.idremision,concat(c.serie,lpad(convert(c.folio using utf8),4,'0')),c.fecha,'REMISIÓN',cl.cantidad from tblinventarioaduana as l inner join  tblventasremisionesaduana cl on l.idaduana=cl.idaduana inner join tblventasremisionesinventario on cl.iddetalle=tblventasremisionesinventario.iddetalle inner join tblventasremisiones c on c.idremision=tblventasremisionesinventario.idremision where l.idaduana=" + pId.ToString + " and c.estado=3;"
    '    Comm.ExecuteNonQuery()
    '    Comm.CommandText = "select iddocumento,tipodoc,fecha,folio,cantidad from tblinventarioaduanaconsulta where idaduana=" + pId.ToString + " order by fecha,folio"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tbladuana")
    '    'DS.WriteXmlSchema("tblclientesana.xml")
    '    Return DS.Tables("tbladuana").DefaultView
    'End Function
    Public Function ConsultaAduanaVentaReader(ByVal pidVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tblventasaduanan.idaduana,tblinventarioaduana.numero,tblinventarioaduana.fecha,tblinventarioaduana.aduana,tblventasaduanan.iddetalle from tblventasaduanan inner join tblventasinventario on tblventasaduanan.iddetalle=tblventasinventario.idventasinventario inner join tblinventarioaduana on tblventasaduanan.idaduana=tblinventarioaduana.idaduana where tblventasinventario.idventa=" + pidVenta.ToString
        Return Comm.ExecuteReader
    End Function

    Public Sub reporteComprasAduanas(ByVal idSucursal As Integer, ByVal desde As String, ByVal hasta As String, ByVal idProveedor As Integer, ByVal idInventario As Integer, ByVal idClasificacion As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer)
        Dim ds As New DataSet
        Comm.CommandText = "select a.cantidad,ia.fecha as fecha,i.clave, i.nombre, p.nombre as nombreProveedor, c.fecha, ia.aduana,ia.numero, concat(c.serie,c.folioi) as folio,ia.idaduana from  tblcomprasaduana as a inner join tblcomprasdetalles as cd on a.iddetalle=cd.iddetalle "
        Comm.CommandText += "inner join tblcompras as c on cd.idcompra=c.idcompra inner join tblproveedores as p on c.idproveedor=p.idproveedor inner join tblinventario as i on cd.idinventario=i.idinventario inner join tblinventarioaduana as ia on ia.idaduana=a.idaduana "
        Comm.CommandText += "where c.fecha>='" + desde + "' and c.fecha<='" + hasta + "' "
        If idSucursal > 0 Then
            Comm.CommandText += "and c.idsucursal=" + idSucursal.ToString() + " "
        End If
        If idInventario > 0 Then
            Comm.CommandText += "and cd.idinventario=" + idInventario.ToString() + " "
        End If
        If idProveedor > 0 Then
            Comm.CommandText += "and c.idproveedor=" + idProveedor.ToString() + " "
        End If
        If idClasificacion > 0 Then
            Comm.CommandText += "and i.idclasificacion=" + idClasificacion.ToString()
        End If
        Comm.CommandText += " order by ia.idaduana,c.fecha,c.serie,c.folioi;"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tblComprasAduanas")
        'ds.WriteXmlSchema("tblComprasAduanas.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repComprasAduanas
        rep.SetDataSource(ds)
        Dim s As New dbSucursales(idSucursal, MySqlcon)
        rep.SetParameterValue("nombreEmpresa", s.Nombre)
        rep.SetParameterValue("desde", desde)
        rep.SetParameterValue("hasta", hasta)
        rep.SetParameterValue("tipoReporte", "Facturas")
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub

    Public Sub reporteComprasRemisionesAduanas(ByVal idSucursal As Integer, ByVal desde As String, ByVal hasta As String, ByVal idProveedor As Integer, ByVal idInventario As Integer, ByVal idClasificacion As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer)
        Dim ds As New DataSet
        Comm.CommandText = "select a.cantidad,ia.fecha as fecha,i.clave, i.nombre, p.nombre as nombreProveedor, c.fecha, ia.aduana,ia.numero, concat(c.serie,c.folioi) as folio,ia.aduana from  tblcomprasremisionesaduana as a inner join tblcomprasremisionesdetalles as cd on a.iddetalle=cd.iddetalle "
        Comm.CommandText += "inner join tblcomprasremisiones as c on cd.idremision=c.idremision inner join tblproveedores as p on c.idproveedor=p.idproveedor inner join tblinventario as i on cd.idinventario=i.idinventario inner join tblinventarioaduana as ia on ia.idaduana=a.idaduana "
        Comm.CommandText += "where c.fecha>='" + desde + "' and c.fecha<='" + hasta + "' "
        If idSucursal > 0 Then
            Comm.CommandText += "and c.idsucursal=" + idSucursal.ToString() + " "
        End If
        If idInventario > 0 Then
            Comm.CommandText += "and cd.idinventario=" + idInventario.ToString() + " "
        End If
        If idProveedor > 0 Then
            Comm.CommandText += "and c.idproveedor=" + idProveedor.ToString() + " "
        End If
        If idClasificacion > 0 Then
            Comm.CommandText += "and i.idclasificacion=" + idClasificacion.ToString()
        End If
        Comm.CommandText += " order by ia.idaduana,c.fecha,c.serie,c.folioi;"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tblComprasAduanas")
        'ds.WriteXmlSchema("tblComprasAduanas.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repComprasAduanas
        rep.SetDataSource(ds)
        Dim s As New dbSucursales(idSucursal, MySqlcon)
        rep.SetParameterValue("nombreEmpresa", s.Nombre)
        rep.SetParameterValue("desde", desde)
        rep.SetParameterValue("hasta", hasta)
        rep.SetParameterValue("tipoReporte", "Remisiones")
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub

    Public Sub reporteVentasAduanas(ByVal idSucursal As Integer, ByVal desde As String, ByVal hasta As String, ByVal idProveedor As Integer, ByVal idInventario As Integer, ByVal idClasificacion As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer, ByVal idVendedor As Integer)
        Dim ds As New DataSet
        Comm.CommandText = "select a.cantidad,ia.fecha as idAduana,i.clave, i.nombre, cli.nombre as nombreCliente, c.fecha, ia.aduana,ia.numero, concat(c.serie,c.folio) as folio from  tblventasaduanan as a inner join tblventasinventario as cd on a.iddetalle=cd.idventasinventario "
        Comm.CommandText += "inner join tblventas as c on cd.idventa=c.idventa inner join tblclientes as cli on c.idcliente=cli.idcliente inner join tblinventario as i on cd.idinventario=i.idinventario inner join tblinventarioaduana as ia on ia.idaduana=a.idaduana "
        Comm.CommandText += "where c.fecha>='" + desde + "' and c.fecha<='" + hasta + "' "
        If idSucursal > 0 Then
            Comm.CommandText += "and c.idsucursal=" + idSucursal.ToString() + " "
        End If
        If idInventario > 0 Then
            Comm.CommandText += "and cd.idinventario=" + idInventario.ToString() + " "
        End If
        If idProveedor > 0 Then
            Comm.CommandText += "and c.idproveedor=" + idProveedor.ToString() + " "
        End If
        If idClasificacion > 0 Then
            Comm.CommandText += "and i.idclasificacion=" + idClasificacion.ToString()
        End If
        Comm.CommandText += " order by ia.idaduana,c.fecha,c.serie,c.folio;"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tblVentasAduanas")
        'ds.WriteXmlSchema("tblVentasAduanas.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repVentasAduanas
        rep.SetDataSource(ds)
        Dim s As New dbSucursales(idSucursal, MySqlcon)
        rep.SetParameterValue("nombreEmpresa", s.Nombre)
        rep.SetParameterValue("desde", desde)
        rep.SetParameterValue("hasta", hasta)
        rep.SetParameterValue("tipoReporte", "Facturas")
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub

    Public Sub reporteVentasRemisionesAduanas(ByVal idSucursal As Integer, ByVal desde As String, ByVal hasta As String, ByVal idProveedor As Integer, ByVal idInventario As Integer, ByVal idClasificacion As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer, ByVal idVendedor As Integer)
        Dim ds As New DataSet
        Comm.CommandText = "select a.cantidad,ia.fecha as idAduana,i.clave, i.nombre, p.nombre as nombreCliente, c.fecha, ia.aduana,ia.numero, concat(c.serie,c.folio) as folio from  tblventasaduanan as a inner join tblventasinventario as cd on a.iddetalle=cd.idventasinventario "
        Comm.CommandText += "inner join tblventas as c on cd.idventa=c.idventa inner join tblclientes as p on c.idcliente=p.idcliente inner join tblinventario as i on cd.idinventario=i.idinventario inner join tblinventarioaduana as ia on ia.idaduana=a.idaduana "
        Comm.CommandText += "where c.fecha>='" + desde + "' and c.fecha<='" + hasta + "' "
        If idSucursal > 0 Then
            Comm.CommandText += "and c.idsucursal=" + idSucursal.ToString() + " "
        End If
        If idInventario > 0 Then
            Comm.CommandText += "and cd.idinventario=" + idInventario.ToString() + " "
        End If
        If idProveedor > 0 Then
            Comm.CommandText += "and c.idcliente=" + idProveedor.ToString() + " "
        End If
        If idClasificacion > 0 Then
            Comm.CommandText += "and i.idclasificacion=" + idClasificacion.ToString()
        End If
        Comm.CommandText += " order by ia.idaduana,c.fecha,c.serie,c.folio;"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tblVentasAduanas")
        'ds.WriteXmlSchema("tblComprasAduanas.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repVentasAduanas
        rep.SetDataSource(ds)
        Dim s As New dbSucursales(idSucursal, MySqlcon)
        rep.SetParameterValue("nombreEmpresa", s.Nombre)
        rep.SetParameterValue("desde", desde)
        rep.SetParameterValue("hasta", hasta)
        rep.SetParameterValue("tipoReporte", "Remisiones")
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub
    Public Sub reporteInventarioAduanas(ByVal idInventario As Integer, ByVal idClas As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer, ByVal existencia As Boolean, pDescon As Byte)
        Dim ds As New DataSet
        Dim clasificacion As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        Dim cla As String = ""
        Comm.CommandText = "select i.clave,i.nombre,l.aduana,l.fecha,l.numero,l.cantidad from tblinventario as i inner join tblinventarioaduana as l on i.idinventario=l.idinventario"
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
        Comm.CommandText += " order by i.idinventario,l.aduana"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tblinventarioAduanas")
        'ds.WriteXmlSchema("tblInventarioAduanas1.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repInventarioAduanas
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
