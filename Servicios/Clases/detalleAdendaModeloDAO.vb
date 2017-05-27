Imports MySql.Data.MySqlClient
Public Class detalleAdendaModeloDAO
    Dim comm As New MySqlCommand
    Dim insert As String = "insert into tbladendamodelodetalle(idAdenda,posicionPedido,codigoEAN,numProveedor,idioma," +
        "cantidadProductosFacturada,unidadMedida,precioBruto,precioNeto,descripcion,numReferenciaAdicional,tipoArancel," +
        "numIdImpuesto,porcentajeImpuesto,montoImpuesto,identificacionImpuesto,precioBrutoArticulos,precioNetoArticulos) "

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub agregar(ByRef detalle As detalleAdendaModelo)
        comm.CommandText = insert + "values(" + detalle.idAdenda.ToString() + "," +
       detalle.posicionPedido.ToString() + "," +
       "'" + detalle.codigoEAN + "','" +
       detalle.numProveedor + "','" +
       detalle.idioma + "'," +
       detalle.cantidadProductosFacturada.ToString() + "," +
       "'" + detalle.unidadMedida + "'," +
       detalle.precioBruto.ToString() + "," +
       detalle.precioNeto.ToString() +
       ",'" + detalle.descripcion + "','" +
       detalle.numReferenciaAdicional + "','" +
       detalle.tipoArancel + "','" +
       detalle.numIdImpuesto + "'," +
       detalle.porcentajeImpuesto.ToString() + "," +
       detalle.montoImpuesto.ToString() + "," +
       "'" + detalle.identificacionImpuesto + "'," +
       detalle.precioBrutoArticulos.ToString() + "," +
       detalle.precioNetoArticulos.ToString() + ");"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub actualizar(ByRef detalle As detalleAdendaModelo)
        comm.CommandText = "update tbladendamodelodetalle set idAdenda=" + detalle.idAdenda.ToString() + ", " +
        "posicionPedido=" + detalle.posicionPedido.ToString() + ", codigoEAN='" + detalle.codigoEAN + "', numProveedor='" + detalle.numProveedor + "', " +
        "idioma='" + detalle.idioma + "', cantidadProductosFacturada=" + detalle.cantidadProductosFacturada.ToString() + ", " +
        "unidadMedida='" + detalle.unidadMedida + "', precioBruto=" + detalle.precioBruto.ToString() + ", precioNeto=" + detalle.precioNeto.ToString() +
        ", descripcion='" + detalle.descripcion + "', numReferenciaAdicional='" + detalle.numReferenciaAdicional + "', tipoArancel='" + detalle.tipoArancel + "', " +
        "numIdImpuesto='" + detalle.numIdImpuesto + "', porcentajeImpuesto=" + detalle.porcentajeImpuesto.ToString() + ", montoImpuesto=" + detalle.montoImpuesto.ToString() + ", " +
        "identificacionImpuesto='" + detalle.identificacionImpuesto + "', precioBrutoArticulos=" + detalle.precioBrutoArticulos.ToString() + ", " +
        "precioNetoArticulos=" + detalle.precioNetoArticulos.ToString() + " where id=" + detalle.id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub eliminar(ByVal idDetalle As Integer)
        comm.CommandText = "delete from tbladendamodelodetalle where id=" + idDetalle.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function vistaDetalles(ByVal idAdenda As Integer) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select * from tbladendamodelodetalle where idAdenda=" + idAdenda.ToString() + ";"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "detallesAdenda")
        Return ds.Tables("detallesAdenda").DefaultView
    End Function

    Public Function vistaDetallesGrid(ByVal idAdenda As Integer) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select id,descripcion,cantidadProductosFacturada,codigoEAN,precioBruto from tbladendamodelodetalle where idAdenda=" + idAdenda.ToString() + ";"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "detallesAdenda")
        Return ds.Tables("detallesAdenda").DefaultView
    End Function

    Public Function listaDetalles(ByVal idAdenda As Integer) As List(Of detalleAdendaModelo)
        comm.CommandText = "select * from tbladendamodelodetalle where idAdenda=" + idAdenda.ToString() + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Dim lista As New List(Of detalleAdendaModelo)
        Dim d As detalleAdendaModelo
        If dr.HasRows Then
            While dr.Read()
                d = New detalleAdendaModelo()
                d.id = dr("id")
                d.idAdenda = dr.GetInt32("idAdenda")
                d.posicionPedido = dr.GetInt32("posicionPedido")
                d.codigoEAN = dr("codigoEAN")
                d.numProveedor = dr("numProveedor")
                d.idioma = dr("idioma")
                d.cantidadProductosFacturada = dr.GetDouble("cantidadProductosFacturada")
                d.unidadMedida = dr("unidadMedida")
                d.precioBruto = dr.GetDouble("precioBruto")
                d.precioNeto = dr.GetDouble("precioNeto")
                d.descripcion = dr("descripcion")
                d.numReferenciaAdicional = dr("numReferenciaAdicional")
                d.tipoArancel = dr("tipoArancel")
                d.numIdImpuesto = dr("numIdImpuesto")
                d.porcentajeImpuesto = dr.GetDouble("porcentajeImpuesto")
                d.montoImpuesto = dr.GetDouble("montoImpuesto")
                d.identificacionImpuesto = dr("identificacionImpuesto")
                d.precioBrutoArticulos = dr.GetDouble("precioBrutoArticulos")
                d.precioNetoArticulos = dr.GetDouble("precioNetoArticulos")
                lista.Add(d)
            End While
            dr.Close()
            Return lista
        Else
            dr.Close()
            Return Nothing
        End If
    End Function

    Public Function buscar(ByVal idDetalle) As detalleAdendaModelo
        comm.CommandText = "select * from tbladendamodelodetalle where id=" + idDetalle.ToString()
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Dim d As New detalleAdendaModelo
        If dr.HasRows() Then
            While dr.Read()

                d.id = dr("id")
                d.idAdenda = dr("idAdenda")
                d.posicionPedido = dr("posicionPedido")
                d.codigoEAN = dr("codigoEAN")
                d.numProveedor = dr("numProveedor")
                d.idioma = dr("idioma")
                d.cantidadProductosFacturada = dr("cantidadProductosFacturada")
                d.unidadMedida = dr("unidadMedida")
                d.precioBruto = dr("precioBruto")
                d.precioNeto = dr("precioNeto")
                d.descripcion = dr("descripcion")
                d.numReferenciaAdicional = dr("numReferenciaAdicional")
                d.tipoArancel = dr("tipoArancel")
                d.numIdImpuesto = dr("numIdImpuesto")
                d.porcentajeImpuesto = dr("porcentajeImpuesto")
                d.montoImpuesto = dr("montoImpuesto")
                d.identificacionImpuesto = dr("identificacionImpuesto")
                d.precioBrutoArticulos = dr("precioBrutoArticulos")
                d.precioNetoArticulos = dr("precioNetoArticulos")
            End While
            dr.Close()
            Return d
        Else
            dr.Close()
            Return Nothing
        End If
    End Function
End Class
