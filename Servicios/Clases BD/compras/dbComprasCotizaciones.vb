Public Class dbComprasCotizaciones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Fecha As String
    Public Referencia As String
    Public Estado As Byte
    Public CondicionesDePago As String
    Public TiempodeEntrega As String
    Public Observaciones As String
    Public idSucursal As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Fecha = ""
        Referencia = ""
        Estado = 0
        CondicionesDePago = ""
        TiempodeEntrega = ""
        Observaciones = ""
        idSucursal = 0
        Comm.Connection = Conexion
    End Sub
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblcomprascotizaciones where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4)),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblcomprascotizaciones where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcomprascotizacion where idcompracotizacion=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Fecha = DReader("fecha")
            Referencia = DReader("referencia")
            Estado = DReader("estado")
            CondicionesDePago = DReader("condicionesdepago")
            TiempodeEntrega = DReader("tiempodeentrega")
            Observaciones = DReader("observaciones")
            idSucursal = DReader("idsucursal")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pFecha As String, ByVal pReferencia As String, ByVal pCondicionesDePago As String, ByVal pTiempoDeEntrega As String, ByVal pObservaciones As String, ByVal pEstado As Byte, ByVal pIdSucursal As Integer)
        Fecha = pFecha
        Referencia = pReferencia
        CondicionesDePago = pCondicionesDePago
        TiempodeEntrega = pTiempoDeEntrega
        Observaciones = pObservaciones
        Estado = pEstado
        idSucursal = pIdSucursal
        Comm.CommandText = "insert into tblcomprascotizacion(fecha,referencia,estado,condicionesdepago,tiempodeentrega,observaciones,idsucursal) values('" + Fecha + "','" + Replace(Referencia, "'", "''") + "'," + Estado.ToString + ",'" + Replace(CondicionesDePago, "'", "''") + "','" + Replace(TiempodeEntrega, "'", "''") + "','" + Replace(Observaciones, "'", "''") + "'," + idSucursal.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idcompracotizacion) from tblcomprascotizacion"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pReferencia As String, ByVal pEstado As Byte, ByVal pCondicionesDePago As String, ByVal pTiempoDeEntrega As String, ByVal pObservaciones As String)
        ID = pID
        Fecha = pFecha
        Referencia = pReferencia
        Estado = pEstado
        CondicionesDePago = pCondicionesDePago
        TiempodeEntrega = pTiempoDeEntrega
        Observaciones = pObservaciones
        Comm.CommandText = "update tblcomprascotizacion set fecha='" + Fecha + "',referencia='" + Replace(Referencia, "'", "''") + "',estado=" + Estado.ToString + ",condicionesdepago='" + Replace(CondicionesDePago, "'", "''") + "',tiempodeentrega='" + Replace(TiempodeEntrega, "'", "''") + "',observaciones='" + Replace(Observaciones, "'", "''") + "' where idcompracotizacion=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcomprascotizacion where idcompracotizacion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pEstado As Byte, Optional ByVal pReferencia As String = "", Optional ByVal pidSucursal As Integer = 0) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblcomprascotizacion.idcompracotizacion,tblcomprascotizacion.fecha,tblcomprascotizacion.referencia,if(tblcomprascotizacion.estado=0,'Abierto','Cerrado') as cestado from tblcomprascotizacion where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        'If pNombreClave <> "" Then
        'Comm.CommandText += " and concat(tblproveedores.clave,tblproveedores.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        'End If
        If pEstado < 2 Then
            Comm.CommandText += " and tblcomprascotizacion.estado=" + pEstado.ToString
        End If
        If pReferencia <> "" Then
            Comm.CommandText += " and tblcomprascotizacion.referencia like '%" + Replace(pReferencia, "'", "''") + "%'"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblcomprascotizacion.idsucursal=" + pidSucursal.ToString
        End If
        Comm.CommandText += " order by tblcomprascotizacion.fecha desc,tblcomprascotizacion.serie,tblcomprascotizacion.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprascotizacion")
        Return DS.Tables("tblcomprascotizacion").DefaultView
    End Function
    Public Function DaTotal(ByVal pidCompra As Integer, ByVal pidMoneda As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0
        Dim Encontro As Double
        Dim Cont As Integer = 1
        Comm.CommandText = "select iddetalle from tblcomprasdetalles where idcompra=" + pidCompra.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblcomprasdetalles where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblcomprasdetalles where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            If IdMonedaC <> pidMoneda Then
                Comm.CommandText = "select if((select cantidad from tblmonedasconversiones where idmoneda=" + pidMoneda.ToString + " and idmoneda2=" + IdMonedaC.ToString + ") is null,0,(select cantidad from tblmonedasconversiones where idmoneda=" + pidMoneda.ToString + " and idmoneda2=" + IdMonedaC.ToString + "))"
                Encontro = Comm.ExecuteScalar
                If Encontro = 0 Then
                    Comm.CommandText = "select if((select cantidad from tblmonedasconversiones where idmoneda2=" + pidMoneda.ToString + " and idmoneda=" + IdMonedaC.ToString + ") is null,1,(select cantidad from tblmonedasconversiones where idmoneda2=" + pidMoneda.ToString + " and idmoneda=" + IdMonedaC.ToString + "))"
                    Encontro = Comm.ExecuteScalar
                    Precio = Precio * Encontro
                Else
                    Precio = Precio / Encontro
                End If
            End If
            Total += Precio
            Cont += 1
        End While

        Return Total
    End Function
End Class
