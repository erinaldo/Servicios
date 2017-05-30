Public Class dbSucursalesFolios
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public FolioInicial As Integer
    Public FolioFinal As Integer
    Public Serie As String
    Public EsElectronica As Byte
    Public IdCertificado As Integer
    Public NoAprobacion As String
    Public YearAprobacion As String
    Public Activo As Byte
    Public TipoDocumento As Byte
    Public IdSucursal As Integer
    Public Formato As String
    Public Enum TipoDocumentos
        Factura = 1
        NotadeCredito = 2
        Devolucionn = 3
        Remision = 4
        NotasDeCargo = 5
        VentasCotizaciones = 6
        VentasPedidos = 7
        ComprasCotizaciones = 8
        ComprasPedidos = 9
        MovimientosCaja = 10
        Apartados = 11
        Nominas = 12
        Compras = 13
        ComprasRemisiones = 14
        ComprasDevoluciones = 15
        ComprasNotasCredito = 16
        ComprasNotasCargo = 17
        FertilizantesPedidos = 18
        FertilizantesMovimientosSalida = 19
        FertilizantesMovimientosEnvio = 20
        FertilizantesMovimientosTraspaso = 21
        FertilizantesDevolucion = 22
        SemillasLiquidacion = 23
        SemillasComprobante = 24
        Gastos = 25
        RemisionesPorSurtir = 26
        InventarioPedidos = 27
        'MovimientosInventario = 50
    End Enum

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = 0
        FolioFinal = 0
        FolioInicial = 0
        Serie = ""
        EsElectronica = 0
        IdCertificado = 0
        NoAprobacion = ""
        YearAprobacion = ""
        Activo = 0
        TipoDocumento = 0
        IdSucursal = 0
        Formato = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblsucursalesfolios where idfolio=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            FolioFinal = DReader("foliofinal")
            FolioInicial = DReader("folioinicial")
            Serie = DReader("serie")
            EsElectronica = DReader("eselectronica")
            IdCertificado = DReader("idcertificado")
            NoAprobacion = DReader("noaprobacion")
            YearAprobacion = DReader("yearaprobacion")
            Activo = DReader("activo")
            TipoDocumento = DReader("tipodocumento")
            IdSucursal = DReader("idsucursal")
            Formato = DReader("formato")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pFolioInicial As Integer, ByVal pFolioFinal As Integer, ByVal pSerie As String, ByVal pEselectronica As Byte, ByVal pIdCertificado As Integer, ByVal pNoAprobacion As String, ByVal pYearAprobacion As String, ByVal pActivo As Byte, ByVal pTipoDeDocumento As Byte, ByVal pIdSucursal As Integer, ByVal pFormato As String)
        FolioFinal = pFolioFinal
        FolioInicial = pFolioInicial
        Serie = pSerie
        EsElectronica = pEselectronica
        IdCertificado = pIdCertificado
        NoAprobacion = pNoAprobacion
        YearAprobacion = pYearAprobacion
        Activo = pActivo
        TipoDocumento = pTipoDeDocumento
        IdSucursal = pIdSucursal
        Formato = pFormato
        Comm.CommandText = "insert into tblsucursalesfolios(idsucursal,folioinicial,foliofinal,serie,eselectronica,idcertificado,noaprobacion,yearaprobacion,activo,tipodocumento,formato,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + IdSucursal.ToString + "," + FolioInicial.ToString + "," + FolioFinal.ToString + ",'" + Replace(Serie, "'", "''") + "'," + EsElectronica.ToString + "," + IdCertificado.ToString + ",'" + Replace(NoAprobacion, "'", "''") + "','" + Replace(YearAprobacion, "'", "''") + "'," + Activo.ToString + "," + TipoDocumento.ToString + ",'" + Replace(Formato, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFolioInicial As Integer, ByVal pFolioFinal As Integer, ByVal pSerie As String, ByVal pEselectronica As Byte, ByVal pIdCertificado As Integer, ByVal pNoAprobacion As String, ByVal pYearAprobacion As String, ByVal pActivo As Byte, ByVal pTipoDeDocumento As Byte, ByVal pIdSucursal As Integer, ByVal pFormato As String)
        ID = pID
        FolioFinal = pFolioFinal
        FolioInicial = pFolioInicial
        Serie = pSerie
        EsElectronica = pEselectronica
        IdCertificado = pIdCertificado
        NoAprobacion = pNoAprobacion
        YearAprobacion = pYearAprobacion
        Activo = pActivo
        TipoDocumento = pTipoDeDocumento
        Formato = pFormato
        Comm.CommandText = "update tblsucursalesfolios set folioinicial=" + FolioInicial.ToString + ",foliofinal=" + FolioFinal.ToString + ",serie='" + Replace(Serie, "'", "''") + "',eselectronica=" + EsElectronica.ToString + ",idcertificado=" + IdCertificado.ToString + ",noaprobacion='" + Replace(NoAprobacion, "'", "''") + "',yearaprobacion='" + Replace(YearAprobacion, "'", "''") + "',activo=" + Activo.ToString + ",tipodocumento=" + TipoDocumento.ToString + ",formato='" + Replace(Formato, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idfolio=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblsucursalesfolios where idfolio=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdSucursal As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idfolio,case tipodocumento when 1 then 'Factura' when 2 then 'Nota de crédito' when 3 then 'Devolución' when 4 then 'Remision' when 5 then 'Nota de Cargo' when 6 then 'Cotización' when 7 then 'Pedido' when 8 then 'C. Cotización' when 9 then 'C. Pedido' when 10 then 'Movimientos Caja' when 11 then 'Apartados' when 12 then 'Nominas' " + _
        "when 13 then 'Compras' when 14 then 'Compras Remisones' when 15 then 'Compras Devoluciones' when 16 then 'Compras Notas de Crédito' when 17 then 'Compras Notas de Cargo' when 18 then 'Fertilizantes Pedidos' when 19 then 'Fert. Movimientos Salida' when 20 then 'Fert. Movimientos Envío' when 21 then 'Fert. Movimientos Traspaso' when 22 then 'Fert. Movimientos Devolución' when 23 then 'Semillas liquidación' when 24 then 'Semillas Comprobante' when 25 then 'Gastos' when 26 then 'Remisiones por Surtir' " +
        "when 27 then 'Inventario Pedidos' end as tipodoc,serie,folioinicial,foliofinal,if(activo=0,'No','Si') as factivo  from tblsucursalesfolios where idsucursal=" + pIdSucursal.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblsucursalesfolios")
        Return DS.Tables("tblsucursalesfolios").DefaultView
    End Function
    Public Function BuscaFolios(ByVal pidSucursal As Integer, ByVal pTipoDocumento As Byte, ByVal pElectronico As Byte) As Boolean
        If pElectronico = 3 Then pElectronico = 2
        Comm.CommandText = "select ifnull((select idfolio from tblsucursalesfolios where idsucursal=" + pidSucursal.ToString + " and tipodocumento=" + pTipoDocumento.ToString + " and activo=1 and eselectronica=" + pElectronico.ToString + " limit 1),0)"
        ID = Comm.ExecuteScalar
        If ID = 0 Then
            Return False
        Else
            LlenaDatos()
            Return True
        End If
    End Function
End Class
