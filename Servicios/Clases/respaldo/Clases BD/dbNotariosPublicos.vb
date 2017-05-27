Imports MySql.Data.MySqlClient


Public Class dbNotariosPublicos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public TipoInmueble As String
    Public Calle As String
    Public NoExterior As String
    Public NoInterior As String
    Public Colonia As String
    Public Localidad As String
    Public Referencia As String
    Public Municipio As String
    Public Estado As Integer
    Public Pais As String
    Public CodigoPostal As String
    Public NumInstrumentoNotarial As Integer
    Public FechaInstNotarial As String
    Public MontoOperacion As Double
    Public Subtotal As Double
    Public IVA As Double
    Public CURP As String
    Public NumNotaria As Integer
    Public EntidadFederativa As Integer
    Public Adscripcion As String
    Public CoproSocConyugalE As Boolean
    Public CoproSocConyugalE2 As Boolean
    Public NombreNotario As String
    Public ID As Integer
    Public dtEnajenante As DataTable
    Public dtAdquiriente As DataTable
    Public dtInmuebles As DataTable
    Public dtImpresionEnajenante As DataTable
    Public dtimpresionAdquiriente As DataTable


    

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        TipoInmueble = ""
        Calle = ""
        NoExterior = ""
        NoInterior = ""
        Colonia = ""
        Localidad = ""
        Referencia = ""
        Municipio = ""
        Estado = 0
        Pais = ""
        CodigoPostal = ""
        NumInstrumentoNotarial = 0
        FechaInstNotarial = ""
        MontoOperacion = 0
        Subtotal = 0
        IVA = 0
        CURP = ""
        NumNotaria = 0
        EntidadFederativa = 0
        Adscripcion = ""
        CoproSocConyugalE = True
        CoproSocConyugalE2 = True
        Comm.Connection = Conexion
    End Sub
    Public Function llenarPais() As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblPais"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblPais")
        Return DS.Tables("tblPais")
    End Function

    Public Sub Guardar(ByVal pNumInstrumentoNotarial As Integer, ByVal pFechaInstNotarial As String, ByVal pMontoOperacion As Double, ByVal pSubtotal As Double, ByVal pIVA As Double, ByVal pCURP As String, ByVal pNumNotaria As Integer, ByVal pEntidadFederativa As Integer, ByVal pAdscripcion As String, ByVal pCoproSocConyugalE As Boolean, ByVal pCoproSocConyugalE2 As Boolean, ByVal pidFactura As Integer, ByVal pNombreNotario As String)
        Comm.CommandText = "insert into tblnotariospublicos(NumInstrumentoNotarial,FechaInstNotarial,MontoOperacion,Subtotal,IVA,CURP,NumNotaria,EntidadFederativa,Adscripcion,CoproSocConyugalE,CoproSocConyugalE2,idFactura,nombrenotario) values(" + pNumInstrumentoNotarial.ToString() + ",'" + pFechaInstNotarial + "'," + pMontoOperacion.ToString() + "," + pSubtotal.ToString + "," + pIVA.ToString + ",'" + pCURP + "'," + pNumNotaria.ToString + "," + pEntidadFederativa.ToString() + ",'" + pAdscripcion.ToString() + "'," + pCoproSocConyugalE.ToString + "," + pCoproSocConyugalE2.ToString + ", " + pidFactura.ToString + ",'" + Replace(pNombreNotario, "'", "''") + "' );"
        'Comm.ExecuteNonQuery()
        Comm.CommandText += "select max(ID) from tblnotariospublicos;"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub guardarInmuebles(ByVal idNotario As Integer, ByVal pTipoInmueble As String, ByVal pCalle As String, ByVal pNoExterior As String, ByVal pNoInterior As String, ByVal pColonia As String, ByVal pLocalidad As String, ByVal pReferencia As String, ByVal pMunicipio As String, ByVal pEstado As Integer, ByVal pPais As String, ByVal pCodigoPostal As String)
        Comm.CommandText = "insert into tblinmueble(idNotario,TipoInmueble,Calle,NoExterior,NoInterior,Colonia,Localidad,Referencia,Municipio,Estado,Pais,CodigoPostal) values(" + idNotario.ToString + ", '" + pTipoInmueble + "','" + pCalle + "','" + pNoExterior + "','" + pNoInterior + "','" + pColonia + "','" + pLocalidad + "','" + pReferencia + "','" + pMunicipio + "'," + pEstado.ToString() + ",'" + pPais + "','" + pCodigoPostal + "' );"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarInmuebles(ByVal idInmueble As Integer, ByVal pTipoInmueble As String, ByVal pCalle As String, ByVal pNoExterior As String, ByVal pNoInterior As String, ByVal pColonia As String, ByVal pLocalidad As String, ByVal pReferencia As String, ByVal pMunicipio As String, ByVal pEstado As Integer, ByVal pPais As String, ByVal pCodigoPostal As String)
        Comm.CommandText = "update tblinmueble set TipoInmueble='" + pTipoInmueble + "',Calle='" + pCalle + "',NoExterior='" + pNoExterior + "',NoInterior='" + pNoInterior + "',Colonia='" + pColonia + "',Localidad='" + pLocalidad + "',Referencia='" + pReferencia + "',Municipio='" + pMunicipio + "',Estado=" + pEstado.ToString() + ",Pais='" + pPais + "',CodigoPostal='" + pCodigoPostal + "' where ID=" + idInmueble.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Modificar(ByVal pID As Integer, ByVal pNumInstrumentoNotarial As Integer, ByVal pFechaInstNotarial As String, ByVal pMontoOperacion As Double, ByVal pSubtotal As Double, ByVal pIVA As Double, ByVal pCURP As String, ByVal pNumNotaria As Integer, ByVal pEntidadFederativa As Integer, ByVal pAdscripcion As String, ByVal pCoproSocConyugalE As Boolean, ByVal pCoproSocConyugalE2 As Boolean, ByVal pNombreNotario As String)

        Comm.CommandText = "update tblnotariospublicos set NumInstrumentoNotarial=" + pNumInstrumentoNotarial.ToString() + ",FechaInstNotarial='" + pFechaInstNotarial + "',MontoOperacion=" + pMontoOperacion.ToString() + ",Subtotal=" + pSubtotal.ToString + ",IVA=" + pIVA.ToString + ",CURP='" + pCURP + "',NumNotaria=" + pNumNotaria.ToString + ",EntidadFederativa=" + pEntidadFederativa.ToString() + ",Adscripcion='" + pAdscripcion.ToString() + "',CoproSocConyugalE=" + pCoproSocConyugalE.ToString + ",CoproSocConyugalE2=" + pCoproSocConyugalE2.ToString + ",nombrenotario='" + Replace(pNombreNotario, "'", "''") + "' where ID=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblnotariospublicos where ID=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    'ADQUIRIENTE
    Public Sub GuardarAdquiriente(ByVal pNombre As String, ByVal pApellidoPaterno As String, ByVal pApellidoMaterno As String, ByVal pRFC As String, ByVal pCURP As String, ByVal pIDNotario As Integer)
        Comm.CommandText = "insert into tbladquiriente(Nombre,ApellidoPaterno, ApellidoMaterno,RFC,CURP,IDNotario) values('" + pNombre + "','" + pApellidoPaterno + "', '" + pApellidoMaterno + "','" + pRFC + "','" + pCURP + "'," + pIDNotario.ToString + ");"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarAdquiriente(ByVal pIDNotario As Integer)
        Comm.CommandText = "delete from tbladquiriente where IDNotario=" + pIDNotario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    'ADQUIRIENTES
    Public Sub GuardarAdquirientes(ByVal pNombre As String, ByVal pApellidoPaterno As String, ByVal pApellidoMaterno As String, ByVal pRFC As String, ByVal pCURP As String, ByVal pPorcenajes As Double, ByVal pIDNotario As Integer)
        Comm.CommandText = "insert into tbladquirientes(Nombre,ApellidoPaterno, ApellidoMaterno,RFC,CURP,Porcentaje,IDNotario) values('" + pNombre + "','" + pApellidoPaterno + "', '" + pApellidoMaterno + "','" + pRFC + "','" + pCURP + "'," + pPorcenajes.ToString() + " , " + pIDNotario.ToString + ");"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarAdquirientes(ByVal pIDNotario As Integer)
        Comm.CommandText = "delete from tbladquirientes where IDNotario=" + pIDNotario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    'Enajenante
    Public Sub GuardarEnajenante(ByVal pNombre As String, ByVal pApellidoPaterno As String, ByVal pApellidoMaterno As String, ByVal pRFC As String, ByVal pCURP As String, ByVal pIDNotario As Integer)
        Comm.CommandText = "insert into tblenajenante(Nombre,ApellidoPaterno, ApellidoMaterno,RFC,CURP,IDNotario) values('" + pNombre + "','" + pApellidoPaterno + "', '" + pApellidoMaterno + "','" + pRFC + "','" + pCURP + "'," + pIDNotario.ToString + ");"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarEnajenante(ByVal pIDNotario As Integer)
        Comm.CommandText = "delete from tblenajenante where IDNotario=" + pIDNotario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    'Enajenates
    Public Sub GuardarEnajenantes(ByVal pNombre As String, ByVal pApellidoPaterno As String, ByVal pApellidoMaterno As String, ByVal pRFC As String, ByVal pCURP As String, ByVal pPorcenajes As Double, ByVal pIDNotario As Integer)
        Comm.CommandText = "insert into tblenajenantes(Nombre,ApellidoPaterno, ApellidoMaterno,RFC,CURP,Porcentaje,IDNotario) values('" + pNombre + "','" + pApellidoPaterno + "', '" + pApellidoMaterno + "','" + pRFC + "','" + pCURP + "'," + pPorcenajes.ToString() + " , " + pIDNotario.ToString + ");"
        Comm.ExecuteNonQuery()
    End Sub
    'Public Sub EliminarEnajenantes(ByVal pIDNotario As Integer)
    '    Comm.CommandText = "delete from tblenajenantes where IDNotario=" + pIDNotario.ToString
    '    Comm.ExecuteNonQuery()
    'End Sub
    Public Function encontrarID(ByVal pidFactura As Integer) As String
        Comm.CommandText = "select ID from tblnotariospublicos where idFactura=" + pidFactura.ToString
        Return Comm.ExecuteScalar
    End Function
    Public Sub LlenaDatos(ByVal pid As Integer) 'Si no te sabes el ID consultalo con el  método "encontrarID"
        ID = pid

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblnotariospublicos where ID=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then

            NumInstrumentoNotarial = DReader("NumInstrumentoNotarial")
            FechaInstNotarial = DReader("FechaInstNotarial")
            MontoOperacion = DReader("MontoOperacion")
            Subtotal = DReader("Subtotal")
            IVA = DReader("IVA")
            CURP = DReader("CURP")
            NumNotaria = DReader("NumNotaria")
            EntidadFederativa = DReader("EntidadFederativa")
            Adscripcion = DReader("Adscripcion")
            CoproSocConyugalE = DReader("CoproSocConyugalE")
            CoproSocConyugalE2 = DReader("CoproSocConyugalE2")
            NombreNotario = DReader("nombrenotario")

        End If
        DReader.Close()
        Dim DS As New DataSet
        Dim DSS As New DataSet
        If CoproSocConyugalE = False Then
            Comm.CommandText = "select * from tblenajenante where IDNotario=" + ID.ToString
            Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA.Fill(DS, "tblempenios")

            dtEnajenante = DS.Tables("tblempenios")

            Comm.CommandText = "select Nombre, CONCAT(ApellidoPaterno, ' ', ApellidoMaterno), RFC, CURP, ' ' as Porcentaje from tblenajenante where IDNotario=" + ID.ToString
            Dim DA1 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA1.Fill(DSS, "tblImpresionEnajenante")
            DSS.WriteXmlSchema("tblImpresionEnajenante.xml")
            dtImpresionEnajenante = DSS.Tables("tblImpresionEnajenante")

        Else
            Comm.CommandText = "select * from tblenajenantes where IDNotario=" + ID.ToString
            Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA.Fill(DS, "tblempenios")
            dtEnajenante = DS.Tables("tblempenios")

            Comm.CommandText = "select Nombre, CONCAT(ApellidoPaterno, ' ', ApellidoMaterno), RFC, CURP, CONCAT('Porcentaje enajenado: ',Porcentaje, '%') as Porcentaje from tblenajenantes where IDNotario=" + ID.ToString
            Dim DA1 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA1.Fill(DSS, "tblImpresionEnajenante")
            DSS.WriteXmlSchema("tblImpresionEnajenante.xml")
            dtImpresionEnajenante = DSS.Tables("tblImpresionEnajenante")

        End If
        Dim DS2 As New DataSet
        Dim DSS2 As New DataSet
        If CoproSocConyugalE2 = False Then
            Comm.CommandText = "select * from tbladquiriente where IDNotario=" + ID.ToString
            Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA.Fill(DS2, "tblempenios")
            dtAdquiriente = DS2.Tables("tblempenios")

            Comm.CommandText = "select Nombre, CONCAT(ApellidoPaterno, ' ', ApellidoMaterno), RFC, CURP, ' ' as Porcentaje from tbladquiriente where IDNotario=" + ID.ToString
            Dim DA1 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA1.Fill(DSS2, "tblImpresionAdquiriente")
            DSS2.WriteXmlSchema("tblImpresionAdquiriente.xml")
            dtimpresionAdquiriente = DSS2.Tables("tblImpresionAdquiriente")

        Else
            Comm.CommandText = "select * from tbladquirientes where IDNotario=" + ID.ToString
            Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA.Fill(DS2, "tblempenios")
            dtAdquiriente = DS2.Tables("tblempenios")

            Comm.CommandText = "select Nombre, CONCAT(ApellidoPaterno, ' ', ApellidoMaterno), RFC, CURP, CONCAT('Porcentaje enajenado: ',Porcentaje, '%') as Porcentaje from tbladquirientes where IDNotario=" + ID.ToString
            Dim DA1 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA1.Fill(DSS2, "tblImpresionAdquiriente")
            DSS2.WriteXmlSchema("tblImpresionAdquiriente.xml")
            dtimpresionAdquiriente = DSS2.Tables("tblImpresionAdquiriente")
        End If

        Dim DS3 As New DataSet
        Comm.CommandText = "select * from tblinmueble where idNotario=" + ID.ToString
        Dim DA3 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA3.Fill(DS3, "tblInmuebles")
        DS3.WriteXmlSchema("tblInmuebles.xml")
        dtInmuebles = DS3.Tables("tblInmuebles")

    End Sub
    Public Function encontrarEstado(ByVal pid As Integer) As String
        Comm.CommandText = "select estado from tblestados where id=" + pid.ToString
        Return Comm.ExecuteScalar
    End Function
    Public Function HayDatosNotarios(ByVal pid As Integer) As String
        Comm.CommandText = "select ID from tblnotariospublicos where idFactura=" + pid.ToString
        Return Comm.ExecuteScalar
    End Function

    Public Function consultaInmuebles(ByVal pidNotario As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblinmueble where idNotario=" + pidNotario.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblInmuebles")
        Return DS.Tables("tblInmuebles")
    End Function

    Public Sub LlenaDatosInmueble(ByVal pid As Integer)


        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblinmueble where ID=" + pid.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            TipoInmueble = DReader("TipoInmueble")
            Calle = DReader("Calle")
            NoExterior = DReader("NoExterior")
            NoInterior = DReader("NoInterior")
            Colonia = DReader("Colonia")
            Localidad = DReader("Localidad")
            Referencia = DReader("Referencia")
            Municipio = DReader("Municipio")
            Estado = DReader("Estado")
            Pais = DReader("Pais")
            CodigoPostal = DReader("CodigoPostal")

        End If
        DReader.Close()
    End Sub
    Public Sub Eliminarinmuebles(ByVal pIDNotario As Integer)
        Comm.CommandText = "delete from tblinmueble where IdNotario=" + pIDNotario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarEnajenantes(ByVal pIDNotario As Integer)
        Comm.CommandText = "delete from tblenajenantes where IDNotario=" + pIDNotario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function encontrartipoInmueble(ByVal pid As Integer) As String
        Comm.CommandText = "select nombre from tbltipoinmueble where ID=" + pid.ToString
        Return Comm.ExecuteScalar
    End Function

    Public Function CreaXML(ByVal pID As Integer) As String 'El pID es el de la factura
        Dim aux As String
        Dim resultado As String
        ' Dim tablaInm As DataTable
        resultado = ""
        LlenaDatos(pID)


        resultado += "<notariospublicos:NotariosPublicos xmlns:notariospublicos=""http://www.sat.gob.mx/notariospublicos"" Version=""1.0"">"
        'DESCRIPCION INMUEBLE
        resultado += "<notariospublicos:DescInmuebles>"
        For i As Integer = 0 To dtInmuebles.Rows.Count - 1
            resultado += "<notariospublicos:DescInmueble TipoInmueble=""" + Integer.Parse(dtInmuebles.Rows(i)(2)).ToString("00") + """ Calle=""" + dtInmuebles.Rows(i)(3).ToString + """"
            If NoExterior <> "" Then
                resultado += " NoExterior=""" + dtInmuebles.Rows(i)(4).ToString + """"
            End If
            If NoInterior <> "" Then
                resultado += " NoInterior=""" + dtInmuebles.Rows(i)(5).ToString + """"
            End If
            If Colonia <> "" Then
                resultado += " Colonia=""" + dtInmuebles.Rows(i)(6).ToString + """"
            End If
            If Localidad <> "" Then
                resultado += " Localidad=""" + dtInmuebles.Rows(i)(7).ToString + """"
            End If
            If Referencia <> "" Then
                resultado += " Referencia=""" + dtInmuebles.Rows(i)(8).ToString + """"
            End If
            resultado += " Municipio=""" + dtInmuebles.Rows(i)(9).ToString + """ Estado=""" + Format(dtInmuebles.Rows(i)(10), "00") + """ Pais=""" + dtInmuebles.Rows(i)(11).ToString + """ CodigoPostal=""" + dtInmuebles.Rows(i)(12).ToString + """/>"
        Next

        resultado += "</notariospublicos:DescInmuebles>"
        'FIN DESCRIPCION INMUEBLE
        ' DatosOperacion

        resultado += "<notariospublicos:DatosOperacion NumInstrumentoNotarial=""" + NumInstrumentoNotarial.ToString + """ FechaInstNotarial=""" + Format(CDate(FechaInstNotarial), "yyyy-MM-dd") + """ MontoOperacion=""" + MontoOperacion.ToString("0.00") + """ Subtotal=""" + Subtotal.ToString("0.00") + """ IVA=""" + IVA.ToString("0.00") + """/>"
        'resultado += "</notariospublicos:DatosOperacion >" + vbCrLf
        'fin  DatosOperacion

        'INICIA DATOS NOTARIO
        resultado += "<notariospublicos:DatosNotario CURP=""" + CURP + """ NumNotaria=""" + NumNotaria.ToString + """ EntidadFederativa=""" + Format(EntidadFederativa, "00") + """"
        If Adscripcion <> "" Then
            resultado += " Adscripcion=""" + Adscripcion + """"
        End If
        resultado += "/>"
        'FIN DE DATOS NOTARIO

        'DATOS ENAJENANTE
        If CoproSocConyugalE = False Then
            aux = "No"
        Else
            aux = "Si"
        End If
        resultado += "<notariospublicos:DatosEnajenante CoproSocConyugalE=""" + aux + """>"
        If CoproSocConyugalE = False Then
            'un enajenante

            resultado += "<notariospublicos:DatosUnEnajenante Nombre=""" + dtEnajenante.Rows(0)(1).ToString + """ ApellidoPaterno=""" + dtEnajenante.Rows(0)(2).ToString + """"
            If dtEnajenante.Rows(0)(3).ToString <> "" Then
                resultado += " ApellidoMaterno=""" + dtEnajenante.Rows(0)(3).ToString + """"
            End If
            resultado += " RFC=""" + dtEnajenante.Rows(0)(4).ToString + """ CURP=""" + dtEnajenante.Rows(0)(5).ToString + """ />"
        Else
            resultado += "<notariospublicos:DatosEnajenantesCopSC>"
            For j As Integer = 0 To dtEnajenante.Rows.Count() - 1
                resultado += "<notariospublicos:DatosEnajenanteCopSC Nombre=""" + dtEnajenante.Rows(j)(1).ToString + """"
                If dtEnajenante.Rows(j)(2).ToString <> "" Then
                    resultado += " ApellidoPaterno=""" + dtEnajenante.Rows(j)(2).ToString + """"
                End If
                If dtEnajenante.Rows(j)(3).ToString <> "" Then
                    resultado += " ApellidoMaterno=""" + dtEnajenante.Rows(j)(3).ToString + """"
                End If
                resultado += " RFC=""" + dtEnajenante.Rows(j)(4).ToString + """"
                If dtEnajenante.Rows(j)(5).ToString <> "" Then
                    resultado += " CURP=""" + dtEnajenante.Rows(j)(5).ToString + """"
                End If
                resultado += " Porcentaje=""" + Double.Parse(dtEnajenante.Rows(j)(6).ToString).ToString("0.00") + """/>" + vbCrLf
            Next
            resultado += "</notariospublicos:DatosEnajenantesCopSC>"

        End If

        resultado += "</notariospublicos:DatosEnajenante> "

        'TERMINA DATOS ENAJENANTE
        'EMPIEZA : DatosAdquiriente

        If CoproSocConyugalE2 = False Then
            aux = "No"
        Else
            aux = "Si"
        End If
        resultado += "<notariospublicos:DatosAdquiriente CoproSocConyugalE=""" + aux + """>"
        If CoproSocConyugalE2 = False Then
            resultado += "<notariospublicos:DatosUnAdquiriente Nombre=""" + dtAdquiriente.Rows(0)(1).ToString + """"
            If dtAdquiriente.Rows(0)(2).ToString <> "" Then
                resultado += " ApellidoPaterno=""" + dtAdquiriente.Rows(0)(2).ToString + """"
            End If

            If dtAdquiriente.Rows(0)(3).ToString <> "" Then
                resultado += " ApellidoMaterno=""" + dtAdquiriente.Rows(0)(3).ToString + """"
            End If
            resultado += " RFC=""" + dtAdquiriente.Rows(0)(4).ToString + """"
            If dtAdquiriente.Rows(0)(5).ToString <> "" Then
                resultado += " CURP=""" + dtAdquiriente.Rows(0)(5).ToString + """"
            End If
            resultado += " />"

        Else
            resultado += "<notariospublicos:DatosAdquirientesCopSC>"
            For j As Integer = 0 To dtAdquiriente.Rows.Count() - 1
                resultado += "<notariospublicos:DatosAdquirienteCopSC Nombre=""" + dtAdquiriente.Rows(j)(1).ToString + """"
                If dtAdquiriente.Rows(j)(2).ToString <> "" Then
                    resultado += " ApellidoPaterno=""" + dtAdquiriente.Rows(j)(2).ToString + """"
                End If
                If dtAdquiriente.Rows(j)(3).ToString <> "" Then
                    resultado += " ApellidoMaterno=""" + dtAdquiriente.Rows(j)(3).ToString + """"
                End If
                resultado += " RFC=""" + dtAdquiriente.Rows(j)(4).ToString + """"
                If dtAdquiriente.Rows(j)(5).ToString <> "" Then
                    resultado += " CURP=""" + dtAdquiriente.Rows(j)(5).ToString + """"
                End If
                resultado += " Porcentaje=""" + Double.Parse(dtAdquiriente.Rows(j)(6).ToString).ToString("0.00") + """/>"
            Next
            resultado += "</notariospublicos:DatosAdquirientesCopSC>"
        End If
        resultado += "</notariospublicos:DatosAdquiriente> "




        resultado += "</notariospublicos:NotariosPublicos>"

        Return resultado
    End Function

    Public Function CreaCadenaOriginal(ByVal pID As Integer) As String
        Dim aux As String
        Dim resultado As String
        ' Dim tablaInm As DataTable
        resultado = ""
        LlenaDatos(pID)


        resultado += "|1.0|"
        'DESCRIPCION INMUEBLE
        For i As Integer = 0 To dtInmuebles.Rows.Count - 1
            resultado += Integer.Parse(dtInmuebles.Rows(i)(2).ToString).ToString("00") + "|" + dtInmuebles.Rows(i)(3).ToString + "|"
            If NoExterior <> "" Then
                resultado += dtInmuebles.Rows(i)(4).ToString + "|"
            End If
            If NoInterior <> "" Then
                resultado += +dtInmuebles.Rows(i)(5).ToString + "|"
            End If
            If Colonia <> "" Then
                resultado += +dtInmuebles.Rows(i)(6).ToString + "|"
            End If
            If Localidad <> "" Then
                resultado += dtInmuebles.Rows(i)(7).ToString + "|"
            End If
            If Referencia <> "" Then
                resultado += dtInmuebles.Rows(i)(8).ToString + "|"
            End If
            resultado += dtInmuebles.Rows(i)(9).ToString + "|" + Format(dtInmuebles.Rows(i)(10), "00") + "|" + dtInmuebles.Rows(i)(11).ToString + "|" + dtInmuebles.Rows(i)(12).ToString + "|"
        Next

        'resultado += "</notariospublicos:DescInmuebles>" + vbCrLf
        'FIN DESCRIPCION INMUEBLE
        ' DatosOperacion

        resultado += NumInstrumentoNotarial.ToString + "|" + Format(CDate(FechaInstNotarial), "yyyy-MM-dd") + "|" + MontoOperacion.ToString("0.00") + "|" + Subtotal.ToString("0.00") + "|" + IVA.ToString("0.00") + "|"
        'resultado += "</notariospublicos:DatosOperacion >" + vbCrLf
        'fin  DatosOperacion

        'INICIA DATOS NOTARIO
        resultado += CURP + "|" + NumNotaria.ToString + "|" + Format(EntidadFederativa, "00") + "|"
        If Adscripcion <> "" Then
            resultado += Adscripcion + "|"
        End If
        'resultado += "/>" + vbCrLf
        'FIN DE DATOS NOTARIO

        'DATOS ENAJENANTE
        If CoproSocConyugalE = False Then
            aux = "No"
        Else
            aux = "Si"
        End If
        resultado += aux + "|"
        If CoproSocConyugalE = False Then
            'un enajenante
            resultado += dtEnajenante.Rows(0)(1).ToString + "|" + dtEnajenante.Rows(0)(2).ToString + "|"
            If dtEnajenante.Rows(0)(3).ToString <> "" Then
                resultado += dtEnajenante.Rows(0)(3).ToString + "|"
            End If
            resultado += dtEnajenante.Rows(0)(4).ToString + "|" + dtEnajenante.Rows(0)(5).ToString + "|"
        Else
            For j As Integer = 0 To dtEnajenante.Rows.Count() - 1
                resultado += dtEnajenante.Rows(j)(1).ToString + "|"
                If dtEnajenante.Rows(j)(2).ToString <> "" Then
                    resultado += dtEnajenante.Rows(j)(2).ToString + "|"
                End If
                If dtEnajenante.Rows(j)(3).ToString <> "" Then
                    resultado += dtEnajenante.Rows(j)(3).ToString + "|"
                End If
                resultado += dtEnajenante.Rows(j)(4).ToString + "|"
                If dtEnajenante.Rows(j)(5).ToString <> "" Then
                    resultado += dtEnajenante.Rows(j)(5).ToString + "|"
                End If
                resultado += Double.Parse(dtEnajenante.Rows(j)(6).ToString).ToString("0.00") + "|"
            Next






        End If



        ' resultado += "</notariospublicos:DatosEnajenante> " + vbCrLf

        'TERMINA DATOS ENAJENANTE
        'EMPIEZA : DatosAdquiriente

        If CoproSocConyugalE2 = False Then
            aux = "No"
        Else
            aux = "Si"
        End If
        resultado += aux + "|"
        If CoproSocConyugalE2 = False Then

            resultado += dtAdquiriente.Rows(0)(1).ToString + "|"
            If dtAdquiriente.Rows(0)(2).ToString <> "" Then
                resultado += dtAdquiriente.Rows(0)(2).ToString + "|"
            End If

            If dtAdquiriente.Rows(0)(3).ToString <> "" Then
                resultado += dtAdquiriente.Rows(0)(3).ToString + "|"
            End If
            resultado += dtAdquiriente.Rows(0)(4).ToString + "|"
            If dtAdquiriente.Rows(0)(5).ToString <> "" Then
                resultado += dtAdquiriente.Rows(0)(5).ToString + "|"
            End If
            '  resultado += " />" + vbCrLf

        Else

            For j As Integer = 0 To dtAdquiriente.Rows.Count() - 1
                resultado += dtAdquiriente.Rows(j)(1).ToString + "|"
                If dtAdquiriente.Rows(j)(2).ToString <> "" Then
                    resultado += dtAdquiriente.Rows(j)(2).ToString + "|"
                End If
                If dtAdquiriente.Rows(j)(3).ToString <> "" Then
                    resultado += dtAdquiriente.Rows(j)(3).ToString + "|"
                End If
                resultado += dtAdquiriente.Rows(j)(4).ToString + "|"
                If dtAdquiriente.Rows(j)(5).ToString <> "" Then
                    resultado += dtAdquiriente.Rows(j)(5).ToString + "|"
                End If
                resultado += Double.Parse(dtAdquiriente.Rows(j)(6).ToString).ToString("0.00") + "|"
            Next

        End If


        'resultado += "</notariospublicos:DatosAdquiriente> " + vbCrLf


        ' resultado += "</NotariosPublicos>"
        While resultado.IndexOf("||") <> -1
            resultado = Replace(resultado, "||", "|") 'Aqui quita los renglones vacíos
        End While
        While resultado.IndexOf("  ") <> -1 'espacios dobles no van
            resultado = Replace(resultado, "  ", " ")
        End While
        resultado = Replace(resultado, vbTab, "") 'quita los tabs
        Return resultado
    End Function
    Public Function seleccionarInmueble(ByVal pID As Integer) As DataTable
        Dim DS3 As New DataSet
        Comm.CommandText = "select * from tblinmueble where idNotario=" + pID.ToString
        Dim DA3 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA3.Fill(DS3, "tblInmuebles")
        DS3.WriteXmlSchema("tblInmuebles.xml")
        Return DS3.Tables("tblInmuebles")
    End Function
End Class
