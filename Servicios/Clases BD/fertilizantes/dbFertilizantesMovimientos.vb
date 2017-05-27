Public Class dbFertilizantesMovimientos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Id As Integer
    Public Fecha As String
    Public Serie As String
    Public Folio As Integer
    Public FechadeEnvio As String
    Public Idalmacen As Integer
    Public PesoBruto As Double
    Public Tara As Double
    Public PesoNeto As Double
    Public Chofer As String
    Public Inspector As String
    Public FechadeDescarga As String
    Public Hora As String
    Public Tipo As Integer
    Public IdAlmacenDestino As Integer
    Public Vehiculo As String
    Public Surtido As Double
    Public Precio As Double
    Public Estado As Byte
    Public IdPedido As Integer
    Public Comentario As String
    Public PesoBruto2 As Double
    Public FechaCarga As String
    Public HoraCarga As String
    Public FechaLlegada As String
    Public HoraLlegada As String
    Public IdInventario As Integer
    Public Placas As String
    Public Almacen As dbAlmacenes
    Public AlmacenDestino As dbAlmacenes
    Public TipoMovimientoString As String
    Public EstadoString As String
    Public EstadoSurtido As Byte
    Public EstadoSurtidoString As String

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub
    Public Sub LlenaDatos(ByVal pId As Integer)
        Id = pId
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblfertilizantesmovimientos where idmovimiento=" + Id.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Fecha = DReader("fecha")
            Serie = DReader("serie")
            Folio = DReader("folio")
            FechadeEnvio = DReader("fechaenvio")
            Idalmacen = DReader("idalmacen")
            PesoBruto = DReader("pesobruto")
            Tara = DReader("tara")
            PesoNeto = DReader("pesoneto")
            Chofer = DReader("chofer")
            Inspector = DReader("inspector")
            FechadeDescarga = DReader("fechadescarga")
            Hora = DReader("hora")
            Tipo = DReader("tipo")
            IdAlmacenDestino = DReader("idalmacendestino")
            Vehiculo = DReader("vehiculo")
            Surtido = DReader("surtido")
            Precio = DReader("precio")
            Estado = DReader("estado")
            IdPedido = DReader("idpedido")
            Comentario = DReader("comentario")
            PesoBruto2 = DReader("pesobruto2")
            FechaCarga = DReader("fechacarga")
            HoraCarga = DReader("horacarga")
            FechaLlegada = DReader("fechallegada")
            HoraLlegada = DReader("horallegada")
            IdInventario = DReader("idinventario")
            Placas = DReader("placas")
            EstadoSurtido = DReader("estadosurtido")
        End If
        DReader.Close()

        Select Case Tipo
            Case 0
                TipoMovimientoString = "SALIDA"
            Case 1
                TipoMovimientoString = "ENVÍO"
            Case 2
                TipoMovimientoString = "TRASPASO"
            Case 3
                TipoMovimientoString = "DEVOLUCIÓN"
        End Select
        If EstadoSurtido = 0 Then
            EstadoSurtidoString = "EN TRÁNSITO"
        Else
            EstadoSurtidoString = "SURTIDO"
        End If
        If Estado = 3 Then
            EstadoString = "GUARDADA"
        Else
            EstadoString = "CANCELADA"
        End If
        Almacen = New dbAlmacenes(Idalmacen, Comm.Connection)
        AlmacenDestino = New dbAlmacenes(IdAlmacenDestino, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pFecha As String, ByVal pSerie As String, ByVal pfolio As Integer, ByVal pFechaEnvio As String, ByVal pIdalmacen As Integer, ByVal pPesobruto As Double, ByVal pTara As Double, ByVal pPesoNEto As Double, ByVal pChofer As String, ByVal pInspector As String, ByVal pFechaDescarga As String, ByVal pHora As String, ByVal pTipo As Byte, ByVal pIdAlmacenDestino As Integer, ByVal pVehiculo As String, ByVal pSurtido As Double, ByVal pPrecio As Double, ByVal pIdPedido As Integer, pComentario As String, pPesobruto2 As Double, pfechaCarga As String, pHoraCarga As String, pFechaLlegada As String, pHorallegada As String, pPlacas As String, pIdInventario As Integer, pEstadoSurtido As Byte)
        Comm.CommandText = "insert into tblfertilizantesmovimientos(fecha,serie,folio,fechaenvio,idalmacen,pesobruto,tara,pesoneto,chofer,inspector,fechadescarga,hora,tipo,idalmacendestino,vehiculo,surtido,precio,estado,idpedido,comentario,pesobruto2,fechacarga,horacarga,fechallegada,horallegada,placas,idinventario,estadosurtido,fechacancelado,horacancelado,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + _
        "'" + pFecha + "','" + Replace(pSerie, "'", "''") + "'," + pfolio.ToString + ",'" + pFechaEnvio + "'," + pIdalmacen.ToString + "," + pPesobruto.ToString + "," + pTara.ToString + "," + pPesoNEto.ToString + ",'" + Replace(pChofer, "'", "''") + "','" + Replace(pInspector, "'", "''") + "','" + pFechaDescarga + "','" + pHora + "'," + pTipo.ToString + "," + pIdAlmacenDestino.ToString + ",'" + Replace(pVehiculo, "'", "''") + "'," + pSurtido.ToString + "," + pPrecio.ToString + ",3," + pIdPedido.ToString + ",'" + Replace(pComentario, "'", "''") + "'," + pPesobruto2.ToString + ",'" + pfechaCarga + "','" + pHoraCarga + "','" + pFechaLlegada + "','" + pHorallegada + "','" + Replace(pPlacas, "'", "''") + "'," + pIdInventario.ToString + "," + pEstadoSurtido.ToString + ",'" + pFecha + "','" + pHora + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idmovimiento) from tblfertilizantesmovimientos"
        Id = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pId As Integer, ByVal pFecha As String, ByVal pSerie As String, ByVal pfolio As Integer, ByVal pFechaEnvio As String, ByVal pIdalmacen As Integer, ByVal pPesobruto As Double, ByVal pTara As Double, ByVal pPesoNEto As Double, ByVal pChofer As String, ByVal pInspector As String, ByVal pFechaDescarga As String, ByVal pHora As String, ByVal pTipo As Byte, ByVal pIdAlmacenDestino As Integer, ByVal pVehiculo As String, ByVal pPrecio As Double, ByVal pEstado As Byte, pComentario As String, pPesobruto2 As Double, pfechaCarga As String, pHoraCarga As String, pFechaLlegada As String, pHorallegada As String, pPlacas As String, pSurtido As Double, pEstadoSurtido As Byte)
        Comm.CommandText = "update tblfertilizantesmovimientos set fecha='" + pFecha + "',serie='" + Replace(pSerie, "'", "''") + "',folio=" + pfolio.ToString + ",fechaenvio='" + pFechaEnvio + "',idalmacen=" + pIdalmacen.ToString + ",pesobruto=" + pPesobruto.ToString + ",tara=" + pTara.ToString + ",pesoneto=" + pPesoNEto.ToString + ",chofer='" + Replace(pChofer, "'", "''") + "',inspector='" + Replace(pInspector, "'", "''") + "',fechadescarga='" + pFechaDescarga + "',hora='" + pHora + "',tipo=" + pTipo.ToString + ",idalmacendestino=" + pIdAlmacenDestino.ToString + ",vehiculo='" + Replace(pVehiculo, "'", "''") + "',precio=" + pPrecio.ToString + ",estado=" + pEstado.ToString + ",comentario='" + Replace(pComentario, "'", "''") + "',pesobruto2=" + pPesobruto2.ToString + ",fechacarga='" + pfechaCarga + "',horacarga='" + pHoraCarga + "',fechallegada='" + pFechaLlegada + "',horallegada='" + pHorallegada + "',placas='" + Replace(pPlacas, "'", "''") + "',surtido=" + pSurtido.ToString + ",estadosurtido=" + pEstadoSurtido.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idmovimiento=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub CancelarEstado(pidMovimiento As Integer)
        Estado = 4
        Comm.CommandText = "update tblfertilizantesmovimientos set estado=4,fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(Date.Now, "HH:mm") + "' where idmovimiento=" + pidMovimiento.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pId As Integer)
        Comm.CommandText = "delete from tblferilizantesmovimientos where idmovimiento=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdPedido As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select fm.idmovimiento,fm.idinventario,i.clave,i.nombre,case fm.tipo when 0 then 'Salida' when 1 then 'Envío' when 2 then 'Traspaso' when 3 then 'Devolución' end as Tipomov,case fm.estadosurtido when 0 then fm.pesoneto when 1 then fm.surtido end,if(fm.tipo<>2,(select nombre from tblalmacenes where idalmacen=fm.idalmacen),(select nombre from tblalmacenes where idalmacen=fm.idalmacendestino)) as almacen,case fm.estadosurtido when 0 then 'En tránsito' when 1 then 'Surtido' end as estadomov,if(fm.estadosurtido=1,fm.pesoneto-fm.surtido,0) as restante,case fm.estado when 3 then 'G' when 4 then 'C' end as estadog  from tblfertilizantesmovimientos fm inner join tblinventario i on fm.idinventario=i.idinventario  where idpedido=" + pIdPedido.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfermovs")
        Return DS.Tables("tblfermovs").DefaultView
    End Function
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblfertilizantesmovimientos where serie='" + Replace(Trim(pSerie), "'", "''") + "'),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblfertilizantesmovimientos where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function

    Public Sub ModificaInventario(ByVal pId As Integer, ByVal PTipo As Byte)
        'PTipo=5 Salida almacen destino
        'PTipo=6 Entrada almacen destino
        If PTipo = dbInventarioConceptos.Tipos.Entrada Then
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,surtido,0,0,1) from tblfertilizantesmovimientos where idmovimiento=" + pId.ToString + ";"
            'Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
        End If
        If PTipo = 6 Then
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacendestino,surtido,0,0,1) from tblfertilizantesmovimientos where idmovimiento=" + pId.ToString + ";"
            'Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
        End If
        If PTipo = dbInventarioConceptos.Tipos.Salida Then
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,surtido,0,1,1) from tblfertilizantesmovimientos where idmovimiento=" + pId.ToString + ";"
            'Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
        End If
        If PTipo = 5 Then
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacendestino,surtido,0,1,1) from tblfertilizantesmovimientos where idmovimiento=" + pId.ToString + ";"
            'Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
        End If
        If PTipo = dbInventarioConceptos.Tipos.Traspaso Then
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,pesoneto,0,1,1) from tblfertilizantesmovimientos where idmovimiento=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacendestino,pesoneto,0,0,1) from tblfertilizantesmovimientos where idmovimiento=" + pId.ToString + ";"
            'Comm.CommandText += "update tblmovimientosdetalles set surtido=peso where idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
        End If
        If PTipo = dbInventarioConceptos.Tipos.Ajuste Then 'Aqui ajuste se utiliza para cancelar un traspaso
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacendestino,pesoneto,0,1,1) from tblfertilizantesmovimientos where idmovimiento=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,pesoneto,0,0,1) from tblfertilizantesmovimientos where idmovimiento=" + pId.ToString + ";"
            'Comm.CommandText += "update tblmovimientosdetalles set surtido=peso where idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Function ChecaTotalSurtido(pIdPedido As Integer, pIdinventario As Integer) As Double
        Dim iSurtido As Double
        Dim iTotalPedido As Double
        Comm.CommandText = "select ifnull((select sum(surtido) from tblfertilizantesmovimientos where idinventario=" + pIdinventario.ToString + " and idpedido=" + pIdPedido.ToString + " and estado=3 and tipo<>3),0)"
        iSurtido = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(cantidad) from tblfertilizantespedidosdetalles where idinventario=" + pIdinventario.ToString + " and idpedido=" + pIdPedido.ToString + " and afavor=0),0)"
        iTotalPedido = Comm.ExecuteScalar
        Return iTotalPedido - iSurtido
    End Function
    Public Function ReporteMovimientos(pFecha1 As String, pFecha2 As String, pTipo As Byte, pIdCliente As Integer, pEstadoMovimiento As Byte, pIdinventario As Integer, pidSucursal As Integer, pidAlmacen As Integer, PSoloEnvios As Boolean, PsoloDev As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select fm.idmovimiento,fm.fecha,fm.serie,fm.folio,fm.tipo,fm.estadosurtido,fm.pesobruto,fm.tara,fm.pesoneto,fm.pesobruto2,fm.surtido,(select nombre from tblalmacenes where idalmacen=fm.idalmacen) as almancen1,(select nombre from tblalmacenes where idalmacen=fm.idalmacendestino) as almancen2,i.nombre,i.clave,fm.fechaenvio,fm.fechadescarga,fp.serie seriep,fp.folio foliop,tblclientes.nombre,fp.idcliente,fm.idinventario " + _
            "from tblfertilizantesmovimientos fm inner join tblfertilizantespedidos fp on fm.idpedido=fp.idpedido inner join tblinventario i on fm.idinventario=i.idinventario  inner join tblclientes on tblclientes.idcliente=fp.idcliente where fm.fecha>='" + pFecha1 + "' and fm.fecha<='" + pFecha2 + "' and fm.estado=3"
        If PsoloDev Then
            pTipo = 4
        End If
        If pTipo <> 0 Then
            Comm.CommandText += " and fm.tipo=" + (pTipo - 1).ToString
            'Else
            'Comm.CommandText += " and fm.tipo<>3"
        End If
        If PSoloEnvios Then
            Comm.CommandText += " and (fm.tipo=1 or fm.tipo=2)"
        End If
        If pIdCliente <> 0 Then
            Comm.CommandText += " and fp.idcliente=" + pIdCliente.ToString
        End If
        If PSoloEnvios = False Then
            If pEstadoMovimiento <> 0 Then
                Comm.CommandText += " and fm.estadosurtido=" + CStr(pEstadoMovimiento - 1)
            End If
        Else
            Comm.CommandText += " and fm.estadosurtido=1"
        End If
        If pIdinventario <> 0 Then
            Comm.CommandText += " and fm.idinventario=" + pIdinventario.ToString
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and fp.idsucursal=" + pidSucursal.ToString
        End If
        If pidAlmacen > 0 Then
            Comm.CommandText += " and fm.idalmacen=" + pidAlmacen.ToString
        End If
        Comm.CommandText += " order by fm.tipo,fm.serie,fm.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblferrepmovs")
        'DS.WriteXmlSchema("repferrepmovs.xml")
        Return DS.Tables("tblferrepmovs").DefaultView
    End Function
End Class
