Imports MySql.Data.MySqlClient
Public Class dbAddendaWalmart
    Private conn As MySqlConnection
    Public Sub New(conn As MySqlConnection)
        Me.conn = conn
    End Sub
    Public Function Buscar(idventa As Integer) As AddendaWalmart
        Dim a As AddendaWalmart = Nothing
        Dim comm As New MySqlCommand("select v.idventa,folio,date(fecha) fecha,v.serie,ifnull(uuid,'') uuid, folioordencompra, ifnull(fechaordencompra,now()) fechaordencompra, foliorecibo, ifnull(fecharecibo,now()) fecharecibo, ifnull(cedulaieps,'') cedulaieps, noproveedorwalmart numeroproveedor,s.nombre nombreproveedor,concat(s.direccion,' ',s.noexterior) calleynumeroproveedor, s.colonia coloniaproveedor,s.ciudad localidadproveedor,s.estado estadoproveedor,s.cp codigopostalproveedor, s.rfc rfcproveedor, c.clave clavecomprador, c.nombre nombrecomprador, c.direccion calleynumerocomprador, c.colonia coloniacomprador, c.ciudad localidadcomprador, c.estado estadocomprador, c.cp codigopostalcomprador, c.rfc rfccomprador, creditodias diascredito,i.iva tasaiva, i.ieps tasaieps, abreviatura moneda, ifnull(clavelugarentrega,'') clavelugarentrega, ifnull(nombrelugarentrega,'') nombrelugarentrega, c.direccion2 calleynumerolugarentrega, c.colonia2 colonialugarentrega, c.ciudad2 localidadlugarentrega, c.estado2 estadolugarentrega, c.cp2 codigopostallugarentrega, round(sum(vi.cantidad*vi.precio),2) subtotal,round(sum(vi.cantidad*vi.precio*vi.iva*.01),2) iva,round(sum(vi.cantidad*vi.precio*vi.ieps*.01),2) ieps,round(sum(vi.cantidad*vi.precio*(vi.iva*.01+1)),2) total from tblopciones2 o inner join tblventas v inner join tblclientes c on c.idcliente=v.idcliente inner join tblmonedas m on m.idmoneda=v.idconversion inner join tblsucursales s on s.idsucursal=v.idsucursal left outer join tbladdendawalmart a on a.idventa=v.idventa left outer join tblventasinventario vi on vi.idventa=v.idventa left outer join tblventastimbrado t on t.idventa=v.idventa left outer join tblinventario i on vi.idinventario=i.idinventario where v.idventa= " + idventa.ToString(), conn)
        Dim dr As MySqlDataReader = comm.ExecuteReader
        If dr.Read Then
            Dim folioorden As Integer?
            Dim foliorecibo As Integer?
            If dr("folioordencompra") IsNot DBNull.Value Then folioorden = CInt(dr("folioordencompra"))
            If dr("foliorecibo") IsNot DBNull.Value Then foliorecibo = CInt(dr("foliorecibo"))
            a = New AddendaWalmart(dr("idventa"), dr("folio"), dr("fecha"), dr("serie"), dr("uuid"), folioorden, dr("fechaordencompra"), foliorecibo, dr("fecharecibo"), dr("clavecomprador"), dr("calleynumerocomprador"), dr("calleynumerolugarentrega"), dr("calleynumeroproveedor"), dr("codigopostalcomprador"), dr("codigopostallugarentrega"), dr("codigopostalproveedor"), dr("clavelugarentrega"), dr("colonialugarentrega"), dr("coloniacomprador"), dr("coloniaproveedor"), dr("estadolugarentrega"), dr("estadocomprador"), dr("estadoproveedor"), dr("localidadlugarentrega"), dr("localidadcomprador"), dr("localidadproveedor"), dr("nombrelugarentrega"), dr("nombrecomprador"), dr("nombreproveedor"), dr("rfccomprador"), dr("rfcproveedor"), dr("moneda"), dr("numeroproveedor"), dr("diascredito"), dr("tasaiva"), dr("iva"), dr("tasaieps"), dr("ieps"), dr("total"), dr("subtotal"), dr("cedulaieps"))
        End If
        dr.Close()
        comm.CommandText = "select v.idventa, vi.cantidad, i.clave codigobarras, i.nombre descripcion, vi.precio, vi.iva tasaiva from tblventas v left outer join tblventasinventario vi on vi.idventa=v.idventa left outer join tblinventario i on vi.idinventario=i.idinventario where v.idventa= " + idventa.ToString()
        dr = comm.ExecuteReader
        While dr.Read
            a.Articulos.Add(New AddendaWalmartArticulo(dr("idventa"), dr("codigobarras"), dr("descripcion"), dr("cantidad"), dr("precio"), dr("tasaiva")))
        End While
        dr.Close()
        Return a
    End Function

    Public Sub Guardar(a As AddendaWalmart)
        Dim comm As New MySqlCommand("select count(idventa) from tbladdendawalmart where idventa=" + a.IdVenta.ToString(), conn)
        comm.Parameters.Add(New MySqlParameter("@fechaordencompra", a.FechaOrden))
        comm.Parameters.Add(New MySqlParameter("@fecharecibo", a.FechaRecibo))
        comm.Parameters.Add(New MySqlParameter("@clavelugarentrega", a.NombreLugarEntrega))
        comm.Parameters.Add(New MySqlParameter("@nombrelugarentrega", a.NombreLugarEntrega))
        comm.Parameters.Add(New MySqlParameter("@cedulaieps", a.CedulaIEPS))
        If comm.ExecuteScalar = 0 Then
            comm.CommandText = "insert into tbladdendawalmart (idventa, folioordencompra, fechaordencompra, foliorecibo, fecharecibo, clavelugarentrega, nombrelugarentrega, cedulaieps) values (" + a.IdVenta.ToString() + "," + If(a.FolioOrden Is Nothing, "null", a.FolioOrden.ToString()) + ",@fechaordencompra," + If(a.FolioRecibo Is Nothing, "null", a.FolioRecibo.ToString()) + ",@fecharecibo,@clavelugarentrega,@nombrelugarentrega,@cedulaieps)"
        Else
            comm.CommandText = "update tbladdendawalmart set folioordencompra = " + If(a.FolioOrden Is Nothing, "null", a.FolioOrden.ToString()) + ", fechaordencompra = @fechaordencompra, foliorecibo = " + If(a.FolioRecibo Is Nothing, "null", a.FolioRecibo.ToString()) + ", fecharecibo = @fecharecibo, clavelugarentrega = @clavelugarentrega, nombrelugarentrega = @nombrelugarentrega, cedulaieps = @cedulaieps where idventa = " + a.IdVenta.ToString()
        End If
        comm.ExecuteNonQuery()
    End Sub
End Class
