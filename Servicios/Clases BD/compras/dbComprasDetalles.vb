Imports MySql.Data.MySqlClient
Public Class dbComprasDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdCompra As Integer
    Public IdAlmacen As Integer
    Public NuevoConcepto As Boolean
    Public Iva As Double
    Public Extra As String
    Public Descuento As Double
    Public Surtido As Double
    Public CostoIndirecto As Double
    Public IEPS As Double
    Public ivaRetenido As Double
    Public Ubicacion As String
    Public Tarima As String
    'Public DeRemision As Byte
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdCompra = 0
        IdAlmacen = 0
        Iva = 0
        Extra = 0
        Descuento = 0
        Surtido = 0

        IEPS = 0
        ivaRetenido = 0
        ' DeRemision = 0
        NuevoConcepto = False
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select cd.*, ifnull(cdu.ubicacion,'') ubicacion, ifnull(cdu.tarima,'') tarima from tblcomprasdetalles cd left outer join tblcomprasubicaciones cdu on cd.iddetalle=cdu.iddetalle where cd.iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdCompra = DReader("idcompra")
            IdAlmacen = DReader("idalmacen")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
            Surtido = DReader("surtido")
            IEPS = DReader("IEPS")
            ivaRetenido = DReader("ivaRetenido")
            CostoIndirecto = DReader("costoindirecto")
            Ubicacion = DReader("ubicacion")
            Tarima = DReader("tarima")
            'DeRemision = DReader("deremision")
        End If
        DReader.Close()
        Inventario = New dbInventario(Idinventario, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdCompra As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pSiempreNuevo As Boolean, ByVal pIEPS As Double, ByVal pivaRetenido As Double, pUbicacion As String, pTarima As String)
        'Dim CTemp As Double
        'Dim PTemp As Double
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdCompra = pIdCompra
        IdAlmacen = pIdAlmacen
        ivaRetenido = pivaRetenido
        IEPS = pIEPS
        Iva = pIva
        Ubicacion = pUbicacion
        Tarima = pTarima
        Descuento = pDescuento
        Comm.CommandText = "insert into tblcomprasdetalles(idinventario, cantidad, precio, idmoneda, idcompra, idalmacen, iva, extra, descuento, surtido, costoindirecto, IEPS, ivaRetenido) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMoneda.ToString + "," + IdCompra.ToString + "," + IdAlmacen.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + ",0,0 ," + IEPS.ToString + ", " + ivaRetenido.ToString + ");"
        Comm.CommandText += "select ifnull(last_insert_id(),0);"
        ID = Comm.ExecuteScalar
        'ubicaciones
        If pUbicacion <> "" Then
            Comm.CommandText = "insert into tblcomprasubicaciones (iddetalle, cantidad, surtido, ubicacion, tarima) values( " + ID.ToString + ", " + Cantidad.ToString() + ", 0, @ubicacion, @tarima);"
            Comm.Parameters.Add(New mysqlparameter("@ubicacion", Ubicacion))
            Comm.Parameters.Add(New MySqlParameter("@tarima", If(Tarima = "", DBNull.Value, Tarima)))
            Comm.ExecuteNonQuery()
            Comm.Parameters.Clear()
        End If

        NuevoConcepto = True

        'End If


    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pIVa As Double, ByVal pDescuento As Double, ByVal pIEPS As Double, ByVal pivaRetenido As Double, pUbicacion As String, pTarima As String)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IEPS = pIEPS
        ivaRetenido = pivaRetenido
        Ubicacion = pUbicacion
        Tarima = pTarima
        Comm.CommandText = "update tblcomprasdetalles set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",iva=" + pIVa.ToString + ",descuento=" + pDescuento.ToString + " ,IEPS=" + IEPS.ToString + " ,ivaRetenido=" + ivaRetenido.ToString + " where iddetalle=" + ID.ToString + "; update tblcomprasubicaciones set cantidad=" + Cantidad.ToString() + " where iddetalle=" + pID.ToString() + ";"
        Comm.ExecuteNonQuery()
        If pUbicacion <> "" Then
            Comm.CommandText = "update tblcomprasubicaciones set ubicacion=@ubicacion, tarima=@tarima, cantidad=" + Cantidad.ToString + " where iddetalle=" + pID.ToString
            Comm.Parameters.Add(New MySqlParameter("@ubicacion", Ubicacion))
            Comm.Parameters.Add(New MySqlParameter("@tarima", If(Tarima = "", DBNull.Value, Tarima)))
            Comm.ExecuteNonQuery()
            Comm.Parameters.Clear()
        End If
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        'checa si la compra estaba guardada
        Comm.CommandText = "select estado from tblcompras c inner join tblcomprasdetalles where iddetalle=" + pID.ToString()
        If Comm.ExecuteScalar = Estados.Guardada Then
            'checa si el detalle tiene ubicacion
            Comm.CommandText = "select ifnull(cu.ubicacion,'') from tblcomprasdetalles cd left outer join tblcomprasubicaciones cu on cd.iddetalle=cu.iddetalle where cd.iddetalle=" + pID.ToString()
            If Comm.ExecuteScalar <> "" Then
                'da de baja el inventario de la ubicacion
                Comm.CommandText = "update tblalmacenesiubicaciones inner join tblcomprasdetalles on tblcomprasdetalles.idalmacen = tblalmacenesiubicaciones.idalmacen and tblcomprasdetalles.idinventario = tblalmacenesiubicaciones.idinventario inner join tblcomprasubicaciones on tblcomprasdetalles.iddetalle=tblcomprasubicaciones.iddetalle and tblcomprasubicaciones.ubicacion=tblalmacenesiubicaciones.ubicacion set tblalmacenesiubicaciones.cantidad = tblalmacenesiubicaciones.cantidad-tblcomprasubicaciones.surtido where tblcomprasubicaciones.iddetalle = " + pID.ToString() + ";"
                Comm.ExecuteNonQuery()
            End If
        End If
        Comm.CommandText = "delete from tblcomprasdetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdcompra As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select cd.iddetalle, cd.cantidad, i.clave, i.nombre, case cd.cantidad when 0 then 0 else cd.precio/cd.cantidad end precio, cd.precio importe, m.abreviatura from tblcomprasdetalles cd inner join tblinventario i on cd.idinventario=i.idinventario inner join tblmonedas m on cd.idmoneda=m.idmoneda where cd.idcompra=" + pIdcompra.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasdetalles")
        Return DS.Tables("tblcomprasdetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pidCompra As Integer, pNada As Byte) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,concat(tblinventario.nombre,spdadetalleslotes(tvi.idcompra,tvi.iddetalle,0," + pNada.ToString + "),spdadetallesaduanaotros(tvi.idcompra,tvi.iddetalle,0," + pNada.ToString + ")) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idinventario,tvi.descuento from tblcomprasdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad where tvi.idcompra=" + pidCompra.ToString
        Return Comm.ExecuteReader
    End Function
    Public Sub QuitaInventarioLotesAduana(pidDetalle As Integer)
        Comm.CommandText = "select spmodificainventariolotesf(tblcomprasdetalles.idinventario,tblcomprasdetalles.idalmacen,tblcompraslotes.surtido,0,1,1,tblcompraslotes.idlote) from tblcompraslotes inner join tblcomprasdetalles on tblcompraslotes.iddetalle=tblcomprasdetalles.iddetalle where tblcomprasdetalles.iddetalle=" + pidDetalle.ToString + ";"
        Comm.CommandText += "select spmodificainventarioaduanaf(tblcomprasdetalles.idinventario,tblcomprasdetalles.idalmacen,tblcomprasaduana.surtido,0,1,1,tblcomprasaduana.idaduana) from tblcomprasaduana inner join tblcomprasdetalles on tblcomprasaduana.iddetalle=tblcomprasdetalles.iddetalle where tblcomprasdetalles.iddetalle=" + pidDetalle.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub
End Class
