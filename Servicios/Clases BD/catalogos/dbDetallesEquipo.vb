Public Class dbDetallesEquipo
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public codigo As String
    Public descripcion As String
    Public nombre As String
    Public marca As String
    Public modelo As String
    Public noserie As String
    Public matricula As String
    Public nombreCliente As String
    Public direccionCliente As String
    Public direccion2 As String
    Public noexterior As String
    Public colonia As String
    Public ciudad As String
    Public estado As String
    Public pais As String
    Public rfc As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub
    Public Sub Guardar(ByVal pIdEquipo As Integer, ByVal pCantidad As Double, ByVal pIdInventariop As Integer, ByVal pVida As String, ByVal pFecha As String)
        Comm.CommandText = "insert into tbldetallesequipos(idEquipo,Cantidad,idInventario,TiempoVida,fecha,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + pIdEquipo.ToString + "," + pCantidad.ToString + "," + pIdInventariop.ToString + "," + pVida.ToString + ",'" + pFecha.ToString + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub GuardarSucursal(ByVal pIdEquipo As Integer, ByVal pCantidad As Double, ByVal pIdInventariop As Integer, ByVal pVida As String, ByVal pFecha As String)
        Comm.CommandText = "insert into tbldetallesequiposs(idEquipo,Cantidad,idInventario,TiempoVida,fecha,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + pIdEquipo.ToString + "," + pCantidad.ToString + "," + pIdInventariop.ToString + "," + pVida.ToString + ",'" + pFecha.ToString + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function detallesEquipo(ByVal pIdEquipo As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tbldetallesequipos where idEquipo='" + pIdEquipo.ToString + "'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldetallesequipos")
        Return DS.Tables("tbldetallesequipos")
    End Function
 
    Public Function detallesEquiposuc(ByVal pIdEquipo As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tbldetallesequiposs where idEquipo='" + pIdEquipo.ToString + "'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldetallesequipos")
        Return DS.Tables("tbldetallesequipos")
    End Function
    Public Function detallesEquipoSucursale(ByVal pIdEquipo As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tbldetallesequiposs where idEquipo='" + pIdEquipo.ToString + "'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldetallesequipos")
        Return DS.Tables("tbldetallesequipos")
    End Function
    Public Sub buscarArticulo(ByVal idProducto As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select clave,nombre from tblinventario where idinventario=" + idProducto.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            codigo = DReader("clave")
            descripcion = DReader("nombre")
        End If
        DReader.Close()

    End Sub
    Public Function buscarIDInventario(ByVal pIdEquipo As Integer, ByVal pidDetalle As Integer) As Integer
        Dim numero As Integer
        Comm.CommandText = "select idInventario from tbldetallesequipos where idEquipo='" + pIdEquipo.ToString + "' and idDetalles='" + pidDetalle.ToString() + "'"
        numero = Comm.ExecuteScalar
        Return numero
    End Function
    Public Function buscarIDInventarioSucursal(ByVal pIdEquipo As Integer, ByVal pidDetalle As Integer) As Integer
        Dim numero As Integer
        Comm.CommandText = "select idInventario from tbldetallesequiposs where idEquipo='" + pIdEquipo.ToString + "' and idDetalle='" + pidDetalle.ToString() + "'"
        numero = Comm.ExecuteScalar
        Return numero
    End Function

    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pIdInventariop As Integer, ByVal pVida As String)

        Comm.CommandText = "update tbldetallesequipos set Cantidad='" + pCantidad.ToString + "', idInventario='" + pIdInventariop.ToString() + "', TiempoVida='" + pVida.ToString() + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idDetalles=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarSucursal(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pIdInventariop As Integer, ByVal pVida As String)

        Comm.CommandText = "update tbldetallesequiposs set Cantidad='" + pCantidad.ToString + "', idInventario='" + pIdInventariop.ToString() + "', TiempoVida='" + pVida.ToString() + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH.mm:ss") + "' where idDetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbldetallesequipos where idDetalles=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarSucursal(ByVal pID As Integer)
        Comm.CommandText = "delete from tbldetallesequiposs where idDetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function cuantosConsumibles(ByVal pidEquipo As Integer, ByVal pidInventario As Integer) As Integer
        Dim Resultado As Integer

        Comm.CommandText = "select count(idevento) from tblserviciosinventario2 where idinventario=" + pidInventario.ToString() + " and idEquipo=" + pidEquipo.ToString()
        Resultado = Comm.ExecuteScalar

        Return Resultado
    End Function
    Public Function cuantosConsumiblessuc(ByVal pidEquipo As Integer, ByVal pidInventario As Integer) As Integer
        Dim Resultado As Integer

        Comm.CommandText = "select count(idevento) from tblserviciosinventario2suc where idinventario=" + pidInventario.ToString() + " and idEquipo=" + pidEquipo.ToString()
        Resultado = Comm.ExecuteScalar

        Return Resultado
    End Function

    Public Function consumibles(ByVal pidEquipo As Integer, ByVal pidInventario As Integer) As DataTable
        Dim DS As New DataSet

        Comm.CommandText = "select fecha,cantidad,idinventario from tblserviciosinventario2 where idinventario=" + pidInventario.ToString() + " and idEquipo=" + pidEquipo.ToString() + " Order by idserviciosinventario DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosinventario2")
        Return DS.Tables("tblserviciosinventario2")

    End Function
    Public Function consumiblessuc(ByVal pidEquipo As Integer, ByVal pidInventario As Integer) As DataTable
        Dim DS As New DataSet

        Comm.CommandText = "select fecha,cantidad,idinventario from tblserviciosinventario2suc where idinventario=" + pidInventario.ToString() + " and idEquipo=" + pidEquipo.ToString() + " Order by idserviciosinventario DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosinventario2")
        Return DS.Tables("tblserviciosinventario2")

    End Function

    Public Sub equipoDatos(ByVal pidEquipo As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select nombre,marca,modelo,noserie,matricula from tblclientesequipos where idEquipo=" + pidEquipo.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            nombre = DReader("nombre")
            marca = DReader("marca")
            modelo = DReader("modelo")
            noserie = DReader("noserie")
            matricula = DReader("matricula")
        End If

        DReader.Close()

    End Sub
    Public Sub equipoDatossuc(ByVal pidEquipo As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select nombre,marca,modelo,noserie,matricula from tblsucequipos where idEquipo=" + pidEquipo.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            nombre = DReader("nombre")
            marca = DReader("marca")
            modelo = DReader("modelo")
            noserie = DReader("noserie")
            matricula = DReader("matricula")
        End If

        DReader.Close()

    End Sub

    Public Sub clienteDatos(ByVal pidCliente As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select nombre,rfc,direccion,noexterior,colonia,ciudad,estado,pais from tblclientes where idcliente=" + pidCliente.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            nombreCliente = DReader("nombre")
            direccionCliente = DReader("direccion")
            noexterior = DReader("noexterior")
            colonia = DReader("colonia")
            ciudad = DReader("ciudad")
            estado = DReader("estado")
            rfc = DReader("rfc")
            pais = DReader("pais")
            direccionCliente = direccionCliente + " " + noexterior + " " + colonia
            direccion2 = ciudad + ", " + estado + ", " + pais

        End If

        DReader.Close()

    End Sub
    Public Sub clienteDatossuc(ByVal pidCliente As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select nombre,rfc,direccion,noexterior,colonia,ciudad,estado,pais from tblsucursales where idsucursal=" + pidCliente.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            nombreCliente = DReader("nombre")
            direccionCliente = DReader("direccion")
            noexterior = DReader("noexterior")
            colonia = DReader("colonia")
            ciudad = DReader("ciudad")
            estado = DReader("estado")
            rfc = DReader("rfc")
            pais = DReader("pais")
            direccionCliente = direccionCliente + " " + noexterior + " " + colonia
            direccion2 = ciudad + ", " + estado + ", " + pais

        End If

        DReader.Close()

    End Sub

End Class
