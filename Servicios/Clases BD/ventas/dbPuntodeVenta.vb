Public Class dbPuntodeVenta
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public TotalVenta As Double
    Public TotalIva As Double
    Public TotalVentaGlobal As Double
    Public TotalIvaGlobal As Double
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub

    Public Function CorteX(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pidVendedor As Integer, ByVal pHora1 As String, ByVal pHora2 As String, ByVal pConHoras As Boolean, ByVal pVendedor As String, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal FormatoFecha As String, ByVal pidSucursal As Integer, ByVal pidCaja As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Dim Cantidad As Double = 0
        'remisiones
        Dim Sucursal As String
        Dim Caja As String
        Comm.CommandText = "select ifnull((select nombre from tblsucursales where idsucursal=" + pidSucursal.ToString + "),'Todas');"
        Sucursal = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select nombre from tblcajas where idcaja=" + pidCaja.ToString + "),'Todas');"
        Caja = Comm.ExecuteScalar

        Comm.CommandText = "delete from tblpuntodeventacortex"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('CORTE X',1,0,'0');"
        'Comm.ExecuteNonQuery()
        If pConHoras Then
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('DEL: " + Format(CDate(pFecha1), FormatoFecha) + " " + pHora1 + "',1,0,'0');"
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('AL: " + Format(CDate(pFecha2), FormatoFecha) + " " + pHora2 + "',1,0,'0');"
        Else
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('DEL: " + Format(CDate(pFecha1), FormatoFecha) + "',1,0,'0');"
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('AL: " + Format(CDate(pFecha2), FormatoFecha) + "',1,0,'0');"
        End If
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Sucursal:',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Sucursal + "',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Caja:',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Caja + "',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Vendedor:',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + pVendedor + "',1,0,'0');"
        Comm.ExecuteNonQuery()


        'Remisiones Cantidad
        Comm.CommandText = "select count(v.idremision) from tblventasremisiones as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
            End If
        End If

        Cantidad = Comm.ExecuteScalar
        If Cantidad > 0 Then
            Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('***TICKETS*** ',1,0,'0');"
            Comm.ExecuteNonQuery()
        End If


        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Cantidad: '," + Cantidad.ToString + ",1,'#,##0')"
        Comm.ExecuteNonQuery()

        'remisiones importe con iva
        Comm.CommandText = "select ifnull((select sum(precio) from tblventasremisiones as v inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where (v.estado=3 or v.estado=4) and tblventasremisionesinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
            End If
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalVenta = Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()

        'remisiones importe sin iva
        Comm.CommandText = "select ifnull((select sum(precio) from tblventasremisiones as v inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where (v.estado=3 or v.estado=4) and tblventasremisionesinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
            End If
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalVenta += Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()

        'remisiones iva importe
        Comm.CommandText = "select ifnull((select sum(v.totalapagar-v.total) from tblventasremisiones as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
            End If
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalIva = Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()


        'remisiones canceladas cantidad
        Comm.CommandText = "select count(v.idremision) from tblventasremisiones as v where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
            End If
        End If
        Cantidad = Comm.ExecuteScalar
        'TotalVenta -= Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Canceladas: '," + Cantidad.ToString + ",1,'#,##0')"
        Comm.ExecuteNonQuery()

        'remisiones canceladas importe
        Comm.CommandText = "select ifnull((select sum(precio) from tblventasremisiones as v inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where v.estado=4 and tblventasremisionesinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
            End If
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalVenta -= Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "select ifnull((select sum(precio) from tblventasremisiones as v inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where v.estado=4 and tblventasremisionesinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pMostrarOc = 1 Then
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
            End If
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalVenta -= Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()

        'remisiones iva canceladas importe
        Comm.CommandText = "select ifnull((select sum(v.totalapagar-v.total) from tblventasremisiones as v where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
            End If
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalIva -= Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()

        'Facturas

        'cantidad
        Comm.CommandText = "select count(v.idventa) from tblventas as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
        End If
        Cantidad = Comm.ExecuteScalar
        If Cantidad > 0 Then
            Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('***FACTURAS*** ',1,0,'0')"
            Comm.ExecuteNonQuery()
        End If
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Cantidad: '," + Cantidad.ToString + ",1,'#,##0')"
        Comm.ExecuteNonQuery()

        'Facturas importe
        Comm.CommandText = "select ifnull((select sum(precio) from tblventas as v inner join tblventasinventario on tblventasinventario.idventa=v.idventa where (v.estado=3 or v.estado=4) and tblventasinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalVenta += Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "select ifnull((select sum(precio) from tblventas as v inner join tblventasinventario on tblventasinventario.idventa=v.idventa where (v.estado=3 or v.estado=4) and tblventasinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalVenta += Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()

        'Facturas iva importe
        Comm.CommandText = "select ifnull((select sum(v.totalapagar-v.total) from tblventas as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalIva += Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()

        'Facturas canceladas cantidad
        Comm.CommandText = "select count(v.idventa) from tblventas as v where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
        End If
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Canceladas: '," + Cantidad.ToString + ",1,'#,##0')"
        Comm.ExecuteNonQuery()

        'Facturas canceladas importe
        'Comm.CommandText = "select ifnull((select sum(v.total) from tblventas as v where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Comm.CommandText = "select ifnull((select sum(precio) from tblventas as v inner join tblventasinventario on tblventasinventario.idventa=v.idventa where v.estado=4 and tblventasinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalVenta -= Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "select ifnull((select sum(precio) from tblventas as v inner join tblventasinventario on tblventasinventario.idventa=v.idventa where v.estado=4 and tblventasinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalVenta -= Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()

        'Facturas iva importe
        Comm.CommandText = "select ifnull((select sum(v.totalapagar-v.total) from tblventas as v where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        TotalIva -= Cantidad
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
        Comm.ExecuteNonQuery()

        'Totales
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Subtotal Venta:  '," + TotalVenta.ToString + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total IVA: '," + TotalIva.ToString + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total Venta: '," + CStr(TotalVenta + TotalIva) + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Movimientos de caja:',1,0,'0');"
        Comm.ExecuteNonQuery()
        'Caja depositos
        Comm.CommandText = "select ifnull((select sum(totalapagar) from tblventasremisiones as v inner join tblformasdepagoremisiones on v.idforma=tblformasdepagoremisiones.idforma where estado=3 and tblformasdepagoremisiones.tipo=1 and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
            End If
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Dep. por ventas: '," + CStr(Cantidad) + ",1,'$#,##0.00');"
        Comm.ExecuteNonQuery()
        'Depósitos
        Comm.CommandText = "select ifnull((select sum(totalapagar) from tblcajasmovimientos as v where estado=3 and tipo=0 and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
            End If
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar

        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Depósitos: '," + CStr(Cantidad) + ",1,'$#,##0.00');"
        Comm.ExecuteNonQuery()
        'Retiros
        Comm.CommandText = "select ifnull((select sum(totalapagar) from tblcajasmovimientos as v where estado=3 and tipo=1 and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        If pidVendedor > 0 Then
            Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        End If
        If pConHoras Then
            Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
            End If
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar

        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Retiros: '," + CStr(Cantidad) + ",1,'$#,##0.00');"
        Comm.ExecuteNonQuery()

        'En caja
        Comm.CommandText = "select ifnull((select sum(deposito) from tblcajas"
        'If pidCaja > 0 Then
        '    Comm.CommandText += " where idcaja=" + pidCaja.ToString
        'End If
        If pidSucursal > 0 Then
            Comm.CommandText += " where idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and idcaja=" + pidCaja.ToString
            End If
        End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Actual en caja: '," + CStr(Cantidad) + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Fecha de Impresión: ',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Format(Date.Now, "yyyy/MM/dd HH:mm") + "',1,0,'0');"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "select * from tblpuntodeventacortex"
        Return Comm.ExecuteReader
    End Function



    Public Function CorteXVendedor(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pidvendedor As Integer, ByVal pHora1 As String, ByVal pHora2 As String, ByVal pConHoras As Boolean, ByVal pVendedor As String, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal FormatoFecha As String, ByVal pidSucursal As Integer, ByVal pidCaja As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Dim Cantidad As Double = 0
        Dim Vendedores As New Collection
        Dim idsVendedores As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        TotalIvaGlobal = 0
        TotalVentaGlobal = 0
        Dim Sucursal As String
        Dim Caja As String
        Comm.CommandText = "select ifnull((select nombre from tblsucursales where idsucursal=" + pidSucursal.ToString + "),'Todas');"
        Sucursal = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select nombre from tblcajas where idcaja=" + pidCaja.ToString + "),'Todas');"
        Caja = Comm.ExecuteScalar

        Comm.CommandText = "select idvendedor,nombre from tblvendedores"
        If pidvendedor > 0 Then
            Comm.CommandText += " where idvendedor=" + pidvendedor.ToString
        End If
        DR = Comm.ExecuteReader
        While DR.Read
            Vendedores.Add(DR("nombre"), CStr(DR("idvendedor")))
            idsVendedores.Add(DR("idvendedor"))
        End While
        DR.Close()
        Comm.Transaction = Comm.Connection.BeginTransaction()
        Comm.CommandText = "delete from tblpuntodeventacortex;"
        Comm.ExecuteNonQuery()
        'encabezado
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('CORTE X POR VENDEDOR',1,0,'0');"
        'Comm.ExecuteNonQuery()
        If pConHoras Then
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('DEL: " + Format(CDate(pFecha1), FormatoFecha) + " " + pHora1 + "',1,0,'0');"
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('AL: " + Format(CDate(pFecha2), FormatoFecha) + " " + pHora2 + "',1,0,'0');"
        Else
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('DEL: " + Format(CDate(pFecha1), FormatoFecha) + "',1,0,'0');"
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('AL: " + Format(CDate(pFecha2), FormatoFecha) + "',1,0,'0');"
        End If
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Sucursal:',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Sucursal + "',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Caja:',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Caja + "',1,0,'0');"
        Comm.ExecuteNonQuery()
        Dim idVendedor As Integer

        For Each idVendedor In idsVendedores
            'revisar si hay movimientos
            Comm.CommandText = "select count(v.idremision) from tblventasremisiones as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
            If idVendedor > 0 Then
                Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
            End If
            If pConHoras Then
                Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
            End If
            If pMostrarOc = 1 Then
                Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
            End If
            If pidSucursal > 0 Then
                Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                If pidCaja > 0 Then
                    Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                End If
            End If
            Cantidad = Comm.ExecuteScalar
            Comm.CommandText = "select count(v.idventa) from tblventas as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
            If idVendedor > 0 Then
                Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
            End If
            If pConHoras Then
                Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
            End If
            If pidSucursal > 0 Then
                Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
            End If
            Cantidad += Comm.ExecuteScalar
            If Cantidad <> 0 Then
                'remisiones
                'Remisiones Cantidad
                TotalIva = 0
                TotalVenta = 0
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Vendedor: ******************',1,0,'0');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Vendedores(idVendedor.ToString) + "',1,0,'0');"
                Comm.ExecuteNonQuery()
                Comm.CommandText = "select count(v.idremision) from tblventasremisiones as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Cantidad = Comm.ExecuteScalar
                If Cantidad > 0 Then
                    Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('***TICKETS*** ',1,0,'0');"
                    Comm.ExecuteNonQuery()
                End If
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Cantidad: '," + Cantidad.ToString + ",1,'#,##0')"
                Comm.ExecuteNonQuery()

                'remisiones importe con iva
                Comm.CommandText = "select ifnull((select sum(precio) from tblventasremisiones as v inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where (v.estado=3 or v.estado=4) and tblventasremisionesinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta = Cantidad
                TotalVentaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'remisiones importe sin iva
                Comm.CommandText = "select ifnull((select sum(precio) from tblventasremisiones as v inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where (v.estado=3 or v.estado=4) and tblventasremisionesinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta += Cantidad
                TotalVentaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'remisiones iva importe
                Comm.CommandText = "select ifnull((select sum(v.totalapagar-v.total) from tblventasremisiones as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalIva = Cantidad
                TotalIvaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()


                'remisiones canceladas cantidad
                Comm.CommandText = "select count(v.idremision) from tblventasremisiones as v where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Cantidad = Comm.ExecuteScalar
                'TotalVenta -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Canceladas: '," + Cantidad.ToString + ",1,'#,##0')"
                Comm.ExecuteNonQuery()

                'remisiones canceladas importe
                Comm.CommandText = "select ifnull((select sum(precio) from tblventasremisiones as v inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where v.estado=4 and tblventasremisionesinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta -= Cantidad
                TotalVentaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                Comm.CommandText = "select ifnull((select sum(precio) from tblventasremisiones as v inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where v.estado=4 and tblventasremisionesinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta -= Cantidad
                TotalVentaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'remisiones iva canceladas importe
                Comm.CommandText = "select ifnull((select sum(v.totalapagar-v.total) from tblventasremisiones as v where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalIva -= Cantidad
                TotalIvaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'Facturas

                'cantidad
                Comm.CommandText = "select count(v.idventa) from tblventas as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                End If
                Cantidad = Comm.ExecuteScalar
                If Cantidad > 0 Then
                    Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('***FACTURAS*** ',1,0,'0')"
                    Comm.ExecuteNonQuery()
                End If
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Cantidad: '," + Cantidad.ToString + ",1,'#,##0')"
                Comm.ExecuteNonQuery()

                'Facturas importe
                Comm.CommandText = "select ifnull((select sum(precio) from tblventas as v inner join tblventasinventario on tblventasinventario.idventa=v.idventa where (v.estado=3 or v.estado=4) and tblventasinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta += Cantidad
                TotalVentaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                Comm.CommandText = "select ifnull((select sum(precio) from tblventas as v inner join tblventasinventario on tblventasinventario.idventa=v.idventa where (v.estado=3 or v.estado=4) and tblventasinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta += Cantidad
                TotalVentaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'Facturas iva importe
                Comm.CommandText = "select ifnull((select sum(v.totalapagar-v.total) from tblventas as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalIva += Cantidad
                TotalIvaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'Facturas canceladas cantidad
                Comm.CommandText = "select count(v.idventa) from tblventas as v where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                End If
                Cantidad = Comm.ExecuteScalar
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Canceladas: '," + Cantidad.ToString + ",1,'#,##0')"
                Comm.ExecuteNonQuery()

                'Facturas canceladas importe
                'Comm.CommandText = "select ifnull((select sum(v.total) from tblventas as v where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                Comm.CommandText = "select ifnull((select sum(precio) from tblventas as v inner join tblventasinventario on tblventasinventario.idventa=v.idventa where v.estado=4 and tblventasinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta -= Cantidad
                TotalVentaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                Comm.CommandText = "select ifnull((select sum(precio) from tblventas as v inner join tblventasinventario on tblventasinventario.idventa=v.idventa where v.estado=4 and tblventasinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta -= Cantidad
                TotalVentaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'Facturas iva importe
                Comm.CommandText = "select ifnull((select sum(v.totalapagar-v.total) from tblventas as v where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idVendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + idVendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pidSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pidSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalIva -= Cantidad
                TotalIvaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'Totales
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Subtotal Venta:  '," + TotalVenta.ToString + ",1,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total IVA: '," + TotalIva.ToString + ",1,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total Venta: '," + CStr(TotalVenta + TotalIva) + ",1,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'0');"
                Comm.ExecuteNonQuery()
            End If

        Next

        'Totales globales
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Subtotal Venta:  '," + TotalVentaGlobal.ToString + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total IVA: '," + TotalIvaGlobal.ToString + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total Venta: '," + CStr(TotalVentaGlobal + TotalIvaGlobal) + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'0');"
        'Comm.Ex0ecuteNonQuery()

        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Fecha de Impresión: ',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Format(Date.Now, FormatoFecha + " HH:mm") + "',1,0,'0');"
        Comm.ExecuteNonQuery()
        Comm.Transaction.Commit()
        Comm.CommandText = "select * from tblpuntodeventacortex"
        Return Comm.ExecuteReader
    End Function
    Public Function CorteXTipodePago(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pidvendedor As Integer, ByVal pHora1 As String, ByVal pHora2 As String, ByVal pConHoras As Boolean, ByVal pVendedor As String, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal FormatoFecha As String, ByVal pIdSucursal As Integer, ByVal pidCaja As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Dim Cantidad As Double = 0
        Dim Formasr As New Collection
        Dim idsFormasr As New Collection
        Dim Formasf As New Collection
        Dim idsFormasf As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        TotalIvaGlobal = 0
        TotalVentaGlobal = 0
        Dim Sucursal As String
        Dim Caja As String
        Try

        
        Comm.CommandText = "select ifnull((select nombre from tblsucursales where idsucursal=" + pIdSucursal.ToString + "),'Todas');"
        Sucursal = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select nombre from tblcajas where idcaja=" + pidCaja.ToString + "),'Todas');"
        Caja = Comm.ExecuteScalar

        Comm.CommandText = "select idforma,nombre from tblformasdepagoremisiones"
        DR = Comm.ExecuteReader
        While DR.Read
            Formasr.Add(DR("nombre"), CStr(DR("idforma")))
            idsFormasr.Add(DR("idforma"))
        End While
        DR.Close()
        Comm.CommandText = "select idforma,nombre from tblformasdepago"
        DR = Comm.ExecuteReader
        While DR.Read
            Formasf.Add(DR("nombre"), CStr(DR("idforma")))
            idsFormasf.Add(DR("idforma"))
        End While
        DR.Close()
        Comm.Transaction = Comm.Connection.BeginTransaction()
        Comm.CommandText = "delete from tblpuntodeventacortex;"
        Comm.ExecuteNonQuery()
        'encabezado
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('CORTE X POR MÉTODO DE PAGO',1,0,'0');"
        'Comm.ExecuteNonQuery()
        If pConHoras Then
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('DEL: " + Format(CDate(pFecha1), FormatoFecha) + " " + pHora1 + "',1,0,'0');"
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('AL: " + Format(CDate(pFecha2), FormatoFecha) + " " + pHora2 + "',1,0,'0');"
        Else
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('DEL: " + Format(CDate(pFecha1), FormatoFecha) + "',1,0,'0');"
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('AL: " + Format(CDate(pFecha2), FormatoFecha) + "',1,0,'0');"
        End If
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Sucursal:',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Sucursal + "',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Caja:',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Caja + "',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Vendedor:',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + pVendedor + "',1,0,'0');"
        Comm.ExecuteNonQuery()
        Dim idFormar As Integer
        Dim idFormaf As Integer
        Dim SolounaVez As Boolean = False
        'Remisiones **********************************************************
        '********************************************************************
        For Each idFormar In idsFormasr
            'revisar si hay movimientos
            Comm.CommandText = "select count(v.idremision) from tblventasremisiones v inner join tblremisionesformasdepago tvf on v.idremision=tvf.idremision where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
            If idFormar > 0 Then
                Comm.CommandText += " and tvf.idforma=" + idFormar.ToString
            End If
            If pidvendedor > 0 Then
                Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
            End If
            If pConHoras Then
                Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
            End If
            If pMostrarOc = 1 Then
                'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                If pidCaja > 0 Then
                    Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                End If
            End If
            Cantidad = Comm.ExecuteScalar

            If Cantidad <> 0 Then
                'remisiones
                'Remisiones Cantidad
                If SolounaVez = False Then
                    Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('***TICKETS*** ',1,0,'0');"
                    Comm.ExecuteNonQuery()
                    SolounaVez = True
                End If
                TotalIva = 0
                TotalVenta = 0
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('******************',1,0,'0');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Formasr(idFormar.ToString) + "',1,0,'0');"
                Comm.ExecuteNonQuery()
                'Comm.CommandText = "select count(v.idremision) from tblventasremisiones as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
                'If idFormar > 0 Then
                '    Comm.CommandText += " and v.idforma=" + idFormar.ToString
                'End If
                'If pidvendedor > 0 Then
                '    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                'End If
                'If pConHoras Then
                '    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                'End If
                'If pMostrarOc = 1 Then
                '    Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                'End If

                'Cantidad = Comm.ExecuteScalar
                'If Cantidad > 0 Then

                'End If
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Cantidad: '," + Cantidad.ToString + ",1,'#,##0')"
                Comm.ExecuteNonQuery()

                'remisiones importe con iva
                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,tblventasremisionesinventario.precio*(tvf.cantidad/v.totalapagar),0),2)) from tblventasremisiones as v inner join tblremisionesformasdepago tvf on v.idremision=tvf.idremision inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where (v.estado=3 or v.estado=4) and tblventasremisionesinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
                If idFormar > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormar.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta = Cantidad
                TotalVentaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'remisiones importe sin iva
                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,tblventasremisionesinventario.precio*(tvf.cantidad/v.totalapagar),0),2)) from tblventasremisiones as v inner join tblremisionesformasdepago tvf on v.idremision=tvf.idremision inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where (v.estado=3 or v.estado=4) and tblventasremisionesinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
                If idFormar > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormar.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta += Cantidad
                TotalVentaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'remisiones iva importe
                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,(v.totalapagar-v.total)*(tvf.cantidad/v.totalapagar),0),2)) from tblventasremisiones as v inner join tblremisionesformasdepago tvf on v.idremision=tvf.idremision where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
                If idFormar > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormar.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalIva = Cantidad
                TotalIvaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()


                'remisiones canceladas cantidad
                Comm.CommandText = "select count(v.idremision) from tblventasremisiones as v inner join tblremisionesformasdepago tvf on v.idremision=tvf.idremision where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
                If idFormar > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormar.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Cantidad = Comm.ExecuteScalar
                'TotalVenta -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Canceladas: '," + Cantidad.ToString + ",1,'#,##0')"
                Comm.ExecuteNonQuery()

                'remisiones canceladas importe
                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,tblventasremisionesinventario.precio*(tvf.cantidad/v.totalapagar),0),2)) from tblventasremisiones as v inner join tblremisionesformasdepago tvf on v.idremision=tvf.idremision inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where v.estado=4 and tblventasremisionesinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
                If idFormar > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormar.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta -= Cantidad
                TotalVentaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,tblventasremisionesinventario.precio*(tvf.cantidad/v.totalapagar),0),2)) from tblventasremisiones as v inner join tblremisionesformasdepago tvf on v.idremision=tvf.idremision inner join tblventasremisionesinventario on tblventasremisionesinventario.idremision=v.idremision where v.estado=4 and tblventasremisionesinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
                If idFormar > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormar.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta -= Cantidad
                TotalVentaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'remisiones iva canceladas importe
                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,(v.totalapagar-v.total)*(tvf.cantidad/v.totalapagar),0),2)) from tblventasremisiones as v inner join tblremisionesformasdepago tvf on v.idremision=tvf.idremision where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and usado=0"
                If idFormar > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormar.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalIva -= Cantidad
                TotalIvaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()
                'Totales
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Subtotal Venta:  '," + TotalVenta.ToString + ",1,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total IVA: '," + TotalIva.ToString + ",1,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total Venta: '," + CStr(TotalVenta + TotalIva) + ",1,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'0');"
                Comm.ExecuteNonQuery()
            End If

        Next
        SolounaVez = False
        'Facturas**************************************************************************
        '***************************************************************************

        For Each idFormaf In idsFormasf
            'revisar si hay movimientos

            Comm.CommandText = "select count(v.idventa) from tblventas as v inner join tblventasformasdepago tvf on v.idventa=tvf.idventa where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
            If idFormar > 0 Then
                Comm.CommandText += " and tvf.idforma=" + idFormaf.ToString
            End If
            If pidvendedor > 0 Then
                Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
            End If
            If pConHoras Then
                Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
            End If
            Cantidad = Comm.ExecuteScalar
            If Cantidad <> 0 Then
                If SolounaVez = False Then
                    Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('***FACTURAS*** ',1,0,'0')"
                    Comm.ExecuteNonQuery()
                    SolounaVez = True
                End If
                'Facturas
                'cantidad
                'Comm.CommandText = "select count(v.idventa) from tblventas as v where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                'If idFormaf > 0 Then
                '    Comm.CommandText += " and v.idforma=" + idFormaf.ToString
                'End If
                'If pidvendedor > 0 Then
                '    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                'End If
                'If pConHoras Then
                '    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                'End If
                'Cantidad = Comm.ExecuteScalar
                'If Cantidad > 0 Then

                'End If
                TotalVenta = 0
                TotalIva = 0
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('******************',1,0,'0');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Formasf(idFormaf.ToString) + "',1,0,'0');"
                Comm.ExecuteNonQuery()
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Cantidad: '," + Cantidad.ToString + ",1,'#,##0')"
                Comm.ExecuteNonQuery()

                'Facturas importe
                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,tblventasinventario.precio*(tvf.cantidad/v.totalapagar),0),2)) from tblventas as v inner join tblventasformasdepago tvf on v.idventa=tvf.idventa inner join tblventasinventario on tblventasinventario.idventa=v.idventa where (v.estado=3 or v.estado=4) and tblventasinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idFormaf > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormaf.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta = Cantidad
                TotalVentaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,tblventasinventario.precio*(tvf.cantidad/v.totalapagar),0),2)) from tblventas as v inner join tblventasformasdepago tvf on v.idventa=tvf.idventa inner join tblventasinventario on tblventasinventario.idventa=v.idventa where (v.estado=3 or v.estado=4) and tblventasinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idFormaf > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormaf.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta += Cantidad
                TotalVentaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'Facturas iva importe
                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,(v.totalapagar-v.total)*(tvf.cantidad/v.totalapagar),0),2)) from tblventas as v inner join tblventasformasdepago tvf on v.idventa=tvf.idventa where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idFormaf > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormaf.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalIva += Cantidad
                TotalIvaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'Facturas canceladas cantidad
                Comm.CommandText = "select count(v.idventa) from tblventas as v inner join tblventasformasdepago tvf on v.idventa=tvf.idventa where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idFormaf > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormaf.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                End If
                Cantidad = Comm.ExecuteScalar
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Canceladas: '," + Cantidad.ToString + ",1,'#,##0')"
                Comm.ExecuteNonQuery()

                'Facturas canceladas importe
                'Comm.CommandText = "select ifnull((select sum(v.total) from tblventas as v where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,tblventasinventario.precio*(tvf.cantidad/v.totalapagar),0),2)) from tblventas as v inner join tblventasformasdepago tvf on v.idventa=tvf.idventa inner join tblventasinventario on tblventasinventario.idventa=v.idventa where v.estado=4 and tblventasinventario.iva<>0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idFormaf > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormaf.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta -= Cantidad
                TotalVentaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. gravable: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,tblventasinventario.precio*(tvf.cantidad/v.totalapagar),0),2)) from tblventas as v inner join tblventasformasdepago tvf on v.idventa=tvf.idventa inner join tblventasinventario on tblventasinventario.idventa=v.idventa where v.estado=4 and tblventasinventario.iva=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idFormaf > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormaf.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta -= Cantidad
                TotalVentaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. Imp. excento: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'Facturas iva importe
                Comm.CommandText = "select ifnull((select sum(round(if(v.totalapagar<>0,(v.totalapagar-v.total)*(tvf.cantidad/v.totalapagar),0),2)) from tblventas as v inner join tblventasformasdepago tvf on v.idventa=tvf.idventa where v.estado=4 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idFormaf > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormaf.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalIva -= Cantidad
                TotalIvaGlobal -= Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('C. IVA: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                'Totales
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Subtotal Venta:  '," + TotalVenta.ToString + ",1,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total IVA: '," + TotalIva.ToString + ",1,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total Venta: '," + CStr(TotalVenta + TotalIva) + ",1,'$#,##0.00');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'0');"
                Comm.ExecuteNonQuery()
            End If

        Next

        'Totales globales*************************************************************
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Subtotal Venta:  '," + TotalVentaGlobal.ToString + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total IVA: '," + TotalIvaGlobal.ToString + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total Venta: '," + CStr(TotalVentaGlobal + TotalIvaGlobal) + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'0');"
        'Comm.ExecuteNonQuery()

        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Fecha de Impresión: ',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Format(Date.Now, FormatoFecha + " HH:mm") + "',1,0,'0');"
        Comm.ExecuteNonQuery()
            Comm.Transaction.Commit()
        Catch ex As Exception
            Comm.Transaction.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, GlobalNombreApp)
        End Try
        Comm.CommandText = "select * from tblpuntodeventacortex"
        Return Comm.ExecuteReader
    End Function

    Public Function CorteXTipodePagoConcentrado(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pidvendedor As Integer, ByVal pHora1 As String, ByVal pHora2 As String, ByVal pConHoras As Boolean, ByVal pVendedor As String, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal FormatoFecha As String, ByVal pIdSucursal As Integer, ByVal pidCaja As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Dim Cantidad As Double = 0
        Dim Formasr As New Collection
        Dim idsFormasr As New Collection
        Dim Formasf As New Collection
        Dim idsFormasf As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        TotalIvaGlobal = 0
        TotalVentaGlobal = 0
        Dim Sucursal As String
        Dim Caja As String
        Try

        Comm.CommandText = "select ifnull((select nombre from tblsucursales where idsucursal=" + pIdSucursal.ToString + "),'Todas');"
        Sucursal = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select nombre from tblcajas where idcaja=" + pidCaja.ToString + "),'Todas');"
        Caja = Comm.ExecuteScalar

        Comm.CommandText = "select idforma,nombre from tblformasdepagoremisiones"
        DR = Comm.ExecuteReader
        While DR.Read
            Formasr.Add(DR("nombre"), CStr(DR("idforma")))
            idsFormasr.Add(DR("idforma"))
        End While
        DR.Close()
        Comm.CommandText = "select idforma,nombre from tblformasdepago"
        DR = Comm.ExecuteReader
        While DR.Read
            Formasf.Add(DR("nombre"), CStr(DR("idforma")))
            idsFormasf.Add(DR("idforma"))
        End While
        DR.Close()
        Comm.Transaction = Comm.Connection.BeginTransaction()
        Comm.CommandText = "delete from tblpuntodeventacortex;"
        Comm.ExecuteNonQuery()
        'encabezado
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('CORTE X POR TIPO DE PAGO',1,0,'0');"
        'Comm.ExecuteNonQuery()
        If pConHoras Then
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('DEL: " + Format(CDate(pFecha1), FormatoFecha) + " " + pHora1 + "',1,0,'0');"
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('AL: " + Format(CDate(pFecha2), FormatoFecha) + " " + pHora2 + "',1,0,'0');"
        Else
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('DEL: " + Format(CDate(pFecha1), FormatoFecha) + "',1,0,'0');"
            Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('AL: " + Format(CDate(pFecha2), FormatoFecha) + "',1,0,'0');"
        End If
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Sucursal:',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Sucursal + "',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Caja:',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Caja + "',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Vendedor:',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + pVendedor + "',1,0,'0');"
        Comm.ExecuteNonQuery()
        Dim idFormar As Integer
        Dim idFormaf As Integer
        Dim SolounaVez As Boolean = False
        'Remisiones **********************************************************
        '********************************************************************
        For Each idFormar In idsFormasr
            'revisar si hay movimientos
                Comm.CommandText = "select count(v.idremision) from tblventasremisiones as v inner join tblremisionesformasdepago tvf on v.idremision=tvf.idremision where (v.estado=3 or v.estado=4) and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
            If idFormar > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormar.ToString
            End If
            If pidvendedor > 0 Then
                Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
            End If
            If pConHoras Then
                Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
            End If
            If pMostrarOc = 1 Then
                'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                If pidCaja > 0 Then
                    Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                End If
            End If
            Cantidad = Comm.ExecuteScalar

            If Cantidad <> 0 Then
                'remisiones
                'Remisiones Cantidad
                If SolounaVez = False Then
                    Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('***TICKETS*** ',1,0,'0');"
                    Comm.ExecuteNonQuery()
                    SolounaVez = True
                End If
                TotalIva = 0
                TotalVenta = 0
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('******************',1,0,'0');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Formasr(idFormar.ToString) + "',1,0,'0');"
                Comm.ExecuteNonQuery()
                
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Cantidad: '," + Cantidad.ToString + ",1,'#,##0')"
                Comm.ExecuteNonQuery()

                'remisiones importe con iva
                    Comm.CommandText = "select ifnull((select sum(tvf.cantidad) from tblventasremisiones as v inner join tblremisionesformasdepago tvf on v.idremision=tvf.idremision where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "' and v.usado=0"
                If idFormar > 0 Then
                        Comm.CommandText += " and tvf.idforma=" + idFormar.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pMostrarOc = 1 Then
                    'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
                    Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                    If pidCaja > 0 Then
                        Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
                    End If
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta = Cantidad
                TotalVentaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()
                
            End If

        Next
        SolounaVez = False
        'Facturas**************************************************************************
        '***************************************************************************

        For Each idFormaf In idsFormasf
            'revisar si hay movimientos

                Comm.CommandText = "select count(v.idventa) from tblventas as v inner join tblventasformasdepago tvf on v.idventa=tvf.idventa where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
            If idFormar > 0 Then
                    Comm.CommandText += " and tvf.idforma=" + idFormaf.ToString
            End If
            If pidvendedor > 0 Then
                Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
            End If
            If pConHoras Then
                Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
            End If
            Cantidad = Comm.ExecuteScalar
            If Cantidad <> 0 Then
                If SolounaVez = False Then
                    Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('***FACTURAS*** ',1,0,'0')"
                    Comm.ExecuteNonQuery()
                    SolounaVez = True
                End If
                'Facturas
                'cantidad
                
                TotalVenta = 0
                TotalIva = 0
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('******************',1,0,'0');"
                Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Formasf(idFormaf.ToString) + "',1,0,'0');"
                Comm.ExecuteNonQuery()
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Cantidad: '," + Cantidad.ToString + ",1,'#,##0')"
                Comm.ExecuteNonQuery()

                'Facturas importe
                    Comm.CommandText = "select ifnull((select sum(tvf.cantidad) from tblventas as v inner join tblventasformasdepago tvf on v.idventa=tvf.idventa where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
                If idFormaf > 0 Then
                        Comm.CommandText += " and tvf.idforma=" + idFormaf.ToString
                End If
                If pidvendedor > 0 Then
                    Comm.CommandText += " and v.idvendedor=" + pidvendedor.ToString
                End If
                If pConHoras Then
                    Comm.CommandText += " and v.hora>='" + pHora1 + "' and v.hora<='" + pHora2 + "'"
                End If
                If pIdSucursal > 0 Then
                    Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
                End If
                Comm.CommandText += "),0)"
                Cantidad = Comm.ExecuteScalar
                TotalVenta = Cantidad
                TotalVentaGlobal += Cantidad
                Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Importe: '," + Cantidad.ToString + ",1,'$#,##0.00')"
                Comm.ExecuteNonQuery()

                
            End If

        Next

        'Totales globales*************************************************************
        Comm.CommandText = "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'$#,##0.00');"
        'Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Subtotal Venta:  '," + TotalVentaGlobal.ToString + ",1,'$#,##0.00');"
        'Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total IVA: '," + TotalIvaGlobal.ToString + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Total Venta: '," + CStr(TotalVentaGlobal + TotalIvaGlobal) + ",1,'$#,##0.00');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'0');"
        'Comm.ExecuteNonQuery()

        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('Fecha de Impresión: ',1,0,'0');"
        Comm.CommandText += vbCrLf + "insert into tblpuntodeventacortex(concepto,cantidad,mostrarcantidad,formato) values('" + Format(Date.Now, FormatoFecha + " HH:mm") + "',1,0,'0');"
        Comm.ExecuteNonQuery()
            Comm.Transaction.Commit()
        Catch ex As Exception
            Comm.Transaction.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, GlobalNombreApp)
        End Try
        Comm.CommandText = "select * from tblpuntodeventacortex"
        Return Comm.ExecuteReader
    End Function

End Class
