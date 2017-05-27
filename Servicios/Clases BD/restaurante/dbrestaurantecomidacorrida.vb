Imports MySql.Data.MySqlClient
Public Class dbrestaurantecomidacorrida
    Private comm As New MySqlCommand
    Public id As Integer
    Public idInventario As Integer
    Public dia As Integer

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Function agregrar(ByVal idInventario As Integer, ByVal dia As Integer) As Boolean
        comm.CommandText = "insert into tblrestaurantecomidacorrida(idinventario,dia)values(" + idInventario.ToString + "," + dia.ToString + ");"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByVal id As Integer, ByVal idInventario As Integer, ByVal dia As Integer) As Boolean
        comm.CommandText = "update tblrestaurantecomidacorrida set idinventario=" + idInventario.ToString + ", dia=" + dia.ToString + " where idcomida=" + id.ToString + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal id As Integer) As Boolean
        comm.CommandText = "delete from tblrestaurantecomidacorrida where idcomida=" + id.ToString + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function buscar(ByVal id As Integer) As Boolean
        comm.CommandText = "select idcomida from tblrestaurantecomidacorrida where idcomida=" + id.ToString + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.id = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblrestaurantecomidacorrida where idcomida=" + id.ToString + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            Me.idInventario = dr("idinventario")
            Me.dia = dr("dia")
        End While
        dr.Close()
    End Sub

    Public Function lista(ByVal dia As Integer) As List(Of Integer)
        Dim l As New List(Of Integer)
        comm.CommandText = "select idinventario from tblrestaurantecomidacorrida where dia=" + dia.ToString + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            l.Add(dr("idinventario"))
        End While
        dr.Close()
        Return l
    End Function

    Public Function vista(ByVal dia As Integer) As DataView
        comm.CommandText = "select c.idcomida, c.idinventario, i.nombre,case c.dia when 1 then 'Lunes' when 2 then 'Martes' when 3 then 'Miercoles' when 4 then 'Jueves' when 5 then 'Viernes' when 6 then 'Sabado' when 7 then 'Domingo' end as dia"
        comm.CommandText += " from tblrestaurantecomidacorrida as c inner join tblinventario as i on c.idinventario=i.idinventario where c.dia=" + dia.ToString + ";"
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "comidas")
        Return ds.Tables("comidas").DefaultView
    End Function
End Class
