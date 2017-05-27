Public Class dbServiciosEquipos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdEquipo As Integer
    Public IdServicio As Integer
    Public Equipo As dbClientesEquipos
    Public nombre As String
    Public marca As String
    Public modelo As String
    Public serie As String
    Public motor As String
    Public matricula As String
    Public color As String
    Public kilometraje As String

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdEquipo = 0
        IdServicio = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblserviciosequipos where idservicioequipo=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdServicio = DReader("idservicio")
            IdEquipo = DReader("idequipo")
        End If
        DReader.Close()
        Equipo = New dbClientesEquipos(IdEquipo, MySqlcon)
    End Sub
    Public Sub Guardar(ByVal pIdServicio As Integer, ByVal pIdEquipo As Integer, ByVal pIdCliente As Integer)
        IdEquipo = pIdEquipo
        IdServicio = pIdServicio
        Comm.CommandText = "insert into tblserviciosequipos(idequipo,idservicio,idcliente) values(" + IdEquipo.ToString + "," + IdServicio.ToString + "," + pIdCliente.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub GuardarSucursal(ByVal pIdServicio As Integer, ByVal pIdEquipo As Integer, ByVal pIdCliente As Integer)
        IdEquipo = pIdEquipo
        IdServicio = pIdServicio
        Comm.CommandText = "insert into tblserviciosequipossuc(idequipo,idservicio,idsucursal) values(" + IdEquipo.ToString + "," + IdServicio.ToString + "," + pIdCliente.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function verificarServicio(ByVal pIdcliente As Integer, ByVal pIdEquipo As Integer) As Integer
        IdEquipo = pIdEquipo
        ' IdServicio = pIdServicio
        Dim resultado As Integer
        Comm.CommandText = "Select count(idequipo) from tblserviciosequipos where idequipo='" + pIdEquipo.ToString() + "'and idcliente='" + pIdcliente.ToString() + "'"
        resultado = Comm.ExecuteScalar
        Return resultado
    End Function
    Public Sub Modificar(ByVal pID As Integer, ByVal pIdEquipo As Integer)
        ID = pID
        IdEquipo = pIdEquipo
        Comm.CommandText = "update tblserviciosequipos set idequipo=" + IdEquipo.ToString + " where idservicioequipo=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblserviciosequipos where idservicioequipo=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdServicio As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblserviciosequipos where idservicio=" + pIdServicio.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosequipos")
        Return DS.Tables("tblserviciosequipos").DefaultView
    End Function
    Public Function BuscaEquipo(ByVal pIdServicio As Integer) As dbClientesEquipos
        Dim IdEquipoC As Integer
        Comm.CommandText = "select if((select idequipo from tblserviciosequipos where idservicio=" + pIdServicio.ToString + ") is null,0,(select idequipo from tblserviciosequipos where idservicio=" + pIdServicio.ToString + "))"
        IdEquipoC = Comm.ExecuteScalar
        If ID <> 0 Then
            Return New dbClientesEquipos(IdEquipoC, Comm.Connection)
        Else
            Return New dbClientesEquipos(Comm.Connection)
        End If
    End Function
    Public Function BuscaEquipoSuc(ByVal pIdServicio As Integer) As dbClientesEquipos
        Dim IdEquipoC As Integer
        Comm.CommandText = "select if((select idequipo from tblserviciosequipossuc where idservicio=" + pIdServicio.ToString + ") is null,0,(select idequipo from tblserviciosequipossuc where idservicio=" + pIdServicio.ToString + "))"
        IdEquipoC = Comm.ExecuteScalar
        If ID <> 0 Then
            Return New dbClientesEquipos(IdEquipoC, Comm.Connection, 1)
        Else
            Return New dbClientesEquipos(Comm.Connection)
        End If
    End Function
    '------
    Public Sub Llenadatos(ByVal pID As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblclientesequipos where idequipo=" + pID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            nombre = DReader("nombre")
            marca = DReader("marca")
            modelo = DReader("modelo")
            serie = DReader("noserie")
            motor = DReader("nomotor")
            matricula = DReader("matricula")
            color = DReader("color")
            kilometraje = DReader("kilometraje")
        End If
        DReader.Close()
    End Sub
    Public Sub LlenadatosSucursales(ByVal pID As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblsucequipos where idequipo=" + pID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            nombre = DReader("nombre")
            marca = DReader("marca")
            modelo = DReader("modelo")
            serie = DReader("noserie")
            motor = DReader("nomotor")
            matricula = DReader("matricula")
            color = DReader("color")
            kilometraje = DReader("kilometraje")
        End If
        DReader.Close()
    End Sub
End Class
