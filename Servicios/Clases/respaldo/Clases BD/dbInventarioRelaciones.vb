Public Class dbInventarioRelaciones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public IdDetalle As Integer
    Public idInventario As Integer
    Public Extra As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        IdDetalle = 0
        idInventario = 0
        Extra = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
        LlenaDAtos(pID)
    End Sub
    Public Sub LlenaDAtos(ByVal PId As Integer)
        ID = pid
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblinventariorelaciones where idrelacion=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Extra = DReader("extra")
        End If
        DReader.Close()
    End Sub
    Public Sub Nuevo()
        ID = 0
        Nombre = ""
        Extra = ""
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pExtra As String)
        
        Comm.CommandText = "insert into tblinventariorelaciones(nombre,extra) values('" + Replace(pNombre, "'", "''") + "','" + Replace(pExtra, "'", "''") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idrelacion) from tblinventariorelaciones"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub GuardarDetalle(ByVal pIdRelacion As Integer, ByVal pIdInventario As Integer)
        Comm.CommandText = "insert into tblinventariorelacionesdetalles(idrelacion,idinventario) values(" + pIdRelacion.ToString + "," + pIdInventario.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub GuardaDetallesMuchos(ByVal pComando As String)
        Comm.CommandText = pComando
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pExtra As String)
        ID = pID
        Comm.CommandText = "update tblinventariorelaciones set nombre='" + Replace(pNombre, "'", "''") + "',extra='" + Replace(pExtra, "'", "''") + "' where idrelacion=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblinventariorelaciones where idrelacion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarDetalle(ByVal pID As Integer)
        Comm.CommandText = "delete from tblinventariorelaciones where idrelacion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idrelacion,nombre from tblinventariorelaciones where nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventariorelaciones")
        Return DS.Tables("tblinventariorelaciones").DefaultView
    End Function
    Public Function ConsultaDetalles(ByVal pIdRelacion As Integer, ByVal PNombreInventario As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select ir.iddetalle,i.clave,i.nombre from tblinventariorelacionesdetalles as ir inner join tblinventario as i on ir.idinventario=i.idinventario where idrelacion=" + pIdRelacion.ToString
        If PNombreInventario <> "" Then
            Comm.CommandText += " concat(i.clave,i.nombre) like '%" + Replace(PNombreInventario, "'", "''") + "%'"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventariorelaciones")
        Return DS.Tables("tblinventariorelaciones").DefaultView
    End Function
    Public Function ConsultaRelaciones(ByVal pIdinventario As Integer) As DataView
        Dim DS As New DataSet
        Dim pIdRelacion As Integer
        Comm.CommandText = "select ifnull((select idrelacion from tblinventariorelacionesdetalles where idinventario=" + pIdinventario.ToString + "),0)"
        pIdRelacion = Comm.ExecuteScalar
        Comm.CommandText = "select ir.iddetalle,i.clave,i.nombre,spdainventario(ir.idinventario,0,0) from tblinventariorelacionesdetalles as ir inner join tblinventario as i on ir.idinventario=i.idinventario where ir.idrelacion=" + pIdRelacion.ToString + " and ir.idinventario<>" + pIdinventario.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventariorelaciones")
        Return DS.Tables("tblinventariorelaciones").DefaultView
    End Function
    Public Function ChecaSiyaEsta(ByVal piDInventario As Integer) As Boolean
        Comm.CommandText = "select count(idinventario) from tblinventariorelacionesdetalles where idinventario=" + piDInventario.ToString
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function reporte() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idalmacen,numero,nombre from tblalmacenes;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblalmacenes")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblalmacenes").DefaultView
    End Function
End Class
