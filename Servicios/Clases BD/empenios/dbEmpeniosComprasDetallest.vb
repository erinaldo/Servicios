Public Class dbEmpeniosComprasDetallest
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
        Comm.CommandText = "select * from tblempenioscomprasdetallest where idDetalle=" + ID.ToString
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

        Comm.CommandText = "insert into tblempenioscomprasdetallest(idMovimiento ,clasificacion ,descripcion,evaluo,importe) values(" + pidMovimiento.ToString + "," + pClasificacion.ToString + ",'" + Replace(pDescripcion, "'", "''") + "'," + pEvaluo.ToString + "," + pImporte.ToString + ");"
        Comm.CommandText += "select ifnull((select max(idDetalle) from tblempenioscomprasdetallest),0);"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pidMovimiento As Integer, ByVal pClasificacion As Integer, ByVal pDescripcion As String, ByVal pEvaluo As Double, ByVal pImporte As Double)
        ID = pID
        Comm.CommandText = "update tblempenioscomprasdetallest set clasificacion=" + pClasificacion.ToString + ", descripcion='" + Replace(pDescripcion, "'", "''") + "',evaluo=" + pEvaluo.ToString + ",importe=" + pImporte.ToString + " where idDetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblempenioscomprasdetallest where idDetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaReader(ByVal pIdMovimiento As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblempenioscomprasdetallest.idDetalle,tblempenioscomprasdetallest.idMovimiento ,tblempeniosclasificacion.nombre as clasificacion,tblempenioscomprasdetallest.descripcion ,tblempenioscomprasdetallest.evaluo,tblempenioscomprasdetallest.importe from tblempenioscomprasdetallest  inner join tblempeniosclasificacion on tblempenioscomprasdetallest.clasificacion=tblempeniosclasificacion.idClasificacion  where tblempenioscomprasdetallest.idMovimiento=" + pIdMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempenioscomprasdetallest")
        Return DS.Tables("tblempenioscomprasdetallest").DefaultView
    End Function

    Public Function DaTotal(ByVal pidMovimiento As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double = 1


        TotalVenta = 0

        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select idDetalle from tblempenioscomprasdetallest where idMovimiento=" + pidMovimiento.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("idDetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select importe from tblempenioscomprasdetallest where idDetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            TotalVenta += Precio
            Cont += 1
        End While

        Return TotalVenta
    End Function
    Public Function impresionRecibo(ByVal pIdMovimiento As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select descripcion as concepto, importe as Precio from tblempenioscomprasdetallest  where idMovimiento=" + pIdMovimiento.ToString

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosgastosdetalles")
        'DS.WriteXmlSchema("tblempeniosgastosdetalles.xml")
        Return DS.Tables("tblempeniosgastosdetalles")
    End Function
End Class
