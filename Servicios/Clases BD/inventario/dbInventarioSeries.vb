Public Class dbInventarioSeries
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public idInventario As Integer
    Public NoSerie As String
    Public FechaCaducidad As String
    Public FechaGarantia As String
    Public idCompra As Integer
    Public idVenta As Integer
    Public idServicio As Integer
    Public idRemision As Integer
    Public idMovimiento As Integer
    Public IdRemisionC As Integer
    Public IdCotizacion As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        idInventario = 0
        NoSerie = ""
        FechaCaducidad = ""
        FechaGarantia = ""
        idCompra = 0
        idVenta = 0
        idRemision = 0
        idServicio = 0
        idMovimiento = 0
        IdCotizacion = 0
        IdRemisionC = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblinventarioseries where idserie=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            idInventario = DReader("idinventario")
            NoSerie = DReader("noserie")
            FechaCaducidad = DReader("fechacaducidad")
            FechaGarantia = DReader("fechagarantia")
            idCompra = DReader("idcompra")
            idVenta = DReader("idventa")
            idServicio = DReader("idservicio")
            idRemision = DReader("idremision")
            IdRemisionC = DReader("idremisionc")
            IdCotizacion = DReader("idcotizacion")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pIdInventario As Integer, ByVal pNoSerie As String, ByVal pFechaCaducidad As String, ByVal pFechaGarantia As String, ByVal pidCompra As Integer, ByVal pidVenta As Integer, ByVal pidRemision As Integer, ByVal pidServicio As Integer, ByVal pidMovimiento As Integer, ByVal pidRemisionC As Integer, ByVal pIdCotizacion As Integer)
        idInventario = pIdInventario
        NoSerie = pNoSerie
        FechaCaducidad = pFechaCaducidad
        FechaGarantia = pFechaGarantia
        idCompra = pidCompra
        idVenta = pidVenta
        idRemision = pidRemision
        idServicio = pidServicio
        idMovimiento = pidMovimiento
        IdRemisionC = pidRemisionC
        IdCotizacion = pIdCotizacion
        Comm.CommandText = "insert into tblinventarioseries(idinventario,noserie,fechacaducidad,fechagarantia,idcompra,idventa,idremision,idservicio,idmovimiento,idremisionc,idcotizacion) values(" + idInventario.ToString + ",'" + NoSerie.ToString + "','" + FechaCaducidad + "','" + FechaGarantia + "'," + idCompra.ToString + "," + idVenta.ToString + "," + idRemision.ToString + "," + idServicio.ToString + "," + idMovimiento.ToString + "," + IdRemisionC.ToString + "," + IdCotizacion.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNoSerie As String, ByVal pFechaCaducidad As String, ByVal pFechaGarantia As String)
        ID = pID
        NoSerie = pNoSerie
        FechaCaducidad = pFechaCaducidad
        FechaGarantia = pFechaGarantia
        Comm.CommandText = "update tblinventarioseries set noserie='" + NoSerie.ToString + "',fechacaducidad='" + FechaCaducidad + "',fechagarantia='" + FechaGarantia + "' where idserie=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub AsignaSerieAVenta(ByVal pID As Integer, ByVal pidVenta As Integer)
        ID = pID
        idVenta = pidVenta
        Comm.CommandText = "update tblinventarioseries set idventa=" + idVenta.ToString + " where idserie=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitaSerieAVenta(ByVal pID As Integer)
        ID = pID
        Comm.CommandText = "update tblinventarioseries set idventa=0 where idserie=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
   
    Public Sub AsignaSerieARemision(ByVal pID As Integer, ByVal pidRemision As Integer)
        ID = pID
        idRemision = pidRemision
        Comm.CommandText = "update tblinventarioseries set idremision=" + idRemision.ToString + " where idserie=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitaSerieARemision(ByVal pID As Integer)
        ID = pID
        Comm.CommandText = "update tblinventarioseries set idremision=0 where idserie=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblinventarioseries where idserie=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdInventario As Integer, ByVal pNoserie As String, Optional ByVal pIdCompra As Integer = 0, Optional ByVal pIdVenta As Integer = 0, Optional ByVal pIdRemision As Integer = 0, Optional ByVal pResultadosSinVenta As Boolean = False, Optional ByVal pidMovimiento As Integer = 0, Optional ByVal pIDRemisionC As Integer = 0, Optional ByVal pIdCotizacion As Integer = 0) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idserie,noserie,fechagarantia,fechacaducidad,idventa,idcompra,idmovimiento,idremision,idremisionc,idcotizacion from tblinventarioseries where idinventario=" + pIdInventario.ToString + " and noserie like '%" + pNoserie + "%'"
        If pIdCompra <> 0 Then Comm.CommandText += " and idcompra=" + pIdCompra.ToString
        If pidMovimiento <> 0 Then Comm.CommandText += " and idmovimiento=" + pidMovimiento.ToString
        If pIdVenta <> 0 Then Comm.CommandText += " and idventa=" + pIdVenta.ToString
        If pIdRemision <> 0 Then Comm.CommandText += " and idremision=" + pIdRemision.ToString
        If pIDRemisionC <> 0 Then Comm.CommandText += " and idremisionc=" + pIDRemisionC.ToString
        If pIdCotizacion <> 0 Then Comm.CommandText += " and idcotizacion=" + pIdCotizacion.ToString
        If pResultadosSinVenta Then Comm.CommandText += " and idventa=0 and idremision=0 and idcotizacion=0"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioseries")
        Return DS.Tables("tblinventarioseries").DefaultView
    End Function
    Public Function ChecaNoSerieRepetido(ByVal pNoSerie As String, ByVal pIdInventario As Integer) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(noserie) from tblinventarioseries where noserie='" + pNoSerie + "' and idinventario=" + pIdInventario.ToString
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaNoSerieRepetido = False
        Else
            ChecaNoSerieRepetido = True
        End If
    End Function
    Public Function CantidadDeSeriesAgregadasaVenta(ByVal pidVenta As Integer, ByVal pIdInventario As Integer) As Integer
        Comm.CommandText = "select count(noserie) from tblinventarioseries where idventa=" + pidVenta.ToString
        If pIdInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pIdInventario.ToString
        End If
        Return Comm.ExecuteScalar
    End Function
    Public Sub QuitaSeriesAVenta(ByVal pidVenta As Integer)
        Comm.CommandText = "update tblinventarioseries set idventa=0 where idventa=" + pidVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitarSeriesAventaxArticulo(ByVal pidInventario As Integer, ByVal pidVenta As Integer)
        Comm.CommandText = "update tblinventarioseries set idventa=0 where idinventario=" + pidInventario.ToString + " and idventa=" + pidVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function CantidadDeSeriesAgregadasaRemision(ByVal pidRemision As Integer, ByVal pidInventario As Integer) As Integer
        Comm.CommandText = "select count(noserie) from tblinventarioseries where idremision=" + pidRemision.ToString
        If pidInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pidInventario.ToString
        End If
        Return Comm.ExecuteScalar
    End Function
    Public Function CantidadDeSeriesAgregadasaCompra(ByVal pidCompra As Integer, ByVal pidInventario As Integer) As Integer
        Comm.CommandText = "select count(noserie) from tblinventarioseries where idcompra=" + pidCompra.ToString
        If pidInventario <> 0 Then Comm.CommandText += " and idinventario=" + pidInventario.ToString
        Return Comm.ExecuteScalar
    End Function
    Public Function CantidadDeSeriesAgregadasaMovimiento(ByVal pidMovimiento As Integer, ByVal pidInventario As Integer) As Integer
        Comm.CommandText = "select count(noserie) from tblinventarioseries where idmovimiento=" + pidMovimiento.ToString
        If pidInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pidInventario.ToString
        End If
        Return Comm.ExecuteScalar
    End Function
    Public Sub QuitaSeriesARemision(ByVal pidRemision As Integer)
        Comm.CommandText = "update tblinventarioseries set idremision=0 where idremision=" + pidRemision.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitarSeriesAremisionxArticulo(ByVal pidInventario As Integer, ByVal pidRemision As Integer)
        Comm.CommandText = "update tblinventarioseries set idremision=0 where idinventario=" + pidInventario.ToString + " and idremision=" + pidRemision.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub AsignaSerieAMovimiento(ByVal pID As Integer, ByVal pidMovimiento As Integer)
        ID = pID
        idMovimiento = pidMovimiento
        Comm.CommandText = "update tblinventarioseries set idmovimiento=" + idMovimiento.ToString + " where idserie=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitaSerieAMovimiento(ByVal pID As Integer)
        ID = pID
        Comm.CommandText = "update tblinventarioseries set idmovimiento=0 where idserie=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitaSeriesAMovimiento(ByVal pidMovimiento As Integer)
        Comm.CommandText = "update tblinventarioseries set idmovimiento=0 where idmovimiento=" + pidMovimiento.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitarSeriesAmovimientoxArticulo(ByVal pidInventario As Integer, ByVal pidMovimiento As Integer)
        Comm.CommandText = "update tblinventarioseries set idmovimiento=0 where idinventario=" + pidInventario.ToString + " and idmovimiento=" + pidMovimiento.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub AsignaSerieACotizacion(ByVal pID As Integer, ByVal pidVenta As Integer)
        ID = pID
        idVenta = pidVenta
        Comm.CommandText = "update tblinventarioseries set idcotizacion=" + idVenta.ToString + " where idserie=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitaSerieACotizacion(ByVal pID As Integer)
        ID = pID
        Comm.CommandText = "update tblinventarioseries set idcotizacion=0 where idserie=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitaSeriesACotizacion(ByVal pidCotizacion As Integer)
        Comm.CommandText = "update tblinventarioseries set idcotizacion=0 where idcotizacion=" + pidCotizacion.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitarSeriesACotizacionxArticulo(ByVal pidInventario As Integer, ByVal pidVenta As Integer)
        Comm.CommandText = "update tblinventarioseries set idcotizacion=0 where idinventario=" + pidInventario.ToString + " and idcotizacion=" + pidVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function CantidadDeSeriesAgregadasaCotizacion(ByVal pidCotizacion As Integer, ByVal pidInventario As Integer) As Integer
        Comm.CommandText = "select count(noserie) from tblinventarioseries where idcotizacion=" + pidCotizacion.ToString
        If pidInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pidInventario.ToString
        End If
        Return Comm.ExecuteScalar
    End Function
    Public Function CantidadDeSeriesAgregadasaRemisionCompra(ByVal pidRemision As Integer, ByVal pidInventario As Integer) As Integer
        Comm.CommandText = "select count(noserie) from tblinventarioseries where idremisionc=" + pidRemision.ToString
        If pidInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pidInventario.ToString
        End If
        Return Comm.ExecuteScalar
    End Function

    Public Sub QuitaSeriesACompra(ByVal pidCompra As Integer)
        Comm.CommandText = "delete from tblinventarioseries where idcompra=" + pidCompra.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitarSeriesACompraxArticulo(ByVal pidInventario As Integer, ByVal pidCompra As Integer)
        Comm.CommandText = "delete from tblinventarioseries where idinventario=" + pidInventario.ToString + " and idcompra=" + pidCompra.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub QuitaSeriesARemisionCompra(ByVal pidRemision As Integer)
        Comm.CommandText = "delete from tblinventarioseries where idremisionc=" + pidRemision.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitarSeriesARemisionCompraxArticulo(ByVal pidInventario As Integer, ByVal pidRemision As Integer)
        Comm.CommandText = "delete from tblinventarioseries where idinventario=" + pidInventario.ToString + " and idremisionc=" + pidRemision.ToString
        Comm.ExecuteNonQuery()
    End Sub

End Class
