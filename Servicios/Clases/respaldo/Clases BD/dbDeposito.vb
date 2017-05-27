Imports MySql.Data.MySqlClient
Public Class dbDeposito
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Fecha, Referencia As String
    Public Banco As Integer
    Public Cantidad As Double
    Public ID As Integer
    Public Elabora, Autoriza, Registra, Solicitud, NumPoliza, cuenta As String
    Public Banco2 As Integer
    Public comentario As String
    Public IdPagoProv As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)

        Comm.Connection = Conexion
    End Sub

    Public Function Guardar(ByVal pFecha As String, ByVal pReferencia As String, ByVal pBanco As String, ByVal pCantidad As String, ByVal pBanco2 As String, ByVal pComentario As String) As Integer
        Dim id As Integer
        Comm.CommandText = "insert into tbldepostito (Fecha,Referencia,Banco,Cantidad,Banco2,comentario,idpagoprov) values('" + Replace(pFecha, "'", "''") + "','" + Replace(pReferencia, "'", "''") + "'," + pBanco + "," + pCantidad + "," + pBanco2 + ",'" + Replace(pComentario, "'", "''") + "',0)"
        Comm.ExecuteNonQuery()
        'Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select ifnull((select max(iddeposito) from tbldepostito),0)"
        id = Comm.ExecuteScalar
        Return id
    End Function
    Public Sub LigarAComprasPagos(ByVal pId As Integer, ByVal pWherestr As String)
        pWherestr = pWherestr.Substring(3, pWherestr.Length - 3)
        Comm.CommandText = "update tblventaspagos set iddeposito=" + pId.ToString + " where " + pWherestr
        Comm.ExecuteNonQuery()
    End Sub
    Public Function LigadoABancos(ByVal pIdDeposito As Integer) As Boolean
        Comm.CommandText = "select ifnull((select count(iddeposito) from tblventaspagos where iddeposito=" + pIdDeposito.ToString + "),0)"
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function Repetida(ByVal pFecha As String, ByVal pReferencia As String, ByVal pBanco As String, ByVal pCantidad As String) As Integer
        Dim num As Integer

        Comm.CommandText = "select count(idDeposito) from tbldepostito where Fecha='" + Replace(pFecha, "'", "''") + "' and Referencia='" + Replace(pReferencia, "'", "''") + "' and Banco=" + pBanco + " and Cantidad=" + pCantidad
        num = Comm.ExecuteNonQuery()
        Return num
    End Function

    Public Sub Modificar(ByVal pFecha As String, ByVal pReferencia As String, ByVal pBanco As String, ByVal pCantidad As String, ByVal pID As Integer, ByVal pBanco2 As String, ByVal pComentario As String)
        'Modificar
        Comm.CommandText = "update tbldepostito set Fecha='" + Replace(pFecha, "'", "''") + "', Referencia='" + Replace(pReferencia, "'", "''") + "', Banco=" + pBanco + ", Cantidad=" + pCantidad + " ,Banco2=" + pBanco2 + ", comentario='" + Replace(pComentario, "'", "''") + "' where idDeposito=" + pID.ToString()
        Comm.ExecuteNonQuery()
    End Sub
    Public Function filtroTodos(pFecha1 As String, pFecha2 As String, pReferenca As String) As DataView
        Dim DS As New DataSet

        'dr("id") = tabla.Rows(i)(0).ToString
        'dr("Fecha") = tabla.Rows(i)(1).ToString
        'dr("Referencia") = tabla.Rows(i)(2).ToString
        'dr("Banco") = P.buscarBanco(tabla.Rows(i)(3).ToString)
        'dr("Cantidad") = tabla.Rows(i)(4).ToString
        'dr("N. Cuenta") = P.buscarNumeroCuenta(tabla.Rows(i)(6).ToString)
        'dr("Banco2") = P.buscarBancoCuenta(tabla.Rows(i)(6).ToString)

        Comm.CommandText = "select iddeposito,fecha,referencia,(select nombre from tblbancoscatalogo where idbanco=tbldepostito.banco),cantidad,tblcuentas.numero,(select nombre from tblbancoscatalogo where idbanco=tblcuentas.banco) from tbldepostito inner join tblcuentas on tbldepostito.banco2=tblcuentas.idcuenta where tbldepostito.fecha>='" + pFecha1 + "' and tbldepostito.fecha<='" + pFecha2 + "'"
        Comm.CommandText += " and tbldepostito.referencia like '%" + Replace(Trim(pReferenca), "'", "''") + "%'"
        Comm.CommandText += " ORDER BY fecha DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldepostito")
        Return DS.Tables("tbldepostito").DefaultView
    End Function
    Public Function buscarBancoCuenta(ByVal idCuenta As String) As String
        Dim banco As String
        Dim num As String
        Dim cantidad As Integer

        Comm.CommandText = "select count(Banco) from tblcuentas where idCuenta=" + idCuenta
        cantidad = Comm.ExecuteScalar

        If cantidad = 0 Then
            banco = " "
        Else
            Comm.CommandText = "select Banco from tblcuentas where idCuenta=" + idCuenta
            num = Comm.ExecuteScalar

            Comm.CommandText = "select count(Nombre) from tblBancos where idBanco=" + num.ToString
            cantidad = Comm.ExecuteScalar
            If cantidad = 0 Then
                banco = " "
            Else
                Comm.CommandText = "select Nombre from tblBancos where idBanco=" + num.ToString
                banco = Comm.ExecuteScalar
            End If

        End If

        Return banco
    End Function
    Public Function buscarBanco(ByVal idCuenta As String) As String
        Dim banco As String
        Dim cantidad As Integer
        Comm.CommandText = "select count(Nombre) from tblBancos where idBanco=" + idCuenta.ToString
        cantidad = Comm.ExecuteScalar
        If cantidad = 0 Then
            banco = " "
        Else
            Comm.CommandText = "select Nombre from tblBancos where idBanco=" + idCuenta.ToString
            banco = Comm.ExecuteScalar
        End If
        
        Return banco
    End Function
    Public Function buscarNumeroCuenta(ByVal idCuenta As String) As String
        Dim num As String
        Dim cantidad As Integer
        Comm.CommandText = "select count(Numero) from tblcuentas where idCuenta=" + idCuenta
        cantidad = Comm.ExecuteScalar
        If cantidad = 0 Then
            num = " "
        Else
            Comm.CommandText = "select Numero from tblcuentas where idCuenta=" + idCuenta
            num = Comm.ExecuteScalar
        End If
        Return num
    End Function
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbldepostito where idDeposito=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    
    Public Sub Buscar(ByVal pID As Integer)
        'ID = pID
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbldepostito where idDeposito='" + pID.ToString + "'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Fecha = DReader("Fecha")
            Referencia = DReader("Referencia")
            Banco = DReader("Banco")
            Cantidad = DReader("Cantidad")
            ' cuenta = DReader("nCuenta")
            Banco2 = DReader("Banco2")
            If IsDBNull(DReader("comentario")) Then
                comentario = ""
            Else
                comentario = DReader("comentario")
            End If
            IdPagoProv = DReader("idpagoprov")
        End If
        DReader.Close()
    End Sub
    Public Function filtroFecha(ByVal fInicio As String, ByVal fFinal As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tbldepostito where DATE(Fecha) between'" + fInicio.ToString + "' AND '" + fFinal.ToString() + "' ORDER BY idDeposito DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldepostito")
        Return DS.Tables("tbldepostito").DefaultView
    End Function

    Public Function filtroProv(ByVal tProv As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tbldepostito where concat(Referencia) like '%" + Replace(tProv, "'", "''") + "%' ORDER BY idDeposito DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov").DefaultView
    End Function
    Public Function filtroAmbos(ByVal tProv As String, ByVal fInicio As String, ByVal fFinal As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tbldepostito where DATE(Fecha) between'" + fInicio.ToString + "' AND '" + fFinal.ToString() + "' AND concat(Referencia) like '%" + Replace(tProv, "'", "''") + "%' ORDER BY idDeposito DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov").DefaultView
    End Function
    '**********Hacer nueva tabla
    Public Function FolioPoliza() As String
        Dim Resultado As Integer
        Dim Rep As String
        Dim repetida As Integer

        Resultado = 1
        Rep = Format(Resultado, "00000")
        repetida = ClaveRepetidapoliza(Rep)

        If repetida > 0 Then 'es repetida

            Do While repetida > 0
                Resultado = Integer.Parse(Rep)
                Resultado = Resultado + 1
                Rep = Format(Resultado, "00000")
                repetida = ClaveRepetidapoliza(Rep)

            Loop
            Return Format(Resultado, "00000")
        Else
            Return Format(Resultado, "00000")
        End If
    End Function
    Public Function ClaveRepetidapoliza(ByVal pCodigo As String) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(NumPoliza) from tblpolizadeposito where NumPoliza='" + Replace(pCodigo, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ClaveRepetidapoliza = 0
        Else
            ClaveRepetidapoliza = 1
        End If
    End Function

    Public Function folioPolizaRepetida(ByVal pFolio As String) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(NumPoliza) from tblpolizadeposito where NumPoliza='" + Replace(pFolio, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function
    Public Sub guardarPoliza(ByVal pidPagoProv As Integer, ByVal pNumPoliza As String, ByVal pID As String, ByVal pCuenta As String, ByVal pDescripcion As String, ByVal pCargo As String, ByVal pAbono As String, ByVal pElabora As String, ByVal pAutoriza As String, ByVal pRegistra As String)
        Comm.CommandText = "insert into tblpolizadeposito (idDeposito,NumPoliza,ID,Cuenta,Descripcion,Cargo,Abono,Elabora,Autoriza,Registra) values('" + Replace(pidPagoProv.ToString(), "'", "''") + "','" + Replace(pNumPoliza, "'", "''") + "','" + Replace(pID, "'", "''") + "','" + Replace(pCuenta, "'", "''") + "','" + Replace(pDescripcion, "'", "''") + "','" + Replace(pCargo, "'", "''") + "','" + Replace(pAbono, "'", "''") + "','" + Replace(pElabora, "'", "''") + "','" + Replace(pAutoriza, "'", "''") + "','" + Replace(pRegistra, "'", "''") + "')"
        Comm.ExecuteNonQuery()

    End Sub
    Public Sub EliminarPoliza(ByVal pID As Integer)
        Comm.CommandText = "delete from tblpolizadeposito where idDeposito=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function tienePoliza(ByVal pIdDeposito As String) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(idDeposito) from tblpolizadeposito where idDeposito=" + pIdDeposito.ToString
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function
    Public Function buscarPoliza(ByVal pIdPagoProv As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select ID,Cuenta,Descripcion,Cargo,Abono,NumPoliza from tblpolizadeposito where idDeposito='" + pIdPagoProv + "'ORDER BY ID ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpolizadeposito")
        Return DS.Tables("tblpolizadeposito").DefaultView
    End Function
    Public Sub buscarDatos(ByVal pIdPagoProv As String)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select Elabora, Autoriza, Registra,NumPoliza from tblpolizadeposito where idDeposito='" + pIdPagoProv.ToString() + "'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Elabora = DReader("Elabora")
            Autoriza = DReader("Autoriza")
            Registra = DReader("Registra")
            NumPoliza = DReader("NumPoliza")
        End If
        DReader.Close()
    End Sub
    '*********** Reportes
    Public Function Reporteid(ByVal fechaI As String, ByVal fechaf As String, ByVal nBanco As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idDeposito,Fecha from tbldepostito where Banco2='" + nBanco + "' and DATE(Fecha) between'" + fechaI + "' AND '" + fechaf + "'ORDER BY idDeposito ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldepostito")
        Return DS.Tables("tbldepostito")
    End Function
    Public Function ReportePoliza(ByVal pId As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select NumPoliza,Cuenta,Descripcion,Cargo,Abono from tblpolizadeposito where idDeposito='" + pId + "'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpolizadeposito")
        'DS.WriteXmlSchema("tblpolizapagoprov.xml")
        Return DS.Tables("tblpolizadeposito")
    End Function
    Public Function DepositoRep(ByVal fechaI As String, ByVal fechaf As String, ByVal nBanco As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select *  from tbldepostito where Banco2='" + nBanco + "' and DATE(Fecha) between'" + fechaI + "' AND '" + fechaf + "'ORDER BY idDeposito ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblDepositoRep")
        'DS.WriteXmlSchema("tblDepositoRep.xml")
        Return DS.Tables("tblDepositoRep")
    End Function
    Public Function BuscarCuenta(ByVal pID As Integer) As String
        Dim pCuenta As String
        Dim pBanco As String
        Dim pIDBanco As Integer
        Comm.CommandText = "select Numero from tblcuentas where idCuenta=" + pID.ToString
        pCuenta = Comm.ExecuteScalar
        Comm.CommandText = "select Banco from tblcuentas where idCuenta=" + pID.ToString
        pIDBanco = Comm.ExecuteScalar
        Comm.CommandText = "select Nombre from tblbancos where idbanco=" + pIDBanco.ToString
        pBanco = Comm.ExecuteScalar
        Return pCuenta + " " + pBanco
    End Function

    Public Sub LigarDeposito(pidVenta As String, pIddeposito As Integer)
        Comm.CommandText = "insert into tblventasdepositos(idventa,iddeposito) values(" + pidVenta + "," + pIddeposito.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub DesligarDeposito(pIdDEposito As Integer)
        Comm.CommandText = "delete from tblventasdepositos where iddeposito=" + pIdDEposito.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function TieneDepositos(pidVenta As Integer) As Boolean
        Comm.CommandText = "select count(idventa) from tblventasdepositos where idventa=" + pidVenta.ToString
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
