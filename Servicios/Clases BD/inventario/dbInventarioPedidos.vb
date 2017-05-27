Public Class dbInventarioPedidos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public IdPedido As Integer
    Public Fecha As String
    Public IdSucursalA As Integer
    Public IdSucursalB As Integer
    Public Serie As String
    Public Folio As Integer
    Public Estado As Byte
    Public Comentario As String
    Public Total As Double
    Public Hora As String
    Public FechaCancelado As String
    Public HoraCancelado As String
    Public Tipo As Byte

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
        Inicializar()
    End Sub
    Public Sub New(pIdPedido As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
        Inicializar()
        LlenaDatos(pIdPedido)
    End Sub
    Public Sub Inicializar()
        IdPedido = 0
        Fecha = ""
        IdSucursalA = 0
        IdSucursalB = 0
        Serie = ""
        Folio = 0
        Estado = 0
        Comentario = ""
        Total = 0
        Tipo = 0
    End Sub
    Public Function DaNuevoFolio(ByVal pSerie As String) As Integer
        Comm.CommandText = "select ifnull((select folio from tblinventariopedidos where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado>=3) order by folio desc limit 1),0)"
        Return Comm.ExecuteScalar + 1        
    End Function
    Public Sub Guardar(pFecha As String, pSerie As String, pFolio As Integer, pIdSucursalA As Integer, pIdSucursalB As Integer, pTipo As Byte)
        Comm.CommandText = "insert into tblinventariopedidos(fecha,idsucursala,idsucursalb,serie,folio,estado,comentario,total,hora,fechacancelado,horacancelado,idusuarioalta,fechaalta,horaalta,idusuariocambio,fechacambio,horacambio,tipo) " +
            "values('" + pFecha + "'," + pIdSucursalA.ToString + "," + pIdSucursalB.ToString + ",'" + Replace(pSerie.Trim, "'", "''") + "'," + pFolio.ToString + ",1,'',0,'" + Date.Now.ToString("HH:mm:ss") + "','" + pFecha + "','" + Date.Now.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + pTipo.ToString + ");"
        Comm.CommandText += "select ifnull(last_insert_id(),0);"
        IdPedido = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(pIdPedido As Integer, pFecha As String, pSerie As String, pFolio As Integer, pIdSucursalA As Integer, pIdSucursalB As Integer, pTotal As Double, pComentario As String, pEstado As Byte, pTipo As Byte)
        If Estado = 3 Then
            Comm.CommandText = "update tblinventariopedidos set" +
                " fecha='" + pFecha + "',idsucursala=" + pIdSucursalA.ToString + ",idsucursalb=" + pIdSucursalB.ToString + ",serie='" + Replace(pSerie.Trim, "'", "''") + "',folio=" + pFolio.ToString + ",estado=" + pEstado.ToString + ",comentario='" + Replace(pComentario.Trim, "'", "''") + "',total=" + pTotal.ToString + ",hora='" + Date.Now.ToString("HH:mm:ss") + "',idusuariocambio=" + GlobalIdUsuario.ToString + ",fechacambio='" + Date.Now.ToString("yyyy/MM/dd") + "',horacambio='" + Date.Now.ToString("HH:mm:ss") + "',tipo=" + pTipo.ToString + " where idpedido=" + pIdPedido.ToString + ";"
        Else
            Comm.CommandText = "update tblinventariopedidos set" +
                " fecha='" + pFecha + "',idsucursala=" + pIdSucursalA.ToString + ",idsucursalb=" + pIdSucursalB.ToString + ",serie='" + Replace(pSerie.Trim, "'", "''") + "',folio=" + pFolio.ToString + ",estado=" + pEstado.ToString + ",comentario='" + Replace(pComentario.Trim, "'", "''") + "',total=" + pTotal.ToString + ",fechacancelado='" + Date.Now.ToString("yyyy/MM/dd") + "',horacancelado='" + Date.Now.ToString("HH:mm:ss") + "',idusuariocambio=" + GlobalIdUsuario.ToString + ",fechacambio='" + Date.Now.ToString("yyyy/MM/dd") + "',horacambio='" + Date.Now.ToString("HH:mm:ss") + "',tipo=" + pTipo.ToString + " where idpedido=" + pIdPedido.ToString + ";"
        End If
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(pIdPedido As Integer)
        Comm.CommandText = "delete from tblinventariopedidos where idpedido=" + pIdPedido.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblinventariopedidos where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pFolio As String, pEstado As Byte, ByVal pIdSucursalA As Integer, pIdSucursalB As Integer, pTipo As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idpedido,fecha,concat(serie,convert(folio using utf8)),(select nombre from tblsucursales where idsucursal=idsucursala) as solicitante,(select nombre from tblsucursales where idsucursal=idsucursalb) as surtidor,case estado when 2 then 'PENDIENTE' when 3 then 'SIN AUTORIZAR' when 4 then 'CANCELADO' when 5 then 'AUTORIZADO' when 6 then 'SURTIDO' end,case tipo when 0 then 'NORMAL' when 1 then 'URGENTE' end from tblinventariopedidos where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pFolio <> "" Then
            Comm.CommandText += " and concat(serie,convert(folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado > 0 Then
            Comm.CommandText += " and estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and estado>1"
        End If
        If pIdSucursalA > 0 Then
            Comm.CommandText += " and idsucursala=" + pIdSucursalA.ToString
        End If
        If pIdSucursalB > 0 Then
            Comm.CommandText += " and idsucursalb=" + pIdSucursalB.ToString
        End If
        If pTipo >= 0 Then
            Comm.CommandText += " and tipo=" + pTipo.ToString
        End If
        Comm.CommandText += " order by tipo desc,fecha desc,serie,folio desc"
        Comm.CommandTimeout = 10000
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpedidos")
        Return DS.Tables("tblpedidos").DefaultView
    End Function
    Public Sub LlenaDatos(pIdPedido As Integer)
        IdPedido = pIdPedido
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblinventariopedidos where idpedido=" + pIdPedido.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read Then
            Fecha = DReader("fecha")
            IdSucursalA = DReader("idsucursala")
            IdSucursalB = DReader("idsucursalb")
            Serie = DReader("serie")
            Folio = DReader("folio")
            Estado = DReader("estado")
            Comentario = DReader("comentario")
            Total = DReader("total")
            Tipo = DReader("tipo")
        End If
        DReader.Close()
    End Sub
    Public Function Datotal(pIdpedido) As Double
        Comm.CommandText = "select ifnull((select sum(round(precio,2)) from tblinventariopedidosdetalles where idpedido=" + pIdpedido.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaAlmacenMovsSucursal(pidsucursal As Integer) As Integer
        Dim IdAlmacenM As Integer
        Comm.CommandText = "select idalmacenm from tblsucursales where idsucursal=" + pidsucursal.ToString
        IdAlmacenM = Comm.ExecuteScalar
        If IdAlmacenM = 0 Then
            Comm.CommandText = "select idalmacen from tblalmacenes where idsucursal=" + pidsucursal.ToString + " limit 1"
            IdAlmacenM = Comm.ExecuteScalar
        End If
        Return IdAlmacenM
    End Function
    Public Function DaConceptoTraspaso(pIdSucursal As Integer) As Integer
        Comm.CommandText = "select idconcepto from tblinventarioconceptos where tipo=3 and idsucursal=" + pIdSucursal.ToString + " limit 1"
        Return Comm.ExecuteScalar
    End Function
    Public Function TieneMovimientos(pIdPedido As Integer) As Boolean
        Comm.CommandText = "select count(idpedido) from tblmovimientos where idpedido=" + pIdPedido.ToString + " and estado=3"
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
