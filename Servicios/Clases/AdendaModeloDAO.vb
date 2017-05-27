Imports MySql.Data.MySqlClient
Public Class AdendaModeloDAO
    Dim comm As New MySqlCommand
    Dim insert As String = "insert into tbladendamodelo(entityType,idCreador,texto,idReferencia,informacionComprador," +
        "idCreadorAlt,tipoDivisa,tipoCambio,numOrden,cantidadBase2,porcentajeTax2,cantidadTax2,tipoTax," +
        "cantidadFinal,idVenta,cantidadTotal) "
    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub agregar(ByRef adenda As AdendaModelo)
        comm.CommandText = insert + "values('" + adenda.entityType +
            "','" + adenda.idCreador +
            "','" + adenda.texto + "'," +
            "'" + adenda.idReferencia +
            "','" + adenda.informacionComprador +
            "','" + adenda.idCreadorAlt +
            "','" + adenda.tipoDivisa +
            "'," + adenda.tipoCambio.ToString() +
            "," + adenda.cantidadBase2.ToString() +
            "," + adenda.porcentajeTax2.ToString() +
            "," + adenda.cantidadTax2.ToString() +
            ",'" + adenda.categoriaTax2 +
            "','" + adenda.tipoTax +
            "'," + adenda.cantidadFinal.ToString() +
            "," + adenda.idVenta.ToString() +
             "," + adenda.cantidadTotal.ToString() + ");"
        Try
            comm.ExecuteNonQuery()
        Catch e As Exception
            Throw e
        End Try
    End Sub

    Public Sub actualizar(ByRef adenda As AdendaModelo)
        comm.CommandText = "update tbladendamodelo set entityType='" + adenda.entityType + "'," +
            "idCreador='" + adenda.idCreador + "', texto='" + adenda.texto + "', idReferencia='" + adenda.idReferencia + "', " +
            "informacionComprador='" + adenda.informacionComprador + "', idCreadorAlt='" + adenda.idCreadorAlt + "', " +
            "tipoDivisa='" + adenda.tipoDivisa + "', tipoCambio=" + adenda.tipoCambio.ToString() + ", cantidadFinal=" + adenda.cantidadFinal.ToString() + ", cantidadTotal=" + adenda.cantidadTotal.ToString() + ", cantidadBase2=" + adenda.cantidadBase2.ToString() + ", " +
            "porcentajeTax2=" + adenda.porcentajeTax2.ToString() + ", cantidadTax2=" + adenda.cantidadTax2.ToString() + ", categoriaTax2='" + adenda.categoriaTax2 + "', tipoTax='" + adenda.tipoTax + "', " +
            "idVenta=" + adenda.idVenta.ToString() + " where id=" + adenda.id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub eliminar(ByRef adenda As AdendaModelo)
        comm.CommandText = "delete from tbladendamodelo where id=" + adenda.id + ";"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function listaAdendas() As List(Of AdendaModelo)
        Return Nothing
    End Function

    Public Function vistaAdendas() As DataView
        Dim ds As New DataSet
        comm.CommandText = "select * from tbladendamodelo;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "adendas")
        Return ds.Tables("adendas").DefaultView
    End Function

    Public Function buscar(ByVal idAdenda As Integer) As AdendaModelo
        comm.CommandText = "select * from tbladendamodelo where id=" + idAdenda.ToString() + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Dim a As New AdendaModelo
        If dr.HasRows Then
            While dr.Read()
                a.id = dr("id")
                a.entityType = dr("entityType")
                a.idCreador = dr("idCreador")
                a.texto = dr("texto")
                a.idReferencia = dr("idReferencia")
                a.informacionComprador = dr("informacionComprador")
                a.idCreadorAlt = dr("idCreadorAlt")
                a.tipoDivisa = dr("tipoDivisa")
                a.tipoCambio = dr("tipoCambio")
                a.cantidadTotal = dr("cantidadTotal")
                a.cantidadBase2 = dr("cantidadBase2")
                a.porcentajeTax2 = dr("porcentajeTax2")
                a.cantidadTax2 = dr("cantidadTax2")
                a.categoriaTax2 = dr("categoriaTax2")
                a.tipoTax = dr("tipoTax")
                a.cantidadFinal = dr("cantidadFinal")
                a.idVenta = dr("idVenta")
            End While
            dr.Close()
            Return a
        Else
            dr.Close()
            Return Nothing
        End If
    End Function

    Public Function buscarPorVenta(ByVal idVenta As Integer) As AdendaModelo
        comm.CommandText = "select * from tbladendamodelo where idVenta=" + idVenta.ToString() + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Dim a As New AdendaModelo
        If dr.HasRows Then
            While dr.Read()
                a.id = dr("id")
                a.entityType = dr("entityType")
                a.idCreador = dr("idCreador")
                a.texto = dr("texto")
                a.idReferencia = dr("idReferencia")
                a.informacionComprador = dr("informacionComprador")
                a.idCreadorAlt = dr("idCreadorAlt")
                a.tipoDivisa = dr("tipoDivisa")
                a.tipoCambio = dr("tipoCambio")
                a.cantidadTotal = dr("cantidadTotal")
                a.cantidadBase2 = dr("cantidadBase2")
                a.porcentajeTax2 = dr("porcentajeTax2")
                a.cantidadTax2 = dr("cantidadTax2")
                a.categoriaTax2 = dr("categoriaTax2")
                a.tipoTax = dr("tipoTax")
                a.cantidadFinal = dr("cantidadFinal")
                a.idVenta = dr("idVenta")
            End While
            dr.Close()
            Return a
        Else
            dr.Close()
            Return Nothing
        End If
    End Function
    Public Function crearAdenda() As AdendaModelo
        comm.CommandText = "insert into tbladendamodelo() values()"
        comm.ExecuteNonQuery()
        Dim a As New AdendaModelo()
        comm.CommandText = "select max(id) as id from tbladendamodelo"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            a.id = dr("id")
        End While
        dr.Close()
        Return a
    End Function

    Public Function ultimoId() As Integer
        Dim id As New Integer
        comm.CommandText = "select max(id) as id from tbladendamodelo"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            id = dr("id")
        End While
        dr.Close()
        Return id
    End Function
End Class
