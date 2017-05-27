Public Class Importador
    Dim ConAdo As Data.OleDb.OleDbConnection
    Dim ConMyslq As MySql.Data.MySqlClient.MySqlConnection
    Dim ComAdo As New Data.OleDb.OleDbCommand
    Dim ComMy As New MySql.Data.MySqlClient.MySqlCommand
    Public Renglon As Integer
    Public Sub New(ByVal pArchivoExcel As String, ByVal pMysqlCon As MySql.Data.MySqlClient.MySqlConnection, ByVal xls2007 As Boolean)
        If xls2007 Then
            ConAdo = New Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""" + pArchivoExcel + """;Extended Properties=""Excel 12.0;HDR=YES;IMEX=1"";")
        Else
            ConAdo = New Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" + pArchivoExcel + """;Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";")
        End If

        ConAdo.Open()
        ConMyslq = pMysqlCon
        ComAdo.Connection = ConAdo
        ComMy.Connection = ConMyslq
    End Sub
    Public Sub New(ByVal pMySqlCon As MySql.Data.MySqlClient.MySqlConnection)
        ConAdo = New Data.OleDb.OleDbConnection()
        ConAdo.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\soco.mdb;"
        ConAdo.Open()
        ComAdo.Connection = ConAdo
    End Sub
    Public Function PasaATexto(ByRef la As Label, ByVal n1 As Integer, ByVal n2 As Integer) As String
        Dim Script As String = ""
        Dim Str As String = ""
        ComAdo.CommandText = "select * from ordendetalle where ordennumero>" + n1.ToString + " and ordennumero<=" + n2.ToString
        Dim dReader As Data.OleDb.OleDbDataReader
        dReader = ComAdo.ExecuteReader
        Renglon = 0
        While dReader.Read
            Script += "insert into ordendetalle(ordennumero,numero,tipoarticulo,descripcion,tipounidad,cantidad,precio,iva,company,provid,categoria,concepto,equipo) " + _
            "values(" + dReader("ordennumero").ToString + "," + dReader("numero").ToString + "," + dReader("numero").ToString + ",'" + Replace(dReader("descripcion"), "'", "''") + "'," + dReader("tipounidad").ToString + _
            "," + dReader("cantidad").ToString + "," + dReader("precio").ToString + "," + dReader("iva").ToString + "," + dReader("company").ToString + "," + dReader("provid").ToString + "," + dReader("categoria").ToString + "," + dReader("concepto").ToString + "," + dReader("equipo").ToString + ");" + vbCrLf
            Str = dReader("ordennumero").ToString
            Renglon += 1
            la.Text = Renglon.ToString
            la.Refresh()
        End While
        dReader.Close()
        Return Script
    End Function
    Public Function ConsultaExcel() As DataView
        'Dim dReader As Data.OleDb.OleDbDataReader
        ComAdo.CommandText = "select * from [Hoja1$]"
        'dReader = ComAdo.ExecuteReader
        Dim DS As New DataSet
        Dim DA As New Data.OleDb.OleDbDataAdapter(ComAdo)
        DA.Fill(DS, "[Hoja1$]")
        Return DS.Tables("[Hoja1$]").DefaultView
    End Function
    Public Sub ImportaProveedores()
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim P As New dbproveedores(ConMyslq)
        ComAdo.CommandText = "select * from [Hoja1$]"
        dReader = ComAdo.ExecuteReader
        While dReader.Read()
            If dReader("codigo") Is DBNull.Value Then
            Else
                P.Guardar(If(dReader("nombre") Is DBNull.Value, "", dReader("nombre")), _
                     If(dReader("direccion") Is DBNull.Value, "", dReader("direccion")), _
                     If(dReader("tel") Is DBNull.Value, "", dReader("tel")), _
                     "", _
                     "", _
                     If(dReader("codigo") Is DBNull.Value, "", dReader("codigo")), _
                     If(dReader("rfc") Is DBNull.Value, "", dReader("rfc")), _
                     "", _
                     If(dReader("ciudad") Is DBNull.Value, "", dReader("ciudad")), _
                     "", _
                    If(dReader("estado") Is DBNull.Value, "", dReader("estado")), _
                     "México", "", "", "", "", "", 0, 0)
            End If

        End While
        dReader.Close()
    End Sub

    Public Sub ImportaClientes()
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim C As New dbClientes(ConMyslq)
        ComAdo.CommandText = "select * from [Hoja1$]"
        dReader = ComAdo.ExecuteReader
        While dReader.Read()
            If dReader("codigo") Is DBNull.Value Then
            Else
                C.Guardar(If(dReader("nombre") Is DBNull.Value, "", dReader("nombre")), _
            If(dReader("domicilio") Is DBNull.Value, "", dReader("domicilio")), _
            If(dReader("tel") Is DBNull.Value, "", dReader("tel")), _
            "", _
            "", _
            If(dReader("codigo") Is DBNull.Value, "", dReader("codigo")), _
            If(dReader("rfc") Is DBNull.Value, "", dReader("rfc")), _
            "", _
            If(dReader("ciudad") Is DBNull.Value, "", dReader("ciudad")), _
            "", _
            If(dReader("estado") Is DBNull.Value, "", dReader("estado")), _
            "", _
            "", _
            "", _
            "", _
            "", _
            "", _
            "", _
            "", _
            "", _
            "", _
            "", _
            "", _
            "", _
            "", _
            "", _
            "", _
            0, 0, 0, 0, 0, 0, 1)
            End If

        End While
        dReader.Close()
    End Sub

    Public Sub ImportaArticulos()
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim C As New dbInventario(ConMyslq)
        Dim Clas As String
        Dim ClasParte As String
        Dim Car As String
        Dim Clasid1 As Integer
        Dim Clasid2 As Integer
        Dim Clasid3 As Integer
        Dim Cont As Integer
        Dim Cl As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        Dim Cl2 As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
        Dim Cl3 As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
        ComAdo.CommandText = "select * from [Hoja1$]"
        dReader = ComAdo.ExecuteReader
        While dReader.Read()

            If dReader("Codigo") Is DBNull.Value Then
            Else
                Clas = dReader("clas")
                Clasid1 = 1
                Clasid2 = 1
                Clasid3 = 1
                Cont = 0
                ClasParte = ""
                While Cont < Clas.Length
                    Car = Clas.Substring(Cont, 1)
                    If Car <> " " Then
                        ClasParte += Car
                    Else
                        If Clasid1 = 1 Then
                            If Cl.BuscaClasificacion(ClasParte) Then
                                Clasid1 = Cl.ID
                                ClasParte = ""
                            End If
                        Else
                            If Clasid2 = 1 Then
                                If Cl2.BuscaClasificacion(ClasParte, Clasid1) Then
                                    Clasid2 = Cl2.ID
                                    ClasParte = ""
                                End If
                            Else
                                If Clasid3 = 1 Then
                                    If Cl3.BuscaClasificacion(ClasParte, Clasid2) Then
                                        Clasid3 = Cl3.ID
                                        ClasParte = ""
                                    End If
                                End If
                            End If
                        End If
                    End If
                    Cont += 1
                End While

                If Clasid1 = 1 Then
                    If Cl.BuscaClasificacion(ClasParte) Then
                        Clasid1 = Cl.ID
                        ClasParte = ""
                    End If
                Else
                    If Clasid2 = 1 Then
                        If Cl2.BuscaClasificacion(ClasParte, Clasid1) Then
                            Clasid2 = Cl2.ID
                            ClasParte = ""
                        End If
                    Else
                        If Clasid3 = 1 Then
                            If Cl3.BuscaClasificacion(ClasParte, Clasid2) Then
                                Clasid3 = Cl3.ID
                                ClasParte = ""
                            End If
                        End If
                    End If
                End If

                C.Guardar(If(dReader("descripcion") Is DBNull.Value, "", dReader("descripcion")), _
                          0, 1, 1, 1, _
                           Clasid1, "", _
                           If(dReader("Codigo") Is DBNull.Value, "", dReader("Codigo")), _
                        0, 0, 2, "", Clasid2, Clasid3, 0, 1, _
                16, 0, "", "", 0)
                Dim P As New dbInventarioPrecios(ConMyslq)
                P.Guardar(dReader("precio"), C.ID, 1, "", 2)
                'If(dReader("IVA") Is DBNull.Value, 16, dReader("IVA")), 0)


            End If

            'C.Guardar(If(dReader("nombre") Is DBNull.Value, "", dReader("nombre")), _
            'If(dReader("Direccion1") Is DBNull.Value, "", dReader("Direccion1")), _
            'If(dReader("Tel1") Is DBNull.Value, "", dReader("Tel1")), _
            '"", _
            'If(dReader("persona") Is DBNull.Value, "", dReader("persona")), _
            'If(dReader("codigo") Is DBNull.Value, "", dReader("codigo")), _
            'If(dReader("R_F_C") Is DBNull.Value, "", dReader("R_F_C")), _
            '"", _
            'If(dReader("Ciudad") Is DBNull.Value, "", dReader("Ciudad")), _
            '"", _
            '"", _
            '"", _
            '"", _
            '"", _
            '"", _
            '"", _
            '"", _
            '"", _
            '"", _
            'If(dReader("Direccion2") Is DBNull.Value, "", dReader("Direccion2")), _
            '"", _
            '"", _
            '"", _
            '"", _
            '"", _
            '"", _
            '"", _
            '0, 0, 0, 0)
        End While
        dReader.Close()
    End Sub
    Public Sub CierraConexiones()
        ConAdo.Close()
    End Sub
    Public Sub IngresaInventario(ByVal pTabla As String, ByVal pidAlmacen As Integer)
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim C As New dbInventario(ConMyslq)
        Dim CS As New dbInventarioSeries(MySqlcon)
        Dim IdAlmacen As Integer
        Dim IdInventario As Integer
        Dim Contador As Integer = 0
        ComAdo.CommandText = "select * from [" + pTabla + "]"
        dReader = ComAdo.ExecuteReader
        IdAlmacen = pidAlmacen
        Try
            While dReader.Read
                If dReader("CODIGO") Is DBNull.Value Then
                    If (dReader("SERIES") Is DBNull.Value) = False And IdInventario <> 0 Then
                        If CS.ChecaNoSerieRepetido(dReader("SERIES"), IdInventario) = False Then
                            ' CS.Guardar(IdInventario, dReader("SERIES"), Format(Date.Now, "yyyy/MM/dd"), Format(Date.Now, "yyyy/MM/dd"), 0, 0, 0, 0, 0)
                        End If
                    End If
                Else
                    If C.BuscaArticulo(dReader("CODIGO"), 0) = False Then
                        C.Guardar( _
If(dReader("NOMBRE") Is DBNull.Value, "", dReader("NOMBRE")) _
, 0, 3, 1, 3, 2, "", _
If(dReader("CODIGO") Is DBNull.Value, "", dReader("CODIGO")) _
, 0, dReader("COSTO"), 2, "", 1, 1, _
If(dReader("SERIES") Is DBNull.Value, 0, 1) _
, 1, 16, 0, "", "", 0)
                        IdInventario = C.ID
                        C.ActualizaInventario(IdInventario, dReader("EXIST"), IdAlmacen)
                        Contador += 1
                        If dReader("SERIES") Is DBNull.Value Then
                        Else
                            If CS.ChecaNoSerieRepetido(dReader("SERIES"), IdInventario) = False Then
                                ' CS.Guardar(IdInventario, dReader("SERIES"), Format(Date.Now, "yyyy/MM/dd"), Format(Date.Now, "yyyy/MM/dd"), 0, 0, 0, 0, 0)
                            End If
                        End If
                    Else
                        IdInventario = C.ID
                    End If
                End If
            End While
            dReader.Close()
        Catch ex As Exception
            MsgBox(ex.Message + " Cont=" + Contador.ToString)
        End Try

    End Sub
End Class
