Public Class dbVentasServicios
    Public ID As Integer
    Public IdServicio As Integer
    Public Servicio As dbServicios
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdVenta As Integer
    Public Descripcion As String
    Public Iva As Double
    Public Extra As String
    Public Descuento As Double
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdServicio = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdVenta = 0
        Descripcion = ""
        Iva = 0
        Extra = ""
        Descuento = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventasservicios where idventasservicio=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            IdServicio = DReader("idservicio")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdVenta = DReader("idventa")
            Descripcion = DReader("descripcion")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
        End If
        DReader.Close()
        Servicio = New dbServicios(IdServicio, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdVenta As Integer, ByVal pIdServicio As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double)
        'Dim CTemp As Double
        'Dim PTemp As Double
        IdServicio = pIdServicio
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdVenta = pIdVenta
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        'Comm.CommandText = "select if(max(cantidad) is null,-1,cantidad) from tblventasinventario where idventa=" + IdVenta.ToString + " and idinventario=" + IdServicio.ToString
        'CTemp = Comm.ExecuteScalar
        'If CTemp > -1 Then
        '    Comm.CommandText = "select max(precio) from tblventasinventario where idventa=" + IdVenta.ToString + " and idinventario=" + IdServicio.ToString
        '    PTemp = Comm.ExecuteScalar
        '    If PTemp <> 0 Then
        '        Precio = PTemp / CTemp
        '    Else
        '        Precio = 0
        '    End If
        '    Cantidad += CTemp
        '    Precio = Precio * Cantidad
        '    Comm.CommandText = "update tblventasinventario set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + " where idinventario=" + IdServicio.ToString + " and idventa=" + IdVenta.ToString
        'Else
        Comm.CommandText = "insert into tblventasservicios(idventa,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) values(" + IdVenta.ToString + "," + IdServicio.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + ")"
        'End If
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select if(max(idventasservicio) is null,0,max(idventasservicio)) from tblventasservicios"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        Comm.CommandText = "update tblventasservicios set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + " where idventasservicio=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventasservicios where idventasservicio=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvs.idventasservicio,tblservicios.folio,tvs.descripcion,tvs.cantidad,tvs.precio,tblmonedas.abreviatura from tblventasservicios tvs inner join tblmonedas on tvs.idmoneda=tblmonedas.idmoneda inner join tblservicios on tblservicios.idservicio=tvs.idservicio where tvs.idventa=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasservicios")
        Return DS.Tables("tblventasservicios").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvs.idventasservicio,tblservicios.folio,tvs.descripcion,tvs.cantidad,tvs.precio,tblmonedas.abreviatura,tvs.iva,tvs.idmoneda from tblventasservicios tvs inner join tblmonedas on tvs.idmoneda=tblmonedas.idmoneda inner join tblservicios on tblservicios.idservicio=tvs.idservicio where tvs.idventa=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
End Class
