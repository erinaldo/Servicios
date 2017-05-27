Public Class dbServicioAltaEstado

    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public nombre As String
    Public codigo As Integer
    Public final As Boolean


    Public idSueldo As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Inicia()
        Comm.Connection = Conexion
    End Sub
    Public Sub Inicia()
        nombre = ""
        codigo = 0
        final = False
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos(ID)
    End Sub
    Public Sub LlenaDatos(ByVal pid As Integer)
        ID = pid
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblserviciosestados where idEstado=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            nombre = DReader("estado")
            final = DReader("final")
        End If
        DReader.Close()
    End Sub
    Public Function Guardar(ByVal pnombre As String, ByVal pFinal As String) As Integer
        nombre = pnombre

        Comm.CommandText = "insert into tblserviciosestados( codigo,estado,final,idusuarioalta,fechaalta,horaalta,idusuariocambio,fechacambio,horacambio) values('0','" + Replace(nombre, "'", "''") + "'," + pFinal.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idEstado) from tblserviciosestados"
        ID = Comm.ExecuteScalar
        Return ID
    End Function
    Public Sub Modificar(ByVal pID As Integer, ByVal pnombre As String, ByVal pFinal As String)
        nombre = pnombre
        ID = pID
        Comm.CommandText = "update tblserviciosestados set estado='" + Replace(nombre, "'", "''") + "', final=" + pFinal + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idEstado=" + ID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblserviciosestados where idEstado=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idEstado,codigo,estado,if(final=1,'FINAL','')as Terminal from tblserviciosestados where estado like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblEmpleados")
        Return DS.Tables("tblEmpleados")
    End Function
    '**************************************sueldos*****************************************
    Public Function buscaCodigo()
        Comm.CommandText = "select ifnull((select max(idEstado) from tblserviciosestados),0)"
        codigo = Comm.ExecuteScalar()
        Return codigo + 1
    End Function
End Class
