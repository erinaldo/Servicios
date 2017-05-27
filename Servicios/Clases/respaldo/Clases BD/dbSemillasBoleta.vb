Imports MySql.Data.MySqlClient

Public Class dbSemillasBoleta
#Region "atributos y propiedades"

    Public Property id As Integer = -1
    Public Property folio As Integer
    Public Property fecha As Date
    Public Property productor As dbproveedores
    Public Property chofer As String = ""
    Public Property producto As dbInventario
    Public Property peso As Double = 0
    Public Property humedad As Double = 0
    Public Property impurezas As Double = 0
    Public Property granoDanado As Double = 0
    Public Property granoQuebrado As Double = 0
    Public Property castigoHumedad As Double = 0
    Public Property castigoImpurezas As Double = 0
    Public Property castigoGranoDanado As Double = 0
    Public Property castigoGranoQuebrado As Double = 0
    Public Property castigoTotal As Double = 0
    Public Property pesoFinal As Double = 0
    Public Property hora As String = ""
    Public Property camion As String = ""
    Public Property placas As String = ""
    Public Property analista As String = ""
    Public Property pesador As String = ""
    Public Property descargo As String = ""
    Public Property portero As String = ""
    Public Property tipoBoleta As String = ""
    Public Property bodega As String = ""
    'Public Property destino As String = ""
    Public Property variedad As String = ""
    Public Property porcentajeHumedad As Double = 0
    Public Property porcentajeImpurezas As Double = 0
    Public Property porcentajeGranoQ As Double = 0
    Public Property porcentajeGranoD As Double = 0
    Public Property liquidada As Boolean = False
    Public Property importe As Double = 0
    Public IdDetalle As Integer = 0
    Public Property precio As Double = 0
    Public Property tara As Double = 0
    Public Property brutosinanalizar As Double = 0
    Public Property horaSalida As String = ""
    Public Property idCliente As Integer = -1

    Public Property serie As String = ""

#End Region
#Region "atributos"
    Dim conexion As MySqlConnection
    Dim ruta As String = " Server=localhost; user id=root; password=masterdb; database=db_services; port=3306;"
    Dim comm As MySqlCommand
    Dim INSERT As String = "insert into tblsemillasboletas(" +
                                "folio," +
                                "fecha," +
                                "productor," +
                                "chofer," +
                                "producto," +
                                "peso," +
                                "humedad," +
                                "impurezas," +
                                "granodanado," +
                                "granoquebrado," +
                                "castigoHumedad," +
                                "castigoImpurezas," +
                                "castigoGranoD," +
                                "castigoGranoQ," +
                                "castigoTotal," +
                                "pesoanalizado," +
                                "hora," +
                                "camion," +
                                "placas," +
                                "analista," +
                                "pesador," +
                                "descargo," +
                                "portero," +
                                "tipoBoleta," +
                                "bodega," +
                                "variedad," +
                                "porcentajeHumedad," +
                                "porcentajeImpurezas," +
                                "porcentajeGranoQ," +
                                "porcentajeGranoD," +
                                "liquidada," +
                                "importe,iddetalle,precio,tara,brutosinanalizar,horasalida,idCliente,serie) " +
                                "values"
    Dim UPDATE As String = "update tblsemillasboletas set "

#End Region
#Region "constructores"
    Public Sub New(ByVal pFolio As Integer,
                   ByVal pFecha As Date,
                   ByRef pProductor As dbproveedores,
                   ByVal pChofer As String,
                   ByRef pProducto As dbInventario,
                   ByVal pPeso As Double,
                   ByVal pHumedad As Double,
                   ByVal pImpurezas As Double,
                   ByVal pGranoDanado As Double,
                   ByVal pGRanoQuebrado As Double,
                   ByVal pCastigoHumedad As Double,
                   ByVal pCastigoImpurezas As Double,
                   ByVal pCastigoGranodanado As Double,
                   ByVal pCastigoGranoQuebrado As Double,
                   ByVal pCastigoTotal As Double,
                   ByVal pPesoFinal As Double,
                   ByVal pHora As String,
                   ByVal pCamion As String,
                   ByVal pPlacas As String,
                   ByVal pAnalista As String,
                   ByVal pPesador As String,
                   ByVal pDescargo As String,
                   ByVal pPortero As String,
                   ByVal pTipoBoleta As String,
                   ByVal pBodega As String,
                   ByVal pVariedad As String,
                   ByVal pPorcentajeHumedad As Double,
                   ByVal pPorcentajeImpurezas As Double,
                   ByVal pPorcentajeGranoQ As Double,
                   ByVal pPorcentajeGranoD As Double,
                   ByVal pLiquidada As Boolean,
                   ByVal pImporte As Double, pIdDEtalle As Integer,
                   ByVal pPrecio As Double, ByVal pTara As Double,
                   ByVal pBrutosinanalizar As Double, ByVal pHoraSalida As String, ByVal pIdCliente As Integer,
                   ByVal pSerie As String)
        folio = pFolio
        fecha = pFecha
        productor = pProductor
        chofer = pChofer
        producto = pProducto
        peso = pPeso
        humedad = pHumedad
        impurezas = pImpurezas
        granoDanado = pGranoDanado
        granoQuebrado = pGRanoQuebrado
        castigoHumedad = pCastigoHumedad
        castigoImpurezas = pCastigoImpurezas
        castigoGranoDanado = pCastigoGranodanado
        castigoGranoQuebrado = pCastigoGranoQuebrado
        castigoTotal = pCastigoTotal
        pesoFinal = pPesoFinal
        hora = pHora
        camion = pCamion
        placas = pPlacas
        analista = pAnalista
        pesador = pPesador
        descargo = pDescargo
        portero = pPortero
        tipoBoleta = pTipoBoleta
        bodega = pBodega
        variedad = pVariedad
        porcentajeHumedad = pPorcentajeHumedad
        porcentajeImpurezas = pPorcentajeImpurezas
        porcentajeGranoQ = pPorcentajeGranoQ
        porcentajeGranoD = pPorcentajeGranoD
        liquidada = pLiquidada
        importe = pImporte
        IdDetalle = pIdDEtalle
        precio = pPrecio
        tara = pTara
        brutosinanalizar = pBrutosinanalizar
        horaSalida = pHoraSalida
        idCliente = pIdCliente
        serie = pSerie
    End Sub
    Public Sub New()

    End Sub
    Public Sub New(ByVal pId As Integer,
               ByVal pFolio As Integer,
               ByVal pFecha As Date,
               ByRef pProductor As dbproveedores,
               ByVal pChofer As String,
               ByRef pProducto As dbInventario,
               ByVal pPeso As Double,
               ByVal pHumedad As Double,
               ByVal pImpurezas As Double,
               ByVal pGranoDanado As Double,
               ByVal pGRanoQuebrado As Double,
               ByVal pCastigoHumedad As Double,
               ByVal pCastigoImpurezas As Double,
               ByVal pCastigoGranodanado As Double,
               ByVal pCastigoGranoQuebrado As Double,
               ByVal pCastigoTotal As Double,
               ByVal pPesoFinal As Double,
               ByVal pHora As String,
               ByVal pCamion As String,
               ByVal pPlacas As String,
               ByVal pAnalista As String,
               ByVal pPesador As String,
               ByVal pDescargo As String,
               ByVal pPortero As String,
               ByVal pTipoBoleta As String,
               ByVal pBodega As String,
               ByVal pVariedad As String,
               ByVal pPorcentajeHumedad As Double,
               ByVal pPorcentajeImpurezas As Double,
               ByVal pPorcentajeGranoQ As Double,
               ByVal pPorcentajeGranoD As Double,
               ByVal pLiquidada As Integer,
               ByVal pImporte As Double, ByVal idDetalle As Integer, ByVal pIdcliente As Integer,
               ByVal pSerie As String)
        id = pId
        folio = pFolio
        fecha = pFecha
        productor = pProductor
        chofer = pChofer
        producto = pProducto
        peso = pPeso
        humedad = pHumedad
        impurezas = pImpurezas
        granoDanado = pGranoDanado
        granoQuebrado = pGRanoQuebrado
        castigoHumedad = pCastigoHumedad
        castigoImpurezas = pCastigoImpurezas
        castigoGranoDanado = pCastigoGranodanado
        castigoGranoQuebrado = pCastigoGranoQuebrado
        castigoTotal = pCastigoTotal
        pesoFinal = pPesoFinal
        hora = pHora
        camion = pCamion
        placas = pPlacas
        analista = pAnalista
        pesador = pPesador
        descargo = pDescargo
        portero = pPortero
        tipoBoleta = pTipoBoleta
        bodega = pBodega
        variedad = pVariedad
        porcentajeHumedad = pPorcentajeHumedad
        porcentajeImpurezas = pPorcentajeImpurezas
        porcentajeGranoQ = pPorcentajeGranoQ
        porcentajeGranoD = pPorcentajeGranoD
        liquidada = pLiquidada
        importe = pImporte
        idDetalle = idDetalle
        idCliente = pIdcliente
        serie = pSerie
    End Sub
    Public Sub New(ByVal pId As Integer)
        id = pId
    End Sub

    Public Sub New(ByVal pFolio As String)
        folio = pFolio
    End Sub
#End Region
    Public Sub New(ByVal conn As MySqlConnection)
        conexion = conn
        'conexion.Open()
        comm = New MySqlCommand
        comm.Connection = conexion
    End Sub
#Region "métodos"
    Public Sub agregar(ByRef boleta As dbSemillasBoleta)
        comm.CommandText = INSERT + "(" + boleta.folio.ToString +
                                        ",'" + boleta.fecha.ToString("yyyy/MM/dd") + "',"
        If boleta.productor Is Nothing Then
            comm.CommandText += "null"
        Else

            comm.CommandText += boleta.productor.ID.ToString()
        End If
        comm.CommandText += ",'" + boleta.chofer +
                                        "'," + boleta.producto.ID.ToString() +
                                        "," + boleta.peso.ToString() +
                                        "," + boleta.humedad.ToString() +
                                        "," + boleta.impurezas.ToString() +
                                        "," + boleta.granoDanado.ToString() +
                                        "," + boleta.granoQuebrado.ToString() +
                                        "," + boleta.castigoHumedad.ToString() +
                                        "," + boleta.castigoImpurezas.ToString() +
                                        "," + boleta.castigoGranoDanado.ToString() +
                                        "," + boleta.castigoGranoQuebrado.ToString() +
                                        "," + boleta.castigoTotal.ToString() +
                                        "," + boleta.pesoFinal.ToString() +
                                        ",'" + boleta.hora + "'" +
                                        ",'" + boleta.camion + "'" +
                                        ",'" + boleta.placas + "'" +
                                        ",'" + boleta.analista + "'" +
                                        ",'" + boleta.pesador + "'" +
                                        ",'" + boleta.descargo + "'" +
                                        ",'" + boleta.portero + "'" +
                                        ",'" + boleta.tipoBoleta + "'" +
                                        ",'" + boleta.bodega.Replace("'", "''") +
                                        "','" + boleta.variedad + "'" +
                                        "," + boleta.porcentajeHumedad.ToString() +
                                        "," + boleta.porcentajeImpurezas.ToString() +
                                        "," + boleta.porcentajeGranoQ.ToString() +
                                        "," + boleta.porcentajeGranoD.ToString() +
                                        "," + boleta.liquidada.ToString() +
                                        "," + boleta.importe.ToString() + "," + boleta.IdDetalle.ToString + "," +
        boleta.precio.ToString + "," + boleta.tara.ToString + "," + boleta.brutosinanalizar.ToString() + ",'" + boleta.horaSalida + "',"
        If boleta.idCliente = -1 Then
            comm.CommandText += "null"
        Else
            comm.CommandText += boleta.idCliente.ToString
        End If
        comm.CommandText += ",'" + boleta.serie + "');"
        Try
            comm.ExecuteNonQuery()
            'MsgBox("Boleta guardada.")
            PopUp("Guardada", 10)
        Catch ex As Exception
            MsgBox("error al guardar el registro: " + ex.ToString)
        End Try
    End Sub

    Public Sub actualiza(ByRef boleta As dbSemillasBoleta)

        comm.CommandText = UPDATE
        If boleta.tipoBoleta = "S" Then
            comm.CommandText += "productor=null"
        Else
            comm.CommandText += "productor=" + boleta.productor.ID.ToString()
        End If
        comm.CommandText += ", chofer='" + boleta.chofer + "'" +
                                  ", producto=" + boleta.producto.ID.ToString() +
                                  ", peso=" + boleta.peso.ToString() +
                                  ", humedad=" + boleta.humedad.ToString() +
                                  ", impurezas=" + boleta.impurezas.ToString() +
                                  ", granoquebrado=" + boleta.granoQuebrado.ToString() +
                                  ", granodanado =" + boleta.granoDanado.ToString() +
                                  ", castigoHumedad=" + boleta.castigoHumedad.ToString() +
                                  ", castigoImpurezas=" + boleta.castigoImpurezas.ToString() +
                                  ", castigoGranoD=" + boleta.castigoGranoDanado.ToString() +
                                  ", castigoGranoQ=" + boleta.castigoGranoQuebrado.ToString() +
                                  ", castigoTotal=" + boleta.castigoTotal.ToString() +
                                  ", pesoanalizado=" + boleta.pesoFinal.ToString() +
                                  ", hora='" + boleta.hora + "'" +
                                  ", analista='" + boleta.analista + "'" +
                                  ", pesador='" + boleta.pesador + "'" +
                                  ", descargo='" + boleta.descargo + "'" +
                                  ", portero='" + boleta.portero + "'" +
                                  ", tipoBoleta='" + boleta.tipoBoleta + "'" +
                                  ", bodega='" + boleta.bodega.ToString() +
                                  "', variedad='" + boleta.variedad + "'" +
                                  ", porcentajeHumedad=" + boleta.porcentajeHumedad.ToString() +
                                  ", porcentajeImpurezas=" + boleta.porcentajeImpurezas.ToString() +
                                  ", porcentajeGranoQ=" + boleta.porcentajeGranoQ.ToString() +
                                  ", porcentajeGranoD =" + boleta.porcentajeGranoD.ToString() +
                                  ", importe=" + boleta.importe.ToString() +
                                  ",fecha='" + boleta.fecha.ToString("yyyy/MM/dd") + "'" +
                                  ",precio=" + boleta.precio.ToString +
                                  ",tara=" + boleta.tara.ToString +
                                  ",brutosinanalizar=" + boleta.brutosinanalizar.ToString +
                                  ",horasalida='" + boleta.horaSalida + "'" +
                                  ",serie='" + boleta.serie + "'"
        If boleta.tipoBoleta = "E" Then
            comm.CommandText += ",idCliente=null"
        Else
            comm.CommandText += ",idCliente=" + boleta.idCliente.ToString
        End If
        comm.CommandText += " where id=" + boleta.id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            PopUp("Guardada", 10)
        Catch ex As Exception
            MsgBox("error al modificar el registro: " + ex.ToString)
        End Try
    End Sub


    Public Sub elimina(ByRef boleta As dbSemillasBoleta)
        Dim res As Boolean = False
        Dim dr As MySqlDataReader
        comm.CommandText = "select * from tblsemillasliquidaciondetalle where idboleta=" + boleta.id.ToString() + ";"
        dr = comm.ExecuteReader
        If dr.HasRows() Then
            res = True
        End If
        dr.Close()
        comm.CommandText = "delete from tblsemillasboletas where id=" + boleta.id.ToString()
        Try
            If res Then
                MsgBox("No se puede elmininar la boleta ya que pertenece a una liquidación.", MsgBoxStyle.Information, GlobalNombreApp)
            Else
                comm.ExecuteNonQuery()
                MsgBox("Boleta Eliminada.")
            End If
        Catch ex As Exception
            MsgBox("Error al borrar el registro: " + ex.ToString, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Public Sub liquidar(ByVal id As Integer, ByVal liquidar As Boolean)
        comm.CommandText = "update tblsemillasboletas set liquidada=" + liquidar.ToString() + " where id=" + id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Error al liquidar la boleta: " + ex.ToString(), MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Public Function consultaBoletas() As DataView
        comm.CommandText = "select b.id as id, concat(b.serie,b.folio) as folio,b.fecha as fecha,pr.nombre as productor,prod.nombre as producto," +
        " format(b.importe,2) as importe from tblsemillasboletas as b inner join tblproveedores as pr on b.productor=pr.idproveedor" +
        " inner join tblinventario as prod on b.producto=prod.idInventario;"
        Dim dc As New MySql.Data.MySqlClient.MySqlDataAdapter(comm)
        Dim ds As New DataSet
        dc.Fill(ds, "boletas")
        Return ds.Tables("boletas").DefaultView
    End Function

    Public Function consultaChofer(ByVal chofer As String) As DataView
        comm.CommandText = "select * from tblsemillasboletas where chofer='" + chofer.ToString + "';"
        Dim dc As New MySql.Data.MySqlClient.MySqlDataAdapter(comm)
        Dim ds As New DataSet
        dc.Fill(ds, "boletas")
        Return ds.Tables("boletas").DefaultView
    End Function

    Public Function consultaProductor(ByRef proveedor As dbproveedores) As DataView
        comm.CommandText = "select b.id as id, b.folio as folio,b.fecha as fecha,pr.nombre as productor,prod.nombre as producto," +
      " b.importe as importe from tblsemillasboletas as b inner join tblproveedores as pr on b.productor=pr.idproveedor" +
      " inner join tblinventario as prod on b.producto=prod.idInventario where b.productor=" + proveedor.ID.ToString() + ";"
        Dim dc As New MySql.Data.MySqlClient.MySqlDataAdapter(comm)
        Dim ds As New DataSet
        dc.Fill(ds, "boletas")
        Return ds.Tables("boletas").DefaultView
    End Function

    Public Function consultaProducto(ByRef producto As dbInventario) As DataView
        comm.CommandText = "select b.id as id, b.folio as folio,b.fecha as fecha,pr.nombre as productor,prod.nombre as producto," +
       " b.importe as importe from tblsemillasboletas as b inner join tblproveedores as pr on b.productor=pr.idproveedor" +
       " inner join tblinventario as prod on b.producto=prod.idInventario where b.producto=" + producto.ID.ToString() + ";"
        Dim dc As New MySql.Data.MySqlClient.MySqlDataAdapter(comm)
        Dim ds As New DataSet
        dc.Fill(ds, "boletas")
        Return ds.Tables("boletas").DefaultView
    End Function

    Public Function consultaFolio(ByVal folio As String) As DataView
        comm.CommandText = "select * from tblsemillasboletas where folio like '%" + folio.ToString + "';"
        Dim dc As New MySql.Data.MySqlClient.MySqlDataAdapter(comm)
        Dim ds As New DataSet
        dc.Fill(ds, "boletas")
        Return ds.Tables("boletas").DefaultView
    End Function

    Public Function buscarFolio(ByRef boleta As dbSemillasBoleta) As dbSemillasBoleta
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader
        Dim b As New dbSemillasBoleta
        comm.CommandText = "select b.id as id,b.serie as serie,b.folio as folio,b.fecha as fecha," +
            "prov.idproveedor as proveedor,b.chofer as chofer,prod.idinventario as producto," +
            "b.peso as peso,b.humedad as humedad,b.impurezas as impurezas,b.granoquebrado as granoQ," +
            "b.granodanado as granoD,b.castigoHumedad as castigoHumedad,b.castigoImpurezas as castigoImpurezas," +
            "b.castigoGranoD as castigoGranoD,b.castigoGranoQ as castigoGranoQ,b.castigoTotal as castigoTotal," +
            "b.pesoanalizado as pesoanalizado,b.hora as hora,b.camion as camion,b.placas as placas," +
            "b.analista as analista,b.pesador as pesador,b.descargo as descargo,b.portero as portero," +
            "b.tipoBoleta as tipoBoleta,b.variedad as variedad,b.porcentajeHumedad as porcentajeHumedad," +
            "b.porcentajeImpurezas as porcentajeImpurezas,b.porcentajeGranoQ as porcentajeGranoQ,b.porcentajeGranoD as porcentajeGranoD, " +
            "b.liquidada as liquidada, b.importe as importe from tblsemillasboletas as b inner join tblproveedores as prov on prov.idproveedor=b.productor" +
            " inner join tblinventario as prod on prod.idinventario=b.producto  where b.folio='" + boleta.folio.ToString() + "';"
        dr = comm.ExecuteReader
        If dr.HasRows = False Then
            dr.Close()
            Return Nothing
        Else
            While dr.Read()
                b.id = dr.GetInt32("id")
                b.folio = dr.GetInt32("folio")
                b.fecha = dr.GetString("fecha")
                b.productor = New dbproveedores(dr.GetInt32("proveedor"), MySqlcon)
                b.chofer = dr.GetString("chofer")
                b.producto = New dbInventario(dr.GetInt32("producto"), MySqlcon)
                b.peso = dr.GetDouble("peso")
                b.humedad = dr.GetDouble("humedad")
                b.impurezas = dr.GetDouble("impurezas")
                b.granoDanado = dr.GetDouble("granoD")
                b.granoQuebrado = dr.GetDouble("granoQ")
                b.castigoHumedad = dr.GetDouble("castigoHumedad")
                b.castigoImpurezas = dr.GetDouble("castigoImpurezas")
                b.castigoGranoDanado = dr.GetDouble("castigoGranoD")
                b.castigoGranoQuebrado = dr.GetDouble("castigoGranoQ")
                b.castigoTotal = dr.GetDouble("castigoTotal")
                b.pesoFinal = dr.GetDouble("pesoanalizado")
                b.hora = dr.GetString("hora")
                b.camion = dr.GetString("camion")
                b.placas = dr.GetString("placas")
                b.analista = dr.GetString("analista")
                b.pesador = dr.GetString("pesador")
                b.descargo = dr.GetString("descargo")
                b.portero = dr.GetString("portero")
                b.tipoBoleta = dr.GetString("tipoBoleta")
                b.variedad = dr.GetString("variedad")
                b.porcentajeHumedad = dr.GetDouble("porcentajeHumedad")
                b.porcentajeImpurezas = dr.GetDouble("porcentajeImpurezas")
                b.porcentajeGranoQ = dr.GetDouble("porcentajeGranoQ")
                b.porcentajeGranoD = dr.GetDouble("porcentajeGranoD")
                b.liquidada = dr("liquidada")
                b.importe = dr("importe")
                b.serie = dr("serie")
            End While
        End If
        dr.Close()
        Return b
    End Function

    Public Function buscar(ByRef boleta As dbSemillasBoleta) As dbSemillasBoleta
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader
        Dim b As New dbSemillasBoleta
        comm.CommandText = "select b.id as id,b.serie as serie,b.folio as folio,b.fecha as fecha," +
            "b.productor as proveedor,b.chofer as chofer,prod.idinventario as producto,b.precio as precio," +
            "b.peso as peso,b.humedad as humedad,b.impurezas as impurezas,b.granoquebrado as granoQ," +
            "b.granodanado as granoD,b.castigoHumedad as castigoHumedad,b.castigoImpurezas as castigoImpurezas," +
            "b.castigoGranoD as castigoGranoD,b.castigoGranoQ as castigoGranoQ,b.castigoTotal as castigoTotal," +
            "b.pesoanalizado as pesoanalizado,b.hora as hora, b.horasalida as horasalida,b.camion as camion,b.placas as placas," +
            "b.analista as analista,b.pesador as pesador,b.descargo as descargo,b.portero as portero,b.bodega as bodega," +
            "b.tipoBoleta as tipoBoleta,b.variedad as variedad,b.porcentajeHumedad as porcentajeHumedad,b.idCliente as idCliente," +
            "b.porcentajeImpurezas as porcentajeImpurezas,b.porcentajeGranoQ as porcentajeGranoQ,b.porcentajeGranoD as porcentajeGranoD, " +
            "ifnull(b.liquidada,0) as liquidada, ifnull(b.importe,0) as importe, b.tara as tara, b.brutosinanalizar as brutosinanalizar from tblsemillasboletas as b" +
            " inner join tblinventario as prod on prod.idinventario=b.producto  where b.id=" + boleta.id.ToString() + ";"

        dr = comm.ExecuteReader
        If dr.HasRows = False Then
            dr.Close()
            Return Nothing
        Else
            While dr.Read()
                b.id = dr.GetInt32("id")
                b.folio = dr.GetInt32("folio")
                b.fecha = dr.GetString("fecha")
                b.productor = New dbproveedores(MySqlcon)
                If IsDBNull(dr("proveedor")) = False Then
                    b.productor.ID = dr.GetInt32("proveedor")
                End If
                b.chofer = dr.GetString("chofer")
                b.producto = New dbInventario(MySqlcon)
                b.producto.ID = dr.GetInt32("producto")
                b.peso = dr.GetDouble("peso")
                b.humedad = dr.GetDouble("humedad")
                b.impurezas = dr.GetDouble("impurezas")
                b.granoDanado = dr.GetDouble("granoD")
                b.granoQuebrado = dr.GetDouble("granoQ")
                b.castigoHumedad = dr.GetDouble("castigoHumedad")
                b.castigoImpurezas = dr.GetDouble("castigoImpurezas")
                b.castigoGranoDanado = dr.GetDouble("castigoGranoD")
                b.castigoGranoQuebrado = dr.GetDouble("castigoGranoQ")
                b.castigoTotal = dr.GetDouble("castigoTotal")
                b.pesoFinal = dr.GetDouble("pesoanalizado")
                b.hora = dr.GetString("hora")
                b.camion = dr.GetString("camion")
                b.placas = dr.GetString("placas")
                b.analista = dr.GetString("analista")
                b.pesador = dr.GetString("pesador")
                b.descargo = dr.GetString("descargo")
                b.portero = dr.GetString("portero")
                b.bodega = dr("bodega")
                b.tipoBoleta = dr.GetString("tipoBoleta")
                b.variedad = dr.GetString("variedad")
                b.porcentajeHumedad = dr.GetDouble("porcentajeHumedad")
                b.porcentajeImpurezas = dr.GetDouble("porcentajeImpurezas")
                b.porcentajeGranoQ = dr.GetDouble("porcentajeGranoQ")
                b.porcentajeGranoD = dr.GetDouble("porcentajeGranoD")
                b.liquidada = dr("liquidada")
                b.importe = dr("importe")
                b.tara = dr("tara")
                If IsDBNull(dr("serie")) Then
                    b.serie = ""
                Else
                    b.serie = dr("serie")
                End If
                b.brutosinanalizar = dr("brutosinanalizar")
                b.horaSalida = dr("horasalida")
                If IsDBNull(dr("idCliente")) Then
                    b.idCliente = -1
                Else
                    b.idCliente = dr("idCliente")
                End If
                b.precio = dr("precio")
            End While
        End If
        dr.Close()
        Return b
    End Function

    Public Function consultaProductorLiquidadas(ByRef proveedor As dbproveedores, ByVal liquidada As Boolean) As DataView
        comm.CommandText = "select b.id as id,ifnull(concat(b.serie,b.folio),b.folio) as folio,b.fecha as fecha," +
            "prod.nombre as producto," +
        "b.importe as importe from tblsemillasboletas as b inner join tblproveedores as prov on prov.idproveedor=b.productor" +
            " inner join tblinventario as prod on prod.idinventario=b.producto  where b.productor=" + proveedor.ID.ToString() + " and liquidada=" + liquidada.ToString() + ";"

        Dim dc As New MySql.Data.MySqlClient.MySqlDataAdapter(comm)
        Dim ds As New DataSet
        dc.Fill(ds, "boletas")
        Return ds.Tables("boletas").DefaultView
    End Function

    Public Function boletasProductorLiquidadas(ByRef productor As dbproveedores, ByVal liquidada As Boolean) As List(Of dbSemillasBoleta)
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader
        Dim b As dbSemillasBoleta
        Dim lista As New List(Of dbSemillasBoleta)
        comm.CommandText = "select b.id as id,b.liquidada as liquidada,concat(b.serie,b.folio) as folio,b.fecha as fecha," +
            "prov.idproveedor as proveedor,b.chofer as chofer,prod.idinventario as producto," +
            "b.peso as peso,b.humedad as humedad,b.impurezas as impurezas,b.granoquebrado as granoQ," +
            "b.granodanado as granoD,b.castigoHumedad as castigoHumedad,b.castigoImpurezas as castigoImpurezas," +
            "b.castigoGranoD as castigoGranoD,b.castigoGranoQ as castigoGranoQ,b.castigoTotal as castigoTotal," +
            "b.pesoanalizado as pesoanalizado,b.hora as hora,b.camion as camion,b.placas as placas," +
            "b.analista as analista,b.pesador as pesador,b.descargo as descargo,b.portero as portero," +
            "b.tipoBoleta as tipoBoleta,b.variedad as variedad,b.porcentajeHumedad as porcentajeHumedad," +
            "b.porcentajeImpurezas as porcentajeImpurezas,b.porcentajeGranoQ as porcentajeGranoQ,b.porcentajeGranoD as porcentajeGranoD, " +
            "b.importe as importe from tblsemillasboletas as b inner join tblproveedores as prov on prov.idproveedor=b.productor" +
            " inner join tblinventario as prod on prod.idinventario=b.producto  where b.productor=" + productor.ID.ToString() + " and liquidada=" + liquidada.ToString() + " or b.liquidada is null;"
        dr = comm.ExecuteReader
        If dr.HasRows = False Then
            dr.Close()
            Return Nothing
        Else
            While dr.Read()
                b = New dbSemillasBoleta
                b.id = dr.GetInt32("id")
                b.folio = dr.GetInt32("folio")
                b.fecha = dr.GetString("fecha")
                b.productor = New dbproveedores(MySqlcon)
                b.productor.id = dr.GetInt32("proveedor")
                b.chofer = dr.GetString("chofer")
                b.producto = New dbInventario(MySqlcon)
                b.producto.id = dr.GetInt32("producto")
                b.peso = dr.GetDouble("peso")
                b.humedad = dr.GetDouble("humedad")
                b.impurezas = dr.GetDouble("impurezas")
                b.granoDanado = dr.GetDouble("granoD")
                b.granoQuebrado = dr.GetDouble("granoQ")
                b.castigoHumedad = dr.GetDouble("castigoHumedad")
                b.castigoImpurezas = dr.GetDouble("castigoImpurezas")
                b.castigoGranoDanado = dr.GetDouble("castigoGranoD")
                b.castigoGranoQuebrado = dr.GetDouble("castigoGranoQ")
                b.castigoTotal = dr.GetDouble("castigoTotal")
                b.pesoFinal = dr.GetDouble("pesoanalizado")
                b.hora = dr.GetString("hora")
                b.camion = dr.GetString("camion")
                b.placas = dr.GetString("placas")
                b.analista = dr.GetString("analista")
                b.pesador = dr.GetString("pesador")
                b.descargo = dr.GetString("descargo")
                b.portero = dr.GetString("portero")
                b.tipoBoleta = dr.GetString("tipoBoleta")
                b.variedad = dr.GetString("variedad")
                b.porcentajeHumedad = dr.GetDouble("porcentajeHumedad")
                b.porcentajeImpurezas = dr.GetDouble("porcentajeImpurezas")
                b.porcentajeGranoQ = dr.GetDouble("porcentajeGranoQ")
                b.porcentajeGranoD = dr.GetDouble("porcentajeGranoD")
                If IsDBNull(dr("liquidada")) Then
                    b.liquidada = False
                End If
                If IsDBNull(dr("importe")) Then
                    b.importe = 0
                End If
                lista.Add(b)
            End While
        End If
        dr.Close()
        Return lista
    End Function

    Public Function totalBoletasProductor(ByRef productor As dbproveedores) As Double
        comm.CommandText = "select ifnull(sum(b.importe),0) as total from tblsemillasboletas as b where productor=" + productor.ID.ToString() + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Dim res As Double = 0
        If dr.HasRows Then
            While dr.Read()
                res += dr("total")
            End While
        End If
        dr.Close()
        Return res
    End Function

    Public Function totalBoletasProductorLiquidadas(ByRef productor As dbproveedores, ByVal liquidada As Boolean) As Double
        comm.CommandText = "select ifnull(sum(b.importe),0) as total from tblsemillasboletas as b where productor=" + productor.ID.ToString() + " and liquidada=" + liquidada.ToString() + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Dim res As Double = 0
        If dr.HasRows Then
            While dr.Read()
                res += dr("total")
            End While
        End If
        dr.Close()
        Return res
    End Function

    Public Function totalPesoBruto(ByRef productor As dbproveedores) As Double
        comm.CommandText = "select ifnull(sum(b.peso),0) as pesoBruto from tblsemillasboletas as b where productor=" + productor.ID.ToString() + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Dim res As Double = 0
        If dr.HasRows Then
            While dr.Read()
                res += dr("pesoBruto")
            End While
        End If
        dr.Close()
        Return res
    End Function

    Public Function totalPesoAnalizado(ByRef productor As dbproveedores) As Double
        comm.CommandText = "select ifnull(sum(b.pesoanalizado),0) as pesoanalizado from tblsemillasboletas as b where productor=" + productor.ID.ToString() + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Dim res As Double = 0
        If dr.HasRows Then
            While dr.Read()
                res += dr("pesoanalizado")
            End While
        End If
        dr.Close()
        Return res
    End Function

    Public Function checarBoletaPendiente(ByVal idboleta As Integer) As Boolean
        comm.CommandText = "select if((select d.id from tblsemillasliquidaciondetalle as d where idboleta=" + idboleta.ToString() + ")=null,(select b.id from tblsemillasboletas as b inner join tblproveedores as prov on b.productor=prov.idproveedor " +
"where b.id=" + idboleta.ToString() + "),0) as id"
        Dim res As Integer = comm.ExecuteScalar
        If res > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Function buscaProductoProductor(ByVal claveProductor As String, ByVal claveCliente As String, ByVal claveProducto As String, pFecha As String, pFecha2 As String, pFolio As String, tipo As String) As DataView
        If tipo = "E" Then
            comm.CommandText = "select b.id as id,ifnull(concat(b.serie,b.folio),b.folio) as folio,b.fecha as fecha,prov.nombre as productor,prod.nombre as producto,b.pesoanalizado as pesofinal, b.tipoBoleta as tipo from tblsemillasboletas as b inner join tblproveedores as prov on prov.idproveedor=b.productor" +
        " inner join tblinventario as prod on prod.idinventario=b.producto  where b.fecha>='" + pFecha + "' and b.fecha<='" + pFecha2 + "'"
        End If
        If tipo = "S" Then
            comm.CommandText = "select b.id as id,ifnull(concat(b.serie,b.folio),b.folio) as folio,b.fecha as fecha,cli.nombre as destino,prod.nombre as producto,b.pesoanalizado as pesofinae,b.tipoBoleta as tipo from tblsemillasboletas as b" +
       " inner join tblinventario as prod on prod.idinventario=b.producto inner join tblclientes as cli on cli.idcliente=b.idCliente where b.fecha>='" + pFecha + "' and b.fecha<='" + pFecha2 + "'"
        End If
        If claveProductor <> "" Then
            comm.CommandText += " and prov.clave like '%" + claveProductor.Replace("'", "''") + "'"
        End If
        If claveCliente <> "" Then
            comm.CommandText += " and cli.clave like '%" + claveCliente.Replace("'", "''") + "'"
        End If
        If claveProducto <> "" Then
            comm.CommandText += " and prod.clave like '%" + claveProducto.Replace("'", "''") + "'"
        End If
        If pFolio <> "" Then
            comm.CommandText += " and b.folio like '%" + pFolio.Replace("'", "''") + "%'"
        End If
        If tipo <> "T" Then
            comm.CommandText += " and b.tipoBoleta='" + tipo + "'"
        End If
        Dim dc As New MySql.Data.MySqlClient.MySqlDataAdapter(comm)
        Dim ds As New DataSet
        dc.Fill(ds, "boletas")
        Return ds.Tables("boletas").DefaultView
    End Function

    Public Function vistaFolio(ByVal folio As String) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select b.id as id,concat(b.serie,b.folio) as folio,b.fecha as fecha," +
    "prov.idproveedor as proveedor,prod.idinventario as producto, b.importe as importe from tblsemillasboletas as b inner join tblproveedores as prov on prov.idproveedor=b.productor" +
    " inner join tblinventario as prod on prod.idinventario=b.producto  where b.folio like '%" + folio.ToString() + "%';"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "boletas")
        Return ds.Tables("boletas").DefaultView
    End Function

    Public Function obtenFolio() As Integer

        comm.CommandText = "select ifnull(max(folio),0) as folio from tblsemillasboletas;"
        Dim f As Integer = comm.ExecuteScalar
        folio = f + 1
        Return folio
    End Function

    Public Function obtenerConfigiracion(ByVal id As Integer) As MySqlDataReader
        comm.CommandText = "select * from tblsemillasconfiguracion where idproducto=" + id.ToString() + ";"
        Return comm.ExecuteReader
    End Function

    Public Function registros(ByVal columna As String) As MySqlDataReader
        comm.CommandText = "select distinct " + columna + " from tblsemillasboletas where " + columna + "<>'';"
        Return comm.ExecuteReader
    End Function
    Public Function DaIdBoletaDeDetalle(pIddetalle As Integer) As Integer
        comm.CommandText = "select ifnull((select id from tblsemillasboletas where iddetalle=" + pIddetalle.ToString + "),0)"
        id = comm.ExecuteScalar
        Return id
    End Function

    Public Function checaFolioRepetido(ByVal serie As String, ByVal folio As String) As Boolean
        comm.CommandText = "select id as id from tblsemillasboletas as b where b.serie='" + serie + "' and b.folio=" + folio + ";"
        Dim res As Integer = comm.ExecuteScalar
        If res > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ultimoId() As Integer
        comm.CommandText = "select max(id) from tblsemillasboletas"
        Dim res As Integer
        res = comm.ExecuteScalar
        Return res
    End Function

    Public Sub actualizaPrecio(ByVal idBoleta As Integer, ByVal precio As Double)
        Try
            comm.CommandText = "update tblsemillasboletas set precio=" + precio.ToString() + " where id=" + idBoleta.ToString()
            comm.ExecuteNonQuery()
            comm.CommandText = "select pesoanalizado from tblsemillasboletas where id=" + idBoleta.ToString()
            Dim peso As Double = comm.ExecuteScalar
            comm.CommandText = "update tblsemillasboletas set importe=" + (precio * peso).ToString() + " where id=" + idBoleta.ToString()
            comm.ExecuteNonQuery()
        Catch ex As Exception

        End Try

    End Sub
#End Region
End Class
