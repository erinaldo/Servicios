Public Class dbInventario
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Cantidad As Double
    Public TipoCantidad As dbTiposCantidades
    Public Contenido As Double
    Public TipoContenido As dbTiposCantidades
    Public Descripcion As String
    Public Clave As String
    Public Clave2 As String
    Public PuntodeReorden As Double
    Public Clasificacion As dbInventarioClasificaciones
    Public Clasificacion2 As dbInventarioClasificaciones
    Public Clasificacion3 As dbInventarioClasificaciones
    Public CostoBase As Double
    'Public CostoBaseMoneda As dbMonedas
    Public NoParte As String
    Public ManejaSeries As Byte
    Public IdControl As Integer
    Public Inventariable As Byte
    Public Iva As Double
    Public RetieneIva As Byte
    Public IvaRet As Double
    Public ISR As Double
    Public IdVariante As Integer
    Public idServicio As Integer
    Public UltimoPrecioCompra As Double
    Public idMonedaCompra As Integer
    Public Fabricante As String
    Public PrecioNeto As Byte
    Public Ubicacion As String
    Public UsaFormula As Byte
    Public EsAmortizacion As Byte
    Public Peso As Double
    Public Maximo As Double
    Public Minimo As Double
    Public EsKit As Byte
    Public SepararKit As Byte
    Public PorLotes As Byte
    Public ieps As Double
    Public ivaRetenido As Double
    Public esRetDev As Byte
    Public Aduana As Byte
    Public Semillas As Byte
    Public Restaurante As Byte
    ' Public zona As String
    Public urlImagen As String
    Public Descontinuado As Byte
    Public FraccionArancel As String
    Public UnidadAduana As String
    Public cProdServ As String
    Public cUnidad As String
    Public SoloVentas As Byte
    Public SoloCompras As Byte
    Public SoloInventario As Byte
    Public UsaUbicacion As Boolean
    Enum TipoMovimiento As Integer
        Alta = 0
        Baja = 1
        Cambio = 2
        CambioBaja = 3
    End Enum
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Cantidad = 0
        TipoCantidad = New dbTiposCantidades(Conexion)
        Contenido = 0
        TipoContenido = New dbTiposCantidades(Conexion)
        Descripcion = ""
        Clave = ""
        Clave2 = ""
        PuntodeReorden = 0
        Clasificacion = New dbInventarioClasificaciones(Conexion, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        Clasificacion2 = New dbInventarioClasificaciones(Conexion, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
        Clasificacion3 = New dbInventarioClasificaciones(Conexion, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
        'CostoBaseMoneda = New dbMonedas(Conexion)
        CostoBase = 0
        NoParte = ""
        ManejaSeries = 0
        IdControl = 0
        Inventariable = 0
        Iva = 0
        RetieneIva = 0
        IdVariante = 0
        idServicio = 0
        Fabricante = ""
        Ubicacion = ""
        UsaFormula = 0
        EsAmortizacion = 0
        Peso = 0
        PorLotes = 0
        Maximo = 0
        Minimo = 0
        EsKit = 0
        SepararKit = 0
        esRetDev = 0
        Descontinuado = 0
        Aduana = 0
        Semillas = 0
        Restaurante = 0
        FraccionArancel = ""
        UnidadAduana = "06 PIEZA"
        SoloCompras = 0
        SoloVentas = 0
        SoloInventario = 0
        Comm.Connection = Conexion
        Comm.CommandTimeout = 120
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Comm.CommandTimeout = 120
        LlenaDatos()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pCantidad As Double, ByVal pidtipoCantidad As Integer, ByVal pContenido As Double, ByVal pIdTipoContenido As Integer, ByVal pIdClasificacion As Integer, ByVal pDescripcion As String, ByVal pClave As String, ByVal pPuntodeReorden As Double, ByVal pCostoBase As Double, ByVal pIdMonedaCostoBase As Integer, ByVal pNoparte As String, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pManejaSeries As Byte, ByVal pInventariable As Byte, ByVal pIva As Double, ByVal pRetieneIva As Byte, ByVal pClave2 As String, ByVal pFabricante As String, ByVal pPrecioNeto As Byte, ByVal pUbicacion As String, ByVal pUsaFormula As Byte, ByVal pEsAmortizacion As Byte, ByVal pPeso As Double, ByVal pMaximo As Double, ByVal pMinimo As Double, ByVal pEsKit As Byte, ByVal pSepararKit As Byte, ByVal pPorLotes As Byte, ByVal pIEPS As Double, ByVal pIvaRetenido As Double, ByVal purlImagen As String, pEsRevDEv As Byte, pAduana As Byte, pSemillas As Byte, pRest As Byte, pFraccionArancel As String, pUniAduana As String, pClaveProdServ As String, pClaveUnidad As String, pSV As Byte, pSC As Byte, pSI As Byte, pUsaUbicacion As Boolean)
        Nombre = pNombre
        Cantidad = pCantidad
        Clave = pClave
        Clave2 = pClave2
        Contenido = pContenido
        Descripcion = pDescripcion
        PuntodeReorden = pPuntodeReorden
        CostoBase = pCostoBase
        NoParte = pNoparte
        ManejaSeries = pManejaSeries
        Inventariable = pInventariable
        RetieneIva = pRetieneIva
        Iva = pIva
        Fabricante = pFabricante
        PrecioNeto = pPrecioNeto
        Ubicacion = pUbicacion
        UsaFormula = pUsaFormula
        EsAmortizacion = pEsAmortizacion
        Peso = pPeso
        Maximo = pMaximo
        Minimo = pMinimo
        EsKit = pEsKit
        PorLotes = pPorLotes
        SepararKit = pSepararKit
        ivaRetenido = pIvaRetenido
        ieps = pIEPS
        esRetDev = pEsRevDEv
        Aduana = pAduana
        Semillas = pSemillas
        UsaUbicacion = pUsaUbicacion
        '  zona = pzona
        urlImagen = purlImagen
        Restaurante = pRest
        Comm.CommandText = "insert into tblinventario(nombre, cantidad, tipocantidad, contenido, tipocontenido, descripcion, clave, idclasificacion, puntodereorden, costobase, idmonedacostobase, noparte, idclasificacion2, idclasificacion3, manejaseries, idcontrol, inventariable, iva,retieneiva, clave2,fabricante, precioneto, ubicacion, usaformula, esamortizacion, peso, maximo, minimo, eskit, separarkit, porlotes, ieps, ivaRetenido, urlImagen, esrevdev, aduana, semillas, descontinuado, idUsuarioAlta, fechaAlta, horaAlta, idUsuarioCambio, fechaCambio, horaCambio, restaurante, fraccionarancel, unidadaduana, cproductoserv, cunidad, soloventas, solocompras, soloinventario, usaubicacion) values ('" + Replace(Nombre, "'", "''") + "'," + Cantidad.ToString + "," + pidtipoCantidad.ToString + "," + Contenido.ToString + "," + pIdTipoContenido.ToString + ",'" + Replace(Descripcion, "'", "''") + "','" + Replace(Clave, "'", "''") + "'," + pIdClasificacion.ToString + "," + PuntodeReorden.ToString + ",0," + pIdMonedaCostoBase.ToString + ",'" + Replace(NoParte, "'", "''") + "'," + pidClasificacion2.ToString + "," + pidClasificacion3.ToString + "," + ManejaSeries.ToString + ",0," + Inventariable.ToString + "," + Iva.ToString + "," + RetieneIva.ToString + ",'" + Replace(Clave2, "'", "''") + "','" + Replace(Trim(Fabricante), "'", "''") + "'," + PrecioNeto.ToString + ",'" + Replace(Ubicacion, "'", "''") + "'," + UsaFormula.ToString + "," + EsAmortizacion.ToString + "," + Peso.ToString + "," + Maximo.ToString + "," + Minimo.ToString + "," + EsKit.ToString + "," + SepararKit.ToString + "," + PorLotes.ToString + "," + ieps.ToString() + "," + ivaRetenido.ToString() + ",'" + Replace(urlImagen, "\", "\\") + "'," + esRetDev.ToString + "," + Aduana.ToString + "," + Semillas.ToString + ",0," + GlobalIdUsuario.ToString + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + Restaurante.ToString + ",'" + Replace(pFraccionArancel, "'", "''") + "','" + pUniAduana + "','" + pClaveProdServ + "','" + pClaveUnidad + "'," + pSV.ToString + "," + pSC.ToString + "," + pSI.ToString + ", " + If(UsaUbicacion, "1", "0") + ")"
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idinventario) from tblinventario"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pCantidad As Double, ByVal pidtipoCantidad As Integer, ByVal pContenido As Double, ByVal pIdTipoContenido As Integer, ByVal pIdClasificacion As Integer, ByVal pDescripcion As String, ByVal pClave As String, ByVal pPuntodeReorden As Double, ByVal pCostoBase As Double, ByVal pIdMonedaCostoBase As Integer, ByVal pNoparte As String, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pManejaSeries As Byte, ByVal pInventariable As Byte, ByVal pIva As Double, ByVal pRetieneIva As Byte, ByVal pClave2 As String, ByVal pFabricante As String, ByVal pPrecioNeto As Byte, ByVal pUbicacion As String, ByVal pUsaFormula As Byte, ByVal pEsamortizacion As Byte, ByVal pPeso As Double, ByVal pMaximo As Double, ByVal pMinimo As Double, ByVal pEsKit As Byte, ByVal pSepararKit As Byte, ByVal pPorLotes As Byte, ByVal pIEPS As Double, ByVal pIVARetenido As Double, ByVal purlImagen As String, pesRevDEv As Byte, pAduana As Byte, pSemillas As Byte, pDescontinuado As Byte, pRest As Byte, pFraccionArancel As String, pUniAduana As String, pClaveProdServ As String, pClaveUnidad As String, pSV As Byte, pSC As Byte, pSI As Byte, pUsaUbicacion As Boolean)
        ID = pID
        Nombre = pNombre
        Cantidad = pCantidad
        Clave = pClave
        Contenido = pContenido
        Descripcion = pDescripcion
        PuntodeReorden = pPuntodeReorden
        CostoBase = pCostoBase
        NoParte = pNoparte
        ManejaSeries = pManejaSeries
        Inventariable = pInventariable
        Iva = pIva
        RetieneIva = pRetieneIva
        Clave2 = pClave2
        Fabricante = pFabricante
        PrecioNeto = pPrecioNeto
        Ubicacion = pUbicacion
        UsaFormula = pUsaFormula
        EsAmortizacion = pEsamortizacion
        Peso = pPeso
        Maximo = pMaximo
        Minimo = pMinimo
        EsKit = pEsKit
        SepararKit = pSepararKit
        PorLotes = pPorLotes
        ieps = pIEPS
        ivaRetenido = pIVARetenido
        urlImagen = purlImagen
        esRetDev = pesRevDEv
        Aduana = pAduana
        Semillas = pSemillas
        Descontinuado = pDescontinuado
        Restaurante = pRest
        UsaUbicacion = pUsaUbicacion
        ' zona = pzona
        Comm.CommandText = "update tblinventario set nombre='" + Replace(Nombre, "'", "''") + "',contenido=" + Contenido.ToString + ",tipocontenido=" + pIdTipoContenido.ToString + ",tipocantidad=" + pidtipoCantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',clave='" + Replace(Clave, "'", "''") + "',idclasificacion=" + pIdClasificacion.ToString + ",puntodereorden=" + PuntodeReorden.ToString + ",idmonedacostobase=" + pIdMonedaCostoBase.ToString + ",noparte='" + Replace(NoParte, "'", "''") + "',idclasificacion2=" + pidClasificacion2.ToString + ",idclasificacion3=" + pidClasificacion3.ToString + ",manejaseries=" + ManejaSeries.ToString + ",inventariable=" + Inventariable.ToString + ",retieneiva=" + RetieneIva.ToString + ",iva=" + Iva.ToString + ",clave2='" + Replace(Clave2, "'", "''") + "',fabricante='" + Replace(Trim(Fabricante), "'", "''") + "',precioneto=" + PrecioNeto.ToString + ",ubicacion='" + Replace(Ubicacion, "'", "''") + "',usaformula=" + UsaFormula.ToString + ",esamortizacion=" + EsAmortizacion.ToString + ",peso=" + Peso.ToString + ",maximo=" + Maximo.ToString + ",minimo=" + Minimo.ToString + ",eskit=" + EsKit.ToString + ",separarkit=" + SepararKit.ToString + ",porlotes=" + PorLotes.ToString + ",ieps=" + ieps.ToString() + ",ivaRetenido=" + ivaRetenido.ToString() + " ,urlImagen='" + Replace(urlImagen, "\", "\\") + "',esrevdev=" + esRetDev.ToString + ",aduana=" + Aduana.ToString + ",semillas=" + Semillas.ToString + ",descontinuado=" + Descontinuado.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "',restaurante=" + Restaurante.ToString + ",fraccionarancel='" + Replace(pFraccionArancel, "'", "''") + "',unidadaduana='" + pUniAduana + "',cproductoserv='" + pClaveProdServ + "',cunidad='" + pClaveUnidad + "', soloventas=" + pSV.ToString + ",solocompras=" + pSC.ToString + ", soloinventario=" + pSI.ToString + ", usaubicacion=" + If(UsaUbicacion, "1", "0") + " where idinventario=" + ID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblinventario where idinventario=" + pID.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdAlmacen As Integer, Optional ByVal pNombre As String = "", Optional ByVal pClave As String = "", Optional ByVal pidClasificacion As Integer = -1, Optional ByVal ClaveyNombre As Boolean = False, Optional ByVal pidClasificacion2 As Integer = 0, Optional ByVal pidClasificacion3 As Integer = 0, Optional ByVal pClave2 As String = "", Optional ByVal pInventariable As Byte = 0, Optional ByVal pFabricante As Boolean = False, Optional ByVal pModoB As Byte = 0, Optional ByVal pEskit As Byte = 0, Optional pDescontinuado As Byte = 0, Optional pRestaurante As Byte = 0, Optional pSoloV As Boolean = False, Optional pSoloC As Boolean = False, Optional pSoloI As Boolean = False) As DataView
        Dim DS As New DataSet
        Dim Palabras() As String
        If pModoB = 1 Then
            Palabras = pNombre.Split(Chr(32))
        Else
            ReDim Palabras(1)
            Palabras(0) = pNombre
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText = "select idinventario,clave,nombre,ifnull((select cantidad from tblalmacenesi where tblalmacenesi.idinventario=tblinventario.idinventario and idalmacen=" + pIdAlmacen.ToString + "),0) as cantidad from tblinventario where idinventario>1"
        Else
            Comm.CommandText = "select idinventario,clave,nombre,ifnull((select sum(cantidad) from tblalmacenesi where tblalmacenesi.idinventario=tblinventario.idinventario),0) as cantidad from tblinventario where idinventario>1"
        End If

        For Each s As String In Palabras
            If pFabricante = True Then
                Comm.CommandText += " and concat(clave,clave2,nombre,fabricante) like '%" + Replace(s, "'", "''") + "%'"
            Else
                Comm.CommandText += " and concat(clave,clave2,nombre) like '%" + Replace(s, "'", "''") + "%'"
            End If
        Next

        If pidClasificacion > 0 Then
            Comm.CommandText += " and idclasificacion=" + pidClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pInventariable = 1 Then
            Comm.CommandText += " and inventariable=1"
        End If
        If pInventariable = 2 Then
            Comm.CommandText += " and inventariable=0"
        End If
        If pEskit = 1 Then
            Comm.CommandText += " and eskit=0"
        End If
        If pRestaurante = 1 Then
            Comm.CommandText += " and restaurante=1"
        End If
        If pDescontinuado = 1 Then
            Comm.CommandText += " and descontinuado=0"
        End If
        If pDescontinuado = 2 Then
            Comm.CommandText += " and descontinuado=1"
        End If
        If pSoloV Then Comm.CommandText += " and ((solocompras=0 and soloinventario=0) or soloventas=1)"
        If pSoloC Then Comm.CommandText += " and ((soloinventario=0 and soloventas=0) or solocompras=1)"
        If pSoloI Then Comm.CommandText += " and ((solocompras=0 and soloventas=0) or soloinventario=1)"
        Comm.CommandText += " order by clave,nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventario")
        Return DS.Tables("tblinventario").DefaultView
    End Function

    Public Function ConsultaInventarioPorAlmacen(ByVal pidSucursal As Integer, ByVal pIdAlmacen As Integer, ByVal pidinventario As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblsucursales.nombre as snombre,tblalmacenes.nombre as anombre,tblalmacenesi.cantidad,"
        Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles md inner join tblmovimientos m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos ic on m.idconcepto=ic.idconcepto where ic.tipo=3 and m.transito=0 and md.idinventario=tblalmacenesi.idinventario and md.idalmacen2=tblalmacenesi.idalmacen and m.estado=3),0),tblalmacenes.idalmacen"
        Comm.CommandText += " from tblalmacenes inner join tblsucursales on tblalmacenes.idsucursal=tblsucursales.idsucursal inner join tblalmacenesi on tblalmacenes.idalmacen=tblalmacenesi.idalmacen where tblalmacenesi.idinventario=" + pidinventario.ToString
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblalmacenes.idsucursal=" + pidSucursal.ToString
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblalmacenes.idalmacen=" + pIdAlmacen.ToString
        End If
        Comm.CommandText += " order by snombre,anombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioalmacenes")
        Return DS.Tables("tblinventarioalmacenes").DefaultView
    End Function

    Public Function ConsultaInventarioPorUbicacion(ByVal pidSucursal As Integer, ByVal pIdAlmacen As Integer, ByVal pidinventario As Integer, ubicacion As String, tarima As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select a.numero, a.nombre, aiu.ubicacion, aiu.tarima, i.clave, i.nombre descripcion, sum(aiu.cantidad) cantidad, spdaultimocostoinv(i.idinventario) ucosto from tblalmacenes a inner join tblalmacenesi ai on a.idalmacen=ai.idalmacen inner join tblinventario i on ai.idinventario=i.idinventario inner join tblalmacenesiubicaciones aiu on aiu.idalmacen=ai.idalmacen and aiu.idinventario=ai.idinventario where aiu.ubicacion like '%" + ubicacion + "%' and ai.idinventario = " + pidinventario.ToString() + " and aiu.cantidad<>0 and tarima like '%" + tarima + "%'" 'a.idsucursal=" + pidSucursal.ToString
        If pIdAlmacen > 0 Then Comm.CommandText += " and a.idalmacen=" + pIdAlmacen.ToString
        Comm.CommandText += " group by a.nombre, aiu.ubicacion order by a.nombre, aiu.ubicacion;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrepinventarioxa")
        'DS.WriteXmlSchema("tblrepinventarioxa.xml")
        Return DS.Tables("tblrepinventarioxa").DefaultView
    End Function

    Public Function ChecaClaveRepetida(ByVal pClave As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(clave) from tblinventario where clave='" + Replace(pClave, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            Comm.CommandText = "select count(clave2) from tblinventario where clave2='" + Replace(pClave, "'", "''") + "'"
            Resultado = Comm.ExecuteScalar
            'If Resultado = 0 Then
            '    Comm.CommandText = "select count(clave) from tblproductos where clave='" + Replace(pClave, "'", "''") + "'"
            '    Resultado = Comm.ExecuteScalar
            'End If
        End If
        If Resultado = 0 Then
            ChecaClaveRepetida = False
        Else
            ChecaClaveRepetida = True
        End If
    End Function
    Public Sub ActualizaInventario(ByVal pIdInventario As Integer, ByVal pCantidad As Double, ByVal pIdAlmacen As Integer)
        Dim A As Integer
        VerificaTablaAlmacen(pIdInventario, pIdAlmacen)
        Comm.CommandText = "update tblalmacenesi set cantidad=" + pCantidad.ToString + " where idinventario=" + pIdInventario.ToString + " and idalmacen=" + pIdAlmacen.ToString
        A = Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblinventario set idcontrol=0 where idinventario=" + pIdInventario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaInventarioLote(ByVal pIdInventario As Integer, ByVal pCantidad As Double, ByVal pIdAlmacen As Integer, pIdLote As Integer)
        VerificaTablaAlmacenLote(pIdInventario, pIdAlmacen, pIdLote)
        Comm.CommandText = "update tblalmacenesilotes set cantidad=" + pCantidad.ToString + " where idinventario=" + pIdInventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + " and idlote=" + pIdLote.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaInventarioAduana(ByVal pIdInventario As Integer, ByVal pCantidad As Double, ByVal pIdAlmacen As Integer, pIdAduana As Integer)
        VerificaTablaAlmacenAduana(pIdInventario, pIdAlmacen, pIdAduana)
        Comm.CommandText = "update tblalmacenesiaduanas set cantidad=" + pCantidad.ToString + " where idinventario=" + pIdInventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + " and idaduana=" + pIdAduana.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub GuardaAHistorial(ByVal Fecha As String)
        Comm.CommandText = "delete from tblhinventario where fecha='" + Fecha + "'"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblhinventario select '" + Fecha + "',idinventario,cantidad from tblinventario"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub MovimientoDeInventario(ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pCantidadAnt As Double, ByVal TipoMov As TipoMovimiento, ByVal pIdAlmacen As Integer)
        Dim cCantidad As Double
        'Comm.CommandText = "select spmodificainventarioi(" + pIdinventario.ToString + "," + pIdAlmacen.ToString + "," + pCantidad.ToString + "," + pCantidadAnt.ToString + "," + CStr(TipoMov) + ")"
        'Comm.ExecuteNonQuery()
        VerificaTablaAlmacen(pIdinventario, pIdAlmacen)
        Dim EsInventariable As Integer
        Comm.CommandText = "select inventariable from tblinventario where idinventario=" + pIdinventario.ToString
        EsInventariable = Comm.ExecuteScalar
        If EsInventariable = 1 Then
            Select Case TipoMov
                Case TipoMovimiento.Alta
                    Comm.CommandText = "select cantidad from tblalmacenesi where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad + pCantidad
                    ActualizaInventario(pIdinventario, cCantidad, pIdAlmacen)
                Case TipoMovimiento.Baja
                    Comm.CommandText = "select cantidad from tblalmacenesi where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad - pCantidad
                    ActualizaInventario(pIdinventario, cCantidad, pIdAlmacen)
                Case TipoMovimiento.Cambio
                    Comm.CommandText = "select cantidad from tblalmacenesi where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad - pCantidadAnt + pCantidad
                    ActualizaInventario(pIdinventario, cCantidad, pIdAlmacen)
                Case TipoMovimiento.CambioBaja
                    Comm.CommandText = "select cantidad from tblalmacenesi where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad + pCantidadAnt - pCantidad
                    ActualizaInventario(pIdinventario, cCantidad, pIdAlmacen)
            End Select
        End If
    End Sub
    Public Sub MovimientoDeInventarioLote(ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pCantidadAnt As Double, ByVal TipoMov As TipoMovimiento, ByVal pIdAlmacen As Integer, pIdLote As Integer)
        Dim cCantidad As Double
        'Comm.CommandText = "select spmodificainventarioi(" + pIdinventario.ToString + "," + pIdAlmacen.ToString + "," + pCantidad.ToString + "," + pCantidadAnt.ToString + "," + CStr(TipoMov) + ")"
        'Comm.ExecuteNonQuery()
        VerificaTablaAlmacenLote(pIdinventario, pIdAlmacen, pIdLote)
        Dim EsInventariable As Integer
        Comm.CommandText = "select inventariable from tblinventario where idinventario=" + pIdinventario.ToString
        EsInventariable = Comm.ExecuteScalar
        If EsInventariable = 1 Then
            Select Case TipoMov
                Case TipoMovimiento.Alta
                    Comm.CommandText = "select cantidad from tblalmacenesilotes where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + " and idlote=" + pIdLote.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad + pCantidad
                    ActualizaInventarioLote(pIdinventario, cCantidad, pIdAlmacen, pIdLote)
                Case TipoMovimiento.Baja
                    Comm.CommandText = "select cantidad from tblalmacenesilotes where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + " and idlote=" + pIdLote.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad - pCantidad
                    ActualizaInventarioLote(pIdinventario, cCantidad, pIdAlmacen, pIdLote)
                Case TipoMovimiento.Cambio
                    Comm.CommandText = "select cantidad from tblalmacenesilotes where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + " and idlote=" + pIdLote.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad - pCantidadAnt + pCantidad
                    ActualizaInventarioLote(pIdinventario, cCantidad, pIdAlmacen, pIdLote)
                Case TipoMovimiento.CambioBaja
                    Comm.CommandText = "select cantidad from tblalmacenesilotes where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + " and idlote=" + pIdLote.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad + pCantidadAnt - pCantidad
                    ActualizaInventarioLote(pIdinventario, cCantidad, pIdAlmacen, pIdLote)
            End Select
        End If
    End Sub
    Public Sub MovimientoDeInventarioAduana(ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pCantidadAnt As Double, ByVal TipoMov As TipoMovimiento, ByVal pIdAlmacen As Integer, pIdAduana As Integer)
        Dim cCantidad As Double
        'Comm.CommandText = "select spmodificainventarioi(" + pIdinventario.ToString + "," + pIdAlmacen.ToString + "," + pCantidad.ToString + "," + pCantidadAnt.ToString + "," + CStr(TipoMov) + ")"
        'Comm.ExecuteNonQuery()
        VerificaTablaAlmacenAduana(pIdinventario, pIdAlmacen, pIdAduana)
        Dim EsInventariable As Integer
        Comm.CommandText = "select inventariable from tblinventario where idinventario=" + pIdinventario.ToString
        EsInventariable = Comm.ExecuteScalar
        If EsInventariable = 1 Then
            Select Case TipoMov
                Case TipoMovimiento.Alta
                    Comm.CommandText = "select cantidad from tblalmacenesiaduanas where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + " and idaduana=" + pIdAduana.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad + pCantidad
                    ActualizaInventarioAduana(pIdinventario, cCantidad, pIdAlmacen, pIdAduana)
                Case TipoMovimiento.Baja
                    Comm.CommandText = "select cantidad from tblalmacenesiaduanas where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + " and idaduana=" + pIdAduana.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad - pCantidad
                    ActualizaInventarioAduana(pIdinventario, cCantidad, pIdAlmacen, pIdAduana)
                Case TipoMovimiento.Cambio
                    Comm.CommandText = "select cantidad from tblalmacenesiaduanas where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + " and idaduana=" + pIdAduana.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad - pCantidadAnt + pCantidad
                    ActualizaInventarioAduana(pIdinventario, cCantidad, pIdAlmacen, pIdAduana)
                Case TipoMovimiento.CambioBaja
                    Comm.CommandText = "select cantidad from tblalmacenesiaduanas where idinventario=" + pIdinventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + " and idaduana=" + pIdAduana.ToString
                    cCantidad = Comm.ExecuteScalar
                    cCantidad = cCantidad + pCantidadAnt - pCantidad
                    ActualizaInventarioAduana(pIdinventario, cCantidad, pIdAlmacen, pIdAduana)
            End Select
        End If
    End Sub
    'Public Function DaPrecioDefault(Optional ByVal pIdInventario As Integer = -1) As dbInventarioPrecios
    '    Dim fID As Integer
    '    Dim fPrecioID As Integer
    '    If pIdInventario = -1 Then
    '        fID = ID
    '    Else
    '        fID = pIdInventario
    '    End If
    '    Comm.CommandText = "select if((select idinventarioprecio from tblinventarioprecios where esdefault=255 and idinventario=" + fID.ToString + " limit 1) is null,-1,(select idinventarioprecio from tblinventarioprecios where esdefault=255 and idinventario=" + fID.ToString + " limit 1))"
    '    fPrecioID = Comm.ExecuteScalar
    '    If fPrecioID = -1 Then
    '        Comm.CommandText = "select if((select idinventarioprecio from tblinventarioprecios where idinventario=" + fID.ToString + " limit 1) is null,-1,(select idinventarioprecio from tblinventarioprecios where idinventario=" + fID.ToString + " limit 1))"
    '        fPrecioID = Comm.ExecuteScalar
    '        If fPrecioID = -1 Then fPrecioID = 0
    '    End If
    '    Return New dbInventarioPrecios(fPrecioID, Comm.Connection)
    'End Function

    Public Sub LlenaDatos()
        Dim idClas As Integer
        Dim IdtipoCant As Integer
        Dim IdTipoCont As Integer
        Dim IdMonedaBase As Integer
        Dim idClas2 As Integer
        Dim idClas3 As Integer
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblinventario where idinventario=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Cantidad = DReader("cantidad")
            Contenido = DReader("contenido")
            Clave = DReader("clave")
            Descripcion = DReader("descripcion")
            idClas = DReader("idclasificacion")
            IdtipoCant = DReader("tipocantidad")
            IdTipoCont = DReader("tipocontenido")
            PuntodeReorden = DReader("puntodereorden")
            IdMonedaBase = DReader("idmonedacostobase")
            CostoBase = DReader("costobase")
            NoParte = DReader("noparte")
            idClas2 = DReader("idclasificacion2")
            idClas3 = DReader("idclasificacion3")
            ManejaSeries = DReader("manejaseries")
            Inventariable = DReader("inventariable")
            Iva = DReader("iva")
            RetieneIva = DReader("retieneiva")
            Clave2 = DReader("clave2")
            Fabricante = DReader("fabricante")
            PrecioNeto = DReader("precioneto")
            Ubicacion = DReader("ubicacion")
            UsaFormula = DReader("usaformula")
            EsAmortizacion = DReader("esamortizacion")
            Peso = DReader("peso")
            Maximo = DReader("maximo")
            Minimo = DReader("minimo")
            EsKit = DReader("eskit")
            SepararKit = DReader("separarkit")
            PorLotes = DReader("porlotes")
            ieps = DReader("ieps")
            ivaRetenido = DReader("ivaRetenido")
            'zona = DReader("zona")
            urlImagen = DReader("urlImagen")
            esRetDev = DReader("esrevdev")
            Semillas = DReader("semillas")
            Aduana = DReader("aduana")
            Descontinuado = DReader("descontinuado")
            Restaurante = DReader("restaurante")
            FraccionArancel = DReader("fraccionarancel")
            UnidadAduana = DReader("unidadaduana")
            cProdServ = DReader("cproductoserv")
            cUnidad = DReader("cunidad")
            SoloVentas = DReader("soloventas")
            SoloCompras = DReader("solocompras")
            SoloInventario = DReader("soloinventario")
            UsaUbicacion = DReader("usaubicacion")
        End If
        DReader.Close()
        Clasificacion = New dbInventarioClasificaciones(idClas, Comm.Connection, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        Clasificacion2 = New dbInventarioClasificaciones(idClas2, Comm.Connection, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
        Clasificacion3 = New dbInventarioClasificaciones(idClas3, Comm.Connection, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
        TipoCantidad = New dbTiposCantidades(IdtipoCant, Comm.Connection)
        TipoContenido = New dbTiposCantidades(IdTipoCont, Comm.Connection)
        'CostoBaseMoneda = New dbMonedas(IdMonedaBase, Comm.Connection)
    End Sub
    Public Function BuscaArticulo(ByVal pClave As String, ByVal pInventariable As Byte, pNombre As String, Optional ByVal pSoloRest As Boolean = False, Optional pSoloV As Boolean = False, Optional pSoloC As Boolean = False, Optional pSoloI As Boolean = False) As Boolean
        Dim Encontro As Integer
        Comm.CommandText = "select ifnull((select idinventario from tblinventario where idinventario>1 and descontinuado=0"
        If pClave <> "" Then Comm.CommandText += " and (clave='" + Replace(pClave, "'", "''") + "' or clave2='" + Replace(pClave, "'", "''") + "')"
        If pInventariable <> 0 Then Comm.CommandText += " and inventariable=1"
        If pSoloRest Then Comm.CommandText += " and restaurante=1"
        If pNombre <> "" Then Comm.CommandText += " and nombre='" + pNombre + "'"
        If pSoloV Then Comm.CommandText += " and ((solocompras=0 and soloinventario=0) or soloventas=1)"
        If pSoloC Then Comm.CommandText += " and ((soloinventario=0 and soloventas=0) or solocompras=1)"
        If pSoloI Then Comm.CommandText += " and ((solocompras=0 and soloventas=0) or soloinventario=1)"

        Comm.CommandText += " limit 1),0)"
        Encontro = Comm.ExecuteScalar
        If Encontro = 0 Then
            Return False
        Else
            ID = Encontro
            LlenaDatos()
            Return True
        End If
    End Function

    Public Function BuscaArticuloXML(ByVal pClave As String, ByVal pInventariable As Byte, Optional ByVal pSoloRest As Boolean = False, Optional pNombre As String = "") As Boolean
        Comm.CommandText = "select ifnull((select idinventario from tblinventario where idinventario>1 and descontinuado=0"
        If pClave <> "" Then Comm.CommandText += " and (clave='" + Replace(pClave, "'", "''") + "' or clave2='" + Replace(pClave, "'", "''") + "')"
        If pInventariable <> 0 Then Comm.CommandText += " and inventariable=1"
        If pNombre <> "" Then Comm.CommandText += " and nombre='" + pNombre + "'"
        If pSoloRest Then Comm.CommandText += " and restaurante=1"
        Comm.CommandText += " limit 1),0)"
        ID = Comm.ExecuteScalar
        If ID <> 0 Then LlenaDatos()
        Return ID <> 0
    End Function
    Public Function DaIdArticulo(ByVal pClave As String) As Boolean
        Dim Encontro As Integer
        If pClave <> "" Then
            Comm.CommandText = "select ifnull((select idinventario from tblinventario where idinventario>1 and (clave='" + Replace(pClave, "'", "''") + "' or clave2='" + Replace(pClave, "'", "''") + "') limit 1),0)"
            Encontro = Comm.ExecuteScalar
            If Encontro = 0 Then
                Return False
            Else
                ID = Encontro
                Return True
            End If
        Else
            Return False
        End If
    End Function
    Public Function DaInventario(ByVal pIdAlmacen As Integer, ByVal pIdInventario As Integer, Optional ubicacion As String = "") As Double
        If ubicacion = "" Then
            Comm.CommandText = "select ifnull((select cantidad from tblalmacenesi where idinventario=" + pIdInventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + "),0)"
        Else
            Comm.CommandText = "select ifnull((select aiu.cantidad from tblalmacenesi ai inner join tblalmacenesiubicaciones aiu on aiu.idalmacen=ai.idalmacen and aiu.idinventario=ai.idinventario where ai.idinventario=" + pIdInventario.ToString + " and ai.idalmacen=" + pIdAlmacen.ToString + " and aiu.ubicacion='" + Trim(Replace(ubicacion, "'", "''")) + "'),0)"
        End If
        Return Comm.ExecuteScalar
    End Function
    Public Function ChecaInventarioKits(ByVal pIdAlmacen As Integer, ByVal pIdInventariop As Integer, ByVal pCantidad As Double) As String
        'Comm.CommandText = "select ifnull((select cantidad from tblalmacenesi where idinventario=" + pIdInventario.ToString + " and idalmacen=" + pIdAlmacen.ToString + "),0)"
        Dim Str As String = ""
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tblinventariodetalles.cantidad,spdainventario(tblinventariodetalles.idinventario," + pIdAlmacen.ToString + ",0) as existencia,tblinventario.nombre,tblinventario.clave from tblinventariodetalles inner join tblinventario on tblinventariodetalles.idinventario=tblinventario.idinventario where idinventariop=" + pIdInventariop.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            If DReader("cantidad") * pCantidad > DReader("existencia") Then
                Str += " Insuficiente inventario para " + DReader("clave") + " " + DReader("nombre") + vbCrLf
            End If
        End While
        DReader.Close()
        'Comm.CommandText = "select count(tblinventariodetalles.idinventario) from tblinventariodetalles where tblinventariodetalles.idinventariop=" + pIdInventariop.ToString + " and tblinventariodetalles.cantidad*" + pCantidad.ToString + "<ifnull((select tblalmacenesi.cantidad from tblalmacenesi inner join tblinventario on tblalmacenesi.idinventario=tblinventario.idinventario where tblalmacenesi.idinventario=tblinventariodetalles.idinventario and tblinventario.inventariable=1 and ablalmacenesi.idalmacen=" + pIdAlmacen.ToString + "),0)"
        Return Str
    End Function
    Public Function KitconSerie(ByVal pIdinventarioP As Integer) As Integer
        Comm.CommandText = "select ifnull((select sum(manejaseries) from tblinventariodetalles inner join tblinventario on tblinventariodetalles.idinventario=tblinventario.idinventario where tblinventariodetalles.idinventariop=" + pIdinventarioP.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function

    Public Function CuantosKits(ByVal pIdinventarioP As Integer, ByVal pIdalmacen As Integer) As Double
        Comm.CommandText = "select ifnull((select min(spdainventario(tblinventariodetalles.idinventario," + pIdalmacen.ToString + ",0)/tblinventariodetalles.cantidad) from tblinventariodetalles where tblinventariodetalles.idinventariop=" + pIdinventarioP.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function

    Public Function DaInventarioTodos(ByVal pIdInventario As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(cantidad) from tblalmacenesi where idinventario=" + pIdInventario.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Private Sub VerificaTablaAlmacen(ByVal pidInventario As Integer, ByVal pidAlmacen As Integer)
        Dim Hay As String
        Comm.CommandText = "select ifnull((select 'si' from tblalmacenesi where idinventario=" + pidInventario.ToString + " and idalmacen=" + pidAlmacen.ToString + "),'no')"
        Hay = Comm.ExecuteScalar
        If Hay = "no" Then
            Comm.CommandText = "insert into tblalmacenesi(idalmacen,idinventario,cantidad) values(" + pidAlmacen.ToString + "," + pidInventario.ToString + ",0)"
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Private Sub VerificaTablaAlmacenLote(ByVal pidInventario As Integer, ByVal pidAlmacen As Integer, pIdLote As Integer)
        Dim Hay As String
        Comm.CommandText = "select ifnull((select 'si' from tblalmacenesilotes where idinventario=" + pidInventario.ToString + " and idalmacen=" + pidAlmacen.ToString + " and idlote=" + pIdLote.ToString + "),'no')"
        Hay = Comm.ExecuteScalar
        If Hay = "no" Then
            Comm.CommandText = "insert into tblalmacenesilotes(idalmacen,idinventario,idlote,cantidad) values(" + pidAlmacen.ToString + "," + pidInventario.ToString + "," + pIdLote.ToString + ",0)"
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Private Sub VerificaTablaAlmacenAduana(ByVal pidInventario As Integer, ByVal pidAlmacen As Integer, pIdAduana As Integer)
        Dim Hay As String
        Comm.CommandText = "select ifnull((select 'si' from tblalmacenesiaduanas where idinventario=" + pidInventario.ToString + " and idalmacen=" + pidAlmacen.ToString + " and idaduana=" + pIdAduana.ToString + "),'no')"
        Hay = Comm.ExecuteScalar
        If Hay = "no" Then
            Comm.CommandText = "insert into tblalmacenesiaduanas(idalmacen,idinventario,idaduana,cantidad) values(" + pidAlmacen.ToString + "," + pidInventario.ToString + "," + pIdAduana.ToString + ",0)"
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Function ConsultaAmbos(ByVal pNombre As String, ByVal pidAlmacen As Integer, ByVal SoloInventariables As Byte, ByVal pFabricante As Boolean, ByVal pModoB As Byte) As DataView
        Dim DS As New DataSet
        Dim Palabras() As String
        If pModoB = 1 Then
            Palabras = pNombre.Split(Chr(32))
        Else
            ReDim Palabras(1)
            Palabras(0) = pNombre
        End If
        'If pFabricante = False Then
        If SoloInventariables = 0 Then
            Comm.CommandText = "select idinventario,clave,nombre,'I' as tipo,spdainventario(idinventario," + pidAlmacen.ToString + ",0) as inv from tblinventario where idinventario>1" 'and concat(clave,clave2,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
            'Comm.CommandText = "select idinventario,clave,nombre,'I' as tipo,spdainventario(idinventario," + pidAlmacen.ToString + ",0) as inv from tblinventario where idinventario>1 and concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%' union select idproducto,clave,nombre,'P' as tipo,if(inventariable=1,spdainventariop(idproducto," + pidAlmacen.ToString + ",0),(select spdainventariovariante(tblproductosvariantes.idvariante," + pidAlmacen.ToString + ",0) from tblproductosvariantes where tblproductosvariantes.idproducto=tblproductos.idproducto limit 1)) as inv from tblproductos where idproducto>1 and concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        Else
            Comm.CommandText = "select idinventario,clave,nombre,'I' as tipo,spdainventario(idinventario," + pidAlmacen.ToString + ",0) as inv from tblinventario where idinventario>1 and inventariable=1" ' and concat(clave,clave2,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
            'Comm.CommandText = "select idinventario,clave,nombre,'I' as tipo,spdainventario(idinventario," + pidAlmacen.ToString + ",0) as inv from tblinventario where idinventario>1 and inventariable=1 and concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%' union select idproducto,clave,nombre,'P' as tipo,if(inventariable=1,spdainventariop(idproducto," + pidAlmacen.ToString + ",0),(select spdainventariovariante(tblproductosvariantes.idvariante," + pidAlmacen.ToString + ",0) from tblproductosvariantes where tblproductosvariantes.idproducto=tblproductos.idproducto limit 1)) as inv from tblproductos where idproducto>1 and inventariable=1 and concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        End If
        'Else
        'If SoloInventariables = 0 Then
        'Comm.CommandText = "select idinventario,clave,nombre,'I' as tipo,spdainventario(idinventario," + pidAlmacen.ToString + ",0) as inv from tblinventario where idinventario>1" 'and concat(clave,clave2,nombre,fabricante) like '%" + Replace(pNombre, "'", "''") + "%'"
        'Comm.CommandText = "select idinventario,clave,nombre,'I' as tipo,spdainventario(idinventario," + pidAlmacen.ToString + ",0) as inv from tblinventario where idinventario>1 and concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%' union select idproducto,clave,nombre,'P' as tipo,if(inventariable=1,spdainventariop(idproducto," + pidAlmacen.ToString + ",0),(select spdainventariovariante(tblproductosvariantes.idvariante," + pidAlmacen.ToString + ",0) from tblproductosvariantes where tblproductosvariantes.idproducto=tblproductos.idproducto limit 1)) as inv from tblproductos where idproducto>1 and concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        'Else
        'Comm.CommandText = "select idinventario,clave,nombre,'I' as tipo,spdainventario(idinventario," + pidAlmacen.ToString + ",0) as inv from tblinventario where idinventario>1" ' and inventariable=1 and concat(clave,clave2,nombre,fabricante) like '%" + Replace(pNombre, "'", "''") + "%'"
        'Comm.CommandText = "select idinventario,clave,nombre,'I' as tipo,spdainventario(idinventario," + pidAlmacen.ToString + ",0) as inv from tblinventario where idinventario>1 and inventariable=1 and concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%' union select idproducto,clave,nombre,'P' as tipo,if(inventariable=1,spdainventariop(idproducto," + pidAlmacen.ToString + ",0),(select spdainventariovariante(tblproductosvariantes.idvariante," + pidAlmacen.ToString + ",0) from tblproductosvariantes where tblproductosvariantes.idproducto=tblproductos.idproducto limit 1)) as inv from tblproductos where idproducto>1 and inventariable=1 and concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        'End If
        'End If

        For Each s As String In Palabras
            If pFabricante = True Then
                Comm.CommandText += " and concat(clave,clave2,nombre,fabricante) like '%" + Replace(s, "'", "''") + "%'"
            Else
                Comm.CommandText += " and concat(clave,clave2,nombre) like '%" + Replace(s, "'", "''") + "%'"
            End If
        Next
        Comm.CommandText += " order by clave,nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventario")
        Return DS.Tables("tblinventario").DefaultView
    End Function

    Public Function ReporteInventario(ByVal pIdAlmacen As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pIdSucursal As Integer, ByVal pConUltimoCosto As Boolean, ByVal pSoloExistencia As Boolean, ByVal pSoloMovimientos As Boolean, pSoloInventariables As Boolean, pDescontinuados As Byte, PordenaNombre As Boolean, pSoloDescontinuados As Boolean) As DataView
        Dim DS As New DataSet
        If pIdAlmacen < 0 Then pIdAlmacen = 0
        If pConUltimoCosto = False Then
            Comm.CommandText = "select idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ") as existencia,costobase,contenido from tblinventario where idinventario>1"
        Else
            Comm.CommandText = "select idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ") as existencia,spdaultimocostoinv(idinventario) as costobase,contenido from tblinventario where idinventario>1"
        End If

        If pIdInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pIdInventario.ToString
        End If
        If pidClasificacion > 0 Then
            Comm.CommandText += " and idclasificacion=" + pidClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pSoloExistencia Then
            Comm.CommandText += " and spdainventario(idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ")<>0"
        End If
        If pSoloMovimientos Then
            Comm.CommandText += " and sp_haymovimiento(idinventario)<>0"
        End If
        If pSoloInventariables Then
            Comm.CommandText += " and inventariable=1"
        End If
        If pDescontinuados = 0 And pSoloDescontinuados = False Then
            Comm.CommandText += " and descontinuado=0"
        End If
        If pSoloDescontinuados Then
            Comm.CommandText += " and descontinuado=1"
        End If
        If PordenaNombre Then
            Comm.CommandText += " order by nombre"
        Else
            Comm.CommandText += " order by clave"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrepinventario")
        'DS.WriteXmlSchema("tblrepinventario.xml")
        Return DS.Tables("tblrepinventario").DefaultView
    End Function
    Public Function ReporteInventarioporAlmacen(ByVal pIdAlmacen As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pIdSucursal As Integer, ByVal pConUltimoCosto As Boolean, ByVal pSoloExistencia As Boolean, ByVal pSoloMovimientos As Boolean, pDescon As Byte, ordenanombre As Boolean) As DataView
        Dim DS As New DataSet
        If pIdAlmacen < 0 Then pIdAlmacen = 0
        If pConUltimoCosto = False Then
            Comm.CommandText = "select tblinventario.idinventario,tblalmacenes.idalmacen,tblalmacenes.nombre,spdainventario(tblinventario.idinventario,tblalmacenes.idalmacen,0) as existencia,tblinventario.nombre as descriocion,tblinventario.clave as codigo,(select nombre from tblsucursales where tblsucursales.idsucursal=tblalmacenes.idsucursal) as sucursal,tblinventario.costobase as costo,spdainventario(tblinventario.idinventario,0,0) as existenciag from tblinventario,tblalmacenes where tblalmacenes.idalmacen>1 and tblinventario.idinventario>1"
        Else
            Comm.CommandText = "select tblinventario.idinventario,tblalmacenes.idalmacen,tblalmacenes.nombre,spdainventario(tblinventario.idinventario,tblalmacenes.idalmacen,0) as existencia,tblinventario.nombre as descriocion,tblinventario.clave as codigo,(select nombre from tblsucursales where tblsucursales.idsucursal=tblalmacenes.idsucursal) as sucursal,spdaultimocostoinv(tblinventario.idinventario) as costo,spdainventario(tblinventario.idinventario,0,0) as existenciag from tblinventario,tblalmacenes where tblalmacenes.idalmacen>1 and tblinventario.idinventario>1"
        End If

        If pIdInventario <> 0 Then
            Comm.CommandText += " and tblinventario.idinventario=" + pIdInventario.ToString
        End If
        If pidClasificacion > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblalmacenes.idsucursal=" + pIdSucursal.ToString
        End If
        'If pSoloExistencia Then
        '    Comm.CommandText += " and spdainventario(idinventario,0,0)<>0"
        'End If
        If pSoloMovimientos Then
            Comm.CommandText += " and sp_haymovimiento(idinventario)<>0"
        End If
        If pDescon = 0 Then
            Comm.CommandText += " and descontinuado=0"
        End If
        If ordenanombre Then
            Comm.CommandText += " order by tblinventario.nombre"
        Else
            Comm.CommandText += " order by tblinventario.clave"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrepinventarioxa")
        'DS.WriteXmlSchema("tblrepinventarioxa.xml")
        Return DS.Tables("tblrepinventarioxa").DefaultView
    End Function

    Public Function ReporteInventarioAFecha(ByVal pIdAlmacen As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pIdSucursal As Integer, ByVal pSoloExistencia As Boolean, ByVal pSoloMovimientos As Boolean, pFecha As String, pDescon As Byte, ordenaNombre As Boolean) As DataView
        Dim DS As New DataSet
        pFecha = Format(DateAdd(DateInterval.Day, 1, CDate(pFecha)), "yyyy/MM/dd")
        If pIdAlmacen < 0 Then pIdAlmacen = 0
        'If pConUltimoCosto = False Then
        Comm.CommandText = "select idinventario,clave,nombre,spdainventarioafecha(idinventario,'" + pFecha + "'," + pIdSucursal.ToString + "," + pIdAlmacen.ToString + ") as existencia,ifnull((select costo from tblinventariocostoh where idinventario=tblinventario.idinventario and fecha<='" + pFecha + "' order by fecha desc limit 1),0) as costobase,contenido from tblinventario where idinventario>1 and inventariable=1"
        'Else
        'Comm.CommandText = "select idinventario,clave,nombre,spdainventarioafecha(idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ") as existencia,spdaultimocostoinv(idinventario) as costobase,contenido from tblinventario where idinventario>1 "
        'End If
        If pIdInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pIdInventario.ToString
        End If
        If pidClasificacion > 0 Then
            Comm.CommandText += " and idclasificacion=" + pidClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + pidClasificacion3.ToString
        End If
        'If pSoloExistencia Then
        '    Comm.CommandText += " and spdainventarioafecha(idinventario,'" + pFecha + "'," + pIdSucursal.ToString + "," + pIdAlmacen.ToString + ")<>0"
        'End If
        If pSoloMovimientos Then
            Comm.CommandText += " and sp_haymovimiento(idinventario)<>0"
        End If
        If pDescon = 0 Then
            Comm.CommandText += " and descontinuado=0"
        End If
        If ordenaNombre Then
            Comm.CommandText += " order by nombre"
        Else
            Comm.CommandText += " order by clave,nombre"
        End If
        Comm.CommandTimeout = 100000
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrepinventario")
        'DS.WriteXmlSchema("tblrepinventario.xml")
        Return DS.Tables("tblrepinventario").DefaultView
    End Function
    Public Function ReporteInventarioPorClasificacion(ByVal pIdAlmacen As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pIdSucursal As Integer, ByVal pConUltimoCosto As Boolean, ByVal pSoloExistencia As Boolean, ByVal pSoloMovimientos As Boolean, pFecha As String, pAfecha As Boolean, pDescon As Byte, ordenanombre As Boolean) As DataView
        Dim DS As New DataSet
        If pIdAlmacen < 0 Then pIdAlmacen = 0
        If pAfecha = False Then
            If pConUltimoCosto = False Then
                Comm.CommandText = "select idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ") as existencia,costobase,contenido," + _
                    "(select nombre from tblinventarioclasificaciones where idclasificacion=tblinventario.idclasificacion) as nclase1,(select nombre from tblinventarioclasificaciones2 where idclasificacion=tblinventario.idclasificacion2) as nclase2,(select nombre from tblinventarioclasificaciones3 where idclasificacion=tblinventario.idclasificacion3) as nclase3,tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3" + _
                    " from tblinventario where idinventario>1 and inventariable=1"
            Else
                Comm.CommandText = "select idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ") as existencia,spdaultimocostoinv(idinventario) as costobase,contenido," + _
                    "(select nombre from tblinventarioclasificaciones where idclasificacion=tblinventario.idclasificacion) as nclase1,(select nombre from tblinventarioclasificaciones2 where idclasificacion=tblinventario.idclasificacion2) as nclase2,(select nombre from tblinventarioclasificaciones3 where idclasificacion=tblinventario.idclasificacion3) as nclase3,tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3" + _
                    " from tblinventario where idinventario>1 and inventariable=1"
            End If
        Else
            Comm.CommandText = "select idinventario,clave,nombre,spdainventarioafecha(idinventario,'" + pFecha + "'," + pIdSucursal.ToString + "," + pIdAlmacen.ToString + ") as existencia,ifnull((select costo from tblinventariocostoh where idinventario=tblinventario.idinventario and fecha<='" + pFecha + "' order by fecha desc limit 1),0) as costobase,contenido," + _
                   "(select nombre from tblinventarioclasificaciones where idclasificacion=tblinventario.idclasificacion) as nclase1,(select nombre from tblinventarioclasificaciones2 where idclasificacion=tblinventario.idclasificacion2) as nclase2,(select nombre from tblinventarioclasificaciones3 where idclasificacion=tblinventario.idclasificacion3) as nclase3,tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3" + _
                    " from tblinventario where idinventario>1 and inventariable=1"
        End If
        If pIdInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pIdInventario.ToString
        End If
        If pidClasificacion > 0 Then
            Comm.CommandText += " and idclasificacion=" + pidClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pSoloExistencia Then
            Comm.CommandText += " and spdainventario(idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ")<>0"
        End If
        If pSoloMovimientos Then
            Comm.CommandText += " and sp_haymovimiento(idinventario)<>0"
        End If
        If pDescon = 0 Then
            Comm.CommandText += " and descontinuado=0"
        End If
        If ordenaNombre Then
            Comm.CommandText += " order by nombre"
        Else
            Comm.CommandText += " order by clave,nombre"
        End If
        Comm.CommandTimeout = 100000
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrepinventariocl")
        'DS.WriteXmlSchema("tblrepinventariocl.xml")
        Return DS.Tables("tblrepinventariocl").DefaultView
    End Function
    Public Function ReporteInventarioApartado(ByVal pIdAlmacen As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pIdSucursal As Integer, ByVal pConUltimoCosto As Boolean, pDescon As Byte, ordenanombre As Boolean) As DataView
        Dim DS As New DataSet
        If pIdAlmacen < 0 Then pIdAlmacen = 0
        If pConUltimoCosto = False Then
            Comm.CommandText = "select idinventario,clave,nombre,ifnull((select sum(cantidad) from tblventasapartadosdetalles inner join tblventasapartados on tblventasapartadosdetalles.idapartado=tblventasapartados.idapartado where tblventasapartados.surtido=0 and tblventasapartadosdetalles.idinventario=tblinventario.idinventario and tblventasapartados.estado=3 and tblventasapartados.dinventario=1),0) as existencia,costobase,contenido from tblinventario where idinventario>1"
        Else
            Comm.CommandText = "select idinventario,clave,nombre,ifnull((select sum(cantidad) from tblventasapartadosdetalles inner join tblventasapartados on tblventasapartadosdetalles.idapartado=tblventasapartados.idapartado where tblventasapartados.surtido=0 and tblventasapartadosdetalles.idinventario=tblinventario.idinventario and tblventasapartados.estado=3 and tblventasapartados.dinventario=1),0) as existencia,spdaultimocostoinv(idinventario) as costobase,contenido from tblinventario where idinventario>1 "
        End If
        If pIdInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pIdInventario.ToString
        End If
        If pidClasificacion > 0 Then
            Comm.CommandText += " and idclasificacion=" + pidClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pDescon = 0 Then
            Comm.CommandText += " and descontinuado=0"
        End If
        'If pSoloExistencia Then
        '    Comm.CommandText += " and spdainventario(idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ")<>0"
        'End If
        'If pSoloMovimientos Then
        '    Comm.CommandText += " and sp_haymovimiento(idinventario)<>0"
        'End If
        If ordenanombre Then
            Comm.CommandText += " order by nombre"
        Else
            Comm.CommandText += " order by clave"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrepinventario")
        'DS.WriteXmlSchema("tblrepinventario.xml")
        Return DS.Tables("tblrepinventario").DefaultView
    End Function

    Public Function ReporteMaxMin(ByVal pIdAlmacen As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pIdSucursal As Integer, ByVal pConUltimoCosto As Boolean, ByVal pSoloExistencia As Boolean, ByVal pSoloMovimientos As Boolean, pDescon As Byte, ordenanombre As Boolean) As DataView
        Dim DS As New DataSet
        If pIdAlmacen < 0 Then pIdAlmacen = 0
        If pConUltimoCosto = False Then
            Comm.CommandText = "select idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ") as existencia,costobase,contenido,maximo,minimo,puntodereorden from tblinventario where idinventario>1"
        Else
            Comm.CommandText = "select idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ") as existencia,spdaultimocostoinv(idinventario) as costobase,contenido,maximo,minimo,puntodereorden from tblinventario where idinventario>1 "
        End If
        If pIdInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pIdInventario.ToString
        End If
        If pidClasificacion > 0 Then
            Comm.CommandText += " and idclasificacion=" + pidClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pSoloExistencia Then
            Comm.CommandText += " and spdainventario(idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ")<>0"
        End If
        If pSoloMovimientos Then
            Comm.CommandText += " and sp_haymovimiento(idinventario)<>0"
        End If
        If pDescon = 0 Then
            Comm.CommandText += " and descontinuado=0"
        End If
        If ordenanombre Then
            Comm.CommandText += " order by nombre"
        Else
            Comm.CommandText += " order by clave"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrepmaxmin")
        'DS.WriteXmlSchema("tblrepmaxmin.xml")
        Return DS.Tables("tblrepmaxmin").DefaultView
    End Function
    Public Function ReporteCostos(ByVal pIdAlmacen As Integer, ByVal pIdInventario As Integer, ByVal pTipoCosteo As Byte, ByVal pTipodeCambio As Double, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidSucursal As Integer, ByVal pconUltimoCosto As Boolean, psinExistencia As Boolean, pDescon As Byte, ordenanombre As Boolean) As DataView
        Dim DS As New DataSet
        'If pconUltimoCosto = False Then
        If pTipoCosteo = 2 Then
            Comm.CommandText = "select idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + "," + pidSucursal.ToString + ") as existencia,spsacacostoarticulo(idinventario," + pTipoCosteo.ToString + ",contenido," + pTipodeCambio.ToString + ") as costo,spdaultimocostoinv(idinventario) as ucosto from tblinventario where idinventario>1 "
        Else
            Comm.CommandText = "select idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + "," + pidSucursal.ToString + ") as existencia,costobase as costo,spdaultimocostoinv(idinventario) as ucosto from tblinventario where idinventario>1 "
        End If
        'Else
        'Comm.CommandText = "select idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + "," + pidSucursal.ToString + ") as existencia,spdaultimocostoinv(idinventario) as costo from tblinventario where idinventario>1 "
        'End If
        If pIdInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pIdInventario.ToString
        End If
        If pidClasificacion > 0 Then
            Comm.CommandText += " and idclasificacion=" + pidClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + pidClasificacion3.ToString
        End If
        If psinExistencia Then
            Comm.CommandText += " and spdainventario(idinventario," + pIdAlmacen.ToString + "," + pidSucursal.ToString + ")<>0"
        End If
        If pDescon = 0 Then
            Comm.CommandText += " and descontinuado=0"
        End If
        If ordenanombre Then
            Comm.CommandText += " order by tblinventario.nombre"
        Else
            Comm.CommandText += " order by tblinventario.clave"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventario")
        DS.WriteXmlSchema("tblinventario.xml")
        Return DS.Tables("tblinventario").DefaultView
    End Function
    Public Function ReporteAnalisis(ByVal pIdAlmacen As Integer, ByVal pIdInventario As Integer, ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer) As DataView
        Dim DS As New DataSet
        Dim P1 As New MySql.Data.MySqlClient.MySqlParameter
        If pIdAlmacen < 0 Then pIdAlmacen = 0
        P1.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Int32
        P1.ParameterName = "pidinventario"
        P1.Value = pIdInventario
        Dim P2 As New MySql.Data.MySqlClient.MySqlParameter
        P2.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Int32
        P2.ParameterName = "pidalmacen"
        P2.Value = pIdAlmacen
        Dim P3 As New MySql.Data.MySqlClient.MySqlParameter
        P3.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.VarChar
        P3.ParameterName = "pfecha1"
        P3.Value = pFecha1
        Dim P4 As New MySql.Data.MySqlClient.MySqlParameter
        P4.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.VarChar
        P4.ParameterName = "pfecha2"
        P4.Value = pFecha2
        Comm.CommandType = CommandType.StoredProcedure
        Comm.Parameters.Clear()
        Comm.Parameters.Add(P1)
        Comm.Parameters.Add(P3)
        Comm.Parameters.Add(P4)
        Comm.Parameters.Add(P2)
        Comm.CommandText = "spanalisisinventario"
        Comm.ExecuteNonQuery()
        Comm.CommandType = CommandType.Text
        If pIdAlmacen = 0 Then
            Comm.CommandText = "select tblinventariomovimientos.idinventario,tblinventario.clave,tblinventario.nombre,tblinventariomovimientos.compras,tblinventariomovimientos.comprasdev,tblinventariomovimientos.ventas,tblinventariomovimientos.ventasdev,tblinventariomovimientos.entradas,tblinventariomovimientos.salidas,tblinventariomovimientos.traspasos,tblinventariomovimientos.recepciones,tblinventariomovimientos.ajustes,tblinventariomovimientos.fechaajuste," + _
            "tblinventariomovimientos.compras-tblinventariomovimientos.comprasdev+tblinventariomovimientos.entradas-tblinventariomovimientos.ventas+tblinventariomovimientos.ventasdev-tblinventariomovimientos.salidas as sugerido" + _
            ",spdainventario(tblinventariomovimientos.idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ") as invactual from tblinventariomovimientos inner join tblinventario on tblinventariomovimientos.idinventario=tblinventario.idinventario "
        Else
            Comm.CommandText = "select tblinventariomovimientos.idinventario,tblinventario.clave,tblinventario.nombre,tblinventariomovimientos.compras,tblinventariomovimientos.comprasdev,tblinventariomovimientos.ventas,tblinventariomovimientos.ventasdev,tblinventariomovimientos.entradas,tblinventariomovimientos.salidas,tblinventariomovimientos.traspasos,tblinventariomovimientos.recepciones,tblinventariomovimientos.ajustes,tblinventariomovimientos.fechaajuste," + _
            "tblinventariomovimientos.compras-tblinventariomovimientos.comprasdev+tblinventariomovimientos.entradas-tblinventariomovimientos.ventas+tblinventariomovimientos.ventasdev-tblinventariomovimientos.salidas-tblinventariomovimientos.traspasos+tblinventariomovimientos.recepciones as sugerido" + _
            ",spdainventario(tblinventariomovimientos.idinventario," + pIdAlmacen.ToString + "," + pIdSucursal.ToString + ") as invactual from tblinventariomovimientos inner join tblinventario on tblinventariomovimientos.idinventario=tblinventario.idinventario "
        End If
        If pIdInventario > 0 Then
            Comm.CommandText += " where tblinventariomovimientos.idinventario=" + pIdInventario.ToString
        End If
        Comm.CommandText += " order by clave,nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventariomovimientos")
        'DS.WriteXmlSchema("tblinventariomovimientos.xml")
        Return DS.Tables("tblinventariomovimientos").DefaultView

    End Function
    Public Function ReporteAnalisisb(ByVal pIdAlmacen As Integer, ByVal pIdInventario As Integer, ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdClase1 As Integer, ByVal pidClase2 As Integer, ByVal pIdClase3 As Integer, pDescon As Byte) As DataView
        Dim DS As New DataSet
        Dim P1 As New MySql.Data.MySqlClient.MySqlParameter
        If pIdAlmacen < 0 Then pIdAlmacen = 0
        P1.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Int32
        P1.ParameterName = "pidinventario"
        P1.Value = pIdInventario
        Dim P2 As New MySql.Data.MySqlClient.MySqlParameter
        P2.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Int32
        P2.ParameterName = "pidalmacen"
        P2.Value = pIdAlmacen
        Dim P3 As New MySql.Data.MySqlClient.MySqlParameter
        P3.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.VarChar
        P3.ParameterName = "pfecha1"
        P3.Value = pFecha1
        Dim P4 As New MySql.Data.MySqlClient.MySqlParameter
        P4.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.VarChar
        P4.ParameterName = "pfecha2"
        P4.Value = pFecha2
        Dim P5 As New MySql.Data.MySqlClient.MySqlParameter
        P5.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Int32
        P5.ParameterName = "pidsucursal"
        P5.Value = pIdSucursal
        Dim P6 As New MySql.Data.MySqlClient.MySqlParameter
        P6.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.VarChar
        P6.ParameterName = "pfecha3"
        P6.Value = Format(DateAdd(DateInterval.Day, 1, CDate(pFecha2)), "yyyy/MM/dd")

        Comm.CommandType = CommandType.StoredProcedure
        Comm.Parameters.Clear()
        Comm.Parameters.Add(P1)
        Comm.Parameters.Add(P3)
        Comm.Parameters.Add(P4)
        Comm.Parameters.Add(P2)
        Comm.Parameters.Add(P5)
        Comm.Parameters.Add(P6)
        If pIdClase1 > 0 Or pidClase2 > 0 Or pIdClase3 > 0 Then
            Dim P7 As New MySql.Data.MySqlClient.MySqlParameter
            P7.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Int32
            P7.ParameterName = "pidclase1"
            P7.Value = pIdClase1
            Dim P8 As New MySql.Data.MySqlClient.MySqlParameter
            P8.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Int32
            P8.ParameterName = "pidclase2"
            P8.Value = pidClase2
            Dim P9 As New MySql.Data.MySqlClient.MySqlParameter
            P9.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Int32
            P9.ParameterName = "pidclase3"
            P9.Value = pIdClase3
            Comm.Parameters.Add(P7)
            Comm.Parameters.Add(P8)
            Comm.Parameters.Add(P9)
            Comm.CommandText = "spanalisisinventarioclases"
        Else
            Comm.CommandText = "spanalisisinventariob"
        End If
        Comm.CommandTimeout = 10000
        Comm.ExecuteNonQuery()
        Comm.CommandType = CommandType.Text
        Comm.CommandText = "select tblinventariomovimientosb.*,tblinventario.clave,tblinventario.nombre from tblinventariomovimientosb inner join tblinventario on tblinventariomovimientosb.idinventario=tblinventario.idinventario where tblinventario.inventariable=1"
        'If pDescon = 0 Then
        '    Comm.CommandText += " and tblinventario.descontinuado=0"
        'End If
        'If pIdInventario > 0 Then
        '    Comm.CommandText += " where tblinventariomovimientos.idinventario=" + pIdInventario.ToString
        'End If
        Comm.CommandText += " order by clave,nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventariomovimientosb")
        'DS.WriteXmlSchema("tblinventariomovimientosb.xml")
        Return DS.Tables("tblinventariomovimientosb").DefaultView
    End Function

    Public Sub DaUltimaidMoneda(ByVal pIdInventario As Integer)
        'Comm.CommandText = "select ifnull((select if(tblcomprasdetalles.idmoneda=2,(precio+tblcomprasdetalles.costoindirecto)/cantidad,((precio+tblcomprasdetalles.costoindirecto)/cantidad)*tblcompras.tipodecambio) from tblcompras inner join tblcomprasdetalles on tblcompras.idcompra=tblcomprasdetalles.idcompra where tblcomprasdetalles.idinventario=" + pIdInventario.ToString + " order by fecha desc,hora desc limit 1),0)"
        'UltimoPrecioCompra = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select tblcomprasdetalles.idmoneda from tblcompras inner join tblcomprasdetalles on tblcompras.idcompra=tblcomprasdetalles.idcompra where tblcomprasdetalles.idinventario=" + pIdInventario.ToString + " order by fecha desc,hora desc limit 1),0)"
        idMonedaCompra = Comm.ExecuteScalar
    End Sub
    Public Sub ConsultaKardex(ByVal pIDInventario As Integer, ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIDalmacen As Integer)
        'Dim S As New DataSet
        Comm.CommandTimeout = 10000
        Comm.CommandText = "select spinventariomovimientosk(" + pIDInventario.ToString + ",'" + pFecha1 + "','" + pFecha2 + "'," + pIdSucursal.ToString + "," + pIDalmacen.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "delete from tblinventariomovimientosk2 where idinventario=" + pIDInventario.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblinventariomovimientosk2(fecha,hora,movimiento,tipomovimiento,estado,serie,folio,entrada,salida,existencia,costo,costoe,costou,almaceno,almacend,idinventario,iddocumento) select fecha,hora," + _
       "case tipomovimiento when 0 then 'I. Inicial' when 1 then 'Entrada' when 2 then 'Salida' when 3 then 'Traspaso' when 4 then 'Recepción' when 5 then 'Compra' when 6 then 'Dev. Compra' when 7 then 'Rem. Compra' when 8 then 'Venta' when 9 then 'Dev. Venta' when 10 then 'Rem. Venta' when 11 then 'Inv. Físico' when 12 then 'Apartado' when 13 then 'Salida F' when 14 then 'Traspaso F' when 15 then 'Recepción F' end as Movimiento,tipomovimiento," + _
       "if(estado=3,'A','C') as Estado,concat(serie,convert(folio using utf8)),concat(tiporef,folioref),entrada,salida,0.0 as existencia,costo,0.0 as costoe,0.0 as costou,(select nombre from tblalmacenes where tblalmacenes.idalmacen=idalmacen1) as AlmacenO,ifnull((select nombre from tblalmacenes where tblalmacenes.idalmacen=idalmacen2),'') as AlmacenD,idinventario,iddocumento from tblinventariomovimientosk where idinventario=" + pIDInventario.ToString
        Comm.CommandText += " order by fecha,tipomovimiento,hora,estado"
        Comm.ExecuteNonQuery()


        'Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        'DA.Fill(DS, "tblinventariok")
        'DS.WriteXmlSchema("tblinventariok.xml")
        'Return DS.Tables("tblinventariok").DefaultView
    End Sub
    Public Function ConsultaKardex2(ByVal pIdInventario As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = " select fecha,hora,movimiento,tipomovimiento,iddocumento,estado,serie,folio,entrada,salida,existencia,costo,costoe,costou,almaceno,almacend,id," + _
        "case tipomovimiento when 0 then 0 when 1 then 1 when 2 then 3 when 3 then 3 when 4 then 2 when 5 then 2 when 6 then 2 when 7 then 2 when 8 then 3 when 9 then 3 when 10 then 3 when 11 then 3 when 12 then 3 when 13 then 3 when 14 then 3 when 15 then 2  end as movimientop" + _
        " from tblinventariomovimientosk2 where idinventario=" + pIdInventario.ToString + " order by fecha,movimientop,hora"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventariok2")
        'DS.WriteXmlSchema("tblinventariok2.xml")
        Return DS.Tables("tblinventariok2").DefaultView
    End Function

    Public Function DaInventarioAFecha(ByVal pIdInventario As Integer, ByVal pFecha As String, ByVal pidsucursal As Integer, ByVal pidalmacen As Integer) As Double
        Comm.CommandText = "select spdainventarioafecha(" + pIdInventario.ToString + ",'" + pFecha + "'," + pidsucursal.ToString + "," + pidalmacen.ToString + ")"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaCostoAFecha(ByVal pIdInventario As Integer, ByVal pFecha As String) As Double
        'Comm.CommandText = "select spsacacostoarticuloafecha(" + pIdInventario.ToString + ",'" + pFecha + "')"
        Comm.CommandText = "select ifnull((select costo from tblinventariocostoh where idinventario=" + pIdInventario.ToString + " and fecha<'" + pFecha + "' order by fecha desc limit 1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaCostoAFechaC(ByVal pIdInventario As Integer, ByVal pFecha As String) As Double
        Comm.CommandText = "select spsacacostoarticuloafechac(" + pIdInventario.ToString + ",'" + pFecha + "')"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaCostoAFechaP(ByVal pIdInventario As Integer, ByVal pFecha As String) As Double
        Comm.CommandText = "select spsacacostoarticuloafechap(" + pIdInventario.ToString + ",'" + pFecha + "')"
        Return Comm.ExecuteScalar
    End Function
    Public Sub LlenaTablaConciliacion(ByVal pFecha As String, ByVal pidSucursal As Integer, ByVal pIdAlmacen As Integer, ByVal pidLista As Integer, pSoloExistencia As Boolean)
        Comm.CommandText = "delete from tblinventarioconciliaciones where fecha='" + pFecha + "' and idsucursal=" + pidSucursal.ToString + " and idalmacen=" + pIdAlmacen.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblinventarioconciliaciones(fecha,idsucursal,idalmacen,idinventario,invactual,existencia,diferencia,precio,importedif) select '" + pFecha + "'," + pidSucursal.ToString + "," + pIdAlmacen.ToString + ",i.idinventario,spdainventarioafecha(idinventario,'" + pFecha + "'," + pidSucursal.ToString + "," + pIdAlmacen.ToString + "),spdainventarioafecha(idinventario,'" + pFecha + "'," + pidSucursal.ToString + "," + pIdAlmacen.ToString + "),0,ifnull((select precio from tblinventarioprecios where idinventario=i.idinventario and idlista=" + pidLista.ToString + " limit 1),0),0  from tblinventario as i where i.idinventario>1"
        If pSoloExistencia Then
            Comm.CommandText += " and spdainventario(idinventario," + pIdAlmacen.ToString + "," + pidSucursal.ToString + ")<>0"
        End If
        Comm.ExecuteNonQuery()
    End Sub
    Public Function conciliarInventario(ByVal pIdAlmacen As Integer, ByVal pIdInventario As Integer, ByVal pFecha As String, ByVal pIdSucursal As Integer) As DataSet
        Dim DS As New DataSet
        Comm.CommandType = CommandType.Text
        Comm.CommandTimeout = 10000
        Comm.CommandText = "select ic.fecha,ic.idsucursal,ic.idalmacen,ic.idinventario,i.clave,i.nombre,ic.invactual,ic.existencia,ic.diferencia,ic.precio,ic.importedif from tblinventarioconciliaciones as ic inner join tblinventario as i on ic.idinventario=i.idinventario where ic.fecha='" + pFecha + "' and ic.idsucursal=" + pIdSucursal.ToString + " and ic.idalmacen=" + pIdAlmacen.ToString + " order by i.clave,i.nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblconciliarinventario")
        Return DS
    End Function

    Public Function ReporteAnalisisC(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdInventario As Integer, ByVal pidSucursal As Integer, ByVal pidAlmacen As Integer, pDescon As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select spanalisisinventariocc('" + pFecha1 + "','" + pFecha2 + "'," + pIdInventario.ToString + "," + pidSucursal.ToString + "," + pidAlmacen.ToString + ")"
        Comm.CommandTimeout = 10000
        Comm.ExecuteNonQuery()
        Comm.CommandTimeout = 10000
        Comm.CommandText = "select idinventario,tipo,concepto,cantidad,precio,orden from tblinventarioanalisisc order by tipo,orden"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioac")
        'DS.WriteXmlSchema("tblinventarioac.xml")
        Return DS.Tables("tblinventarioac").DefaultView
    End Function
    Public Sub ReCalculaCostos(ByVal pIdInventario As Integer, ByVal pTipoCosteo As Byte)
        Comm.CommandTimeout = 10000
        Comm.CommandText = "select sprecalculacostos('2000/01/01'," + pIdInventario.ToString + "," + pTipoCosteo.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblinventario set costobase=ifnull((select costo from tblinventariocostoh where idinventario=tblinventario.idinventario and fecha<='" + Format(Date.Now, "yyyy/MM/dd") + "' order by fecha desc limit 1),0)"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ReCalculaCostosB(ByVal pIdInventario As Integer, ByVal pTipoCosteo As Byte, ByVal pFecha As String)
        Comm.CommandTimeout = 10000
        Comm.CommandText = "select sprecalculacostos('" + pFecha + "'," + pIdInventario.ToString + "," + pTipoCosteo.ToString + ")"
        Comm.ExecuteNonQuery()
        If pIdInventario = 0 Then
            Comm.CommandText = "update tblinventario set costobase=ifnull((select costo from tblinventariocostoh where idinventario=tblinventario.idinventario and fecha<='" + Format(Date.Now, "yyyy/MM/dd") + "' order by fecha desc limit 1),0)"
            Comm.ExecuteNonQuery()
        Else
            Comm.CommandText = "update tblinventario set costobase=ifnull((select costo from tblinventariocostoh where idinventario=tblinventario.idinventario and fecha<='" + Format(Date.Now, "yyyy/MM/dd") + "' order by fecha desc limit 1),0) where idinventario=" + pIdInventario.ToString
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub RecalculaInventario(ByVal pIdinventario As Integer)
        Comm.CommandTimeout = 10000
        Comm.CommandText = "select spchecaalmacenes(tblinventario.idinventario,tblalmacenes.idalmacen) from tblalmacenes,tblinventario where tblalmacenes.idalmacen>1 and tblinventario.idinventario=" + pIdinventario.ToString + ";"
        Comm.CommandText += "select spchecaalmaceneslotes(tblinventariolotes.idinventario,tblalmacenes.idalmacen,tblinventariolotes.idlote) from tblalmacenes,tblinventariolotes where tblalmacenes.idalmacen>1 and tblinventariolotes.idinventario=" + pIdinventario.ToString + ";"
        Comm.CommandText += "select spchecaalmacenesaduanas(tblinventarioaduana.idinventario,tblalmacenes.idalmacen,tblinventarioaduana.idaduana) from tblalmacenes,tblinventarioaduana where tblalmacenes.idalmacen>1 and tblinventarioaduana.idinventario=" + pIdinventario.ToString + ";"
        Comm.CommandText += "update tblalmacenesi inner join tblalmacenes on tblalmacenesi.idalmacen=tblalmacenes.idalmacen set cantidad=spdainventarioafecha(idinventario,'2100/01/01',idsucursal,tblalmacenesi.idalmacen) where idinventario=" + pIdinventario.ToString + ";"
        Comm.CommandText += "update tblalmacenesilotes inner join tblalmacenes on tblalmacenesilotes.idalmacen=tblalmacenes.idalmacen set cantidad=spdainventarioafechalotes(idlote,idinventario,'2100/01/01',idsucursal,tblalmacenesilotes.idalmacen) where idinventario=" + pIdinventario.ToString + ";"
        Comm.CommandText += "update tblalmacenesiaduanas inner join tblalmacenes on tblalmacenesiaduanas.idalmacen=tblalmacenes.idalmacen set cantidad=spdainventarioafechaaduana(idaduana,idinventario,'2100/01/01',idsucursal,tblalmacenesiaduanas.idalmacen) where idinventario=" + pIdinventario.ToString + ";"
        Comm.CommandText += "update tblalmacenesiubicaciones inner join tblalmacenes on tblalmacenesiubicaciones.idalmacen=tblalmacenes.idalmacen set cantidad=spdainventarioafechaubicacion(ubicacion,idinventario,'2100/01/01',idsucursal,tblalmacenesiubicaciones.idalmacen) where idinventario=" + pIdinventario.ToString + ";"
        Comm.ExecuteNonQuery()

    End Sub
    Public Function DaArticuloNoInventariable() As Integer
        Comm.CommandText = "select ifnull((select idinventario from tblinventario where inventariable=0 and precioneto=1 limit 1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function ConsultaIds(ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidInventario As Integer, ByVal pExtraCampos As Boolean) As MySql.Data.MySqlClient.MySqlDataReader
        'Dim DS As New DataSet
        If pExtraCampos Then
            Comm.CommandText = "select idinventario,nombre,clave from tblinventario where idinventario>1 and inventariable=1 "
        Else
            Comm.CommandText = "select idinventario from tblinventario where idinventario>1 and inventariable=1 "
        End If

        If pidInventario <> 0 Then
            Comm.CommandText += " and idinventario=" + pidInventario.ToString
        End If
        If pidClasificacion > 0 Then
            Comm.CommandText += " and idclasificacion=" + pidClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + pidClasificacion3.ToString
        End If

        'Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        'DA.Fill(DS, "tblinventario")
        Return Comm.ExecuteReader
    End Function
    Public Function DaUltimoCosto(ByVal pIdInventario As Integer) As Double
        Comm.CommandText = "select spdaultimocostoinv(" + pIdInventario.ToString + ")"
        UltimoPrecioCompra = Comm.ExecuteScalar
        Return UltimoPrecioCompra
    End Function
    Public Sub AjustaCostoSalidas(ByVal pidInventario As Integer, ByVal pFecha As String)
        Comm.CommandText = "update tblmovimientosdetalles md inner join tblmovimientos m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos ic on m.idconcepto=ic.idconcepto set precio=ifnull((select costo from tblinventariocostoh where idinventario=md.idinventario and fecha<=m.fecha order by fecha desc limit 1),0)*cantidad where (ic.tipo=1 or ic.tipo=3) and m.fecha>='" + pFecha + "' and md.idinventario=" + pidInventario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaInventarioApartado(ByVal pIdInventario As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(cantidad) from tblventasapartadosdetalles inner join tblventasapartados on tblventasapartadosdetalles.idapartado=tblventasapartados.idapartado where tblventasapartados.surtido=0 and tblventasapartadosdetalles.idinventario=" + pIdInventario.ToString + " and tblventasapartados.estado=3 and tblventasapartados.dinventario=1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Sub AgregaPreciosFromTemp(ByVal pidInventario As Integer)
        Comm.CommandText = "insert into tblinventarioprecios(idinventario,precio,comentario,idmoneda,idlista,utilidad,descuentoprecio) select " + pidInventario.ToString + ",precio,comentario,idmoneda,idlista,utilidad,descuentoprecio from tblinventariopreciostemp"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta2(ByVal pIdAlmacen As Integer, Optional ByVal pNombre As String = "", Optional ByVal pClave As String = "", Optional ByVal pidClasificacion As Integer = -1, Optional ByVal ClaveyNombre As Boolean = False, Optional ByVal pidClasificacion2 As Integer = 0, Optional ByVal pidClasificacion3 As Integer = 0, Optional ByVal pClave2 As String = "", Optional ByVal pInventariable As Byte = 0, Optional ByVal pFabricante As Boolean = False, Optional ByVal pModoB As Byte = 0, Optional ByVal pEskit As Byte = 0) As DataView
        Dim DS As New DataSet
        Dim Palabras() As String
        If pModoB = 1 Then
            Palabras = pNombre.Split(Chr(32))
        Else
            ReDim Palabras(1)
            Palabras(0) = pNombre
        End If
        'If pFabricante = False Then
        Comm.CommandText = "select 1 as selec, idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + ",0) from tblinventario where idinventario>1" 'and concat(clave,clave2,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        'Else
        'Comm.CommandText = "select idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + ",0) from tblinventario where idinventario>1 and concat(clave,clave2,nombre,fabricante) like '%" + Replace(pNombre, "'", "''") + "%'"
        'End If
        For Each s As String In Palabras
            If pFabricante = True Then
                Comm.CommandText += " and concat(clave,clave2,nombre,fabricante) like '%" + Replace(s, "'", "''") + "%'"
            Else
                Comm.CommandText += " and concat(clave,clave2,nombre) like '%" + Replace(s, "'", "''") + "%'"
            End If
        Next

        If pidClasificacion > 0 Then
            Comm.CommandText += " and idclasificacion=" + pidClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pInventariable = 1 Then
            Comm.CommandText += " and inventariable=1"
        End If
        If pEskit = 1 Then
            Comm.CommandText += " and eskit=0"
        End If
        Comm.CommandText += " order by clave,nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventario")
        Return DS.Tables("tblinventario").DefaultView
    End Function
    Public Sub ModificarImpuesto(ByVal pID As Integer, ByVal pImpuesto As Double)

        Comm.CommandText = "update tblinventario set iva=" + pImpuesto.ToString + " where idinventario=" + pID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarIEPS(ByVal pID As Integer, ByVal pIEPS As Double)

        Comm.CommandText = "update tblinventario set ieps=" + pIEPS.ToString() + " where idinventario=" + pID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarIVARetenido(ByVal pID As Integer, ByVal pRetenido As Double)

        Comm.CommandText = "update tblinventario set ivaRetenido=" + ivaRetenido.ToString() + " where idinventario=" + pID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarFabricante(ByVal pID As Integer, ByVal pFabricante As String)

        Comm.CommandText = "update tblinventario set fabricante='" + Replace(Trim(pFabricante), "'", "''") + "' where idinventario=" + pID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarUbicacion(ByVal pID As Integer, ByVal pUbicacion As String)

        Comm.CommandText = "update tblinventario set ubicacion='" + Replace(pUbicacion, "'", "''") + "' where idinventario=" + pID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarCampo(ByVal pID As Integer, ByVal pCampo As String, pTipo As Byte, pValor As String)
        Comm.CommandText = "update tblinventario set "
        If pTipo = 0 Then
            Comm.CommandText += pCampo + "=" + Replace(pValor, "'", "''")
        Else
            Comm.CommandText += pCampo + "='" + Replace(pValor, "'", "''") + "'"
        End If
        Comm.CommandText += " where idinventario=" + pID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarClasificacion(ByVal pID As Integer, ByVal pNivel1 As Integer, ByVal pNivel2 As Integer, ByVal pNivel3 As Integer)

        Comm.CommandText = "update tblinventario set idclasificacion=" + pNivel1.ToString + " ,idclasificacion2= " + pNivel2.ToString + " ,idclasificacion3=" + pNivel3.ToString + " where idinventario=" + pID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarPrecioNeto(ByVal pID As Integer, ByVal pPrecioNeto As Integer)

        Comm.CommandText = "update tblinventario set precioneto=" + pPrecioNeto.ToString + " where idinventario=" + pID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    ' Comm.CommandText = "update tblinventario set nombre='" + Replace(Nombre, "'", "''") + "',contenido=" + Contenido.ToString + ",tipocontenido=" + pIdTipoContenido.ToString + ",tipocantidad=" + pidtipoCantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',clave='" + Replace(Clave, "'", "''") + "',idclasificacion=" + pIdClasificacion.ToString + ",puntodereorden=" + PuntodeReorden.ToString + ",idmonedacostobase=" + pIdMonedaCostoBase.ToString + ",noparte='" + Replace(NoParte, "'", "''") + "',idclasificacion2=" + pidClasificacion2.ToString + ",idclasificacion3=" + pidClasificacion3.ToString + ",manejaseries=" + ManejaSeries.ToString + ",inventariable=" + Inventariable.ToString + ",retieneiva=" + RetieneIva.ToString + ",iva=" + Iva.ToString + ",clave2='" + Replace(Clave2, "'", "''") + "',fabricante='" + Replace(Trim(Fabricante), "'", "''") + "',precioneto=" + PrecioNeto.ToString + ",ubicacion='" + Replace(Ubicacion, "'", "''") + "',usaformula=" + UsaFormula.ToString + ",esamortizacion=" + EsAmortizacion.ToString + ",peso=" + Peso.ToString + ",maximo=" + Maximo.ToString + ",minimo=" + Minimo.ToString + ",eskit=" + EsKit.ToString + ",separarkit=" + SepararKit.ToString + ",porlotes=" + PorLotes.ToString + ",ieps=" + ieps.ToString() + ",ivaRetenido=" + ivaRetenido.ToString() + " where idinventario=" + ID.ToString
    'Public Sub LlenaDatos(ByVal pID As Integer)
    '    Dim idClas As Integer
    '    Dim IdtipoCant As Integer
    '    Dim IdTipoCont As Integer
    '    Dim IdMonedaBase As Integer
    '    Dim idClas2 As Integer
    '    Dim idClas3 As Integer
    '    Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
    '    ID = pID
    '    Comm.CommandText = "select * from tblinventario where idinventario=" + pID.ToString
    '    DReader = Comm.ExecuteReader
    '    If DReader.Read() Then
    '        Nombre = DReader("nombre")
    '        Cantidad = DReader("cantidad")
    '        Contenido = DReader("contenido")
    '        Clave = DReader("clave")
    '        Descripcion = DReader("descripcion")
    '        idClas = DReader("idclasificacion")
    '        IdtipoCant = DReader("tipocantidad")
    '        IdTipoCont = DReader("tipocontenido")
    '        PuntodeReorden = DReader("puntodereorden")
    '        IdMonedaBase = DReader("idmonedacostobase")
    '        CostoBase = DReader("costobase")
    '        NoParte = DReader("noparte")
    '        idClas2 = DReader("idclasificacion2")
    '        idClas3 = DReader("idclasificacion3")
    '        ManejaSeries = DReader("manejaseries")
    '        Inventariable = DReader("inventariable")
    '        Iva = DReader("iva")
    '        RetieneIva = DReader("retieneiva")
    '        Clave2 = DReader("clave2")
    '        Fabricante = DReader("fabricante")
    '        PrecioNeto = DReader("precioneto")
    '        Ubicacion = DReader("ubicacion")
    '        UsaFormula = DReader("usaformula")
    '        EsAmortizacion = DReader("esamortizacion")
    '        Peso = DReader("peso")
    '        Maximo = DReader("maximo")
    '        Minimo = DReader("minimo")
    '        EsKit = DReader("eskit")
    '        SepararKit = DReader("separarkit")
    '        PorLotes = DReader("porlotes")
    '        ieps = DReader("ieps")
    '        ivaRetenido = DReader("ivaRetenido")
    '        'zona = DReader("zona")
    '        urlImagen = DReader("urlImagen")
    '    End If
    '    DReader.Close()

    'End Sub
    Public Function buscarNoInventariable() As Integer
        Comm.CommandText = "select idinventario from tblinventario where inventariable=0 and idinventario<>1"
        Return Comm.ExecuteScalar
    End Function

    Public Function Consulta22(ByVal pIdAlmacen As Integer, Optional ByVal pNombre As String = "", Optional ByVal pClave As String = "", Optional ByVal pidClasificacion As Integer = -1, Optional ByVal ClaveyNombre As Boolean = False, Optional ByVal pidClasificacion2 As Integer = 0, Optional ByVal pidClasificacion3 As Integer = 0, Optional ByVal pClave2 As String = "", Optional ByVal pInventariable As Byte = 0, Optional ByVal pFabricante As Boolean = False, Optional ByVal pModoB As Byte = 0, Optional ByVal pEskit As Byte = 0) As DataView
        Dim DS As New DataSet
        Dim Palabras() As String
        If pModoB = 1 Then
            Palabras = pNombre.Split(Chr(32))
        Else
            ReDim Palabras(1)
            Palabras(0) = pNombre
        End If
        'If pFabricante = False Then
        Comm.CommandText = "select 0 as selec, idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + ",0) from tblinventario where idinventario>1" 'and concat(clave,clave2,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        'Else
        'Comm.CommandText = "select idinventario,clave,nombre,spdainventario(idinventario," + pIdAlmacen.ToString + ",0) from tblinventario where idinventario>1 and concat(clave,clave2,nombre,fabricante) like '%" + Replace(pNombre, "'", "''") + "%'"
        'End If
        For Each s As String In Palabras
            If pFabricante = True Then
                Comm.CommandText += " and concat(clave,clave2,nombre,fabricante) like '%" + Replace(s, "'", "''") + "%'"
            Else
                Comm.CommandText += " and concat(clave,clave2,nombre) like '%" + Replace(s, "'", "''") + "%'"
            End If
        Next

        If pidClasificacion > 0 Then
            Comm.CommandText += " and idclasificacion=" + pidClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pInventariable = 1 Then
            Comm.CommandText += " and inventariable=1"
        End If
        If pEskit = 1 Then
            Comm.CommandText += " and eskit=0"
        End If
        Comm.CommandText += " order by clave,nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventario")
        Return DS.Tables("tblinventario").DefaultView
    End Function
    Public Sub ModificarCodigo2(ByVal pID As Integer, ByVal pCodigo2 As String)

        Clave2 = pCodigo2

        Comm.CommandText = "update tblinventario set clave2='" + Replace(Clave2, "'", "''") + "' where idinventario=" + pID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub

    Public Function DaMaximo(ByVal pSinFormato As Boolean) As String
        Dim Maximo As Integer
        Dim ClaveaChecar As String
        Comm.CommandText = "select ifnull((select count(clave) from tblinventario where idinventario>1),0)"
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
        Return Format(Maximo, "0000")
    End Function

    Public Sub reporteInventarioLotes(ByVal idInventario As Integer, ByVal idClas As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer, ByVal existencia As Boolean)
        Dim ds As New DataSet
        Dim clasificacion As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        Dim cla As String = ""
        Comm.CommandText = "select i.clave,i.nombre,l.lote,l.fechacaducidad,l.cantidad from tblinventario as i inner join tblinventariolotes as l on i.idinventario=l.idinventario"
        If existencia Then
            Comm.CommandText += " where l.cantidad>0 "
        Else
            Comm.CommandText += " where l.cantidad>=0 "
        End If
        If idInventario > 0 Then
            Comm.CommandText += " and i.idinventario=" + idInventario.ToString()
        End If
        If idClas > 0 Then
            Comm.CommandText += " and i.idclasificacion=" + idClas.ToString()
            clasificacion = New dbInventarioClasificaciones(idClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            cla += clasificacion.Nombre + " - "
        End If
        If idClas2 > 0 Then
            Comm.CommandText += " and i.idclasificacion2=" + idClas2.ToString()
            clasificacion = New dbInventarioClasificaciones(idClas2, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            cla += clasificacion.Nombre + " - "
        End If
        If idClas3 > 0 Then
            Comm.CommandText += " and i.idclasificacion3=" + idClas3.ToString()
            clasificacion = New dbInventarioClasificaciones(idClas3, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            cla += clasificacion.Nombre
        End If
        Comm.CommandText += " order by i.idinventario,l.lote"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tblinventario")
        'ds.WriteXmlSchema("tblInventarioLotes.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repInventarioLotes
        rep.SetDataSource(ds)
        rep.SetParameterValue("nombreEmpresa", GlobalNombreEmpresa)
        If cla <> "" Then
            rep.SetParameterValue("clasificacion", cla)
        Else
            rep.SetParameterValue("clasificacion", "Todas")
        End If
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub

    Public Sub reporteInventarioAduanas(ByVal idInventario As Integer, ByVal idClas As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer, ByVal existencia As Boolean)
        Dim ds As New DataSet
        Dim clasificacion As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        Dim cla As String = ""
        Comm.CommandText = "select i.clave,i.nombre,l.aduana,l.fecha,l.numero,l.cantidad from tblinventario as i inner join tblinventarioaduana as l on i.idinventario=l.idinventario"
        If existencia Then
            Comm.CommandText += " where l.cantidad>0 "
        Else
            Comm.CommandText += " where l.cantidad>=0 "
        End If
        If idInventario > 0 Then
            Comm.CommandText += " and i.idinventario=" + idInventario.ToString()
        End If
        If idClas > 0 Then
            Comm.CommandText += " and i.idclasificacion=" + idClas.ToString()
            clasificacion = New dbInventarioClasificaciones(idClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            cla += clasificacion.Nombre + " - "
        End If
        If idClas2 > 0 Then
            Comm.CommandText += " and i.idclasificacion2=" + idClas2.ToString()
            clasificacion = New dbInventarioClasificaciones(idClas2, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            cla += clasificacion.Nombre + " - "
        End If
        If idClas3 > 0 Then
            Comm.CommandText += " and i.idclasificacion3=" + idClas3.ToString()
            clasificacion = New dbInventarioClasificaciones(idClas3, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            cla += clasificacion.Nombre
        End If
        Comm.CommandText += " order by i.idinventario,l.aduana"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tblinventarioAduanas")
        'ds.WriteXmlSchema("tblInventarioAduanas1.xml")
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repInventarioAduanas
        rep.SetDataSource(ds)
        rep.SetParameterValue("nombreEmpresa", GlobalNombreEmpresa)
        If cla <> "" Then
            rep.SetParameterValue("clasificacion", cla)
        Else
            rep.SetParameterValue("clasificacion", "Todas")
        End If
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub
    Public Function DaIds(pTipo As Byte, pIdAlmacen As Integer, pIdinventario As Integer, pIdClasificacion As Integer, pIdClasificacion2 As Integer, pIdClasificacion3 As Integer, pFecha As String) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandTimeout = 10000
        If pTipo = 0 Then
            Comm.CommandText = "select nombre,idinventario,ifnull((select cantidad from tblalmacenesi where tblalmacenesi.idinventario=tblinventario.idinventario and idalmacen=" + pIdAlmacen.ToString + "),0) as cantidad,ifnull((select costo from tblinventariocostoh where idinventario=tblinventario.idinventario and fecha<='" + pFecha + "' order by fecha desc limit 1),0) as costo from tblinventario where inventariable=1 and ifnull((select cantidad from tblalmacenesi where tblalmacenesi.idinventario=tblinventario.idinventario and idalmacen=" + pIdAlmacen.ToString + "),0)>0"
        Else
            Comm.CommandText = "select nombre,idinventario,ifnull((select cantidad from tblalmacenesi where tblalmacenesi.idinventario=tblinventario.idinventario and idalmacen=" + pIdAlmacen.ToString + "),0) as cantidad,ifnull((select costo from tblinventariocostoh where idinventario=tblinventario.idinventario and fecha<='" + pFecha + "' order by fecha desc limit 1),0) as costo from tblinventario where inventariable=1 and ifnull((select cantidad from tblalmacenesi where tblalmacenesi.idinventario=tblinventario.idinventario and idalmacen=" + pIdAlmacen.ToString + "),0)<0"
        End If
        If pIdinventario > 0 Then
            Comm.CommandText += " and tblinventario.idinventario=" + pIdinventario.ToString
        End If
        If pIdClasificacion > 0 Then
            Comm.CommandText += " and idclasificacion=" + pIdClasificacion.ToString
        End If
        If pIdClasificacion2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + pIdClasificacion2.ToString
        End If
        If pIdClasificacion3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + pIdClasificacion3.ToString
        End If
        Return Comm.ExecuteReader
    End Function
    Public Function DaInventarioEnTransito(pIdInventario As Integer, pIdSucursal As Integer, pIdAlmacen As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(md.cantidad) from tblmovimientosdetalles md inner join tblmovimientos m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos ic on m.idconcepto=ic.idconcepto where ic.tipo=3 and m.transito=0 and m.estado=3 and md.idinventario=" + pIdInventario.ToString
        If pIdSucursal > 0 Then
            Comm.CommandText += " and m.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and md.idalmacen2=" + pIdAlmacen.ToString
        End If
        Comm.CommandText += "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function ReportePedidos(pFecha1 As String, pFecha2 As String, pIdSucursalB As Integer, pIdInventario As Integer, pIdClasificacion1 As Integer, pIdClasificacion2 As Integer, pIdClasificacion3 As Integer, pAutorizado As Boolean, pIdsucursalA As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select i.clave,i.nombre,p.fecha,pd.cantidadaut,pd.autorizado,p.serie,p.folio,s.nombre nombres,p.idsucursalb,(select s2.nombre from tblsucursales s2 where s2.idsucursal=p.idsucursalb) nombres2,ifnull((select sum(surtido) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idpedido=p.idpedido and tblmovimientos.estado=3 and tblmovimientosdetalles.idinventario=pd.idinventario),0) surtido" +
            " from tblinventariopedidos p inner join tblinventariopedidosdetalles pd on p.idpedido=pd.idpedido inner join tblinventario i on pd.idinventario=i.idinventario inner join tblsucursales s on p.idsucursala=s.idsucursal where p.fecha>='" + pFecha1 + "' and p.fecha<='" + pFecha2 + "' "
        If pIdInventario > 0 Then
            Comm.CommandText += " and pd.idinventario=" + pIdInventario.ToString
        End If
        If pIdSucursalB > 0 Then
            Comm.CommandText += " and p.idsucursalb=" + pIdSucursalB.ToString
        End If
        If pIdsucursalA > 0 Then
            Comm.CommandText += " and p.idsucursala=" + pIdsucursalA.ToString
        End If
        If pAutorizado Then
            Comm.CommandText += " and pd.autorizado=1"
        End If
        If pIdClasificacion1 > 0 Then
            Comm.CommandText += " and i.idclasificacion=" + pIdClasificacion1.ToString
        End If
        If pIdClasificacion2 > 0 Then
            Comm.CommandText += " and i.idclasificacion2=" + pIdClasificacion1.ToString
        End If
        If pIdClasificacion3 > 0 Then
            Comm.CommandText += " and i.idclasificacion3=" + pIdClasificacion1.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblreppedidos")
        DS.WriteXmlSchema("tblreppedidos.xml")
        Return DS.Tables("tblreppedidos").DefaultView
    End Function
    Public Function listaClaves(ByVal idClas1 As Integer, ByVal idClas2 As Integer, ByVal idClas3 As Integer) As List(Of String)
        Dim lista As New List(Of String)
        Comm.CommandText = "select clave from tblinventario "
        If idClas1 > 0 Or idClas2 > 0 Or idClas3 > 0 Then
            Comm.CommandText += "where"
        End If
        If idClas1 > 0 Then
            Comm.CommandText += " idclasificacion=" + idClas1.ToString
        End If
        If idClas2 > 0 Then
            Comm.CommandText += " and idclasificacion2=" + idClas2.ToString
        End If
        If idClas3 > 0 Then
            Comm.CommandText += " and idclasificacion3=" + idClas3.ToString
        End If
        Comm.CommandText += " and restaurante=1;"
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader = Comm.ExecuteReader
        While dr.Read()
            lista.Add(dr("clave"))
        End While
        dr.Close()
        Return lista
    End Function

    Public Function totalArticulos() As Integer
        Comm.CommandText = "select count(*) from tblinventario;"
        Dim i As Integer = Comm.ExecuteScalar
        Return i
    End Function

    Public Function Ubicaciones(idalmacen As Integer, idinventario As Integer) As DataTable
        Comm.CommandText = "select au.ubicacion, concat(au.ubicacion, ' (', ifnull(aiu.cantidad,0), ')') ubicacionc from tblalmacenesubicaciones au left outer join tblalmacenesiubicaciones aiu on au.idalmacen=aiu.idalmacen and au.ubicacion=aiu.ubicacion and aiu.idinventario=" + idinventario.ToString() + " where au.idalmacen=" + idalmacen.ToString() + " order by au.ubicacion;"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        Dim ds As New DataSet
        da.Fill(ds, "tabla")
        Return ds.Tables("tabla")
    End Function
End Class
