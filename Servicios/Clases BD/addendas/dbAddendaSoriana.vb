Imports MySql.Data.MySqlClient
Public Class dbAddendaSoriana
    Private mysqlConn As MySqlConnection
    Public Sub New(mysqlConn As MySqlConnection)
        Me.mysqlConn = mysqlConn
    End Sub

    Public Function Buscar(idventa As Integer) As AddendaSoriana
        Dim a As AddendaSoriana = Nothing
        Dim comm As New MySqlCommand("select v.idventa,noproveedorsoriana,concat(serie,folio) as folio,fecha,ifnull(tienda,0) as tienda,ifnull(c.idmoneda-1,1) as moneda,ifnull(tipobulto,1) as tipobulto,ifnull(entregamercancia,0) as entregamercancia,ifnull(cantidadbultos,0) as cantidadbultos,round(sum(d.precio*d.cantidad),2) as subtotal,round(sum(d.precio*d.cantidad*d.ieps*.01),2) as ieps,round(sum(d.precio*d.cantidad*d.iva*.01),2) as iva,0 as otros,round(sum(d.precio*d.cantidad+d.precio*d.cantidad*d.ieps*.01+d.precio*d.cantidad*d.iva*.01),2) as total,ifnull(fechaentregamercancia,now()) as fechaentrega,ifnull(cita,0) as cita, folionotaentrada, pedimento, aduana, agenteaduanal, tipopedimento, fechapedimento, fecharecibolaredo, fechabilloflanding from tblopciones2 as o inner join tblventas as v inner join tblventasinventario as d on v.idventa=d.idventa left outer join tblmonedasconversiones as c on v.idconversion=c.idconversion left outer join tbladdendasoriana as a on a.idventa=v.idventa left outer join tbladdendasorianapedimento p on p.idventa = a.idventa where v.idventa=" + idventa.ToString() + " group by v.idventa;", MySqlcon)
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Try
            If dr.Read Then
                a = New AddendaSoriana(dr("idventa"), dr("noproveedorsoriana"), dr("folio"), dr("fecha"), dr("tienda"), dr("moneda"), dr("tipobulto"), dr("entregamercancia"), dr("cantidadbultos"), dr("subtotal"), dr("iva"), dr("ieps"), dr("otros"), dr("total"), dr("fechaentrega"), dr("cita"), If(dr("folionotaentrada") Is DBNull.Value, 0, dr("folionotaentrada")))
                If dr("pedimento") IsNot DBNull.Value Then
                    a.Pedimento = New AddendaSorianaPedimento(dr("idventa"), dr("pedimento"), dr("aduana"), dr("agenteaduanal"), dr("tipopedimento"), dr("fechapedimento"), dr("fecharecibolaredo"), dr("fechabilloflanding"))
                End If
            End If
            dr.Close()
            comm.CommandText = "select count(idventa) from tbladdendasorianaarticulo where idventa=" + idventa.ToString()
            If comm.ExecuteScalar = 0 Then
                comm.CommandText = "SELECT v.idventa,ifnull(p.foliopedido,0) folio,ifnull(p.tienda,0) tienda, ifnull( clave2,0) codigo, ifnull(ar.cantidad,0) cantidadunidadcompra, round(ifnull(ar.precio,0),2) costonetounidadcompra, ifnull(ar.iva,0) porcentajeiva, ifnull(ar.ieps,0) porcentajeieps FROM tblventas as v left outer join tbladdendasoriana as a on v.idventa=a.idventa left outer join tbladdendasorianapedido as p on a.idventa=p.idventa left outer join tblventasinventario ar on ar.idventa=v.idventa left outer join tblinventario i on i.idinventario=ar.idinventario where v.idventa = " + idventa.ToString()
            Else
                comm.CommandText = "SELECT v.idventa,ifnull(p.foliopedido,0) folio,ifnull(p.tienda,0) tienda, ifnull(codigo,0) codigo, ifnull(cantidadunidadcompra,0) cantidadunidadcompra, round(ifnull(costonetounidadcompra,0),2) costonetounidadcompra, ifnull(porcentajeiva,0) porcentajeiva, ifnull(porcentajeieps,0) porcentajeieps FROM tblventas as v left outer join tbladdendasoriana as a on v.idventa=a.idventa left outer join tbladdendasorianapedido as p on a.idventa=p.idventa left outer join tbladdendasorianaarticulo as ar on ar.idventa=p.idventa and ar.foliopedido=p.foliopedido where v.idventa=" + idventa.ToString()
            End If
            dr = comm.ExecuteReader
            Dim folio As Integer = -1
            While dr.Read
                If dr("folio") <> folio Then
                    folio = dr("folio")
                    a.Pedidos.Add(New AddendaSorianaPedido(dr("idventa"), dr("folio"), dr("tienda")))
                End If
                a.Pedidos(a.Pedidos.Count - 1).Articulos.Add(New AddendaSorianaArticulo(dr("idventa"), dr("folio"), dr("codigo"), dr("cantidadunidadcompra"), dr("costonetounidadcompra"), dr("porcentajeiva"), dr("porcentajeieps")))
            End While
            dr.Close()
        Finally
            If Not dr.IsClosed Then dr.Close()
        End Try
            Return a
    End Function
    Public Function Existe(idventa As Integer) As Boolean
        Dim comm As New MySqlCommand("select count(idventa) from tbladdendasoriana where idventa=" + idventa.ToString(), mysqlConn)
        Return comm.ExecuteScalar = 1
    End Function
    Public Sub Guardar(a As AddendaSoriana)
        Dim comm As New MySqlCommand("select count(idventa) from tbladdendasoriana where idventa=" + a.IdVenta.ToString(), mysqlConn)
        Try
            comm.Transaction = MySqlcom.Connection.BeginTransaction()
            If comm.ExecuteScalar() = 0 Then
                comm.CommandText = "insert into tbladdendasoriana (idventa, remision, tienda, tipomoneda, tipobulto, entregamercancia, cantidadbultos, fechaentregamercancia, cita"
                If a.FolioNotaEntrada IsNot Nothing Then comm.CommandText += ", folionotaentrada"
                comm.CommandText += ") values (" + a.IdVenta.ToString() + ",@remision," + a.Tienda.ToString() + "," + a.TipoMoneda.ToString() + ", " + a.TipoBulto.ToString() + "," + a.EntregaMercancia.ToString() + ", " + a.CantidadBultos.ToString() + ",@fechaentregamercancia," + a.Cita.ToString()
                If a.FolioNotaEntrada IsNot Nothing Then comm.CommandText += ", " + a.FolioNotaEntrada.ToString()
                comm.CommandText += ");"
                comm.Parameters.Add(New MySqlParameter("@remision", a.Remision))
                comm.Parameters.Add(New MySqlParameter("@fechaentregamercancia", a.FechaEntrega))
                comm.ExecuteNonQuery()
                comm.Parameters.Clear()
                If a.Pedimento IsNot Nothing Then
                    comm.CommandText = "insert into tbladdendasorianapedimento (idventa, pedimento, aduana, agenteaduanal, tipopedimento, fechapedimento, fecharecibolaredo, fechabilloflanding) values (" + a.IdVenta.ToString() + ", " + a.Pedimento.Pedimento.ToString() + ", " + a.Pedimento.Aduana.ToString() + ", " + a.Pedimento.AgenteAduanal.ToString() + ", @tipopedimento, @fechapedimento, @fecharecibolaredo, @fechabilloflanding);"
                    comm.Parameters.Add(New MySqlParameter("@tipopedimento", a.Pedimento.TipoPedimento))
                    comm.Parameters.Add(New MySqlParameter("@fechapedimento", a.Pedimento.FechaPedimento))
                    comm.Parameters.Add(New MySqlParameter("@fecharecibolaredo", a.Pedimento.FechaReciboLaredo))
                    comm.Parameters.Add(New MySqlParameter("@fechabilloflanding", a.Pedimento.FechaBillOfLanding))
                    comm.ExecuteNonQuery()
                    comm.Parameters.Clear()
                End If
                For Each p As AddendaSorianaPedido In a.Pedidos
                    comm.CommandText = "insert into tbladdendasorianapedido (idventa,foliopedido,tienda)values(" + p.IdVenta.ToString() + "," + p.FolioPedido.ToString() + "," + p.Tienda.ToString() + ")"
                    comm.ExecuteNonQuery()
                    For Each ar As AddendaSorianaArticulo In p.Articulos
                        comm.CommandText = "insert into tbladdendasorianaarticulo (idventa, foliopedido, codigo, cantidadunidadcompra, costonetounidadcompra, porcentajeiva, porcentajeieps) values (" + p.IdVenta.ToString() + "," + p.FolioPedido.ToString() + ",'" + ar.Codigo.ToString() + "'," + ar.CantidadUnidadCompra.ToString() + "," + ar.CostoNetoUnidadCompra.ToString() + "," + ar.PorcentajeIVA.ToString() + "," + ar.PorcentajeIEPS.ToString() + ")"
                        comm.ExecuteNonQuery()
                    Next
                Next
            Else
                comm.CommandText = "update tbladdendasoriana set idventa = " + a.IdVenta.ToString() + ", remision = @remision, tienda = " + a.TipoBulto.ToString() + ", tipomoneda = " + a.TipoMoneda.ToString() + ", tipobulto = " + a.TipoBulto.ToString + ", entregamercancia = " + a.EntregaMercancia.ToString() + ", cantidadbultos = " + a.CantidadBultos.ToString() + ", fechaentregamercancia = @fechaentregamercancia, cita = " + a.Cita.ToString()
                If a.FolioNotaEntrada IsNot Nothing Then comm.CommandText += ", folionotaentrada = " + a.FolioNotaEntrada.ToString() + ";"
                comm.Parameters.Add(New MySqlParameter("@remision", a.Remision))
                comm.Parameters.Add(New MySqlParameter("@fechaentregamercancia", a.FechaEntrega))
                comm.ExecuteNonQuery()
                comm.Parameters.Clear()
                comm.CommandText = "delete from tbladdendasorianapedimento where idventa = " + a.IdVenta.ToString() + ";"
                comm.ExecuteNonQuery()
                If a.Pedimento IsNot Nothing Then
                    comm.CommandText = "insert into tbladdendasorianapedimento (idventa, pedimento, aduana, agenteaduanal, tipopedimento, fechapedimento, fecharecibolaredo, fechabilloflanding) values (" + a.IdVenta.ToString() + ", " + a.Pedimento.Pedimento.ToString() + ", " + a.Pedimento.Aduana.ToString() + ", " + a.Pedimento.AgenteAduanal.ToString() + ", @tipopedimento, @fechapedimento, @fecharecibolaredo, @fechabilloflanding);"
                    comm.Parameters.Add(New MySqlParameter("@tipopedimento", a.Pedimento.TipoPedimento))
                    comm.Parameters.Add(New MySqlParameter("@fechapedimento", a.Pedimento.FechaPedimento))
                    comm.Parameters.Add(New MySqlParameter("@fecharecibolaredo", a.Pedimento.FechaReciboLaredo))
                    comm.Parameters.Add(New MySqlParameter("@fechabilloflanding", a.Pedimento.FechaBillOfLanding))
                    comm.ExecuteNonQuery()
                    comm.Parameters.Clear()
                End If
                comm.CommandText = "delete from tbladdendasorianaarticulo where idventa = " + a.IdVenta.ToString() + ";delete from tbladdendasorianapedido where idventa = " + a.IdVenta.ToString() + ";"
                comm.ExecuteNonQuery()
                For Each p As AddendaSorianaPedido In a.Pedidos
                    comm.CommandText = "insert into tbladdendasorianapedido (idventa,foliopedido,tienda)values(" + p.IdVenta.ToString() + "," + p.FolioPedido.ToString() + "," + p.Tienda.ToString() + ")"
                    comm.ExecuteNonQuery()
                    For Each ar As AddendaSorianaArticulo In p.Articulos
                        comm.CommandText = "insert into tbladdendasorianaarticulo (idventa, foliopedido, codigo, cantidadunidadcompra, costonetounidadcompra, porcentajeiva, porcentajeieps) values (" + p.IdVenta.ToString() + "," + p.FolioPedido.ToString() + ",'" + ar.Codigo.ToString() + "'," + ar.CantidadUnidadCompra.ToString() + "," + ar.CostoNetoUnidadCompra.ToString() + "," + ar.PorcentajeIVA.ToString() + "," + ar.PorcentajeIEPS.ToString() + ")"
                        comm.ExecuteNonQuery()
                    Next
                Next
            End If
            comm.Transaction.Commit()
        Catch ex As Exception
            comm.Transaction.Rollback()
        End Try
    End Sub

    Public Sub Modificar(a As AddendaSoriana)

    End Sub
End Class
