Public Class dbComprasCotizacionDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    Public IdProveedor As Integer
    Public Proveedor As dbproveedores
    Public Proveedor2 As dbproveedores
    Public Proveedor3 As dbproveedores
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdCompraCotizacion As Integer
    Public Seleccionado As Byte
    Public Seleccionado2 As Byte
    Public Seleccionado3 As Byte
    Public Precio2 As Double
    Public Precio3 As Double
    Public IdProveedor2 As Integer
    Public IdProveedor3 As Integer
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        Precio = 0
        IdCompraCotizacion = 0
        IdProveedor = 0
        Seleccionado = 0
        Precio2 = 0
        Precio3 = 0
        IdProveedor2 = 0
        IdProveedor3 = 0
        Seleccionado2 = 0
        Seleccionado3 = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcomprascotizaciondetalles where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdProveedor = DReader("idproveedor")
            Seleccionado = DReader("seleccionado")
            IdCompraCotizacion = DReader("idcompracotizacion")
            Precio2 = DReader("precio2")
            Precio3 = DReader("precio3")
            IdProveedor2 = DReader("idproveedor2")
            IdProveedor3 = DReader("idproveedor3")
            Seleccionado2 = DReader("seleccionado2")
            Seleccionado3 = DReader("seleccionado3")
        End If
        DReader.Close()
        Inventario = New dbInventario(Idinventario, Comm.Connection)
        Proveedor = New dbproveedores(IdProveedor, Comm.Connection)
        Proveedor2 = New dbproveedores(IdProveedor2, Comm.Connection)
        Proveedor3 = New dbproveedores(IdProveedor3, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdCompraCotizacion As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdProveedor As Integer, ByVal pPrecio2 As Double, ByVal pIdProveedor2 As Integer, ByVal pPrecio3 As Integer, ByVal pIdProveedor3 As Integer)
        Dim CTemp As Double
        Dim PTemp As Double
        Dim P2Temp As Double
        Dim P3Temp As Double
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdCompraCotizacion = pIdCompraCotizacion
        IdProveedor = pIdProveedor
        IdProveedor2 = pIdProveedor2
        IdProveedor3 = pIdProveedor3
        Precio2 = pPrecio2
        Precio3 = pPrecio3
        Comm.CommandText = "select if(max(cantidad) is null,-1,cantidad) from tblcomprascotizaciondetalles where idcompracotizacion=" + IdCompraCotizacion.ToString + " and idinventario=" + Idinventario.ToString '+ " and idproveedor=" + IdProveedor.ToString
        CTemp = Comm.ExecuteScalar
        If CTemp > -1 Then
            Comm.CommandText = "select max(precio) from tblcomprascotizaciondetalles where idcompracotizacion=" + IdCompraCotizacion.ToString + " and idinventario=" + Idinventario.ToString '+ " and idproveedor=" + IdProveedor.ToString
            PTemp = Comm.ExecuteScalar
            Comm.CommandText = "select max(precio2) from tblcomprascotizaciondetalles where idcompracotizacion=" + IdCompraCotizacion.ToString + " and idinventario=" + Idinventario.ToString '+ " and idproveedor=" + IdProveedor.ToString
            P2Temp = Comm.ExecuteScalar
            Comm.CommandText = "select max(precio3) from tblcomprascotizaciondetalles where idcompracotizacion=" + IdCompraCotizacion.ToString + " and idinventario=" + Idinventario.ToString '+ " and idproveedor=" + IdProveedor.ToString
            P2Temp = Comm.ExecuteScalar
            If PTemp <> 0 Then
                Precio = PTemp / CTemp
            Else
                Precio = 0
            End If
            If P2Temp <> 0 Then
                Precio2 = P2Temp / CTemp
            Else
                Precio2 = 0
            End If
            If P3Temp <> 0 Then
                Precio3 = P3Temp / CTemp
            Else
                Precio3 = 0
            End If
            Cantidad += CTemp
            Precio = Precio * Cantidad
            Comm.CommandText = "update tblcomprascotizaciondetalles set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + ",precio2=" + Precio2.ToString + ",precio3=" + Precio3.ToString + " where idinventario=" + Idinventario.ToString + " and idcompracotizacion=" + IdCompraCotizacion.ToString + " and idproveedor=" + IdProveedor.ToString
        Else
            Comm.CommandText = "insert into tblcomprascotizaciondetalles(idinventario,cantidad,precio,idcompracotizacion,idproveedor,seleccionado,precio2,idproveedor2,precio3,idproveedor3,seleccionado2,seleccionado3) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdCompraCotizacion.ToString + "," + IdProveedor.ToString + ",0," + Precio2.ToString + "," + IdProveedor2.ToString + "," + Precio3.ToString + "," + IdProveedor3.ToString + ",0,0)"
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select if(max(iddetalle) is null,0,max(iddetalle)) from tblcomprascotizaciondetalles"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pPrecio2 As Double, ByVal pPrecio3 As Integer)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        Precio2 = pPrecio2
        Precio3 = pPrecio3
        Comm.CommandText = "update tblcomprascotizaciondetalles set precio=" + Precio.ToString + ",cantidad=" + Cantidad.ToString + ",precio2=" + Precio2.ToString + ",precio3=" + Precio3.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Seleccionar(ByVal pID As Integer, ByVal pSeleccionado As Byte, ByVal pSeleccionado2 As Byte, ByVal pSeleccionado3 As Byte)
        ID = pID
        Seleccionado = pSeleccionado
        Seleccionado2 = pSeleccionado2
        Seleccionado3 = pSeleccionado3
        Comm.CommandText = "update tblcomprascotizaciondetalles set seleccionado=" + Seleccionado.ToString + ",seleccionado2=" + Seleccionado2.ToString + ",seleccionado3=" + Seleccionado3.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcomprascotizaciondetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdcompraCotizacion As Integer) As DataView
        Dim DS As New DataSet
        'Comm.CommandText = "select tblinventario.clave,tblinventario.nombre,seleccionado,(select clave from tblproveedores where idproveedor=tblcomprascotizaciones.idproveedor) as proveedor1,seleccionado2,(select clave from tblproveedores where idproveedor=tblcomprascotizaciones.idproveedor2) as proveedor2,seleccionado,(select clave from tblproveedores where idproveedor=tblcomprascotizaciones.idproveedor3) as proveedor3 from tblcomprascotizaciondetalles.idcompracotizacion=" + pIdcompraCotizacion.ToString
        Comm.CommandText = "select tblcomprascotizaciondetalles.iddetalle,tblinventario.clave,tblinventario.nombre as nombrea,tblcomprascotizaciondetalles.cantidad,seleccionado,(select clave from tblproveedores where idproveedor=tblcomprascotizaciondetalles.idproveedor) as proveedor1,precio,seleccionado2,(select clave from tblproveedores where idproveedor=tblcomprascotizaciondetalles.idproveedor2) as proveedor2,precio2,seleccionado3,(select clave from tblproveedores where idproveedor=tblcomprascotizaciondetalles.idproveedor3) as proveedor3,precio3 from tblcomprascotizaciondetalles inner join tblinventario on tblcomprascotizaciondetalles.idinventario=tblinventario.idinventario where tblcomprascotizaciondetalles.idcompracotizacion=" + pIdcompraCotizacion.ToString + " order by tblinventario.clave"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprascotizaciondetalles")
        Return DS.Tables("tblcomprascotizaciondetalles").DefaultView
    End Function
    Public Sub DaProveedores(ByVal pIdCompraCotizacion As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select idproveedor,idproveedor2,idproveedor3 from tblcomprascotizaciondetalles where idcompracotizacion=" + pIdCompraCotizacion.ToString + " limit 1"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdProveedor = DReader("idproveedor")
            IdProveedor2 = DReader("idproveedor2")
            IdProveedor3 = DReader("idproveedor3")
        End If
        DReader.Close()
        Proveedor = New dbproveedores(IdProveedor, Comm.Connection)
        Proveedor2 = New dbproveedores(IdProveedor2, Comm.Connection)
        Proveedor3 = New dbproveedores(IdProveedor3, Comm.Connection)
    End Sub
End Class
