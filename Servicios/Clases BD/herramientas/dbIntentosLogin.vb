Imports MySql.Data.MySqlClient
Public Class dbIntentosLogin
    Private comm As New MySqlCommand
    Private id As Integer
    Private idUsuario As Integer
    Private fecha As String
    Private hora As String
    Private licencia As String
    Private comentario As String

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub agregar(ByVal idUsuario As Integer, ByVal comentario As String, ByVal exitoso As Integer, ByVal falla As Integer)
        comm.CommandText = "insert into tblintentoslogin(idUsuario,fecha,hora,licencia,comentario,exitosos,fallos) values(" + idUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "','" + GlobalLicenciaSTR + "','" + comentario + "'," + exitoso.ToString() + "," + falla.ToString() + ");"
        comm.ExecuteNonQuery()
    End Sub

    Public Function vistaIntentos(ByVal idUsuario As Integer) As DataView
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select i.fecha as Fecha, i.hora as Hora, i.licencia as Licencia, i.comentario as Entro, u.nombreusuario as usuario from tblintentoslogin as i inner join tblusuarios as u on i.idusuario=u.idusuario where i.idusuario=" + idUsuario.ToString() + ";"
        Else
            comm.CommandText = "select i.fecha as Fecha, i.hora as Hora, i.licencia as Licencia, i.comentario as Entro, u.nombreusuario as usuario from tblintentoslogin as i inner join tblusuarios as u on i.idusuario=u.idusuario;"
        End If

        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "intentos")
        Return ds.Tables("intentos").DefaultView
    End Function

    Public Sub borraViejos(ByVal idUsuario As Integer, ByVal fecha As String)
        comm.CommandText = "delete from tblintentoslogin where fecha<'" + fecha + "';"
        comm.ExecuteNonQuery()
    End Sub

End Class
