Imports MySql.Data.MySqlClient
Public Class dbCodigodeBarras
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Codigo As String
    Public Nombre As String
    Public tipo As Integer
    Public ID As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Codigo = ""
        Nombre = ""
        tipo = 0
        ID = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos(ID)
    End Sub
    Public Sub LlenaDatos(ByVal pid As Integer)
        ID = pid
        Nombre = ""
        Codigo = ""
        tipo = 1
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcodigobarras where ID=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Codigo = DReader("codigo")
            tipo = DReader("tipo")
        End If
        DReader.Close()
    End Sub
    Public Sub buscar(ByVal pIDInv)
        Comm.CommandText = "select ID from tblcodigobarras where idInventario=" + pIDInv.ToString
        ID = Comm.ExecuteScalar
        If ID <> 0 Then
            LlenaDatos(ID)

        End If

    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pCodigo As String, ByVal pTipo As Integer, ByVal pIDInv As Integer)
        Nombre = pNombre
        Codigo = pCodigo
        tipo = pTipo
        Comm.CommandText = "insert into tblcodigobarras(nombre,tipo,codigo, idInventario,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Nombre, "'", "''") + "'," + tipo.ToString + ",'" + Replace(Codigo, "'", "''") + "'," + pIDInv.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        'Comm.ExecuteNonQuery()
        Comm.CommandText += "select max(ID) from tblcodigobarras;"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pCodigo As String, ByVal pTipo As Integer)
        ID = pID
        Nombre = pNombre
        Codigo = pCodigo
        tipo = pTipo
        Comm.CommandText = "update tblcodigobarras set nombre='" + Replace(Nombre, "'", "''") + "',codigo='" + Replace(Codigo, "'", "''") + "',tipo=" + tipo.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where ID=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcodigobarras where ID=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    'Public Function Consulta(ByVal PNombre As String) As DataView
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select * from tblcodigobarras where nombre like '%" + PNombre + "%'"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblcodigobarras")
    '    Return DS.Tables("tblcodigobarras").DefaultView
    'End Function
    Public Function Consulta(ByVal PNombre As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblcodigobarras where nombre like '%" + PNombre + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblAdjudicacion")
        Return DS.Tables("tblAdjudicacion")
    End Function
    'Public Function ConsultaT(ByVal PNombre As String) As DataTable
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select * from tblcodigobarras"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblAdjudicacion")
    '    Return DS.Tables("tblAdjudicacion")
    'End Function
End Class
