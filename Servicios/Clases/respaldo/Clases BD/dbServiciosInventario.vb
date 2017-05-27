Public Class dbServiciosInventario
    Public ID As Integer
    Public IdEvento As Integer
    Public IdInventario As Integer
    Public Idmoneda As Integer
    Public Cantidad As Double
    Public Precio As Double
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdEvento = 0
        IdInventario = 0
        Idmoneda = 0
        Cantidad = 0
        Precio = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblserviciosinventario where idserviciosinventario=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            IdEvento = DReader("idevento")
            IdInventario = DReader("idinventario")
            Idmoneda = DReader("idmoneda")
            Cantidad = DReader("cant")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pIdEvento As Integer, ByVal pIdInventario As Integer, ByVal pCantidad As Integer, ByVal pPrecio As Double, ByVal pIdMoneda As Integer)
        IdEvento = pIdEvento
        IdInventario = pIdInventario
        Idmoneda = pIdMoneda
        Cantidad = pCantidad
        Precio = pPrecio
        Dim CTemp As Double
        Dim PTemp As Double
        Comm.CommandText = "select if(max(cant) is null,-1,cant) from tblserviciosinventario where idevento=" + IdEvento.ToString + " and idinventario=" + IdInventario.ToString
        CTemp = Comm.ExecuteScalar
        If CTemp > -1 Then
            Comm.CommandText = "select max(precio) from tblserviciosinventario where idevento=" + IdEvento.ToString + " and idinventario=" + IdInventario.ToString
            PTemp = Comm.ExecuteScalar
            If PTemp <> 0 Then
                Precio = PTemp / CTemp
            Else
                Precio = 0
            End If
            Cantidad += CTemp
            Precio = Precio * Cantidad
            Comm.CommandText = "update tblserviciosinventario set cant=" + Cantidad.ToString + ",precio=" + Precio.ToString + " where idinventario=" + IdInventario.ToString + " and idevento=" + IdEvento.ToString
        Else
            Comm.CommandText = "insert into tblserviciosinventario(idevento,cant,idinventario,precio,idmoneda) values(" + IdEvento.ToString + "," + Cantidad.ToString + "," + IdInventario.ToString + "," + Precio.ToString + "," + Idmoneda.ToString + ")"
        End If
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Integer, ByVal pPrecio As Double)
        ID = pID
        Precio = pPrecio
        Cantidad = pCantidad
        Comm.CommandText = "update tblserviciosinventario set precio=" + Precio.ToString + ",cant=" + Cantidad.ToString + " where idserviciosinventario=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblserviciosinventario where idserviciosinventario=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdEvento As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblinventario.clave,tblinventario.nombre,tblserviciosinventario.cant,tblserviciosinventario.precio,tblmonedas.nombre from tblserviciosinventario inner join tblinventario on tblserviciosinventario.idinventario=tblinventario.idinventario inner join tblmonedas on tblserviciosinventario.idmoneda=tblmonedas.idmoneda where idevento=" + pIdEvento
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosinventario")
        Return DS.Tables("tblserviciosinventario").DefaultView
    End Function
    Public Function ChecaRepetido(ByVal pIdEvento As Integer, ByVal pIdInventario As Integer) As Boolean

        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(idinventario) from tblserviciosinventario where idevento=" + pIdEvento.ToString + " and idinventario=" + pIdInventario.ToString
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaRepetido = False
        Else
            ChecaRepetido = True
        End If
    End Function
End Class
