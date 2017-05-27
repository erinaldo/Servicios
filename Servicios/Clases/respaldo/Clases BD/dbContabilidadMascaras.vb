Public Class dbContabilidadMascaras
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Modulo As Byte
    Public Tipo As Byte
    Public Titulo As String
    Public Activo As Byte
    Public Estado As Byte
    Public Credito As Byte
    Public Canceladas As Byte
    Public IdSucursal As Integer
    Public IdClasificacion As Integer
    Public TipoPoliza As String
    Public Negativo As Byte
    Public IdCuentaCMov As Integer
    Public CuentaCMov As String
    Public IdCuentaPMov As Integer
    Public CuentaPMov As String
    Public IdCuentaBMov As Integer
    Public CuentaBMov As String
    Public IdCuentaTMov As Integer
    Public CuentaTMov As String

    Public TIVAN As Double
    Public TN As Double
    Public TSI As Double
    Public TIVA As Double
    Public TIVAR As Double
    Public TISR As Double
    Public TIEPS As Double

    Public TIVANC As Double
    Public TNC As Double
    Public TSIC As Double
    Public TIVAC As Double
    Public TIVARC As Double
    Public TIEPSC As Double

    Public TIVAND As Double
    Public TND As Double
    Public TSID As Double
    Public TIVAD As Double
    Public TIVARD As Double
    Public TISRD As Double
    Public TIEPSD As Double

    Public TIVANDC As Double
    Public TNDC As Double
    Public TSIDC As Double
    Public TIVADC As Double
    Public TIVARDC As Double
    Public TIEPSDC As Double

    Public TIVANNC As Double
    Public TNNC As Double
    Public TSINC As Double

    Public TIVANNCA As Double
    Public TNNCA As Double
    Public TSINCA As Double

    Public TIVANNCC As Double
    Public TNNCC As Double
    Public TSINCC As Double

    Public TIVANNCAC As Double
    Public TNNCAC As Double
    Public TSINCAC As Double

    Public TIVANDE As Double
    Public TNDE As Double
    Public TSIDE As Double
    Public TIVADE As Double
    Public TIVARDE As Double
    Public TISRDE As Double
    Public TIEPSDE As Double

    Public TIVANRE As Double
    Public TNRE As Double
    Public TSIRE As Double
    Public TIVARE As Double
    Public TIVARRE As Double
    Public TIEPSRE As Double

    Public TNNO As Double
    Public TNNONeg As Double


    Public TIVANNeg As Double
    Public TNNeg As Double
    Public TSINeg As Double
    Public TIVANeg As Double
    Public TIVARNeg As Double
    Public TISRNeg As Double
    Public TIEPSNeg As Double


    Public TIVANCNeg As Double
    Public TNCNeg As Double
    Public TSICNeg As Double
    Public TIVACNeg As Double
    Public TIVARCNeg As Double
    Public TIEPSCNeg As Double

    Public TIVANDNeg As Double
    Public TNDNeg As Double
    Public TSIDNeg As Double
    Public TIVADNeg As Double
    Public TIVARDNeg As Double
    Public TISRDNeg As Double
    Public TIEPSDNeg As Double

    Public TIVANDCNeg As Double
    Public TNDCNeg As Double
    Public TSIDCNeg As Double
    Public TIVADCNeg As Double
    Public TIVARDCNeg As Double
    Public TIEPSDCNeg As Double

    Public TIVANNCNeg As Double
    Public TNNCNeg As Double
    Public TSINCNeg As Double

    Public TIVANNCANeg As Double
    Public TNNCANeg As Double
    Public TSINCANeg As Double

    Public TIVANNCCNeg As Double
    Public TNNCCNeg As Double
    Public TSINCCNeg As Double

    Public TIVANNCACNeg As Double
    Public TNNCACNeg As Double
    Public TSINCACNeg As Double

    Public TIVANCon As Double
    Public TNCon As Double
    Public TSICon As Double
    Public TIVACon As Double
    Public TIVARCon As Double
    Public TISRCon As Double
    Public TIEPSCon As Double

    Public TIVANCCon As Double
    Public TNCCon As Double
    Public TSICCon As Double
    Public TIVACCon As Double
    Public TIVARCCon As Double
    Public TIEPSCCon As Double

    Public TIVANDCon As Double
    Public TNDCon As Double
    Public TSIDCon As Double
    Public TIVADCon As Double
    Public TIVARDCon As Double
    Public TISRDCon As Double
    Public TIEPSDCon As Double

    Public TIVANDCCon As Double
    Public TNDCCon As Double
    Public TSIDCCon As Double
    Public TIVADCCon As Double
    Public TIVARDCCon As Double
    Public TIEPSDCCon As Double

   

    Public TIVANNegCon As Double
    Public TNNegCon As Double
    Public TSINegCon As Double
    Public TIVANegCon As Double
    Public TIVARNegCon As Double
    Public TISRNegCon As Double
    Public TIEPSNegCon As Double

    Public TIVANCNegCon As Double
    Public TNCNegCon As Double
    Public TSICNegCon As Double
    Public TIVACNegCon As Double
    Public TIVARCNegCon As Double
    Public TIEPSCNegCon As Double

    Public TIVANDNegCon As Double
    Public TNDNegCon As Double
    Public TSIDNegCon As Double
    Public TIVADNegCon As Double
    Public TIVARDNegCon As Double
    Public TISRDNegCon As Double
    Public TIEPSDNegCon As Double

    Public TIVANDCNegCon As Double
    Public TNDCNegCon As Double
    Public TSIDCNegCon As Double
    Public TIVADCNegCon As Double
    Public TIVARDCNegCon As Double
    Public TIEPSDCNegCon As Double

    Public TIVANCr As Double
    Public TNCr As Double
    Public TSICr As Double
    Public TIVACr As Double
    Public TIVARCr As Double
    Public TISRCr As Double
    Public TIEPSCr As Double

    Public TIVANCCr As Double
    Public TNCCr As Double
    Public TSICCr As Double
    Public TIVACCr As Double
    Public TIVARCCr As Double
    Public TIEPSCCr As Double

    Public TIVANDCr As Double
    Public TNDCr As Double
    Public TSIDCr As Double
    Public TIVADCr As Double
    Public TIVARDCr As Double
    Public TISRDCr As Double
    Public TIEPSDCr As Double

    Public TIVANDCCr As Double
    Public TNDCCr As Double
    Public TSIDCCr As Double
    Public TIVADCCr As Double
    Public TIVARDCCr As Double
    Public TIEPSDCCr As Double

    Public TIVANNegCr As Double
    Public TNNegCr As Double
    Public TSINegCr As Double
    Public TIVANegCr As Double
    Public TIVARNegCr As Double
    Public TISRNegCr As Double
    Public TIEPSNegCr As Double

    Public TIVANCNegCr As Double
    Public TNCNegCr As Double
    Public TSICNegCr As Double
    Public TIVACNegCr As Double
    Public TIVARCNegCr As Double
    Public TIEPSCNegCr As Double

    Public TIVANDNegCr As Double
    Public TNDNegCr As Double
    Public TSIDNegCr As Double
    Public TIVADNegCr As Double
    Public TIVARDNegCr As Double
    Public TISRDNegCr As Double
    Public TIEPSDNegCr As Double

    Public TIVANDCNegCr As Double
    Public TNDCNegCr As Double
    Public TSIDCNegCr As Double
    Public TIVADCNegCr As Double
    Public TIVARDCNegCr As Double
    Public TIEPSDCNegCr As Double

    Public TP As Double
    Public TD As Double
    Public TPG As Double
    Public TPE As Double
    Public TDG As Double
    Public TDE As Double
    Public TI As Double
    Public THE As Double
    'Public TDSI As Double

    Public TPNeg As Double
    Public TDNeg As Double
    Public TPGNeg As Double
    Public TPENeg As Double
    Public TDGNeg As Double
    Public TDENeg As Double
    Public TINeg As Double
    Public THENeg As Double
    'Public TDSINeg As Double
    Public TPxC As New Collection
    Public TDxC As New Collection
    Public TPxCNeg As New Collection
    Public TDxCNeg As New Collection
    Public Beneficiario1 As String
    Public Beneficiario2 As String
    Public Beneficiario3 As String
    Public PN1 As Integer
    Public pN2 As Integer
    Public pN3 As Integer
    Public pN4 As Integer
    Public pN5 As Integer

    Public TIVANDep As Double
    Public TNDep As Double
    Public TSIDep As Double
    Public TIVADep As Double
    Public TIVARDep As Double
    Public TISRDep As Double
    Public TIEPSDep As Double

    Public TIVANDepCon As Double
    Public TNDepCon As Double
    Public TSIDepCon As Double
    Public TIVADepCon As Double
    Public TIVARDepCon As Double
    Public TISRDepCon As Double
    Public TIEPSDepCon As Double

    Public TIVANDepCr As Double
    Public TNDepCr As Double
    Public TSIDepCr As Double
    Public TIVADepCr As Double
    Public TIVARDepCr As Double
    Public TISRDepCr As Double
    Public TIEPSDepCr As Double

    Public TIVANRet As Double
    Public TNRet As Double
    Public TSIRet As Double
    Public TIVARet As Double
    Public TIVARRet As Double
    Public TISRRet As Double
    Public TIEPSRet As Double

    Public TIVANRetCon As Double
    Public TNRetCon As Double
    Public TSIRetCon As Double
    Public TIVARetCon As Double
    Public TIVARRetCon As Double
    Public TISRRetCon As Double
    Public TIEPSRetCon As Double

    Public TIVANRetCr As Double
    Public TNRetCr As Double
    Public TSIRetCr As Double
    Public TIVARetCr As Double
    Public TIVARRetCr As Double
    Public TISRRetCr As Double
    Public TIEPSRetCr As Double

    Public TNSLDep As Double
    Public TNSLRet As Double
    Public TIVASLDep As Double
    Public TIVASLRet As Double
    Public TSISLDep As Double
    Public TSISLRet As Double

    Public TPxB As Collection
    Public UUIDs As New Collection
    Public UUIDsNeg As New Collection
    Public UUIDsDev As New Collection
    Public UUIDsDevNeg As New Collection
    Public UUIDsNCr As New Collection
    Public UUIDsNCrNeg As New Collection
    Public UUIDsNCA As New Collection
    Public UUIDsNCANeg As New Collection
    Public UUIDsNOM As New Collection
    Public UUIDsNOMNeg As New Collection
    Public UUIDsC As New Collection
    Public UUIDsCNeg As New Collection
    Public UUIDsDevC As New Collection
    Public UUIDsDevCNeg As New Collection
    Public UUIDsNCrC As New Collection
    Public UUIDsNCrCNeg As New Collection
    Public UUIDsNCAC As New Collection
    Public UUIDsNCACNeg As New Collection

    Public UUIDsDep As New Collection
    Public UUIDsRet As New Collection
    Public UUIDsRetSL As New Collection
    Public UUIDsRetCr As New Collection
    Public UUIDsRetCon As New Collection
    Public UUIDsDepSL As New Collection
    Public UUIDsDepCr As New Collection
    Public UUIDsDepCon As New Collection

    Public TIVAxPagoRet As New Collection
    Public TIVARetxPagoRet As New Collection
    Public TIEPSxPagoRet As New Collection

    Public TIVAxPagoRetCr As New Collection
    Public TIVARetxPagoRetCr As New Collection
    Public TIEPSxPagoRetCr As New Collection

    Public TIVAxPagoRetCon As New Collection
    Public TIVARetxPagoRetCon As New Collection
    Public TIEPSxPagoRetCon As New Collection

    Public TIVAxPagoRetSL As New Collection
    Public TIVARetxPagoRetSL As New Collection
    Public TIEPSxPagoRetSL As New Collection
    Public RetirosChequeTrans As New Collection

    Public Structure stIvaInfo
        Dim TotalIva As Double
        Dim TotalIvaRet As Double
        Dim TotalIeps As Double
        Dim Ivapor As Double
        Dim Ivaretpor As Double
        Dim IepsPor As Double
        Dim IdProveedor As Integer
        Dim NombreProveedor As String
        Dim ValorActos As Double
        Dim Cuenta As String
        Dim IdCuenta As String
        Dim Concepto As String
    End Structure
    Public ChequesTrans As New Collection
    Public Structure stUuids
        Dim Uuid As String
        Dim Fecha As String
        Dim RFC As String
        Dim Monto As String
        Dim IdMoneda As String
        Dim TipodeCambio As Double
        Dim IdDocumento As Integer
    End Structure
    Public Structure stChequeTrans
        Dim NumeroCheque As String
        Dim IdBancoOrigen As Integer
        Dim NoCuentaOrigen As String
        Dim IdBancoDestino As Integer
        Dim NoCuentaDestino As String
        Dim Fecha As String
        Dim Monto As Double
        Dim Beneficiario As String
        Dim RFC As String
        Dim BancoEx As String
        Dim IdMoneda As Integer
        Dim TipodeCambio As Double
        Dim BancoOrigenEx As String
        Dim BancoDestinoEx As String
    End Structure
    Public Structure stDepositos
        Dim Cantidad As Double
        Dim IdDeposito As Integer
        Dim Idcuenta As Integer
        Dim Concepto As String
        Dim Iva As Double
        Dim Ieps As Double
        Dim Retendido As Double
        Dim ISR As Double
        Dim Cuenta As String
        Dim IvaPor As Double
        Dim IVaRetPor As Double
        Dim IEpsPor As Double
        Dim IdProveedor As Integer
        Dim Folio As String
        Dim NombreProveedor As String
        Dim Uuid As stUuids
        Dim ChequeTrans As stChequeTrans
    End Structure
    

    Public DepositosLista As New Collection
    Public DepositosListaCon As New Collection
    Public DepositosListaCr As New Collection
    Public PagosLista As New Collection
    Public PagosListaCon As New Collection
    Public PagosListaCr As New Collection

    Public DepositosListaRet As New Collection
    Public DepositosListaRetCon As New Collection
    Public DepositosListaRetCr As New Collection
    Public PagosListaRet As New Collection
    Public PagosListaRetCon As New Collection
    Public PagosListaRetCr As New Collection

    Public FacturasLista As New Collection
    Public FacturasListaNeg As New Collection
    Public FacturasListaCon As New Collection
    Public FacturasListaNegCon As New Collection
    Public FacturasListaCr As New Collection
    Public FacturasListaNegCr As New Collection

    Public FacturasListaC As New Collection
    Public FacturasListaCNeg As New Collection
    Public FacturasListaCCon As New Collection
    Public FacturasListaCNegCon As New Collection
    Public FacturasListaCCr As New Collection
    Public FacturasListaCNegCr As New Collection

    Public FacturasListaD As New Collection
    Public FacturasListaDNeg As New Collection
    Public FacturasListaDCon As New Collection
    Public FacturasListaDNegCon As New Collection
    Public FacturasListaDCr As New Collection
    Public FacturasListaDNegCr As New Collection

    Public FacturasListaDC As New Collection
    Public FacturasListaDCNeg As New Collection
    Public FacturasListaDCCon As New Collection
    Public FacturasListaDCNegCon As New Collection
    Public FacturasListaDCCr As New Collection
    Public FacturasListaDCNegCr As New Collection

    Public FacturasListaNC As New Collection
    Public FacturasListaNCNeg As New Collection
    Public FacturasListaNCA As New Collection
    Public FacturasListaNCANeg As New Collection
    Public FacturasListaNCC As New Collection
    Public FacturasListaNCCNeg As New Collection
    Public FacturasListaNCAC As New Collection
    Public FacturasListaNCACNeg As New Collection


    Public Structure conceptosNomina
        Dim concepto As String
        Dim importe As Double
        Dim idcuenta As Integer
        Dim Cuenta As String
    End Structure
    Dim ConceptoNomina As conceptosNomina
    Public StrAfectadas As String
    Public strAfectadasNeg As String
    Public Beneficiario As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Tipo = 0
        Modulo = 0
        Titulo = ""
        Activo = 0
        Estado = 0
        Credito = 0
        Canceladas = 0
        IdSucursal = 0
        IdClasificacion = 0
        TipoPoliza = ""
        Negativo = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub InicializaVariables()
        TIVAN = 0
        TN = 0
        TSI = 0
        TIVA = 0
        TIVAR = 0
        TISR = 0
        TIEPS = 0

        TIVANC = 0
        TNC = 0
        TSIC = 0
        TIVAC = 0
        TIVARC = 0
        TIEPSC = 0

        TIVAND = 0
        TND = 0
        TSID = 0
        TIVAD = 0
        TIVARD = 0
        TISRD = 0
        TIEPSD = 0

        TIVANDC = 0
        TNDC = 0
        TSIDC = 0
        TIVADC = 0
        TIVARDC = 0
        TIEPSDC = 0

        TIVANNC = 0
        TNNC = 0
        TSINC = 0

        TIVANNCA = 0
        TNNCA = 0
        TSINCA = 0

        TIVANNCC = 0
        TNNCC = 0
        TSINCC = 0

        TIVANNCAC = 0
        TNNCAC = 0
        TSINCAC = 0

        TIVANDE = 0
        TNDE = 0
        TSIDE = 0
        TIVADE = 0
        TIVARDE = 0
        TISRDE = 0
        TIEPSDE = 0

        TIVANRE = 0
        TNRE = 0
        TSIRE = 0
        TIVARE = 0
        TIVARRE = 0
        TIEPSRE = 0

        TNNO = 0
        TNNONeg = 0


        TIVANNeg = 0
        TNNeg = 0
        TSINeg = 0
        TIVANeg = 0
        TIVARNeg = 0
        TISRNeg = 0
        TIEPSNeg = 0


        TIVANCNeg = 0
        TNCNeg = 0
        TSICNeg = 0
        TIVACNeg = 0
        TIVARCNeg = 0
        TIEPSCNeg = 0

        TIVANDNeg = 0
        TNDNeg = 0
        TSIDNeg = 0
        TIVADNeg = 0
        TIVARDNeg = 0
        TISRDNeg = 0
        TIEPSDNeg = 0

        TIVANDCNeg = 0
        TNDCNeg = 0
        TSIDCNeg = 0
        TIVADCNeg = 0
        TIVARDCNeg = 0
        TIEPSDCNeg = 0

        TIVANNCNeg = 0
        TNNCNeg = 0
        TSINCNeg = 0

        TIVANNCANeg = 0
        TNNCANeg = 0
        TSINCANeg = 0

        TIVANNCCNeg = 0
        TNNCCNeg = 0
        TSINCCNeg = 0

        TIVANNCACNeg = 0
        TNNCACNeg = 0
        TSINCACNeg = 0

        TIVANCon = 0
        TNCon = 0
        TSICon = 0
        TIVACon = 0
        TIVARCon = 0
        TISRCon = 0
        TIEPSCon = 0

        TIVANCCon = 0
        TNCCon = 0
        TSICCon = 0
        TIVACCon = 0
        TIVARCCon = 0
        TIEPSCCon = 0

        TIVANDCon = 0
        TNDCon = 0
        TSIDCon = 0
        TIVADCon = 0
        TIVARDCon = 0
        TISRDCon = 0
        TIEPSDCon = 0

        TIVANDCCon = 0
        TNDCCon = 0
        TSIDCCon = 0
        TIVADCCon = 0
        TIVARDCCon = 0
        TIEPSDCCon = 0



        TIVANNegCon = 0
        TNNegCon = 0
        TSINegCon = 0
        TIVANegCon = 0
        TIVARNegCon = 0
        TISRNegCon = 0
        TIEPSNegCon = 0

        TIVANCNegCon = 0
        TNCNegCon = 0
        TSICNegCon = 0
        TIVACNegCon = 0
        TIVARCNegCon = 0
        TIEPSCNegCon = 0

        TIVANDNegCon = 0
        TNDNegCon = 0
        TSIDNegCon = 0
        TIVADNegCon = 0
        TIVARDNegCon = 0
        TISRDNegCon = 0
        TIEPSDNegCon = 0

        TIVANDCNegCon = 0
        TNDCNegCon = 0
        TSIDCNegCon = 0
        TIVADCNegCon = 0
        TIVARDCNegCon = 0
        TIEPSDCNegCon = 0

        TIVANCr = 0
        TNCr = 0
        TSICr = 0
        TIVACr = 0
        TIVARCr = 0
        TISRCr = 0
        TIEPSCr = 0

        TIVANCCr = 0
        TNCCr = 0
        TSICCr = 0
        TIVACCr = 0
        TIVARCCr = 0
        TIEPSCCr = 0

        TIVANDCr = 0
        TNDCr = 0
        TSIDCr = 0
        TIVADCr = 0
        TIVARDCr = 0
        TISRDCr = 0
        TIEPSDCr = 0

        TIVANDCCr = 0
        TNDCCr = 0
        TSIDCCr = 0
        TIVADCCr = 0
        TIVARDCCr = 0
        TIEPSDCCr = 0

        TIVANNegCr = 0
        TNNegCr = 0
        TSINegCr = 0
        TIVANegCr = 0
        TIVARNegCr = 0
        TISRNegCr = 0
        TIEPSNegCr = 0

        TIVANCNegCr = 0
        TNCNegCr = 0
        TSICNegCr = 0
        TIVACNegCr = 0
        TIVARCNegCr = 0
        TIEPSCNegCr = 0

        TIVANDNegCr = 0
        TNDNegCr = 0
        TSIDNegCr = 0
        TIVADNegCr = 0
        TIVARDNegCr = 0
        TISRDNegCr = 0
        TIEPSDNegCr = 0

        TIVANDCNegCr = 0
        TNDCNegCr = 0
        TSIDCNegCr = 0
        TIVADCNegCr = 0
        TIVARDCNegCr = 0
        TIEPSDCNegCr = 0

        TP = 0
        TD = 0
        TPG = 0
        TPE = 0
        TDG = 0
        TDE = 0
        TI = 0
        THE = 0
        ' TDSI

        TPNeg = 0
        TDNeg = 0
        TPGNeg = 0
        TPENeg = 0
        TDGNeg = 0
        TDENeg = 0
        TINeg = 0
        THENeg = 0
        ' TDSINeg

        Beneficiario1 = ""
        Beneficiario2 = ""
        Beneficiario3 = ""


        TIVANDep = 0
        TNDep = 0
        TSIDep = 0
        TIVADep = 0
        TIVARDep = 0
        TISRDep = 0
        TIEPSDep = 0

        TIVANDepCon = 0
        TNDepCon = 0
        TSIDepCon = 0
        TIVADepCon = 0
        TIVARDepCon = 0
        TISRDepCon = 0
        TIEPSDepCon = 0

        TIVANDepCr = 0
        TNDepCr = 0
        TSIDepCr = 0
        TIVADepCr = 0
        TIVARDepCr = 0
        TISRDepCr = 0
        TIEPSDepCr = 0

        TIVANRet = 0
        TNRet = 0
        TSIRet = 0
        TIVARet = 0
        TIVARRet = 0
        TISRRet = 0
        TIEPSRet = 0

        TIVANRetCon = 0
        TNRetCon = 0
        TSIRetCon = 0
        TIVARetCon = 0
        TIVARRetCon = 0
        TISRRetCon = 0
        TIEPSRetCon = 0

        TIVANRetCr = 0
        TNRetCr = 0
        TSIRetCr = 0
        TIVARetCr = 0
        TIVARRetCr = 0
        TISRRetCr = 0
        TIEPSRetCr = 0

        TNSLDep = 0
        TNSLRet = 0
        TIVASLDep = 0
        TIVASLRet = 0
        TSISLDep = 0
        TSISLRet = 0
        StrAfectadas = ""
        strAfectadasNeg = ""
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcontabilidadmascaras where idmascara=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Titulo = DReader("titulo")
            Modulo = DReader("modulo")
            Activo = DReader("activo")
            Tipo = DReader("tipo")
            Estado = DReader("estado")
            Credito = DReader("credito")
            Canceladas = DReader("canceladas")
            IdSucursal = DReader("idsucursal")
            IdClasificacion = DReader("idclasificacion")
            TipoPoliza = DReader("tipopoliza")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(pModulo As Byte, pTipo As Byte, pTitulo As String, pActivo As Byte, pCredito As Byte, pCanceladas As Byte, pidSucursal As Integer, pidCasificacion As Integer, pTipoPoliza As String)
        Comm.CommandText = "insert into tblcontabilidadmascaras(titulo,modulo,tipo,activo,estado,credito,canceladas,idsucursal,idclasificacion,tipopoliza) values('" + Replace(pTitulo.Trim, "'", "''") + "'," + pModulo.ToString + "," + pTipo.ToString + "," + pActivo.ToString + ",1," + pCredito.ToString + "," + pCanceladas.ToString + "," + pidSucursal.ToString + "," + pidCasificacion.ToString + ",'" + pTipoPoliza + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idmascara) from tblcontabilidadmascaras"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, pTitulo As String, pActivo As Byte, pEstado As Byte, pCredito As Byte, pCanceladas As Byte, pidSucursal As Integer, pidCasificacion As Integer, pTipoPoliza As String, pTipo As Byte, pModulo As Integer)
        ID = pID
        Comm.CommandText = "update tblcontabilidadmascaras set titulo='" + Replace(pTitulo.Trim, "'", "''") + "',activo=" + pActivo.ToString + ",estado=" + pEstado.ToString + ",credito=" + pCredito.ToString + ",canceladas=" + pCanceladas.ToString + ",idsucursal=" + pidSucursal.ToString + ",idclasificacion=" + pidCasificacion.ToString + ",tipopoliza='" + pTipoPoliza + "',tipo=" + pTipo.ToString + ",modulo=" + pModulo.ToString + " where idmascara=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcontabilidadmascaras where idmascara=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pTitulo As String, pModulo As Byte, pTipo As Byte, pEstado As Byte) As DataView
        Dim DS As New DataSet
        'ComboBox6.Items.Add("Ventas")
        'ComboBox6.Items.Add("Compras")
        'ComboBox6.Items.Add("Ventas Devoluciones")
        'ComboBox6.Items.Add("Compras Devoluciones")
        'ComboBox6.Items.Add("Ventas Pagos")
        'ComboBox6.Items.Add("Compras Pagos")

        'ComboBox4.Items.Add("Todos")
        'ComboBox4.Items.Add("Mensual")
        'ComboBox4.Items.Add("Semanal")
        'ComboBox4.Items.Add("Diario")
        'ComboBox4.Items.Add("Por Movimiento")

        Comm.CommandText = "select idmascara,titulo,case modulo when 0 then 'Ventas' when 1 then 'Compras' when 2 then 'Ventas Dev.' when 3 then 'Compras Dev.' when 4 then 'Depósitos' when 5 then 'Pagos' when 6 then 'N. CR. Ventas' when 7 then 'N. CR. Compras' when 8 then 'NC Ventas' when 9 then 'NC Compras' when 10 then 'Nómina' end as Modulo," + _
            "case tipo when 0 then 'Mensual' when 1 then 'Semanal' when 2 then 'Diario' when 3 then 'Por Movimiento' WHEN 4 then 'Por Rango' end as Tipo,if(activo=0,'','Activo') Estado from tblcontabilidadmascaras where titulo like '%" + Replace(pTitulo.Trim, "'", "''") + "%'"
        If pModulo > 0 Then
            pModulo = pModulo - 1
            Comm.CommandText += " and modulo=" + pModulo.ToString
        End If
        If pTipo > 0 Then
            pTipo = pTipo - 1
            Comm.CommandText += " and tipo=" + pTipo.ToString
        End If
        If pEstado > 0 Then
            pEstado = pEstado - 1
            Comm.CommandText += " and activo=" + pEstado.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmascaras")
        Return DS.Tables("tblmascaras").DefaultView
    End Function
    Public Function ChecaTituloRepetido(ByVal pTitulo As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(titulo) from tblcontabilidadmascaras where titulo='" + Replace(pTitulo.Trim, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    
   
    'Public Function DaNivel(pidCuenta As Integer) As Integer
    '    Comm.CommandText = "select ifnull((select nivel from tblccontables where idCContable=" + pidCuenta.ToString + " limit 1),0)"
    '    Return Comm.ExecuteScalar()
    'End Function

    Public Sub LlenaVariables(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        'ComboBox2.Items.Add("Ventas") '0
        'ComboBox2.Items.Add("Compras") '1
        'ComboBox2.Items.Add("Ventas Devoluciones") '2
        'ComboBox2.Items.Add("Compras Devoluciones") '3
        'ComboBox2.Items.Add("Ventas Pagos") '4
        'ComboBox2.Items.Add("Compras Pagos") '5
        'ComboBox2.Items.Add("Notas de crédito ventas") '6
        'ComboBox2.Items.Add("Notas de crédito compras") '7
        'ComboBox2.Items.Add("Notas de cargo ventas") '8
        'ComboBox2.Items.Add("Notas de cargo compras") '9
        'ComboBox2.Items.Add("Nómina") '10
        
        Select Case pModulo
            Case 0
                LlenaVariablesFacturas(pModulo, pFecha1, pFecha2, pIdMovimiento, pCredito, pIdSucursal)
            Case 1
                LlenaVariablesCompras(pModulo, pFecha1, pFecha2, pIdMovimiento, pCredito, pIdSucursal)
            Case 2
                LlenaVariablesDevoluciones(pModulo, pFecha1, pFecha2, pIdMovimiento, pCredito, pIdSucursal)
            Case 3
                LlenaVariablesDevolucionesC(pModulo, pFecha1, pFecha2, pIdMovimiento, pCredito, pIdSucursal)
            Case 4
                LlenaVariablesDepositos(pModulo, pFecha1, pFecha2, pIdMovimiento, pCredito, pIdSucursal)
            Case 5
                LlenaVariablesRetiros(pModulo, pFecha1, pFecha2, pIdMovimiento, pCredito, pIdSucursal)
            Case 6
                LlenaVariablesNotasdeCredito(pModulo, pFecha1, pFecha2, pIdMovimiento, pCredito, pIdSucursal)
            Case 7
                LlenaVariablesNotasdeCreditoCompras(pModulo, pFecha1, pFecha2, pIdMovimiento, pCredito, pIdSucursal)
            Case 8
                LlenaVariablesNotasdeCargo(pModulo, pFecha1, pFecha2, pIdMovimiento, pCredito, pIdSucursal)
            Case 9
                LlenaVariablesNotasdeCargoCompras(pModulo, pFecha1, pFecha2, pIdMovimiento, pCredito, pIdSucursal)
            Case 10
                LlenaVariablesNominas(pModulo, pFecha1, pFecha2, pIdMovimiento, pCredito, pIdSucursal)
        End Select
    End Sub
    Private Sub LlenaVariablesCompras(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        '****************************************COMPRAS***********
        '***********************************************************************
        '********************************************************************
        '***************************************************************
        '***************************Contado
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio,2),round(tblcomprasdetalles.precio*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=3 "
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        ' End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSICCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.iva/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVACCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.ivaretenido/100,2),round(precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompra.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARCCon = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.ieps/100,2),round(precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIEPSCCon = Comm.ExecuteScalar

        ''Credito
        '***********************
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio,2),round(tblcomprasdetalles.precio*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=3 "
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSICCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.iva/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVACCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.ivaretenido/100,2),round(precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompra.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARCCr = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.ieps/100,2),round(precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        ' Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIEPSCCr = Comm.ExecuteScalar

        TIVANCCon = TIVACCon + TIEPSCCon - TIVARCCon
        TNCCon = TSICCon + TIVANCCon
        TIVANCCr = TIVACCr + TIEPSCCr - TIVARCCr
        TNCCr = TSICCr + TIVANCCr
        TIVANC = TIVANCCon + TIVANCCr
        TSIC = TSICCon + TSICCr
        TIVAC = TIVACCon + TIVACCr
        TIEPSC = TIEPSCCon + TIEPSCCr
        TIVARC = TIVARCCon + TIVARCCr
        TNC = TNCCon + TNCCr

        'Llena variables Canceladas
        'contado
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio,2),round(precio*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TSICNegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.iva/100,2),round(precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVACNegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.ivaretenido/100,2),round(precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVARCNegCon = Comm.ExecuteScalar
        
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.ieps/100,2),round(precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIEPSCNegCon = Comm.ExecuteScalar

        'Llena variables Canceladas
        'credito
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio,2),round(precio*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TSICNegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.iva/100,2),round(precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVACNegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.ivaretenido/100,2),round(precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVARCNegCr = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(if(tblcomprasdetalles.idmoneda=2,round(precio*tblcomprasdetalles.ieps/100,2),round(precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio,2))) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIEPSCNegCr = Comm.ExecuteScalar

        TIVANCNegCon = TIVACNegCon + TIEPSCNegCon - TIVARCNegCon
        TNCNegCon = TSICNegCon + TIVANCNegCon
        TIVANCNegCr = TIVACNegCr + TIEPSCNegCr - TIVARCNegCr
        TNCNegCr = TSICNegCr + TIVANCNegCr
        TIVANCNeg = TIVANCNegCon + TIVANCNegCr
        TSICNeg = TSICNegCon + TSICNegCr
        TIEPSCNeg = TIEPSCNegCon + TIEPSCNegCr
        TIVARCNeg = TIVARCNegCon + TIVARCNegCr
        TNCNeg = TNCNegCon + TNCNegCr


        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        '***************Por Compra Contado***********
        Comm.CommandText = "select ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio,2),round(tblcomprasdetalles.precio*tblcompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio,2))),0) as totalieps," +
            " tblcompras.idcompra,ifnull(concat(tblcompras.referencia,' ',tblcompras.serie),'*Nohay') as serie,tblcompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tblcompras.foliocfdi,tblcompras.idmoneda,tblcompras.tipodecambio,tblcompras.fecha,round(tblcompras.totalapagar,2) totalapagar" +
            " from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor where tblcompras.estado=3 "
        
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblcompras.fecha>='" + pFecha1 + "' and tblcompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblcompras.idcompra order by tblcompras.fecha,tblcompras.serie,tblcompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaCCon.Clear()
        Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "COMPRA: " + DR("serie") + Format(DR("folioi"), "0000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idcompra")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaCCon.Add(F)
            End If
        End While
        DR.Close()

        '***************Por Compra Crédito***********
        Comm.CommandText = "select ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio,2),round(tblcomprasdetalles.precio*tblcompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio,2))),0) as totalieps," +
            " tblcompras.idcompra,ifnull(concat(tblcompras.referencia,' ',tblcompras.serie),'*Nohay') as serie,tblcompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tblcompras.foliocfdi,tblcompras.idmoneda,tblcompras.tipodecambio,tblcompras.fecha,round(tblcompras.totalapagar,2) totalapagar" +
            " from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor where tblcompras.estado=3 "

        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblcompras.fecha>='" + pFecha1 + "' and tblcompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblcompras.idcompra order by tblcompras.fecha,tblcompras.serie,tblcompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaCCr.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "COMPRA: " + DR("serie") + Format(DR("folioi"), "0000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idcompra")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0
                F.NombreProveedor = ""
                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                FacturasListaCCr.Add(F)
            End If
        End While
        DR.Close()


        '***************Por Compra Contado Cancelado***********
        Comm.CommandText = "select ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio,2),round(tblcomprasdetalles.precio*tblcompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio,2))),0) as totalieps," +
            " tblcompras.idcompra,ifnull(concat(tblcompras.referencia,' ',tblcompras.serie),'*Nohay') as serie,tblcompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tblcompras.foliocfdi,tblcompras.idmoneda,tblcompras.tipodecambio,tblcompras.fecha,round(tblcompras.totalapagar,2) totalapagar" +
            " from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor where tblcompras.estado=4 "

        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblcompras.idcompra order by tblcompras.fecha,tblcompras.serie,tblcompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaCNegCon.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "COMPRA: " + DR("serie") + Format(DR("folio"), "0000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idcompra")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaCNegCon.Add(F)
            End If
        End While
        DR.Close()

        '***************Por Compra Crédito cancelado***********
        Comm.CommandText = "select ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio,2),round(tblcomprasdetalles.precio*tblcompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio,2))),0) as totalieps," +
            " tblcompras.idcompra,ifnull(concat(tblcompras.referencia,' ',tblcompras.serie),'*Nohay') as serie,tblcompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tblcompras.foliocfdi,tblcompras.idmoneda,tblcompras.tipodecambio,tblcompras.fecha,round(tblcompras.totalapagar,2) totalapagar" +
            " from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor where tblcompras.estado=4 "

        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblcompras.idcompra order by tblcompras.fecha,tblcompras.serie,tblcompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaCNegCr.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "COMPRA: " + DR("serie") + Format(DR("folioi"), "0000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idcompra")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaCNegCr.Add(F)
            End If
        End While
        DR.Close()

        '***************Por Compra todas***********
        Comm.CommandText = "select ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio,2),round(tblcomprasdetalles.precio*tblcompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio,2))),0) as totalieps," +
            " tblcompras.idcompra,ifnull(concat(tblcompras.referencia,' ',tblcompras.serie),'*Nohay') as serie,tblcompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tblcompras.foliocfdi,tblcompras.idmoneda,tblcompras.tipodecambio,tblcompras.fecha,round(tblcompras.totalapagar,2) totalapagar" +
            " from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor where tblcompras.estado=3 "

        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblcompras.fecha>='" + pFecha1 + "' and tblcompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblcompras.idcompra order by tblcompras.fecha,tblcompras.serie,tblcompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaC.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "COMPRA: " + DR("serie") + Format(DR("folioi"), "0000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idcompra")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaC.Add(F)
            End If
        End While
        DR.Close()

        '***************Por Compra todas cancelado***********
        Comm.CommandText = "select ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio,2),round(tblcomprasdetalles.precio*tblcompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio,2))),0) as totalieps," +
            " tblcompras.idcompra,ifnull(concat(tblcompras.referencia,' ',tblcompras.serie),'*Nohay') as serie,tblcompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tblcompras.foliocfdi,tblcompras.idmoneda,tblcompras.tipodecambio,tblcompras.fecha,round(tblcompras.totalapagar,2) totalapagar" +
            " from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor where tblcompras.estado=4 "

        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblcompras.idcompra order by tblcompras.fecha,tblcompras.serie,tblcompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaCNeg.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "COMPRA: " + DR("serie") + Format(DR("folioi"), "0000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idcompra")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaCNeg.Add(F)
            End If
        End While
        DR.Close()

        DocumentosProcesados(pModulo, pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)

    End Sub
    Private Sub LlenaVariablesNominas(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        '****************************************NÓMINAS***********
        '***********************************************************************
        '********************************************************************
        '***************************************************************
        '***************************
        Comm.CommandText = "select ifnull((select sum(round(importegravado,2)) from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina where tblnominadetalles.tipodetalle=0 and estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TPG = Comm.ExecuteScalar
        
        Comm.CommandText = "select ifnull((select sum(round(importeexento,2)) from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina where tblnominadetalles.tipodetalle=0 and estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TPE = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(round(importegravado,2)) from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina where tblnominadetalles.tipodetalle=1 and estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TDG = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(round(importeexento,2)) from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina where tblnominadetalles.tipodetalle=1 and estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TDE = Comm.ExecuteScalar


        Comm.CommandText = "select ifnull((select sum(round(importepagado,2)) from tblnominas inner join tblnominahorasextra on tblnominas.idnomina=tblnominahorasextra.idnomina where estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        THE = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(round(descuento,2)) from tblnominas inner join tblnominaincapacidades on tblnominas.idnomina=tblnominaincapacidades.idnomina where estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TI = Comm.ExecuteScalar


        Comm.CommandText = "select sum(round(importeexento+importegravado,2)) as importe,tblpercepciones.descripcion,tblpercepciones.idcuenta,concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN5.ToString + ",'0'))) as Cuenta from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina inner join tblpercepciones on tblnominadetalles.tipo=tblpercepciones.idpercepcion inner join tblccontables c on c.idccontable=tblpercepciones.idcuenta where tblnominadetalles.tipodetalle=0 and estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnominas.fecha>='" + pFecha1 + "' and tblnominas.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnominadetalles.tipo order by Cuenta"
        Dim Reader As MySql.Data.MySqlClient.MySqlDataReader

        Reader = Comm.ExecuteReader
        TPxC.Clear()
        While Reader.Read
            ConceptoNomina.concepto = Reader("descripcion")
            ConceptoNomina.importe = Reader("importe")
            ConceptoNomina.idcuenta = Reader("idcuenta")
            ConceptoNomina.Cuenta = Reader("cuenta")
            TPxC.Add(ConceptoNomina)
        End While
        Reader.Close()


        Comm.CommandText = "select sum(round(importeexento+importegravado,2)) as importe,tbldeducciones.descripcion,tbldeducciones.idcuenta,concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN5.ToString + ",'0'))) as Cuenta from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina inner join tbldeducciones on tblnominadetalles.tipo=tbldeducciones.iddeduccion inner join tblccontables c on c.idccontable=tbldeducciones.idcuenta where tblnominadetalles.tipodetalle=1 and estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnominas.fecha>='" + pFecha1 + "' and tblnominas.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnominadetalles.tipo order by Cuenta"
        'Dim Reader As MySql.Data.MySqlClient.MySqlDataReader

        Reader = Comm.ExecuteReader
        TDxC.Clear()
        While Reader.Read
            ConceptoNomina.concepto = Reader("descripcion")
            ConceptoNomina.importe = Reader("importe")
            ConceptoNomina.idcuenta = Reader("idcuenta")
            ConceptoNomina.Cuenta = Reader("cuenta")
            TDxC.Add(ConceptoNomina)
        End While
        Reader.Close()

        TP = TPG + TPE
        TD = TDG + TDE
        TNNO = TP - TD
        'TDSI = TD - TISR

        'Llena variables Canceladas
        

        Comm.CommandText = "select ifnull((select sum(round(importegravado,2)) from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina where tblnominadetalles.tipodetalle=0 and estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnominas.fechacancelado>='" + pFecha1 + "' and tblnominas.fechacancelado<='" + pFecha2 + "' and tblnominas.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TPGNeg = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(round(importeexento,2)) from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina where tblnominadetalles.tipodetalle=0 and estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnominas.fechacancelado>='" + pFecha1 + "' and tblnominas.fechacancelado<='" + pFecha2 + "' and tblnominas.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TPENeg = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(round(importegravado,2)) from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina where tblnominadetalles.tipodetalle=1 and estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnominas.fechacancelado>='" + pFecha1 + "' and tblnominas.fechacancelado<='" + pFecha2 + "' and tblnominas.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TDGNeg = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(round(importeexento,2)) from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina where tblnominadetalles.tipodetalle=1 and estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnominas.fechacancelado>='" + pFecha1 + "' and tblnominas.fechacancelado<='" + pFecha2 + "' and tblnominas.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TDENeg = Comm.ExecuteScalar


        Comm.CommandText = "select ifnull((select sum(round(importepagado,2)) from tblnominas inner join tblnominahorasextra on tblnominas.idnomina=tblnominahorasextra.idnomina where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnominas.fechacancelado>='" + pFecha1 + "' and tblnominas.fechacancelado<='" + pFecha2 + "' and tblnominas.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        THENeg = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(round(descuento,2)) from tblnominas inner join tblnominaincapacidades on tblnominas.idnomina=tblnominaincapacidades.idnomina where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnominas.fechacancelado>='" + pFecha1 + "' and tblnominas.fechacancelado<='" + pFecha2 + "' and tblnominas.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TINeg = Comm.ExecuteScalar


        Comm.CommandText = "select sum(round(importeexento+importegravado,2)) as importe,tblpercepciones.descripcion,tblpercepciones.idcuenta,concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN5.ToString + ",'0'))) as Cuenta from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina inner join tblpercepciones on tblnominadetalles.tipo=tblpercepciones.idpercepcion inner join tblccontables c on c.idccontable=tblpercepciones.idcuenta where tblnominadetalles.tipodetalle=0 and estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnominas.fechacancelado>='" + pFecha1 + "' and tblnominas.fechacancelado<='" + pFecha2 + "' and tblnominas.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnominadetalles.tipo"
        'Dim Reader As MySql.Data.MySqlClient.MySqlDataReader

        Reader = Comm.ExecuteReader
        TPxCNeg.Clear()
        While Reader.Read
            ConceptoNomina.concepto = Reader("descripcion")
            ConceptoNomina.importe = Reader("importe")
            ConceptoNomina.idcuenta = Reader("idcuenta")
            ConceptoNomina.Cuenta = Reader("cuenta")
            TPxCNeg.Add(ConceptoNomina)
        End While
        Reader.Close()


        Comm.CommandText = "select sum(round(importeexento+importegravado,2)) as importe,tbldeducciones.descripcion,tbldeducciones.idcuenta,concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN5.ToString + ",'0'))) as Cuenta from tblnominas inner join tblnominadetalles on tblnominas.idnomina=tblnominadetalles.idnomina inner join tbldeducciones on tblnominadetalles.tipo=tbldeducciones.iddeduccion inner join tblccontables c on c.idccontable=tbldeducciones.idcuenta where tblnominadetalles.tipodetalle=1 and estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnominas.fechacancelado>='" + pFecha1 + "' and tblnominas.fechacancelado<='" + pFecha2 + "' and tblnominas.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnominadetalles.tipo"
        'Dim Reader As MySql.Data.MySqlClient.MySqlDataReader

        Reader = Comm.ExecuteReader
        TDxCNeg.Clear()
        While Reader.Read
            ConceptoNomina.concepto = Reader("descripcion")
            ConceptoNomina.importe = Reader("importe")
            ConceptoNomina.idcuenta = Reader("idcuenta")
            ConceptoNomina.Cuenta = Reader("cuenta")
            TDxCNeg.Add(ConceptoNomina)
        End While
        Reader.Close()

        TPNeg = TPGNeg + TPENeg
        TDNeg = TDGNeg + TDENeg
        TNnoNeg = TPNeg - TDNeg




        DocumentosProcesados(pModulo, pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)

    End Sub
    Private Sub LlenaVariablesFacturas(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        'Facturas llenar variables****************************************FACTURAS***********
        '**********Contado*************
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(tblventasinventario.precio,2),round(tblventasinventario.precio*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSICon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.iva/100,2),round(tblventasinventario.precio*tblventasinventario.iva/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVACon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ivaretenido/100,2),round(precio*tblventasinventario.ivaretenido/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(total*ivaretenido/100,2),round(total*ivaretenido/100*tipodecambio,2))) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARCon += Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(total*isr/100,2),round(total*isr/100*tipodecambio,2))) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TISRCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ieps/100,2),round(precio*tblventasinventario.ieps/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIEPSCon = Comm.ExecuteScalar

        '**************CREDITO***************************

        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(tblventasinventario.precio,2),round(tblventasinventario.precio*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSICr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.iva/100,2),round(tblventasinventario.precio*tblventasinventario.iva/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVACr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ivaretenido/100,2),round(precio*tblventasinventario.ivaretenido/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(total*ivaretenido/100,2),round(total*ivaretenido/100*tipodecambio,2))) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARCr += Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(total*isr/100,2),round(total*isr/100*tipodecambio,2))) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TISRCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ieps/100,2),round(precio*tblventasinventario.ieps/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        ' End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIEPSCr = Comm.ExecuteScalar


        TIVA = TIVACon + TIVACr
        TIEPS = TIEPSCon + TIEPSCr
        TIVAR = TIVARCon + TIVARCr
        TISR = TISRCon + TISRCr
        TIVAN = TIVA + TIEPS - TIVAR - TISR
        TIVANCon = TIVACon + TIEPSCon - TIVARCon - TISRCon
        TIVANCr = TIVACr + TIEPSCr - TIVARCr - TISRCr
        TSI = TSICon + TSICr
        TNCon = TSICon + TIVANCon
        TNCr = TSICr + TIVANCr
        TN = TSI + TIVAN '- TISR - TIVAR + TIEPS

        'Llena variables Canceladas
        '***************CONTADO*******************
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio,2),round(precio*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TSINegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.iva/100,2),round(precio*tblventasinventario.iva/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVANegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ivaretenido/100,2),round(precio*tblventasinventario.ivaretenido/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVARNegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(total*ivaretenido/100,2),round(total*ivaretenido/100*tipodecambio,2))) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVARNegCon += Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(total*isr/100,2),round(total*isr/100*tipodecambio,2))) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TISRNegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ieps/100,2),round(precio*tblventasinventario.ieps/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIEPSNegCon = Comm.ExecuteScalar

        '****************CREDITO canceladas***********************

        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio,2),round(precio*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TSINegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.iva/100,2),round(precio*tblventasinventario.iva/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVANegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ivaretenido/100,2),round(precio*tblventasinventario.ivaretenido/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVARNegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(total*ivaretenido/100,2),round(total*ivaretenido/100*tipodecambio,2))) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVARNegCr += Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(total*isr/100,2),round(total*isr/100*tipodecambio,2))) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TISRNegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ieps/100,2),round(precio*tblventasinventario.ieps/100*tblventas.tipodecambio,2))) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIEPSNegCr = Comm.ExecuteScalar

        TIVANeg = TIVANegCon + TIVANegCr
        TIEPSNeg = TIEPSNegCon + TIEPSNegCr
        TIVARNeg = TIVARNegCon + TIVARNegCr
        TISRNeg = TISRNegCon + TISRNegCr
        TIVANNeg = TIVANeg + TIEPSNeg - TIVARNeg - TISRNeg
        TIVANNegCon = TIVANegCon + TIEPSNegCon - TIVARNegCon - TISRNegCon
        TIVANNegCr = TIVANegCr + TIEPSNegCr - TIVARNegCr - TISRNegCr
        TSINeg = TSINegCon + TSINegCr
        TNNegCon = TSINegCon + TIVANNegCon
        TNNegCr = TSINegCr + TIVANNegCr
        TNNeg = TSINeg + TIVANNeg

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        '***************Por Factura Contado***********
        Comm.CommandText = "select ifnull(sum(if(tblventasinventario.idmoneda=2,round(tblventasinventario.precio,2),round(tblventasinventario.precio*tblventas.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.iva/100,2),round(tblventasinventario.precio*tblventasinventario.iva/100*tblventas.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ivaretenido/100,2),round(precio*tblventasinventario.ivaretenido/100*tblventas.tipodecambio,2))),0)+ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.ivaretenido/100,2),round(tblventas.total*tblventas.ivaretenido/100*tblventas.tipodecambio,2)),0) as totalivaret," +
            " ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.isr/100,2),round(tblventas.total*tblventas.isr/100*tblventas.tipodecambio,2)),0) as totalisr," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ieps/100,2),round(precio*tblventasinventario.ieps/100*tblventas.tipodecambio,2))),0) as totalieps," +
            " tblventas.idventa,ifnull(tblventas.serie,'*Nohay') as serie,tblventas.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tblventas.tipodecambio,tblventas.idconversion,tblventas.fecha,ifnull((select tblventastimbrado.uuid from tblventastimbrado where tblventastimbrado.idventa=tblventas.idventa),'') as foliocfdi,round(tblventas.totalapagar,2) totalapagar" +
            " from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.estado=3 "
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblventas.idventa order by tblventas.fecha,tblventas.serie,tblventas.folio"

        DR = Comm.ExecuteReader
        FacturasListaCon.Clear()
        Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = DR("totalisr")
                F.Retendido = DR("totalivaret")
                F.Concepto = "FACTURA: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idventa")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idconversion") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaCon.Add(F)
            End If
        End While
        DR.Close()


        '***************Por Factura Credito***********
        Comm.CommandText = "select ifnull(sum(if(tblventasinventario.idmoneda=2,round(tblventasinventario.precio,2),round(tblventasinventario.precio*tblventas.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.iva/100,2),round(tblventasinventario.precio*tblventasinventario.iva/100*tblventas.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ivaretenido/100,2),round(precio*tblventasinventario.ivaretenido/100*tblventas.tipodecambio,2))),0)+ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.ivaretenido/100,2),round(tblventas.total*tblventas.ivaretenido/100*tblventas.tipodecambio,2)),0) as totalivaret," +
            " ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.isr/100,2),round(tblventas.total*tblventas.isr/100*tblventas.tipodecambio,2)),0) as totalisr," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ieps/100,2),round(precio*tblventasinventario.ieps/100*tblventas.tipodecambio,2))),0) as totalieps," +
            " tblventas.idventa,ifnull(tblventas.serie,'*Nohay') as serie,tblventas.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tblventas.tipodecambio,tblventas.idconversion,tblventas.fecha,ifnull((select tblventastimbrado.uuid from tblventastimbrado where tblventastimbrado.idventa=tblventas.idventa),'') as foliocfdi,round(tblventas.totalapagar,2) totalapagar" +
            " from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.estado=3 "
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblventas.idventa order by tblventas.fecha,tblventas.serie,tblventas.folio"

        DR = Comm.ExecuteReader
        FacturasListaCr.Clear()
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = DR("totalisr")
                F.Retendido = DR("totalivaret")
                F.Concepto = "FACTURA: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idventa")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idconversion") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaCr.Add(F)
            End If
        End While
        DR.Close()
        'Por facturas todas
        Comm.CommandText = "select ifnull(sum(if(tblventasinventario.idmoneda=2,round(tblventasinventario.precio,2),round(tblventasinventario.precio*tblventas.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.iva/100,2),round(tblventasinventario.precio*tblventasinventario.iva/100*tblventas.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ivaretenido/100,2),round(precio*tblventasinventario.ivaretenido/100*tblventas.tipodecambio,2))),0)+ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.ivaretenido/100,2),round(tblventas.total*tblventas.ivaretenido/100*tblventas.tipodecambio,2)),0) as totalivaret," +
            " ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.isr/100,2),round(tblventas.total*tblventas.isr/100*tblventas.tipodecambio,2)),0) as totalisr," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ieps/100,2),round(precio*tblventasinventario.ieps/100*tblventas.tipodecambio,2))),0) as totalieps," +
            " tblventas.idventa,ifnull(tblventas.serie,'*Nohay') as serie,tblventas.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tblventas.tipodecambio,tblventas.idconversion,tblventas.fecha,ifnull((select tblventastimbrado.uuid from tblventastimbrado where tblventastimbrado.idventa=tblventas.idventa),'') as foliocfdi,round(tblventas.totalapagar,2) totalapagar" +
            " from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.estado=3 "
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblventas.idventa order by tblventas.fecha,tblventas.serie,tblventas.folio"

        DR = Comm.ExecuteReader
        FacturasLista.Clear()
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = DR("totalisr")
                F.Retendido = DR("totalivaret")
                F.Concepto = "FACTURA: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idventa")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idconversion") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasLista.Add(F)
            End If
        End While
        DR.Close()

        '***************Por Factura Contado Cancelado***********
        Comm.CommandText = "select ifnull(sum(if(tblventasinventario.idmoneda=2,round(tblventasinventario.precio,2),round(tblventasinventario.precio*tblventas.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.iva/100,2),round(tblventasinventario.precio*tblventasinventario.iva/100*tblventas.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ivaretenido/100,2),round(precio*tblventasinventario.ivaretenido/100*tblventas.tipodecambio,2))),0)+ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.ivaretenido/100,2),round(tblventas.total*tblventas.ivaretenido/100*tblventas.tipodecambio,2)),0) as totalivaret," +
            " ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.isr/100,2),round(tblventas.total*tblventas.isr/100*tblventas.tipodecambio,2)),0) as totalisr," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ieps/100,2),round(precio*tblventasinventario.ieps/100*tblventas.tipodecambio,2))),0) as totalieps," +
            " tblventas.idventa,ifnull(tblventas.serie,'*Nohay') as serie,tblventas.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tblventas.tipodecambio,tblventas.idconversion,tblventas.fecha,ifnull((select tblventastimbrado.uuid from tblventastimbrado where tblventastimbrado.idventa=tblventas.idventa),'') as foliocfdi,round(tblventas.totalapagar,2) totalapagar" +
            " from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.estado=4 "
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblventas.idventa order by tblventas.fecha,tblventas.serie,tblventas.folio"

        DR = Comm.ExecuteReader
        FacturasListaNegCon.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                If DR("idventa") <> 0 Then
                    F.Cantidad = DR("subtotal")
                    F.Iva = DR("totaliva")
                    F.Ieps = DR("totalieps")
                    F.ISR = DR("totalisr")
                    F.Retendido = DR("totalivaret")
                    F.Concepto = "FACTURA CANCELADA: " + DR("serie") + Format(DR("folio"), "000")
                    F.Cuenta = DR("cuenta")
                    F.Idcuenta = DR("idcuenta")
                    F.IdDeposito = DR("idventa")
                    F.Folio = ""
                    F.IdProveedor = 0
                    F.IvaPor = 0
                    F.IVaRetPor = 0
                    F.IEpsPor = 0
                    F.ChequeTrans.BancoDestinoEx = ""
                    F.ChequeTrans.BancoEx = ""
                    F.ChequeTrans.BancoOrigenEx = ""
                    F.ChequeTrans.Beneficiario = ""
                    F.ChequeTrans.Fecha = ""
                    F.ChequeTrans.IdBancoDestino = 0
                    F.ChequeTrans.IdBancoOrigen = 0
                    F.ChequeTrans.IdMoneda = 0
                    F.ChequeTrans.Monto = 0
                    F.ChequeTrans.NoCuentaDestino = ""
                    F.ChequeTrans.NoCuentaOrigen = ""
                    F.ChequeTrans.NumeroCheque = ""
                    F.ChequeTrans.RFC = ""
                    F.ChequeTrans.TipodeCambio = 0

                    F.Uuid.Uuid = DR("foliocfdi")
                    F.Uuid.Fecha = DR("fecha")
                    F.Uuid.Monto = DR("totalapagar")
                    F.Uuid.RFC = DR("rfc")
                    F.Uuid.TipodeCambio = DR("tipodecambio")
                    If DR("idconversion") = 2 Then
                        F.Uuid.IdMoneda = 100
                    Else
                        F.Uuid.IdMoneda = 147
                    End If
                    F.NombreProveedor = ""
                    FacturasListaNegCon.Add(F)
                End If
            End If
        End While
        DR.Close()


        '***************Por Factura Credito canceladas***********
        Comm.CommandText = "select ifnull(sum(if(tblventasinventario.idmoneda=2,round(tblventasinventario.precio,2),round(tblventasinventario.precio*tblventas.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.iva/100,2),round(tblventasinventario.precio*tblventasinventario.iva/100*tblventas.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ivaretenido/100,2),round(precio*tblventasinventario.ivaretenido/100*tblventas.tipodecambio,2))),0)+ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.ivaretenido/100,2),round(tblventas.total*tblventas.ivaretenido/100*tblventas.tipodecambio,2)),0) as totalivaret," +
            " ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.isr/100,2),round(tblventas.total*tblventas.isr/100*tblventas.tipodecambio,2)),0) as totalisr," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ieps/100,2),round(precio*tblventasinventario.ieps/100*tblventas.tipodecambio,2))),0) as totalieps," +
            " tblventas.idventa,ifnull(tblventas.serie,'*Nohay') as serie,tblventas.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tblventas.tipodecambio,tblventas.idconversion,tblventas.fecha,ifnull((select tblventastimbrado.uuid from tblventastimbrado where tblventastimbrado.idventa=tblventas.idventa),'') as foliocfdi,round(tblventas.totalapagar,2) totalapagar" +
            " from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.estado=4 "
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblventas.idventa order by tblventas.fecha,tblventas.serie,tblventas.folio"

        DR = Comm.ExecuteReader
        FacturasListaNegCr.Clear()
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                If DR("idventa") <> 0 Then
                    F.Cantidad = DR("subtotal")
                    F.Iva = DR("totaliva")
                    F.Ieps = DR("totalieps")
                    F.ISR = DR("totalisr")
                    F.Retendido = DR("totalivaret")
                    F.Concepto = "FACTURA CANCELADA: " + DR("serie") + Format(DR("folio"), "000")
                    F.Cuenta = DR("cuenta")
                    F.Idcuenta = DR("idcuenta")
                    F.IdDeposito = DR("idventa")
                    F.Folio = ""
                    F.IdProveedor = 0
                    F.IvaPor = 0
                    F.IVaRetPor = 0
                    F.IEpsPor = 0
                    F.ChequeTrans.BancoDestinoEx = ""
                    F.ChequeTrans.BancoEx = ""
                    F.ChequeTrans.BancoOrigenEx = ""
                    F.ChequeTrans.Beneficiario = ""
                    F.ChequeTrans.Fecha = ""
                    F.ChequeTrans.IdBancoDestino = 0
                    F.ChequeTrans.IdBancoOrigen = 0
                    F.ChequeTrans.IdMoneda = 0
                    F.ChequeTrans.Monto = 0
                    F.ChequeTrans.NoCuentaDestino = ""
                    F.ChequeTrans.NoCuentaOrigen = ""
                    F.ChequeTrans.NumeroCheque = ""
                    F.ChequeTrans.RFC = ""
                    F.ChequeTrans.TipodeCambio = 0

                    F.Uuid.Uuid = DR("foliocfdi")
                    F.Uuid.Fecha = DR("fecha")
                    F.Uuid.Monto = DR("totalapagar")
                    F.Uuid.RFC = DR("rfc")
                    F.Uuid.TipodeCambio = DR("tipodecambio")
                    If DR("idconversion") = 2 Then
                        F.Uuid.IdMoneda = 100
                    Else
                        F.Uuid.IdMoneda = 147
                    End If
                    F.NombreProveedor = ""
                    FacturasListaNegCr.Add(F)
                End If
            End If
        End While
        DR.Close()
        'Por facturas todas canceladas
        Comm.CommandText = "select ifnull(sum(if(tblventasinventario.idmoneda=2,round(tblventasinventario.precio,2),round(tblventasinventario.precio*tblventas.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.iva/100,2),round(tblventasinventario.precio*tblventasinventario.iva/100*tblventas.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ivaretenido/100,2),round(precio*tblventasinventario.ivaretenido/100*tblventas.tipodecambio,2))),0)+ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.ivaretenido/100,2),round(tblventas.total*tblventas.ivaretenido/100*tblventas.tipodecambio,2)),0) as totalivaret," +
            " ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.isr/100,2),round(tblventas.total*tblventas.isr/100*tblventas.tipodecambio,2)),0) as totalisr," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ieps/100,2),round(precio*tblventasinventario.ieps/100*tblventas.tipodecambio,2))),0) as totalieps," +
            " tblventas.idventa,ifnull(tblventas.serie,'*Nohay') as serie,tblventas.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tblventas.tipodecambio,tblventas.idconversion,tblventas.fecha,ifnull((select tblventastimbrado.uuid from tblventastimbrado where tblventastimbrado.idventa=tblventas.idventa),'') as foliocfdi,round(tblventas.totalapagar,2) totalapagar" +
            " from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.estado=4 "
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblventas.idventa order by tblventas.fecha,tblventas.serie,tblventas.folio"

        DR = Comm.ExecuteReader
        FacturasListaNeg.Clear()
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                If DR("idventa") <> 0 Then
                    F.Cantidad = DR("subtotal")
                    F.Iva = DR("totaliva")
                    F.Ieps = DR("totalieps")
                    F.ISR = DR("totalisr")
                    F.Retendido = DR("totalivaret")
                    F.Concepto = "FACTURA CANCELADA: " + DR("serie") + Format(DR("folio"), "000")
                    F.Cuenta = DR("cuenta")
                    F.Idcuenta = DR("idcuenta")
                    F.IdDeposito = DR("idventa")
                    F.Folio = ""
                    F.IdProveedor = 0
                    F.IvaPor = 0
                    F.IVaRetPor = 0
                    F.IEpsPor = 0
                    F.ChequeTrans.BancoDestinoEx = ""
                    F.ChequeTrans.BancoEx = ""
                    F.ChequeTrans.BancoOrigenEx = ""
                    F.ChequeTrans.Beneficiario = ""
                    F.ChequeTrans.Fecha = ""
                    F.ChequeTrans.IdBancoDestino = 0
                    F.ChequeTrans.IdBancoOrigen = 0
                    F.ChequeTrans.IdMoneda = 0
                    F.ChequeTrans.Monto = 0
                    F.ChequeTrans.NoCuentaDestino = ""
                    F.ChequeTrans.NoCuentaOrigen = ""
                    F.ChequeTrans.NumeroCheque = ""
                    F.ChequeTrans.RFC = ""
                    F.ChequeTrans.TipodeCambio = 0

                    F.Uuid.Uuid = DR("foliocfdi")
                    F.Uuid.Fecha = DR("fecha")
                    F.Uuid.Monto = DR("totalapagar")
                    F.Uuid.RFC = DR("rfc")
                    F.Uuid.TipodeCambio = DR("tipodecambio")
                    If DR("idconversion") = 2 Then
                        F.Uuid.IdMoneda = 100
                    Else
                        F.Uuid.IdMoneda = 147
                    End If
                    F.NombreProveedor = ""
                    FacturasListaNeg.Add(F)
                End If
            End If
        End While
        DR.Close()

        DocumentosProcesados(pModulo, pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
    End Sub

    Private Sub LlenaVariablesDevoluciones(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        'Devoluciones contado***************************************************
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(tbldevolucionesdetalles.precio,2),round(tbldevolucionesdetalles.precio*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSIDCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.iva/100,2),round(tbldevolucionesdetalles.precio*tbldevolucionesdetalles.iva/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVADCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ivaretenido/100,2),round(precio*tbldevolucionesdetalles.ivaretenido/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARDCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100,2),round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100*tbldevoluciones.tipodecambio,2))) from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARDCon += Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.isr/100,2),round(tbldevoluciones.total*tbldevoluciones.isr/100*tbldevoluciones.tipodecambio,2))) from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TISRDCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ieps/100,2),round(precio*tbldevolucionesdetalles.ieps/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIEPSDCon = Comm.ExecuteScalar

        'Devoluciones cradito***************************************************
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(tbldevolucionesdetalles.precio,2),round(tbldevolucionesdetalles.precio*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSIDCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.iva/100,2),round(tbldevolucionesdetalles.precio*tbldevolucionesdetalles.iva/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVADCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ivaretenido/100,2),round(precio*tbldevolucionesdetalles.ivaretenido/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARDCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100,2),round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100*tbldevoluciones.tipodecambio,2))) from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARDCr += Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.isr/100,2),round(tbldevoluciones.total*tbldevoluciones.isr/100*tbldevoluciones.tipodecambio,2))) from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TISRDCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ieps/100,2),round(precio*tbldevolucionesdetalles.ieps/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIEPSDCr = Comm.ExecuteScalar

        TIVAD = TIVADCon + TIVADCr
        TIEPSD = TIEPSDCon + TIEPSDCr
        TIVARD = TIVARDCon + TIVARDCr
        TISRD = TISRDCon + TISRDCr
        TIVAND = TIVAD + TIEPSD - TIVARD - TISRD
        TIVANDCon = TIVADCon + TIEPSDCon - TIVARDCon - TISRDCon
        TIVANDCr = TIVADCr + TIEPSDCr - TIVARDCr - TISRDCr
        TSID = TSIDCon + TSIDCr
        TNDCon = TSIDCon + TIVANDCon
        TNDCr = TSIDCr + TIVANDCr
        TND = TSID + TIVAND

        'Llena variables Canceladas contado
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio,2),round(precio*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TSIDNegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.iva/100,2),round(precio*tbldevolucionesdetalles.iva/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVADNegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ivaretenido/100,2),round(precio*tbldevolucionesdetalles.ivaretenido/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        ' End If
        'End If
        Comm.CommandText += "),0)"
        TIVARDNegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100,2),round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100*tbldevoluciones.tipodecambio,2))) from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVARDNegCon += Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.isr/100,2),round(tbldevoluciones.total*tbldevoluciones.isr/100*tbldevoluciones.tipodecambio,2))) from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TISRDNegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ieps/100,2),round(precio*tbldevolucionesdetalles.ieps/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIEPSDNegCon = Comm.ExecuteScalar


        'Llena variables Canceladas credito
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio,2),round(precio*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TSIDNegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.iva/100,2),round(precio*tbldevolucionesdetalles.iva/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVADNegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ivaretenido/100,2),round(precio*tbldevolucionesdetalles.ivaretenido/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        ' End If
        'End If
        Comm.CommandText += "),0)"
        TIVARDNegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100,2),round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100*tbldevoluciones.tipodecambio,2))) from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVARDNegCr += Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.isr/100,2),round(tbldevoluciones.total*tbldevoluciones.isr/100*tbldevoluciones.tipodecambio,2))) from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TISRDNegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ieps/100,2),round(precio*tbldevolucionesdetalles.ieps/100*tbldevoluciones.tipodecambio,2))) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIEPSDNegCr = Comm.ExecuteScalar

        TIVADNeg = TIVADNegCon + TIVADNegCr
        TIEPSDNeg = TIEPSDNegCon + TIEPSDNegCr
        TIVARDNeg = TIVARDNegCon + TIVARDNegCr
        TISRDNeg = TISRDNegCon + TISRDNegCr
        TIVANDNeg = TIVADNeg + TIEPSDNeg - TIVARDNeg - TISRDNeg
        TIVANDNegCon = TIVADNegCon + TIEPSDNegCon - TIVARDNegCon - TISRDNegCon
        TIVANDNegCr = TIVADNegCr + TIEPSDNegCr - TIVARDNegCr - TISRDNegCr
        TSIDNeg = TSIDNegCon + TSIDNegCr
        TNDNegCon = TSIDNegCon + TIVANDNegCon
        TNDNegCr = TSIDNegCr + TIVANDNegCr
        TNDNeg = TSIDNeg + TIVANDNeg

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        '***************Por dev Contado***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio,2),round(precio*tbldevoluciones.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.iva/100,2),round(precio*tbldevolucionesdetalles.iva/100*tbldevoluciones.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ivaretenido/100,2),round(precio*tbldevolucionesdetalles.ivaretenido/100*tbldevoluciones.tipodecambio,2))),0)+ifnull(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100,2),round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100*tbldevoluciones.tipodecambio,2)),0) as totalivaret," +
            " ifnull(sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.isr/100,2),round(tbldevoluciones.total*tbldevoluciones.isr/100*tbldevoluciones.tipodecambio,2))),0) as totalisr," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ieps/100,2),round(precio*tbldevolucionesdetalles.ieps/100*tbldevoluciones.tipodecambio,2))),0) as totalieps," +
            " tbldevoluciones.iddevolucion,ifnull(tbldevoluciones.serie,'*Nohay') as serie,tbldevoluciones.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion idmoneda,tbldevoluciones.fecha,ifnull((select tbldevolucionestimbrado.uuid from tbldevolucionestimbrado where tbldevolucionestimbrado.iddevolucion=tbldevoluciones.iddevolucion),'') as foliocfdi,round(tbldevoluciones.totalapagar,2) totalapagar" +
            " from tbldevoluciones inner join tbldevolucionesdetalles on tbldevoluciones.iddevolucion=tbldevolucionesdetalles.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevoluciones.iddevolucion order by tbldevoluciones.fecha,tbldevoluciones.serie,tbldevoluciones.folio"

        DR = Comm.ExecuteReader
        FacturasListaDCon.Clear()
        Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = DR("totalisr")
                F.Retendido = DR("totalivaret")
                F.Concepto = "DEV: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaDCon.Add(F)
            End If
        End While
        DR.Close()

        '***************Por dev Credito***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio,2),round(precio*tbldevoluciones.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.iva/100,2),round(precio*tbldevolucionesdetalles.iva/100*tbldevoluciones.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ivaretenido/100,2),round(precio*tbldevolucionesdetalles.ivaretenido/100*tbldevoluciones.tipodecambio,2))),0)+ifnull(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100,2),round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100*tbldevoluciones.tipodecambio,2)),0) as totalivaret," +
            " ifnull(sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.isr/100,2),round(tbldevoluciones.total*tbldevoluciones.isr/100*tbldevoluciones.tipodecambio,2))),0) as totalisr," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ieps/100,2),round(precio*tbldevolucionesdetalles.ieps/100*tbldevoluciones.tipodecambio,2))),0) as totalieps," +
            " tbldevoluciones.iddevolucion,ifnull(tbldevoluciones.serie,'*Nohay') as serie,tbldevoluciones.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion idmoneda,tbldevoluciones.fecha,ifnull((select tbldevolucionestimbrado.uuid from tbldevolucionestimbrado where tbldevolucionestimbrado.iddevolucion=tbldevoluciones.iddevolucion),'') as foliocfdi,round(tbldevoluciones.totalapagar,2) totalapagar" +
            " from tbldevoluciones inner join tbldevolucionesdetalles on tbldevoluciones.iddevolucion=tbldevolucionesdetalles.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente  where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevoluciones.iddevolucion order by tbldevoluciones.fecha,tbldevoluciones.serie,tbldevoluciones.folio"

        DR = Comm.ExecuteReader
        FacturasListaDCr.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = DR("totalisr")
                F.Retendido = DR("totalivaret")
                F.Concepto = "DEV: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaDCr.Add(F)
            End If
        End While
        DR.Close()

        '***************Por dev Contado canceladas***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio,2),round(precio*tbldevoluciones.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.iva/100,2),round(precio*tbldevolucionesdetalles.iva/100*tbldevoluciones.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ivaretenido/100,2),round(precio*tbldevolucionesdetalles.ivaretenido/100*tbldevoluciones.tipodecambio,2))),0)+ifnull(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100,2),round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100*tbldevoluciones.tipodecambio,2)),0) as totalivaret," +
            " ifnull(sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.isr/100,2),round(tbldevoluciones.total*tbldevoluciones.isr/100*tbldevoluciones.tipodecambio,2))),0) as totalisr," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ieps/100,2),round(precio*tbldevolucionesdetalles.ieps/100*tbldevoluciones.tipodecambio,2))),0) as totalieps," +
            " tbldevoluciones.iddevolucion,ifnull(tbldevoluciones.serie,'*Nohay') as serie,tbldevoluciones.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion idmoneda,tbldevoluciones.fecha,ifnull((select tbldevolucionestimbrado.uuid from tbldevolucionestimbrado where tbldevolucionestimbrado.iddevolucion=tbldevoluciones.iddevolucion),'') as foliocfdi,round(tbldevoluciones.totalapagar,2) totalapagar" +
            " from tbldevoluciones inner join tbldevolucionesdetalles on tbldevoluciones.iddevolucion=tbldevolucionesdetalles.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente  where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevoluciones.iddevolucion order by tbldevoluciones.fecha,tbldevoluciones.serie,tbldevoluciones.folio"

        DR = Comm.ExecuteReader
        FacturasListaDNegCon.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = DR("totalisr")
                F.Retendido = DR("totalivaret")
                F.Concepto = "FACTURA: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaDNegCon.Add(F)
            End If
        End While
        DR.Close()

        '***************Por dev Credito canceladas***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio,2),round(precio*tbldevoluciones.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.iva/100,2),round(precio*tbldevolucionesdetalles.iva/100*tbldevoluciones.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ivaretenido/100,2),round(precio*tbldevolucionesdetalles.ivaretenido/100*tbldevoluciones.tipodecambio,2))),0)+ifnull(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100,2),round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100*tbldevoluciones.tipodecambio,2)),0) as totalivaret," +
            " ifnull(sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.isr/100,2),round(tbldevoluciones.total*tbldevoluciones.isr/100*tbldevoluciones.tipodecambio,2))),0) as totalisr," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ieps/100,2),round(precio*tbldevolucionesdetalles.ieps/100*tbldevoluciones.tipodecambio,2))),0) as totalieps," +
            " tbldevoluciones.iddevolucion,ifnull(tbldevoluciones.serie,'*Nohay') as serie,tbldevoluciones.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion idmoneda,tbldevoluciones.fecha,ifnull((select tbldevolucionestimbrado.uuid from tbldevolucionestimbrado where tbldevolucionestimbrado.iddevolucion=tbldevoluciones.iddevolucion),'') as foliocfdi,round(tbldevoluciones.totalapagar,2) totalapagar" +
            " from tbldevoluciones inner join tbldevolucionesdetalles on tbldevoluciones.iddevolucion=tbldevolucionesdetalles.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente  where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevoluciones.iddevolucion order by tbldevoluciones.fecha,tbldevoluciones.serie,tbldevoluciones.folio"

        DR = Comm.ExecuteReader
        FacturasListaDNegCr.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = DR("totalisr")
                F.Retendido = DR("totalivaret")
                F.Concepto = "FACTURA: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaDNegCr.Add(F)
            End If
        End While
        DR.Close()

        '***************Por dev todo***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio,2),round(precio*tbldevoluciones.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.iva/100,2),round(precio*tbldevolucionesdetalles.iva/100*tbldevoluciones.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ivaretenido/100,2),round(precio*tbldevolucionesdetalles.ivaretenido/100*tbldevoluciones.tipodecambio,2))),0)+ifnull(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100,2),round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100*tbldevoluciones.tipodecambio,2)),0) as totalivaret," +
            " ifnull(sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.isr/100,2),round(tbldevoluciones.total*tbldevoluciones.isr/100*tbldevoluciones.tipodecambio,2))),0) as totalisr," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ieps/100,2),round(precio*tbldevolucionesdetalles.ieps/100*tbldevoluciones.tipodecambio,2))),0) as totalieps," +
            " tbldevoluciones.iddevolucion,ifnull(tbldevoluciones.serie,'*Nohay') as serie,tbldevoluciones.folio,tblclientes.idcuenta," +
            " if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion idmoneda,tbldevoluciones.fecha,ifnull((select tbldevolucionestimbrado.uuid from tbldevolucionestimbrado where tbldevolucionestimbrado.iddevolucion=tbldevoluciones.iddevolucion),'') as foliocfdi,round(tbldevoluciones.totalapagar,2) totalapagar" +
            " from tbldevoluciones inner join tbldevolucionesdetalles on tbldevoluciones.iddevolucion=tbldevolucionesdetalles.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente  where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevoluciones.iddevolucion order by tbldevoluciones.fecha,tbldevoluciones.serie,tbldevoluciones.folio"

        DR = Comm.ExecuteReader
        FacturasListaD.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = DR("totalisr")
                F.Retendido = DR("totalivaret")
                F.Concepto = "DEV: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaD.Add(F)
            End If
        End While
        DR.Close()

        '***************Por dev todo cancelado***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio,2),round(precio*tbldevoluciones.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.iva/100,2),round(precio*tbldevolucionesdetalles.iva/100*tbldevoluciones.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ivaretenido/100,2),round(precio*tbldevolucionesdetalles.ivaretenido/100*tbldevoluciones.tipodecambio,2))),0)+ifnull(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100,2),round(tbldevoluciones.total*tbldevoluciones.ivaretenido/100*tbldevoluciones.tipodecambio,2)),0) as totalivaret," +
            " ifnull(sum(if(tbldevoluciones.idconversion=2,round(tbldevoluciones.total*tbldevoluciones.isr/100,2),round(tbldevoluciones.total*tbldevoluciones.isr/100*tbldevoluciones.tipodecambio,2))),0) as totalisr," +
            " ifnull(sum(if(tbldevolucionesdetalles.idmoneda=2,round(precio*tbldevolucionesdetalles.ieps/100,2),round(precio*tbldevolucionesdetalles.ieps/100*tbldevoluciones.tipodecambio,2))),0) as totalieps," +
            " tbldevoluciones.iddevolucion,ifnull(tbldevoluciones.serie,'*Nohay') as serie,tbldevoluciones.folio,tblclientes.idcuenta," +
            " if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion idmoneda,tbldevoluciones.fecha,ifnull((select tbldevolucionestimbrado.uuid from tbldevolucionestimbrado where tbldevolucionestimbrado.iddevolucion=tbldevoluciones.iddevolucion),'') as foliocfdi,round(tbldevoluciones.totalapagar,2) totalapagar" +
            " from tbldevoluciones inner join tbldevolucionesdetalles on tbldevoluciones.iddevolucion=tbldevolucionesdetalles.iddevolucion inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente  where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevoluciones.iddevolucion order by tbldevoluciones.fecha,tbldevoluciones.serie,tbldevoluciones.folio"

        DR = Comm.ExecuteReader
        FacturasListaDNeg.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = DR("totalisr")
                F.Retendido = DR("totalivaret")
                F.Concepto = "DEV: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaDNeg.Add(F)
            End If
        End While
        DR.Close()

        DocumentosProcesados(pModulo, pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
    End Sub

    Private Sub LlenaVariablesDevolucionesC(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        'Facturas llenar variables****************************************FACTURAS***********
        'contado
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(tbldevolucionesdetallesc.precio,2),round(tbldevolucionesdetallesc.precio*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0"
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSIDCCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.iva/100,2),round(tbldevolucionesdetallesc.precio*tbldevolucionesdetallesc.iva/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVADCCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ivaretenido/100,2),round(precio*tbldevolucionesdetallesc.ivaretenido/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARDCCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ieps/100,2),round(precio*tbldevolucionesdetallesc.ieps/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIEPSDCCon = Comm.ExecuteScalar

        'Credito
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(tbldevolucionesdetallesc.precio,2),round(tbldevolucionesdetallesc.precio*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSIDCCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.iva/100,2),round(tbldevolucionesdetallesc.precio*tbldevolucionesdetallesc.iva/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVADCCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ivaretenido/100,2),round(precio*tbldevolucionesdetallesc.ivaretenido/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIVARDCCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ieps/100,2),round(precio*tbldevolucionesdetallesc.ieps/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TIEPSDCCr = Comm.ExecuteScalar


        TIVADC = TIVADCCon + TIVADCCr
        TIEPSDC = TIEPSDCCon + TIEPSDCCr
        TIVARDC = TIVARDCCon + TIVARDCCr
        TIVANDC = TIVADC + TIEPSDC - TIVARDC
        TIVANDCCon = TIVADCCon + TIEPSDCCon - TIVARDCCon
        TIVANDCCr = TIVADCCr + TIEPSDCCr - TIVARDCCr
        TSIDC = TSIDCCon + TSIDCCr
        TNDCCon = TSIDCCon + TIVANDCCon
        TNDCCr = TSIDCCr + TIVANDCCr
        TNDC = TSIDC + TIVANDC

        'Llena variables Canceladas contado
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio,2),round(precio*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TSIDCNegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.iva/100,2),round(precio*tbldevolucionesdetallesc.iva/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVADCNegCon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ivaretenido/100,2),round(precio*tbldevolucionesdetallesc.ivaretenido/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVARDCNegCon = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ieps/100,2),round(precio*tbldevolucionesdetallesc.ieps/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIEPSDCNegCon = Comm.ExecuteScalar

        'Llena variables Canceladas credito
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio,2),round(precio*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TSIDCNegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.iva/100,2),round(precio*tbldevolucionesdetallesc.iva/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVADCNegCr = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ivaretenido/100,2),round(precio*tbldevolucionesdetallesc.ivaretenido/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIVARDCNegCr = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ieps/100,2),round(precio*tbldevolucionesdetallesc.ieps/100*tbldevolucionescompras.tipodecambio,2))) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        Comm.CommandText += "),0)"
        TIEPSDCNegCr = Comm.ExecuteScalar

        TIVADCNeg = TIVADCNegCon + TIVADCNegCr
        TIEPSDCNeg = TIEPSDCNegCon + TIEPSDCNegCr
        TIVARDCNeg = TIVARDCNegCon + TIVARDCNegCr
        TIVANDCNeg = TIVADCNeg + TIEPSDCNeg - TIVARDCNeg
        TIVANDCNegCon = TIVADCNegCon + TIEPSDCNegCon - TIVARDCNegCon
        TIVANDCNegCr = TIVADCNegCr + TIEPSDCNegCr - TIVARDCNegCr
        TSIDCNeg = TSIDCNegCon + TSIDCNegCr
        TNdcNegCon = TSIDCNegCon + TIVANDCNegCon
        TNdcNegCr = TSIDCNegCr + TIVANDCNegCr
        TNDCNeg = TSIDCNeg + TIVANDCNeg

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        '***************Por dev contado***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio,2),round(precio*tbldevolucionescompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.iva/100,2),round(precio*tbldevolucionesdetallesc.iva/100*tbldevolucionescompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ivaretenido/100,2),round(precio*tbldevolucionesdetallesc.ivaretenido/100*tbldevolucionescompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ieps/100,2),round(precio*tbldevolucionesdetallesc.ieps/100*tbldevolucionescompras.tipodecambio,2))),0) as totalieps," +
            " tbldevolucionescompras.iddevolucion,ifnull(concat(tbldevolucionescompras.folio,' ',tbldevolucionescompras.serie),'*Nohay') as serie,tbldevolucionescompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tbldevolucionescompras.uuid,tbldevolucionescompras.idconversion idmoneda,tbldevolucionescompras.tipodecambio,tbldevolucionescompras.fecha,round(tbldevolucionescompras.totalapagar,2) totalapagar" +
            " from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0"
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevolucionescompras.iddevolucion order by tbldevolucionescompras.fecha,tbldevolucionescompras.serie,tbldevolucionescompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaDCCon.Clear()
        Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "DEV: " + DR("serie") + Format(DR("folioi"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("uuid")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaDCCon.Add(F)
            End If
        End While
        DR.Close()


        '***************Por dev credito***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio,2),round(precio*tbldevolucionescompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.iva/100,2),round(precio*tbldevolucionesdetallesc.iva/100*tbldevolucionescompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ivaretenido/100,2),round(precio*tbldevolucionesdetallesc.ivaretenido/100*tbldevolucionescompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ieps/100,2),round(precio*tbldevolucionesdetallesc.ieps/100*tbldevolucionescompras.tipodecambio,2))),0) as totalieps," +
            " tbldevolucionescompras.iddevolucion,ifnull(concat(tbldevolucionescompras.folio,' ',tbldevolucionescompras.serie),'*Nohay') as serie,tbldevolucionescompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tbldevolucionescompras.uuid,tbldevolucionescompras.idconversion idmoneda,tbldevolucionescompras.tipodecambio,tbldevolucionescompras.fecha,round(tbldevolucionescompras.totalapagar,2) totalapagar" +
            " from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevolucionescompras.iddevolucion order by tbldevolucionescompras.fecha,tbldevolucionescompras.serie,tbldevolucionescompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaDCCr.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "DEV: " + DR("serie") + Format(DR("folioi"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("uuid")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaDCCr.Add(F)
            End If
        End While
        DR.Close()

        '***************Por dev  todo***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio,2),round(precio*tbldevolucionescompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.iva/100,2),round(precio*tbldevolucionesdetallesc.iva/100*tbldevolucionescompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ivaretenido/100,2),round(precio*tbldevolucionesdetallesc.ivaretenido/100*tbldevolucionescompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ieps/100,2),round(precio*tbldevolucionesdetallesc.ieps/100*tbldevolucionescompras.tipodecambio,2))),0) as totalieps," +
            " tbldevolucionescompras.iddevolucion,ifnull(concat(tbldevolucionescompras.folio,' ',tbldevolucionescompras.serie),'*Nohay') as serie,tbldevolucionescompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tbldevolucionescompras.uuid,tbldevolucionescompras.idconversion idmoneda,tbldevolucionescompras.tipodecambio,tbldevolucionescompras.fecha,round(tbldevolucionescompras.totalapagar,2) totalapagar" +
            " from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevolucionescompras.iddevolucion order by tbldevolucionescompras.fecha,tbldevolucionescompras.serie,tbldevolucionescompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaDC.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "DEV: " + DR("serie") + Format(DR("folioi"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("uuid")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaDC.Add(F)
            End If
        End While
        DR.Close()

        '***************Por dev  cancelado***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio,2),round(precio*tbldevolucionescompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.iva/100,2),round(precio*tbldevolucionesdetallesc.iva/100*tbldevolucionescompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ivaretenido/100,2),round(precio*tbldevolucionesdetallesc.ivaretenido/100*tbldevolucionescompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ieps/100,2),round(precio*tbldevolucionesdetallesc.ieps/100*tbldevolucionescompras.tipodecambio,2))),0) as totalieps," +
            " tbldevolucionescompras.iddevolucion,ifnull(concat(tbldevolucionescompras.folio,' ',tbldevolucionescompras.serie),'*Nohay') as serie,tbldevolucionescompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tbldevolucionescompras.uuid,tbldevolucionescompras.idconversion idmoneda,tbldevolucionescompras.tipodecambio,tbldevolucionescompras.fecha,round(tbldevolucionescompras.totalapagar,2) totalapagar" +
            " from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevolucionescompras.iddevolucion order by tbldevolucionescompras.fecha,tbldevolucionescompras.serie,tbldevolucionescompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaDCNeg.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "DEV: " + DR("serie") + Format(DR("folioi"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("uuid")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaDCNeg.Add(F)
            End If
        End While
        DR.Close()

        '***************Por dev  ontado cancelado***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio,2),round(precio*tbldevolucionescompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.iva/100,2),round(precio*tbldevolucionesdetallesc.iva/100*tbldevolucionescompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ivaretenido/100,2),round(precio*tbldevolucionesdetallesc.ivaretenido/100*tbldevolucionescompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ieps/100,2),round(precio*tbldevolucionesdetallesc.ieps/100*tbldevolucionescompras.tipodecambio,2))),0) as totalieps," +
            " tbldevolucionescompras.iddevolucion,ifnull(concat(tbldevolucionescompras.folio,' ',tbldevolucionescompras.serie),'*Nohay') as serie,tbldevolucionescompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tbldevolucionescompras.uuid,tbldevolucionescompras.idconversion idmoneda,tbldevolucionescompras.tipodecambio,tbldevolucionescompras.fecha,round(tbldevolucionescompras.totalapagar,2) totalapagar" +
            " from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        'If pCredito = 0 Then
        Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevolucionescompras.iddevolucion order by tbldevolucionescompras.fecha,tbldevolucionescompras.serie,tbldevolucionescompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaDCNegCon.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "DEV: " + DR("serie") + Format(DR("folioi"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("uuid")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaDCNegCon.Add(F)
            End If
        End While
        DR.Close()

        '***************Por dev credito cancelado***********
        Comm.CommandText = "select ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio,2),round(precio*tbldevolucionescompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.iva/100,2),round(precio*tbldevolucionesdetallesc.iva/100*tbldevolucionescompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ivaretenido/100,2),round(precio*tbldevolucionesdetallesc.ivaretenido/100*tbldevolucionescompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tbldevolucionesdetallesc.idmoneda=2,round(precio*tbldevolucionesdetallesc.ieps/100,2),round(precio*tbldevolucionesdetallesc.ieps/100*tbldevolucionescompras.tipodecambio,2))),0) as totalieps," +
            " tbldevolucionescompras.iddevolucion,ifnull(concat(tbldevolucionescompras.folio,' ',tbldevolucionescompras.serie),'*Nohay') as serie,tbldevolucionescompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tbldevolucionescompras.uuid,tbldevolucionescompras.idconversion idmoneda,tbldevolucionescompras.tipodecambio,tbldevolucionescompras.fecha,round(tbldevolucionescompras.totalapagar,2) totalapagar" +
            " from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tbldevolucionescompras.iddevolucion order by tbldevolucionescompras.fecha,tbldevolucionescompras.serie,tbldevolucionescompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaDCNegCr.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = DR("totalieps")
                F.ISR = 0
                F.Retendido = DR("totalivaret")
                F.Concepto = "DEV: " + DR("serie") + Format(DR("folioi"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("iddevolucion")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("uuid")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaDCNegCr.Add(F)
            End If
        End While
        DR.Close()

        DocumentosProcesados(pModulo, pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
    End Sub
    Private Sub LlenaVariablesNotasdeCredito(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        'Facturas llenar variables****************************************FACTURAS***********
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecreditodetalles.idmoneda=2,round(tblnotasdecreditodetalles.precio/(1+tblnotasdecreditodetalles.iva/100),2),round(tblnotasdecreditodetalles.precio*tblnotasdecredito.tipodecambio/(1+tblnotasdecreditodetalles.iva/100),2))) from tblnotasdecreditodetalles inner join tblnotasdecredito on tblnotasdecreditodetalles.idnota=tblnotasdecredito.idnota where estado=3"
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSINC = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecreditodetalles.idmoneda=2,round(tblnotasdecreditodetalles.precio-tblnotasdecreditodetalles.precio/(1+tblnotasdecreditodetalles.iva/100),2),round(tblnotasdecreditodetalles.precio-tblnotasdecreditodetalles.precio*tblnotasdecredito.tipodecambio/(1+tblnotasdecreditodetalles.iva/100),2))) from tblnotasdecreditodetalles inner join tblnotasdecredito on tblnotasdecreditodetalles.idnota=tblnotasdecredito.idnota where estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdSucursal.ToString
        End If

        Comm.CommandText += "),0)"
        TIVANNC = Comm.ExecuteScalar

        'TIVAN = TIVA
        TNNC = TSINC + TIVANNC '- TISR - TIVAR + TIEPS

        'Llena variables Canceladas
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecreditodetalles.idmoneda=2,round(tblnotasdecreditodetalles.precio/(1+tblnotasdecreditodetalles.iva/100),2),round(tblnotasdecreditodetalles.precio/(1+tblnotasdecreditodetalles.iva/100)*tblnotasdecredito.tipodecambio,2))) from tblnotasdecreditodetalles inner join tblnotasdecredito on tblnotasdecreditodetalles.idnota=tblnotasdecredito.idnota where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdSucursal.ToString
        End If

        Comm.CommandText += "),0)"
        TSINCNeg = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecreditodetalles.idmoneda=2,round(tblnotasdecreditodetalles.precio-tblnotasdecreditodetalles.precio/(1+tblnotasdecreditodetalles.iva/100),2),round((tblnotasdecreditodetalles.precio-tblnotasdecreditodetalles.precio/(1+tblnotasdecreditodetalles.iva/100))*tblnotasdecredito.tipodecambio,2))) from tblnotasdecreditodetalles inner join tblnotasdecredito on tblnotasdecreditodetalles.idnota=tblnotasdecredito.idnota where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdSucursal.ToString
        End If

        Comm.CommandText += "),0)"
        TIVANNCNeg = Comm.ExecuteScalar

        'TIVANNeg = TIVANeg
        TNNCNeg = TSINCNeg + TIVANNCNeg '- TISRNeg - TIVARNeg + TIEPSNeg

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        '***************Por nota***********
        Comm.CommandText = "select ifnull(sum(if(tblnotasdecreditodetalles.idmoneda=2,round(precio/(1+tblnotasdecreditodetalles.iva/100),2),round((precio/(1+tblnotasdecreditodetalles.iva/100))*tblnotasdecredito.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblnotasdecreditodetalles.idmoneda=2,round(precio-precio/(1+tblnotasdecreditodetalles.iva/100),2),round((precio-precio/(1+tblnotasdecreditodetalles.iva/100))*tblnotasdecredito.tipodecambio,2))),0) as totaliva," +
            " tblnotasdecredito.idnota,ifnull(tblnotasdecredito.serie,'*Nohay') as serie,tblnotasdecredito.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tblnotasdecredito.tipodecambio,tblnotasdecredito.idmoneda,tblnotasdecredito.fecha,ifnull((select tblnotasdecreditotimbrado.uuid from tblnotasdecreditotimbrado where tblnotasdecreditotimbrado.idnota=tblnotasdecredito.idnota),'') as foliocfdi,round(tblnotasdecredito.totalapagar,2) totalapagar" +
            " from tblnotasdecreditodetalles inner join tblnotasdecredito on tblnotasdecreditodetalles.idnota=tblnotasdecredito.idnota inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente where tblnotasdecredito.estado=3"

        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnotasdecredito.fecha>='" + pFecha1 + "' and tblnotasdecredito.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnotasdecredito.idnota order by tblnotasdecredito.fecha,tblnotasdecredito.serie,tblnotasdecredito.folio"

        DR = Comm.ExecuteReader
        FacturasListaNC.Clear()
        Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = 0
                F.ISR = 0
                F.Retendido = 0
                F.Concepto = "NCA: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idnota")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaNC.Add(F)
            End If
        End While
        DR.Close()

        '***************Por nota***********
        Comm.CommandText = "select ifnull(sum(if(tblnotasdecreditodetalles.idmoneda=2,round(precio/(1+tblnotasdecreditodetalles.iva/100),2),round(precio/(1+tblnotasdecreditodetalles.iva/100)*tblnotasdecredito.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblnotasdecreditodetalles.idmoneda=2,round(precio-precio/(1+tblnotasdecreditodetalles.iva/100),2),round((precio-precio/(1+tblnotasdecreditodetalles.iva/100))*tblnotasdecredito.tipodecambio,2))),0) as totaliva," +
            " tblnotasdecredito.idnota,ifnull(tblnotasdecredito.serie,'*Nohay') as serie,tblnotasdecredito.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tblnotasdecredito.tipodecambio,tblnotasdecredito.idmoneda,tblnotasdecredito.fecha,ifnull((select tblnotasdecreditotimbrado.uuid from tblnotasdecreditotimbrado where tblnotasdecreditotimbrado.idnota=tblnotasdecredito.idnota),'') as foliocfdi,round(tblnotasdecredito.totalapagar,2) totalapagar" +
            " from tblnotasdecreditodetalles inner join tblnotasdecredito on tblnotasdecreditodetalles.idnota=tblnotasdecredito.idnota inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente where tblnotasdecredito.estado=4"

        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnotasdecredito.idnota order by tblnotasdecredito.fecha,tblnotasdecredito.serie,tblnotasdecredito.folio"

        DR = Comm.ExecuteReader
        FacturasListaNCNeg.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = 0
                F.ISR = 0
                F.Retendido = 0
                F.Concepto = "NCA: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idnota")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaNCNeg.Add(F)
            End If
        End While
        DR.Close()

        DocumentosProcesados(pModulo, pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
    End Sub
    Private Sub LlenaVariablesNotasdeCreditoCompras(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        'Facturas llenar variables****************************************FACTURAS***********
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecreditodetallesc.idmoneda=2,round(tblnotasdecreditodetallesc.precio/(1+tblnotasdecreditodetallesc.iva/100),2),round(tblnotasdecreditodetallesc.precio/(1+tblnotasdecreditodetallesc.iva/100)*tblnotasdecreditocompras.tipodecambio,2))) from tblnotasdecreditodetallesc inner join tblnotasdecreditocompras on tblnotasdecreditodetallesc.idnota=tblnotasdecreditocompras.idnota where estado=3"
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSINCC = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecreditodetallesc.idmoneda=2,round(precio-precio/(1+tblnotasdecreditodetallesc.iva/100),2),round((tblnotasdecreditodetallesc.precio-precio/(1+tblnotasdecreditodetallesc.iva/100))*tblnotasdecreditodetallesc.iva/100*tblnotasdecreditocompras.tipodecambio,2))) from tblnotasdecreditodetallesc inner join tblnotasdecreditocompras on tblnotasdecreditodetallesc.idnota=tblnotasdecreditocompras.idnota where estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdSucursal.ToString
        End If

        Comm.CommandText += "),0)"
        TIVANNCC = Comm.ExecuteScalar

        'TIVAN = TIVA
        TNNCC = TSINCC + TIVANNCC '- TISR - TIVAR + TIEPS

        'Llena variables Canceladas
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecreditodetallesc.idmoneda=2,round(precio/(1+tblnotasdecreditodetallesc.iva/100),2),round(precio/(1+tblnotasdecreditodetallesc.iva/100)*tblnotasdecreditocompras.tipodecambio,2))) from tblnotasdecreditodetallesc inner join tblnotasdecreditocompras on tblnotasdecreditodetallesc.idnota=tblnotasdecreditocompras.idnota where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        Comm.CommandText += "),0)"
        TSINCCNeg = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecreditodetallesc.idmoneda=2,round(precio-precio/(1+tblnotasdecreditodetallesc.iva/100),2),round((precio-precio/(1+tblnotasdecreditodetallesc.iva/100))*tblnotasdecreditocompras.tipodecambio,2))) from tblnotasdecreditodetallesc inner join tblnotasdecreditocompras on tblnotasdecreditodetallesc.idnota=tblnotasdecreditocompras.idnota where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdSucursal.ToString
        End If

        Comm.CommandText += "),0)"
        TIVANNCCNeg = Comm.ExecuteScalar

        'TIVANNeg = TIVANeg
        TNNCCNeg = TSINCCNeg + TIVANNCCNeg '- TISRNeg - TIVARNeg + TIEPSNeg

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        '***************Por nota***********
        Comm.CommandText = "select ifnull(sum(if(tblnotasdecreditodetallesc.idmoneda=2,round(precio/(1+tblnotasdecreditodetallesc.iva/100),2),round(precio/(1+tblnotasdecreditodetallesc.iva/100)*tblnotasdecreditocompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblnotasdecreditodetallesc.idmoneda=2,round(precio-precio/(1+tblnotasdecreditodetallesc.iva/100),2),round((precio-precio/(1+tblnotasdecreditodetallesc.iva/100))*tblnotasdecreditocompras.tipodecambio,2))),0) as totaliva," +
            " tblnotasdecreditocompras.idnota,ifnull(concat(tblnotasdecreditocompras.folio,' ',tblnotasdecreditocompras.serie),'*Nohay') as serie,tblnotasdecreditocompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tblnotasdecreditocompras.foliocfdi,tblnotasdecreditocompras.idmoneda,tblnotasdecreditocompras.tipodecambio,tblnotasdecreditocompras.fecha,round(tblnotasdecreditocompras.totalapagar,2) totalapagar" +
            " from tblnotasdecreditodetallesc inner join tblnotasdecreditocompras on tblnotasdecreditodetallesc.idnota=tblnotasdecreditocompras.idnota inner join tblproveedores on tblnotasdecreditocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecreditocompras.estado=3"

        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.fecha>='" + pFecha1 + "' and tblnotasdecreditocompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnotasdecreditocompras.idnota order by tblnotasdecreditocompras.fecha,tblnotasdecreditocompras.serie,tblnotasdecreditocompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaNCC.Clear()
        Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = 0
                F.ISR = 0
                F.Retendido = 0
                F.Concepto = "NCA: " + DR("serie") + Format(DR("folioi"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idnota")

                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaNCC.Add(F)
            End If
        End While
        DR.Close()

        '***************Por nota cancelado***********
        Comm.CommandText = "select ifnull(sum(if(tblnotasdecreditodetallesc.idmoneda=2,round(precio/(1+tblnotasdecreditodetallesc.iva/100),2),round(precio/(1+tblnotasdecreditodetallesc.iva/100)*tblnotasdecreditocompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblnotasdecreditodetallesc.idmoneda=2,round(precio-precio/(1+tblnotasdecreditodetallesc.iva/100),2),round((precio-precio/(1+tblnotasdecreditodetallesc.iva/100))*tblnotasdecreditocompras.tipodecambio,2))),0) as totaliva," +
            " tblnotasdecreditocompras.idnota,ifnull(concat(tblnotasdecreditocompras.folio,' ',tblnotasdecreditocompras.serie),'*Nohay') as serie,tblnotasdecreditocompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tblnotasdecreditocompras.foliocfdi,tblnotasdecreditocompras.idmoneda,tblnotasdecreditocompras.tipodecambio,tblnotasdecreditocompras.fecha,round(tblnotasdecreditocompras.totalapagar,2) totalapagar" +
            " from tblnotasdecreditodetallesc inner join tblnotasdecreditocompras on tblnotasdecreditodetallesc.idnota=tblnotasdecreditocompras.idnota inner join tblproveedores on tblnotasdecreditocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecreditocompras.estado=4"

        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnotasdecreditocompras.idnota order by tblnotasdecreditocompras.fecha,tblnotasdecreditocompras.serie,tblnotasdecreditocompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaNCCNeg.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = 0
                F.ISR = 0
                F.Retendido = 0
                F.Concepto = "NCA: " + DR("serie") + Format(DR("folioi"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idnota")
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0

                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaNCCNeg.Add(F)
            End If
        End While
        DR.Close()


        DocumentosProcesados(pModulo, pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
    End Sub

    Private Sub LlenaVariablesNotasdeCargo(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        'Facturas llenar variables****************************************FACTURAS***********
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecargodetalles.idmoneda=2,round(tblnotasdecargodetalles.precio,2),round(tblnotasdecargodetalles.precio*tblnotasdecargo.tipodecambio,2))) from tblnotasdecargodetalles inner join tblnotasdecargo on tblnotasdecargodetalles.idcargo=tblnotasdecargo.idcargo where estado=3"
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSINCA = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecargodetalles.idmoneda=2,round(precio*tblnotasdecargodetalles.iva/100,2),round(tblnotasdecargodetalles.precio*tblnotasdecargodetalles.iva/100*tblnotasdecargo.tipodecambio,2))) from tblnotasdecargodetalles inner join tblnotasdecargo on tblnotasdecargodetalles.idcargo=tblnotasdecargo.idcargo where estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        Comm.CommandText += "),0)"
        TIVANNCA = Comm.ExecuteScalar

        'TIVANNCA = TIVA
        TNNCA = TSINCA + TIVANNCA '- TISR - TIVAR + TIEPS

        'Llena variables Canceladas
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecargodetalles.idmoneda=2,round(precio,2),round(precio*tblnotasdecargo.tipodecambio,2))) from tblnotasdecargodetalles inner join tblnotasdecargo on tblnotasdecargodetalles.idcargo=tblnotasdecargo.idcargo where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        Comm.CommandText += "),0)"
        TSINCANeg = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecargodetalles.idmoneda=2,round(precio*tblnotasdecargodetalles.iva/100,2),round(precio*tblnotasdecargodetalles.iva/100*tblnotasdecargo.tipodecambio,2))) from tblnotasdecargodetalles inner join tblnotasdecargo on tblnotasdecargodetalles.idcargo=tblnotasdecargo.idcargo where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        Comm.CommandText += "),0)"
        TIVANNCANeg = Comm.ExecuteScalar

        TNNCANeg = TSINCANeg + TIVANNCANeg '- TISRNeg - TIVARNeg + TIEPSNeg

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        '***************Por nota***********
        Comm.CommandText = "select ifnull(sum(if(tblnotasdecargodetalles.idmoneda=2,round(tblnotasdecargodetalles.precio,2),round(tblnotasdecargodetalles.precio*tblnotasdecargo.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblnotasdecargodetalles.idmoneda=2,round(precio*tblnotasdecargodetalles.iva/100,2),round(precio*tblnotasdecargodetalles.iva/100*tblnotasdecargo.tipodecambio,2))),0) as totaliva," +
            " tblnotasdecargo.idcargo,ifnull(tblnotasdecargo.serie,'*Nohay') as serie,tblnotasdecargo.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tblnotasdecargo.tipodecambio,tblnotasdecargo.idmoneda,tblnotasdecargo.fecha,ifnull((select tblnotasdecargotimbrado.uuid from tblnotasdecargotimbrado where tblnotasdecargotimbrado.idcargo=tblnotasdecargo.idcargo),'') as foliocfdi,round(tblnotasdecargo.totalapagar,2) totalapagar" +
            " from tblnotasdecargodetalles inner join tblnotasdecargo on tblnotasdecargodetalles.idcargo=tblnotasdecargo.idcargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.estado=3"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnotasdecargo.fecha>='" + pFecha1 + "' and tblnotasdecargo.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnotasdecargo.idcargo order by tblnotasdecargo.fecha,tblnotasdecargo.serie,tblnotasdecargo.folio"

        DR = Comm.ExecuteReader
        FacturasListaNCA.Clear()
        Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = 0
                F.ISR = 0
                F.Retendido = 0
                F.Concepto = "NCA: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idcargo")

                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""

                FacturasListaNCA.Add(F)
            End If
        End While
        DR.Close()

        '***************Por nota cancelados***********
        Comm.CommandText = "select ifnull(sum(if(tblnotasdecargodetalles.idmoneda=2,round(tblnotasdecargodetalles.precio,2),round(tblnotasdecargodetalles.precio*tblnotasdecargo.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblnotasdecargodetalles.idmoneda=2,round(precio*tblnotasdecargodetalles.iva/100,2),round(precio*tblnotasdecargodetalles.iva/100*tblnotasdecargo.tipodecambio,2))),0) as totaliva," +
            " tblnotasdecargo.idcargo,ifnull(tblnotasdecargo.serie,'*Nohay') as serie,tblnotasdecargo.folio,tblclientes.idcuenta," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tblnotasdecargo.tipodecambio,tblnotasdecargo.idmoneda,tblnotasdecargo.fecha,ifnull((select tblnotasdecargotimbrado.uuid from tblnotasdecargotimbrado where tblnotasdecargotimbrado.idcargo=tblnotasdecargo.idcargo),'') as foliocfdi,round(tblnotasdecargo.totalapagar,2) totalapagar" +
            " from tblnotasdecargodetalles inner join tblnotasdecargo on tblnotasdecargodetalles.idcargo=tblnotasdecargo.idcargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.estado=4"
        'If pCredito = 0 Then
        'Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        'If pCredito = 1 Then
        'Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnotasdecargo.idcargo order by tblnotasdecargo.fecha,tblnotasdecargo.serie,tblnotasdecargo.folio"

        DR = Comm.ExecuteReader
        FacturasListaNCANeg.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = 0
                F.ISR = 0
                F.Retendido = 0
                F.Concepto = "NCA: " + DR("serie") + Format(DR("folio"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idcargo")

                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""

                FacturasListaNCANeg.Add(F)
            End If
        End While
        DR.Close()


        DocumentosProcesados(pModulo, pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
    End Sub

    Private Sub LlenaVariablesNotasdeCargoCompras(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        'Facturas llenar variables****************************************FACTURAS***********
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecargodetallesc.idmoneda=2,round(tblnotasdecargodetallesc.precio,2),round(tblnotasdecargodetallesc.precio*tblnotasdecargocompras.tipodecambio,2))) from tblnotasdecargodetallesc inner join tblnotasdecargocompras on tblnotasdecargodetallesc.idcargo=tblnotasdecargocompras.idcargo where estado=3"
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += "),0)"
        TSINCAC = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecargodetallesc.idmoneda=2,round(precio*tblnotasdecargodetallesc.iva/100,2),round(tblnotasdecargodetallesc.precio*tblnotasdecargodetallesc.iva/100*tblnotasdecargocompras.tipodecambio,2))) from tblnotasdecargodetallesc inner join tblnotasdecargocompras on tblnotasdecargodetallesc.idcargo=tblnotasdecargocompras.idcargo where estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        Comm.CommandText += "),0)"
        TIVANNCAC = Comm.ExecuteScalar

        'TIVAN = TIVA
        TNNCAC = TSINCAC + TIVANNCAC '- TISR - TIVAR + TIEPS

        'Llena variables Canceladas
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecargodetallesc.idmoneda=2,round(precio,2),round(precio*tblnotasdecargocompras.tipodecambio,2))) from tblnotasdecargodetallesc inner join tblnotasdecargocompras on tblnotasdecargodetallesc.idcargo=tblnotasdecargocompras.idcargo where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
        End If

        Comm.CommandText += "),0)"
        TSINCACNeg = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblnotasdecargodetallesc.idmoneda=2,round(precio*tblnotasdecargodetallesc.iva/100,2),round(precio*tblnotasdecargodetallesc.iva/100*tblnotasdecargocompras.tipodecambio,2))) from tblnotasdecargodetallesc inner join tblnotasdecargocompras on tblnotasdecargodetallesc.idcargo=tblnotasdecargocompras.idcargo where estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
        End If

        Comm.CommandText += "),0)"
        TIVANNCACNeg = Comm.ExecuteScalar

        'TIVANNCACNeg = TIVANeg
        TNNCACNeg = TSINCACNeg + TIVANNCACNeg '- TISRNeg - TIVARNeg + TIEPSNeg

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        '***************Por nota***********
        Comm.CommandText = "select ifnull(sum(if(tblnotasdecargodetallesc.idmoneda=2,round(tblnotasdecargodetallesc.precio,2),round(tblnotasdecargodetallesc.precio*tblnotasdecargocompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblnotasdecargodetallesc.idmoneda=2,round(precio*tblnotasdecargodetallesc.iva/100,2),round(precio*tblnotasdecargodetallesc.iva/100*tblnotasdecargocompras.tipodecambio,2))),0) as totaliva," +
            " tblnotasdecargocompras.idcargo,ifnull(concat(tblnotasdecargocompras.folio,' ',tblnotasdecargocompras.serie),'*Nohay') as serie,tblnotasdecargocompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tblnotasdecargocompras.foliocfdi,tblnotasdecargocompras.idmoneda,tblnotasdecargocompras.tipodecambio,tblnotasdecargocompras.fecha,round(tblnotasdecargocompras.totalapagar,2) totalapagar" +
            " from tblnotasdecargodetallesc inner join tblnotasdecargocompras on tblnotasdecargodetallesc.idcargo=tblnotasdecargocompras.idcargo inner join tblproveedores on tblnotasdecargocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecargocompras.estado=3"

        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.fecha>='" + pFecha1 + "' and tblnotasdecargocompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnotasdecargocompras.idcargo order by tblnotasdecargocompras.fecha,tblnotasdecargocompras.serie,tblnotasdecargocompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaNCAC.Clear()
        Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = 0
                F.ISR = 0
                F.Retendido = 0
                F.Concepto = "NCA: " + DR("serie") + Format(DR("folioi"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idcargo")

                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaNCAC.Add(F)
            End If
        End While
        DR.Close()


        '***************Por nota cancelados***********
        Comm.CommandText = "select ifnull(sum(if(tblnotasdecargodetallesc.idmoneda=2,round(tblnotasdecargodetallesc.precio,2),round(tblnotasdecargodetallesc.precio*tblnotasdecargocompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblnotasdecargodetallesc.idmoneda=2,round(precio*tblnotasdecargodetallesc.iva/100,2),round(precio*tblnotasdecargodetallesc.iva/100*tblnotasdecargocompras.tipodecambio,2))),0) as totaliva," +
            " tblnotasdecargocompras.idcargo,ifnull(concat(tblnotasdecargocompras.folio,' ',tblnotasdecargocompras.serie),'*Nohay') as serie,tblnotasdecargocompras.folioi,tblproveedores.idcuenta," +
            "if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tblnotasdecargocompras.foliocfdi,tblnotasdecargocompras.idmoneda,tblnotasdecargocompras.tipodecambio,tblnotasdecargocompras.fecha,round(tblnotasdecargocompras.totalapagar,2) totalapagar" +
            " from tblnotasdecargodetallesc inner join tblnotasdecargocompras on tblnotasdecargodetallesc.idcargo=tblnotasdecargocompras.idcargo inner join tblproveedores on tblnotasdecargocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecargocompras.estado=4"

        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by tblnotasdecargocompras.idcargo order by tblnotasdecargocompras.fecha,tblnotasdecargocompras.serie,tblnotasdecargocompras.folioi"

        DR = Comm.ExecuteReader
        FacturasListaNCACNeg.Clear()
        'Dim F As stDepositos
        While DR.Read
            If DR("serie") <> "*Nohay" Then
                F.Cantidad = DR("subtotal")
                F.Iva = DR("totaliva")
                F.Ieps = 0
                F.ISR = 0
                F.Retendido = 0
                F.Concepto = "NCA: " + DR("serie") + Format(DR("folioi"), "000")
                F.Cuenta = DR("cuenta")
                F.Idcuenta = DR("idcuenta")
                F.IdDeposito = DR("idcargo")

                F.ChequeTrans.BancoDestinoEx = ""
                F.ChequeTrans.BancoEx = ""
                F.ChequeTrans.BancoOrigenEx = ""
                F.ChequeTrans.Beneficiario = ""
                F.ChequeTrans.Fecha = ""
                F.ChequeTrans.IdBancoDestino = 0
                F.ChequeTrans.IdBancoOrigen = 0
                F.ChequeTrans.IdMoneda = 0
                F.ChequeTrans.Monto = 0
                F.ChequeTrans.NoCuentaDestino = ""
                F.ChequeTrans.NoCuentaOrigen = ""
                F.ChequeTrans.NumeroCheque = ""
                F.ChequeTrans.RFC = ""
                F.ChequeTrans.TipodeCambio = 0
                F.Folio = ""
                F.IdProveedor = 0
                F.IvaPor = 0
                F.IVaRetPor = 0
                F.IEpsPor = 0
                F.Uuid.Uuid = DR("foliocfdi")
                F.Uuid.Fecha = DR("fecha")
                F.Uuid.Monto = DR("totalapagar")
                F.Uuid.RFC = DR("rfc")
                F.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    F.Uuid.IdMoneda = 100
                Else
                    F.Uuid.IdMoneda = 147
                End If
                F.NombreProveedor = ""
                FacturasListaNCACNeg.Add(F)
            End If
        End While
        DR.Close()

        DocumentosProcesados(pModulo, pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
    End Sub

    Public Function DaValorVariable(pCodigo As String, pModulo As Byte, pNegativo As Boolean) As Double
        'Select Case pModulo
        '   Case 0

        'Public TDSI As Double

        If pNegativo = False Then
            Select Case pCodigo
                Case "TIVAN"
                    Select Case pModulo
                        Case 0
                            Return TIVAN
                        Case 1
                            Return TIVANC
                        Case 2
                            Return TIVAND
                        Case 3
                            Return TIVANDC
                        Case 4
                            Return TIVANDep
                        Case 5
                            Return TIVANRet
                        Case 6
                            Return TIVANNC
                        Case 7
                            Return TIVANNCC
                        Case 8
                            Return TIVANNCA
                        Case 9
                            Return TIVANNCAC
                        Case 10
                            Return TIVAN
                    End Select
                Case "TN"
                    Select Case pModulo
                        Case 0
                            Return TN
                        Case 1
                            Return TNC
                        Case 2
                            Return TND
                        Case 3
                            Return TNDC
                        Case 4
                            Return TNDep
                        Case 5
                            Return TNRet
                        Case 6
                            Return TNNC
                        Case 7
                            Return TNNCC
                        Case 8
                            Return TNNCA
                        Case 9
                            Return TNNCAC
                        Case 10
                            Return TNNO
                    End Select
                Case "TSI"
                    Select Case pModulo
                        Case 0
                            Return TSI
                        Case 1
                            Return TSIC
                        Case 2
                            Return TSID
                        Case 3
                            Return TSIDC
                        Case 4
                            Return TSIDep
                        Case 5
                            Return TSIRet
                        Case 6
                            Return TSINC
                        Case 7
                            Return TSINCC
                        Case 8
                            Return TSINCA
                        Case 9
                            Return TSINCAC
                        Case 10
                            Return 0
                    End Select
                Case "TIVA"
                    Select Case pModulo
                        Case 0
                            Return TIVA
                        Case 1
                            Return TIVAC
                        Case 2
                            Return TIVAD
                        Case 3
                            Return TIVADC
                        Case 4
                            Return TIVADep
                        Case 5
                            Return TIVARet
                        Case 6
                            Return TIVANC
                        Case 7
                            Return TIVANNCC
                        Case 8
                            Return TIVANNCA
                        Case 9
                            Return TIVANNCAC
                        Case 10
                            Return 0
                    End Select
                Case "TIVAR"
                    Select Case pModulo
                        Case 0
                            Return TIVAR
                        Case 1
                            Return TIVARC
                        Case 2
                            Return TIVARD
                        Case 3
                            Return TIVARDC
                        Case 4
                            Return TIVARDep
                        Case 5
                            Return TIVARRet
                        Case 6
                            Return 0
                        Case 7
                            Return 0
                        Case 8
                            Return 0
                        Case 9
                            Return 0
                        Case 10
                            Return 0
                    End Select
                Case "TISR"
                    Select Case pModulo
                        Case 0
                            Return TISR
                        Case 1
                            Return 0
                        Case 2
                            Return TISRD
                        Case 3
                            Return 0
                        Case 4
                            Return TISRDep
                        Case 5
                            Return 0
                        Case 6
                            Return 0
                        Case 7
                            Return 0
                        Case 8
                            Return 0
                        Case 9
                            Return 0
                        Case 10
                            Return 0
                    End Select
                Case "TIEPS"
                    Select Case pModulo
                        Case 0
                            Return TIEPS
                        Case 1
                            Return TIEPSC
                        Case 2
                            Return TIEPSD
                        Case 3
                            Return TIEPSDC
                        Case 4
                            Return TIEPSDep
                        Case 5
                            Return TIEPSRet
                        Case 6
                            Return 0
                        Case 7
                            Return 0
                        Case 8
                            Return 0
                        Case 9
                            Return 0
                        Case 10
                            Return 0
                    End Select
                Case "TP"
                    Return TP
                Case "TD"
                    Return TD
                Case "TPG"
                    Return TPG
                Case "TPE"
                    Return TPE
                Case "TDG"
                    Return TDG
                Case "TDE"
                    Return TDE
                Case "TI"
                    Return TI
                Case "THE"
                    Return THE

                Case "TIVANCon"
                    Select Case pModulo
                        Case 0
                            Return TIVANCon
                        Case 1
                            Return TIVANCCon
                        Case 2
                            Return TIVANDCon
                        Case 3
                            Return TIVANDCCon
                        Case 4
                            Return TIVANDepCon
                        Case 5
                            Return TIVANRetCon
                        Case 6
                            Return 0
                        Case 7
                            Return 0
                        Case 8
                            Return 0
                        Case 9
                            Return 0
                        Case 10
                            Return 0
                    End Select
                Case "TNCon"
                    Select Case pModulo
                        Case 0
                            Return TNCon
                        Case 1
                            Return TNCCon
                        Case 2
                            Return TNDCon
                        Case 3
                            Return TNDCCon
                        Case 4
                            Return TNDepCon
                        Case 5
                            Return TNRetCon
                        Case 6
                            Return 0
                        Case 7
                            Return 0
                        Case 8
                            Return 0
                        Case 9
                            Return 0
                        Case 10
                            Return 0
                    End Select
                Case "TSICon"
                    Select Case pModulo
                        Case 0
                            Return TSICon
                        Case 1
                            Return TSICCon
                        Case 2
                            Return TSIDCon
                        Case 3
                            Return TSIDCCon
                        Case 4
                            Return TSIDepCon
                        Case 5
                            Return TSIRetCon
                    End Select
                Case "TIVACon"
                    Select Case pModulo
                        Case 0
                            Return TIVACon
                        Case 1
                            Return TIVACCon
                        Case 2
                            Return TIVADCon
                        Case 3
                            Return TIVADCCon
                        Case 4
                            Return TIVADepCon
                        Case 5
                            Return TIVARetCon
                    End Select
                Case "TIVARCon"
                    Select Case pModulo
                        Case 0
                            Return TIVARCon
                        Case 1
                            Return TIVARCCon
                        Case 2
                            Return TIVARDCon
                        Case 3
                            Return TIVARDCCon
                        Case 4
                            Return TIVARDepCon
                        Case 5
                            Return TIVARRetCon
                    End Select
                Case "TISRCon"
                    Select Case pModulo
                        Case 0
                            Return TISRCon
                        Case 1
                            Return 0
                        Case 2
                            Return TISRDCon
                        Case 3
                            Return 0
                        Case 4
                            Return TISRDepCon
                        Case 5
                            Return 0
                    End Select
                Case "TIEPSCon"
                    Select Case pModulo
                        Case 0
                            Return TIEPSCon
                        Case 1
                            Return TIEPSCCon
                        Case 2
                            Return TIEPSDCon
                        Case 3
                            Return TIEPSDCCon
                        Case 4
                            Return TIEPSDepCon
                        Case 5
                            Return TIEPSRetCon
                    End Select

                Case "TIVANCr"
                    Select Case pModulo
                        Case 0
                            Return TIVANCr
                        Case 1
                            Return TIVANCCr
                        Case 2
                            Return TIVANDCr
                        Case 3
                            Return TIVANDCCr
                        Case 4
                            Return TIVANDepCr
                        Case 5
                            Return TIVANRetCr
                    End Select
                Case "TNCr"
                    Select Case pModulo
                        Case 0
                            Return TNCr
                        Case 1
                            Return TNCCr
                        Case 2
                            Return TNDCr
                        Case 3
                            Return TNDCCr
                        Case 4
                            Return TNDepCr
                        Case 5
                            Return TNRetCr
                    End Select
                Case "TSICr"
                    Select Case pModulo
                        Case 0
                            Return TSICr
                        Case 1
                            Return TSICCr
                        Case 2
                            Return TSIDCr
                        Case 3
                            Return TSIDCCr
                        Case 4
                            Return TSIDepCr
                        Case 5
                            Return TSIRetCr
                    End Select
                Case "TIVACr"
                    Select Case pModulo
                        Case 0
                            Return TIVACr
                        Case 1
                            Return TIVACCr
                        Case 2
                            Return TIVADCr
                        Case 3
                            Return TIVADCCr
                        Case 4
                            Return TIVADepCr
                        Case 5
                            Return TIVARetCr
                    End Select
                Case "TIVARCr"
                    Select Case pModulo
                        Case 0
                            Return TIVARCr
                        Case 1
                            Return TIVARCCr
                        Case 2
                            Return TIVARDCr
                        Case 3
                            Return TIVARDCCr
                        Case 4
                            Return TIVARDepCr
                        Case 5
                            Return TIVARRetCr
                    End Select
                Case "TISRCr"
                    Select Case pModulo
                        Case 0
                            Return TISRCr
                        Case 1
                            Return 0
                        Case 2
                            Return TISRDCr
                        Case 3
                            Return 0
                        Case 4
                            Return TISRDepCr
                        Case 5
                            Return 0
                    End Select
                Case "TIEPSCr"
                    Select Case pModulo
                        Case 0
                            Return TIEPSCr
                        Case 1
                            Return TIEPSCCr
                        Case 2
                            Return TIEPSDCr
                        Case 3
                            Return TIEPSDCCr
                        Case 4
                            Return TIEPSDepCr
                        Case 5
                            Return TIEPSRetCr
                    End Select
                Case "TNSL"
                    Select Case pModulo
                        Case 4
                            Return TNSLDep
                        Case 5
                            Return TNSLRet
                    End Select
                Case "TIVASL"
                    Select Case pModulo
                        Case 4
                            Return TIVASLDep
                        Case 5
                            Return TIVASLRet
                    End Select
                Case "TSISL"
                    Select Case pModulo
                        Case 4
                            Return TSISLDep
                        Case 5
                            Return TSISLRet
                    End Select
            End Select
        Else
            '+++++++++++++++++++++++++++++++++++++++++
            '+++++++++++++++++++++++++++++++++++++++++++
            '+++++++++++++++NEGATIVOS+++++++++++++++++++++
            '++++++++++++++++++++++++++++++++++++++++++
            Select Case pCodigo
                Case "TIVAN"
                    Select Case pModulo
                        Case 0
                            Return TIVANNeg
                        Case 1
                            Return TIVANCNeg
                        Case 2
                            Return TIVANDNeg
                        Case 3
                            Return TIVANDCNeg
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                        Case 6
                            Return TIVANNCNeg
                        Case 7
                            Return TIVANNCCNeg
                        Case 8
                            Return TIVANNCANeg
                        Case 9
                            Return TIVANNCACNeg
                        Case 10
                            Return TIVANNeg
                    End Select
                Case "TN"
                    Select Case pModulo
                        Case 0
                            Return TNNeg
                        Case 1
                            Return TNCNeg
                        Case 2
                            Return TNDNeg
                        Case 3
                            Return TNDCNeg
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                        Case 6
                            Return TNNCNeg
                        Case 7
                            Return TNNCCNeg
                        Case 8
                            Return TNNCANeg
                        Case 9
                            Return TNNCACNeg
                        Case 10
                            Return TNNONeg
                    End Select
                Case "TSI"
                    Select Case pModulo
                        Case 0
                            Return TSINeg
                        Case 1
                            Return TSICNeg
                        Case 2
                            Return TSIDNeg
                        Case 3
                            Return TSIDCNeg
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                        Case 6
                            Return TSINCNeg
                        Case 7
                            Return TSINCCNeg
                        Case 8
                            Return TSINCANeg
                        Case 9
                            Return TSINCACNeg
                        Case 10
                            Return 0
                    End Select
                Case "TIVA"
                    Select Case pModulo
                        Case 0
                            Return TIVANeg
                        Case 1
                            Return TIVACNeg
                        Case 2
                            Return TIVADNeg
                        Case 3
                            Return TIVADCNeg
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                        Case 6
                            Return TIVANCNeg
                        Case 7
                            Return TIVANNCCNeg
                        Case 8
                            Return TIVANNCANeg
                        Case 9
                            Return TIVANNCACNeg
                        Case 10
                            Return 0
                    End Select
                Case "TIVAR"
                    Select Case pModulo
                        Case 0
                            Return TIVARNeg
                        Case 1
                            Return TIVARCNeg
                        Case 2
                            Return TIVARDNeg
                        Case 3
                            Return TIVARDCNeg
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                        Case 6
                            Return 0
                        Case 7
                            Return 0
                        Case 8
                            Return 0
                        Case 9
                            Return 0
                        Case 10
                            Return 0
                    End Select
                Case "TISR"
                    Select Case pModulo
                        Case 0
                            Return TISRNeg
                        Case 1
                            Return 0
                        Case 2
                            Return TISRDNeg
                        Case 3
                            Return 0
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                        Case 6
                            Return 0
                        Case 7
                            Return 0
                        Case 8
                            Return 0
                        Case 9
                            Return 0
                        Case 10
                            Return 0
                    End Select
                Case "TIEPS"
                    Select Case pModulo
                        Case 0
                            Return TIEPSNeg
                        Case 1
                            Return TIEPSCNeg
                        Case 2
                            Return TIEPSDNeg
                        Case 3
                            Return TIEPSDCNeg
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                        Case 6
                            Return 0
                        Case 7
                            Return 0
                        Case 8
                            Return 0
                        Case 9
                            Return 0
                        Case 10
                            Return 0
                    End Select
                Case "TP"
                    Return TPNeg
                Case "TD"
                    Return TDNeg
                Case "TPG"
                    Return TPGNeg
                Case "TPE"
                    Return TPENeg
                Case "TDG"
                    Return TDGNeg
                Case "TDE"
                    Return TDENeg
                Case "TI"
                    Return TINeg
                Case "THE"
                    Return THENeg

                Case "TIVANCon"
                    Select Case pModulo
                        Case 0
                            Return TIVANNegCon
                        Case 1
                            Return TIVANCNegCon
                        Case 2
                            Return TIVANDNegCon
                        Case 3
                            Return TIVANDCNegCon
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                        Case 6
                            Return 0
                        Case 7
                            Return 0
                        Case 8
                            Return 0
                        Case 9
                            Return 0
                        Case 10
                            Return 0
                    End Select
                Case "TNCon"
                    Select Case pModulo
                        Case 0
                            Return TNNegCon
                        Case 1
                            Return TNCNegCon
                        Case 2
                            Return TNDNegCon
                        Case 3
                            Return TNDCNegCon
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                        Case 6
                            Return 0
                        Case 7
                            Return 0
                        Case 8
                            Return 0
                        Case 9
                            Return 0
                        Case 10
                            Return 0
                    End Select
                Case "TSICon"
                    Select Case pModulo
                        Case 0
                            Return TSINegCon
                        Case 1
                            Return TSICNegCon
                        Case 2
                            Return TSIDNegCon
                        Case 3
                            Return TSIDCNegCon
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select
                Case "TIVACon"
                    Select Case pModulo
                        Case 0
                            Return TIVANegCon
                        Case 1
                            Return TIVACNegCon
                        Case 2
                            Return TIVADNegCon
                        Case 3
                            Return TIVADCNegCon
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select
                Case "TIVARCon"
                    Select Case pModulo
                        Case 0
                            Return TIVARNegCon
                        Case 1
                            Return TIVARCNegCon
                        Case 2
                            Return TIVARDNegCon
                        Case 3
                            Return TIVARDCNegCon
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select
                Case "TISRCon"
                    Select Case pModulo
                        Case 0
                            Return TISRNegCon
                        Case 1
                            Return 0
                        Case 2
                            Return TISRDNegCon
                        Case 3
                            Return 0
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select
                Case "TIEPSCon"
                    Select Case pModulo
                        Case 0
                            Return TIEPSNegCon
                        Case 1
                            Return TIEPSCNegCon
                        Case 2
                            Return TIEPSDNegCon
                        Case 3
                            Return TIEPSDCNegCon
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select

                Case "TIVANCr"
                    Select Case pModulo
                        Case 0
                            Return TIVANNegCr
                        Case 1
                            Return TIVANCNegCr
                        Case 2
                            Return TIVANDNegCr
                        Case 3
                            Return TIVANDCNegCr
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select
                Case "TNCr"
                    Select Case pModulo
                        Case 0
                            Return TNNegCr
                        Case 1
                            Return TNCNegCr
                        Case 2
                            Return TNDNegCr
                        Case 3
                            Return TNDCNegCr
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select
                Case "TSICr"
                    Select Case pModulo
                        Case 0
                            Return TSINegCr
                        Case 1
                            Return TSICNegCr
                        Case 2
                            Return TSIDNegCr
                        Case 3
                            Return TSIDCNegCr
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select
                Case "TIVACr"
                    Select Case pModulo
                        Case 0
                            Return TIVANegCr
                        Case 1
                            Return TIVACNegCr
                        Case 2
                            Return TIVADNegCr
                        Case 3
                            Return TIVADCNegCr
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select
                Case "TIVARCr"
                    Select Case pModulo
                        Case 0
                            Return TIVARNegCr
                        Case 1
                            Return TIVARCNegCr
                        Case 2
                            Return TIVARDNegCr
                        Case 3
                            Return TIVARDCNegCr
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select
                Case "TISRCr"
                    Select Case pModulo
                        Case 0
                            Return TISRNegCr
                        Case 1
                            Return 0
                        Case 2
                            Return TISRDNegCr
                        Case 3
                            Return 0
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select
                Case "TIEPSCr"
                    Select Case pModulo
                        Case 0
                            Return TIEPSNegCr
                        Case 1
                            Return TIEPSCNegCr
                        Case 2
                            Return TIEPSDNegCr
                        Case 3
                            Return TIEPSDCNegCr
                        Case 4
                            Return 0
                        Case 5
                            Return 0
                    End Select
            End Select
            '++++++++++++++++++++++++++++++++++++++++++++++++
            '+++++++++++++++++++++++++++++++++++
        End If
        Return 0
        'End Select
    End Function
    Public Sub DocumentosProcesados(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pIdSucursal As Integer, pCredito As Byte)
        'ComboBox2.Items.Add("Ventas") '0
        'ComboBox2.Items.Add("Compras") '1
        'ComboBox2.Items.Add("Ventas Devoluciones") '2
        'ComboBox2.Items.Add("Compras Devoluciones") '3
        'ComboBox2.Items.Add("Ventas Pagos") '4
        'ComboBox2.Items.Add("Compras Pagos") '5
        'ComboBox2.Items.Add("Notas de crédito ventas") '6
        'ComboBox2.Items.Add("Notas de crédito compras") '7
        'ComboBox2.Items.Add("Notas de cargo ventas") '8
        'ComboBox2.Items.Add("Notas de cargo compras") '9
        'ComboBox2.Items.Add("Nómina") '10
        'StrAfectadas = ""
        'strAfectadasNeg = ""
        'Dim Ids As New Collection

        Select Case pModulo
            Case 0
                DocumentosProcesadosFacturas(pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
            Case 1
                DocumentosProcesadosCompras(pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
            Case 2
                DocumentosProcesadosDevoluciones(pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
            Case 3
                DocumentosProcesadosDevolucionesC(pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
                'Case 4
                '   DocumentosProcesadosFacturas(pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
                'Case 5
                '   DocumentosProcesadosFacturas(pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
            Case 6
                DocumentosProcesadosNotasCredito(pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
            Case 7
                DocumentosProcesadosNotasCreditoc(pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
            Case 8
                DocumentosProcesadosNotasCargo(pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
            Case 9
                DocumentosProcesadosNotasCargoC(pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
            Case 10
                DocumentosProcesadosNominas(pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
        End Select

    End Sub

    Private Sub DocumentosProcesadosFacturas(pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pIdSucursal As Integer, pCredito As Byte)
        Dim Cuales As Integer
        Dim CualesNeg As Integer
        Dim Series As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Saca referencia de folio 
        If pCredito = 0 Then
            Cuales = 0
            CualesNeg = 0
        End If
        If pCredito = 1 Then
            Cuales = 1
            CualesNeg = 1
        End If
        If pCredito = 2 Then
            If TNCon <> 0 Then Cuales = 0
            If TNCr <> 0 Then Cuales = 1
            If TNCon <> 0 And TNCr <> 0 Then Cuales = 3

            If TNNegCon <> 0 Then CualesNeg = 0
            If TNNegCr <> 0 Then CualesNeg = 1
            If TNNegCon <> 0 And TNNegCr <> 0 Then CualesNeg = 3

        End If

        Comm.CommandText = "select tblventas.fecha,tblventas.totalapagar,tblventas.idconversion idmoneda,tblventas.tipodecambio,tblclientes.rfc,tblventastimbrado.uuid from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblventastimbrado on tblventastimbrado.idventa=tblventas.idventa where tblventas.estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If Cuales = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If Cuales = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        UUIDs.Clear()
        Dim UnUuid As stUuids
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("uuid")
            UUIDs.Add(UnUuid)
        End While
        DR.Close()

        Comm.CommandText = "select distinct serie from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblventas.estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If

        If Cuales = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If Cuales = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        While DR.Read
            Series.Add(DR("serie"))
        End While
        DR.Close()
        If StrAfectadas.Length <> 0 Then StrAfectadas += " "
        If pIdMovimiento = 0 Then
            StrAfectadas += "FACTURAS:"
        Else
            StrAfectadas += "FACTURA: "
        End If
        For Each Serie As String In Series
            Comm.CommandText = "select min(folio) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblventas.estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
            If pIdMovimiento = 0 Then
                Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
            Else
                Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
            End If
            If pIdSucursal <> 0 Then
                Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
            End If
            If Cuales = 0 Then
                Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
            Else
                If Cuales = 1 Then
                    Comm.CommandText += " and tblformasdepago.tipo=0"
                End If
            End If
            StrAfectadas += " " + Serie + Format(Comm.ExecuteScalar, "0000")
            If pIdMovimiento = 0 Then
                Comm.CommandText = "select max(folio) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
                If pIdMovimiento = 0 Then
                    Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
                Else
                    Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
                End If
                If pIdSucursal <> 0 Then
                    Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
                End If
                If Cuales = 0 Then
                    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
                Else
                    If Cuales = 1 Then
                        Comm.CommandText += " and tblformasdepago.tipo=0"
                    End If
                End If
                StrAfectadas += " AL " + Serie + Format(Comm.ExecuteScalar, "0000")
            End If
        Next

        'Saca referencia de folio canceladas 
        Comm.CommandText = "select folio,serie from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If CualesNeg = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If CualesNeg = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        'Series.Clear()
        If strAfectadasNeg.Length <> 0 Then strAfectadasNeg += " "
        If pIdMovimiento = 0 Then
            strAfectadasNeg += "FACTURAS CANCELADAS:"
        Else
            strAfectadasNeg += "FACTURA CANCELADA: "
        End If
        While DR.Read
            Series.Add(DR("serie"))
            strAfectadasNeg += " " + DR("serie") + Format(DR("folio"), "0000")
        End While
        DR.Close()


        Comm.CommandText = "select tblventas.fecha,tblventas.totalapagar,tblventas.idconversion idmoneda,tblventas.tipodecambio,tblclientes.rfc,tblventastimbrado.uuid from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblventastimbrado on tblventastimbrado.idventa=tblventas.idventa where tblventas.estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblventas.idventa=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If Cuales = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If Cuales = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        UUIDsNeg.Clear()
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("uuid")
            UUIDsNeg.Add(UnUuid)
        End While
        DR.Close()

    End Sub

    Private Sub DocumentosProcesadosNotasCargo(pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pIdSucursal As Integer, pCredito As Byte)

        Dim Series As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Saca referencia de folio 
        Comm.CommandText = "select distinct serie from tblnotasdecargo where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        While DR.Read
            Series.Add(DR("serie"))
        End While
        DR.Close()
        If StrAfectadas.Length <> 0 Then StrAfectadas += " "
        If pIdMovimiento = 0 Then
            StrAfectadas += "NOTAS DE CARGO:"
        Else
            StrAfectadas += "NOTA DE CARGO: "
        End If
        For Each Serie As String In Series
            Comm.CommandText = "select min(folio) from tblnotasdecargo where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
            If pIdMovimiento = 0 Then
                Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
            Else
                Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
            End If
            If pIdSucursal <> 0 Then
                Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
            End If
            'If pCredito = 0 Then
            '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
            'Else
            '    Comm.CommandText += " and tblformasdepago.tipo=0"
            'End If
            StrAfectadas += " " + Serie + Format(Comm.ExecuteScalar, "0000")
            If pIdMovimiento = 0 Then
                Comm.CommandText = "select max(folio) from tblnotasdecargo where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
                If pIdMovimiento = 0 Then
                    Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
                Else
                    Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
                End If
                If pIdSucursal <> 0 Then
                    Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
                End If
                'If pCredito = 0 Then
                '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
                'Else
                '    Comm.CommandText += " and tblformasdepago.tipo=0"
                'End If
                StrAfectadas += " AL " + Serie + Format(Comm.ExecuteScalar, "0000")
            End If
        Next

        'Saca referencia de folio canceladas 
        Comm.CommandText = "select serie,folio from tblnotasdecargo where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        DR = Comm.ExecuteReader
        'Series.Clear()
        If strAfectadasNeg.Length <> 0 Then strAfectadasNeg += " "
        If pIdMovimiento = 0 Then
            strAfectadasNeg += "NOTAS DE CARGO CANCELADAS:"
        Else
            strAfectadasNeg += "NOTA DE CARGO CANCELADA: "
        End If
        While DR.Read
            'Series.Add(DR("serie"))
            strAfectadasNeg += " " + DR("serie") + Format(DR("folio"), "0000")
        End While
        DR.Close()

        Comm.CommandText = "select tblnotasdecargo.fecha,tblnotasdecargo.totalapagar,tblnotasdecargo.idmoneda,tblnotasdecargo.tipodecambio,tblclientes.rfc,tblnotasdecargotimbrado.uuid from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente inner join tblnotasdecargotimbrado on tblnotasdecargotimbrado.idcargo=tblnotasdecargo.idcargo where tblnotasdecargo.estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        UUIDsNCA.Clear()
        Dim UnUuid As stUuids
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("uuid")
            UUIDsNCA.Add(UnUuid)
        End While
        DR.Close()

        Comm.CommandText = "select tblnotasdecargo.fecha,tblnotasdecargo.totalapagar,tblnotasdecargo.idmoneda,tblnotasdecargo.tipodecambio,tblclientes.rfc,tblnotasdecargotimbrado.uuid from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente inner join tblnotasdecargotimbrado on tblnotasdecargotimbrado.idcargo=tblnotasdecargo.idcargo where tblnotasdecargo.estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargo.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        UUIDsNCANeg.Clear()
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("uuid")
            UUIDsNCANeg.Add(UnUuid)
        End While
        DR.Close()

    End Sub

    Private Sub DocumentosProcesadosNotasCredito(pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pIdSucursal As Integer, pCredito As Byte)

        Dim Series As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Saca referencia de folio 
        Comm.CommandText = "select distinct serie from tblnotasdecredito where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        While DR.Read
            Series.Add(DR("serie"))
        End While
        DR.Close()
        If StrAfectadas.Length <> 0 Then StrAfectadas += " "
        If pIdMovimiento = 0 Then
            StrAfectadas += "NOTAS DE CRÉDITO:"
        Else
            StrAfectadas += "NOTA DE CRÉDITO:"
        End If
        For Each Serie As String In Series
            Comm.CommandText = "select min(folio) from tblnotasdecredito where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
            If pIdMovimiento = 0 Then
                Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
            Else
                Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
            End If
            If pIdSucursal <> 0 Then
                Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdSucursal.ToString
            End If
            'If pCredito = 0 Then
            '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
            'Else
            '    Comm.CommandText += " and tblformasdepago.tipo=0"
            'End If
            StrAfectadas += " " + Serie + Format(Comm.ExecuteScalar, "0000")
            If pIdMovimiento = 0 Then
                Comm.CommandText = "select max(folio) from tblnotasdecredito where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
                If pIdMovimiento = 0 Then
                    Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
                Else
                    Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
                End If
                If pIdSucursal <> 0 Then
                    Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdSucursal.ToString
                End If
                'If pCredito = 0 Then
                '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
                'Else
                '    Comm.CommandText += " and tblformasdepago.tipo=0"
                'End If
                StrAfectadas += " AL " + Serie + Format(Comm.ExecuteScalar, "0000")
            End If
        Next

        'Saca referencia de folio canceladas 
        Comm.CommandText = "select serie,folio from tblnotasdecredito where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        DR = Comm.ExecuteReader
        'Series.Clear()
        If strAfectadasNeg.Length <> 0 Then strAfectadasNeg += " "
        If pIdMovimiento = 0 Then
            strAfectadasNeg += "NOTAS DE CRÉDITO CANCELADAS:"
        Else
            strAfectadasNeg += "NOTA DE CRÉDITO CANCELADA: "
        End If
        While DR.Read
            'Series.Add(DR("serie"))
            strAfectadasNeg += " " + DR("serie") + Format(DR("folio"), "0000")
        End While
        DR.Close()

        Comm.CommandText = "select tblnotasdecredito.fecha,tblnotasdecredito.totalapagar,tblnotasdecredito.idmoneda,tblnotasdecredito.tipodecambio,tblclientes.rfc,tblnotasdecreditotimbrado.uuid from tblnotasdecredito inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente inner join tblnotasdecreditotimbrado on tblnotasdecreditotimbrado.idnota=tblnotasdecredito.idnota where tblnotasdecredito.estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        UUIDsNCr.Clear()
        Dim UnUuid As stUuids
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("uuid")
            UUIDsNCr.Add(UnUuid)
        End While
        DR.Close()

        Comm.CommandText = "select tblnotasdecredito.fecha,tblnotasdecredito.totalapagar,tblnotasdecredito.idmoneda,tblnotasdecredito.tipodecambio,tblclientes.rfc,tblnotasdecreditotimbrado.uuid from tblnotasdecredito inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente inner join tblnotasdecreditotimbrado on tblnotasdecreditotimbrado.idnota=tblnotasdecredito.idnota where tblnotasdecredito.estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecredito.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        UUIDsNCrNeg.Clear()
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("uuid")
            UUIDsNCrNeg.Add(UnUuid)
        End While
        DR.Close()

    End Sub
    Private Sub DocumentosProcesadosNominas(pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pIdSucursal As Integer, pCredito As Byte)

        Dim Series As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Saca referencia de folio 
        Comm.CommandText = "select distinct serie from tblnominas where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        While DR.Read
            Series.Add(DR("serie"))
        End While
        DR.Close()
        If StrAfectadas.Length <> 0 Then StrAfectadas += " "
        If pIdMovimiento = 0 Then
            StrAfectadas += "NÓMINAS: "
        Else
            StrAfectadas += "NÓMINA: "
        End If
        For Each Serie As String In Series
            Comm.CommandText = "select min(folio) from tblnominas where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
            If pIdMovimiento = 0 Then
                Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
            Else
                Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
            End If
            If pIdSucursal <> 0 Then
                Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
            End If
            'If pCredito = 0 Then
            '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
            'Else
            '    Comm.CommandText += " and tblformasdepago.tipo=0"
            'End If
            StrAfectadas += " " + Serie + Format(Comm.ExecuteScalar, "0000")
            If pIdMovimiento = 0 Then
                Comm.CommandText = "select max(folio) from tblnominas where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
                If pIdMovimiento = 0 Then
                    Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
                Else
                    Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
                End If
                If pIdSucursal <> 0 Then
                    Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
                End If
                'If pCredito = 0 Then
                '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
                'Else
                '    Comm.CommandText += " and tblformasdepago.tipo=0"
                'End If
                StrAfectadas += " AL " + Serie + Format(Comm.ExecuteScalar, "0000")
            End If
        Next

        'Saca referencia de folio canceladas 
        Comm.CommandText = "select serie,folio from tblnominas where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        DR = Comm.ExecuteReader
        Series.Clear()
        If strAfectadasNeg.Length <> 0 Then strAfectadasNeg += " "
        If pIdMovimiento = 0 Then
            strAfectadasNeg += "NÓMINAS CANCELADAS:"
        Else
            strAfectadasNeg += "NÓMINA CANCELADA: "
        End If
        While DR.Read
            'Series.Add(DR("serie"))
            strAfectadasNeg += " " + DR("serie") + Format(DR("folio"), "0000")
        End While
        DR.Close()

        Comm.CommandText = "select tblnominas.fecha,tblnominas.totalapagar,tblnominas.idmoneda,tblnominas.tipodecambio,tbltrabajadores.rfc,tblnominastimbrado.uuid from tblnominas inner join tbltrabajadores on tblnominas.idtrabajador=tbltrabajadores.idtrabajador inner join tblnominastimbrado on tblnominastimbrado.idnomina=tblnominas.idnomina where tblnominas.estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        UUIDsNOM.Clear()
        Dim UnUuid As stUuids
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("uuid")
            UUIDsNOM.Add(UnUuid)
        End While
        DR.Close()

        Comm.CommandText = "select tblnominas.fecha,tblnominas.totalapagar,tblnominas.idmoneda,tblnominas.tipodecambio,tbltrabajadores.rfc,tblnominastimbrado.uuid from tblnominas inner join tbltrabajadores on tblnominas.idtrabajador=tbltrabajadores.idtrabajador inner join tblnominastimbrado on tblnominastimbrado.idnomina=tblnominas.idnomina where tblnominas.estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnominas.idnomina=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        UUIDsNOMNeg.Clear()
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("uuid")
            UUIDsNOMNeg.Add(UnUuid)
        End While
        DR.Close()

    End Sub

    Private Sub DocumentosProcesadosNotasCreditoc(pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pIdSucursal As Integer, pCredito As Byte)

        Dim Series As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Saca referencia de folio 
        Comm.CommandText = "select distinct serie from tblnotasdecreditocompras where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        While DR.Read
            Series.Add(DR("serie"))
        End While
        DR.Close()
        If StrAfectadas.Length <> 0 Then StrAfectadas += " "
        If pIdMovimiento = 0 Then
            StrAfectadas += "NOTAS DE CRÉDITO COMPRAS:"
        Else
            StrAfectadas += "NOTA DE CRÉDITO COMPRAS:"
        End If
        For Each Serie As String In Series
            Comm.CommandText = "select min(folioi) from tblnotasdecreditocompras where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
            If pIdMovimiento = 0 Then
                Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
            Else
                Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
            End If
            If pIdSucursal <> 0 Then
                Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdSucursal.ToString
            End If
            'If pCredito = 0 Then
            '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
            'Else
            '    Comm.CommandText += " and tblformasdepago.tipo=0"
            'End If
            StrAfectadas += " " + Serie + Format(Comm.ExecuteScalar, "0000")
            If pIdMovimiento = 0 Then
                Comm.CommandText = "select max(folioi) from tblnotasdecreditocompras where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
                If pIdMovimiento = 0 Then
                    Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
                Else
                    Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
                End If
                If pIdSucursal <> 0 Then
                    Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdSucursal.ToString
                End If
                'If pCredito = 0 Then
                '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
                'Else
                '    Comm.CommandText += " and tblformasdepago.tipo=0"
                'End If
                StrAfectadas += " AL " + Serie + Format(Comm.ExecuteScalar, "0000")
            End If
        Next

        'Saca referencia de folio canceladas 
        Comm.CommandText = "select serie,folio,folioi from tblnotasdecreditocompras where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        DR = Comm.ExecuteReader
        'Series.Clear()
        If strAfectadasNeg.Length <> 0 Then strAfectadasNeg += " "
        If pIdMovimiento = 0 Then
            strAfectadasNeg += "NOTAS DE CRÉDITO COMPRAS CANCELADAS:"
        Else
            strAfectadasNeg += "NOTA DE CRÉDITO COMPRAS CANCELADA:"
        End If
        While DR.Read
            strAfectadasNeg += " " + DR("folio") + "-" + DR("serie") + Format(DR("folioi"), "0000")
        End While
        DR.Close()

        Comm.CommandText = "select tblnotasdecreditocompras.fecha,tblnotasdecreditocompras.totalapagar,tblnotasdecreditocompras.idmoneda,tblnotasdecreditocompras.tipodecambio,tblproveedores.rfc,tblnotasdecreditocompras.foliocfdi from tblnotasdecreditocompras inner join tblproveedores on tblnotasdecreditocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecreditocompras.estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        UUIDsNCAC.Clear()
        Dim UnUuid As stUuids
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("foliocfdi")
            UUIDsNCAC.Add(UnUuid)
        End While
        DR.Close()

        Comm.CommandText = "select tblnotasdecreditocompras.fecha,tblnotasdecreditocompras.totalapagar,tblnotasdecreditocompras.idmoneda,tblnotasdecreditocompras.tipodecambio,tblproveedores.rfc,tblnotasdecreditocompras.foliocfdi from tblnotasdecreditocompras inner join tblproveedores on tblnotasdecreditocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecreditocompras.estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecreditocompras.idnota=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdSucursal.ToString
        End If

        DR = Comm.ExecuteReader
        UUIDsNCACNeg.Clear()
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("foliocfdi")
            UUIDsNCACNeg.Add(UnUuid)
        End While
        DR.Close()

    End Sub

    Private Sub DocumentosProcesadosNotasCargoC(pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pIdSucursal As Integer, pCredito As Byte)

        Dim Series As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Saca referencia de folio 
        Comm.CommandText = "select distinct serie from tblnotasdecargocompras where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        While DR.Read
            Series.Add(DR("serie"))
        End While
        DR.Close()
        If StrAfectadas.Length <> 0 Then StrAfectadas += " "
        If pIdMovimiento = 0 Then
            StrAfectadas += "NOTAS DE CARGO COMPRAS:"
        Else
            StrAfectadas += "NOTA DE CARGO COMPRAS:"
        End If
        For Each Serie As String In Series
            Comm.CommandText = "select min(folioi) from tblnotasdecargocompras where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
            If pIdMovimiento = 0 Then
                Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
            Else
                Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
            End If
            If pIdSucursal <> 0 Then
                Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
            End If
            'If pCredito = 0 Then
            '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
            'Else
            '    Comm.CommandText += " and tblformasdepago.tipo=0"
            'End If
            StrAfectadas += " " + Serie + Format(Comm.ExecuteScalar, "0000")
            If pIdMovimiento = 0 Then
                Comm.CommandText = "select max(folioi) from tblnotasdecargocompras where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
                If pIdMovimiento = 0 Then
                    Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
                Else
                    Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
                End If
                If pIdSucursal <> 0 Then
                    Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
                End If
                'If pCredito = 0 Then
                '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
                'Else
                '    Comm.CommandText += " and tblformasdepago.tipo=0"
                'End If
                StrAfectadas += " AL " + Serie + Format(Comm.ExecuteScalar, "0000")
            End If
        Next

        'Saca referencia de folio canceladas 
        Comm.CommandText = "select serie,folio,folioi from tblnotasdecargocompras where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and idsucursal=" + pIdSucursal.ToString
        End If
        'If pCredito = 0 Then
        '    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        'Else
        '    Comm.CommandText += " and tblformasdepago.tipo=0"
        'End If
        DR = Comm.ExecuteReader
        'Series.Clear()
        If strAfectadasNeg.Length <> 0 Then strAfectadasNeg += " "
        If pIdMovimiento = 0 Then
            strAfectadasNeg += "NOTAS DE CARGO CANCELADAS:"
        Else
            strAfectadasNeg += "NOTA DE CARGO CANCELADA: "
        End If
        While DR.Read
            strAfectadasNeg += " " + DR("folio") + " " + DR("serie") + Format(DR("folioi"), "0000")
        End While
        DR.Close()

        Comm.CommandText = "select tblnotasdecargocompras.fecha,tblnotasdecargocompras.totalapagar,tblnotasdecargocompras.idmoneda,tblnotasdecargocompras.tipodecambio,tblproveedores.rfc,tblnotasdecargocompras.foliocfdi from tblnotasdecargocompras inner join tblproveedores on tblnotasdecargocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecargocompras.estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        UUIDsNCAC.Clear()
        Dim UnUuid As stUuids
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("foliocfdi")
            UUIDsNCAC.Add(UnUuid)
        End While
        DR.Close()

        Comm.CommandText = "select tblnotasdecargocompras.fecha,tblnotasdecargocompras.totalapagar,tblnotasdecargocompras.idmoneda,tblnotasdecargocompras.tipodecambio,tblproveedores.rfc,tblnotasdecargocompras.foliocfdi from tblnotasdecargocompras inner join tblproveedores on tblnotasdecargocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecargocompras.estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblnotasdecargocompras.idcargo=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
        End If

        DR = Comm.ExecuteReader
        UUIDsNCACNeg.Clear()
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("foliocfdi")
            UUIDsNCACNeg.Add(UnUuid)
        End While
        DR.Close()
    End Sub

    Private Sub DocumentosProcesadosDevoluciones(pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pIdSucursal As Integer, pCredito As Byte)

        Dim Series As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Saca referencia de folio 
        Dim Cuales As Integer
        Dim CualesNeg As Integer
        If pCredito = 0 Then
            Cuales = 0
            CualesNeg = 0
        End If
        If pCredito = 1 Then
            Cuales = 1
            CualesNeg = 1
        End If
        If pCredito = 2 Then
            If TND <> 0 Then Cuales = 0
            If TNDCr <> 0 Then Cuales = 1
            If TNDCon <> 0 And TNDCr <> 0 Then Cuales = 3

            If TNDNegCon <> 0 Then CualesNeg = 0
            If TNDNegCr <> 0 Then CualesNeg = 1
            If TNDNegCon <> 0 And TNDNegCr <> 0 Then CualesNeg = 3

        End If

        Comm.CommandText = "select distinct tbldevoluciones.serie from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idventa<>0 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If Cuales = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If Cuales = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        While DR.Read
            Series.Add(DR("serie"))
        End While
        DR.Close()
        If StrAfectadas.Length <> 0 Then StrAfectadas += " "
        If pIdMovimiento = 0 Then
            StrAfectadas += "DEVOLUCIONES:"
        Else
            StrAfectadas += "DEVOLUCION:"
        End If
        For Each Serie As String In Series
            Comm.CommandText = "select min(tbldevoluciones.folio) from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.serie='" + Replace(Serie, "'", "''") + "' and tbldevoluciones.idventa<>0"
            If pIdMovimiento = 0 Then
                Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
            Else
                Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
            End If
            If pIdSucursal <> 0 Then
                Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
            End If
            If Cuales = 0 Then
                Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
            Else
                If Cuales = 1 Then
                    Comm.CommandText += " and tblformasdepago.tipo=0"
                End If
            End If
            StrAfectadas += " " + Serie + Format(Comm.ExecuteScalar, "0000")
            If pIdMovimiento = 0 Then
                Comm.CommandText = "select max(tbldevoluciones.folio) from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=3 and tbldevoluciones.serie='" + Replace(Serie, "'", "''") + "' and tbldevoluciones.idventa<>0"
                If pIdMovimiento = 0 Then
                    Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
                Else
                    Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
                End If
                If pIdSucursal <> 0 Then
                    Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
                End If
                If Cuales = 0 Then
                    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
                Else
                    If Cuales = 1 Then
                        Comm.CommandText += " and tblformasdepago.tipo=0"
                    End If
                End If
                StrAfectadas += " AL " + Serie + Format(Comm.ExecuteScalar, "0000")
            End If
        Next

        'Saca referencia de folio canceladas 
        Comm.CommandText = "select  tbldevoluciones.serie,tbldevoluciones.folio from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tbldevoluciones.estado=4 and tbldevoluciones.idventa<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        If CualesNeg = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If CualesNeg = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        'Series.Clear()
        If strAfectadasNeg.Length <> 0 Then strAfectadasNeg += " "
        If pIdMovimiento = 0 Then
            strAfectadasNeg += "DEVOLUCIONES CANCELADAS:"
        Else
            strAfectadasNeg += "DEVOLUCIÓN CANCELADA:"
        End If
        While DR.Read
            strAfectadasNeg += " " + DR("serie") + Format(DR("folio"), "0000")
            '   Series.Add(DR("serie"))
        End While
        DR.Close()

        Comm.CommandText = "select tbldevoluciones.fecha,tbldevoluciones.totalapagar,tbldevoluciones.idconversion idmoneda,tbldevoluciones.tipodecambio,tblclientes.rfc,tbldevolucionestimbrado.uuid from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente inner join tbldevolucionestimbrado on tbldevolucionestimbrado.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fechacancelado>='" + pFecha1 + "' and tbldevoluciones.fechacancelado<='" + pFecha2 + "' and tbldevoluciones.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        If CualesNeg = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If CualesNeg = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        UUIDsDevNeg.Clear()
        Dim UnUuid As stUuids
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("uuid")
            UUIDsDevNeg.Add(UnUuid)
        End While
        DR.Close()

        Comm.CommandText = "select tbldevoluciones.fecha,tbldevoluciones.totalapagar,tbldevoluciones.idconversion idmoneda,tbldevoluciones.tipodecambio,tblclientes.rfc,tbldevolucionestimbrado.uuid from tbldevoluciones inner join tblventas on tbldevoluciones.idventa=tblventas.idventa inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente inner join tbldevolucionestimbrado on tbldevolucionestimbrado.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevoluciones.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        If CualesNeg = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If CualesNeg = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        UUIDsDev.Clear()
        'Dim UnUuid As stUuids
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("uuid")
            UUIDsDev.Add(UnUuid)
        End While
        DR.Close()

    End Sub

    Private Sub DocumentosProcesadosDevolucionesC(pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pIdSucursal As Integer, pCredito As Byte)

        Dim Series As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Saca referencia de folio 
        Dim Cuales As Integer
        Dim CualesNeg As Integer
        If pCredito = 0 Then
            Cuales = 0
            CualesNeg = 0
        End If
        If pCredito = 1 Then
            Cuales = 1
            CualesNeg = 1
        End If
        If pCredito = 2 Then
            If TNDCCon <> 0 Then Cuales = 0
            If TNDCCr <> 0 Then Cuales = 1
            If TNDCCon <> 0 And TNDCCr <> 0 Then Cuales = 3

            If TNDCNegCon <> 0 Then CualesNeg = 0
            If TNDCNegCr <> 0 Then CualesNeg = 1
            If TNDCNegCon <> 0 And TNDCNegCr <> 0 Then CualesNeg = 3

        End If
        Comm.CommandText = "select distinct tbldevolucionescompras.serie from tbldevolucionescompras inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idcompra<>0 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If Cuales = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If Cuales = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        While DR.Read
            Series.Add(DR("serie"))
        End While
        DR.Close()
        If StrAfectadas.Length <> 0 Then StrAfectadas += " "
        If pIdMovimiento = 0 Then
            StrAfectadas += "DEVOLUCIONES COMPRAS:"
        Else
            StrAfectadas += "DEVOLUCION COMPRA: "
        End If
        For Each Serie As String In Series
            Comm.CommandText = "select min(tbldevolucionescompras.folioi) from tbldevolucionescompras inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.serie='" + Replace(Serie, "'", "''") + "' and tbldevolucionescompras.idcompra<>0"
            If pIdMovimiento = 0 Then
                Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
            Else
                Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
            End If
            If pIdSucursal <> 0 Then
                Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
            End If
            If Cuales = 0 Then
                Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
            Else
                If Cuales = 1 Then
                    Comm.CommandText += " and tblformasdepago.tipo=0"
                End If
            End If
            StrAfectadas += " " + Serie + Format(Comm.ExecuteScalar, "0000")
            If pIdMovimiento = 0 Then
                Comm.CommandText = "select max(tbldevolucionescompras.folioi) from tbldevolucionescompras inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.serie='" + Replace(Serie, "'", "''") + "' and tbldevolucionescompras.idcompra<>0"
                If pIdMovimiento = 0 Then
                    Comm.CommandText += " and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
                Else
                    Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
                End If
                If pIdSucursal <> 0 Then
                    Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
                End If
                If Cuales = 0 Then
                    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
                Else
                    If Cuales = 1 Then
                        Comm.CommandText += " and tblformasdepago.tipo=0"
                    End If
                End If
                StrAfectadas += " AL " + Serie + Format(Comm.ExecuteScalar, "0000")
            End If
        Next

        'Saca referencia de folio canceladas 
        Comm.CommandText = "select tbldevolucionescompras.folio,tbldevolucionescompras.folioi, tbldevolucionescompras.serie from tbldevolucionescompras inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tbldevolucionescompras.estado=4 and tbldevolucionescompras.idcompra<>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldevolucionescompras.fechacancelado>='" + pFecha1 + "' and tbldevolucionescompras.fechacancelado<='" + pFecha2 + "' and tbldevolucionescompras.fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        If CualesNeg = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If CualesNeg = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        'Series.Clear()
        If strAfectadasNeg.Length <> 0 Then strAfectadasNeg += " "
        If pIdMovimiento = 0 Then
            strAfectadasNeg += "DEVOLUCIONES COMPRAS CANCELADAS:"
        Else
            strAfectadasNeg += "DEVOLUCIÓN COMPRA CANCELADA: "
        End If
        While DR.Read
            'Series.Add(DR("s"))
            strAfectadasNeg += " " + DR("folio") + "-" + DR("serie") + Format(DR("folioi"), "0000")
        End While
        DR.Close()

        Comm.CommandText = "select tbldevolucionescompras.fecha,tbldevolucionescompras.totalapagar,tbldevolucionescompras.idconversion idmoneda,tbldevolucionescompras.tipodecambio,tblproveedores.rfc,tbldevolucionescompras.uuid foliocfdi from tbldevolucionescompras inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor where tbldevolucionescompras.estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        If Cuales = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If Cuales = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        UUIDsDevC.Clear()
        Dim UnUuid As stUuids
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("foliocfdi")
            UUIDsDevC.Add(UnUuid)
        End While
        DR.Close()

        Comm.CommandText = "select tbldevolucionescompras.fecha,tbldevolucionescompras.totalapagar,tbldevolucionescompras.idconversion idmoneda,tbldevolucionescompras.tipodecambio,tblproveedores.rfc,tbldevolucionescompras.uuid foliocfdi from tbldevolucionescompras inner join tblcompras on tbldevolucionescompras.idcompra=tblcompras.idcompra inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor where tbldevolucionescompras.estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tbldevolucionescompras.iddevolucion=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        If Cuales = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If Cuales = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        UUIDsDevCNeg.Clear()
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("foliocfdi")
            UUIDsDevCNeg.Add(UnUuid)
        End While
        DR.Close()

    End Sub

    Private Sub DocumentosProcesadosCompras(pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pIdSucursal As Integer, pCredito As Byte)

        Dim Series As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader

        Dim Cuales As Integer
        Dim CualesNeg As Integer
        If pCredito = 0 Then
            Cuales = 0
            CualesNeg = 0
        End If
        If pCredito = 1 Then
            Cuales = 1
            CualesNeg = 1
        End If
        If pCredito = 2 Then
            If TNCCon <> 0 Then Cuales = 0
            If TNCCr <> 0 Then Cuales = 1
            If TNCCon <> 0 And TNCCr <> 0 Then Cuales = 3

            If TNCNegCon <> 0 Then CualesNeg = 0
            If TNCNegCr <> 0 Then CualesNeg = 1
            If TNCNegCon <> 0 And TNCNegCr <> 0 Then CualesNeg = 3

        End If


        Comm.CommandText = "select distinct serie from tblcompras inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=3 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If Cuales = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If Cuales = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        DR = Comm.ExecuteReader
        While DR.Read
            Series.Add(DR("serie"))
        End While
        DR.Close()
        If StrAfectadas.Length <> 0 Then StrAfectadas += " "
        If pIdMovimiento = 0 Then
            StrAfectadas += "COMPRAS:"
        Else
            StrAfectadas += "COMPRA: "
        End If
        For Each Serie As String In Series
            Comm.CommandText = "select min(folioi) from tblcompras inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
            If pIdMovimiento = 0 Then
                Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
            Else
                Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
            End If
            If pIdSucursal <> 0 Then
                Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
            End If
            If Cuales = 0 Then
                Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
            Else
                If Cuales = 1 Then
                    Comm.CommandText += " and tblformasdepago.tipo=0"
                End If
            End If
            StrAfectadas += " " + Serie + Format(Comm.ExecuteScalar, "0000")
            If pIdMovimiento = 0 Then
                Comm.CommandText = "select max(folioi) from tblcompras inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=3 and serie='" + Replace(Serie, "'", "''") + "'"
                If pIdMovimiento = 0 Then
                    Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
                Else
                    Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
                End If
                If pIdSucursal <> 0 Then
                    Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
                End If
                If Cuales = 0 Then
                    Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
                Else
                    If Cuales = 1 Then
                        Comm.CommandText += " and tblformasdepago.tipo=0"
                    End If
                End If
                StrAfectadas += " AL " + Serie + Format(Comm.ExecuteScalar, "0000")
            End If
        Next

        'Saca referencia de folio canceladas 
        Comm.CommandText = "select serie,folioi,referencia from tblcompras inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where estado=4 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        If CualesNeg = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If CualesNeg = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        'Series.Clear()
        If strAfectadasNeg.Length <> 0 Then strAfectadasNeg += " "
        If pIdMovimiento = 0 Then
            strAfectadasNeg += "COMPRAS CANCELADAS:"
        Else
            strAfectadasNeg += "COMPRA CANCELADA:"
        End If
        While DR.Read
            'Series.Add(DR("serie"))
            strAfectadasNeg += " " + DR("referencia") + "-" + DR("serie") + Format(DR("folioi"), "0000")
        End While
        DR.Close()

        Comm.CommandText = "select tblcompras.fecha,tblcompras.totalapagar,tblcompras.idmoneda,tblcompras.tipodecambio,tblproveedores.rfc,tblcompras.foliocfdi from tblcompras inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor where tblcompras.estado=3"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        If Cuales = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If Cuales = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        UUIDsC.Clear()
        Dim UnUuid As stUuids
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("foliocfdi")
            UUIDsC.Add(UnUuid)
        End While
        DR.Close()

        Comm.CommandText = "select tblcompras.fecha,tblcompras.totalapagar,tblcompras.idmoneda,tblcompras.tipodecambio,tblproveedores.rfc,tblcompras.foliocfdi from tblcompras inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor where tblcompras.estado=4"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and fechacancelado>='" + pFecha1 + "' and fechacancelado<='" + pFecha2 + "' and fecha<'" + pFecha1 + "'"
        Else
            Comm.CommandText += " and tblcompras.idcompra=" + pIdMovimiento.ToString
        End If
        If pIdSucursal <> 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        If Cuales = 0 Then
            Comm.CommandText += " and (tblformasdepago.tipo=1 or tblformasdepago.tipo=2)"
        Else
            If Cuales = 1 Then
                Comm.CommandText += " and tblformasdepago.tipo=0"
            End If
        End If
        DR = Comm.ExecuteReader
        UUIDsCNeg.Clear()
        While DR.Read
            UnUuid.Fecha = DR("fecha")
            If DR("idmoneda") = 2 Then
                UnUuid.IdMoneda = 101
            Else
                UnUuid.IdMoneda = 147
            End If
            UnUuid.Monto = DR("totalapagar")
            UnUuid.RFC = DR("rfc")
            UnUuid.TipodeCambio = DR("tipodecambio")
            UnUuid.Uuid = DR("foliocfdi")
            UUIDsCNeg.Add(UnUuid)
        End While
        DR.Close()

    End Sub

    Private Sub LlenaVariablesDepositos(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        ' llenar variables****************************************FACTURAS***********

        Dim UnPago As stDepositos
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Depositos solos
        Comm.CommandText = "select round(cantidad,2) as cant,iddeposito,tblcuentas.idccontable,comentario,if(tblcuentas.idccontable>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblcuentas.idccontable),'') as Cuenta from tbldepostito inner join tblcuentas on tblcuentas.idcuenta=tbldepostito.banco2 where"
        Comm.CommandText += " (select count(tblventaspagos.iddeposito) from tblventaspagos where tblventaspagos.iddeposito=tbldepostito.iddeposito)=0 and (select count(tblventasdepositos.iddeposito) from tblventasdepositos where tblventasdepositos.iddeposito=tbldepostito.iddeposito)=0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldepostito.fecha>='" + pFecha1 + "' and tbldepostito.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and iddeposito=" + pIdMovimiento.ToString
        End If

        DR = Comm.ExecuteReader
        Dim UnDeposito As stDepositos
        DepositosLista.Clear()
        UUIDsDep.Clear()
        UUIDsDepCr.Clear()
        UUIDsDepCon.Clear()
        While DR.Read
            UnDeposito.Cantidad = DR("cant")
            TNDep += UnDeposito.Cantidad
            TNSLDep += UnDeposito.Cantidad
            UnDeposito.Idcuenta = DR("idccontable")
            UnDeposito.IdDeposito = DR("iddeposito")
            UnDeposito.Concepto = DR("comentario")
            StrAfectadas += " " + UnDeposito.Concepto.Replace("FACTURAS: ", "")
            UnDeposito.Iva = 0
            UnDeposito.ISR = 0
            UnDeposito.Retendido = 0
            UnDeposito.Ieps = 0
            UnDeposito.Cuenta = DR("Cuenta")

            UnDeposito.ChequeTrans.BancoDestinoEx = ""
            UnDeposito.ChequeTrans.BancoEx = ""
            UnDeposito.ChequeTrans.BancoOrigenEx = ""
            UnDeposito.ChequeTrans.Beneficiario = ""
            UnDeposito.ChequeTrans.Fecha = ""
            UnDeposito.ChequeTrans.IdBancoDestino = 0
            UnDeposito.ChequeTrans.IdBancoOrigen = 0
            UnDeposito.ChequeTrans.IdMoneda = 0
            UnDeposito.ChequeTrans.Monto = 0
            UnDeposito.ChequeTrans.NoCuentaDestino = ""
            UnDeposito.ChequeTrans.NoCuentaOrigen = ""
            UnDeposito.ChequeTrans.NumeroCheque = ""
            UnDeposito.ChequeTrans.RFC = ""
            UnDeposito.ChequeTrans.TipodeCambio = 0
            UnDeposito.Folio = ""
            UnDeposito.IdProveedor = 0
            UnDeposito.IvaPor = 0
            UnDeposito.IVaRetPor = 0
            UnDeposito.IEpsPor = 0
            UnDeposito.Uuid.Uuid = ""
            UnDeposito.Uuid.Fecha = ""
            UnDeposito.Uuid.Monto = 0
            UnDeposito.Uuid.RFC = ""
            UnDeposito.Uuid.TipodeCambio = 0
            UnDeposito.Uuid.IdMoneda = 101
            UnDeposito.NombreProveedor = ""
            DepositosLista.Add(UnDeposito)
        End While
        DR.Close()


        'Depositos Credito
        Comm.CommandText = "select round(cantidad,2) as cant,iddeposito,tblcuentas.idccontable,comentario,if(tblcuentas.idccontable>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblcuentas.idccontable),'') as Cuenta from tbldepostito inner join tblcuentas on tblcuentas.idcuenta=tbldepostito.banco2 where"
        'StrAfectadas = ""
        Comm.CommandText += " (select count(tblventaspagos.iddeposito) from tblventaspagos where tblventaspagos.iddeposito=tbldepostito.iddeposito)>0 "
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldepostito.fecha>='" + pFecha1 + "' and tbldepostito.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and iddeposito=" + pIdMovimiento.ToString
        End If
        DR = Comm.ExecuteReader
        'Dim UnDeposito As stDepositos
        Dim StrTemp As String = ""


        DepositosListaCr.Clear()
        While DR.Read
            UnDeposito.Cantidad = DR("cant")
            TNDep += UnDeposito.Cantidad
            TNDepCr += UnDeposito.Cantidad
            UnDeposito.Idcuenta = DR("idccontable")
            UnDeposito.IdDeposito = DR("iddeposito")
            UnDeposito.Concepto = DR("comentario")
            If StrTemp.Contains(UnDeposito.Concepto) = False Then
                StrTemp += " " + UnDeposito.Concepto.Replace("FACTURAS: ", "").Replace("FACTURA:", "")
            End If
            UnDeposito.Iva = 0
            UnDeposito.ISR = 0
            UnDeposito.Retendido = 0
            UnDeposito.Ieps = 0
            UnDeposito.Cuenta = DR("Cuenta")

            UnDeposito.ChequeTrans.BancoDestinoEx = ""
            UnDeposito.ChequeTrans.BancoEx = ""
            UnDeposito.ChequeTrans.BancoOrigenEx = ""
            UnDeposito.ChequeTrans.Beneficiario = ""
            UnDeposito.ChequeTrans.Fecha = ""
            UnDeposito.ChequeTrans.IdBancoDestino = 0
            UnDeposito.ChequeTrans.IdBancoOrigen = 0
            UnDeposito.ChequeTrans.IdMoneda = 0
            UnDeposito.ChequeTrans.Monto = 0
            UnDeposito.ChequeTrans.NoCuentaDestino = ""
            UnDeposito.ChequeTrans.NoCuentaOrigen = ""
            UnDeposito.ChequeTrans.NumeroCheque = ""
            UnDeposito.ChequeTrans.RFC = ""
            UnDeposito.ChequeTrans.TipodeCambio = 0

            UnDeposito.Uuid.Uuid = ""
            UnDeposito.Uuid.Fecha = ""
            UnDeposito.Uuid.Monto = 0
            UnDeposito.Uuid.RFC = ""
            UnDeposito.Uuid.TipodeCambio = 0
            UnDeposito.Uuid.IdMoneda = 101
            UnDeposito.Folio = ""
            UnDeposito.IdProveedor = 0
            UnDeposito.IvaPor = 0
            UnDeposito.IVaRetPor = 0
            UnDeposito.IEpsPor = 0
            UnDeposito.NombreProveedor = ""
            DepositosListaCr.Add(UnDeposito)
        End While
        DR.Close()
        If StrTemp <> "" Then
            If StrAfectadas.Length <> 0 Then StrAfectadas += " "
            StrAfectadas += " " + StrTemp
        End If
        PagosLista.Clear()

        For Each Dep As stDepositos In DepositosListaCr
            Comm.CommandText = "select round(cantidad,2) as cantidad,idventa,idcargo,iddocumentod," +
                "if(idventa<>0,ifnull((select concat(v.serie,convert(v.folio using utf8),' ',tblclientes.nombre) from tblventas v inner join tblclientes on v.idcliente=tblclientes.idcliente where v.idventa=tblventaspagos.idventa),''),'') as ventaconcepto," +
                "if(idcargo<>0,ifnull((select concat(v.serie,convert(v.folio using utf8),' ',tblclientes.nombre) from tblnotasdecargo v inner join tblclientes on v.idcliente=tblclientes.idcliente where v.idcargo=tblventaspagos.idcargo),''),'') as ncconcepto," +
                "if(iddocumentod<>0,ifnull((select concat(if(v.tiposaldo=0,concat(v.serie,convert(v.folio using utf8)),concat(v.seriereferencia,convert(v.folioreferencia using utf8))),' ',tblclientes.nombre) from tbldocumentosclientes v inner join tblclientes on v.idcliente=tblclientes.idcliente where v.iddocumento=tblventaspagos.iddocumentod),''),'') as docconcepto," +
                "(select idcuenta from tblclientes where tblclientes.idcliente=tblventaspagos.idcliente) as ccontable," +
                "round(if(iddocumentod<>0,ifnull((select cantidad*(dc.totalapagar-(dc.totalapagar/1.16))/dc.totalapagar from tbldocumentosclientes dc where dc.iddocumento=iddocumentod),0)," +
                "if(idcargo<>0,spdaivaproporcionalnc(idcargo,cantidad),spdaivaproporcionalf(idventa,cantidad,0))),2) as ivapago," +
                "round(if(idventa<>0,spdaivaproporcionalf(idventa,cantidad,2),0),2) as ivaretpago," +
                "round(if(idventa<>0,spdaivaproporcionalf(idventa,cantidad,1),0),2) as iepspago," +
                "round(if(idventa<>0,spdaivaproporcionalf(idventa,cantidad,3),0),2) as isrpago," +
                "(select rfc from tblclientes where tblclientes.idcliente=tblventaspagos.idcliente) as rfc," +
                "if(idventa<>0,(select fecha from tblventas where tblventas.idventa=tblventaspagos.idventa),if(idcargo<>0,(select fecha from tblnotasdecargo where tblnotasdecargo.idcargo=tblventaspagos.idcargo),'')) as pfecha," +
                "if(idventa<>0,(select round(totalapagar,2) from tblventas where tblventas.idventa=tblventaspagos.idventa),if(idcargo<>0,(select round(totalapagar,2) from tblnotasdecargo where tblnotasdecargo.idcargo=tblventaspagos.idcargo),0)) as monto," +
                "if(idventa<>0,(select idconversion from tblventas where tblventas.idventa=tblventaspagos.idventa),if(idcargo<>0,(select idmoneda from tblnotasdecargo where tblnotasdecargo.idcargo=tblventaspagos.idcargo),2)) as idmoneda," +
                "if(idventa<>0,(select round(tipodecambio,2) from tblventas where tblventas.idventa=tblventaspagos.idventa),if(idcargo<>0,(select round(tipodecambio,2) from tblnotasdecargo where tblnotasdecargo.idcargo=tblventaspagos.idcargo),'')) as tipodecambio," +
                "if(idventa<>0,(select uuid from tblventastimbrado where tblventastimbrado.idventa=tblventaspagos.idventa),if(idcargo<>0,(select uuid from tblnotasdecargotimbrado where tblnotasdecargotimbrado.idcargo=tblventaspagos.idcargo),'')) as uuid," +
                "if((select idcuenta from tblclientes where tblclientes.idcliente=tblventaspagos.idcliente)>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=(select idcuenta from tblclientes where tblclientes.idcliente=tblventaspagos.idcliente)),'') as Cuenta" +
                " from tblventaspagos where iddeposito=" + Dep.IdDeposito.ToString
            DR = Comm.ExecuteReader

            While DR.Read
                UnPago.Cantidad = DR("cantidad")
                UnPago.Concepto = DR("docconcepto")
                UnPago.Idcuenta = DR("ccontable")
                UnPago.IdDeposito = 0
                If DR("idventa") <> 0 Then
                    UnPago.Concepto = DR("ventaconcepto")
                End If
                If DR("idcargo") <> 0 Then
                    UnPago.Concepto = DR("ncconcepto")
                End If
                If DR("iddocumentod") <> 0 Then
                    UnPago.Concepto = DR("docconcepto")
                End If
                UnPago.Iva = DR("ivapago")
                UnPago.ISR = DR("isrpago")
                UnPago.Retendido = DR("ivaretpago")
                UnPago.Ieps = DR("iepspago")
                UnPago.Cuenta = DR("Cuenta")
                TIVADepCr += UnPago.Iva
                TIEPSDepCr += UnPago.Ieps
                TISRDepCr += UnPago.ISR
                TIVARDepCr += UnPago.Retendido

                UnPago.ChequeTrans.BancoDestinoEx = ""
                UnPago.ChequeTrans.BancoEx = ""
                UnPago.ChequeTrans.BancoOrigenEx = ""
                UnPago.ChequeTrans.Beneficiario = ""
                UnPago.ChequeTrans.Fecha = ""
                UnPago.ChequeTrans.IdBancoDestino = 0
                UnPago.ChequeTrans.IdBancoOrigen = 0
                UnPago.ChequeTrans.IdMoneda = 0
                UnPago.ChequeTrans.Monto = 0
                UnPago.ChequeTrans.NoCuentaDestino = ""
                UnPago.ChequeTrans.NoCuentaOrigen = ""
                UnPago.ChequeTrans.NumeroCheque = ""
                UnPago.ChequeTrans.RFC = ""
                UnPago.ChequeTrans.TipodeCambio = 0

                UnPago.Uuid.Uuid = DR("uuid")
                UnPago.Uuid.Fecha = DR("PFecha")
                UnPago.Uuid.Monto = DR("Monto")
                UnPago.Uuid.RFC = DR("rfc")
                UnPago.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    UnPago.Uuid.IdMoneda = 101
                Else
                    UnPago.Uuid.IdMoneda = 147
                End If
                If DR("idventa") <> 0 Or DR("idcargo") <> 0 Then
                    UUIDsDepCr.Add(UnPago.Uuid)
                    UUIDsDep.Add(UnPago.Uuid)
                End If
                UnPago.Folio = ""
                UnPago.IdProveedor = 0
                UnPago.IvaPor = 0
                UnPago.IVaRetPor = 0
                UnPago.IEpsPor = 0
                UnPago.NombreProveedor = ""
                PagosLista.Add(UnPago)
            End While
            DR.Close()
        Next

        'Depositos Contado
        Comm.CommandText = "select round(cantidad,2) as cant,iddeposito,tblcuentas.idccontable,comentario,if(tblcuentas.idccontable>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblcuentas.idccontable),'') as Cuenta from tbldepostito inner join tblcuentas on tblcuentas.idcuenta=tbldepostito.banco2 where"
        'StrAfectadas = ""
        Comm.CommandText += " (select count(tblventasdepositos.iddeposito) from tblventasdepositos where tblventasdepositos.iddeposito=tbldepostito.iddeposito)>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tbldepostito.fecha>='" + pFecha1 + "' and tbldepostito.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and iddeposito=" + pIdMovimiento.ToString
        End If
        DR = Comm.ExecuteReader
        'Dim UnDeposito As stDepositos
        StrTemp = ""
        DepositosListaCon.Clear()
        While DR.Read
            UnDeposito.Cantidad = DR("cant")
            TNDep += UnDeposito.Cantidad
            TNDepCon += UnDeposito.Cantidad
            UnDeposito.Idcuenta = DR("idccontable")
            UnDeposito.IdDeposito = DR("iddeposito")
            UnDeposito.Concepto = DR("comentario")
            If StrTemp.Contains(UnDeposito.Concepto) = False Then
                StrTemp += " " + UnDeposito.Concepto.Replace("FACTURAS: ", "").Replace("FACTURA: ", "")
            End If
            UnDeposito.Iva = 0
            UnDeposito.ISR = 0
            UnDeposito.Retendido = 0
            UnDeposito.Ieps = 0
            UnDeposito.Cuenta = DR("Cuenta")
            UnDeposito.ChequeTrans.BancoDestinoEx = ""
            UnDeposito.ChequeTrans.BancoEx = ""
            UnDeposito.ChequeTrans.BancoOrigenEx = ""
            UnDeposito.ChequeTrans.Beneficiario = ""
            UnDeposito.ChequeTrans.Fecha = ""
            UnDeposito.ChequeTrans.IdBancoDestino = 0
            UnDeposito.ChequeTrans.IdBancoOrigen = 0
            UnDeposito.ChequeTrans.IdMoneda = 0
            UnDeposito.ChequeTrans.Monto = 0
            UnDeposito.ChequeTrans.NoCuentaDestino = ""
            UnDeposito.ChequeTrans.NoCuentaOrigen = ""
            UnDeposito.ChequeTrans.NumeroCheque = ""
            UnDeposito.ChequeTrans.RFC = ""
            UnDeposito.ChequeTrans.TipodeCambio = 0
            UnDeposito.Folio = ""
            UnDeposito.IdProveedor = 0
            UnDeposito.IvaPor = 0
            UnDeposito.IVaRetPor = 0
            UnDeposito.IEpsPor = 0
            UnDeposito.Uuid.Uuid = ""
            UnDeposito.Uuid.Fecha = ""
            UnDeposito.Uuid.Monto = 0
            UnDeposito.Uuid.RFC = ""
            UnDeposito.Uuid.TipodeCambio = 0
            UnDeposito.Uuid.IdMoneda = 101
            UnDeposito.NombreProveedor = ""
            DepositosListaCon.Add(UnDeposito)
        End While
        DR.Close()
        If StrTemp <> "" Then
            If StrAfectadas.Length <> 0 Then StrAfectadas += " "
            StrAfectadas += " " + StrTemp
        End If
        PagosListaCon.Clear()
        For Each Dep As stDepositos In DepositosListaCon

            Comm.CommandText = "select ifnull(sum(if(tblventasinventario.idmoneda=2,round(tblventasinventario.precio,2),round(tblventasinventario.precio*tblventas.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.iva/100,2),round(tblventasinventario.precio*tblventasinventario.iva/100*tblventas.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ivaretenido/100,2),round(precio*tblventasinventario.ivaretenido/100*tblventas.tipodecambio,2))),0)+ifnull(if(tblventas.idconversion=2,round(tblventas.total*tblventas.ivaretenido/100,2),round(tblventas.total*tblventas.ivaretenido/100*tblventas.tipodecambio,2)),0) as totalivaret," +
            " ifnull(sum(if(tblventas.idconversion=2,round(tblventas.total*tblventas.isr/100,2),round(tblventas.total*tblventas.isr/100*tblventas.tipodecambio,2))),0) as totalisr," +
            " ifnull(sum(if(tblventasinventario.idmoneda=2,round(precio*tblventasinventario.ieps/100,2),round(precio*tblventasinventario.ieps/100*tblventas.tipodecambio,2))),0) as totalieps," +
            " tblventas.idventa,ifnull(tblventas.serie,'*Nohay') as serie,tblventas.folio,tblclientes.idcuenta,concat(tblventas.serie,convert(tblventas.folio using utf8),' ',tblclientes.nombre) as ventaconcepto," +
            "if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as Cuenta," +
            "tblclientes.rfc,tblventas.tipodecambio,tblventas.idconversion,tblventas.fecha,ifnull((select tblventastimbrado.uuid from tblventastimbrado where tblventastimbrado.idventa=tblventas.idventa),'') as foliocfdi,round(tblventas.totalapagar,2) totalapagar" +
            " from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblventasdepositos on tblventasdepositos.idventa=tblventas.idventa where tblventas.estado=3 and tblventasdepositos.iddeposito=" + Dep.IdDeposito.ToString
            DR = Comm.ExecuteReader
            While DR.Read
                UnPago.Cantidad = DR("subtotal")
                UnPago.Idcuenta = DR("idcuenta")
                UnPago.IdDeposito = 0
                UnPago.Concepto = DR("ventaconcepto")
                UnPago.Iva = DR("totaliva")
                UnPago.ISR = DR("totalisr")
                UnPago.Retendido = DR("totalivaret")
                UnPago.Ieps = DR("totalieps")
                UnPago.Cuenta = DR("Cuenta")

                UnPago.ChequeTrans.BancoDestinoEx = ""
                UnPago.ChequeTrans.BancoEx = ""
                UnPago.ChequeTrans.BancoOrigenEx = ""
                UnPago.ChequeTrans.Beneficiario = ""
                UnPago.ChequeTrans.Fecha = ""
                UnPago.ChequeTrans.IdBancoDestino = 0
                UnPago.ChequeTrans.IdBancoOrigen = 0
                UnPago.ChequeTrans.IdMoneda = 0
                UnPago.ChequeTrans.Monto = 0
                UnPago.ChequeTrans.NoCuentaDestino = ""
                UnPago.ChequeTrans.NoCuentaOrigen = ""
                UnPago.ChequeTrans.NumeroCheque = ""
                UnPago.ChequeTrans.RFC = ""
                UnPago.ChequeTrans.TipodeCambio = 0

                UnPago.Uuid.Uuid = DR("foliocfdi")
                UnPago.Uuid.Fecha = DR("fecha")
                UnPago.Uuid.Monto = DR("totalapagar")
                UnPago.Uuid.RFC = DR("rfc")
                UnPago.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idconversion") = 2 Then
                    UnPago.Uuid.IdMoneda = 101
                Else
                    UnPago.Uuid.IdMoneda = 147
                End If
                UUIDsDepCon.Add(UnPago.Uuid)
                UUIDsDep.Add(UnPago.Uuid)
                UnPago.Folio = ""
                UnPago.IdProveedor = 0
                UnPago.IvaPor = 0
                UnPago.IVaRetPor = 0
                UnPago.IEpsPor = 0
                UnPago.NombreProveedor = ""
                TIVADepCon += UnPago.Iva
                TIEPSDepCon += UnPago.Ieps
                TISRDepCon += UnPago.ISR
                TIVARDepCon += UnPago.Retendido
                PagosListaCon.Add(UnPago)
            End While
            DR.Close()
        Next
        TIVADep = TIVADepCon + TIVADepCr
        TSISLDep = TNSLDep + TIVASLDep
        TIEPSDep = TIEPSDepCon + TIEPSDepCr
        TIVARDep = TIVARDepCon + TIVARDepCr
        TISRDep = TISRDepCon + TISRDepCr
        TSIDepCon = TNDepCon - TIVADepCon - TIEPSDepCon + TISRDepCon + TIVARDepCon
        TSIDepCr = TNDepCr - TIVADepCr - TIEPSDepCr + TISRDepCr + TIVARDepCr
        TSIDep = TSIDepCon + TSIDepCr + TSISLDep
        StrAfectadas = StrAfectadas.Trim
        'DocumentosProcesados(pModulo, pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
    End Sub
    Private Sub LlenaVariablesRetiros(pModulo As Integer, pFecha1 As String, pFecha2 As String, pIdMovimiento As Integer, pCredito As Byte, pIdSucursal As Integer)
        ' llenar variables****************************************FACTURAS***********
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select round(cantidad,2) as cantidad,round((cantidad/(1+(tblpagoprov.iva-tblpagoprov.ivaret+tblpagoprov.ieps)/100)*(tblpagoprov.iva/100)),2) as tiva,round((cantidad/(1+(tblpagoprov.iva-tblpagoprov.ivaret+tblpagoprov.ieps)/100)*(tblpagoprov.ivaret/100)),2) as tivaret,round((cantidad/(1+(tblpagoprov.iva-tblpagoprov.ivaret+tblpagoprov.ieps)/100)*(tblpagoprov.ieps/100)),2) as tieps,idpagoprov,tblcuentas.idccontable,referencia,if(tblcuentas.idccontable>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblcuentas.idccontable),'') as Cuenta,tblproveedores.nombre," +
            "tblproveedores.rfc,tblproveedores.nombre,tblpagoprov.fecha,tblcuentas.numero,tblpagoprov.bancoorigenex as nocheque,tblpagoprov.bancodestinoex,tblpagoprov.idmoneda,tblpagoprov.tipodecambio,(select clave from tblbancoscatalogo where tblbancoscatalogo.idbanco=tblpagoprov.idbancod) as idbancod,(select clave from tblbancoscatalogo where tblbancoscatalogo.idbanco=tblcuentas.banco) as bancoorigen,tblpagoprov.cuentadestino,tblcuentas.esextranjero,tblcuentas.nombreex,tblpagoprov.ivaret,tblpagoprov.ieps,tblpagoprov.iva,tblpagoprov.idproveedor,tblpagoprov.folio pfolio" +
            " from tblpagoprov inner join tblcuentas on tblcuentas.idcuenta=tblpagoprov.banco inner join tblproveedores on tblpagoprov.idproveedor=tblproveedores.idproveedor where "
        Comm.CommandText += " (select count(cp.idpagoprov) from tblcompraspagos cp where cp.idpagoprov=tblpagoprov.idpagoprov)=0 and (select count(cr.idpagoprov) from tblcomprasretiros cr where cr.idpagoprov=tblpagoprov.idpagoprov)=0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblpagoprov.fecha>='" + pFecha1 + "' and tblpagoprov.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and idpagoprov=" + pIdMovimiento.ToString
        End If
        DR = Comm.ExecuteReader
        Dim UnDeposito As stDepositos
        Dim Ivainfo As stIvaInfo
        TIVAxPagoRet.Clear()
        TIVAxPagoRetCon.Clear()
        TIVAxPagoRetCr.Clear()
        TIVAxPagoRetSL.Clear()
        UUIDsRet.Clear()
        UUIDsRetSL.Clear()
        UUIDsRetCon.Clear()
        UUIDsRetCr.Clear()
        DepositosListaRet.Clear()
        While DR.Read
            UnDeposito.Cantidad = DR("cantidad")
            TNRet += UnDeposito.Cantidad
            TNSLRet += UnDeposito.Cantidad
            UnDeposito.Idcuenta = DR("idccontable")
            UnDeposito.IdDeposito = DR("idpagoprov")
            UnDeposito.Concepto = DR("referencia")
            StrAfectadas += " " + UnDeposito.Concepto.Replace("FACTURAS: ", "").Replace("FACTURA: ", "").Replace("COMPRA: ", "")
            UnDeposito.Iva = DR("tiva")
            'If DR("haypagos") = 0 Then
            TIVARet += DR("tiva")
            TIVASLRet += DR("tiva")
            'End If
            Ivainfo.IdProveedor = DR("idproveedor")
            Ivainfo.IepsPor = DR("ieps")
            Ivainfo.Ivapor = DR("iva")
            Ivainfo.Ivaretpor = DR("ivaret")
            Ivainfo.NombreProveedor = DR("nombre")
            Ivainfo.TotalIeps = DR("tieps")
            Ivainfo.TotalIva = DR("tiva")
            Ivainfo.TotalIvaRet = DR("tivaret")
            Ivainfo.IdCuenta = DR("idccontable")
            Ivainfo.Cuenta = DR("Cuenta")
            Ivainfo.Concepto = DR("referencia")
            If Ivainfo.Ivapor = 0 And Ivainfo.IepsPor = 0 And Ivainfo.Ivaretpor = 0 Then
                Ivainfo.ValorActos = DR("cantidad")
            Else
                Ivainfo.ValorActos = 0
            End If
            TIVAxPagoRet.Add(Ivainfo)
            TIVAxPagoRetSL.Add(Ivainfo)

            UnDeposito.Retendido = DR("tivaret")
            UnDeposito.Ieps = DR("tieps")
            UnDeposito.IvaPor = DR("iva")
            UnDeposito.IVaRetPor = DR("ivaret")
            UnDeposito.IEpsPor = DR("ieps")
            UnDeposito.Cuenta = DR("Cuenta")
            UnDeposito.IdProveedor = DR("idproveedor")
            UnDeposito.NombreProveedor = DR("nombre")
            UnDeposito.Folio = ""
            UnDeposito.ChequeTrans.BancoDestinoEx = DR("bancodestinoex")
            UnDeposito.ChequeTrans.BancoEx = DR("esextranjero").ToString
            If DR("esextranjero") = 1 Then
                UnDeposito.ChequeTrans.BancoOrigenEx = DR("nombreex")
            Else
                UnDeposito.ChequeTrans.BancoOrigenEx = ""
            End If
            UnDeposito.ChequeTrans.Beneficiario = DR("nombre")
            UnDeposito.ChequeTrans.Fecha = DR("fecha")
            UnDeposito.ChequeTrans.IdBancoDestino = DR("idbancod")
            UnDeposito.ChequeTrans.IdBancoOrigen = DR("bancoorigen")
            UnDeposito.ChequeTrans.IdMoneda = DR("idmoneda")
            UnDeposito.ChequeTrans.Monto = DR("cantidad")
            UnDeposito.ChequeTrans.NoCuentaDestino = DR("cuentadestino")
            UnDeposito.ChequeTrans.NoCuentaOrigen = DR("numero")
            UnDeposito.ChequeTrans.NumeroCheque = DR("nocheque")
            UnDeposito.ChequeTrans.RFC = DR("rfc")
            UnDeposito.ChequeTrans.TipodeCambio = 0

            UnDeposito.Uuid.Uuid = ""
            UnDeposito.Uuid.Fecha = ""
            UnDeposito.Uuid.Monto = 0
            UnDeposito.Uuid.RFC = ""
            UnDeposito.Uuid.TipodeCambio = 0
            UnDeposito.Uuid.IdMoneda = 101

            Beneficiario1 += "insert into tblprovtemp(nombre,tipo) values('" + Replace(DR("pfolio") + " " + DR("nombre"), "'", "''").Trim + "',1);"
            DepositosListaRet.Add(UnDeposito)
        End While
        DR.Close()
        Dim UnUUid As stUuids
        For Each Dep As stDepositos In DepositosListaRet
            Comm.CommandText = "select * from tblbancosuuids where idpagoprov=" + Dep.IdDeposito.ToString
            DR = Comm.ExecuteReader
            UnUUid.Fecha = DR("fecha")
            UnUUid.IdDocumento = Dep.IdDeposito
            UnUUid.IdMoneda = DR("moneda")
            UnUUid.Monto = DR("monto")
            UnUUid.RFC = Dep.ChequeTrans.RFC
            UnUUid.TipodeCambio = DR("tipocambio")
            UnUUid.Uuid = DR("uuid")
            UUIDsRetSL.Add(UnUUid)
            UUIDsRet.Add(UnUUid)
        Next

        'PagosListaRet.Clear()
        Comm.CommandText = "select round(cantidad,2) as cantidad,round(cantidad-(cantidad/(1+iva/100)),2) as tiva,idpagoprov,tblcuentas.idccontable,referencia,if(tblcuentas.idccontable>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblcuentas.idccontable),'') as Cuenta,tblproveedores.nombre," +
            "tblproveedores.rfc,tblproveedores.nombre,tblpagoprov.fecha,tblcuentas.numero,tblpagoprov.bancoorigenex as nocheque,tblpagoprov.bancodestinoex,tblpagoprov.idmoneda,tblpagoprov.tipodecambio,(select clave from tblbancoscatalogo where tblbancoscatalogo.idbanco=tblpagoprov.idbancod) as idbancod,(select clave from tblbancoscatalogo where tblbancoscatalogo.idbanco=tblcuentas.banco) bancoorigen,tblpagoprov.cuentadestino,tblcuentas.esextranjero,tblcuentas.nombreex,tblpagoprov.folio pfolio" +
            " from tblpagoprov inner join tblcuentas on tblcuentas.idcuenta=tblpagoprov.banco inner join tblproveedores on tblpagoprov.idproveedor=tblproveedores.idproveedor where "
        Comm.CommandText += " (select count(cp.idpagoprov) from tblcompraspagos cp where cp.idpagoprov=tblpagoprov.idpagoprov)>0" 'and (select count(cr.idpagoprov) from tblcomprasretiros cr where cr.idpagoprov=tblpagoprov.idpagoprov)=0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblpagoprov.fecha>='" + pFecha1 + "' and tblpagoprov.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and idpagoprov=" + pIdMovimiento.ToString
        End If
        DR = Comm.ExecuteReader
        'Dim UnDeposito As stDepositos
        DepositosListaRetCr.Clear()
        Dim StrTemp As String = ""
        While DR.Read
            UnDeposito.Cantidad = DR("cantidad")
            TNRet += UnDeposito.Cantidad
            TNRetCr += UnDeposito.Cantidad
            UnDeposito.Idcuenta = DR("idccontable")
            UnDeposito.IdDeposito = DR("idpagoprov")
            UnDeposito.Concepto = DR("referencia")
            If StrTemp.Contains(UnDeposito.Concepto) = False Then
                StrTemp += " " + UnDeposito.Concepto.Replace("FACTURAS: ", "").Replace("FACTURA: ", "").Replace("COMPRA: ", "")
            End If
            UnDeposito.Iva = 0
            'If DR("haypagos") = 0 Then
            'TIVA += DR("tiva")
            'End If
            UnDeposito.Retendido = 0
            UnDeposito.Ieps = 0
            UnDeposito.Cuenta = DR("Cuenta")

            UnDeposito.ChequeTrans.BancoDestinoEx = DR("bancodestinoex")
            UnDeposito.ChequeTrans.BancoEx = DR("esextranjero").ToString
            If DR("esextranjero") = 1 Then
                UnDeposito.ChequeTrans.BancoOrigenEx = DR("nombreex")
            Else
                UnDeposito.ChequeTrans.BancoOrigenEx = ""
            End If
            UnDeposito.ChequeTrans.Beneficiario = DR("nombre")
            UnDeposito.ChequeTrans.Fecha = DR("fecha")
            UnDeposito.ChequeTrans.IdBancoDestino = DR("idbancod")
            UnDeposito.ChequeTrans.IdBancoOrigen = DR("bancoorigen")
            UnDeposito.ChequeTrans.IdMoneda = DR("idmoneda")
            UnDeposito.ChequeTrans.Monto = DR("cantidad")
            UnDeposito.ChequeTrans.NoCuentaDestino = DR("cuentadestino")
            UnDeposito.ChequeTrans.NoCuentaOrigen = DR("numero")
            UnDeposito.ChequeTrans.NumeroCheque = DR("nocheque")
            UnDeposito.ChequeTrans.RFC = DR("rfc")
            UnDeposito.ChequeTrans.TipodeCambio = 0
            UnDeposito.IEpsPor = 0
            UnDeposito.IvaPor = 0
            UnDeposito.IVaRetPor = 0
            UnDeposito.Uuid.Uuid = ""
            UnDeposito.Uuid.Fecha = ""
            UnDeposito.Uuid.Monto = 0
            UnDeposito.Uuid.RFC = ""
            UnDeposito.Uuid.TipodeCambio = 0
            UnDeposito.Uuid.IdMoneda = 101
            UnDeposito.Folio = ""
            UnDeposito.IdProveedor = 0
            UnDeposito.IvaPor = 0
            UnDeposito.IVaRetPor = 0
            UnDeposito.IEpsPor = 0
            UnDeposito.NombreProveedor = ""
            Beneficiario2 += "insert into tblprovtemp(nombre,tipo) values('" + Replace(DR("pfolio") + " " + DR("nombre"), "'", "''").Trim + "',2);"
            DepositosListaRetCr.Add(UnDeposito)
        End While
        DR.Close()
        PagosListaRet.Clear()
        If StrTemp <> "" Then
            If StrAfectadas.Length <> 0 Then StrAfectadas += " "
            StrAfectadas += " " + StrTemp
        End If

        For Each Dep As stDepositos In DepositosListaRetCr
            Comm.CommandText = "select cantidad,idcompra,idcargo,iddocumentod," +
                "if(idcompra<>0,ifnull((select concat(v.serie,convert(v.folioi using utf8),' ',tblproveedores.nombre) from tblcompras v inner join tblproveedores on v.idproveedor=tblproveedores.idproveedor where v.idcompra=tblcompraspagos.idcompra),''),'') as ventaconcepto," +
                "if(idcargo<>0,ifnull((select concat(v.serie,convert(v.folioi using utf8),' ',tblproveedores.nombre) from tblnotasdecargocompras v inner join tblproveedores on v.idproveedor=tblproveedores.idproveedor where v.idcargo=tblcompraspagos.idcargo),''),'') as ncconcepto," +
                "if(iddocumentod<>0,ifnull((select concat(if(v.tiposaldo=0,concat(v.serie,convert(v.folio using utf8)),concat(v.seriereferencia,convert(v.folioreferencia using utf8))),' ',tblproveedores.nombre) from tbldocumentosproveedores v inner join tblproveedores on v.idproveedor=tblproveedores.idproveedor where v.iddocumento=tblcompraspagos.iddocumentod),''),'') as docconcepto," +
                "(select idcuenta from tblproveedores where tblproveedores.idproveedor=tblcompraspagos.idproveedor) as ccontable," +
                "if(iddocumentod<>0,ifnull((select cantidad*(dc.totalapagar-(dc.totalapagar/1.16))/dc.totalapagar from tbldocumentosproveedores dc where dc.iddocumento=iddocumentod),0)," +
                "if(idcargo<>0,spdaivaproporcionalnccompras(idcargo,cantidad),spdaivaproporcionalfcompras(idcompra,cantidad,0))) as ivapago," +
                "if(idcompra<>0,spdaivaproporcionalfcompras(idcompra,cantidad,2),0) as ivaretpago," +
                "if(idcompra<>0,spdaivaproporcionalfcompras(idcompra,cantidad,1),0) as iepspago," +
                 "if(idcompra<>0,(select avg(tblcomprasdetalles.ivaretenido) from tblcomprasdetalles where tblcomprasdetalles.idcompra=idcompra and tblcomprasdetalles.ivaretenido<>0),0) as ivaretpor," +
                "if(idcompra<>0,ifnull((select avg(tblcomprasdetalles.ieps) from tblcomprasdetalles where tblcomprasdetalles.idcompra=idcompra and tblcomprasdetalles.ieps<>0),0),0) as iepspor," +
                "if(idcompra<>0,ifnull((select avg(tblcomprasdetalles.iva) from tblcomprasdetalles where tblcomprasdetalles.idcompra=idcompra and tblcomprasdetalles.iva<>0),0),0) as ivapor," +
                "if(idcargo<>0,ifnull((select avg(tblnotasdecargodetallesc.iva) from tblnotasdecargodetallesc where tblnotasdecargodetallesc.idcargo=idcargo and tblnotasdecargodetallesc.iva<>0),0),0) as ivancpor," +
                "if(idcompra<>0,ifnull((select tblcompras.referencia from tblcompras where tblcompras.idcompra=idcompra limit 1),''),'') as foliocompra," +
                "if(idcargo<>0,ifnull((select tblnotasdecargocompras.folio from tblnotasdecargocompras where tblnotasdecargocompras.idcargo=idcargo limit 1),''),'') as foliocargo," +
                "if(iddocumento<>0,ifnull((select concat(tbldocumentosproveedores.seriereferencia,tbldocumentosproveedores.folioreferencia) from tbldocumentosproveedores where tbldocumentosproveedores.iddocumento=iddocumento limit 1),''),'') as foliodoc," +
                "tblcompraspagos.idproveedor," +
                "if(idcompra<>0,(select foliocfdi from tblcompras where tblcompras.idcompra=tblcompraspagos.idcompra),if(idcargo<>0,(select foliocfdi from tblnotasdecargocompras where tblnotasdecargocompras.idcargo=tblcompraspagos.idcargo),'')) as uuid," +
                "if(idcompra<>0,(select fecha from tblcompras where tblcompras.idcompra=tblcompraspagos.idcompra),if(idcargo<>0,(select fecha from tblnotasdecargocompras where tblnotasdecargocompras.idcargo=tblcompraspagos.idcargo),'')) as fecha," +
                "if(idcompra<>0,(select round(totalapagar,2) from tblcompras where tblcompras.idcompra=tblcompraspagos.idcompra),if(idcargo<>0,(select round(totalapagar,2) from tblnotasdecargocompras where tblnotasdecargocompras.idcargo=tblcompraspagos.idcargo),0)) as monto," +
                "(select rfc from tblproveedores where tblproveedores.idproveedor=tblcompraspagos.idproveedor) as prfc," +
                "if(idcompra<>0,(select round(tipodecambio,2) from tblcompras where tblcompras.idcompra=tblcompraspagos.idcompra),if(idcargo<>0,(select round(tipodecambio,2) from tblnotasdecargocompras where tblnotasdecargocompras.idcargo=tblcompraspagos.idcargo),0)) as tipodecambio," +
                "if(idcompra<>0,(select idmoneda from tblcompras where tblcompras.idcompra=tblcompraspagos.idcompra),if(idcargo<>0,(select idmoneda from tblnotasdecargocompras where tblnotasdecargocompras.idcargo=tblcompraspagos.idcargo),2)) as idmoneda," +
                "if((select idcuenta from tblproveedores where tblproveedores.idproveedor=tblcompraspagos.idproveedor)>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN5.ToString + ",'0'))) from tblccontables c where c.idccontable=(select idcuenta from tblproveedores where tblproveedores.idproveedor=tblcompraspagos.idproveedor)),'') as Cuenta," +
                "(select nombre from tblproveedores where tblproveedores.idproveedor=tblcompraspagos.idproveedor) as pnombre" +
                " from tblcompraspagos where idpagoprov=" + Dep.IdDeposito.ToString
            DR = Comm.ExecuteReader
            Dim UnPago As stDepositos
            While DR.Read
                UnPago.Cantidad = DR("cantidad")
                UnPago.Concepto = DR("docconcepto")
                UnPago.Idcuenta = DR("ccontable")
                UnPago.IdDeposito = 0
                UnPago.Folio = ""
                If DR("idcompra") <> 0 Then
                    UnPago.Concepto = DR("ventaconcepto")
                    UnPago.Folio = DR("foliocompra")
                End If
                If DR("idcargo") <> 0 Then
                    UnPago.Concepto = DR("ncconcepto")
                    UnPago.Folio = DR("foliocargo")
                End If
                If DR("iddocumentod") <> 0 Then
                    UnPago.Concepto = DR("docconcepto")
                    UnPago.Folio = DR("foliodoc")
                End If
                UnPago.IdProveedor = DR("idproveedor")
                UnPago.Iva = DR("ivapago")
                UnPago.ISR = 0
                UnPago.Retendido = DR("ivaretpago")
                UnPago.Ieps = DR("iepspago")
                UnPago.Cuenta = DR("Cuenta")
                If DR("iddocumentod") = 0 Then
                    UnPago.IvaPor = DR("ivapor")
                Else
                    UnPago.IvaPor = 16
                End If
                UnPago.IEpsPor = DR("iepspor")
                UnPago.IVaRetPor = DR("ivaretpor")

                Ivainfo.IdProveedor = DR("idproveedor")
                Ivainfo.IepsPor = DR("iepspor")
                Ivainfo.Ivapor = UnPago.IvaPor
                Ivainfo.Ivaretpor = DR("ivaretpor")
                Ivainfo.NombreProveedor = DR("pnombre")
                Ivainfo.TotalIeps = DR("iepspago")
                Ivainfo.TotalIva = DR("ivapago")
                Ivainfo.TotalIvaRet = DR("ivaretpago")
                If Ivainfo.Ivapor = 0 And Ivainfo.IepsPor = 0 And Ivainfo.Ivaretpor = 0 Then
                    Ivainfo.ValorActos = DR("cantidad")
                Else
                    Ivainfo.ValorActos = 0
                End If
                Ivainfo.Cuenta = DR("Cuenta")
                Ivainfo.IdCuenta = DR("ccontable")
                Ivainfo.Concepto = UnPago.Concepto

                TIVAxPagoRet.Add(Ivainfo)
                TIVAxPagoRetCr.Add(Ivainfo)

                UnPago.ChequeTrans.BancoDestinoEx = ""
                UnPago.ChequeTrans.BancoEx = ""
                UnPago.ChequeTrans.BancoOrigenEx = ""
                UnPago.ChequeTrans.Beneficiario = ""
                UnPago.ChequeTrans.Fecha = ""
                UnPago.ChequeTrans.IdBancoDestino = 0
                UnPago.ChequeTrans.IdBancoOrigen = 0
                UnPago.ChequeTrans.IdMoneda = 0
                UnPago.ChequeTrans.Monto = 0
                UnPago.ChequeTrans.NoCuentaDestino = ""
                UnPago.ChequeTrans.NoCuentaOrigen = ""
                UnPago.ChequeTrans.NumeroCheque = ""
                UnPago.ChequeTrans.RFC = ""
                UnPago.ChequeTrans.TipodeCambio = 0
                UnPago.NombreProveedor = DR("pnombre")
                UnPago.Uuid.Uuid = DR("uuid")
                UnPago.Uuid.Fecha = DR("fecha")
                UnPago.Uuid.Monto = DR("Monto")
                UnPago.Uuid.RFC = DR("prfc")
                UnPago.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    UnPago.Uuid.IdMoneda = 101
                Else
                    UnPago.Uuid.IdMoneda = 147
                End If
                UnPago.Uuid.IdDocumento = 0
                If DR("idcompra") <> 0 Or DR("idcargo") <> 0 Then
                    UUIDsRetCr.Add(UnPago.Uuid)
                    UUIDsRet.Add(UnPago.Uuid)
                End If
                TIVARetCr += UnPago.Iva
                TIEPSRetCr += UnPago.Ieps
                TISRRetCr += UnPago.ISR
                TIVARRetCr += UnPago.Retendido
                PagosListaRet.Add(UnPago)
            End While
            DR.Close()
        Next


        'PagosListaRetCon.Clear()
        Comm.CommandText = "select round(cantidad,2) as cantidad,round(cantidad-(cantidad/(1+iva/100)),2) as tiva,idpagoprov,tblcuentas.idccontable,referencia,if(tblcuentas.idccontable>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblcuentas.idccontable),'') as Cuenta,tblproveedores.nombre," +
            "tblproveedores.rfc,tblproveedores.nombre,tblpagoprov.fecha,tblcuentas.numero,tblpagoprov.bancoorigenex as nocheque,tblpagoprov.bancodestinoex,tblpagoprov.idmoneda,tblpagoprov.tipodecambio,(select clave from tblbancoscatalogo where tblbancoscatalogo.idbanco=tblpagoprov.idbancod) idbancod,(select clave from tblbancoscatalogo where tblbancoscatalogo.idbanco=tblcuentas.banco) bancoorigen,tblpagoprov.cuentadestino,tblcuentas.esextranjero,tblcuentas.nombreex,tblpagoprov.folio pfolio" +
            " from tblpagoprov inner join tblcuentas on tblcuentas.idcuenta=tblpagoprov.banco inner join tblproveedores on tblpagoprov.idproveedor=tblproveedores.idproveedor where"
        Comm.CommandText += " (select count(cr.idpagoprov) from tblcomprasretiros cr where cr.idpagoprov=tblpagoprov.idpagoprov)>0"
        If pIdMovimiento = 0 Then
            Comm.CommandText += " and tblpagoprov.fecha>='" + pFecha1 + "' and tblpagoprov.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and idpagoprov=" + pIdMovimiento.ToString
        End If
        DR = Comm.ExecuteReader
        'Dim UnDeposito As stDepositos
        StrTemp = ""
        DepositosListaRetCon.Clear()
        While DR.Read
            UnDeposito.Cantidad = DR("cantidad")
            TNRet += UnDeposito.Cantidad
            TNRetCon += UnDeposito.Cantidad
            UnDeposito.Idcuenta = DR("idccontable")
            UnDeposito.IdDeposito = DR("idpagoprov")
            UnDeposito.Concepto = DR("referencia")
            If StrTemp.Contains(UnDeposito.Concepto) = False Then
                StrTemp += " " + UnDeposito.Concepto.Replace("FACTURAS: ", "").Replace("FACTURA: ", "").Replace("COMPRA: ", "")
            End If
            UnDeposito.Iva = 0
            'If DR("haypagos") = 0 Then
            'TIVA += DR("tiva")
            'End If
            UnDeposito.Retendido = 0
            UnDeposito.Ieps = 0
            UnDeposito.Cuenta = DR("Cuenta")

            UnDeposito.ChequeTrans.BancoDestinoEx = DR("bancodestinoex")
            UnDeposito.ChequeTrans.BancoEx = DR("esextranjero").ToString
            If DR("esextranjero") = 1 Then
                UnDeposito.ChequeTrans.BancoOrigenEx = DR("nombreex")
            Else
                UnDeposito.ChequeTrans.BancoOrigenEx = ""
            End If
            UnDeposito.ChequeTrans.Beneficiario = DR("nombre")
            UnDeposito.ChequeTrans.Fecha = DR("fecha")
            UnDeposito.ChequeTrans.IdBancoDestino = DR("idbancod")
            UnDeposito.ChequeTrans.IdBancoOrigen = DR("bancoorigen")
            UnDeposito.ChequeTrans.IdMoneda = DR("idmoneda")
            UnDeposito.ChequeTrans.Monto = DR("cantidad")
            UnDeposito.ChequeTrans.NoCuentaDestino = DR("cuentadestino")
            UnDeposito.ChequeTrans.NoCuentaOrigen = DR("numero")
            UnDeposito.ChequeTrans.NumeroCheque = DR("nocheque")
            UnDeposito.ChequeTrans.RFC = DR("rfc")
            UnDeposito.ChequeTrans.TipodeCambio = 0

            UnDeposito.Uuid.Uuid = ""
            UnDeposito.Uuid.Fecha = ""
            UnDeposito.Uuid.Monto = 0
            UnDeposito.Uuid.RFC = ""
            UnDeposito.Uuid.TipodeCambio = 0
            UnDeposito.Uuid.IdMoneda = 101
            UnDeposito.Folio = ""
            UnDeposito.IdProveedor = 0
            UnDeposito.IvaPor = 0
            UnDeposito.IVaRetPor = 0
            UnDeposito.IEpsPor = 0
            UnDeposito.NombreProveedor = ""
            Beneficiario3 += "insert into tblprovtemp(nombre,tipo) values('" + Replace(DR("pfolio") + " " + DR("nombre"), "'", "''").Trim + "',3);"
            DepositosListaRetCon.Add(UnDeposito)
        End While
        DR.Close()
        PagosListaRetCon.Clear()
        If StrTemp <> "" Then
            If StrAfectadas.Length <> 0 Then StrAfectadas += " "
            StrAfectadas += " " + StrTemp
        End If
        For Each Dep As stDepositos In DepositosListaRetCon
            Comm.CommandText = "select ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio,2),round(tblcomprasdetalles.precio*tblcompras.tipodecambio,2))),0) as subtotal," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio,2))),0) as totaliva," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio,2))),0) as totalivaret," +
            " ifnull(sum(if(tblcomprasdetalles.idmoneda=2,round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100,2),round(tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio,2))),0) as totalieps," +
            " ifnull(avg(distinct tblcomprasdetalles.iva),0) as poriva," +
            " ifnull(avg(distinct tblcomprasdetalles.ivaretenido),0) as porivaret," +
            " ifnull(avg(distinct tblcomprasdetalles.ieps),0) as porieps," +
            " tblcompras.idcompra,ifnull(concat(tblcompras.referencia,' ',tblcompras.serie),'*Nohay') as serie,tblcompras.folioi,tblproveedores.idcuenta,tblcompras.idproveedor,tblcompras.referencia," +
            " concat(tblcompras.serie,convert(tblcompras.folioi using utf8),' ',tblproveedores.nombre) as ventaconcepto," +
            " if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as Cuenta," +
            " tblproveedores.rfc,tblproveedores.rfc,tblproveedores.nombre,tblcompras.foliocfdi,tblcompras.idmoneda,tblcompras.tipodecambio,tblcompras.fecha,round(tblcompras.totalapagar,2) totalapagar" +
            " from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor INNER join tblcomprasretiros on tblcomprasretiros.idcompra=tblcompras.idcompra where tblcompras.estado=3 and tblcomprasretiros.idpagoprov=" + Dep.IdDeposito.ToString
            DR = Comm.ExecuteReader
            Dim UnPago As stDepositos
            While DR.Read
                UnPago.Cantidad = DR("subtotal")
                'UnPago.Concepto = DR("docconcepto")
                UnPago.Idcuenta = DR("idcuenta")
                UnPago.IdDeposito = 0
                'If DR("idcompra") <> 0 Then
                UnPago.Concepto = DR("ventaconcepto")
                'End If
                'If DR("idcargo") <> 0 Then
                'UnPago.Concepto = DR("ncconcepto")
                'End If
                'If DR("iddocumentod") <> 0 Then
                ' UnPago.Concepto = DR("docconcepto")
                'End If
                UnPago.Iva = DR("totaliva")
                UnPago.ISR = 0
                UnPago.Retendido = DR("totalivaret")
                UnPago.Ieps = DR("totalieps")
                UnPago.Cuenta = DR("Cuenta")
                UnPago.IdProveedor = DR("idproveedor")
                UnPago.Folio = DR("referencia")
                UnPago.IvaPor = DR("poriva")
                UnPago.IVaRetPor = DR("porivaret")
                UnPago.IEpsPor = DR("porieps")

                Ivainfo.IdProveedor = DR("idproveedor")
                Ivainfo.IepsPor = DR("porieps")
                Ivainfo.Ivapor = DR("poriva")
                Ivainfo.Ivaretpor = DR("porivaret")
                Ivainfo.NombreProveedor = DR("nombre")
                Ivainfo.TotalIeps = DR("totalieps")
                Ivainfo.TotalIva = DR("totaliva")
                Ivainfo.TotalIvaRet = DR("totalivaret")
                If Ivainfo.Ivapor = 0 And Ivainfo.IepsPor = 0 And Ivainfo.Ivaretpor = 0 Then
                    Ivainfo.ValorActos = DR("cantidad")
                Else
                    Ivainfo.ValorActos = 0
                End If
                Ivainfo.IdCuenta = DR("idcuenta")
                Ivainfo.Cuenta = DR("Cuenta")
                Ivainfo.Concepto = DR("ventaconcepto")

                TIVAxPagoRet.Add(Ivainfo)
                TIVAxPagoRetCon.Add(Ivainfo)

                UnPago.ChequeTrans.BancoDestinoEx = ""
                UnPago.ChequeTrans.BancoEx = ""
                UnPago.ChequeTrans.BancoOrigenEx = ""
                UnPago.ChequeTrans.Beneficiario = ""
                UnPago.ChequeTrans.Fecha = ""
                UnPago.ChequeTrans.IdBancoDestino = 0
                UnPago.ChequeTrans.IdBancoOrigen = 0
                UnPago.ChequeTrans.IdMoneda = 0
                UnPago.ChequeTrans.Monto = 0
                UnPago.ChequeTrans.NoCuentaDestino = ""
                UnPago.ChequeTrans.NoCuentaOrigen = ""
                UnPago.ChequeTrans.NumeroCheque = ""
                UnPago.ChequeTrans.RFC = ""
                UnPago.ChequeTrans.TipodeCambio = 0
                UnPago.NombreProveedor = DR("nombre")
                UnPago.Uuid.Uuid = DR("foliocfdi")
                UnPago.Uuid.Fecha = DR("fecha")
                UnPago.Uuid.Monto = DR("totalapagar")
                UnPago.Uuid.RFC = DR("rfc")
                UnPago.Uuid.TipodeCambio = DR("tipodecambio")
                If DR("idmoneda") = 2 Then
                    UnPago.Uuid.IdMoneda = 101
                Else
                    UnPago.Uuid.IdMoneda = 147
                End If
                UUIDsRetCon.Add(UnPago.Uuid)
                UUIDsRet.Add(UnPago.Uuid)
                TIVARetCon += UnPago.Iva
                TIEPSRetCon += UnPago.Ieps
                TISRRetCon += UnPago.ISR
                TIVARRetCon += UnPago.Retendido
                PagosListaRetCon.Add(UnPago)
            End While
            DR.Close()
        Next
        TSISLRet = TNSLRet - TIVASLRet
        TIVARet = TIVARet + TIVARetCon + TIVARetCr
        TIEPSRet = TIEPSRetCon + TIEPSRetCr
        TIVARRet = TIVARRetCon + TIVARRetCr
        TSIRetCon = TNRetCon - TIVARetCon - TIEPSRetCon + TISRRetCon + TIVARRetCon
        TSIRetCr = TNRetCr - TIVARetCr - TIEPSRetCr + TISRRetCr + TIVARRetCr
        TSIRet = TSIRetCon + TSIRetCr + TSISLRet
        StrAfectadas = StrAfectadas.Trim
        'DocumentosProcesados(pModulo, pFecha1, pFecha2, pIdMovimiento, pIdSucursal, pCredito)
    End Sub
    Public Function InsertayDaProveedoresTemp(pB1 As Boolean, pB2 As Boolean, pB3 As Boolean) As String
        Dim Provs As String = ""
        Comm.CommandText = "delete from tblprovtemp"
        Comm.ExecuteNonQuery()
        If pB1 And Beneficiario1 <> "" Then
            Comm.CommandText = Beneficiario1
            Comm.ExecuteNonQuery()
        End If
        If pB2 And Beneficiario2 <> "" Then
            Comm.CommandText = Beneficiario2
            Comm.ExecuteNonQuery()
        End If
        If pB3 And Beneficiario3 <> "" Then
            Comm.CommandText = Beneficiario3
            Comm.ExecuteNonQuery()
        End If
        Dim dR As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select distinct nombre from tblprovtemp"
        dR = Comm.ExecuteReader
        While dR.Read
            Provs += dR("nombre") + " "
        End While
        dR.Close()
        Return Provs
    End Function
    Public Sub GuardaUsada(pIdPoliza As Integer, pIdMascara As Integer, pFecha As String)
        Comm.CommandText = "insert into tblmascarasusadas(idmascara,fecha,idpoliza,hora) values(" + pIdMascara.ToString + ",'" + pFecha + "'," + pIdPoliza.ToString + ",'" + Date.Now.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub

    Public Function ConsultaUsadas() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblmascarasusadas.idpoliza,tblcontabilidadmascaras.titulo,tblpolizas.tipo,tblpolizas.numero,tblpolizas.fecha,tblpolizas.concepto from tblmascarasusadas inner join tblpolizas on tblmascarasusadas.idpoliza=tblpolizas.id inner join tblcontabilidadmascaras on tblcontabilidadmascaras.idmascara=tblmascarasusadas.idmascara order by tblmascarasusadas.fecha desc,tblmascarasusadas.hora desc,tblpolizas.tipo,tblpolizas.numero"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmascaras")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblmascaras").DefaultView
    End Function
    ''' <summary>
    ''' Valores donde: 0 Clientes, 1 Proveedores, 2 Trabajadores, 3 Cuenta Banco
    ''' </summary>
    ''' <param name="idGenerico"></param>
    ''' <param name="donde"></param>
    ''' <remarks></remarks>
    Public Sub DaCuentaComodin(idGenerico As Integer, donde As Byte)
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Select Case donde
            Case 0
                Comm.CommandText = "select idcuenta as idcuentam,if(tblclientes.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblclientes.idcuenta),'') as cuentamov from tblclientes where idcliente=" + idGenerico.ToString
            Case 1
                Comm.CommandText = "select idcuenta as idcuentam,if(tblproveedores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblproveedores.idcuenta),'') as cuentamov from tblproveedores where idproveedor=" + idGenerico.ToString
            Case 2
                Comm.CommandText = "select idcuenta as idcuentam,if(tbltrabajadores.idcuenta>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tbltrabajadores.idcuenta),'') as cuentamov from tbltrabajadores where idtrabajador=" + idGenerico.ToString
            Case 3
                Comm.CommandText = "select tblcuentas.idccontable as idcuentam,if(tblcuentas.idccontable>0,(select concat(LPAD(c.cuenta," + PN1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + pN2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + pN3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + pN4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + pN2.ToString + ",'0'))) from tblccontables c where c.idccontable=tblcuentas.idccontable),'') as Cuentamov from tblcuentas where idcuenta=" + idGenerico.ToString
        End Select
        DR = Comm.ExecuteReader
        If DR.Read Then
            Select Case donde
                Case 0
                    IdCuentaCMov = DR("idcuentam")
                    CuentaCMov = DR("cuentamov")
                Case 1
                    IdCuentaPMov = DR("idcuentam")
                    CuentaPMov = DR("cuentamov")
                Case 2
                    IdCuentaTMov = DR("idcuentam")
                    CuentaTMov = DR("cuentamov")
                Case 3
                    IdCuentaBMov = DR("idcuentam")
                    CuentaBMov = DR("cuentamov")
            End Select
            
        End If
        DR.Close()
    End Sub

    Public Function Creardesde(pIdMascara As Integer) As Integer
        Dim Idmascaran As Integer
        Comm.CommandText = "insert into tblcontabilidadmascaras(titulo,modulo,tipo,activo,estado,credito,canceladas,idsucursal,idclasificacion,tipopoliza) select titulo,modulo,tipo,activo,1,credito,canceladas,idsucursal,idclasificacion,tipopoliza from tblcontabilidadmascaras where idmascara=" + pIdMascara.ToString + ";" +
            "select max(idmascara) from tblcontabilidadmascaras;"
        Idmascaran = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblcontabilidadmascarasd(idvariable,idcuenta,cargo,abono,idmascara,negativo,beneficiario,modulo,inpagoinfo,inuuids) select idvariable,idcuenta,cargo,abono," + Idmascaran.ToString + ",negativo,beneficiario,modulo,inpagoinfo,inuuids from tblcontabilidadmascarasd where idmascara=" + pIdMascara.ToString
        Comm.ExecuteNonQuery()
        Return Idmascaran
    End Function

    Public Function CuantasHay(pModulo As Integer, pCancelado As Byte, pForma As Byte) As Integer
        Comm.CommandText = "select count(idmascara) from tblcontabilidadmascaras where activo=1 and tipo=3 and modulo=" + pModulo.ToString
        If pForma < 2 Then
            Comm.CommandText += " and credito=" + pForma.ToString
        End If
        Return Comm.ExecuteScalar
    End Function
    Public Function DaMascaraActiva(pModulo As Integer, pCancelado As Byte, pForma As Byte) As Integer
        Comm.CommandText = "select idmascara from tblcontabilidadmascaras where activo=1 and tipo=3 and modulo=" + pModulo.ToString
        If pForma < 2 Then
            Comm.CommandText += " and credito=" + pForma.ToString
        End If
        Return Comm.ExecuteScalar
    End Function
End Class
