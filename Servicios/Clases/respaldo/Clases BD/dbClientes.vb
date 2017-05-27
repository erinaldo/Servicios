Public Class dbClientes
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
    Public DireccionFiscal As Byte
    Public Credito As Double
    Public CreditoDias As Double
    Public Saldo As Double
    Public IvaRetenido As Double
    Public ISR As Double
    Public IdVendedor As Integer
    Public IdLista As Integer
    Public CURP As String
    Public IVA As Double
    Public SobreescribeIVA As Byte
    Public EscondeIva As Byte
    Public Representante As String
    Public RepresentanteRFC As String
    Public RepresentanteRegistro As String
    Public UsaAdenda As Integer
    Public SaldoAFavor As Double
    Public ActivarImpuestos As Byte
    Public zona As Integer
    Public zona2 As Integer
    Public imagen As String
    Public identificacion As Integer
    Public numeroID As String
    Public IdCuenta As Integer
    Public IdTipo As Integer

    Public fechaCambio As String
    Public idUsuario As Integer
    Public fechaCreacion As String
    Public horaCambio As String
    Public horaCreacion As String
    Public idUsuarioAlta As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Inicia()
        Comm.Connection = Conexion
    End Sub
    Public Sub Inicia()
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
        imagen = ""
        NoExterior = ""
        NoInterior = ""
        Colonia = ""
        Municipio = ""
        ReferenciaDomicilio = ""
        IdVendedor = 0
        NoExterior2 = ""
        NoInterior2 = ""
        Colonia2 = ""
        Municipio2 = ""
        ReferenciaDomicilio2 = ""
        DireccionFiscal = 0
        Credito = 0
        CreditoDias = 0
        Saldo = 0
        ISR = 0
        IvaRetenido = 0
        IdLista = 1
        CURP = ""
        IVA = 0
        IdTipo = 1
        SaldoAFavor = 0
        EscondeIva = 0
        SobreescribeIVA = 0
        Representante = ""
        RepresentanteRegistro = ""
        RepresentanteRFC = ""
        UsaAdenda = 0
        SaldoAFavor = 0
        ActivarImpuestos = 0
        IdCuenta = 0
        fechaCambio = ""
        idUsuario = 1000
        fechaCreacion = ""
        horaCreacion = ""
        horaCambio = ""
        idUsuarioAlta = 1000
        'identificacion = 0
        'numeroID = ""
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pContacto As String, ByVal pClave As String, ByVal pRFC As String, ByVal pGiro As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String, ByVal pDireccion2 As String, ByVal pCiudad2 As String, ByVal pCP2 As String, ByVal pEstado2 As String, ByVal pPais2 As String, ByVal pNoexterior As String, ByVal pNoInterior As String, ByVal pColonia As String, ByVal pMunicipio As String, ByVal pReferenciaDomicilio As String, ByVal pNoexterior2 As String, ByVal pNoInterior2 As String, ByVal pColonia2 As String, ByVal pMunicipio2 As String, ByVal pReferenciaDomicilio2 As String, ByVal pDireccionFiscal As Byte, ByVal pCredito As Double, ByVal pCreditoDias As Double, ByVal pSaldo As Double, ByVal pisr As Double, ByVal pIvaRetenido As Double, ByVal pIdVendedor As Integer, ByVal pIdLista As Integer, ByVal pCurp As String, ByVal pSobreescribeIVA As Byte, ByVal pIVA As Double, ByVal pEscondeIva As Byte, ByVal pRepresentante As String, ByVal pRepresentanteRFC As String, ByVal pRepresentanteRegistro As String, ByVal pUsaAddenda As Integer, ByVal pActivarImpuestos As Byte, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal pImagen As String, ByVal pIdentificacion As Integer, ByVal pNumeroID As String, pIdCuenta As Integer, pIdTipo As Integer)
        NoExterior = pNoexterior
        NoInterior = pNoInterior
        Colonia = pColonia
        Municipio = Trim(pMunicipio)
        ReferenciaDomicilio = pReferenciaDomicilio
        imagen = pImagen
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
        DireccionFiscal = pDireccionFiscal
        Credito = pCredito
        CreditoDias = pCreditoDias
        Saldo = pSaldo
        IvaRetenido = pIvaRetenido
        ISR = pisr
        IdVendedor = pIdVendedor
        IdLista = pIdLista
        CURP = pCurp
        IVA = pIVA
        SobreescribeIVA = pSobreescribeIVA
        EscondeIva = pEscondeIva
        Representante = pRepresentante
        RepresentanteRFC = pRepresentanteRFC
        RepresentanteRegistro = pRepresentanteRegistro
        UsaAdenda = pUsaAddenda
        ActivarImpuestos = pActivarImpuestos
        zona = pZona
        zona2 = pZona2
        IdCuenta = pIdCuenta
        IdTipo = pIdTipo
        
        'identificacion = pIdentificacion
        'numeroID = pNumeroID


        Comm.CommandText = "insert into tblclientes(nombre,direccion,telefono,email,contacto,clave,rfc,giro,ciudad,cp,estado,pais,direccion2,ciudad2,cp2,estado2,pais2,noexterior,nointerior,colonia,municipio,referenciadomicilio,noexterior2,nointerior2,colonia2,municipio2,referenciadomicilio2,direccionfiscal,credito,creditodias,saldo,ivaretenido,isr,idvendedor,idlista,curp,iva,sobreescribeiva,escondeiva,representante,representanterfc,representanteregistro,usaadenda,saldoafavor,activarimpuestos,zona,zona2, imagen,idcuenta,idtipo,fechaCambio,idUsuario,fechaCreacion,horaCreacion,horaCambio,idusuarioAlta) " + _
        "values('" + Replace(Nombre, "'", "''") + "','" + Replace(Direccion, "'", "''") + "','" + Replace(Telefono, "'", "''") + "','" + Replace(Email, "'", "''") + "','" + Replace(Contacto, "'", "''") + "','" + Replace(Clave, "'", "''") + "','" + Replace(RFC, "'", "''") + "','" + Replace(Giro, "'", "''") + "','" + Replace(Ciudad, "'", "''") + "','" + Replace(CP, "'", "''") + "','" + Replace(Estado, "'", "''") + "','" + Replace(Pais, "'", "''") + "','" + Replace(Direccion2, "'", "''") + "','" + Replace(Ciudad2, "'", "''") + "','" + Replace(CP2, "'", "''") + "','" + Replace(Estado2, "'", "''") + "','" + Replace(Pais2, "'", "''") + "'," + _
        "'" + Replace(NoExterior, "'", "''") + "','" + Replace(NoInterior, "'", "''") + "','" + Replace(Colonia, "'", "''") + "','" + Replace(Municipio, "'", "''") + "','" + Replace(ReferenciaDomicilio, "'", "''") + "'," + _
        "'" + Replace(NoExterior2, "'", "''") + "','" + Replace(NoInterior2, "'", "''") + "','" + Replace(Colonia2, "'", "''") + "','" + Replace(Municipio2, "'", "''") + "','" + Replace(ReferenciaDomicilio2, "'", "''") + "'," + DireccionFiscal.ToString + "," + Credito.ToString + "," + CreditoDias.ToString + "," + Saldo.ToString + "," + IvaRetenido.ToString + "," + ISR.ToString + "," + IdVendedor.ToString + "," + IdLista.ToString + ",'" + Replace(Trim(CURP), "'", "''") + "'," + IVA.ToString + "," + SobreescribeIVA.ToString + "," + EscondeIva.ToString + ",'" + Replace(Representante, "'", "''") + "','" + Replace(RepresentanteRFC, "'", "''") + "','" + Replace(RepresentanteRegistro, "'", "''") + "'," + UsaAdenda.ToString + ",0," + ActivarImpuestos.ToString + "," + zona.ToString + "," + zona2.ToString + ",'" + Replace(imagen.ToString, "\", "\\") + "'," + pIdCuenta.ToString + "," + IdTipo.ToString + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ")"
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idcliente) from tblclientes"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pContacto As String, ByVal pClave As String, ByVal pRFC As String, ByVal pGiro As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String, ByVal pDireccion2 As String, ByVal pCiudad2 As String, ByVal pCP2 As String, ByVal pEstado2 As String, ByVal pPais2 As String, ByVal pNoexterior As String, ByVal pNoInterior As String, ByVal pColonia As String, ByVal pMunicipio As String, ByVal pReferenciaDomicilio As String, ByVal pNoexterior2 As String, ByVal pNoInterior2 As String, ByVal pColonia2 As String, ByVal pMunicipio2 As String, ByVal pReferenciaDomicilio2 As String, ByVal pDireccionFiscal As Byte, ByVal pCredito As Double, ByVal pCreditoDias As Double, ByVal pISR As Double, ByVal pIvaRetenido As Double, ByVal pIdVendedor As Integer, ByVal pidlista As Integer, ByVal pCurp As String, ByVal pSobreescribeIVA As Byte, ByVal pIVA As Double, ByVal pEscondeiva As Byte, ByVal pRepresentante As String, ByVal pRepresentanteRFC As String, ByVal pRepresentanteRegistro As String, ByVal pUsaAdenda As Integer, ByVal pActivarImpuestos As Byte, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal pImagen As String, ByVal pIdentificacion As Integer, ByVal pNumeroID As String, pIdCuenta As Integer, pIdTipo As Integer)
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
        Municipio = Trim(pMunicipio)
        ReferenciaDomicilio = pReferenciaDomicilio

        imagen = pImagen
        NoExterior2 = pNoexterior2
        NoInterior2 = pNoInterior2
        Colonia2 = pColonia2
        Municipio2 = pMunicipio2
        ReferenciaDomicilio2 = pReferenciaDomicilio2
        DireccionFiscal = pDireccionFiscal
        Credito = pCredito
        CreditoDias = pCreditoDias
        IvaRetenido = pIvaRetenido
        ISR = pISR
        IdVendedor = pIdVendedor
        IdLista = pidlista
        CURP = pCurp
        IVA = pIVA
        SobreescribeIVA = pSobreescribeIVA
        EscondeIva = pEscondeiva
        Representante = pRepresentante
        RepresentanteRFC = pRepresentanteRFC
        RepresentanteRegistro = pRepresentanteRegistro
        UsaAdenda = pUsaAdenda
        ActivarImpuestos = pActivarImpuestos
        zona = pZona
        zona2 = pZona2
        IdCuenta = pIdCuenta
        IdTipo = pIdTipo
     
        'identificacion = pIdentificacion
        'numeroID = pNumeroID
        Comm.CommandText = "update tblclientes set nombre='" + Replace(Nombre, "'", "''") + "',direccion='" + Replace(Direccion, "'", "''") + "',telefono='" + Replace(Telefono, "'", "''") + "',email='" + Replace(Email, "'", "''") + "',contacto='" + Replace(Contacto, "'", "''") + "',clave='" + Replace(Clave, "'", "''") + "',rfc='" + Replace(RFC, "'", "''") + "',giro='" + Replace(Giro, "'", "''") + "',ciudad='" + Replace(Ciudad, "'", "''") + "',cp='" + Replace(CP, "'", "''") + "',estado='" + Replace(Estado, "'", "''") + "',pais='" + Replace(Pais, "'", "''") + "',direccion2='" + Replace(Direccion2, "'", "''") + "',ciudad2='" + Replace(Ciudad2, "'", "''") + "',cp2='" + Replace(CP2, "'", "''") + "',estado2='" + Replace(Estado2, "'", "''") + "',pais2='" + Replace(Pais2, "'", "''") + "'," + _
        "noexterior='" + Replace(NoExterior, "'", "''") + "',nointerior='" + Replace(NoInterior, "'", "''") + "',colonia='" + Replace(Colonia, "'", "''") + "',municipio='" + Replace(Municipio, "'", "''") + "',referenciadomicilio='" + Replace(ReferenciaDomicilio, "'", "''") + "'," + _
        "noexterior2='" + Replace(NoExterior2, "'", "''") + "',nointerior2='" + Replace(NoInterior2, "'", "''") + "',colonia2='" + Replace(Colonia2, "'", "''") + "',municipio2='" + Replace(Municipio2, "'", "''") + "',referenciadomicilio2='" + Replace(ReferenciaDomicilio2, "'", "''") + "',direccionfiscal=" + DireccionFiscal.ToString + ",credito=" + Credito.ToString + ",creditodias=" + CreditoDias.ToString + _
        ",ivaretenido=" + IvaRetenido.ToString + ",isr=" + ISR.ToString + ",idvendedor=" + IdVendedor.ToString + ",idlista=" + IdLista.ToString + ",curp='" + Replace(Trim(CURP), "'", "''") + "',iva=" + IVA.ToString + ",sobreescribeiva=" + SobreescribeIVA.ToString + ",escondeiva=" + EscondeIva.ToString + _
        ",representante='" + Replace(Representante, "'", "''") + "',representanterfc='" + Replace(RepresentanteRFC, "'", "''") + "',representanteregistro='" + Replace(RepresentanteRegistro, "'", "''") + "',usaadenda=" + UsaAdenda.ToString + ",activarimpuestos=" + ActivarImpuestos.ToString + " , zona=" + zona.ToString + ",zona2=" + zona2.ToString + ", imagen='" + Replace(imagen.ToString, "\", "\\") + "',idcuenta=" + IdCuenta.ToString + ",idtipo=" + pIdTipo.ToString + ", fechaCambio='" + fechaCambio + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "', idUsuario=" + GlobalIdUsuario.ToString() + _
        " where idcliente=" + ID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblclientes where idcliente=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaMaximo(ByVal pSinFormato As Boolean) As Integer
        Dim Maximo As Integer
        Dim ClaveaChecar As String
        Comm.CommandText = "select ifnull((select count(clave) from tblclientes),0)"
        Maximo = Comm.ExecuteScalar + 1
        If pSinFormato Then
            ClaveaChecar = Maximo.ToString
        Else
            ClaveaChecar = Format(Maximo, "0000")
        End If
        While ChecaClaveRepetida(ClaveaChecar)
            Maximo += 1
            If pSinFormato Then
                ClaveaChecar = Maximo.ToString
            Else
                ClaveaChecar = Format(Maximo, "0000")
            End If
        End While
        Return Maximo
    End Function
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idcliente,clave,nombre,rfc from tblclientes where concat(clave,rfc,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientes")
        Return DS.Tables("tblclientes").DefaultView
    End Function
    Public Function ConsultaMovimientosXArticulo(ByVal pIdCliente As Integer, ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdInventario As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "delete from tblrepmovimientosxarticulo"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblrepmovimientosxarticulo(idventa,tipodoc,fecha,folio,clave,nombre,cantidad,preciou,precio) select tblventas.idventa,'F',tblventas.fecha,concat(tblventas.serie,convert(tblventas.folio using utf8)),tblinventario.clave,tblinventario.nombre,tblventasinventario.cantidad,tblventasinventario.precio/tblventasinventario.cantidad,tblventasinventario.precio from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario where tblventas.fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idcliente=" + pIdCliente.ToString
        If pIdInventario > 0 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
        End If
        'Comm.CommandText += " order by fecha desc"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblrepmovimientosxarticulo(idventa,tipodoc,fecha,folio,clave,nombre,cantidad,preciou,precio) select tblventas.idremision,'R',tblventas.fecha,concat(tblventas.serie,convert(tblventas.folio using utf8)),tblinventario.clave,tblinventario.nombre,tblventasinventario.cantidad,tblventasinventario.precio/tblventasinventario.cantidad,tblventasinventario.precio from tblventasremisionesinventario as tblventasinventario inner join tblventasremisiones as tblventas on tblventasinventario.idremision=tblventas.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario where tblventas.fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idcliente=" + pIdCliente.ToString
        If pIdInventario > 0 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
        End If
        Comm.ExecuteNonQuery()

        Comm.CommandText = "select idventa,tipodoc,fecha,folio,clave,nombre,cantidad,preciou,precio from tblrepmovimientosxarticulo order by fecha desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesma")
        Return DS.Tables("tblclientesma").DefaultView
    End Function
    Public Function ConsultaMovimientos(ByVal pIdCliente As Integer, ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pGenerarTabla As Boolean) As DataView
        Dim DS As New DataSet
        Comm.Parameters.Clear()
        Comm.CommandTimeout = 10000
        If pGenerarTabla Then
            If pIdCliente > 0 Then
                Comm.CommandText = "select spclientesmovimientos(" + pIdCliente.ToString + ",'" + pFecha1 + "','" + pFecha2 + "')"
                Comm.ExecuteNonQuery()
            Else
                Comm.CommandText = "select spclientesmovimientostodos('" + pFecha1 + "','" + pFecha2 + "')"
                Comm.ExecuteNonQuery()
            End If
        End If
        If pIdCliente > 0 Then
            Comm.CommandText = "select fecha,hora,case tipomovimiento when 1 then 'Factura' when 2 then 'Nota de Crédito' when 3 then 'Devolución' when 4 then 'Pago' when 5 then 'Nota de Cargo' when 6 then 'Documento' when 7 then 'Saldo Inicial' when 8 then 'Remisión' end as tipodocs,if(estado=3,'A','C')," + _
            "serie,folio,cargo,abono,saldo,estado,tipomovimiento,iddocumento from tblclientesmovimientos where idcliente=" + pIdCliente.ToString + " order by fecha,hora,estado,folio"
        Else
            Comm.CommandText = "delete from tblclientesmovimientossaldos"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select idcliente,spdasaldoafechaclientes(idcliente,'" + pFecha1 + "') from tblclientesmovimientos group by idcliente"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select fecha,hora,case tipomovimiento when 1 then 'Factura' when 2 then 'Nota de Crédito' when 3 then 'Devolución' when 4 then 'Pago' when 5 then 'Nota de Cargo' when 6 then 'Documento' when 7 then 'Saldo Inicial' when 8 then 'Remisión' end as tipodocs,if(tblclientesmovimientos.estado=3,'A','C')," + _
            "serie,folio,cargo,abono,tblclientesmovimientos.saldo,tblclientesmovimientos.estado,tipomovimiento,iddocumento,tblclientes.nombre,tblclientes.clave,saldoant,tblclientesmovimientos.idcliente from tblclientesmovimientos inner join tblclientes on tblclientesmovimientos.idcliente=tblclientes.idcliente inner join tblclientesmovimientossaldos on tblclientesmovimientossaldos.idcliente=tblclientesmovimientos.idcliente order by fecha,hora,estado,folio"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientes")
        'If pIdCliente = 0 Then DS.WriteXmlSchema("clientesmovimientostodos.xml")
        Return DS.Tables("tblclientes").DefaultView
    End Function

    Public Function DaSaldoAFecha(ByVal pIdCliente As Integer, ByVal pFecha As String) As Double
        Dim dSaldo As Double
        Comm.CommandText = "select ifnull((select if(idconversion=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and idcliente=" + pIdCliente.ToString + " and fecha<'" + pFecha + "' and estado=3),0)"
        dSaldo = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select if(idmoneda=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tblnotasdecargo where idcliente=" + pIdCliente.ToString + " and fecha<'" + pFecha + "' and estado=3),0)"
        dSaldo += Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select if(idmoneda=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tbldocumentosclientes where idcliente=" + pIdCliente.ToString + " and fecha<'" + pFecha + "' and estado=3),0)"
        dSaldo += Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select if(idmoneda=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tblventasremisiones inner join tblformasdepagoremisiones on tblventasremisiones.idforma=tblformasdepagoremisiones.idforma where tblformasdepagoremisiones.tipo=3 and idcliente=" + pIdCliente.ToString + " and fecha<'" + pFecha + "' and estado=3),0)"
        dSaldo += Comm.ExecuteScalar
        'pagos 
        Comm.CommandText = "select ifnull((select if(idmoneda=2,sum(cantidad),sum(cantidad*ptipodecambio)) from tblventaspagos where idcliente=" + pIdCliente.ToString + " and tblventaspagos.estado=3 and tblventaspagos.fecha<'" + pFecha + "'),0)"
        dSaldo -= Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select if(idmoneda=2,sum(cantidad),sum(cantidad*ptipodecambio)) from tblventaspagosremisiones where idcliente=" + pIdCliente.ToString + " and tblventaspagosremisiones.estado=3 and tblventaspagosremisiones.fecha<'" + pFecha + "'),0)"
        dSaldo -= Comm.ExecuteScalar
        Return dSaldo
    End Function
    Public Function TieneCreditoporFecha(pidCliente As Integer, pDiasCredito As Integer) As Boolean
        Dim Fmin, Fmin2 As String
        Comm.CommandText = "select ifnull((select min(fecha) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where estado=3 and round(totalapagar-credito,2)>0 and tblformasdepago.tipo=0 and tblventas.idcliente=" + pidCliente.ToString + "),'2100/01/01')"
        Fmin = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select min(fecha) from tblventasremisiones inner join tblformasdepagoremisiones on tblventasremisiones.idforma=tblformasdepagoremisiones.idforma where round(totalapagar-credito,2)>0 and tblformasdepagoremisiones.tipo=3 and tblventasremisiones.idcliente=" + pidCliente.ToString + " and estado=3),'2100/01/01')"
        Fmin2 = Comm.ExecuteScalar
        If Fmin2 < Fmin Then
            Fmin = Fmin2
        End If
        If DateAdd(DateInterval.Day, pDiasCredito, CDate(Fmin)).ToString("yyyy/MM/dd") < Date.Now.ToString("yyyy/MM/dd") Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function ReporteAnalitico(ByVal PidCliente As String, ByVal pFecha1 As String, ByVal pFecha2 As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblclientes.nombre,tblclientes.clave,round(spdasaldoafechaclientes(idcliente,'" + pFecha1 + "'),2) as saldoant," + _
        "round((ifnull((select if(tblventas.idconversion=2,sum(tblventas.totalapagar),sum(tblventas.totalapagar*tblventas.tipodecambio)) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.idcliente=tblclientes.idcliente and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3),0)+" + _
        "ifnull((select if(tblnotasdecargo.idmoneda=2,sum(tblnotasdecargo.totalapagar),sum(tblnotasdecargo.totalapagar*tblnotasdecargo.tipodecambio)) from tblnotasdecargo where tblnotasdecargo.idcliente=tblclientes.idcliente and tblnotasdecargo.fecha>='" + pFecha1 + "' and tblnotasdecargo.fecha<='" + pFecha2 + "' and tblnotasdecargo.estado=3),0)+" + _
        "ifnull((select if(tbldocumentosclientes.idmoneda=2,sum(tbldocumentosclientes.totalapagar),sum(tbldocumentosclientes.totalapagar*tbldocumentosclientes.tipodecambio)) from tbldocumentosclientes where tbldocumentosclientes.idcliente=tblclientes.idcliente and tbldocumentosclientes.fecha>='" + pFecha1 + "' and tbldocumentosclientes.fecha<='" + pFecha2 + "' and tbldocumentosclientes.estado=3),0)" + _
        "),2) as ventas," + _
        "round(ifnull((select if(tblventaspagos.idmoneda=2,sum(tblventaspagos.cantidad),sum(tblventaspagos.cantidad*tblventaspagos.ptipodecambio)) from tblventaspagos where tblventaspagos.idcliente=tblclientes.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha>='" + pFecha1 + "' and tblventaspagos.fecha<='" + pFecha2 + "'),0),2) as pagos" + _
        " from tblclientes"
        If PidCliente Then
            Comm.CommandText += " where tblclientes.idcliente=" + PidCliente.ToString
        End If
        Comm.CommandText += " order by tblclientes.nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesana")
        'DS.WriteXmlSchema("tblclientesana.xml")
        Return DS.Tables("tblclientesana").DefaultView
    End Function
    Public Function ReporteAnaliticoRem(ByVal PidCliente As String, ByVal pFecha1 As String, ByVal pFecha2 As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblclientes.nombre,tblclientes.clave,round(spdasaldoafechaclientesrem(idcliente,'" + pFecha1 + "'),2) as saldoant," + _
        "round((ifnull((select if(tblventasremisiones.idmoneda=2,sum(tblventasremisiones.totalapagar),sum(tblventasremisiones.totalapagar*tblventasremisiones.tipodecambio)) from tblventasremisiones inner join tblformasdepagoremisiones on tblventasremisiones.idforma=tblformasdepagoremisiones.idforma where tblformasdepagoremisiones.tipo=3 and tblventasremisiones.idcliente=tblclientes.idcliente and tblventasremisiones.fecha>='" + pFecha1 + "' and tblventasremisiones.fecha<='" + pFecha2 + "' and tblventasremisiones.estado=3),0)" + _
        "),2) as ventas," + _
        "round(ifnull((select if(tblventaspagosremisiones.idmoneda=2,sum(tblventaspagosremisiones.cantidad),sum(tblventaspagosremisiones.cantidad*tblventaspagosremisiones.ptipodecambio)) from tblventaspagosremisiones where tblventaspagosremisiones.idcliente=tblclientes.idcliente and tblventaspagosremisiones.estado=3 and tblventaspagosremisiones.fecha>='" + pFecha1 + "' and tblventaspagosremisiones.fecha<='" + pFecha2 + "'),0),2) as pagos" + _
        " from tblclientes"
        If PidCliente Then
            Comm.CommandText += " where tblclientes.idcliente=" + PidCliente.ToString
        End If
        Comm.CommandText += " order by tblclientes.nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesana")
        'DS.WriteXmlSchema("tblclientesana.xml")
        Return DS.Tables("tblclientesana").DefaultView
    End Function
    Public Function ChecaClaveRepetida(ByVal pClave As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(clave) from tblclientes where clave='" + Replace(pClave, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaClaveRepetida = False
        Else
            ChecaClaveRepetida = True
        End If
    End Function
    Public Function ChecaNombreRepetido(ByVal pNombre As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(nombre) from tblclientes where nombre='" + Replace(pNombre, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function BuscaCliente(ByVal pClave As String) As Boolean
        Dim Encontro As Integer
        If pClave <> "" Then
            Comm.CommandText = " select if((select idcliente from tblclientes where clave='" + Replace(pClave, "'", "''") + "') is null,0,(select idcliente from tblclientes where clave='" + Replace(pClave, "'", "''") + "'))"
            Encontro = Comm.ExecuteScalar
        Else
            Encontro = 0
        End If
        If Encontro = 0 Then
            BuscaCliente = False
        Else
            ID = Encontro
            BuscaCliente = True
            LlenaDatos()
        End If
    End Function
    Public Function BuscaClienteporRFC(ByVal pClave As String) As Boolean
        Dim Encontro As Integer
        Comm.CommandText = " select ifnull((select idcliente from tblclientes where rfc='" + Replace(pClave, "'", "''") + "' limit 1),0)"
        Encontro = Comm.ExecuteScalar
        If Encontro = 0 Then
            Return False
        Else
            ID = Encontro

            LlenaDatos()
            Return True
        End If
    End Function

    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblclientes where idcliente=" + ID.ToString
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
            DireccionFiscal = DReader("direccionfiscal")
            Credito = DReader("credito")
            CreditoDias = DReader("creditodias")
            Saldo = DReader("saldo")
            ISR = DReader("isr")
            IvaRetenido = DReader("ivaretenido")
            IdVendedor = DReader("idvendedor")
            IdLista = DReader("idlista")
            CURP = DReader("curp")
            IVA = DReader("iva")
            SobreescribeIVA = DReader("sobreescribeiva")
            Representante = DReader("representante")
            RepresentanteRFC = DReader("representanterfc")
            RepresentanteRegistro = DReader("representanteregistro")
            UsaAdenda = DReader("usaadenda")
            SaldoAFavor = DReader("saldoafavor")
            ActivarImpuestos = DReader("activarimpuestos")
            zona = DReader("zona")
            zona2 = DReader("zona2")
            imagen = DReader("imagen")
            IdCuenta = DReader("idcuenta")
            IdTipo = DReader("idtipo")

            fechaCambio = DReader("fechaCambio")
            idUsuario = DReader("idUsuario")
            idUsuarioAlta = DReader("idUsuarioAlta")
            horaCambio = DReader("horaCambio")
            fechaCreacion = DReader("fechaCreacion")
            horaCreacion = DReader("horaCreacion")
            'identificacion = DReader("identificacion")
            'numeroID = DReader("numeroID")
        End If
        DReader.Close()
    End Sub
    'Public Sub ModificaSaldo(ByVal pidCliente As Integer, ByVal pCantidad As Double, ByVal pTipo As Byte)
    '    If pTipo = 0 Then
    '        Comm.CommandText = "update tblclientes set saldo=saldo+" + pCantidad.ToString + " where idcliente=" + pidCliente.ToString
    '        Comm.ExecuteNonQuery()
    '    Else
    '        Comm.CommandText = "update tblclientes set saldo=saldo-" + pCantidad.ToString + " where idcliente=" + pidCliente.ToString
    '        Comm.ExecuteNonQuery()
    '    End If
    'End Sub
    Public Function Reporte(ByVal PCliente As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblclientes where concat(clave,nombre) like '%" + Replace(PCliente, "'", "''") + "%' order by nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientes")
        'DS.WriteXmlSchema("tblclientes.xml")
        Return DS.Tables("tblclientes").DefaultView
    End Function
    '******************
    Public Function Reporte2(ByVal pZona As Integer, ByVal pClave As String) As DataView
        Dim DS As New DataSet
        If pZona > 0 Then
            Comm.CommandText = "select * from tblclientes where zona= '" + pZona.ToString() + "' or zona2='" + pZona.ToString() + "' and clave like '%" + Replace(pClave, "'", "''") + "%' order by nombre"
        Else
            Comm.CommandText = "select * from tblclientes where clave like '%" + Replace(pClave, "'", "''") + "%' order by nombre"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientes")
        'DS.WriteXmlSchema("tblclientes.xml")
        Return DS.Tables("tblclientes").DefaultView
    End Function
    '******************

    Public Function ReporteSaldoaFavor(ByVal PidCliente As Integer, ByVal pFecha1 As String, ByVal pFecha2 As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "delete from tblrepsaldoafavor"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblrepsaldoafavor(idcliente,fecha,serie,folio,nombrec,clave,precio,iva,tipo,cantidad,saldoant) select tblventas.idcliente,tblventas.fecha,tblventas.serie,tblventas.folio,tblclientes.nombre,tblclientes.clave,tblventasinventario.precio,tblventasinventario.iva,'F',tblventasinventario.cantidad,spdasaldoafavorafecha(tblventas.idcliente,'" + pFecha1 + "') from tblventasinventario inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblventas on tblventas.idventa=tblventasinventario.idventa inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblinventario.esamortizacion=1 and tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If PidCliente <> 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + PidCliente.ToString
        End If
        Comm.ExecuteNonQuery()

        Comm.CommandText = "insert into tblrepsaldoafavor(idcliente,fecha,serie,folio,nombrec,clave,precio,iva,tipo,cantidad,saldoant) select tblventasremisiones.idcliente,tblventasremisiones.fecha,tblventasremisiones.serie,tblventasremisiones.folio,tblclientes.nombre,tblclientes.clave,tblventasremisionesinventario.precio,tblventasremisionesinventario.iva,'R',tblventasremisionesinventario.cantidad,spdasaldoafavorafecha(tblventasremisiones.idcliente,'" + pFecha1 + "') from tblventasremisionesinventario inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario inner join tblventasremisiones on tblventasremisiones.idremision=tblventasremisionesinventario.idremision inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente where tblinventario.esamortizacion=1 and tblventasremisiones.estado=3 and tblventasremisiones.fecha>='" + pFecha1 + "' and tblventasremisiones.fecha<='" + pFecha2 + "' and tblventasremisiones.usado=0"
        If PidCliente <> 0 Then
            Comm.CommandText += " and tblventasremisiones.idcliente=" + PidCliente.ToString
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select idcliente,fecha,serie,folio,nombrec,clave,precio,iva,tipo,cantidad,saldoant from tblrepsaldoafavor order by idcliente"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientessfavor")
        'DS.WriteXmlSchema("tblclientessfavorn.xml")
        Return DS.Tables("tblclientessfavor").DefaultView
    End Function
    Public Function DaSaldoAFavor(ByVal pIdcliente As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(precio*(1+tblventasinventario.iva/100)) from tblventasinventario inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblventas on tblventas.idventa=tblventasinventario.idventa where tblinventario.esamortizacion=1 and tblventas.estado=3 and tblventas.idcliente=" + pIdcliente.ToString + "),0)" + _
        "+ifnull((select sum(precio*(1+tblventasremisionesinventario.iva/100)) from tblventasremisionesinventario inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario inner join tblventasremisiones on tblventasremisiones.idremision=tblventasremisionesinventario.idremision where tblinventario.esamortizacion=1 and tblventasremisiones.estado=3 and tblventasremisiones.idcliente=" + pIdcliente.ToString + " and tblventasremisiones.usado=0),0)"
        Return Comm.ExecuteScalar
    End Function

    Public Function Consulta2() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idcliente,clave,nombre,rfc from tblclientes "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientes")
        Return DS.Tables("tblclientes").DefaultView
    End Function

    '****************************************************************
    Public Sub GuardarIdentificacion(ByVal pID As Integer, ByVal pIdentificacion As Integer, ByVal pNumeroID As String)

        'identificacion = pIdentificacion
        'numeroID = pNumeroID


        Comm.CommandText = "insert into tblclientesidenti(idcliente,identificacion,numero )values(" + pID.ToString + "," + pIdentificacion.ToString + ",'" + Replace(pNumeroID.ToString, "'", "''") + "')"
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()

    End Sub
    Public Sub ModificarIdentificacion(ByVal pID As Integer, ByVal pIdentificacion As Integer, ByVal pNumeroID As String)

        'identificacion = pIdentificacion
        'numeroID = pNumeroID
        Comm.CommandText = "update tblclientesidenti set numero='" + Replace(pNumeroID.ToString, "'", "''") + "' where idcliente=" + pID.ToString + " and identificacion=" + pIdentificacion.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub EliminarIdentificacion(ByVal pID As Integer, ByVal pIdentificacion As Integer)
        Comm.CommandText = "delete from tblclientesidenti where idcliente=" + pID.ToString + " and identificacion=" + pIdentificacion.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function existeIdentificacion(ByVal pID As Integer, ByVal pIdentificacion As Integer)
        Comm.CommandText = "select count(ID) from tblclientesidenti where idcliente=" + pID.ToString + " and identificacion=" + pIdentificacion.ToString
        Return Comm.ExecuteScalar()
    End Function
    Public Function obtenerIdentificacion(ByVal pID As Integer, ByVal pIdentificacion As Integer)
        Dim resultado As Integer = 0
        Dim aux As String = ""
        Comm.CommandText = "select count(ID) from tblclientesidenti where idcliente=" + pID.ToString + " and identificacion=" + pIdentificacion.ToString
        resultado = Comm.ExecuteScalar()
        If resultado <> 0 Then
            Comm.CommandText = "select numero from tblclientesidenti where idcliente=" + pID.ToString + " and identificacion=" + pIdentificacion.ToString
            aux = Comm.ExecuteScalar()
            Return aux
        Else
            Return aux
        End If

    End Function
    Public Function buscarUltimoID(ByVal pID As Integer)
        Dim resultado As Integer = 0
        Dim aux As Integer = -100
        Comm.CommandText = "select count(ID) from tblclientesidenti where idcliente=" + pID.ToString
        resultado = Comm.ExecuteScalar()
        If resultado <> 0 Then
            Comm.CommandText = "select max(ID) from tblclientesidenti where idcliente=" + pID.ToString
            resultado = Comm.ExecuteScalar()
            Comm.CommandText = "select identificacion from tblclientesidenti where ID=" + resultado.ToString
            Return Comm.ExecuteScalar()
        Else
            Return aux
        End If

    End Function
End Class
