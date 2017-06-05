Imports MySql.Data.MySqlClient
Public Class dbCartasSalida
    Private comm As MySqlCommand
    Public Sub New(conn As MySqlConnection)
        Me.comm = New MySqlCommand("", conn)
    End Sub
    Public Sub Guardar(carta As CartaSalida)
        comm.Transaction = comm.Connection.BeginTransaction()
        Try
            comm.CommandText = "select count(*) from tblcartassalida where id=" + carta.Id.ToString()
            If comm.ExecuteScalar = 0 Then
                comm.CommandText = "insert into tblcartassalida (id, fecha, unidad, marca, modelo, color, placas, transportista, chofer, lote, observaciones) values (@id, @fecha, @unidad, @marca, @modelo, @color, @placas, @transportista, @chofer, @lote, @observaciones);"
            Else
                comm.CommandText = "update tblcartassalida set fecha=@fecha, unidad=@unidad, marca=@marca, modelo=@modelo, color=@color, placas=@placas, transportista=@transportista, chofer=@chofer, lote=@lote, observaciones=@observaciones where id=@id;"
            End If
            comm.Parameters.Add(New MySqlParameter("@id", carta.Id))
            comm.Parameters.Add(New MySqlParameter("@fecha", carta.Fecha))
            comm.Parameters.Add(New MySqlParameter("@unidad", carta.Unidad))
            comm.Parameters.Add(New MySqlParameter("@marca", carta.Marca))
            comm.Parameters.Add(New MySqlParameter("@modelo", carta.Modelo))
            comm.Parameters.Add(New MySqlParameter("@color", carta.Color))
            comm.Parameters.Add(New MySqlParameter("@placas", carta.Placas))
            comm.Parameters.Add(New MySqlParameter("@transportista", carta.Transportista))
            comm.Parameters.Add(New MySqlParameter("@chofer", carta.Chofer))
            comm.Parameters.Add(New MySqlParameter("@lote", carta.Lote))
            comm.Parameters.Add(New MySqlParameter("@observaciones", carta.Observaciones))
            comm.ExecuteNonQuery()
            comm.Parameters.Clear()

            comm.CommandText = "delete from tblcartassalidadetalles where idcarta=" + carta.Id.ToString()
            comm.ExecuteNonQuery()
            For Each d As CartaSalidaDetalle In carta.Detalles
                comm.CommandText = "insert into tblcartassalidadetalles (idcarta, cantidad, descripcion, kilosunidad) values (@idcarta, @cantidad, @descripcion, @kilosunidad);"
                comm.Parameters.Add(New MySqlParameter("@idcarta", carta.Id))
                comm.Parameters.Add(New MySqlParameter("@cantidad", d.Cantidad))
                comm.Parameters.Add(New MySqlParameter("@descripcion", d.Descripcion))
                comm.Parameters.Add(New MySqlParameter("@kilosunidad", d.KilosUnidad))
                comm.ExecuteNonQuery()
                comm.Parameters.Clear()

            Next
            comm.CommandText = "delete from tblcartassalidasellos where idcarta=" + carta.Id.ToString()
            comm.ExecuteNonQuery()
            For Each d As CartaSalidaSello In carta.Sellos
                comm.CommandText = "insert into tblcartassalidasellos (idcarta, numero) values (@idcarta, @numero);"
                comm.Parameters.Add(New MySqlParameter("@idcarta", carta.Id))
                comm.Parameters.Add(New MySqlParameter("@numero", d.Numero))
                comm.ExecuteNonQuery()
                comm.Parameters.Clear()
            Next
            comm.Transaction.Commit()
        Catch ex As MySqlException
            comm.Transaction.Rollback()
            If ex.ErrorCode = -2147467259 Then
                Throw New Exception("No es posible agregar descripciones o números de sello repetidos.")
            End If
        End Try
    End Sub
    Public Function Buscar(idmovimiento As Integer) As CartaSalida
        comm.CommandText = "select * from tblcartassalida where id=" + idmovimiento.ToString() + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Try
            Dim carta As New CartaSalida(idmovimiento, Now, "", "", "", "", "", "", "", "", "")
            If dr.Read Then
                carta = New CartaSalida(dr("id"), dr("fecha"), dr("unidad"), dr("marca"), dr("modelo"), dr("color"), dr("placas"), dr("transportista"), dr("chofer"), dr("lote"), dr("observaciones"))
            End If
            dr.Close()
            comm.CommandText = "select * from tblcartassalidadetalles where idcarta=" + idmovimiento.ToString() + ";"
            dr = comm.ExecuteReader
            While dr.Read
                carta.Detalles.Add(New CartaSalidaDetalle(dr("idcarta"), dr("cantidad"), dr("descripcion"), dr("kilosunidad")))
            End While
            dr.Close()
            If carta.Detalles.Count = 0 Then carta.Detalles.Add(New CartaSalidaDetalle(idmovimiento, 0, "", 0))
            comm.CommandText = "select * from tblcartassalidasellos where idcarta=" + idmovimiento.ToString() + ";"
            dr = comm.ExecuteReader
            While dr.Read
                carta.Sellos.Add(New CartaSalidaSello(dr("idcarta"), dr("numero")))
            End While
            If carta.Sellos.Count = 0 Then carta.Sellos.Add(New CartaSalidaSello(idmovimiento, ""))
            Return carta
        Finally
            If Not dr.IsClosed Then dr.Close()
        End Try
    End Function
    'Public Function Consultar(desde As DateTime, hasta As DateTime) As DataTable
    '    comm.CommandText = "select * from tblcartassalida where date(fecha)>=@desde and date(fecha)<=@hasta order by fecha;"
    '    comm.Parameters.Add(New MySqlParameter("@desde", desde))
    '    comm.Parameters.Add(New MySqlParameter("@hasta", hasta))
    '    Dim ds As New DataSet
    '    Dim da As New MySqlDataAdapter(comm)
    '    da.Fill(ds, "tabla")
    '    comm.Parameters.Clear()
    '    Return ds.Tables("tabla")
    'End Function
End Class
