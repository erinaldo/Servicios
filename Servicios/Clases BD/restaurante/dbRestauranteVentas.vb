Imports MySql.Data.MySqlClient
Public Class dbRestauranteVentas
    Public Property idVenta As Integer
    Public Property folio As String
    Public Property fecha As String
    Public Property hora As String
    Public Property total As Double
    Public Property totalapagar As Double
    Public Property idCliente As Integer
    Public Property iva As Double
    Public Property idMesero As Integer
    Public Property descuento As Double
    Public Property estado As Integer
    Public Property idSucursal As Integer
    Public NoPersonas As Integer
    Public IdMesa As Integer
    Public IdCajero As Integer
    Public IdCaja As Integer
    Public MotivoCancelacion As String
    Private comm As New MySqlCommand

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        idVenta = -1
        folio = 0
        fecha = ""
        hora = ""
        total = 0
        totalapagar = 0
        idCliente = -1
        iva = 0
        idMesero = -1
        descuento = 0
        IdMesa = 0
        IdCajero = 0
        IdCaja = 0
        MotivoCancelacion = ""
        NoPersonas = 1
    End Sub

    Public Sub New(ByVal idVenta As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.idVenta = idVenta
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblrestauranteventas where idventa=" + idVenta.ToString()
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        While dr.Read()
            folio = dr("folio")
            fecha = dr("fecha")
            hora = dr("hora")
            total = dr("total")
            totalapagar = dr("totalapagar")
            idCliente = dr("idcliente")
            idMesero = dr("idmesero")
            descuento = dr("descuento")
            estado = dr("estado")
            idSucursal = dr("idsucursal")
            NoPersonas = dr("nopersonas")
            IdMesa = dr("idmesa")
            IdCajero = dr("idcajero")
            IdCaja = dr("idcaja")
            MotivoCancelacion = dr("motivocan")
            'cambio = dr("cambio")
        End While
        dr.Close()
    End Sub
    Public Sub AumentaComensal(pIdVenta As Integer)
        comm.CommandText = "update tblrestauranteventas set nopersonas=nopersonas+1 where idventa=" + pIdVenta.ToString
        comm.ExecuteNonQuery()
        NoPersonas += 1
    End Sub
    Public Sub QuitaComensal(pIdVenta As Integer)
        comm.CommandText = "update tblrestauranteventas set nopersonas=nopersonas-1 where idventa=" + pIdVenta.ToString
        comm.ExecuteNonQuery()
        NoPersonas -= 1
    End Sub
    Public Sub modificar(ByVal idVenta As Integer, ByVal idCliente As Integer, ByVal descuento As Double, ByVal total As Double, ByVal totalapagar As Double, ByVal estado As Integer, ByVal fecha As String, ByVal idSucursal As Integer, pidCajero As Integer, pIdCaja As Integer, pMotivo As String)
        comm.CommandText = "update tblrestauranteventas set idcliente=" + idCliente.ToString() + ", descuento=" + descuento.ToString() + ",total=" + total.ToString() + ", totalapagar=" + totalapagar.ToString() + ", estado=" + estado.ToString() + ", fecha='" + fecha + "', idsucursal=" + idSucursal.ToString + ",idcajero=" + pidCajero.ToString + ",idcaja=" + pIdCaja.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(Date.Now, "HH:mm:ss") + "',motivocan='" + Replace(pMotivo, "'", "''") + "'"
        comm.CommandText += " where idventa=" + idVenta.ToString()
        comm.ExecuteNonQuery()
    End Sub
    Public Sub eliminar(ByVal idVenta As Integer)
        comm.CommandText = "delete from tblrestauranteventas where idventa=" + idVenta.ToString()
        comm.ExecuteNonQuery()
    End Sub
    Public Function checaFolioRepetido(ByVal serie As String, ByVal folio As Integer) As Boolean
        comm.CommandText = "select serie,folio from tblrestauranteventas where serie='" + serie + "' and folio=" + folio.ToString() + ";"
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        If dr.HasRows() Then
            dr.Close()
            Return True
        Else
            dr.Close()
            Return False
        End If
    End Function

    Public Function DaNuevoFolio(ByVal pSerie As String) As Integer
        comm.CommandText = "select ifnull((select max(folio) from tblsemillasliquidacion where serie='" + Replace(Trim(pSerie), "'", "''") + "'),0)"
        Return comm.ExecuteScalar + 1
    End Function

    Public Function Guardar(ByVal folio As Integer, ByVal idMesero As Integer, ByVal idCliente As Integer, ByVal idSucursal As Integer, pIdMesa As Integer, pidCaja As Integer) As Integer
        comm.CommandText = "insert into tblrestauranteventas(folio,fecha,hora,total,totalapagar,idcliente,idmesero,descuento,estado,idsucursal,nopersonas,serie,idmesa,idcajero,idcaja,fechacancelado,horacancelado,motivocan) "
        comm.CommandText += "values(" + folio.ToString() + ",'" + Date.Now.ToString("yyyy/MM/dd") + "','" + Date.Now.ToString("HH:mm:ss") + "'," + total.ToString() + "," + totalapagar.ToString() + "," + idCliente.ToString() + "," + idMesero.ToString() + "," + descuento.ToString() + ",2," + idSucursal.ToString + ",1,''," + pIdMesa.ToString + "," + idMesero.ToString + "," + pidCaja.ToString + ",'" + Date.Now.ToString("yyyy/MM/dd") + "','" + Date.Now.ToString("HH:mm:ss") + "','');"
        comm.CommandText += "select ifnull(last_insert_id(),0);"
        idVenta = comm.ExecuteScalar()
        Me.folio = folio
        Me.fecha = Date.Now.ToString("yyyy/MM/dd")
        Me.hora = Date.Now.ToString("HH:mm:ss")
        Me.total = total
        Me.totalapagar = totalapagar
        Me.idCliente = idCliente
        Me.idMesero = idMesero
        Me.IdCajero = idMesero
        Me.descuento = descuento
        Me.estado = estado
        Me.idSucursal = idSucursal
        NoPersonas = 1
        Return idVenta
    End Function
    Public Function buscarventaabierta(ByVal idMesa As Integer) As Boolean
        comm.CommandText = "select ifnull((select idventa from tblrestauranteventas where idMesa=" + idMesa.ToString() + " and estado<3 limit 1),0)"
        idVenta = comm.ExecuteScalar
        If idVenta <> 0 Then
            llenaDatos()
            Return True
        End If
        Return False
    End Function
    Public Function buscar(ByVal idVenta As Integer) As Boolean
        comm.CommandText = "select idventa from tblrestauranteventas where idventa=" + idVenta.ToString() + ";"
        Dim res As Integer = comm.ExecuteScalar
        If res > 0 Then
            Me.idVenta = idVenta
            llenaDatos()
            Return True
        End If
        Return False
    End Function
    Public Function DaTotal(pIdVenta As Integer) As Double
        comm.CommandText = "select ifnull((select sum(precio*(1+(iva/100))) from tblrestauranteventasdetalles where idventa=" + pIdVenta.ToString + "),0)"
        Return comm.ExecuteScalar
    End Function
    Public Function buscarFolio(ByVal folio As Integer, ByVal idSucursal As Integer) As Boolean
        comm.CommandText = "select idventa from tblrestauranteventas where folio=" + folio.ToString() + " and idsucursal=" + idSucursal.ToString()
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idVenta = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function obtenFolio() As Integer
        comm.CommandText = "select ifnull(max(folio),0) from tblrestauranteventas;"
        Dim f = comm.ExecuteScalar
        f = f + 1
        Return f
    End Function

    'Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer, ByVal pEsElectronica As Integer, pModoB As String) As Integer
    '    If pModoB = "0" Then
    '        'Comm.CommandText = "select ifnull((select max(folio) from tblventas where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4) and eselectronica=" + pEsElectronica.ToString + " ),0)"
    '        Comm.CommandText = "select ifnull((select folio from tblventas where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4) and eselectronica=" + pEsElectronica.ToString + " order by folio desc limit 1),0)"
    '        Return Comm.ExecuteScalar + 1
    '    Else
    '        'Comm.CommandText = "select ifnull((select max(folio) from tblventas where serie='" + Replace(Trim(pSerie), "'", "''") + "' and eselectronica=" + pEsElectronica.ToString + " ),0)"
    '        Comm.CommandText = "select ifnull((select folio from tblventas where serie='" + Replace(Trim(pSerie), "'", "''") + "' and eselectronica=" + pEsElectronica.ToString + " order by folio desc limit 1),0)"
    '        Return Comm.ExecuteScalar + 1
    '    End If
    'End Function

    Public Function buscarSucursal(ByVal idSucursal) As DataView
        comm.CommandText = "select idventa,folio,fecha,hora,total from tblrestauranteventas where idSucursal=" + idSucursal.ToString()
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "ventas")
        Return ds.Tables("ventas").DefaultView
    End Function

    Public Function buscar(ByVal idSucursal As Integer, ByVal desde As String, ByVal hasta As String) As DataView
        comm.CommandText = "select idventa,folio,fecha,hora,total from tblrestauranteventas where fecha>='" + desde + "' and fecha<='" + hasta + "'"
        If idSucursal > 0 Then
            comm.CommandText += " and idSucursal=" + idSucursal.ToString
        End If
        comm.CommandText += " and estado=" + CInt(Estados.Guardada).ToString
        comm.CommandText += " order by fecha"
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "ventas")
        Return ds.Tables("ventas").DefaultView
    End Function

    Public Function listaDetalles(ByVal idVenta As Integer, ByVal estado As Integer, Optional ByVal pagado As Integer = -1) As List(Of Integer)
        comm.CommandText = "select iddetalle from tblrestauranteventasdetalles where idventa=" + idVenta.ToString
        If pagado > -1 Then
            comm.CommandText += " and pagado=" + pagado.ToString
        End If
        If estado > -1 Then
            comm.CommandText += " and estado=" + estado.ToString
        End If
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Dim lista As New List(Of Integer)
        While dr.Read()
            lista.Add(dr("iddetalle"))
        End While
        dr.Close()
        Return lista
    End Function

    Public Function checaPendientes(ByVal idVenta As Integer) As Boolean
        comm.CommandText = "select count(*) from tblrestauranteventasdetalles where idventa=" + idVenta.ToString + " and estado=" + CInt(estadosPlatillos.sinEnviar).ToString
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Function cuantosPlatillos(ByVal idVenta As Integer, ByVal pagado As Integer) As Integer
        comm.CommandText = "select count(*) from tblrestauranteventasdetalles where idventa=" + idVenta.ToString + " and pagado=" + pagado.ToString
        Return comm.ExecuteScalar
    End Function

    Public Function reporte(ByVal desde As String, ByVal hasta As String, ByVal idMesero As Integer, ByVal idCajero As Integer, ByVal idSucursal As Integer, ByVal idSeccion As Integer, ByVal idProducto As Integer) As DataView
        Return New DataView
    End Function

    Public Function vistaDetalles(ByVal idVenta As Integer, ByVal estado As Integer, ByVal pagado As Integer) As DataView
        comm.CommandText = "select iddetalle,cantidad,descripcion,precio,(precio*cantidad) as total from tblrestauranteventasdetalles where idVenta=" + idVenta.ToString
        If estado > -1 Then
            comm.CommandText += " and estado=" + estado.ToString()
        End If
        If pagado > -1 Then
            comm.CommandText += " and pagado=" + pagado.ToString()
        End If
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "detalles")
        Return ds.Tables("detalles").DefaultView
    End Function
End Class
