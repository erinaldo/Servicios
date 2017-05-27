Imports MySql.Data.MySqlClient
Public Class dbSemillasDetalleComprobante

#Region "propiedades"
    Public Property id As Integer = -1
    Public Property superficie As Double = 0
    Public Property descripcion As String = ""
    Public Property volumen As Double = 0
    Public Property rendimiento As Double = 0
    Public Property idComprobante As Integer
    Public Property guardado As Boolean
#End Region

#Region "constructores"
    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer)
        Me.id = id
    End Sub

    Public Sub New(ByVal id As Integer, ByVal superficie As Double, ByVal descripcion As String, ByVal volumen As Double, ByVal rendimiento As Double, ByVal idComprobante As Integer, ByVal guardado As Boolean)
        Me.id = id
        Me.superficie = superficie
        Me.descripcion = descripcion
        Me.volumen = volumen
        Me.rendimiento = rendimiento
        Me.idComprobante = idComprobante
        Me.guardado = guardado
    End Sub

    Public Sub New(ByVal superficie As Double, ByVal descripcion As String, ByVal volumen As Double, ByVal rendimiento As Double, ByVal idComprobante As Integer, ByVal guardado As Boolean)
        Me.superficie = superficie
        Me.descripcion = descripcion
        Me.volumen = volumen
        Me.rendimiento = rendimiento
        Me.idComprobante = idComprobante
        Me.guardado = guardado
    End Sub

#End Region
    Private comm As MySqlCommand

    Public Sub New(ByVal conexion As MySqlConnection)
        comm = New MySqlCommand
        comm.Connection = conexion
    End Sub

    Public Sub agregar(ByVal detalle As dbSemillasDetalleComprobante)
        comm.CommandText = "insert into tblsemillascomprobantedetalle(superficie,descripcion,volumen,rendimiento,idcomprobante,guardado) " +
            "values(" + detalle.superficie.ToString() + "," +
            "'" + detalle.descripcion + "'," +
            detalle.volumen.ToString() + "," +
            detalle.rendimiento.ToString() + "," +
            detalle.idComprobante.ToString() + "," + detalle.guardado.ToString() + ");"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("no se puede agregar: " + ex.ToString())
        End Try
    End Sub

    Public Function buscar(ByVal id) As dbSemillasDetalleComprobante
        Dim d As New dbSemillasDetalleComprobante
        Dim dr As MySqlDataReader
        comm.CommandText = "select * from tblsemillascomprobantedetalle where id=" + id.ToString() + ";"
        dr = comm.ExecuteReader
        Try
            If dr.HasRows Then
                While dr.Read()
                    d.id = dr("id")
                    d.superficie = dr("superficie")
                    d.descripcion = dr("descripcion")
                    d.volumen = dr("volumen")
                    d.rendimiento = dr("rendimiento")
                    d.idComprobante = dr("idcomprobante")
                End While
                dr.Close()
                Return d
            Else
                dr.Close()
                Return Nothing
            End If
        Catch ex As Exception
            dr.Close()
            Return Nothing
        End Try
    End Function

    Public Sub actualizar(ByVal detalle As dbSemillasDetalleComprobante)
        comm.CommandText = "update tblsemillascomprobantedetalle set superficie=" + detalle.superficie.ToString() + ", " +
            " descripcion='" + detalle.descripcion + "', " +
            " volumen=" + detalle.volumen.ToString() + ", " +
            " rendimiento=" + detalle.rendimiento.ToString() + ", " +
            " idcomprobante=" + detalle.idComprobante.ToString() + " where id=" + detalle.id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("no se puede modificar: " + ex.ToString())
        End Try
    End Sub

    Public Sub eliminar(ByVal id As Integer)
        comm.CommandText = "delete from tblsemillascomprobantedetalle where id=" + id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("no se puede eliminar: " + ex.ToString())
        End Try
    End Sub

    Public Function vistaDetalles() As DataView
        Dim ds As New DataSet
        comm.CommandText = "select d.id as id, d.numcomprobante as folio, cli.nombre as cliente, d.volumen as volumen, d.superficie as superficie from tblsemillascomprobantedetalle as d " +
            "inner join tblclientes as cli on d.cliente=cli.idcliente;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "detalles")
        Return ds.Tables("detalles").DefaultView
    End Function

    Public Function vistaDetallesComprobante(ByVal idcomprobante As Integer) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select d.id as id, d.superficie as superficie, d.descripcion as descripcion, d.volumen as volumen, d.rendimiento as rendimiento from tblsemillascomprobantedetalle as d where d.idcomprobante=" + idcomprobante.ToString() + ";"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "detalles")
        Return ds.Tables("detalles").DefaultView
    End Function

    Public Function sumaSuperficies(ByVal idComprobante As Integer) As Double
        comm.CommandText = "select ifnull(sum(d.superficie),0) as sumaSuperficie from tblsemillascomprobantedetalle as d where idcomprobante=" + idComprobante.ToString() + ";"
        Dim res As Double = comm.ExecuteScalar()
        Return res
    End Function

    Public Function sumaVolumen(ByVal idComprobante As Integer) As Double
        comm.CommandText = "select ifnull(sum(d.volumen),0) as sumaVolumen from tblsemillascomprobantedetalle as d where idcomprobante=" + idComprobante.ToString() + ";"
        Dim res As Double = comm.ExecuteScalar()
        Return res
    End Function

    Public Sub borraSinGuardar(ByVal idComprobante As Integer)
        comm.CommandText = "delete from tblsemillascomprobantedetalle where guardado=false and idcomprobante=" + idComprobante.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub guardar(ByVal idComprobante As Integer)
        comm.CommandText = "update tblsemillascomprobantedetalle set guardado=true where idcomprobante=" + idComprobante.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub
End Class
