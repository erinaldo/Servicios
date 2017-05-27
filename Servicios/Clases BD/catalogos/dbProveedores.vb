Public Class dbproveedores
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

    Public NoExterior As String
    Public NoInterior As String
    Public Colonia As String
    Public Municipio As String
    Public ReferenciaDomicilio As String

    Public DiasCredito As Double
    Public LimiteCredito As Double

    Public curp As String
    Public tipo As Integer
    Public idCuenta As Integer

    Public representateLegal As String
    Public IvaRet As Double
    Public Ieps As Double
    Public IdTipo As Integer

    Public idUsuarioAlta As Integer
    Public horaCreacion As String
    Public idUsuarioCambio As Integer
    Public horaCambio As String
    Public fechaCreacion As String
    Public fechaCambio As String
    Public Cuenta As dbProveedoresCuentas
    Public IdCuenta2 As Integer
    Public IdCuenta3 As Integer
    Public IdCuenta4 As Integer
    Public ISR As Double
    Public FechasMes(12) As String

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
        curp = ""
        tipo = 0
        Estado = ""
        Pais = ""
        DiasCredito = 0
        LimiteCredito = 0
        NoExterior = ""
        NoInterior = ""
        Colonia = ""
        Municipio = ""
        ISR = 0
        ReferenciaDomicilio = ""
        idCuenta = 0
        representateLegal = ""
        Ieps = 0
        IvaRet = 0
        IdTipo = 1
        IdCuenta2 = 0
        IdCuenta3 = 0
        IdCuenta4 = 0
        idUsuarioAlta = 1000
        fechaCreacion = ""
        horaCreacion = ""
        idUsuarioCambio = 1000
        fechaCambio = ""
        horaCambio = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
        
    End Sub
    Public Function DaMaximo() As Integer
        Dim Maximo As Integer
        Dim ClaveaChecar As String
        Comm.CommandText = "select ifnull((select count(clave) from tblproveedores),0)"
        Maximo = Comm.ExecuteScalar + 1
        'If pSinFormato Then
        'ClaveaChecar = Maximo.ToString
        'Else
        ClaveaChecar = Format(Maximo, "0000")
        'End If
        While ChecaClaveRepetida(ClaveaChecar)
            Maximo += 1
            'If pSinFormato Then
            'ClaveaChecar = Maximo.ToString
            'Else
            ClaveaChecar = Format(Maximo, "0000")
            'End If
        End While
        Return Maximo
    End Function
    'Public Function DaMaximoCodigo() As String
    '    Dim num As Integer
    '    Dim con As Integer = 9
    '    Comm.CommandText = "select count(idproveedor) from tblproveedores"
    '    num = Comm.ExecuteScalar + 1

    '    While con <> 0
    '        Comm.CommandText = "select count(idproveedor) from tblproveedores where clave='" + num.ToString + "'"
    '        con = Comm.ExecuteScalar
    '        If con <> 0 Then
    '            num += 1
    '        End If
    '    End While
    '    Return num

    'End Function
    Public Sub Guardar(ByVal pNombre As String, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pContacto As String, ByVal pClave As String, ByVal pRFC As String, ByVal pGiro As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String, ByVal pNoexterior As String, ByVal pNoInterior As String, ByVal pColonia As String, ByVal pMunicipio As String, ByVal pReferenciaDomicilio As String, ByVal pDiasCredito As Double, ByVal pLimiteCredito As Double, ByVal pCURP As String, ByVal pTipo As Integer, pIdCuenta As Integer, pRepresentanteLegal As String, pIvaRet As Double, pIeps As Double, pIdTipo As Integer, pIdCuenta2 As Integer, pIdcuenta3 As Integer, pIdCuenta4 As Integer, pISR As Double)
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

        NoExterior = pNoexterior
        NoInterior = pNoInterior
        Colonia = pColonia
        Municipio = pMunicipio
        ReferenciaDomicilio = pReferenciaDomicilio
        LimiteCredito = pLimiteCredito
        DiasCredito = pDiasCredito
        curp = pCURP
        tipo = pTipo
        idCuenta = pIdCuenta
        representateLegal = pRepresentanteLegal
        IvaRet = pIvaRet
        Ieps = pIeps
        IdTipo = pIdTipo
        ISR = pISR
        Comm.CommandText = "insert into tblproveedores(nombre,direccion,telefono,email,contacto,clave,rfc,giro,ciudad,cp,estado,pais,noexterior,nointerior,colonia,municipio,referenciadomicilio,diasdecredito,limitedecredito,curp,tipo,idcuenta,representantelegal,ivaretp,iepsp,idtipo,idusuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,idcuenta2,idcuenta3,idcuenta4,isr) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Direccion, "'", "''") + "','" + Replace(Telefono, "'", "''") + "','" + Replace(Email, "'", "''") + "','" + Replace(Contacto, "'", "''") + "','" + Replace(Clave, "'", "''") + "','" + Replace(RFC, "'", "''") + "','" + Replace(Giro, "'", "''") + "','" + Replace(Ciudad, "'", "''") + "','" + Replace(CP, "'", "''") + "','" + Replace(Estado, "'", "''") + "','" + Replace(Pais, "'", "''") + "','" + Replace(NoExterior, "'", "''") + "','" + Replace(NoInterior, "'", "''") + "','" + Replace(Colonia, "'", "''") + "','" + Replace(Municipio, "'", "''") + "','" + Replace(ReferenciaDomicilio, "'", "''") + "'," + DiasCredito.ToString + "," + LimiteCredito.ToString + ",'" + Replace(curp, "'", "''") + "'," + tipo.ToString + "," + idCuenta.ToString + ",'" + Replace(representateLegal, "'", "''") + "'," + IvaRet.ToString + "," + Ieps.ToString + "," + IdTipo.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'" +
            "," + pIdCuenta2.ToString + "," + pIdcuenta3.ToString + "," + pIdCuenta4.ToString + "," + ISR.ToString + ")"
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idproveedor) from tblproveedores"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pContacto As String, ByVal pClave As String, ByVal pRFC As String, ByVal pGiro As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String, ByVal pNoexterior As String, ByVal pNoInterior As String, ByVal pColonia As String, ByVal pMunicipio As String, ByVal pReferenciaDomicilio As String, ByVal pDiasCredito As Double, ByVal pLimiteCredito As Double, ByVal pCURP As String, ByVal pTipo As Integer, pidcuenta As Integer, pRepresentanteLegal As String, pIvaRet As Double, pIeps As Double, pIdTipo As Integer, PidCuenta2 As Integer, pIdCuenta3 As Integer, pIdCuenta4 As Integer, pISR As Double)
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
        DiasCredito = pDiasCredito
        LimiteCredito = pLimiteCredito
        NoExterior = pNoexterior
        NoInterior = pNoInterior
        Colonia = pColonia
        Municipio = pMunicipio
        ReferenciaDomicilio = pReferenciaDomicilio
        curp = pCURP
        tipo = pTipo
        idCuenta = pidcuenta
        representateLegal = pRepresentanteLegal
        IvaRet = pIvaRet
        Ieps = pIeps
        IdTipo = pIdTipo
        ISR = pISR
        Comm.CommandText = "update tblproveedores set nombre='" + Replace(Nombre, "'", "''") + "',direccion='" + Replace(Direccion, "'", "''") + "',telefono='" + Replace(Telefono, "'", "''") + "',email='" + Replace(Email, "'", "''") + "',contacto='" + Replace(Contacto, "'", "''") + "',clave='" + Replace(Clave, "'", "''") + "',rfc='" + Replace(RFC, "'", "''") + "',giro='" + Replace(Giro, "'", "''") + "',ciudad='" + Replace(Ciudad, "'", "''") + "',cp='" + Replace(CP, "'", "''") + "',estado='" + Replace(Estado, "'", "''") + "',pais='" + Replace(Pais, "'", "''") + "'," + _
        "noexterior='" + Replace(NoExterior, "'", "''") + "',nointerior='" + Replace(NoInterior, "'", "''") + "',colonia='" + Replace(Colonia, "'", "''") + "',municipio='" + Replace(Municipio, "'", "''") + "',referenciadomicilio='" + Replace(ReferenciaDomicilio, "'", "''") + "',diasdecredito=" + DiasCredito.ToString + ",limitedecredito=" + LimiteCredito.ToString + ",curp='" + Replace(curp, "'", "''") + "' ,tipo=" + tipo.ToString + ",idcuenta=" + idCuenta.ToString + _
        ", representantelegal='" + representateLegal + "',ivaretp=" + IvaRet.ToString + ",iepsp=" + Ieps.ToString + ",idtipo=" + IdTipo.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "'" +
        ",idcuenta2=" + PidCuenta2.ToString + ",idcuenta3=" + pIdCuenta3.ToString + ",idcuenta4=" + pIdCuenta4.ToString + ",isr=" + ISR.ToString +
        " where idproveedor=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblproveedores where idproveedor=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "", Optional ByVal pTipo As Integer = 0) As DataView
        Dim DS As New DataSet
        'If ClaveyNombre Then
        Comm.CommandText = "select idproveedor,clave,nombre,rfc from tblproveedores where concat(clave,rfc,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        'Else
        'Comm.CommandText = "select idproveedor,clave,nombre,telefono from tblproveedores where nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        'End If
        If pTipo <> 0 Then
            If pTipo = 1 Then
                Comm.CommandText += " and tipo=0"
            Else
                Comm.CommandText += " and tipo=1"
            End If

        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproveedores")
        Return DS.Tables("tblproveedores").DefaultView
    End Function
    Public Function contadorPolizas(ByVal pIDProveedor As Integer)
        Dim contador As Integer = 0
        Dim nombreProv As String = ""
        Comm.CommandText = "select count(id) from tblpolizasdetalles where idProveedor=" + pIDProveedor.ToString
        contador = Comm.ExecuteScalar
        'Comm.CommandText = "select nombre from tblproveedores where idProveedor=" + pIDProveedor.ToString
        'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'DR = Comm.ExecuteReader
        'If DR.Read Then
        '    nombreProv = DR("nombre")
        'End If
        'DR.Close()
        Comm.CommandText = "select count(idPagoProv) from tblpagoprov  where idProveedor=" + pIDProveedor.ToString
        contador += Comm.ExecuteScalar
        Comm.CommandText = "select count(idCompra) from tblcompras where idProveedor=" + pIDProveedor.ToString
        contador += Comm.ExecuteScalar
        Return contador
    End Function
    Public Function ConsultaDIOT(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        'If ClaveyNombre Then
        Comm.CommandText = "select idproveedor,clave,nombre,rfc from tblproveedores where concat(clave,rfc,nombre) like '%" + Replace(pNombre, "'", "''") + "%' and tipo=1"
        'Else
        'Comm.CommandText = "select idproveedor,clave,nombre,telefono from tblproveedores where nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        'End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproveedores")
        Return DS.Tables("tblproveedores").DefaultView
    End Function
    Public Function ChecaClaveRepetida(ByVal pClave As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(clave) from tblproveedores where clave='" + Replace(pClave, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaClaveRepetida = False
        Else
            ChecaClaveRepetida = True
        End If
    End Function
    Public Function BuscaProveedor(ByVal pClave As String, Optional pRfc As String = "") As Boolean
        If pRfc = "" Then
            Comm.CommandText = "select ifnull((select idproveedor from tblproveedores where clave='" + Replace(pClave, "'", "''") + "'),0)"
        ElseIf pClave = "" Then
            Comm.CommandText = "select ifnull((select idproveedor from tblproveedores where rfc='" + Replace(pRfc, "'", "''") + "'),0)"
        Else
            Comm.CommandText = "select ifnull((select idproveedor from tblproveedores where clave='" + Replace(pClave, "'", "''") + "' and rfc = '" + pRfc + "'),0)"
        End If
        ID = Comm.ExecuteScalar
        If ID <> 0 Then
            LlenaDatos()
            Return True
        End If
        Return False
    End Function
    Public Function BuscaProveedorDIOT(ByVal pClave As String) As Boolean
        Dim Encontro As Integer
        Comm.CommandText = "select ifnull((select idproveedor from tblproveedores where tipo=1 and clave='" + Replace(pClave, "'", "''") + "'),0)"
        Encontro = Comm.ExecuteScalar
        If Encontro = 0 Then
            BuscaProveedorDIOT = False
            Nombre = ""
        Else
            ID = Encontro
            BuscaProveedorDIOT = True
            LlenaDatos()
        End If
    End Function
    Public Function validarDIOT(ByVal pFolio As String, ByVal pIDProv As Integer, ByVal pIDRenglon As Integer)
        Comm.CommandText = "select count(id) from tblpolizasdetalles where factura='" + pFolio.ToString + "' and idProveedor=" + pIDProv.ToString + " and id<>" + pIDRenglon.ToString
        If Comm.ExecuteScalar > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Function BuscaProveedorDIOTModif(ByVal pClave As String, ByVal pid As Integer) As Boolean
        Dim Encontro As Integer
        Comm.CommandText = " select count(idProveedor) from tblproveedores where clave='" + Replace(pClave, "'", "''") + "' and idProveedor<>" + pid.ToString
        Encontro = Comm.ExecuteScalar
        If Encontro = 0 Then
            BuscaProveedorDIOTModif = False
            Nombre = ""
        Else
            ID = Encontro
            BuscaProveedorDIOTModif = True
            LlenaDatos()
        End If
    End Function
    Public Sub LlenaDatos(Optional ByVal pID As Integer = -1)
        If pID <> -1 Then
            ID = pID
        End If
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblproveedores where idproveedor=" + ID.ToString
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

            NoExterior = DReader("noexterior")
            NoInterior = DReader("nointerior")
            Colonia = DReader("colonia")
            Municipio = DReader("municipio")
            ReferenciaDomicilio = DReader("referenciadomicilio")
            DiasCredito = DReader("diasdecredito")
            LimiteCredito = DReader("limitedecredito")
            curp = DReader("curp")
            tipo = DReader("tipo")
            idCuenta = DReader("idcuenta")
            IdCuenta2 = DReader("idcuenta2")
            IdCuenta3 = DReader("idcuenta3")
            IdCuenta4 = DReader("idcuenta4")
            representateLegal = DReader("representantelegal")
            IvaRet = DReader("ivaretp")
            Ieps = DReader("iepsp")
            IdTipo = DReader("idtipo")

            idUsuarioAlta = DReader("idUsuarioAlta")
            fechaCreacion = DReader("fechaAlta")
            horaCreacion = DReader("horaAlta")
            idUsuarioCambio = DReader("idusuarioCambio")
            fechaCambio = DReader("fechaCambio")
            horaCambio = DReader("horaCambio")
            ISR = DReader("isr")
        End If
        DReader.Close()
    End Sub
    Public Function ConsultaMovimientos(ByVal pIdProveedor As Integer, ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pGenerarTabla As Boolean) As DataView
        Dim DS As New DataSet
        Comm.Parameters.Clear()
        If pGenerarTabla Then
            Comm.CommandText = "select spproveedoresmovimientos(" + pIdProveedor.ToString + ",'" + pFecha1 + "','" + pFecha2 + "')"
            Comm.ExecuteNonQuery()
        End If
        Comm.CommandText = "select fecha,hora,case tipomovimiento when 1 then 'Factura' when 2 then 'Nota de Crédito' when 3 then 'Devolución' when 4 then 'Pago' when 5 then 'Nota de Cargo' when 6 then 'Documento' when 7 then 'Saldo Inicial' end as tipodocs,if(estado=3,'A','C')," + _
        "folio,cargo,abono,saldo,estado,tipomovimiento,iddocumento from tblproveedoresmovimientos where idproveedor=" + pIdProveedor.ToString + " order by fecha,hora"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproveedoresmovimientos")
        DS.WriteXmlSchema("proveedoresmovimientos.xml")
        Return DS.Tables("tblproveedoresmovimientos").DefaultView
    End Function

    Public Function DaSaldoAFecha(ByVal pIdProveedor As Integer, ByVal pFecha As String) As Double
        Comm.CommandText = "select spdasaldoafechaproveedor(" + pIdProveedor.ToString + ",'" + pFecha + "')"
        Return Comm.ExecuteScalar
    End Function
    Public Function Reporte(pidTipo As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblproveedores"
        If pidTipo >= 0 Then Comm.CommandText += " where idtipo=" + pidTipo.ToString
        Comm.CommandText += " order by nombre;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproveedores")
        'DS.WriteXmlSchema("tblproveedores.xml")
        Return DS.Tables("tblproveedores").DefaultView
    End Function
    Public Sub DaPrimerCuenta()
        Dim IdCuenta As Integer
        Comm.CommandText = "select ifnull((select idprovcuenta from tblproveedorescuentas where idprov=" + ID.ToString + " limit 1),0)"
        IdCuenta = Comm.ExecuteScalar
        Cuenta = New dbProveedoresCuentas(IdCuenta, Comm.Connection)
    End Sub
    Public Function ReporteAnalitico(ByVal PidProveedor As Integer, ByVal pFecha1 As String, ByVal pFecha2 As String, pIdtipo As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblproveedores.nombre,tblproveedores.clave,round(spdasaldoafechaproveedor(idproveedor,'" + pFecha1 + "'),2) as saldoant," + _
        "round((ifnull((select if(tblcompras.idmoneda=2,sum(tblcompras.totalapagar),sum(tblcompras.totalapagar*tblcompras.tipodecambio)) from tblcompras inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblcompras.idproveedor=tblproveedores.idproveedor and tblcompras.fecha>='" + pFecha1 + "' and tblcompras.fecha<='" + pFecha2 + "' and tblcompras.estado=3),0)+" + _
        "ifnull((select if(tblnotasdecargocompras.idmoneda=2,sum(tblnotasdecargocompras.totalapagar),sum(tblnotasdecargocompras.totalapagar*tblnotasdecargocompras.tipodecambio)) from tblnotasdecargocompras where tblnotasdecargocompras.idproveedor=tblproveedores.idproveedor and tblnotasdecargocompras.fecha>='" + pFecha1 + "' and tblnotasdecargocompras.fecha<='" + pFecha2 + "' and tblnotasdecargocompras.estado=3),0)+" + _
        "ifnull((select if(tbldocumentosproveedores.idmoneda=2,sum(tbldocumentosproveedores.totalapagar),sum(tbldocumentosproveedores.totalapagar*tbldocumentosproveedores.tipodecambio)) from tbldocumentosproveedores where tbldocumentosproveedores.idproveedor=tblproveedores.idproveedor and tbldocumentosproveedores.fecha>='" + pFecha1 + "' and tbldocumentosproveedores.fecha<='" + pFecha2 + "' and tbldocumentosproveedores.estado=3),0)" + _
        "),2) as ventas," + _
        "round(ifnull((select if(tblcompraspagos.idmoneda=2,sum(tblcompraspagos.cantidad),sum(tblcompraspagos.cantidad*tblcompraspagos.ptipodecambio)) from tblcompraspagos where tblcompraspagos.idproveedor=tblproveedores.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha>='" + pFecha1 + "' and tblcompraspagos.fecha<='" + pFecha2 + "'),0),2) as pagos" + _
        " from tblproveedores"
        If PidProveedor <> 0 Then Comm.CommandText += " where tblproveedores.idproveedor=" + PidProveedor.ToString
        If PidProveedor = 0 And pIdtipo > 0 Then Comm.CommandText += " where tblproveedores.idtipo=" + pIdtipo.ToString
        Comm.CommandText += " order by tblproveedores.nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesana")
        'DS.WriteXmlSchema("tblprovana.xml")
        Return DS.Tables("tblclientesana").DefaultView
    End Function
    Public Function ReporteProveedoresAxM(pFecha1 As String, pFecha2 As String, pidSucursal As Integer, pIdProveedor As Integer, pIdInventario As Integer, pIdTipoProv As Integer) As DataView
        Dim DS As New DataSet
        Dim FechasI(12) As String
        Dim FechasF(12) As String

        Dim Ftemp As String = pFecha1
        For i As Integer = 0 To 10 Step 1
            If i > 0 Then
                Ftemp = Format(DateAdd(DateInterval.Day, 1, CDate(Ftemp)), "yyyy/MM/dd")
            Else
                FechasI(i) = Ftemp
                FechasMes(i) = Format(CDate(Ftemp), "MMM").ToUpper
            End If
            Ftemp = Format(DateAdd(DateInterval.Month, 1, CDate(Ftemp)), "yyyy/MM/dd")
            FechasI(i + 1) = Ftemp
            FechasMes(i + 1) = Format(CDate(Ftemp), "MMM").ToUpper
            Ftemp = Format(DateAdd(DateInterval.Day, -1, CDate(Ftemp)), "yyyy/MM/dd")
            FechasF(i) = Ftemp
        Next
        Ftemp = DateAdd(DateInterval.Month, 1, CDate(Ftemp))
        Ftemp = Format(DateAdd(DateInterval.Day, -1, CDate(Ftemp)), "yyyy/MM/dd")
        FechasF(12) = Ftemp


        Comm.CommandText = "delete from tblrepprovtemp;"
        Comm.CommandText += "insert tblrepprovtemp(idinventario,idproveedor) select cd.idinventario,c.idproveedor from tblcompras c  inner join tblcomprasdetalles cd on cd.idcompra=c.idcompra inner join tblproveedores p on c.idproveedor=p.idproveedor where fecha>='" + pFecha1 + "' and fecha<='" + FechasF(12) + "' and c.estado=3"
        If pidSucursal > 0 Then Comm.CommandText += " and c.idsucursal=" + pidSucursal.ToString
        If pIdProveedor > 0 Then Comm.CommandText += " and c.idproveedor=" + pIdProveedor.ToString
        If pIdTipoProv > 0 Then Comm.CommandText += " and p.idtipo=" + pIdTipoProv.ToString
        If pIdInventario > 0 Then Comm.CommandText += " and cd.idinventario=" + pIdInventario.ToString
        Comm.CommandText += " group by idinventario,idproveedor order by idproveedor;"
        Comm.ExecuteNonQuery()
        Dim FiltrosSuc As String = ""
        If pidSucursal > 0 Then FiltrosSuc = " and tblcompras.idsucursal=" + pidSucursal.ToString + " "

        Comm.CommandText = "select t.idproveedor,p.nombre,i.clave,i.nombre inombre,t.idinventario," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(0) + "' and tblcompras.fecha<='" + FechasF(0) + "'" + FiltrosSuc + "),0) col1," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(1) + "' and tblcompras.fecha<='" + FechasF(1) + "'" + FiltrosSuc + "),0) col2," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(2) + "' and tblcompras.fecha<='" + FechasF(2) + "'" + FiltrosSuc + "),0) col3," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(3) + "' and tblcompras.fecha<='" + FechasF(3) + "'" + FiltrosSuc + "),0) col4," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(4) + "' and tblcompras.fecha<='" + FechasF(4) + "'" + FiltrosSuc + "),0) col5," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(5) + "' and tblcompras.fecha<='" + FechasF(5) + "'" + FiltrosSuc + "),0) col6," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(6) + "' and tblcompras.fecha<='" + FechasF(6) + "'" + FiltrosSuc + "),0) col7," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(7) + "' and tblcompras.fecha<='" + FechasF(7) + "'" + FiltrosSuc + "),0) col8," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(8) + "' and tblcompras.fecha<='" + FechasF(8) + "'" + FiltrosSuc + "),0) col9," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(9) + "' and tblcompras.fecha<='" + FechasF(9) + "'" + FiltrosSuc + "),0) col10," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(10) + "' and tblcompras.fecha<='" + FechasF(10) + "'" + FiltrosSuc + "),0) col11," +
        "ifnull((select sum(tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra where tblcomprasdetalles.idinventario=t.idinventario and tblcompras.idproveedor=t.idproveedor and tblcompras.estado=3 and tblcompras.fecha>='" + FechasI(11) + "' and tblcompras.fecha<='" + FechasF(11) + "'" + FiltrosSuc + "),0) col12" +
        " from tblrepprovtemp t inner join tblproveedores p on p.idproveedor=t.idproveedor inner join tblinventario i on i.idinventario=t.idinventario order by t.idproveedor;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproveedoresaxm")
        'DS.WriteXmlSchema("tblproveedoresaxm.xml")
        Return DS.Tables("tblproveedoresaxm").DefaultView
    End Function
End Class
