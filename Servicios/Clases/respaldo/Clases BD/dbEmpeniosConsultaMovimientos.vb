Public Class dbEmpeniosConsultaMovimientos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub
    Public Function ConsultaMovimientos(pFecha1 As String, pFecha2 As String, pidCliente As Integer, pVis As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "delete from tblempeniosmovimientos where idcliente=" + pidCliente.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblempeniosmovimientos(id,fecha,movimiento,serie,folio,cargo,abono,saldo,pago,estado,hora,idcliente,importe) select idmovimiento, fecha,'Empeño',concat(serie,'-',convert(tblempenios.folio using utf8)),tblempenios.folio, total,0,0,'',pagado,hora," + pidCliente.ToString + ",total from tblEmpenios where idCliente=" + pidCliente.ToString + " and estado=3 and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "';"
        Comm.CommandText += "insert into tblempeniosmovimientos(id,fecha,movimiento,serie,folio,cargo,abono,saldo,pago,estado,hora,idcliente,importe) select tblempeniosadjudicaciones.idEmpenio, tblempeniosadjudicaciones.fechaAdjudicacion,'Adjudicación',concat(tblEmpenios.serie,'-',convert(tblEmpenios.folio using utf8)),tblEmpenios.folio,0,tblEmpenios.TotalAux,0,'',0,'00:00:00'," + pidCliente.ToString + ",tblEmpenios.TotalAux from tblempeniosadjudicaciones inner join tblEmpenios on tblempeniosadjudicaciones.idEmpenio=tblempenios.idmovimiento where tblempenios.idCliente=" + pidCliente.ToString + " and tblempenios.estado=3 and tblempeniosadjudicaciones.fechaAdjudicacion>='" + pFecha1 + "' and tblempeniosadjudicaciones.fechaAdjudicacion<='" + pFecha2 + "';"
        Comm.CommandText += "insert into tblempeniosmovimientos(id,fecha,movimiento,serie,folio,cargo,abono,saldo,pago,estado,hora,idcliente,importe) select 0,tblempeniosabono.fecha,'Abono',concat(serie,'-',convert(tblempenios.folio using utf8)), tblEmpenios.folio,0, tblempeniosabono.cantidad,0,if(tblempeniosabono.descuento=1,'Descuento',if(tblempeniosabono.tipoPago=0,'Capital',if(tblempeniosabono.tipoPago=1,'Interés','Renovación'))),0,tblempeniosabono.hora," + pidCliente.ToString + ",tblempeniosabono.cantidad from tblempeniosabono inner join tblempenios on tblempeniosabono.idEmpenio=tblempenios.idmovimiento  where tblempenios.idCliente=" + pidCliente.ToString + " and tblempenios.estado=3   and tblempeniosabono.fecha>='" + pFecha1 + "' and tblempeniosabono.fecha<='" + pFecha2 + "'"
        If pVis <> 0 Then
            Comm.CommandText += " and tblempeniosabono.vis=1"
        End If
        Comm.CommandText += ";"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select id,fecha,movimiento,serie,folio,importe,cargo,abono,saldo,pago,estado,hora,idcliente from tblempeniosmovimientos where idcliente=" + pidCliente.ToString + "  order by fecha,hora"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblEmpeniosMovimientos")
        'DS.WriteXmlSchema("tblEmpeniosMovimientos.xml")
        Return DS.Tables("tblEmpeniosMovimientos").DefaultView
    End Function
    Public Function UltimaConsulta(pidcliente As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select id,fecha,movimiento,serie,folio,importe,cargo,abono,saldo,pago,estado,hora,idcliente from tblempeniosmovimientos order by fecha,hora"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblEmpeniosMovimientos")
        'DS.WriteXmlSchema("tblEmpeniosMovimientos.xml")
        Return DS.Tables("tblEmpeniosMovimientos").DefaultView
    End Function
    Public Function ConsultaEmpenios(ByVal pFechaI As String, ByVal pFechaf As String, ByVal pIdCliente As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idmovimiento, fecha, folio, serie, total from tblEmpenios where idCliente=" + pIdCliente.ToString + " and estado=3 and fecha<='" + pFechaf + "' and fecha>='" + pFechaI + "' ORDER BY fecha ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblEmpenios")
        Return DS.Tables("tblEmpenios")
    End Function
    Public Function ConsultaAdjudicaciones(ByVal pFechaI As String, ByVal pFechaf As String, ByVal pIdCliente As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select tblempeniosadjudicaciones.idEmpenio, tblempeniosadjudicaciones.fechaAdjudicacion, tblEmpenios.folio, tblEmpenios.serie, tblEmpenios.TotalAux from tblempeniosadjudicaciones inner join tblEmpenios on tblempeniosadjudicaciones.idEmpenio=tblempenios.idmovimiento where tblempenios.idCliente=" + pIdCliente.ToString + " and tblempenios.estado=3 and tblempeniosadjudicaciones.fechaAdjudicacion<='" + pFechaf + "' and tblempeniosadjudicaciones.fechaAdjudicacion>='" + pFechaI + "' ORDER BY tblempeniosadjudicaciones.fechaAdjudicacion ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblAdjudicacion")
        Return DS.Tables("tblAdjudicacion")
    End Function
    Public Function ConsultaPagos(ByVal pFechaI As String, ByVal pFechaf As String, ByVal pIdCliente As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select tblempeniosabono.fecha, tblEmpenios.folio, tblEmpenios.serie, tblempeniosabono.cantidad, tblempeniosabono.totalEmepnio,tblempeniosabono.totalRefrendo,if(tblempeniosabono.descuento=1,-1,tblempeniosabono.tipoPago) as tipoPago from tblempeniosabono inner join tblempenios on tblempeniosabono.idEmpenio=tblempenios.idmovimiento  where tblempenios.idCliente=" + pIdCliente.ToString + " and tblempenios.estado=3   and tblempeniosabono.fecha<='" + pFechaf + "' and tblempeniosabono.fecha>='" + pFechaI + "' ORDER BY tblempeniosabono.fecha ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblEmpeniosAbono")
        Return DS.Tables("tblEmpeniosAbono")
    End Function
    Public Function ConsultaEmpeniosCompras(ByVal pFechaI As String, ByVal pFechaf As String, ByVal pIdCliente As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idmovimiento, fecha, folio, serie, total from tblempenioscompras where idCliente=" + pIdCliente.ToString + " and estado=3 and fecha<='" + pFechaf + "' and fecha>='" + pFechaI + "' ORDER BY fecha ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblEmpenios")
        Return DS.Tables("tblEmpenios")
    End Function
   
    'para calcular el saldo inicial
    Public Function ConsultaEmpeniosSI(ByVal pFechaI As String, ByVal pFechaf As String, ByVal pIdCliente As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idmovimiento, fecha, folio, serie, total from tblEmpenios where idCliente=" + pIdCliente.ToString + " and estado=3 and fecha<'" + pFechaI + "'  ORDER BY fecha ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblEmpenios")
        Return DS.Tables("tblEmpenios")
    End Function
    Public Function ConsultaPagosSI(ByVal pFechaI As String, ByVal pFechaf As String, ByVal pIdCliente As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select tblempeniosabono.fecha, tblEmpenios.folio, tblEmpenios.serie, tblempeniosabono.cantidad, tblempeniosabono.totalEmepnio,tblempeniosabono.totalRefrendo,tblempeniosabono.tipoPago from tblempeniosabono inner join tblempenios on tblempeniosabono.idEmpenio=tblempenios.idmovimiento  where tblempenios.idCliente=" + pIdCliente.ToString + " and tblempenios.estado=3 and tblempeniosabono.fecha<'" + pFechaI + "'  ORDER BY tblempeniosabono.fecha ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblEmpeniosAbono")
        Return DS.Tables("tblEmpeniosAbono")
    End Function
    Public Function calcularRefrendoAdjudicaciones(ByVal pIdEmpenio As Integer) As Double
        Dim pRefrendo As Double
        Comm.CommandText = "select refrendo from tblempeniosadjudicaciones where idEmpenio=" + pIdEmpenio.ToString
        pRefrendo = Comm.ExecuteScalar()

        Return pRefrendo
    End Function
End Class
