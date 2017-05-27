Public Class dbEmpeniosDetalles
    Public ID As Integer
    Public idDetalle As Integer
    Public idMovimiento As Integer
    Public descripcion As String
    Public precio As Double
    Public NuevoConcepto As Boolean
    Public TotalVenta As Double
    Public clasificacion As Integer
    Public kilates As Double
    Public peso As Double
    Public evaluo As Double
    Public tipo As Integer
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        idDetalle = 0
        idMovimiento = 0
        descripcion = ""
        kilates = 0
        peso = 0
        precio = 0
        evaluo = 0
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
        Comm.CommandText = "select * from tblempeniosdetalles where idDetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            idMovimiento = DReader("idMovimiento")
            descripcion = DReader("descripcion")
            precio = DReader("precio")
            clasificacion = DReader("clasificacion")
            kilates = DReader("kilates")
            peso = DReader("peso")
            evaluo = DReader("evaluo")
            tipo = DReader("tipo")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pidMovimiento As Integer, ByVal pDescripcion As String, ByVal pPrecio As Double, ByVal pClasificacion As Integer, ByVal pKilates As Double, ByVal pPeso As Double, ByVal pEvaluo As Double, ByVal pTipo As Integer)
        idMovimiento = pidMovimiento
        descripcion = pDescripcion
        precio = pPrecio
        clasificacion = pClasificacion
        kilates = pKilates
        peso = pPeso
        evaluo = pEvaluo
        Comm.CommandText = "insert into tblempeniosdetalles(idMovimiento,descripcion, precio,clasificacion,kilates,peso,evaluo,tipo) values(" + idMovimiento.ToString + ",'" + Replace(descripcion, "'", "''") + "'," + precio.ToString + "," + clasificacion.ToString() + "," + kilates.ToString + "," + peso.ToString + "," + evaluo.ToString + " , " + pTipo.ToString + ");"
        Comm.CommandText += "select ifnull((select max(idDetalle) from tblempeniosdetalles),0);"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pidMovimiento As Integer, ByVal pDescripcion As String, ByVal pPrecio As Double, ByVal pClasificacion As Integer, ByVal pKilates As Double, ByVal pPeso As Double, ByVal pEvaluo As Double, ByVal pTipo As Integer)
        ID = pID
        idMovimiento = pidMovimiento
        descripcion = pDescripcion
        precio = pPrecio
        clasificacion = pClasificacion
        kilates = pKilates
        peso = pPeso
        evaluo = pEvaluo
        Comm.CommandText = "update tblempeniosdetalles set descripcion='" + Replace(descripcion, "'", "''") + "',Precio=" + precio.ToString() + ",clasificacion=" + clasificacion.ToString + ",kilates=" + kilates.ToString + ",peso=" + peso.ToString + ",evaluo=" + evaluo.ToString + ",tipo=" + pTipo.ToString + " where idDetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblempeniosdetalles where idDetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaReader(ByVal pIdMovimiento As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idDetalle,descripcion,if (tipo=0,'Oro','Plata') as Tipo, CASE kilates WHEN 0 THEN '0k' WHEN 1 THEN '8k' WHEN 2 THEN '10k' WHEN 3 THEN '14k' WHEN 4 THEN '18k' WHEN 5 THEN '24k' ELSE 'otro/desconocido' End AS Kilataje ,peso as Peso,clasificacion as Clasificación,evaluo as Evalúo, precio as Precio,kilates from tblempeniosdetalles  where idMovimiento=" + pIdMovimiento.ToString

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosdetalles")
        Return DS.Tables("tblempeniosdetalles").DefaultView
    End Function

    Public Function DaTotal(ByVal pidMovimiento As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double = 1


        TotalVenta = 0

        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select idDetalle from tblempeniosdetalles where idMovimiento=" + pidMovimiento.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("idDetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblempeniosdetalles where idDetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            TotalVenta += Precio
            Cont += 1
        End While

        Return TotalVenta
    End Function
End Class
