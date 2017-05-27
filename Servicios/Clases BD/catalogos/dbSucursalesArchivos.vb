Public Class dbSucursalesArchivos

    Dim ConAdo As New Data.OleDb.OleDbConnection
    Dim ComAdo As New Data.OleDb.OleDbCommand
    Public ID As Integer
    Public RutaCer As String
    Public RutaKey As String
    Public IdSucursal As Integer
    Public RutaPFX As String
    Public PassPFX As String
    Public Impresora As String
    Public Activa As Byte
    Public TipoImpresora As Byte
    Public Documentopv As Byte
    Public idCaja As Integer
    Public CodigoImpresora As String
    Public TipoDocumentoImpresora As Integer
    Public ImpresoraDetalles As String
    Public Enum TipoRutas
        FacturaXML = 0
        FacturaPDF = 1
        NotasdeCreditoXML = 2
        NotasdeCreditoPDF = 3
        NotasdeCargoXML = 4
        NotasdeCargoPDF = 5
        DevolucionesXML = 6
        DevolucionesPDF = 7
        CotizacionesPDF = 8
        PedidosPDF = 9
        OtrosPDF = 10
        RemisionesPDF = 11
        NominasXML = 12
        NominasPDF = 13
        FertilizantesPDF = 14
        Validador = 15
        AValidadar = 16
        Validadas = 17
    End Enum
    Public Enum TipoImpresoras
        Normal = 0
        Ticket = 1
    End Enum
    Public Sub New()
        ConAdo.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\rutas.mdb;"
        ConAdo.Open()
        ComAdo.Connection = ConAdo
    End Sub
    Public Function DaRuta(ByVal TipoDocumento As Integer, ByVal pIdSucursal As Integer, ByVal pidEmpresa As Integer, ByVal CerrarDB As Boolean) As String
        Dim Ruta As String
        Dim Hay As Integer
        ComAdo.CommandText = "select count(id) from fondos where documento=" + TipoDocumento.ToString + " and idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            ComAdo.CommandText = "insert into fondos(ruta,documento,idsucursal,idempresa) values(''," + TipoDocumento.ToString + "," + pIdSucursal.ToString + "," + pidEmpresa.ToString + ")"
            ComAdo.ExecuteNonQuery()
        End If
        ComAdo.CommandText = "select ruta from fondos where documento=" + TipoDocumento.ToString + " and idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString
        Ruta = ComAdo.ExecuteScalar
        If CerrarDB Then ConAdo.Close()
        Return Ruta
    End Function
    Public Function DaImpresoraActiva(ByVal pIdSucursal As Integer, ByVal pidEmpresa As Integer, ByVal CerrarDB As Boolean, ByVal pActiva As Byte, ByVal pDocumento As Byte) As String
        Dim Ruta As String
        Dim Hay As Integer
        ComAdo.CommandText = "select count(id) from impresoras where idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and documento=" + pDocumento.ToString
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            ComAdo.CommandText = "insert into impresoras(impresora,tipo,idsucursal,idempresa,activa,documento) values('',0," + pIdSucursal.ToString + "," + pidEmpresa.ToString + ",1," + pDocumento.ToString + ")"
            ComAdo.ExecuteNonQuery()
        End If
        ComAdo.CommandText = "select impresora from impresoras where idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and activa=1 and documento=" + pDocumento.ToString
        Ruta = ComAdo.ExecuteScalar
        ComAdo.CommandText = "select tipo from impresoras where idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and activa=1 and documento=" + pDocumento.ToString
        TipoImpresora = ComAdo.ExecuteScalar
        If CerrarDB Then ConAdo.Close()
        Return Ruta
    End Function
    Public Function DaImpresoraPorTipo(ByVal pIdSucursal As Integer, ByVal pidEmpresa As Integer, ByVal CerrarDB As Boolean, ByVal pTipoImpresora As Byte, ByVal pDocumento As Byte) As String
        'Dim Ruta As String
        Dim Hay As Integer
        ComAdo.CommandText = "select count(id) from impresoras where idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and tipo=" + pTipoImpresora.ToString + " and documento=" + pDocumento.ToString
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            ComAdo.CommandText = "insert into impresoras(impresora,tipo,idsucursal,idempresa,activa,documento) values(''," + pTipoImpresora.ToString + "," + pIdSucursal.ToString + "," + pidEmpresa.ToString + ",0," + pDocumento.ToString + ")"
            ComAdo.ExecuteNonQuery()
        End If
        ComAdo.CommandText = "select impresora from impresoras where idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and tipo=" + pTipoImpresora.ToString + " and documento=" + pDocumento.ToString
        Impresora = ComAdo.ExecuteScalar
        ComAdo.CommandText = "select activa from impresoras where idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and tipo=" + pTipoImpresora.ToString + " and documento=" + pDocumento.ToString
        Activa = ComAdo.ExecuteScalar
        If CerrarDB Then ConAdo.Close()

        Return Impresora
    End Function
    Public Sub GuardaRuta(ByVal TipoDocumento As Integer, ByVal pidSucursal As Integer, ByVal pRuta As String, ByVal pidEmpresa As Integer)
        Dim Hay As Integer
        ComAdo.CommandText = "select count(id) from fondos where documento=" + TipoDocumento.ToString + " and idsucursal=" + pidSucursal.ToString + " and idempresa=" + pidEmpresa.ToString
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            ComAdo.CommandText = "insert into fondos(ruta,documento,idsucursal,idempresa) values('" + Replace(pRuta, "'", "''") + "'," + TipoDocumento.ToString + "," + pidSucursal.ToString + "," + pidEmpresa.ToString + ")"
            ComAdo.ExecuteNonQuery()
        Else
            ComAdo.CommandText = "update fondos set ruta='" + Replace(pRuta, "'", "''") + "' where documento=" + TipoDocumento.ToString + " and idsucursal=" + pidSucursal.ToString + " and idempresa=" + pidEmpresa.ToString
            ComAdo.ExecuteNonQuery()
        End If
    End Sub
    Public Sub GuardaImpresora(ByVal TipoImpresora As Integer, ByVal pidSucursal As Integer, ByVal pImpresora As String, ByVal pidEmpresa As Integer, ByVal pActiva As Byte, ByVal pDocumento As Byte)
        Dim Hay As Integer
        ComAdo.CommandText = "select count(id) from impresoras where tipo=" + TipoImpresora.ToString + " and idsucursal=" + pidSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and documento=" + pDocumento.ToString
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            ComAdo.CommandText = "insert into impresoras(impresora,tipo,idsucursal,idempresa,activa,documento) values('" + Replace(pImpresora, "'", "''") + "'," + TipoImpresora.ToString + "," + pidSucursal.ToString + "," + pidEmpresa.ToString + "," + pActiva.ToString + "," + pDocumento.ToString + ")"
            ComAdo.ExecuteNonQuery()
        Else
            ComAdo.CommandText = "update impresoras set impresora='" + Replace(pImpresora, "'", "''") + "',activa=" + pActiva.ToString + " where tipo=" + TipoImpresora.ToString + " and idsucursal=" + pidSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and documento=" + pDocumento.ToString
            ComAdo.ExecuteNonQuery()
        End If
    End Sub
    Public Function DaRutaCER(ByVal pIdSucursal As Integer, ByVal pidEmpresa As Integer, ByVal CerrarDB As Boolean) As String
        Dim Hay As Integer
        ComAdo.CommandText = "select count(id) from certificados where idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            ComAdo.CommandText = "insert into certificados(rutacer,rutakey,passkey,idsucursal,idempresa) values('','',''," + pIdSucursal.ToString + "," + pidEmpresa.ToString + ")"
            ComAdo.ExecuteNonQuery()
        End If
        ComAdo.CommandText = "select rutacer from certificados where idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString
        RutaCer = ComAdo.ExecuteScalar
        ComAdo.CommandText = "select rutakey from certificados where idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString
        RutaKey = ComAdo.ExecuteScalar

        If CerrarDB Then ConAdo.Close()
        Return RutaCer
    End Function

    Public Sub GuardaRutaCER(ByVal pidSucursal As Integer, ByVal pRutaCer As String, ByVal pRutaKey As String, ByVal pPassKey As String, ByVal pidEmpresa As Integer)
        Dim Hay As Integer
        ComAdo.CommandText = "select count(id) from certificados where idsucursal=" + pidSucursal.ToString + " and idempresa=" + pidEmpresa.ToString
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            ComAdo.CommandText = "insert into certificados(rutacer,rutakey,passkey,idsucursal,idempresa) values('" + Replace(pRutaCer, "'", "''") + "','" + Replace(pRutaKey, "'", "''") + "','" + Replace(pRutaKey, "'", "''") + "'," + pidSucursal.ToString + "," + pidEmpresa.ToString + ")"
            ComAdo.ExecuteNonQuery()
        Else
            ComAdo.CommandText = "update certificados set rutacer='" + Replace(pRutaCer, "'", "''") + "',rutakey='" + Replace(pRutaKey, "'", "''") + "',passkey='" + Replace(pRutaKey, "'", "''") + "' where idsucursal=" + pidSucursal.ToString + " and idempresa=" + pidEmpresa.ToString
            ComAdo.ExecuteNonQuery()
        End If
    End Sub

    Public Function DaRutaArchivos(ByVal pIdSucursal As Integer, ByVal pidEmpresa As Integer, ByVal pTipo As Integer, ByVal CerrarDB As Boolean) As String
        Dim Hay As Integer
        Dim RutaR As String
        ComAdo.CommandText = "select count(id) from rutasarchivos where idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and tipo=" + pTipo.ToString
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            ComAdo.CommandText = "insert into rutasarchivos(idempresa,idsucursal,tipo,ruta) values(" + pidEmpresa.ToString + "," + pIdSucursal.ToString + "," + pTipo.ToString + ",'')"
            ComAdo.ExecuteNonQuery()
        End If
        ComAdo.CommandText = "select ruta from rutasarchivos where idsucursal=" + pIdSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and tipo=" + pTipo.ToString
        RutaR = ComAdo.ExecuteScalar
        If CerrarDB Then ConAdo.Close()
        Return RutaR
    End Function

    Public Sub GuardaRutaArchivos(ByVal pidSucursal As Integer, ByVal pRuta As String, ByVal pidEmpresa As Integer, ByVal pTipo As Integer)
        Dim Hay As Integer
        ComAdo.CommandText = "select count(id) from rutasarchivos where idsucursal=" + pidSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and tipo=" + pTipo.ToString
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            ComAdo.CommandText = "insert into rutasarchivos(idempresa,idsucursal,tipo,ruta) values(" + pidEmpresa.ToString + "," + pidSucursal.ToString + "," + pTipo.ToString + ",'" + Replace(Trim(pRuta), "'", "''") + "')"
            ComAdo.ExecuteNonQuery()
        Else
            ComAdo.CommandText = "update rutasarchivos set ruta='" + Replace(Trim(pRuta), "'", "''") + "' where idsucursal=" + pidSucursal.ToString + " and idempresa=" + pidEmpresa.ToString + " and tipo=" + pTipo.ToString
            ComAdo.ExecuteNonQuery()
        End If
    End Sub
    Public Sub CierraDB()
        ConAdo.Close()
    End Sub

    Public Function DaOpciones(ByVal pidEmpresa As Integer, ByVal CerrarDB As Boolean) As String
        Dim Hay As Integer
        ComAdo.CommandText = "select count(id) from opciones where idempresa=" + pidEmpresa.ToString
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            ComAdo.CommandText = "insert into opciones(rutapfx,passwordpfx,sucursaldefault,idempresa,idcaja,documentopv) values('','',0," + pidEmpresa.ToString + ",0,0)"
            ComAdo.ExecuteNonQuery()
        End If
        Dim DRAdo As Data.OleDb.OleDbDataReader
        ComAdo.CommandText = "select * from opciones where idempresa=" + pidEmpresa.ToString
        DRAdo = ComAdo.ExecuteReader
        If DRAdo.Read Then
            RutaPFX = DRAdo("rutapfx")
            PassPFX = DRAdo("passwordpfx")
            IdSucursal = DRAdo("sucursaldefault")
            idCaja = DRAdo("idcaja")
            Documentopv = DRAdo("documentopv")
        End If
        DRAdo.Close()
        'ComAdo.CommandText = "select rutapfx from opciones where idempresa=" + pidEmpresa.ToString
        'RutaPFX = ComAdo.ExecuteScalar
        'ComAdo.CommandText = "select passwordpfx from opciones where idempresa=" + pidEmpresa.ToString
        'PassPFX = ComAdo.ExecuteScalar
        'ComAdo.CommandText = "select sucursaldefault from opciones where idempresa=" + pidEmpresa.ToString
        'IdSucursal = ComAdo.ExecuteScalar
        If CerrarDB Then ConAdo.Close()
        Return ""
    End Function

    Public Sub GuardaOpciones(ByVal pidSucursal As Integer, ByVal pRutaPFX As String, ByVal pPassPFX As String, ByVal pidEmpresa As Integer, ByVal pidCaja As Integer, ByVal pDocumentoPV As Byte)
        Dim Hay As Integer
        ComAdo.CommandText = "select count(id) from opciones where idempresa=" + pidEmpresa.ToString
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            ComAdo.CommandText = "insert into opciones(rutapfx,passwordpfx,sucursaldefault,idempresa,idcaja,documentopv) values('" + Replace(Trim(pRutaPFX), "'", "''") + "','" + Replace(Trim(pPassPFX), "'", "''") + "'," + pidSucursal.ToString + "," + pidEmpresa.ToString + "," + pidCaja.ToString + "," + pDocumentoPV.ToString + ")"
            ComAdo.ExecuteNonQuery()
        Else
            ComAdo.CommandText = "update opciones set rutapfx='" + Replace(Trim(pRutaPFX), "'", "''") + "',passwordpfx='" + Replace(Trim(pPassPFX), "'", "''") + "',sucursaldefault=" + pidSucursal.ToString + ",idcaja=" + pidCaja.ToString + ",documentopv=" + pDocumentoPV.ToString + " where idempresa=" + pidEmpresa.ToString
            ComAdo.ExecuteNonQuery()
        End If
    End Sub
    Public Function ChecaClaveImpresora(ByVal pClaveImpresora As String, ByVal pTipo As Byte, ByVal pIdSucursal As Integer) As Boolean
        Dim Hay As Integer
        ComAdo.CommandText = "select count(idsucursal) from impresionesdetalles where idsucursal=" + pIdSucursal.ToString + " and tipo=" + pTipo.ToString + " and clave='" + Replace(pClaveImpresora, "'", "''") + "'"
        Hay = ComAdo.ExecuteScalar
        If Hay = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Sub GuardaImpresoraDetalles(ByVal pIdSucursal As Integer, ByVal pImpresora As String, ByVal pClaveImpresora As String, ByVal pTipo As Byte)
        
        ComAdo.CommandText = "insert into impresionesdetalles(idimpresora,idsucursal,impresora,clave,tipo) values(" + pIdSucursal.ToString + ",'" + Replace(pImpresora, "'", "''") + "','" + Replace(pClaveImpresora, "'", "''") + "'," + pTipo.ToString + ")"
        ComAdo.ExecuteNonQuery()
    End Sub
    Public Sub ModificaImpresoraDetalles(ByVal pIdImpresora As Integer, ByVal pClaveImpresora As String, ByVal pImpresora As String)
        ComAdo.CommandText = "update impresionesdetalles set impresora='" + Replace(pImpresora, "'", "''") + "',clave='" + Replace(pClaveImpresora, "'", "''") + "' where idimpresora=" + pIdImpresora.ToString
        ComAdo.ExecuteNonQuery()
    End Sub
    Public Sub EliminarImpresoraDetalles(ByVal pidImpresora As Integer)
        ComAdo.CommandText = "delete from impresionesdetalles where idimpresora=" + pidImpresora.ToString
        ComAdo.ExecuteNonQuery()
    End Sub
    Public Function ConsultaImpresoras(ByVal pidSucursal As Integer, ByVal pTipo As Byte) As DataView
        Dim DS As New DataSet
        ComAdo.CommandText = "select idimpresora,clave,impresora from impresionesdetalles where idsucursal=" + pidSucursal.ToString + " and tipo=" + pTipo.ToString
        Dim DA As New Data.OleDb.OleDbDataAdapter(ComAdo)
        DA.Fill(DS, "tblimp")
        Return DS.Tables("tblimp").DefaultView
    End Function
    Public Sub LlenaDAtosImpresoraDetalles(ByVal pIdImpresora As Integer)
        Dim DRAdo As Data.OleDb.OleDbDataReader
        ComAdo.CommandText = "select * from impresionesdetalles where idimpresora=" + pIdImpresora.ToString
        DRAdo = ComAdo.ExecuteReader
        If DRAdo.Read Then
            CodigoImpresora = DRAdo("clave")
            ImpresoraDetalles = DRAdo("impresora")
            TipoDocumentoImpresora = DRAdo("tipo")
        End If
        DRAdo.Close()
    End Sub
End Class
