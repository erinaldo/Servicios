Public Class dbVentasApartados
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Id As Integer
    Public Fecha As String
    Public Estado As Byte
    Public IdSucursal As Integer
    Public Folio As Integer
    Public Serie As String
    Public Prioridad As Byte
    Public Idcliente As Integer
    Public Comentario As String
    Public PrioridadSTR As String
    Public FechaSalida As String
    Public HoraSalida As String
    Public Hora As String
    Public FechaCancelado As String
    Public HoraCancelado As String
    Public Totalapagar As Double
    Public IdMoneda As Integer
    Public Credito As Double
    Public TipodeCambio As Double
    Public Cliente As dbClientes
    Public TotalVenta As Double
    Public Subtotal As Double
    Public TotalIva As Double
    Public IdVendedor As Integer
    Public TotalPeso As Double
    Public Surtido As Integer
    Public SurtidoDonde As Byte
    Public DInventario As Byte
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Id = 0
        Fecha = "1900/01/01"
        Estado = 0
        IdSucursal = 0
        Folio = 0
        Serie = ""
        Prioridad = 0
        Idcliente = 0
        Comentario = ""
        PrioridadSTR = ""
        FechaSalida = "1900/01/01"
        HoraSalida = "00:00"
        Hora = "00:00"
        FechaCancelado = "1900/01/01"
        HoraCancelado = "00:00"
        Totalapagar = 0
        IdMoneda = 0
        Credito = 0
        TipodeCambio = 0
        TotalVenta = 0
        Subtotal = 0
        TotalIva = 0
        IdVendedor = 0
        Surtido = 0
        SurtidoDonde = 0
        DInventario = 0
        Comm.Connection = Conexion
        Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Id = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandType = CommandType.Text
        Comm.CommandText = "select * from tblventasapartados where idapartado=" + Id.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Fecha = DReader("fecha")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            Folio = DReader("folio")
            Serie = DReader("serie")
            Prioridad = DReader("prioridad")
            Idcliente = DReader("idcliente")
            Comentario = DReader("comentario")
            PrioridadSTR = DReader("prioridadstr")
            FechaSalida = DReader("fechasalida")
            HoraSalida = DReader("horasalida")
            Hora = DReader("hora")
            FechaCancelado = DReader("fechacancelado")
            HoraCancelado = DReader("horacancelado")
            Totalapagar = DReader("totalapagar")
            IdMoneda = DReader("idmoneda")
            Credito = DReader("credito")
            TipodeCambio = DReader("tipodecambio")
            IdVendedor = DReader("idvendedor")
            Surtido = DReader("surtido")
            SurtidoDonde = DReader("surtidodonde")
            DInventario = DReader("dinventario")
        End If
        DReader.Close()
        Cliente = New dbClientes(Idcliente, Comm.Connection)
    End Sub
    'Public Function ExisteFolio(ByVal pfolio As Integer, Optional ByVal idventa As Integer = -1) As Boolean
    '    Folio = pfolio
    '    Comm.CommandText = "select count(folio) from tblventas where folio=" + Folio.ToString + If(idventa = -1, "", " and idventa<>" + CStr(idventa))
    '    If Comm.ExecuteScalar = 0 Then Return False Else Return True
    'End Function
    'Id = 0
   
    Public Sub Guardar(ByVal pFecha As String, ByVal pIdSucursal As Integer, ByVal pFolio As Integer, ByVal pSerie As String, ByVal pPrioridad As Byte, ByVal pIdcliente As Integer, ByVal pComentario As String, ByVal pPrioridadSTR As String, ByVal pFechaSalida As String, ByVal pHoraSalida As String, ByVal pHora As String, ByVal pIdMoneda As Integer, ByVal pTipodeCambio As Double, ByVal pIdVendedor As Integer, ByVal pDinventario As String)
        'Id = 0        Fecha = "1900/01/01"        Estado = 0        IdSucursal =  Folio serie'''''''' Prioridad Idcliente  Comentario PrioridadSTR   FechaSalida = "19 HoraSalida =   Hora  FechaCancelado  HoraCancelado  Totalapagar IdMonedaCredito =TipodeCambio  T IdVendedor = 0
        Comm.CommandText = "insert into tblventasapartados(fecha,estado,idsucursal,folio,serie,prioridad,idcliente,comentario,prioridadstr,fechasalida,horasalida,hora,fechacancelado,horacancelado,totalapagar,idmoneda,credito,tipodecambio,idvendedor,surtido,surtidodonde,dinventario,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + _
        "'" + pFecha + "',1," + pIdSucursal.ToString + "," + pFolio.ToString + ",'" + Replace(pSerie, "'", "''") + "'," + pPrioridad.ToString + "," + pIdcliente.ToString + ",'" + Replace(pComentario, "'", "''") + "','" + Replace(pPrioridadSTR, "'", "''") + "','" + pFechaSalida + "','" + pHoraSalida + "','" + Format(Date.Now, "HH:mm:ss") + "','" + pFecha + "','" + Format(Date.Now, "HH:mm:ss") + "',0," + pIdMoneda.ToString + ",0," + pTipodeCambio.ToString + "," + pIdVendedor.ToString + ",0,0," + pDinventario + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        Comm.CommandText += "select max(idapartado) from tblventasapartados;"
        Id = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pSerie As String, ByVal pPrioridad As Byte, ByVal pIdcliente As Integer, ByVal pComentario As String, ByVal pPrioridadSTR As String, ByVal pFechaSalida As String, ByVal pHoraSalida As String, ByVal pIdMoneda As Integer, ByVal pTipodeCambio As Double, ByVal pIdVendedor As Integer, ByVal pEstado As Byte, ByVal pTotalApagar As Double, ByVal pSurtido As Byte)
        Id = pID
        Comm.CommandText = "update tblventasapartados set fecha='" + pFecha + "',estado=" + pEstado.ToString + ",folio=" + pFolio.ToString + ",serie='" + Replace(pSerie, "'", "''") + "',prioridad=" + pPrioridad.ToString + ",idcliente=" + pIdcliente.ToString + ",comentario='" + Replace(pComentario, "'", "''") + "',prioridadstr='" + Replace(pPrioridadSTR, "'", "''") + "',fechasalida='" + pFechaSalida + "',horasalida='" + pHoraSalida + "',fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(Date.Now, "HH:mm:ss") + "',totalapagar=" + pTotalApagar.ToString + ",idmoneda=" + pIdMoneda.ToString + ",tipodecambio=" + pTipodeCambio.ToString + ",idvendedor=" + pIdVendedor.ToString + ",surtido=" + pSurtido.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idapartado=" + Id.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificaEstado(ByVal pidVenta As Integer, ByVal pEstado As Byte)
        Comm.CommandText = "update tblventasapartados set estado=" + pEstado.ToString + " where idapartado=" + pidVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        'DesligaRemisiones(pID)
        Comm.CommandText = "delete from tblventasapartados where idapartado=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pNombreClave As String, ByVal pFolio As String, ByVal pEstado As Byte, ByVal pIdVendedor As Integer, ByVal pPrioridad As Byte, ByVal pSurtido As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventas.idapartado,tblventas.fecha,concat(tblventas.serie,convert(tblventas.folio using utf8)),tblclientes.clave,tblclientes.nombre as Cliente,tblventas.totalapagar,case tblventas.prioridad when 0 then 'Alta' when 1 then 'Media' when 2 then 'Baja' end  as sprioridad,tblventas.prioridadstr,concat(fechasalida,' ',horasalida) as fechasalida,case tblventas.surtido when 0 then 'No' when 1 then 'Si' end as surtido,case tblventas.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado from tblventasapartados as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblclientes.clave,tblclientes.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblventas.serie,convert(tblventas.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado <> 0 Then
            Comm.CommandText += " and tblventas.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblventas.estado<>1"
        End If
        If pIdVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pIdVendedor.ToString
        End If
        If pPrioridad > 0 Then
            pPrioridad -= 1
            Comm.CommandText += " and tblventas.prioridad=" + pPrioridad.ToString
        End If
        If pSurtido > 0 Then
            pSurtido -= 1
            Comm.CommandText += " and tblventas.surtido=" + pSurtido.ToString
        End If
        Comm.CommandText += " order by tblventas.fecha desc,tblventas.prioridad,tblventas.serie,tblventas.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasapartados")
        'DS.WriteXmlSchema("tblventasapartados.xml")
        Return DS.Tables("tblventasapartados").DefaultView
    End Function
    Public Function ConsultaDeudas(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidCliente As Integer, ByVal pFolio As String, ByVal pidTipodePago As Integer, ByVal PorFechas As Boolean, ByVal Todas As Boolean, ByVal pTipodeOrden As Byte, ByVal pMostrarCanceladas As Boolean, ByVal pTipodeCambio As Double, ByVal pEnPesos As Boolean, ByVal pIdSucursal As Integer) As DataView
        Dim DS As New DataSet

        'Comm.CommandText = "delete from tblclientesdeudas where idcliente=" + pidCliente.ToString
        'Comm.ExecuteNonQuery()

        'Remisiones
        '"select iddocumento as idventa,0 as sel,cant,fecha,case tipo when 0 then 'Factura'  Par.' end as tipodoc,if(estado=3,'A','C') as estadof,serie,folio,credito,totalapagar,totalapagar-credito as restante,tipo,totalapagar-credito as restante2 from tblclientesdeudas where idcliente=" + pidCliente.ToString

        If pEnPesos Then
            Comm.CommandText = "select " + _
        "tblventasremisiones.idapartado as idremision,0 as sel,tblventasremisiones.fecha,'Apartado' as tipodoc,if(tblventasremisiones.estado=3,'A','C') as estadof,tblventasremisiones.serie,tblventasremisiones.folio," + _
        "if(tblventasremisiones.idmoneda=2,tblventasremisiones.credito,tblventasremisiones.credito*" + pTipodeCambio.ToString + ") as credito," + _
        "if(tblventasremisiones.idmoneda=2,tblventasremisiones.totalapagar,tblventasremisiones.totalapagar*" + pTipodeCambio.ToString + ") as totalapagar," + _
        "if(tblventasremisiones.idmoneda=2,tblventasremisiones.totalapagar-tblventasremisiones.credito,(tblventasremisiones.totalapagar-tblventasremisiones.credito)*" + pTipodeCambio.ToString + ") as restante," + _
        "0 as tipo," + _
        "if(tblventasremisiones.idmoneda=2,tblventasremisiones.totalapagar-tblventasremisiones.credito,(tblventasremisiones.totalapagar-tblventasremisiones.credito)*" + pTipodeCambio.ToString + ") as restante2" + _
        " from tblventasapartados as tblventasremisiones inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente where tblventasremisiones.idcliente=" + pidCliente.ToString + " "
        Else
            Comm.CommandText = "select " + _
        "tblventasremisiones.idapartado as idremision,0 as sel,tblventasremisiones.fecha,'Apartado' as tipodoc,if(tblventasremisiones.estado=3,'A','C') as estadof,tblventasremisiones.serie,tblventasremisiones.folio,tblventasremisiones.credito,tblventasremisiones.totalapagar,0 from tblventasremisiones inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente where tblventasremisiones.idcliente=" + pidCliente.ToString + " "
        End If
        If Todas = False Then
            Comm.CommandText += " and round(tblventasremisiones.totalapagar-tblventasremisiones.credito,2)>0"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblventasremisiones.serie,convert(tblventasremisiones.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pMostrarCanceladas Then
            Comm.CommandText += " and (tblventasremisiones.estado=4 or tblventasremisiones.estado=3)"
        Else
            Comm.CommandText += " and tblventasremisiones.estado=3"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventasremisiones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.ExecuteNonQuery()


        'If pEnPesos Then
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",tblventas.idventa,4,tblventas.fecha,tblventas.estado,tblventas.serie,tblventas.folio,if(tblventas.idconversion=2,tblventas.credito,tblventas.credito*" + pTipodeCambio.ToString + "),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*" + pTipodeCambio.ToString + "),0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblventas.idforma=98 and tblformasdepago.tipo=" + pidTipodePago.ToString
        'Else
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",tblventas.idventa,4,tblventas.fecha,tblventas.estado,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblventas.idforma=98 and tblformasdepago.tipo=" + pidTipodePago.ToString
        'End If

        ''select tblventas.idventa,0 as sel,tblventas.fecha,if(tblventas.estado=3,'A','C') as estadof,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,tblventas.totalapagar-tblventas.credito as restante from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        'If Todas = False Then
        '    Comm.CommandText += " and round(tblventas.totalapagar-tblventas.credito,2)>0"
        'End If
        'If PorFechas Then
        '    Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        'End If
        'If pFolio <> "" Then
        '    Comm.CommandText += " and concat(tblventas.serie,convert(tblventas.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        'End If
        'If pMostrarCanceladas Then
        '    Comm.CommandText += " and (tblventas.estado=4 or tblventas.estado=3)"
        'Else
        '    Comm.CommandText += " and tblventas.estado=3"
        'End If
        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        'End If
        'Comm.ExecuteNonQuery()

        ''Notas de Cargo
        'If pEnPesos Then
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",tblnotasdecargo.idcargo,1,tblnotasdecargo.fecha,tblnotasdecargo.estado,tblnotasdecargo.serie,tblnotasdecargo.folio,if(tblnotasdecargo.idmoneda=2,tblnotasdecargo.aplicado,tblnotasdecargo.aplicado*" + pTipodeCambio.ToString + "),if(tblnotasdecargo.idmoneda=2,tblnotasdecargo.totalapagar,tblnotasdecargo.totalapagar*" + pTipodeCambio.ToString + "),0 from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.idcliente=" + pidCliente.ToString
        'Else
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",tblnotasdecargo.idcargo,1,tblnotasdecargo.fecha,tblnotasdecargo.estado,tblnotasdecargo.serie,tblnotasdecargo.folio,tblnotasdecargo.aplicado,tblnotasdecargo.totalapagar,0 from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.idcliente=" + pidCliente.ToString
        'End If


        'If Todas = False Then
        '    Comm.CommandText += " and round(tblnotasdecargo.totalapagar-tblnotasdecargo.aplicado,2)>0"
        'End If
        'If PorFechas Then
        '    Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        'End If
        'If pFolio <> "" Then
        '    Comm.CommandText += " and concat(tblnotasdecargo.serie,convert(tblnotasdecargo.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        'End If
        'If pMostrarCanceladas Then
        '    Comm.CommandText += " and (tblnotasdecargo.estado=4 or tblnotasdecargo.estado=3)"
        'Else
        '    Comm.CommandText += " and tblnotasdecargo.estado=3"
        'End If
        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
        'End If
        'Comm.ExecuteNonQuery()

        ''Documentos saldo inicial
        'If pEnPesos Then
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",dc.iddocumento,2,dc.fecha,dc.estado,dc.serie,dc.folio,if(dc.idmoneda=2,dc.credito,dc.credito*" + pTipodeCambio.ToString + "),if(dc.idmoneda=2,dc.totalapagar,dc.totalapagar*" + pTipodeCambio.ToString + "),0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=0"
        'Else
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '    ",dc.iddocumento,2,dc.fecha,dc.estado,dc.serie,dc.folio,dc.credito,dc.totalapagar,0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=0"
        'End If


        'If Todas = False Then
        '    Comm.CommandText += " and round(dc.totalapagar-dc.credito,2)>0"
        'End If
        'If PorFechas Then
        '    Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        'End If
        'If pFolio <> "" Then
        '    Comm.CommandText += " and concat(dc.serie,convert(dc.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        'End If
        'If pMostrarCanceladas Then
        '    Comm.CommandText += " and (dc.estado=4 or dc.estado=3)"
        'Else
        '    Comm.CommandText += " and dc.estado=3"
        'End If
        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and dc.idsucursal=" + pIdSucursal.ToString
        'End If
        'Comm.ExecuteNonQuery()

        ''Documentos documentos
        'If pEnPesos Then
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",dc.iddocumento,3,dc.fecha,dc.estado,dc.seriereferencia,dc.folioreferencia,if(dc.idmoneda=2,dc.credito,dc.credito*" + pTipodeCambio.ToString + "),if(dc.idmoneda=2,dc.totalapagar,dc.totalapagar*" + pTipodeCambio.ToString + "),0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=1"
        'Else
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '    ",dc.iddocumento,3,dc.fecha,dc.estado,dc.seriereferencia,dc.folioreferencia,dc.credito,dc.totalapagar,0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=1"
        'End If


        'If Todas = False Then
        '    Comm.CommandText += " and round(dc.totalapagar-dc.credito,2)>0"
        'End If
        'If PorFechas Then
        '    Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        'End If
        'If pFolio <> "" Then
        '    Comm.CommandText += " and concat(dc.seriereferencia,convert(dc.folioreferencia using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        'End If
        'If pMostrarCanceladas Then
        '    Comm.CommandText += " and (dc.estado=4 or dc.estado=3)"
        'Else
        '    Comm.CommandText += " and dc.estado=3"
        'End If
        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and dc.idsucursal=" + pIdSucursal.ToString
        'End If
        'Comm.ExecuteNonQuery()


        'Comm.CommandText = "select iddocumento as idventa,0 as sel,cant,fecha,case tipo when 0 then 'Factura' when 1 then 'Nota de Cargo' when 2 then 'S. Inicial' when 3 then 'Documento' when 4 then 'Factura Par.' end as tipodoc,if(estado=3,'A','C') as estadof,serie,folio,credito,totalapagar,totalapagar-credito as restante,tipo,totalapagar-credito as restante2 from tblclientesdeudas where idcliente=" + pidCliente.ToString
        'select tblventas.idventa,0 as sel,tblventas.fecha,if(tblventas.estado=3,'A','C') as estadof,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,tblventas.totalapagar-tblventas.credito as restante from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        If pTipodeOrden = 0 Then
            Comm.CommandText += " order by fecha,serie,folio"
        Else
            Comm.CommandText += " order by totalapagar,serie,folio"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesdeudasr")
        Return DS.Tables("tblclientesdeudasr").DefaultView
    End Function
    Public Function DaTotal(ByVal pidVenta As Integer, ByVal pidMoneda As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0
        'Dim Encontro As Double
        Dim iIva As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double
        'Dim iIsr As Double
        'Dim iIvaRetenido As Double
        'Dim iCargo As Double
        'Dim iDescuento As Double

        'Dim iTruncar As Byte
        Subtotal = 0
        TotalIva = 0
        TotalVenta = 0
        Comm.CommandText = "select tipodecambio from tblventasapartados where idapartado=" + pidVenta.ToString
        iTipoCambio = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tblventasapartadosdetalles where idapartado=" + pidVenta.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        'Dim Desc As Double
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblventasapartadosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select iva from tblventasapartadosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblventasapartadosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            'Comm.CommandText = "select descuento from tblventasapartadosdetalles where idventasinventario=" + IDs.Item(Cont).ToString
            'Desc = Comm.ExecuteScalar
            If pidMoneda = 2 Then
                If pidMoneda <> IdMonedaC Then
                    Precio = Precio * iTipoCambio
                End If
            Else
                If IdMonedaC = 2 Then
                    Precio = Precio / iTipoCambio
                End If
            End If
            TotalIva += (Precio * (iIva / 100))
            'TotalDescuento += (Precio / (1 - Desc / 100)) - Precio
            Subtotal += Precio
            Cont += 1
        End While
        'If iTruncar = 0 Then
        'Subtototal = Subtototal
        'TotalISR = Subtototal * (iIsr / 100)
        'TotalIvaRetenido = Subtototal * (iIvaRetenido / 100)
        TotalVenta = Subtotal + TotalIva
        Comm.CommandText = "select ifnull((select sum(tblinventario.peso*tblventasapartadosdetalles.cantidad) from tblventasapartadosdetalles inner join tblinventario on tblventasapartadosdetalles.idinventario=tblinventario.idinventario where tblventasapartadosdetalles.idapartado=" + pidVenta.ToString + "),0)"
        TotalPeso = Comm.ExecuteScalar
        'Else
        'TotalIva = Math.Truncate(TotalIva)
        'Subtototal = Subtototal
        'TotalISR = Subtototal * (iIsr / 100)
        'TotalIvaRetenido = Subtototal * (iIvaRetenido / 100)
        'TotalVenta = Subtototal + TotalIva - TotalISR - TotalIvaRetenido - iDescuento + iCargo
        'End If

        Return TotalVenta
    End Function
    Public Function DaNuevoFolio(ByVal pSerie As String) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblventasapartados where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4)),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblventasapartados where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    Public Function DaId(ByVal pFolio As Integer, ByVal pSerie As String) As Integer
        Comm.CommandText = "select ifnull((select idapartado from tblventasapartados where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2 limit 1),0)"
        Id = Comm.ExecuteScalar
        If Id <> 0 Then LlenaDatos()
        Return Id
    End Function
    

    Public Sub AgregarDetallesReferencia(ByVal PidVenta As Integer, ByVal pIdDocumento As Integer(), ByVal Tipo As Byte, ByVal pidAlmacen As Integer)
        '0 cotizacion
        '1 pedido
        '2 remision
        '3 ventas

        Dim whereFields As String = ""
        Dim id As Integer

        If Tipo = 0 Then
            For Each id In pIdDocumento
                whereFields += " or idcotizacion=" + id.ToString
            Next
            Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm) select " + PidVenta.ToString + ",idinventario,sum(cantidad),sum(precio),descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0,0,sum(cantidad),if(idinventario>1,(select tipocantidad from tblinventario where tblinventario.idinventario=tblventascotizacionesinventario.idinventario),(select tipocantidad from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventascotizacionesinventario.idvariante )) from tblventascotizacionesinventario where false" + whereFields + " group by idinventario,precio;"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "update tblinventarioseries set idventa=" + PidVenta.ToString + ",idcotizacion=0 where false " + whereFields
            Comm.ExecuteNonQuery()
        End If

        If Tipo = 1 Then
            For Each id In pIdDocumento
                whereFields += " or idpedido=" + id.ToString
            Next
            Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm) select " + PidVenta.ToString + ",idinventario,sum(cantidad),sum(precio),descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0,0,sum(cantidad),if(idinventario>1,(select tipocantidad from tblinventario where tblinventario.idinventario=tblventaspedidosinventario.idinventario),(select tipocantidad from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventaspedidosinventario.idvariante )) from tblventaspedidosinventario where false" + whereFields + " group by idinventario,precio;"
            Comm.ExecuteNonQuery()
        End If

        If Tipo = 2 Then
            For Each id In pIdDocumento
                whereFields += " or idremision=" + id.ToString
            Next
            Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm) select " + PidVenta.ToString + ",idinventario,sum(cantidad),sum(precio),descripcion,tblventasremisionesinventario.idmoneda,idalmacen,tblventasremisionesinventario.iva,extra,descuento,idvariante,idservicio,sum(cantidad),sum(cantidad),if(idinventario>1,(select tipocantidad from tblinventario where tblinventario.idinventario=tblventasremisionesinventario.idinventario),(select tipocantidad from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventasremisionesinventario.idvariante )) from tblventasremisionesinventario inner join tblventasremisiones on tblventasremisionesinventario.idremision=tblventasremisiones.idremision where tblventasremisiones.estado=3 and (false " + Replace(whereFields, "idremision", "tblventasremisiones.idremision") + ") group by idinventario,round(precio/cantidad,3);"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "update tblinventarioseries set idventa=" + PidVenta.ToString + ",idremision=0 where false " + whereFields
            Comm.ExecuteNonQuery()
        End If

        If Tipo = 3 Then
            For Each id In pIdDocumento
                whereFields += " or idventa=" + id.ToString
            Next
            'Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm) select " + PidVenta.ToString + ",idinventario,sum(cantidad),sum(precio),descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,0,sum(cantidadm),tipocantidadm from tblventasinventario where false" + whereFields + " group by idinventario,precio;"
            Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm) select " + PidVenta.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,0,cantidadm,tipocantidadm from tblventasinventario where false" + whereFields + " order by idventasinventario;"
            Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Sub RegresaInventario(ByVal pId As Integer)
        Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,surtido,0,0,0),0) from tblventasapartadosdetalles where idapartado=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaIvas(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tblventasapartadosdetalles.iva,tblventasapartadosdetalles.precio,tblventasapartadosdetalles.idmoneda,tblventasapartados.tipodecambio from tblventasapartadosdetalles inner join tblventasapartados on tblventasapartadosdetalles.idapartado=tblventasapartados.idapartado where tblventasapartadosdetalles.idapartado=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function

    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidinventario As Integer, ByVal pidclasificacion1 As Integer, ByVal pidclasificacion2 As Integer, ByVal pidclasificacion3 As Integer, ByVal pSoloCanceladas As Boolean, ByVal pSerie As String, pIdTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idapartado as idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idmoneda=2,round(tblventas.totalapagar,2),round(tblventas.totalapagar*tblventas.tipodecambio,2)) as totalapagar,tblventas.totalapagar as total,tblventas.fecha,tblventas.tipodecambio,tblventas.idmoneda,tblclientes.nombre as cnombre,concat(tblventas.fechasalida,' ',tblventas.horasalida) as fecha1,tblventas.surtido as usado,tblventas.prioridadstr ref,0 tipo,tblventas.credito  " + _
            "from tblventasapartados as tblventas inner join tblventasapartadosdetalles as tblventasinventario on tblventas.idapartado=tblventasinventario.idapartado inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblsucursales s on tblventas.idsucursal=s.idsucursal where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblventas.idapartado as idventa,tblventas.folio,tblventas.serie,tblventas.estado,round(tblventas.totalapagar,2) as totalapagar,tblventas.totalapagar as total,tblventas.fecha,tblventas.tipodecambio,tblventas.idmoneda,tblclientes.nombre as cnombre,concat(tblventas.fechasalida,' ',tblventas.horasalida) as fecha1,tblventas.surtido as usado,tblventas.prioridadstr ref, 0 tipo,tblventas.credito  " + _
            "from tblventasapartados as tblventas inner join tblventasapartadosdetalles as tblventasinventario on tblventas.idapartado=tblventasinventario.idapartado inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblsucursales s on tblventas.idsucursal=s.idsucursal where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If
        If pSoloCanceladas Then
            Comm.CommandText += " and tblventas.estado=4"
        Else
            Comm.CommandText += " and tblventas.estado=3"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        If pidinventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pidinventario.ToString
        Else
            If pidclasificacion1 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidclasificacion1.ToString
            End If
            If pidclasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidclasificacion2.ToString
            End If
            If pidclasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidclasificacion3.ToString
            End If
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        Comm.CommandText += " group by tblventas.idapartado order by tblventas.fecha,tblventas.serie,tblventas.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblremisiones")
        'DS.WriteXmlSchema("tblremisiones.xml")
        Return DS.Tables("tblremisiones").DefaultView
    End Function
    Public Function ReporteVentasArticulos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pUsado As Boolean, ByVal pCondensado As Boolean, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal pSerie As String, ByVal pPorVendedor As Boolean, pIdTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idapartado idventa,v.folio,v.serie,v.estado,v.totalapagar as total,v.totalapagar,v.fecha,v.tipodecambio,vi.idmoneda,vi.cantidad,i.nombre as descripcion,round(if(vi.idmoneda=2,vi.precio,vi.precio*v.tipodecambio),2) as precio,0 as costoinv,0 as costopro,vi.idinventario,0 as idvariante,c.nombre as cnombre,vi.iva,v.surtido as usado,round(if(vi.idmoneda=2,vi.precio/vi.cantidad,vi.precio*v.tipodecambio/vi.cantidad),2) as preciou,i.clave as codigoa,v.idvendedor,(select nombre from tblvendedores where idvendedor=v.idvendedor) as vnombre,0 as porsurtir " + _
            "from tblventasapartados v inner join tblventasapartadosdetalles vi on v.idapartado=vi.idapartado inner join tblinventario i on vi.idinventario=i.idinventario inner join tblclientes c on v.idcliente=c.idcliente inner join tblsucursales s on v.idsucursal=s.idsucursal where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idapartado idventa,v.folio,v.serie,v.estado,v.totalapagar as total,v.totalapagar,v.fecha,v.tipodecambio,vi.idmoneda,vi.cantidad,i.nombre as descripcion,round(vi.precio,2),0 as costoinv,0 as costopro,vi.idinventario,0 as idvariante,c.nombre as cnombre,vi.iva,v.surtido as usado,round(vi.precio/vi.cantidad,2) as preciou,i.clave as codigoa,v.idvendedor,(select nombre from tblvendedores where idvendedor=v.idvendedor) as vnombre,0 as porsurtir " + _
            "from tblventasapartados v inner join tblventasapartadosdetalles vi on v.idapartado=vi.idapartado inner join tblinventario i on vi.idinventario=i.idinventario inner join tblclientes c on v.idcliente=c.idcliente inner join tblsucursales s on v.idsucursal=s.idsucursal where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idsucursal=" + pIdTipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pidVendedor > 0 Then Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        If pidMoneda > 0 Then Comm.CommandText += " and vi.idmoneda=" + pidMoneda.ToString
        'If pUsado Then Comm.CommandText += " and v.surtido=0"
        If pIdInventario > 1 Then
            Comm.CommandText += " and vi.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then Comm.CommandText += " and i.idclasificacion=" + pidClasificacion.ToString
            If pidClasificacion2 > 0 Then Comm.CommandText += " and i.idclasificacion2=" + pidClasificacion.ToString
            If pidClasificacion3 > 0 Then Comm.CommandText += " and i.idclasificacion3=" + pidClasificacion.ToString
        End If
        'If pMostrarOc = 1 Then
        '    Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        'End If
        If pSerie <> "" Then
            Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pCondensado = False Then
            Comm.CommandText += " order by v.fecha,v.serie,v.folio"
        Else
            If pPorVendedor = False Then
                Comm.CommandText += " order by vi.idinventario,preciou"
            Else
                Comm.CommandText += " order by v.idvendedor,vi.idinventario"
            End If
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventasra.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Function ReporteVentasArticulosCon(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pUsado As Boolean, ByVal pCondensado As Boolean, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal pSerie As String, ByVal pPorVendedor As Boolean, pIdTipoSucursal As Integer, pSoloNosurtido As Byte) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select 0 idventa,0 folio,'' serie,3 estado,0 as total,0 totalapagar,'' fecha,0 tipodecambio,2 idmoneda,sum(vi.cantidad) cantidad,i.nombre as descripcion,sum(round(if(vi.idmoneda=2,vi.precio,vi.precio*v.tipodecambio),2)) as precio,0 as costoinv,0 as costopro,vi.idinventario,0 as idvariante,'' as cnombre,16 iva,0 as usado,0 as preciou,i.clave as codigoa,0 idvendedor,'' as vnombre,0 as porsurtir " + _
            "from tblventasapartados v inner join tblventasapartadosdetalles vi on v.idapartado=vi.idapartado inner join tblinventario i on vi.idinventario=i.idinventario inner join tblclientes c on v.idcliente=c.idcliente inner join tblsucursales s on v.idsucursal=s.idsucursal where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select 0 idventa,0 folio,'' serie,3 estado,0 as total,0 totalapagar,'' fecha,0 tipodecambio,2 idmoneda,sum(vi.cantidad) cantidad,i.nombre as descripcion,sum(round(vi.precio,2)) as precio,0 as costoinv,0 as costopro,vi.idinventario,0 as idvariante,'' cnombre,16 iva,0 surtido as usado,0 as preciou,i.clave as codigoa,0 idvendedor,'' as vnombre,0 as porsurtir " + _
            "from tblventasapartados v inner join tblventasapartadosdetalles vi on v.idapartado=vi.idapartado inner join tblinventario i on vi.idinventario=i.idinventario inner join tblsucursales s on v.idsucursal=s.idsucursal where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idsucursal=" + pIdTipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pidVendedor > 0 Then Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        If pidMoneda > 0 Then Comm.CommandText += " and vi.idmoneda=" + pidMoneda.ToString
        If pSoloNosurtido = 0 Then Comm.CommandText += " and v.surtido=0"
        If pSoloNosurtido = 1 Then Comm.CommandText += " and v.surtido=1"
        'If pUsado Then Comm.CommandText += " and v.surtido=0"
        If pIdInventario > 1 Then
            Comm.CommandText += " and vi.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then Comm.CommandText += " and i.idclasificacion=" + pidClasificacion.ToString
            If pidClasificacion2 > 0 Then Comm.CommandText += " and i.idclasificacion2=" + pidClasificacion.ToString
            If pidClasificacion3 > 0 Then Comm.CommandText += " and i.idclasificacion3=" + pidClasificacion.ToString
        End If
        'If pMostrarOc = 1 Then
        '    Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        'End If
        If pSerie <> "" Then
            Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        Comm.CommandText += " group by vi.idinventario order by i.clave"

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventasra.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Function ReporteVentasArticulosMasVendidos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSerie As String, ByVal pOrdenxVendedor As Boolean, ByVal pLimite As String, ByVal pTipoOrden As Byte) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select sum(tblventasinventario.cantidad) as cantidad,tblinventario.nombre as descripcion,sum(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio)) as precio,tblventasinventario.idinventario,tblventasinventario.iva,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo<>2 and tblventasinventario.cantidad<>0"
        Else
            Comm.CommandText = "select sum(tblventasinventario.cantidad) as cantidad,tblinventario.nombre as descripcion,sum(tblventasinventario.precio) as precio,tblventasinventario.idinventario,tblventasinventario.iva,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo<>2 and tblventasinventario.cantidad<>0"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
        End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            End If
            If pidClasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
            End If
            If pidClasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
            End If
        End If

        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If

        If pOrdenxVendedor Then
            Comm.CommandText += " group by tblventas.idvendedor,tblventasinventario.idinventario"
        Else
            Comm.CommandText += " group by tblventasinventario.idinventario"
        End If
        If pTipoOrden = 0 Then
            Comm.CommandText += " order by cantidad desc limit " + pLimite
        Else
            Comm.CommandText += " order by precio desc limit " + pLimite
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasmas")
        'DS.WriteXmlSchema("tblventasmas.xml")
        Return DS.Tables("tblventasmas").DefaultView
    End Function
    Public Sub ModificaInventario(ByVal pId As Integer)
        Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,cantidad-surtido,0,1,0),0) from tblventasapartadosdetalles where idapartado=" + pId.ToString + ";"
        Comm.CommandText += "update tblventasapartadosdetalles set surtido=cantidad where idapartado=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function VerificaExistencias(ByVal pId As Integer) As String
        Dim Str As String = ""
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Cont As Integer = 1
        Comm.CommandText = "select idinventario from tblventasapartadosdetalles where idapartado=" + pId.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read()
            IDs.Add(DReader("idinventario"))
        End While
        DReader.Close()
        Dim I As New dbInventario(MySqlcon)
        Dim iIdInventario As Integer
        'Dim iIdVariante As Integer
        Dim iCantidad As Double
        Dim iIdAlmacen As Integer
        Dim iCantidad2 As Double
        Dim EsInventariable As Integer
        Dim iContenido As Double
        While Cont <= IDs.Count
            'Comm.CommandText = "select idinventario,idvariante,cantidad,idalmacen,if(idinventario>1,(select inventariable from tblinventario where tblinventario.idinventario=tblventasinventario.idinventario),0) as esinventariable,if(idvariante>1,(select inventariable from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventasinventario.idvariante),0) as esinventariablep,(select contenido from tblinventario where tblinventario.idinventario=tblventasinventario.idinventario) as contenido from tblventasinventario where idventasinventario=" + IDs(Cont).ToString
            Comm.CommandText = "select tblinventario.inventariable as esinventariable,tblinventario.contenido,ifnull((sum(tblventasapartadosdetalles.cantidad)),0) as cantidad,tblventasapartadosdetalles.idalmacen from tblventasapartadosdetalles inner join tblinventario on tblinventario.idinventario=tblventasapartadosdetalles.idinventario where tblventasapartadosdetalles.idapartado=" + pId.ToString + " and tblventasapartadosdetalles.idinventario=" + IDs(Cont).ToString + " limit 1"
            DReader = Comm.ExecuteReader
            If DReader.Read() Then
                iIdInventario = IDs(Cont)
                iCantidad = DReader("cantidad")
                'iIdVariante = DReader("idvariante")
                iIdAlmacen = DReader("idalmacen")
                EsInventariable = DReader("esinventariable")
                iContenido = DReader("contenido")
                DReader.Close()
                If iIdInventario > 1 And EsInventariable = 1 Then
                    iCantidad2 = I.DaInventario(iIdAlmacen, iIdInventario)
                    If iContenido <> 1 Then
                        iCantidad2 = iCantidad2 * iContenido
                    End If
                    If iCantidad > iCantidad2 Then
                        Str = " Hay artículos con insuficiente inventario."
                    End If
                End If
                'If iIdVariante > 1 Then
                '    Comm.CommandText = "select spverificaexistenciarecetas(" + iIdVariante.ToString + "," + iIdAlmacen.ToString + "," + iCantidad.ToString + ")"
                '    Str = Comm.ExecuteScalar
                'End If
            Else
                DReader.Close()
            End If
            Cont += 1
        End While
        Return Str
    End Function



    Public Sub ActualizaComentario(ByVal pidventa As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblventasapartados set comentario='" + Replace(pTexto, "'", "''") + "' where idapartado=" + pidventa.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub SurtirApartado(ByVal pidventa As Integer, ByVal pSurtido As Byte)
        Comm.CommandText = "update tblventasapartados set surtido=" + pSurtido.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idapartado=" + pidventa.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function RevisaConceptos(ByVal pIdVenta As Integer) As Boolean
        Dim C1 As Integer
        Dim C2 As Integer
        Comm.CommandText = "select count(idmoneda) from tblventasapartadosdetalles where idapartado=" + pIdVenta.ToString + " and idmoneda=2"
        C1 = Comm.ExecuteScalar
        Comm.CommandText = "select count(idmoneda) from tblventasapartadosdetalles where idapartado=" + pIdVenta.ToString + " and idmoneda<>2"
        C2 = Comm.ExecuteScalar
        If C1 <> 0 And C2 <> 0 Then
            Return False
        End If
        Return True
    End Function

    Public Function DaTotalCantidad(ByVal pIdVenta As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(tblventasapartadosdetalles.cantidad) from tblventasapartadosdetalles inner join tblinventario on tblventasapartadosdetalles.idinventario=tblinventario.idinventario where idapartado=" + pIdVenta.ToString + " and inventariable=1),0)"
        Return Comm.ExecuteScalar
    End Function

End Class
