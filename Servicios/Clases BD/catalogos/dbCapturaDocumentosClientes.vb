Public Class dbCapturaDocumentosClientes
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdCliente As Integer
    Public Fecha As String
    Public Cliente As dbClientes
    Public Folio As Integer
    Public Credito As Double
    Public TotalaPagar As Double
    Public Hora As String
    Public Serie As String
    Public Estado As Byte
    Public IdSucursal As Integer
    Public TipodeCambio As Double
    Public IdMoneda As Integer
    Public TotalVenta As Double
    Public Subtototal As Double
    Public FolioReferencia As Integer
    Public SerieReferencia As String
    Public IdFormadePago As Integer
    Public TipoSaldo As Integer
 
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdCliente = -1
        Fecha = ""
        Hora = ""
        Folio = 0
        Credito = 0
        TotalaPagar = 0
        Serie = ""
        Estado = 0
        IdSucursal = 0
        TipodeCambio = 0
        IdMoneda = 0
        FolioReferencia = 0
        SerieReferencia = ""
        IdFormadePago = 0
        TipoSaldo = 0
        Comm.Connection = Conexion
        Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandType = CommandType.Text
        Comm.CommandText = "select idcliente,fecha,folio,credito,totalapagar,hora,serie,estado,idsucursal,tipodecambio,idmoneda,ifnull(folioreferencia,0) folioreferencia,ifnull(seriereferencia,'') seriereferencia,idforma,tiposaldo from tbldocumentosclientes where iddocumento=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdCliente = DReader("idcliente")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            Credito = DReader("credito")
            TotalaPagar = DReader("totalapagar")
            Hora = DReader("hora")
            Serie = DReader("serie")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            TipodeCambio = DReader("tipodecambio")
            IdMoneda = DReader("idmoneda")
            FolioReferencia = DReader("folioreferencia")
            SerieReferencia = DReader("seriereferencia")
            IdFormadePago = DReader("idforma")
            TipoSaldo = DReader("tiposaldo")
        End If
        DReader.Close()
        Cliente = New dbClientes(IdCliente, Comm.Connection)
    End Sub

    Public Sub Guardar(ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pEstado As Integer, ByVal pTotalAPagar As Double, ByVal pidSucursal As Integer, ByVal pTipodeCambio As Double, ByVal pIdMoneda As Integer, ByVal pFolioReferencia As Integer, ByVal pSerieReferencia As String, ByVal pIdFormadePago As Integer, ByVal pTipoSaldo As Integer)
        IdCliente = pIdCliente
        Fecha = pFecha
        TotalaPagar = pTotalAPagar
        IdSucursal = pidSucursal
        TipodeCambio = pTipodeCambio
        IdMoneda = pIdMoneda
        FolioReferencia = pFolioReferencia
        SerieReferencia = pSerieReferencia
        IdFormadePago = pIdFormadePago
        Estado = pEstado
        TipoSaldo = pTipoSaldo
        If TipoSaldo = 1 Then
            Comm.CommandText = "insert into tbldocumentosclientes(idcliente,fecha,folio,credito,totalapagar,hora,serie,estado,idsucursal,tipodecambio,fechacancelado,horacancelado,idmoneda,folioreferencia,seriereferencia,idforma,tiposaldo,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + IdCliente.ToString + ",'" + Fecha + "'," + DaNuevoFolio("", IdSucursal).ToString + ",0," + TotalaPagar.ToString + ",'" + Format(TimeOfDay, "HH:mm:ss") + "',''," + Estado.ToString + "," + IdSucursal.ToString + "," + TipodeCambio.ToString + ",'',''," + IdMoneda.ToString + "," + FolioReferencia.ToString + ",'" + Replace(SerieReferencia, "'", "''") + "',2," + TipoSaldo.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Else
            Comm.CommandText = "insert into tbldocumentosclientes(idcliente,fecha,folio,credito,totalapagar,hora,serie,estado,idsucursal,tipodecambio,fechacancelado,horacancelado,idmoneda,folioreferencia,seriereferencia,idforma,tiposaldo,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + IdCliente.ToString + ",'" + Fecha + "'," + DaNuevoFolio("SINI", IdSucursal).ToString + ",0," + TotalaPagar.ToString + ",'" + Format(TimeOfDay, "HH:mm:ss") + "','SINI'," + Estado.ToString + "," + IdSucursal.ToString + "," + TipodeCambio.ToString + ",'',''," + IdMoneda.ToString + "," + FolioReferencia.ToString + ",'" + Replace(SerieReferencia, "'", "''") + "',2," + TipoSaldo.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(iddocumento) from tbldocumentosclientes"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pIdCliente As Integer, ByVal pEstado As Integer, ByVal pFecha As String, ByVal pTotalAPagar As Double, ByVal pidSucursal As Integer, ByVal pTipodeCambio As Double, ByVal pIdMoneda As Integer, ByVal pFolioReferencia As Integer, ByVal pSerieReferencia As String, ByVal pIdFormadePago As Integer, ByVal pTipoSaldo As Integer)
        ID = pID
        Fecha = pFecha
        Credito = Credito
        TipodeCambio = pTipodeCambio
        IdMoneda = pIdMoneda
        IdCliente = pIdCliente
        FolioReferencia = pFolioReferencia
        SerieReferencia = pSerieReferencia
        IdFormadePago = pIdFormadePago
        Estado = pEstado
        TipoSaldo = pTipoSaldo
        Comm.CommandText = "update tbldocumentosclientes set fecha='" + Fecha + "',estado=" + Estado.ToString + ",credito=" + TotalaPagar.ToString + ",tipodecambio=" + TipodeCambio.ToString + ",idmoneda=" + IdMoneda.ToString + ",totalapagar=" + pTotalAPagar.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',idcliente=" + IdCliente.ToString + ",folioreferencia=" + FolioReferencia.ToString + ",seriereferencia='" + Replace(Trim(SerieReferencia), "'", "''") + "',idforma=" + IdFormadePago.ToString + ",tiposaldo=" + TipoSaldo.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where iddocumento=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificaEstado(ByVal piddocumento As Integer, ByVal pEstado As Byte)
        Comm.CommandText = "update tbldocumentosclientes set estado=" + pEstado.ToString + " where iddocumento=" + piddocumento.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbldocumentosclientes where iddocumento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pidcliente As Integer, ByVal pFolio As String, ByVal pEstado As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select dc.iddocumento,dc.fecha,if(dc.tiposaldo=0,dc.serie,dc.seriereferencia) as seriereferencia,if(dc.tiposaldo=0,folio,folioreferencia) as folioreferencia,dc.totalapagar,if(dc.idmoneda=2,'PESOS','DOLARES') moneda,if(idmoneda=2,1,dc.tipodecambio) tipodecambio,case dc.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelado' end  as sestado,case dc.tiposaldo when 0 then 'Saldo Inicial' when 1 then 'Documento' end as tiposaldostr from tbldocumentosclientes dc inner join tblclientes c on dc.idcliente=c.idcliente where true"
        'If pFecha <> "" And pFecha2 <> "" Then
        '    Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        'End If
        'If pidcliente > 0 Or pFolio <> "" Or pEstado <> 0 Then Comm.CommandText += " where "
        If pidcliente <> 0 Then
            Comm.CommandText += " and dc.idcliente=" + pidcliente.ToString
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(dc.serie,convert(dc.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado <> 0 Then
            Comm.CommandText += " and dc.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and dc.estado<>1"
        End If

        Comm.CommandText += " order by dc.fecha,dc.hora,dc.serie,dc.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldocumentosclientes")
        Return DS.Tables("tbldocumentosclientes").DefaultView
    End Function
    Public Function Reporte(ByVal pidcliente As Integer, ByVal pFecha As String, ByVal pFecha2 As String, ByVal pSerie As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select dc.iddocumento,dc.fecha,if(dc.tiposaldo=0,dc.serie,dc.seriereferencia) as seriereferencia,if(dc.tiposaldo=0,folio,folioreferencia) as folioreferencia,dc.totalapagar,case dc.estado when 2 then 'Pendiente' when 3 then 'Activo' when 4 then 'Cancelado' end  as sestado,case dc.tiposaldo when 0 then 'Saldo Inicial' when 1 then 'Documento' end as tiposaldostr,c.idcliente,c.clave,c.nombre from tbldocumentosclientes dc inner join tblclientes c on dc.idcliente=c.idcliente where dc.fecha>='" + pFecha + "' and dc.fecha<='" + pFecha2 + "' and dc.estado>2 "
        If pidcliente <> 0 Then
            Comm.CommandText += " and dc.idcliente=" + pidcliente.ToString
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and dc.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        Comm.CommandText += " order by dc.fecha,dc.hora,dc.serie,dc.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldocumentosclientes")
        'DS.WriteXmlSchema("tbldocumentosclientes.xml")
        Return DS.Tables("tbldocumentosclientes").DefaultView
    End Function
    Public Function ConsultaDeudas(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidCliente As Integer, ByVal pFolio As String, ByVal pidTipodePago As Integer, ByVal PorFechas As Boolean, ByVal Todas As Boolean, ByVal pTipodeOrden As Byte, ByVal pMostrarCanceladas As Boolean, ByVal pTipodeCambio As Double, ByVal pEnPesos As Boolean) As DataView
        Dim DS As New DataSet

        Comm.CommandText = "delete from tblclientesdeudas where idcliente=" + pidCliente.ToString
        Comm.ExecuteNonQuery()

        'Facturas
        If pEnPesos Then
            Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        ",tbldocumentosclientes.iddocumento,0,tbldocumentosclientes.fecha,tbldocumentosclientes.estado,tbldocumentosclientes.serie,tbldocumentosclientes.folio,if(tbldocumentosclientes.idconversion=2,tbldocumentosclientes.credito,tbldocumentosclientes.credito*" + pTipodeCambio.ToString + "),if(tbldocumentosclientes.idconversion=2,tbldocumentosclientes.totalapagar,tbldocumentosclientes.totalapagar*" + pTipodeCambio.ToString + "),0 from tbldocumentosclientes inner join tblclientes on tbldocumentosclientes.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tbldocumentosclientes.idforma where tbldocumentosclientes.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        Else
            Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        ",tbldocumentosclientes.iddocumento,0,tbldocumentosclientes.fecha,tbldocumentosclientes.estado,tbldocumentosclientes.serie,tbldocumentosclientes.folio,tbldocumentosclientes.credito,tbldocumentosclientes.totalapagar,0 from tbldocumentosclientes inner join tblclientes on tbldocumentosclientes.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tbldocumentosclientes.idforma where tbldocumentosclientes.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        End If

        If Todas = False Then
            Comm.CommandText += " and tbldocumentosclientes.credito<tbldocumentosclientes.totalapagar"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tbldocumentosclientes.serie,convert(tbldocumentosclientes.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pMostrarCanceladas Then
            Comm.CommandText += " and (tbldocumentosclientes.estado=4 or tbldocumentosclientes.estado=3)"
        Else
            Comm.CommandText += " and tbldocumentosclientes.estado=3"
        End If
        Comm.ExecuteNonQuery()

        'Notas de Cargo
        If pEnPesos Then
            Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        ",tblnotasdecargo.idcargo,1,tblnotasdecargo.fecha,tblnotasdecargo.estado,tblnotasdecargo.serie,tblnotasdecargo.folio,if(tblnotasdecargo.idmoneda=2,tblnotasdecargo.aplicado,tblnotasdecargo.aplicado*" + pTipodeCambio.ToString + "),if(tblnotasdecargo.idmoneda=2,tblnotasdecargo.totalapagar,tblnotasdecargo.totalapagar*" + pTipodeCambio.ToString + "),0 from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.idcliente=" + pidCliente.ToString
        Else
            Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        ",tblnotasdecargo.idcargo,1,tblnotasdecargo.fecha,tblnotasdecargo.estado,tblnotasdecargo.serie,tblnotasdecargo.folio,tblnotasdecargo.aplicado,tblnotasdecargo.totalapagar,0 from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.idcliente=" + pidCliente.ToString
        End If


        If Todas = False Then
            Comm.CommandText += " and tblnotasdecargo.aplicado<tblnotasdecargo.totalapagar"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblnotasdecargo.serie,convert(tblnotasdecargo.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pMostrarCanceladas Then
            Comm.CommandText += " and (tblnotasdecargo.estado=4 or tblnotasdecargo.estado=3)"
        Else
            Comm.CommandText += " and tblnotasdecargo.estado=3"
        End If
        Comm.ExecuteNonQuery()


        Comm.CommandText = "select iddocumento as iddocumento,0 as sel,cant,fecha,if(estado=3,'A','C') as estadof,serie,folio,credito,totalapagar,totalapagar-credito as restante,tipo,totalapagar-credito as restante2 from tblclientesdeudas where idcliente=" + pidCliente.ToString

        If pTipodeOrden = 0 Then
            Comm.CommandText += " order by tipo,fecha,serie,folio"
        Else
            Comm.CommandText += " order by tipo,totalapagar,serie,folio"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesdeudas")
        Return DS.Tables("tblclientesdeudas").DefaultView
    End Function

    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tbldocumentosclientes where serie='" + Replace(Trim(pSerie), "'", "''") + "'),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Sub ChecaFolioRepetido(ByVal pFolioreferencia As Integer, ByVal pSeriereferencia As String, ByVal tiposaldo As Integer, ByVal idcliente As Integer, Optional ByVal iddocumento As Integer = -1)
        Dim Resultado As Integer = 0
        If tiposaldo = 0 Then
            Comm.CommandText = "select count(folio) from tbldocumentosclientes where idcliente=" + idcliente.ToString + " and iddocumento<>" + iddocumento.ToString + " and tiposaldo=" + tiposaldo.ToString + " and estado=3"
            If Comm.ExecuteScalar > 0 Then Throw New Exception("El cliente ya cuenta con un saldo incial.")
        Else
            Comm.CommandText = "select count(folio) from tbldocumentosclientes where folioreferencia=" + pFolioreferencia.ToString + " and seriereferencia='" + Replace(Trim(pSeriereferencia), "'", "''") + "' and iddocumento<>" + iddocumento.ToString
            If Comm.ExecuteScalar > 0 Then Throw New Exception("Folio repetido.")
        End If
    End Sub
   
    Public Sub CerrarPendientes(ByVal idcliente As String)
        Comm.CommandText = "update tbldocumentosclientes set estado=3 where estado=2 and idcliente=" + idcliente
        Comm.ExecuteNonQuery()
    End Sub
End Class
