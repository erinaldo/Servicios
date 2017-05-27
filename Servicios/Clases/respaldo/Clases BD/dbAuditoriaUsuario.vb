Imports MySql.Data.MySqlClient
Public Class dbAuditoriaUsuario
    Private comm As New MySqlCommand
    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Function modificacionesClientes(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select c.clave as clave,c.nombre as nombre,c.fechaCambio as fecha,c.horaCambio as hora, u.nombre as usuario from tblclientes as c inner join tblusuarios as u on c.idusuario=u.idusuario where c.idUsuario=" + idUsuario.ToString()
            comm.CommandText += " and c.fechaCambio>='" + desde + "' and c.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select c.clave as clave,c.nombre as nombre,c.fechaCambio as fecha,c.horaCambio as hora, u.nombre as usuario from tblclientes as c inner join tblusuarios as u on c.idusuario=u.idusuario where "
            comm.CommandText += "c.fechaCambio>='" + desde + "' and c.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by c.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "cambiosClientes")
        Return ds.Tables("cambiosClientes")
    End Function

    Public Function modificacionesProveedores(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select p.clave as clave,p.nombre as nombre,p.fechaCambio as fecha,p.horacambio as hora, u.nombre as usuario from tblproveedores as p inner join tblusuarios as u on p.idusuarioCambio=u.idUsuario where p.idUsuarioCambio=" + idUsuario.ToString()
            comm.CommandText += " and p.fechaCambio>='" + desde + "' and p.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select p.clave as clave,p.nombre as nombre,p.fechaCambio as fecha,p.horacambio as hora, u.nombre as usuario from tblproveedores as p inner join tblusuarios as u on p.idusuarioCambio=u.idUsuario from tblproveedores as p where "
            comm.CommandText += "p.fechaCambio>='" + desde + "' and p.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by p.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "cambiosProveedores")
        Return ds.Tables("cambiosProveedores")
    End Function

    Public Function modificacionesVentas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select v.folio, v.fecha, v.totalapagar, u.nombre as usuario from tblventas as v inner join tblusuarios as u on v.idusuarioCambio=u.idusuario where v.idUsuarioAlta=" + idUsuario.ToString() + " and v.fecha>='" + desde + "' and v.fecha<='" + hasta + "'"
        Else
            comm.CommandText = "select v.folio, v.fecha, v.totalapagar, u.nombre as usuario from tblventas as v inner join tblusuarios as u on v.idusuarioCambio=u.idusuario where v.fecha>='" + desde + "' and v.fecha<='" + hasta + "'"
        End If
        comm.CommandText += " order by v.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "ventas")
        Return ds.Tables("ventas")
    End Function

    Public Function modificacionesVendedores(ByVal idUsuario As Integer, ByVal desde As String, hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select v.clave as clave,v.nombre as nombre,v.fechaCambio as fecha,v.horaCambio as hora, u.nombre as usuario from tblvendedores as v inner join tblusuarios as u on v.idUsuarioCambio=u.idUsuario where v.idUsuarioCambio=" + idUsuario.ToString() + " and v.fechaCambio>='" + desde + "' and v.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select v.clave as clave,v.nombre as nombre,v.fechaCambio as fecha,v.horaCambio as hora, u.nombre as usuario from tblvendedores as v inner join tblusuarios as u on v.idUsuarioCambio=u.idUsuario  from tblvendedores as v where v.fechaCambio>='" + desde + "' and v.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by v.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "vendedores")
        Return ds.Tables("vendedores")
    End Function

    Public Function modificacionesArticulos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select i.clave as clave, i.nombre as nombre,i.fechaCambio as fecha,i.horaCambio as hora, u.nombre as usuario from tblinventario as i inner join tblusuarios as u on i.idUsuarioCambio = u.idUsuario where i.idUsuarioCambio=" + idUsuario.ToString() + " and i.fechaCambio>='" + desde + "' and i.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select i.clave as clave, i.nombre as nombre,i.fechaCambio as fecha,i.horaCambio as hora, u.nombre as usuario from tblinventario as i inner join tblusuarios as u on i.idUsuarioCambio = u.idUsuario from tblinventario as i where i.fechaCambio>='" + desde + "' and i.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by i.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "articulos")
        Return ds.Tables("articulos")
    End Function

    Public Function modificacionesTrabajadores(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select t.nombre as nombre,t.numeroempleado as numero,t.fechacambio as fecha, t.horacambio as hora, u.nombre as usuario from tbltrabajadores as t inner join tblusuarios as u on t.idUsuarioCambio=u.idUsuario where idUsuarioCambio=" + idUsuario.ToString() + " and t.fechaCambio>='" + desde + "' and t.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select t.nombre as nombre,t.numeroempleado as numero,t.fechacambio as fecha, t.horacambio as hora, u.nombre as usuario from tbltrabajadores as t inner join tblusuarios as u on t.idUsuarioCambio=u.idUsuario from tbltrabajadores as t where t.fechaCambio>='" + desde + "' and t.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by t.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "trabajadores")
        Return ds.Tables("trabajadores")
    End Function

    Public Function modificacionesListasPrecios(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select l.numero as numero, u.nombre as usuario from tbllistasprecios as l inner join tblusuarios as u  on l.idUsuarioCambio=u.idUsuario where idUsuarioCambio=" + idUsuario.ToString() + " and fechaCambio>='" + desde + "' and fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select l.numero as numero, u.nombre as usuario from tbllistasprecios as l inner join tblusuarios as u  on l.idUsuarioCambio=u.idUsuario where fechaCambio>='" + desde + "' and fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by l.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "listas")
        Return ds.Tables("listas")
    End Function

    Public Function modificacionesAlmacenes(ByVal idUsuario As Integer, ByVal desdes As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select a.numero as numero, a.nombre as nombre, a.fechaCambio as fechaCambio, a.horaCambio as hora, u.nombre as usuario from tblalmacenes as a inner join tblusuarios as u on a.idUsuarioCambio=u.idusuario where a.idUsuarioCambio=" + idUsuario.ToString() + " and a.fechaCambio>='" + desdes + "' and a.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select a.numero as numero, a.nombre as nombre, a.fechaCambio as fechaCambio, a.horaCambio as hora, u.nombre as usuario from tblalmacenes as a inner join tblusuarios as u on a.idUsuarioCambio=u.idusuario from tblalmacenes as a where a.fechaCambio>='" + desdes + "' and a.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by a.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "almacenes")
        Return ds.Tables("almacenes")
    End Function

    Public Function modificacionesCajas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select c.nombre, c.fechaCambio as fecha, c.horaCambio as hora, u.nombre as usuario from tblcajas as c inner join tblusuarios as u on c.idUsuarioCambio=u.idUsuario where c.idUsuarioCambio=" + idUsuario.ToString() + " and c.fechaCambio>='" + desde + "' and c.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select c.nombre, c.fechaCambio as fecha, c.horaCambio as hora, u.nombre as usuario from tblcajas as c inner join tblusuarios as u on c.idUsuarioCambio=u.idUsuario from tblcajas as c where c.fechaCambio>='" + desde + "' and c.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by c.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "cajas")
        Return ds.Tables("cajas")
    End Function

    Public Function modificacionesSucursales(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select s.nombre as nombre,s.fechaCambio as fecha,s.horaCambio as hora, u.nombre as usuario from tblsucursales as s inner join tblusuarios as u on s.idUsuarioCambio=u.idUsuario where idUsuarioCambio=" + idUsuario.ToString() + " and s.fechaCambio>='" + desde + "' and s.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select s.nombre as nombre,s.fechaCambio as fecha,s.horaCambio as hora, u.nombre as usuario from tblsucursales as s inner join tblusuarios as u on s.idUsuarioCambio=u.idUsuario from tblsucursales as s where s.fechaCambio>='" + desde + "' and s.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by s.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "sucursales")
        Return ds.Tables("sucursales")
    End Function

    Public Function modificacionesTiposClientes(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select t.nombre as nombre, t.fechaCambio as fecha, t.horaCambio as hora, u.nombre as usuario from tblclientestipos as t inner join tblusuarios as u on t.idUsuarioCambio=u.idusuario where t.idUsuarioCambio=" + idUsuario.ToString() + " and t.fechaCambio>='" + desde + "' and t.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select t.nombre as nombre, t.fechaCambio as fecha, t.horaCambio as hora, u.nombre as usuario from tblclientestipos as t inner join tblusuarios as u on t.idUsuarioCambio=u.idusuario where t.fechaCambio>='" + desde + "' and t.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by t.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "tipos")
        Return ds.Tables("tipos")
    End Function
End Class
