Public Class dbEmpeniosAdjudicaciones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Dim pesoTotal As Double
    Dim kilatesTotal As Double
    Dim evaluoTotal As Double
    Dim Folio As String
    Public cantFechaUltomo As Double
    Public TotalPeso As Double
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)

        Inicia()
        Comm.Connection = Conexion
    End Sub
    Public Sub Inicia()
        Folio = ""
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ' IdEmpleado = pID
        Comm.Connection = Conexion
        '  LlenaDatos(IdEmpleado)
    End Sub
    'Public Function buscarDescripcion(ByVal pidMovimiento As Integer) As String
    '    Dim DS As New DataSet
    '    Dim tabla As DataTable
    '    pesoTotal = 0
    '    kilatesTotal = 0
    '    evaluoTotal = 0
    '    Dim descripcion As String = ""
    '    Comm.CommandText = "select descripcion, peso, kilates, evaluo from tblempeniosdetalles where idMovimiento=" + pidMovimiento.ToString
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblempenios")
    '    tabla = DS.Tables("tblempenios")
    '    For i As Integer = 0 To tabla.Rows.Count - 1
    '        pesoTotal += Double.Parse(tabla.Rows(i)(1).ToString)
    '        kilatesTotal += Double.Parse(tabla.Rows(i)(2).ToString)
    '        evaluoTotal += Double.Parse(tabla.Rows(i)(3).ToString)
    '        descripcion += tabla.Rows(i)(0).ToString + ". "
    '    Next
    '    '  descripcion += " Peso: " + APeso.ToString("0.00") + " Kilates: " + Akilates.ToString("0.00") + " Evalúo: " + Aevaluo.ToString("C2")
    '    Return descripcion
    'End Function
    Public Function buscarDescripcion(ByVal pIdMovimiento As Integer) As String
        Dim descripcion As String
        Comm.CommandText = "select CONCAT(if (tblempeniosdetalles.tipo=0,'Oro','Plata'),'  ',tblempeniosdetalles.descripcion,'  ', CASE tblempeniosdetalles.kilates WHEN 0 THEN '0k' WHEN 1 THEN '8k' WHEN 2 THEN '10k' WHEN 3 THEN '14k' WHEN 4 THEN '18k' WHEN 5 THEN '24k' ELSE 'otro/desconocido' End,'  ',tblempeniosdetalles.peso,'gr.') as concepto, precio as Precio from tblempeniosdetalles  where idMovimiento=" + pIdMovimiento.ToString

        descripcion = Comm.ExecuteScalar
        Return descripcion
    End Function
    Public Function buscarDescripcionv(ByVal pIdMovimiento As Integer) As String
        Dim descripcion As String
        Comm.CommandText = "select CONCAT('MARCA:',tblempeniosdetallesv.marca,' MODELO:' ,tblempeniosdetallesv.modelo,' COLOR:',tblempeniosdetallesv.color,' N° SERIE:',tblempeniosdetallesv.noSerie,' PLACAS:',tblempeniosdetallesv.placas) as concepto, importe as Precio from tblempeniosdetallesv  where idMovimiento=" + pIdMovimiento.ToString
        descripcion = Comm.ExecuteScalar
        Return descripcion
    End Function
    Public Function buscarDescripciont(ByVal pIdMovimiento As Integer) As String
        Dim descripcion As String
        Comm.CommandText = "select tblempeniosdetallest.descripcion, importe as Precio from tblempeniosdetallest  where tblempeniosdetallest.idMovimiento=" + pIdMovimiento.ToString

        descripcion = Comm.ExecuteScalar
        Return descripcion
    End Function

    Public Function filtroBusqueda(ByVal fechaI As String, ByVal fechaF As String, ByVal pSerie As String, ByVal pFolio As String, ByVal pSucursal As Integer, ByVal pidCliente As Integer) As DataTable
        'Dim DS As New DataSet
        'Dim fechita As Date = Date.Now()

        'fechita = fechita.AddDays(-121)

        'Comm.CommandText = "select DISTINCT tblempenios.idmovimiento,tblempenios.fechaContrato,tblempenios.fechaContrato,concat(tblempenios.serie,'-',tblempenios.folio) as Folio,tblempenios.TotalAux as total,tblclientes.nombre, tblempenios.tipoEmpenio   from tblempenios  inner join tblclientes on tblempenios.idcliente=tblclientes.idcliente where tblempenios.pagado=0 and tblempenios.estado=3 and tblempenios.Adjudicado=0 "

        'If fechaI <> "" Then
        '    Comm.CommandText += " and tblempenios.fechaContrato>='" + fechaI + "' and tblempenios.fechaContrato<='" + fechaF + "'"
        'End If

        'If pSerie <> "" Then
        '    Comm.CommandText += " and tblempenios.serie like'%" + pSerie + "%'"

        'End If
        'If pFolio <> "" Then
        '    Comm.CommandText += " and tblempenios.folio like'%" + pFolio + "%'"
        'End If
        'If pidCliente <> 0 Then
        '    Comm.CommandText += " and tblempenios.idcliente=" + pidCliente.ToString
        'End If
        'If pSucursal > 0 Then
        '    Comm.CommandText += " and tblempenios.idsucursal =" + pSucursal.ToString + ""
        'End If

        'Comm.CommandText += "  group by idmovimiento ORDER BY concat(tblempenios.serie,'-',tblempenios.folio) asc "
        'Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        'DA.Fill(DS, "tblempenios")
        'Return DS.Tables("tblempenios")

        Comm.CommandText = "select ifnull(("
        'Cantidad Ultimo Pago
        Comm.CommandText += "select sum(tblempeniosdetalles.peso)"

        Comm.CommandText += " from tblempenios  inner join tblclientes on tblempenios.idcliente=tblclientes.idcliente inner join tblempeniosdetalles on tblempeniosdetalles.idmovimiento=tblempenios.idmovimiento where tblempenios.pagado=0 and tblempenios.estado=3 and tblempenios.Adjudicado=0 "

        If fechaI <> "" Then
            Comm.CommandText += " and tblempenios.fechaContrato>='" + fechaI + "' and tblempenios.fechaContrato<='" + fechaF + "'"
        End If

        If pSerie <> "" Then
            Comm.CommandText += " and tblempenios.serie like'%" + pSerie + "%'"

        End If
        If pFolio <> "" Then
            Comm.CommandText += " and tblempenios.folio like'%" + pFolio + "%'"
        End If
        If pidCliente <> 0 Then
            Comm.CommandText += " and tblempenios.idcliente=" + pidCliente.ToString
        End If
        If pSucursal > 0 Then
            Comm.CommandText += " and tblempenios.idsucursal =" + pSucursal.ToString
        End If
        Comm.CommandText += "),0)"

        TotalPeso = Comm.ExecuteScalar




        Dim DS As New DataSet
        Dim fechita As Date = Date.Now()

        fechita = fechita.AddDays(-121)
        Comm.CommandTimeout = 10000
        'idMovimiento
        Comm.CommandText = "select 0 as Selec, tblempenios.idmovimiento as idMovimiento,"
        'Folio
        Comm.CommandText += " tblempenios.folio as Folio,"
        'Fecha Empeño Inicio
        Comm.CommandText += " tblempenios.fecha as Fecha,"
        'Fecha Renvacion
        Comm.CommandText += " tblempenios.fechaContrato as FechaContrato,"
        'Descripcion empeño
        Comm.CommandText += " if(tblempenios.tipoEmpenio=0,(select CONCAT(if (tblempeniosdetalles.tipo=0,'ORO -','PLATA -'),'  ',tblempeniosdetalles.peso,'gr.') from tblempeniosdetalles  where tblempeniosdetalles.idMovimiento=tblEmpenios.idMovimiento limit 1),if(tblempenios.tipoEmpenio=1,'CARRO','TERRENO')) as Descripcion,"
        'cliente
        Comm.CommandText += " tblclientes.nombre as Cliente,"
        'Total restante
        Comm.CommandText += " tblempenios.TotalAux as Total, "
        'refrendo
        Comm.CommandText += " if (DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)=0,0,if (DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)>=1 and DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)<=30,(((tblempenios.TotalAux*tblempenios.A1a30*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)+((tblempenios.TotalAux*tblempenios.B1a30*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)),if (DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)>=31 and DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)<=60,(((tblempenios.TotalAux*tblempenios.A31a60*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)+((tblempenios.TotalAux*tblempenios.B31a60*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)),if (DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)>=61 and DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)<=90,(((tblempenios.TotalAux*IF(tblempenios.A61a90<>0,tblempenios.A61a90,tblempenios.A31a60)*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)+((tblempenios.TotalAux*IF(tblempenios.B61a90<>0,tblempenios.B61a90,tblempenios.B31a60)*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)),(((tblempenios.TotalAux*if(tblempenios.A90mas<>0,tblempenios.A90mas,if(tblempenios.A61a90<>0,tblempenios.A61a90,tblempenios.A31a60))*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)+((tblempenios.TotalAux*if(tblempenios.B90mas<>0,tblempenios.B90mas,if(tblempenios.B61a90<>0,tblempenios.B61a90,tblempenios.B31a60))*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)))))) as Refrendo,"
        'Restante
        Comm.CommandText += " ((if(DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)=0,0,if (DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)>=1 and DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)<=30,(((tblempenios.TotalAux*tblempenios.A1a30*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)+((tblempenios.TotalAux*tblempenios.B1a30*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)),if (DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)>=31 and DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)<=60,(((tblempenios.TotalAux*tblempenios.A31a60*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)+((tblempenios.TotalAux*tblempenios.B31a60*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)),if (DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)>=61 and DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato)<=90,(((tblempenios.TotalAux*IF(tblempenios.A61a90<>0,tblempenios.A61a90,tblempenios.A31a60)*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)+((tblempenios.TotalAux*IF(tblempenios.B61a90<>0,tblempenios.B61a90,tblempenios.B31a60)*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)),(((tblempenios.TotalAux*if(tblempenios.A90mas<>0,tblempenios.A90mas,if(tblempenios.A61a90<>0,tblempenios.A61a90,tblempenios.A31a60))*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)+((tblempenios.TotalAux*if(tblempenios.B90mas<>0,tblempenios.B90mas,if(tblempenios.B61a90<>0,tblempenios.B61a90,tblempenios.B31a60))*DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato))/100)))))))+tblempenios.TotalAux) as Restante,"
        'FechaUltimoPago
        Comm.CommandText += " ifnull((Select concat(tblempeniosabono.fecha,' $',convert(format(tblempeniosabono.cantidad,2) using utf8)) from tblempeniosabono where tblempeniosabono.idEmpenio=tblempenios.idmovimiento and tblempeniosabono.descuento=0 order by fecha desc limit 1),'-') as FechaUltimoPago, "
        'Cantidad Ultimo Pago
        Comm.CommandText += " ifnull((select sum(tblempeniosdetalles.peso) from tblempeniosdetalles where tblempeniosdetalles.idmovimiento=tblempenios.idmovimiento),0) as CantidadUltimoPago "

        Comm.CommandText += " from tblempenios  inner join tblclientes on tblempenios.idcliente=tblclientes.idcliente where tblempenios.pagado=0 and tblempenios.estado=3 and tblempenios.Adjudicado=0 "

        If fechaI <> "" Then
            Comm.CommandText += " and tblempenios.fechaContrato>='" + fechaI + "' and tblempenios.fechaContrato<='" + fechaF + "'"
        End If

        If pSerie <> "" Then
            Comm.CommandText += " and tblempenios.serie like'%" + pSerie + "%'"

        End If
        If pFolio <> "" Then
            Comm.CommandText += " and tblempenios.folio like'%" + pFolio + "%'"
        End If
        If pidCliente <> 0 Then
            Comm.CommandText += " and tblempenios.idcliente=" + pidCliente.ToString
        End If
        If pSucursal > 0 Then
            Comm.CommandText += " and tblempenios.idsucursal =" + pSucursal.ToString + ""
        End If

        Comm.CommandText += "  ORDER BY tblempenios.fechaContrato asc,tblempenios.folio "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempenios")
        'DS.WriteXmlSchema("tblEmpeniosAdjudicaciones.xml")
        Return DS.Tables("tblempenios")




    End Function
    Public Function buscarFechaUltimoPago(ByVal pIdEmpenio As Integer) As String
        Dim fechaUltimo As String = ""
        Comm.CommandText = "select COALESCE(max(fecha),'0') from tblempeniosabono where idEmpenio=" + pIdEmpenio.ToString + ";"
        fechaUltimo = Comm.ExecuteScalar
        If fechaUltimo <> "0" Then
            cantFechaUltomo = buscarCantUltimoPago(fechaUltimo, pIdEmpenio)
        Else
            cantFechaUltomo = 0
        End If
        Return fechaUltimo
    End Function
    Public Function buscarCantUltimoPago(ByVal pfecha As String, ByVal pidEmpenio As Integer) As Double

        Comm.CommandText = "select cantidad from tblempeniosabono where fecha='" + pfecha.ToString + "' and idEmpenio=" + pidEmpenio.ToString + " order by fecha DESC "
        cantFechaUltomo = Comm.ExecuteScalar
        Return cantFechaUltomo
    End Function
    Public Sub Adjudicar(ByVal pidEmpenio As Integer, ByVal pRefrendo As Double, ByVal pFechaAdj As String)

        Comm.CommandText = "update tblempenios set pagado=1, Adjudicado=1, idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idmovimiento=" + pidEmpenio.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblempeniosadjudicaciones(idEmpenio, refrendo, fechaAdjudicacion,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + pidEmpenio.ToString + "," + pRefrendo.ToString + ",'" + pFechaAdj + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        Comm.ExecuteNonQuery()
        
    End Sub
End Class
