Public Class dbPedidos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Folio As String
    Public IdProveedor As Integer
    Public Fecha As String
    Public Proveedor As dbproveedores
    Public Desglosar As Byte
    Public Estado As Byte
    Public Autorizado As Byte
    Public EmbarcarA As String
    Public LAB As String
    Public Condiciones As String
    Public EmbarcarPor As String
    Public ConsignarA As String
    Public Observaciones As String
    Public IdSucursal As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdProveedor = -1
        Fecha = ""
        Desglosar = 0
        Estado = 0
        Autorizado = 0
        EmbarcarA = ""
        LAB = ""
        Condiciones = ""
        EmbarcarPor = ""
        ConsignarA = ""
        Observaciones = ""
        Folio = ""
        IdSucursal = 0
        Comm.Connection = Conexion
        Proveedor = New dbproveedores(Comm.Connection)
    End Sub
    Public Function ChecaFolioRepetido(ByVal pFolio As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(idpedido) from tblpedidos where folio='" + Replace(pFolio, "'", "''") + "'"
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
        Comm.CommandText = "select * from tblpedidos where idpedido=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdProveedor = DReader("idproveedor")
            Fecha = DReader("fecha")
            Desglosar = DReader("desglosar")
            Autorizado = DReader("autorizado")
            Estado = DReader("estado")
            EmbarcarA = DReader("embarcara")
            LAB = DReader("lab")
            Condiciones = DReader("condiciones")
            EmbarcarPor = DReader("embarcarpor")
            ConsignarA = DReader("consignara")
            Observaciones = DReader("observaciones")
            IdSucursal = DReader("idsucursal")
        End If
        DReader.Close()
        Proveedor = New dbproveedores(IdProveedor, Comm.Connection)

    End Sub
    Public Sub Guardar(ByVal pIdProveedor As Integer, ByVal pFecha As String, ByVal pDesglosar As Byte, ByVal pEstado As Byte, ByVal pAutorizado As Byte, ByVal pEmbarcarA As String, ByVal pLab As String, ByVal pCondiciones As String, ByVal pEmbarcarPor As String, ByVal pConsignarA As String, ByVal pObservaciones As String, ByVal pFolio As String, ByVal pIdSucursal As Integer)
        IdProveedor = pIdProveedor
        Fecha = pFecha
        Desglosar = pDesglosar
        Autorizado = pAutorizado
        Estado = pEstado
        EmbarcarA = pEmbarcarA
        LAB = pLab
        Condiciones = pCondiciones
        EmbarcarPor = pEmbarcarPor
        ConsignarA = pConsignarA
        Folio = pFolio
        Observaciones = pObservaciones
        IdSucursal = pIdSucursal
        Comm.CommandText = "insert into tblpedidos(idproveedor,fecha,desglosar,estado,autorizado,embarcara,lab,condiciones,embarcarpor,consignara,observaciones,folio,idsucursal) values(" + IdProveedor.ToString + ",'" + Fecha + "'," + Desglosar.ToString + "," + Estado.ToString + "," + Autorizado.ToString + ",'" + Replace(EmbarcarA, "'", "''") + "','" + Replace(LAB, "'", "''") + "','" + Replace(Condiciones, "'", "''") + "','" + Replace(EmbarcarPor, "'", "''") + "','" + Replace(ConsignarA, "'", "''") + "','" + Replace(Observaciones, "'", "''") + "','" + Folio + "'," + IdSucursal.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idpedido) from tblpedidos"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pDesglosar As Byte, ByVal pEstado As Byte, ByVal pAutorizado As Byte, ByVal pEmbarcarA As String, ByVal pLab As String, ByVal pCondiciones As String, ByVal pEmbarcarPor As String, ByVal pConsignarA As String, ByVal pObservaciones As String)
        ID = pID
        Fecha = pFecha
        Desglosar = pDesglosar
        Autorizado = pAutorizado
        Estado = pEstado
        EmbarcarA = pEmbarcarA
        LAB = pLab
        Condiciones = pCondiciones
        EmbarcarPor = pEmbarcarPor
        ConsignarA = pConsignarA
        Observaciones = pObservaciones
        Comm.CommandText = "update tblpedidos set fecha='" + Fecha + "',desglosar=" + Desglosar.ToString + ",estado=" + Estado.ToString + ",autorizado=" + Autorizado.ToString + ",embarcara='" + Replace(EmbarcarA, "'", "''") + "',lab='" + Replace(LAB, "'", "''") + "',condiciones='" + Replace(Condiciones, "'", "''") + "',embarcarpor='" + Replace(EmbarcarPor, "'", "''") + "',consignara='" + Replace(ConsignarA, "'", "''") + "',observaciones='" + Replace(Observaciones, "'", "''") + "' where idpedido=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblpedidos where idpedido=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, Optional ByVal pNombreClave As String = "", Optional ByVal pEstado As Byte = 3, Optional ByVal pAutorizado As Byte = 3) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblpedidos.idpedido,tblpedidos.fecha,tblpedidos.folio,tblproveedores.clave,tblproveedores.nombre as Proveedor from tblpedidos inner join tblproveedores on tblpedidos.idproveedor=tblproveedores.idproveedor where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pEstado <> 2 Then
            Comm.CommandText += " and tblpedidos.estado=" + pEstado.ToString
        End If
        If pAutorizado <> 2 Then
            Comm.CommandText += " and tblpedidos.autorizado=" + pAutorizado.ToString
        End If
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblproveedores.clave,tblproveedores.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpedidos")
        Return DS.Tables("tblpedidos").DefaultView
    End Function
    Public Function DaTotal(ByVal pidCompra As Integer, ByVal pidMoneda As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0
        Dim Encontro As Double
        Dim Cont As Integer = 1
        Comm.CommandText = "select iddetalle from tblpedidosdetalles where idpedido=" + pidCompra.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblpedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblpedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
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
