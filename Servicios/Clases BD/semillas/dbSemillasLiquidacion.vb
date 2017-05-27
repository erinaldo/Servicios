Imports MySql.Data.MySqlClient
Public Class dbSemillasLiquidacion
#Region "propiedes"
    Public Property id As Integer = -1
    Public Property subtotal As Double = 0
    Public Property total As Double = 0
    Public Property proveedor As dbproveedores
    Public Property folio As String = ""
    Public Property serie As String = ""
    Public Property estado As Integer
    Public Property idSucursal As Integer = -1

    Public Property fecha As String

#End Region

#Region "constructores"
    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer)
        Me.id = id
    End Sub

    Public Sub New(ByVal id As Integer, ByVal subtotal As Double, ByVal total As Double, ByRef proveedor As dbproveedores, ByVal folio As String, ByVal serie As String, ByVal estado As Integer, ByVal idsucursal As Integer, ByVal fecha As String)
        Me.id = id
        Me.subtotal = subtotal
        Me.total = total
        Me.proveedor = proveedor
        Me.folio = folio
        Me.serie = serie
        Me.estado = estado
        Me.idSucursal = idsucursal
        Me.fecha = fecha
    End Sub

    Public Sub New(ByVal subtotal As Double, ByVal total As Double, ByRef proveedor As dbproveedores, ByVal folio As String, ByVal serie As String, ByVal estado As Integer, ByVal idSucursal As Integer)
        Me.subtotal = subtotal
        Me.total = total
        Me.proveedor = proveedor
        Me.folio = folio
        Me.serie = serie
        Me.estado = estado
        Me.idSucursal = idSucursal
    End Sub

    Public Sub New(ByVal subtotal As Double, ByVal total As Double, ByRef proveedor As dbproveedores, ByVal folio As String, ByVal serie As String, ByVal idSucursal As Integer)
        Me.subtotal = subtotal
        Me.total = total
        Me.proveedor = proveedor
        Me.folio = folio
        Me.serie = serie
        Me.idSucursal = idSucursal
    End Sub

    Public Overrides Function ToString() As String
        Return folio
    End Function

#End Region
    Private comm As MySqlCommand

    Public Sub New(ByVal conexion As MySqlConnection)
        comm = New MySqlCommand
        comm.Connection = conexion
    End Sub

    Public Function obten(ByVal liquidacion As dbSemillasLiquidacion) As dbSemillasLiquidacion
        comm.CommandText = "select * from tblsemillasliquidacion where id=" + liquidacion.id.ToString() + ";"
        Dim dr As MySqlDataReader
        Dim l As New dbSemillasLiquidacion
        dr = comm.ExecuteReader
        If dr.HasRows() Then
            While dr.Read()
                l.id = dr("id")
                l.subtotal = dr("subtotal")
                l.total = dr("total")
                l.proveedor = New dbproveedores(MySqlcon)
                l.proveedor.ID = dr("idProveedor")
                l.folio = dr("folio")
                l.serie = dr("serie")
                If IsDBNull(dr("estado")) Then
                    l.estado = Estados.Inicio
                Else
                    l.estado = dr("estado")
                End If
                If IsDBNull(dr("sucursal")) Then
                    l.idSucursal = GlobalIdSucursalDefault
                Else
                    l.idSucursal = dr("sucursal")
                End If
                If IsDBNull(dr("fecha")) Then
                    l.fecha = ""
                Else
                    l.fecha = dr("fecha")
                End If
            End While
            dr.Close()
            Return l
        Else
            dr.Close()
            Return Nothing
        End If
    End Function
    Public Function obten(ByVal serie As String, ByVal folio As String) As dbSemillasLiquidacion
        comm.CommandText = "select * from tblsemillasliquidacion where concat(serie,folio)='" + (serie + folio) + "';"
        Dim dr As MySqlDataReader
        Dim l As New dbSemillasLiquidacion
        dr = comm.ExecuteReader
        If dr.HasRows() Then
            While dr.Read()
                l.id = dr("id")
                l.subtotal = dr("subtotal")
                l.total = dr("total")
                l.proveedor = New dbproveedores(MySqlcon)
                l.proveedor.ID = dr("idProveedor")
                l.folio = dr("folio")
                l.serie = dr("serie")
                l.estado = dr("estado")
                If IsDBNull(dr("sucursal")) Then
                    l.idSucursal = GlobalIdSucursalDefault
                Else
                    l.idSucursal = dr("sucursal")
                End If
                If IsDBNull(dr("fecha")) Then
                    l.fecha = ""
                Else
                    l.fecha = dr("fecha")
                End If
            End While
            dr.Close()
            Return l
        Else
            dr.Close()
            Return Nothing
        End If
    End Function

    Public Sub agregar(ByVal liquidacion As dbSemillasLiquidacion)
        comm.CommandText = "insert into tblsemillasliquidacion(subtotal,total,idProveedor,folio,serie,estado,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) value(" +
            liquidacion.subtotal.ToString() + "," +
            liquidacion.total.ToString() + "," +
            liquidacion.proveedor.ID.ToString() + "," +
            liquidacion.folio.ToString() + "," +
            "'" + liquidacion.serie + "'," + liquidacion.estado.ToString() + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub actualizar(ByVal liquidacion As dbSemillasLiquidacion)
        comm.CommandText = "update tblsemillasliquidacion set subtotal=" + liquidacion.subtotal.ToString() + ", " +
            "total=" + liquidacion.total.ToString() + ", " +
            "idProveedor=" + liquidacion.proveedor.ID.ToString() + ", estado=" + liquidacion.estado.ToString() +
            ", sucursal=" + liquidacion.idSucursal.ToString() + ", serie='" + liquidacion.serie + "', " +
            "folio=" + liquidacion.folio.ToString() + ", fecha='" + liquidacion.fecha + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where id=" + liquidacion.id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("error: " + ex.ToString())
        End Try
    End Sub

    Public Sub eliminar(ByVal liquidacion As dbSemillasLiquidacion)
        comm.CommandText = "update tblsemillasboletas as b inner join tblsemillasliquidaciondetalle as d on b.id=d.idboleta set b.liquidada=false where d.idliquidacion=" + liquidacion.id.ToString() + ";"
        comm.ExecuteNonQuery()
        comm.CommandText = "delete from tblsemillasliquidaciondetalle where idliquidacion=" + liquidacion.id.ToString() + ";"
        comm.ExecuteNonQuery()
        comm.CommandText = "delete from tblsemillasliquidacion where id=" + liquidacion.id.ToString() + ";"
        comm.ExecuteNonQuery()
        comm.CommandText = "delete from tblsemillasanticipos where idliquidacion=" + liquidacion.id.ToString() + ";"
        comm.ExecuteNonQuery()
        comm.CommandText = "delete from tblsemillasanticiposfacturas where idliquidacion=" + liquidacion.id.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub


    Public Function listaLiquidaciones() As List(Of dbSemillasLiquidacion)
        Dim lista As New List(Of dbSemillasLiquidacion)
        comm.CommandText = "select * from tblsemillasliquidacion;"
        Dim dr As MySqlDataReader
        Dim l As dbSemillasLiquidacion
        dr = comm.ExecuteReader
        If dr.HasRows() Then
            While dr.Read()
                l = New dbSemillasLiquidacion
                l.id = dr("id")
                l.subtotal = dr("subtotal")
                l.total = dr("total")
                l.proveedor = New dbproveedores(dr("idProveedor"), MySqlcon)
                l.folio = dr("folio")
                l.serie = dr("serie")
                l.estado = dr("estado")
                l.idSucursal = dr("sucursal")
                lista.Add(l)
            End While
            dr.Close()
            Return lista
        Else
            dr.Close()
            Return Nothing
        End If
    End Function

    Public Function vistaLiquidaciones() As DataView
        Dim ds As New DataSet
        comm.CommandText = "select distinct l.id as id, concat(l.serie,l.folio) as folio, prov.nombre as productor, prod.nombre as producto,format(l.total,2) as total, case l.estado when 0 then 'INICIO' when 1 then 'SIN GUARDAR' when 2 then 'PENDIENTE' when 3 then 'GUARDADA' when 4 then 'CANCELADA' end as estado " +
            "from tblsemillasliquidacion as l inner join tblsemillasliquidaciondetalle as d on d.idliquidacion=l.id " +
            "inner join tblsemillasboletas as b on d.idboleta=b.id inner join tblinventario as prod " +
            "on b.producto=prod.idinventario inner join tblproveedores as prov on b.productor=prov.idproveedor"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "liquidaciones")
        Return ds.Tables("liquidaciones").DefaultView
    End Function

    Public Function obtenFolio() As Integer
        comm.CommandText = "select max(id) as id from tblsemillasliquidacion;"
        Return comm.ExecuteScalar + 1
    End Function

    Public Function liquidacionesProductor(ByVal proveedor As dbproveedores) As DataView
        comm.CommandText = "select l.id as id, l.folio as folio, prov.nombre as productor, l.total as total " +
            "from tblsemillasliquidacion as l inner join tblproveedores as prov on l.idproveedor=prov.idproveedor " +
            "where prov.idproveedor=" + proveedor.ID.ToString() + ";"
        Dim da As New MySqlDataAdapter(comm)
        Dim ds As New DataSet
        da.Fill(ds, "liquidaciones")
        Return ds.Tables("liquidaciones").DefaultView
    End Function

    Public Function listaBoleta(ByVal liquidacion As dbSemillasLiquidacion, ByVal liquidada As Boolean) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select b.id as id,b.folio as folio, b.fecha as fecha,prod.nombre as producto, b.pesoanalizado as peso,b.precio as precio, b.importe as importe " +
            "from tblsemillasliquidaciondetalle as d inner join tblsemillasboletas as b on d.idboleta=b.id inner join tblproveedores as prov on b.productor=prov.idproveedor " +
            "inner join tblinventario as prod on b.producto=prod.idinventario inner join tblsemillasliquidacion as liq on d.idliquidacion=liq.id where liq.id=" + liquidacion.id.ToString() + " and b.liquidada=" + liquidada.ToString() + ";"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "boletas")
        Return ds.Tables("boletas").DefaultView
    End Function

    Public Function ultimaLiquiacion() As Integer
        Dim id As Integer
        comm.CommandText = "select max(id) from tblsemillasliquidacion"
        id = comm.ExecuteScalar
        Return id
    End Function

    Public Function totalBoletasLiquidadas(ByVal liquidacion As dbSemillasLiquidacion) As Double
        comm.CommandText = "select ifnull(sum(b.importe),0) as total from tblsemillasboletas as b inner join tblsemillasliquidaciondetalle as d " +
            "on b.id=d.idboleta where d.idliquidacion=" + liquidacion.id.ToString() + ";"
        Dim res As Double = comm.ExecuteScalar
        Return res
    End Function

    Public Function filtroLiquidaciones(ByVal claveProducto As String, ByVal claveProductor As String, pFecha1 As String, pFecha2 As String, pFolio As String) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select l.id as id,l.fecha, l.folio as folio, prov.nombre as productor, prod.nombre as producto, l.total as total, case l.estado when 0 then 'INICIO' when 1 then 'SIN GUARDAR' when 2 then 'PENDIENTE' when 3 then 'GUARDADA' when 4 then 'CANCELADA' end as estado " +
            "from tblsemillasliquidacion as l inner join tblsemillasliquidaciondetalle as d on d.idliquidacion=l.id " +
            "inner join tblsemillasboletas as b on d.idboleta=b.id inner join tblinventario as prod " +
            "on b.producto=prod.idinventario inner join tblproveedores as prov on b.productor=prov.idproveedor where l.fecha>='" + pFecha1 + "' and l.fecha<='" + pFecha2 + "'"
        If claveProductor <> "" Then
            comm.CommandText += " and prov.clave like '%" + claveProductor.Replace("'", "''") + "'"
        End If
        If claveProducto <> "" Then
            comm.CommandText += " and prod.clave like'%" + claveProducto.Replace("'", "''") + "'"
        End If
        If pFolio <> "" Then
            comm.CommandText += " and concat(l.serie,l.folio) like '%" + folio + "%'"
        End If
        comm.CommandText += " group by l.id order by l.fecha desc,l.folio desc"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "liquidaciones")
        Return ds.Tables("liquidaciones").DefaultView
    End Function

    Public Function buscarPorFolio(ByVal folio As String) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select distinct l.id as id, concat(l.serie,l.folio) as folio, prov.nombre as productor, prod.nombre as producto, l.total as total,case l.estado when 0 then 'INICIO' when 1 then 'SIN GUARDAR' when 2 then 'PENDIENTE' when 3 then 'GUARDADA' when 4 then 'CANCELADA' end as estado " +
            "from tblsemillasliquidacion as l inner join tblsemillasliquidaciondetalle as d on d.idliquidacion=l.id " +
            "inner join tblsemillasboletas as b on d.idboleta=b.id inner join tblinventario as prod " +
            "on b.producto=prod.idinventario inner join tblproveedores as prov on b.productor=prov.idproveedor where concat(l.serie,l.folio) like '%" + folio + "%';"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "liquidaciones")
        Return ds.Tables("liquidaciones").DefaultView
    End Function

    Public Function DaNuevoFolio(ByVal pSerie As String) As Integer
        comm.CommandText = "select ifnull((select max(folio) from tblsemillasliquidacion where serie='" + Replace(Trim(pSerie), "'", "''") + "'),0)"
        Return comm.ExecuteScalar + 1
    End Function

    Public Function sumaBoletas(ByVal idLiquidacion As Integer, ByVal liquidada As Boolean) As Double
        comm.CommandText = "select ifnull(sum(b.importe),0) as total from tblsemillasboletas as b inner join tblsemillasliquidaciondetalle as d on b.id=d.idBoleta " +
            "where d.idLiquidacion=" + idLiquidacion.ToString() + " and b.liquidada=" + liquidada.ToString() + ";"
        Dim res As Double = comm.ExecuteScalar
        Return res
    End Function

    Public Function checaFolioRepetido(ByVal serie As String, ByVal folio As Integer) As Boolean
        comm.CommandText = "select serie,folio from tblsemillasliquidacion where serie='" + serie + "' and folio=" + folio.ToString() + ";"
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        If dr.HasRows() Then
            dr.Close()
            Return True
        Else
            dr.Close()
            Return False
        End If
    End Function

    Public Function boletas(ByVal idLiquidacion As Integer) As MySqlDataReader
        Dim dr As MySqlDataReader
        comm.CommandText = "select b.fecha,b.folio,round(b.peso,3) as peso,b.castigohumedad as humedad,b.castigogranoQ as granoQuebrado,b.castigogranoD as granoDanado,b.castigoimpurezas as impurezas,b.pesoanalizado as pesoanalizado,round(b.importe,2) as importe,b.precio as precio,b.castigototal from tblsemillasboletas as b inner join tblsemillasliquidaciondetalle as d on d.idboleta=b.id where d.idliquidacion=" + idLiquidacion.ToString()
        dr = comm.ExecuteReader
        Return dr
    End Function

    Public Function facturas(ByVal idliquidacion As Integer, ByVal guardado As Boolean) As MySqlDataReader
        Dim dr As MySqlDataReader
        comm.CommandText = "select v.folio,d.importe from tblsemillasanticiposfacturas as d inner join tblventas as v on d.idfactura=v.idventa where d.idliquidacion =" + idliquidacion.ToString() + " and guardado=" + guardado.ToString() + ";"
        dr = comm.ExecuteReader
        Return dr
    End Function

    Public Function anticipos(ByVal idLiquidacion As Integer) As MySqlDataReader
        Dim dr As MySqlDataReader
        comm.CommandText = "select * from tblsemillasanticipos where idLiquidacion=" + idLiquidacion.ToString() + " and guardado=true;"
        dr = comm.ExecuteReader
        Return dr
    End Function

    Public Function productoLiquidacion(ByVal idLiquidacion As Integer) As Integer
        comm.CommandText = "select inv.idinventario from tblinventario as inv inner join tblsemillasboletas as b on inv.idinventario=b.producto " +
            "inner join tblsemillasliquidaciondetalle as d on d.idboleta=b.id where d.idliquidacion=" + idLiquidacion.ToString + " limit 1;"
        Return comm.ExecuteScalar
    End Function

    Public Sub eliminarBoletaLiquidacion(ByVal idboleta As Integer, ByVal idliquidacion As Integer)
        comm.CommandText = "update tblsemillasboletas as b inner join tblsemillasliquidaciondetalle as d on b.id=d.idboleta set b.liquidada=false where d.idliquidacion=" + idliquidacion.ToString() + " and d.idboleta=" + idboleta.ToString() + ";"
        comm.ExecuteNonQuery()
        comm.CommandText = "delete from tblsemillasliquidaciondetalle where idboleta=" + idboleta.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub eliminaSinGuardar()
        comm.CommandText = "delete from tblsemillasliquidacion where estado=" + CInt(Estados.Inicio).ToString() + " or estado=" + CInt(Estados.SinGuardar).ToString() + ";"
        comm.ExecuteNonQuery()
        comm.CommandText = "delete from tblsemillasanticipos where guardado=0;"
        comm.ExecuteNonQuery()
        comm.CommandText = "delete from tblsemillasanticiposfacturas where guardado=0;"
        comm.ExecuteNonQuery()
    End Sub
End Class
