Public Class dbVentasRemisionesServicios
    Public ID As Integer
    Public IdServicio As Integer
    Public Servicio As dbServicios
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdRemision As Integer
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
        IdRemision = 0
        Iva = 0
        Extra = ""
        Descuento = 0
        Descripcion = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventasremisionesservicios where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            IdServicio = DReader("idservicio")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdRemision = DReader("idremision")
            Descripcion = DReader("descripcion")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
        End If
        DReader.Close()
        Servicio = New dbServicios(IdServicio, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdRemision As Integer, ByVal pIdServicio As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double)
        IdServicio = pIdServicio
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdRemision = pIdRemision
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        Comm.CommandText = "insert into tblventasremisionesservicios(idremision,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) values(" + IdRemision.ToString + "," + IdServicio.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull(max(iddetalle),0) from tblventasremisionesservicios"
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
        Comm.CommandText = "update tblventasremisionesservicios set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + " where idventasremisionesservicio=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventasremisionesservicios where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvs.iddetalle,tblservicios.folio,tvs.descripcion,tvs.cantidad,tvs.precio,tblmonedas.abreviatura from tblventasremisionesservicios tvs inner join tblmonedas on tvs.idmoneda=tblmonedas.idmoneda inner join tblservicios on tblservicios.idservicio=tvs.idservicio where tvs.idremision=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasremisionesservicios")
        Return DS.Tables("tblventasremisionesservicios").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvs.iddetalle,tblservicios.folio,tvs.descripcion,tvs.cantidad,tvs.precio,tblmonedas.abreviatura from tblventasremisionesservicios tvs inner join tblmonedas on tvs.idmoneda=tblmonedas.idmoneda inner join tblservicios on tblservicios.idservicio=tvs.idservicio where tvs.idremision=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
End Class
