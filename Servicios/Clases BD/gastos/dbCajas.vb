Public Class dbCajas
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public IdSucursal As Integer
    Public Serie As String
    Public SerieCot As String
    Public SeriePed As String
    Public Deposito As Double

    Public idUsuarioAlta As Integer
    Public fechaAlta As String
    Public horaAlta As String
    Public idUsuarioCambio As Integer
    Public fechaCambio As String
    Public horaCambio As String
    Public Maximo As Double
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        IdSucursal = 0
        Serie = ""
        SerieCot = ""
        SeriePed = ""
        Deposito = 0
        Maximo = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcajas where idcaja=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            IdSucursal = DReader("idsucursal")
            Serie = DReader("serie")
            Deposito = DReader("deposito")
            SerieCot = DReader("seriecot")
            SeriePed = DReader("serieped")
            idUsuarioAlta = DReader("idUsuarioAlta")
            fechaAlta = DReader("fechaAlta")
            horaAlta = DReader("horaAlta")
            idUsuarioCambio = DReader("idUsuarioCambio")
            fechaCambio = DReader("fechaCambio")
            horaCambio = DReader("horaCambio")
            Maximo = DReader("maximo")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pIdSucursal As Integer, ByVal pSerie As String, ByVal pSerieCot As String, ByVal pSeriePed As String, pMaximo As Double)
        Nombre = pNombre
        IdSucursal = pIdSucursal
        Serie = pSerie
        SerieCot = pSerieCot
        SeriePed = pSeriePed
        Comm.CommandText = "insert into tblcajas(nombre,idsucursal,serie,deposito,seriecot,serieped,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,maximo) values('" + Replace(Nombre, "'", "''") + "'," + IdSucursal.ToString + ",'" + Replace(Serie, "'", "''") + "',0,'" + Replace(SerieCot, "'", "''") + "','" + Replace(SeriePed, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + pMaximo.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pSerie As String, ByVal pSerieCot As String, ByVal pSeriePed As String, pMaximo As Double)
        ID = pID
        Nombre = pNombre
        Serie = pSerie
        SerieCot = pSerieCot
        SeriePed = pSeriePed
        Comm.CommandText = "update tblcajas set nombre='" + Replace(Nombre, "'", "''") + "',serie='" + Replace(Serie, "'", "''") + "',seriecot='" + Replace(SerieCot, "'", "''") + "',serieped='" + Replace(SeriePed, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "',maximo=" + pMaximo.ToString + " where idcaja=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcajas where idcaja=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idcaja,tblcajas.nombre as cnombre,tblsucursales.nombre as snombre from tblcajas inner join tblsucursales on tblcajas.idsucursal=tblsucursales.idsucursal where idcaja>1 and tblcajas.nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcaja")
        Return DS.Tables("tblcaja").DefaultView
    End Function
    Public Function ChecaNombreRepetido(ByVal pNombre As String, ByVal pIdSucursal As Integer) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(idcaja) from tblcajas where idsucursal=" + pIdSucursal.ToString + " and nombre like '" + Replace(pNombre, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Sub MovimientodeCaja(ByVal pIdCaja As Integer, ByVal pCantidad As Double)
        Comm.CommandText = "update tblcajas set deposito=deposito+" + pCantidad.ToString + " where idcaja=" + pIdCaja.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function CuantoEnCaja(pIdCaja As Integer) As Double
        Comm.CommandText = "select ifnull((select deposito from tblcajas where idcaja=" + pIdCaja.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
End Class
