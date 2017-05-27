
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Public Class dbContabilidadClasificacion
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public p As New dbContabilidadPolizas(MySqlcon)
    Public nombre As String
    Public cuenta As String
    Public IDcuenta As Integer
    'Datos cuenta
    Public N1 As String
    Public N2 As String
    Public N3 As String
    Public N4 As String
    Public N5 As String
    Public N1Ant As String
    Public N2Ant As String
    Public N3Ant As String
    Public N4Ant As String
    Public N5Ant As String
    Dim intN1 As String = ""
    Dim intN2 As String = ""
    Dim intN3 As String = ""
    Dim intN4 As String = ""
    Dim intn5 As String = ""
    Public Descripcion As String = ""
    Public Descripcion2 As String = ""
    Public Descripcion3 As String = ""
    Public Descripcion4 As String = ""
    Public Descripcion5 As String = ""
    Public Nivel As Integer = -1
    Public Tipo As Integer = -1
    Public Naturaleza As Integer = -1
    Public fecha As String = ""
    Public cmb1 As Integer
    Public cmb2 As Integer
    Public idContable As Integer
    Public ErroresSincro As String = ""
    Public DIOT As Integer
    Public cuentaCompleta As String
    Public Descontinuada As String
    Public IdCuentaMayor As Integer
    Public IdCuentaAnt As Integer
    Public YaTieneMovimientos As Boolean
    Public NombreAnt As String
    Public CuentaAnt As String

    Public idNivel1 As Integer
    Public idNivel2 As Integer
    Public idNivel3 As Integer
    Public idNivel4 As Integer
    Public idNivel5 As Integer
    Public Depreciacion As Byte
    'Fin datos cuenta

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
        nombre = ""
        cuenta = ""
        N1 = ""
        N2 = ""
        N3 = ""
        N4 = ""
        N5 = ""
        p.llenaDatosConfig()
    End Sub
    Public Sub separador(ByVal pCuenta As String, ByVal pDescripcion As String, ByVal pTipo As String, ByVal pNatur As String, ByVal pAgrupador As String, ByVal pFecha As String)
        Dim tipo As Integer
        Dim Naturaleza As Integer


        nombre = ""
        cuenta = ""
        N1 = ""
        N2 = ""
        N3 = ""
        N4 = ""
        N5 = ""
        Nivel = 1
        idContable = 0
        Dim errores As Boolean = False
        Dim aux As String = ""
        Dim bandera As Boolean = False
        fecha = Date.Now.Year.ToString + "/" + Date.Now.Month.ToString("00") + "/" + Date.Now.Day.ToString("00")
        'agregar Cuentas
        If pTipo = "A" Then
            tipo = 0
        Else
            If pTipo = "P" Then
                tipo = 1
            Else
                If pTipo = "C" Then
                    tipo = 2
                Else
                    If pTipo = "I" Then
                        tipo = 3
                    Else
                        If pTipo = "CO" Then
                            tipo = 4
                        Else
                            If pTipo = "E" Then
                                tipo = 5
                            Else
                                If pTipo = "O" Then
                                    tipo = 6
                                Else
                                    tipo = 0
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

        If pNatur = "D" Then
            Naturaleza = 0
        Else
            Naturaleza = 1
        End If
        Nivel = 0
        For i As Integer = 0 To pCuenta.Length() - 1
            If pCuenta.Chars(i) <> " " Then
                aux += pCuenta.Chars(i)
            Else
                Nivel += 1
                If Nivel > 5 Then Nivel = 5
                If Nivel = 5 Then
                    N5 = aux
                    'Nivel = 5
                End If
                If Nivel = 4 Then
                    N4 = aux
                    'Nivel = 5
                End If
                If Nivel = 3 Then
                    N3 = aux
                    'Nivel = 4
                End If
                If Nivel = 2 Then
                    N2 = aux
                    'Nivel = 3
                End If
                If Nivel = 1 Then
                    N1 = aux
                    'Nivel = 2
                End If
                aux = ""
            End If

        Next
        If aux <> "" Then
            Nivel += 1
            If Nivel > 5 Then Nivel = 5
            If Nivel = 5 Then
                N5 = aux
            End If
            If Nivel = 4 Then
                N4 = aux
            End If
            If Nivel = 3 Then
                N3 = aux
            End If
            If Nivel = 2 Then
                N2 = aux
            End If
            If Nivel = 1 Then
                N1 = aux
            End If
        End If
        If N1 <> "" Then
            N1 = quitarCeros(N1)
        End If
        If N2 <> "" Then
            N2 = quitarCeros(N2)
        End If
        If N3 <> "" Then
            N3 = quitarCeros(N3)
        End If
        If N4 <> "" Then
            N4 = quitarCeros(N4)
        End If
        If N5 <> "" Then
            N5 = quitarCeros(N5)
        End If


        'If 
        existe(N1, pDescripcion, Nivel, N1, N2, N3, N4, N5, True)
        'asegurarse que los niveles anteriores (si aplica) existan
        If Nivel >= 2 Then
            If existe(N1, Descripcion, 1, N1, "", "", "", "") = False Then
                errores = True
            End If
        End If
        If Nivel >= 3 Then
            If existe(N1, Descripcion, 2, N1, N2, "", "", "") = False Then
                errores = True
            End If
        End If

        If Nivel >= 4 Then
            If existe(N1, Descripcion, 3, N1, N2, N3, "", "") = False Then
                errores = True
            End If
        End If
        If Nivel >= 5 Then
            If existe(N1, Descripcion, 4, N1, N2, N3, N4, "") = False Then
                errores = True
            End If
        End If
        If errores = False Then
            'LA CUENTA NO EXISTE
            Comm.CommandText = "select ifnull((select id from tblagrupadorcuentas where codigo='" + pAgrupador + "'),0)"
            idContable = Comm.ExecuteScalar
            'If idContable = 0 Then
            '    Comm.CommandText = "select min(id) from tblagrupadorcuentas "
            '    idContable = Comm.ExecuteScalar
            'End If

            Guardar(N1, N2, N3, N4, N5, pDescripcion, Nivel, tipo, Naturaleza, pFecha, 0, 0, idContable, 0, 0, 0, 0, 0, 0)
            'Else
            '    ErroresSincro += pCuenta + vbCrLf
            'End If

        End If



    End Sub
    Public Sub AjustaIdsCuentas()
        Comm.CommandText = "DROP TABLE IF EXISTS `tblccontablestemp`;" + vbCrLf +
"CREATE TABLE  `tblccontablestemp` (" + vbCrLf +
  "`idCContable` int(10) unsigned DEFAULT NULL," + vbCrLf +
  "`Cuenta` varchar(5) DEFAULT NULL," + vbCrLf +
  "`N2` varchar(5) DEFAULT NULL," + vbCrLf +
  "`N3` varchar(5) DEFAULT NULL," + vbCrLf +
  "`N4` varchar(5) DEFAULT NULL," + vbCrLf +
  "`Descripcion` varchar(65) DEFAULT NULL," + vbCrLf +
  "`Nivel` int(10) unsigned DEFAULT NULL," + vbCrLf +
  "`CuentaComp` varchar(35) DEFAULT NULL," + vbCrLf +
  "`Tipo` int(10) unsigned DEFAULT NULL," + vbCrLf +
  "`Naturaleza` int(10) unsigned DEFAULT NULL," + vbCrLf +
  "`N5` varchar(5) DEFAULT NULL," + vbCrLf +
  "`fecha` varchar(11) NOT NULL," + vbCrLf +
  "`cmb1` int(10) unsigned NOT NULL," + vbCrLf +
  "`cmb2` int(10) NOT NULL," + vbCrLf +
  "`idContable` int(10) unsigned NOT NULL," + vbCrLf +
  "`DIOT` int(10) unsigned NOT NULL," + vbCrLf +
  "`idUsuarioAlta` int(10) unsigned NOT NULL DEFAULT '1000'," + vbCrLf +
  "`fechaAlta` varchar(45) NOT NULL," + vbCrLf +
  "`horaAlta` varchar(45) NOT NULL," + vbCrLf +
  "`idUsuarioCambio` int(10) unsigned NOT NULL DEFAULT '1000'," + vbCrLf +
  "`fechaCambio` varchar(45) NOT NULL," + vbCrLf +
  "`horaCambio` varchar(45) NOT NULL," + vbCrLf +
  "`descontinuada` varchar(200) NOT NULL," + vbCrLf +
  "`idcuentam` int(10) unsigned NOT NULL," + vbCrLf +
  "`idcuentan2` int(10) unsigned NOT NULL," + vbCrLf +
  "`idcuentan3` int(10) unsigned NOT NULL," + vbCrLf +
  "`idcuentan4` int(10) unsigned NOT NULL," + vbCrLf +
  "`idcuentan5` int(10) unsigned NOT NULL" + vbCrLf +
") ENGINE=InnoDB DEFAULT CHARSET=utf8;" + vbCrLf +
"insert into tblccontablestemp select * from tblccontables;" + vbCrLf +
"update tblccontables a set idcuentam=(select i.idccontable from tblccontablestemp i where i.cuenta=a.cuenta and i.n2='' and i.n3='' and i.n4='' and i.n5='' limit 1);" + vbCrLf +
"update tblccontables a set idcuentan2=(select i.idccontable from tblccontablestemp i where i.cuenta=a.cuenta and i.n2=a.n2 and i.n3='' and i.n4='' and i.n5='' limit 1);" + vbCrLf +
"update tblccontables a set idcuentan3=(select i.idccontable from tblccontablestemp i where i.cuenta=a.cuenta and i.n2=a.n2 and i.n3=a.n3 and i.n4='' and i.n5='' limit 1);" + vbCrLf +
"update tblccontables a set idcuentan4=(select i.idccontable from tblccontablestemp i where i.cuenta=a.cuenta and i.n2=a.n2 and i.n3=a.n3 and i.n4=a.n4 and i.n5='' limit 1);" + vbCrLf +
"update tblccontables a set idcuentan5=(select i.idccontable from tblccontablestemp i where i.cuenta=a.cuenta and i.n2=a.n2 and i.n3=a.n3 and i.n4=a.n4 and i.n5=a.n5 limit 1);" + vbCrLf +
"DROP TABLE IF EXISTS `tblccontablestemp`;"
        Comm.ExecuteNonQuery()
    End Sub
    Private Function quitarCeros(ByVal pTexto As String)
        Dim texto As String = ""
        If pTexto.Chars(0) = "0" Then
            For i As Integer = 0 To pTexto.Length.ToString - 1
                If pTexto.Chars(i) <> "0" Then
                    For j As Integer = i To pTexto.Length - 1
                        texto += pTexto.Chars(j)
                    Next
                    If texto = "" Then
                        texto = "0"
                    End If
                    Return texto
                End If
            Next
        Else
            texto = pTexto
        End If
        If texto = "" Then
            texto = "0"
        End If

        Return texto
    End Function
    Public Sub datosCombo(ByVal pidCuenta As Integer, ByVal pNivel As Integer)
        Comm.CommandText = "select descripcion from tblccontables where idCContable=" + pidCuenta.ToString()
        nombre = Comm.ExecuteScalar
        If pNivel = 1 Then
            Comm.CommandText = "select Cuenta from tblccontables where idCContable=" + pidCuenta.ToString()
            cuenta = Comm.ExecuteScalar
        End If
        If pNivel = 2 Then
            Comm.CommandText = "select N2 from tblccontables where idCContable=" + pidCuenta.ToString()
            cuenta = Comm.ExecuteScalar
        End If
        If pNivel = 3 Then
            Comm.CommandText = "select N3 from tblccontables where idCContable=" + pidCuenta.ToString()
            cuenta = Comm.ExecuteScalar
        End If
        If pNivel = 4 Then
            Comm.CommandText = "select N4 from tblccontables where idCContable=" + pidCuenta.ToString()
            cuenta = Comm.ExecuteScalar
        End If
        If pNivel = 5 Then
            Comm.CommandText = "select N5 from tblccontables where idCContable=" + pidCuenta.ToString()
            cuenta = Comm.ExecuteScalar
        End If
    End Sub

    Public Function existe(ByVal pcuenta As String, ByVal pnombre As String, ByVal pnivel As Integer, ByVal pn1 As String, ByVal pn2 As String, ByVal pn3 As String, ByVal pn4 As String, ByVal pn5 As String, Optional ByVal pBuscar As Boolean = False) As Boolean
        Dim contador As Integer = 0
        Dim aux As String = ""
        If pcuenta <> "" Then
            Comm.CommandText = "select count(descripcion) from tblccontables where cuenta='" + pcuenta.ToString() + "' and nivel=" + pnivel.ToString
            aux = "select idCContable from tblccontables where cuenta='" + pcuenta.ToString() + "' and nivel=" + pnivel.ToString
        Else
            Comm.CommandText = "select count(descripcion) from tblccontables where descripcion='" + pnombre.ToString() + "' and nivel=" + pnivel.ToString
            aux = "select idCContable from tblccontables where descripcion='" + pnombre.ToString() + "' and nivel=" + pnivel.ToString
        End If
        If pnivel >= 1 Then
            If pcuenta = "" And pnivel = 1 Then

            Else
                Comm.CommandText += " and Cuenta='" + pn1 + "'"
                aux += " and Cuenta='" + pn1 + "'"

            End If


        End If
        If pnivel >= 2 Then

            If pcuenta = "" And pnivel = 2 Then

            Else
                Comm.CommandText += " and N2='" + pn2 + "'"
                aux += " and N2='" + pn2 + "'"
            End If

        End If
        If pnivel >= 3 Then

            If pcuenta = "" And pnivel = 3 Then

            Else
                Comm.CommandText += " and N3='" + pn3 + "'"
                aux += " and N3='" + pn3 + "'"
            End If

        End If
        If pnivel >= 4 Then

            If pcuenta = "" And pnivel = 4 Then

            Else
                Comm.CommandText += " and N4='" + pn4 + "'"
                aux += " and N4='" + pn4 + "'"
            End If

        End If
        If pnivel >= 5 Then

            If pcuenta = "" And pnivel = 5 Then

            Else
                Comm.CommandText += " and N5='" + pn5 + "'"
                aux += " and N5='" + pn5 + "'"
            End If

        End If

        contador = Comm.ExecuteScalar

        If contador > 0 Then
            Comm.CommandText = aux
            IDcuenta = Comm.ExecuteScalar
            If pBuscar = True Then
                Eliminar(pcuenta, pn2, pn3, pn4, pn5, pnivel)
            End If

            Return True
        Else
            Return False
        End If
    End Function
    Public Sub LlenaIdsExtra(pId As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select idcuentam,idcuentan2,idcuentan3,idcuentan4,idcuentan5 from tblccontables where idccontable=" + pId.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read Then
            IdCuentaMayor = DReader("idcuentam")
            idNivel1 = DReader("idcuentam")
            idNivel2 = DReader("idcuentan2")
            idNivel3 = DReader("idcuentan3")
            idNivel4 = DReader("idcuentan4")
            idNivel5 = DReader("idcuentan5")
        End If
        DReader.Close()
    End Sub
    Public Function busquedaRegistro(ByVal pid As String) As Boolean
        IDcuenta = CInt(pid)
        Dim Encontro As Boolean = False
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where idCContable=" + IDcuenta.ToString + ""
        DReader = Comm.ExecuteReader
        Dim DescTemp As String = ""
        If DReader.Read() Then
            N1 = DReader("Cuenta")
            N2 = DReader("N2")
            N3 = DReader("N3")
            N4 = DReader("N4")
            N5 = DReader("N5")
            DescTemp = DReader("Descripcion")
            Descripcion = DescTemp
            NombreAnt = DescTemp
            Nivel = DReader("Nivel")
            Tipo = DReader("Tipo")
            Naturaleza = DReader("Naturaleza")
            fecha = DReader("fecha")
            cmb1 = DReader("cmb1")
            cmb2 = DReader("cmb2")
            idContable = DReader("idContable")
            DIOT = DReader("DIOT")
            Descontinuada = DReader("descontinuada")
            IdCuentaMayor = DReader("idcuentam")
            idNivel1 = IdCuentaMayor
            idNivel2 = DReader("idcuentan2")
            idNivel3 = DReader("idcuentan3")
            idNivel4 = DReader("idcuentan4")
            idNivel5 = DReader("idcuentan5")
            CuentaAnt = N1.PadLeft(p.NNiv1, "0")
            If N2 <> "" Then CuentaAnt += N2.PadLeft(p.NNiv2, "0")
            If N3 <> "" Then CuentaAnt += N3.PadLeft(p.NNiv3, "0")
            If N4 <> "" Then CuentaAnt += N4.PadLeft(p.NNiv4, "0")
            If N5 <> "" Then CuentaAnt += N5.PadLeft(p.NNiv5, "0")
            Depreciacion = DReader("depreciacion")
            Encontro = True
        End If
        DReader.Close()
        Descripcion2 = ""
        Descripcion3 = ""
        Descripcion4 = ""
        Descripcion5 = ""
        If Nivel > 1 Then
            Comm.CommandText = "select descripcion from tblccontables where cuenta='" + N1 + "' and n2='' and n3='' and n4='' and n5=''"
            Descripcion = Comm.ExecuteScalar
            Descripcion2 = DescTemp
        End If
        If Nivel > 2 Then
            Comm.CommandText = "select descripcion from tblccontables where cuenta='" + N1 + "' and n2='" + N2 + "' and n3='' and n4='' and n5=''"
            Descripcion2 = Comm.ExecuteScalar
            Descripcion3 = DescTemp
        End If
        If Nivel > 3 Then
            Comm.CommandText = "select descripcion from tblccontables where cuenta='" + N1 + "' and n2='" + N2 + "' and n3='" + N3 + "' and n4='' and n5=''"
            Descripcion3 = Comm.ExecuteScalar
            Descripcion4 = DescTemp
        End If
        If Nivel > 4 Then
            Comm.CommandText = "select descripcion from tblccontables where cuenta='" + N1 + "' and n2='" + N2 + "' and n3='" + N3 + "' and n4='" + N4 + "' and n5=''"
            Descripcion4 = Comm.ExecuteScalar
            Descripcion5 = DescTemp
        End If

        Return Encontro
    End Function
    Public Sub buscarID(ByVal pn1 As String, ByVal pN2 As String, ByVal pn3 As String, ByVal pN4 As String, ByVal pn5 As String, ByVal pniv As Integer)
        Comm.CommandText = "select idCContable from tblccontables where Cuenta='" + pn1 + "'"
        If pniv >= 2 Then
            Comm.CommandText += " and N2='" + pN2 + "'"
        End If
        If pniv >= 3 Then
            Comm.CommandText += " and N3='" + pn3 + "'"
        End If
        If pniv >= 4 Then
            Comm.CommandText += " and N4='" + pN4 + "'"
        End If
        If pniv >= 5 Then
            Comm.CommandText += " and N5='" + pn5 + "'"
        End If

        Comm.CommandText += " and nivel=" + pniv.ToString + ""

        idContable = Comm.ExecuteScalar
        busquedaRegistro(idContable)
    End Sub

    Public Function buscarIDlight(ByVal pn1 As String, ByVal pN2 As String, ByVal pn3 As String, ByVal pN4 As String, ByVal pn5 As String) As Integer
        Comm.CommandText = "select ifnull((select idCContable from tblccontables where Cuenta='" + CInt(pn1).ToString + "'"
        If pN2 <> "" And pn3 <> "" And pN4 = "" And pn5 = "" Then
            Comm.CommandText += " and N2='" + CInt(pN2).ToString + "'"
        End If
        If pN2 <> "" And pn3 <> "" And pN4 <> "" And pn5 = "" Then
            Comm.CommandText += " and N3='" + CInt(pn3).ToString + "'"
        End If
        If pN2 <> "" And pn3 <> "" And pN4 <> "" And pn5 <> "" Then
            Comm.CommandText += " and N4='" + CInt(pN4).ToString + "'"
        End If
        Comm.CommandText += "),0)"
        Return Comm.ExecuteScalar
        'busquedaRegistro(idContable)
    End Function


    Public Sub Guardar(ByVal pCuenta As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pDescripcion As String, ByVal pNivel As Integer, ByVal pTipo As String, ByVal pNaturaleza As String, ByVal pFecha As String, ByVal pcmb1 As Integer, ByVal pcmb2 As Integer, ByVal pIDCOntable As Integer, ByVal pDIOT As Integer, pIdCuentam As Integer, pIdNivel2 As Integer, pIdNivel3 As Integer, pIdNivel4 As Integer, pIdNivel5 As Integer)
        If pN2 <> "" Then pN2 = CInt(pN2).ToString
        If pN3 <> "" Then pN3 = CInt(pN3).ToString
        If pN4 <> "" Then pN4 = CInt(pN4).ToString
        If pN5 <> "" Then pN5 = CInt(pN5).ToString
        If pNaturaleza = 2 Then
            pNaturaleza = 0
            Depreciacion = 1
        Else
            Depreciacion = 0
        End If

        Comm.CommandText = "insert into tblccontables (Cuenta,N2,N3,N4,N5,Descripcion, Nivel, CuentaComp, Tipo, Naturaleza, fecha,cmb1,cmb2,idContable,DIOT,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,descontinuada,idcuentam,idcuentan2,idcuentan3,idcuentan4,idcuentan5,depreciacion) values('" + Replace(CInt(pCuenta).ToString, "'", "''") + "','" + Replace(pN2, "'", "''") + "','" + Replace(pN3, "'", "''") + "','" + Replace(pN4, "'", "''") + "','" + Replace(pN5, "'", "''") + "','" + Replace(pDescripcion, "'", "''") + "'," + pNivel.ToString + ",'" + Replace(CInt(pCuenta).ToString + " " + pN2 + " " + pN3 + " " + pN4 + " " + pN5, "'", "''") + "'," + pTipo.ToString + "," + pNaturaleza + ",'" + pFecha + "'," + pcmb1.ToString + "," + pcmb2.ToString + "," + pIDCOntable.ToString + "," + pDIOT.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "',''," + pIdCuentam.ToString + "," + pIdNivel2.ToString + "," + pIdNivel3.ToString + "," + pIdNivel4.ToString + "," + pIdNivel5.ToString + "," + Depreciacion.ToString + ");"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull(last_insert_id(),0);"
        IDcuenta = Comm.ExecuteScalar
        If pNivel = 1 Then
            IdCuentaMayor = IDcuenta
            Comm.CommandText = "update tblccontables set idcuentam=" + IDcuenta.ToString + " where idccontable=" + IDcuenta.ToString
            Comm.ExecuteNonQuery()
        End If
        If pNivel = 2 Then
            idNivel2 = IDcuenta
            Comm.CommandText = "update tblccontables set idcuentan2=" + IDcuenta.ToString + " where idccontable=" + IDcuenta.ToString
            Comm.ExecuteNonQuery()
        End If
        If pNivel = 3 Then
            idNivel3 = IDcuenta
            Comm.CommandText = "update tblccontables set idcuentan3=" + IDcuenta.ToString + " where idccontable=" + IDcuenta.ToString
            Comm.ExecuteNonQuery()
        End If
        If pNivel = 4 Then
            idNivel4 = IDcuenta
            Comm.CommandText = "update tblccontables set idcuentan4=" + IDcuenta.ToString + " where idccontable=" + IDcuenta.ToString
            Comm.ExecuteNonQuery()
        End If
        IdCuentaAnt = IDcuenta
    End Sub
    Public Sub ModificarAgrupador(pidCuenta As Integer, pNivel As Byte, pIdAgrupador As Integer)
        Dim str As String = ""
        If pNivel < 5 Then
            If pNivel = 1 Then str = " idcuentam=" + pidCuenta.ToString
            If pNivel = 2 Then str = " idcuentan2=" + pidCuenta.ToString
            If pNivel = 3 Then str = " idcuentan3=" + pidCuenta.ToString
            If pNivel = 4 Then str = " idcuentan4=" + pidCuenta.ToString
            Comm.CommandText = "update tblccontables set idcontable=" + pIdAgrupador.ToString + " where " + str
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCuenta As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pDescripcion As String, ByVal pNivel As Integer, ByVal pTipo As String, ByVal pNaturaleza As String, ByVal pFecha As String, ByVal pcmb1 As Integer, ByVal pcmb2 As Integer, ByVal pIDCOntable As Integer, ByVal pDIOT As Integer, PDescon As String)
        If pN2 <> "" Then pN2 = CInt(pN2).ToString
        If pN3 <> "" Then pN3 = CInt(pN3).ToString
        If pN4 <> "" Then pN4 = CInt(pN4).ToString
        If pN5 <> "" Then pN5 = CInt(pN5).ToString
        If pNaturaleza = 2 Then
            pNaturaleza = 0
            Depreciacion = 1
        Else
            Depreciacion = 0
        End If
        Comm.CommandText = "update tblccontables set Cuenta='" + Replace(CInt(pCuenta).ToString, "'", "''") + "',N2='" + Replace(pN2, "'", "''") + "',N3='" + Replace(pN3, "'", "''") + "',N4='" + Replace(pN4, "'", "''") + "',N5='" + Replace(pN5, "'", "''") + "',Descripcion='" + Replace(pDescripcion, "'", "''") + "', Nivel=" + pNivel.ToString + ", CuentaComp='" + Replace(CInt(pCuenta).ToString + " " + pN2 + " " + pN3 + " " + pN4 + " " + pN5, "'", "''") + "', Tipo=" + pTipo.ToString + ", Naturaleza=" + pNaturaleza.ToString + ", fecha='" + pFecha + "', cmb1=" + pcmb1.ToString + " ,cmb2=" + pcmb2.ToString + ", idContable=" + pIDCOntable.ToString + ",DIOT=" + pDIOT.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "',descontinuada='" + Replace(PDescon, "'", "''") + "',depreciacion=" + Depreciacion.ToString + " where idCContable=" + pID.ToString
        Comm.ExecuteNonQuery()
        If N4Ant <> pN4 Then
            Comm.CommandText = "update tblccontables set n4='" + Replace(CInt(pN4).ToString, "'", "''") + "',CuentaComp=concat(cuenta,' ',n2,' ',n3,' ','" + Replace(CInt(pN4).ToString, "'", "''") + "',' ',n5) where cuenta='" + N1Ant + "' and n2='" + N2Ant + "' and n3='" + N3Ant + "' and n4='" + N4Ant + "'"
            Comm.ExecuteNonQuery()
        End If
        If N3Ant <> pN3 Then
            Comm.CommandText = "update tblccontables set n3='" + Replace(CInt(pN3).ToString, "'", "''") + "',CuentaComp=concat(cuenta,' ',n2,' ','" + Replace(CInt(pN3).ToString, "'", "''") + "',' ',n4,' ',n5) where cuenta='" + N1Ant + "' and n2='" + N2Ant + "' and n3='" + N3Ant + "'"
            Comm.ExecuteNonQuery()
        End If
        If N2Ant <> pN2 Then
            Comm.CommandText = "update tblccontables set n2='" + Replace(CInt(pN2).ToString, "'", "''") + "',CuentaComp=concat(cuenta,' ','" + Replace(CInt(pN2).ToString, "'", "''") + "',' ',n3,' ',n4,' ',n5) where cuenta='" + N1Ant + "' and n2='" + N2Ant + "'"
            Comm.ExecuteNonQuery()
        End If
        If N1Ant <> pCuenta Then
            Comm.CommandText = "update tblccontables set cuenta='" + Replace(CInt(pCuenta).ToString, "'", "''") + "',CuentaComp=concat('" + Replace(CInt(pCuenta).ToString, "'", "''") + "',' ',n2,' ',n3,' ',n4,' ',n5) where cuenta='" + N1Ant + "'"
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub ModificarTipoSubC(ByVal pCuenta As String, ByVal pTipoCuenta As Integer, ByVal pNaturaleza As Integer)
        Comm.CommandText = "update tblccontables set  Tipo=" + pTipoCuenta.ToString + ", Naturaleza=" + pNaturaleza.ToString + " where Cuenta=" + pCuenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function BuscaSubcuentas(pIdCuenta As Integer) As Integer
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select cuenta,n2,n3,n4,n5 from tblccontables where idccontable=" + pIdCuenta.ToString
        DR = Comm.ExecuteReader
        If DR.Read Then
            intN1 = DR("cuenta")
            intN2 = DR("n2")
            intN3 = DR("n3")
            intN4 = DR("n4")
            intn5 = DR("n5")
        End If
        DR.Close()
        Comm.CommandText = "select count(idccontable) from tblccontables where cuenta='" + intN1 + "'"
        If intN2 <> "" Then Comm.CommandText += " and n2='" + intN2 + "'"
        If intN3 <> "" Then Comm.CommandText += " and n3='" + intN3 + "'"
        If intN4 <> "" Then Comm.CommandText += " and n4='" + intN4 + "'"
        If intn5 <> "" Then Comm.CommandText += " and n5='" + intn5 + "'"
        Return Comm.ExecuteScalar
    End Function
    Public Function TieneMovimientosLasSubcuentas(pidCuenta As Integer) As Boolean
        Comm.CommandText = "select count(idcuenta) from tblpolizasdetalles inner join tblccontables on tblpolizasdetalles.idcuenta=tblccontables.idccontable where tblccontables.cuenta='" + intN1 + "'"
        If intN2 <> "" Then Comm.CommandText += " and tblccontables.n2='" + intN2 + "'"
        If intN3 <> "" Then Comm.CommandText += " and tblccontables.n3='" + intN3 + "'"
        If intN4 <> "" Then Comm.CommandText += " and tblccontables.n4='" + intN4 + "'"
        If intn5 <> "" Then Comm.CommandText += " and tblccontables.n5='" + intn5 + "'"
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function subcategorias(ByVal pCuenta As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pNivel As Integer) As Integer
        Dim contados As Integer = 0
        Comm.CommandText = "select count(idCContable) from tblccontables where Cuenta='" + Replace(pCuenta, "'", "''") + "'"
        If Nivel >= 2 Then
            Comm.CommandText += " and N2='" + Replace(pN2, "'", "''") + "'"
        End If
        If Nivel >= 3 Then
            Comm.CommandText += " and N3='" + Replace(pN3, "'", "''") + "'"
        End If
        If Nivel >= 4 Then
            Comm.CommandText += " and N4='" + Replace(pN4, "'", "''") + "'"
        End If
        If Nivel >= 5 Then
            Comm.CommandText += " and N5='" + Replace(pN5, "'", "''") + "'"
        End If
        contados = Comm.ExecuteScalar
        Return contados
    End Function
    Public Sub Eliminar(ByVal pCuenta As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pNivel As Integer)
        Comm.CommandText = "delete from tblccontables where Cuenta='" + Replace(intN1, "'", "''") + "'"
        If intN2 <> "" Then Comm.CommandText += " and n2='" + intN2 + "'"
        If intN3 <> "" Then Comm.CommandText += " and n3='" + intN3 + "'"
        If intN4 <> "" Then Comm.CommandText += " and n4='" + intN4 + "'"
        If intn5 <> "" Then Comm.CommandText += " and n5='" + intn5 + "'"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub conocerID(ByVal pN1 As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pNivel As Integer)
        Dim pID As Integer = 0
        Comm.CommandText = "select idCContable from tblccontables where Cuenta='" + Replace(pN1, "'", "''") + "'"
        If pNivel >= 2 Then
            Comm.CommandText += " and N2='" + Replace(pN2, "'", "''") + "'"
        End If
        If pNivel >= 3 Then
            Comm.CommandText += " and N3='" + Replace(pN3, "'", "''") + "'"
        End If
        If pNivel >= 4 Then
            Comm.CommandText += " and N4='" + Replace(pN4, "'", "''") + "'"
        End If
        If pNivel >= 5 Then
            Comm.CommandText += " and N5='" + Replace(pN5, "'", "''") + "'"
        End If
        pID = Comm.ExecuteScalar
        busquedaRegistro(pID)

    End Sub
    Public Function repetida(ByVal pCuenta As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pn4 As String, ByVal pn5 As String, ByVal pID As Integer, ByVal pNivel As Integer) As Boolean
        Dim con As Integer = 0
        Comm.CommandText = "select count(idCContable) from tblccontables where "
        'If pNivel >= 1 Then
        Comm.CommandText += "Cuenta='" + Replace(CInt(pCuenta).ToString, "'", "''") + "'"
        'End If
        If pN2 <> "" Then
            Comm.CommandText += " and N2='" + Replace(CInt(pN2).ToString, "'", "''") + "'"
        End If
        If pN3 <> "" Then
            Comm.CommandText += " and N3='" + Replace(CInt(pN3).ToString, "'", "''") + "'"
        End If
        If pn4 <> "" Then
            Comm.CommandText += " and N4='" + Replace(CInt(pn4).ToString, "'", "''") + "'"
        End If
        If pn5 <> "" Then
            Comm.CommandText += " and N5='" + Replace(CInt(pn5).ToString, "'", "''") + "'"
        End If
        'Comm.CommandText += " and nivel=" + pNivel.ToString + " and idCContable<>" + pID.ToString
        con = Comm.ExecuteScalar
        If con > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function NombreRepetido(ByVal pCuenta As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pn4 As String, ByVal pn5 As String, ByVal pDescripcion As String, ByVal pNivel As Integer) As Boolean
        Dim con As Integer = 0
        Comm.CommandText = "select count(idCContable) from tblccontables where "
        If pNivel > 1 Then
            Comm.CommandText += "Cuenta='" + Replace(CInt(pCuenta).ToString, "'", "''") + "'"
        Else
            Comm.CommandText += "Cuenta<>''"
        End If
        If pNivel > 2 Then
            Comm.CommandText += " and N2='" + Replace(CInt(pN2).ToString, "'", "''") + "'"
        Else
            If pNivel = 2 Then
                Comm.CommandText += " and N2<>''"
            Else
                Comm.CommandText += " and N2=''"
            End If

        End If
        If pNivel > 3 Then
            Comm.CommandText += " and N3='" + Replace(CInt(pN3).ToString, "'", "''") + "'"
        Else
            If pNivel = 3 Then
                Comm.CommandText += " and N3<>''"
            Else
                Comm.CommandText += " and N3=''"
            End If

        End If
        If pNivel > 4 Then
            Comm.CommandText += " and N4='" + Replace(CInt(pn4).ToString, "'", "''") + "'"
        Else
            If pNivel = 4 Then
                Comm.CommandText += " and N4<>''"
            Else
                Comm.CommandText += " and N4=''"
            End If

        End If
        If pNivel = 5 Then
            Comm.CommandText += " and N5<>''"
        Else
            Comm.CommandText += " and N5=''"
        End If
        Comm.CommandText += " and descripcion like '" + pDescripcion.ToString + "'"
        con = Comm.ExecuteScalar
        If con > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub ModificarCuentas(ByVal anterior As String, ByVal pCuenta As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pNivel As String)

        Comm.CommandText = "update tblccontables set "
        If pNivel = 1 Then
            Comm.CommandText += " Cuenta='" + Replace(pCuenta, "'", "''") + "'"
        Else
            If pNivel = 2 Then
                Comm.CommandText += " N2='" + Replace(pN2, "'", "''") + "'"
            Else
                If pNivel = 3 Then
                    Comm.CommandText += " N3='" + Replace(pN3, "'", "''") + "'"
                Else
                    If pNivel = 4 Then
                        Comm.CommandText += " N4='" + Replace(pN4, "'", "''") + "'"
                    Else
                        If pNivel = 5 Then
                            Comm.CommandText += " N5='" + Replace(pN5, "'", "''") + "'"
                        End If
                    End If
                End If
            End If
        End If

        ' Comm.CommandText += ", CuentaComp='" + pCuenta + " " + pN2 + " " + pN3 + " " + pN4 + " " + pN5 + "'"
        Comm.CommandText += " where "
        If pNivel = 1 Then
            Comm.CommandText += " Cuenta='" + Replace(anterior, "'", "''") + "'"
        Else
            If pNivel > 1 Then
                Comm.CommandText += " Cuenta='" + Replace(pCuenta, "'", "''") + "'"
            End If
        End If
        If pNivel = 2 Then
            Comm.CommandText += " and  N2='" + Replace(anterior, "'", "''") + "'"
        Else
            If pNivel > 2 Then
                Comm.CommandText += " and N2='" + Replace(pN2, "'", "''") + "'"
            End If
        End If
        If pNivel = 3 Then
            Comm.CommandText += " and  N3='" + Replace(anterior, "'", "''") + "'"
        Else
            If pNivel > 3 Then
                Comm.CommandText += " and N3='" + Replace(pN3, "'", "''") + "'"
            End If
        End If
        If pNivel = 4 Then
            Comm.CommandText += " and  N4='" + Replace(anterior, "'", "''") + "'"
        Else
            If pNivel > 4 Then
                Comm.CommandText += " and N4='" + Replace(pN4, "'", "''") + "'"
            End If
        End If
        If pNivel = 5 Then
            Comm.CommandText += " and  N5='" + Replace(anterior, "'", "''") + "'"
        Else
            If pNivel > 5 Then
                Comm.CommandText += " and N5='" + Replace(pN5, "'", "''") + "'"
            End If
        End If

        Comm.ExecuteNonQuery()
        'Comm.CommandText = "update tblccontables set cuentacomp='(Select Cuenta,N2,N3,N4 from tblccontables)'"

    End Sub
    Public Sub ModificarCODSAT(ByVal anterior As String, ByVal pCuenta As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pNivel As String, ByVal pIDSAT As Integer, ByVal pcmb1 As Integer, ByVal pcmb2 As Integer)

        Comm.CommandText = "update tblccontables set idContable=" + pIDSAT.ToString + ", cmb1=" + pcmb1.ToString + " ,cmb2=" + pcmb2.ToString

        Comm.CommandText += " where "
        If pNivel = 1 Then
            Comm.CommandText += " Cuenta='" + Replace(anterior, "'", "''") + "'"
        Else
            If pNivel > 1 Then
                Comm.CommandText += " Cuenta='" + Replace(pCuenta, "'", "''") + "'"
            End If
        End If
        If pNivel = 2 Then
            Comm.CommandText += " and  N2='" + Replace(anterior, "'", "''") + "'"
        Else
            If pNivel > 2 Then
                Comm.CommandText += " and N2='" + Replace(pN2, "'", "''") + "'"
            End If
        End If
        If pNivel = 3 Then
            Comm.CommandText += " and  N3='" + Replace(anterior, "'", "''") + "'"
        Else
            If pNivel > 3 Then
                Comm.CommandText += " and N3='" + Replace(pN3, "'", "''") + "'"
            End If
        End If
        If pNivel = 4 Then
            Comm.CommandText += " and  N4='" + Replace(anterior, "'", "''") + "'"
        Else
            If pNivel > 4 Then
                Comm.CommandText += " and N4='" + Replace(pN4, "'", "''") + "'"
            End If
        End If
        If pNivel = 5 Then
            Comm.CommandText += " and  N5='" + Replace(anterior, "'", "''") + "'"
        Else
            If pNivel > 5 Then
                Comm.CommandText += " and N5='" + Replace(pN5, "'", "''") + "'"
            End If
        End If

        Comm.ExecuteNonQuery()


    End Sub
    'filtros
    Public Function reporte() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblccontables.idCContable,LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),tblccontables.Descripcion,tblccontables.Nivel,CASE tblccontables.tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'COSTOS' WHEN 5 THEN 'EGRESOS' WHEN 6 then 'ORDEN' END, case tblccontables.Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end,tblagrupadorcuentas.codigo,tblccontables.Cuenta  from tblccontables inner join tblagrupadorcuentas on tblccontables.IdContable=tblagrupadorcuentas.id where tblccontables.Nivel='1' ORDER BY tblccontables.Cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblCuentas")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblCuentas").DefaultView
    End Function
    Public Function subCuentas(ByVal id As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblccontables.idCContable,CONCAT(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0')),tblccontables.Descripcion,tblccontables.Nivel,tblccontables.N2,CASE tblccontables.tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'COSTOS' WHEN 5 THEN 'EGRESOS' WHEN 6 then 'ORDEN' END, case tblccontables.Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end, tblagrupadorcuentas.codigo  from tblccontables inner join tblagrupadorcuentas on tblccontables.IdContable=tblagrupadorcuentas.id where tblccontables.Cuenta='" + id.ToString + "' AND tblccontables.Nivel='2' ORDER BY tblccontables.Cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblbancos").DefaultView
    End Function

    Public Function subCuentas3(ByVal id As String, ByVal pC2 As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblccontables.idCContable,CONCAT(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0')),tblccontables.Descripcion,tblccontables.Nivel,tblccontables.N3,CASE tblccontables.tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'COSTOS' WHEN 5 THEN 'EGRESOS' WHEN 6 then 'ORDEN' END, case tblccontables.Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end ,tblagrupadorcuentas.codigo  from tblccontables inner join tblagrupadorcuentas on tblccontables.IdContable=tblagrupadorcuentas.id where tblccontables.Cuenta='" + id.ToString + "' AND tblccontables.Nivel='3' AND tblccontables.N2='" + pC2.ToString + "' ORDER BY tblccontables.Cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblbancos").DefaultView
    End Function

    Public Function subCuentas4(ByVal id As String, ByVal pC2 As String, ByVal pC3 As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblccontables.idCContable,CONCAT(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),' ',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0')),tblccontables.Descripcion,tblccontables.Nivel,CASE tblccontables.tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'COSTOS' WHEN 5 THEN 'EGRESOS' WHEN 6 then 'ORDEN' END, case tblccontables.Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end,tblagrupadorcuentas.codigo,tblccontables.n4  from tblccontables inner join tblagrupadorcuentas on tblccontables.IdContable=tblagrupadorcuentas.id where tblccontables.Cuenta='" + id.ToString + "' AND tblccontables.Nivel='4' AND tblccontables.N2='" + pC2.ToString + "'AND tblccontables.N3='" + pC3.ToString + "' ORDER BY tblccontables.Cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblbancos").DefaultView
    End Function


    Public Function subCuentas5(ByVal id As String, ByVal pC2 As String, ByVal pC3 As String, ByVal pC4 As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblccontables.idCContable,CONCAT(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),' ',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0'),' ',LPAD(tblccontables.N5," + p.NNiv5.ToString + ",'0')),tblccontables.Descripcion,tblccontables.Nivel,CASE tblccontables.tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'COSTOS' WHEN 5 THEN 'EGRESOS' WHEN 6 then 'ORDEN' END, case tblccontables.Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end ,tblagrupadorcuentas.codigo  from tblccontables inner join tblagrupadorcuentas on tblccontables.IdContable=tblagrupadorcuentas.id where tblccontables.Cuenta='" + id.ToString + "' AND tblccontables.Nivel='5' AND tblccontables.N2='" + pC2.ToString + "'AND tblccontables.N3='" + pC3.ToString + "'AND tblccontables.N4='" + pC4.ToString + "' ORDER BY tblccontables.Cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblbancos").DefaultView
    End Function
    Public Function Consulta(ByVal pcuenta As String, ByVal pnivel As Integer, ByVal pnombre As String, pMostrarDesc As Boolean, pSoloDescon As Boolean) As DataView
        Dim DS As New DataSet
        Dim Palabras() As String
        'If pModoB = 1 Then
        Palabras = pnombre.Split(Chr(32))
        'Else
        'ReDim Palabras(1)
        'Palabras(0) = pnombre
        'End If
        Comm.CommandText = "select tblccontables.idCContable,IF(tblccontables.Nivel=1,LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),IF(tblccontables.Nivel=2,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0')),IF(tblccontables.Nivel=3,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0')),IF(tblccontables.Nivel=4,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),' ',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0')),IF(tblccontables.Nivel=5,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),' ',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0'),' ',LPAD(tblccontables.N5," + p.NNiv5.ToString + ",'0')),''))))) as cpncat_cuenta," + _
"  tblccontables.Descripcion,tblccontables.nivel, CASE tblccontables.tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'COSTOS' WHEN 5 THEN 'EGRESOS' WHEN 6 then 'ORDEN' END AS TIPO,if(tblccontables.naturaleza=1 or tblccontables.depreciacion=1,'ACREEDORA','DEUDORA') as natiraleza ,ifnull((select codigo from tblagrupadorcuentas where id=tblccontables.idcontable),'Sin Asignar') as codigo  from tblccontables where tblccontables.idCContable<>0"
        ' Comm.CommandText = "select tblccontables.idCContable,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),' ',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0'),' ',LPAD(tblccontables.N5," + p.NNiv5.ToString + ",'0')) as cpncat_cuenta," + _
        '" tblccontables.Descripcion,tblccontables.nivel, CASE tblccontables.tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'COSTOS' WHEN 5 THEN 'EGRESOS' WHEN 6 then 'ORDEN' END, case tblccontables.Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end ,tblagrupadorcuentas.codigo  from tblccontables inner join tblagrupadorcuentas on tblccontables.IdContable=tblagrupadorcuentas.id where tblccontables.idCContable<>0"
        'For Each s As String In Palabras
        If pcuenta <> "" Then
            'Dim aux() As Char = pcuenta.ToCharArray
            If pcuenta.Length >= 6 Then
                If pcuenta.Chars(5) <> "0" Then
                    Comm.CommandText += " and IF(tblccontables.Nivel=1,tblccontables.Cuenta,IF(tblccontables.Nivel=2,concat(tblccontables.Cuenta,' ',tblccontables.N2),IF(tblccontables.Nivel=3,concat(tblccontables.Cuenta,' ',concat(tblccontables.N2,' ',tblccontables.N3)),IF(tblccontables.Nivel=4,concat(tblccontables.Cuenta,' ',concat(tblccontables.N2,' ',concat(tblccontables.N3,' ',tblccontables.N4))),IF(tblccontables.Nivel=5,concat(tblccontables.Cuenta,' ',concat(tblccontables.N2,' ',concat(tblccontables.N3,' ',concat(tblccontables.N4,' ',tblccontables.N5)))),''))))) like '%" + Replace(pcuenta.Trim, "'", "''") + "%'"
                Else
                    Comm.CommandText += " and IF(tblccontables.Nivel=1,LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),IF(tblccontables.Nivel=2,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0')),IF(tblccontables.Nivel=3,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0')),IF(tblccontables.Nivel=4,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),' ',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0')),IF(tblccontables.Nivel=5,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),' ',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0'),' ',LPAD(tblccontables.N5," + p.NNiv5.ToString + ",'0')),''))))) like '%" + Replace(pcuenta.Trim, "'", "''") + "%'"
                End If
            Else
                Comm.CommandText += " and IF(tblccontables.Nivel=1,tblccontables.Cuenta,IF(tblccontables.Nivel=2,concat(tblccontables.Cuenta,' ',tblccontables.N2),IF(tblccontables.Nivel=3,concat(tblccontables.Cuenta,' ',concat(tblccontables.N2,' ',tblccontables.N3)),IF(tblccontables.Nivel=4,concat(tblccontables.Cuenta,' ',concat(tblccontables.N2,' ',concat(tblccontables.N3,' ',tblccontables.N4))),IF(tblccontables.Nivel=5,concat(tblccontables.Cuenta,' ',concat(tblccontables.N2,' ',concat(tblccontables.N3,' ',concat(tblccontables.N4,' ',tblccontables.N5)))),''))))) like '%" + Replace(pcuenta.Trim, "'", "''") + "%'"

            End If
            'Comm.CommandText += " and IF(tblccontables.Nivel=1,tblccontables.Cuenta,IF(tblccontables.Nivel=2,concat(tblccontables.Cuenta,' ',tblccontables.N2),IF(tblccontables.Nivel=3,concat(tblccontables.Cuenta,' ',concat(tblccontables.N2,' ',tblccontables.N3)),IF(tblccontables.Nivel=4,concat(tblccontables.Cuenta,' ',concat(tblccontables.N2,' ',concat(tblccontables.N3,' ',tblccontables.N4))),IF(tblccontables.Nivel=5,concat(tblccontables.Cuenta,' ',concat(tblccontables.N2,' ',concat(tblccontables.N3,' ',concat(tblccontables.N4,' ',tblccontables.N5)))),''))))) like '%" + Replace(pcuenta.ToString, "'", "''") + "%'"
        End If
        'If pcuenta <> "" Then
        '    Comm.CommandText += " and concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),' ',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0'),' ',LPAD(tblccontables.N5," + p.NNiv5.ToString + ",'0')) like '%" + Replace(pcuenta, "'", "''") + "%'"
        'End If
        If pnivel <> 0 Then
            Comm.CommandText += " and tblccontables.nivel=" + pnivel.ToString
        End If
        If pSoloDescon = False Then
            If pMostrarDesc Then
                Comm.CommandText += " and (tblccontables.descontinuada like '%" + p.anio + "%')=False"
            End If
        Else
            Comm.CommandText += " and tblccontables.descontinuada like '%" + p.anio + "%'"
        End If
        'If pnombre <> "" Then
        '    Comm.CommandText += " and tblccontables.Descripcion like '%" + Replace(pnombre.Trim, "'", "''") + "%'"
        'End If
        For Each st As String In Palabras
            Comm.CommandText += " and tblccontables.Descripcion like '%" + Replace(st, "'", "''") + "%'"
        Next
        'Next


        Comm.CommandText += " order by cpncat_cuenta"

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblContabilidadCatalogo")
        '  DS.WriteXmlSchema("tblContabilidadCatalogo.xml")
        Return DS.Tables("tblContabilidadCatalogo").DefaultView



        'MySqlcom.Transaction = MySqlcom.Connection.BeginTransaction()
        'Dim r As DataGridViewRow
        'For Each r In DGServicios.Rows
        '    MySqlcom.CommandText = "update tblinventarioconciliaciones set existencia=" + r.Cells(7).Value.ToString + ",diferencia=" + r.Cells(8).Value.ToString + ",precio=" + r.Cells(9).Value.ToString + ",importedif=" + r.Cells(10).Value.ToString + " where fecha='" + Format(DateTimePicker2.Value, "yyyy/MM/dd") + "' and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString + " and idalmacen=" + IdAlmacenes.Valor(ComboBox8.SelectedIndex).ToString + " and idinventario=" + r.Cells(3).Value.ToString + ";"
        '    MySqlcom.ExecuteNonQuery()
        'Next
        'MySqlcom.Transaction.Commit()

    End Function
    Public Function ConDes(ByVal pNombre As String, ByVal pCuenta As String, ByVal pNivel As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idCContable,CONCAT(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),''),' ',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),''),' ',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0'),''),' ',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + p.NNiv5.ToString + ",'0'),'')),Descripcion,Nivel,CASE tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'COSTOS' WHEN 5 THEN 'EGRESOS' WHEN 6 then 'ORDEN' END, case Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end from tblccontables where idCContable>0"
        If pNombre <> "" Then
            Comm.CommandText += " and concat(Descripcion) like '%" + Replace(pNombre, "'", "''") + "%'"
        End If
        If pCuenta <> "" Then

            Comm.CommandText += " and CONCAT(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),' ',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),''),' ',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),''),' ',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0'),''),' ',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + p.NNiv5.ToString + ",'0'),'')) like '%" + Replace(pCuenta, "'", "''") + "%'"
        End If
        If pNivel <> 0 Then
            Comm.CommandText += " and Nivel=" + pNivel.ToString
        End If

        Comm.CommandText += " ORDER BY tblccontables.Cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        Return DS.Tables("tblbancos").DefaultView
    End Function

    Public Sub cosas()
        Dim DS As New DataSet
        Dim tabla As DataTable
        Dim st As String = ""
        Comm.CommandText = "select * from tblagrupadorcuentas "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        tabla = DS.Tables("tblbancos").DefaultView.ToTable

        For i As Integer = 0 To tabla.Rows.Count() - 1
            st += "insert into tblagrupadorcuentas (codigo,nombre,tipo,subtipo)values('" + tabla.Rows(i)(1).ToString + "','" + tabla.Rows(i)(2).ToString + "','" + tabla.Rows(i)(3).ToString + "','" + tabla.Rows(i)(4).ToString + "')" + vbCrLf
        Next
    End Sub
    Public Function TieneMovimientos(ByVal pID As Integer) As Boolean
        Comm.CommandText = "select count(idcuenta) from tblpolizasdetalles where idCuenta=" + pID.ToString + ""
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function TieneMovEliminar(ByVal pN1 As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pNivel As Integer)
        Dim DS As New DataSet
        Dim tabla As DataTable
        Dim st As String = ""
        Comm.CommandText = "select idCContable from tblccontables where Cuenta='" + pN1 + "' "
        If pNivel >= 2 Then
            Comm.CommandText += " and N2='" + pN2 + "'"
        End If
        If pNivel >= 3 Then
            Comm.CommandText += " and N3='" + pN3 + "'"
        End If
        If pNivel >= 4 Then
            Comm.CommandText += " and N4='" + pN4 + "'"
        End If
        If pNivel >= 5 Then
            Comm.CommandText += " and N5='" + pN5 + "'"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        tabla = DS.Tables("tblbancos").DefaultView.ToTable
        For i As Integer = 0 To tabla.Rows.Count - 1
            Comm.CommandText = "select count(id) from tblpolizasdetalles where idCuenta=" + tabla.Rows(i)(0).ToString + ""
            If Comm.ExecuteScalar > 0 Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function nombreEmpresa() As String 'consulta nombre de la empresa
        Dim nombre1 As String
        Comm.CommandText = "select nombre from tblsucursales where idSucursal=" + GlobalIdSucursalDefault.ToString
        nombre1 = Comm.ExecuteScalar
        Return nombre1
    End Function
    Public Function RFCEmpresa() As String 'consulta rfc de la empresa
        Dim rfc1 As String
        Comm.CommandText = "select rfc from tblsucursales where idSucursal=" + GlobalIdSucursalDefault.ToString
        rfc1 = Comm.ExecuteScalar
        Return rfc1
    End Function
    Public Function CuentasCount(ByVal pFechaI As String, ByVal pFechaF As String) As String
        Dim cuenta As Integer
        Comm.CommandText = "select count(idCContable) from tblccontables where tblccontables.fecha>='" + pFechaI + "' and tblccontables.fecha<='" + pFechaF + "'"
        cuenta = Comm.ExecuteScalar
        Return cuenta
    End Function
    Private Function RC(ByVal pTexto As String)
        Dim texto As String = pTexto
        texto = Replace(texto, "&", "&amp;")
        texto = Replace(texto, "'", "&apos;")
        texto = Replace(texto, ">", "&gt;")
        texto = Replace(texto, "<", "&lt;")
        texto = Replace(texto, """", "&quot;")
        Return texto
    End Function
    'GENERAR XML
    'Public Function generaXML(ByVal pFechaI As String, ByVal pFechaF As String)
    '    Dim xml As String = ""
    '    Dim xml2 As String = ""
    '    Dim tabla As DataTable
    '    Dim xmldoc As New System.Xml.XmlDocument
    '    Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
    '    Dim cadena As String = ""

    '    cadena = "||1.1|" + s.RFC + "|" + Date.Parse(pFechaI).Month.ToString("00") + "|" + Date.Parse(pFechaI).Year.ToString
    '    ' xml = "<Catalogo Version=""1.1"" RFC=""" + s.RFC + """ TotalCtas=""" + CuentasCount(pFechaI, pFechaF) + """ Mes=""" + Date.Parse(pFechaI).Month.ToString("00") + """ Ano=""" + Date.Parse(pFechaI).Year.ToString + """>" + vbCrLf
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select tblagrupadorcuentas.codigo,IF(tblccontables.Nivel=1,LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),IF(tblccontables.Nivel=2,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0')),IF(tblccontables.Nivel=3,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0')),IF(tblccontables.Nivel=4,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),'',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0')),IF(tblccontables.Nivel=5,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),'',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0'),'',LPAD(tblccontables.N5," + p.NNiv5.ToString + ",'0')),''))))) as cpncat_cuenta,tblccontables.Descripcion,tblccontables.Nivel, case tblccontables.Naturaleza when 0 then 'D' when 1 then 'A' end  as tipo," + _
    '    "IF(tblccontables.Nivel=1,'',IF(tblccontables.Nivel=2,LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),IF(tblccontables.Nivel=3,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0')),IF(tblccontables.Nivel=4,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0')),IF(tblccontables.Nivel=5,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),'',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0')),''))))) as subCuenta" + _
    '    "  from tblccontables inner join tblagrupadorcuentas on tblccontables.IdContable=tblagrupadorcuentas.id  order by cpncat_cuenta"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblCuentas")
    '    tabla = DS.Tables("tblCuentas")

    '    For i As Integer = 0 To tabla.Rows.Count - 1
    '        xml += "<catalogocuentas:Ctas CodAgrup=""" + tabla.Rows(i)(0).ToString + """ NumCta=""" + tabla.Rows(i)(1).ToString + """ Desc=""" + RC(tabla.Rows(i)(2).ToString) + """ "

    '        If tabla.Rows(i)(5).ToString <> "" Then
    '            xml += " SubCtaDe=""" + tabla.Rows(i)(5).ToString + """"
    '        End If
    '        xml += " Nivel=""" + tabla.Rows(i)(3).ToString + """ Natur=""" + tabla.Rows(i)(4).ToString + """/>" + vbCrLf
    '        cadena += "|" + tabla.Rows(i)(0).ToString + "|" + tabla.Rows(i)(1).ToString + "|" + tabla.Rows(i)(2).ToString
    '        If tabla.Rows(i)(5).ToString <> "" Then
    '            cadena += "|" + tabla.Rows(i)(5).ToString
    '        End If
    '        cadena += "|" + tabla.Rows(i)(3).ToString + "|" + tabla.Rows(i)(4).ToString
    '    Next
    '    cadena += "||"
    '    'aqui ya se tiene la cadena
    '    Dim Archivos As New dbSucursalesArchivos
    '    Dim en As New Encriptador
    '    Dim Sello As String = ""

    '    Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
    '    Sello = en.GeneraSello(cadena, Archivos.RutaCer, "2011")

    '    Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, True)
    '    en.Leex509(Archivos.RutaCer)

    '    xml2 = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
    '    xml2 += "<catalogocuentas:Catalogo "
    '    xml2 += "xsi:schemaLocation=""http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/CatalogoCuentas/CatalogoCuentas_1_1.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""  xmlns:catalogocuentas=""http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/CatalogoCuentas"""
    '    xml2 += " Version=""1.1"" RFC=""" + s.RFC + """ Mes=""" + Date.Parse(pFechaI).Month.ToString("00") + """ Anio=""" + Date.Parse(pFechaI).Year.ToString + """"
    '    ' If Sello <> "" Then
    '    xml2 += " Sello=""" + Sello + """"
    '    ' End If
    '    ' If pNoCertificado <> "" Then
    '    xml2 += " noCertificado=""" + en.Seriex509 + """"
    '    ' End If
    '    ' If pCertificado <> "" Then
    '    xml2 += " Certificado=""" + en.Certificado64 + """"
    '    ' End If
    '    xml2 += ">"
    '    xml2 += xml
    '    xml2 += "</catalogocuentas:Catalogo>"
    '    xmldoc.LoadXml(xml2)

    '    Return xmldoc
    'End Function
    Public Function generaXML(ByVal pFechaI As String, ByVal pFechaF As String) As System.Xml.XmlDocument
        Dim xml As String = ""
        Dim xml2 As String = ""
        Dim tabla As DataTable
        Dim xmldoc As New System.Xml.XmlDocument
        Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Dim cadena As String = ""

        cadena = "||1.1|" + s.RFC + "|" + Date.Parse(pFechaI).Month.ToString("00") + "|" + Date.Parse(pFechaI).Year.ToString
        ' xml = "<Catalogo Version=""1.1"" RFC=""" + s.RFC + """ TotalCtas=""" + CuentasCount(pFechaI, pFechaF) + """ Mes=""" + Date.Parse(pFechaI).Month.ToString("00") + """ Ano=""" + Date.Parse(pFechaI).Year.ToString + """>" + vbCrLf
        Dim DS As New DataSet
        Comm.CommandText = "select tblagrupadorcuentas.codigo,IF(tblccontables.Nivel=1,LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),IF(tblccontables.Nivel=2,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0')),IF(tblccontables.Nivel=3,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0')),IF(tblccontables.Nivel=4,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),'',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0')),IF(tblccontables.Nivel=5,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),'',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0'),'',LPAD(tblccontables.N5," + p.NNiv5.ToString + ",'0')),''))))) as cpncat_cuenta,tblccontables.Descripcion,tblccontables.Nivel, case tblccontables.Naturaleza when 0 then 'D' when 1 then 'A' end  as tipo," + _
        "IF(tblccontables.Nivel=1,'',IF(tblccontables.Nivel=2,LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),IF(tblccontables.Nivel=3,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0')),IF(tblccontables.Nivel=4,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0')),IF(tblccontables.Nivel=5,concat(LPAD(tblccontables.Cuenta," + p.NNiv1.ToString + ",'0'),'',LPAD(tblccontables.N2," + p.NNiv2.ToString + ",'0'),'',LPAD(tblccontables.N3," + p.NNiv3.ToString + ",'0'),'',LPAD(tblccontables.N4," + p.NNiv4.ToString + ",'0')),''))))) as subCuenta" + _
        "  from tblccontables inner join tblagrupadorcuentas on tblccontables.IdContable=tblagrupadorcuentas.id where (tblccontables.descontinuada like '%" + p.anio + "%')=false  order by cpncat_cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblCuentas")
        tabla = DS.Tables("tblCuentas")

        For i As Integer = 0 To tabla.Rows.Count - 1
            xml += "<catalogocuentas:Ctas CodAgrup=""" + tabla.Rows(i)(0).ToString.Trim + """ NumCta=""" + tabla.Rows(i)(1).ToString.Trim + """ Desc=""" + RC(tabla.Rows(i)(2).ToString.Trim) + """ "

            If tabla.Rows(i)(5).ToString <> "" Then
                xml += " SubCtaDe=""" + tabla.Rows(i)(5).ToString.Trim + """"
            End If
            xml += " Nivel=""" + tabla.Rows(i)(3).ToString.Trim + """ Natur=""" + tabla.Rows(i)(4).ToString.Trim + """/>" + vbCrLf
            cadena += "|" + tabla.Rows(i)(0).ToString.Trim + "|" + tabla.Rows(i)(1).ToString.Trim + "|" + tabla.Rows(i)(2).ToString.Trim
            If tabla.Rows(i)(5).ToString <> "" Then
                cadena += "|" + tabla.Rows(i)(5).ToString.Trim
            End If
            cadena += "|" + tabla.Rows(i)(3).ToString.Trim + "|" + tabla.Rows(i)(4).ToString.Trim
        Next
        cadena += "||"
        'aqui ya se tiene la cadena
        Dim Archivos As New dbSucursalesArchivos
        Dim en As New Encriptador
        Dim Sello As String = ""

        While cadena.IndexOf("  ") <> -1
            cadena = Replace(cadena, "  ", " ")
        End While

        Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
        Sello = en.GeneraSello(cadena, Archivos.RutaCer, "2011", False)

        Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)

        xml2 = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
        xml2 += "<catalogocuentas:Catalogo "
        xml2 += "xsi:schemaLocation=""www.sat.gob.mx/esquemas/ContabilidadE/1_1/CatalogoCuentas http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/CatalogoCuentas/CatalogoCuentas_1_1.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:catalogocuentas=""www.sat.gob.mx/esquemas/ContabilidadE/1_1/CatalogoCuentas"""
        xml2 += " Version=""1.1"" RFC=""" + s.RFC + """ Mes=""" + Date.Parse(pFechaI).Month.ToString("00") + """ Anio=""" + Date.Parse(pFechaI).Year.ToString + """"
        ' If Sello <> "" Then
        xml2 += " Sello=""" + Sello + """"
        ' End If
        ' If pNoCertificado <> "" Then
        xml2 += " noCertificado=""" + en.Seriex509 + """"
        ' End If
        ' If pCertificado <> "" Then
        xml2 += " Certificado=""" + en.Certificado64 + """"
        ' End If
        xml2 += ">"
        xml2 += xml
        xml2 += "</catalogocuentas:Catalogo>"
        xmldoc.LoadXml(xml2)

        Return xmldoc
    End Function

    Public Sub separarCuenta(ByVal pCuentaCompleta As String)
        Nivel = 0
        Dim aux As String = ""
        For i As Integer = 0 To pCuentaCompleta.Length - 1
            If pCuentaCompleta.Chars(i) = " " Then
                moviendoCuentas(aux)
                aux = ""
            Else
                aux += pCuentaCompleta.Chars(i)
            End If
        Next
        moviendoCuentas(aux)
        buscarID(N1, N2, N3, N4, N5, Nivel)
        cuentaCompleta = N1.PadLeft(p.NNiv1, "0")
        If Nivel >= 2 Then
            cuentaCompleta += " " + N2.PadLeft(p.NNiv2, "0")
        End If
        If Nivel >= 3 Then
            cuentaCompleta += " " + N3.PadLeft(p.NNiv3, "0")
        End If
        If Nivel >= 4 Then
            cuentaCompleta += " " + N4.PadLeft(p.NNiv4, "0")
        End If
        If Nivel >= 5 Then
            cuentaCompleta += " " + N5.PadLeft(p.NNiv5, "0")
        End If
        If ultimoNivel(N1, N2, N3, N4, N5, Nivel) <> 0 Then
            IDcuenta = 0
        End If

    End Sub
    Public Function ultimoNivel(ByVal pN1 As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pNivel As Integer)
        Dim cont As Integer
        Comm.CommandText = "select count(idCContable) from tblccontables where cuenta='" + pN1.ToString + "'"
        If pNivel >= 2 Then
            Comm.CommandText += " and N2='" + pN2.ToString + "'"
        End If
        If pNivel >= 3 Then
            Comm.CommandText += " and N3='" + pN3.ToString + "'"
        End If
        If pNivel >= 4 Then
            Comm.CommandText += " and N4='" + pN4.ToString + "'"
        End If
        If pNivel >= 5 Then
            Comm.CommandText += " and N5='" + pN5.ToString + "'"
        End If
        Comm.CommandText += " and nivel=" + (pNivel + 1).ToString
        cont = Comm.ExecuteScalar
        Return cont
    End Function

    Public Sub moviendoCuentas(ByVal aux As String)
        If Nivel = 4 Then
            Nivel = 5
            N5 = quitarCeros(aux)
        End If
        If Nivel = 3 Then
            Nivel = 4
            N4 = quitarCeros(aux)
        End If
        If Nivel = 2 Then
            Nivel = 3
            N3 = quitarCeros(aux)
        End If
        If Nivel = 1 Then
            Nivel = 2
            N2 = quitarCeros(aux)
        End If
        If Nivel = 0 Then
            Nivel = 1
            N1 = quitarCeros(aux)
        End If

    End Sub
    Public Function fechaCambio(ByVal pFecha As String) As String
        Return pFecha.Chars(6) + pFecha.Chars(7) + pFecha.Chars(8) + pFecha.Chars(9) + "/" + pFecha.Chars(3) + pFecha.Chars(4) + "/" + pFecha.Chars(0) + pFecha.Chars(1)
    End Function
    Public Function agrupadores(ByVal pNombre As String, ByVal pTipo As String)
        Dim DS As New DataSet
        Comm.CommandText = "select id, codigo as cod, nombre, tipo,if(tipo=subtipo,'',subtipo)as sub, '' as nivel from tblagrupadorcuentas where id<>-1"
        If pNombre <> "" Then
            Comm.CommandText += " and nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        End If
        If pTipo <> "TODOS" Then

            Comm.CommandText += " and tipo='" + pTipo + "'"
        End If
        Comm.CommandText += " ORDER BY id"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        Return DS.Tables("tblbancos").DefaultView
    End Function
    Public Function DaNivel(pidCuenta As Integer) As Integer
        Comm.CommandText = "select ifnull((select nivel from tblccontables where idCContable=" + pidCuenta.ToString + " limit 1),0)"
        Return Comm.ExecuteScalar()
    End Function
    Public Function DaDiot(pidCuenta As Integer) As Integer
        Comm.CommandText = "select ifnull((select diot from tblccontables where idCContable=" + pidCuenta.ToString + " limit 1),0)"
        Return Comm.ExecuteScalar()
    End Function

    Public Function ChecaCuentaSiEsUltimoNivel(ByVal pN1 As String) As Integer
        Comm.CommandText = "select ifnull((select max(nivel) from tblccontables where cuenta='" + pN1.ToString + "'),0)"
        Return Comm.ExecuteScalar
    End Function
End Class
