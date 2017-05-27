Imports MySql.Data.MySqlClient

Public Class AddendaData
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    'Dim connectionString As String = "server=localhost;user id=root; password=privado; database=addendaprueba"
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub
    ''' <summary>
    ''' Método que tiene por función guardar un artículo a una addenda
    ''' </summary>
    ''' <param name="articulo">Artículo que se va a vincular a la addenda</param>
    ''' <param name="idAddenda">Identificador de la addenda</param>
    ''' <returns>Una respesta tipo Boolean, falsa o verdadera si pudo o no guardar el registro</returns>
    ''' <remarks></remarks>
    Public Function guardarArticuloPorAddenda(ByVal articulo As Articulo, ByVal idAddenda As Integer) As Boolean
        'conexion = New MySqlConnection()
        Try
            'conexion.ConnectionString = connectionString
            'conexion.Open()

            'Dim Comm As MySqlCommand = conexion.CreateCommand()
            Comm.CommandType = CommandType.Text
            Comm.CommandText = "INSERT INTO tbladdendaoxxodetalles(`pedidoAdicional`,`remision`,`fechaEntrega`,`crTienda`,`nombreTienda`,`numProducto`,`descripcion`,`unidadMedida`,`cantidad`,`noSerieProductos`,`porcIva`,`montoIva`,`porcIeps1`,`montoIeps1`,`porcIeps2`,`montoIeps2`,`porcIeps3`,`montoIeps3`,`ImporteNeto`,`idAddenda` )VALUES('" & articulo.pedidoAdicional & "','" & articulo.remision & "','" & articulo.fechaEntrega & "','" & articulo.crTienda & "','" & articulo.nombreTienda & "','" & articulo.numProducto & "','" & articulo.descripcion & "','" & articulo.unidadMedida & "','" & articulo.cantidad & "','" & articulo.noSerieProductos & "','" & articulo.porcIva & "','" & articulo.montoIva & "','" & articulo.porcIeps1 & "','" & articulo.montoIeps1 & "','" & articulo.porcIeps2 & "','" & articulo.montoIeps2 & "','" & articulo.porcIeps3 & "','" & articulo.montoIeps3 & "','" & articulo.ImporteNeto & "', '" & idAddenda & "');"
            Comm.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            Return False
            'Finally
            '    If conexion.State = ConnectionState.Open Then
            '        conexion.Close()
            '    End If
        End Try
    End Function

    ''' <summary>
    ''' Método que tiene por función guardar una addenda
    ''' </summary>
    ''' <param name="addenda">Addenda a guardar</param>
    ''' <returns>Una respuesta tipo Boolean, falsa o verdadera si pudo o no guardar el registro</returns>
    ''' <remarks></remarks>
    Public Function guardarAddenda(ByVal addenda As AddendaOxxo, ByVal pidVenta As Integer) As Boolean

        'conexion = New MySqlConnection()
        Dim idAddenda As Integer

        Try
            'conexion.ConnectionString = connectionString
            'conexion.Open()

            'Dim Comm As MySqlCommand = conexion.CreateCommand()
            Comm.CommandType = CommandType.Text
            Comm.CommandText = "INSERT INTO tbladdendaoxxo (`noVersAdd`,`claseDoc`,`plaza`,`tipoProv`,`locType`,`folioPago`,`ordCompra`,`glnEmisor`,`glnReceptor`,`moneda`,`tipoCambio`,`cfdReferenciaSerie`,`cfdReferenciaFolio`,`montoDescuento0`,`tipoDescuento0`,`montoDescuento1`,`tipoDescuento1`,`montoDescuento2`,`tipoDescuento2`,`montoDescuento3`,`tipoDescuento3`,`importeTotal`,`tipoValidacion`,`fuenteNota`,idventa) VALUES('" & addenda.noVersAdd & "', '" & addenda.claseDoc & "','" & addenda.plaza & "','" & addenda.tipoProv & "','" & addenda.locType & "','" & addenda.folioPago & "','" & addenda.ordCompra & "','" & addenda.glnEmisor & "','" & addenda.glnReceptor & "','" & addenda.moneda & "','" & addenda.tipoCambio & "','" & addenda.cfdReferenciaSerie & "','" & addenda.cfdReferenciaFolio & "','" & addenda.montoDescuento0 & "','" & addenda.tipoDescuento0 & "','" & addenda.montoDescuento1 & "','" & addenda.tipoDescuento1 & "','" & addenda.montoDescuento2 & "','" & addenda.tipoDescuento2 & "','" & addenda.montoDescuento3 & "','" & addenda.tipoDescuento3 & "','" & addenda.importeTotal & "','" & addenda.tipoValidacion & "','" & addenda.fuenteNota & "'," + pidVenta.ToString + "); SELECT LAST_INSERT_ID();"

            idAddenda = Comm.ExecuteScalar()
            'Establecemos el identificador del registro recien insertado en la base de datos a una variable globar gIdAddenda
            gIdAddenda = idAddenda

            'Si la addenda tiene articulos, se registran 
            If addenda.articulos.Count > 0 Then
                For Each Articulo As Articulo In addenda.articulos
                    Comm.CommandType = CommandType.Text
                    Comm.CommandText = "INSERT INTO tbladdendaoxxodetalles(`pedidoAdicional`,`remision`,`fechaEntrega`,`crTienda`,`nombreTienda`,`numProducto`,`descripcion`,`unidadMedida`,`cantidad`,`noSerieProductos`,`porcIva`,`montoIva`,`porcIeps1`,`montoIeps1`,`porcIeps2`,`montoIeps2`,`porcIeps3`,`montoIeps3`,`ImporteNeto`,`idAddenda` )VALUES('" & Articulo.pedidoAdicional & "','" & Articulo.remision & "','" & Articulo.fechaEntrega & "','" & Articulo.crTienda & "','" & Articulo.nombreTienda & "','" & Articulo.numProducto & "','" & Articulo.descripcion & "','" & Articulo.unidadMedida & "','" & Articulo.cantidad & "','" & Articulo.noSerieProductos & "','" & Articulo.porcIva & "','" & Articulo.montoIva & "','" & Articulo.porcIeps1 & "','" & Articulo.montoIeps1 & "','" & Articulo.porcIeps2 & "','" & Articulo.montoIeps2 & "','" & Articulo.porcIeps3 & "','" & Articulo.montoIeps3 & "','" & Articulo.ImporteNeto & "', '" & idAddenda & "');"
                    Comm.ExecuteNonQuery()
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
            'Finally
            '    If conexion.State = ConnectionState.Open Then
            '        conexion.Close()
            '    End If
        End Try

    End Function

    ''' <summary>
    ''' Método que tiene por función actualizar un artículo
    ''' </summary>
    ''' <param name="articulo">Artículo el cual se va actualizar</param>
    ''' <returns>Un respuesta tipo Boolean, falsa o verdadera si pudo o no actualizar el registro</returns>
    ''' <remarks></remarks>
    Public Function actualizarArticulo(ByVal articulo As Articulo) As Boolean
        'conexion = New MySqlConnection()

        Try
            'conexion.ConnectionString = connectionString
            'conexion.Open()

            'Dim Comm As MySqlCommand = conexion.CreateCommand()
            Comm.CommandType = CommandType.Text
            Comm.CommandText = "UPDATE `tbladdendaoxxodetalles` SET`pedidoAdicional` = '" & articulo.pedidoAdicional & "',`remision` = '" & articulo.remision & "',`fechaEntrega` ='" & articulo.fechaEntrega & "',`crTienda` ='" & articulo.crTienda & "' ,`nombreTienda` ='" & articulo.nombreTienda & "' ,`numProducto` = '" & articulo.numProducto & "',`descripcion` ='" & articulo.descripcion & "',`unidadMedida` ='" & articulo.unidadMedida & "',`cantidad` ='" & articulo.cantidad & "' ,`noSerieProductos` = '" & articulo.noSerieProductos & "',`porcIva` = '" & articulo.porcIva & "',`montoIva` = '" & articulo.montoIva & "',`porcIeps1` = '" & articulo.porcIeps1 & "',`montoIeps1` ='" & articulo.montoIeps1 & "',`porcIeps2` ='" & articulo.porcIeps2 & "' ,`montoIeps2` ='" & articulo.montoIeps2 & "' ,`porcIeps3` ='" & articulo.porcIeps3 & "' ,`montoIeps3` ='" & articulo.montoIeps3 & "',`ImporteNeto` ='" & articulo.ImporteNeto & "'WHERE id ='" & articulo.Id & "'"
            Comm.ExecuteNonQuery()
            gIdArticulo = 0

            Return True
        Catch ex As Exception
            Return False
            'Finally
            '   If conexion.State = ConnectionState.Open Then
            'conexion.Close()
            'End If
        End Try

    End Function

    ''' <summary>
    ''' Método que tiene por función actualizar una addenda
    ''' </summary>
    ''' <param name="addenda">Addenda la cual se va actualizar</param>
    ''' <returns>Un respuesta tipo Boolea, falsa o verdadera si pudo o no actalizar el registro</returns>
    ''' <remarks></remarks>
    Public Function actualizarAddenda(ByVal addenda As AddendaOxxo) As Boolean
        'conexion = New MySqlConnection()

        Try
            'conexion.ConnectionString = connectionString
            'conexion.Open()

            ' Dim Comm As MySqlCommand = conexion.CreateCommand()
            Comm.CommandType = CommandType.Text
            Comm.CommandText = "UPDATE `tbladdendaoxxo` SET`noVersAdd` = '" & addenda.noVersAdd & "',`claseDoc` = '" & addenda.claseDoc & "',`plaza` = '" & addenda.plaza & "',`tipoProv` ='" & addenda.tipoProv & "',`locType` = '" & addenda.locType & "',`folioPago` = '" & addenda.folioPago & "' ,`ordCompra` = '" & addenda.ordCompra & "' ,`glnEmisor` = '" & addenda.glnEmisor & "' ,`glnReceptor` = '" & addenda.glnReceptor & "',`moneda` = '" & addenda.moneda & "',`tipoCambio` ='" & addenda.tipoCambio & "' ,`cfdReferenciaSerie` = '" & addenda.cfdReferenciaSerie & "',`cfdReferenciaFolio` ='" & addenda.cfdReferenciaFolio & "',`montoDescuento0` ='" & addenda.montoDescuento0 & "',`tipoDescuento0` ='" & addenda.tipoDescuento0 & "' ,`montoDescuento1` = '" & addenda.montoDescuento1 & "' ,`tipoDescuento1` ='" & addenda.tipoDescuento1 & "',`montoDescuento2` = '" & addenda.montoDescuento2 & "',`tipoDescuento2` = '" & addenda.tipoDescuento2 & "',`montoDescuento3` = '" & addenda.montoDescuento3 & "',`tipoDescuento3` ='" & addenda.tipoDescuento3 & "',`importeTotal` ='" & addenda.importeTotal & "',`tipoValidacion` = '" & addenda.tipoValidacion & "',`fuenteNota` ='" & addenda.fuenteNota & "' WHERE id ='" & addenda.Id & "'"
            Comm.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            Return False
            'Finally
            '   If conexion.State = ConnectionState.Open Then
            'conexion.Close()
            ' E'nd If
        End Try

    End Function

    ''' <summary>
    ''' Método que tiene por función obtener los articulos de una addenda en particular
    ''' </summary>
    ''' <param name="idAddenda">Identificador de la addenda</param>
    ''' <returns>Información de los articulos en una variable de tipo MySqlDataAdapter</returns>
    ''' <remarks></remarks>
    Public Function obtenerArticulosPorAddenda(ByVal idAddenda As Integer) As MySqlDataAdapter
        'conexion = New MySqlConnection()
        Dim da As MySqlDataAdapter = New MySqlDataAdapter()

        Try
            'conexion.ConnectionString = connectionString
            'conexion.Open()

            'Dim Comm As MySqlCommand = conexion.CreateCommand()
            Comm.CommandType = CommandType.Text
            Comm.CommandText = "select * from tbladdendaoxxodetalles where idAddenda = '" & idAddenda & "'"
            Comm.ExecuteNonQuery()

            da.SelectCommand = Comm

            Return da
        Catch ex As Exception
            Return da
            'Finally
            '   If conexion.State = ConnectionState.Open Then
            'conexion.Close()
            'End If
        End Try
    End Function

    ''' <summary>
    ''' Método que tiene por función obtener una addenda con todos sus articulos 
    ''' </summary>
    ''' <param name="idVenta">Identificador de la addenda a buscar con sus articulos</param>
    ''' <returns>Un objeto tipo AddendaOxxo</returns>
    ''' <remarks></remarks>
    Public Function obtenerAddendaConArticulos(ByVal idVenta As Integer) As AddendaOxxo
        'conexion = New MySqlConnection()
        Dim dr As MySqlDataReader = Nothing
        Dim addenda As New AddendaOxxo()
        Dim idAddenda As Integer
        Try
            'conexion.ConnectionString = connectionString
            'conexion.Open()

            'Dim Comm As MySqlCommand = conexion.CreateCommand()
            Comm.CommandType = CommandType.Text
            Comm.CommandText = "select * from tbladdendaoxxo where idventa = " + idVenta.ToString
            dr = Comm.ExecuteReader()



            If dr.HasRows Then
                Do While dr.Read()
                    addenda.noVersAdd = dr("noVersAdd")
                    addenda.claseDoc = dr("claseDoc")
                    addenda.plaza = dr("plaza")
                    addenda.tipoProv = dr("tipoProv")
                    addenda.locType = dr("locType")
                    addenda.folioPago = dr("folioPago")
                    addenda.ordCompra = dr("ordCompra")
                    addenda.glnEmisor = dr("glnEmisor")
                    addenda.glnReceptor = dr("glnReceptor")
                    addenda.moneda = dr("moneda")
                    addenda.tipoCambio = dr("tipoCambio")
                    addenda.cfdReferenciaSerie = dr("cfdReferenciaSerie")
                    addenda.cfdReferenciaFolio = dr("cfdReferenciaFolio")
                    addenda.montoDescuento0 = dr("montoDescuento0")
                    addenda.tipoDescuento0 = dr("tipoDescuento0")
                    addenda.montoDescuento1 = dr("montoDescuento1")
                    addenda.tipoDescuento1 = dr("tipoDescuento1")
                    addenda.montoDescuento2 = dr("montoDescuento2")
                    addenda.tipoDescuento2 = dr("tipoDescuento2")
                    addenda.montoDescuento3 = dr("montoDescuento3")
                    addenda.tipoDescuento3 = dr("tipoDescuento3")
                    addenda.importeTotal = dr("importeTotal")
                    addenda.tipoValidacion = dr("tipoValidacion")
                    addenda.fuenteNota = dr("fuenteNota")
                    addenda.Id = dr("id")
                    idAddenda = dr("id")
                Loop
            End If

            dr.Close()
            Comm.CommandType = CommandType.Text
            Comm.CommandText = "select * from tbladdendaoxxodetalles where idAddenda = " & idAddenda.ToString
            dr = Comm.ExecuteReader()

            If dr.HasRows Then
                Do While dr.Read()
                    Dim articulo As New Articulo()
                    articulo.pedidoAdicional = dr("pedidoAdicional")
                    articulo.remision = dr("remision")
                    articulo.fechaEntrega = dr("fechaEntrega")
                    articulo.crTienda = dr("crTienda")
                    articulo.nombreTienda = dr("nombreTienda")
                    articulo.numProducto = dr("numProducto")
                    articulo.descripcion = dr("descripcion")
                    articulo.unidadMedida = dr("unidadMedida")
                    articulo.cantidad = dr("cantidad")
                    articulo.noSerieProductos = dr("noSerieProductos")
                    articulo.porcIva = dr("porcIva")
                    articulo.montoIva = dr("montoIva")
                    articulo.porcIeps1 = dr("porcIeps1")
                    articulo.montoIeps1 = dr("montoIeps1")
                    articulo.porcIeps2 = dr("porcIeps2")
                    articulo.montoIeps2 = dr("montoIeps2")
                    articulo.porcIeps3 = dr("porcIeps3")
                    articulo.montoIeps3 = dr("montoIeps3")
                    articulo.ImporteNeto = dr("ImporteNeto")
                    addenda.articulos.Add(articulo)
                Loop
            End If
            Return addenda
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Return addenda
        Finally
            dr.Close()
            'If conexion.State = ConnectionState.Open Then
            'conexion.Close()
            ' End If

        End Try
    End Function





End Class
