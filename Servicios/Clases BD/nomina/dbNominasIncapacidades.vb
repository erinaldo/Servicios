Public Class dbNominasIncapacidades
    Public ID As Integer
    Public NuevoConcepto As Boolean
    Public TipoIncapacidad As Byte
    Public IdNomina As Integer
    'Public TipoPercepcionDeduccion As Integer
    'Public Clave As String
    'Public Concepto As String
    Public Dias As Integer
    Public Descuento As Double

    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        TipoIncapacidad = 0
        IdNomina = 0
        Dias = 0
        Descuento = 0
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
        Comm.CommandText = "select * from tblnominaincapacidades where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            TipoIncapacidad = DReader("tipoincapacidad")
            IdNomina = DReader("idnomina")
            Dias = DReader("dias")
            Descuento = DReader("descuento")
        End If
        DReader.Close()
        'If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
        'If IdVariante > 1 Then Producto = New dbProductosVariantes(IdVariante, Comm.Connection)
        'Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdNomina As Integer, ByVal pTipo As Byte, ByVal pDias As Integer, ByVal pDescuento As Double)
        NuevoConcepto = True
        Comm.CommandText = "insert into tblnominaincapacidades(idnomina,tipoincapacidad,dias,descuento) values(" + pIdNomina.ToString + "," + pTipo.ToString + "," + pDias.ToString + "," + pDescuento.ToString + ");"
        Comm.CommandText += "select ifnull(last_insert_id(),0);"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pTipo As Byte, ByVal pDias As Integer, ByVal pDescuento As Double)
        ID = pID
        Comm.CommandText = "update tblnominaincapacidades set tipoincapacidad=" + pTipo.ToString + ",dias=" + pDias.ToString + ",descuento=" + pDescuento.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblnominaincapacidades where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,case tvi.tipoincapacidad when 0 then 'Riesgo de trabajo' when 1 then 'Enfermedad en general' when 2 then 'Maternidad' end as tipod,tvi.dias,tvi.descuento from tblnominaincapacidades as tvi where tvi.idnomina=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblnominaincapacidades")
        Return DS.Tables("tblnominaincapacidades").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tvi.tipoincapacidad tin,case tvi.tipoincapacidad when 0 then 'Riesgo de trabajo' when 1 then 'Enfermedad en general' when 2 then 'Maternidad' end as tipod,tvi.dias,tvi.descuento from tblnominaincapacidades as tvi where tvi.idnomina=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
End Class
