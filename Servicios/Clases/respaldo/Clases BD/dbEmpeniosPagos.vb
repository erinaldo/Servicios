Public Class dbEmpeniosPagos
    Public Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public idPago As Integer
    Public idEmpenio As Integer
    Public Fecha As String
    Public Cantidad As Double
    Public Estado As Boolean
    Public caja As Integer
    Public hora As String
    Public idMovimiento As Integer ' movimiento de caja
    Public Folio As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        idPago = 0
        idEmpenio = 0
        Fecha = ""
        Cantidad = 0
        hora = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub llenaDatos(ByVal pidPago As Integer)
        idPago = pidPago
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblempeniosabono where idAbono=" + idPago.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            idEmpenio = DReader("idEmpenio")
            Fecha = DReader("Fecha")
            Cantidad = DReader("Cantidad")
            Estado = DReader("habilitado")
            caja = DReader("caja")
            idMovimiento = DReader("idMovimiento")
            hora = DReader("hora")
            Folio = DReader("folio")
        End If
        DReader.Close()

    End Sub

    Public Function filtroBusqueda(ByVal pidCliente As Integer, ByVal fechaI As String, ByVal fechaF As String, ByVal pSerie As String, ByVal pFolio As String, ByVal pSucursal As Integer, ByVal canceladas As Integer, ByVal pagadas As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select DISTINCT tblempenios.idmovimiento,tblempenios.fecha,concat(tblempenios.serie,'-',convert(tblempenios.folio using utf8)),tblempenios.TotalAux as total, tblempenios.fechaContrato,tblempenios.pagado, tblempenios.tipoEmpenio, DATEDIFF('" + Date.Now().ToString("yyyy/MM/dd") + "',tblempenios.fechaContrato) as Dias  from tblempenios  where tblempenios.idCliente=" + pidCliente.ToString

        If fechaI <> "" Then
            Comm.CommandText += " and tblempenios.fechaContrato>='" + fechaI + "' and tblempenios.fechaContrato<='" + fechaF + "'"

        End If

        If pSerie <> "" Then
            Comm.CommandText += " and tblempenios.serie like'%" + pSerie + "%'"

        End If
        If pFolio <> "" Then
            Comm.CommandText += " and tblempenios.folio like'%" + pFolio + "%'"
        End If
        If pSucursal > 0 Then
            Comm.CommandText += " and tblempenios.idsucursal =" + pSucursal.ToString + ""
        End If
        If canceladas = 0 Then
            Comm.CommandText += " and tblempenios.estado <>4"
        End If
        If pagadas = 0 Then
            '    Comm.CommandText += " and tblempenios.pagado =true"
            'Else
            Comm.CommandText += " and tblempenios.pagado =0"
        End If

        Comm.CommandText += "  group by idmovimiento ORDER BY tblempenios.fecha DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempenios")
        Return DS.Tables("tblempenios")
    End Function

    Public Function buscarDescripcion(ByVal pidMovimiento As Integer) As String
        Dim DS As New DataSet
        Dim tabla As DataTable
        Dim descripcion As String = ""
        Comm.CommandText = "select descripcion from tblempeniosdetalles where idMovimiento=" + pidMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempenios")
        tabla = DS.Tables("tblempenios")
        For i As Integer = 0 To tabla.Rows.Count - 1
            descripcion += tabla.Rows(i)(0).ToString + ". "
        Next
        Return descripcion
    End Function
    Public Function buscarDescripcionV(ByVal pidMovimiento As Integer) As String
        Dim DS As New DataSet
        Dim tabla As DataTable
        Dim descripcion As String = ""
        Comm.CommandText = "select marca,', ', modelo,', ',color from tblempeniosdetallesv where idMovimiento=" + pidMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempenios")
        tabla = DS.Tables("tblempenios")
        For i As Integer = 0 To tabla.Rows.Count - 1
            descripcion += tabla.Rows(i)(0).ToString + ". "
        Next
        Return descripcion
    End Function
    Public Function buscarDescripcionT(ByVal pidMovimiento As Integer) As String
        Dim DS As New DataSet
        Dim tabla As DataTable
        Dim descripcion As String = ""
        Comm.CommandText = "select descripcion from tblempeniosdetallest where idMovimiento=" + pidMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempenios")
        tabla = DS.Tables("tblempenios")
        For i As Integer = 0 To tabla.Rows.Count - 1
            descripcion += tabla.Rows(i)(0).ToString + ". "
        Next
        Return descripcion
    End Function

    Public Function buscarPagos(ByVal idEmpenio As Integer, ByVal pFecha As String) As Double
        Dim totoal As Double = 0
        If pFecha <> "" Then
            Comm.CommandText = "select sum(cantidad) from tblempeniosabono where idEmpenio=" + idEmpenio.ToString + " and fecha>='" + Format(CDate(pFecha), "yyyy/MM/dd") + "' and habilitado=0 ORDER BY fecha DESC"
        Else
            Return 0
        End If

        Dim resultn As Object = Comm.ExecuteScalar()
        If resultn IsNot DBNull.Value Then
            totoal = Comm.ExecuteScalar
        End If

        Return totoal
    End Function
    Public Function buscarDescripcioncom(ByVal pidEmpenio As Integer)
        Dim descripcion As String
        Dim tipo As Integer
        Comm.CommandText = "select tblempenios.tipoEmpenio from tblempenios where idmovimiento=" + pidEmpenio.ToString
        tipo = Comm.ExecuteScalar

        If tipo = 0 Then
            'Joyas
            Comm.CommandText = "select CONCAT(if (tblempeniosdetalles.tipo=0,'ORO','PLATA'),'  ',tblempeniosdetalles.descripcion,'  ', CASE tblempeniosdetalles.kilates WHEN 0 THEN '0k' WHEN 1 THEN '8k' WHEN 2 THEN '10k' WHEN 3 THEN '14k' WHEN 4 THEN '18k' WHEN 5 THEN '24k' ELSE 'otro/desconocido' End,'  ',tblempeniosdetalles.peso,'gr.') as concepto from tblempeniosdetalles  where idMovimiento=" + pidEmpenio.ToString
            descripcion = Comm.ExecuteScalar
            Return descripcion
        Else
            If tipo = 1 Then
                'Autos
                Comm.CommandText = "select CONCAT('MARCA:',tblempeniosdetallesv.marca,' MODELO:' ,tblempeniosdetallesv.modelo,' COLOR:',tblempeniosdetallesv.color,' N° SERIE:',tblempeniosdetallesv.noSerie,' PLACAS:',tblempeniosdetallesv.placas) as concepto from tblempeniosdetallesv  where idMovimiento=" + pidEmpenio.ToString
                descripcion = Comm.ExecuteScalar
            Else
                'Terrenos
                Comm.CommandText = "select tblempeniosdetallest.descripcion from tblempeniosdetallest  where tblempeniosdetallest.idMovimiento=" + pidEmpenio.ToString

                descripcion = Comm.ExecuteScalar
            End If
        End If



        Return descripcion
    End Function
    Public Sub renovado(ByVal pID As Integer)
     
        Comm.CommandText = "update tblempenios set renovado=1 where idmovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function filtroBusqueda(ByVal pidEmpenio As Integer, ByVal pFecha As String, pVer As Byte) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idAbono,idempenio,fecha,FORMAT(cantidad,2)as Cantidad, if(habilitado=0,'Activo', 'Inactivo') as Estado,if(descuento=1,'Desc.',' ') as Descuento from tblempeniosabono where idEmpenio=" + pidEmpenio.ToString
        If pVer <> 0 Then
            Comm.CommandText += " and vis=1"
        End If
        Comm.CommandText += " ORDER BY fecha DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosPagos")
        Return DS.Tables("tblempeniosPagos")
    End Function

    Public Sub GuardarPago(ByVal pIdEmpenio As Integer, ByVal pFecha As String, ByVal pCantidad As Double, ByVal pTotalG As Double, ByVal pRefrengoG As Double, ByVal pTipo As Integer, ByVal pCaja As Integer, ByVal pIDMovimiento As Integer, ByVal pDescuento As Integer)
        idEmpenio = pIdEmpenio
        Fecha = pFecha
        Cantidad = pCantidad
        Comm.CommandText = "insert into tblempeniosabono(idEmpenio, fecha, cantidad,habilitado,totalEmepnio,totalRefrendo,tipoPago,caja, idMovimiento,descuento,hora,vis,folio) values(" + idEmpenio.ToString + " , '" + Replace(Fecha, "'", "''") + "', " + Cantidad.ToString + ",0," + pTotalG.ToString + "," + pRefrengoG.ToString + "," + pTipo.ToString + "," + pCaja.ToString + "," + pIDMovimiento.ToString + "," + pDescuento.ToString + ",'" + Date.Now.ToString("HH:mm:ss") + "',1,0);"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarPago(ByVal pIdPago As Integer, ByVal pIdEmpenio As Integer, ByVal pFecha As String, ByVal pCantidad As Double, ByVal pDescuento As Integer)
        idPago = pIdPago
        idEmpenio = pIdEmpenio
        Fecha = pFecha
        Cantidad = pCantidad
        Comm.CommandText = "update tblempeniosabono set fecha='" + Replace(pFecha, "'", "''") + "', cantidad=" + Cantidad.ToString + ", descuento=" + pDescuento.ToString + " where idAbono=" + idPago.ToString
        Comm.ExecuteScalar()
    End Sub
    Public Sub EliminarPago(ByVal pIdPago As Integer)
        Comm.CommandText = "delete from tblempeniosabono  where idAbono=" + pidPago.ToString
        Comm.ExecuteScalar()
    End Sub
    Public Sub ModificarFechaEmpenio(ByVal pIdEmpenio As Integer, ByVal pFecha As String)
        Comm.CommandText = "update tblempenios set fechaContrato='" + Replace(pFecha, "'", "''") + "' where idmovimiento=" + pIdEmpenio.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarFolio(ByVal pIdEmpenio As Integer, ByVal pSerie As String, ByVal pFolio As String)
        Comm.CommandText = "update tblempenios set folio=" + pFolio + ", serie='" + Replace(pSerie, "'", "''") + "' where idmovimiento=" + pIdEmpenio.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarTotalAuxEmpenio(ByVal pIdEmpenio As Integer, ByVal pTotal As Double)
        Comm.CommandText = "update tblempenios set TotalAux=" + pTotal.ToString + " where idmovimiento=" + pIdEmpenio.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarHabilitado(ByVal pidEmpenio As Integer)
        Comm.CommandText = "update tblempeniosabono set habilitado=1 where idEmpenio=" + pidEmpenio.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub ModificarPagado(ByVal pidEmpenio As Integer)
        Comm.CommandText = "update tblempenios set pagado=1 where idmovimiento=" + pidEmpenio.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function buscarUltimoPago(ByVal idEmpenio As Integer) As String
        Dim fechita As String = Date.Now.ToString("yyyy/MM/dd")
        Comm.CommandText = "select max(fecha) from tblempeniosabono where idEmpenio=" + idEmpenio.ToString
        Dim resultn As Object = Comm.ExecuteScalar()
        If resultn IsNot DBNull.Value Then
            fechita = Comm.ExecuteScalar
        End If
        Return fechita
    End Function
    Public Function buscaTotalAux(ByVal pidMovimiento As Integer) As String
        Dim Resultado As String
        Comm.CommandText = "select TotalAux from tblEmpenios where idMovimiento=" + pidMovimiento.ToString
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function

    Public Function ConsultaPagosExtra(pFecha1 As String, pFecha2 As String, pRango1 As Double, pRango2 As Double, pTipoOrden As Byte, pIdSucursal As Integer) As MySql.Data.MySqlClient.MySqlDataReader

        Comm.CommandText = "select ea.idabono,concat(e.serie,' ',convert(e.folio using utf8)) as folioe,ea.fecha,ea.cantidad,ea.vis,ea.folio from tblempeniosabono as ea inner join tblempenios as e on ea.idempenio=e.idmovimiento where ea.fecha>='" + pFecha1 + "' and ea.fecha<='" + pFecha2 + "' and tipopago=1"
        'If pRango1 <> 0 Then
        If pRango2 = 0 Then pRango2 = pRango1
        Comm.CommandText += " and ea.cantidad>=" + pRango1.ToString + " and ea.cantidad<=" + pRango2.ToString
        'End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and e.idsucursal=" + pIdSucursal.ToString
        End If
        If pTipoOrden = 0 Then
            Comm.CommandText += " order by ea.fecha"
        End If
        If pTipoOrden = 1 Then
            Comm.CommandText += " order by ea.cantidad"
        End If
        If pTipoOrden = 2 Then
            Comm.CommandText += " order by ea.cantidad desc"
        End If

        'Dim DS As New DataSet
        'Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        'DA.Fill(DS, "tblepagos")
        Return Comm.ExecuteReader
    End Function
    Public Sub OcultaPago(pidAbono As Integer, Ver As Byte)
        Comm.CommandText = "update tblempeniosabono set vis=" + Ver.ToString + ",folio=" + DaNuevoFolio().ToString + " where idabono=" + pidAbono.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaNuevoFolio() As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblempeniosabono),0)"
        Return Comm.ExecuteScalar + 1
    End Function
    Public Sub DeshacerVis(pfecha As String, pIdSucursal As Integer, pVis As Byte)
        Comm.CommandText = "update tblempeniosabono inner join tblempenios on tblempeniosabono.idempenio=tblempenios.idmovimiento set tblempeniosabono.vis=" + pVis.ToString + " where tblempeniosabono.fecha='" + pfecha + "' and tblempeniosabono.tipopago=1"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DiaProcesado(pFecha As String, pIdSucursal As Integer) As Boolean
        Comm.CommandText = "select count(tblempeniosabono.folio) from tblempeniosabono inner join tblempenios on tblempeniosabono.idempenio=tblempenios.idmovimiento where tblempeniosabono.fecha='" + pFecha + "' and tblempeniosabono.tipopago=1 and tblempeniosabono.folio<>0"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblempenios.idsucursal=" + pIdSucursal.ToString
        End If
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
