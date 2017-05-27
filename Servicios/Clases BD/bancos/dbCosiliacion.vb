Imports MySql.Data.MySqlClient
Public Class dbCosiliacion
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
  

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)

        Comm.Connection = Conexion
    End Sub
    'nuevo
    Public Function filtroDepositos(ByVal fInicio As String, ByVal fFinal As String, ByVal pBanco As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select 1 as seleccion,idDeposito as ID,Fecha,Referencia,(select nombre from tblbancoscatalogo where idBanco=Banco) as Banco,(select numero from tblcuentas where idCuenta=Banco2)as NoCuenta,(select nombre from tblbancos inner join tblcuentas on tblbancos.idBanco=tblCuentas.Banco where tblCuentas.idCuenta=Banco2)as Banco2,Cantidad from tbldepostito where idDeposito>0"
        If fInicio <> "" Then
            Comm.CommandText += " and Fecha>='" + fInicio.ToString + "' AND fecha<='" + fFinal.ToString() + "'"
        End If
        If pBanco <> "" Then
            Comm.CommandText += " and Banco2='" + pBanco + "'"
        End If
        Comm.CommandText += "order by Fecha ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldepostito")
        Return DS.Tables("tbldepostito").DefaultView
    End Function
    Public Function filtroPagoProv(ByVal fInicio As String, ByVal fFinal As String, ByVal pBanco As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select 1 as seleccion,idPagoProv as ID,Folio,(select nombre from tblproveedores where tblproveedores.idproveedor=tblpagoprov.idproveedor) Proveedor, Fecha, FechaCobro as Cobro,Referencia,(select numero from tblcuentas where idCuenta=tblpagoprov.Banco)as NoCuenta,(select nombre from tblbancoscatalogo inner join tblcuentas on tblbancoscatalogo.idBanco=tblCuentas.Banco where tblCuentas.idCuenta=tblpagoprov.Banco)as Banco,null as banco2, Cantidad as Cargo,null as abono, Tipo, IVA, Leyenda, EsCheque, Estado from tblpagoprov  where idPagoProv>0 "
        If fInicio <> "" Then
            Comm.CommandText += " and Fechacobro>='" + fInicio + "' AND  Fechacobro<='" + fFinal + "'"
        End If
        If pBanco <> "" Then
            Comm.CommandText += " and Banco='" + pBanco + "'"
        End If
        Comm.CommandText += " union all (select 1 as seleccion,idDeposito as ID,null,null,Fecha,null,Referencia,(select numero from tblcuentas where idCuenta=Banco2) as NoCuenta,(select nombre from tblbancoscatalogo where idBanco=Banco),(select nombre from tblbancos inner join tblcuentas on tblbancos.idBanco=tblCuentas.Banco where tblCuentas.idCuenta=Banco2)as Banco2,null,Cantidad as abono,null,null,null,null,null from tbldepostito where idDeposito>0"
        If fInicio <> "" Then
            Comm.CommandText += " and Fecha>='" + fInicio.ToString + "' AND fecha<='" + fFinal.ToString() + "'"
        End If
        If pBanco <> "" Then
            Comm.CommandText += " and Banco2='" + pBanco + "'"
        End If
        Comm.CommandText += ") order by Fecha ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov").DefaultView
    End Function

    Public Function filtroFecha(ByVal fInicio As String, ByVal fFinal As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select 1 as selection,idDeposito,Fecha,Referencia,Banco,Banco2,Cantidad from tbldepostito where DATE(Fecha) between'" + fInicio.ToString + "' AND '" + fFinal.ToString() + "' ORDER BY idDeposito DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldepostito")
        Return DS.Tables("tbldepostito").DefaultView
    End Function
    Public Function filtroCuenta(ByVal pBanco As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select 1 as selection,idDeposito,Fecha,Referencia,Banco,Banco2,Cantidad from tbldepostito where Banco2='" + pBanco.ToString() + "' ORDER BY idDeposito DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldepostito")
        Return DS.Tables("tbldepostito").DefaultView
    End Function
    Public Function filtroFechaPagos(ByVal fInicio As String, ByVal fFinal As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select 1 as selection,idPagoProv,Folio,Proveedor, Fecha, FechaCobro,Referencia,Banco, Cantidad,Tipo, IVA, Leyenda, EsCheque, Estado from tblpagoprov where DATE(Fecha) between'" + fInicio.ToString + "' AND '" + fFinal.ToString() + "' ORDER BY idPagoProv DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov").DefaultView
    End Function
    Public Function filtroCuentaPagos(ByVal pBanco As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select 1 as selection,idPagoProv,Folio,Proveedor, Fecha, FechaCobro,Referencia,Banco, Cantidad,Tipo, IVA, Leyenda, EsCheque, Estado from tblpagoprov where Banco='" + pBanco.ToString() + "' ORDER BY idPagoProv DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov").DefaultView
    End Function
    Public Function filtroFecha2(ByVal fInicio As String, pidCuenta As Integer) As Double 'error
        ' Dim DS As New DataSet
        Comm.CommandText = "select ifNull(sum(Cantidad),0) from tbldepostito where Fecha<'" + fInicio.ToString() + "' and banco2=" + pidCuenta.ToString
        'Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        'DA.Fill(DS, "tbldepostito")
        'Return DS.Tables("tbldepostito")
        Return Comm.ExecuteScalar
    End Function
    Public Function filtroFechaPagos2(ByVal fInicio As String, pidcuenta As Integer) As Double 'error
        ' Dim DS As New DataSet
        Comm.CommandText = "select ifNull(sum(Cantidad),0) from tblpagoprov where Fechacobro<'" + fInicio + "' and banco=" + pidcuenta.ToString
        'Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        'DA.Fill(DS, "tblpagoprov")
        'Return DS.Tables("tblpagoprov")
        Return Comm.ExecuteScalar
    End Function
  
    Public Function filtroAmbosPagos(ByVal fInicio As String, ByVal fFinal As String, ByVal pBanco As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select 1 as selection,idPagoProv,Folio,Proveedor, Fecha, FechaCobro,Referencia,Banco, Cantidad,Tipo, IVA, Leyenda, EsCheque, Estado from tblpagoprov where Banco='" + pBanco.ToString() + "' And DATE(Fecha) between'" + fInicio.ToString + "' AND '" + fFinal.ToString() + "' ORDER BY idPagoProv DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov").DefaultView
    End Function
    Public Function filtroAmbos(ByVal fInicio As String, ByVal fFinal As String, ByVal pBanco As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select 1 as selection,idDeposito,Fecha,Referencia,Banco,Banco2,Cantidad from tbldepostito where Banco2='" + pBanco.ToString() + "' And DATE(Fecha) between'" + fInicio.ToString + "' AND '" + fFinal.ToString() + "' ORDER BY idDeposito DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldepostito")
        Return DS.Tables("tbldepostito").DefaultView
    End Function
    'Especifico
    Public Function saldoInicialAmbos(ByVal fInicio As String, ByVal pBanco As String) As Double
        Comm.CommandText = "select ifnull((select sum(Cantidad) from tbldepostito where  Banco2='" + pBanco.ToString() + "' And fecha<'" + fInicio + "'),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function saldoInicialAmbosPagos(ByVal fInicio As String, ByVal pBanco As String) As Double
        Comm.CommandText = "select ifnull((select sum(Cantidad) from tblpagoprov where Banco='" + pBanco + "' And fechacobro<'" + fInicio + "'),0)"
        Return Comm.ExecuteScalar
    End Function
    '############reportes###########
    Public Function filtroAmbos1(ByVal fInicio As String, ByVal fFinal As String, ByVal pBanco As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select 1 as selection,idDeposito,Fecha,Referencia,Banco,nCuenta,Banco2,Cantidad from tbldepostito where Banco2='" + pBanco.ToString() + "' And DATE(Fecha) between'" + fInicio.ToString + "' AND '" + fFinal.ToString() + "' ORDER BY idDeposito DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldepostito")
        Return DS.Tables("tbldepostito")
    End Function
    Public Function filtroAmbosPagos1(ByVal fInicio As String, ByVal fFinal As String, ByVal pBanco As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select 1 as selection,idPagoProv,Folio,tblproveedores.nombre as Proveedor, Fecha, FechaCobro,Referencia,tblcuentas.numero as nCuenta,tblbancoscatalogo.nombre as Banco, Cantidad,tblpagoprov.Tipo, IVA, Leyenda, EsCheque, tblpagoprov.Estado from tblpagoprov inner join tblproveedores on tblpagoprov.idproveedor=tblproveedores.idproveedor inner join tblcuentas on tblcuentas.idcuenta=tblpagoprov.banco inner join tblbancoscatalogo on tblbancoscatalogo.idbanco=tblcuentas.banco where tblpagoprov.Banco=" + pBanco + " And Fechacobro>='" + fInicio + "' AND fechacobro<='" + fFinal + "' ORDER BY fecha"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov")
    End Function
    Public Function filtroFecha3(ByVal fInicio As String, ByVal nCuenta As String, ByVal nBanco As String) As DataTable 'error
        Dim DS As New DataSet
        Comm.CommandText = "select Cantidad from tbldepostito where DATE(Fecha) between' 0000/00/00' AND '" + fInicio.ToString() + "' and Banco2='" + nBanco + "' and nCuenta='" + nCuenta + "'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldepostito")
        Return DS.Tables("tbldepostito")
    End Function
    Public Function filtroFechaPagos3(ByVal fInicio As String, ByVal nCuenta As String, ByVal nBanco As String) As DataTable 'error
        Dim DS As New DataSet
        Comm.CommandText = "select Cantidad from tblpagoprov where DATE(Fecha) between' 0000/00/00' AND '" + fInicio.ToString() + "' and Banco='" + nBanco + "' and nCuenta='" + nCuenta + "'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov")
    End Function
End Class
