Public Class dbTiposCP
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Tipo As Byte
    Public DeQueTipo As Byte

    Public idUsuarioAlta As Integer
    Public fechaAlta As String
    Public horaAlta As String
    Public idUsuarioCambio As Integer
    Public fechaCambio As String
    Public horaCambio As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection, pDequeTipo As Byte)
        ID = -1
        Nombre = ""
        Tipo = 0
        DeQueTipo = pDequeTipo

        idUsuarioAlta = 1000
        fechaAlta = ""
        horaAlta = ""
        idUsuarioCambio = 1000
        fechaCambio = ""
        horaCambio = ""

        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection, pDequetipo As Byte)
        ID = pID
        DeQueTipo = pDequetipo
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Select Case DeQueTipo
            Case 0
                Comm.CommandText = "select * from tblclientestipos where idtipo=" + ID.ToString
                DReader = Comm.ExecuteReader
                If DReader.Read() Then
                    Nombre = DReader("nombre")
                End If
                DReader.Close()
            Case 1
                Comm.CommandText = "select * from tblproveedorestipos where idtipo=" + ID.ToString
                DReader = Comm.ExecuteReader
                If DReader.Read() Then
                    Nombre = DReader("nombre")
                    Tipo = DReader("tipo")
                End If
                DReader.Close()
            Case 2
                Comm.CommandText = "select * from tblsucursalestipos where idtipo=" + ID.ToString
                DReader = Comm.ExecuteReader
                If DReader.Read() Then
                    Nombre = DReader("nombre")
                End If
                DReader.Close()
        End Select
        
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pTipo As Byte)
        Nombre = pNombre
        Tipo = pTipo
        Select Case DeQueTipo
            Case 0
                Comm.CommandText = "insert into tblclientestipos(nombre,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Nombre, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
                Comm.ExecuteNonQuery()
                Comm.CommandText = "select max(idtipo) from tblclientestipos"
                ID = Comm.ExecuteScalar
            Case 1
                Comm.CommandText = "insert into tblproveedorestipos(nombre,tipo,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Nombre, "'", "''") + "'," + Tipo.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
                Comm.ExecuteNonQuery()
                Comm.CommandText = "select max(idtipo) from tblproveedorestipos"
                ID = Comm.ExecuteScalar
            Case 2
                Comm.CommandText = "insert into tblsucursalestipos(nombre,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Nombre, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
                Comm.ExecuteNonQuery()
                Comm.CommandText = "select max(idtipo) from tblsucursalestipos"
                ID = Comm.ExecuteScalar
        End Select
        
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pTipo As Byte)
        ID = pID
        Nombre = pNombre
        Tipo = pTipo
        Select Case DeQueTipo
            Case 0
                Comm.CommandText = "update tblclientestipos set nombre='" + Replace(Nombre, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idtipo=" + ID.ToString
                Comm.ExecuteNonQuery()
            Case 1
                Comm.CommandText = "update tblproveedorestipos set nombre='" + Replace(Nombre, "'", "''") + "',tipo=" + Tipo.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idtipo=" + ID.ToString
                Comm.ExecuteNonQuery()
            Case 2
                Comm.CommandText = "update tblsucursalestipos set nombre='" + Replace(Nombre, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idtipo=" + ID.ToString
                Comm.ExecuteNonQuery()
        End Select
        
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Select Case DeQueTipo
            Case 0
                Comm.CommandText = "delete from tblclientestipos where idtipo=" + pID.ToString
                Comm.ExecuteNonQuery()
            Case 1
                Comm.CommandText = "delete from tblproveedorestipos where idtipo=" + pID.ToString
                Comm.ExecuteNonQuery()
            Case 2
                Comm.CommandText = "delete from tblsucursalestipos where idtipo=" + pID.ToString
                Comm.ExecuteNonQuery()
        End Select
        
    End Sub
    'Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select idconcepto,nombre,precio from tblformasdepago where nombre like '%" + Replace(pNombre, "'", "''") + "%'"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblformasdepago")
    '    Return DS.Tables("tblformasdepago").DefaultView
    'End Function

End Class
