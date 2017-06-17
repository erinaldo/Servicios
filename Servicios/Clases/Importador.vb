Public Class Importador
    Dim ConAdo As Data.OleDb.OleDbConnection
    Dim ConMyslq As MySql.Data.MySqlClient.MySqlConnection
    Dim ComAdo As New Data.OleDb.OleDbCommand
    Dim ComMy As New MySql.Data.MySqlClient.MySqlCommand
    Public Renglon As Integer
    Public Fallos As String
    Public Property RenglonCont() As Integer
        Get
            Return Renglon
        End Get
        Set(value As Integer)
            Renglon = value
            RaiseEvent RenglonLeido()
        End Set
    End Property
    Public Event RenglonLeido()
    Public Sub New(ByVal pArchivoExcel As String, ByVal pMysqlCon As MySql.Data.MySqlClient.MySqlConnection)
        If IO.Path.GetExtension(pArchivoExcel).ToLower = ".xlsx" Then
            ConAdo = New Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""" + pArchivoExcel + """;Extended Properties=""Excel 12.0;HDR=YES;IMEX=1"";")
        ElseIf IO.Path.GetExtension(pArchivoExcel).ToLower = ".xls" Then
            ConAdo = New Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" + pArchivoExcel + """;Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";")
        Else
            Throw New Exception("Tipo de archivo no válido.")
        End If
        ConAdo.Open()
        ConMyslq = pMysqlCon
        ComAdo.Connection = ConAdo
        ComMy.Connection = ConMyslq
    End Sub
    'Public Sub New(ByVal pMySqlCon As MySql.Data.MySqlClient.MySqlConnection)
    '    ConAdo = New Data.OleDb.OleDbConnection()
    '    ConAdo.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\soco.mdb;"
    '    ConAdo.Open()
    '    ComAdo.Connection = ConAdo
    'End Sub
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
    Public Function FraccionArancelScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("clave") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tblfraccionarancelaria(clave,descripcion,unidadmedida) values('" + dReader("clave") + "','" + If(dReader("desc") Is DBNull.Value, "", Replace(dReader("desc"), "'", "''")) + "','" + If(dReader("medida") Is DBNull.Value, "", Replace(Format(dReader("medida"), "00"), "'", "''")) + "');" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Function CPsScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("cp") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tblcodigospostales(codigopostal,estado,municipio,localidad) values('" + dReader("cp") + "','" + If(dReader("estado") Is DBNull.Value, "", Replace(dReader("estado"), "'", "''")) + "','" + If(dReader("mun") Is DBNull.Value, "", Replace(dReader("mun"), "'", "''")) + "','" + If(dReader("loc") Is DBNull.Value, "", Replace(dReader("loc"), "'", "''")) + "');" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Function UsosScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("clave") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tblusoscfdi(clave,descripcion,fisica,moral) values('" + dReader("clave") + "','" + If(dReader("descripcion") Is DBNull.Value, "", Replace(dReader("descripcion"), "'", "''")) + "','" + If(dReader("fisica") Is DBNull.Value, "", Replace(dReader("fisica"), "'", "''")) + "','" + If(dReader("moral") Is DBNull.Value, "", Replace(dReader("moral"), "'", "''")) + "');" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Function pedimentoaduanaScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("aduana") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tblpedimentoaduana(aduana,patente,ejercicio,cantidad) values('" + CInt(dReader("aduana")).ToString("00") + "','" + If(dReader("patente") Is DBNull.Value, "", Replace(dReader("patente"), "'", "''")) + "','" + If(dReader("ejercicio") Is DBNull.Value, "", Replace(dReader("ejercicio").ToString, "'", "''")) + "','" + If(dReader("cantidad") Is DBNull.Value, "", Replace(dReader("cantidad"), "'", "''")) + "');" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Function LocalidadesScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("loc") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tbllocalidades(clave,estado,nombre) values('" + dReader("loc") + "','" + If(dReader("estado") Is DBNull.Value, "", Replace(dReader("estado"), "'", "''")) + "','" + If(dReader("descripcion") Is DBNull.Value, "", Replace(dReader("descripcion"), "'", "''")) + "');" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Function EstadosScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("clave") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tblestados(clave,pais,nombre) values('" + dReader("clave") + "','" + If(dReader("pais") Is DBNull.Value, "", Replace(dReader("pais"), "'", "''")) + "','" + If(dReader("nombre") Is DBNull.Value, "", Replace(dReader("nombre"), "'", "''")) + "');" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Function AduanasScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("clave") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tblclavesprodserv(clave,descripcion) values('" + Format(CInt(dReader("clave")), "00") + "','" + If(dReader("nombre") Is DBNull.Value, "", Replace(dReader("nombre"), "'", "''")) + "');" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Function PaisScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("clave") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tblpaises(clave,descripcion) values('" + dReader("clave") + "','" + If(dReader("descripcion") Is DBNull.Value, "", Replace(dReader("descripcion"), "'", "''")) + "');" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Function MonedasScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("clave") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tblmonedas(clave,descripcion,decimales) values('" + dReader("clave") + "','" + If(dReader("descripcion") Is DBNull.Value, "", Replace(dReader("descripcion"), "'", "''")) + "',2);" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Function UnidadesScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("clave") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tblunidadesmedida(clave,nombre,descripcion,simbolo) values('" + dReader("clave") + "','" + If(dReader("nombre") Is DBNull.Value, "", Replace(dReader("nombre"), "'", "''")) + "','" + If(dReader("descripcion") Is DBNull.Value, "", Replace(dReader("descripcion"), "'", "''")) + "','" + If(dReader("simbolo") Is DBNull.Value, "", Replace(dReader("simbolo"), "'", "''")) + "');" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Function MunicipiosScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("clave") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tblmunicipios(clave,estado,nombre) values('" + dReader("clave") + "','" + If(dReader("estado") Is DBNull.Value, "", Replace(dReader("estado"), "'", "''")) + "','" + If(dReader("descripcion") Is DBNull.Value, "", Replace(dReader("descripcion"), "'", "''")) + "');" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Function ColoniasScrip() As String
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Scrip As String = ""
        ComAdo.CommandText = "select * from [Hoja1$]"
        RenglonCont = 0
        'dReader = ComAdo.ExecuteReader
        dReader = ComAdo.ExecuteReader
        While dReader.Read
            If dReader("clave") IsNot DBNull.Value Then
                Scrip += "insert into db_catalogossat.tblcolonias(clave,codigopostal,nombre) values('" + dReader("clave") + "','" + If(dReader("cp") Is DBNull.Value, "", Replace(dReader("cp"), "'", "''")) + "','" + If(dReader("nombre") Is DBNull.Value, "", Replace(dReader("nombre"), "'", "''")) + "');" + vbCrLf
                RenglonCont += 1
                My.Application.DoEvents()
            End If
        End While
        Return Scrip
    End Function
    Public Sub ImportaProveedores()
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim P As New dbproveedores(ConMyslq)
        RenglonCont = 0
        ComAdo.CommandText = "select * from [PROVEEDORES$]"
        dReader = ComAdo.ExecuteReader
        While dReader.Read()
            If dReader("codigo") IsNot DBNull.Value Then
                P.Guardar(If(dReader("nombre") Is DBNull.Value, "", dReader("nombre")), _
                    If(dReader("direccion") Is DBNull.Value, "", dReader("direccion")), _
                    If(dReader("tel") Is DBNull.Value, "", dReader("tel")), "", "", _
                    If(dReader("codigo") Is DBNull.Value, "", dReader("codigo")), _
                    If(dReader("rfc") Is DBNull.Value, "", dReader("rfc")), "", _
                    If(dReader("ciudad") Is DBNull.Value, "", dReader("ciudad")), _
                    If(dReader("cp") Is DBNull.Value, "", dReader("cp")), _
                    If(dReader("estado") Is DBNull.Value, "", dReader("estado")), _
                    If(dReader("pais") Is DBNull.Value, "México", dReader("pais")), _
                    If(dReader("noexterior") Is DBNull.Value, "", dReader("noexterior")), _
                    If(dReader("nointerior") Is DBNull.Value, "", dReader("nointerior")), _
                    If(dReader("colonia") Is DBNull.Value, "", dReader("colonia")), _
                    If(dReader("municipio") Is DBNull.Value, "", dReader("municipio")), _
                    If(dReader("referencia") Is DBNull.Value, "", dReader("referencia")), _
                    If(dReader("diascredito") Is DBNull.Value, 0, dReader("diascredito")), _
                    If(dReader("limitecredito") Is DBNull.Value, 0, dReader("limitecredito")), _
                    If(dReader("curp") Is DBNull.Value, "", dReader("curp")), _
                    If(dReader("tipo") Is DBNull.Value, 0, dReader("tipo")), 0, "", 0, 0, 1, 0, 0, 0, 0)
                RenglonCont += 1
                My.Application.DoEvents()
                End If
        End While
        dReader.Close()
    End Sub

    Public Sub ImportaClientes()
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim C As New dbClientes(ConMyslq)
        Dim Ref As String
        RenglonCont = 0
        ComAdo.CommandText = "select * from [CLIENTES$]"
        dReader = ComAdo.ExecuteReader
        While dReader.Read()
            If dReader("codigo") IsNot DBNull.Value Then
                Ref = If(dReader("referencia") Is DBNull.Value, "", dReader("referencia"))
                C.Guardar(If(dReader("razonsocial") Is DBNull.Value, "", dReader("razonsocial").ToString.Trim), _
                If(dReader("Calle") Is DBNull.Value, "", dReader("Calle")), _
                If(dReader("telefono") Is DBNull.Value, "", dReader("telefono")), _
                If(dReader("email") Is DBNull.Value, "", dReader("email")), _
                If(dReader("contacto") Is DBNull.Value, "", dReader("contacto")), _
                dReader("codigo"), _
                If(dReader("rfc") Is DBNull.Value, "XAXX010101000", Replace(Replace(Trim(dReader("rfc")), "-", ""), " ", "")), _
                If(dReader("Giro") Is DBNull.Value, "", dReader("Giro")), _
                If(dReader("ciudad") Is DBNull.Value, "", dReader("ciudad")), _
                If(dReader("cp") Is DBNull.Value, "", dReader("cp")), _
                If(dReader("estado") Is DBNull.Value, "", dReader("estado")), _
                "México", "", "", "", "", "", _
                If(dReader("Num") Is DBNull.Value, "", dReader("Num")), _
                If(dReader("Int") Is DBNull.Value, "", dReader("Int")), _
                If(dReader("Colonia") Is DBNull.Value, "", dReader("Colonia")), _
                If(dReader("municipio") Is DBNull.Value, "", dReader("municipio")), _
                Ref, "", "", "", "", "", 0, _
                0, _
                If(dReader("CREDITO") Is DBNull.Value, 0, dReader("CREDITO")), 0, 0, 0, 1, 1, "", 0, 0, 0, "", "", "", 0, 0, "0", "0", "", 0, "", 0, 1, 0, 0, 0, 1, 1, 0, "", "", "", "", "", "", "G01")
                RenglonCont += 1
                My.Application.DoEvents()
                'If cuentaR >= 200 Then
                'MsgBox("Van 200")
                'cuentaR = 0
                'End If
            End If
        End While
        dReader.Close()
    End Sub

    Public Sub ImportaDocumentosClientes()
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim CodigoAnt As String = ""
        RenglonCont = 0
        Dim CD As New dbCapturaDocumentosClientes(ConMyslq)
        ComAdo.CommandText = "select * from [DOCCLIENTES$]"
        dReader = ComAdo.ExecuteReader
        Try
            While dReader.Read()
                If dReader("codigo") IsNot DBNull.Value Then
                    If dReader("codigo") <> CodigoAnt Then
                        CD.Cliente.BuscaCliente(dReader("codigo"))
                    End If
                    If (dReader("folio") Is DBNull.Value) = False Then
                        If IsNumeric(dReader("folio")) Then
                            If CD.Cliente.ID > 0 Then
                                CD.Guardar(CD.Cliente.ID,
                                           If(dReader("fecha") Is DBNull.Value, Date.Now.ToString("yyyy/MM/dd"), CDate(dReader("fecha")).ToString("yyyy/MM/dd")),
                                           3,
                                           If(dReader("importe") Is DBNull.Value, 0, dReader("importe")),
                                           GlobalIdSucursalDefault, 0, 2,
                                            dReader("folio"),
                                           If(dReader("serie") Is DBNull.Value, "", dReader("serie")), 2, 1)
                            End If
                        End If
                    End If
                    CodigoAnt = dReader("codigo")
                    RenglonCont += 1
                    My.Application.DoEvents()
                End If
            End While
            dReader.Close()
        Catch ex As Exception
            MySqlcom.Transaction.Rollback()
            Throw New Exception(ex.Message + vbNewLine + "Registro: " + RenglonCont.ToString)
        Finally
            If Not dReader.IsClosed Then dReader.Close()
        End Try
    End Sub

    Public Sub ImportaDocumentosProv()
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim CodigoAnt As String = ""
        RenglonCont = 0
        Dim CD As New dbCapturaDocumentosProveedores(ConMyslq)
        ComAdo.CommandText = "select * from [DOCPROVEEDORES$]"
        dReader = ComAdo.ExecuteReader
        Try
            While dReader.Read()
                If dReader("codigo") IsNot DBNull.Value Then
                    If dReader("codigo") <> CodigoAnt Then
                        CD.Proveedor.BuscaProveedor(dReader("codigo"))
                    End If
                    If (dReader("folio") Is DBNull.Value) = False Then
                        If IsNumeric(dReader("folio")) Then
                            If CD.Proveedor.ID > 0 Then
                                CD.Guardar(CD.Proveedor.ID,
                                           If(dReader("fecha") Is DBNull.Value, Date.Now.ToString("yyyy/MM/dd"), CDate(dReader("fecha")).ToString("yyyy/MM/dd")),
                                           3,
                                           If(dReader("importe") Is DBNull.Value, 0, dReader("importe")),
                                           GlobalIdSucursalDefault, 0, 2,
                                            dReader("folio"),
                                           If(dReader("serie") Is DBNull.Value, "", dReader("serie")), 2, 1, "", 16, 0, 0)
                            End If
                        End If
                    End If
                    CodigoAnt = dReader("codigo")
                    RenglonCont += 1
                    My.Application.DoEvents()
                End If
            End While
            dReader.Close()
        Catch ex As Exception
            MySqlcom.Transaction.Rollback()
            Throw New Exception(ex.Message + vbNewLine + "Registro: " + RenglonCont.ToString)
        Finally
            If Not dReader.IsClosed Then dReader.Close()
        End Try
    End Sub

    Public Sub ImportaArticulos(ByVal pAgregarInvInicial As Boolean, ByVal pidSucursal As Integer, ByVal pIdAlmacen As Integer, ByVal pIdConcepto As Integer, pCalculaExCost As Boolean)
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim C As New dbInventario(ConMyslq)
        Dim Clasid1 As Integer
        Dim Clasid2 As Integer
        Dim Clasid3 As Integer
        Dim IdUnidad As Integer
        Dim codigoclas1 As Integer = 0
        Dim codigoclas2 As Integer = 0
        Dim codigoclas3 As Integer = 0
        Dim codigoart As Integer = 1

        Dim Precio As Double
        Dim Precio2 As Double
        Dim Precio3 As Double
        Dim precio4 As Double
        Dim precio5 As Double
        Dim Codigo2 As String
        Dim Folio As Integer = 0
        Dim op As New dbOpciones(MySqlcon)
        Dim Mov As New dbMovimientos(MySqlcon)
        Dim MovD As New dbMovimientosDetalles(MySqlcon)
        Dim Concepto As New dbInventarioConceptos(pIdConcepto, MySqlcon)
        If pAgregarInvInicial Then
            Folio = Mov.DaNuevoFolio(Concepto.Serie, pidSucursal, pIdConcepto)
        End If
        RenglonCont = 0
        ComAdo.CommandText = "select * from [CATALOGO$]"
        dReader = ComAdo.ExecuteReader
        MySqlcom.Transaction = MySqlcon.BeginTransaction
        Try
            While dReader.Read()
                If dReader("descripcion") IsNot DBNull.Value Then
                    If dReader("descripcion") <> "" Then
                        If dReader("clas1") Is DBNull.Value Then
                            Clasid1 = 1
                        Else
                            If dReader("clas1") = "" Then
                                Clasid1 = 1
                            Else
                                MySqlcom.CommandText = "select ifnull(idclasificacion,0) from tblinventarioclasificaciones where nombre='" + Replace(dReader("clas1"), "'", "''") + "';"
                                Clasid1 = MySqlcom.ExecuteScalar
                            End If

                        End If
                        'Throw New Exception("Indique un clasificación.")
                        'checa si existe la clasificacion, si no establece el id a 0

                        'si el id es 0 inserta la clasificacion
                        If Clasid1 = 0 Then
                            'busca el siguiente codigo
                            MySqlcom.CommandText = "select max(ifnull(codigo,0))+1 from tblinventarioclasificaciones;"
                            codigoclas1 = MySqlcom.ExecuteScalar
                            MySqlcom.CommandText = "insert into tblinventarioclasificaciones (codigo,nombre,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio)values(lpad(" + codigoclas1.ToString + ",3,'0'),'" + Replace(dReader("clas1"), "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
                            MySqlcom.CommandText = Replace(MySqlcom.CommandText, "|", "")
                            MySqlcom.ExecuteNonQuery()
                            MySqlcom.CommandText = "select max(idclasificacion) from tblinventarioclasificaciones;"
                            'obtiene el i'd
                            Clasid1 = MySqlcom.ExecuteScalar
                        End If

                        Clasid2 = 1
                        ''si hay clasificacion una descripcion de la clasificacion
                        If dReader("clas2") IsNot DBNull.Value AndAlso dReader("clas2") <> "" Then
                            'se busca si esta dada de alta
                            MySqlcom.CommandText = "select ifnull(idclasificacion,0) from tblinventarioclasificaciones2 where nombre='" + Replace(dReader("clas2"), "'", "''") + "' and idnivelsuperior=" + Clasid1.ToString + ";"
                            Clasid2 = MySqlcom.ExecuteScalar
                            If Clasid2 = 0 Then
                                'si no esta dada de alta se da de alta
                                'obtiene el siguiente id del nivel de la subclas del nivel superior correspondiente
                                MySqlcom.CommandText = "select ifnull(max(codigo),0)+1 from tblinventarioclasificaciones2 where idnivelsuperior=" + Clasid1.ToString + ";"
                                codigoclas2 = MySqlcom.ExecuteScalar
                                MySqlcom.CommandText = "insert into tblinventarioclasificaciones2 (codigo,nombre,idnivelsuperior,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio)values(lpad(" + codigoclas2.ToString + ",3,'0'),'" + Replace(dReader("clas2"), "'", "''") + "'," + Clasid1.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
                                MySqlcom.CommandText = Replace(MySqlcom.CommandText, "|", "")
                                MySqlcom.ExecuteNonQuery()
                                MySqlcom.CommandText = "select max(idclasificacion) from tblinventarioclasificaciones2 where idnivelsuperior=" + Clasid1.ToString + ";"
                                Clasid2 = MySqlcom.ExecuteScalar
                            End If
                        End If

                        Clasid3 = 1
                        'si hay clasificacion una descripcion de la clasificacion
                        If dReader("clas3") IsNot DBNull.Value AndAlso dReader("clas3") <> "" Then
                            'se busca si esta dada de alta
                            MySqlcom.CommandText = "select ifnull(idclasificacion,0) from tblinventarioclasificaciones3 where nombre='" + Replace(dReader("clas3"), "'", "''") + "' and idnivelsuperior=" + Clasid2.ToString + ";"
                            Clasid3 = MySqlcom.ExecuteScalar
                            If Clasid3 = 0 Then
                                'si no esta dada de alta se da de alta
                                'obtiene el siguiente id del nivel de la subclas del nivel superior correspondiente
                                MySqlcom.CommandText = "select ifnull(max(codigo),0)+1 from tblinventarioclasificaciones3 where idnivelsuperior=" + Clasid2.ToString + ";"
                                codigoclas3 = MySqlcom.ExecuteScalar
                                MySqlcom.CommandText = "insert into tblinventarioclasificaciones3 (codigo,nombre,idnivelsuperior,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio)values(lpad(" + codigoclas3.ToString + ",3,'0'),'" + Replace(dReader("clas3"), "'", "''") + "'," + Clasid2.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
                                MySqlcom.CommandText = Replace(MySqlcom.CommandText, "|", "")
                                MySqlcom.ExecuteNonQuery()
                                MySqlcom.CommandText = "select max(idclasificacion) from tblinventarioclasificaciones3 where idnivelsuperior=" + Clasid2.ToString + ";"
                                Clasid3 = MySqlcom.ExecuteScalar
                            End If
                        End If
                        If dReader("unidad") IsNot DBNull.Value Then
                            MySqlcom.CommandText = "select ifnull(idtipocantidad,0) from tbltiposcantidades where nombre='" + Replace(dReader("unidad"), "'", "''") + "';"
                            IdUnidad = MySqlcom.ExecuteScalar
                            If IdUnidad = 0 Then
                                MySqlcom.CommandText = "insert into tbltiposcantidades(nombre,abreviatura,usabascula,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,pararemision) values('" + Replace(dReader("unidad"), "'", "''") + "','" + Replace(dReader("unidad"), "'", "''") + "',0," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "',0);"
                                MySqlcom.CommandText = Replace(MySqlcom.CommandText, "|", "")
                                MySqlcom.ExecuteNonQuery()
                                MySqlcom.CommandText = "select max(idtipocantidad) from tbltiposcantidades;"
                                IdUnidad = MySqlcom.ExecuteScalar
                            End If
                        Else
                            IdUnidad = 7
                        End If
                        If dReader("IVA") IsNot DBNull.Value Then
                            If IsNumeric(dReader("IVA")) Then
                                C.Iva = CDbl(dReader("IVA"))
                            Else
                                C.Iva = 16
                            End If
                        Else
                            C.Iva = 16
                        End If
                        If dReader("IEPS") IsNot DBNull.Value Then
                            If IsNumeric(dReader("IEPS")) Then
                                C.ieps = CDbl(dReader("IEPS"))
                            Else
                                C.ieps = 0
                            End If
                        Else
                            C.ieps = 0
                        End If
                        'inserta el articulo

                        If dReader("codigo") Is DBNull.Value Then
                            'sin codigo
                            'MySqlcom.CommandText = "insert into tblinventario(nombre,cantidad,tipocantidad,contenido,tipocontenido,descripcion,clave,idclasificacion,puntodereorden,costobase,idmonedacostobase,noparte,idclasificacion2,idclasificacion3,manejaseries,idcontrol,inventariable,iva,retieneiva,clave2,fabricante,precioneto) values('" + Replace(dReader("descripcion"), "'", "''") + "',0,1,1,1,'',lpad(" + codigoart.ToString + ",3,'0')," + Clasid1.ToString + ",0,0,2,''," + Clasid2.ToString + "," + Clasid3.ToString + ",0,0,1,16,0,'','" + Replace(If(dReader("Fabricante") Is DBNull.Value, "", dReader("Fabricante")), "'", "''") + "',1);insert into tblinventarioprecios(idinventario,precio,esdefault,comentario,idmoneda)values((select max(idinventario) from tblinventario)," + If(dReader("Precio") Is DBNull.Value, 0, dReader("Precio")).ToString + ",1,'',2);"
                            'MySqlcom.ExecuteNonQuery()
                            'codigoart += 1
                        Else
                            'con codigo
                            If C.DaIdArticulo(dReader("codigo")) = False Then
                                If dReader("ubicacion") IsNot DBNull.Value Then
                                    C.Ubicacion = dReader("ubicacion")
                                Else
                                    C.Ubicacion = ""
                                End If
                                If dReader("codigo2") Is DBNull.Value Then
                                    Codigo2 = ""
                                Else
                                    Codigo2 = dReader("codigo2")
                                    If C.DaIdArticulo(dReader("codigo2")) = True Then
                                        Codigo2 = ""
                                    End If
                                    If dReader("codigo") = dReader("codigo2") Then
                                        Codigo2 = ""
                                    End If
                                End If
                                C.Guardar(dReader("descripcion"), 0, IdUnidad, 1, IdUnidad, Clasid1, "", dReader("codigo"), 0, 0, 2, If(dReader("noparte") Is DBNull.Value, "", dReader("noparte")), Clasid2, Clasid3, 0, 1, C.Iva, 0, Codigo2, "", 0, C.Ubicacion, 0, 0, 0, 0, 0, 0, 0, 0, C.ieps, 0, "", 0, 0, 0, 0, "", "06 PIEZA", "", "", 0, 0, "", "")
                                Precio = 0
                                If dReader("precio") IsNot DBNull.Value Then Precio = CDbl(dReader("precio"))
                                Precio2 = 0
                                If dReader("precio2") IsNot DBNull.Value Then Precio2 = CDbl(dReader("precio2"))
                                Precio3 = 0
                                If dReader("precio3") IsNot DBNull.Value Then Precio3 = CDbl(dReader("precio3"))
                                precio4 = 0
                                If dReader("precio4") IsNot DBNull.Value Then precio4 = CDbl(dReader("precio4"))
                                precio5 = 0
                                If dReader("precio5") IsNot DBNull.Value Then precio5 = CDbl(dReader("precio5"))

                                Dim IP As New dbInventarioPrecios(ConMyslq)
                                IP.AsignaListas(C.ID)
                                IP.BuscaPrecio(C.ID, 1)
                                IP.Modificar(IP.ID, Precio, 0, "Lista 1", 2, 0, False)
                                IP.BuscaPrecio(C.ID, 2)
                                IP.Modificar(IP.ID, Precio2, 0, "Lista 2", 2, 0, False)
                                IP.BuscaPrecio(C.ID, 3)
                                IP.Modificar(IP.ID, Precio3, 0, "Lista 3", 2, 0, False)
                                IP.BuscaPrecio(C.ID, 4)
                                IP.Modificar(IP.ID, precio4, 0, "Lista 4", 2, 0, False)
                                IP.BuscaPrecio(C.ID, 5)
                                IP.Modificar(IP.ID, precio5, 0, "Lista 5", 2, 0, False)
                                If pAgregarInvInicial Then
                                    If dReader("costo") IsNot DBNull.Value And dReader("existencia") IsNot DBNull.Value Then
                                        If IsNumeric(dReader("costo")) And IsNumeric(dReader("existencia")) Then
                                            If dReader("costo") > 0 And Math.Round(dReader("existencia"), 2) > 0 Then
                                                'Mov.Guardar(Mov.DaNuevoFolio("", pidSucursal, pIdConcepto), Format(Date.Now, "yyyy/MM/dd"), pIdConcepto, "", pidSucursal, 0, 2)
                                                Mov.GuardarImp(Folio, Format(Date.Now, "yyyy/MM/dd"), pIdConcepto, Concepto.Serie, pidSucursal, 0, 2, dReader("costo") * dReader("existencia"), pIdAlmacen, 0, 0, 0)
                                                MovD.Guardar(Mov.ID, C.ID, dReader("existencia"), dReader("costo") * dReader("existencia"), 2, C.Nombre, pIdAlmacen, pIdAlmacen, 1, 0, 0, "", "", "", "")
                                                If pCalculaExCost Then
                                                    Mov.ModificaInventario(Mov.ID, op._TipoCosteo, 0)
                                                    Mov.ReCalculaCostos(Mov.ID, op._TipoCosteo, op.CostoTiempoReal, 0)
                                                End If
                                                Folio += 1
                                                'Mov.DaTotal(Mov.ID, 2)
                                                'Mov.Modificar(Mov.ID, Mov.Folio, 3, "", Mov.Serie, Mov.TotalVenta, Mov.TotalVenta, 0, 2, Mov.Fecha, 0, 0, 0)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                RenglonCont += 1
                My.Application.DoEvents()
            End While
            MySqlcom.Transaction.Commit()
        Catch ex As Exception
            MySqlcom.Transaction.Rollback()
            Throw New Exception(ex.Message + vbNewLine + "Registro: " + RenglonCont.ToString)
        Finally
            If Not dReader.IsClosed Then dReader.Close()
        End Try
    End Sub

    Public Sub ImportaCuentasContables()
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Conta As New dbContabilidadClasificacion(MySqlcon)
        Dim registro As Integer = 0
        Dim cuenta As String = ""
        Dim Descripcion As String = ""
        Dim tipo As String = ""
        Dim natur As String = ""
        Dim Agrupador As String = ""
        Dim renglones As Integer = 0
        Dim ren As Integer
        Dim fecha As String = ""
        ComAdo.CommandText = "select count(CUENTA) from [CUENTAS$]"
        renglones = ComAdo.ExecuteScalar
        ren = renglones
        ComAdo.CommandText = "select * from [CUENTAS$]"
        dReader = ComAdo.ExecuteReader
        MySqlcom.Transaction = MySqlcon.BeginTransaction
        Dim Progreso As New frmProgreso(renglones, 0)
        Progreso.Show()
        renglones = 0
        Try
            While dReader.Read()
                If registro + 1 <= ren Then

                    If dReader("CUENTA") IsNot DBNull.Value Then
                        cuenta = dReader("CUENTA")

                        If dReader("DESCRIPCION") IsNot DBNull.Value Then
                            Descripcion = dReader("DESCRIPCION")
                        Else
                            Descripcion = ""
                        End If
                        If dReader("TIPO") IsNot DBNull.Value Then
                            tipo = dReader("TIPO")
                        Else
                            tipo = "A"
                        End If
                        If dReader("NATURALEZA") IsNot DBNull.Value Then
                            natur = dReader("NATURALEZA")
                        Else
                            natur = "D"
                        End If

                        If dReader("AGRUPADOR") IsNot DBNull.Value Then
                            Agrupador = dReader("AGRUPADOR")
                        Else
                            Agrupador = "Nada"
                        End If
                        If dReader("FECHA") IsNot DBNull.Value Then
                            fecha = Conta.fechaCambio(dReader("FECHA"))
                        Else
                            fecha = Date.Now.ToString("yyyy/MM/dd")
                        End If
                    End If
                    Conta.separador(cuenta, Descripcion, tipo.Trim, natur.Trim, Agrupador.Trim, fecha)
                    registro += 1
                    renglones += 1
                    Progreso.Aumentar(renglones)
                End If
            End While
            MySqlcom.Transaction.Commit()


            Conta.AjustaIdsCuentas()

            If Conta.ErroresSincro <> "" Then
                MsgBox("Las siguietes cuentas no se han podido agregar ya que carecian de cuentas precesoras:" + vbCrLf + Conta.ErroresSincro, MsgBoxStyle.OkOnly, "Pull System Soft")
            End If
        Catch ex As Exception
            MySqlcom.Transaction.Rollback()
            Throw New Exception(ex.Message + vbNewLine + "Registro: " + registro.ToString)
        Finally
            If Not dReader.IsClosed Then dReader.Close()
        End Try
    End Sub
    Public Sub ImportaClasificaciones()
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim Conta As New dbContabilidadClasificacion(MySqlcon)
        Dim registro As Integer = 0
        Dim cuenta As String = ""
        Dim Descripcion As String = ""
        Dim tipo As String = ""
        Dim natur As String = ""
        Dim Agrupador As String = ""
        Dim renglones As Integer = 0
        Dim ren As Integer
        Dim fecha As String = ""
        Dim todo As String = ""
        ComAdo.CommandText = "select count(CLAVE) from [pago$]"
        renglones = ComAdo.ExecuteScalar
        ren = renglones
        ComAdo.CommandText = "select * from [pago$]"
        dReader = ComAdo.ExecuteReader
        MySqlcom.Transaction = MySqlcon.BeginTransaction
        Dim Progreso As New frmProgreso(renglones, 0)
        Progreso.Show()
        renglones = 0
        Try
            While dReader.Read()
                If registro + 1 <= ren Then
                    If dReader("CLAVE") IsNot DBNull.Value Then
                        todo += "Insert into tblMetodosPago(clave,concepto)values('" + dReader("CLAVE").ToString + "','" + dReader("CONCEPTO") + "');" + vbCrLf


                    End If
                    registro += 1
                    renglones += 1
                    Progreso.Aumentar(renglones)
                End If
            End While
            MySqlcom.Transaction.Commit()
            If Conta.ErroresSincro <> "" Then
                MsgBox("Las siguietes cuentas no se han podido agregar ya que carecian de cuentas precesoras:" + vbCrLf + Conta.ErroresSincro, MsgBoxStyle.OkOnly, "Pull System Soft")
            End If
        Catch ex As Exception
            MySqlcom.Transaction.Rollback()
            Throw New Exception(ex.Message + vbNewLine + "Registro: " + registro.ToString)
        Finally
            If Not dReader.IsClosed Then dReader.Close()
        End Try
    End Sub
    Public Sub ImportaPolizas()
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim poli As New dbContabilidadPolizas(MySqlcon)
        Dim conta As New dbContabilidadClasificacion(MySqlcon)
        poli.buscarPeriodo()
        Dim registro As Integer = 0
        Dim cuenta As String = ""
        Dim tipoPoliza As String = ""
        Dim numPoliza As Integer = -1
        Dim fecha As String = ""
        Dim concepto As String = ""
        Dim cantidad As String = ""
        Dim descripcion As String = ""
        Dim cargo As String = ""
        Dim abono As String = ""
        Dim importe As Double = 0
        Dim renglones As Integer = 0
        Dim errores As String = ""
        Dim fechaOriginal As String = ""
        'Dim DIOT As Integer
        Dim auxImporte As Double = 0
        Dim beneficiario = ""
        Dim ren As Integer
        ComAdo.CommandText = "select count(PFEC) from [POLIZAS$]"
        renglones = ComAdo.ExecuteScalar
        ren = renglones
        ComAdo.CommandText = "select * from [POLIZAS$]"
        dReader = ComAdo.ExecuteReader
        MySqlcom.Transaction = MySqlcon.BeginTransaction
        Dim Progreso As New frmProgreso(renglones, 0)
        Progreso.Show()
        renglones = 0
        Try
            While dReader.Read()
                If registro + 1 <= ren Then

                    If dReader("PFEC") IsNot DBNull.Value Then
                        If dReader("PCUENTA") Is DBNull.Value Then
                            'ES GENERAL
                            If importe <> 0 Then
                                poli.llenaDatosPoliza(poli.IDPoliza)
                                poli.modificarPoliza(poli.IDPoliza, poli.tipo2, poli.Numero, poli.fecha, poli.concepto, poli.beneficiario, "$" + importe.ToString("0.00"), 1, 0, 0)
                            End If
                            If tipoPoliza = dReader("PTPOL") And numPoliza = dReader("PNPOL") Then
                                If dReader("PCNC2") >= 1 And dReader("PCNC2") <= 10 Then
                                    concepto += dReader("PCONC") + " "
                                Else
                                    beneficiario += dReader("PCONC") + " "
                                End If

                            Else
                                numPoliza = dReader("PNPOL")
                                If dReader("PMES") = 0 Then
                                    tipoPoliza = "A"
                                Else
                                    tipoPoliza = dReader("PTPOL")
                                    If tipoPoliza = "O" Then
                                        tipoPoliza = "D"
                                        numPoliza += 100
                                    End If
                                End If


                                fechaOriginal = dReader("PFEC")
                                fecha = poli.fechaCambio(fechaOriginal)
                                concepto = dReader("PCONC")
                                cuenta = ""
                                importe = 0
                            End If
                        Else
                            'ES RENGLÓN
                            If dReader("PCUENTA").ToString.Trim = "" Then
                                'ES GENERAL
                                If importe <> 0 Then
                                    poli.llenaDatosPoliza(poli.IDPoliza)
                                    poli.modificarPoliza(poli.IDPoliza, poli.tipo2, poli.Numero, poli.fecha, poli.concepto, poli.beneficiario, "$" + importe.ToString("0.00"), 1, 0, 0)
                                End If
                                If tipoPoliza = dReader("PTPOL") And numPoliza = dReader("PNPOL") Then
                                    If dReader("PCNC2") >= 1 And dReader("PCNC2") <= 10 Then
                                        concepto += dReader("PCONC") + " "
                                    Else
                                        beneficiario += dReader("PCONC") + " "
                                    End If

                                Else
                                    numPoliza = dReader("PNPOL")
                                    If dReader("PMES") = 0 Then
                                        tipoPoliza = "A"
                                    Else
                                        tipoPoliza = dReader("PTPOL")
                                        If tipoPoliza = "O" Then
                                            tipoPoliza = "D"
                                            numPoliza += 100
                                        End If
                                    End If


                                    fechaOriginal = dReader("PFEC")
                                    fecha = poli.fechaCambio(fechaOriginal)
                                    concepto = dReader("PCONC")
                                    cuenta = ""
                                    importe = 0
                                End If
                            Else
                                If importe = 0 And cuenta = "" Then
                                    'SI ES LA PRIMERA VEZ QUE ENTRA, GUARDAR POLIZA
                                    'comprobar folio
                                    fechaOriginal = dReader("PFEC")
                                    numPoliza = dReader("PNPOL")
                                    If dReader("PMES") = 0 Then
                                        tipoPoliza = "A"
                                    Else
                                        tipoPoliza = dReader("PTPOL")
                                        If tipoPoliza = "O" Then
                                            tipoPoliza = "D"
                                        End If
                                    End If
                                    Dim pID As Integer = poli.folioRepetidoId(fechaOriginal.Chars(3) + fechaOriginal.Chars(4), fechaOriginal.Chars(6) + fechaOriginal.Chars(7) + fechaOriginal.Chars(8) + fechaOriginal.Chars(9), tipoPoliza, numPoliza, 1)
                                    If pID <> 0 Then
                                        'numPoliza = poli.bucarNumero(fechaOriginal.Chars(3) + fechaOriginal.Chars(4), fechaOriginal.Chars(6) + fechaOriginal.Chars(7) + fechaOriginal.Chars(8) + fechaOriginal.Chars(9), tipoPoliza, 1)
                                        poli.IDPoliza = pID
                                        poli.eliminarDetalles()
                                        poli.eliminarPoliza(pID)
                                    End If
                                    poli.guardarPoliza(tipoPoliza, numPoliza, fecha, concepto, beneficiario, importe, 1, 3, 0, 0)
                                    poli.DetallesOrden = 1
                                    concepto = ""
                                    beneficiario = ""
                                End If

                                cuenta = dReader("PCUENTA")
                                conta.separarCuenta(cuenta)
                                descripcion = dReader("PCONC")
                                'If dReader("PBCU") IsNot DBNull.Value Then
                                '    DIOT = 1
                                'Else
                                '    DIOT = 0
                                'End If
                                If dReader("PDH") = "D" Then
                                    'DEBE (CARGO)
                                    cargo = dReader("PIMP")
                                    abono = "-999999999"
                                    importe += Double.Parse(cargo)
                                Else
                                    abono = dReader("PIMP")
                                    cargo = "-999999999"
                                End If
                                If conta.IDcuenta = 0 Then
                                    'NO SE ENCONTRÓ LA CUENTA
                                    errores += tipoPoliza + numPoliza.ToString + "  " + cuenta + vbCrLf
                                    descripcion = ""
                                Else
                                    'GUARDAR
                                    poli.guardarDetalles(conta.cuentaCompleta, descripcion, cargo, abono, conta.IDcuenta, "", 0, "0", "", 0, 0.0, fecha, "", 0, 0, poli.DetallesOrden, 0, 0, 0, 0, 0)
                                    descripcion = ""
                                End If
                            End If


                        End If


                    End If


                    '  Conta.separador(cuenta, Descripcion, tipo, natur, Agrupador)
                    registro += 1
                    renglones += 1
                    Progreso.Aumentar(renglones)

                End If
            End While
            If cuenta = "" And concepto <> "" Then
                'no se ha guadardo poliza
                poli.guardarPoliza(tipoPoliza, numPoliza, fecha, concepto, "", importe, 1, 3, 0, 0)
                concepto = ""
            Else
                If descripcion <> "" Then
                    'no se ha guardado detalle
                    If conta.idContable = 0 Then
                        'NO SE ENCONTRÓ LA CUENTA
                        errores += tipoPoliza + numPoliza + "  " + cuenta + vbCrLf
                    Else
                        'GUARDAR
                        poli.guardarDetalles(cuenta, descripcion, cargo, abono, conta.idContable, "", 0, "0", "", 0, 0, fecha, "", 0, 0, poli.DetallesOrden, 0, 0, 0, 0, 0)
                        descripcion = ""
                    End If
                End If
            End If
            poli.ActulizaIdsPolizas()
            MySqlcom.Transaction.Commit()
            If errores <> "" Then
                MsgBox("Las siguietes cuentas no se han podido agregar ya que no existian o no eran último nivel:" + vbCrLf + "POLIZA - CUENTA" + vbCrLf + errores, MsgBoxStyle.OkOnly, "Pull System Soft")
            End If
        Catch ex As Exception
            MySqlcom.Transaction.Rollback()
            Throw New Exception(ex.Message + vbNewLine + "Registro: " + registro.ToString)
        Finally
            If Not dReader.IsClosed Then dReader.Close()
        End Try
    End Sub

    Public Sub ImportaExistencia(ByVal pAgregarInvInicial As Boolean, ByVal pidSucursal As Integer, ByVal pIdAlmacen As Integer, ByVal pIdConcepto As Integer, pCalculaExCost As Boolean)
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim C As New dbInventario(ConMyslq)
        'Dim Clasid1 As Integer
        'Dim Clasid2 As Integer
        'Dim Clasid3 As Integer
        'Dim IdUnidad As Integer
        'Dim codigoclas1 As Integer = 0
        'Dim codigoclas2 As Integer = 0
        'Dim codigoclas3 As Integer = 0
        'Dim codigoart As Integer = 1
        RenglonCont = 0
        'Dim Precio As Double
        'Dim Precio2 As Double
        'Dim Precio3 As Double
        'Dim precio4 As Double
        'Dim precio5 As Double
        Dim Folio As Integer = 0
        Dim op As New dbOpciones(ConMyslq)
        Dim Mov As New dbMovimientos(ConMyslq)
        Dim MovD As New dbMovimientosDetalles(ConMyslq)
        Dim Concepto As New dbInventarioConceptos(pIdConcepto, ConMyslq)
        ComAdo.CommandText = "select * from [CATALOGO$]"
        dReader = ComAdo.ExecuteReader
        Folio = Mov.DaNuevoFolio(Concepto.Serie, pidSucursal, pIdConcepto)
        MySqlcom.Transaction = MySqlcon.BeginTransaction
        Try
            While dReader.Read()
                If dReader("codigo") IsNot DBNull.Value Then
                    If C.DaIdArticulo(dReader("codigo")) Then
                        If dReader("costo") IsNot DBNull.Value And dReader("existencia") IsNot DBNull.Value Then
                            If IsNumeric(dReader("costo")) And IsNumeric(dReader("existencia")) Then
                                If dReader("costo") > 0 And Math.Round(dReader("existencia"), 2) > 0 Then
                                    Mov.GuardarImp(Folio, Format(Date.Now, "yyyy/MM/dd"), pIdConcepto, Concepto.Serie, pidSucursal, 0, 2, dReader("costo") * dReader("existencia"), pIdAlmacen, 0, 0, 0)
                                    MovD.Guardar(Mov.ID, C.ID, dReader("existencia"), dReader("costo") * dReader("existencia"), 2, C.Nombre, pIdAlmacen, pIdAlmacen, 1, 0, 0, "", "", "", "")
                                    'Mov.DaTotal(Mov.ID, 2)
                                    'Mov.Modificar(Mov.ID, Mov.Folio, 3, "", Mov.Serie, Mov.TotalVenta, Mov.TotalVenta, 0, 2, Mov.Fecha, 0, 0, 0)
                                    If pCalculaExCost Then
                                        Mov.ModificaInventario(Mov.ID, op._TipoCosteo, 0)
                                        Mov.ReCalculaCostos(Mov.ID, op._TipoCosteo, op.CostoTiempoReal, 0)
                                    End If
                                    Folio += 1
                                End If
                            End If
                        End If
                    End If
                    'End If
                End If
                RenglonCont += 1
                My.Application.DoEvents()
            End While
            MySqlcom.Transaction.Commit()
        Catch ex As Exception
            MySqlcom.Transaction.Rollback()
            Throw New Exception(ex.Message + vbNewLine + "Registro: " + RenglonCont.ToString)
        Finally
            If Not dReader.IsClosed Then dReader.Close()
        End Try
    End Sub
    Public Sub ImportarPrecios(pIdClas1 As Integer, pIdClas2 As Integer, pIdClas3 As Integer)
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim C As New dbInventario(ConMyslq)
        'Dim Clasid1 As Integer
        'Dim Clasid2 As Integer
        'Dim Clasid3 As Integer
        'Dim IdUnidad As Integer
        'Dim codigoclas1 As Integer = 0
        'Dim codigoclas2 As Integer = 0
        'Dim codigoclas3 As Integer = 0
        'Dim codigoart As Integer = 1
        RenglonCont = 0
        Dim Precio As Double = 0
        ComAdo.CommandText = "select * from [CATALOGO$]"
        dReader = ComAdo.ExecuteReader
        Dim IP As New dbInventarioPrecios(ConMyslq)
        Dim I As Integer = 0
        Dim Ok As Boolean = False
        Dim ActDescuentos As Boolean
        'Folio = Mov.DaNuevoFolio(Concepto.Serie, pidSucursal, pIdConcepto)
        MySqlcom.Transaction = MySqlcon.BeginTransaction
        Try
            While dReader.Read()
                If dReader("codigo") IsNot DBNull.Value Then
                    If C.DaIdArticulo(dReader("codigo")) Then
                        C.LlenaDatos()
                        If pIdClas1 <= 0 Then
                            Ok = True
                        Else
                            If C.Clasificacion.ID = pIdClas1 Then
                                Ok = True
                                If pIdClas2 > 0 And C.Clasificacion2.ID <> pIdClas2 Then Ok = False
                                If pIdClas3 > 0 And C.Clasificacion3.ID <> pIdClas3 Then Ok = False
                            Else
                                Ok = False
                            End If
                        End If
                        If Ok Then
                            For I = 1 To 20 Step 1
                                Try
                                    If dReader("precio" + I.ToString) IsNot DBNull.Value Then
                                        Precio = CDbl(dReader("precio" + I.ToString))
                                        IP.BuscaPrecio(C.ID, I)
                                        If I = 1 Then
                                            ActDescuentos = True
                                        Else
                                            ActDescuentos = False
                                        End If
                                        If IP.utilidad = 0 Then IP.Modificar(IP.ID, Precio, IP.utilidad, IP.Comentario, IP.IdMoneda, IP.Descuento, ActDescuentos)
                                    End If
                                Catch ex As Exception
                                End Try
                            Next
                        End If
                    End If
                End If
                RenglonCont += 1
                My.Application.DoEvents()
            End While
            MySqlcom.Transaction.Commit()
        Catch ex As Exception
            MySqlcom.Transaction.Rollback()
            Throw New Exception(ex.Message + vbNewLine + "Registro: " + RenglonCont.ToString)
        Finally
            If Not dReader.IsClosed Then dReader.Close()
        End Try
    End Sub
    Public Sub CierraConexiones()
        ConAdo.Close()
    End Sub

    '    Public Sub IngresaInventario(ByVal pTabla As String, ByVal pidAlmacen As Integer)
    '        Dim dReader As Data.OleDb.OleDbDataReader
    '        Dim C As New dbInventario(ConMyslq)
    '        Dim CS As New dbInventarioSeries(MySqlcon)
    '        Dim IdAlmacen As Integer
    '        Dim IdInventario As Integer
    '        Dim Contador As Integer = 0
    '        ComAdo.CommandText = "select * from [" + pTabla + "]"
    '        dReader = ComAdo.ExecuteReader
    '        IdAlmacen = pidAlmacen
    '        Try
    '            While dReader.Read
    '                If dReader("CODIGO") Is DBNull.Value Then
    '                    If (dReader("SERIES") Is DBNull.Value) = False And IdInventario <> 0 Then
    '                        If CS.ChecaNoSerieRepetido(dReader("SERIES"), IdInventario) = False Then
    '                            ' CS.Guardar(IdInventario, dReader("SERIES"), Format(Date.Now, "yyyy/MM/dd"), Format(Date.Now, "yyyy/MM/dd"), 0, 0, 0, 0, 0)
    '                        End If
    '                    End If
    '                Else
    '                    If C.BuscaArticulo(dReader("CODIGO"), 0) = False Then
    '                        C.Guardar( _
    'If(dReader("NOMBRE") Is DBNull.Value, "", dReader("NOMBRE")) _
    ', 0, 3, 1, 3, 2, "", _
    'If(dReader("CODIGO") Is DBNull.Value, "", dReader("CODIGO")) _
    ', 0, dReader("COSTO"), 2, "", 1, 1, _
    'If(dReader("SERIES") Is DBNull.Value, 0, 1) _
    ', 1, 16, 0, "", "", 0, "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, 0, 0)
    '                        IdInventario = C.ID
    '                        C.ActualizaInventario(IdInventario, dReader("EXIST"), IdAlmacen)
    '                        Contador += 1
    '                        If dReader("SERIES") Is DBNull.Value Then
    '                        Else
    '                            If CS.ChecaNoSerieRepetido(dReader("SERIES"), IdInventario) = False Then
    '                                ' CS.Guardar(IdInventario, dReader("SERIES"), Format(Date.Now, "yyyy/MM/dd"), Format(Date.Now, "yyyy/MM/dd"), 0, 0, 0, 0, 0)
    '                            End If
    '                        End If
    '                    Else
    '                        IdInventario = C.ID
    '                    End If
    '                End If
    '            End While
    '            dReader.Close()
    '        Catch ex As Exception
    '            MsgBox(ex.Message + " Cont=" + Contador.ToString)
    '        End Try

    '    End Sub
    Public Sub Takochequera()
        Try
            Dim dReader As Data.OleDb.OleDbDataReader
            'Dim C As New dbClientes(ConMyslq)
            Dim cuentaR As Integer = 0
            Dim Ref As String = ""
            Dim Hay As String
            MySqlcom.Transaction = MySqlcon.BeginTransaction
            ComAdo.CommandText = "select * from [solopdas2011$]"
            dReader = ComAdo.ExecuteReader
            While dReader.Read()
                If dReader("CAPITULO") IsNot DBNull.Value Then

                    If dReader("CAPITULO") IsNot DBNull.Value Then
                        If CStr(dReader("CAPITULO")).Length < 5 Then
                            Ref = CStr(dReader("CAPITULO")).PadLeft(5, "0")
                        Else
                            Ref = CStr(dReader("CAPITULO"))
                        End If

                    End If
                    If dReader("CONCEPTOc") IsNot DBNull.Value Then
                        If CStr(dReader("CONCEPTOc")).Length < 5 Then
                            Ref += CStr(dReader("CONCEPTOc")).PadLeft(5, "0")
                        Else
                            Ref += CStr(dReader("CONCEPTOc"))
                        End If

                    End If
                    If dReader("PARTIDAv") IsNot DBNull.Value Then
                        If CStr(dReader("PARTIDAv")).Length < 5 Then
                            Ref += CStr(dReader("PARTIDAv")).PadLeft(5, "0")
                        Else
                            Ref += CStr(dReader("PARTIDAv"))
                        End If
                    End If
                    MySqlcom.CommandText = "select ifnull((select numero from chequera_nue.cuentascontables where numero='" + Replace(Ref, "'", "''") + "'),'No')"
                    Hay = MySqlcom.ExecuteScalar
                    If Hay = "No" Then
                        MySqlcom.CommandText = "insert into chequera_nue.cuentascontables(numero,descripcion) values('" + Replace(Ref, "'", "''") + "','" + Replace(dReader("CONCEPTO"), "'", "''") + "')"
                    Else
                        MySqlcom.CommandText = "update chequera_nue.cuentascontables set descripcion='" + Replace(dReader("CONCEPTO"), "'", "''") + "' where numero='" + Replace(Ref, "'", "''") + "'"
                    End If
                    MySqlcom.ExecuteNonQuery()
                    cuentaR += 1
                    If cuentaR >= 200 Then
                        MsgBox("Van 200")
                        cuentaR = 0
                    End If
                End If
            End While
            dReader.Close()
            MySqlcom.Transaction.Commit()

        Catch ex As Exception
            MySqlcom.Transaction.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Public Sub ImportarNominas(ByVal pIdSucuesal As Integer, ByVal pNCert As String, ByVal pSerie As String, ByVal pClaveConta As String)
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim N As New dbNominas(ConMyslq)
        Dim ND As New dbNominasDetalles(ConMyslq)
        Dim T As New dbTrabajadores(ConMyslq)
        Dim cuentaR As Integer = 0
        Dim GuardaNuevo As Boolean
        Dim FaltaDato As Boolean
        Fallos = ""
        'Dim Ref As String
        ComAdo.CommandText = "select * from [recibos por timbrar$]"
        dReader = ComAdo.ExecuteReader
        While dReader.Read()
            GuardaNuevo = False
            FaltaDato = False
            N.Reset()
            If dReader("ATRAB") IsNot DBNull.Value Then
                If T.BuscaTrabajador(dReader("ATRAB")) = False Then
                    GuardaNuevo = True
                End If
                T.NumeroEmpleado = dReader("ATRAB")
            End If
            T.Antiguedad = 0
            T.Banco = 0
            T.Ciudad = ""
            T.CLABE = ""
            T.Colonia = ""
            T.CP = ""
            If dReader("CURP") IsNot DBNull.Value Then
                T.Curp = dReader("CURP")
            Else
                FaltaDato = True
            End If
            T.Departamento = ""
            T.Direccion = ""
            T.Email = ""
            T.Estado = ""
            If dReader("FechaInicioLaboral") IsNot DBNull.Value Then
                T.FechaInicioLaboral = Format(CDate(dReader("FechaInicioLaboral")), "yyyy/MM/dd")
            Else
                FaltaDato = True
            End If
            T.Municipio = ""
            T.NoExterior = ""
            T.NoInterior = ""
            If dReader("ReceptorNombre") IsNot DBNull.Value Then
                T.Nombre = dReader("ReceptorNombre")
            Else
                FaltaDato = True
            End If
            If dReader("NSS") IsNot DBNull.Value Then
                T.NumeroSeguroSocial = dReader("NSS")
            Else
                FaltaDato = True
            End If
            T.Pais = "México"
            If dReader("Periodicidad") IsNot DBNull.Value Then
                T.Periodicidad = dReader("Periodicidad")
            Else
                FaltaDato = True
            End If
            If dReader("Puesto") IsNot DBNull.Value Then
                T.Puesto = dReader("Puesto")
            Else
                FaltaDato = True
            End If
            T.ReferenciaDomicilio = ""

            If dReader("RegistroPatronal") IsNot DBNull.Value Then
                T.RegistroPatronal = dReader("RegistroPatronal")
            Else
                FaltaDato = True
            End If

            If dReader("ReceptorRFC") IsNot DBNull.Value Then
                T.RFC = dReader("ReceptorRFC")
            Else
                FaltaDato = True
            End If
            T.RiesgoPuesto = 1

            If dReader("SalarioBase") IsNot DBNull.Value Then
                T.SalarioBaseCotApor = dReader("SalarioBase")
            Else
                FaltaDato = True
            End If
            If dReader("SDI") IsNot DBNull.Value Then
                T.SalarioDiarioIntegrado = dReader("SDI")
            Else
                FaltaDato = True
            End If
            T.Telefono = ""
            If dReader("TipoContrato") IsNot DBNull.Value Then
                T.TipoContrato = dReader("TipoContrato")
            Else
                FaltaDato = True
            End If

            If dReader("TipoJornada") IsNot DBNull.Value Then
                T.TipoJornada = dReader("TipoJornada")
            Else
                FaltaDato = True
            End If

            If dReader("TipoRegimen") IsNot DBNull.Value Then
                T.TipoRegimen = dReader("TipoRegimen")
            Else
                FaltaDato = True
            End If

            If dReader("Antiguedad") IsNot DBNull.Value Then
                N.Antiguedad = dReader("Antiguedad")
            Else
                FaltaDato = True
            End If
            N.Banco = 0
            N.Clabe = ""
            If dReader("DiasPagados") IsNot DBNull.Value Then
                N.DiasPagados = dReader("DiasPagados")
            Else
                FaltaDato = True
            End If
            If dReader("FechaFinal") IsNot DBNull.Value Then
                N.FechaFinalPAgo = Format(CDate(dReader("FechaFinal")), "yyyy/MM/dd")
            Else
                FaltaDato = True
            End If
            If dReader("FechaInicial") IsNot DBNull.Value Then
                N.FechaInicialPago = Format(CDate(dReader("FechaInicial")), "yyyy/MM/dd")
            Else
                FaltaDato = True
            End If
            If dReader("FechaPago") IsNot DBNull.Value Then
                N.FechaPago = Format(CDate(dReader("FechaPago")), "yyyy/MM/dd")
            Else
                FaltaDato = True
            End If
            If dReader("Folio") IsNot DBNull.Value Then
                N.Folio = dReader("Folio")
            Else
                FaltaDato = True
            End If
            N.Importado = 1
            If dReader("Total") IsNot DBNull.Value Then
                N.TotalaPagar = dReader("Total")
            Else
                FaltaDato = True
            End If

            If FaltaDato = False Then
                If GuardaNuevo Then
                    T.Guardar(T.Nombre, T.RegistroPatronal, T.NumeroEmpleado, T.Curp, T.TipoRegimen, T.NumeroSeguroSocial, T.FechaInicioLaboral, 0, T.Departamento, T.Puesto, T.TipoJornada, T.TipoContrato, T.Periodicidad, T.SalarioBaseCotApor, T.RiesgoPuesto, T.SalarioDiarioIntegrado, T.Direccion, T.Telefono, T.Email, T.RFC, T.Ciudad, T.CP, T.Estado, T.Pais, T.NoExterior, T.NoInterior, T.Colonia, T.Municipio, T.ReferenciaDomicilio, T.Banco, T.CLABE, 0, 0, 0, 0, "O", "", "")
                Else
                    T.Modificar(T.ID, T.Nombre, T.RegistroPatronal, T.NumeroEmpleado, T.Curp, T.TipoRegimen, T.NumeroSeguroSocial, T.FechaInicioLaboral, 0, T.Departamento, T.Puesto, T.TipoJornada, T.TipoContrato, T.Periodicidad, T.SalarioBaseCotApor, T.RiesgoPuesto, T.SalarioDiarioIntegrado, T.Direccion, T.Telefono, T.Email, T.RFC, T.Ciudad, T.CP, T.Estado, T.Pais, T.NoExterior, T.NoInterior, T.Colonia, T.Municipio, T.ReferenciaDomicilio, T.Banco, T.CLABE, 0, 0, 0, 0, "O", "", "")
                End If
                If N.ChecaFolioRepetido(N.Folio, pSerie) = False Then
                    N.Guardar(T.ID, Format(Date.Now, "yyyy/MM/dd"), N.Folio, 0, 0, pIdSucuesal, pSerie, 0, "", "", pNCert, 2, 2, 0, 0, N.FechaPago, N.FechaInicialPago, N.FechaFinalPAgo, N.Banco, N.Clabe, N.DiasPagados, N.Antiguedad, 1, 1, "O", "", 0)
                    'ND.Guardar(N.ID, 1, 0, pClaveConta, "Sueldos, Salarios, Rayas y Jornales", N.TotalaPagar, 0)
                    'N.DaTotal(N.ID, 2)
                    N.Modificar(N.ID, Format(Date.Now, "yyyy/MM/dd"), N.Folio, 0, 0, 2, N.TotalaPagar, N.TotalaPagar, T.ID, pSerie, 0, "", "", pNCert, 2, 0, 2, "", N.FechaPago, N.FechaInicialPago, N.FechaFinalPAgo, N.DiasPagados, N.Antiguedad, 1, "O", "", 0)
                Else
                    Fallos += "Trabajador: " + T.Nombre + " folio repetido: " + N.Folio.ToString + vbCrLf
                End If
            Else
                Fallos += "Trabajador: " + T.Nombre + " folio: " + N.Folio.ToString + vbCrLf
            End If
        End While
        dReader.Close()
    End Sub


    Public Sub ImportarNominas2(ByVal pIdSucuesal As Integer, ByVal pNCert As String, ByVal pSerie As String, ByVal pClaveConta As String)
        Dim dReader As Data.OleDb.OleDbDataReader
        Dim N As New dbNominas(ConMyslq)
        Dim ND As New dbNominasDetalles(ConMyslq)
        Dim NH As New dbNoominaHorasExtra(ConMyslq)
        Dim NI As New dbNominasIncapacidades(ConMyslq)
        Dim T As New dbTrabajadores(ConMyslq)
        Dim cuentaR As Integer = 0
        Dim GuardaNuevo As Boolean
        Dim FaltaDato As Boolean
        Dim ConDetalles As Boolean = False
        Dim NoempleadoAnt As String = ""
        Fallos = ""
        'Dim Ref As String
        ComAdo.CommandText = "select * from [recibos por timbrar$]"
        dReader = ComAdo.ExecuteReader
        While dReader.Read()
            If dReader("Noempleado") IsNot DBNull.Value Then

                If CStr(dReader("Noempleado")) <> NoempleadoAnt And N.ID > 0 And ConDetalles = True Then
                    N.DaTotal(N.ID, 2)
                    N.Modificar(N.ID, Format(Date.Now, "yyyy/MM/dd"), N.Folio, 0, 0, 2, N.TotalaPagar, N.TotalaPagar, T.ID, N.Serie, 0, "", "", pNCert, 2, 0, 2, "", N.FechaPago, N.FechaInicialPago, N.FechaFinalPAgo, N.DiasPagados, N.Antiguedad, 1, "O", "", 0)
                End If
                If CStr(dReader("Noempleado")) <> NoempleadoAnt And dReader("tipoconcepto") Is DBNull.Value Then
                    GuardaNuevo = False
                    FaltaDato = False
                    ConDetalles = False
                    N.Reset()
                    If dReader("Noempleado") IsNot DBNull.Value Then
                        If T.BuscaTrabajador(dReader("noempleado")) = False Then
                            GuardaNuevo = True
                        End If
                        T.NumeroEmpleado = dReader("noempleado")
                        NoempleadoAnt = T.NumeroEmpleado
                    End If
                    T.Antiguedad = 0
                    T.Banco = 0
                    If dReader("ciudad") IsNot DBNull.Value Then
                        T.Ciudad = dReader("ciudad")
                    Else
                        T.Ciudad = ""
                        '   FaltaDato = True
                    End If
                    T.CLABE = ""
                    If dReader("colonia") IsNot DBNull.Value Then
                        T.Colonia = dReader("colonia")
                    Else
                        T.Colonia = ""
                        'FaltaDato = True
                    End If
                    If dReader("codigopostal") IsNot DBNull.Value Then
                        T.CP = dReader("codigopostal")
                    Else
                        T.CP = ""
                        '   FaltaDato = True
                    End If
                    If dReader("CURP") IsNot DBNull.Value Then
                        T.Curp = dReader("CURP")
                    Else
                        FaltaDato = True
                    End If
                    T.Departamento = ""
                    If dReader("calle") IsNot DBNull.Value Then
                        T.Direccion = dReader("calle")
                    Else
                        T.Direccion = ""
                        '   FaltaDato = True
                    End If
                    T.Email = ""
                    If dReader("estado") IsNot DBNull.Value Then
                        T.Estado = dReader("estado")
                    Else
                        T.Estado = ""
                        '   FaltaDato = True
                    End If
                    If dReader("FechaInicioLaboral") IsNot DBNull.Value Then
                        T.FechaInicioLaboral = Format(CDate(dReader("FechaInicioLaboral")), "yyyy/MM/dd")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("municipio") IsNot DBNull.Value Then
                        T.Municipio = dReader("municipio")
                    Else
                        T.Municipio = ""
                        '   FaltaDato = True
                    End If
                    If dReader("noexterior") IsNot DBNull.Value Then
                        T.NoExterior = dReader("noexterior")
                    Else
                        T.NoExterior = ""
                        '   FaltaDato = True
                    End If
                    If dReader("nointerior") IsNot DBNull.Value Then
                        T.NoInterior = dReader("nointerior")
                    Else
                        T.NoInterior = ""
                        '   FaltaDato = True
                    End If
                    If dReader("ReceptorNombre") IsNot DBNull.Value Then
                        T.Nombre = dReader("ReceptorNombre")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("NoSeguroSocial") IsNot DBNull.Value Then
                        T.NumeroSeguroSocial = dReader("NoSeguroSocial")
                    Else
                        FaltaDato = True
                    End If
                    T.Pais = "México"
                    If dReader("Periodicidad") IsNot DBNull.Value Then
                        T.Periodicidad = dReader("Periodicidad")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("Puesto") IsNot DBNull.Value Then
                        T.Puesto = dReader("Puesto")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("referenciadomicilio") IsNot DBNull.Value Then
                        T.ReferenciaDomicilio = dReader("referenciadomicilio")
                    Else
                        T.ReferenciaDomicilio = ""
                        '   FaltaDato = True
                    End If

                    If dReader("RegistroPatronal") IsNot DBNull.Value Then
                        T.RegistroPatronal = dReader("RegistroPatronal")
                    Else
                        FaltaDato = True
                    End If

                    If dReader("ReceptorRFC") IsNot DBNull.Value Then
                        T.RFC = dReader("ReceptorRFC")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("RiesgoPuesto") IsNot DBNull.Value Then
                        T.RiesgoPuesto = dReader("RiesgoPuesto")
                    Else
                        FaltaDato = True
                    End If

                    If dReader("SalarioBase") IsNot DBNull.Value Then
                        T.SalarioBaseCotApor = dReader("SalarioBase")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("SDI") IsNot DBNull.Value Then
                        T.SalarioDiarioIntegrado = dReader("SDI")
                    Else
                        FaltaDato = True
                    End If
                    T.Telefono = ""
                    If dReader("TipoContrato") IsNot DBNull.Value Then
                        T.TipoContrato = dReader("TipoContrato")
                    Else
                        FaltaDato = True
                    End If

                    If dReader("TipoJornada") IsNot DBNull.Value Then
                        T.TipoJornada = dReader("TipoJornada")
                    Else
                        FaltaDato = True
                    End If

                    If dReader("TipoRegimen") IsNot DBNull.Value Then
                        T.TipoRegimen = dReader("TipoRegimen")
                    Else
                        FaltaDato = True
                    End If

                    If dReader("Antiguedad") IsNot DBNull.Value Then
                        N.Antiguedad = dReader("Antiguedad")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("banco") IsNot DBNull.Value Then
                        N.Banco = dReader("banco")
                    Else
                        N.Banco = 0
                    End If
                    If dReader("CLABE") IsNot DBNull.Value Then
                        N.Clabe = dReader("CLABE")
                    Else
                        N.Clabe = ""
                    End If
                    If dReader("DiasPagados") IsNot DBNull.Value Then
                        N.DiasPagados = dReader("DiasPagados")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("FechaFinal") IsNot DBNull.Value Then
                        N.FechaFinalPAgo = Format(CDate(dReader("FechaFinal")), "yyyy/MM/dd")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("FechaInicial") IsNot DBNull.Value Then
                        N.FechaInicialPago = Format(CDate(dReader("FechaInicial")), "yyyy/MM/dd")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("FechaPago") IsNot DBNull.Value Then
                        N.FechaPago = Format(CDate(dReader("FechaPago")), "yyyy/MM/dd")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("Folio") IsNot DBNull.Value Then
                        N.Folio = dReader("Folio")
                    Else
                        FaltaDato = True
                    End If
                    N.Importado = 1
                    If dReader("Total") IsNot DBNull.Value Then
                        N.TotalaPagar = dReader("Total")
                    Else
                        FaltaDato = True
                    End If
                    If dReader("serie") IsNot DBNull.Value Then
                        N.Serie = dReader("serie")
                    Else
                        N.Serie = ""
                    End If
                    If FaltaDato = False Then
                        If GuardaNuevo Then
                            T.Guardar(T.Nombre, T.RegistroPatronal, T.NumeroEmpleado, T.Curp, T.TipoRegimen, T.NumeroSeguroSocial, T.FechaInicioLaboral, 0, T.Departamento, T.Puesto, T.TipoJornada, T.TipoContrato, T.Periodicidad, T.SalarioBaseCotApor, T.RiesgoPuesto, T.SalarioDiarioIntegrado, T.Direccion, T.Telefono, T.Email, T.RFC, T.Ciudad, T.CP, T.Estado, T.Pais, T.NoExterior, T.NoInterior, T.Colonia, T.Municipio, T.ReferenciaDomicilio, T.Banco, T.CLABE, 0, 0, 0, 0, "O", "", "")
                        Else
                            T.Modificar(T.ID, T.Nombre, T.RegistroPatronal, T.NumeroEmpleado, T.Curp, T.TipoRegimen, T.NumeroSeguroSocial, T.FechaInicioLaboral, 0, T.Departamento, T.Puesto, T.TipoJornada, T.TipoContrato, T.Periodicidad, T.SalarioBaseCotApor, T.RiesgoPuesto, T.SalarioDiarioIntegrado, T.Direccion, T.Telefono, T.Email, T.RFC, T.Ciudad, T.CP, T.Estado, T.Pais, T.NoExterior, T.NoInterior, T.Colonia, T.Municipio, T.ReferenciaDomicilio, T.Banco, T.CLABE, 0, 0, 0, 0, "O", "", "")
                        End If
                        If N.ChecaFolioRepetido(N.Folio, N.Serie) = False Then
                            N.Guardar(T.ID, Format(Date.Now, "yyyy/MM/dd"), N.Folio, 0, 0, pIdSucuesal, N.Serie, 0, "", "", pNCert, 2, 2, 0, 0, N.FechaPago, N.FechaInicialPago, N.FechaFinalPAgo, N.Banco, N.Clabe, N.DiasPagados, N.Antiguedad, 1, 1, "O", "", 0)
                            'ND.Guardar(N.ID, 1, 0, pClaveConta, "Sueldos, Salarios, Rayas y Jornales", N.TotalaPagar, 0)

                        Else
                            Fallos += "Trabajador: " + T.Nombre + " folio repetido: " + N.Folio.ToString + vbCrLf
                        End If
                    Else
                        Fallos += "Trabajador: " + T.Nombre + " folio: " + N.Folio.ToString + vbCrLf
                    End If
                Else
                    'Conceptos
                    If dReader("tipoconcepto") IsNot DBNull.Value And N.ID > 0 And FaltaDato = False And NoempleadoAnt = CStr(dReader("noempleado")) Then
                        If dReader("tipoconcepto") = "P" Then
                            ND.TipoPercepcionDeduccion = 0
                            ConDetalles = True
                            If dReader("tipo") IsNot DBNull.Value Then
                                ND.Tipo = dReader("tipo")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("clave") IsNot DBNull.Value Then
                                ND.Clave = dReader("clave")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("concepto") IsNot DBNull.Value Then
                                ND.Concepto = dReader("concepto")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("totalgravado") IsNot DBNull.Value Then
                                ND.ImporteGravado = dReader("totalgravado")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("totalexento") IsNot DBNull.Value Then
                                ND.ImporteExento = dReader("totalexento")
                            Else
                                FaltaDato = True
                            End If
                            If FaltaDato = False Then
                                ND.Guardar(N.ID, ND.Tipo, ND.TipoPercepcionDeduccion, ND.Clave, ND.Concepto, ND.ImporteGravado, ND.ImporteExento, 0, 0)
                            End If
                        End If

                        If dReader("tipoconcepto") = "D" Then
                            ND.TipoPercepcionDeduccion = 1
                            ConDetalles = True
                            If dReader("tipo") IsNot DBNull.Value Then
                                ND.Tipo = dReader("tipo")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("clave") IsNot DBNull.Value Then
                                ND.Clave = dReader("clave")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("concepto") IsNot DBNull.Value Then
                                ND.Concepto = dReader("concepto")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("totalgravado") IsNot DBNull.Value Then
                                ND.ImporteGravado = dReader("totalgravado")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("totalexento") IsNot DBNull.Value Then
                                ND.ImporteExento = dReader("totalexento")
                            Else
                                FaltaDato = True
                            End If
                            If FaltaDato = False Then
                                ND.Guardar(N.ID, ND.Tipo, ND.TipoPercepcionDeduccion, ND.Clave, ND.Concepto, ND.ImporteGravado, ND.ImporteExento, 0, 0)
                            End If
                        End If

                        If dReader("tipoconcepto") = "H" Then
                            'ND.TipoPercepcionDeduccion = 0
                            ConDetalles = True
                            If dReader("tipo") IsNot DBNull.Value Then
                                NH.TipoHoras = dReader("tipo")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("dias") IsNot DBNull.Value Then
                                NH.Dias = dReader("dias")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("horas") IsNot DBNull.Value Then
                                NH.HorasExtra = dReader("horas")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("importe") IsNot DBNull.Value Then
                                NH.ImportePagado = dReader("importe")
                            Else
                                FaltaDato = True
                            End If
                            If FaltaDato = False Then
                                NH.Guardar(N.ID, NH.Dias, NH.TipoHoras, NH.HorasExtra, NH.ImportePagado)
                            End If
                        End If
                        If dReader("tipoconcepto") = "I" Then
                            'ND.TipoPercepcionDeduccion = 0
                            ConDetalles = True
                            If dReader("tipo") IsNot DBNull.Value Then
                                NI.TipoIncapacidad = dReader("tipo")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("dias") IsNot DBNull.Value Then
                                NI.Dias = dReader("dias")
                            Else
                                FaltaDato = True
                            End If
                            If dReader("importe") IsNot DBNull.Value Then
                                NI.Descuento = dReader("importe")
                            Else
                                FaltaDato = True
                            End If
                            If FaltaDato = False Then
                                NI.Guardar(N.ID, NI.TipoIncapacidad, NI.Dias, NI.Descuento)
                            End If
                        End If
                    End If
                End If
            End If
        End While
        If N.ID <> 0 And FaltaDato = False Then
            N.DaTotal(N.ID, 2)
            N.Modificar(N.ID, Format(Date.Now, "yyyy/MM/dd"), N.Folio, 0, 0, 2, N.TotalaPagar, N.TotalaPagar, T.ID, N.Serie, 0, "", "", pNCert, 2, 0, 2, "", N.FechaPago, N.FechaInicialPago, N.FechaFinalPAgo, N.DiasPagados, N.Antiguedad, 1, "O", "", 0)

        End If
        dReader.Close()
    End Sub
End Class
