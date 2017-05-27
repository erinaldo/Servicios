Public Class dbAddendaLey
    Public Clasificacion As Byte
    Public Tipo As Byte
    Public Proveedor As String '10
    Public Centro As String '4
    Public NumeroEntrada As String '10
    Public FechaEntrada As String 'fecha
    Public ProveededorSap As String '10 op
    Public NoRemision As String
    Public Descuento As Double
    'Dim ProveedorSapAfys As String '10 
    Public NombreProveedorSap As String
    Public IdVenta As Integer
    Public Pedido As String
    Public Idaddenda As Integer
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Inicializa()
        Comm.Connection = Conexion
    End Sub
    Private Sub Inicializa()
        Clasificacion = 0
        Tipo = 0
        Proveedor = ""
        Centro = ""
        NumeroEntrada = ""
        FechaEntrada = ""
        ProveededorSap = ""
        NoRemision = ""
        Descuento = 0
        IdVenta = 0
        Pedido = ""
        Idaddenda = 0
        NombreProveedorSap = ""
    End Sub
    Public Sub Guardar(ByVal pIdVenta As Integer, ByVal pClasificacion As Byte, ByVal pTipo As Byte, ByVal pProveedor As String, ByVal pCentro As String, ByVal pNoEntrada As String, ByVal pNoRemision As String, ByVal pProveedorSap As String, ByVal pNombreProveedorSAp As String, ByVal pFechaEntrada As String, ByVal pDescuento As Double, ByVal pPedido As String)
        Clasificacion = pClasificacion
        Tipo = pTipo
        Proveedor = pProveedor
        Centro = pCentro
        NumeroEntrada = pNoEntrada
        FechaEntrada = pFechaEntrada
        ProveededorSap = pProveedorSap
        NoRemision = pNoRemision
        Descuento = pDescuento
        IdVenta = pIdVenta
        NombreProveedorSap = pNombreProveedorSAp
        Pedido = pPedido
        Comm.CommandText = "insert into tbladdendaley(idventa,clasificacion,tipo,proveedor,centro,numeroentrada,noremision,proveedorsap,nombreproveedor,fechaentrada,descuento,pedido) values(" + _
        IdVenta.ToString + "," + Clasificacion.ToString + "," + Tipo.ToString + ",'" + Replace(Proveedor, "'", "''") + "','" + Replace(Centro, "'", "''") + "','" + Replace(NumeroEntrada, "'", "''") + "','" + Replace(NoRemision, "'", "''") + "','" + Replace(ProveededorSap, "'", "''") + "','" + Replace(NombreProveedorSap, "'", "''") + "','" + FechaEntrada + "','" + Descuento.ToString + "','" + Replace(Pedido, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pIdVenta As Integer, ByVal pClasificacion As Byte, ByVal pTipo As Byte, ByVal pProveedor As String, ByVal pCentro As String, ByVal pNoEntrada As String, ByVal pNoRemision As String, ByVal pProveedorSap As String, ByVal pNombreProveedorSAp As String, ByVal pFechaEntrada As String, ByVal pDescuento As Double, ByVal pPedido As String)
        Clasificacion = pClasificacion
        Tipo = pTipo
        Proveedor = pProveedor
        Centro = pCentro
        NumeroEntrada = pNoEntrada
        FechaEntrada = pFechaEntrada
        ProveededorSap = pProveedorSap
        NoRemision = pNoRemision
        Descuento = pDescuento
        IdVenta = pIdVenta
        Pedido = pPedido
        NombreProveedorSap = pNombreProveedorSAp
        Comm.CommandText = "update tbladdendaley set clasificacion=" + pClasificacion.ToString + ",tipo=" + pTipo.ToString + ",proveedor='" + Replace(Proveedor, "'", "''") + "',centro='" + Replace(Centro, "'", "''") + "',numeroentrada='" + Replace(NumeroEntrada, "'", "''") + "',proveedorsap='" + Replace(ProveededorSap, "'", "''") + "',noremision='" + Replace(NoRemision, "'", "''") + "',descuento=" + Descuento.ToString + ",pedido='" + Replace(Pedido, "'", "''") + "' where idventa=" + IdVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub LlenaDatos(ByVal pIdVenta As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbladdendaley where idventa=" + pIdVenta.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Clasificacion = 0
            Tipo = DReader("tipo")
            Proveedor = DReader("proveedor")
            Centro = DReader("centro")
            NumeroEntrada = DReader("numeroentrada")
            FechaEntrada = DReader("fechaentrada")
            ProveededorSap = DReader("proveedorsap")
            NoRemision = DReader("noremision")
            Descuento = DReader("descuento")
            IdVenta = DReader("idventa")
            NombreProveedorSap = DReader("nombreproveedor")
            Pedido = DReader("pedido")
            Idaddenda = DReader("idaddenda")
        End If
        DReader.Close()
    End Sub
    Private Sub ArreglaDatos()
        'Replace(Replace(Replace(Replace(Replace(DR("clave2"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;")
        'Clasificacion = 0
        'Tipo = DReader("tipo")
        If Proveedor <> "" Then Proveedor = Replace(Replace(Replace(Replace(Replace(Proveedor, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;")
        If Centro <> "" Then Centro = Replace(Replace(Replace(Replace(Replace(Centro, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;")
        If NumeroEntrada <> "" Then NumeroEntrada = Replace(Replace(Replace(Replace(Replace(NumeroEntrada, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;")
        'FechaEntrada = DReader("fechaentrada")
        If ProveededorSap <> "" Then ProveededorSap = Replace(Replace(Replace(Replace(Replace(ProveededorSap, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;")
        If NoRemision <> "" Then NoRemision = Replace(Replace(Replace(Replace(Replace(NoRemision, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;")
        'Descuento = DReader("descuento")
        'IdVenta = DReader("idventa")
        If NombreProveedorSap <> "" Then NombreProveedorSap = Replace(Replace(Replace(Replace(Replace(NombreProveedorSap, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;")
        If Pedido <> "" Then Pedido = Replace(Replace(Replace(Replace(Replace(Pedido, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;")
    End Sub
    Public Function CreaXML(ByVal pIdVenta As Integer, ByVal Eselectronica As Byte, ByVal pemail As String) As String
        Dim XMLdoc As String
        LlenaDatos(pIdVenta)
        ArreglaDatos()
        If Eselectronica = 1 Then
            XMLdoc = "<Addenda>" + vbCrLf
        Else
            XMLdoc = "<cfdi:Addenda>" + vbCrLf
        End If
        XMLdoc += "<cley:ADDENDA_CLEY xmlns:cley=""http://2servicios.casaley.com.mx/factura_electronica"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xsi:schemaLocation=""http://servicios.casaley.com.mx/factura_electronica http://servicios.casaley.com.mx/factura_electronica/XSD_ADDENDA_CASALEY.xsd"" VERSION=""1.0"" CORREO=""" + Replace(Replace(Replace(Replace(Replace(pemail, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf
        Select Case Clasificacion
            Case 0
                XMLdoc += "<cley:MERCADERIAS>" + vbCrLf
            Case 1
                XMLdoc += "<cley:ACTIVOSFIJOS>" + vbCrLf
            Case 2
                XMLdoc += "<cley:SERVICIOS>" + vbCrLf
        End Select

        If Clasificacion = 0 And Tipo = 0 Then
            XMLdoc += "<cley:PD PROVEEDOR=""" + Proveedor.PadLeft(10, "0") + """ CENTRO=""" + Centro.PadLeft(4, "0") + """ NUMERO_ENTRADA=""" + NumeroEntrada.PadLeft(10, "0") + """ PROVEEDOR_SAP=""" + ProveededorSap.PadLeft(10, "0") + """ NO_REMISION=""" + NoRemision.PadLeft(10, "0") + """ DESCUENTO=""" + Format(Descuento, "0.00") + """ />" + vbCrLf
        End If
        If Clasificacion = 0 And Tipo = 1 Then
            XMLdoc += "<cley:PC PROVEEDOR=""" + Proveedor.PadLeft(10, "0") + """ CENTRO=""" + Centro.PadLeft(4, "0") + """ NUMERO_ENTRADA=""" + NumeroEntrada.PadLeft(10, "0") + """ PROVEEDOR_SAP=""" + ProveededorSap.PadLeft(10, "0") + """ NO_REMISION=""" + NoRemision.PadLeft(10, "0") + """ DESCUENTO=""" + Format(Descuento, "0.00") + """ />" + vbCrLf
        End If
        If Clasificacion = 0 And Tipo = 2 Then
            XMLdoc += "<cley:PA PROVEEDOR=""" + Proveedor.PadLeft(10, "0") + """ CENTRO=""" + Centro.PadLeft(4, "0") + """ NUMERO_ENTRADA=""" + NumeroEntrada.PadLeft(10, "0") + """ PROVEEDOR_SAP=""" + ProveededorSap.PadLeft(10, "0") + """ NO_REMISION=""" + NoRemision.PadLeft(10, "0") + """ DESCUENTO=""" + Format(Descuento, "0.00") + """ />" + vbCrLf
        End If
        If Clasificacion = 0 And Tipo = 3 Then
            XMLdoc += "<cley:RMP PROVEEDOR=""" + Proveedor.PadLeft(10, "0") + """ CENTRO=""" + Centro.PadLeft(4, "0") + """ NUMERO_ENTRADA=""" + NumeroEntrada.PadLeft(10, "0") + """ PROVEEDOR_SAP=""" + ProveededorSap.PadLeft(10, "0") + """ NO_REMISION=""" + NoRemision.PadLeft(10, "0") + """ DESCUENTO=""" + Format(Descuento, "0.00") + """ />" + vbCrLf
        End If
        If Clasificacion = 0 And Tipo = 4 Then
            XMLdoc += "<cley:CD PROVEEDOR=""" + Proveedor.PadLeft(10, "0") + """ CENTRO=""" + Centro.PadLeft(4, "0") + """ NUMERO_ENTRADA=""" + NumeroEntrada.PadLeft(10, "0") + """ PROVEEDOR_SAP=""" + ProveededorSap.PadLeft(10, "0") + """ NO_REMISION=""" + NoRemision.PadLeft(10, "0") + """ DESCUENTO=""" + Format(Descuento, "0.00") + """ />" + vbCrLf
        End If

        If Clasificacion = 0 Then
            'Detalles
            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            Dim VI As New dbVentasInventario(MySqlcon)
            DR = VI.ConsultaReader(pIdVenta, False, "0", 0, "0")
            Dim Cont As Integer = 1
            While DR.Read
                If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                    XMLdoc += "<cley:DETALLE "
                    XMLdoc += "RENGLON=""" + Cont.ToString + """ "
                    XMLdoc += "CANTIDAD=""" + DR("cantidad").ToString + """ "
                    XMLdoc += "UMC=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
                    XMLdoc += "DESCUENTO=""" + Format(((DR("precio") / (1 - DR("descuento") / 100)) - DR("precio")), "0.00") + """ "
                    XMLdoc += "PRECIO_LISTA=""" + Format(DR("precio") / DR("cantidad")) + """ "
                    XMLdoc += "IMPUESTO_IVA=""" + Format((DR("precio") * (1 + DR("iva") / 100)) - DR("precio"), "0.00") + """ "
                    XMLdoc += "IMPUESTO_IEPS=""0.00"" "
                    XMLdoc += "TASA_IVA=""" + DR("iva").ToString + """ "
                    XMLdoc += "TASA_IEPS=""0"" >" + vbCrLf
                    XMLdoc += "<cley:CODBAR_ARTICULO COD_BAR="""" /> "
                    XMLdoc += "<cley:CLEY_ARTICULO COD_ARTICULO=""" + Replace(Replace(Replace(Replace(Replace(DR("clave2"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ />"
                    XMLdoc += "</cley:DETALLE>" + vbCrLf
                    Cont += 1
                End If
            End While
            DR.Close()
        End If
        If Clasificacion >= 1 And Tipo = 0 Then
            XMLdoc += "<cley:PEDIDO_SISTEMA PROVEEDOR_SAP=""" + ProveededorSap.PadLeft(10, "0") + """ NOMBRE_PROVEEDOR=""" + NombreProveedorSap + """ PEDIDO=""" + Pedido + """ CENTRO=""" + Centro.PadLeft(4, "0") + """>"
        End If
        If Clasificacion >= 1 And Tipo = 1 Then
            XMLdoc += "<cley:PEDIDO_MANUAL PROVEEDOR_SAP=""" + ProveededorSap.PadLeft(10, "0") + """ NOMBRE_PROVEEDOR=""" + NombreProveedorSap + """ CENTRO=""" + Centro.PadLeft(4, "0") + """>"
        End If
        Select Case Clasificacion
            Case 0
                XMLdoc += "</cley:MERCADERIAS>" + vbCrLf
            Case 1
                XMLdoc += "</cley:ACTIVOSFIJOS>" + vbCrLf
            Case 2
                XMLdoc += "</cley:SERVICIOS>" + vbCrLf
        End Select
        XMLdoc += "</cley:ADDENDA_CLEY>" + vbCrLf
        If Eselectronica = 1 Then
            XMLdoc += "</Addenda>" + vbCrLf
        Else
            XMLdoc += "</cfdi:Addenda>" + vbCrLf
        End If
        Return XMLdoc
    End Function

End Class
