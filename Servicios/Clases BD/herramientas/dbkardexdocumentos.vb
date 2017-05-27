Public Class dbkardexdocumentos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub
    Public Function MovimientosFactura(pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "delete from tbldockardexf where idventa=" + pIdVenta.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tbldockardexf(idmov,tipo,fecha,hora,movimiento,folio,importe,idventa) select idpago,0,fecha,hora,'PAGO','-',cantidad,idventa from tblventaspagos where idventa=" + pIdVenta.ToString + " and iddocumento=0"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tbldockardexf(idmov,tipo,fecha,hora,movimiento,folio,importe,idventa) select iddevolucion,1,fecha,hora,'DEVOLUCIÓN',concat(serie,lpad(convert(folio using utf8),5,'0')),totalapagar,idventa from tbldevoluciones where idventa=" + pIdVenta.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tbldockardexf(idmov,tipo,fecha,hora,movimiento,folio,importe,idventa) select tblnotasdecredito.idnota,2,fecha,hora,'NOTA DE CRÉDITO',concat(serie,lpad(convert(folio using utf8),5,'0')),precio,idventa from tblnotasdecredito inner join tblnotasdecreditodetalles on tblnotasdecredito.idnota=tblnotasdecreditodetalles.idnota where idventa=" + pIdVenta.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tbldockardexf(idmov,tipo,fecha,hora,movimiento,folio,importe,idventa) select idmovimiento,3,fecha,hora,'MOV. INVENTARIO',concat(serie,lpad(convert(folio using utf8),5,'0')),totalapagar,idventa from tblmovimientos where idventa=" + pIdVenta.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select idmov,tipo,fecha,movimiento,folio,importe from tbldockardexf where idventa=" + pIdVenta.ToString + " order by fecha,hora,tipo"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldockardex")
        DS.WriteXmlSchema("tbldockardex.xml")
        Return DS.Tables("tbldockardex").DefaultView
    End Function

    Public Function MovimientosRemision(pIdRemision As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "delete from tbldockardexr where idremision=" + pIdRemision.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tbldockardexr(idmov,tipo,fecha,hora,movimiento,folio,importe,idremision) select idpago,0,fecha,hora,'PAGO','-',cantidad,idremision from tblventaspagosremisiones where idremision=" + pIdRemision.ToString + " and iddevolucion=0"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tbldockardexr(idmov,tipo,fecha,hora,movimiento,folio,importe,idremision) select iddevolucion,1,fecha,hora,'DEVOLUCIÓN',concat(serie,lpad(convert(folio using utf8),5,'0')),totalapagar,idremision from tbldevoluciones where idremision=" + pIdRemision.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tbldockardexr(idmov,tipo,fecha,hora,movimiento,folio,importe,idremision) select idmovimiento,3,fecha,hora,'MOV. INVENTARIO',concat(serie,lpad(convert(folio using utf8),5,'0')),totalapagar,idremision from tblmovimientos where idremision=" + pIdRemision.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tbldockardexr(idmov,tipo,fecha,hora,movimiento,folio,importe,idremision) select idventa,4,fecha,hora,'FACTURADO EN',concat(serie,lpad(convert(folio using utf8),5,'0')),totalapagar," + pIdRemision.ToString + " from tblventas where idventa=(select idventar from tblventasremisiones where idremision=" + pIdRemision.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select idmov,tipo,fecha,movimiento,folio,importe from tbldockardexr where idremision=" + pIdRemision.ToString + " order by fecha,hora,tipo"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldockardex")
        Return DS.Tables("tbldockardex").DefaultView
    End Function
    Public Function MovimientosCompra(pIdCompra As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "delete from tbldockardexc where idcompra=" + pIdCompra.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tbldockardexc(idmov,tipo,fecha,hora,movimiento,folio,importe,idcompra) select idpago,0,fecha,hora,'PAGO','-',cantidad,idcompra from tblcompraspagos where idcompra=" + pIdCompra.ToString + " and iddocumento=0"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tbldockardexc(idmov,tipo,fecha,hora,movimiento,folio,importe,idcompra) select iddevolucion,1,fecha,hora,'DEVOLUCIÓN',concat(serie,lpad(convert(folioi using utf8),5,'0')),totalapagar,idcompra from tbldevolucionescompras where idcompra=" + pIdCompra.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tbldockardexc(idmov,tipo,fecha,hora,movimiento,folio,importe,idcompra) select tblnotasdecreditocompras.idnota,2,fecha,hora,'NOTA DE CRÉDITO',concat(serie,lpad(convert(folioi using utf8),5,'0')),precio,idcompra from tblnotasdecreditocompras inner join tblnotasdecreditodetallesc on tblnotasdecreditocompras.idnota=tblnotasdecreditodetallesc.idnota where idcompra=" + pIdCompra.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select idmov,tipo,fecha,movimiento,folio,importe from tbldockardexc where idcompra=" + pIdCompra.ToString + " order by fecha,hora,tipo"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldockardex")
        Return DS.Tables("tbldockardex").DefaultView
    End Function

    Public Function MovimientosFacturaCant(pIdVenta As Integer) As Integer
        
        Dim Total As Integer = 0
        Comm.CommandText = "select count(idpago) from tblventaspagos where idventa=" + pIdVenta.ToString + " and iddocumento=0"
        Total += Comm.ExecuteScalar
        Comm.CommandText = "select count(iddevolucion) from tbldevoluciones where idventa=" + pIdVenta.ToString
        Total += Comm.ExecuteScalar
        Comm.CommandText = "select count(tblnotasdecredito.idnota) from tblnotasdecredito inner join tblnotasdecreditodetalles on tblnotasdecredito.idnota=tblnotasdecreditodetalles.idnota where idventa=" + pIdVenta.ToString
        Total += Comm.ExecuteScalar
        Comm.CommandText = "select count(idmovimiento) from tblmovimientos where idventa=" + pIdVenta.ToString
        Total += Comm.ExecuteScalar
        Return Total
    End Function

    Public Function MovimientosRemisionCant(pIdRemision As Integer) As Integer
        Dim Total As Integer = 0
        Comm.CommandText = "select count(idpago) from tblventaspagosremisiones where idremision=" + pIdRemision.ToString + " and iddevolucion=0"
        Total += Comm.ExecuteScalar
        Comm.CommandText = "select count(iddevolucion) from tbldevoluciones where idremision=" + pIdRemision.ToString
        Total += Comm.ExecuteScalar
        Comm.CommandText = "select count(idmovimiento) from tblmovimientos where idremision=" + pIdRemision.ToString
        Total += Comm.ExecuteScalar
        Comm.CommandText = "select count(idventa) from tblventas where idventa=(select idventar from tblventasremisiones where idremision=" + pIdRemision.ToString + ")"
        Total += Comm.ExecuteScalar
        Comm.ExecuteNonQuery()
        Return Total
    End Function
    Public Function MovimientosCompraCant(pIdCompra As Integer) As Integer
        Dim Total As Integer = 0
        Comm.CommandText = "select count(idpago) from tblcompraspagos where idcompra=" + pIdCompra.ToString + " and iddocumento=0"
        Total += Comm.ExecuteScalar
        Comm.CommandText = "select count(iddevolucion) from tbldevolucionescompras where idcompra=" + pIdCompra.ToString
        Total += Comm.ExecuteScalar
        Comm.CommandText = "select count(tblnotasdecreditocompras.idnota) from tblnotasdecreditocompras inner join tblnotasdecreditodetallesc on tblnotasdecreditocompras.idnota=tblnotasdecreditodetallesc.idnota where idcompra=" + pIdCompra.ToString
        Total += Comm.ExecuteScalar
        Return Total
    End Function

    Public Function MovimientosInvFactura(pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select i.clave,i.nombre,vi.cantidad," +
            "ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idventa=vi.idventa and tbldevolucionesdetalles.idinventario=vi.idinventario),0), " +
            "ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=vi.idventa and tblmovimientosdetalles.idinventario=vi.idinventario),0), " +
            "vi.cantidad" +
            "-ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idventa=vi.idventa and tbldevolucionesdetalles.idinventario=vi.idinventario),0) " +
            "-ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=vi.idventa and tblmovimientosdetalles.idinventario=vi.idinventario),0) " +
            "from tblventasinventario vi inner join tblinventario i on vi.idinventario=i.idinventario where vi.idventa=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldocinvkardex")
        'DS.WriteXmlSchema("tbldockardex.xml")
        Return DS.Tables("tbldocinvkardex").DefaultView
    End Function
    Public Function MovimientosInvRemision(pIdRemision As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select i.clave,i.nombre,vi.cantidad," +
            "ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idremision=vi.idremision and tbldevolucionesdetalles.idinventario=vi.idinventario),0), " +
            "ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=vi.idremision and tblmovimientosdetalles.idinventario=vi.idinventario),0), " +
            "vi.cantidad" +
            "-ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idremision=vi.idremision and tbldevolucionesdetalles.idinventario=vi.idinventario),0) " +
            "-ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=vi.idremision and tblmovimientosdetalles.idinventario=vi.idinventario),0) " +
            "from tblventasremisionesinventario vi inner join tblinventario i on vi.idinventario=i.idinventario where vi.idremision=" + pIdRemision.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldocinvkardex")
        'DS.WriteXmlSchema("tbldockardex.xml")
        Return DS.Tables("tbldocinvkardex").DefaultView
    End Function
    Public Function MovimientosInvCompra(pIdCompra As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select i.clave,i.nombre,vi.cantidad," +
            "ifnull((select sum(tbldevolucionesdetallesc.cantidad) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idcompra=vi.idcompra and tbldevolucionesdetallesc.idinventario=vi.idinventario),0),0, " +
            "vi.cantidad" +
            "-ifnull((select sum(tbldevolucionesdetallesc.cantidad) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idcompra=vi.idcompra and tbldevolucionesdetallesc.idinventario=vi.idinventario),0) " +
            "from tblcomprasdetalles vi inner join tblinventario i on vi.idinventario=i.idinventario where vi.idcompra=" + pIdCompra.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldocinvkardex")
        'DS.WriteXmlSchema("tbldockardex.xml")
        Return DS.Tables("tbldocinvkardex").DefaultView
    End Function
End Class
