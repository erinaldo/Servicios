Public Class dbGastosDetalles
    Public ID As Integer
    Public idDetalle As Integer
    Public idMovimiento As Integer
    Public descripcion As String
    Public precio As Double
    Public NuevoConcepto As Boolean
    Public TotalVenta As Double
    Public IdClasificacion As Integer
    Public IdClasificacion2 As Integer
    Public IdClasificacion3 As Integer
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        idDetalle = 0
        idMovimiento = 0
        descripcion = ""
        precio = 0
        NuevoConcepto = False
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblgastosdetalles where idDetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            idMovimiento = DReader("idMovimiento")
            descripcion = DReader("descripcion")
            precio = DReader("precio")
            IdClasificacion = DReader("clasificacion")
            IdClasificacion2 = DReader("idclasificacion2")
            IdClasificacion3 = DReader("idclasificacion3")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pidMovimiento As Integer, ByVal pDescripcion As String, ByVal pPrecio As Double, ByVal pClasificacion As Integer, ByVal pIdClasificacion2 As Integer, pIdClasificacion3 As Integer)
        idMovimiento = pidMovimiento
        descripcion = pDescripcion
        precio = pPrecio
        IdClasificacion = pClasificacion
        IdClasificacion2 = pIdClasificacion2
        IdClasificacion3 = pIdClasificacion3
        Comm.CommandText = "insert into tblgastosdetalles(idMovimiento,descripcion, precio,clasificacion,esSalario,idclasificacion2,idclasificacion3) values(" + idMovimiento.ToString + ",'" + Replace(descripcion, "'", "''") + "'," + precio.ToString + "," + IdClasificacion.ToString() + ",0," + IdClasificacion2.ToString + "," + IdClasificacion3.ToString + ");"
        Comm.CommandText += "select ifnull((select max(idDetalle) from tblgastosdetalles),0);"
        ID = Comm.ExecuteScalar


    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pidMovimiento As Integer, ByVal pDescripcion As String, ByVal pPrecio As Double, ByVal pClasificacion As Integer, ByVal pIdClasificacion2 As Integer, pIdClasificacion3 As Integer)
        ID = pID
        idMovimiento = pidMovimiento
        descripcion = pDescripcion
        precio = pPrecio
        IdClasificacion = pClasificacion
        IdClasificacion2 = pIdClasificacion2
        IdClasificacion3 = pIdClasificacion3
        Comm.CommandText = "update tblgastosdetalles set descripcion='" + Replace(descripcion, "'", "''") + "',Precio=" + precio.ToString() + ",clasificacion=" + IdClasificacion.ToString + ",idclasificacion2=" + IdClasificacion2.ToString + ",idclasificacion3=" + IdClasificacion3.ToString + " where idDetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblgastosdetalles where idDetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaReader(ByVal pIdMovimiento As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idDetalle,descripcion,precio,clasificacion from tblgastosdetalles  where idMovimiento=" + pIdMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblgastosdetalles")
        Return DS.Tables("tblgastosdetalles").DefaultView
    End Function
    Public Function DaTotal(ByVal pidMovimiento As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double = 1


        TotalVenta = 0

        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select idDetalle from tblgastosdetalles where idMovimiento=" + pidMovimiento.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("idDetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblgastosdetalles where idDetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            TotalVenta += Precio
            Cont += 1
        End While

        Return TotalVenta
    End Function
End Class
