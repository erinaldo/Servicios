Public Class dbInventarioDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public IdInventariop As Integer
    'Public Inventario As dbInventario
    'Public Moneda As dbMonedas
    Public Cantidad As Double
    'Public Precio As Double
    'Public IdMoneda As Integer
    'Public IdCompra As Integer
    'Public IdAlmacen As Integer
    'Public NuevoConcepto As Boolean
    'Public Iva As Double
    'Public Extra As String
    'Public Descuento As Double
    'Public Surtido As Double
    'Public CostoIndirecto As Double
    'Public DeRemision As Byte
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdInventariop = 0
        'IdMoneda = 0
        'Precio = 0
        'IdCompra = 0
        'IdAlmacen = 0
        'Iva = 0
        'Extra = 0
        'Descuento = 0
        'Surtido = 0
        ' DeRemision = 0
        'NuevoConcepto = False
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblinventariodetalles where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            'Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdInventariop = DReader("idinventariop")
            'IdMoneda = DReader("idmoneda")
            'IdCompra = DReader("idcompra")
            'IdAlmacen = DReader("idalmacen")
            'Iva = DReader("iva")
            'Extra = DReader("extra")
            'Descuento = DReader("descuento")
            'Surtido = DReader("surtido")
            'CostoIndirecto = DReader("costoindirecto")
            'DeRemision = DReader("deremision")
        End If
        DReader.Close()
        'Inventario = New dbInventario(Idinventario, Comm.Connection)
        'Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pIdInventariop As Integer)
        Idinventario = pIdinventario
        Cantidad = pCantidad
        IdInventariop = pIdInventariop
        Comm.CommandText = "insert into tblinventariodetalles(idinventario,cantidad,idinventariop) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + IdInventariop.ToString + ")"
        Comm.ExecuteNonQuery()
        'Comm.CommandText = "select if(max(iddetalle) is null,0,max(iddetalle)) from tblcomprasdetalles"
        'ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double)
        ID = pID
        Cantidad = pCantidad
        Comm.CommandText = "update tblinventariodetalles set cantidad=" + Cantidad.ToString + " where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblinventariodetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdcompra As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblinventariodetalles.iddetalle,tblinventariodetalles.cantidad,tblinventario.clave,tblinventario.nombre from tblinventariodetalles inner join tblinventario on tblinventariodetalles.idinventario=tblinventario.idinventario where tblinventariodetalles.idinventariop=" + pIdcompra.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventariodetalles")
        Return DS.Tables("tblinventariodetalles").DefaultView
    End Function
    Public Function ConsultaAgregadosVentas(ByVal pIddetalle As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventaskits.iddetallekit,tblventaskits.cantidad,tblinventario.clave,tblinventario.nombre from tblventaskits inner join tblinventario on tblventaskits.idinventario=tblinventario.idinventario where tblventaskits.iddetalle=" + pIddetalle.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaskits")
        Return DS.Tables("tblventaskits").DefaultView
    End Function
    Public Function ConsultaAgregadosRemisiones(ByVal pIddetalle As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventaskits.iddetallekit,tblventaskits.cantidad,tblinventario.clave,tblinventario.nombre from tblventaskitsr as tblventaskits inner join tblinventario on tblventaskits.idinventario=tblinventario.idinventario where tblventaskits.iddetalle=" + pIddetalle.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaskits")
        Return DS.Tables("tblventaskits").DefaultView
    End Function
    Public Function ConsultaAgregadosPedidos(ByVal pIddetalle As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventaskits.iddetallekit,tblventaskits.cantidad,tblinventario.clave,tblinventario.nombre from tblventaskitsp as tblventaskits inner join tblinventario on tblventaskits.idinventario=tblinventario.idinventario where tblventaskits.iddetalle=" + pIddetalle.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaskits")
        Return DS.Tables("tblventaskits").DefaultView
    End Function
    Public Function ConsultaAgregadosCotizaciones(ByVal pIddetalle As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventaskits.iddetallekit,tblventaskits.cantidad,tblinventario.clave,tblinventario.nombre from tblventaskitsc as tblventaskits inner join tblinventario on tblventaskits.idinventario=tblinventario.idinventario where tblventaskits.iddetalle=" + pIddetalle.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaskits")
        Return DS.Tables("tblventaskits").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pidCompra As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tblinventario.nombre as descripcion,tvi.cantidad,tvi.idinventario from tblinventariodetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario where tvi.idinventariop=" + pidCompra.ToString
        Return Comm.ExecuteReader
    End Function
End Class
