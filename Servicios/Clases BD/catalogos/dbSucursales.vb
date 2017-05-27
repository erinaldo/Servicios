Public Class dbSucursales
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Direccion As String
    Public Telefono As String
    Public Email As String
    Public Contacto As String
    Public Clave As String
    Public RFC As String
    Public Giro As String
    Public Ciudad As String
    Public CP As String
    Public Estado As String
    Public Pais As String
    Public Direccion2 As String
    Public Ciudad2 As String
    Public CP2 As String
    Public Estado2 As String
    Public Pais2 As String
    Public NombreFiscal As String
    Public NoExterior As String
    Public NoInterior As String
    Public Colonia As String
    Public Municipio As String
    Public ReferenciaDomicilio As String

    Public NoExterior2 As String
    Public NoInterior2 As String
    Public Colonia2 As String
    Public Municipio2 As String
    Public ReferenciaDomicilio2 As String
    
    Public Serie As String
    Public FolioInicial As Integer
    Public FolioFinal As Integer
    Public Formato As String
    Public Impuesto As Double
    Public idAlmacen As Integer
    Public RegimenFiscal As String
    Public CURP As String

    Public HayExp As Boolean
    Public LugarExp As String
    Public CalleExp As String
    Public NumExp As String
    Public IdAlmacenC As Integer
    Public IdAlmacenM As Integer

    Public idUsuarioAlta As Integer
    Public fechaAlta As String
    Public horaAlta As String
    Public idUsuarioCambio As Integer
    Public fechaCambio As String
    Public horaCambio As String
    Public idTipo As Integer
    Public RegistroPatronal As String
    Public ClaveRegimen As Integer

    Public cColonia As String
    Public cLocalidad As String
    Public cMunicipio As String
    Public cEstado As String
    Public cPais As String
    Public cRegimen2 As String


    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Direccion = ""
        Telefono = ""
        Email = ""
        Contacto = ""
        Clave = ""
        RFC = ""
        Giro = ""
        Ciudad = ""
        CP = ""
        Estado = ""
        Pais = ""
        Ciudad2 = ""
        CP2 = ""
        Estado2 = ""
        Pais2 = ""
        Direccion2 = ""
        RegimenFiscal = ""
        NoExterior = ""
        NoInterior = ""
        Colonia = ""
        Municipio = ""
        ReferenciaDomicilio = ""

        NoExterior2 = ""
        NoInterior2 = ""
        Colonia2 = ""
        Municipio2 = ""
        ReferenciaDomicilio2 = ""
        Serie = ""
        FolioInicial = 1
        FolioFinal = 1
        Formato = ""
        NombreFiscal = ""
        Impuesto = 0
        idAlmacen = 0
        IdAlmacenC = 0
        IdAlmacenM = 0
        idTipo = 0
        CURP = ""
        ClaveRegimen = 0
        RegistroPatronal = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Nombre = ""
        Direccion = ""
        Telefono = ""
        Email = ""
        Contacto = ""
        Clave = ""
        RFC = ""
        Giro = ""
        Ciudad = ""
        CP = ""
        Estado = ""
        Pais = ""
        Ciudad2 = ""
        CP2 = ""
        Estado2 = ""
        Pais2 = ""
        Direccion2 = ""
        RegimenFiscal = ""
        NoExterior = ""
        NoInterior = ""
        Colonia = ""
        Municipio = ""
        ReferenciaDomicilio = ""
        RegistroPatronal = ""
        NoExterior2 = ""
        NoInterior2 = ""
        Colonia2 = ""
        Municipio2 = ""
        ReferenciaDomicilio2 = ""
        Serie = ""
        FolioInicial = 1
        FolioFinal = 1
        Formato = ""
        NombreFiscal = ""
        Impuesto = 0
        idAlmacen = 0
        IdAlmacenC = 0
        IdAlmacenM = 0
        idTipo = 0
        CURP = ""
        ClaveRegimen = 0
        LlenaDatos()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pContacto As String, ByVal pClave As String, ByVal pRFC As String, ByVal pGiro As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String, ByVal pDireccion2 As String, ByVal pCiudad2 As String, ByVal pCP2 As String, ByVal pEstado2 As String, ByVal pPais2 As String, ByVal pNoexterior As String, ByVal pNoInterior As String, ByVal pColonia As String, ByVal pMunicipio As String, ByVal pReferenciaDomicilio As String, ByVal pNoexterior2 As String, ByVal pNoInterior2 As String, ByVal pColonia2 As String, ByVal pMunicipio2 As String, ByVal pReferenciaDomicilio2 As String, ByVal pSerie As String, ByVal pFolioInicial As Integer, ByVal pFolioFinal As Integer, ByVal pFormato As String, ByVal pImpuesto As Double, ByVal pNombreFiscal As String, ByVal pRegimenFiscal As String, ByVal pCurp As String, pidTipo As Integer, pRegistroPatronal As String, pClaveregimen As Integer, pClaveColonia As String, pClavelocalidad As String, pClaveMunicipio As String, pClaveEstado As String, pClavePais As String, pClaveRegimen2 As Integer)
        NoExterior = pNoexterior
        NoInterior = pNoInterior
        Colonia = pColonia
        Municipio = pMunicipio
        ReferenciaDomicilio = pReferenciaDomicilio

        NoExterior2 = pNoexterior2
        NoInterior2 = pNoInterior2
        Colonia2 = pColonia2
        Municipio2 = pMunicipio2
        ReferenciaDomicilio2 = pReferenciaDomicilio2

        Nombre = pNombre
        Direccion = pDireccion
        Telefono = pTelefono
        Email = pEmail
        Contacto = pContacto
        Clave = pClave
        RFC = pRFC
        Giro = pGiro
        Ciudad = pCiudad
        CP = pCP
        Estado = pEstado
        Pais = pPais
        Direccion2 = pDireccion2
        Ciudad2 = pCiudad2
        CP2 = pCP2
        Estado2 = pEstado2
        Pais2 = pPais2
        Serie = pSerie
        FolioInicial = pFolioInicial
        FolioFinal = pFolioFinal
        Formato = pFormato
        Impuesto = pImpuesto
        NombreFiscal = pNombreFiscal
        RegimenFiscal = pRegimenFiscal
        CURP = pCurp
        idTipo = pidTipo
        RegistroPatronal = pRegistroPatronal
        ClaveRegimen = pClaveregimen
        'IdAlmacenC = pIdAlamcenC
        'IdAlmacenM = pIdalmacenM
        Comm.CommandText = "insert into tblsucursales(nombre,direccion,telefono,email,contacto,clave,rfc,giro,ciudad,cp,estado,pais,direccion2,ciudad2,cp2,estado2,pais2,noexterior,nointerior,colonia,municipio,referenciadomicilio,noexterior2,nointerior2,colonia2,municipio2,referenciadomicilio2,serie,folioinicial,foliofinal,formato,impuesto,idalmacen,nombrefiscal,regimenfiscal,curp,idalmacenc,idalmacenm,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,idtipo,registropatronal,claveregimen,clavecolonia,clavelocalidad,clavemun,claveestado,clavepais,claveregimen2) " + _
        "values('" + Replace(Nombre, "'", "''") + "','" + Replace(Direccion, "'", "''") + "','" + Replace(Telefono, "'", "''") + "','" + Replace(Email, "'", "''") + "','" + Replace(Contacto, "'", "''") + "','" + Replace(Clave, "'", "''") + "','" + Replace(RFC, "'", "''") + "','" + Replace(Giro, "'", "''") + "','" + Replace(Ciudad, "'", "''") + "','" + Replace(CP, "'", "''") + "','" + Replace(Estado, "'", "''") + "','" + Replace(Pais, "'", "''") + "','" + Replace(Direccion2, "'", "''") + "','" + Replace(Ciudad2, "'", "''") + "','" + Replace(CP2, "'", "''") + "','" + Replace(Estado2, "'", "''") + "','" + Replace(Pais2, "'", "''") + "'," + _
        "'" + Replace(NoExterior, "'", "''") + "','" + Replace(NoInterior, "'", "''") + "','" + Replace(Colonia, "'", "''") + "','" + Replace(Municipio, "'", "''") + "','" + Replace(ReferenciaDomicilio, "'", "''") + "'," + _
        "'" + Replace(NoExterior2, "'", "''") + "','" + Replace(NoInterior2, "'", "''") + "','" + Replace(Colonia2, "'", "''") + "','" + Replace(Municipio2, "'", "''") + "','" + Replace(ReferenciaDomicilio2, "'", "''") + "','" + Replace(Serie, "'", "''") + "'," + FolioInicial.ToString + "," + FolioFinal.ToString + ",'" + Replace(Formato, "'", "''") + "'," + Impuesto.ToString + ",0,'" + Replace(NombreFiscal, "'", "''") + "','" + Replace(Trim(RegimenFiscal), "'", "''") + "','" + Replace(Trim(CURP), "'", "''") + "',0,0," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + pidTipo.ToString + ",'" + Replace(pRegistroPatronal, "'", "''") + "'," + ClaveRegimen.ToString +
        ",'" + pClaveColonia + "','" + pClavelocalidad + "','" + pClaveMunicipio + "','" + pClaveEstado + "','" + pClavePais + "'," + pClaveRegimen2.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idsucursal) from tblsucursales"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pContacto As String, ByVal pClave As String, ByVal pRFC As String, ByVal pGiro As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String, ByVal pDireccion2 As String, ByVal pCiudad2 As String, ByVal pCP2 As String, ByVal pEstado2 As String, ByVal pPais2 As String, ByVal pNoexterior As String, ByVal pNoInterior As String, ByVal pColonia As String, ByVal pMunicipio As String, ByVal pReferenciaDomicilio As String, ByVal pNoexterior2 As String, ByVal pNoInterior2 As String, ByVal pColonia2 As String, ByVal pMunicipio2 As String, ByVal pReferenciaDomicilio2 As String, ByVal pSerie As String, ByVal pFolioInicial As Integer, ByVal pFolioFinal As Integer, ByVal pFormato As String, ByVal pImpuesto As Double, ByVal pidAlmancen As Integer, ByVal pNombreFiscal As String, ByVal pRegimenFiscal As String, ByVal pCurp As String, ByVal pIdAlmacenC As Integer, ByVal pIdAlmacenM As Integer, pIdTipo As Integer, pRegistroPatronal As String, pClaveRegimen As Integer, pClaveColonia As String, pClavelocalidad As String, pClaveMunicipio As String, pClaveEstado As String, pClavePais As String, pClaveRegimen2 As Integer)
        ID = pID
        Nombre = pNombre
        Direccion = pDireccion
        Telefono = pTelefono
        Email = pEmail
        Contacto = pContacto
        Clave = pClave
        RFC = pRFC
        Giro = pGiro
        Ciudad = pCiudad
        CP = pCP
        Estado = pEstado
        Pais = pPais
        Direccion2 = pDireccion2
        Ciudad2 = pCiudad2
        CP2 = pCP2
        Estado2 = pEstado2
        Pais2 = pPais2

        NoExterior = pNoexterior
        NoInterior = pNoInterior
        Colonia = pColonia
        Municipio = pMunicipio
        ReferenciaDomicilio = pReferenciaDomicilio

        NoExterior2 = pNoexterior2
        NoInterior2 = pNoInterior2
        Colonia2 = pColonia2
        Municipio2 = pMunicipio2
        ReferenciaDomicilio2 = pReferenciaDomicilio2

        Serie = pSerie
        FolioInicial = pFolioInicial
        FolioFinal = pFolioFinal
        Formato = pFormato
        Impuesto = pImpuesto
        idAlmacen = pidAlmancen
        NombreFiscal = pNombreFiscal
        RegimenFiscal = pRegimenFiscal
        CURP = pCurp
        IdAlmacenC = pIdAlmacenC
        IdAlmacenM = pIdAlmacenM
        idTipo = pIdTipo
        RegistroPatronal = pRegistroPatronal
        ClaveRegimen = pClaveRegimen
        Comm.CommandText = "update tblsucursales set nombre='" + Replace(Nombre, "'", "''") + "',direccion='" + Replace(Direccion, "'", "''") + "',telefono='" + Replace(Telefono, "'", "''") + "',email='" + Replace(Email, "'", "''") + "',contacto='" + Replace(Contacto, "'", "''") + "',clave='" + Replace(Clave, "'", "''") + "',rfc='" + Replace(RFC, "'", "''") + "',giro='" + Replace(Giro, "'", "''") + "',ciudad='" + Replace(Ciudad, "'", "''") + "',cp='" + Replace(CP, "'", "''") + "',estado='" + Replace(Estado, "'", "''") + "',pais='" + Replace(Pais, "'", "''") + "',direccion2='" + Replace(Direccion2, "'", "''") + "',ciudad2='" + Replace(Ciudad2, "'", "''") + "',cp2='" + Replace(CP2, "'", "''") + "',estado2='" + Replace(Estado2, "'", "''") + "',pais2='" + Replace(Pais2, "'", "''") + "'," + _
        "noexterior='" + Replace(NoExterior, "'", "''") + "',nointerior='" + Replace(NoInterior, "'", "''") + "',colonia='" + Replace(Colonia, "'", "''") + "',municipio='" + Replace(Municipio, "'", "''") + "',referenciadomicilio='" + Replace(ReferenciaDomicilio, "'", "''") + "'," + _
        "noexterior2='" + Replace(NoExterior2, "'", "''") + "',nointerior2='" + Replace(NoInterior2, "'", "''") + "',colonia2='" + Replace(Colonia2, "'", "''") + "',municipio2='" + Replace(Municipio2, "'", "''") + "',referenciadomicilio2='" + Replace(ReferenciaDomicilio2, "'", "''") + "',serie='" + Replace(Serie, "'", "''") + "',folioinicial=" + FolioInicial.ToString + ",foliofinal=" + FolioFinal.ToString + ",formato='" + Replace(Formato, "'", "''") + "',impuesto=" + Impuesto.ToString + ",idalmacen=" + idAlmacen.ToString + ",nombrefiscal='" + Replace(NombreFiscal, "'", "''") + "',regimenfiscal='" + Replace(Trim(RegimenFiscal), "'", "''") + "',curp='" + Replace(Trim(CURP), "'", "''") + "',idalmacenc=" + IdAlmacenC.ToString + ",idalmacenm=" + IdAlmacenM.ToString + _
        ",idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "',idtipo=" + pIdTipo.ToString + ",registropatronal='" + Replace(pRegistroPatronal, "'", "''") + "',claveregimen=" + ClaveRegimen.ToString +
        ",clavecolonia='" + pClaveColonia + "',clavelocalidad='" + pClavelocalidad + "',clavemun='" + pClaveMunicipio + "',claveestado='" + pClaveEstado + "',clavepais='" + pClavePais + "',claveregimen2=" + pClaveRegimen2.ToString +
        " where idsucursal=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblsucursales where idsucursal=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "", Optional ByVal ClaveyNombre As Boolean = False) As DataView
        Dim DS As New DataSet
        'If ClaveyNombre Then
        Comm.CommandText = "select idsucursal,clave,nombre,telefono from tblsucursales where concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        'Else
        'Comm.CommandText = "select idsucursal,clave,nombre,telefono from tblsucursales where nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        'End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblsucursales")
        Return DS.Tables("tblsucursales").DefaultView
    End Function
    Public Function ChecaClaveRepetida(ByVal pClave As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(clave) from tblsucursales where clave='" + Replace(pClave, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaClaveRepetida = False
        Else
            ChecaClaveRepetida = True
        End If
    End Function
    'Public Function BuscaCliente(ByVal pClave As String) As Boolean
    '    Dim Encontro As Integer
    '    Comm.CommandText = " select if((select idcliente from tblclientes where clave='" + Replace(pClave, "'", "''") + "') is null,0,(select idcliente from tblclientes where clave='" + Replace(pClave, "'", "''") + "'))"
    '    encontro = Comm.ExecuteScalar
    '    If Encontro = 0 Then
    '        BuscaCliente = False
    '    Else
    '        ID = Encontro
    '        BuscaCliente = True
    '        LlenaDatos()
    '    End If
    'End Function
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblsucursales where idsucursal=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Direccion = DReader("direccion")
            Telefono = DReader("telefono")
            Email = DReader("email")
            Contacto = DReader("contacto")
            Clave = DReader("clave")
            RFC = DReader("rfc")
            Giro = DReader("giro")
            Ciudad = DReader("ciudad")
            CP = DReader("cp")
            Estado = DReader("estado")
            Pais = DReader("pais")
            Ciudad2 = DReader("ciudad2")
            CP2 = DReader("cp2")
            Estado2 = DReader("estado2")
            Pais2 = DReader("pais2")
            Direccion2 = DReader("direccion2")

            NoExterior = DReader("noexterior")
            NoInterior = DReader("nointerior")
            Colonia = DReader("colonia")
            Municipio = DReader("municipio")
            ReferenciaDomicilio = DReader("referenciadomicilio")

            NoExterior2 = DReader("noexterior2")
            NoInterior2 = DReader("nointerior2")
            Colonia2 = DReader("colonia2")
            Municipio2 = DReader("municipio2")
            ReferenciaDomicilio2 = DReader("referenciadomicilio2")

            Serie = DReader("serie")
            FolioInicial = DReader("folioinicial")
            FolioFinal = DReader("foliofinal")
            Formato = DReader("formato")
            Impuesto = DReader("impuesto")
            idAlmacen = DReader("idalmacen")
            NombreFiscal = DReader("nombrefiscal")
            RegimenFiscal = DReader("regimenfiscal")
            CURP = DReader("curp")
            IdAlmacenC = DReader("idalmacenc")
            IdAlmacenM = DReader("idalmacenm")
            idTipo = DReader("idtipo")
            RegistroPatronal = DReader("registropatronal")
            ClaveRegimen = DReader("claveregimen")
            cColonia = DReader("clavecolonia")
            cMunicipio = DReader("clavemun")
            cEstado = DReader("claveestado")
            cPais = DReader("clavepais")
            cLocalidad = DReader("clavelocalidad")

        End If
        DReader.Close()
    End Sub
    Public Sub LlenaExp(ByVal pIdDocumento As Integer, ByVal pTipo As Byte)
        Comm.CommandText = "select * from tblventasexpedicion where documento=" + pTipo.ToString + " and iddocumento=" + pIdDocumento.ToString
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        DReader = Comm.ExecuteReader
        If DReader.Read Then
            HayExp = True
            LugarExp = DReader("lugarexp")
            CalleExp = DReader("calexp")
            NumExp = DReader("numexp")
        Else
            HayExp = False
        End If
        DReader.Close()
    End Sub
End Class
