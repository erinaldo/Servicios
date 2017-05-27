Public Class dbEmpeniosDetallesV
    Public ID As Integer
    Public idMovimiento As Integer
    Public clasificacion As Integer
    Public marca As String
    Public modelo As String
    Public color As String
    Public noSerie As String
    Public placas As String
    Public evaluo As Double
    Public importe As Double
    Public TotalVenta As Double

    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        idMovimiento = 0
        clasificacion = 0
        marca = ""
        modelo = ""
        color = ""
        noSerie = ""
        placas = ""
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
        Comm.CommandText = "select * from tblempeniosdetallesv where idDetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            idMovimiento = DReader("idMovimiento")
            clasificacion = DReader("clasificacion")
            marca = DReader("marca")
            modelo = DReader("modelo")
            color = DReader("color")
            noSerie = DReader("noSerie")
            placas = DReader("placas")
            evaluo = DReader("evaluo")
            importe = DReader("importe")
        End If
        DReader.Close()
    End Sub

    Public Sub Guardar(ByVal pidMovimiento As Integer, ByVal pClasificacion As Integer, ByVal pMarca As String, ByVal pModelo As String, ByVal pColor As String, ByVal pNoSeria As String, ByVal pPlacas As String, ByVal pEvaluo As Double, ByVal pImporte As Double)

        Comm.CommandText = "insert into tblempeniosdetallesv(idMovimiento ,clasificacion,marca ,modelo,color,noSerie,placas,evaluo,importe) values(" + pidMovimiento.ToString + "," + pClasificacion.ToString + ",'" + Replace(pMarca, "'", "''") + "','" + Replace(pModelo, "'", "''") + "','" + Replace(pColor, "'", "''") + "','" + Replace(pNoSeria, "'", "''") + "','" + Replace(pPlacas, "'", "''") + "'," + pEvaluo.ToString + "," + pImporte.ToString + ");"
        Comm.CommandText += "select ifnull((select max(idDetalle) from tblempeniosdetallesv),0);"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pidMovimiento As Integer, ByVal pClasificacion As Integer, ByVal pMarca As String, ByVal pModelo As String, ByVal pColor As String, ByVal pNoSeria As String, ByVal pPlacas As String, ByVal pEvaluo As Double, ByVal pImporte As Double)
        ID = pID
        Comm.CommandText = "update tblempeniosdetallesv set clasificacion=" + pClasificacion.ToString + ",marca='" + Replace(pMarca, "'", "''") + "' ,modelo='" + Replace(pModelo, "'", "''") + "',color='" + Replace(pColor, "'", "''") + "',noSerie='" + Replace(pNoSeria, "'", "''") + "',placas='" + Replace(pPlacas, "'", "''") + "',evaluo=" + pEvaluo.ToString + ",importe=" + pImporte.ToString + " where idDetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblempeniosdetallesv where idDetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaReader(ByVal pIdMovimiento As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblempeniosdetallesv.idDetalle,tblempeniosdetallesv.idMovimiento ,tblempeniosclasificacion.nombre as clasificacion,tblempeniosdetallesv.marca ,tblempeniosdetallesv.modelo,tblempeniosdetallesv.color,tblempeniosdetallesv.noSerie,tblempeniosdetallesv.placas,tblempeniosdetallesv.evaluo,tblempeniosdetallesv.importe from tblempeniosdetallesv  inner join tblempeniosclasificacion on tblempeniosdetallesv.clasificacion=tblempeniosclasificacion.idClasificacion  where tblempeniosdetallesv.idMovimiento=" + pIdMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosdetallesv")
        Return DS.Tables("tblempeniosdetallesv").DefaultView
    End Function

    Public Function DaTotal(ByVal pidMovimiento As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double = 1


        TotalVenta = 0

        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select idDetalle from tblempeniosdetallesv where idMovimiento=" + pidMovimiento.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("idDetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select importe from tblempeniosdetallesv where idDetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            TotalVenta += Precio
            Cont += 1
        End While

        Return TotalVenta
    End Function
End Class
