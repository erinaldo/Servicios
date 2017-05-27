Imports MySql.Data.MySqlClient
Public Class dbnominasubcontratacion
    Private comm As New MySqlCommand
    Public idSubcontratacion As Integer
    Public idTrabajador As Integer
    Public rfclaboral As String
    Public porcentaje As Double

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal id As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.idSubcontratacion = id
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblnominasubcontratacion where idsubcontratacion=" + idSubcontratacion.ToString + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader()
        While dr.Read()
            Me.idTrabajador = dr("idtrabajador")
            Me.rfclaboral = dr("rfclaboral")
            Me.porcentaje = dr("porcentaje")
        End While
        dr.Close()
    End Sub

    Public Function buscar(ByVal id) As Boolean
        comm.CommandText = "select ifnull(idsubcontratacion,0) from tblnominasubcontratacion where idsubcontratacion=" + id.ToString + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idSubcontratacion = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Sub agregar(ByVal idTrabajador As Integer, ByVal rfclaboral As String, ByVal porcentaje As Double)
        comm.CommandText = "insert into tblnominasubcontratacion(idtrabajador,rfclaboral,porcentaje) values(" + idTrabajador.ToString + ",'" + Replace(rfclaboral, "'", "''") + "'," + porcentaje.ToString + ");"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub modificar(ByVal id As Integer, ByVal idTrabajador As Integer, ByVal rfclaboral As String, ByVal porcentaje As Double)
        comm.CommandText = "update tblnominasubcontratacion set idtrabajador=" + idTrabajador.ToString + ", rfclaboral='" + rfclaboral + "', porcentaje=" + porcentaje.ToString + " where idsubcontratacion=" + id.ToString + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Function eliminar(ByVal id As Integer) As Boolean
        comm.CommandText = "delete from tblnominasubcontratacion where idsubcontratacion=" + id.ToString + ";"
        comm.ExecuteNonQuery()
    End Function

    Public Function buscarTrabajador(ByVal idTrabajador) As Boolean
        comm.CommandText = "select ifnull(idsubcontratacion,0) from tblnominasubcontratacion where idtrabajador=" + idTrabajador.ToString + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idSubcontratacion = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function vista(ByVal idTrabajador As Integer) As DataView
        comm.CommandText = "select idsubcontratacion,rfclaboral,porcentaje from tblnominasubcontratacion where idtrabajador=" + idTrabajador.ToString + ";"
        Dim da As New MySqlDataAdapter(comm)
        Dim ds As New DataSet
        da.Fill(ds, "subcontrataciones")
        Return ds.Tables("subcontrataciones").DefaultView
    End Function

    Public Function ConsultaReader(ByVal idTrabajador As Integer) As MySqlDataReader
        comm.CommandText = "select porcentaje,rfclaboral from tblnominasubcontratacion where idtrabajador=" + idTrabajador.ToString + ";"
        Return comm.ExecuteReader
    End Function
End Class
