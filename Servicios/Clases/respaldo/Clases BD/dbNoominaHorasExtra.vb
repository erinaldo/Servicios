Public Class dbNoominaHorasExtra
    Public ID As Integer
    Public NuevoConcepto As Boolean
    Public TipoHoras As String
    Public IdNomina As Integer
    'Public TipoPercepcionDeduccion As Integer
    'Public Clave As String
    'Public Concepto As String
    Public Dias As Integer
    Public ImportePagado As Double
    Public HorasExtra As Integer

    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        TipoHoras = "dobles"
        IdNomina = 0
        Dias = 0
        ImportePagado = 0
        HorasExtra = 0
        NuevoConcepto = False
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblnominahorasextra where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Dias = DReader("dias")
            TipoHoras = DReader("tipohoras")
            HorasExtra = DReader("horasextra")
            ImportePagado = DReader("importepagado")
            IdNomina = DReader("idnomina")
        End If
        DReader.Close()
        'If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
        'If IdVariante > 1 Then Producto = New dbProductosVariantes(IdVariante, Comm.Connection)
        'Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdNomina As Integer, ByVal pDias As Integer, ByVal pTipoHoras As String, ByVal pHorasExtra As Integer, ByVal pImporte As Double)
        NuevoConcepto = True
        Comm.CommandText = "insert into tblnominahorasextra(idnomina,dias,tipohoras,horasextra,importepagado) values(" + pIdNomina.ToString + "," + pDias.ToString + ",'" + pTipoHoras + "'," + pHorasExtra.ToString + "," + pImporte.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull((select max(iddetalle) from tblnominahorasextra),0)"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pDias As Integer, ByVal pTipoHoras As String, ByVal pHorasExtra As Integer, ByVal pImporte As Double)
        ID = pID
        Comm.CommandText = "update tblnominahorasextra set dias=" + pDias.ToString + ",tipohoras='" + pTipoHoras + "',horasextra=" + pHorasExtra.ToString + ",importepagado=" + pImporte.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblnominahorasextra where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tvi.tipohoras,tvi.dias,tvi.horasextra,tvi.importepagado from tblnominahorasextra as tvi where tvi.idnomina=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblnominahorasextra")
        Return DS.Tables("tblnominahorasextra").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tvi.tipohoras,tvi.dias,tvi.horasextra,tvi.importepagado from tblnominahorasextra as tvi where tvi.idnomina=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
End Class
