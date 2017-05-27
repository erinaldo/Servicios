Public Class dbProductosVariantes
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdProducto As Integer
    Public Precio As Double
    Public Nombre As String
    Public Moneda As dbMonedas
    Enum TipoMovimiento As Integer
        Alta = 0
        Baja = 1
        Cambio = 2
        CambioBaja = 3
    End Enum
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdProducto = -1
        Precio = 0
        Nombre = ""
        Comm.Connection = Conexion
        Moneda = New dbMonedas(Conexion)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim idMoneda As Integer
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblproductosvariantes where idvariante=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdProducto = DReader("idproducto")
            Precio = DReader("precio")
            Nombre = DReader("Nombre")
            idMoneda = DReader("idmoneda")
        End If
        DReader.Close()
        Moneda = New dbMonedas(idMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdProducto As Integer, ByVal pNombre As String, ByVal pPrecio As Double, ByVal pIdMoneda As Integer)
        IdProducto = pIdProducto
        Precio = pPrecio
        Nombre = pNombre
        Comm.CommandText = "insert into tblproductosvariantes(idproducto,nombre,precio,idmoneda) values(" + IdProducto.ToString + ",'" + Replace(Nombre, "'", "''") + "'," + Precio.ToString + "," + pIdMoneda.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pPrecio As Double, ByVal pIdMoneda As Integer)
        ID = pID
        Precio = pPrecio
        Nombre = pNombre
        Comm.CommandText = "update tblproductosvariantes set nombre='" + Replace(Nombre, "'", "''") + "',precio=" + Precio.ToString + ",idmoneda=" + pIdMoneda.ToString + " where idvariante=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblproductosvariantes where idvariante=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdProducto As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblproductosvariantes.idvariante,tblproductosvariantes.nombre,tblproductosvariantes.precio,tblmonedas.abreviatura from tblproductosvariantes inner join tblmonedas on tblproductosvariantes.idmoneda=tblmonedas.idmoneda where idproducto=" + pIdProducto.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproductosvariantes")
        Return DS.Tables("tblproductosvariantes").DefaultView
    End Function

    Public Sub ModificaInventario(ByVal pIdVariante As Integer, ByVal pCantidad As Double, ByVal pIdAlmacen As Integer)

        Dim P1 As New MySql.Data.MySqlClient.MySqlParameter
        Dim P2 As New MySql.Data.MySqlClient.MySqlParameter
        Dim P3 As New MySql.Data.MySqlClient.MySqlParameter
        Comm.CommandType = CommandType.StoredProcedure
        Comm.CommandText = "spmodificainventario"
        P1.DbType = DbType.Int32
        P1.ParameterName = "pidvariante"
        P2.DbType = DbType.Double
        P2.ParameterName = "pcantidad"
        P3.DbType = DbType.Int32
        P3.ParameterName = "pidalmacen"
        P1.Value = pIdVariante
        P2.Value = pCantidad
        P3.Value = pIdAlmacen
        Comm.Parameters.Clear()
        Comm.Parameters.Add(P1)
        Comm.Parameters.Add(P2)
        Comm.Parameters.Add(P3)
        Comm.ExecuteNonQuery()

        Comm.CommandType = CommandType.Text
    End Sub
    Public Sub MovimientoDeInventario(ByVal pIdVariante As Integer, ByVal pCantidad As Double, ByVal pCantidadAnt As Double, ByVal TipoMov As TipoMovimiento, ByVal pIdAlmacen As Integer)
        Dim cCantidad As Double
        ID = pIdVariante
        LlenaDatos()
        'VerificaTablaAlmacen(pIdinventario, pIdAlmacen)
        Select Case TipoMov
            Case TipoMovimiento.Alta
                Comm.CommandText = "select cantidad from tblalmacenesp where idproducto=" + IdProducto.ToString + " and idalmacen=" + pIdAlmacen.ToString
                cCantidad = Comm.ExecuteScalar
                cCantidad = cCantidad + pCantidad
                ModificaInventario(pIdVariante, cCantidad, pIdAlmacen)
            Case TipoMovimiento.Baja
                Comm.CommandText = "select cantidad from tblalmacenesp where idproducto=" + IdProducto.ToString + " and idalmacen=" + pIdAlmacen.ToString
                cCantidad = Comm.ExecuteScalar
                cCantidad = cCantidad - pCantidad
                ModificaInventario(pIdVariante, cCantidad, pIdAlmacen)
            Case TipoMovimiento.Cambio
                Comm.CommandText = "select cantidad from tblalmacenesp where idproducto=" + IdProducto.ToString + " and idalmacen=" + pIdAlmacen.ToString
                cCantidad = Comm.ExecuteScalar
                cCantidad = cCantidad - pCantidadAnt + pCantidad
                ModificaInventario(pIdVariante, cCantidad, pIdAlmacen)
            Case TipoMovimiento.CambioBaja
                Comm.CommandText = "select cantidad from tblalmacenesp where idproducto=" + IdProducto.ToString + " and idalmacen=" + pIdAlmacen.ToString
                cCantidad = Comm.ExecuteScalar
                cCantidad = cCantidad + pCantidadAnt - pCantidad
                ModificaInventario(pIdVariante, cCantidad, pIdAlmacen)
        End Select
    End Sub

End Class
