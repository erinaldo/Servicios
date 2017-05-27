Public Class dbVentasCartaPorte
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Origen As String
    Public Destino As String
    Public Chofer As String
    Public Matricula As String
    Public Mercancia As String
    Public Peso As String
    Public Fecha As String
    Public IdVenta As Integer
    Public ValorUnitario As String
    Public ValorDeclarado As String
    Public Referencia As String
    Public Pedimento As String
    Public FechaPedimento As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Origen = ""
        Destino = ""
        Chofer = ""
        Matricula = ""
        Mercancia = ""
        Peso = ""
        Fecha = ""
        ValorDeclarado = ""
        ValorUnitario = ""
        Referencia = ""
        Pedimento = ""
        FechaPedimento = ""
        IdVenta = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        IdVenta = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventascartaporte where idventa=" + IdVenta.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Origen = DReader("origen")
            Destino = DReader("destino")
            Chofer = DReader("chofer")
            Matricula = DReader("matricula")
            Mercancia = DReader("mercancia")
            Peso = DReader("peso")
            Fecha = DReader("fecha")
            ID = DReader("id")
            ValorDeclarado = DReader("valordeclarado")
            ValorUnitario = DReader("valorunitario")
            Referencia = DReader("referencia")
            Pedimento = DReader("pedimento")
            FechaPedimento = DReader("fechapedimento")
        Else
            Origen = "Nohay"
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pOrigen As String, ByVal pDestino As String, ByVal pChofer As String, ByVal pMatricula As String, ByVal pMercancia As String, ByVal pPeso As String, ByVal pFecha As String, ByVal pidVenta As Integer, ByVal pVUnitario As String, ByVal pVDeclarado As String, ByVal pReferencia As String, ByVal pPedimento As String, ByVal pFechaPedimento As String)
        Comm.CommandText = "insert into tblventascartaporte(origen,destino,chofer,matricula,mercancia,peso,fecha,idventa,valorunitario,valordeclarado,referencia,pedimento,fechapedimento) values('" + Replace(pOrigen, "'", "''") + "','" + Replace(pDestino, "'", "''") + "','" + Replace(pChofer, "'", "''") + "','" + Replace(pMatricula, "'", "''") + "','" + Replace(pMercancia, "'", "''") + "','" + Replace(pPeso, "'", "''") + "','" + pFecha + "'," + pidVenta.ToString + ",'" + Replace(pVUnitario, "'", "''") + "','" + Replace(pVDeclarado, "'", "''") + "','" + Replace(pReferencia, "'", "''") + "','" + Replace(pPedimento, "'", "''") + "','" + Replace(pFechaPedimento, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pId As Integer, ByVal pOrigen As String, ByVal pDestino As String, ByVal pChofer As String, ByVal pMatricula As String, ByVal pMercancia As String, ByVal pPeso As String, ByVal pFecha As String, ByVal pidVenta As Integer, ByVal pVUnitario As String, ByVal pVDeclarado As String, ByVal pReferencia As String, ByVal pPedimento As String, ByVal pFechaPedimento As String)
        Comm.CommandText = "update tblventascartaporte set origen='" + Replace(pOrigen, "'", "''") + "',destino='" + Replace(pDestino, "'", "''") + "',chofer='" + Replace(pChofer, "'", "''") + "',matricula='" + Replace(pMatricula, "'", "''") + "',mercancia='" + Replace(pMercancia, "'", "''") + "',peso='" + Replace(pPeso, "'", "''") + "',fecha='" + pFecha + "',valorunitario='" + Replace(pVUnitario, "'", "''") + "',valordeclarado='" + Replace(pVDeclarado, "'", "''") + "',referencia='" + Replace(pReferencia, "'", "''") + "',pedimento='" + Replace(pPedimento, "'", "''") + "',fechapedimento='" + Replace(pFechaPedimento, "'", "''") + "' where id=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventascartaporte where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblventascartaporte"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventascartaporte")
        Return DS.Tables("tblventascartaporte").DefaultView
    End Function
    
  
End Class
