Imports MySql.Data.MySqlClient
Public Class dbPagosProveedores
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Tipoo As String
    Public Folioo As String
    Public Proveedor As String
    Public Fecha As String
    Public FechaCobro As String
    Public Referencia As String
    Public Cantidad As Double
    Public IVA As Double
    Public Leyenda As Boolean
    Public EsCheque As Boolean
    Public Estado As String
    Public ID As String
    Public Elabora As String
    Public Autoriza As String
    Public Registra As String
    Public NumPoliza As String
    Public nCuenta As String
    Public Banco As Integer
    Public IdProveedor As Integer
    Public CuentaDestino As String
    Public IdBancoD As Integer
    Public BancoOrigenEx As String
    Public BancoDestinoEx As String
    Public IdMoneda As Integer
    Public TipodeCambio As Double
    Public IvaRet As Double
    Public IEPS As Double
    Public ISR As Double
    Public EsTraspaso As Byte
    Public IdCuentaDest As Integer
    'Public IdProveedor As Integer
    Public Solicitud, CantidadGravadaISR, CantidadGravadaIVA, CantidadGravadaIEPS, CantidadRetenidaISR, CantidadRetenidaIVA, CantidadRetenidaIEPS As String

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
       
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        
    End Sub

    Public Function ClaveRepetida(ByVal pCodigo As String) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(Folio) from tblpagoprov where Folio='" + Replace(pCodigo, "'", "''") + "'And EsCheque='1'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ClaveRepetida = 0
        Else
            ClaveRepetida = 1
        End If
    End Function

    
    Public Function Folio() As String
        Dim Resultado As Integer
        Dim Rep As String
        Dim repetida As Integer

        Resultado = 1
        Rep = Format(Resultado, "0000")
        repetida = ClaveRepetida(Rep)

        If repetida > 0 Then 'es repetida

            Do While repetida > 0
                Resultado = Integer.Parse(Rep)
                Resultado = Resultado + 1
                Rep = Format(Resultado, "0000")
                repetida = ClaveRepetida(Rep)

            Loop
            Return Format(Resultado, "0000")
        Else
            Return Format(Resultado, "0000")
        End If
    End Function

    Public Function Guardar(ByVal pTipo As String, ByVal pFolio As String, ByVal pProveedor As String, ByVal pFecha As String, ByVal pFechaCobro As String, ByVal pReferencia As String, ByVal pCantidad As String, ByVal pIVA As String, ByVal pLeyenda As Integer, ByVal pEsCheque As Integer, ByVal pEstado As String, ByVal pBanco As String, pCuentaDestino As String, pIdBancoD As Integer, pBancoDestinoEx As String, pIdMoneda As Integer, pTipodeCambio As Double, pNoCheque As String, pIvaret As Double, pIeps As Double, pISR As Double, pEstraspaso As Byte, pIdCuentaDest As Integer) As Integer
        'Public CuentaDestino As String
        'Public IdCuentaD As Integer
        'Public BancoOrigenEx As String
        'Public BancoDestinoEx As String
        'Public IdMoneda As Integer
        'Public TipodeCambio As Double
        Dim id As Integer
        Comm.CommandText = "insert into tblpagoprov (Tipo,Folio, Fecha, FechaCobro,Referencia, Cantidad, IVA, Leyenda, EsCheque, Estado, Banco,idproveedor,cuentadestino,idbancod,bancoorigenex,bancodestinoex,idmoneda,tipodecambio,ivaret,ieps,isr,estraspaso,idcuentadest) values('" + Replace(pTipo, "'", "''") + "','" + Replace(pFolio, "'", "''") + "','" + Replace(pFecha, "'", "''") + "','" + Replace(pFechaCobro, "'", "''") + "','" + Replace(pReferencia, "'", "''") + "'," + pCantidad + "," + pIVA + ",'" + Replace(pLeyenda, "'", "''") + "','" + Replace(pEsCheque, "'", "''") + "','" + Replace(pEstado, "'", "''") + "'," + pBanco + "," + pProveedor + ",'" + Replace(pCuentaDestino, "'", "''") + "'," + pIdBancoD.ToString + ",'" + pNoCheque.Replace("'", "''") + "','" + pBancoDestinoEx.Replace("'", "''") + "'," + pIdMoneda.ToString + "," + pTipodeCambio.ToString + "," + pIvaret.ToString + "," + pIeps.ToString + "," + pISR.ToString + "," + pEstraspaso.ToString + "," + pIdCuentaDest.ToString + ")"
        Comm.ExecuteNonQuery()
        'Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        'Comm.CommandText = "select * from tblpagoprov where Tipo='" + Replace(pTipo, "'", "''") + "' and Folio='" + Replace(pFolio, "'", "''") + "' and Proveedor='" + Replace(pProveedor, "'", "''") + "' and Fecha='" + Replace(pFecha, "'", "''") + "' and FechaCobro='" + Replace(pFechaCobro, "'", "''") + "' and Referencia='" + Replace(pReferencia, "'", "''") + "' and Cantidad='" + Replace(pCantidad, "'", "''") + "' and IVA='" + Replace(pIVA, "'", "''") + "' and Leyenda='" + Replace(pLeyenda, "'", "''") + "' and EsCheque='" + Replace(pEsCheque, "'", "''") + "'"
        'DReader = Comm.ExecuteReader
        'If DReader.Read() Then
        '    id = DReader("idPagoProv")
        'End If
        'DReader.Close()
        Comm.CommandText = "select max(idpagoprov) from tblpagoprov "
        id = Comm.ExecuteScalar
        Return id
    End Function
    Public Sub LigarAComprasPagos(ByVal pId As Integer, ByVal pWherestr As String)
        pWherestr = pWherestr.Substring(3, pWherestr.Length - 3)
        Comm.CommandText = "update tblcompraspagos set idpagoprov=" + pId.ToString + " where " + pWherestr
        Comm.ExecuteNonQuery()
    End Sub
    Public Function LigadoABancos(ByVal pIdPagoprov As Integer) As Boolean
        Comm.CommandText = "select ifnull((select count(idpagoprov) from tblcompraspagos where idpagoprov=" + pIdPagoprov.ToString + "),0)"
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Sub Modificar(ByVal pId As Integer, ByVal pTipo As String, ByVal pFolio As String, ByVal pProveedor As String, ByVal pFecha As String, ByVal pFechaCobro As String, ByVal pReferencia As String, ByVal pCantidad As String, ByVal pIVA As String, ByVal pLeyenda As Integer, ByVal pEsCheque As Integer, ByVal pEstado As String, ByVal pBanco As String, pCuentaDestino As String, pIdBancoD As Integer, pBancoDestinoEx As String, pIdMoneda As Integer, pTipodeCambio As Double, pNoCheque As String, pIvaret As Double, pIeps As Double, pIsr As Double, pIdCuentaDest As Integer, pEstraspado As Byte)
        'Modificar
        Comm.CommandText = "update tblpagoprov set Tipo='" + Replace(pTipo, "'", "''") + "', Folio='" + Replace(pFolio, "'", "''") + "',Fecha='" + Replace(pFecha, "'", "''") + "',FechaCobro='" + Replace(pFechaCobro, "'", "''") + "',Referencia='" + Replace(pReferencia, "'", "''") + "',Cantidad=" + pCantidad + ",IVA=" + pIVA + ",Leyenda='" + Replace(pLeyenda, "'", "''") + "',EsCheque='" + Replace(pEsCheque, "'", "''") + "',Banco=" + pBanco + ",cuentadestino='" + pCuentaDestino.Replace("'", "''") + "',idbancod=" + pIdBancoD.ToString + ",bancodestinoex='" + pBancoDestinoEx.Replace("'", "''") + "',idmoneda=" + pIdMoneda.ToString + ",tipodecambio=" + pTipodeCambio.ToString + ",bancoorigenex='" + pNoCheque.Replace("'", "''") + "',ivaret=" + pIvaret.ToString + ",ieps=" + pIeps.ToString + ",isr=" + pIsr.ToString + "idcuentadest=" + pIdCuentaDest.ToString + " where idPagoProv=" + pId.ToString()
        Comm.ExecuteNonQuery()

    End Sub

    Public Function ClaveRepetidaEfectivo(ByVal pCodigo As String) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(Folio) from tblpagoprov where Folio='" + Replace(pCodigo, "'", "''") + "'And Tipo='EFECTIVO'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ClaveRepetidaEfectivo = 0
        Else
            ClaveRepetidaEfectivo = 1
        End If
    End Function


    Public Function FolioEfectivo() As String
        Dim Resultado As Integer
        Dim Rep As String
        Dim repetida As Integer

        Resultado = 1
        Rep = Format(Resultado, "0000")
        repetida = ClaveRepetidaEfectivo(Rep)

        If repetida > 0 Then 'es repetida

            Do While repetida > 0
                Resultado = Integer.Parse(Rep)
                Resultado = Resultado + 1
                Rep = Format(Resultado, "0000")
                repetida = ClaveRepetidaEfectivo(Rep)

            Loop
            Return Format(Resultado, "0000")
        Else
            Return Format(Resultado, "0000")
        End If
    End Function

    Public Function filtroTodos(pFecha1 As String, pFecha2 As String, pReferencia As String) As DataView
        Dim DS As New DataSet

        'DataGridView1.Columns(0).HeaderText = "id"
        'DataGridView1.Columns(1).HeaderText = "Folio"
        'DataGridView1.Columns(2).HeaderText = "Proveedor"
        'DataGridView1.Columns(3).HeaderText = "Fecha"
        'DataGridView1.Columns(4).HeaderText = "Fecha Cobro"
        'DataGridView1.Columns(5).HeaderText = "Referencia"
        'DataGridView1.Columns(6).HeaderText = "Cantidad"
        'DataGridView1.Columns(7).HeaderText = "Tipo"
        'DataGridView1.Columns(8).HeaderText = "IVA"
        'DataGridView1.Columns(9).HeaderText = "Leyenda"
        'DataGridView1.Columns(10).HeaderText = "EsCheque"
        'DataGridView1.Columns(11).HeaderText = "Estado"
        'DataGridView1.Columns(12).HeaderText = "N. Cuenta"
        'DataGridView1.Columns(13).HeaderText = "Banco"

        Comm.CommandText = "select idPagoProv,Folio,tblproveedores.nombre, Fecha, FechaCobro,Referencia, Cantidad,tblcuentas.numero,(select nombre from tblbancoscatalogo where idbanco=tblcuentas.banco) from tblpagoprov inner join tblcuentas on tblpagoprov.banco=tblcuentas.idcuenta inner join tblproveedores on tblproveedores.idproveedor=tblpagoprov.idproveedor"
        Comm.CommandText += " where fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        Comm.CommandText += " and tblproveedores.nombre like '%" + Replace(pReferencia.Trim, "'", "''") + "%'"
        Comm.CommandText += "order by fecha desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov").DefaultView
    End Function
    Public Function filtroFecha(ByVal fInicio As String, ByVal fFinal As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idPagoProv,Folio,Proveedor, Fecha, FechaCobro,Referencia, Cantidad,Tipo, IVA, Leyenda, EsCheque, Estado,Banco from tblpagoprov where DATE(Fecha) between'" + fInicio.ToString + "' AND '" + fFinal.ToString() + "' ORDER BY idPagoProv DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov").DefaultView
    End Function
    Public Function filtroProv(ByVal tProv As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idPagoProv,Folio,Proveedor, Fecha, FechaCobro,Referencia, Cantidad,Tipo, IVA, Leyenda, EsCheque, Estado,Banco from tblpagoprov where concat(Proveedor) like '%" + Replace(tProv, "'", "''") + "%' ORDER BY idPagoProv DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov").DefaultView
    End Function
    Public Function filtroAmbos(ByVal tProv As String, ByVal fInicio As String, ByVal fFinal As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idPagoProv,Folio,Proveedor, Fecha, FechaCobro,Referencia, Cantidad,Tipo, IVA, Leyenda, EsCheque, Estado,Banco from tblpagoprov where DATE(Fecha) between'" + fInicio.ToString + "' AND '" + fFinal.ToString() + "' AND concat(Proveedor) like '%" + Replace(tProv, "'", "''") + "%' ORDER BY idPagoProv DESC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpagoprov")
        Return DS.Tables("tblpagoprov").DefaultView
    End Function
    Public Sub Eliminar(ByVal pId As Integer)
        'Modificar

        Comm.CommandText = "delete from tblpagoprov where idPagoProv=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Buscar(ByVal pID As Integer)
        ID = pID

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblpagoprov where idPagoProv='" + ID.ToString + "'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Tipoo = DReader("Tipo")
            Folioo = DReader("Folio")
            'Proveedor = DReader("Proveedor")
            Fecha = DReader("Fecha")
            FechaCobro = DReader("FechaCobro")
            Referencia = DReader("Referencia")
            Cantidad = DReader("Cantidad")
            IVA = DReader("IVA")
            Leyenda = DReader("Leyenda")
            Estado = DReader("Estado")
            'nCuenta = DReader("nCuenta")
            Banco = DReader("Banco")
            IdProveedor = DReader("idproveedor")
            CuentaDestino = DReader("cuentadestino")
            IdBancoD = DReader("idbancod")
            BancoDestinoEx = DReader("bancodestinoex")
            BancoOrigenEx = DReader("bancoorigenex")
            IdMoneda = DReader("idmoneda")
            TipodeCambio = DReader("tipodecambio")
            IvaRet = DReader("ivaret")
            IEPS = DReader("ieps")
            ISR = DReader("isr")
            EsTraspaso = DReader("estraspaso")
            IdCuentaDest = DReader("idcuentadest")
        End If
        DReader.Close()
    End Sub

    Public Function Existe(ByVal pNombre As String) As Integer
        Dim Resultado As Integer
        Comm.CommandText = "select * from tblproveedores where nombre='" + pNombre.ToString() + "'"
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function

    Public Sub buscarEncargado()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select Elabora, Autoriza, Registra from tblelabora"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Elabora = DReader("Elabora")
            Autoriza = DReader("Autoriza")
            Registra = DReader("Registra")
        End If
        DReader.Close()
    End Sub
    Public Sub guardarEncargado(ByVal pElabora As String, ByVal pAutoriza As String, ByVal pRegistra As String)
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(idElabora) from tblelabora"
        Resultado = Comm.ExecuteScalar

        If Resultado = 0 Then
            Comm.CommandText = "insert into tblelabora (Elabora, Autoriza, Registra) values('" + Replace(pElabora, "'", "''") + "','" + Replace(pAutoriza, "'", "''") + "','" + Replace(pRegistra, "'", "''") + "')"
            Comm.ExecuteNonQuery()
        Else
            Comm.CommandText = "update tblelabora set Elabora='" + Replace(pElabora, "'", "''") + "', Autoriza='" + Replace(pAutoriza, "'", "''") + "',Registra='" + Replace(pRegistra, "'", "''") + "'"
            Comm.ExecuteNonQuery()
        End If


        
    End Sub
    Public Sub guardarPoliza(ByVal pidPagoProv As Integer, ByVal pNumPoliza As String, ByVal pID As String, ByVal pCuenta As String, ByVal pDescripcion As String, ByVal pCargo As String, ByVal pAbono As String, ByVal pElabora As String, ByVal pAutoriza As String, ByVal pRegistra As String, ByVal pSolicitud As String, ByVal pCantidadGravada1 As String, ByVal pCantidadGravada2 As String, ByVal pCantidadGravada3 As String, ByVal pCantidadRetenida1 As String, ByVal pCantidadRetenida2 As String, ByVal pCantidadRetenida3 As String)
        Comm.CommandText = "insert into tblpolizapagoprov (idPagoProv,NumPoliza,ID,Cuenta,Descripcion,Cargo,Abono,Elabora,Autoriza,Registra,Solicitud,CantidadGravadaISR,CantidadGravadaIVA,CantidadGravadaIEPS,CantidadRetenidaISR,CantidadRetenidaIVA,CantidadRetenidaIEPS) values('" + Replace(pidPagoProv.ToString(), "'", "''") + "','" + Replace(pNumPoliza, "'", "''") + "','" + Replace(pID, "'", "''") + "','" + Replace(pCuenta, "'", "''") + "','" + Replace(pDescripcion, "'", "''") + "','" + Replace(pCargo, "'", "''") + "','" + Replace(pAbono, "'", "''") + "','" + Replace(pElabora, "'", "''") + "','" + Replace(pAutoriza, "'", "''") + "','" + Replace(pRegistra, "'", "''") + "','" + Replace(pSolicitud, "'", "''") + "','" + Replace(pCantidadGravada1, "'", "''") + "','" + Replace(pCantidadGravada2, "'", "''") + "','" + Replace(pCantidadGravada3, "'", "''") + "','" + Replace(pCantidadRetenida1, "'", "''") + "','" + Replace(pCantidadRetenida2, "'", "''") + "','" + Replace(pCantidadRetenida3, "'", "''") + "')"
        Comm.ExecuteNonQuery()

    End Sub
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
        Comm.CommandText = "select count(NumPoliza) from tblpolizapagoprov where NumPoliza='" + Replace(pCodigo, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ClaveRepetidapoliza = 0
        Else
            ClaveRepetidapoliza = 1
        End If
    End Function


    Public Function tienePoliza(ByVal pIdPagoProv As String) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(idPagoProv) from tblpolizapagoprov where idPagoProv='" + Replace(pIdPagoProv, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function

    Public Function buscarPoliza(ByVal pIdPagoProv As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select ID,Cuenta,Descripcion,Cargo,Abono,NumPoliza from tblpolizapagoprov where idPagoProv='" + pIdPagoProv + "'ORDER BY ID ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpolizapagoprov")
        Return DS.Tables("tblpolizapagoprov").DefaultView
    End Function
    Public Sub buscarDatos(ByVal pIdPagoProv As String)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select Elabora, Autoriza, Registra, Solicitud,CantidadGravadaISR,CantidadGravadaIVA,CantidadGravadaIEPS,CantidadRetenidaISR,CantidadRetenidaIVA,CantidadRetenidaIEPS,NumPoliza from tblpolizapagoprov where idPagoProv='" + pIdPagoProv.ToString() + "'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Elabora = DReader("Elabora")
            Autoriza = DReader("Autoriza")
            Registra = DReader("Registra")
            Solicitud = DReader("Solicitud")
            CantidadGravadaISR = DReader("CantidadGravadaISR")
            CantidadGravadaIVA = DReader("CantidadGravadaIVA")
            CantidadGravadaIEPS = DReader("CantidadGravadaIEPS")
            CantidadRetenidaISR = DReader("CantidadRetenidaISR")
            CantidadRetenidaIVA = DReader("CantidadRetenidaIVA")
            CantidadRetenidaIEPS = DReader("CantidadRetenidaIEPS")
            NumPoliza = DReader("NumPoliza")
        End If
        DReader.Close()
    End Sub

    Public Function folioPolizaRepetida(ByVal pFolio As String) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(NumPoliza) from tblpolizapagoprov where NumPoliza='" + Replace(pFolio, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function
    Public Function folioSolicitudRepetida(ByVal pFolio As String, ByVal pSolicitud As String) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(NumPoliza) from tblpolizapagoprov where NumPoliza='" + Replace(pFolio, "'", "''") + "' AND Solicitud='" + Replace(pSolicitud, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function

    Public Sub EliminarPoliza(ByVal pID As Integer)
        Comm.CommandText = "delete from tblpolizapagoprov where idPagoProv=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarTodaPoliza(ByVal pID As Integer)
        Comm.CommandText = "delete from tblpolizapagoprov where Folio=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function buscarCuenta() As DataTable
        Dim DS As New DataSet
        ' Comm.CommandText = "select Numero,Banco from tblcuentas ORDER BY IdCuenta ASC"
        Comm.CommandText = "SELECT p1.idCuenta,p1.Numero,p1.Banco,p2.Nombre,concat(p1.numero,' ',p2.Nombre) nombre2 FROM tblCuentas AS p1 inner join tblBancos AS p2 on p1.Banco = p2.idBanco ORDER BY IdCuenta ASC;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcuentas")
        Return DS.Tables("tblcuentas")
    End Function
    Public Function buscarBancos() As DataTable
        Dim DS As New DataSet
        ' Comm.CommandText = "select Numero,Banco from tblcuentas ORDER BY IdCuenta ASC"
        Comm.CommandText = "SELECT idBanco,Codigo,Nombre FROM tblBancos ORDER BY IdBanco ASC;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblBancos")
        Return DS.Tables("tblBancos")
    End Function
    Public Function ConsultaRFC(ByVal pNombre As String) As String
        Dim rfc As String
        Comm.CommandText = "select rfc from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
        rfc = Comm.ExecuteScalar
        Return rfc
    End Function
    'Public Function nombre() As String 'consulta nombre de la empresa
    '    Dim nombre1 As String
    '    Comm.CommandText = "select nombre from tblsucursales"
    '    nombre1 = Comm.ExecuteScalar
    '    Return nombre1
    'End Function
    'Public Function RFC() As String 'consulta rfc de la empresa
    '    Dim rfc1 As String
    '    Comm.CommandText = "select rfc from tblsucursales"
    '    rfc1 = Comm.ExecuteScalar
    '    Return rfc1
    'End Function
    Public Function Consultadireccion(ByVal pNombre As String) As String
        Dim calle As String
        Dim num As String
        Dim colonia As String
        Dim ciudad As String
        'Dim municipio As String
        Dim estado As String
        Dim pais As String
        Dim total As String

        Comm.CommandText = "select direccion from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
        calle = Comm.ExecuteScalar
        Comm.CommandText = "select noexterior from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
        num = Comm.ExecuteScalar
        Comm.CommandText = "select colonia from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
        colonia = Comm.ExecuteScalar
        Comm.CommandText = "select ciudad from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
        ciudad = Comm.ExecuteScalar
        Comm.CommandText = "select estado from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
        estado = Comm.ExecuteScalar
        Comm.CommandText = "select pais from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
        pais = Comm.ExecuteScalar
        total = calle + " #" + num + ", " + colonia + ", " + ciudad + ", " + estado + ", " + pais
        Return total
    End Function
    'Public Function Calles(ByVal pNombre As String) As String
    '    Dim calle As String
    '    Dim num As String
    '    ' Dim colonia As String
    '    ' Dim ciudad As String
    '    'Dim municipio As String
    '    ' Dim estado As String
    '    ' Dim pais As String
    '    Dim total As String

    '    Comm.CommandText = "select direccion from tblsucursales where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    calle = Comm.ExecuteScalar
    '    Comm.CommandText = "select noexterior from tblsucursales where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    num = Comm.ExecuteScalar
    '    ' Comm.CommandText = "select colonia from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    ' colonia = Comm.ExecuteScalar
    '    ' Comm.CommandText = "select ciudad from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    ' ciudad = Comm.ExecuteScalar
    '    ' Comm.CommandText = "select estado from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    ' estado = Comm.ExecuteScalar
    '    ' Comm.CommandText = "select pais from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    ' pais = Comm.ExecuteScalar
    '    total = calle + " #" + num
    '    Return total
    'End Function
    'Public Function direccion2(ByVal pNombre As String) As String
    '    ' Dim calle As String
    '    'Dim num As String
    '    ' Dim colonia As String
    '    Dim ciudad As String
    '    '  Dim municipio As String
    '    Dim estado As String
    '    Dim pais As String
    '    Dim total As String

    '    '  Comm.CommandText = "select direccion from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    '  calle = Comm.ExecuteScalar
    '    '  Comm.CommandText = "select noexterior from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    '  num = Comm.ExecuteScalar
    '    '  Comm.CommandText = "select colonia from tblproveedores where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    ' colonia = Comm.ExecuteScalar
    '    Comm.CommandText = "select ciudad from tblsucursales where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    ciudad = Comm.ExecuteScalar
    '    Comm.CommandText = "select estado from tblsucursales where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    estado = Comm.ExecuteScalar
    '    Comm.CommandText = "select pais from tblsucursales where nombre='" + Replace(pNombre, "'", "''") + "'"
    '    pais = Comm.ExecuteScalar
    '    total = ciudad + ", " + estado + ", " + pais
    '    Return total
    'End Function

    '*********** Reportes
    Public Function Reporteid(ByVal fechaI As String, ByVal fechaf As String, ByVal nBanco As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idPagoProv,Fecha from tblpagoprov where Estado='Activo' and Banco='" + nBanco + "' and DATE(Fecha) between'" + fechaI + "' AND '" + fechaf + "'ORDER BY idPagoProv ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpolizapagoprov")
        DS.WriteXmlSchema("tblpolizapagoprov.xml")
        Return DS.Tables("tblpolizapagoprov")
    End Function
    Public Function ReportePoliza(ByVal pId As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select NumPoliza,Cuenta,Descripcion,Cargo,Abono from tblpolizapagoprov where idPagoProv='" + pId + "'ORDER BY NumPoliza ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpolizapagoprov")
        'DS.WriteXmlSchema("tblpolizapagoprov.xml")
        Return DS.Tables("tblpolizapagoprov")
    End Function
    Public Function ReporteDIOT(ByVal fechaI As String, ByVal fechaF As String, ByVal banco As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idPagoProv,Proveedor,IVA,Cantidad,Fecha,Tipo,Folio,Referencia from tblpagoprov where Banco='" + banco + "' and DATE(Fecha) between'" + fechaI + "' AND '" + fechaF + "'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpolizapagoprov")
        'DS.WriteXmlSchema("tblpolizapagoprov.xml")
        Return DS.Tables("tblpolizapagoprov")
    End Function
    Public Function buscarRFCPorveedor(ByVal nombre As String) As String
        Dim rfc As String
        Comm.CommandText = "select rfc from tblproveedores where nombre='" + nombre + "'"
        rfc = Comm.ExecuteScalar
        Return rfc
    End Function
    Public Function buscarNumPoliza(ByVal id As String) As String
        Dim poliza As String
        Comm.CommandText = "select numPoliza from tblpolizapagoprov where idPagoProv='" + id + "'"
        poliza = Comm.ExecuteScalar
        Return poliza
    End Function

    Public Function pagoProvedoorRep(ByVal fechaI As String, ByVal fechaf As String, ByVal nBanco As String) As DataTable

        Dim DS As New DataSet
        Comm.CommandText = "select idpagoprov,tblpagoprov.Tipo,Folio,Fecha,FechaCobro,Referencia,cantidad,iva,Leyenda,EsCheque,tblpagoprov.Estado,banco,tblproveedores.nombre as proveedor from tblpagoprov inner join tblproveedores on tblpagoprov.idproveedor=tblproveedores.idproveedor where tblpagoprov.Estado='Activo' and Banco='" + nBanco + "' and DATE(Fechacobro) between'" + fechaI + "' AND '" + fechaf + "'ORDER BY idPagoProv ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpolizapagoprov")
        'DS.WriteXmlSchema("tblPagoProv.xml")
        Return DS.Tables("tblpolizapagoprov")
    End Function

    Public Sub LigarRetiro(pidCompra As String, pIdPagoProv As Integer)
        Comm.CommandText = "insert into tblcomprasretiros(idcompra,idpagoprov) values(" + pidCompra + "," + pIdPagoProv.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub DesligarRetiros(pIdPagoProv As Integer)
        Comm.CommandText = "delete from tblcomprasretiros where idpagoprov=" + pIdPagoProv.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function TieneRetiros(pidCompra As Integer) As Boolean
        Comm.CommandText = "select count(idcompra) from tblcomprasretiros where idcompra=" + pidCompra.ToString
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
