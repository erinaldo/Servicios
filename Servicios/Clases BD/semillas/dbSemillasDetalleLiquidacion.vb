Imports MySql.Data.MySqlClient
Public Class dbSemillasDetalleLiquidacion

#Region "propiedades"
    Public Property id As Integer = -1
    Public Property boleta As dbSemillasBoleta
    Public Property liquidacion As dbSemillasLiquidacion
    Public Property importe As Double
    Public Property guardado As Boolean

#End Region

#Region "constructores"
    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer)
        Me.id = id
    End Sub

    Public Sub New(ByVal id As Integer, ByRef boleta As dbSemillasBoleta, ByRef liquidacion As dbSemillasLiquidacion, ByVal importe As Double, ByVal guardado As Boolean)
        Me.id = id
        Me.boleta = boleta
        Me.liquidacion = liquidacion
        Me.importe = importe
        Me.guardado = guardado
    End Sub

    Public Sub New(ByRef boleta As dbSemillasBoleta, ByRef liquidacion As dbSemillasLiquidacion, ByVal importe As Double, ByVal guardado As Boolean)
        Me.boleta = boleta
        Me.liquidacion = liquidacion
        Me.importe = importe
        Me.guardado = guardado
    End Sub
#End Region
    Private comm As MySqlCommand

    Public Sub New(ByVal conexion As MySqlConnection)
        comm = New MySqlCommand
        comm.Connection = conexion
    End Sub

    Public Function obten(ByVal detalle As dbSemillasDetalleLiquidacion) As dbSemillasDetalleLiquidacion
        comm.CommandText = "select * from tblsemillasliquidaciondetalle where id=" + detalle.id.ToString() + ";"
        Dim dr As MySqlDataReader
        Dim d As New dbSemillasDetalleLiquidacion
        dr = comm.ExecuteReader
        While dr.Read()
            d.id = dr("id")
            d.importe = dr("importe")
            d.liquidacion = New dbSemillasLiquidacion(Convert.ToInt32(dr("idliquidacion")))
            d.boleta = New dbSemillasBoleta(Integer.Parse(dr("idboleta")))
            d.guardado = dr("guardado")
        End While
        Return d
    End Function

    Public Sub agregar(detalle As dbSemillasDetalleLiquidacion)
        comm.CommandText = "insert into tblsemillasliquidaciondetalle(idBoleta,idLiquidacion,importe,guardado) values(" +
            detalle.boleta.id.ToString() + "," +
            detalle.liquidacion.id.ToString() + "," +
            detalle.importe.ToString() + "," + detalle.guardado.ToString() + ");"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub eliminar(detalle As dbSemillasDetalleLiquidacion)
        comm.CommandText = "delete from tblsemillasliquidaciondetalle where id=" + detalle.id.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Function vistaDetallesPorLiquidacion(ByVal liquidacion As dbSemillasLiquidacion) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select b.folio as folio,b.fecha as fecha,prov.nombre as productor,prod.nombre as producto,b.importe as importe from tblsemillasliquidaciondetalle as d" +
            " inner join tblsemillasboletas as b on d.idboleta=b.id inner join tblinventario as prod on prod.idinventario=b.producto " +
            " inner join tblproveedores as prov on prov.idproveedor=b.productor where d.idLiquidacion=" + liquidacion.id.ToString() + ";"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "detalles")
        Return ds.Tables("detalles").DefaultView
    End Function

    Public Function listaDetalles() As List(Of dbSemillasDetalleLiquidacion)
        Dim lista As New List(Of dbSemillasDetalleLiquidacion)
        comm.CommandText = "select * from tblsemillasliquidaciondetalle;"
        Dim dr As MySqlDataReader
        Dim d As dbSemillasDetalleLiquidacion
        dr = comm.ExecuteReader
        While dr.Read()
            d = New dbSemillasDetalleLiquidacion
            d.id = dr("id")
            d.importe = dr("importe")
            d.liquidacion = New dbSemillasLiquidacion(Convert.ToInt32(dr("idliquidacion")))
            d.boleta = New dbSemillasBoleta(Integer.Parse(dr("idboleta")))
            d.guardado = dr("guardado")
            lista.Add(d)
        End While
        Return lista
    End Function

    Public Function sumaBoletas(ByVal liquidacion As dbSemillasLiquidacion, ByVal liquidada As Boolean) As Double
        Dim res As Double
        comm.CommandText = "select ifnull(sum(b.importe),0) as importe from tblsemillasboletas as b " +
            "inner join tblsemillasliquidaciondetalle as ld on ld.idboleta=b.id inner join tblsemillasliquidacion as liq " +
            "on ld.idliquidacion=liq.id where liq.id=" + liquidacion.id.ToString() + " and b.liquidada=" + liquidada.ToString() + ";"
        res = comm.ExecuteScalar
        Return res
    End Function

    Public Function sumaPesoBruto(ByVal idLiquidacion As Integer) As Double
        comm.CommandText = "select ifnull(sum(b.peso),0) as peso from tblsemillasboletas as b inner join tblsemillasliquidaciondetalle as d on d.idBoleta=b.id where d.idLiquidacion=" + idLiquidacion.ToString() + ";"
        Dim res As Double = comm.ExecuteScalar
        Return res
    End Function

    Public Function sumaPesoAnalizado(ByVal idLiquidacion As Integer) As Double
        comm.CommandText = "select ifnull(sum(b.pesoanalizado),0) as peso from tblsemillasboletas as b inner join tblsemillasliquidaciondetalle as d on d.idBoleta=b.id where d.idLiquidacion=" + idLiquidacion.ToString() + ";"
        Dim res As Double = comm.ExecuteScalar
        Return res
    End Function

    Public Sub borraSinGuardar(ByVal idLiquidacion As Integer)
        comm.CommandText = "update tblsemillasboletas as b inner join tblsemillasliquidaciondetalle as d on b.id=d.idboleta set b.liquidada=false where d.idliquidacion=" + idLiquidacion.ToString() + " and d.guardado=false;"
        comm.ExecuteNonQuery()
        comm.CommandText = "delete from tblsemillasliquidaciondetalle where guardado=false and idliquidacion=" + idLiquidacion.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub guardar(ByVal idLiquidacion As Integer)
        comm.CommandText = "update tblsemillasliquidaciondetalle set guardado=true where idLiquidacion=" + idLiquidacion.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub
End Class
