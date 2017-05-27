Public Class dbTrabajadores
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public RegistroPatronal As String
    Public NumeroEmpleado As String
    Public Curp As String
    Public TipoRegimen As Integer
    Public NumeroSeguroSocial As String
    Public FechaInicioLaboral As String
    Public Antiguedad As Integer
    Public Departamento As String
    Public Puesto As String
    Public TipoJornada As String
    Public TipoContrato As String
    Public Periodicidad As String
    Public SalarioBaseCotApor As Double
    Public RiesgoPuesto As Integer
    Public SalarioDiarioIntegrado As Double

    Public Direccion As String
    Public Telefono As String
    Public Email As String
    Public RFC As String
    Public Ciudad As String
    Public CP As String
    Public Estado As String
    Public Pais As String
    Public NoExterior As String
    Public NoInterior As String
    Public Colonia As String
    Public Municipio As String
    Public ReferenciaDomicilio As String
    Public Banco As Integer
    Public CLABE As String
    Public IdCuenta As Integer
    Public IdCuenta2 As Integer
    Public IdCuenta3 As Integer
    Public IdCuenta4 As Integer
    Public idUsuarioAlta As Integer
    Public fechaAlta As String
    Public horaAlta As String
    Public idUsuarioCambio As Integer
    Public fechaCambio As String
    Public horaCambio As String
    Public RFCPatronOrigen As String
    Public sindicalizado As Integer
    Public EstadoLabora As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        RegistroPatronal = ""
        NumeroEmpleado = ""
        Curp = ""
        TipoRegimen = 0
        NumeroSeguroSocial = ""
        FechaInicioLaboral = ""
        Antiguedad = 0
        Departamento = ""
        Puesto = ""
        TipoJornada = ""
        TipoContrato = ""
        Periodicidad = ""
        SalarioBaseCotApor = 0
        RiesgoPuesto = 0
        SalarioDiarioIntegrado = 0
        Direccion = ""
        Telefono = ""
        Email = ""
        RFC = ""
        IdCuenta = 0
        Ciudad = ""
        CP = ""
        Estado = ""
        Pais = ""
        NoExterior = ""
        NoInterior = ""
        Colonia = ""
        Municipio = ""
        Banco = 0
        CLABE = ""
        ReferenciaDomicilio = ""
        IdCuenta2 = 0
        IdCuenta3 = 0
        IdCuenta4 = 0
        idUsuarioAlta = 1000
        fechaAlta = ""
        horaAlta = ""
        idUsuarioCambio = 1000
        fechaCambio = ""
        horaCambio = ""
        sindicalizado = 0
        RFCPatronOrigen = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)

        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbltrabajadores where idtrabajador=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            RegistroPatronal = DReader("registropatronal")
            NumeroEmpleado = DReader("numeroempleado")
            Curp = DReader("curp")
            TipoRegimen = DReader("tiporegimen")
            NumeroSeguroSocial = DReader("numerosegurosocial")
            FechaInicioLaboral = DReader("fechainiciolaboral")
            Antiguedad = DReader("antiguedad")
            Departamento = DReader("departamento")
            Puesto = DReader("puesto")
            TipoJornada = DReader("tipojornada")
            TipoContrato = DReader("tipocontrato")
            Periodicidad = DReader("periodicidad")
            SalarioBaseCotApor = DReader("salariobasecotapor")
            RiesgoPuesto = DReader("riesgopuesto")
            SalarioDiarioIntegrado = DReader("salariodiariointegrado")

            Direccion = DReader("direccion")
            Telefono = DReader("telefono")
            Email = DReader("email")
            RFC = DReader("rfc")
            Ciudad = DReader("ciudad")
            CP = DReader("cp")
            Estado = DReader("estado")
            Pais = DReader("pais")
            NoExterior = DReader("noexterior")
            NoInterior = DReader("nointerior")
            Colonia = DReader("colonia")
            Municipio = DReader("municipio")
            ReferenciaDomicilio = DReader("referencia")
            Banco = DReader("banco")
            CLABE = DReader("clabe")
            IdCuenta = DReader("idcuenta")
            IdCuenta2 = DReader("idcuenta2")
            IdCuenta3 = DReader("idcuenta3")
            IdCuenta4 = DReader("idcuenta4")
            idUsuarioAlta = DReader("idUsuarioAlta")
            fechaAlta = DReader("fechaAlta")
            horaAlta = DReader("horaAlta")
            idUsuarioCambio = DReader("idUsuarioCambio")
            fechaCambio = DReader("fechaCambio")
            horaCambio = DReader("horaCambio")
            RFCPatronOrigen = DReader("rfcpatronorigen")
            sindicalizado = DReader("sindicalizado")
            EstadoLabora = DReader("estadolabora")
        End If
        DReader.Close()

    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pRegistroPatronal As String, ByVal pNumeroEmpleado As String, ByVal pCurp As String, ByVal pTipoRegimen As Integer, ByVal pNumeroSeguroSocial As String, ByVal pFechaInicioLaboral As String, ByVal pAntiguedad As Integer, ByVal pDepartamento As String, ByVal pPuesto As String, ByVal pTipoJornada As String, ByVal pTipoContrato As String, ByVal pPeriodicidad As String, ByVal pSalarioBaseCotApor As Double, ByVal pRiesgoPuesto As Integer, ByVal pSalarioDiarioIntegrado As Double, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pRFC As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String, ByVal pNoExterior As String, ByVal pNoInterior As String, ByVal pColonia As String, ByVal pMunicipio As String, ByVal pReferenciaDomicilio As String, ByVal pBanco As Integer, ByVal pClabe As String, pIdCuenta As Integer, pIdCuenta2 As Integer, pIdCuenta3 As Integer, pIdCuenta4 As Integer, sindicalizado As Integer, pRFCPatronOrigen As String, pEstadoLabora As String)
        If pBanco < 0 Then pBanco = 0
        Comm.CommandText = "insert into tbltrabajadores(nombre,registropatronal,numeroempleado,curp,tiporegimen,numerosegurosocial,fechainiciolaboral,antiguedad,departamento,puesto,tipojornada,tipocontrato,periodicidad,salariobasecotapor,riesgopuesto,salariodiariointegrado,direccion,telefono,email,rfc,ciudad,cp,estado,pais,noexterior,nointerior,colonia,municipio,referencia,banco,clabe,idcuenta,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,idcuenta2,idcuenta3,idcuenta4,sindicalizado,rfcpatronorigen,estadolabora) " + _
        "values('" + Replace(pNombre.Trim, "'", "''") + "','" + Replace(pRegistroPatronal.Trim, "'", "''") + "','" + Replace(pNumeroEmpleado.Trim, "'", "''") + "','" + Replace(pCurp.Trim, "'", "''") + "'," + pTipoRegimen.ToString + ",'" + Replace(pNumeroSeguroSocial.Trim, "'", "''") + "','" + pFechaInicioLaboral + "'," + pAntiguedad.ToString + ",'" + Replace(pDepartamento.Trim, "'", "''") + "','" + Replace(pPuesto.Trim, "'", "''") + "','" + Replace(pTipoJornada.Trim, "'", "''") + "','" + Replace(pTipoContrato.Trim, "'", "''") + "','" + Replace(pPeriodicidad.Trim, "'", "''") + "'," + pSalarioBaseCotApor.ToString + "," + pRiesgoPuesto.ToString + "," + pSalarioDiarioIntegrado.ToString + "," + _
        "'" + Replace(pDireccion.Trim, "'", "''") + "','" + Replace(pTelefono.Trim, "'", "''") + "','" + Replace(pEmail.Trim, "'", "''") + "','" + Replace(pRFC.Trim, "'", "''") + "','" + Replace(pCiudad.Trim, "'", "''") + "','" + Replace(pCP.Trim, "'", "''") + "','" + Replace(pEstado.Trim, "'", "''") + "','" + Replace(pPais.Trim, "'", "''") + "','" + Replace(pNoExterior.Trim, "'", "''") + "','" + Replace(pNoInterior.Trim, "'", "''") + "','" + Replace(pColonia.Trim, "'", "''") + "','" + Replace(pMunicipio.Trim, "'", "''") + "','" + Replace(pReferenciaDomicilio.Trim, "'", "''") + "'," + Banco.ToString + ",'" + Replace(pClabe.Trim, "'", "''") + "'," + pIdCuenta.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'" +
        "," + pIdCuenta2.ToString + "," + pIdCuenta3.ToString + "," + pIdCuenta4.ToString + "," + sindicalizado.ToString + ",'" + Replace(pRFCPatronOrigen, "'", "''") + "',estadolabora='" + Replace(pEstadoLabora, "'", "''") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idtrabajador) from tbltrabajadores"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pRegistroPatronal As String, ByVal pNumeroEmpleado As String, ByVal pCurp As String, ByVal pTipoRegimen As Integer, ByVal pNumeroSeguroSocial As String, ByVal pFechaInicioLaboral As String, ByVal pAntiguedad As Integer, ByVal pDepartamento As String, ByVal pPuesto As String, ByVal pTipoJornada As String, ByVal pTipoContrato As String, ByVal pPeriodicidad As String, ByVal pSalarioBaseCotApor As Double, ByVal pRiesgoPuesto As Integer, ByVal pSalarioDiarioIntegrado As Double, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pRFC As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String, ByVal pNoExterior As String, ByVal pNoInterior As String, ByVal pColonia As String, ByVal pMunicipio As String, ByVal pReferenciaDomicilio As String, ByVal pBanco As Integer, ByVal pClabe As String, pIdCuenta As Integer, pIdCuenta2 As Integer, pIdCuenta3 As Integer, pIdCuenta4 As Integer, sindicalizado As Boolean, pRfcPatronOrigen As String, pEstadoLabora As String)
        ID = pID
        If pBanco < 0 Then pBanco = 0
        Comm.CommandText = "update tbltrabajadores set nombre='" + Replace(pNombre.Trim, "'", "''") + "',registropatronal='" + Replace(pRegistroPatronal.Trim, "'", "''") + "',numeroempleado='" + Replace(pNumeroEmpleado.Trim, "'", "''") + "',curp='" + Replace(pCurp.Trim, "'", "''") + "',tiporegimen=" + pTipoRegimen.ToString + ",numerosegurosocial='" + Replace(pNumeroSeguroSocial.Trim, "'", "''") + "',fechainiciolaboral='" + pFechaInicioLaboral + "',antiguedad=" + pAntiguedad.ToString + ",departamento='" + Replace(pDepartamento.Trim, "'", "''") + "',puesto='" + Replace(pPuesto.Trim, "'", "''") + "',tipojornada='" + Replace(pTipoJornada.Trim, "'", "''") + "',tipocontrato='" + Replace(pTipoContrato.Trim, "'", "''") + "',periodicidad='" + Replace(pPeriodicidad.Trim, "'", "''") + "',salariobasecotapor=" + pSalarioBaseCotApor.ToString + ",riesgopuesto=" + pRiesgoPuesto.ToString + ",salariodiariointegrado=" + pSalarioDiarioIntegrado.ToString + _
        ",direccion='" + Replace(pDireccion.Trim, "'", "''") + "',telefono='" + Replace(pTelefono.Trim, "'", "''") + "',email='" + Replace(pEmail.Trim, "'", "''") + "',rfc='" + Replace(pRFC.Trim, "'", "''") + "',ciudad='" + Replace(pCiudad.Trim, "'", "''") + "',cp='" + Replace(pCP.Trim, "'", "''") + "',estado='" + Replace(pEstado.Trim, "'", "''") + "',pais='" + Replace(pPais.Trim, "'", "''") + "',noexterior='" + Replace(pNoExterior.Trim, "'", "''") + "',nointerior='" + Replace(pNoInterior.Trim, "'", "''") + "',colonia='" + Replace(pColonia.Trim, "'", "''") + "',municipio='" + Replace(pMunicipio.Trim, "'", "''") + "',referencia='" + Replace(pReferenciaDomicilio.Trim, "'", "''") + "',banco=" + pBanco.ToString + ",clabe='" + Replace(pClabe.Trim, "'", "''") + "',idcuenta=" + pIdCuenta.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "'" +
        ",idcuenta2=" + pIdCuenta2.ToString + ",idcuenta3=" + pIdCuenta3.ToString + ",idcuenta4=" + pIdCuenta4.ToString +
        ", sindicalizado=" + sindicalizado.ToString + ",rfcpatronorigen='" + Replace(pRfcPatronOrigen, "'", "''") + "',estadolabora='" + Replace(pEstadoLabora, "'", "''") + "' where idtrabajador=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbltrabajadores where idtrabajador=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        'If ClaveyNombre Then
        Comm.CommandText = "select idtrabajador,nombre,numeroempleado,registropatronal from tbltrabajadores where concat(nombre,numeroempleado,registropatronal) like '%" + Replace(pNombre, "'", "''") + "%'"
        'Else
        'Comm.CommandText = "select idtecnico,clave,nombre,telefono from tbltecnicos where nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        'End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbltrabajadores")
        Return DS.Tables("tbltrabajadores").DefaultView
    End Function
    Public Function ChecaClaveRepetida(ByVal pClave As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(clave) from tbltecnicos where clave='" + Replace(pClave, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaClaveRepetida = False
        Else
            ChecaClaveRepetida = True
        End If
    End Function
    Public Function DaMaximo() As Integer
        Comm.CommandText = "select count(idtrabajador) from tbltrabajadores"
        Return Comm.ExecuteScalar + 1
    End Function
    Public Function BuscaTrabajador(ByVal pClave As String) As Boolean
        Dim Encontro As Integer
        If pClave <> "" Then
            Comm.CommandText = " select ifnull((select idtrabajador from tbltrabajadores where numeroempleado='" + Replace(pClave, "'", "''") + "'),0)"
            Encontro = Comm.ExecuteScalar
        Else
            Encontro = 0
        End If
        If Encontro = 0 Then
            Return False
        Else
            ID = Encontro
            LlenaDatos()
            Return True
        End If
    End Function
    Public Function DaAntiguedad(ByVal pFechaInicio As String) As Integer
        Return CInt((DateDiff(DateInterval.Day, CDate(pFechaInicio), Date.Now) + 1) / 7)
    End Function
    Public Function Reporte() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tbltrabajadores order by nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbltrabajadores")
        'DS.WriteXmlSchema("tbltrabajadores.xml")
        Return DS.Tables("tbltrabajadores").DefaultView
    End Function
End Class
