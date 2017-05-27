Public Class dbAdendasFemsa
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public idVenta As Integer
    Public Version As Byte
    Public claveDoc As Byte
    Public NoSociedad As String
    Public NoProveedor As String
    Public NoPedido As String
    Public Moneda As String
    Public NoEntrada As String
    Public NoRemision As String
    Public Nosocio As String
    Public Centro As String
    Public IniPerLiq As String
    Public FinPerLiq As String
    Public Retencion1 As String
    Public Retencion2 As String
    Public Retencion3 As String
    Public DatosAdicionales As String
    Public TipoOperacion As String
    Public TipoDocumento As Byte
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        idVenta = 0
        Version = 1
        claveDoc = 1
        NoSociedad = ""
        NoProveedor = ""
        NoPedido = ""
        Moneda = "MXN"
        NoEntrada = ""
        NoRemision = ""
        Nosocio = ""
        Centro = ""
        IniPerLiq = ""
        FinPerLiq = ""
        Retencion1 = ""
        Retencion2 = ""
        Retencion3 = ""
        DatosAdicionales = ""
        TipoOperacion = ""
        TipoDocumento = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pIDVenta As Integer, ByVal pTipodocumento As Byte, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
        idVenta = pIDVenta
        TipoDocumento = pTipodocumento
        ID = -1
        LlenaDatos()
    End Sub
    Public Sub Guardar(ByVal pIdVenta As Integer, ByVal pVersion As Byte, ByVal pClaveDoc As Byte, ByVal pNoSociedad As String, ByVal pNoProveedor As String, ByVal pNoPedido As String, ByVal pMoneda As String, ByVal pnoEntrada As String, ByVal pnoRemision As String, ByVal pnoSocio As String, ByVal pCentro As String, ByVal pIniPerLiq As String, ByVal pFinPerLiq As String, ByVal pRetencion1 As String, ByVal pRetencion2 As String, ByVal pretencion3 As String, ByVal pDatosAdicionales As String, ByVal pTipoOperacion As String, ByVal pTipoDocumento As Byte)
        idVenta = pIdVenta
        Version = pVersion
        claveDoc = pClaveDoc
        NoSociedad = pNoSociedad
        NoProveedor = pNoProveedor
        NoPedido = pNoPedido
        Moneda = pMoneda
        NoEntrada = pnoEntrada
        NoRemision = pnoRemision
        Nosocio = pnoSocio
        Centro = pCentro
        IniPerLiq = pIniPerLiq
        FinPerLiq = pFinPerLiq
        Retencion1 = pRetencion1
        Retencion2 = pRetencion2
        Retencion3 = pretencion3
        DatosAdicionales = pDatosAdicionales
        TipoOperacion = pTipoOperacion
        TipoDocumento = pTipoDocumento
        Comm.CommandText = "insert into tbladendafemsa(idventa,version,clasedoc,nosociedad,noproveedor,nopedido,moneda,noentrada,noremision,nosocio,centro,iniperliq,finperliq,retencion1,retencion2,retencion3,datosadicionales,tipooperacion,tipodocumento) values(" + _
        idVenta.ToString + "," + Version.ToString + "," + claveDoc.ToString + ",'" + Replace(NoSociedad, "'", "''") + "','" + Replace(NoProveedor, "'", "''") + "','" + Replace(NoPedido, "'", "''") + "','" + Replace(Moneda, "'", "''") + "','" + Replace(NoEntrada, "'", "''") + "','" + Replace(NoRemision, "'", "''") + "','" + Replace(Nosocio, "'", "''") + "','" + Replace(Centro, "'", "''") + "','" + Replace(IniPerLiq, "'", "''") + "','" + Replace(FinPerLiq, "'", "''") + "'," + _
        "'" + Replace(Retencion1, "'", "''") + "','" + Replace(Retencion2, "'", "''") + "','" + Replace(Retencion3, "'", "''") + "','" + Replace(DatosAdicionales, "'", "''") + "','" + Replace(TipoOperacion, "'", "''") + "'," + TipoDocumento.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(id) from tbladendafemsa"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pVersion As Byte, ByVal pClaveDoc As Byte, ByVal pNoSociedad As String, ByVal pNoProveedor As String, ByVal pNoPedido As String, ByVal pMoneda As String, ByVal pnoEntrada As String, ByVal pnoRemision As String, ByVal pnoSocio As String, ByVal pCentro As String, ByVal pIniPerLiq As String, ByVal pFinPerLiq As String, ByVal pRetencion1 As String, ByVal pRetencion2 As String, ByVal pretencion3 As String, ByVal pDatosAdicionales As String, ByVal pTipoOperacion As String)
        ID = pID
        Version = pVersion
        claveDoc = pClaveDoc
        NoSociedad = pNoSociedad
        NoProveedor = pNoProveedor
        NoPedido = pNoPedido
        Moneda = pMoneda
        NoEntrada = pnoEntrada
        NoRemision = pnoRemision
        Nosocio = pnoSocio
        Centro = pCentro
        IniPerLiq = pIniPerLiq
        FinPerLiq = pFinPerLiq
        Retencion1 = pRetencion1
        Retencion2 = pRetencion2
        Retencion3 = pretencion3
        DatosAdicionales = pDatosAdicionales
        TipoOperacion = pTipoOperacion
        Comm.CommandText = "update tbladendafemsa set version=" + Version.ToString + ",clasedoc=" + claveDoc.ToString + ",nosociedad='" + Replace(NoSociedad, "'", "''") + "',noproveedor='" + Replace(NoProveedor, "'", "''") + "',nopedido='" + Replace(NoPedido, "'", "''") + "',moneda='" + Replace(Moneda, "'", "''") + "',noentrada='" + Replace(NoEntrada, "'", "''") + "',noremision='" + Replace(NoRemision, "'", "''") + "',nosocio='" + Replace(Nosocio, "'", "''") + "',centro='" + Replace(Centro, "'", "''") + "',iniperliq='" + Replace(IniPerLiq, "'", "''") + "',finperliq='" + Replace(FinPerLiq, "'", "''") + "'," + _
        "retencion1='" + Replace(Retencion1, "'", "''") + "',retencion2='" + Replace(Retencion2, "'", "''") + "',retencion3='" + Replace(Retencion3, "'", "''") + "',datosadicionales='" + Replace(DatosAdicionales, "'", "''") + "',tipooperacion='" + Replace(TipoOperacion, "'", "''") + "' where id=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbladendafemsa where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbladendafemsa where idventa=" + idVenta.ToString + " and tipodocumento=" + TipoDocumento.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            ID = DReader("id")
            Version = DReader("version")
            claveDoc = DReader("clasedoc")
            NoSociedad = DReader("nosociedad")
            NoProveedor = DReader("noproveedor")
            NoPedido = DReader("nopedido")
            Moneda = DReader("moneda")
            NoEntrada = DReader("noentrada")
            NoRemision = DReader("noremision")
            Nosocio = DReader("nosocio")
            Centro = DReader("centro")
            IniPerLiq = DReader("iniperliq")
            FinPerLiq = DReader("finperliq")
            Retencion1 = DReader("retencion1")
            Retencion2 = DReader("retencion2")
            Retencion3 = DReader("retencion3")
            DatosAdicionales = DReader("datosadicionales")
            TipoOperacion = DReader("tipooperacion")
            TipoDocumento = DReader("tipodocumento")
        End If
        DReader.Close()
    End Sub
    Public Function CreaXMLCFDI(ByVal pTipoCFD As Byte, ByVal pEmail As String) As String
        Dim XMLdoc As String
        If pTipoCFD = 1 Then
            XMLdoc = "<Addenda>" + vbCrLf + "<Documento>" + vbCrLf + "<FacturaFemsa>" + vbCrLf
        Else

            XMLdoc = "<cfdi:Addenda>" + vbCrLf + "<Documento>" + vbCrLf + "<FacturaFemsa>" + vbCrLf
        End If

        XMLdoc += "<noVersAdd>" + Version.ToString + "</noVersAdd>" + vbCrLf
        XMLdoc += "<claseDoc>" + claveDoc.ToString + "</claseDoc>" + vbCrLf
        XMLdoc += "<noSociedad>" + Replace(Replace(Replace(Replace(Replace(NoSociedad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</noSociedad>" + vbCrLf
        XMLdoc += "<noProveedor>" + Replace(Replace(Replace(Replace(Replace(NoProveedor, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</noProveedor>" + vbCrLf
        XMLdoc += "<noPedido>" + Replace(Replace(Replace(Replace(Replace(NoPedido, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</noPedido>" + vbCrLf
        XMLdoc += "<moneda>" + Replace(Replace(Replace(Replace(Replace(Moneda, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</moneda>" + vbCrLf
        XMLdoc += "<noEntrada>" + Replace(Replace(Replace(Replace(Replace(NoEntrada, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</noEntrada>" + vbCrLf
        XMLdoc += "<noRemision>" + Replace(Replace(Replace(Replace(Replace(NoRemision, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</noRemision>" + vbCrLf
        If Nosocio = "" Then
            XMLdoc += "<noSocio/>" + vbCrLf
        Else
            XMLdoc += "<noSocio>" + Replace(Replace(Replace(Replace(Replace(Nosocio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</noSocio>" + vbCrLf
        End If

        If claveDoc = 2 Then
            XMLdoc += "<Centro>" + Replace(Replace(Replace(Replace(Replace(Centro, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</Centro>" + vbCrLf
            XMLdoc += "<iniPerLiq>" + Replace(Replace(Replace(Replace(Replace(IniPerLiq, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</iniPerLiq>" + vbCrLf
            XMLdoc += "<finPerLiq>" + Replace(Replace(Replace(Replace(Replace(FinPerLiq, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</finPerLiq>" + vbCrLf
        Else
            XMLdoc += "<centroCostos/>" + vbCrLf
            XMLdoc += "<iniPerLiq/>" + vbCrLf
            XMLdoc += "<finPerLiq/>" + vbCrLf
        End If
        If Retencion1 = "" Then
            XMLdoc += "<retencion1/>" + vbCrLf
        Else
            XMLdoc += "<retencion1>" + Replace(Replace(Replace(Replace(Replace(Retencion1, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</retencion1>" + vbCrLf
        End If
        If Retencion2 = "" Then
            XMLdoc += "<retencion2/>" + vbCrLf
        Else
            XMLdoc += "<retencion2>" + Replace(Replace(Replace(Replace(Replace(Retencion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</retencion2>" + vbCrLf
        End If
        If Retencion3 <> "" Then XMLdoc += "<retencion3>" + Replace(Replace(Replace(Replace(Replace(Retencion3, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</retencion3>" + vbCrLf
        If DatosAdicionales <> "" Then XMLdoc += "<datosAdicionales>" + Replace(Replace(Replace(Replace(Replace(DatosAdicionales, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</datosAdicionales>" + vbCrLf
        If TipoOperacion <> "" Then XMLdoc += "<tipoOperacion>" + Replace(Replace(Replace(Replace(Replace(TipoOperacion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</tipoOperacion>" + vbCrLf

        XMLdoc += "<email>" + Replace(Replace(Replace(Replace(Replace(pEmail, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "</email>" + vbCrLf

        If pTipoCFD = 1 Then
            XMLdoc += "</FacturaFemsa>" + vbCrLf + "</Documento>" + vbCrLf + "</Addenda>"
        Else
            XMLdoc += "</FacturaFemsa>" + vbCrLf + "</Documento>" + vbCrLf + "</cfdi:Addenda>"
        End If

        Return XMLdoc
    End Function
    Public Function BuscaAddenda(ByVal pidVenta As Integer, ByVal pTipoDocumento As Byte) As Boolean

        Comm.CommandText = "select ifnull((select id from tbladendafemsa where idventa=" + pidVenta.ToString + " and tipodocumento=" + pTipoDocumento.ToString + "),0)"
        ID = Comm.ExecuteScalar
        If ID = 0 Then
            Return False
        Else
            idVenta = pidVenta
            TipoDocumento = pTipoDocumento
            LlenaDatos()
            Return True
        End If
    End Function
End Class
