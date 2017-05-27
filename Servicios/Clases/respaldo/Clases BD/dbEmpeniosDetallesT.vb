Public Class dbEmpeniosDetallesT
    Public ID As Integer
    Public idMovimiento As Integer
    Public clasificacion As Integer
    Public descripcion As String
    Public evaluo As Double
    Public importe As Double
    Public TotalVenta As Double

    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        idMovimiento = 0
        clasificacion = 0
        descripcion = ""
        evaluo = 0
        importe = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblempeniosdetallest where idDetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            idMovimiento = DReader("idMovimiento")
            clasificacion = DReader("clasificacion")
            descripcion = DReader("descripcion")
            evaluo = DReader("evaluo")
            importe = DReader("importe")
        End If
        DReader.Close()
    End Sub

    Public Sub Guardar(ByVal pidMovimiento As Integer, ByVal pClasificacion As Integer, ByVal pDescripcion As String, ByVal pEvaluo As Double, ByVal pImporte As Double)

        Comm.CommandText = "insert into tblempeniosdetallest(idMovimiento ,clasificacion ,descripcion,evaluo,importe) values(" + pidMovimiento.ToString + "," + pClasificacion.ToString + ",'" + Replace(pDescripcion, "'", "''") + "'," + pEvaluo.ToString + "," + pImporte.ToString + ");"
        Comm.CommandText += "select ifnull((select max(idDetalle) from tblempeniosdetallest),0);"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pidMovimiento As Integer, ByVal pClasificacion As Integer, ByVal pDescripcion As String, ByVal pEvaluo As Double, ByVal pImporte As Double)
        ID = pID
        Comm.CommandText = "update tblempeniosdetallest set clasificacion=" + pClasificacion.ToString + ", descripcion='" + Replace(pDescripcion, "'", "''") + "',evaluo=" + pEvaluo.ToString + ",importe=" + pImporte.ToString + " where idDetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblempeniosdetallest where idDetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaReader(ByVal pIdMovimiento As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblempeniosdetallest.idDetalle,tblempeniosdetallest.idMovimiento ,tblempeniosclasificacion.nombre as clasificacion,tblempeniosdetallest.descripcion ,tblempeniosdetallest.evaluo,tblempeniosdetallest.importe from tblempeniosdetallest  inner join tblempeniosclasificacion on tblempeniosdetallest.clasificacion=tblempeniosclasificacion.idClasificacion  where tblempeniosdetallest.idMovimiento=" + pIdMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosdetallest")
        Return DS.Tables("tblempeniosdetallest").DefaultView
    End Function

    Public Function DaTotal(ByVal pidMovimiento As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double = 1


        TotalVenta = 0

        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select idDetalle from tblempeniosdetallest where idMovimiento=" + pidMovimiento.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("idDetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select importe from tblempeniosdetallest where idDetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            TotalVenta += Precio
            Cont += 1
        End While

        Return TotalVenta
    End Function
End Class
