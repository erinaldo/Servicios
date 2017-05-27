Public Class dbComprasSolicitudes
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public idDepartamento As Integer
    Public Fecha As String
    Public Surtido As Integer
    Public Referencia As String
    Public IdDepartamentoS As Integer
    Public Estado As Byte
    Public IdUsuario As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcomprassolicitudes where idsolicitud=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Fecha = DReader("fecha")
            Surtido = DReader("surtido")
            Referencia = DReader("referencia")
            idDepartamento = DReader("iddepartamento")
            Estado = DReader("estado")
            IdUsuario = DReader("idusuario")
        End If
        DReader.Close()
        Comm.CommandText = "select iddepartamentos from tbldepartamentosareas where iddepartamento=" + idDepartamento.ToString
        IdDepartamentoS = Comm.ExecuteScalar
    End Sub
    Public Sub Guardar(ByVal pIddepartamento As Integer, ByVal pFecha As String, ByVal pSurtido As Integer, ByVal pReferencia As String, ByVal pIdUsuario As Integer)
        Fecha = pFecha
        Surtido = pSurtido
        Referencia = pReferencia
        idDepartamento = pIddepartamento
        IdUsuario = pIdUsuario
        Comm.CommandText = "insert into tblcomprassolicitudes(iddepartamento,fecha,surtido,referencia,idusuario,estado) values(" + idDepartamento.ToString + ",'" + Fecha + "'," + Surtido.ToString + ",'" + Replace(Referencia, "'", "''") + "'," + IdUsuario.ToString + "," + CStr(Estados.SinGuardar) + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idsolicitud) from tblcomprassolicitudes"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pId As Integer, ByVal pSurtido As Integer, ByVal pReferencia As String, ByVal pEstado As Byte)
        ID = pId
        Surtido = pSurtido
        Referencia = pReferencia
        Estado = pEstado
        Comm.CommandText = "update tblcomprassolicitudes set surtido=" + Surtido.ToString + ",referencia='" + Replace(Referencia, "'", "''") + "',estado=" + Estado.ToString + " where idsolicitud=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcomprassolicitudes where idsolicitud=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdDepartamento As Integer, ByVal pSurtido As Integer, ByVal pIdDepartamentoS As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblcomprassolicitudes.idsolicitud,tblcomprassolicitudes.fecha,tbldepartamentos.nombre as dnombre,tbldepartamentosareas.nombre as anombre,(case surtido when 0 then 'Nuevo' when 1 then 'Revisado' when 2 then 'En proceso' when 3 then 'Cerrado' end) as estado,tblcomprassolicitudes.referencia from tblcomprassolicitudes inner join tbldepartamentosareas on tblcomprassolicitudes.iddepartamento=tbldepartamentosareas.iddepartamento inner join tbldepartamentos on tbldepartamentosareas.iddepartamentos=tbldepartamentos.iddepartamento where tblcomprassolicitudes.fecha>='" + pFecha1 + "' and tblcomprassolicitudes.fecha<='" + pFecha2 + "' and tblcomprassolicitudes.estado<>1"
        If pIdDepartamento > 0 Then
            Comm.CommandText += " and tblcomprassolicitudes.iddepartamento=" + pIdDepartamento.ToString
        End If
        If pIdDepartamentoS > 0 Then
            Comm.CommandText += " and tbldepartamentos.iddepartamento=" + pIdDepartamentoS.ToString
        End If
        'If pReferencia <> "" Then
        'Comm.CommandText += " and tblcomprassolicitudes.referencia like '%" + Replace(pReferencia, "'", "''") + "%'"
        'End If
        If pSurtido >= 0 Then
            Comm.CommandText += " and tblcomprassolicitudes.surtido=" + pSurtido.ToString
        End If
        Comm.CommandText += " order by tblcomprassolicitudes.fecha desc,dnombre,anombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprassolicitudes")
        Return DS.Tables("tblcomprassolicitudes").DefaultView
    End Function
    Public Function ConsultaConSurtido(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdDepartamento As Integer, ByVal pSurtido As Integer, ByVal pIdDepartamentoS As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblcomprassolicitudes.idsolicitud,tblcomprassolicitudes.fecha,tbldepartamentos.nombre as dnombre,tbldepartamentosareas.nombre as anombre,(case surtido when 0 then 'Nuevo' when 1 then 'Revisado' when 2 then 'En proceso' when 3 then 'Cerrado' end) as estado,tblcomprassolicitudes.referencia from tblcomprassolicitudes inner join tbldepartamentosareas on tblcomprassolicitudes.iddepartamento=tbldepartamentosareas.iddepartamento inner join tbldepartamentos on tbldepartamentosareas.iddepartamentos=tbldepartamentos.iddepartamento where tblcomprassolicitudes.fecha>='" + pFecha1 + "' and tblcomprassolicitudes.fecha<='" + pFecha2 + "'"
        If pIdDepartamento > 0 Then
            Comm.CommandText += " and tblcomprassolicitudes.iddepartamento=" + pIdDepartamento.ToString
        End If
        If pIdDepartamentoS > 0 Then
            Comm.CommandText += " and tbldepartamentos.iddepartamento=" + pIdDepartamentoS.ToString
        End If
        'If pReferencia <> "" Then
        'Comm.CommandText += " and tblcomprassolicitudes.referencia like '%" + Replace(pReferencia, "'", "''") + "%'"
        'End If
        If pSurtido >= 0 Then
            Comm.CommandText += " and tblcomprassolicitudes.surtido=" + pSurtido.ToString
        End If
        Comm.CommandText += " order by tblcomprassolicitudes.fecha desc,anombre,dnombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprassolicitudes")
        Return DS.Tables("tblcomprassolicitudes").DefaultView
    End Function
End Class
