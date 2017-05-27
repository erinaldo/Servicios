Imports MySql.Data.MySqlClient
Public Class dbAnticipos
#Region "propiedades"
    Public Property id As Integer = -1
    Public Property medio As String = ""
    Public Property importe As Double = 0
    Public Property liquidacion As dbSemillasLiquidacion
    Public Property guardado As Boolean
    'Public Property factura As Integer

#End Region

#Region "constructores"
    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer)
        Me.id = id
    End Sub

    Public Sub New(ByVal id As Integer, ByVal medio As String, ByVal importe As Double, ByVal liquidacion As dbSemillasLiquidacion, ByVal guardado As Boolean)
        Me.id = id
        Me.medio = medio
        Me.importe = importe
        Me.liquidacion = liquidacion
        Me.guardado = guardado

    End Sub

#End Region
    Private comm As MySqlCommand

    Public Sub New(ByVal conexion As MySqlConnection)
        comm = New MySqlCommand
        comm.Connection = conexion
    End Sub

    Public Function obten(ByVal anticipo As dbAnticipos) As dbAnticipos
        comm.CommandText = "select * from tblsemillasanticipos where id=" + anticipo.id.ToString() + ";"
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        Dim a As New dbAnticipos
        If dr.HasRows Then
            While dr.Read()
                a.id = MySqlDReader("id")
                a.medio = dr("medio")
                a.importe = dr("importe")
                a.liquidacion = New dbSemillasLiquidacion(Convert.ToInt32(dr("idLiquidacion")))
                a.guardado = dr("guardado")
                'a.factura = dr("idFactura")
            End While
            dr.Close()
            Return a
        Else
            dr.Close()
            Return Nothing
        End If
    End Function

    Public Sub agregar(ByVal anticipo As dbAnticipos)
        comm.CommandText = "insert into tblsemillasanticipos(medio,importe,idLiquidacion,guardado) values(" +
            "'" + anticipo.medio + "'," + anticipo.importe.ToString() + "," + anticipo.liquidacion.id.ToString() + ",false);"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub eliminar(ByVal id As Integer)
        comm.CommandText = "delete from tblsemillasanticipos where id=" + id.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub
    Public Function listaAnticipos() As List(Of dbAnticipos)
        comm.CommandText = "select * from tblsemillasanticipos;"
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        Dim lista As New List(Of dbAnticipos)
        Dim a As dbAnticipos
        If dr.HasRows Then
            While dr.Read()
                a = New dbAnticipos
                a.id = MySqlDReader("id")
                a.medio = dr("medio")
                a.importe = dr("importe")
                a.liquidacion = New dbSemillasLiquidacion(Convert.ToInt32(dr("idLiquidacion")))
                a.guardado = dr("guardado")
                ' a.factura = dr("idFactura")
                lista.Add(a)
            End While
            dr.Close()
            Return lista
        Else
            dr.Close()
            Return Nothing
        End If
    End Function

    Public Function vistaAnticipos(ByVal liquidacion As dbSemillasLiquidacion) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select a.id as id, a.medio as medio, a.importe as importe from tblsemillasanticipos as a where idLiquidacion=" + liquidacion.id.ToString() + ";"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "anticipos")
        Return ds.Tables("anticipos").DefaultView
    End Function

    Public Function sumaAnticipos(ByVal idLiquidacion As Integer) As Double
        'comm.CommandText = "select ifnull((select sum(importe)from tblsemillasanticipos where idliquidacion=" + idLiquidacion.ToString() + "),0)+ifnull((select sum(importe)from tblsemillasanticiposfacturas where idliquidacion=" + idLiquidacion.ToString() + "),0) as importe"
        comm.CommandText = "select ifnull(sum(d.importe),0) as importe from tblsemillasanticipos as d where d.idliquidacion=" + idLiquidacion.ToString() + ";"
        Dim res As Double = comm.ExecuteScalar
        Return res
    End Function

    Public Function vistaFactursa(ByVal liquidacion As dbSemillasLiquidacion) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select f.folio as folio, f.totalapagar as total from tblventas as f inner join tblsemillasanticipos as a on f.idventa=a.idFactura " +
            "where a.idLiquidacion=" + liquidacion.id.ToString() + ";"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "facturas")
        Return ds.Tables("facturas").DefaultView
    End Function

    Public Sub borraSinGuardar(ByVal idLiquidacion As Integer)
        comm.CommandText = "delete from tblsemillasanticipos where guardado=false and idliquidacion=" + idLiquidacion.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub guardar(ByVal idLiquidacion As Integer)
        comm.CommandText = "update tblsemillasanticipos set guardado=true where idLiquidacion=" + idLiquidacion.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub
End Class
