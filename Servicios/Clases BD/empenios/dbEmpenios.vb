Imports MySql.Data.MySqlClient
Public Class dbEmpenios
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Dim NombreClasificacion As String 'clasificaciones
    Dim IDClasificacion As String 'clasificaciones
    Public ID As Integer
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
    Public diasPrestamo As Integer
    Public TotalAux As Double
    Public A1a30 As Double
    Public A31a60 As Double
    Public A61a90 As Double
    Public A90mas As Double
    Public B1a30 As Double
    Public B31a60 As Double
    Public B61a90 As Double
    Public B90mas As Double
    Public IdForma As Integer
    Public formaPago As String
    Public Pagado As Boolean
    Public fechaContrato As String
    Public tipoEmpenio As Integer
    Public renovado As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
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
        diasPrestamo = 0
        TotalAux = 0
        A1a30 = 0
        A31a60 = 0
        A61a90 = 0
        A90mas = 0
        B1a30 = 0
        B31a60 = 0
        B61a90 = 0
        B90mas = 0
        IdForma = 0
        formaPago = ""
        tipoEmpenio = 0
        fechaContrato = ""
        renovado = 0
        Pagado = False
        Comm.Connection = Conexion
        Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos(ID)
    End Sub
    Public Function buscaVendedor() As String
        Comm.CommandText = "select nombre from tblvendedores where idvendedor=" + IdVendedor.ToString
        Return Comm.ExecuteScalar
    End Function
    Public Function CreaRenovado(pid As Integer, pFecha As String, pCantidad As Double, pidCaja As Integer) As Integer
        Dim CuantosRenglones As Integer
        Comm.CommandText = "insert into tblempenios(fecha,folio,hora,estado,total,idsucursal,serie,idcaja,idvendedor,comentario,tipo,idcliente, diasPrestamos,A1a30,A31a60,A61a90,A90mas,B1a30,B31a60,B61a90,B90mas,fechaContrato,pagado, TotalAux,idforma,Adjudicado,tipoEmpenio, renovado,idempenioant,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,estatus) "
        Comm.CommandText += "select  '" + pFecha + "',folio,'" + Date.Now.ToString("HH:mm:ss") + "',estado," + pCantidad.ToString + ",idsucursal,serie," + pidCaja.ToString + ",idvendedor,comentario,tipo,idcliente, diasPrestamos,A1a30,A31a60,A61a90,A90mas,B1a30,B31a60,B61a90,B90mas,'" + pFecha + "',0," + pCantidad.ToString + ",idforma,Adjudicado,tipoEmpenio,1," + pid.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + CInt(estatusEmpenios.empeniado).ToString + " from tblempenios where idmovimiento=" + pid.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblempenios set estatus=" + CInt(estatusEmpenios.renovado).ToString() + " where idmovimiento=" + pid.ToString()
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idmovimiento) from tblempenios"
        ID = Comm.ExecuteScalar
        Comm.CommandText = "select count(idmovimiento) from tblempeniosdetalles where idmovimiento=" + pid.ToString
        CuantosRenglones = Comm.ExecuteScalar
        If CuantosRenglones = 0 Then CuantosRenglones = 1
        Comm.CommandText = "insert into tblempeniosdetalles(idMovimiento,descripcion, precio,clasificacion,kilates,peso,evaluo,tipo) select " + ID.ToString + ",descripcion," + CStr(pCantidad / CuantosRenglones) + ",clasificacion,kilates,peso,evaluo,tipo from tblempeniosdetalles where idmovimiento=" + pid.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select count(idmovimiento) from tblempeniosdetallest where idmovimiento=" + pid.ToString
        CuantosRenglones = Comm.ExecuteScalar
        If CuantosRenglones = 0 Then CuantosRenglones = 1
        Comm.CommandText = "insert into tblempeniosdetallest(idMovimiento ,clasificacion ,descripcion,evaluo,importe) select " + ID.ToString + ",clasificacion ,descripcion,evaluo," + CStr(pCantidad / CuantosRenglones) + " from tblempeniosdetallest where idmovimiento=" + pid.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select count(idmovimiento) from tblempeniosdetallesv where idmovimiento=" + pid.ToString
        CuantosRenglones = Comm.ExecuteScalar
        If CuantosRenglones = 0 Then CuantosRenglones = 1
        Comm.CommandText = "insert into tblempeniosdetallesv(idMovimiento ,clasificacion,marca ,modelo,color,noSerie,placas,evaluo,importe) select " + ID.ToString + ",clasificacion,marca ,modelo,color,noSerie,placas,evaluo," + CStr(pCantidad / CuantosRenglones) + " from tblempeniosdetallesv where idmovimiento=" + pid.ToString
        Comm.ExecuteNonQuery()
        Return ID
    End Function
    Public Sub LlenaDatos(ByVal pid As Integer)
        ID = pid
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblempenios where idmovimiento=" + ID.ToString
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
            diasPrestamo = DReader("diasPrestamos")
            TotalAux = DReader("TotalAux")
            A1a30 = DReader("A1a30")
            A31a60 = DReader("A31a60")
            A61a90 = DReader("A61a90")
            A90mas = DReader("A90mas")
            B1a30 = DReader("B1a30")
            B31a60 = DReader("B31a60")
            B61a90 = DReader("B61a90")
            B90mas = DReader("B90mas")
            IdForma = DReader("idforma")
            Pagado = DReader("pagado")
            fechaContrato = DReader("fechaContrato")
            tipoEmpenio = DReader("tipoEmpenio")
            renovado = DReader("renovado")
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
        diasPrestamo = pDiasPrestamos
        tipoEmpenio = ptipoEmpenio
        Comm.CommandText = "insert into tblempenios(fecha,folio,hora,estado,total,idsucursal,serie,idcaja,idvendedor,comentario,tipo,idcliente, diasPrestamos,A1a30,A31a60,A61a90,A90mas,B1a30,B31a60,B61a90,B90mas,fechaContrato,pagado, TotalAux,idforma,Adjudicado,tipoEmpenio, renovado,idempenioant,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,estatus) values('" + Replace(Fecha, "'", "''") + "'," + Folio.ToString + ",'" + Format(TimeOfDay, "HH:mm:ss") + "'," + CStr(Estados.SinGuardar) + "," + Total.ToString() + "," + IdSucursal.ToString + ",'" + Replace(Trim(Serie), "'", "''") + "'," + IdCaja.ToString + "," + IdVendedor.ToString + ",'" + Replace(Comentario, "'", "''") + "'," + Tipo.ToString + "," + idCliente.ToString + "," + diasPrestamo.ToString + ",0,0,0,0,0,0,0,0,'" + Replace(Fecha, "'", "''") + "',0," + Total.ToString + "," + Pidforma.ToString + ", 0," + tipoEmpenio.ToString + ",0,0," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + CInt(estatusEmpenios.empeniado).ToString + ");"
        'Comm.ExecuteNonQuery()
        Comm.CommandText += "select max(idmovimiento) from tblempenios;"
        ID = Comm.ExecuteScalar
    End Sub

    Public Sub Modificar(ByVal pID As Integer, ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pTotal As Double, ByVal pidSucursal As Integer, ByVal pSerie As String, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pTipo As Byte, ByVal pComentario As String, ByVal pEstado As Integer, ByVal pDiasPrestamos As Integer, ByVal PIdForma As Integer, ByVal ptipoEmpenio As Integer)
        ID = pID
        Fecha = pFecha '
        Folio = pFolio '
        Total = pTotal
        IdSucursal = pidSucursal
        Serie = pSerie
        IdCaja = pidCaja
        IdVendedor = pIdVendedor
        Comentario = pComentario
        Tipo = pTipo
        Estado = pEstado
        idCliente = pIdCliente
        diasPrestamo = pDiasPrestamos
        IdForma = PIdForma
        tipoEmpenio = ptipoEmpenio
        Comm.CommandText = "update tblempenios set fecha='" + Replace(Fecha, "'", "''") + "', fechaContrato='" + Replace(Fecha, "'", "''") + "',folio=" + Folio.ToString + ",estado=" + Estado.ToString + ",total=" + Total.ToString + ",TotalAux=" + Total.ToString + ",serie='" + Replace(Trim(Serie), "'", "''") + "',idvendedor=" + IdVendedor.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "',tipo=" + Tipo.ToString + ",idcaja=" + IdCaja.ToString + ",idcliente=" + idCliente.ToString + ",diasPrestamos=" + diasPrestamo.ToString + ",idforma=" + PIdForma.ToString + ", tipoEmpenio=" + tipoEmpenio.ToString + ",idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idmovimiento=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaComentario(ByVal pID As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblempenios set comentario='" + Replace(pTexto, "'", "''") + "' where idMovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub ActualizarDiasPrestamo(ByVal pID As Integer, ByVal pDiasPrestamo As Integer)
        diasPrestamo = pDiasPrestamo
        Comm.CommandText = "update tblempenios set  diasPrestamos=" + diasPrestamo.ToString + " where idMovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblempenios where idmovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pEstado As Byte, ByVal pFolio As String, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal PCliente As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblempenios.idmovimiento,tblempenios.fechaContrato,concat(tblempenios.serie,convert(tblempenios.folio using utf8)),concat(tblclientes.clave,' ',tblclientes.nombre),tblempenios.totalAux as total,tblcajas.nombre as caja,case tblempenios.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado, case tblempenios.estatus when 0 then 'Empeñado' when 1 then 'Adjudicado' when 2 then 'Entregado' when 3 then 'Renovado' end as Status from tblempenios inner join tblcajas on tblempenios.idcaja=tblcajas.idcaja inner join tblclientes on tblempenios.idcliente=tblclientes.idcliente where tblempenios.fecha>='" + pFecha + "' and tblempenios.fecha<='" + pFecha2 + "'"
        If PCliente <> "" Then
            Comm.CommandText += " and concat(tblclientes.clave,tblclientes.nombre) like '%" + Replace(PCliente, "'", "''") + "%'"
        End If
        If pEstado <> Estados.Inicio Then
            Comm.CommandText += " and tblempenios.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblempenios.estado<>1"
        End If
        'If pTipo > 0 Then
        '    Comm.CommandText += " and tblempenios.tipo=" + CStr(pTipo - 1)
        'End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and tblempenios.idcaja=" + pidCaja.ToString
            End If
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblempenios.serie,convert(tblempenios.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If

        Comm.CommandText += " order by tblempenios.fecha DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempenios")
        Return DS.Tables("tblempenios").DefaultView
    End Function

    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblempenios where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4)),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblempenios where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado<>1 and estado<>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function

    Public Function BuscaMovimiento(ByVal pFolio As String) As Integer
        Comm.CommandText = "select ifnull((select idmovimiento from tblempenios where concat(tblempenios.serie,convert(tblempenios.folio using utf8))='" + Replace(pFolio, "'", "''") + "' and estado=3 limit 1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaId(ByVal pFolio As String) As Integer
        Comm.CommandText = "select ifnull((select idmovimiento from tblempenios where tblempenios.estado=3 and usado=0 and concat(tblempenios.serie,convert(tblempenios.folio using utf8))='" + Replace(pFolio, "'", "''") + "'),0)"
        Return Comm.ExecuteScalar
    End Function

    Public Function buscaClasificacion(ByVal pidClasificacion As Integer) As String
        Dim Resultado As String
        Comm.CommandText = "select nombre from tblempeniosclasificacion where idClasificacion=" + pidClasificacion.ToString
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function

    Public Sub ModificarConfiguracion(ByVal pID As Integer, ByVal pA1a30 As Double, ByVal pA31a60 As Double, ByVal pA61a90 As Double, ByVal pA90mas As Double, ByVal pB1a30 As Double, ByVal pB31a60 As Double, ByVal pB61a90 As Double, ByVal pB90mas As Double)

        Comm.CommandText = "update tblempenios set A1a30=" + pA1a30.ToString + ",A31a60=" + pA31a60.ToString + ",A61a90=" + pA61a90.ToString + ",A90mas=" + pA90mas.ToString + ",B1a30=" + pB1a30.ToString + ",B31a60=" + pB31a60.ToString + ",B61a90=" + pB61a90.ToString + ",B90mas=" + pB90mas.ToString + " where idmovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function TienePagos(ByVal pIdEmpenio As Integer) As Boolean
        Comm.CommandText = "select count(idempenio) from tblempeniosabono where idempenio=" + pIdEmpenio.ToString
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function


    Public Function filtroTodos(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pIDCliente As Integer, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblempenios.idmovimiento, tblempenios.fecha, CONCAT(tblempenios.serie,' ',tblempenios.folio), tblempenios.total, tblclientes.nombre as cliente, tblvendedores.nombre as vendedor from tblempenios inner join tblclientes on tblempenios.idcliente =tblclientes.idcliente  inner join tblvendedores on tblempenios.idvendedor = tblvendedores.idvendedor where tblempenios.estado=3 "

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
    Public Function DaUltimaFecha() As String
        Dim F As String
        Comm.CommandText = "select ifnull((select max(fecha) from tblempenios),'-')"
        F = Comm.ExecuteScalar
        If F = "-" Then
            Return Format(Date.Now, "yyyy/MM/dd")
        Else
            Return F
        End If
    End Function

    Public Function adjudicar(ByVal idEmpenio As Integer) As Boolean
        Comm.CommandText = "update tblempenios set estatus=" + CInt(estatusEmpenios.adjudicado).ToString + " where idmovimiento=" + idEmpenio.ToString()
        Try
            Comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function entregar(ByVal idEmpenio As Integer) As Boolean
        Comm.CommandText = "update tblempenios set estatus=" + CInt(estatusEmpenios.entregado).ToString + " where idmovimiento=" + idEmpenio.ToString()
        Try
            Comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function reporteEstados(ByVal desde As String, ByVal hasta As String, ByVal filtro As String, ByVal idCliente As Integer) As DataView
        Dim ds As New DataSet
        Comm.CommandText = "select *,cli.nombre from tblempenios inner join tblclientes as cli on tblempenios.idcliente=cli.idcliente where fecha>='" + desde + "' and fecha<='" + hasta + "'"
        Select Case filtro
            Case "Empeñado"
                Comm.CommandText += " and estatus=" + CInt(estatusEmpenios.empeniado).ToString
            Case "Entregado"
                Comm.CommandText += " and estatus=" + CInt(estatusEmpenios.entregado).ToString
            Case "Adjudicado"
                Comm.CommandText += " and estatus=" + CInt(estatusEmpenios.adjudicado).ToString
        End Select
        If idCliente > 0 Then
            Comm.CommandText += " and cli.idcliente=" + idCliente.ToString()
        End If
        Dim da As New MySqlDataAdapter(Comm)
        da.Fill(ds, "empenios")
        'ds.WriteXmlSchema("repEmpeniosEstados.xml")
        Return ds.Tables("empenios").DefaultView
    End Function
End Class
