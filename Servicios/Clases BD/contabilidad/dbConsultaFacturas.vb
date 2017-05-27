Imports MySql.Data.MySqlClient
Public Class dbConsultaFacturas
    Private comm As New MySqlCommand
    Private SA As dbSucursalesArchivos
    Private Ruta As String
    Private O As dbOpciones

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        SA = New dbSucursalesArchivos
        O = New dbOpciones(conexion)
    End Sub

    Public Function filtrar(ByVal rfc As String, ByVal nombre As String, ByVal folio As String, ByVal desde As String, ByVal hasta As String, ByVal uuid As String) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select t.fecha as Fecha, t.uuid as UUID, t.rfc as RFC, t.monto as Total from tblxmlvalidados as t where t.fecha>='" + desde + "' and t.fecha<='" + hasta + "'"
        If rfc <> "" Then
            comm.CommandText += " and t.rfc like'%" + rfc + "%'"
        End If
        If nombre <> "" Then
            comm.CommandText = "select t.fecha as Fecha, t.uuid as UUID, t.rfc as RFC, t.monto as Total from tblxmlvalidados as t inner join tblproveedores as p on t.rfc=p.rfc where t.fecha>='" + desde + "' and t.fecha<='" + hasta + "' and p.nombre like '%" + nombre + "%'"
        End If
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "Facturas")
        Return ds.Tables("Facturas").DefaultView
    End Function

    Private Function buscaSubCarpetas(ruta As String, path As String) As String()

        Dim lista As New List(Of String)
        lista.AddRange(IO.Directory.GetFiles(ruta, path))
        Dim aux As String() = IO.Directory.GetDirectories(ruta)
        If aux.Length > 0 Then
            For Each f As String In aux
                lista.AddRange(buscaSubCarpetas(f, path))
            Next
        End If
        Return lista.ToArray()
    End Function

    Public Sub imprimir(ByVal pUUID As String, ByRef tabla As DataTable, ByVal resultado As String, ByVal pRuta As String, Optional ByVal archivos As String() = Nothing)
        Dim xmldoc As New Xml.XmlDocument
        Me.Ruta = pRuta
        Dim facturas As String()
        If archivos Is Nothing Then
            If My.Computer.FileSystem.DirectoryExists(Ruta + "\VALIDADAS") = False Then
                Exit Sub
            End If
            facturas = buscaSubCarpetas(Ruta + "\VALIDADAS", "*.xml")
        Else
            facturas = archivos
        End If
        Dim UUID As String
        Dim serie As String
        Dim folio As String
        Dim rr As DataRow = tabla.NewRow
        For Each f As String In facturas
            xmldoc.Load(f)
            Try
                UUID = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
            Catch
                UUID = ""
            End Try
            If UUID = pUUID Then
                rr("UUID") = UUID
                rr("Importe Total") = Format(CDbl(xmldoc.Item("cfdi:Comprobante").Attributes("total").Value), O._formatoTotal)
                rr("RFC Emisor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value
                rr("Nombre Emisor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("nombre").Value.ToString()
                rr("Fecha Emisión") = CDate(Replace(Replace(xmldoc.Item("cfdi:Comprobante").Attributes("fecha").Value, "T", " "), "-", "/")).ToString()
                rr("Versión") = xmldoc.Item("cfdi:Comprobante").Attributes("version").Value
                rr("Tipo Comprobante") = xmldoc.Item("cfdi:Comprobante").Attributes("tipoDeComprobante").Value
                rr("Certificado SAT") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                rr("Certificado Emisor") = xmldoc.Item("cfdi:Comprobante").Attributes("noCertificado").Value
                rr("RFC Receptor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Receptor").Attributes("rfc").Value
                rr("Nombre Receptor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Receptor").Attributes("nombre").Value
                rr("Fecha Certificación") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                rr("Resultado de Validación") = resultado
                Try
                    serie = xmldoc.Item("cfdi:Comprobante").Attributes("serie").Value
                Catch ex As Exception
                    serie = ""
                End Try
                Try
                    folio = xmldoc.Item("cfdi:Comprobante").Attributes("folio").Value.ToString
                Catch ex As Exception
                    folio = ""
                End Try
                rr("Folio") = serie + folio
                tabla.Rows.Add(rr("Versión"), rr("Tipo Comprobante"), rr("Certificado SAT"), rr("Certificado Emisor"), rr("Fecha Emisión"), rr("Fecha Certificación"), rr("UUID"), rr("Importe Total"), rr("RFC Emisor"), rr("Nombre Emisor"), rr("RFC Receptor"), rr("Nombre Receptor"), rr("Resultado de Validación"), rr("Folio"))
                Exit For
            End If
        Next
        'Return tabla
    End Sub

    Public Function buscaFolio(ByVal pFolio As String, ByRef tabla As DataTable, ByVal pRuta As String) As DataView
        Dim xmldoc As New Xml.XmlDocument
        Ruta = pRuta
        If My.Computer.FileSystem.DirectoryExists(Ruta + "\VALIDADAS") = False Then
            Return Nothing
        End If
        Dim facturas As String() = buscaSubCarpetas(Ruta + "\VALIDADAS", "*.xml")
        Dim UUID As String
        Dim folio As String
        Dim serie As String
        Dim rr As DataRow = tabla.NewRow
        For Each f As String In facturas
            xmldoc.Load(f)
            Try
                UUID = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
            Catch
                UUID = ""
            End Try
            rr("UUID") = UUID
            rr("Total") = xmldoc.Item("cfdi:Comprobante").Attributes("total").Value
            rr("RFC") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value
            'rr("Nombre Emisor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("nombre").Value
            rr("Fecha") = CDate(Replace(Replace(xmldoc.Item("cfdi:Comprobante").Attributes("fecha").Value, "T", " "), "-", "/")).ToString()
            'rr("Versión") = xmldoc.Item("cfdi:Comprobante").Attributes("version").Value
            'rr("Tipo Comprobante") = xmldoc.Item("cfdi:Comprobante").Attributes("tipoDeComprobante").Value
            'rr("Certificado SAT") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
            'rr("Certificado Emisor") = xmldoc.Item("cfdi:Comprobante").Attributes("noCertificado").Value
            'rr("RFC Receptor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Receptor").Attributes("rfc").Value
            'rr("Nombre Receptor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Receptor").Attributes("nombre")
            'rr("Fecha Certificación") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
            'rr("Resultado de Validación") = resultado

            Try
                serie = xmldoc.Item("cfdi:Comprobante").Attributes("serie").Value
            Catch ex As Exception
                serie = ""
            End Try
            Try
                folio = xmldoc.Item("cfdi:Comprobante").Attributes("folio").Value.ToString
            Catch ex As Exception
                folio = ""
            End Try
            Dim s As String = serie + folio
            If s.StartsWith(pFolio) Then
                tabla.Rows.Add(rr("Fecha"), rr("UUID"), rr("Total"), rr("RFC"))
            End If
            Exit For

        Next
        Return tabla.DefaultView
    End Function
    Public Sub imprimir2(ByVal pUUID As String, ByRef tabla As DataTable, ByVal resultado As String, ByVal pRuta As String)
        Dim xmldoc As New Xml.XmlDocument
        Me.Ruta = pRuta
        If My.Computer.FileSystem.DirectoryExists(Ruta + "\VALIDADAS") = False Then
            Exit Sub
        End If
        Dim facturas As String() = buscaSubCarpetas(Ruta + "\VALIDADAS", "*.xml")
        Dim UUID As String
        Dim serie As String
        Dim folio As String
        Dim rr As DataRow = tabla.NewRow
        For Each f As String In facturas
            xmldoc.Load(f)
            Try
                UUID = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
            Catch
                UUID = ""
            End Try
            If UUID = pUUID Then
                rr("UUID") = UUID
                rr("Importe Total") = Format(CDbl(xmldoc.Item("cfdi:Comprobante").Attributes("total").Value), O._formatoTotal)
                rr("RFC Emisor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value
                rr("Nombre Emisor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("nombre").Value.ToString()
                rr("Fecha Emisión") = CDate(Replace(Replace(xmldoc.Item("cfdi:Comprobante").Attributes("fecha").Value, "T", " "), "-", "/")).ToString()
                rr("Versión") = xmldoc.Item("cfdi:Comprobante").Attributes("version").Value
                rr("Tipo Comprobante") = xmldoc.Item("cfdi:Comprobante").Attributes("tipoDeComprobante").Value
                rr("Certificado SAT") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                rr("Certificado Emisor") = xmldoc.Item("cfdi:Comprobante").Attributes("noCertificado").Value
                rr("RFC Receptor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Receptor").Attributes("rfc").Value
                rr("Nombre Receptor") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Receptor").Attributes("nombre").Value
                rr("Fecha Certificación") = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                rr("Resultado de Validación") = resultado
                Try
                    serie = xmldoc.Item("cfdi:Comprobante").Attributes("serie").Value
                Catch ex As Exception
                    serie = ""
                End Try
                Try
                    folio = xmldoc.Item("cfdi:Comprobante").Attributes("folio").Value.ToString
                Catch ex As Exception
                    folio = ""
                End Try
                rr("Folio") = serie + folio
                tabla.Rows.Add(rr("Versión"), rr("Tipo Comprobante"), rr("Certificado SAT"), rr("Certificado Emisor"), rr("Fecha Emisión"), rr("Fecha Certificación"), rr("UUID"), rr("Importe Total"), rr("RFC Emisor"), rr("Nombre Emisor"), rr("RFC Receptor"), rr("Nombre Receptor"), rr("Resultado de Validación"), rr("Folio"))
                Exit For
            End If
        Next
        'Return tabla
    End Sub
End Class
