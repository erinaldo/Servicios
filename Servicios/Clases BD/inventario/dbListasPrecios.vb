Public Class dbListasPrecios
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Descripcion As String
    Public Numero As String

    Public idUsuarioAlta As Integer
    Public fechaAlta As String
    Public horaAlta As String
    Public idUsuarioCambio As Integer
    Public fechaCambio As String
    Public horaCambio As String

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Descripcion = ""
        Numero = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbllistasprecios where idlista=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Numero = DReader("numero")
            Descripcion = DReader("descripcion")

            idUsuarioAlta = DReader("idUsuarioAlta")
            fechaAlta = DReader("fechaAlta")
            horaAlta = DReader("horaAlta")
            idUsuarioCambio = DReader("idUsuarioCambio")
            fechaCambio = DReader("fechaCambio")
            horaCambio = DReader("horaCambio")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pDescripcion As String, ByVal pnumero As String)
        Descripcion = pDescripcion
        Numero = pnumero
        Comm.CommandText = "insert into tbllistasprecios(descripcion,numero,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Descripcion, "'", "''") + "','" + Replace(Numero, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');insert into tblinventarioprecios (idinventario,precio,esdefault,comentario,idmoneda,idlista) select idinventario,0,0,'',2,(select max(idlista) from tbllistasprecios) from tblinventario;"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pDescripcion As String, ByVal pNumero As String)
        ID = pID
        Descripcion = pDescripcion
        Numero = pNumero
        Comm.CommandText = "update tbllistasprecios set descripcion='" + Replace(pDescripcion, "'", "''") + "',numero='" + Replace(Numero, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ",  fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idlista=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbllistasprecios where idlista=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idlista,numero,descripcion from tbllistasprecios order by numero"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbllistasprecios")
        Return DS.Tables("tbllistasprecios").DefaultView
    End Function
    Public Function ChecaNumeroRepetido(ByVal pNumero As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(numero) from tbllistasprecios where numero='" + Replace(pNumero, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaNumeroRepetido = False
        Else
            ChecaNumeroRepetido = True
        End If
    End Function
    Public Function Reporte() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idlista,numero,descripcion from tbllistasprecios;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbllistasprecios")
        'DS.WriteXmlSchema("replistasprecios.xml")
        Return DS.Tables("tbllistasprecios").DefaultView
    End Function

    
End Class
