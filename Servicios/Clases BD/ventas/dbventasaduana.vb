Public Class dbventasaduana
    Public ID As Integer
    'Public Idinventario As Integer
    Public Iddetalle As Integer
    'Public Inventario As dbInventario
    'Public Moneda As dbMonedas
    'Public Cantidad As Double
    Public Numero As String
    Public Fecha As String
    Public Aduana As String
    Public YValidacion As String
    Public ClaveAduana As String
    Public Patente As String
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
        'Idinventario = 0
        'Cantidad = 0
        Iddetalle = 0
        Numero = ""
        Fecha = ""
        Aduana = ""
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
    Public Sub Reset()
        Numero = ""
        Fecha = ""
        Aduana = ""
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventasaduana where idaduana=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Numero = DReader("numero")
            Iddetalle = DReader("iddetalle")
            Fecha = DReader("fecha")
            Aduana = DReader("aduana")
        End If
        DReader.Close()
        'Inventario = New dbInventario(Idinventario, Comm.Connection)
        'Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIddetalle As Integer, ByVal pNumero As String, ByVal pFecha As String, ByVal pAduana As String)
        'Idinventario = pIdinventario
        'Cantidad = pCantidad
        'Iddetalle = pIdInventariop
        Comm.CommandText = "insert into tblventasaduana(iddetalle,numero,fecha,aduana) values(" + pIddetalle.ToString + ",'" + Replace(pNumero, "'", "''") + "','" + pFecha + "','" + Replace(pAduana, "'", "''") + "')"
        Comm.ExecuteNonQuery()
        'Comm.CommandText = "select if(max(iddetalle) is null,0,max(iddetalle)) from tblcomprasdetalles"
        'ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pId As Integer, ByVal pNumero As String, ByVal pFecha As String, ByVal pAduana As String)
        ID = pId
        'Cantidad = pCantidad
        Comm.CommandText = "update tblventasaduana set numero='" + Replace(pNumero, "'", "''") + "',fecha='" + pFecha + "',aduana='" + Replace(pAduana, "'", "''") + "' where idaduana=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventasaduana where idaduana=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIddetalle As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventasaduana.idaduana,tblventasaduana.numero,tblventasaduana.fecha,tblventasaduana.aduana from tblventasaduana where tblventasaduana.iddetalle=" + pIddetalle.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasaduana")
        Return DS.Tables("tblventasaduana").DefaultView
    End Function
    
    Public Function ConsultaReader(ByVal pidDetalle As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tbliventasaduana.idaduana,tblventasaduana.numero,tblventasaduana.fecha,tblventasaduana.aduana from tblventasaduana where tblventasaduana.iddetalle=" + pidDetalle.ToString
        Return Comm.ExecuteReader
    End Function
    Public Function ConsultaTodoReader(ByVal pidVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tblventasaduana.idaduana,tblventasaduana.numero,tblventasaduana.fecha,tblventasaduana.aduana,tblventasaduana.iddetalle,tblventasaduana.yvalidacion,tblventasaduana.claveaduana,tblventasaduana.patente from tblventasaduana inner join tblventasinventario on tblventasaduana.iddetalle=tblventasinventario.idventasinventario where tblventasinventario.idventa=" + pidVenta.ToString
        Return Comm.ExecuteReader
    End Function

End Class
