Public Class dbiKardex
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub
    Public Function DaIdControl(ByVal pIdInventario As Integer, ByVal pFecha1 As String, ByVal pFecha2 As String) As Integer
        Comm.CommandText = "select spikardex(" + pIdInventario.ToString + ",'" + pFecha1 + "','" + pFecha2 + "')"
        Return Comm.ExecuteScalar
    End Function
    Public Function Consulta(ByVal pidControl As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblikardextemp.fecha,tbltipomovimientos.nombre,tbltipomovimientos.tipo,tblikardextemp.folio,tblikardextemp.cantidad,0,0,tblikardextemp.tipomovimiento,tblikardextemp.idmovimiento from tblikardextemp inner join tbltipomovimientos on tblikardextemp.tipomovimiento=tbltipomovimientos.id where tblikardextemp.id=" + pidControl.ToString + " order by fecha,hora"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblikardextemp")
        Return DS.Tables("tblikardextemp").DefaultView
    End Function
End Class