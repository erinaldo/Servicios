Public Class dbMonedasConversiones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdMoneda As Integer
    Public IdMoneda2 As Integer
    Public Cantidad As Double

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdMoneda = 0
        IdMoneda2 = 0
        Cantidad = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblmonedasconversiones where idconversion=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdMoneda = DReader("idmoneda")
            IdMoneda2 = DReader("idmoneda2")
            Cantidad = DReader("cantidad")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pIdMoneda As Integer, ByVal pIdMoneda2 As Integer, ByVal pCantidad As Double)
        IdMoneda = pIdMoneda
        IdMoneda2 = pIdMoneda2
        Cantidad = pCantidad
        Comm.CommandText = "insert into tblmonedasconversiones(idmoneda,idmoneda2,cantidad) values(" + IdMoneda.ToString + "," + IdMoneda2.ToString + "," + Cantidad.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pId As Integer, ByVal pCantidad As Double)
        ID = pID
        Cantidad = pCantidad
        Comm.CommandText = "update tblmonedasconversiones set cantidad=" + Cantidad.ToString + " where idconversion=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblmonedasconversiones where idconversion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idconversion,(select nombre from tblmonedas where tblmonedas.idmoneda=tblmonedasconversiones.idmoneda) as moneda1,(select nombre from tblmonedas where tblmonedas.idmoneda=tblmonedasconversiones.idmoneda2) as moneda2,cantidad from tblmonedasconversiones"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmonedasconversiones")
        Return DS.Tables("tblmonedasconversiones").DefaultView
    End Function
End Class
