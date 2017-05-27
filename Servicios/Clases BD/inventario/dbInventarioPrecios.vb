Public Class dbInventarioPrecios
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public idInventario As Integer
    Public Precio As Double
    Public utilidad As Double
    Public Comentario As String
    Public IdMoneda As Integer
    Public Descuento As Double
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        idInventario = 0
        Precio = 0
        utilidad = 0
        Comentario = ""
        IdMoneda = 0
        Descuento = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblinventarioprecios where idinventarioprecio=" + ID.ToString + ";"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            idInventario = DReader("idinventario")
            utilidad = DReader("utilidad")
            'If DReader("utilidad") = 0 Then
            Precio = DReader("precio")
            'Else
            'Precio = DReader("precio") + DReader("precio") * DReader("costo")
            'End If
            Comentario = DReader("comentario")
            IdMoneda = DReader("idmoneda")
            Descuento = DReader("descuentoprecio")
        End If
        DReader.Close()
    End Sub
    Public Sub LlenaDatosTemp()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblinventariopreciostemp where id=" + ID.ToString + ";"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            idInventario = DReader("idinventario")
            utilidad = DReader("utilidad")
            'If DReader("utilidad") = 0 Then
            Precio = DReader("precio")
            Descuento = DReader("descuentoprecio")
            'Else
            'Precio = DReader("precio") + DReader("precio") * DReader("costo")
            'End If
            Comentario = DReader("comentario")
            IdMoneda = DReader("idmoneda")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pPrecio As Double, ByVal pIdInventario As Integer, ByVal pUtilidad As Byte, ByVal pComentario As String, ByVal pIdMoneda As Integer, ByVal pDescuento As Integer)
        Precio = pPrecio
        idInventario = pIdInventario
        utilidad = pUtilidad
        Comentario = pComentario
        IdMoneda = pIdMoneda
        Descuento = pDescuento
        Comm.CommandText = "insert into tblinventarioprecios(idinventario,precio,utilidad,comentario,idmoneda,descuentoprecio) values(" + idInventario.ToString + "," + Precio.ToString + "," + utilidad.ToString + ",'" + Replace(Comentario, "'", "''") + "'," + IdMoneda.ToString + "," + Descuento.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pPrecio As Double, ByVal pUtilidad As Double, ByVal pComentario As String, ByVal pIdMoneda As Integer, ByVal pDescuento As Double, pActualizarDesc As Boolean)
        ID = pID
        Precio = pPrecio
        utilidad = pUtilidad
        Comentario = pComentario
        IdMoneda = pIdMoneda
        Descuento = pDescuento
        Comm.CommandText = "select idinventario from tblinventarioprecios where idinventarioprecio=" + ID.ToString
        idInventario = Comm.ExecuteScalar()
        Comm.CommandText = "update tblinventarioprecios set precio=" + Precio.ToString + ",utilidad=" + utilidad.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "',idmoneda=" + IdMoneda.ToString + ",descuentoprecio=" + pDescuento.ToString + " where idinventarioprecio=" + ID.ToString
        Comm.ExecuteNonQuery()
        If pActualizarDesc Then
            Comm.CommandText = "update tblinventarioprecios set precio=" + Precio.ToString + "-(" + Precio.ToString + "*descuentoprecio/100) where idlista>1 and descuentoprecio<>0 and idinventario=" + idInventario.ToString
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub ModificarTemp(ByVal pID As Integer, ByVal pPrecio As Double, ByVal pUtilidad As Byte, ByVal pComentario As String, ByVal pIdMoneda As Integer, ByVal pDescuento As Integer)
        ID = pID
        Precio = pPrecio
        utilidad = pUtilidad
        Comentario = pComentario
        IdMoneda = pIdMoneda
        Descuento = pDescuento
        'Comm.CommandText = "select idinventario from tblinventarioprecios where idinventarioprecio=" + ID.ToString
        'idInventario = Comm.ExecuteScalar()
        Comm.CommandText = "update tblinventariopreciostemp set precio=" + Precio.ToString + ",utilidad=" + utilidad.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "',idmoneda=" + IdMoneda.ToString + ",descuentoprecio=" + Descuento.ToString + " where id=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblinventarioprecios where idinventarioprecio=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdInventario As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idinventarioprecio,descripcion,precio,utilidad,descuentoprecio,comentario from tblinventarioprecios p inner join tbllistasprecios l on p.idlista=l.idlista where idinventario=" + pIdInventario.ToString + " order by numero"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioprecios")
        Return DS.Tables("tblinventarioprecios").DefaultView
    End Function
    Public Function ConsultaTemp(ByVal pIdInventario As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select id,descripcion,precio,utilidad,descuentoprecio,comentario from tblinventariopreciostemp p inner join tbllistasprecios l on p.idlista=l.idlista order by numero"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioprecios")
        Return DS.Tables("tblinventarioprecios").DefaultView
    End Function
    Public Function Reporte(ByVal pIdInventario As Integer, ByVal pIdClas1 As Integer, ByVal pIDClas2 As Integer, ByVal pIDclas3 As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select i.idinventario,i.nombre,i.clave," + _
        "ifnull((select precio from tblinventarioprecios where idlista=1 and idinventario=i.idinventario),0) as Lista1," + _
        "ifnull((select precio from tblinventarioprecios where idlista=2 and idinventario=i.idinventario),0) as Lista2," + _
        "ifnull((select precio from tblinventarioprecios where idlista=3 and idinventario=i.idinventario),0) as Lista3," + _
        "ifnull((select precio from tblinventarioprecios where idlista=4 and idinventario=i.idinventario),0) as Lista4," + _
        "ifnull((select precio from tblinventarioprecios where idlista=5 and idinventario=i.idinventario),0) as Lista5" + _
        " from tblinventario as i  where i.idinventario>1 and descontinuado=0 "
        If pIdInventario > 0 Then
            Comm.CommandText += " and i.idinventario=" + pIdInventario.ToString
        End If
        If pIdClas1 > 0 Then
            Comm.CommandText += " and i.idclasificacion=" + pIdClas1.ToString
        End If
        If pIDClas2 > 0 Then
            Comm.CommandText += " and i.idclasificacion2=" + pIDClas2.ToString
        End If
        If pIDclas3 > 0 Then
            Comm.CommandText += " and i.idclasificacion3=" + pIDclas3.ToString
        End If
        Comm.CommandText += " order by i.nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioprecios")
        'DS.WriteXmlSchema("replistasprecios.xml")
        Return DS.Tables("tblinventarioprecios").DefaultView
    End Function
    Public Sub BuscaPrecio(ByVal pIdInventario As Integer, ByVal pIdLista As Integer)
        Comm.CommandText = "select ifnull((select idinventarioprecio from tblinventarioprecios where idinventario=" + pIdInventario.ToString + " and idlista=" + pIdLista.ToString + "),0)"
        ID = Comm.ExecuteScalar
        If ID > 0 Then
            LlenaDatos()
        Else
            Precio = 0
            IdMoneda = 2
        End If
    End Sub
    Public Sub AsignaListas(ByVal pIdInventario As Integer)
        Comm.CommandText = "insert into tblinventarioprecios(idinventario,precio,comentario,idmoneda,idlista,utilidad,descuentoprecio) select " + pIdInventario.ToString + ",0,'',2,idlista,0,0 from tbllistasprecios"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaPrecios(ByVal pIdinventario As Integer, ByVal pCosto As Double, ByVal pPrecioNeto As Boolean, ByVal pIva As Double, ByVal pTipo As String, ByVal pIeps As Double, ByVal pIvaREt As Double)
        Dim p1 As Double
        p1 = DaPrecioListaUno(pIdinventario)
        Comm.CommandText = "update tblinventarioprecios set precio=" + p1.ToString + "-(" + p1.ToString + "*descuentoprecio/100) where idlista>1 and descuentoprecio<>0 and idinventario=" + pIdinventario.ToString
        Comm.ExecuteNonQuery()
        If pTipo = "0" Then
            If pPrecioNeto = False Then
                Comm.CommandText = "update tblinventarioprecios set precio=" + pCosto.ToString + "*(1+(utilidad/100)) where utilidad<>0 and idinventario=" + pIdinventario.ToString
                Comm.ExecuteNonQuery()
            Else
                Comm.CommandText = "update tblinventarioprecios set precio=" + pCosto.ToString + "*(1+(utilidad/100)) where utilidad<>0 and idinventario=" + pIdinventario.ToString
                Comm.ExecuteNonQuery()
                Comm.CommandText = "update tblinventarioprecios set precio=precio*(1+((" + pIva.ToString + "+" + pIeps.ToString + "-" + pIvaREt.ToString + ")/100)) where utilidad<>0 and idinventario=" + pIdinventario.ToString
                Comm.ExecuteNonQuery()
            End If
        Else
            If pPrecioNeto = False Then
                Comm.CommandText = "update tblinventarioprecios set precio=ifnull((" + pCosto.ToString + "/(1-(utilidad/100))),0) where utilidad<>0 and idinventario=" + pIdinventario.ToString
                Comm.ExecuteNonQuery()
            Else
                Comm.CommandText = "update tblinventarioprecios set precio=ifnull((" + pCosto.ToString + "/(1-(utilidad/100))),0) where utilidad<>0 and idinventario=" + pIdinventario.ToString
                Comm.ExecuteNonQuery()
                Comm.CommandText = "update tblinventarioprecios set precio=ifnull((precio*(1+((" + pIva.ToString + "+" + pIeps.ToString + "-" + pIvaREt.ToString + ")/100))),0) where utilidad<>0 and idinventario=" + pIdinventario.ToString
                Comm.ExecuteNonQuery()
            End If
        End If
    End Sub
    Public Sub CambiaPrecio(ByVal pIdInventario As Integer, ByVal pIdClas As Integer, ByVal pIdclas2 As Integer, ByVal pIdClas3 As Integer, ByVal pIdLista As Integer, ByVal pCantidad As Double, ByVal pTipo As Byte, pMetodoUtilidad As String)
        Dim Filtros As String = ""
        Select Case pTipo
            Case 0
                Comm.CommandText = "update tblinventarioprecios inner join tblinventario on tblinventario.idinventario=tblinventarioprecios.idinventario set precio=precio+" + pCantidad.ToString + " where utilidad=0 "
            Case 1
                Comm.CommandText = "update tblinventarioprecios inner join tblinventario on tblinventario.idinventario=tblinventarioprecios.idinventario set precio=" + pCantidad.ToString + " where utilidad=0 "
            Case 2
                Comm.CommandText = "update tblinventarioprecios inner join tblinventario on tblinventario.idinventario=tblinventarioprecios.idinventario set utilidad=utilidad+" + pCantidad.ToString + " where true "
            Case 3
                Comm.CommandText = "update tblinventarioprecios inner join tblinventario on tblinventario.idinventario=tblinventarioprecios.idinventario set utilidad=" + pCantidad.ToString + " where true "
            Case 4
                Comm.CommandText = "update tblinventarioprecios inner join tblinventario on tblinventario.idinventario=tblinventarioprecios.idinventario set descuentoprecio=descuentoprecio+" + pCantidad.ToString + " where true "
            Case 5
                Comm.CommandText = "update tblinventarioprecios inner join tblinventario on tblinventario.idinventario=tblinventarioprecios.idinventario set descuentoprecio=" + pCantidad.ToString + " where true "
            Case 6
                Comm.CommandText = "update tblinventarioprecios inner join tblinventario on tblinventario.idinventario=tblinventarioprecios.idinventario set precio=precio+(precio*" + pCantidad.ToString + "/100) where utilidad=0 "
        End Select


        If pIdInventario > 1 Then
            Filtros += " and tblinventarioprecios.idinventario=" + pIdInventario.ToString
        End If
        If pIdClas > 1 Then
            Filtros += " and tblinventario.idclasificacion=" + pIdClas.ToString
        End If
        If pIdclas2 > 1 Then
            Filtros += " and tblinventario.idclasificacion2=" + pIdclas2.ToString
        End If
        If pIdClas3 > 1 Then
            Filtros += " and tblinventario.idclasificacion3=" + pIdClas3.ToString
        End If
        If pIdLista > 0 Then
            Filtros += " and tblinventarioprecios.idlista=" + pIdLista.ToString
        End If
        Comm.CommandText += Filtros
        Comm.ExecuteNonQuery()
        If pTipo = 2 Or pTipo = 3 Then
            If pMetodoUtilidad = "0" Then
                Comm.CommandText = "update tblinventarioprecios inner join tblinventario on tblinventario.idinventario=tblinventarioprecios.idinventario set precio=costobase*(1+(utilidad/100)) where true" + Filtros
            Else
                Comm.CommandText = "update tblinventarioprecios inner join tblinventario on tblinventario.idinventario=tblinventarioprecios.idinventario set precio=costobase/(1-(utilidad/100)) where true" + Filtros
            End If
            Comm.ExecuteNonQuery()

            Comm.CommandText = "update tblinventarioprecios inner join tblinventario on tblinventario.idinventario=tblinventarioprecios.idinventario set precio=precio+(precio*(iva/100))+(precio*(ieps/100))-(precio*(ivaretenido/100)) where precioneto=1 " + Filtros
            Comm.ExecuteNonQuery()
        End If
        If pTipo = 4 Or pTipo = 5 Then
            Comm.CommandText = "update tblinventarioprecios inner join tblinventario on tblinventario.idinventario=tblinventarioprecios.idinventario set precio=spdapreciolistauno(tblinventario.idinventario)-(spdapreciolistauno(tblinventario.idinventario)*(descuentoprecio/100)) where true" + Filtros + " and idlista<>1"
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub ReseteaTemp()
        Comm.CommandText = "update tblinventariopreciostemp set precio=0,comentario='',utilidad=0,descuentoprecio=0"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaPrecioListaUno(ByVal pIdInventario As Integer) As Double
        Comm.CommandText = "select ifnull((select precio from tblinventarioprecios where idlista=1 and idinventario=" + pIdInventario.ToString + " limit 1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaPrecioListaUnotEMP(ByVal pIdInventario As Integer) As Double
        Comm.CommandText = "select ifnull((select precio from tblinventariopreciostemp where idlista=1 and idinventario=" + pIdInventario.ToString + " limit 1),0)"
        Return Comm.ExecuteScalar
    End Function
End Class
