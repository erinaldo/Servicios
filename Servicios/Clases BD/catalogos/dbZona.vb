Public Class dbZona

    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public zona As String
    Public ID As Integer
    Public maxID As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        zona = ""
        Comm.Connection = Conexion
    End Sub
    Public Function reporte() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idZona,zona from tblzona;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblzona")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblzona").DefaultView
    End Function
    Public Function Consultar(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblzona where concat(zona) like '%" + Replace(pNombre, "'", "''") + "%' "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblzona")
        Return DS.Tables("tblzona").DefaultView
    End Function
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblzona where idZona=" + ID.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then

            ID = DReader("idZona")
            zona = DReader("zona")

        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String)

        Comm.CommandText = "insert into tblzona(zona,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(pNombre, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String)
        Comm.CommandText = "update tblzona set zona='" + Replace(pNombre, "'", "''") + "'" + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idZona=" + pID.ToString()
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblzona where idZona=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function Folio() As Integer
        Dim x As Integer
        Comm.CommandText = "select COUNT(idZona) from tblzona"
        x = Comm.ExecuteScalar
        If x > 0 Then
            Comm.CommandText = "select MAX(idZona) from tblzona"
            maxID = Comm.ExecuteScalar
        Else
            maxID = 0
        End If
       
        Return maxID + 1
    End Function
End Class
