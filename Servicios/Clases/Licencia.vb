Public Class Licencia
    Public MySqlconE As MySql.Data.MySqlClient.MySqlConnection
    Dim Comm As MySql.Data.MySqlClient.MySqlCommand
    Public Id As Integer
    Public IdCliente As Integer
    Public TipoVersion As ULong
    Public Licencia As String
    Public Activada As Byte
    Public Comentario As String
    Public Conectado As Boolean
    Public NombreCliente As String
    Public ClienteCorreo As String
    Public FechaInstalacion As String
    Public FechaUltimaEntrada As String
    Public Fecha As String
    Public RFC As String
    Public TipoVerFinal As Byte
    Public ConPV As Boolean
    Public Lic30dias As Boolean
    Public ConLic As Boolean
    Public Bancos As Boolean
    Public Servicios As Boolean
    Public Nomina As Boolean
    Public Gastos As Boolean
    Public Empenios As Boolean
    Public ServiciosInterno As Boolean
    Public MensajeError As String
    Public Distribuidor As Integer
    Public nombre As String
    Public contacto As String
    Public telefono As String
    Public celular As String
    Public email As String
    Public direccion As String
    Public comentarioDis As String
    Public IDDis As Integer
    Public IDTimbre As Integer
    Public cantidadTimbre As Integer
    Public fechaTimbre As String
    Public IDLicencia As Integer
    Public distribuidorNombre As String
    Public Tel As String
    Public Contabilidad As Boolean
    Public Fertilizantes As Boolean
    Public Validador As Boolean
    Public Semillas As Boolean
    Public Extra As Boolean
    Public Integracion As Boolean
    Public Usuarios As Boolean
    Public Restaurant As Boolean
    Public Sub New(ByVal pServidor As String, ByVal pConectar As Boolean)
        Try
            If pConectar Then
                Conectar(pServidor, False)
            End If
        Catch ex As Exception
            Conectado = False
            MsgBox("No se pudo establecer una conexion con el servidor de licencias.", MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Public Sub Conectar(ByVal pServidor As String, pSinMensaje As Boolean)
        Try
            MySqlconE = New MySql.Data.MySqlClient.MySqlConnection("Server=" + pServidor + ";Database=db_licencias;Uid=root;Pwd=masterdb;Port=3306")
            Comm = New MySql.Data.MySqlClient.MySqlCommand
            MySqlconE.Open()
            Comm.Connection = MySqlconE
            Conectado = True
        Catch ex As Exception
            If pSinMensaje = False Then MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Conectado = False
        End Try
    End Sub
    Public Function GuardaCliente(ByVal pNombre As String, ByVal pRFC As String, ByVal pCorreo As String, ByVal pDis As Integer, ByVal pTelefono As String) As Integer
        Comm.CommandText = "insert into tblclientes(nombre,rfc,correo,distribuidor,telefono) values('" + Replace(pNombre, "'", "''") + "','" + Replace(pRFC, "'", "''") + "','" + Replace(pCorreo, "'", "''") + "'," + pDis.ToString + ",'" + Replace(pTelefono, "'", "''") + "'); select max(idcliente) from tblclientes;"
        IdCliente = Comm.ExecuteScalar
        Return IdCliente
    End Function
    Public Sub ModificaCliente(ByVal pIdCliente As Integer, ByVal pNombre As String, ByVal pRFC As String, ByVal pCorreo As String, ByVal pDis As Integer, ByVal pTelefono As String)
        Comm.CommandText = "update tblclientes set nombre='" + Replace(pNombre, "'", "''") + "',rfc='" + Replace(pRFC, "'", "''") + "',correo='" + Replace(pCorreo, "'", "''") + "', distribuidor=" + pDis.ToString + ", telefono='" + Replace(pTelefono, "'", "''") + "' where idcliente=" + pIdCliente.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarCliente(ByVal pIdCliente As Integer)
        Comm.CommandText = "delete from tbllicencias where idcliente=" + pIdCliente.ToString + ";delete from tblclientes where idcliente=" + pIdCliente.ToString
        Comm.ExecuteNonQuery()
        IdCliente = 0
    End Sub
    Public Function TieneLicencias(ByVal pidCliente As Integer) As Boolean
        Comm.CommandText = "select count(id) from tbllicencias where idciente=" + pidCliente.ToString
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function CreaLicencia() As String
        Dim Lic As String
        Randomize()
        Dim R As New Random
        Lic = Chr(R.Next(65, 90))
        Lic += Chr(R.Next(65, 90))
        Lic += Chr(R.Next(48, 57))
        Lic += Chr(R.Next(65, 90))
        Lic += Chr(R.Next(48, 57))
        Lic += Chr(R.Next(65, 90))
        Lic += Chr(R.Next(65, 90))
        Lic += Chr(R.Next(48, 57))
        Lic += Chr(R.Next(65, 90))
        Lic += Chr(R.Next(65, 90))
        Return Lic
    End Function
    Public Sub GuardarLicencia(ByVal pIdcliente As Integer, ByVal pTipoVersion As Integer, ByVal pLicencia As String, ByVal pFecha As String, ByVal pComentario As String)
        Comm.CommandText = "insert into tbllicencias(idcliente,tipoversion,licencia,activada,fecha,comentario) values(" + pIdcliente.ToString + "," + pTipoVersion.ToString + ",'" + Replace(pLicencia, "'", "''") + "',0,'" + Replace(pFecha, "'", "''") + "','" + Replace(pComentario, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarLicencia(ByVal pId As Integer)
        Comm.CommandText = "delete from tbllicencias where id=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Private Sub ActivaLicencia(ByVal pId As Integer)
        Comm.CommandText = "update tbllicencias set activada=1 where id=" + pId.ToString
        Comm.ExecuteNonQuery()
        Activada = 1
    End Sub
    Public Function ChecaClienteRepetido(ByVal pRFC As String) As Boolean
        Comm.CommandText = "select count(nombre) from tblclientes where rfc like '" + Replace(pRFC, "'", "''") + "'"
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function ChecaLienciaRepetida(ByVal pLicencia As String) As Boolean
        Comm.CommandText = "select count(licencia) from tbllicencias where licencia like '" + Replace(pLicencia, "'", "''") + "'"
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function ConsultaLicencia(ByVal pLicencia As String) As Boolean
        Comm.CommandText = "select id from tbllicencias where licencia like '" + Replace(pLicencia, "'", "''") + "'"
        Id = Comm.ExecuteScalar
        If Id = 0 Then
            Return False
        Else
            LlenaDatosLicencia(Id)
            Return True
        End If
    End Function
    Public Sub CierraConexion()
        Comm.Dispose()
        MySqlconE.Close()
        MySqlconE.Dispose()
    End Sub
    Public Sub LlenaDatosLicencia(ByVal pId As Integer)
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbllicencias where id=" + pId.ToString
        DR = Comm.ExecuteReader
        If DR.Read Then
            Licencia = DR("licencia")
            Activada = DR("activada")
            TipoVersion = DR("tipoversion")
            Fecha = DR("fecha")
            IdCliente = DR("idcliente")
            Comentario = DR("comentario")
        End If
        DR.Close()
    End Sub
    Public Sub LlenaDatosCliente(ByVal pIdCliente As Integer)
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblclientes where idcliente=" + pIdCliente.ToString
        DR = Comm.ExecuteReader
        If DR.Read Then
            NombreCliente = DR("nombre")
            RFC = DR("rfc")
            ClienteCorreo = DR("correo")
            Distribuidor = DR("distribuidor")
            Tel = DR("telefono")
        End If

        DR.Close()
        Comm.CommandText = "select nombre from tbldistribuidor where ID=" + Distribuidor.ToString
        distribuidorNombre = Comm.ExecuteScalar

    End Sub
    Public Function ConsultaClientes(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idcliente,rfc,nombre from tblclientes where concat(rfc,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientes")
        Return DS.Tables("tblclientes").DefaultView
    End Function
    'Public Function ClientesRep(ByVal pActivada As Integer, ByVal pfechaI As String, ByVal pfechaF As String, ByVal pTipo As Integer) As DataView
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select tblclientes.idcliente,tblclientes.rfc,tblclientes.nombre, tbldistribuidor.nombre as distribuidor,tbllicencias.tipoversion, tbllicencias.licencia, tbllicencias.activada,tbllicencias.fecha  from tbllicencias inner join tblclientes on tbllicencias.idcliente= tblclientes.idcliente inner join tbldistribuidor on tblclientes.distribuidor=tbldistribuidor.ID where tbllicencias.fecha>='" + pfechaI.ToString + "' and tbllicencias.fecha<='" + pfechaF.ToString + "'"
    '    If pActivada <> -1 Then
    '        Comm.CommandText += " and tbllicencias.activada=" + pActivada.ToString
    '    End If
    '    If pTipo <> -1 Then
    '        Comm.CommandText += " and tbllicencias.tipoversion=" + pTipo.ToString
    '    End If
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblclientes")
    '    'DS.WriteXmlSchema("tblReporteLicencia.xml")
    '    Return DS.Tables("tblclientes").DefaultView
    'End Function
    Public Function ClientesRep(ByVal pActivada As Integer, ByVal pfechaI As String, ByVal pfechaF As String, ByVal ptipo As Integer, ByVal pCompleto As Boolean, ByVal pfacturacion As Boolean, ByVal pDias As Boolean, ByVal pgastos As Boolean, ByVal pbancos As Boolean, ByVal pEmpenios As Boolean, ByVal pNomina As Boolean, ByVal pPuntodeVenta As Boolean, ByVal pServicios As Boolean, ByVal pLicencias As Boolean, ByVal pConector As Boolean, ByVal pSoloFac As Boolean, ByVal pServiciosInt As Boolean, ByVal pdistribuidor As Integer, ByVal pContabilidad As Boolean, ByVal pFertilizantes As Boolean, pValidador As Boolean, pSemillas As Boolean, pExtra As Boolean, pUsuarios As Boolean, pIntegracion As Boolean, pRestaurant As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbllicencias.idcliente, tbllicencias.tipoversion, tbllicencias.licencia, tbllicencias.activada,tbllicencias.fecha, tblclientes.distribuidor   from tbllicencias  inner join tblclientes on tbllicencias.idcliente=tblclientes.idcliente where tbllicencias.fecha>='" + pfechaI.ToString + "' and tbllicencias.fecha<='" + pfechaF.ToString + "'"
        If pActivada <> -1 Then
            Comm.CommandText += " and tbllicencias.activada=" + pActivada.ToString
        End If
        If ptipo <> -1 Then
            If pCompleto Then
                '   Comm.CommandText += " and tbllicencias.tipoversion=" + pTipo.ToString
                Comm.CommandText += " and (tbllicencias.tipoversion & 1)<>0"
            End If
            If pfacturacion Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 4)<>0"
            End If

            If pDias Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 32)<>0"
            End If
            If pgastos Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 1024)<>0"
            End If
            If pbancos Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 128)<>0"
            End If
            If pEmpenios Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 2048)<>0"
            End If
            If pNomina Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 256)<>0"
            End If
            If pPuntodeVenta Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 8)<>0"
            End If
            If pServicios Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 512)<>0"
            End If
            If pLicencias Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 16)<>0"
            End If
            If pConector Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 64)<>0"
            End If
            If pSoloFac Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 2)<>0"
            End If
            If pServiciosInt Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 4096)<>0"
            End If
            If pContabilidad Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 8192)<>0"
            End If
            If pFertilizantes Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 16384)<>0"
            End If
            If pValidador Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 32768)<>0"
            End If
            '    "if(tipoversion & 65536,'X','') as Semi," + _
            '"if(tipoversion & 131072,'X','') as Ext," + _
            '"if(tipoversion & 262144,'X','') as Intgrl," + _
            '"if(tipoversion & 524288,'X','') as Users," + _
            If pSemillas Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 65536)<>0"
            End If
            If pExtra Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 131072)<>0"
            End If
            If pUsuarios Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 524288)<>0"
            End If
            If pRestaurant Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 1048576)<>0"
            End If
            If pIntegracion Then
                Comm.CommandText += " and (tbllicencias.tipoversion & 262144)<>0"
            End If
        End If
        'Comm.CommandText += " and tbllicencias.fecha>='" + pfechaI + "' and tbllicencias.fecha<='" + pfechaF + "'"
        If pdistribuidor <> 0 Then
            Comm.CommandText += " and tblclientes.distribuidor=" + pdistribuidor.ToString
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientes")
        'DS.WriteXmlSchema("tblReporteLicencia.xml")
        Return DS.Tables("tblclientes").DefaultView
    End Function
    Public Function ClientesRepDistri(ByVal pdistribuidor As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblclientes.idcliente, tblclientes.nombre, tblclientes.rfc, tblclientes.correo, tblclientes.telefono, tbldistribuidor.nombre from tblclientes inner join tbldistribuidor on tblclientes.distribuidor=tbldistribuidor.ID "
        
        If pdistribuidor <> 0 Then
            Comm.CommandText += " and tblclientes.distribuidor=" + pdistribuidor.ToString
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientes")
        ' DS.WriteXmlSchema("tblReporteClientesRepDis.xml")
        Return DS.Tables("tblclientes").DefaultView
    End Function
    Public Function ConsultaLicencias(ByVal pIdCliente As Integer) As DataView
        Dim DS As New DataSet
        '1 completo
        '2 solo fac
        '4 fac clientes
        '8 con pv
        '16 con lic
        Comm.CommandText = "select id,licencia,fecha,if(activada=0,'No','Si') as Activa," + _
        "if(tipoversion & 1,'X','') as Com," + _
        "if(tipoversion & 2,'X','') as Solo," + _
        "if(tipoversion & 4,'X','') as FC," + _
        "if(tipoversion & 8,'X','') as PV," + _
        "if(tipoversion & 128,'X','') as Bancos," + _
        "if(tipoversion & 32,'X','') as Temp," + _
        "if(tipoversion & 64,'X','') as Conect," + _
        "if(tipoversion & 16,'X','') as Lic," + _
        "if(tipoversion & 256,'X','') as Nom," + _
        "if(tipoversion & 512,'X','') as Serv," + _
        "if(tipoversion & 1024,'X','') as Gas," + _
        "if(tipoversion & 2048,'X','') as Emp," + _
        "if(tipoversion & 4096,'X','') as SerInt," + _
        "if(tipoversion & 8192,'X','') as Cont," + _
        "if(tipoversion & 16384,'X','') as Fert," + _
        "if(tipoversion & 32768,'X','') as Val," + _
        "if(tipoversion & 65536,'X','') as Semi," + _
        "if(tipoversion & 131072,'X','') as Ext," + _
        "if(tipoversion & 262144,'X','') as Intgrl," + _
        "if(tipoversion & 524288,'X','') as Users," + _
        "if(tipoversion & 1048576,'X','') as Rest," + _
        "comentario from tbllicencias where idcliente=" + pIdCliente.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbllicencias")
        Return DS.Tables("tbllicencias").DefaultView

        '"if(tipoversion & 16384,'X','') as Fert," + _
    End Function
    Public Function Validar(ByVal pSerie As String, ByVal pNueva As Boolean) As Boolean
        Try
            Dim Serial As String
            Dim En As New Encriptador
            'Serial = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE", Microsoft.Win32.RegistryKeyPermissionCheck.ReadSubTree).OpenSubKey("Microsoft", Microsoft.Win32.RegistryKeyPermissionCheck.ReadSubTree).OpenSubKey("pverschecks", Microsoft.Win32.RegistryKeyPermissionCheck.ReadSubTree).GetValue("serial")
            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Pverschecks", "serial", Nothing) Is Nothing Then
                My.Computer.Registry.CurrentUser.CreateSubKey("SOFTWARE\Pverschecks", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree)
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Pverschecks", "serial", "No validado")
            End If
            If pNueva Then
                Serial = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Pverschecks", "serial", Nothing)
                If Serial <> "No validado" Then
                    Serial = En.Desencriptar3DES(Convert.FromBase64String(Serial))
                    Dim SN() As String
                    SN = Serial.Split("|")
                    Licencia = SN(0)
                Else
                    Licencia = ""
                End If
                If Licencia <> pSerie Then
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Pverschecks", "serial", "No validado")
                Else
                    MensajeError = "No es necesario validar la licencia actual."
                    Return False
                End If
            End If
            Serial = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Pverschecks", "serial", Nothing)
            If Serial = "No validado" Then
                Conectar("pullsystem.dyndns.org", False)
                'Conectar("SERVIDOR")
                If Conectado Then
                    If ConsultaLicencia(pSerie) Then
                        LlenaDatosCliente(IdCliente)
                        If Activada = 1 Then
                            MensajeError = "Esta licencia ya se encuentra activada. Solicite una nueva licencia."
                            Return False
                        Else
                            ActivaLicencia(Id)
                            Serial = Licencia + "|" + Activada.ToString + "|" + TipoVersion.ToString + "|" + NombreCliente + "|" + RFC + "|" + Format(Date.Now, "yyyy/MM/dd")
                            Serial = Convert.ToBase64String(En.Encriptar3DES(Serial))
                            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Pverschecks", "serial", Serial)
                        End If
                        My.Settings.serial = Licencia
                        My.Settings.ultimaejecucion = Format(Date.Now, "yyyy/MM/dd")
                        My.Settings.Save()
                        ConPV = False
                        Lic30dias = False
                        ConLic = False
                        Bancos = False
                        Servicios = False
                        Nomina = False
                        Gastos = False
                        Empenios = False
                        ServiciosInterno = False
                        Fertilizantes = False
                        Contabilidad = False
                        Validador = False
                        Semillas = False
                        Extra = False
                        Integracion = False
                        Usuarios = False
                        Restaurant = False
                        FechaInstalacion = Format(Date.Now, "yyyy/MM/dd")
                        If (TipoVersion And 1) <> 0 Then TipoVerFinal = 0
                        If (TipoVersion And 2) <> 0 Then TipoVerFinal = 1
                        If (TipoVersion And 4) <> 0 Then TipoVerFinal = 2
                        If (TipoVersion And 64) <> 0 Then TipoVerFinal = 3
                        If (TipoVersion And 1) = 0 And (TipoVersion And 2) = 0 And (TipoVersion And 4) = 0 And (TipoVersion And 64) = 0 Then
                            TipoVerFinal = 4
                        End If
                        If (TipoVersion And 8) <> 0 Then ConPV = True
                        If (TipoVersion And 32) <> 0 Then Lic30dias = True
                        If (TipoVersion And 16) <> 0 Then ConLic = True
                        If (TipoVersion And 128) <> 0 Then Bancos = True
                        If (TipoVersion And 256) <> 0 Then Nomina = True
                        If (TipoVersion And 512) <> 0 Then Servicios = True
                        If (TipoVersion And 1024) <> 0 Then Gastos = True
                        If (TipoVersion And 2048) <> 0 Then Empenios = True
                        If (TipoVersion And 4096) <> 0 Then ServiciosInterno = True
                        If (TipoVersion And 8192) <> 0 Then Contabilidad = True
                        If (TipoVersion And 16384) <> 0 Then Fertilizantes = True
                        If (TipoVersion And 32768) <> 0 Then Validador = True
                        If (TipoVersion And 65536) <> 0 Then Semillas = True
                        If (TipoVersion And 131072) <> 0 Then Extra = True
                        If (TipoVersion And 262144) <> 0 Then Integracion = True
                        If (TipoVersion And 524288) <> 0 Then Usuarios = True
                        If (TipoVersion And 1048576) <> 0 Then Restaurant = True
                        Return True
                    Else
                        'No existe la licencia
                        MensajeError = "No existe la licencia que intenta activar. Indique una licencia válida."
                        BorrarLicenciadeSistema()
                        Return False
                    End If
                Else
                    MensajeError = "No se pudo validar la licencia intente mas tarde o consulte a su proveedor."
                    Return False
                    'No se pudo conectar al servidor
                End If
            Else
                Serial = En.Desencriptar3DES(Convert.FromBase64String(Serial))
                Dim S() As String
                S = Serial.Split("|")
                Activada = CByte(S(1))
                NombreCliente = S(3)
                RFC = S(4)
                Licencia = S(0)
                TipoVersion = S(2)
                FechaInstalacion = S(5)
                ConPV = False
                ConLic = False
                Lic30dias = False
                Bancos = False
                Servicios = False
                Nomina = False
                Gastos = False
                Empenios = False
                ServiciosInterno = False
                Contabilidad = False
                Fertilizantes = False
                Validador = False
                Semillas = False
                Extra = False
                Integracion = False
                Usuarios = False
                Restaurant = False
                If (TipoVersion And 1) <> 0 Then TipoVerFinal = 0
                If (TipoVersion And 2) <> 0 Then TipoVerFinal = 1
                If (TipoVersion And 4) <> 0 Then TipoVerFinal = 2
                If (TipoVersion And 64) <> 0 Then TipoVerFinal = 3
                If (TipoVersion And 8) <> 0 Then ConPV = True
                If (TipoVersion And 32) <> 0 Then Lic30dias = True
                If (TipoVersion And 16) <> 0 Then ConLic = True
                If (TipoVersion And 128) <> 0 Then Bancos = True
                If (TipoVersion And 256) <> 0 Then Nomina = True
                If (TipoVersion And 512) <> 0 Then Servicios = True
                If (TipoVersion And 1024) <> 0 Then Gastos = True
                If (TipoVersion And 2048) <> 0 Then Empenios = True
                If (TipoVersion And 4096) <> 0 Then ServiciosInterno = True
                If (TipoVersion And 8192) <> 0 Then Contabilidad = True
                If (TipoVersion And 16384) <> 0 Then Fertilizantes = True
                If (TipoVersion And 32768) <> 0 Then Validador = True
                If (TipoVersion And 65536) <> 0 Then Semillas = True
                If (TipoVersion And 131072) <> 0 Then Extra = True
                If (TipoVersion And 262144) <> 0 Then Integracion = True
                If (TipoVersion And 524288) <> 0 Then Usuarios = True
                If (TipoVersion And 1048576) <> 0 Then Restaurant = True
                If (TipoVersion And 1) = 0 And (TipoVersion And 2) = 0 And (TipoVersion And 4) = 0 And (TipoVersion And 64) = 0 Then
                    TipoVerFinal = 4
                End If
                If Activada = 1 Then
                    Encuesta(Licencia)
                    Return True
                Else
                    Return False
                End If
                'checar licencia del registro
            End If
        Catch ex As Exception
            MensajeError = "No se pudo validar la licencia intente mas tarde o consulta a su proveedor. " + ex.Message
            Return False
        End Try
    End Function
    Public Sub BorrarLicenciadeSistema()
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Pverschecks", "serial", "No validado")
        My.Settings.serial = ""
        My.Settings.Save()
    End Sub
    Public Sub ActualizaComentario(ByVal IdLic As Integer, ByVal pComentario As String)
        Comm.CommandText = "update tbllicencias set comentario='" + Replace(pComentario, "'", "''") + "' where id=" + IdLic.ToString
        Comm.ExecuteNonQuery()
    End Sub
    'Distribuidores
    Public Sub LlenaDatos(ByVal pid As Integer)
        ID = pid
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbldistribuidor where ID=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            nombre = DReader("nombre")
            contacto = DReader("contacto")
            telefono = DReader("telefono")
            celular = DReader("celular")
            email = DReader("email")
            direccion = DReader("direccion")
            comentario = DReader("comentario")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pContacto As String, ByVal pTelefono As String, ByVal pCelular As String, ByVal pEmail As String, ByVal pDireccion As String, ByVal pComentario As String)
        nombre = pNombre
        contacto = pContacto
        telefono = pTelefono
        celular = pCelular
        email = pEmail
        direccion = pDireccion
        comentario = pComentario
        Comm.CommandText = "insert into tbldistribuidor(nombre,contacto,telefono,celular, email, direccion,comentario) values('" + Replace(nombre, "'", "''") + "','" + Replace(contacto, "'", "''") + "','" + Replace(telefono, "'", "''") + "','" + Replace(celular, "'", "''") + "','" + Replace(email, "'", "''") + "','" + Replace(direccion, "'", "''") + "','" + Replace(comentario, "'", "''") + "');"
        Comm.CommandText += "select max(ID) from tbldistribuidor;"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pId As Integer, ByVal pNombre As String, ByVal pContacto As String, ByVal pTelefono As String, ByVal pCelular As String, ByVal pEmail As String, ByVal pDireccion As String, ByVal pComentario As String)
        nombre = pNombre
        contacto = pContacto
        telefono = pTelefono
        celular = pCelular
        email = pEmail
        direccion = pDireccion
        comentario = pComentario
        ID = pId
        Comm.CommandText = "update tbldistribuidor set nombre='" + Replace(nombre, "'", "''") + "',contacto='" + Replace(contacto, "'", "''") + "',telefono='" + Replace(telefono, "'", "''") + "',celular='" + Replace(celular, "'", "''") + "', email='" + Replace(email, "'", "''") + "', direccion='" + Replace(direccion, "'", "''") + "',comentario='" + Replace(comentario, "'", "''") + "' where ID=" + ID.ToString + ";"
        Comm.ExecuteScalar()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbldistribuidor where ID=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pnombre As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tbldistribuidor  where nombre like '%" + pnombre + "%' or contacto like '%" + pnombre + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldistribuidor")
        Return DS.Tables("tbldistribuidor").DefaultView
    End Function
    Public Function ConsultaCombo(ByVal pnombre As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select ID, nombre from tbldistribuidor  where nombre like '%" + pnombre + "%' or contacto like '%" + pnombre + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldistribuidor")
        Return DS.Tables("tbldistribuidor").DefaultView
    End Function

    Public Sub GuardarTimbre(ByVal pIDLicencia As Integer, ByVal pcantidad As Integer, ByVal pFecha As String)
        cantidadTimbre = pcantidad
        fechaTimbre = pFecha
        IDLicencia = pIDLicencia
        Comm.CommandText = "insert into tbltimbres(idLicencia,cantidad,fecha) values(" + IDLicencia.ToString + "," + cantidadTimbre.ToString + ",'" + Replace(fechaTimbre, "'", "''") + "');"
        Comm.CommandText += "select max(ID) from tbltimbres;"
        IDTimbre = Comm.ExecuteScalar
    End Sub
    Public Sub ModificarTimbre(ByVal pID As Integer, ByVal pIDLicencia As Integer, ByVal pcantidad As Integer, ByVal pFecha As String)
        cantidadTimbre = pcantidad
        fechaTimbre = pFecha
        IDLicencia = pIDLicencia
        IDTimbre = pID
        Comm.CommandText = "update tbltimbres set idLicencia=" + IDLicencia.ToString + ",cantidad=" + cantidadTimbre.ToString + ",fecha='" + Replace(fechaTimbre, "'", "''") + "' where ID=" + IDTimbre.ToString + ";"
        Comm.ExecuteScalar
    End Sub
    Public Sub EliminarTimbre(ByVal pID As Integer)
        Comm.CommandText = "delete from tbltimbres where ID=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaTimbres(ByVal pidLic) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tbltimbres  where idLicencia =" + pidLic.ToString + " order by fecha DESC;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldistribuidor")
        Return DS.Tables("tbldistribuidor").DefaultView
    End Function
    Public Sub LlenaDatosTimbre(ByVal pid As Integer)
        IDTimbre = pid
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbltimbres where ID=" + IDTimbre.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            cantidadTimbre = DReader("cantidad")
            fechaTimbre = DReader("fecha")
        End If
        DReader.Close()
    End Sub
    Public Function ClientesLicencias(ByVal pIDCliente As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbllicencias.idcliente as ID, concat(tbllicencias.tipoversion,'') tipoVersion, tbllicencias.licencia as lic, tbllicencias.activada as activada,tbllicencias.fecha as fecha, tblclientes.distribuidor as dis    from tbllicencias  inner join tblclientes on tbllicencias.idcliente=tblclientes.idcliente where  tbllicencias.idcliente=" + pIDCliente.ToString
        Comm.CommandText += " order by tbllicencias.fecha"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientes")
        'DS.WriteXmlSchema("tblReporteLicenciaCliente.xml")
        Return DS.Tables("tblclientes").DefaultView
    End Function
    Public Sub Encuesta(pLicencia As String)
        Try
            If My.Settings.encuestado = False Then
                Conectar("pullsystem.dyndns.org", True)
                If Conectado Then
                    Comm.CommandText = "insert into tblencuesta(licencia,res,ram,cpu,os) values('" + pLicencia + "','" + My.Computer.Screen.Bounds.Width.ToString + "x" + My.Computer.Screen.Bounds.Height.ToString + "','" + My.Computer.Info.TotalPhysicalMemory.ToString + "','','" + My.Computer.Info.OSFullName.Replace("'", "''") + "')"
                    Comm.ExecuteNonQuery()
                    My.Settings.encuestado = True
                    My.Settings.Save()
                    MySqlconE.Close()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
