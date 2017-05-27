Imports MySql.Data.MySqlClient
Public Class dbDescuentos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub


    Public Sub Guardar(ByVal activado As Boolean, ByVal descuento As String, ByVal descripcion As String, ByVal fechai As String, ByVal horai As String, ByVal fechaf As String, ByVal horaf As String, ByVal productos As String, ByVal tipo As String, ByVal pFolio As Integer)
        Comm.CommandText = "insert into tbldescuentos (activado, descuento, descripcion, fechai, horai, fechaf, horaf, productos,tipoDescuento,Folio,FechaInicioC,FechaFinalC) values(" + activado.ToString() + " , '" + descuento.ToString() + "' ,'" + Replace(descripcion, "'", "''") + "','" + Replace(fechai, "'", "''") + "','" + Replace(horai, "'", "''") + "','" + Replace(fechaf, "'", "''") + "','" + Replace(horaf, "'", "''") + "','" + Replace(productos, "'", "''") + "','" + Replace(tipo, "'", "''") + "','" + pFolio.ToString() + "','" + fechai + " " + horai + "','" + fechaf + " " + horaf + "');"
        Comm.ExecuteNonQuery()

    End Sub
    Public Sub Guardar2(ByVal activado As Boolean, ByVal descuento As String, ByVal descripcion As String, ByVal fechai As String, ByVal horai As String, ByVal fechaf As String, ByVal horaf As String, ByVal DataGridview1 As DataGridView, ByVal tipo As String, ByVal pFolio As Integer, ByVal psucursal As Integer)
        MySqlcom.Transaction = MySqlcom.Connection.BeginTransaction()
        Dim r As DataGridViewRow
        For Each r In DataGridview1.Rows
            Comm.CommandText = "insert into tbldescuentos (activado, descuento, descripcion, fechai, horai, fechaf, horaf, productos,tipoDescuento,Folio,FechaInicioC,FechaFinalC,sucursal) values(" + activado.ToString() + " , '" + descuento.ToString() + "' ,'" + Replace(descripcion, "'", "''") + "','" + Replace(fechai, "'", "''") + "','" + Replace(horai, "'", "''") + "','" + Replace(fechaf, "'", "''") + "','" + Replace(horaf, "'", "''") + "','" + Replace(r.Cells(1).Value, "'", "''") + "','" + Replace(tipo, "'", "''") + "','" + pFolio.ToString() + "','" + fechai + " " + horai + "','" + fechaf + " " + horaf + "'," + psucursal.ToString + ");"
            Comm.ExecuteNonQuery()
        Next
        MySqlcom.Transaction.Commit()
    End Sub

    Public Sub Modificar(ByVal pId As Integer, ByVal activado As Boolean, ByVal descuento As String, ByVal descripcion As String, ByVal fechai As String, ByVal horai As String, ByVal fechaf As String, ByVal horaf As String, ByVal productos As String, ByVal tipo As String)
        'Modifica
        Comm.CommandText = "update tbldescuentos set activado=" + activado.ToString() + " , descuento='" + descuento.ToString() + "' ,descripcion='" + Replace(descripcion, "'", "''") + "', fechai='" + Replace(fechai, "'", "''") + "', horai='" + Replace(horai, "'", "''") + "', fechaf='" + Replace(fechaf, "'", "''") + "' ,horaf='" + Replace(horaf, "'", "''") + "', productos='" + Replace(productos, "'", "''") + "', tipoDescuento='" + Replace(tipo, "'", "''") + "', FechaInicioC=" + fechai.ToString() + " " + horai.ToString() + "', FechaFinalC=" + fechaf.ToString() + " " + horaf.ToString() + "' where idDescuento=" + pId.ToString()
        Comm.ExecuteNonQuery()
    End Sub

    Public Function filtroDescuentos(ByVal descuento As String, ByVal pIdSucursal As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tbldescuentos where descripcion LIKE '%" + descuento + "%'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " group by Folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldescuentos")
        Return DS.Tables("tbldescuentos").DefaultView
    End Function
    Public Function Productos(ByVal pFolio As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select productos from tbldescuentos where Folio=" + pFolio.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldescuentos")
        Return DS.Tables("tbldescuentos").DefaultView
    End Function
    Public Sub Eliminar(ByVal pId As Integer)
        'Eliminar
        Comm.CommandText = "delete from tbldescuentos where Folio=" + pId.ToString()
        Comm.ExecuteNonQuery()
    End Sub

    Public Function ClaveRepetida(ByVal pCodigo As Integer) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(Folio) from tbldescuentos where Folio=" + Replace(pCodigo, "'", "''")
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ClaveRepetida = 0
        Else
            ClaveRepetida = 1
        End If
    End Function


    Public Function Folio() As Integer
        Dim Resultado As Integer
        ' Dim Rep As String
        Dim repetida As Integer

        Resultado = 1

        repetida = ClaveRepetida(Resultado)

        If repetida > 0 Then 'es repetida

            Do While repetida > 0
                Resultado = Resultado + 1
                repetida = ClaveRepetida(Resultado)

            Loop
            Return Resultado
        Else
            Return Resultado
        End If
    End Function

    '************Aplicar descuentos
    'Verifica si entra en alñgun descuento, si no encuentra regresa cero si encunetra regresa el id
    Public Function HayDescuento(ByVal pIdProducto As Integer, ByVal pFecha As String, ByVal pIdSucursal As Integer) As Integer 'Solo para saber si tiene
        Dim id As Integer = 0 'en caso de que no se encuentre el Folio, se vaa  regresar cero
        Dim Resultado As Integer = 0
        ' Comm.CommandText = "select count(Folio) from tbldescuentos where productos='" + pIdProducto.ToString() + "' and DATE(STR_TO_DATE('" + pFecha.ToString() + "', '%d/%m/%Y %h:%i:%s %p')) between STR_TO_DATE(FechaInicioC, '%d/%m/%Y' '%h:%i:%s %p') and STR_TO_DATE(FechaFinalC, '%d/%m/%Y' '%h:%i:%s %p');"
        Comm.CommandText = "select count(Folio) from tbldescuentos where productos='" + pIdProducto.ToString() + "' and '" + pFecha.ToString() + "' between FechaInicioC and FechaFinalC and activado=1 and (sucursal=" + pIdSucursal.ToString + " or sucursal=0)"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            id = 0
        Else 'Si se encontraron coincidencias, buscar el folio
            Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
            Comm.CommandText = "select idDescuento from tbldescuentos where productos='" + pIdProducto.ToString() + "' and '" + pFecha.ToString() + "' between FechaInicioC and FechaFinalC and (sucursal=" + pIdSucursal.ToString + " or sucursal=0)"
            'Comm.CommandText = "select idDescuento from tbldescuentos where productos=" + pIdProducto.ToString() + " and DATE(STR_TO_DATE('" + pFecha.ToString() + "', '%d/%m/%Y %h:%i:%s %p')) between STR_TO_DATE(FechaInicioC, '%d/%m/%Y' '%h:%i:%s %p') and STR_TO_DATE(FechaFinalC, '%d/%m/%Y' '%h:%i:%s %p');"
            '"select Folio from tbldescuentos where productos=" + pIdProducto.ToString() + "and DATE(STR_TO_DATE('" + pFecha.ToString() + "', '%d/%m/%Y %h:%i:%s %p')) between STR_TO_DATE(FechaInicioC, '%d/%m/%Y' '%h:%i:%s %p') and STR_TO_DATE(FechaFinalC, '%d/%m/%Y' '%h:%i:%s %p');"
            DReader = Comm.ExecuteReader
            If DReader.Read() Then

                id = Integer.Parse(DReader("idDescuento").ToString())

            End If
            DReader.Close()

        End If

        Return id
    End Function
    'asegurarse que no se empalmen las fechas
    Public Function HayDescuento(ByVal pIdProducto As Integer, ByVal pFechaI As String, ByVal pFechaf As String, ByVal pIdSucursal As Integer) As Integer 'Solo para saber si tiene
        Dim coincidencias As Integer = 0 'en caso de que no se encuentre el Folio, se vaa  regresar cero
        Dim Resultado1 As Integer = 0
        Dim Resultado2 As Integer = 0
        Dim Resultado3 As Integer = 0
        Dim Resultado4 As Integer = 0
        Comm.CommandText = "select count(Folio) from tbldescuentos where productos='" + pIdProducto.ToString() + "' and DATE(STR_TO_DATE('" + pFechaI.ToString() + "', '%d/%m/%Y %h:%i:%s %p')) between STR_TO_DATE(FechaInicioC, '%d/%m/%Y' '%h:%i:%s %p') and STR_TO_DATE(FechaFinalC, '%d/%m/%Y' '%h:%i:%s %p') and (sucursal=" + pIdSucursal.ToString + " or sucursal=0);"
        Resultado1 = Comm.ExecuteScalar
        Comm.CommandText = "select count(Folio) from tbldescuentos where productos='" + pIdProducto.ToString() + "' and DATE(STR_TO_DATE('" + pFechaf.ToString() + "', '%d/%m/%Y %h:%i:%s %p')) between STR_TO_DATE(FechaInicioC, '%d/%m/%Y' '%h:%i:%s %p') and STR_TO_DATE(FechaFinalC, '%d/%m/%Y' '%h:%i:%s %p') and (sucursal=" + pIdSucursal.ToString + " or sucursal=0);"
        Resultado2 = Comm.ExecuteScalar

        Comm.CommandText = "select count(Folio) from tbldescuentos where productos='" + pIdProducto.ToString() + "' and DATE(STR_TO_DATE(FechaInicioC, '%d/%m/%Y %h:%i:%s %p')) between STR_TO_DATE('" + pFechaI.ToString() + "', '%d/%m/%Y' '%h:%i:%s %p') and STR_TO_DATE('" + pFechaf.ToString() + "', '%d/%m/%Y' '%h:%i:%s %p') and (sucursal=" + pIdSucursal.ToString + " or sucursal=0);"
        Resultado3 = Comm.ExecuteScalar
        Comm.CommandText = "select count(Folio) from tbldescuentos where productos='" + pIdProducto.ToString() + "' and DATE(STR_TO_DATE(FechaFinalC, '%d/%m/%Y %h:%i:%s %p')) between STR_TO_DATE('" + pFechaI.ToString() + "', '%d/%m/%Y' '%h:%i:%s %p') and STR_TO_DATE('" + pFechaf.ToString() + "', '%d/%m/%Y' '%h:%i:%s %p') and (sucursal=" + pIdSucursal.ToString + " or sucursal=0);"
        Resultado4 = Comm.ExecuteScalar


        If Resultado1 = 0 And Resultado2 = 0 And Resultado3 = 0 And Resultado4 = 0 Then
            coincidencias = 0
        Else
            coincidencias = 1
        End If

        Return coincidencias
    End Function

    Public Function Descuento(ByVal pFolio As Integer) As DataTable
        Dim Tabla As DataTable
        Comm.CommandText = "select * from tbldescuentos where Folio=" + pFolio.ToString()
        Tabla = Comm.ExecuteScalar
        Return Tabla
    End Function










    '**********************************tblVentasdesc**********************************
    Public Function GuardarDesc(ByVal idVenta As Integer, ByVal idDesc As Integer, ByVal folioVenta As Integer, ByVal perte As String) As Integer
        Dim id As Integer
        Comm.CommandText = "insert into tblventasdesc (idVenta, idDescuento, folioVenta, perteneciente,perte) values(" + idVenta.ToString() + " , " + idDesc.ToString() + " , " + folioVenta.ToString() + " , " + (idVenta - 1).ToString() + " , '" + perte.ToString() + "');"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "Select last_insert_id();"
        id = Comm.ExecuteScalar
        Return id
        'checar si devulve el ultimo id insertado
    End Function
    'Public Sub EliminarDesc(ByVal pId As Integer)
    '    'Eliminar descuento de ventas
    '    Comm.CommandText = "delete from tblventasdesc where idVentasDesc=" + pId.ToString()
    '    Comm.ExecuteNonQuery()
    'End Sub
    Public Function tablaDesc(ByVal idDesc As Integer) As DataTable

        Dim DS As New DataSet
        Comm.CommandText = "Select * from  tbldescuentos where idDescuento=" + idDesc.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldescuentos")
        Return DS.Tables("tbldescuentos")

    End Function

    'metodo para saber si el campo que se modificó tenia descuento, si tenia devolver el numero de id de VentasInventario para modificarlo
    Public Function descModificar(ByVal idVentaPerte As Integer, ByVal perte As String)
        Dim id As Integer
        Comm.CommandText = "Select idVenta from tblventasdesc where perteneciente=" + idVentaPerte.ToString() + " and perte='" + perte.ToString() + "'"
        id = Comm.ExecuteScalar
        'If id = "" Then
        '    id = 0
        'End If
        Return id
    End Function

    Public Sub ModificarDescuento(ByVal idVenta As Integer, ByVal idDesc As Integer, ByVal folioVenta As Integer, ByVal perte As String)
        Dim id As Integer
        Comm.CommandText = "Select idVentasDesc from tblventasdesc where idVenta=" + idVenta.ToString() + " and perte='" + perte.ToString() + "'"
        id = Comm.ExecuteScalar
        'Comm.CommandText = "update tblelabora set Elabora='" + Replace(pElabora, "'", "''") + "', Autoriza='" + Replace(pAutoriza, "'", "''") + "',Registra='" + Replace(pRegistra, "'", "''") + "'"

        Comm.CommandText = "update tblventasdesc set idDescuento=" + idDesc.ToString() + ", folioVenta=" + folioVenta.ToString() + ", perteneciente=" + (idVenta - 1).ToString() + " where idVentasDesc=" + id.ToString()

        Comm.ExecuteNonQuery()
    End Sub

    Public Sub EliminarDesc(ByVal pIdVenta As Integer, ByVal perte As String)
        Comm.CommandText = "delete from tblventasdesc where idVenta=" + pIdVenta.ToString + " and perte='" + perte.ToString() + "'"
        Comm.ExecuteNonQuery()
    End Sub

    '***********************promociones*******************

    'metodo de buscar todos los articulos que sean de la misma promocion y misma venta
    Public Function buscarPromociones(ByVal folioVenta As Integer, ByVal idDesc As Integer, ByVal idproducto As Integer) As DataTable
        'Dim dt As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "Select * from  tbldescpromociones where folioVenta=" + folioVenta.ToString() + " and folioDescuento=" + idDesc.ToString() + " and idProducto=" + idproducto.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldescpromociones")
        Return DS.Tables("tbldescpromociones")
    End Function

    Public Sub guardarPromocion(ByVal folioVenta As Integer, ByVal idDesc As Integer, ByVal idProducto As Integer, ByVal renglon As Integer)
        Comm.CommandText = "insert into tbldescpromociones (folioVenta, folioDescuento,idProducto, renglon) values(" + folioVenta.ToString() + " , " + idDesc.ToString() + " , " + idProducto.ToString() + " , " + renglon.ToString() + ");"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarDesc(ByVal folioVenta As Integer, ByVal idDesc As Integer, ByVal idProducto As Integer)
        Comm.CommandText = "delete from tbldescpromociones where folioVenta=" + folioVenta.ToString() + " and folioDescuento=" + idDesc.ToString() + " and idProducto=" + idProducto.ToString()
        Comm.ExecuteNonQuery()
    End Sub
    Public Function BuscarTipo(ByVal pIdDesc As Integer) As String
        Dim Resultado As String = ""
        Comm.CommandText = "select tipoDescuento from tbldescuentos where idDescuento=" + pIdDesc.ToString
        Resultado = Comm.ExecuteScalar
        If Resultado = Nothing Then
            Resultado = "nada"
        End If
        Return Resultado
    End Function
   

    'Limpiar
    Public Sub limpiarVentasdesc()
        Comm.CommandText = "delete from tblventasdesc "
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub limpiarDescPromociones()
        Comm.CommandText = "delete from tbldescpromociones "
        Comm.ExecuteNonQuery()
    End Sub


    'otros
    Public Sub EliminarDescAnadidos(ByVal folioVenta As Integer, ByVal descripcion As String)
        Comm.CommandText = "delete from tblventasinventario where idventa=" + folioVenta.ToString() + " and descripcion='" + descripcion.ToString() + "'"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function buscarDesAnadidos(ByVal folioVenta As Integer, ByVal idInventario As Integer) As DataTable
        'Dim dt As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "Select * from  tblventasinventario where idventa=" + folioVenta.ToString() + " and idinventario=" + idInventario.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasinventario")
        Return DS.Tables("tblventasinventario")

    End Function

    'otros para remisiones
    Public Sub EliminarDescAnadidosRem(ByVal idRemision As Integer, ByVal descripcion As String)
        Comm.CommandText = "delete from tblventasremisionesinventario where idremision=" + idRemision.ToString() + " and descripcion='" + descripcion.ToString() + "'"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function buscarDesAnadidosRem(ByVal idRemision As Integer, ByVal idInventario As Integer) As DataTable
        'Dim dt As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "Select * from  tblventasremisionesinventario where idremision=" + idRemision.ToString() + " and idinventario=" + idInventario.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasremisionesinventario")
        Return DS.Tables("tblventasremisionesinventario")

    End Function

    'otros para Pedidos
    Public Sub EliminarDescAnadidosPed(ByVal idPedido As Integer, ByVal descripcion As String)
        Comm.CommandText = "delete from tblventaspedidosinventario where idpedido=" + idPedido.ToString() + " and descripcion='" + descripcion.ToString() + "'"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function buscarDesAnadidosPed(ByVal idPedido As Integer, ByVal idInventario As Integer) As DataTable
        'Dim dt As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "Select * from  tblventaspedidosinventario where idpedido=" + idPedido.ToString() + " and idinventario=" + idInventario.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspedidosinventario")
        Return DS.Tables("tblventaspedidosinventario")

    End Function

    'otros para cotizaciones
    'otros para Pedidos
    Public Sub EliminarDescAnadidosCot(ByVal idCotizacion As Integer, ByVal descripcion As String)
        Comm.CommandText = "delete from tblventascotizacionesinventario where idcotizacion=" + idCotizacion.ToString() + " and descripcion='" + descripcion.ToString() + "'"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function buscarDesAnadidosCot(ByVal idCotizacion As Integer, ByVal idInventario As Integer) As DataTable
        'Dim dt As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "Select * from  tblventascotizacionesinventario where idcotizacion=" + idCotizacion.ToString() + " and idinventario=" + idInventario.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventascotizacionesinventario")
        Return DS.Tables("tblventascotizacionesinventario")

    End Function


    Public Function busqNombreProducto(ByVal idInv As Integer) As String
        Dim id As String
        Comm.CommandText = "Select nombre from tblinventario where idinventario=" + idInv.ToString()
        id = Comm.ExecuteScalar
        Return id
    End Function

    Public Function PrecioPromRemi(ByVal pidRemision As Integer, ByVal pidInventario As Integer) As Double
        'Dim dt As DataTable
        Dim cant As Double = 0
        Dim precio As Double = 0
        Dim prom As Double = 0
        Dim DS As New DataSet
        Dim tabla As DataTable
        Comm.CommandText = "Select * from  tblventasremisionesinventario where idremision=" + pidRemision.ToString() + " and idinventario=" + pidInventario.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasremisionesinventario")
        tabla = DS.Tables("tblventasremisionesinventario")

        For i As Integer = 0 To tabla.Rows.Count() - 1
            cant = cant + tabla.Rows(i)(3)
            precio = precio + tabla.Rows(i)(4)
        Next
        prom = precio / cant
        Return prom
    End Function


    Public Function PrecioPromPedido(ByVal pidRemision As Integer, ByVal pidInventario As Integer) As Double
        'Dim dt As DataTable
        Dim cant As Double = 0
        Dim precio As Double = 0
        Dim prom As Double = 0
        Dim DS As New DataSet
        Dim tabla As DataTable
        Comm.CommandText = "Select * from  tblventaspedidosinventario where idpedido=" + pidRemision.ToString() + " and idinventario=" + pidInventario.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspedidosinventario")
        tabla = DS.Tables("tblventaspedidosinventario")

        For i As Integer = 0 To tabla.Rows.Count() - 1
            cant = cant + tabla.Rows(i)(4).ToString()
            precio = precio + tabla.Rows(i)(5).ToString()
        Next
        prom = precio / cant
        Return prom
    End Function

    Public Function PrecioPromCot(ByVal pidRemision As Integer, ByVal pidInventario As Integer) As Double
        'Dim dt As DataTable
        Dim cant As Double = 0
        Dim precio As Double = 0
        Dim prom As Double = 0
        Dim DS As New DataSet
        Dim tabla As DataTable
        Comm.CommandText = "Select * from  tblventascotizacionesinventario where idcotizacion=" + pidRemision.ToString() + " and idinventario=" + pidInventario.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventascotizacionesinventario")
        ' Return DS.Tables("tblventasremisionesinventario")
        tabla = DS.Tables("tblventascotizacionesinventario")
        For i As Integer = 0 To tabla.Rows.Count() - 1
            cant = cant + tabla.Rows(i)(4).ToString()
            precio = precio + tabla.Rows(i)(5).ToString()
        Next
        prom = precio / cant
        Return prom
    End Function


    'Buscar nombre
    Public Function buscarnombreInventarioCot(ByVal idcot As Integer) As String
        Dim id As Integer
        Dim nombre As String
        Comm.CommandText = "Select idinventario from tblventascotizacionesinventario where iddetalle=" + idcot.ToString()
        id = Comm.ExecuteScalar
        Comm.CommandText = "Select nombre from tblinventario where idinventario=" + id.ToString()
        nombre = Comm.ExecuteScalar
        Return nombre
    End Function
    Public Function buscarnombreInventarioPed(ByVal idPedido As Integer) As String
        Dim id As Integer
        Dim nombre As String
        Comm.CommandText = "Select idinventario from tblventaspedidosinventario where iddetalle=" + idPedido.ToString()
        id = Comm.ExecuteScalar
        Comm.CommandText = "Select nombre from tblinventario where idinventario=" + id.ToString()
        nombre = Comm.ExecuteScalar
        Return nombre
    End Function
    Public Function buscarnombreInventarioRem(ByVal idRem As Integer) As String
        Dim id As Integer
        Dim nombre As String
        Comm.CommandText = "Select idinventario from tblventasremisionesinventario where iddetalle=" + idRem.ToString()
        id = Comm.ExecuteScalar
        Comm.CommandText = "Select nombre from tblinventario where idinventario=" + id.ToString()
        nombre = Comm.ExecuteScalar
        Return nombre
    End Function

    Public Function buscarPromocionesAnadidasCot(ByVal descripcion As String, ByVal pID As Integer) As DataTable
        'Dim dt As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "Select * from  tblventascotizacionesinventario where descripcion='" + descripcion.ToString() + "' and idcotizacion=" + pID.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventascotizacionesinventario")
        Return DS.Tables("tblventascotizacionesinventario")
    End Function
    Public Function buscarPromocionesAnadidasPed(ByVal descripcion As String, ByVal pID As Integer) As DataTable
        'Dim dt As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "Select * from  tblventaspedidosinventario where descripcion='" + descripcion.ToString() + "' and idpedido=" + pID.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspedidosinventario")
        Return DS.Tables("tblventaspedidosinventario")
    End Function
    Public Function buscarPromocionesAnadidasRem(ByVal descripcion As String, ByVal pID As Integer) As DataTable
        'Dim dt As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "Select * from  tblventasremisionesinventario where descripcion='" + descripcion.ToString() + "' and idremision=" + pID.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasremisionesinventario")
        Return DS.Tables("tblventasremisionesinventario")
    End Function
    Public Sub cambiarPrecioProm(ByVal pID As Integer, ByVal precio As Double, ByVal nTabla As String)
        'Dim dt As DataTable
        Comm.CommandText = "update " + nTabla + " set precio=" + precio.ToString() + " where iddetalle=" + pID.ToString()
        Comm.ExecuteNonQuery()
    End Sub

    Public Function consultaDescuentos(ByVal pFecha As String, ByVal desc As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "Select t1.idDescuento,t2.nombre, t1.tipoDescuento,t1.descuento,t1.descripcion,  t1.FechaInicioC, t1.FechaFinalC from  tbldescuentos t1 INNER JOIN tblinventario t2 ON (t1.productos=t2.idinventario) where '" + pFecha + "' between t1.FechaInicioC and t1.FechaFinalC and t1.activado=1 and t2.nombre like '%" + Replace(desc, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasremisionesinventario")
        Return DS.Tables("tblventasremisionesinventario")

    End Function
End Class
