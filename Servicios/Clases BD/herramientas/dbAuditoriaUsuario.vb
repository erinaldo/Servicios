Imports MySql.Data.MySqlClient
Public Class dbAuditoriaUsuario
    Private comm As New MySqlCommand
    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Function modificacionesClientes(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select c.nombre as referencia, c.fechaCreacion as Alta, c.horaCreacion as horaAlta,c.fechaCambio as fecha,c.horaCambio as hora, u.nombreusuario as usuario, if(c.horaCreacion=c.horaCambio,'Alta','Cambio') as tipo,us.nombreusuario as usAlta from tblclientes as c inner join tblusuarios as u on c.idusuario=u.idusuario inner join tblusuarios as us on c.idusuarioalta=us.idusuario where c.idUsuario=" + idUsuario.ToString()
            comm.CommandText += " and c.fechaCambio>='" + desde + "' and c.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select c.nombre as referencia, c.fechaCreacion as Alta, c.horaCreacion as horaAlta,c.fechaCambio as fecha,c.horaCambio as hora, u.nombreusuario as usuario, if(c.horaCreacion=c.horaCambio,'Alta','Cambio') as tipo,us.nombreusuario as usAlta from tblclientes as c inner join tblusuarios as u on c.idusuario=u.idusuario inner join tblusuarios as us on c.idusuarioalta=us.idusuario where "
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
            comm.CommandText = "select p.nombre as referencia,p.fechaAlta as Alta, p.horaAlta as horaAlta, p.fechaCambio as fecha,p.horacambio as hora, u.nombreusuario as usuario, if(p.horaAlta=p.horaCambio,'Alta','Cambio') as tipo,us.nombreusuario as usAlta from tblproveedores as p inner join tblusuarios as u on p.idusuarioCambio=u.idUsuario inner join tblusuarios as us on p.idusuarioAlta=us.idusuario where p.idUsuarioCambio=" + idUsuario.ToString()
            comm.CommandText += " and p.fechaCambio>='" + desde + "' and p.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select p.nombre as referencia,p.fechaAlta as Alta, p.horaAlta as horaAlta, p.fechaCambio as fecha,p.horacambio as hora, u.nombreusuario as usuario, if(p.horaAlta=p.horaCambio,'Alta','Cambio') as tipo,us.nombreusuario as usAlta from tblproveedores as p inner join tblusuarios as u on p.idusuarioCambio=u.idUsuario inner join tblusuarios as us on p.idusuarioAlta=us.idusuario where "
            comm.CommandText += "p.fechaCambio>='" + desde + "' and p.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by p.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "cambiosProveedores")
        Return ds.Tables("cambiosProveedores")
    End Function
    Public Function modificacionesVendedores(ByVal idUsuario As Integer, ByVal desde As String, hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select v.nombre as referencia, v.fechaAlta as Alta, v.horaAlta as horaAlta, v.fechaCambio as fecha,v.horaCambio as hora, u.nombreusuario as usuario, if(v.horaAlta=v.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblvendedores as v inner join tblusuarios as u on v.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on v.idusuarioalta=us.idusuario where v.idUsuarioCambio=" + idUsuario.ToString() + " and v.fechaCambio>='" + desde + "' and v.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select v.nombre as referencia, v.fechaAlta as Alta, v.horaAlta as horaAlta, v.fechaCambio as fecha,v.horaCambio as hora, u.nombreusuario as usuario, if(v.horaAlta=v.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblvendedores as v inner join tblusuarios as u on v.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on v.idusuarioalta=us.idusuario where v.fechaCambio>='" + desde + "' and v.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by v.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "vendedores")
        Return ds.Tables("vendedores")
    End Function

    Public Function modificacionesArticulos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(i.clave,' ', i.nombre) as referencia, i.fechaAlta as Alta, i.horaAlta as horaAlta, i.fechaCambio as fecha,i.horaCambio as hora, u.nombreusuario as usuario, if(i.horaAlta=i.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventario as i inner join tblusuarios as u on i.idUsuarioCambio = u.idUsuario inner join tblusuarios as us on i.idusuarioalta=us.idusuario where i.idUsuarioCambio=" + idUsuario.ToString() + " and i.fechaCambio>='" + desde + "' and i.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(i.clave,' ', i.nombre) as referencia, i.fechaAlta as Alta, i.horaAlta as horaAlta, i.fechaCambio as fecha,i.horaCambio as hora, u.nombreusuario as usuario, if(i.horaAlta=i.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventario as i inner join tblusuarios as u on i.idUsuarioCambio = u.idUsuario inner join tblusuarios as us on i.idusuarioalta=us.idusuario where i.fechaCambio>='" + desde + "' and i.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by i.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "articulos")
        Return ds.Tables("articulos")
    End Function

    Public Function modificacionesTrabajadores(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select t.nombre as referencia, t.fechaAlta as Alta, t.horaAlta as horaAlta, t.fechacambio as fecha, t.horacambio as hora, u.nombreusuario as usuario, if(t.horaAlta=t.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbltrabajadores as t inner join tblusuarios as u on t.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on t.idusuarioalta=us.idusuario where t.idUsuarioCambio=" + idUsuario.ToString() + " and t.fechaCambio>='" + desde + "' and t.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select t.nombre as referencia, t.fechaAlta as Alta, t.horaAlta as horaAlta, t.fechacambio as fecha, t.horacambio as hora, u.nombreusuario as usuario, if(t.horaAlta=t.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbltrabajadores as t inner join tblusuarios as u on t.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on t.idusuarioalta=us.idusuario where t.fechaCambio>='" + desde + "' and t.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by t.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "trabajadores")
        Return ds.Tables("trabajadores")
    End Function

    Public Function modificacionesListasPrecios(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select l.numero as referencia, l.fechaAlta as Alta, l.horaAlta as horaAlta, u.nombreusuario as usuario, if(l.horaAlta=l.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbllistasprecios as l inner join tblusuarios as u  on l.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on l.idusuarioalta=us.idusuario where idUsuarioCambio=" + idUsuario.ToString() + " and fechaCambio>='" + desde + "' and fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select l.numero as referencia, l.fechaAlta as Alta, l.horaAlta as horaAlta, u.nombreusuario as usuario, if(l.horaAlta=l.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbllistasprecios as l inner join tblusuarios as u  on l.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on l.idusuarioalta=us.idusuario where fechaCambio>='" + desde + "' and fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by l.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "listas")
        Return ds.Tables("listas")
    End Function

    Public Function modificacionesAlmacenes(ByVal idUsuario As Integer, ByVal desdes As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(a.numero,' ',a.nombre) as referencia, a.fechaAlta as Alta, a.horaAlta as horaAlta, a.fechaCambio as fecha, a.horaCambio as hora, u.nombreusuario as usuario, if(a.horaAlta=a.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblalmacenes as a inner join tblusuarios as u on a.idUsuarioCambio=u.idusuario inner join tblusuarios as us on a.idusuarioalta=us.idusuario where a.idUsuarioCambio=" + idUsuario.ToString() + " and a.fechaCambio>='" + desdes + "' and a.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(a.numero,' ',a.nombre) as referencia, a.fechaAlta as Alta, a.horaAlta as horaAlta, a.fechaCambio as fecha, a.horaCambio as hora, u.nombreusuario as usuario, if(a.horaAlta=a.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblalmacenes as a inner join tblusuarios as u on a.idUsuarioCambio=u.idusuario inner join tblusuarios as us on a.idusuarioalta=us.idusuario where a.fechaCambio>='" + desdes + "' and a.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by a.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "almacenes")
        Return ds.Tables("almacenes")
    End Function

    Public Function modificacionesCajas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select c.nombre as referencia, c.fechaAlta as Alta, c.horaAlta as horaAlta, c.fechaCambio as fecha, c.horaCambio as hora, u.nombreusuario as usuario, if(c.horaAlta=c.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcajas as c inner join tblusuarios as u on c.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on c.idusuarioalta=us.idusuario where c.idUsuarioCambio=" + idUsuario.ToString() + " and c.fechaCambio>='" + desde + "' and c.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select c.nombre as referencia, c.fechaAlta as Alta, c.horaAlta as horaAlta, c.fechaCambio as fecha, c.horaCambio as hora, u.nombreusuario as usuario, if(c.horaAlta=c.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcajas as c inner join tblusuarios as u on c.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on c.idusuarioalta=us.idusuario where c.fechaCambio>='" + desde + "' and c.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by c.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "cajas")
        Return ds.Tables("cajas")
    End Function

    Public Function modificacionesSucursales(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select s.nombre as referencia, s.fechaAlta as Alta, s.horaAlta as horaAlta,s.fechaCambio as fecha,s.horaCambio as hora, u.nombreusuario as usuario, if(s.horaAlta=s.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsucursales as s inner join tblusuarios as u on s.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on s.idusuarioalta=us.idusuario where s.idUsuarioCambio=" + idUsuario.ToString() + " and s.fechaCambio>='" + desde + "' and s.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select s.nombre as referencia, s.fechaAlta as Alta, s.horaAlta as horaAlta,s.fechaCambio as fecha,s.horaCambio as hora, u.nombreusuario as usuario, if(s.horaAlta=s.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsucursales as s inner join tblusuarios as u on s.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on s.idusuarioalta=us.idusuario where s.fechaCambio>='" + desde + "' and s.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by s.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "sucursales")
        Return ds.Tables("sucursales")
    End Function

    Public Function modificacionesTiposClientes(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select t.nombre as referencia, t.fechaAlta as Alta, t.horaAlta as horaAlta, t.fechaCambio as fecha, t.horaCambio as hora, u.nombreusuario as usuario, if(t.horaAlta=t.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblclientestipos as t inner join tblusuarios as u on t.idUsuarioCambio=u.idusuario inner join tblusuarios as us on t.idusuarioalta=us.idusuario where t.idUsuarioCambio=" + idUsuario.ToString() + " and t.fechaCambio>='" + desde + "' and t.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select t.nombre as referencia, t.fechaAlta as Alta, t.horaAlta as horaAlta, t.fechaCambio as fecha, t.horaCambio as hora, u.nombreusuario as usuario, if(t.horaAlta=t.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblclientestipos as t inner join tblusuarios as u on t.idUsuarioCambio=u.idusuario inner join tblusuarios as us on t.idusuarioalta=us.idusuario where t.fechaCambio>='" + desde + "' and t.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by t.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "tipos")
        Return ds.Tables("tipos")
    End Function

    Public Function modificacionesZonas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select z.zona as referencia, z.fechaAlta as Alta, z.horaAlta as horaAlta, z.horaCambio as hora, z.fechaCambio as fecha, u.nombreusuario as usuario, if(z.horaAlta=z.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblzona as z inner join tblusuarios as u on z.idUsuarioCambio=u.idusuario inner join tblusuarios as us on z.idusuarioalta=us.idusuario where z.idUsuarioCambio=" + idUsuario.ToString() + " and z.fechaCambio>='" + desde + "' and z.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select z.zona as referencia, z.fechaAlta as Alta, z.horaAlta as horaAlta, z.horaCambio as hora, z.fechaCambio as fecha, u.nombreusuario as usuario, if(z.horaAlta=z.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblzona as z inner join tblusuarios as u on z.idUsuarioCambio=u.idusuario inner join tblusuarios as us on z.idusuarioalta=us.idusuario where z.fechaCambio>='" + desde + "' and z.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by z.horaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "zonas")
        Return ds.Tables("zonas")
    End Function

    Public Function modificacionesTiposFormasPagos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select fp.nombre as referencia, fp.fechaAlta as Alta, fp.horaAlta as horaAlta, fp.fechaCambio as fecha, fp.horaCambio as hora, u.nombreusuario as usuario, if(fp.horaAlta=fp.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblformasdepago as fp inner join tblusuarios as u on fp.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on fp.idusuarioalta=us.idusuario where fp.idUsuarioCambio=" + idUsuario.ToString() + " and fp.fechaCambio>='" + desde + "' and fp.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select fp.nombre as referencia, fp.fechaAlta as Alta, fp.horaAlta as horaAlta, fp.fechaCambio as fecha, fp.horaCambio as hora, u.nombreusuario as usuario, if(fp.horaAlta=fp.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblformasdepago as fp inner join tblusuarios as u on fp.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on fp.idusuarioalta=us.idusuario where fp.fechaCambio>='" + desde + "' and fp.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by fp.fechaCambio"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "formasPago")
        Return ds.Tables("formasPago")
    End Function

    Public Function modificacionesTiposCantidades(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbltiposcantidades as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbltiposcantidades as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "tiposCantidades")
        Return ds.Tables("tiposCantidades")
    End Function

    Public Function modificacionesDepartamentos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldepartamentos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldepartamentos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "departamentos")
        Return ds.Tables("departamentos")
    End Function

    Public Function modificacionesDepartamentosAreas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldepartamentosareas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldepartamentosareas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "departamentosAreas")
        Return ds.Tables("departamentosAreas")
    End Function
    Public Function modificacionesModelos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblmodelos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblmodelos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "modelos")
        Return ds.Tables("modelos")
    End Function

    Public Function modificacionesConceptosInventario(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventarioconceptos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventarioconceptos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "conceptosinventario")
        Return ds.Tables("conceptosinventario")
    End Function

    Public Function modificacionesMonedas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblmonedas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblmonedas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "monedas")
        Return ds.Tables("monedas")
    End Function

    Public Function modificacionesCodigosBarras(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcodigobarras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcodigobarras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "codigos")
        Return ds.Tables("codigos")
    End Function

    Public Function modificacionesDescuentos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.descuento as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldescuentos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.descuento as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldescuentos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "descuentos")
        Return ds.Tables("descuentos")
    End Function

    Public Function modificacionesTallas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbltallas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbltallas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "tallas")
        Return ds.Tables("tallas")
    End Function

    Public Function modificacionesColores(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcolores as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcolores as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "colores")
        Return ds.Tables("colores")
    End Function

    Public Function modificacionesTecnicos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbltecnicos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbltecnicos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "tecnicos")
        Return ds.Tables("tecnicos")
    End Function

    Public Function modificacionesProveedoresCuentas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.cuenta as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, p.nombre as nombre, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblproveedorescuentas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblproveedores as p on tc.idProv=p.idproveedor inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.cuenta as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, p.nombre as nombre, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblproveedorescuentas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblproveedores as p on tc.idProv=p.idproveedor inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "proveedorescuentas")
        Return ds.Tables("proveedorescuentas")
    End Function
    Public Function modificacionesClientesCuentas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.cuenta as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, p.nombre as nombre, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblclientescuentas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblclientes as p on tc.idcliente=p.idcliente inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.cuenta as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, p.nombre as nombre, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblclientescuentas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblclientes as p on tc.idcliente=p.idcliente inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "clientesCuentas")
        Return ds.Tables("clientesCuentas")
    End Function

    Public Function modificacionesClientesEquipos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblclientesequipos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblclientesequipos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "clientesEquipos")
        Return ds.Tables("clientesEquipos")
    End Function

    Public Function modificacionesConceptosNotasCompras(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblconceptosnotascompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblconceptosnotascompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "conceptos")
        Return ds.Tables("conceptos")
    End Function

    Public Function modificacionesClasificaciones1(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventarioclasificaciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventarioclasificaciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "clasificaciones1")
        Return ds.Tables("clasificaciones1")
    End Function

    Public Function modificacionesClasificaciones2(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventarioclasificaciones2 as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventarioclasificaciones2 as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "clasificaciones2")
        Return ds.Tables("clasificaciones2")
    End Function
    Public Function modificacionesClasificaciones3(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventarioclasificaciones3 as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventarioclasificaciones3 as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "clasificaciones3")
        Return ds.Tables("clasificaciones3")
    End Function
    Public Function modificacionesConceptosNotasVentas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblconceptosnotasventas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblconceptosnotasventas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "conceptos")
        Return ds.Tables("conceptos")
    End Function

    Public Function modificacionesVentas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "ventas")
        Return ds.Tables("ventas")
    End Function

    Public Function modificacionesVentasPedidos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventaspedidos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventaspedidos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "ventasPedidos")
        Return ds.Tables("ventasPedidos")
    End Function

    Public Function modificacionesVentasCotizaciones(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventascotizaciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventascotizaciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "ventascotizaciones")
        Return ds.Tables("ventascotizaciones")
    End Function

    Public Function modificacionesVentasRemisioes(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventasremisiones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventasremisiones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "remisiones")
        Return ds.Tables("remisiones")
    End Function

    Public Function modificacionesVentasApartados(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventasapartados as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventasapartados as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "remisiones")
        Return ds.Tables("remisiones")
    End Function

    Public Function modificacionesVentasPagos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.tipo as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventaspagos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.tipo as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventaspagos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "ventasPagos")
        Return ds.Tables("ventasPagos")
    End Function

    Public Function modificacionesVentasNotasDeCargo(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblnotasdecargo as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblnotasdecargo as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "notasDeCargo")
        Return ds.Tables("notasDeCargo")
    End Function

    Public Function modificacionesVentasNotasDeCredito(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblnotasdecredito as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblnotasdecredito as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "notasDeCredito")
        Return ds.Tables("notasDeCredito")
    End Function

    Public Function modificacionesCompras(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.referencia as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.referencia as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "compras")
        Return ds.Tables("compras")
    End Function

    Public Function modificacionesComprasCotizaciones(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcomprascotizacionesb as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcomprascotizacionesb as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "comprasCotizaciones")
        Return ds.Tables("comprasCotizaciones")
    End Function

    Public Function modificacionesComprasPedidos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcompraspedidos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcompraspedidos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "comprasPedidos")
        Return ds.Tables("comprasPedidos")
    End Function

    Public Function modificacionesComprasRemisiones(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcomprasremisiones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcomprasremisiones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "comprasRemisiones")
        Return ds.Tables("comprasRemisiones")
    End Function

    Public Function modificacionesComprasNotasDeCargo(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblnotasdecargocompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblnotasdecargocompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "comprasNotasDeCargo")
        Return ds.Tables("comprasNotasDeCargo")
    End Function

    Public Function modificacionesComprasNotasDeCredito(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblnotasdecreditocompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblnotasdecreditocompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "comprasNotasDeCredito")
        Return ds.Tables("comprasNotasDeCredito")
    End Function

    Public Function modificacionesMovimientosInventario(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.fechaCambio as fecha, concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblmovimientos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.fechaCambio as fecha, concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblmovimientos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "movimientosInventario")
        Return ds.Tables("movimientosInventario")
    End Function

    Public Function modificacionesMovimientosCajas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select c.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcajasmovimientos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblcajas as c on tc.idcaja=c.idcaja inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select c.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcajasmovimientos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblcajas as c on tc.idcaja=c.idcaja inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "movimientosCajas")
        Return ds.Tables("movimientosCajas")
    End Function

    'Public Function modificacionesBancos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
    '    Dim ds As New DataSet
    '    If idUsuario > 0 Then
    '        comm.CommandText = "select tc.nombre as referencia, tc.horaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo from tblbancos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
    '    Else
    '        comm.CommandText = "select tc.nombre as referencia, tc.horaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo from tblbancos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
    '    End If
    '    comm.CommandText += " order by tc.fechaCambio;"
    '    Dim da As New MySqlDataAdapter(comm)
    '    da.Fill(ds, "bancos")
    '    Return ds.Tables("bancos")
    'End Function

    Public Function modificacionesNominas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.folio as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblnominas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.folio as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblnominas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "nominas")
        Return ds.Tables("nominas")
    End Function

    Public Function modificacionesEmpenosConfiguracion(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select  tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, 'Cambio de configuración' as referencia, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempeniosconfiguracion as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select  tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, 'Cambio de configuración' as referencia, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempeniosconfiguracion as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "empeniosConfiguracion")
        Return ds.Tables("empeniosConfiguracion")
    End Function



    Public Function modificacionesEmpeniosAdjudicaciones(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(e.serie,e.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempeniosadjudicaciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblempenios as e on tc.idempenio=e.idmovimiento inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(e.serie,e.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempeniosadjudicaciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblempenios as e on tc.idempenio=e.idmovimiento inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "empeniosAdjudicaciones")
        Return ds.Tables("empeniosAdjudicaciones")
    End Function

    Public Function modificacionesEmpenios(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempenios as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempenios as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "empenios")
        Return ds.Tables("empenios")
    End Function

    Public Function modificacionesEmpeniosPagos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(e.serie,e.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempeniosabono as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblempenios as e on tc.idEmpenio=e.idmovimiento inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(e.serie,e.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempeniosabono as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblempenios as e on tc.idEmpenio=e.idmovimiento inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "empeniosPagos")
        Return ds.Tables("empeniosPagos")
    End Function

    Public Function modificacionesEmpeniosCompras(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempenioscompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempenioscompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "empeniosCompras")
        Return ds.Tables("empeniosCompras")
    End Function

    Public Function modificacionesContabilidadConfiguracion(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, 'Cambio de configuración' as referencia, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcontabilidadconf as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, 'Cambio de configuración' as referencia, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcontabilidadconf as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "empeniosConfiguracion")
        Return ds.Tables("empeniosConfiguracion")
    End Function

    Public Function modificacionesContabilidadCatalogoCuentas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.descripcion as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblccontables as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.descripcion as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblccontables as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "catalogoDeCuentas")
        Return ds.Tables("catalogoDeCuentas")
    End Function

    Public Function modificacionesContabilidadPolizas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.tipo,' ',tc.numero) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblpolizas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.tipo,' ',tc.numero) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta  from tblpolizas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "polizas")
        Return ds.Tables("polizas")
    End Function

    Public Function modificacionesFertlizantesPedidos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblfertilizantespedidos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblfertilizantespedidos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "fertilizantesPedidos")
        Return ds.Tables("fertilizantesPedidos")
    End Function

    Public Function modificacionesFertlizantesMovimientos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblfertilizantesmovimientos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblfertilizantesmovimientos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "fertilizantesMovimientos")
        Return ds.Tables("fertilizantesMovimientos")
    End Function

    Public Function modificacionesSemillasBoletas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsemillasboletas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsemillasboletas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "semillasBoletas")
        Return ds.Tables("semillasBoletas")
    End Function

    Public Function modificacionesSemillasLiquidaciones(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsemillasliquidacion as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsemillasliquidacion as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "semillasLiquidaciones")
        Return ds.Tables("semillasLiquidaciones")
    End Function

    Public Function modificacionesSemillasComprobantes(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsemillascomprobante as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsemillascomprobante as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "semillasComprobantes")
        Return ds.Tables("semillasComprobantes")
    End Function

    Public Function modificacionesSemillasAnticipos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.medio as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsemillasanticipos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.medio as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsemillasanticipos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "semillasAnticipos")
        Return ds.Tables("semillasAnticipos")
    End Function

    Public Function modificacionesSemillasAnticiposFacturas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(f.serie,f.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblfertilizantesmovimientos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblventas as f on tc.idfactura=f.idventa inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(f.serie,f.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblfertilizantesmovimientos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblventas as f on tc.idfactura)f.idventa inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "semillasAnticiposFacturas")
        Return ds.Tables("semillasAnticiposFacturas")
    End Function

    Public Function modificacionesClientesImpuetos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblclientesimpuestos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblclientesimpuestos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "clientesImpuestos")
        Return ds.Tables("clientesImpuestos")
    End Function

    Public Function modificacionesSucursalesCertificados(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.noserie as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsucursalescertificados as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.noserie as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsucursalescertificados as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "sucursalesCertificados")
        Return ds.Tables("sucursalesCertificados")
    End Function

    Public Function modificacionesSucursalesFolios(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.serie as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsucursalesfolios as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.serie as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsucursalesfolios as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "sucursalesFolios")
        Return ds.Tables("sucursalesFolios")
    End Function

    Public Function modificacionesSucursalesEquipos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.noserie as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsucequipos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.noserie as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblsucequipos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "sucursalesEquipos")
        Return ds.Tables("sucursalesEquipos")
    End Function

    Public Function modificacionesDetallesEquipos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select e.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldetallesequipos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblinventario as e on tc.idinventario=e.idinventario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select e.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldetallesequipos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblinventario as e on tc.idinventario=e.idinventario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "detallesEquipos")
        Return ds.Tables("detallesEquipos")
    End Function

    Public Function modificacionesDetalleEquiposS(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select i.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldetallesequiposs as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblinventario as i on tc.idinventario=i.idinventario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select i.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldetallesequiposs as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblinventario as i on tc.idinventario=i.idinventario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "detallesEquiposS")
        Return ds.Tables("detallesEquiposS")
    End Function

    Public Function modificacionesInventarioDetalles(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select i.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventariodetalles as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblinventario as i on tc.idinventario=i.idinventario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select i.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventariodetalles as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblinventario as i on tc.idinventario=i.idinventario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "inventarioDetalles")
        Return ds.Tables("inventarioDetalles")
    End Function

    Public Function modificacionesInventarioRelaciones(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventariorelaciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblinventariorelaciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "inventarioRelaciones")
        Return ds.Tables("inventarioRelaciones")
    End Function

    Public Function modificacionesFormasPagoRemisiones(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblformasdepagoremisiones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblformasdepagoremisiones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "formasPagoRemisiones")
        Return ds.Tables("formasPagoRemisiones")
    End Function

    Public Function modificacionesServiciosClasificaciones(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblserviciosclasificaciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblserviciosclasificaciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "serviciosClasificaciones")
        Return ds.Tables("serviciosClasificaciones")
    End Function

    Public Function modificacionesServiciosClasificaciones2(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblserviciosclasificaciones2 as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblserviciosclasificaciones2 as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "serviciosClasificaciones2")
        Return ds.Tables("serviciosClasificaciones2")
    End Function

    Public Function modificacionesUsuario(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, tc.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblusuarios as tc inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, tc.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblusuarios as tc inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "usuarios")
        Return ds.Tables("usuarios")
    End Function

    Public Function modificacionesVentasPagosApartados(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.tipo as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventaspagosapartados as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.tipo as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblventaspagosapartados as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "ventasPagosApartados")
        Return ds.Tables("ventasPagosApartados")
    End Function

    Public Function modificacionesDocumentosClientes(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldocumentosclientes as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldocumentosclientes as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "documentosClientes")
        Return ds.Tables("documentosClientes")
    End Function
    Public Function modificacionesDevoluciones(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldevoluciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldevoluciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "devoluciones")
        Return ds.Tables("devoluciones")
    End Function


    Public Function modificacionesDevolucionesCompras(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldevolucionescompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldevolucionescompras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "devolucionesCompras")
        Return ds.Tables("devolucionesCompras")
    End Function

    Public Function modificacionesDocumentosProveedores(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldocumentosproveedores as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tbldocumentosproveedores as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "documentosProveedores")
        Return ds.Tables("documentosProveedores")
    End Function

    Public Function modificacionesCuentas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.numero as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcuentas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.numero as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcuentas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "cuentas")
        Return ds.Tables("cuentas")
    End Function

    Public Function modificacionesGastosClasificacion(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblgastosclasificacion as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblgastosclasificacion as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "gastosClasificacion")
        Return ds.Tables("gastosClasificacion")
    End Function

    Public Function modificacionesGastosClasificacion2(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblgastosclasificacion2 as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblgastosclasificacion2 as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "gastosClasificacion2")
        Return ds.Tables("gastosClasificacion2")
    End Function

    Public Function modificacionesGastosClasificacion3(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblgastosclasificacion3 as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblgastosclasificacion3 as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "gastosClasificacion3")
        Return ds.Tables("gastosClasificacion3")
    End Function

    Public Function modificacionesGastos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblgastos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select concat(tc.serie,tc.folio) as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblgastos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "gastos")
        Return ds.Tables("gastos")
    End Function

    Public Function modificacionesGastosProgramables(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblgastosprogramables as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblgastosprogramables as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "gastosProgramables")
        Return ds.Tables("gastosProgramables")
    End Function

    Public Function modificacionesIdentificacion(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblidentificacion as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblidentificacion as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "identificacion")
        Return ds.Tables("identificacion")
    End Function

    Public Function modificacionesEmpeniosClasificacion(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempeniosclasificacion as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblempeniosclasificacion as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "empeniosClasificacion")
        Return ds.Tables("empeniosClasificacion")
    End Function

    Public Function modificacionesContabilidadClas(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcontabilidadclas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.nombre as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcontabilidadclas as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "contabilidadClas")
        Return ds.Tables("contabilidadClas")
    End Function

    Public Function modificacionesContabilidadMascaras(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.titulo as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcontabilidadmascaras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.titulo as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblcontabilidadmascaras as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "contabilidadMascaras")
        Return ds.Tables("contabilidadMascaras")
    End Function
    Public Function modificacionesOpciones(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select 'Cambio' as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblopciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select 'Cambio' as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblopciones as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "opciones")
        Return ds.Tables("opciones")
    End Function
    Public Function modificacionesServicios(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.detalles as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblservicios as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.detalles as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblservicios as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "servicios")
        Return ds.Tables("servicios")
    End Function
    Public Function modificacionesServiciosEventos(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.comentario as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblservicioseventos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.comentario as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblservicioseventos as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "serviciosEventos")
        Return ds.Tables("serviciosEventos")
    End Function

    Public Function modificacionesServiciosEstados(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.estado as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblserviciosestados as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.estado as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblserviciosestados as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "serviciosEstados")
        Return ds.Tables("serviciosEstados")
    End Function

    Public Function modificacionesServiciosSuc(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.detalles as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblserviciossuc as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.detalles as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblserviciossuc as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "serviciosSuc")
        Return ds.Tables("serviciosSuc")
    End Function

    Public Function modificacionesServiciosEventosSuc(ByVal idUsuario As Integer, ByVal desde As String, ByVal hasta As String) As DataTable
        Dim ds As New DataSet
        If idUsuario > 0 Then
            comm.CommandText = "select tc.comentario as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblservicioseventossuc as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.idUsuarioCambio=" + idUsuario.ToString() + " and tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        Else
            comm.CommandText = "select tc.comentario as referencia, tc.fechaAlta as Alta, tc.horaAlta as horaAlta, tc.fechaCambio as fecha, tc.horaCambio as hora, u.nombreusuario as usuario, if(tc.horaAlta=tc.horaCambio,'Alta','Cambio') as tipo, us.nombreusuario as usAlta from tblservicioseventossuc as tc inner join tblusuarios as u on tc.idUsuarioCambio=u.idUsuario inner join tblusuarios as us on tc.idusuarioalta=us.idusuario where tc.fechaCambio>='" + desde + "' and tc.fechaCambio<='" + hasta + "'"
        End If
        comm.CommandText += " order by tc.fechaCambio;"
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "serviciosEventosSuc")
        Return ds.Tables("serviciosEventosSuc")
    End Function
End Class
