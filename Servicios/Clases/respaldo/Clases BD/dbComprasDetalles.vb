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
        Comm.CommandText = "select * from tblcomprasdetalles where iddetalle=" + ID.ToString
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
            'DeRemision = DReader("deremision")
        End If
        DReader.Close()
        Inventario = New dbInventario(Idinventario, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdCompra As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pSiempreNuevo As Boolean, ByVal pIEPS As Double, ByVal pivaRetenido As Double)
        Dim CTemp As Double
        Dim PTemp As Double
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdCompra = pIdCompra
        IdAlmacen = pIdAlmacen
        ivaRetenido = pivaRetenido
        IEPS = pIEPS
        Iva = pIva
        'Extra = pExtra
        Descuento = pDescuento
        Comm.CommandText = "select if(max(cantidad) is null,-1,cantidad) from tblcomprasdetalles where idcompra=" + IdCompra.ToString + " and idinventario=" + Idinventario.ToString
        CTemp = Comm.ExecuteScalar
        If CTemp > -1 And pSiempreNuevo = False Then
            Comm.CommandText = "select max(precio) from tblcomprasdetalles where idcompra=" + IdCompra.ToString + " and idinventario=" + Idinventario.ToString
            PTemp = Comm.ExecuteScalar
            If PTemp <> 0 Then
                Precio = PTemp / CTemp
            Else
                Precio = 0
            End If
            Cantidad += CTemp
            Precio = Precio * Cantidad
            Comm.CommandText = "update tblcomprasdetalles set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + ",iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + ",IEPS=" + IEPS.ToString + ",ivaRetenido=" + ivaRetenido.ToString + " where idinventario=" + Idinventario.ToString + " and idcompra=" + IdCompra.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select iddetalle from tblcomprasdetalles where idinventario=" + Idinventario.ToString + " and idcompra=" + IdCompra.ToString
            ID = Comm.ExecuteScalar
            LlenaDatos()
            NuevoConcepto = False
        Else
            Comm.CommandText = "insert into tblcomprasdetalles(idinventario,cantidad,precio,idmoneda,idcompra,idalmacen,iva,extra,descuento,surtido,costoindirecto, IEPS, ivaRetenido) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMoneda.ToString + "," + IdCompra.ToString + "," + IdAlmacen.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + ",0,0 ," + IEPS.ToString + ", " + ivaRetenido.ToString + ")"
            Comm.ExecuteNonQuery()
            NuevoConcepto = True
            Comm.CommandText = "select if(max(iddetalle) is null,0,max(iddetalle)) from tblcomprasdetalles"
            ID = Comm.ExecuteScalar
        End If


    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pIVa As Double, ByVal pDescuento As Double, ByVal pIEPS As Double, ByVal pivaRetenido As Double)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IEPS = pIEPS
        ivaRetenido = pivaRetenido
        Comm.CommandText = "update tblcomprasdetalles set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",iva=" + pIVa.ToString + ",descuento=" + pDescuento.ToString + " ,IEPS=" + IEPS.ToString + " ,ivaRetenido=" + ivaRetenido.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcomprasdetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdcompra As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblcomprasdetalles.iddetalle,tblcomprasdetalles.cantidad,tblinventario.clave,tblinventario.nombre,tblcomprasdetalles.precio,tblmonedas.abreviatura from tblcomprasdetalles inner join tblinventario on tblcomprasdetalles.idinventario=tblinventario.idinventario inner join tblmonedas on tblcomprasdetalles.idmoneda=tblmonedas.idmoneda where tblcomprasdetalles.idcompra=" + pIdcompra.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasdetalles")
        Return DS.Tables("tblcomprasdetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pidCompra As Integer, pNada As Byte) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,concat(tblinventario.nombre,spdadetalleslotes(tvi.idcompra,tvi.iddetalle,0," + pNada.ToString + "),spdadetallesaduanaotros(tvi.idcompra,tvi.iddetalle,0," + pNada.ToString + ")) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idinventario from tblcomprasdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad where tvi.idcompra=" + pidCompra.ToString
        Return Comm.ExecuteReader
    End Function
    Public Sub QuitaInventarioLotesAduana(pidDetalle As Integer)
        Comm.CommandText = "select spmodificainventariolotesf(tblcompraslotes.idlote,tblcompraslotes.surtido,0,1,1) from tblcompraslotes inner join tblcomprasdetalles on tblcompraslotes.iddetalle=tblcomprasdetalles.iddetalle where tblcomprasdetalles.iddetalle=" + pidDetalle.ToString + ";"
        Comm.CommandText += "select spmodificainventarioaduanaf(tblcomprasaduana.idaduana,tblcomprasaduana.surtido,0,1,1) from tblcomprasaduana inner join tblcomprasdetalles on tblcomprasaduana.iddetalle=tblcomprasdetalles.iddetalle where tblcomprasdetalles.iddetalle=" + pidDetalle.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub
End Class
