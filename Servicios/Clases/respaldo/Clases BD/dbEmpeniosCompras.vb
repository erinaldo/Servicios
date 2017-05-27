Imports MySql.Data.MySqlClient
Public Class dbEmpeniosCompras
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Dim NombreClasificacion As String 'clasificaciones
    Dim IDClasificacion As String 'clasificaciones
    Public Idmovimiento As Integer
    Public Fecha As String
    Public Folio As Integer
    Public Total As Double
    Public Hora As String
    Public Estado As Byte
    Public IdSucursal As Integer
    Public Serie As String
    Public IdCaja As Integer
    Public IdVendedor As Integer
    Public Comentario As String
    Public Tipo As Byte
    Public idCliente As Integer
    Public Cliente As dbClientes
    Public IdForma As Integer
    Public formaPago As String
    Public Pagado As Boolean
    Public fechaContrato As String
    Public tipoEmpenio As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Idmovimiento = -1
        Fecha = ""
        Hora = ""
        Serie = ""
        Folio = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        IdCaja = 1
        Tipo = 0
        idCliente = 0
        IdVendedor = 0
        Comentario = ""

        IdForma = 0
        formaPago = ""
        tipoEmpenio = 0
        fechaContrato = ""
        Pagado = False
        Comm.Connection = Conexion
        Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Idmovimiento = pID
        Comm.Connection = Conexion
        LlenaDatos(Idmovimiento)
    End Sub
    Public Function buscaVendedor() As String
        Comm.CommandText = "select nombre from tblvendedores where idvendedor=" + IdVendedor.ToString
        Return Comm.ExecuteScalar
    End Function

    Public Sub LlenaDatos(ByVal pid As Integer)
        Idmovimiento = pid
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblempenioscompras where idmovimiento=" + Idmovimiento.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            Hora = DReader("hora")
            Estado = DReader("estado")
            Total = DReader("total")
            IdSucursal = DReader("idsucursal")
            Serie = DReader("serie")
            IdCaja = DReader("idcaja")
            IdVendedor = DReader("idvendedor")
            Comentario = DReader("comentario")
            Tipo = DReader("tipo")
            idCliente = DReader("idcliente")
            IdForma = DReader("idforma")
            '  Pagado = DReader("pagado")
            ' fechaContrato = DReader("fechaContrato")
            tipoEmpenio = DReader("tipoEmpenio")
        End If
        DReader.Close()
        Comm.CommandText = "select nombre from tblformasdepagoremisiones where idforma=" + IdForma.ToString
        formaPago = Comm.ExecuteScalar
        Cliente = New dbClientes(idCliente, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pTotal As Double, ByVal pidSucursal As Integer, ByVal pSerie As String, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pTipo As Byte, ByVal pComentario As String, ByVal pDiasPrestamos As Integer, ByVal Pidforma As Integer, ByVal ptipoEmpenio As Integer)
        Fecha = pFecha
        Folio = pFolio
        Total = pTotal
        IdSucursal = pidSucursal
        Serie = pSerie
        IdCaja = pidCaja
        IdVendedor = pIdVendedor
        Comentario = pComentario
        Tipo = pTipo
        idCliente = pIdCliente
        tipoEmpenio = ptipoEmpenio
        Comm.CommandText = "insert into tblempenioscompras(fecha,folio,hora,estado,total,idsucursal,serie,idcaja,idvendedor,comentario,tipo,idcliente,idforma,tipoEmpenio) values('" + Replace(Fecha, "'", "''") + "','" + Folio.ToString + "','" + Format(TimeOfDay, "HH:mm:ss") + "'," + CStr(Estados.SinGuardar) + "," + Total.ToString() + "," + IdSucursal.ToString + ",'" + Replace(Trim(Serie), "'", "''") + "'," + IdCaja.ToString + "," + IdVendedor.ToString + ",'" + Replace(Comentario, "'", "''") + "'," + Tipo.ToString + "," + idCliente.ToString + ",0," + tipoEmpenio.ToString + ");"
        'Comm.ExecuteNonQuery()
        Comm.CommandText += "select max(idmovimiento) from tblempenioscompras;"
        Idmovimiento = Comm.ExecuteScalar
    End Sub

    Public Sub Modificar(ByVal pID As Integer, ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pTotal As Double, ByVal pidSucursal As Integer, ByVal pSerie As String, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pTipo As Byte, ByVal pComentario As String, ByVal pDiasPrestamos As Integer, ByVal Pidforma As Integer, ByVal ptipoEmpenio As Integer, ByVal pEstado As Integer)
        Idmovimiento = pID
        Fecha = pFecha
        Folio = pFolio
        Total = pTotal
        IdSucursal = pidSucursal
        Serie = pSerie
        IdCaja = pidCaja
        IdVendedor = pIdVendedor
        Comentario = pComentario
        Tipo = pTipo
        idCliente = pIdCliente
        tipoEmpenio = ptipoEmpenio
        Comm.CommandText = "update tblempenioscompras set fecha='" + Replace(Fecha, "'", "''") + "',folio='" + Folio.ToString + "',estado=" + Estado.ToString + ",total=" + Total.ToString + ",serie='" + Replace(Trim(Serie), "'", "''") + "',idvendedor=" + IdVendedor.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "',tipo=" + Tipo.ToString + ",idcaja=" + IdCaja.ToString + ",idcliente=" + idCliente.ToString + ",idforma=" + Pidforma.ToString + ", tipoEmpenio=" + tipoEmpenio.ToString + ", estado=" + pEstado.ToString + " where idmovimiento=" + Idmovimiento.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaComentario(ByVal pID As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblempenioscompras set comentario='" + Replace(pTexto, "'", "''") + "' where idmovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub EliminarGasto(ByVal pID As Integer)
        Comm.CommandText = "delete from tblempenioscompras where idmovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pEstado As Byte, ByVal pFolio As String, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal PCliente As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblempenioscompras.idmovimiento,tblempenioscompras.fecha,concat(tblempenioscompras.serie,convert(tblempenioscompras.folio using utf8)),concat(tblclientes.clave,' ',tblclientes.nombre),tblempenioscompras.total,tblcajas.nombre as caja,case tblempenioscompras.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado from tblempenioscompras inner join tblcajas on tblempenioscompras.idcaja=tblcajas.idcaja inner join tblclientes on tblempenioscompras.idcliente=tblclientes.idcliente where tblempenioscompras.fecha>='" + pFecha + "' and tblempenioscompras.fecha<='" + pFecha2 + "'"
        If PCliente <> "" Then
            Comm.CommandText += " and concat(tblclientes.clave,tblclientes.nombre) like '%" + Replace(PCliente, "'", "''") + "%'"
        End If
        If pEstado <> Estados.Inicio Then
            Comm.CommandText += " and tblempenioscompras.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblempenioscompras.estado<>1"
        End If
        'If pTipo > 0 Then
        '    Comm.CommandText += " and tblempenios.tipo=" + CStr(pTipo - 1)
        'End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblempenioscompras.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and tblempenioscompras.idcaja=" + pidCaja.ToString
            End If
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblempenioscompras.serie,convert(tblempenioscompras.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If

        Comm.CommandText += " order by tblempenioscompras.fecha DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempenioscompras")
        Return DS.Tables("tblempenioscompras").DefaultView
    End Function

    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblempenioscompras where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4)),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblempenioscompras where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado<>1 and estado<>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function

    Public Function BuscaMovimiento(ByVal pFolio As String) As Integer
        Comm.CommandText = "select ifnull((select idmovimiento from tblempenioscompras where concat(tblempenioscompras.serie,convert(tblempenioscompras.folio using utf8))='" + Replace(pFolio, "'", "''") + "' and estado=3 limit 1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaId(ByVal pFolio As String) As Integer
        Comm.CommandText = "select ifnull((select idmovimiento from tblempenioscompras where tblempenios.estado=3 and usado=0 and concat(tblempenioscompras.serie,convert(ttblempenioscompras.folio using utf8))='" + Replace(pFolio, "'", "''") + "'),0)"
        Return Comm.ExecuteScalar
    End Function

    Public Function buscaClasificacion(ByVal pidClasificacion As Integer) As String
        Dim Resultado As String
        Comm.CommandText = "select nombre from tblempeniosclasificacion where idClasificacion=" + pidClasificacion.ToString
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function

    'Public Sub ModificarConfiguracion(ByVal pID As Integer, ByVal pA1a30 As Double, ByVal pA31a60 As Double, ByVal pA61a90 As Double, ByVal pA90mas As Double, ByVal pB1a30 As Double, ByVal pB31a60 As Double, ByVal pB61a90 As Double, ByVal pB90mas As Double)

    '    Comm.CommandText = "update tblempenios set A1a30=" + pA1a30.ToString + ",A31a60=" + pA31a60.ToString + ",A61a90=" + pA61a90.ToString + ",A90mas=" + pA90mas.ToString + ",B1a30=" + pB1a30.ToString + ",B31a60=" + pB31a60.ToString + ",B61a90=" + pB61a90.ToString + ",B90mas=" + pB90mas.ToString + " where idmovimiento=" + pID.ToString
    '    Comm.ExecuteNonQuery()
    'End Sub
    'Public Function TienePagos(ByVal pIdEmpenio As Integer) As Boolean
    '    Comm.CommandText = "select count(idempenio) from tblempeniosabono where idempenio=" + pIdEmpenio.ToString
    '    If Comm.ExecuteScalar = 0 Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function


    Public Function filtroTodos(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblempenioscompras.idmovimiento, tblempenioscompras.fecha, CONCAT(tblempenioscompras.serie,' ',tblempenioscompras.folio), tblempenioscompras.total, tblclientes.nombre as cliente, tblvendedores.nombre as vendedor from tblempenios inner join tblclientes on tblempenioscompras.idcliente =tblclientes.idcliente  inner join tblvendedores on tblempenioscompras.idvendedor = tblvendedores.idvendedor where tblempenios.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblempenios.fecha>='" + pFechaI + "' and tblempenios.fecha<='" + pFechaF + "' "
        End If
        If pIDCliente <> 0 Then
            Comm.CommandText += " and tblempenios.idCliente=" + pIDCliente.ToString
        End If
        If pidCaja <> -1 Then
            Comm.CommandText += " and tblempenios.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor <> -1 Then
            Comm.CommandText += " and tblempenios.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal <> -1 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempenios")
        'DS.WriteXmlSchema("tblImpresionEnajenante.xml")
        Return DS.Tables("tblempenios").DefaultView
    End Function


End Class
