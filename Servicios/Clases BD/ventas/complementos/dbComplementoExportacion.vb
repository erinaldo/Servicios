Imports MySql.Data.MySqlClient
Public Class dbComplementoExportacion
    Private comm As New MySqlCommand
    Public idComplemento As Integer
    Public tipoOperacion As String
    Public clavePedimiento As String
    Public certificadoOrigen As Integer
    Public numCertificado As String
    Public numExportarConfiable As String
    Public incoterm As String
    Public subDivision As Integer
    Public observaciones As String
    Public tipoCambioUSD As Double
    Public totalUSD As Double
    Public idFactura As Integer
    Public estado As Integer
    Public curpEmisor As String
    Public motivoTraslado As String
    Public idCliente As Integer
    Public VerCFDI As String
    Public Sub New(ByVal conexion As MySqlConnection, ByVal idCliente As Integer)
        comm.Connection = conexion
        Me.idCliente = idCliente
    End Sub

    Public Sub New(ByVal idFactura As Integer, ByVal conexion As MySqlConnection, ByVal idCliente As Integer)
        Me.idFactura = idFactura
        Me.idCliente = idCliente
        comm.Connection = conexion
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblcomercioexterior where idfactura=" + idFactura.ToString()
        Dim dr As MySqlDataReader = comm.ExecuteReader()
        While dr.Read()
            idComplemento = dr("idComplemento")
            tipoOperacion = dr("tipoOperacion")
            clavePedimiento = dr("clavePedimiento")
            certificadoOrigen = dr("certificadoOrigen")
            numCertificado = dr("numCertificado")
            numExportarConfiable = dr("numExportarConfiable")
            incoterm = dr("incoterm")
            subDivision = dr("subDivision")
            observaciones = dr("observaciones")
            tipoCambioUSD = dr("tipoCambioUSD")
            totalUSD = dr("totalUSD")
            estado = dr("estado")
            curpEmisor = dr("curpEmisor")
            motivoTraslado = dr("motivoTraslado")
        End While
        dr.Close()
    End Sub

    Public Function buscar(ByVal idFactura As Integer) As Boolean
        comm.CommandText = "select idfactura from tblcomercioexterior where idfactura=" + idFactura.ToString()
        Dim i = comm.ExecuteScalar
        If i > 0 Then
            Me.idFactura = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function guardar(ByVal tipoOperacion As String, ByVal clavePedimiento As String, ByVal certificadoOrigen As Integer, ByVal numCertificado As String, ByVal numExportarConfiable As String, ByVal incoterm As String, ByVal subDivision As Integer, ByVal observaciones As String, ByVal tipoCambioUSD As Double, ByVal totalUSD As Double, ByVal idFactura As Integer, ByVal curpEmisor As String, ByVal motivoTraslado As String) As Boolean
        comm.CommandText = "insert into tblcomercioexterior(tipoOperacion,clavepedimiento,certificadoorigen,numcertificado,numexportarconfiable,incoterm,subdivision,observaciones,tipocambiousd,totalusd,idfactura,estado,curpEmisor,motivoTraslado)"
        comm.CommandText += "values('" + tipoOperacion + "','" + clavePedimiento + "'," + certificadoOrigen.ToString() + ",'" + numCertificado + "','" + numExportarConfiable + "','" + incoterm + "'," + subDivision.ToString() + ",'" + observaciones + "'," + tipoCambioUSD.ToString() + "," + totalUSD.ToString() + "," + idFactura.ToString() + "," + CInt(Estados.Inicio).ToString() + ",'" + curpEmisor + "','" + motivoTraslado + "');"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Function modificar(ByVal idComplemento As Integer, ByVal tipoOperacion As String, ByVal clavePedimiento As String, ByVal certificadoOrigen As Integer, ByVal numCertificado As String, ByVal numExportarConfiable As String, ByVal incoterm As String, ByVal subDivision As Integer, ByVal observaciones As String, ByVal tipoCambioUSD As Double, ByVal totalUSD As Double, ByVal idFactura As Integer, ByVal curpEmisor As String, ByVal motivoTraslado As String) As Boolean
        comm.CommandText = "update tblcomercioexterior set tipooperacion='" + tipoOperacion + "', clavepedimiento='" + clavePedimiento + "', certificadoorigen=" + certificadoOrigen.ToString + ", numcertificado='" + numCertificado + "', numexportarconfiable='" + numExportarConfiable + "', incoterm='" + incoterm + "', subdivision=" + subDivision.ToString + ", observaciones='" + observaciones + "', tipocambiousd=" + tipoCambioUSD.ToString + ", totalusd=" + totalUSD.ToString + ", idfactura=" + idFactura.ToString
        comm.CommandText += ", estado=" + CInt(Estados.Guardada).ToString() + ", curpEmisor='" + curpEmisor + "', motivoTraslado='" + motivoTraslado + "' where idcomplemento=" + idComplemento.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "delete from tblcomercioexterior where idcomplemento=" + idComplemento.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function guardar(ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "update tblcomercioexterior set estado=" + CInt(Estados.Guardada).ToString() + " where idcomplemento=" + idComplemento.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function crearXML() As String
        Dim r As New dbComplementoExportacionReceptor(MySqlcon)
        r.buscaComplemento(idComplemento)
        Dim d As New dbComplementoExportacioDestinatario(MySqlcon)
        Dim dom As New dbComplementoExportacionDomicilio(MySqlcon)
        Dim m As New dbComplementoExportacionMercancia(MySqlcon)
        Dim p As New dbComplementoPropietarios(MySqlcon)
        Dim des As New dbComplementoExportacionDescripcion(MySqlcon)
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Dim C As New dbClientes(idCliente, MySqlcon)
        Dim IdsMercancia() As Integer
        Dim Idspropietarios() As Integer
        Dim IdsDescripciones() As Integer
        Dim xml As String = "<cce11:ComercioExterior Version=""1.1"""
        If Me.motivoTraslado <> "" Then
            xml += " MotivoTraslado=""" + motivoTraslado + """"
        End If
        If Me.tipoOperacion <> "" Then
            xml += " TipoOperacion=""" + tipoOperacion + """"
        End If
        If Me.clavePedimiento <> "" Then
            xml += " ClaveDePedimento=""" + clavePedimiento + """"
        End If
        If certificadoOrigen > 0 Then
            xml += " CertificadoOrigen=""" + certificadoOrigen.ToString() + """"
        End If
        If certificadoOrigen = 1 Then
            If numCertificado <> "" Then
                xml += " NumCertificadoOrigen=""" + numCertificado + """"
            End If
        End If
        If numExportarConfiable <> "" Then
            xml += " NumeroExportadorConfiable=""" + numExportarConfiable + """"
        End If
        If incoterm <> "" Then
            xml += " Incoterm=""" + incoterm + """"
        End If
        If subDivision <> 0 Then
            xml += " Subdivision=""" + subDivision.ToString() + """"
        End If
        If observaciones <> "" Then
            xml += " Observaciones=""" + observaciones + """"
        End If
        If tipoCambioUSD <> 0 Then
            xml += " TipoCambioUSD=""" + Format(tipoCambioUSD, "0.00") + """"
        End If
        If totalUSD <> 0 Then
            xml += " TotalUSD=""" + Format(totalUSD, "0.00") + """"
        End If
        xml += ">" + vbCrLf
        'EMISOR
        xml += "<cce11:Emisor "
        If S.CURP <> "" Then
            xml += "Curp=""" + S.CURP + """>" + vbCrLf
        End If
        If VerCFDI = "3.3" Then
            If S.Direccion <> "" Then
                xml += "<cce11:Domicilio Calle=""" + S.Direccion.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            End If
            If S.NoExterior <> "" Then
                xml += " NumeroExterior=""" + S.NoExterior + """"
            End If
            If S.NoInterior <> "" Then
                xml += " NumeroInterior=""" + S.NoInterior + """"
            End If
            'If S.Colonia <> "" Then
            '    xml += " Colonia=""" + S.Colonia + """"
            'End If
            'If S.Ciudad <> "" Then
            '    xml += " Ciudad=""" + S.Ciudad + """"
            'End If|
            If S.ReferenciaDomicilio <> "" Then
                xml += " Referencia=""" + S.ReferenciaDomicilio.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            End If
            'If S.Municipio <> "" Then
            '    xml += " Municipio=""" + S.Municipio + """"
            'End If
            If S.Estado <> "" Then 'CODIGO
                xml += " Estado=""" + DaClaveEstadosMexico(S.Estado) + """"
            End If
            If S.Pais <> "" Then 'CODIGO
                xml += " Pais=""" + "MEX" + """"
            End If
            If S.CP <> "" Then
                xml += " CodigoPostal=""" + S.CP + """"
            End If
            xml += " />" + vbCrLf
        End If
        xml += "</cce11:Emisor>" + vbCrLf
        'Termina Emisor

        'Comienza Propietario en caso de haber
        If motivoTraslado = "05" Then
            Idspropietarios = p.buscaComplemento(idComplemento)
            If Idspropietarios.Length > 0 Then
                xml += "<cce11:Propietarios>" + vbCrLf
                For Each id As Integer In Idspropietarios
                    p.buscar(id)
                    xml += "<cce11:Propietario"
                    xml += " NumRegIdTrib=""" + p.numRegIdTrib + """"
                    xml += " ResidenciaFiscal=""" + p.clavePais + "</cce11:Propietario>" + vbCrLf
                Next
                xml += "</cce11:Propietarios>" + vbCrLf
            End If
        End If
        'Termina Propietario

        'Receptor
        xml += "<cce11:Receptor"
        xml += " NumRegIdTrib=""" + r.numRegIdTrib + """>" + vbCrLf
        If VerCFDI = "3.3" Then
            If C.Direccion <> "" Then
                xml += "<cce11:Domicilio Calle=""" + C.Direccion.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            End If
            If C.NoExterior <> "" Then
                xml += " NumeroExterior=""" + C.NoExterior + """"
            End If
            If C.NoInterior <> "" Then
                xml += " NumeroInterior=""" + C.NoInterior + """"
            End If
            If C.Colonia <> "" Then
                xml += " Colonia=""" + C.Colonia.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            End If
            If C.Ciudad <> "" Then
                xml += " Localidad=""" + C.Ciudad.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            End If
            If C.ReferenciaDomicilio <> "" Then
                xml += " Referencia=""" + C.ReferenciaDomicilio.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            End If
            If C.Municipio <> "" Then
                xml += " Municipio=""" + C.Municipio.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            End If
            If C.Estado <> "" Then 'CODIGO
                xml += " Estado=""" + DaClaveEstadosMexico(C.Estado) + """"
            End If
            If r.clave_Pais <> "" Then 'CODIGO
                xml += " Pais=""" + r.clave_Pais + """"
            End If
            If C.CP <> "" Then
                xml += " CodigoPostal=""" + C.CP + """/>" + vbCrLf
            ElseIf C.CP = "" Then
                xml += " />" + vbCrLf
            End If
        End If
        xml += "</cce11:Receptor>" + vbCrLf
        'termina receptor

        If d.buscaComplemento(idComplemento) Then
            xml += "<cce11:Destinatario"
            If d.numRegIdTrip <> "" Then
                xml += " NumRegIdTrib=""" + d.numRegIdTrip + """"
            End If
            If d.nombre <> "" Then
                xml += " Nombre=""" + d.nombre + """"
            End If
            xml += ">" + vbCrLf
            dom.buscaDestinatario(d.idDestinatario)
            xml += "<cce11:Domicilio Calle=""" + dom.calle.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            If dom.numExterior <> "" Then
                xml += " NumeroExterior=""" + dom.numExterior + """"
            End If
            If dom.numInterior <> "" Then
                xml += " NumeroInterior=""" + dom.numInterior + """"
            End If
            If dom.colonia <> "" Then
                xml += " Colonia=""" + dom.colonia.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            End If
            If dom.localidad <> "" Then
                xml += " Localidad=""" + dom.localidad.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            End If
            If dom.referencia <> "" Then
                xml += " Referencia=""" + dom.referencia.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            End If
            If dom.municipio <> "" Then
                xml += " Municipio=""" + dom.municipio.Replace(".", "").Replace("(", "").Replace(")", "") + """"
            End If
            xml += " Estado=""" + dom.estado + """" 'CODIGO 
            xml += " Pais=""" + dom.pais + """" 'CODIGO
            xml += " CodigoPostal=""" + dom.cp + """/>"
            xml += "</cce11:Destinatario>" + vbCrLf
        End If

        IdsMercancia = m.buscaComplemento(idComplemento)
        If IdsMercancia.Length > 0 Then
            xml += "<cce11:Mercancias>" + vbCrLf
            For Each id As Integer In IdsMercancia
                m.buscar(id)
                xml += "<cce11:Mercancia"
                xml += " NoIdentificacion=""" + m.noIdentificacion + """"
                If m.fraccionArancelaria <> "" Then
                    xml += " FraccionArancelaria=""" + m.fraccionArancelaria + """"
                End If
                If m.cantidadAduana <> 0 Then
                    xml += " CantidadAduana=""" + m.cantidadAduana.ToString() + """"
                End If
                If m.unidadAduana <> "" Then
                    xml += " UnidadAduana=""" + m.unidadAduana + """"
                End If
                If m.valorUnitarioAduana <> 0 Then
                    xml += " ValorUnitarioAduana=""" + Format(m.valorUnitarioAduana, "0.00") + """"
                End If
                xml += " ValorDolares=""" + Format(m.valorDolares, "0.00") + """>" + vbCrLf
                IdsDescripciones = des.buscaMercancia(m.idMercancia)
                If IdsDescripciones.Length > 0 Then
                    For Each x As Integer In IdsDescripciones
                        des.buscar(x)
                        xml += "<cce11:DescripcionesEspecificas"
                        xml += " Marca=""" + des.marca + """"
                        If des.modelo <> "" Then
                            xml += " Modelo=""" + des.modelo + """"
                        End If
                        If des.submodelo <> "" Then
                            xml += " SubModelo=""" + des.submodelo + """"
                        End If
                        If des.numeroSerie <> "" Then
                            xml += " NumeroSerie=""" + des.numeroSerie + """"
                        End If
                        xml += " />" + vbCrLf
                    Next
                End If
                xml += "</cce11:Mercancia>" + vbCrLf
            Next
            xml += "</cce11:Mercancias>" + vbCrLf
        End If
        xml += "</cce11:ComercioExterior>" + vbCrLf
        Return xml
    End Function
   
    Public Function creaCadenaOriginal() As String
        Dim r As New dbComplementoExportacionReceptor(MySqlcon)
        r.buscaComplemento(idComplemento)
        Dim d As New dbComplementoExportacioDestinatario(MySqlcon)
        Dim dom As New dbComplementoExportacionDomicilio(MySqlcon)
        Dim m As New dbComplementoExportacionMercancia(MySqlcon)
        Dim p As New dbComplementoPropietarios(MySqlcon)
        Dim des As New dbComplementoExportacionDescripcion(MySqlcon)
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Dim C As New dbClientes(idCliente, MySqlcon)
        Dim IdsMercancia() As Integer
        Dim Idspropietarios() As Integer
        Dim IdsDescripciones() As Integer
        Dim xml As String = "|1.1|"
        If motivoTraslado <> "" Then xml += motivoTraslado + "|"
        xml += tipoOperacion + "|"
        If Me.clavePedimiento <> "" Then
            xml += clavePedimiento + "|"
        End If
        If certificadoOrigen > 0 Then
            xml += certificadoOrigen.ToString() + "|"
        End If
        If certificadoOrigen = 1 Then
            If numCertificado <> "" Then
                xml += numCertificado + "|"
            End If
        End If
        If numExportarConfiable <> "" Then
            xml += numExportarConfiable + "|"
        End If
        If incoterm <> "" Then
            xml += incoterm + "|"
        End If
        If subDivision <> 0 Then
            xml += subDivision.ToString() + "|"
        End If
        If observaciones <> "" Then
            xml += observaciones + "|"
        End If
        If tipoCambioUSD <> 0 Then
            xml += Format(tipoCambioUSD, "0.00") + "|"
        End If
        If totalUSD <> 0 Then
            xml += Format(totalUSD, "0.00") + "|"
        End If
        'Emisor
        If curpEmisor <> "" Then
            xml += curpEmisor + "|"
        End If
        If VerCFDI = "3.3" Then
            If S.Direccion <> "" Then
                xml += S.Direccion.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            End If
            If S.NoExterior <> "" Then
                xml += S.NoExterior + "|"
            End If
            If S.NoInterior <> "" Then
                xml += S.NoInterior + "|"
            End If
            'If S.Colonia <> "" Then
            '    xml += S.Colonia + "|"
            'End If
            'If S.Ciudad <> "" Then
            '    xml += S.Ciudad + "|"
            'End If
            If S.ReferenciaDomicilio <> "" Then
                xml += S.ReferenciaDomicilio.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            End If
            'If S.Municipio <> "" Then
            '    xml += S.Municipio + "|"
            'End If
            If S.Estado <> "" Then
                xml += DaClaveEstadosMexico(S.Estado) + "|"
            End If
            If S.Pais <> "" Then
                xml += "MEX" + "|"
            End If
            If S.CP <> "" Then
                xml += S.CP + "|"
            End If
            'Termina Emisor
        End If
        'Propietario
        If motivoTraslado = "05" Then
            Idspropietarios = p.buscaComplemento(idComplemento)
            If Idspropietarios.Length > 0 Then
                For Each id As Integer In Idspropietarios
                    p.buscar(id)
                    xml += p.numRegIdTrib + "|"
                    xml += p.clavePais + "|"
                Next
            End If
        End If
        'Termina propietario

        'Receptor
        xml += r.numRegIdTrib + "|"
        If VerCFDI = "3.3" Then
            If C.Direccion <> "" Then
                xml += C.Direccion.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            End If
            If C.NoExterior <> "" Then
                xml += C.NoExterior + "|"
            End If
            If C.NoInterior <> "" Then
                xml += C.NoInterior + "|"
            End If
            If C.Colonia <> "" Then
                xml += C.Colonia.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            End If
            If C.Ciudad <> "" Then
                xml += C.Ciudad.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            End If
            If C.ReferenciaDomicilio <> "" Then
                xml += C.ReferenciaDomicilio.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            End If
            If C.Municipio <> "" Then
                xml += C.Municipio.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            End If
            If C.Estado <> "" Then
                xml += DaClaveEstadosMexico(C.Estado) + "|"
            End If
            If r.clave_Pais <> "" Then
                xml += r.clave_Pais + "|"
            End If
            If C.CP <> "" Then
                xml += C.CP + "|"
            End If
            'Termina Receptor
        End If
        'destinatario
        If d.buscaComplemento(idComplemento) Then
            If d.numRegIdTrip <> "" Then
                xml += d.numRegIdTrip + "|"
            End If
            If d.nombre <> "" Then
                xml += d.nombre + "|"
            End If
            dom.buscaDestinatario(d.idDestinatario)
            xml += dom.calle.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            If dom.numExterior <> "" Then
                xml += "|" + dom.numExterior + "|"
            End If
            If dom.numInterior <> "" Then
                xml += dom.numInterior + "|"
            End If
            If dom.colonia <> "" Then
                xml += dom.colonia.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            End If
            If dom.localidad <> "" Then
                xml += dom.localidad.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            End If
            If dom.referencia <> "" Then
                xml += dom.referencia.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            End If
            If dom.municipio <> "" Then
                xml += dom.municipio.Replace(".", "").Replace("(", "").Replace(")", "") + "|"
            End If
            xml += dom.estado + "|"
            xml += dom.pais + "|"
            xml += dom.cp + "|"
        End If
        'termina destinatario

        'Mercancias
        IdsMercancia = m.buscaComplemento(idComplemento)
        If IdsMercancia.Length > 0 Then

            For Each id As Integer In IdsMercancia
                m.buscar(id)

                xml += m.noIdentificacion + "|"
                If m.fraccionArancelaria <> "" Then
                    xml += m.fraccionArancelaria + "|"
                End If
                If m.cantidadAduana <> 0 Then
                    xml += m.cantidadAduana.ToString() + "|"
                End If
                If m.unidadAduana <> "" Then
                    xml += m.unidadAduana + "|"
                End If
                If m.valorUnitarioAduana <> 0 Then
                    xml += Format(m.valorUnitarioAduana, "0.00") + "|"
                End If
                xml += m.valorDolares.ToString("0.00") + "|" + vbCrLf
                IdsDescripciones = des.buscaMercancia(m.idMercancia)
                If IdsDescripciones.Length > 0 Then
                    For Each x As Integer In IdsDescripciones
                        des.buscar(x)
                        xml += des.marca + "|"
                        If des.modelo <> "" Then
                            xml += des.modelo + "|"
                        End If
                        If des.submodelo <> "" Then
                            xml += des.submodelo + "|"
                        End If
                        If des.numeroSerie <> "" Then
                            xml += des.numeroSerie + "|"
                        End If
                    Next
                End If
            Next
        End If
        'Termina Mercancias
        Return xml
    End Function
End Class
