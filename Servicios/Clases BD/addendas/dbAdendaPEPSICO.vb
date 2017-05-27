Public Class dbAdendaPEPSICO
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public tipo As String
    Public version As Integer
    Public idPedido As String
    Public idSolicitudPago As String
    Public referencia As String
    Public serie As String
    Public folio As String
    Public folioUUID As String
    Public tipoDoc As Integer
    Public idProveedor As String
    Public idRecepcion As String
    Public idVenta As Integer
    Public idAddenda As Integer
    Dim tablaRecepciones As DataTable
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Inicializa()
        Comm.Connection = Conexion
    End Sub
    Private Sub Inicializa()
        tipo = ""
        version = 0
        idPedido = 0
        idSolicitudPago = ""
        referencia = ""
        serie = ""
        folio = ""
        folioUUID = ""
        tipoDoc = 0
        idProveedor = 0
        idRecepcion = 0
        idVenta = 0
        idAddenda = 0
    End Sub
    Public Sub Guardar(ByVal ptipo As String, ByVal pversion As String, ByVal pidPedido As String, ByVal pidSolicitudPago As String, ByVal preferencia As String, ByVal pserie As String, ByVal pfolio As String, ByVal pfolioUUID As String, ByVal ptipoDoc As String, ByVal pidProveedor As String, ByVal pidVenta As String)

        tipo = ptipo
        version = Integer.Parse(pversion)
        idPedido = pidPedido
        idSolicitudPago = pidSolicitudPago
        referencia = preferencia
        serie = pserie
        folio = pfolio
        folioUUID = pfolioUUID
        tipoDoc = Integer.Parse(ptipoDoc)
        idProveedor = pidProveedor
        ' idRecepcion = Integer.Parse(pidRecepcion)
        idVenta = Integer.Parse(pidVenta)


        Comm.CommandText = "insert into tbladdendapepsico(tipo,version,idPedido,idSolicitudPago,referencia,serie,folio,folioUUID,tipoDoc,idProveedor,idVenta) values( '" + _
        Replace(tipo, "'", "''") + "' ," + version.ToString + ",'" + idPedido + "','" + Replace(idSolicitudPago, "'", "''") + "','" + Replace(referencia, "'", "''") + "','" + Replace(serie, "'", "''") + "','" + Replace(folio, "'", "''") + "','" + Replace(folioUUID, "'", "''") + "'," + tipoDoc.ToString() + ",'" + idProveedor + "'," + idVenta.ToString() + ")"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "select MAX(idAddenda) FROM tbladdendapepsico"
        idAddenda = Comm.ExecuteScalar

    End Sub
    Public Function obtenerDatosVenta(ByVal pidinventario As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select t1.cantidad,t2.nombre,t1.descripcion,t1.precio,t1.idventasinventario from tblventasinventario t1,tbltiposcantidades t2 where t1.tipocantidadm=t2.idtipocantidad and t1.idventasinventario=" + pidinventario.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasinventario")
        Return DS.Tables("tblventasinventario")
    End Function


    Public Sub Modificar(ByVal ptipo As String, ByVal pversion As String, ByVal pidPedido As String, ByVal pidSolicitudPago As String, ByVal preferencia As String, ByVal pserie As String, ByVal pfolio As String, ByVal pfolioUUID As String, ByVal ptipoDoc As String, ByVal pidProveedor As String, ByVal pidVenta As String)
        tipo = ptipo
        version = Integer.Parse(pversion)
        idPedido = pidPedido
        idSolicitudPago = pidSolicitudPago
        referencia = preferencia
        serie = pserie
        folio = pfolio
        folioUUID = pfolioUUID
        tipoDoc = Integer.Parse(ptipoDoc)
        idProveedor = pidProveedor
        ' idRecepcion = Integer.Parse(pidRecepcion)
        idVenta = Integer.Parse(pidVenta)
        Comm.CommandText = "update tbladdendapepsico set tipo='" + Replace(tipo, "'", "''") + "' ,version=" + version.ToString() + " ,idPedido='" + idPedido.ToString() + "' ,idSolicitudPago='" + Replace(idSolicitudPago, "'", "''") + "' ,referencia='" + Replace(referencia, "'", "''") + "' ,serie='" + Replace(serie, "'", "''") + "' ,folio='" + Replace(folio, "'", "''") + "' ,folioUUID='" + Replace(folioUUID, "'", "''") + "' ,tipoDoc=" + tipoDoc.ToString() + " ,idProveedor='" + idProveedor.ToString() + "' where(idVenta = " + idVenta.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub LlenaDatos(ByVal pIdVenta As Integer)
        idAddenda = 0
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbladdendapepsico where idventa=" + pIdVenta.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            tipo = DReader("tipo")
            version = DReader("version")
            idPedido = DReader("idPedido")
            idSolicitudPago = DReader("idSolicitudPago")
            referencia = DReader("referencia")
            serie = DReader("serie")
            folio = DReader("folio")
            folioUUID = DReader("folioUUID")
            tipoDoc = DReader("tipoDoc")
            idProveedor = DReader("idProveedor")
            'idRecepcion = DReader("idRecepcion")
            idVenta = pIdVenta
            idAddenda = DReader("idAddenda")

        End If
        DReader.Close()
        tablaRecepciones = llenaTablaRecepciones(idAddenda)
    End Sub
    Public Function CreaXML(ByVal pIdVenta As Integer) As String
        Dim tablaVenta As DataTable
        Dim XMLdoc As String
        Dim Precio As Double
        LlenaDatos(pIdVenta)

        '    ArreglaDatos()

        XMLdoc = " <cfdi:Addenda>" + vbCrLf
        XMLdoc += "<RequestCFD tipo=""AddendaPCO"" version=""2.0"" idPedido=""" + idPedido.ToString() + """>" + vbCrLf
        XMLdoc += "<Documento folioUUID=""" + folioUUID.ToString() + """ tipoDoc=""" + tipoDoc.ToString() + """/>" + vbCrLf
        XMLdoc += "<Proveedor idProveedor=""" + idProveedor.ToString() + """/>" + vbCrLf
        XMLdoc += "<Recepciones>" + vbCrLf
        For i As Integer = 0 To tablaRecepciones.Rows.Count() - 1
            tablaVenta = obtenerDatosVenta(Integer.Parse(tablaRecepciones.Rows(i)(2).ToString()))
            Precio = (Double.Parse(tablaVenta.Rows(0)(3).ToString)) / (Double.Parse(tablaVenta.Rows(0)(0).ToString))
            XMLdoc += "<Recepcion idRecepcion=""" + tablaRecepciones.Rows(i)(3).ToString() + """>" + vbCrLf
            XMLdoc += "<Concepto cantidad=""" + Format(Double.Parse(tablaVenta.Rows(0)(0).ToString), "0.00") + """"
            XMLdoc += " descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(tablaVenta.Rows(0)(2), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ importe=""" + Format(Double.Parse(tablaVenta.Rows(0)(3).ToString), "0.00") + """"
            XMLdoc += " unidad=""" + tablaVenta.Rows(0)(1).ToString + """ valorUnitario=""" + Format(Precio, "0.00") + """/>" + vbCrLf
            XMLdoc += " </Recepcion>" + vbCrLf
        Next
        XMLdoc += " </Recepciones>" + vbCrLf
        XMLdoc += "</RequestCFD>" + vbCrLf
        XMLdoc += "</cfdi:Addenda>" + vbCrLf

        Return XMLdoc
    End Function
    Public Function llenaTabla(ByVal pidVenta As Integer) As DataTable


        Dim DS As New DataSet
        Comm.CommandText = "select t1.cantidad,t2.nombre,t1.descripcion,t1.precio,t1.idventasinventario from tblventasinventario t1,tbltiposcantidades t2 where t1.tipocantidadm=t2.idtipocantidad and t1.idventa=" + pidVenta.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasinventario")
        Return DS.Tables("tblventasinventario")



    End Function
    Public Sub GuardarRecepcion(ByVal pidAddenda As Integer, ByVal pidInventario As Integer, ByVal precepcion As String)

        Comm.CommandText = "insert into tbladdendapesicorecepcion(idAddenda,idInventario,recepcion) values( " + _
       pidAddenda.ToString() + " ," + pidInventario.ToString() + ",'" + precepcion.ToString() + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub eliminarRecepcion(ByVal pidAddenda As Integer)

        Comm.CommandText = "delete from tbladdendapesicorecepcion where  idAddenda=" + pidAddenda.ToString
        Comm.ExecuteNonQuery()

    End Sub
    Public Function llenaTablaRecepciones(ByVal pidAddenda As Integer) As DataTable


        Dim DS As New DataSet
        Comm.CommandText = "select * from tbladdendapesicorecepcion  where idAddenda=" + pidAddenda.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbladdendapesicorecepcion")
        Return DS.Tables("tbladdendapesicorecepcion")



    End Function

End Class
