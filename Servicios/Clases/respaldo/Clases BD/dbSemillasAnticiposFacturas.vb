Imports MySql.Data.MySqlClient
Public Class dbSemillasAnticiposFacturas
    Public Property id As Integer
    Public Property idLiquidacion As Integer
    Public Property idVenta As Integer
    Public Property importe As Double
    Public Property guardado As Boolean

    Private comm As MySqlCommand

    Public Sub New()

    End Sub
    Public Sub New(ByVal id As Integer, ByVal conexion As MySqlConnection)
        Me.id = id
        comm = New MySqlCommand
        comm.Connection = conexion
        llenaDatos()
    End Sub

    Public Sub New(ByVal idLiquidacion As Integer, ByVal idVenta As Integer)
        Me.idLiquidacion = idLiquidacion
        Me.idVenta = idVenta
    End Sub

    Public Sub New(ByVal id As Integer, ByVal idLiquidacion As Integer, ByVal idVenta As Integer, ByVal importe As Double, ByVal guardado As Boolean)
        Me.id = id
        Me.idLiquidacion = idLiquidacion
        Me.idVenta = idVenta
        Me.importe = importe
        Me.guardado = guardado
    End Sub

    Public Sub New(ByVal conexion As MySqlConnection)
        comm = New MySqlCommand
        comm.Connection = conexion
    End Sub

    Public Sub agregar(ByVal factura As dbSemillasAnticiposFacturas)
        comm.CommandText = "insert into tblsemillasanticiposfacturas(idliquidacion,idfactura,importe,guardado) values" +
            "(" + factura.idLiquidacion.ToString() + "," + factura.idVenta.ToString() + "," + factura.importe.ToString() + "," + factura.guardado.ToString() + ");"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub eliminar(ByVal id As Integer)
        comm.CommandText = "delete from tblsemillasanticiposfacturas where id=" + id.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Private Sub llenaDatos()
        Dim dr As MySqlDataReader
        comm.CommandText = "select * tblsemillasanticiposfacturas where id=" + id.ToString() + ";"
        dr = comm.ExecuteReader
        While dr.Read()
            idLiquidacion = dr("idliquidacion")
            idVenta = dr("idventa")
            importe = dr("importe")
            guardado = dr("guardado")
        End While
        dr.Close()
    End Sub

    Public Function buscar(ByVal id As Integer) As Boolean
        comm.CommandText = "select ifnull((select * from tblsemillasanticiposfacturas where id=" + id.ToString() + "),0);"
        Dim i As Integer = comm.ExecuteScalar
        If i Then
            Me.id = id
            llenaDatos()
            Return True
        Else
            Return False
        End If
    End Function

    Public Function vistaFacturas(ByVal idLiquidacion As Integer) As DataView
        comm.CommandText = "select a.id, v.folio, a.importe from tblsemillasanticiposfacturas as a inner join " +
            "tblventas as v on a.idFactura=v.idVenta where a.idLiquidacion=" + idLiquidacion.ToString() + ";"
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "facturas")
        Return ds.Tables("facturas").DefaultView
    End Function

    Public Function sumaFacturas(ByVal idLiquidacion As Integer) As Double
        comm.CommandText = "select ifnull(sum(f.importe),0) as total from tblsemillasanticiposfacturas as f where idLiquidacion=" + idLiquidacion.ToString() + ";"
        Dim res As Double = comm.ExecuteScalar
        Return res
    End Function

    Public Sub borraSinGuardar(ByVal idLiquidacion As Integer)
        comm.CommandText = "delete from tblsemillasanticiposfacturas where guardado=false and idliquidacion=" + idLiquidacion.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub guardar(ByVal idLiquidacion As Integer)
        comm.CommandText = "update tblsemillasanticiposfacturas set guardado=true where idLiquidacion=" + idLiquidacion.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub
End Class
