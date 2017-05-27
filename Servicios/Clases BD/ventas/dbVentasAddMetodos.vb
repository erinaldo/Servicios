Public Class dbVentasAddMetodos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub
    Public Sub Guardar(pTipo As Byte, pIdforma As Integer, pCantidad As Double, pIdMovimiento As Integer)
        Select Case pTipo
            Case 0
                Comm.CommandText = "insert into tblventasformasdepago(idventa,idforma,cantidad) values(" + pIdMovimiento.ToString + "," + pIdforma.ToString + "," + pCantidad.ToString + ")"
            Case 1
                Comm.CommandText = "insert into tblremisionesformasdepago(idremision,idforma,cantidad) values(" + pIdMovimiento.ToString + "," + pIdforma.ToString + "," + pCantidad.ToString + ")"
            Case 2
                Comm.CommandText = "insert into tblnominasformasdepago(idnomina,idforma,cantidad) values(" + pIdMovimiento.ToString + "," + pIdforma.ToString + "," + pCantidad.ToString + ")"
        End Select
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(pTipo As Byte, pIdmetodo As Integer)
        Select Case pTipo
            Case 0
                Comm.CommandText = "delete from tblventasformasdepago where idmetodo=" + pIdmetodo.ToString
            Case 1
                Comm.CommandText = "delete from tblremisionesformasdepago where idmetodo=" + pIdmetodo.ToString
            Case 2
                Comm.CommandText = "delete from tblnominasformasdepago where idmetodo=" + pIdmetodo.ToString
        End Select
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaReader(pTipo As Byte, pidMovimiento As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Select Case pTipo
            Case 0
                Comm.CommandText = "select vfp.cantidad,fp.clavesat,fp.nombre,vfp.idforma from tblventasformasdepago vfp inner join tblformasdepago fp on vfp.idforma=fp.idforma where vfp.idventa=" + pidMovimiento.ToString + " order by vfp.cantidad desc"
            Case 1
                Comm.CommandText = "select vfp.cantidad,'',fp.nombre,vfp.idforma from tblremisionesformasdepago vfp inner join tblformasdepagoremisiones fp on vfp.idforma=fp.idforma where vfp.idremision=" + pidMovimiento.ToString + " order by vfp.cantidad desc"
            Case 2
                Comm.CommandText = "select vfp.cantidad,fp.clavesat,fp.nombre,vfp.idforma from tblnominasformasdepago vfp inner join tblformasdepago fp on vfp.idforma=fp.idforma where vfp.idnomina=" + pidMovimiento.ToString + " order by vfp.cantidad desc"
        End Select
        Return Comm.ExecuteReader
    End Function
    Public Function Consulta(pTipo As Byte, pidMovimiento As Integer) As DataView
        Dim DS As New DataSet
        Select Case pTipo
            Case 0
                Comm.CommandText = "select vfp.idmetodo,fp.clavesat,fp.nombre,vfp.cantidad from tblventasformasdepago vfp inner join tblformasdepago fp on vfp.idforma=fp.idforma where vfp.idventa=" + pidMovimiento.ToString + " order by vfp.cantidad desc"
            Case 1
                Comm.CommandText = "select vfp.idmetodo,'',fp.nombre,vfp.cantidad from tblremisionesformasdepago vfp inner join tblformasdepagoremisiones fp on vfp.idforma=fp.idforma where vfp.idremision=" + pidMovimiento.ToString + " order by vfp.cantidad desc"
            Case 2
                Comm.CommandText = "select vfp.idmetodo,fp.clavesat,fp.nombre,vfp.cantidad from tblnominasformasdepago vfp inner join tblformasdepago fp on vfp.idforma=fp.idforma where vfp.idnomina=" + pidMovimiento.ToString + " order by vfp.cantidad desc"
        End Select
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfp")
        Return DS.Tables("tblfp").DefaultView
    End Function
    Public Function TotalAgregado(pTipo As Byte, pIdMov As Integer) As Double
        Select Case pTipo
            Case 0
                Comm.CommandText = "select ifnull((select sum(cantidad) from tblventasformasdepago where idventa=" + pIdMov.ToString + "),0)"
            Case 1
                Comm.CommandText = "select ifnull((select sum(cantidad) from tblremisionesformasdepago where idremision=" + pIdMov.ToString + "),0)"
            Case 2
                Comm.CommandText = "select ifnull((select sum(cantidad) from tblnominasformasdepago where idnomina=" + pIdMov.ToString + "),0)"
        End Select
        Return Comm.ExecuteScalar
    End Function
    Public Function TotalAgregadoPorTipoRemisiones(pTipo As Byte, pIdMov As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(cantidad) from tblremisionesformasdepago inner join tblformasdepagoremisiones on tblremisionesformasdepago.idforma=tblformasdepagoremisiones.idforma where idremision=" + pIdMov.ToString + " and tblformasdepagoremisiones.tipo=" + pTipo.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Sub RemoverTodo(pTipo As Byte, pIdMo As Integer)
        Select Case pTipo
            Case 0
                Comm.CommandText = "delete from tblventasformasdepago where idventa=" + pIdMo.ToString
            Case 1
                Comm.CommandText = "delete from tblremisionesformasdepago where idremision=" + pIdMo.ToString
            Case 2
                Comm.CommandText = "delete from tblnominasformasdepago where idnomina=" + pIdMo.ToString
        End Select
        Comm.ExecuteNonQuery()
    End Sub
End Class
