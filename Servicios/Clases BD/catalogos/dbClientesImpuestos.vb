Public Class dbClientesImpuestos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public IdCliente As Integer
    Public Tipo As String
    Public Tasa As String
    Public Importe As Double
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Tipo = 0
        Tasa = 0
        IdCliente = 0
        Importe = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblclientesimpuestos where idimpuesto=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Tipo = DReader("tipo")
            Tasa = DReader("tasa")
            IdCliente = DReader("idcliente")
            'Importe = DReader("importe")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pTipo As Byte, ByVal pTasa As Double, ByVal pIdcliente As Integer)
        Comm.CommandText = "insert into tblclientesimpuestos(idcliente,tipo,tasa,nombre,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + pIdcliente.ToString + "," + pTipo.ToString + "," + pTasa.ToString + ",'" + Replace(pNombre, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pTipo As Byte, ByVal pTasa As Double)
        ID = pID
        Comm.CommandText = "update tblclientesimpuestos set nombre='" + Replace(pNombre, "'", "''") + "',tipo=" + pTipo.ToString + ",tasa=" + pTasa.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idimpuesto=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblclientesimpuestos where idimpuesto=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdcliente As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idimpuesto,nombre,tasa,if(tipo=0,'Traslado','Retención') as tipoi from tblclientesimpuestos where  idcliente=" + pIdcliente.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesimpuestos")
        Return DS.Tables("tblclientesimpuestos").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdcliente As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Dim DS As New DataSet
        Comm.CommandText = "select idimpuesto,nombre,tasa,tipo from tblclientesimpuestos where idcliente=" + pIdcliente.ToString + " order by tipo desc"
        Return Comm.ExecuteReader
    End Function
    Public Function ConsultaReaderI(ByVal pIdventa As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Dim DS As New DataSet
        Comm.CommandText = "select idimpuesto,nombre,tasa,tipo,importe from tblventasimpuestos where idventa=" + pIdventa.ToString + " order by tipo desc"
        Return Comm.ExecuteReader
    End Function
    Public Function ConsultaI(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idimpuesto,nombre,tasa,if(tipo=0,'Traslado','Retención') as tipoi,importe from tblventasimpuestos where  idventa=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasimpuestos")
        Return DS.Tables("tblventasimpuestos").DefaultView
    End Function
    Public Sub InsertaImpuestos(ByVal pidVenta As Integer, ByVal pIdCliente As Integer)
        Comm.CommandText = "insert into tblventasimpuestos(nombre,idventa,tipo,tasa,importe) select nombre," + pidVenta.ToString + ",tipo,tasa,0 from tblclientesimpuestos where idcliente=" + pIdCliente.ToString
        Comm.ExecuteNonQuery()
    End Sub
End Class
