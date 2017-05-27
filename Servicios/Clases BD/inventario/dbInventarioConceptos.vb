Public Class dbInventarioConceptos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Tipo As Byte
    Public Serie As String
    Public Folio As Integer
    Public Formato As String
    Public IdSucursal As Integer
    Public Enum Tipos
        Entrada = 0
        Salida = 1
        Ajuste = 2
        Traspaso = 3
        InventarioInicial = 4
    End Enum
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Tipo = 0
        Serie = ""
        Folio = 0
        Formato = ""
        IdSucursal = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblinventarioconceptos where idconcepto=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Tipo = DReader("tipo")
            Serie = DReader("serie")
            Folio = DReader("folio")
            Formato = DReader("formato")
            IdSucursal = DReader("idsucursal")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pTipo As Tipos, ByVal pSerie As String, ByVal pFolio As Integer, ByVal pFormato As String, ByVal pIdSucursal As Integer)
        Nombre = pNombre
        Tipo = pTipo
        Serie = pSerie
        Formato = pFormato
        Folio = pFolio
        IdSucursal = pIdSucursal
        Comm.CommandText = "insert into tblinventarioconceptos(nombre,tipo,serie,folio,formato,idsucursal,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Trim(Nombre), "'", "''") + "'," + Tipo.ToString + ",'" + Replace(Trim(Serie), "'", "''") + "'," + Folio.ToString + ",'" + Replace(Trim(Formato), "'", "''") + "'," + IdSucursal.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pTipo As Tipos, ByVal pSerie As String, ByVal pFolio As Integer, ByVal pFormato As String)
        ID = pID
        Nombre = pNombre
        Tipo = pTipo
        Serie = pSerie
        Formato = pFormato
        Folio = pFolio
        Comm.CommandText = "update tblinventarioconceptos set nombre='" + Replace(Nombre, "'", "''") + "',tipo=" + Tipo.ToString + ",serie='" + Replace(Trim(Serie), "'", "''") + "',folio=" + Folio.ToString + ",formato='" + Replace(Trim(Formato), "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idconcepto=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblinventarioconceptos where idconcepto=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pNombre As String, ByVal pIdSucursal As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idconcepto,nombre,case tipo when 0 then 'Entrada' when 1 then 'Salida' when 2 then 'Inv. Físico' when 3 then 'Traspaso' when 4 then 'Inv. Inicial' end as Tipom,serie,folio,formato from tblinventarioconceptos where nombre like '%" + Replace(pNombre, "'", "''") + "%' and idsucursal=" + pIdSucursal.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioconceptos")
        Return DS.Tables("tblinventarioconceptos").DefaultView
    End Function
    Public Sub DaPrimerConcepto(pTipo As Byte)
        Comm.CommandText = "select ifnull((select min(idconcepto) from tblinventarioconceptos where tipo=" + pTipo.ToString + "),0)"
        ID = Comm.ExecuteScalar
        LlenaDatos()
    End Sub
End Class
