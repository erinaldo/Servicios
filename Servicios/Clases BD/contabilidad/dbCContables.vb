Imports MySql.Data.MySqlClient
Public Class dbCContables
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public C1 As String
    Public CComp As String
    Public C2 As String
    Public C3 As String
    Public C4 As String
    Public C5 As String
    Public D1 As String
    Public ID As Integer
    Public T1 As String
    Public T2 As String
    Public T3 As String
    Public T4 As String
    Public T5 As String
    Public N1 As String
    Public N2 As String
    Public N3 As String
    Public N4 As String
    Public N5 As String
    Public Nivel As String
    Public Niv1 As String
    Public Niv2 As String
    Public Niv3 As String
    Public Niv4 As String
    Public Niv5 As String
    Public fecha As String
    Public Descontinuada As String
    Public IdCuentaMayor As Integer
    Public IdCuentaAnt As Integer
    'Public ID As Integer
    Public auxiliar As String
 
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        C1 = 0
        Comm.Connection = Conexion
    End Sub

    
    Public Function esRepetida(ByVal x As Integer) As Integer
        Dim Resultado1 As Integer = 0
        Dim Resultado As Integer = x
        'checar si esta usandose
        Comm.CommandText = "select count(Cuenta) from tblccontables where Cuenta=" + Resultado.ToString()
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function
    Public Function esRepetidaDescripcion(ByVal x As String) As Integer
        Dim Resultado1 As Integer = 0
        Dim Resultado As String = x
        'checar si esta usandose
        Comm.CommandText = "select count(Descripcion) from tblccontables where Descripcion='" + Replace(Resultado, "'", "''") + "'"
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Sub Guardar(ByVal pCodigo As String, ByVal pNombre As String, ByVal pTipo As String, ByVal pNaturaleza As String)
        'Guardar nivel 1
        C1 = pCodigo
        D1 = pNombre
        Dim nivel As String = "1"
        Comm.CommandText = "insert into tblccontables (Cuenta,Descripcion,Nivel,Tipo,Naturaleza,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,descontinuada) values('" + Replace(C1, "'", "''") + "','" + Replace(D1, "'", "''") + "','" + Replace(nivel, "'", "''") + "','" + Replace(pTipo, "'", "''") + "','" + Replace(pNaturaleza, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "','')"
        Comm.ExecuteNonQuery()
        CuentaComple(1, C1)

    End Sub
 
    Public Sub Modificar(ByVal pCodigo As String, ByVal pNombre As String, ByVal pTipo As String, ByVal pNaturaleza As String)
        'Modificar Nivel 1
        ' Dim nivel As String = "1"
        C1 = pCodigo
        D1 = pNombre
        Comm.CommandText = "update tblccontables set Cuenta='" + Replace(C1, "'", "''") + "',Descripcion='" + Replace(D1, "'", "''") + "',Tipo='" + Replace(pTipo, "'", "''") + "', Naturaleza='" + Replace(pNaturaleza, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio=" + TimeOfDay.ToString("HH:mm:ss") + "' where Cuenta='" + C1 + "' AND Nivel='1'"
        Comm.ExecuteNonQuery()
        CuentaComple(1, C1)
    End Sub

    Public Sub Eliminar(ByVal pFolio As String)
        C1 = pFolio
        Comm.CommandText = "delete from tblccontables where Cuenta=" + C1.ToString
        Comm.ExecuteNonQuery()
    End Sub


    Public Sub New(ByVal pDescripcion As String, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        D1 = pDescripcion
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where Descripcion='" + Replace(D1, "'", "''") + "'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T1 = DReader("Tipo")
            C1 = DReader("Cuenta")
            D1 = DReader("Descripcion")
            N1 = DReader("Naturaleza")
            'Tipo = DReader("Tipo")
            ' IdSucursal = DReader("idsucursal")
        End If
        DReader.Close()
    End Sub


    '***************************************************************************
    '*******************************Nivel 2*************************************
    '***************************************************************************

    Public Function FolioN2(ByVal pC1 As String) As String
        Dim Resultado As Integer = 0
        Dim repetida As Integer = 0
        Dim Rep As String = ""
        C1 = pC1

        ' Comm.CommandText = "select count(N2) from tblccontables where Cuenta='" + Replace(C1, "'", "''") + "'"
        ' Comm.ExecuteNonQuery()
        Resultado = 1
        Rep = Format(Resultado, "00000")
        repetida = esRepetida2(Rep, C1)

        If repetida > 0 Then 'es repetida

            Do While repetida > 0
                Resultado = Integer.Parse(Rep)
                Resultado = Resultado + 1
                Rep = Format(Resultado, "00000")
                repetida = esRepetida2(Rep, C1)

            Loop
            Return Format(Resultado, "00000")
        Else
            Return Format(Resultado, "00000")
        End If
    End Function


    Public Function esRepetida2(ByVal x As String, ByVal pC1 As String) As Integer
        Dim Resultado1 As Integer = 0
        Dim Resultado As String = x
        C1 = pC1
        'checar si esta usandose
        Comm.CommandText = "select count(Cuenta) from tblccontables where N2='" + Replace(Resultado, "'", "''") + "' AND Cuenta='" + Replace(C1, "'", "''") + "'"
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Sub Guardar2(ByVal pCodigo As String, ByVal pC2 As String, ByVal pNombre As String, ByVal pTipo As String, ByVal pNaturaleza As String)
        'Guardar nivel 2
        C1 = pCodigo
        D1 = pNombre
        C2 = pC2
        Dim nivel As String = "2"
        Comm.CommandText = "insert into tblccontables (Cuenta,N2,Descripcion,Nivel,Tipo,Naturaleza) values('" + Replace(C1, "'", "''") + "','" + Replace(C2, "'", "''") + "','" + Replace(D1, "'", "''") + "','" + Replace(nivel, "'", "''") + "','" + Replace(pTipo, "'", "''") + "','" + Replace(pNaturaleza, "'", "''") + "')"
        Comm.ExecuteNonQuery()
        CuentaComple(2, C1, C2)

    End Sub
    Public Sub Modificar2(ByVal pN2 As String, ByVal PN1 As String, ByVal pDes As String, ByVal pTipo As String, ByVal pNaturaleza As String)
        'Modificar Nivel 1
        C1 = PN1
        C2 = pN2
        D1 = pDes
        Comm.CommandText = "update tblccontables set Descripcion='" + Replace(D1, "'", "''") + "', Tipo='" + pTipo + "', Naturaleza='" + Replace(pNaturaleza, "'", "''") + "' where N2='" + Replace(C2, "'", "''") + "' AND Cuenta='" + Replace(C1, "'", "''") + "' AND Nivel='2'"
        Comm.ExecuteNonQuery()
        CuentaComple(2, C1, C2)
    End Sub

    Public Sub LlenarCombos2(ByVal Tabla As String, ByRef ComboaLlenar As ComboBox, ByVal CampoN As String, ByVal AliasN As String, ByVal CampoID As String, ByRef ListaId As elemento, Optional ByVal ValorWhere As String = "", Optional ByVal VAnd As String = "", Optional ByVal ValorAnd As String = "")
        'Dim SQLDataR As SqlClient.SqlDataReader
        ComboaLlenar.Items.Clear()
        ListaId.Limpiar()
        ' If PrimerValor <> "" Then
        'ComboaLlenar.Items.Add(PrimerValor)
        '   ListaId.Agregar(-2)
        '  End If
        ' If OrderBy = "" Then
        'OrderBy = CampoN
        'End If
        If ValorWhere = "" Then
            ' MySqlcomInt.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " order by " + OrderBy
        Else
            MySqlcomInt.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " where " + ValorWhere + " And " + VAnd + " ='" + ValorAnd + "'"
        End If
        MySqlDReader = MySqlcomInt.ExecuteReader
        While MySqlDReader.Read
            ComboaLlenar.Items.Add(MySqlDReader.Item(AliasN))
            ListaId.Agregar(MySqlDReader.Item(CampoID))
        End While
        MySqlDReader.Close()
        If ComboaLlenar.DropDownStyle = ComboBoxStyle.DropDown Then
            ComboaLlenar.Text = ""
        Else
            If ComboaLlenar.Items.Count > 0 Then
                ComboaLlenar.SelectedIndex = 0 'tenia 0
            End If
        End If
    End Sub


    Public Sub SeleccionarN2(ByVal pDescripcion As String, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        D1 = pDescripcion
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where Descripcion='" + Replace(D1, "'", "''") + "'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T2 = DReader("Tipo")
            C2 = DReader("N2")
            D1 = DReader("Descripcion")
            N2 = DReader("Naturaleza")
            'Tipo = DReader("Tipo")
            ' IdSucursal = DReader("idsucursal")
        End If
        DReader.Close()
    End Sub
    Public Sub Eliminar2(ByVal pC1 As String, ByVal pC2 As String)
        C1 = pC1
        C2 = pC2
        Comm.CommandText = "delete from tblccontables where Cuenta=" + C1.ToString + " AND N2= " + C2.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function hayN2(ByVal x As String) As Integer
        Dim nivel As Integer = 2
        C1 = x
        Dim Resultado1 = 0
        'checar si esta usandose
        Comm.CommandText = "select count(Descripcion) from tblccontables where Cuenta=" + C1.ToString + " AND Nivel= " + nivel.ToString
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    '***************************************************************************
    '*******************************Nivel 3*************************************
    '***************************************************************************

    Public Sub Guardar3(ByVal pCodigo As String, ByVal pC2 As String, ByVal pC3 As String, ByVal pNombre As String, ByVal pTipo As String, ByVal pNaturaleza As String)
        'Guardar nivel 2
        C1 = pCodigo
        D1 = pNombre
        C2 = pC2
        C3 = pC3
        Dim nivel As String = "3"
        Comm.CommandText = "insert into tblccontables (Cuenta,N2,N3,Descripcion,Nivel,Tipo,Naturaleza) values('" + Replace(C1, "'", "''") + "','" + Replace(C2, "'", "''") + "','" + Replace(C3, "'", "''") + "','" + Replace(D1, "'", "''") + "','" + Replace(nivel, "'", "''") + "','" + Replace(pTipo, "'", "''") + "','" + Replace(pNaturaleza, "'", "''") + "')"
        Comm.ExecuteNonQuery()
        CuentaComple(3, C1, C2, C3)
    End Sub

    Public Function FolioN3(ByVal pC1 As String, ByVal pC2 As String) As String
        Dim Resultado As Integer = 0
        Dim repetida As Integer = 0
        Dim Rep As String = ""
        C1 = pC1
        C2 = pC2
        Resultado = 1
        Rep = Format(Resultado, "00000")
        repetida = esRepetida3(Rep, C1, C2)

        If repetida > 0 Then 'es repetida

            Do While repetida > 0
                Resultado = Integer.Parse(Rep)
                Resultado = Resultado + 1
                Rep = Format(Resultado, "00000")
                repetida = esRepetida3(Rep, C1, C2)

            Loop
            Return Format(Resultado, "00000")
        Else
            Return Format(Resultado, "00000")
        End If
    End Function

    Public Function esRepetida3(ByVal x As String, ByVal pC1 As String, ByVal pC2 As String) As Integer
        Dim Resultado1 As Integer = 0
        Dim Resultado As String = x
        C1 = pC1
        C2 = pC2
        'checar si esta usandose
        Comm.CommandText = "select count(Cuenta) from tblccontables where N3='" + Replace(Resultado, "'", "''") + "' AND Cuenta='" + Replace(C1, "'", "''") + "' AND N2='" + Replace(C2, "'", "''") + "'"
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Sub Modificar3(ByVal pN2 As String, ByVal PN1 As String, ByVal PN3 As String, ByVal pDes As String, ByVal pTipo As String, ByVal pNaturaleza As String)
        'Modificar Nivel 1
        C1 = PN1
        C2 = pN2
        D1 = pDes
        C3 = PN3
        Comm.CommandText = "update tblccontables set Descripcion='" + Replace(D1, "'", "''") + "', Tipo='" + Replace(pTipo, "'", "''") + "', Naturaleza='" + Replace(pNaturaleza, "'", "''") + "' where N2='" + Replace(C2, "'", "''") + "' AND Cuenta='" + Replace(C1, "'", "''") + "'AND N3='" + Replace(C3, "'", "''") + "' AND Nivel='3'"
        Comm.ExecuteNonQuery()
        CuentaComple(3, C1, C2, C3)
    End Sub

    Public Function hayN3(ByVal x As String, ByVal pC2 As String) As Integer
        Dim nivel As Integer = 2
        C1 = x
        C2 = pC2
        Dim Resultado1 = 0
        'checar si esta usandose
        Comm.CommandText = "select count(Descripcion) from tblccontables where N2='" + Replace(C2, "'", "''") + "' AND Cuenta='" + Replace(C1, "'", "''") + "' AND Nivel='3'"
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Sub LlenarCombos3(ByVal Tabla As String, ByRef ComboaLlenar As ComboBox, ByVal CampoN As String, ByVal AliasN As String, ByVal CampoID As String, ByRef ListaId As elemento, Optional ByVal ValorWhere As String = "", Optional ByVal VAnd As String = "", Optional ByVal ValorAnd As String = "", Optional ByVal VAnd2 As String = "", Optional ByVal ValorAnd2 As String = "")
        'Dim SQLDataR As SqlClient.SqlDataReader
        ComboaLlenar.Items.Clear()
        ListaId.Limpiar()

        If ValorWhere = "" Then
            ' MySqlcomInt.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " order by " + OrderBy
        Else
            MySqlcomInt.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " where " + ValorWhere + " And " + VAnd + " ='" + ValorAnd + "'" + " And " + VAnd2 + " ='" + ValorAnd2 + "'"
        End If
        MySqlDReader = MySqlcomInt.ExecuteReader
        While MySqlDReader.Read
            ComboaLlenar.Items.Add(MySqlDReader.Item(AliasN))
            ListaId.Agregar(MySqlDReader.Item(CampoID))
        End While
        MySqlDReader.Close()
        If ComboaLlenar.DropDownStyle = ComboBoxStyle.DropDown Then
            ComboaLlenar.Text = ""
        Else
            If ComboaLlenar.Items.Count > 0 Then
                ComboaLlenar.SelectedIndex = 0 'tenia 0
            End If
        End If
    End Sub

    Public Sub SeleccionarN3(ByVal pDescripcion As String, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        D1 = pDescripcion
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where Descripcion='" + Replace(D1, "'", "''") + "' And Nivel='3'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T3 = DReader("Tipo")
            C3 = DReader("N3")
            D1 = DReader("Descripcion")
            N3 = DReader("Naturaleza")
            'Tipo = DReader("Tipo")
            ' IdSucursal = DReader("idsucursal")
        End If
        DReader.Close()
    End Sub


    Public Sub Eliminar3(ByVal pC1 As String, ByVal pC2 As String, ByVal pC3 As String)
        C1 = pC1
        C2 = pC2
        C3 = pC3
        Comm.CommandText = "delete from tblccontables where Cuenta='" + C1.ToString + "' AND N2= '" + C2.ToString + "' And N3='" + C3 + "'"
        Comm.ExecuteNonQuery()
    End Sub

    '***************************************************************************
    '*******************************Nivel 4*************************************
    '***************************************************************************



    Public Function FolioN4(ByVal pC1 As String, ByVal pC2 As String, ByVal pC3 As String) As String
        Dim Resultado As Integer = 0
        Dim repetida As Integer = 0
        Dim Rep As String = ""
        C1 = pC1
        C2 = pC2
        C3 = pC3
        Resultado = 1
        Rep = Format(Resultado, "00000")
        repetida = esRepetida4(Rep, C1, C2, C3)

        If repetida > 0 Then 'es repetida

            Do While repetida > 0
                Resultado = Integer.Parse(Rep)
                Resultado = Resultado + 1
                Rep = Format(Resultado, "00000")
                repetida = esRepetida4(Rep, C1, C2, C3)

            Loop
            Return Format(Resultado, "00000")
        Else
            Return Format(Resultado, "00000")
        End If
    End Function

    Public Function esRepetida4(ByVal x As String, ByVal pC1 As String, ByVal pC2 As String, ByVal PC3 As String) As Integer
        Dim Resultado1 As Integer = 0
        Dim Resultado As String = x
        C1 = pC1
        C2 = pC2
        C3 = PC3
        'checar si esta usandose
        Comm.CommandText = "select count(Cuenta) from tblccontables where N4='" + Replace(Resultado, "'", "''") + "' AND Cuenta='" + Replace(C1, "'", "''") + "' AND N2='" + Replace(C2, "'", "''") + "' AND N3='" + Replace(C3, "'", "''") + "'"
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function


    Public Function hayN4(ByVal x As String, ByVal pC2 As String, ByVal pC3 As String) As Integer

        C1 = x
        C2 = pC2
        C3 = pC3
        Dim Resultado1 = 0
        'checar si esta usandose
        Comm.CommandText = "select count(Descripcion) from tblccontables where N2='" + Replace(C2, "'", "''") + "' AND Cuenta='" + Replace(C1, "'", "''") + "' AND N3='" + Replace(C3, "'", "''") + "' AND Nivel='4'"
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Sub LlenarCombos4(ByVal Tabla As String, ByRef ComboaLlenar As ComboBox, ByVal CampoN As String, ByVal AliasN As String, ByVal CampoID As String, ByRef ListaId As elemento, Optional ByVal ValorWhere As String = "", Optional ByVal VAnd As String = "", Optional ByVal ValorAnd As String = "", Optional ByVal VAnd2 As String = "", Optional ByVal ValorAnd2 As String = "", Optional ByVal VAnd3 As String = "", Optional ByVal ValorAnd3 As String = "")
        'Dim SQLDataR As SqlClient.SqlDataReader
        ComboaLlenar.Items.Clear()
        ListaId.Limpiar()

        If ValorWhere = "" Then
            ' MySqlcomInt.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " order by " + OrderBy
        Else
            MySqlcomInt.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " where " + ValorWhere + " And " + VAnd + " ='" + ValorAnd + "'" + " And " + VAnd2 + " ='" + ValorAnd2 + "'" + " And " + VAnd3 + " ='" + ValorAnd3 + "'"
        End If
        MySqlDReader = MySqlcomInt.ExecuteReader
        While MySqlDReader.Read
            ComboaLlenar.Items.Add(MySqlDReader.Item(AliasN))
            ListaId.Agregar(MySqlDReader.Item(CampoID))
        End While
        MySqlDReader.Close()
        If ComboaLlenar.DropDownStyle = ComboBoxStyle.DropDown Then
            ComboaLlenar.Text = ""
        Else
            If ComboaLlenar.Items.Count > 0 Then
                ComboaLlenar.SelectedIndex = 0 'tenia 0
            End If
        End If
    End Sub

    Public Sub Guardar4(ByVal pCodigo As String, ByVal pC2 As String, ByVal pC3 As String, ByVal pC4 As String, ByVal pNombre As String, ByVal pTipo As String, ByVal pNaturaleza As String)
        'Guardar nivel 2
        C1 = pCodigo
        D1 = pNombre
        C2 = pC2
        C3 = pC3
        C4 = pC4
        Dim nivel As String = "4"
        Comm.CommandText = "insert into tblccontables (Cuenta,N2,N3,N4,Descripcion,Nivel,Tipo,Naturaleza) values('" + Replace(C1, "'", "''") + "','" + Replace(C2, "'", "''") + "','" + Replace(C3, "'", "''") + "','" + Replace(C4, "'", "''") + "','" + Replace(D1, "'", "''") + "','" + Replace(nivel, "'", "''") + "','" + Replace(pTipo, "'", "''") + "','" + Replace(pNaturaleza, "'", "''") + "')"
        Comm.ExecuteNonQuery()
        CuentaComple(4, C1, C2, C3, C4)
    End Sub

    Public Sub Modificar4(ByVal pN2 As String, ByVal PN1 As String, ByVal PN3 As String, ByVal PN4 As String, ByVal pDes As String, ByVal pTipo As String, ByVal pNaturaleza As String)
        'Modificar Nivel4
        C1 = PN1
        C2 = pN2
        D1 = pDes
        C3 = PN3
        C4 = PN4
        Comm.CommandText = "update tblccontables set Descripcion='" + Replace(D1, "'", "''") + "', Tipo='" + Replace(pTipo, "'", "''") + "', Naturaleza='" + Replace(pNaturaleza, "'", "''") + "' where N2='" + Replace(C2, "'", "''") + "' AND Cuenta='" + Replace(C1, "'", "''") + "'AND N3='" + Replace(C3, "'", "''") + "'AND N4='" + Replace(C4, "'", "''") + "' AND Nivel='4'"
        Comm.ExecuteNonQuery()
        CuentaComple(4, C1, C2, C3, C4)
    End Sub

    Public Sub SeleccionarN4(ByVal pDescripcion As String, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        D1 = pDescripcion
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where Descripcion='" + Replace(D1, "'", "''") + "' And Nivel='4'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T4 = DReader("Tipo")
            C4 = DReader("N4")
            D1 = DReader("Descripcion")
            N4 = DReader("Naturaleza")
            'Tipo = DReader("Tipo")
            ' IdSucursal = DReader("idsucursal")
        End If
        DReader.Close()
    End Sub

    Public Sub Eliminar4(ByVal pC1 As String, ByVal pC2 As String, ByVal pC3 As String, ByVal pC4 As String)
        C1 = pC1
        C2 = pC2
        C3 = pC3
        C4 = pC4
        Comm.CommandText = "delete from tblccontables where Cuenta='" + C1.ToString + "' AND N2= '" + C2.ToString + "' And N3='" + C3.ToString + "' And N4='" + C4.ToString + "'"
        Comm.ExecuteNonQuery()
    End Sub

    '***********************
    Public Sub CuentaComple(ByVal Nivel As Integer, ByVal pC1 As String, Optional ByVal pC2 As String = "", Optional ByVal pC3 As String = "", Optional ByVal pC4 As String = "", Optional ByVal pC5 As String = "")
        'Modificar Nivel4
        C1 = pC1
        C2 = pC2
        C3 = pC3
        C4 = pC4
        C5 = pC5
        Dim CuentaC As String = ""
        If Nivel = 1 Then
            Comm.CommandText = "update tblccontables set CuentaComp='" + Replace(C1, "'", "''") + "' where Cuenta='" + Replace(C1, "'", "''") + "' AND Nivel='1'"
        End If
        If Nivel = 2 Then
            CuentaC = C1
            CuentaC += " "
            CuentaC += C2
            Comm.CommandText = "update tblccontables set CuentaComp='" + Replace(CuentaC, "'", "''") + "' where Cuenta='" + Replace(C1, "'", "''") + "' AND N2='" + Replace(C2, "'", "''") + "' AND Nivel='2'"
        End If

        If Nivel = 3 Then
            CuentaC = C1
            CuentaC += " "
            CuentaC += C2
            CuentaC += " "
            CuentaC += C3
            Comm.CommandText = "update tblccontables set CuentaComp='" + Replace(CuentaC, "'", "''") + "' where Cuenta='" + Replace(C1, "'", "''") + "' AND N2='" + Replace(C2, "'", "''") + "' AND N3='" + Replace(C3, "'", "''") + "' AND Nivel='3'"
        End If

        If Nivel = 4 Then
            CuentaC = C1
            CuentaC += " "
            CuentaC += C2
            CuentaC += " "
            CuentaC += C3
            CuentaC += " "
            CuentaC += C4
            Comm.CommandText = "update tblccontables set CuentaComp='" + Replace(CuentaC, "'", "''") + "' where Cuenta='" + Replace(C1, "'", "''") + "' AND N2='" + Replace(C2, "'", "''") + "' AND N3='" + Replace(C3, "'", "''") + "' AND N4='" + Replace(C4, "'", "''") + "' AND Nivel='4'"
        End If
        If Nivel = 5 Then
            CuentaC = C1
            CuentaC += " "
            CuentaC += C2
            CuentaC += " "
            CuentaC += C3
            CuentaC += " "
            CuentaC += C4
            CuentaC += " "
            CuentaC += C5
            Comm.CommandText = "update tblccontables set CuentaComp='" + Replace(CuentaC, "'", "''") + "' where Cuenta='" + Replace(C1, "'", "''") + "' AND N2='" + Replace(C2, "'", "''") + "' AND N3='" + Replace(C3, "'", "''") + "' AND N4='" + Replace(C4, "'", "''") + "' AND N5='" + Replace(C5, "'", "''") + "' AND Nivel='5'"
        End If
        Comm.ExecuteNonQuery()
    End Sub

    Public Function reporte() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idCContable,Cuenta,Descripcion,Nivel,CASE tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'EGRESOS' WHEN 5 THEN 'ORDEN' END, case Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end from tblccontables where Nivel='1' ORDER BY Cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblbancos").DefaultView
    End Function

    Public Function subCuentas(ByVal id As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idCContable,Concat(Cuenta,' ',N2),Descripcion,Nivel,N2,CASE tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'EGRESOS' WHEN 5 THEN 'ORDEN' END, case Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end from tblccontables where Cuenta='" + id.ToString + "' AND Nivel='2' ORDER BY Cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblbancos").DefaultView
    End Function

    Public Function subCuentas3(ByVal id As String, ByVal pC2 As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idCContable,Concat(Cuenta,' ',N2,' ',N3),Descripcion,Nivel,N3,CASE tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'EGRESOS' WHEN 5 THEN 'ORDEN' END, case Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end from tblccontables where Cuenta='" + id.ToString + "' AND Nivel='3' AND N2='" + pC2.ToString + "' ORDER BY Cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblbancos").DefaultView
    End Function

    Public Function subCuentas4(ByVal id As String, ByVal pC2 As String, ByVal pC3 As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idCContable,Concat(Cuenta,' ',N2,' ',N3,' ',N4),Descripcion,Nivel,CASE tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'EGRESOS' WHEN 5 THEN 'ORDEN' END, case Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end,N4 from tblccontables where Cuenta='" + id.ToString + "' AND Nivel='4' AND N2='" + pC2.ToString + "'AND N3='" + pC3.ToString + "' ORDER BY Cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblbancos").DefaultView
    End Function

    
    Public Function subCuentas5(ByVal id As String, ByVal pC2 As String, ByVal pC3 As String, ByVal pC4 As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idCContable,Concat(Cuenta,' ',N2,' ',N3,' ',N4,' ',N5),Descripcion,Nivel,CASE tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'EGRESOS' WHEN 5 THEN 'ORDEN' END, case Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end from tblccontables where Cuenta='" + id.ToString + "' AND Nivel='5' AND N2='" + pC2.ToString + "'AND N3='" + pC3.ToString + "'AND N4='" + pC4.ToString + "' ORDER BY Cuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblbancos").DefaultView
    End Function
    Public Function ConCuenta(Optional ByVal pCuenta As String = "", Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idCContable,Concat(Cuenta,' ',N2,' ',N3,' ',N4,' ',N5),Descripcion,Nivel,CASE tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'EGRESOS' WHEN 5 THEN 'ORDEN' END, case Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end from tblccontables where idCContable>0"
        If pCuenta <> "" Then
            Comm.CommandText += " and Concat(Cuenta,' ',N2,' ',N3,' ',N4,' ',N5) like '%" + Replace(pCuenta, "'", "''") + "%' "
        End If
        If pCuenta <> "" Then
            Comm.CommandText += " and Descripcion like '%" + Replace(pNombre, "'", "''") + "%' "
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        Return DS.Tables("tblbancos").DefaultView
    End Function
    Public Function ConDes(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idCContable,Concat(Cuenta,' ',N2,' ',N3,' ',N4,' ',N5),Descripcion,Nivel,CASE tipo WHEN 0 THEN 'ACTÍVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'EGRESOS' WHEN 5 THEN 'ORDEN' END, case Naturaleza when 0 then 'DEUDORA' when 1 then 'ACREEDORA' end from tblccontables where Descripcion like '%" + Replace(pNombre, "'", "''") + "%' "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        Return DS.Tables("tblbancos").DefaultView
    End Function
    Public Sub Buscar1(ByVal pID As Integer)
        ID = pID

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where idCContable=" + ID.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T1 = DReader("Tipo")
            C1 = DReader("Cuenta")
            D1 = DReader("Descripcion")
            N1 = DReader("Naturaleza")
        End If
        DReader.Close()
    End Sub

    Public Sub Buscar2(ByVal pID As String)
        ' ID = pID

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where idCContable=" + pID.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T3 = DReader("Tipo")
            C1 = DReader("Cuenta")
            D1 = DReader("Descripcion")
            C2 = DReader("N2")
            CComp = DReader("CuentaComp")
            N2 = DReader("Naturaleza")
        End If
        DReader.Close()
    End Sub

    Public Sub BuscarDes(ByVal pC1 As String, ByVal niv As String)
        C1 = pC1

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where Cuenta='" + C1.ToString() + "' And Nivel='" + niv + "'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T1 = DReader("Tipo")
            N1 = DReader("Naturaleza")
            auxiliar = DReader("Descripcion")

        End If
        DReader.Close()
    End Sub

    Public Sub Buscar3(ByVal pID As String)
        ' ID = pID

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where idCContable=" + pID.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T3 = DReader("Tipo")
            C1 = DReader("Cuenta")
            D1 = DReader("Descripcion")
            C2 = DReader("N2")
            C3 = DReader("N3")
            N3 = DReader("Naturaleza")
            CComp = DReader("CuentaComp")
        End If
        DReader.Close()
    End Sub

    Public Sub BuscarDes2(ByVal pC1 As String, ByVal niv As String, ByVal pC2 As String)
        C1 = pC1
        C2 = pC2
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where Cuenta='" + C1.ToString() + "' And Nivel='" + niv + "' And N2='" + C2 + "'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T2 = DReader("Tipo")
            N2 = DReader("Naturaleza")
            auxiliar = DReader("Descripcion")

        End If
        DReader.Close()
    End Sub

    Public Sub BuscarDes3(ByVal pC1 As String, ByVal niv As String, ByVal pC2 As String, ByVal pC3 As String)
        C1 = pC1
        C2 = pC2
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where Cuenta='" + C1.ToString() + "' And Nivel='" + niv + "' And N2='" + C2 + "' And N3='" + pC3 + "'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T3 = DReader("Tipo")
            N3 = DReader("Naturaleza")
            auxiliar = DReader("Descripcion")

        End If
        DReader.Close()
    End Sub

    Public Sub BuscarDes4(ByVal pC1 As String, ByVal niv As String, ByVal pC2 As String, ByVal pC3 As String, ByVal pC4 As String)
        C1 = pC1
        C2 = pC2
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where Cuenta='" + C1.ToString() + "' And Nivel='" + niv + "' And N2='" + C2 + "' And N3='" + pC3 + "' And N4='" + pC4 + "'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T4 = DReader("Tipo")
            N4 = DReader("Naturaleza")
            auxiliar = DReader("Descripcion")

        End If
        DReader.Close()
    End Sub

    Public Sub Buscar4(ByVal pID As String)
        ' ID = pID

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where idCContable=" + pID.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T4 = DReader("Tipo")
            C1 = DReader("Cuenta")
            D1 = DReader("Descripcion")
            C2 = DReader("N2")
            C3 = DReader("N3")
            C4 = DReader("N4")
            N4 = DReader("Naturaleza")
            CComp = DReader("CuentaComp")
        End If
        DReader.Close()
    End Sub
    '******************************************************************************************
    '****************************Nivel 5*******************************************************
    '*****************************************************************************************
    Public Function FolioN5(ByVal pC1 As String, ByVal pC2 As String, ByVal pC3 As String, ByVal pC4 As String) As String
        Dim Resultado As Integer = 0
        Dim repetida As Integer = 0
        Dim Rep As String = ""
        C1 = pC1
        C2 = pC2
        C3 = pC3
        C4 = pC4
        Resultado = 1
        Rep = Format(Resultado, "00000")
        repetida = esRepetida5(Rep, C1, C2, C3, C4)

        If repetida > 0 Then 'es repetida

            Do While repetida > 0
                Resultado = Integer.Parse(Rep)
                Resultado = Resultado + 1
                Rep = Format(Resultado, "00000")
                repetida = esRepetida5(Rep, C1, C2, C3, C4)

            Loop
            Return Format(Resultado, "00000")
        Else
            Return Format(Resultado, "00000")
        End If
    End Function

    Public Function esRepetida5(ByVal x As String, ByVal pC1 As String, ByVal pC2 As String, ByVal PC3 As String, ByVal pC4 As String) As Integer
        Dim Resultado1 As Integer = 0
        Dim Resultado As String = x
        C1 = pC1
        C2 = pC2
        C3 = PC3
        C4 = pC4

        'checar si esta usandose
        Comm.CommandText = "select count(Cuenta) from tblccontables where N5='" + Replace(Resultado, "'", "''") + "' AND Cuenta='" + Replace(C1, "'", "''") + "' AND N4='" + Replace(C4, "'", "''") + "' AND N2='" + Replace(C2, "'", "''") + "' AND N3='" + Replace(C3, "'", "''") + "'"
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function
    Public Function hayN5(ByVal x As String, ByVal pC2 As String, ByVal pC3 As String, ByVal pC4 As String) As Integer

        C1 = x
        C2 = pC2
        C3 = pC3
        C4 = pC4
        Dim Resultado1 = 0
        'checar si esta usandose
        Comm.CommandText = "select count(Descripcion) from tblccontables where N2='" + Replace(C2, "'", "''") + "' AND Cuenta='" + Replace(C1, "'", "''") + "' AND N3='" + Replace(C3, "'", "''") + "' AND N4='" + Replace(C4, "'", "''") + "' AND Nivel='5'"
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Sub Guardar5(ByVal pCodigo As String, ByVal pC2 As String, ByVal pC3 As String, ByVal pC4 As String, ByVal pC5 As String, ByVal pNombre As String, ByVal pTipo As String, ByVal pNaturaleza As String)
        'Guardar nivel 2
        C1 = pCodigo
        D1 = pNombre
        C2 = pC2
        C3 = pC3
        C4 = pC4
        C5 = pC5
        Dim nivel As String = "5"
        Comm.CommandText = "insert into tblccontables (Cuenta,N2,N3,N4,N5,Descripcion,Nivel,Tipo,Naturaleza) values('" + Replace(C1, "'", "''") + "','" + Replace(C2, "'", "''") + "','" + Replace(C3, "'", "''") + "','" + Replace(C4, "'", "''") + "','" + Replace(C5, "'", "''") + "','" + Replace(D1, "'", "''") + "','" + Replace(nivel, "'", "''") + "','" + Replace(pTipo, "'", "''") + "','" + Replace(pNaturaleza, "'", "''") + "')"
        Comm.ExecuteNonQuery()
        CuentaComple(5, C1, C2, C3, C4, C5)
    End Sub

    Public Sub Modificar5(ByVal pN2 As String, ByVal PN1 As String, ByVal PN3 As String, ByVal PN4 As String, ByVal pN5 As String, ByVal pDes As String, ByVal pTipo As String, ByVal pNaturaleza As String)
        'Modificar Nivel4
        C1 = PN1
        C2 = pN2
        D1 = pDes
        C3 = PN3
        C4 = PN4
        C5 = pN5
        Comm.CommandText = "update tblccontables set Descripcion='" + Replace(D1, "'", "''") + "', Tipo='" + Replace(pTipo, "'", "''") + "', Naturaleza='" + Replace(pNaturaleza, "'", "''") + "' where N2='" + Replace(C2, "'", "''") + "' AND Cuenta='" + Replace(C1, "'", "''") + "'AND N3='" + Replace(C3, "'", "''") + "'AND N4='" + Replace(C4, "'", "''") + "'AND N5='" + Replace(pN5, "'", "''") + "' AND Nivel='5'"
        Comm.ExecuteNonQuery()
        CuentaComple(5, C1, C2, C3, C4, C5)
    End Sub
    Public Sub LlenarCombos5(ByVal Tabla As String, ByRef ComboaLlenar As ComboBox, ByVal CampoN As String, ByVal AliasN As String, ByVal CampoID As String, ByRef ListaId As elemento, Optional ByVal ValorWhere As String = "", Optional ByVal VAnd As String = "", Optional ByVal ValorAnd As String = "", Optional ByVal VAnd2 As String = "", Optional ByVal ValorAnd2 As String = "", Optional ByVal VAnd3 As String = "", Optional ByVal ValorAnd3 As String = "", Optional ByVal VAnd4 As String = "", Optional ByVal ValorAnd4 As String = "")
        'Dim SQLDataR As SqlClient.SqlDataReader
        ComboaLlenar.Items.Clear()
        ListaId.Limpiar()

        If ValorWhere = "" Then
            ' MySqlcomInt.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " order by " + OrderBy
        Else
            MySqlcomInt.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " where " + ValorWhere + " And " + VAnd + " ='" + ValorAnd + "'" + " And " + VAnd2 + " ='" + ValorAnd2 + "'" + " And " + VAnd3 + " ='" + ValorAnd3 + "'" + " And " + VAnd4 + " ='" + ValorAnd4 + "'"
        End If
        MySqlDReader = MySqlcomInt.ExecuteReader
        While MySqlDReader.Read
            ComboaLlenar.Items.Add(MySqlDReader.Item(AliasN))
            ListaId.Agregar(MySqlDReader.Item(CampoID))
        End While
        MySqlDReader.Close()
        If ComboaLlenar.DropDownStyle = ComboBoxStyle.DropDown Then
            ComboaLlenar.Text = ""
        Else
            If ComboaLlenar.Items.Count > 0 Then
                ComboaLlenar.SelectedIndex = 0 'tenia 0
            End If
        End If
    End Sub
    Public Sub Eliminar5(ByVal pC1 As String, ByVal pC2 As String, ByVal pC3 As String, ByVal pC4 As String, ByVal pC5 As String)
        C1 = pC1
        C2 = pC2
        C3 = pC3
        C4 = pC4
        C5 = pC5
        Comm.CommandText = "delete from tblccontables where Cuenta='" + C1.ToString + "' AND N2= '" + C2.ToString + "' And N3='" + C3.ToString + "' And N4='" + C4.ToString + "'" + " And N5='" + C5.ToString + "'"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub SeleccionarN5(ByVal pDescripcion As String, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        D1 = pDescripcion
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where Descripcion='" + Replace(D1, "'", "''") + "' And Nivel='5'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T5 = DReader("Tipo")
            C5 = DReader("N3")
            D1 = DReader("Descripcion")
            N5 = DReader("Naturaleza")
            'Tipo = DReader("Tipo")
            ' IdSucursal = DReader("idsucursal")
        End If
        DReader.Close()
    End Sub
    Public Sub Buscar5(ByVal pID As String)
        ' ID = pID

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where idCContable=" + pID.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            T5 = DReader("Tipo")
            C1 = DReader("Cuenta")
            D1 = DReader("Descripcion")
            C2 = DReader("N2")
            C3 = DReader("N3")
            C4 = DReader("N4")
            C5 = DReader("N5")
            N5 = DReader("Naturaleza")
            CComp = DReader("CuentaComp")
        End If
        DReader.Close()
    End Sub

    Public Function UtlimoNivel(ByVal pID As String, ByVal pNivel As String) As Integer
        Dim Resultado As Integer
        If pNivel = 1 Then
            Comm.CommandText = "select Cuenta from tblccontables where idCContable=" + pID.ToString()
            C1 = Comm.ExecuteScalar
            Comm.CommandText = "select Descripcion from tblccontables where idCContable=" + pID.ToString()
            D1 = Comm.ExecuteScalar
            Comm.CommandText = "select * from tblccontables where Nivel=2 and Cuenta='" + C1.ToString() + "'"
            Resultado = Comm.ExecuteScalar
        End If
        If pNivel = 2 Then
            Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
            Comm.CommandText = "select * from tblccontables where idCContable=" + pID.ToString()
            DReader = Comm.ExecuteReader
            If DReader.Read() Then
                C1 = DReader("Cuenta")
                C2 = DReader("N2")
                D1 = DReader("Descripcion")
            End If
            DReader.Close()
            Comm.CommandText = "select * from tblccontables where Nivel=3 and Cuenta='" + C1.ToString() + "'AND N2='" + C2.ToString() + "'"
            Resultado = Comm.ExecuteScalar
        End If
        If pNivel = 3 Then
            Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
            Comm.CommandText = "select * from tblccontables where idCContable=" + pID.ToString()
            DReader = Comm.ExecuteReader
            If DReader.Read() Then
                C1 = DReader("Cuenta")
                C2 = DReader("N2")
                C3 = DReader("N3")
                D1 = DReader("Descripcion")
            End If
            DReader.Close()
            Comm.CommandText = "select * from tblccontables where Nivel=4 and Cuenta='" + C1.ToString() + "'AND N2='" + C2.ToString() + "'AND N3='" + C3.ToString() + "'"
            Resultado = Comm.ExecuteScalar
        End If
        If pNivel = 4 Then
            Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
            Comm.CommandText = "select * from tblccontables where idCContable=" + pID.ToString()
            DReader = Comm.ExecuteReader
            If DReader.Read() Then
                C1 = DReader("Cuenta")
                C2 = DReader("N2")
                C3 = DReader("N3")
                C4 = DReader("N4")
                D1 = DReader("Descripcion")
            End If
            DReader.Close()
            Comm.CommandText = "select * from tblccontables where Nivel=5 and Cuenta='" + C1.ToString() + "'AND N2='" + C2.ToString() + "'AND N3='" + C3.ToString() + "'AND N4='" + C4.ToString() + "'"
            Resultado = Comm.ExecuteScalar
        End If
        If pNivel = 5 Then
            Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
            Comm.CommandText = "select * from tblccontables where idCContable=" + pID.ToString()
            DReader = Comm.ExecuteReader
            If DReader.Read() Then
                C1 = DReader("Cuenta")
                C2 = DReader("N2")
                C3 = DReader("N3")
                C4 = DReader("N4")
                C5 = DReader("N5")
                D1 = DReader("Descripcion")
            End If
            DReader.Close()
            Resultado = 0
        End If
        Return Resultado
    End Function
    Public Function Cuenta(ByVal pID As String, ByVal pNivel As Integer) As String
        Dim Resultado As String

        Comm.CommandText = "select concat(cuenta"
        If pNivel >= 2 Then
            Comm.CommandText += ",' ',N2 "
        End If
        If pNivel >= 3 Then
            Comm.CommandText += ",' ',N3 "
        End If
        If pNivel >= 4 Then
            Comm.CommandText += ",' ',N4 "
        End If
        If pNivel >= 5 Then
            Comm.CommandText += ",' ',N5 "
        End If
        Comm.CommandText += ") from tblccontables where idCContable=" + pID.ToString()
        Resultado = Comm.ExecuteScalar


        Return Resultado
    End Function

    Public Function existeCuenta(ByVal pCuenta As String) As Integer
        Dim Resultado As Integer

        Comm.CommandText = "select count(Cuenta) from tblccontables where CuentaComp='" + pCuenta.ToString() + "'"
        Resultado = Comm.ExecuteScalar


        Return Resultado
    End Function

    Public Sub PreUltimo(ByVal pCuenta As String)
        ' Dim Resultado As Integer

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where Cuentacomp='" + pCuenta.ToString() + "'"
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nivel = DReader("Nivel")
            ID = DReader("idCContable")
        End If
        DReader.Close()
    End Sub
    Public Sub idCuenta(ByVal pN1 As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pNivel As Integer)
        Comm.CommandText = "select ifnull(idCContable,-1) from tblccontables where cuenta='" + pN1.ToString() + "'"
        If pNivel > 1 Then
            'nivel 2
            Comm.CommandText += " and N2='" + pN2.ToString + "'"
        End If
        If pNivel > 2 Then
            'nivel 3
            Comm.CommandText += " and N3='" + pN3.ToString + "'"
        End If
        If pNivel > 3 Then
            'nivel 4
            Comm.CommandText += " and N4='" + pN4.ToString + "'"
        End If
        If pNivel > 4 Then
            'nivel 5
            Comm.CommandText += " and N5='" + pN5.ToString + "'"
        End If
        Comm.CommandText += " and Nivel=" + pNivel.ToString
        ID = Comm.ExecuteScalar
        If ID <> 0 Then
            Buscar(ID)
        End If
    End Sub
    Public Sub Buscar(ByVal pID As Integer)
        ID = pID

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblccontables where idCContable=" + ID.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Niv1 = DReader("Cuenta")
            Niv2 = DReader("N2")
            Niv3 = DReader("N3")
            Niv4 = DReader("N4")
            Niv5 = DReader("N5")
            T1 = DReader("Tipo")
            D1 = DReader("Descripcion")
            N1 = DReader("Naturaleza")
            fecha = DReader("fecha")
        End If
        DReader.Close()
    End Sub
End Class

