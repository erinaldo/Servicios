Imports MySql.Data.MySqlClient
Public Class dbNominas
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdTrabajador As Integer
    Public Fecha As String
    Public Trabajador As dbTrabajadores
    Public Folio As Integer
    Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    Public Estado As Byte
    Public Desglosar As Byte
    Public IdSucursal As Integer
    Public Serie As String
    Public Subtotal As Double
    Public TotalIva As Double
    Public TotalNota As Double
    Public TipodeCambio As Double
    Public NoAprobacion As String
    Public YearAprobacion As String
    Public NoCertificado As String
    Public EsElectronica As Byte
    Public IdMoneda As Integer
    Public Aplicado As Double
    Public HoraCancelado As String
    Public FechaCancelado As String
    Public TotalISR As Double
    Public TotalIvaRetenido As Double
    Public ISR As Double
    Public IvaRetenido As Double
    Public uuid As String
    Public FechaTimbrado As String
    Public SelloCFD As String
    Public NoCertificadoSAT As String
    Public SelloSAT As String
    Public IdConcepto As Integer
    Public Comentario As String
    Public FechaPago As String
    Public FechaInicialPago As String
    Public FechaFinalPAgo As String
    'Public NumDiasPagados As Integer
    Public Banco As Integer
    Public Clabe As String
    Public TotalGravadoP As Double
    Public TotalExentoP As Double
    Public TotalGravadoD As Double
    Public TotalExentoD As Double
    Public TotalISRcon As Double
    Public TotalHorasExtra As Double
    Public totalIncapacidades As Double
    Public DiasPagados As Double
    Public Antiguedad As Integer
    Public HayConceptos As Boolean
    Public Importado As Byte
    Public IdsPorTimbrar As Collection
    Public Idforma As Integer
    Public totalDeducciones As Double
    Public tipoNomina As String
    Public totalPercepciones As Double
    Public totalOtrosPagos As Double
    Public origenRecurso As String
    Public montoRecurso As Double
    Public TotalSueldos As Double
    Public TotalJubilacion As Double
    Public TotalSeparacion As Double
    Public TotalOtrasDeducciones As Double
    Public TotalImpuestosRetenidos As Double

    Public HayJubilacionPE As Boolean
    Public HayJubilacionPP As Boolean
    Public HaySeparacionP As Boolean
    Dim CantidadOtrosPagos As Integer
    Dim nominaT As New dbNominaTRabajador(MySqlcon)
    Private Structure HEx
        Dim IdDetalle As Integer
        Dim Dias As Integer
        Dim TipoHoras As String
        Dim HorasExtra As Integer
        Dim ImportePagado As Double
    End Structure

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdTrabajador = -1
        Fecha = ""
        Hora = ""
        Folio = 0
        Desglosar = 0
        Iva = 0
        TotalaPagar = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        Serie = ""
        NoAprobacion = ""
        YearAprobacion = ""
        NoCertificado = ""
        EsElectronica = 0
        TipodeCambio = 0
        IdMoneda = 0
        Aplicado = 0
        HoraCancelado = 0
        FechaCancelado = 0
        ISR = 0
        IvaRetenido = 0
        Comm.Connection = Conexion
        IdConcepto = 0
        Comentario = ""
        FechaPago = ""
        FechaFinalPAgo = ""
        FechaInicialPago = ""
        Banco = 0
        Clabe = ""
        DiasPagados = 0
        Antiguedad = 0
        Importado = 0
        Idforma = 0
        tipoNomina = ""
        Trabajador = New dbTrabajadores(Comm.Connection)
    End Sub
    Public Sub Reset()
        IdTrabajador = -1
        Fecha = ""
        Hora = ""
        Folio = 0
        Desglosar = 0
        Iva = 0
        TotalaPagar = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        Serie = ""
        NoAprobacion = ""
        YearAprobacion = ""
        NoCertificado = ""
        EsElectronica = 0
        TipodeCambio = 0
        IdMoneda = 0
        Aplicado = 0
        HoraCancelado = 0
        FechaCancelado = 0
        ISR = 0
        IvaRetenido = 0
        IdConcepto = 0
        Comentario = ""
        FechaPago = ""
        FechaFinalPAgo = ""
        FechaInicialPago = ""
        Banco = 0
        Clabe = ""
        DiasPagados = 0
        Antiguedad = 0
        Importado = 0
        Idforma = 0
        origenRecurso = "NA"
        montoRecurso = 0
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblnominas where idnomina=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdTrabajador = DReader("idtrabajador")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            'Desglosar = DReader("desglosar")
            Iva = DReader("iva")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            Serie = DReader("serie")
            TipodeCambio = DReader("tipodecambio")
            NoAprobacion = DReader("noaprobacion")
            YearAprobacion = DReader("yearaprobacion")
            NoCertificado = DReader("nocertificado")
            EsElectronica = DReader("eselectronica")
            IdMoneda = DReader("idmoneda")
            'Aplicado = DReader("aplicado")
            FechaCancelado = DReader("fechacancelado")
            HoraCancelado = DReader("horacancelado")
            ISR = DReader("isr")
            IvaRetenido = DReader("ivaretenido")
            'IdConcepto = DReader("idconcepto")
            Comentario = DReader("comentario")
            FechaPago = DReader("fechapago")
            FechaInicialPago = DReader("fechainicialpago")
            FechaFinalPAgo = DReader("fechafinalpago")
            Banco = DReader("banco")
            Clabe = DReader("clabe")
            DiasPagados = DReader("diaspagados")
            Antiguedad = DReader("antiguedad")
            Importado = DReader("importado")
            Idforma = DReader("idforma")
            tipoNomina = DReader("tipoNomina")
            origenRecurso = DReader("origenrecurso")
            montoRecurso = DReader("monto")
        End If
        DReader.Close()
        Trabajador = New dbTrabajadores(IdTrabajador, Comm.Connection)
    End Sub
    'Public Function ExisteFolio(ByVal pfolio As Integer, Optional ByVal idventa As Integer = -1) As Boolean
    '    Folio = pfolio
    '    Comm.CommandText = "select count(folio) from tblventas where folio=" + Folio.ToString + If(idventa = -1, "", " and idventa<>" + CStr(idventa))
    '    If Comm.ExecuteScalar = 0 Then Return False Else Return True
    'End Function

    Public Sub Guardar(ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pidSucursal As Integer, ByVal pSerie As String, ByVal pTipoDeCambio As Double, ByVal pNoAprobacion As String, ByVal pYearAprobacion As String, ByVal pNoCertificado As String, ByVal pEselectronica As Byte, ByVal pIdMoneda As Integer, ByVal pIsr As Double, ByVal pIvaRetenido As Double, ByVal pFechaPago As String, ByVal pFechaInicialPago As String, ByVal pFechaFinalPago As String, ByVal pBanco As Integer, ByVal pClabe As String, ByVal pDiasPagados As Double, ByVal pantiguedad As Integer, ByVal pImportado As Byte, ByVal pIdForma As Integer, ByVal ptipoNomina As String, ByVal porigenRecurso As String, ByVal pmonto As Double)
        IdTrabajador = pIdCliente
        Fecha = pFecha
        Folio = pFolio
        Iva = pIva
        IdSucursal = pidSucursal
        Serie = pSerie
        TipodeCambio = pTipoDeCambio
        NoAprobacion = pNoAprobacion
        NoCertificado = pNoCertificado
        YearAprobacion = pYearAprobacion
        EsElectronica = pEselectronica
        IdMoneda = pIdMoneda
        ISR = pIsr
        IvaRetenido = pIvaRetenido
        FechaPago = pFechaPago
        FechaInicialPago = pFechaInicialPago
        FechaFinalPAgo = pFechaFinalPago
        Banco = pBanco
        DiasPagados = pDiasPagados
        Importado = pImportado
        Antiguedad = pantiguedad
        Idforma = pIdForma
        tipoNomina = ptipoNomina
        origenRecurso = porigenRecurso
        montoRecurso = pmonto
        If Banco < 0 Then Banco = 0
        Clabe = pClabe
        Comm.CommandText = "insert into tblnominas(idtrabajador,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,serie,tipodecambio,noaprobacion,yearaprobacion,nocertificado,eselectronica,idmoneda,fechacancelado,horacancelado,isr,ivaretenido,comentario,fechapago,fechainicialpago,fechafinalpago,banco,clabe,diaspagados,antiguedad,importado,idforma,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,tipoNomina,origenrecurso,monto) values(" + IdTrabajador.ToString + ",'" + Fecha + "'," + Folio.ToString + ",0,'" + Format(TimeOfDay, "HH:mm:ss") + "'," + CStr(Estados.SinGuardar) + "," + Iva.ToString + ",0," + IdSucursal.ToString + ",'" + Replace(Serie, "'", "''") + "'," + TipodeCambio.ToString + ",'" + Replace(NoAprobacion, "'", "''") + "','" + Replace(YearAprobacion, "'", "''") + "','" + Replace(NoCertificado, "'", "''") + "'," + EsElectronica.ToString + "," + IdMoneda.ToString + ",'',''," + ISR.ToString + "," + IvaRetenido.ToString + ",'','" + FechaPago + "','" + FechaInicialPago + "','" + FechaFinalPAgo + "'," + Banco.ToString + ",'" + Replace(Clabe, "'", "''") + "'," + DiasPagados.ToString + "," + Antiguedad.ToString + "," + Importado.ToString + "," + Idforma.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "','" + tipoNomina + "','" + origenRecurso + "'," + montoRecurso.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idnomina) from tblnominas"
        ID = Comm.ExecuteScalar
    End Sub

    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pEstado As Byte, ByVal pTotal As Double, ByVal pTotalaPagar As Double, ByVal pIdCliente As Integer, ByVal pSerie As String, ByVal pTipodeCambio As Double, ByVal pNoAprobacion As String, ByVal pYearAprobacion As String, ByVal pNoCertificado As String, ByVal pÌdMoneda As Integer, ByVal pIdConcepto As Integer, ByVal pEselectronica As Byte, ByVal pComentario As String, ByVal pFechaPago As String, ByVal pFechaInicialPago As String, ByVal pFechaFinalPago As String, ByVal pDiasPagados As Double, ByVal pAntiguedad As Integer, ByVal pIdForma As Integer, ByVal ptipoNomina As String, ByVal porigenRecurso As String, ByVal pmonto As Double)
        ID = pID
        Fecha = pFecha
        Folio = pFolio
        Desglosar = pDesglosar
        Iva = pIva
        Estado = pEstado
        Total = pTotal
        TotalaPagar = pTotalaPagar
        IdTrabajador = pIdCliente
        Serie = pSerie
        TipodeCambio = pTipodeCambio
        NoAprobacion = pNoAprobacion
        NoCertificado = pNoCertificado
        YearAprobacion = pYearAprobacion
        IdMoneda = pÌdMoneda
        Estado = pEstado
        EsElectronica = pEselectronica
        IdConcepto = pIdConcepto
        Comentario = pComentario
        FechaPago = pFechaPago
        FechaInicialPago = pFechaInicialPago
        FechaFinalPAgo = pFechaFinalPago
        Antiguedad = pAntiguedad
        DiasPagados = pDiasPagados
        Idforma = pIdForma
        tipoNomina = ptipoNomina
        origenRecurso = porigenRecurso
        montoRecurso = pmonto
        Comm.CommandText = "update tblnominas set fecha='" + Fecha + "',folio=" + Folio.ToString + ",iva=" + Iva.ToString + ",estado=" + Estado.ToString + ",total=" + Total.ToString + ",totalapagar=" + TotalaPagar.ToString + ",idtrabajador=" + pIdCliente.ToString + ",serie='" + Replace(Serie, "'", "''") + "',tipodecambio=" + TipodeCambio.ToString + ",noaprobacion='" + Replace(NoAprobacion, "'", "''") + "',yearaprobacion='" + Replace(YearAprobacion, "'", "''") + "',nocertificado='" + Replace(NoCertificado, "'", "''") + "',idmoneda=" + IdMoneda.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',eselectronica=" + EsElectronica.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "',fechapago='" + FechaPago + "',fechainicialpago='" + FechaInicialPago + "',fechafinalpago='" + FechaFinalPAgo + "',diaspagados=" + DiasPagados.ToString + ",antiguedad=" + Antiguedad.ToString + ",idforma=" + pIdForma.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "', tipoNomina='" + tipoNomina + "', origenrecurso='" + origenRecurso + "', monto=" + montoRecurso.ToString
        If Estado <> 4 Then
            Comm.CommandText += ",hora='" + Format(TimeOfDay, "HH:mm:ss") + "'"
        End If
        Comm.CommandText += " where idnomina=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaComentario(ByVal pidnota As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblnominas set comentario='" + Replace(pTexto, "'", "''") + "' where idnomina=" + pidnota.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblnominas where idnomina=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, Optional ByVal pNombreClave As String = "", Optional ByVal pFolio As String = "", Optional ByVal pEstado As Byte = Estados.Inicio, Optional ByVal pSinAplicar As Boolean = False) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblnotasdecargo.idnomina,tblnotasdecargo.fecha,tblnotasdecargo.serie,tblnotasdecargo.folio,tbltrabajadores.numeroempleado,tbltrabajadores.nombre as Trabajador,tblnotasdecargo.totalapagar,case tblnotasdecargo.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado  from tblnominas as tblnotasdecargo inner join tbltrabajadores on tblnotasdecargo.idtrabajador=tbltrabajadores.idtrabajador where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and tbltrabajadores.nombre like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblnotasdecargo.serie,convert(tblnotasdecargo.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado <> Estados.Inicio Then
            Comm.CommandText += " and tblnotasdecargo.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblnotasdecargo.estado<>1"
        End If
        Comm.CommandText += " order by tblnotasdecargo.fecha desc,tblnotasdecargo.serie,tblnotasdecargo.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblnominas")
        Return DS.Tables("tblnominas").DefaultView
    End Function

    'Public Function ConsultaxCliente(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdCliente As Integer, ByVal pPorFecha As Boolean, ByVal pFolio As String, ByVal pOrdenDecendente As Boolean) As DataView
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select tblnotasdecargo.idcargo,tblnotasdecargo.fecha,tblnotasdecargo.serie,tblnotasdecargo.folio,tblclientes.clave,tblclientes.nombre as Cliente,tblnotasdecargo.totalapagar,tblnotasdecargo.aplicado,case tblnotasdecargo.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado  from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.estado<>1"
    '    If pPorFecha Then
    '        Comm.CommandText += " fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
    '    End If
    '    If pFolio <> "" Then
    '        Comm.CommandText += " and concat(tblnotasdecargo.serie,convert(tblnotasdecargo.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
    '    End If
    '    If IdTrabajador <> 0 Then
    '        Comm.CommandText += " and tblnotasdecargo.idcliente=" + pIdCliente.ToString
    '    End If
    '    If pOrdenDecendente Then
    '        Comm.CommandText += " order by tblnotasdecargo.serie desc,tblnotasdecargo.folio desc,tblnotasdecargo.fecha desc"
    '    Else
    '        Comm.CommandText += " order by tblnotasdecargo.serie,tblnotasdecargo.folio,tblnotasdecargo.fecha"
    '    End If
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblnotasdecargo")
    '    Return DS.Tables("tblnotasdecargo").DefaultView
    'End Function

    'Public Function ConsultaDeudas(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidCliente As Integer, Optional ByVal pFolio As String = "") As DataView
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select tblventas.idventa,tblventas.fecha,tblventas.folio,tblventas.total,tblventas.totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "' and tblventas.facturado=1 and tblventas.credito=1 and tblventas.idcliente=" + pidCliente.ToString
    '    If pFolio <> "" Then
    '        Comm.CommandText += " and tblventas.folio like '%" + Replace(pFolio, "'", "''") + "%'"
    '    End If
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblventas")
    '    Return DS.Tables("tblventas").DefaultView
    'End Function

    Public Function DaTotal(ByVal pidCargo As Integer, ByVal pidMoneda As Integer) As Double
        'Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        'Dim Precio As Double
        'Dim IdMonedaC As Integer
        Dim Total As Double = 0
        'Dim Encontro As Double
        'Dim iIva As Double
        'Dim iTipo As Byte
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double
        Dim iIsr As Double
        Dim iIvaRetenido As Double
        Subtotal = 0
        TotalIva = 0
        TotalNota = 0
        TotalExentoD = 0
        TotalExentoP = 0
        TotalGravadoD = 0
        TotalGravadoP = 0
        Comm.CommandText = "select tipodecambio from tblnominas where idnomina=" + pidCargo.ToString
        iTipoCambio = Comm.ExecuteScalar
        Comm.CommandText = "select isr from tblnominas where idnomina=" + pidCargo.ToString
        iIsr = Comm.ExecuteScalar
        Comm.CommandText = "select ivaretenido from tblnominas where idnomina=" + pidCargo.ToString
        iIvaRetenido = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select count(iddetalle) from tblnominadetalles where idnomina=" + pidCargo.ToString
        'DReader = Comm.ExecuteReader
        If Comm.ExecuteScalar = 0 Then
            HayConceptos = False
        Else
            HayConceptos = True
        End If
        'While DReader.Read
        '    IDs.Add(DReader("iddetalle"))
        '    HayConceptos = True
        'End While
        'DReader.Close()

        'While Cont <= IDs.Count
        '    Comm.CommandText = "select importegravado+importeexento from tblnominadetalles where iddetalle=" + IDs.Item(Cont).ToString
        '    Precio = Comm.ExecuteScalar
        '    Comm.CommandText = "select tipodetalle from tblnominadetalles where iddetalle=" + IDs.Item(Cont).ToString
        '    iTipo = Comm.ExecuteScalar
        '    'Comm.CommandText = "select iva from tblnotasdecargodetalles where iddetalle=" + IDs.Item(Cont).ToString
        '    'iIva = Comm.ExecuteScalar
        '    'Comm.CommandText = "select idmoneda from tblnotasdecargodetalles where iddetalle=" + IDs.Item(Cont).ToString
        '    'IdMonedaC = Comm.ExecuteScalar
        '    'If pidMoneda = 2 Then
        '    '    If pidMoneda <> IdMonedaC Then
        '    '        Precio = Precio * iTipoCambio
        '    '    End If
        '    'Else
        '    '    If IdMonedaC = 2 Then
        '    '        Precio = Precio / iTipoCambio
        '    '    End If
        '    'End If
        '    If iTipo = 0 Then
        '        Subtotal += Precio
        '        TotalIva += Precio
        '    Else
        '        Subtotal -= Precio
        '        TotalIva -= Precio
        '    End If
        '    'TotalIva += (Precio * (iIva / 100))
        '    Cont += 1
        'End While
        HayJubilacionPE = False
        HayJubilacionPP = False
        HaySeparacionP = False
        Comm.CommandText = "select ifnull((select sum(importeexento+importegravado) from tblnominadetalles where idnomina=" + pidCargo.ToString + " and tipodetalle=1 and tipo=2),0)"
        TotalISRcon = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(importeexento) from tblnominadetalles where idnomina=" + pidCargo.ToString + " and tipodetalle=1),0)"
        TotalExentoD = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(importegravado) from tblnominadetalles where idnomina=" + pidCargo.ToString + " and tipodetalle=1),0)"
        TotalGravadoD = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(importeexento) from tblnominadetalles where idnomina=" + pidCargo.ToString + " and tipodetalle=0),0)"
        TotalExentoP = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(importegravado) from tblnominadetalles where idnomina=" + pidCargo.ToString + " and tipodetalle=0),0)"
        TotalGravadoP = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(importepagado) from tblnominahorasextra where idnomina=" + pidCargo.ToString + "),0)"
        TotalHorasExtra = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(descuento) from tblnominaincapacidades where idnomina=" + pidCargo.ToString + "),0)"
        totalIncapacidades = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(importe) from tblnominaotrospagos where idnomina=" + pidCargo.ToString + "),0)"
        totalOtrosPagos = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select count(importe) from tblnominaotrospagos where idnomina=" + pidCargo.ToString + "),0)"
        CantidadOtrosPagos = Comm.ExecuteScalar
        'Subtotal = Subtotal '+ TotalHorasExtra - totalIncapacidades
        Comm.CommandText = "select ifnull((select sum(importegravado+importeexento) from tblnominadetalles inner join tblpercepciones on tblnominadetalles.tipo=tblpercepciones.idpercepcion where tipodetalle=0 and tblpercepciones.clave<>'022' and tblpercepciones.clave<>'023' and tblpercepciones.clave<>'025' and tblpercepciones.clave<>'039' and tblpercepciones.clave<>'044' and idnomina=" + pidCargo.ToString + "),0)"
        TotalSueldos = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(importegravado+importeexento) from tblnominadetalles inner join tblpercepciones on tblnominadetalles.tipo=tblpercepciones.idpercepcion where tipodetalle=0 and (tblpercepciones.clave='022' or tblpercepciones.clave='023' or tblpercepciones.clave='025') and idnomina=" + pidCargo.ToString + "),0)"
        TotalSeparacion = Comm.ExecuteScalar
        If TotalSeparacion <> 0 Then HaySeparacionP = True
        Dim iTotalJubilacion As Double
        Comm.CommandText = "select ifnull((select sum(importegravado+importeexento) from tblnominadetalles inner join tblpercepciones on tblnominadetalles.tipo=tblpercepciones.idpercepcion where tipodetalle=0 and tblpercepciones.clave='039' and idnomina=" + pidCargo.ToString + "),0)"
        iTotalJubilacion = Comm.ExecuteScalar
        TotalJubilacion = iTotalJubilacion
        If iTotalJubilacion <> 0 Then HayJubilacionPE = True
        Comm.CommandText = "select ifnull((select sum(importegravado+importeexento) from tblnominadetalles inner join tblpercepciones on tblnominadetalles.tipo=tblpercepciones.idpercepcion where tipodetalle=0 and tblpercepciones.clave='044' and idnomina=" + pidCargo.ToString + "),0)"
        iTotalJubilacion = Comm.ExecuteScalar
        If iTotalJubilacion <> 0 Then HayJubilacionPP = True
        TotalJubilacion += iTotalJubilacion
        Comm.CommandText = "select ifnull((select sum(importegravado+importeexento) from tblnominadetalles inner join tbldeducciones on tblnominadetalles.tipo=tbldeducciones.iddeduccion where tipodetalle=1 and tbldeducciones.clave<>'002' and idnomina=" + pidCargo.ToString + "),0)"
        TotalOtrasDeducciones = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(importegravado+importeexento) from tblnominadetalles inner join tbldeducciones on tblnominadetalles.tipo=tbldeducciones.iddeduccion where tipodetalle=1 and tbldeducciones.clave='002' and idnomina=" + pidCargo.ToString + "),0)"
        TotalImpuestosRetenidos = Comm.ExecuteScalar
        'TotalISR = Subtotal * (iIsr / 100)
        'TotalIvaRetenido = Subtotal * (iIvaRetenido / 100)
        TotalNota = TotalGravadoP + TotalExentoP - TotalGravadoD - TotalExentoD + totalOtrosPagos
        Return TotalNota
    End Function
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblnominas where serie='" + pSerie + "' and (estado=3 or estado=4) ),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As String, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblnominas where folio=" + pFolio + " and serie='" + Replace(pSerie, "'", "''") + "' and estado<>1 and estado<>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function

    'Public ReadOnly Property totalLetra(ByVal idmoneda As Integer) As String
    '    Get
    '        Dim f As New PaseLetras
    '        Return f.PASELETRAS(DaTotal(ID, idmoneda), idmoneda) + " " + [Enum].GetName(GetType(MONEDAS), idmoneda)
    '    End Get
    'End Property


    Public Function CreaCadenaOriginali32(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|3.2|"
        ID = pIdVenta
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, IdMoneda)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        'CO += Serie + "|"
        'CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        'CO += NoAprobacion + "|"
        'CO += YearAprobacion + "|"
        CO += "egreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        If HayConceptos Then
            CO += Format(TotalExentoP + TotalGravadoP, "#0.00####") + "|" 'subtotal
            CO += Format(TotalExentoD + TotalGravadoD - TotalISRcon, "#0.00####") + "|" 'descuento
        Else
            CO += Format(TotalaPagar, "#0.00####") + "|"
            CO += "0.00|" 'descuento
        End If


        'Tipo de cambio
        If IdMoneda <> 2 Then
            CO += Format(TipodeCambio, "#0.00####") + "|"
            Dim Moneda As New dbMonedas(IdMoneda, Comm.Connection)
            CO += Moneda.Abreviatura + "|"
        Else
            CO += "MXN|"
        End If
        If HayConceptos Then
            CO += Format(TotalExentoP + TotalGravadoP - TotalExentoD - TotalGravadoD + TotalHorasExtra - totalIncapacidades, "#0.00####") + "|" ' total factura con iva
        Else
            CO += Format(TotalaPagar, "#0.00####") + "|" ' total factura con iva
        End If
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'metododepago
        Dim FP As New dbFormasdePago(Idforma, Comm.Connection)
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        If Fecha < "2016/06/01" Then
            CO += Trim(FP.Nombre) + "|"
        Else
            Dim strMetodos As String = ""
            Dim MeP As New dbVentasAddMetodos(Comm.Connection)
            DR = MeP.ConsultaReader(2, ID)
            While DR.Read()
                If strMetodos <> "" Then strMetodos += ","
                If DR("clavesat") < 1000 Then
                    strMetodos += Format(DR("clavesat"), "00")
                Else
                    strMetodos += "NA"
                End If
            End While
            DR.Close()
            CO += strMetodos + "|"
        End If
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """"
        'Else
        'CO += "No aplica|"
        'End If
        'lugar de expedicion
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            CO += Trim(Sucursal.Ciudad2) + "," + Trim(Sucursal.Estado2) + "|"
        Else
            CO += Trim(Sucursal.Ciudad) + "," + Trim(Sucursal.Estado) + "|"
        End If
        'If NoCuenta <> "" And FP.Tipo = dbFormasdePago.Tipos.Contado Then CO += Trim(NoCuenta) + "|"

        'Aqui lo de parcialidades
        'proximamente

        CO += Trim(Sucursal.RFC) + "|"
        CO += Trim(Sucursal.NombreFiscal) + "|"
        CO += Trim(Sucursal.Direccion) + "|"
        CO += Trim(Sucursal.NoExterior) + "|"
        CO += Trim(Sucursal.NoInterior) + "|"
        CO += Trim(Sucursal.Colonia) + "|"
        CO += Trim(Sucursal.Ciudad) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio) + "|"
        CO += Trim(Sucursal.Municipio) + "|"
        CO += Trim(Sucursal.Estado) + "|"
        CO += Trim(Sucursal.Pais) + "|"
        CO += Trim(Sucursal.CP) + "|"

        CO += Trim(Sucursal.Direccion2) + "|"
        CO += Trim(Sucursal.NoExterior2) + "|"
        CO += Trim(Sucursal.NoInterior2) + "|"
        CO += Trim(Sucursal.Colonia2) + "|"
        CO += Trim(Sucursal.Ciudad2) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio2) + "|"
        CO += Trim(Sucursal.Municipio2) + "|"
        CO += Trim(Sucursal.Estado2) + "|"
        CO += Trim(Sucursal.Pais2) + "|"
        CO += Trim(Sucursal.CP2) + "|"

        'regimen

        Dim Pos As Integer = 0
        Dim Listo As Boolean = False
        Dim AddDir As String = ""

        While Sucursal.RegimenFiscal.Length > Pos
            If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
                CO += Trim(AddDir) + "|"
                AddDir = ""
                Listo = True
            Else
                AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
                Listo = False
            End If
            Pos += 1
        End While

        If Listo = False Then CO += Trim(AddDir) + "|"

        CO += Trim(Trabajador.RFC) + "|"
        CO += Trim(Trabajador.Nombre) + "|"
        'If Trabajador.DireccionFiscal = 0 Then
        CO += Trim(Trabajador.Direccion) + "|"
        CO += Trim(Trabajador.NoExterior) + "|"
        CO += Trim(Trabajador.NoInterior) + "|"
        CO += Trim(Trabajador.Colonia) + "|"
        CO += Trim(Trabajador.Ciudad) + "|"
        CO += Trim(Trabajador.ReferenciaDomicilio) + "|"
        CO += Trim(Trabajador.Municipio) + "|"
        CO += Trim(Trabajador.Estado) + "|"
        CO += Trim(Trabajador.Pais) + "|"
        CO += Trim(Trabajador.CP) + "|"
        'Else
        'CO += Trim(Trabajador.Direccion2) + "|"
        'CO += Trim(Trabajador.NoExterior2) + "|"
        'CO += Trim(Trabajador.NoInterior2) + "|"
        'CO += Trim(Trabajador.Colonia2) + "|"
        'CO += Trim(Trabajador.Ciudad2) + "|"
        'CO += Trim(Trabajador.ReferenciaDomicilio2) + "|"
        'CO += Trim(Trabajador.Municipio2) + "|"
        'CO += Trim(Trabajador.Estado2) + "|"
        'CO += Trim(Trabajador.Pais2) + "|"
        'CO += Trim(Trabajador.CP2) + "|"
        'End If

        'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Dim VI As New dbNotasdeCargoDetalles(MySqlcon)
        'DR = VI.ConsultaReader(ID)
        'Dim PrecioTemp As Double
        'While DR.Read
        'If DR("cantidad") <> 0 And DR("precio") <> 0 Then
        CO += "1|"
        CO += "Servicio|"
        'CO += DR("clave") + "|"
        CO += "Pago de Nómina|"
        'If DR("idmoneda") <> 2 Then
        '    CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
        '    CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
        'Else

        If HayConceptos = False Then
            CO += Format(TotalaPagar, "#0.00####") + "|"
            CO += Format(TotalaPagar, "#0.00####") + "|"
        Else
            CO += Format(TotalExentoP + TotalGravadoP, "#0.00####") + "|"
            CO += Format(TotalExentoP + TotalGravadoP, "#0.00####") + "|"
        End If

        'End If
        'End If
        'End While
        'DR.Close()

        If ISR <> 0 Or TotalISRcon <> 0 Then
            CO += "ISR|" + Format(TotalISR + TotalISRcon, "#0.00####") + "|"
        End If
        If IvaRetenido <> 0 Then
            CO += "IVA|" + Format(TotalIvaRetenido, "#0.00####") + "|"
        End If
        If ISR <> 0 Or IvaRetenido <> 0 Or TotalISRcon <> 0 Then
            CO += Format(TotalISR + TotalIvaRetenido + TotalISRcon, "#0.00####") + "|"
        End If
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        'Dim IAnt As Double
        ''DR = DaIvas(ID)

        'While DR.Read
        '    If Ivas.Contains(DR("iva").ToString) = False Then
        '        Ivas.Add(DR("iva"), DR("iva").ToString)
        '    End If
        '    If IvasImporte.Contains(DR("iva").ToString) = False Then
        '        'If DR("idmoneda") <> 2 Then
        '        '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
        '        'Else
        '        IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
        '        ' End If
        '    Else
        '        IAnt = IvasImporte(DR("iva").ToString)
        '        IvasImporte.Remove(DR("iva").ToString)
        '        'If DR("idmoneda") <> 2 Then
        '        '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
        '        'Else
        '        IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
        '        'End If
        '    End If
        'End While
        'DR.Close()
        'For Each I As Double In Ivas
        '    CO += "IVA|"
        '    CO += Format(I, "#0.00") + "|"
        '    CO += Format(IvasImporte(I.ToString), "#0.00") + "|"
        'Next

        CO += Format(0, "#0.00####") + "|"

        '-----------------Aqui lo de la nomina
        CO += "1.1|"
        'If Trabajador.RegistroPatronal
        CO += Trim(Trabajador.RegistroPatronal) + "|" 'registro patronal
        CO += Trim(Trabajador.NumeroEmpleado) + "|" 'num empleado
        CO += Trim(Trabajador.Curp) + "|" 'curp
        CO += Trim(Trabajador.TipoRegimen) + "|" 'tipo regimen
        CO += Trim(Trabajador.NumeroSeguroSocial) + "|" 'num seg social
        CO += Replace(FechaPago, "/", "-") + "|" 'fecha pago
        CO += Replace(FechaInicialPago, "/", "-") + "|" 'fecha inicial
        CO += Replace(FechaFinalPAgo, "/", "-") + "|" ' fecha final
        CO += Format(DiasPagados, "#0.00") + "|" ' numero dias
        CO += Trabajador.Departamento + "|" ' departamento
        If Banco > 0 Then
            If Clabe <> "" Then
                CO += Clabe + "|" 'Clabe
            End If
            CO += Format(Banco, "000") + "|" 'banco
        End If
        If Antiguedad <> 0 Then
            CO += Replace(Trabajador.FechaInicioLaboral, "/", "-") + "|" 'fechainiciolaboral
            CO += CStr(Antiguedad) + "|" 'anti
        End If
        CO += Trabajador.Puesto + "|" 'puesto
        CO += Trabajador.TipoContrato + "|" 'tipocontrato
        CO += Trabajador.TipoJornada + "|" 'tipo jornada
        CO += Trabajador.Periodicidad + "|" 'periodicidad pago
        If Trabajador.SalarioBaseCotApor <> 0 Then CO += Format(Trabajador.SalarioBaseCotApor, "#0.00####") + "|" 'salariobato a cot
        CO += Trabajador.RiesgoPuesto.ToString + "|" 'riesgo puesto
        If Trabajador.SalarioDiarioIntegrado <> 0 Then CO += Format(Trabajador.SalarioDiarioIntegrado, "#0.00####") + "|" 'salario diaro


        Dim VI As New dbNominasDetalles(MySqlcon)
        'Percepciones
        DR = VI.ConsultaReader(ID, 0)
        Dim Hay As Byte = 0
        While DR.Read
            If Hay = 0 Then
                'deducciones
                CO += Format(TotalGravadoP, "#0.00####") + "|" 'totalgrava
                CO += Format(TotalExentoP, "#0.00####") + "|" 'totalexen
                Hay = 1
            End If
            'XMLDoc += "<nomina:Percepcion ImporteGravado=""" + +""" ImporteExento=""" + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("concepto"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoPercepcion=""" + +""" />"
            'por cada un
            CO += DR("tipocl") + "|" 'tipo
            CO += Trim(DR("clave")) + "|" 'clave
            CO += Trim(DR("concepto")) + "|" 'concepto
            CO += Format(DR("importegravado"), "#0.00####") + "|" 'importe grava
            CO += Format(DR("importeexento"), "#0.00####") + "|" 'importe exn
        End While
        DR.Close()

        'Deducciones
        DR = VI.ConsultaReader(ID, 1)
        Hay = 0
        While DR.Read
            If Hay = 0 Then
                'percepciones
                CO += Format(TotalGravadoD, "#0.00####") + "|" 'totalgrava
                CO += Format(TotalExentoD, "#0.00####") + "|" ' toalexen
                Hay = 1
            End If
            'por cada una
            CO += DR("tipocl") + "|" 'tipo
            CO += Trim(DR("clave")) + "|" 'clave
            CO += Trim(DR("concepto")) + "|" 'concepto
            CO += Format(DR("importegravado"), "#0.00####") + "|" 'importe grava
            CO += Format(DR("importeexento"), "#0.00####") + "|" 'importe exn
        End While
        DR.Close()
        Dim VIn As New dbNominasIncapacidades(MySqlcon)
        'incapacidades
        DR = VIn.ConsultaReader(ID)
        Hay = 0
        While DR.Read
            CO += DR("dias").ToString + "|" 'dias
            CO += CStr(DR("tin") + 1) + "|" 'tipo
            CO += Format(DR("descuento"), "#0.00####") + "|" 'descuento
        End While
        DR.Close()

        Dim VIhe As New dbNoominaHorasExtra(MySqlcon)
        'Horas Extra
        DR = VIhe.ConsultaReader(ID)
        Hay = 0
        While DR.Read
            'horas extra
            CO += DR("dias").ToString + "|" 'dias
            CO += DR("tipohoras") + "|" 'tipohoras
            CO += DR("horasextra").ToString + "|" 'horas
            CO += Format(DR("importepagado"), "#0.00####") + "|" 'importe
        End While
        DR.Close()
        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"

        Dim e As New Encriptador
        e.GuardaArchivoTexto("co.txt", CO, System.Text.Encoding.Default)
        Return CO

    End Function
    Public Function CreaCadenaOriginali32n12(ByVal pIdNomina As Integer, ByVal pIdMoneda As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|3.2|"
        ID = pIdNomina
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, IdMoneda)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        'CO += Serie + "|"
        'CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        'CO += NoAprobacion + "|"
        'CO += YearAprobacion + "|"
        CO += "egreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        If HayConceptos Then
            CO += Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + "|" 'subtotal
            CO += Format(TotalExentoD + TotalGravadoD, "#0.00####") + "|" 'descuento
        Else
            CO += Format(TotalaPagar, "#0.00####") + "|"
            CO += "0.00|" 'descuento
        End If


        'Tipo de cambio
        'If IdMoneda <> 2 Then
        '    CO += Format(TipodeCambio, "#0.00####") + "|"
        '    Dim Moneda As New dbMonedas(IdMoneda, Comm.Connection)
        '    CO += Moneda.Abreviatura + "|"
        'Else
        CO += "MXN|"
        'End If
        If HayConceptos Then
            CO += Format(TotalExentoP + TotalGravadoP - TotalExentoD - TotalGravadoD + totalOtrosPagos, "#0.00####") + "|" ' total factura con iva
        Else
            CO += Format(TotalaPagar, "#0.00####") + "|" ' total factura con iva
        End If
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'metododepago
        'Dim FP As New dbFormasdePago(Idforma, Comm.Connection)
        ''If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        'If Fecha < "2016/06/01" Then
        '    CO += Trim(FP.Nombre) + "|"
        'Else
        '    Dim strMetodos As String = ""
        '    Dim MeP As New dbVentasAddMetodos(Comm.Connection)
        '    DR = MeP.ConsultaReader(2, ID)
        '    While DR.Read()
        '        If strMetodos <> "" Then strMetodos += ","
        '        If DR("clavesat") < 1000 Then
        '            strMetodos += Format(DR("clavesat"), "00")
        '        Else
        '            strMetodos += "NA"
        '        End If
        '    End While
        '    DR.Close()
        '    CO += strMetodos + "|"
        'End If
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """"
        'Else
        CO += "NA|"
        'End If
        'lugar de expedicion
        If Sucursal.CP2 <> "" Then
            CO += Sucursal.CP2.Trim + "|"
        Else
            CO += Sucursal.CP.Trim + "|"
        End If
        'If NoCuenta <> "" And FP.Tipo = dbFormasdePago.Tipos.Contado Then CO += Trim(NoCuenta) + "|"

        'Aqui lo de parcialidades
        'proximamente

        CO += Trim(Sucursal.RFC) + "|"
        CO += Trim(Sucursal.NombreFiscal) + "|"
        'CO += Trim(Sucursal.Direccion) + "|"
        'CO += Trim(Sucursal.NoExterior) + "|"
        'CO += Trim(Sucursal.NoInterior) + "|"
        'CO += Trim(Sucursal.Colonia) + "|"
        'CO += Trim(Sucursal.Ciudad) + "|"
        'CO += Trim(Sucursal.ReferenciaDomicilio) + "|"
        'CO += Trim(Sucursal.Municipio) + "|"
        'CO += Trim(Sucursal.Estado) + "|"
        'CO += Trim(Sucursal.Pais) + "|"
        'CO += Trim(Sucursal.CP) + "|"

        'CO += Trim(Sucursal.Direccion2) + "|"
        'CO += Trim(Sucursal.NoExterior2) + "|"
        'CO += Trim(Sucursal.NoInterior2) + "|"
        'CO += Trim(Sucursal.Colonia2) + "|"
        'CO += Trim(Sucursal.Ciudad2) + "|"
        'CO += Trim(Sucursal.ReferenciaDomicilio2) + "|"
        'CO += Trim(Sucursal.Municipio2) + "|"
        'CO += Trim(Sucursal.Estado2) + "|"
        'CO += Trim(Sucursal.Pais2) + "|"
        'CO += Trim(Sucursal.CP2) + "|"

        'regimen

        'Dim Pos As Integer = 0
        'Dim Listo As Boolean = False
        'Dim AddDir As String = ""

        'While Sucursal.RegimenFiscal.Length > Pos
        '    If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
        '        CO += Trim(AddDir) + "|"
        '        AddDir = ""
        '        Listo = True
        '    Else
        '        AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
        '        Listo = False
        '    End If
        '    Pos += 1
        'End While

        'If Listo = False Then CO += Trim(AddDir) + "|"
        'regimen fiscal
        CO += Sucursal.ClaveRegimen.ToString + "|"

        CO += Trim(Trabajador.RFC) + "|"
        CO += Trim(Trabajador.Nombre) + "|"
        'If Trabajador.DireccionFiscal = 0 Then
        'CO += Trim(Trabajador.Direccion) + "|"
        'CO += Trim(Trabajador.NoExterior) + "|"
        'CO += Trim(Trabajador.NoInterior) + "|"
        'CO += Trim(Trabajador.Colonia) + "|"
        'CO += Trim(Trabajador.Ciudad) + "|"
        'CO += Trim(Trabajador.ReferenciaDomicilio) + "|"
        'CO += Trim(Trabajador.Municipio) + "|"
        'CO += Trim(Trabajador.Estado) + "|"
        'CO += Trim(Trabajador.Pais) + "|"
        'CO += Trim(Trabajador.CP) + "|"
        ''Else
        ''CO += Trim(Trabajador.Direccion2) + "|"
        ''CO += Trim(Trabajador.NoExterior2) + "|"
        ''CO += Trim(Trabajador.NoInterior2) + "|"
        ''CO += Trim(Trabajador.Colonia2) + "|"
        ''CO += Trim(Trabajador.Ciudad2) + "|"
        ''CO += Trim(Trabajador.ReferenciaDomicilio2) + "|"
        ''CO += Trim(Trabajador.Municipio2) + "|"
        ''CO += Trim(Trabajador.Estado2) + "|"
        ''CO += Trim(Trabajador.Pais2) + "|"
        ''CO += Trim(Trabajador.CP2) + "|"
        ''End If

        'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Dim VI As New dbNotasdeCargoDetalles(MySqlcon)
        'DR = VI.ConsultaReader(ID)
        'Dim PrecioTemp As Double
        'While DR.Read
        'If DR("cantidad") <> 0 And DR("precio") <> 0 Then
        CO += "1|"
        CO += "ACT|"
        'CO += DR("clave") + "|"
        CO += "Pago de nómina|"
        'If DR("idmoneda") <> 2 Then
        '    CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
        '    CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
        'Else

        If HayConceptos = False Then
            CO += Format(TotalaPagar, "#0.00####") + "|"
            CO += Format(TotalaPagar, "#0.00####") + "|"
        Else
            CO += Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + "|"
            CO += Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + "|"
        End If

        'End If
        'End If
        'End While
        'DR.Close()

        'If ISR <> 0 Or TotalISRcon <> 0 Then
        '    CO += "ISR|" + Format(TotalISR + TotalISRcon, "#0.00####") + "|"
        'End If
        'If IvaRetenido <> 0 Then
        '    CO += "IVA|" + Format(TotalIvaRetenido, "#0.00####") + "|"
        'End If
        'If ISR <> 0 Or IvaRetenido <> 0 Or TotalISRcon <> 0 Then
        '    CO += Format(TotalISR + TotalIvaRetenido + TotalISRcon, "#0.00####") + "|"
        'End If
        'Dim Ivas As New Collection
        'Dim IvasImporte As New Collection
        ''Dim IAnt As Double
        ' ''DR = DaIvas(ID)

        ''While DR.Read
        ''    If Ivas.Contains(DR("iva").ToString) = False Then
        ''        Ivas.Add(DR("iva"), DR("iva").ToString)
        ''    End If
        ''    If IvasImporte.Contains(DR("iva").ToString) = False Then
        ''        'If DR("idmoneda") <> 2 Then
        ''        '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
        ''        'Else
        ''        IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
        ''        ' End If
        ''    Else
        ''        IAnt = IvasImporte(DR("iva").ToString)
        ''        IvasImporte.Remove(DR("iva").ToString)
        ''        'If DR("idmoneda") <> 2 Then
        ''        '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
        ''        'Else
        ''        IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
        ''        'End If
        ''    End If
        ''End While
        ''DR.Close()
        ''For Each I As Double In Ivas
        ''    CO += "IVA|"
        ''    CO += Format(I, "#0.00") + "|"
        ''    CO += Format(IvasImporte(I.ToString), "#0.00") + "|"
        ''Next

        'CO += Format(0, "#0.00####") + "|"

        '-----------------Aqui lo de la nomina
        CO += "1.2|"
        CO += tipoNomina + "|"
        CO += Replace(FechaPago, "/", "-") + "|" 'fecha pago
        CO += Replace(FechaInicialPago, "/", "-") + "|" 'fecha inicial
        CO += Replace(FechaFinalPAgo, "/", "-") + "|" ' fecha final
        CO += Format(DiasPagados, "0") + "|" ' numero dias
        If TotalExentoP + TotalGravadoP <> 0 Then CO += Format(TotalExentoP + TotalGravadoP, "#0.00####") + "|"
        If TotalExentoD + TotalGravadoD <> 0 Then CO += Format(TotalExentoD + TotalGravadoD, "#0.00####") + "|"
        If totalOtrosPagos <> 0 Or CantidadOtrosPagos <> 0 Then CO += Format(totalOtrosPagos, "#0.00####") + "|"
        If Sucursal.CURP <> "" Then CO += Sucursal.CURP.Trim + "|"
        If Trabajador.RegistroPatronal <> "" Then CO += Trabajador.RegistroPatronal + "|"
        If Trabajador.RFCPatronOrigen <> "" Then CO += Trabajador.RFCPatronOrigen.Trim + "|"
        If origenRecurso <> "NA" And origenRecurso <> "" Then
            CO += origenRecurso + "|"
            If origenRecurso = "IM" Then CO += Format(montoRecurso, "0.00####") + "|"
        End If

        'If Trabajador.RegistroPatronal
        CO += Trim(Trabajador.Curp) + "|" 'curp
        CO += Trim(Trabajador.NumeroSeguroSocial) + "|" 'num seg social
        If Antiguedad <> 0 And Trabajador.RegistroPatronal <> "" Then
            CO += Replace(Trabajador.FechaInicioLaboral, "/", "-") + "|" 'fechainiciolaboral
            CO += CStr(Antiguedad) + "|" 'anti
        End If
        'CO += "01|"
        CO += Trabajador.TipoContrato.Substring(0, 2) + "|" 'tipocontrato
        If Trabajador.sindicalizado = 1 Then CO += "Sí|"
        CO += Trabajador.TipoJornada.Substring(0, 2) + "|" 'tipo jornada
        'CO += "01|"
        If Trabajador.TipoRegimen < 12 Then
            CO += Trim(Trabajador.TipoRegimen.ToString("00")) + "|" 'tipo regimen
        Else
            CO += "99|"
        End If
        CO += Trim(Trabajador.NumeroEmpleado) + "|" 'num empleado
        CO += Trabajador.Departamento + "|" ' departamento
        CO += Trabajador.Puesto + "|" 'puesto
        If Trabajador.RiesgoPuesto < 6 Then CO += Trabajador.RiesgoPuesto.ToString + "|" 'riesgo puesto
        If Trabajador.Periodicidad <> "" And Trabajador.Periodicidad <> "No aplica" Then CO += Trabajador.Periodicidad.Substring(0, 2) + "|" 'periodicidad pago
        'CO += "04|"
        If Trabajador.Banco > 0 Then
            CO += Format(Trabajador.Banco, "000") + "|" 'banco
            If Trabajador.CLABE <> "" And Trabajador.CLABE.Length <> 18 Then
                CO += Trabajador.CLABE + "|" 'Clabe
            End If
        End If
        If Trabajador.Banco = 0 And Trabajador.CLABE.Length = 18 Then
            CO += Trabajador.CLABE + "|" 'Clabe
        End If
        If Trabajador.SalarioBaseCotApor <> 0 Then CO += Format(Trabajador.SalarioBaseCotApor, "#0.00####") + "|" 'salariobato a cot
        If Trabajador.SalarioDiarioIntegrado <> 0 Then CO += Format(Trabajador.SalarioDiarioIntegrado, "#0.00####") + "|" 'salario diaro
        CO += DaClaveEstadosMexico(Trabajador.EstadoLabora) + "|"
        Dim Subc As New dbnominasubcontratacion(Comm.Connection)
        DR = Subc.ConsultaReader(IdTrabajador)
        While DR.Read
            CO += DR("rfclaboral".Trim) + "|"
            CO += DR("porcentaje").ToString + "|"
        End While
        DR.Close()

        Dim HE As New dbNoominaHorasExtra(Comm.Connection)
        Dim HorasCol As New Collection
        Dim HoraE As HEx
        DR = HE.ConsultaReaderCxml(pIdNomina)
        While DR.Read
            HoraE.IdDetalle = DR("iddetalle")
            HoraE.Dias = DR("dias")
            HoraE.HorasExtra = DR("horasextra")
            HoraE.TipoHoras = DR("tipohoras")
            HoraE.ImportePagado = DR("importepagado")
            HorasCol.Add(HoraE)
        End While
        DR.Close()

        Dim HayJubilacionUnaEx As Boolean = False
        Dim HayJubilacionParcial As Boolean = False
        Dim HaySeparacion As Boolean = False
        Dim VI As New dbNominasDetalles(MySqlcon)
        'Percepciones
        DR = VI.ConsultaReader(ID, 0)
        Dim Hay As Byte = 0
        While DR.Read
            If Hay = 0 Then
                If TotalSueldos <> 0 Then CO += Format(TotalSueldos, "#0.00####") + "|"
                If TotalSeparacion <> 0 Then CO += Format(TotalSeparacion, "#0.00####") + "|"
                If TotalJubilacion <> 0 Then CO += Format(TotalJubilacion, "#0.00####") + "|"
                CO += Format(TotalGravadoP, "#0.00####") + "|" 'totalgrava
                CO += Format(TotalExentoP, "#0.00####") + "|" 'totalexen
                Hay = 1
            End If
            HayJubilacionUnaEx = False
            HayJubilacionParcial = False
            HaySeparacion = False
            'XMLDoc += "<nomina:Percepcion ImporteGravado=""" + +""" ImporteExento=""" + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("concepto"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoPercepcion=""" + +""" />"
            'por cada un
            If DR("tipocl") = "039" Then
                HayJubilacionUnaEx = True
            End If
            If DR("tipocl") = "044" Then
                HayJubilacionParcial = True
            End If
            If DR("tipocl") = "022" Or DR("tipocl") = "023" Or DR("tipocl") = "025" Then
                HaySeparacion = True
            End If
            CO += DR("tipocl") + "|" 'tipo
            CO += Trim(DR("clave")) + "|" 'clave
            CO += Trim(DR("tipode")) + "|" 'concepto
            CO += Format(DR("importegravado"), "#0.00####") + "|" 'importe grava
            CO += Format(DR("importeexento"), "#0.00####") + "|" 'importe exn
            If DR("tipocl") = "045" Then
                CO += Format(DR("valormercado"), "#0.00####") + "|"
                CO += Format(DR("precioalotorgarse"), "#0.00####") + "|"
            End If
            If DR("tipocl") = "019" Then
                For Each horita As HEx In HorasCol
                    If horita.IdDetalle = DR("iddetalle") Then
                        CO += horita.Dias.ToString + "|" 'dias
                        CO += horita.TipoHoras.Substring(0, 2) + "|" 'tipohoras
                        CO += horita.HorasExtra.ToString + "|" 'horas
                        CO += Format(horita.ImportePagado, "#0.00####") + "|" 'importe
                        'XMLDoc += "<nomina12:HorasExtra Dias=""" + horita.Dias.ToString + """ TipoHoras=""" + horita.TipoHoras + """ HorasExtra=""" + horita.HorasExtra.ToString + """ ImportePagado=""" + Format(horita.ImportePagado, "#0.00####") + """ />"
                    End If
                Next
            End If
        End While
        DR.Close()

        Dim NT As New dbNominaTRabajador(pIdNomina, Comm.Connection)
        If NT.HayHatos Then
            If HayJubilacionUnaEx Then
                CO += Format(NT.JtotalUnaExhibicion, "#0.00####") + "|"
                CO += Format(NT.Jacumulable, "#0.00####") + "|"
                CO += Format(NT.JnoAcumulable, "#0.00####") + "|"
            Else
                If HayJubilacionParcial Then
                    CO += Format(NT.JtotalParcialidad, "#0.00####") + "|"
                    CO += Format(NT.JmontoDiario, "#0.00####") + "|"
                    CO += Format(NT.Jacumulable, "#0.00####") + "|"
                    CO += Format(NT.JnoAcumulable, "#0.00####") + "|"
                End If
            End If
            If HaySeparacion Then
                CO += Format(NT.StotalPagado, "#0.00####") + "|"
                CO += NT.SanhosServicio.ToString + "|"
                CO += Format(NT.SsueldoMensual, "#0.00####") + "|"
                CO += Format(NT.Sacumulable, "#0.00####") + "|"
                CO += Format(NT.SnoAcumulable, "#0.00####") + "|"
            End If
        End If
        'Fin Percepciones

        'Deducciones
        If TotalOtrasDeducciones <> 0 Then    CO += Format(TotalOtrasDeducciones, "#0.00####") + "|"
        If TotalImpuestosRetenidos <> 0 Then CO += Format(TotalImpuestosRetenidos, "#0.00####") + "|"
        DR = VI.ConsultaReader(ID, 1)
        While DR.Read
            'por cada una
            CO += DR("tipocl") + "|" 'tipo
            CO += Trim(DR("clave")) + "|" 'clave
            CO += Trim(DR("tipode")) + "|" 'concepto
            CO += Format(DR("importegravado") + DR("importeexento"), "#0.00####") + "|" 'importe grava
        End While
        DR.Close()
        'Otros Pagos

        Dim OP As New dbNominaOtrosPagos(Comm.Connection)
        DR = OP.consultaPagos(pIdNomina)
        Dim TipoOP As String
        Dim ConOp As String
        While DR.Read
            If DR("tipopago") < 4 Then
                TipoOP = Format(DR("tipopago") + 1, "000")
            Else
                TipoOP = "999"
            End If
            ConOp = DR("concepto")
            CO += TipoOP + "|"
            CO += DR("clave") + "|"
            CO += ConOp.Substring(4, ConOp.Length - 4) + "|"
            CO += Format(DR("importe"), "0.00####") + "|"
            If TipoOP = "002" Then
                CO += Format(DR("subsidio"), "0.00####") + "|"
            End If
            If TipoOP = "004" Then
                CO += Format(DR("saldoafavor"), "0.00####") + "|"
                CO += Format(DR("anhos"), "0.00####") + "|"
                CO += Format(DR("remanente"), "0.00####") + "|"
            End If
        End While
        DR.Close()
        

        Dim VIn As New dbNominasIncapacidades(MySqlcon)
        'incapacidades
        DR = VIn.ConsultaReader(ID)
        Hay = 0
        While DR.Read
            CO += DR("dias").ToString + "|" 'dias
            CO += Format(DR("tin") + 1, "00") + "|" 'tipo
            CO += Format(DR("descuento"), "#0.00####") + "|" 'descuento
        End While
        DR.Close()

        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"

        Dim e As New Encriptador
        e.GuardaArchivoTexto("co.txt", CO, System.Text.Encoding.Default)
        Return CO

    End Function
    Public Function CreaXMLi32(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>"

        XMLDoc += "<cfdi:Comprobante "
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdMoneda)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        XMLDoc += "version=""3.2"" "
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        XMLDoc += "folio=""" + Folio.ToString + """ "
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """ "
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """ "

        XMLDoc += "formaDePago=""Pago en una sola exhibición"" "

        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """ "
        XMLDoc += "certificado=""" + en.Certificado64 + """ "
        If HayConceptos Then
            XMLDoc += "subTotal=""" + Format(TotalExentoP + TotalGravadoP, "#0.00####") + """ "
            XMLDoc += "descuento=""" + Format(TotalExentoD + TotalGravadoD - TotalISRcon, "#0.00####") + """ "
        Else
            XMLDoc += "subTotal=""" + Format(TotalaPagar, "#0.00####") + """ "
            XMLDoc += "descuento=""" + Format(0, "#0.00####") + """ "
        End If
        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf



        'Tipo deCambio nuevo
        If IdMoneda <> 2 Then
            XMLDoc += "TipoCambio=""" + Format(TipodeCambio, "#0.00####") + """ "
            Dim Moneda As New dbMonedas(IdMoneda, Comm.Connection)
            XMLDoc += "Moneda=""" + Moneda.Abreviatura + """ "
        Else
            XMLDoc += "Moneda=""MXN"" "
        End If
        If HayConceptos Then
            XMLDoc += "total=""" + Format(TotalExentoP + TotalGravadoP - TotalExentoD - TotalGravadoD + TotalHorasExtra - totalIncapacidades, "#0.00####") + """ "
        Else
            XMLDoc += "total=""" + Format(TotalaPagar, "#0.00####") + """ "
        End If
        XMLDoc += "tipoDeComprobante=""egreso"" "
        'Metodo de pago lugar exibibicion nuevo
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim FP As New dbFormasdePago(Idforma, Comm.Connection)
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        If Fecha < "2016/06/01" Then
            XMLDoc += "metodoDePago=""" + Trim(FP.Nombre) + """" + vbCrLf
        Else
            Dim strMetodos As String = ""
            Dim MeP As New dbVentasAddMetodos(Comm.Connection)
            DR = MeP.ConsultaReader(2, ID)
            While DR.Read()
                If strMetodos <> "" Then strMetodos += ","
                If DR("clavesat") < 1000 Then
                    strMetodos += Format(DR("clavesat"), "00")
                Else
                    strMetodos += "NA"
                End If
            End While
            DR.Close()
            XMLDoc += "metodoDePago=""" + Trim(strMetodos) + """" + vbCrLf
        End If
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """" + vbCrLf
        'Else
        'XMLDoc += "metodoDePago=""No identificado"" "
        'End If
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad2 + "," + Sucursal.Estado2 + """ "
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad + "," + Sucursal.Estado + """ "
        End If

        'XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
        'XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        'XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf
        '++++++++++++++++++++++++++++
        'XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:implocal=""http://www.sat.gob.mx/implocal"" "
        'XMLDoc += "xmlns:terceros=""http://www.sat.gob.mx/terceros"" xmlns:detallista=""http://www.sat.gob.mx/detallista"" xmlns:psgecfd=""http://www.sat.gob.mx/psgecfd"" xmlns:ecc=""http://www.sat.gob.mx/ecc"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital"" "
        'XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/TimbreFiscalDigital/TimbreFiscalDigital.xsd http://www.sat.gob.mx/detallista " + _
        '"http://www.sat.gob.mx/sitio_internet/cfd/detallista/detallista.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd " + _
        '"http://www.sat.gob.mx/ecc http://www.sat.gob.mx/sitio_internet/cfd/ecc/ecc.xsd http://www.sat.gob.mx/psgecfd http://www.sat.gob.mx/sitio_internet/cfd/psgecfd/psgecfd.xsd " + _
        '"http://www.sat.gob.mx/terceros http://www.sat.gob.mx/sitio_internet/cfd/terceros/terceros11.xsd http://www.sat.gob.mx/donat http://www.sat.gob.mx/sitio_internet/cfd/donat/donat11.xsd http://www.sat.gob.mx/nomina http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina11.xsd"""

        XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" "
        XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/nomina http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina11.xsd"""

        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        XMLDoc += ">"

        XMLDoc += "<cfdi:Emisor rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>"

        XMLDoc += "<cfdi:DomicilioFiscal "
        If Sucursal.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        XMLDoc += "/>"
        If Sucursal.Pais2 <> "" Then
            XMLDoc += "<cfdi:ExpedidoEn  "


            If Sucursal.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "

            'If O._CalleLocal <> "" Then XMLDoc += "calle=""" + O._CalleLocal + """" + vbCrLf
            'If O._noExteriorLocal <> "" Then XMLDoc += "noExterior=""" + O._noExteriorLocal + """" + vbCrLf
            'If O._noInteriorLocal <> "" Then XMLDoc += "noInterior=""" + O._noInteriorLocal + """" + vbCrLf
            'If O._ColoniaLocal <> "" Then XMLDoc += "colonia=""" + O._ColoniaLocal + """" + vbCrLf
            'If O._LocalidadLocal <> "" Then XMLDoc += "localidad=""" + O._LocalidadLocal + """" + vbCrLf
            'If O._ReferenciaDomicilioLocal <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilioLocal + """" + vbCrLf
            'If O._MunicipioLocal <> "" Then XMLDoc += "municipio=""" + O._MunicipioLocal + """" + vbCrLf
            'If O._EstadoLocal <> "" Then XMLDoc += "estado=""" + O._EstadoLocal + """" + vbCrLf
            'If O._PaisLocal <> "" Then XMLDoc += "pais=""" + O._PaisLocal + """" + vbCrLf
            'If O._CodigoPostalLocal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostalLocal + """"
            XMLDoc += "/>"
        End If

        Dim Pos As Integer = 0
        Dim Listo As Boolean = False
        Dim AddDir As String = ""

        While Sucursal.RegimenFiscal.Length > Pos
            If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
                XMLDoc += "<cfdi:RegimenFiscal Regimen=""" + AddDir + """/>"
                AddDir = ""
                Listo = True
            Else
                AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
                Listo = False
            End If
            Pos += 1
        End While

        If Listo = False Then XMLDoc += "<cfdi:RegimenFiscal Regimen=""" + AddDir + """/>"



        XMLDoc += "</cfdi:Emisor>"


        XMLDoc += "<cfdi:Receptor rfc=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>"
        XMLDoc += "<cfdi:Domicilio "
        'If Trabajador.DireccionFiscal = 0 Then
        If Trabajador.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'Else
        '    If Trabajador.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Trabajador.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Trabajador.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Trabajador.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Trabajador.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Trabajador.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Trabajador.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Trabajador.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Trabajador.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Trabajador.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'End If
        XMLDoc += "/>"

        XMLDoc += "</cfdi:Receptor>"

        XMLDoc += "<cfdi:Conceptos>"

        'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Dim VI As New dbNotasdeCargoDetalles(MySqlcon)
        'DR = VI.ConsultaReader(ID)

        'While DR.Read
        'If DR("cantidad") <> 0 And DR("precio") <> 0 Then
        XMLDoc += "<cfdi:Concepto "
        XMLDoc += "cantidad=""1"" "
        'XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "unidad=""Servicio"" "
        'Dim Des As String
        'Des = Trim("Pago de Nómina")
        'While Des.IndexOf("  ") <> -1
        '    Des = Replace(Des, "  ", " ")
        'End While
        'Des = Replace(Des, vbTab, "")
        XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace("Pago de Nómina", vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If DR("idmoneda") <> 2 Then
        '    XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
        '    XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
        '    XMLDoc += "/> " + vbCrLf
        'Else
        If HayConceptos = False Then
            XMLDoc += "valorUnitario=""" + Format(TotalaPagar, "#0.00####") + """ "
            XMLDoc += "importe=""" + Format(TotalaPagar, "#0.00####") + """ "
        Else
            XMLDoc += "valorUnitario=""" + Format(TotalExentoP + TotalGravadoP, "#0.00####") + """ "
            XMLDoc += "importe=""" + Format(TotalExentoP + TotalGravadoP, "#0.00####") + """ "
        End If
        XMLDoc += "/>"
        'End If
        'End If
        'End While
        'DR.Close()

        XMLDoc += "</cfdi:Conceptos>"

        XMLDoc += "<cfdi:Impuestos totalImpuestosTrasladados=""" + Format(0, "#0.00####") + """"
        If ISR <> 0 Or IvaRetenido <> 0 Or TotalISRcon <> 0 Then
            XMLDoc += " totalImpuestosRetenidos=""" + Format(TotalISRcon, "#0.00####") + """"
        End If
        XMLDoc += ">"

        If ISR <> 0 Or IvaRetenido <> 0 Or TotalISRcon <> 0 Then
            XMLDoc += "<cfdi:Retenciones>"
            If ISR <> 0 Or TotalISRcon <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""ISR"" "
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalISRcon, "#0.00####") + """/>"
            End If

            If IvaRetenido <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""IVA"" "
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00####") + """/>"
            End If

            XMLDoc += "</cfdi:Retenciones>"

        End If



        'XMLDoc += "<cfdi:Traslados>"


        'DR = DaIvas(ID)
        'Dim IAnt As Double
        'While DR.Read
        '    If Ivas.Contains(DR("iva").ToString) = False Then
        '        Ivas.Add(DR("iva"), DR("iva").ToString)
        '    End If
        '    If IvasImporte.Contains(DR("iva").ToString) = False Then
        '        'If DR("idmoneda") <> 2 Then
        '        '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
        '        'Else
        '        IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
        '        'End If
        '    Else
        '        IAnt = IvasImporte(DR("iva").ToString)
        '        IvasImporte.Remove(DR("iva").ToString)
        '        'If DR("idmoneda") <> 2 Then
        '        '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
        '        'Else
        '        IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
        '        'End If

        '    End If
        'End While
        'DR.Close()
        'For Each I As Double In Ivas

        '    XMLDoc += "<cfdi:Traslado impuesto=""IVA"" "
        '    XMLDoc += "tasa=""" + Format(I, "#0.00") + """ "
        '    XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00") + """/>"

        'Next



        'XMLDoc += "</cfdi:Traslados>"





        XMLDoc += "</cfdi:Impuestos>"


        '-----------------Aqui lo de la nomina
        XMLDoc += "<cfdi:Complemento>"
        XMLDoc += "<nomina:Nomina RiesgoPuesto=""" + Trabajador.RiesgoPuesto.ToString + """ PeriodicidadPago=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Periodicidad, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.SalarioBaseCotApor <> 0 Then
            XMLDoc += "SalarioBaseCotApor=""" + Format(Trabajador.SalarioBaseCotApor, "#0.00####") + """ "
        End If
        If Trabajador.SalarioDiarioIntegrado <> 0 Then
            XMLDoc += "SalarioDiarioIntegrado=""" + Format(Trabajador.SalarioDiarioIntegrado, "#0.00####") + """ "
        End If
        If Trabajador.Puesto <> "" Then
            XMLDoc += "Puesto=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Puesto, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.TipoContrato <> "" Then
            XMLDoc += "TipoContrato=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.TipoContrato, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.TipoJornada <> "" Then
            XMLDoc += "TipoJornada=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.TipoJornada, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Antiguedad <> 0 Then
            XMLDoc += "Antiguedad=""" + CStr(Antiguedad) + """ "
            XMLDoc += "FechaInicioRelLaboral=""" + Replace(Trabajador.FechaInicioLaboral, "/", "-") + """ "
        End If
        If Trabajador.Departamento <> "" Then
            XMLDoc += "Departamento=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Departamento, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        XMLDoc += "NumDiasPagados=""" + Format(DiasPagados, "#0.00") + """ FechaFinalPago=""" + Replace(FechaFinalPAgo, "/", "-") + """ FechaInicialPago=""" + Replace(FechaInicialPago, "/", "-") + """ FechaPago=""" + Replace(FechaPago, "/", "-") + """  TipoRegimen=""" + Trabajador.TipoRegimen.ToString + """ "
        XMLDoc += "CURP=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Curp, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ NumEmpleado=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.NumeroEmpleado, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Version=""1.1"" xmlns:nomina=""http://www.sat.gob.mx/nomina"" "
        If Trabajador.RegistroPatronal <> "" Then
            XMLDoc += "RegistroPatronal=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.RegistroPatronal, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.NumeroSeguroSocial <> "" Then
            XMLDoc += "NumSeguridadSocial=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.NumeroSeguroSocial, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Banco > 0 Then
            XMLDoc += " Banco=""" + Format(Trabajador.Banco, "000") + """ "
            If Clabe <> "" Then XMLDoc += "CLABE=""" + Replace(Replace(Replace(Replace(Replace(Replace(Clabe, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        Dim Hayc = HayConceptos
        If Hayc Then
            XMLDoc += ">"
        Else
            XMLDoc += "/>"
        End If
        'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNominasDetalles(MySqlcon)
        'Percepciones
        DR = VI.ConsultaReader(ID, 0)
        Dim Hay As Byte = 0
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina:Percepciones TotalGravado=""" + Format(TotalGravadoP, "#0.00####") + """ TotalExento=""" + Format(TotalExentoP, "#0.00####") + """>"
                Hay = 1
            End If
            XMLDoc += "<nomina:Percepcion ImporteGravado=""" + Format(DR("importegravado"), "#0.00####") + """ ImporteExento=""" + Format(DR("importeexento"), "#0.00####") + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("concepto"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoPercepcion=""" + Format(DR("tipo"), "000") + """ />"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina:Percepciones>"
        End If
        'Deducciones
        DR = VI.ConsultaReader(ID, 1)
        Hay = 0
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina:Deducciones TotalGravado=""" + Format(TotalGravadoD, "#0.00####") + """ TotalExento=""" + Format(TotalExentoD, "#0.00####") + """>"
                Hay = 1
            End If
            XMLDoc += "<nomina:Deduccion ImporteGravado=""" + Format(DR("importegravado"), "#0.00####") + """ ImporteExento=""" + Format(DR("importeexento"), "#0.00####") + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("concepto"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoDeduccion=""" + Format(DR("tipo"), "000") + """ />"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina:Deducciones>"
        End If

        Dim VIn As New dbNominasIncapacidades(MySqlcon)
        'Incapacidades
        DR = VIn.ConsultaReader(ID)
        Hay = 0
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina:Incapacidades>"
                Hay = 1
            End If
            XMLDoc += "<nomina:Incapacidad Descuento=""" + Format(DR("descuento"), "#0.00####") + """ TipoIncapacidad=""" + CStr(DR("tin") + 1) + """ DiasIncapacidad=""" + DR("dias").ToString + """ />"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina:Incapacidades>"
        End If

        Dim VIhe As New dbNoominaHorasExtra(MySqlcon)
        'Horas Extra
        DR = VIhe.ConsultaReader(ID)
        Hay = 0
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina:HorasExtras>"
                Hay = 1
            End If
            XMLDoc += "<nomina:HorasExtra ImportePagado=""" + Format(DR("importepagado"), "#0.00####") + """ HorasExtra=""" + DR("horasextra").ToString + """ TipoHoras=""" + DR("tipohoras") + """ Dias=""" + DR("dias").ToString + """ />"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina:HorasExtras>"
        End If
        If Hayc Then XMLDoc += "</nomina:Nomina>"
        XMLDoc += "</cfdi:Complemento>"
        XMLDoc += "</cfdi:Comprobante>"


        Return XMLDoc

    End Function

    Public Function CreaXMLi32n12(ByVal pIdNomina As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>"

        XMLDoc += "<cfdi:Comprobante "
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdNomina
        LlenaDatos()
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdMoneda)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        XMLDoc += "version=""3.2"" "
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        XMLDoc += "folio=""" + Folio.ToString + """ "
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """ "
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """ "

        XMLDoc += "formaDePago=""Pago en una sola exhibición"" "

        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """ "
        XMLDoc += "certificado=""" + en.Certificado64 + """ "
        If HayConceptos Then
            XMLDoc += "subTotal=""" + Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + """ "
            XMLDoc += "descuento=""" + Format(TotalExentoD + TotalGravadoD, "#0.00####") + """ "
        Else
            XMLDoc += "subTotal=""" + Format(TotalaPagar, "#0.00####") + """ "
            XMLDoc += "descuento=""" + Format(0, "#0.00####") + """ "
        End If
        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf



        'Tipo deCambio nuevo
        'If IdMoneda <> 2 Then
        '    XMLDoc += "TipoCambio=""" + Format(TipodeCambio, "#0.00####") + """ "
        '    Dim Moneda As New dbMonedas(IdMoneda, Comm.Connection)
        '    XMLDoc += "Moneda=""" + Moneda.Abreviatura + """ "
        'Else
        XMLDoc += "Moneda=""MXN"" "
        'End If
        If HayConceptos Then
            XMLDoc += "total=""" + Format(TotalExentoP + TotalGravadoP - TotalExentoD - TotalGravadoD + totalOtrosPagos, "#0.00####") + """ "
        Else
            XMLDoc += "total=""" + Format(TotalaPagar, "#0.00####") + """ "
        End If
        XMLDoc += "tipoDeComprobante=""egreso"" "
        XMLDoc += "metodoDePago=""NA"" "
        'Metodo de pago lugar exibibicion nuevo
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Dim FP As New dbFormasdePago(Idforma, Comm.Connection)
        ''If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        'If Fecha < "2016/06/01" Then
        '    XMLDoc += "metodoDePago=""" + Trim(FP.Nombre) + """" + vbCrLf
        'Else
        '    Dim strMetodos As String = ""
        '    Dim MeP As New dbVentasAddMetodos(Comm.Connection)
        '    DR = MeP.ConsultaReader(2, ID)
        '    While DR.Read()
        '        If strMetodos <> "" Then strMetodos += ","
        '        If DR("clavesat") < 1000 Then
        '            strMetodos += Format(DR("clavesat"), "00")
        '        Else
        '            strMetodos += "NA"
        '        End If
        '    End While
        '    DR.Close()
        '    XMLDoc += "metodoDePago=""" + Trim(strMetodos) + """" + vbCrLf
        'End If
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """" + vbCrLf
        'Else
        'XMLDoc += "metodoDePago=""No identificado"" "
        'End If
        If Sucursal.CP2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.CP2 + """ "
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.CP + """ "
        End If

        'XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
        'XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        'XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf
        '++++++++++++++++++++++++++++
        'XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:implocal=""http://www.sat.gob.mx/implocal"" "
        'XMLDoc += "xmlns:terceros=""http://www.sat.gob.mx/terceros"" xmlns:detallista=""http://www.sat.gob.mx/detallista"" xmlns:psgecfd=""http://www.sat.gob.mx/psgecfd"" xmlns:ecc=""http://www.sat.gob.mx/ecc"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital"" "
        'XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/TimbreFiscalDigital/TimbreFiscalDigital.xsd http://www.sat.gob.mx/detallista " + _
        '"http://www.sat.gob.mx/sitio_internet/cfd/detallista/detallista.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd " + _
        '"http://www.sat.gob.mx/ecc http://www.sat.gob.mx/sitio_internet/cfd/ecc/ecc.xsd http://www.sat.gob.mx/psgecfd http://www.sat.gob.mx/sitio_internet/cfd/psgecfd/psgecfd.xsd " + _
        '"http://www.sat.gob.mx/terceros http://www.sat.gob.mx/sitio_internet/cfd/terceros/terceros11.xsd http://www.sat.gob.mx/donat http://www.sat.gob.mx/sitio_internet/cfd/donat/donat11.xsd http://www.sat.gob.mx/nomina http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina11.xsd"""

        XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:nomina12=""http://www.sat.gob.mx/nomina12"" "
        XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/nomina12 http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina12.xsd"" "
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        XMLDoc += ">"

        XMLDoc += "<cfdi:Emisor rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>"

        'XMLDoc += "<cfdi:DomicilioFiscal "
        'If Sucursal.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Sucursal.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Sucursal.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Sucursal.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Sucursal.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Sucursal.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Sucursal.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Sucursal.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Sucursal.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Sucursal.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'XMLDoc += "/>"
        'If Sucursal.Pais2 <> "" Then
        '    XMLDoc += "<cfdi:ExpedidoEn  "


        '    If Sucursal.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Sucursal.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Sucursal.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Sucursal.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        '    If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "

        '    'If O._CalleLocal <> "" Then XMLDoc += "calle=""" + O._CalleLocal + """" + vbCrLf
        '    'If O._noExteriorLocal <> "" Then XMLDoc += "noExterior=""" + O._noExteriorLocal + """" + vbCrLf
        '    'If O._noInteriorLocal <> "" Then XMLDoc += "noInterior=""" + O._noInteriorLocal + """" + vbCrLf
        '    'If O._ColoniaLocal <> "" Then XMLDoc += "colonia=""" + O._ColoniaLocal + """" + vbCrLf
        '    'If O._LocalidadLocal <> "" Then XMLDoc += "localidad=""" + O._LocalidadLocal + """" + vbCrLf
        '    'If O._ReferenciaDomicilioLocal <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilioLocal + """" + vbCrLf
        '    'If O._MunicipioLocal <> "" Then XMLDoc += "municipio=""" + O._MunicipioLocal + """" + vbCrLf
        '    'If O._EstadoLocal <> "" Then XMLDoc += "estado=""" + O._EstadoLocal + """" + vbCrLf
        '    'If O._PaisLocal <> "" Then XMLDoc += "pais=""" + O._PaisLocal + """" + vbCrLf
        '    'If O._CodigoPostalLocal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostalLocal + """"
        '    XMLDoc += "/>"
        'End If

        'Dim Pos As Integer = 0
        'Dim Listo As Boolean = False
        'Dim AddDir As String = ""
        'While Sucursal.RegimenFiscal.Length > Pos
        '    If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
        '        XMLDoc += "<cfdi:RegimenFiscal Regimen=""" + AddDir + """/>"
        '        AddDir = ""
        '        Listo = True
        '    Else
        '        AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
        '        Listo = False
        '    End If
        '    Pos += 1
        'End While
        'If Listo = False Then XMLDoc += "<cfdi:RegimenFiscal Regimen=""" + AddDir + """/>"
        XMLDoc += "<cfdi:RegimenFiscal Regimen=""" + Sucursal.ClaveRegimen.ToString + """/>"
        XMLDoc += "</cfdi:Emisor>"


        XMLDoc += "<cfdi:Receptor rfc=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>"
        'XMLDoc += "<cfdi:Domicilio "
        ''If Trabajador.DireccionFiscal = 0 Then
        'If Trabajador.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Trabajador.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Trabajador.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Trabajador.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Trabajador.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Trabajador.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Trabajador.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Trabajador.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Trabajador.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If Trabajador.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        ''Else
        ''    If Trabajador.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        ''    If Trabajador.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        ''    If Trabajador.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        ''    If Trabajador.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        ''    If Trabajador.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        ''    If Trabajador.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        ''    If Trabajador.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        ''    If Trabajador.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        ''    If Trabajador.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        ''    If Trabajador.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        ''End If
        'XMLDoc += "/>"

        XMLDoc += "</cfdi:Receptor>"

        XMLDoc += "<cfdi:Conceptos>"

        'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Dim VI As New dbNotasdeCargoDetalles(MySqlcon)
        'DR = VI.ConsultaReader(ID)

        'While DR.Read
        'If DR("cantidad") <> 0 And DR("precio") <> 0 Then
        XMLDoc += "<cfdi:Concepto "
        XMLDoc += "cantidad=""1"" "
        'XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "unidad=""ACT"" "
        'Dim Des As String
        'Des = Trim("Pago de Nómina")
        'While Des.IndexOf("  ") <> -1
        '    Des = Replace(Des, "  ", " ")
        'End While
        'Des = Replace(Des, vbTab, "")
        XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace("Pago de nómina", vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'If DR("idmoneda") <> 2 Then
        '    XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
        '    XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
        '    XMLDoc += "/> " + vbCrLf
        'Else
        If HayConceptos = False Then
            XMLDoc += "valorUnitario=""" + Format(TotalaPagar, "#0.00####") + """ "
            XMLDoc += "importe=""" + Format(TotalaPagar, "#0.00####") + """ "
        Else
            XMLDoc += "valorUnitario=""" + Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + """ "
            XMLDoc += "importe=""" + Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + """ "
        End If
        XMLDoc += "/>"
        'End If
        'End If
        'End While
        'DR.Close()

        XMLDoc += "</cfdi:Conceptos>"
        XMLDoc += "<cfdi:Impuestos/>"
        'XMLDoc += "<cfdi:Impuestos totalImpuestosTrasladados=""" + Format(0, "#0.00####") + """"
        'If ISR <> 0 Or IvaRetenido <> 0 Or TotalISRcon <> 0 Then
        '    XMLDoc += " totalImpuestosRetenidos=""" + Format(TotalISRcon, "#0.00####") + """"
        'End If
        'XMLDoc += ">"

        'If ISR <> 0 Or IvaRetenido <> 0 Or TotalISRcon <> 0 Then
        '    XMLDoc += "<cfdi:Retenciones>"
        '    If ISR <> 0 Or TotalISRcon <> 0 Then
        '        XMLDoc += "<cfdi:Retencion impuesto=""ISR"" "
        '        'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(TotalISRcon, "#0.00####") + """/>"
        '    End If

        '    If IvaRetenido <> 0 Then
        '        XMLDoc += "<cfdi:Retencion impuesto=""IVA"" "
        '        'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00####") + """/>"
        '    End If

        '    XMLDoc += "</cfdi:Retenciones>"

        'End If



        ''XMLDoc += "<cfdi:Traslados>"


        ''DR = DaIvas(ID)
        ''Dim IAnt As Double
        ''While DR.Read
        ''    If Ivas.Contains(DR("iva").ToString) = False Then
        ''        Ivas.Add(DR("iva"), DR("iva").ToString)
        ''    End If
        ''    If IvasImporte.Contains(DR("iva").ToString) = False Then
        ''        'If DR("idmoneda") <> 2 Then
        ''        '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
        ''        'Else
        ''        IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
        ''        'End If
        ''    Else
        ''        IAnt = IvasImporte(DR("iva").ToString)
        ''        IvasImporte.Remove(DR("iva").ToString)
        ''        'If DR("idmoneda") <> 2 Then
        ''        '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
        ''        'Else
        ''        IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
        ''        'End If

        ''    End If
        ''End While
        ''DR.Close()
        ''For Each I As Double In Ivas

        ''    XMLDoc += "<cfdi:Traslado impuesto=""IVA"" "
        ''    XMLDoc += "tasa=""" + Format(I, "#0.00") + """ "
        ''    XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00") + """/>"

        ''Next



        ''XMLDoc += "</cfdi:Traslados>"





        'XMLDoc += "</cfdi:Impuestos>"


        '-----------------Aqui lo de la nomina
        Dim HayJubilacionUnaEx As Boolean = False
        Dim HayJubilacionParcial As Boolean = False
        Dim HaySeparacion As Boolean = False
        XMLDoc += "<cfdi:Complemento>"
        XMLDoc += "<nomina12:Nomina Version=""1.2"" TipoNomina=""" + tipoNomina + """ FechaPago=""" + Replace(FechaPago, "/", "-") + """ FechaInicialPago=""" + Replace(FechaInicialPago, "/", "-") + """ FechaFinalPago=""" + Replace(FechaFinalPAgo, "/", "-") + """ NumDiasPagados=""" + Format(DiasPagados, "0") + """ "
        If TotalExentoP + TotalGravadoP <> 0 Then
            XMLDoc += "TotalPercepciones=""" + Format(TotalExentoP + TotalGravadoP, "#0.00####") + """ "
        End If
        If TotalExentoD + TotalGravadoD <> 0 Then
            XMLDoc += "TotalDeducciones=""" + Format(TotalExentoD + TotalGravadoD, "#0.00####") + """ "
        End If
        If totalOtrosPagos <> 0 Or CantidadOtrosPagos <> 0 Then
            XMLDoc += "TotalOtrosPagos=""" + Format(totalOtrosPagos, "#0.00####") + """ "
        End If
        XMLDoc += ">"
        XMLDoc += "<nomina12:Emisor "
        If Sucursal.CURP <> "" Then
            XMLDoc += "Curp=""" + Sucursal.CURP + """ "
        End If
        If Trabajador.RegistroPatronal <> "" Then
            XMLDoc += "RegistroPatronal=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.RegistroPatronal, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.RFCPatronOrigen <> "" Then
            XMLDoc += "RfcPatronOrigen=""" + Trabajador.RFCPatronOrigen + """"
        End If
        XMLDoc += ">"
        If origenRecurso <> "NA" And origenRecurso <> "" Then
            XMLDoc += "<nomina12:EntidadSNCF OrigenRecurso=""" + origenRecurso + """ "
            If origenRecurso = "IM" Then
                XMLDoc += "MontoRecursoPropio=""" + Format(montoRecurso, "0.00####") + """ "
            End If
            XMLDoc += "/>"
        End If
        XMLDoc += "</nomina12:Emisor>"
        XMLDoc += "<nomina12:Receptor Curp=""" + Trabajador.Curp + """ "
        If Trabajador.NumeroSeguroSocial <> "" Then
            XMLDoc += "NumSeguridadSocial=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.NumeroSeguroSocial, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Antiguedad <> 0 And Trabajador.RegistroPatronal <> "" Then
            XMLDoc += "FechaInicioRelLaboral=""" + Replace(Trabajador.FechaInicioLaboral, "/", "-") + """ "
            XMLDoc += "Antigüedad=""P" + CStr(Antiguedad) + "W"" "
        End If
        'XMLDoc += "TipoContrato=""01"" "
        XMLDoc += "TipoContrato=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.TipoContrato.Substring(0, 2), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.sindicalizado = 1 Then
            XMLDoc += "Sindicalizado=""Sí"" "
        End If
        If Trabajador.TipoJornada <> "No aplica" Then
            'XMLDoc += "TipoJornada=""01"" "
            XMLDoc += "TipoJornada=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.TipoJornada.Substring(0, 2), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.TipoRegimen < 12 Then
            XMLDoc += "TipoRegimen=""" + Trabajador.TipoRegimen.ToString("00") + """ "
        Else
            XMLDoc += "TipoRegimen=""99"" "
        End If
        XMLDoc += "NumEmpleado=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.NumeroEmpleado, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.Departamento <> "" Then
            XMLDoc += "Departamento=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Departamento, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.Puesto <> "" Then
            XMLDoc += "Puesto=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Puesto, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.RiesgoPuesto < 6 Then
            XMLDoc += "RiesgoPuesto=""" + Trabajador.RiesgoPuesto.ToString + """ "
        End If
        If Trabajador.Periodicidad <> "" And Trabajador.Periodicidad <> "No aplica" Then XMLDoc += "PeriodicidadPago=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Periodicidad.Substring(0, 2), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'XMLDoc += "PeriodicidadPago=""04"" "
        If Trabajador.Banco > 0 Then
            XMLDoc += " Banco=""" + Format(Trabajador.Banco, "000") + """ "
            If Clabe <> "" And Clabe.Length <> 18 Then XMLDoc += "CuentaBancaria=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.CLABE, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.Banco = 0 And Trabajador.CLABE.Length = 18 Then
            If Clabe <> "" Then XMLDoc += "CuentaBancaria=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.CLABE, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.SalarioBaseCotApor <> 0 Then
            XMLDoc += "SalarioBaseCotApor=""" + Format(Trabajador.SalarioBaseCotApor, "#0.00####") + """ "
        End If
        If Trabajador.SalarioDiarioIntegrado <> 0 Then
            XMLDoc += "SalarioDiarioIntegrado=""" + Format(Trabajador.SalarioDiarioIntegrado, "#0.00####") + """ "
        End If
        XMLDoc += "ClaveEntFed=""" + DaClaveEstadosMexico(Trabajador.EstadoLabora) + """ "
        XMLDoc += ">"
        Dim Subc As New dbnominasubcontratacion(Comm.Connection)
        DR = Subc.ConsultaReader(IdTrabajador)
        While DR.Read
            XMLDoc += "<nomina12:SubContratacion RfcLabora=""" + DR("rfclaboral") + """ PorcentajeTiempo=""" + DR("porcentaje").ToString + """/>"
        End While
        DR.Close()
        XMLDoc += "</nomina12:Receptor>"
        Dim HE As New dbNoominaHorasExtra(Comm.Connection)
        Dim HorasCol As New Collection
        Dim HoraE As HEx
        DR = HE.ConsultaReaderCxml(pIdNomina)
        While DR.Read
            HoraE.IdDetalle = DR("iddetalle")
            HoraE.Dias = DR("dias")
            HoraE.HorasExtra = DR("horasextra")
            HoraE.TipoHoras = DR("tipohoras")
            HoraE.ImportePagado = DR("importepagado")
            HorasCol.Add(HoraE)
        End While
        DR.Close()
        Dim VI As New dbNominasDetalles(Comm.Connection)
        'Percepciones
        DR = VI.ConsultaReader(ID, 0)
        Dim Hay As Byte = 0
        While DR.Read
            HayJubilacionUnaEx = False
            HayJubilacionParcial = False
            HaySeparacion = False
            If Hay = 0 Then
                XMLDoc += "<nomina12:Percepciones "
                If TotalSueldos <> 0 Then
                    XMLDoc += "TotalSueldos=""" + Format(TotalSueldos, "#0.00####") + """ "
                End If
                If TotalSeparacion <> 0 Then
                    XMLDoc += "TotalSeparacionIndemnizacion=""" + Format(TotalSeparacion, "#0.00####") + """ "
                End If
                If TotalJubilacion <> 0 Then
                    XMLDoc += "TotalJubilacionPensionRetiro=""" + Format(TotalJubilacion, "#0.00####") + """ "
                End If
                XMLDoc += "TotalGravado=""" + Format(TotalGravadoP, "#0.00####") + """ TotalExento=""" + Format(TotalExentoP, "#0.00####") + """>"
                Hay = 1
            End If
            If DR("tipocl") = "039" Then
                HayJubilacionUnaEx = True
            End If
            If DR("tipocl") = "044" Then
                HayJubilacionParcial = True
            End If
            If DR("tipocl") = "022" Or DR("tipocl") = "023" Or DR("tipocl") = "025" Then
                HaySeparacion = True
            End If
            XMLDoc += "<nomina12:Percepcion ImporteGravado=""" + Format(DR("importegravado"), "#0.00####") + """ ImporteExento=""" + Format(DR("importeexento"), "#0.00####") + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("tipode"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoPercepcion=""" + DR("tipocl") + """ "
            If DR("tipocl") = "045" Or DR("tipocl") = "019" Then
                XMLDoc += ">"
            Else
                XMLDoc += "/>"
            End If
            If DR("tipocl") = "045" Then
                XMLDoc += "<nomina12:AccionesOTitulos ValorMercado=""" + Format(DR("valormercado"), "#0.00####") + """ PrecioAlOtorgarse=""" + Format(DR("precioalotorgarse"), "#0.00####") + """ />"
            End If
            If DR("tipocl") = "019" Then
                For Each horita As HEx In HorasCol
                    If horita.IdDetalle = DR("iddetalle") Then
                        XMLDoc += "<nomina12:HorasExtra Dias=""" + horita.Dias.ToString + """ TipoHoras=""" + horita.TipoHoras.Substring(0, 2) + """ HorasExtra=""" + horita.HorasExtra.ToString + """ ImportePagado=""" + Format(horita.ImportePagado, "#0.00####") + """ />"
                    End If
                Next
            End If
            If DR("tipocl") = "045" Or DR("tipocl") = "019" Then
                XMLDoc += "</nomina12:Percepcion>"
            End If
        End While
        DR.Close()
        Dim NT As New dbNominaTRabajador(pIdNomina, Comm.Connection)
        If NT.HayHatos Then
            If HayJubilacionUnaEx Then
                XMLDoc += "<nomina12:JubilacionPensionRetiro TotalUnaExhibicion=""" + Format(NT.JtotalUnaExhibicion, "#0.00####") + """ IngresoAcumulable=""" + Format(NT.Jacumulable, "#0.00####") + """ IngresoNoAcumulable=""" + Format(NT.JnoAcumulable, "#0.00####") + """ />"
            Else
                If HayJubilacionParcial Then
                    XMLDoc += "<nomina12:JubilacionPensionRetiro TotalParcialidad=""" + Format(NT.JtotalParcialidad, "#0.00####") + """ MontoDiario=""" + Format(NT.JmontoDiario, "#0.00####") + """ IngresoAcumulable=""" + Format(NT.Jacumulable, "#0.00####") + """ IngresoNoAcumulable=""" + Format(NT.JnoAcumulable, "#0.00####") + """ />"
                End If
            End If
            If HaySeparacion Then
                XMLDoc += "<nomina12:SeparacionIndemnizacion TotalPagado=""" + Format(NT.StotalPagado, "#0.00####") + """ NumAñosServicio=""" + NT.SanhosServicio.ToString + """ UltimoSueldoMensOrd=""" + Format(NT.SsueldoMensual, "#0.00####") + """ IngresoAcumulable=""" + Format(NT.Sacumulable, "#0.00####") + """ IngresoNoAcumulable=""" + Format(NT.SnoAcumulable, "#0.00####") + """/>"
            End If
        End If
        If Hay = 1 Then
            XMLDoc += "</nomina12:Percepciones>"
        End If
        'Deducciones
        DR = VI.ConsultaReader(ID, 1)
        Hay = 0
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina12:Deducciones "
                If TotalOtrasDeducciones <> 0 Then
                    XMLDoc += " TotalOtrasDeducciones=""" + Format(TotalOtrasDeducciones, "#0.00####") + """ "
                End If
                If TotalImpuestosRetenidos <> 0 Then
                    XMLDoc += " TotalImpuestosRetenidos=""" + Format(TotalImpuestosRetenidos, "#0.00####") + """ "
                End If
                XMLDoc += ">"
                Hay = 1
            End If
            XMLDoc += "<nomina12:Deduccion Importe=""" + Format(DR("importegravado") + DR("importeexento"), "#0.00####") + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("tipode"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoDeduccion=""" + DR("tipocl") + """ />"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina12:Deducciones>"
        End If

        Dim OP As New dbNominaOtrosPagos(Comm.Connection)
        DR = OP.consultaPagos(pIdNomina)
        Hay = 0
        Dim TipoOP As String
        Dim ConOP As String
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina12:OtrosPagos>"
                Hay = 1
            End If
            If DR("tipopago") < 4 Then
                TipoOP = Format(DR("tipopago") + 1, "000")
            Else
                TipoOP = "999"
            End If
            ConOP = DR("concepto")
            XMLDoc += "<nomina12:OtroPago  TipoOtroPago=""" + TipoOP + """ Clave=""" + DR("clave") + """ Concepto=""" + ConOP.Substring(4, ConOP.Length - 4) + """ Importe=""" + Format(DR("importe"), "0.00####") + """>"
            If TipoOP = "002" Then
                XMLDoc += "<nomina12:SubsidioAlEmpleo SubsidioCausado=""" + Format(DR("subsidio"), "0.00####") + """/>"
            End If
            If TipoOP = "004" Then
                XMLDoc += "<nomina12:CompensacionSaldosAFavor SaldoaFavor=""" + Format(DR("saldoafavor"), "0.00####") + """ Año=""" + Format(DR("anhos"), "0.00####") + """ RemanenteSalFav=""" + Format(DR("remanente"), "0.00####") + """ />"
            End If
            XMLDoc += "</nomina12:OtroPago>"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina12:OtrosPagos>"
        End If
        Dim VIn As New dbNominasIncapacidades(MySqlcon)
        'Incapacidades
        DR = VIn.ConsultaReader(ID)
        Hay = 0
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina12:Incapacidades>"
                Hay = 1
            End If
            XMLDoc += "<nomina12:Incapacidad ImporteMonetario=""" + Format(DR("descuento"), "#0.00####") + """ TipoIncapacidad=""" + Format(DR("tin") + 1, "00") + """ DiasIncapacidad=""" + DR("dias").ToString + """ />"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina12:Incapacidades>"
        End If

        XMLDoc += "</nomina12:Nomina>"
        XMLDoc += "</cfdi:Complemento>"
        XMLDoc += "</cfdi:Comprobante>"


        Return XMLDoc

    End Function

    

    Public Function DaIvas(ByVal pIdNota As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iva,precio,idmoneda from tblnotasdecargodetalles where idcargo=" + pIdNota.ToString
        Return Comm.ExecuteReader
    End Function



    Private Function ValidarCertificadoRemoto(ByVal sender As Object, ByVal certificate As Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As Security.Cryptography.X509Certificates.X509Chain, ByVal policyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function


    'Public Function Timbrar(ByVal RFCEmisor As String, ByVal RFCCliente As String, ByVal ArchivoCer As String, ByVal CerPassword As String, ByVal strXml As String, ByVal pDireccionTimbrado As String) As TimbreFiscal.TimbreFiscalDigital
    '    Dim en As New Encriptador
    '    Dim T As TimbreFiscal.TimbreFiscalDigital
    '    Try
    '        Dim Tcfdi As New TimbreFiscal.TimbradoCFDI
    '        Dim x509 As New Security.Cryptography.X509Certificates.X509Certificate(en.LeeArchivo(ArchivoCer), CerPassword)
    '        Tcfdi.ClientCertificates.Add(x509)
    '        Tcfdi.Url = pDireccionTimbrado '"https://demotf.buzonfiscal.com/timbrado?wsdl"
    '        Dim Req As New TimbreFiscal.RequestTimbradoCFDType
    '        Req.InfoBasica = New TimbreFiscal.InfoBasicaType
    '        Req.InfoBasica.RfcEmisor = RFCEmisor
    '        Req.InfoBasica.RfcReceptor = Cliente.RFC
    '        Req.InfoBasica.Serie = Serie
    '        Req.RefID = Serie + Folio.ToString
    '        'Req.Documento = New TimbreFiscal.DocumentoType
    '        'Dim Encoder As New System.Text.UTF8Encoding
    '        'Dim Bytes() As Byte = Encoder.GetBytes(strXml)
    '        'Req.Documento.Archivo = Bytes
    '        Dim s As New Xml.Serialization.XmlSerializer(GetType(TimbreFiscal.Comprobante), "http://www.sat.gob.mx/cfd/3")
    '        Dim xml As String = strXml
    '        Dim c As TimbreFiscal.Comprobante = s.Deserialize(New IO.StringReader(xml))
    '        Req.Comprobante = c

    '        Dim x As New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificadoRemoto)
    '        System.Net.ServicePointManager.ServerCertificateValidationCallback = x
    '        System.Net.ServicePointManager.Expect100Continue = True
    '        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
    '        T = Tcfdi.timbradoCFD(Req)
    '        Return T
    '    Catch ex As Exception
    '        T = New TimbreFiscal.TimbreFiscalDigital
    '        T.noCertificadoSAT = "Error"
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '        Return T
    '    End Try

    'End Function

    Public Function Timbrar(ByVal RFCEmisor As String, ByVal RFCCliente As String, ByVal ArchivoCer As String, ByVal CerPassword As String, ByVal strXml As String, ByVal pDireccionTimbrado As String) As TimbreFiscal.TimbreFiscalDigital
        Dim en As New Encriptador
        Dim T As TimbreFiscal.TimbreFiscalDigital
        Try
            Dim Tcfdi As New TimbreFiscal.TimbradoCFDI
            Dim x509 As New Security.Cryptography.X509Certificates.X509Certificate(en.LeeArchivo(ArchivoCer), CerPassword)
            Tcfdi.ClientCertificates.Add(x509)
            Tcfdi.Url = pDireccionTimbrado '"https://demotf.buzonfiscal.com/timbrado?wsdl"
            Dim Req As New TimbreFiscal.RequestTimbradoCFDType
            Req.InfoBasica = New TimbreFiscal.InfoBasicaType
            Req.InfoBasica.RfcEmisor = RFCEmisor
            Req.InfoBasica.RfcReceptor = Trabajador.RFC
            Req.InfoBasica.Serie = Serie

            Req.RefID = Serie + Folio.ToString
            'Req.Documento.Tipo = TimbreFiscal.DocumentoTypeTipo.XML
            Req.Documento = New TimbreFiscal.DocumentoType
            ' Req.Documento.Version = "3.0"
            Dim Encoder As New System.Text.UTF8Encoding
            'Dim Bytes() As Byte = Encoder.GetBytes(strXml)
            'Req.Documento.Archivo = Bytes
            'Dim s As New Xml.Serialization.XmlSerializer(GetType(TimbreFiscal.Comprobante), "http://www.sat.gob.mx/cfd/3")
            'Dim xml As String = strXml
            'Dim c As TimbreFiscal.Comprobante = s.Deserialize(New IO.StringReader(xml))
            Dim Bytes() As Byte = Encoder.GetBytes(strXml)
            Req.Documento.Archivo = Bytes
            'Dim c As TimbreFiscal.Comprobante = s.Deserialize(New IO.StringReader(Convert.ToBase64String(xml)))
            'Req.Comprobante = c
            Dim x As New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificadoRemoto)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = x
            System.Net.ServicePointManager.Expect100Continue = True
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
            'Tcfdi.SoapVersion = Web.Services.Protocols.SoapProtocolVersion.Default
            T = Tcfdi.timbradoCFD(Req)
            Return T
        Catch ex As Exception
            T = New TimbreFiscal.TimbreFiscalDigital
            T.noCertificadoSAT = "Error"
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Return T
        End Try

    End Function

    Public Sub GuardaDatosTimbrado(ByVal pidCargo As Integer, ByVal pUuid As String, ByVal pFechaTimbrado As String, ByVal pSellocfd As String, ByVal pNoCertificadoSat As String, ByVal pSelloSat As String)
        Comm.CommandText = "insert into tblnominastimbrado(idnomina,uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat) values(" + pidCargo.ToString + ",'" + Replace(pUuid, "'", "''") + "','" + Replace(pFechaTimbrado, "'", "''") + "','" + Replace(pSellocfd, "'", "''") + "','" + Replace(pNoCertificadoSat, "'", "''") + "','" + Replace(pSelloSat, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub DaDatosTimbrado(ByVal pidCargo As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblnominastimbrado where idnomina=" + pidCargo.ToString
        DReader = Comm.ExecuteReader
        uuid = "**No Timbrado**"
        If DReader.Read Then
            uuid = DReader("uuid")
            FechaTimbrado = DReader("fechatimbrado")
            SelloCFD = DReader("sellocfd")
            NoCertificadoSAT = DReader("nocertificadosat")
            SelloSAT = DReader("sellosat")
        End If
        DReader.Close()
    End Sub

    Public Sub ModificaEstado(ByVal pidVenta As Integer, ByVal pEstado As Byte)
        Comm.CommandText = "update tblnominas set estado=" + pEstado.ToString + " where idnomina=" + pidVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Timbrar2(ByVal pUsuario As String, ByVal pPassword As String, ByVal pRFC As String, ByVal pRutaXML As String, ByVal pRutaSalida As String, ByVal pConector As Byte) As Integer
        Try
            Dim Comando As String
            If pConector = 0 Then
                Comando = Application.StartupPath + "\conector\timbrador_cliente.exe " + pUsuario + "*" + pPassword + "*" + pRFC + "*" + pRutaXML + "*" + pRutaSalida
            Else
                Comando = Application.StartupPath + "\conector2012\timbrar.exe " + pUsuario + "*" + pPassword + "*" + pRFC + "*" + pRutaSalida
            End If
            Shell(Comando, AppWinStyle.Hide, True)
            Return 1
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            NoCertificadoSAT = "Error"
            Return 0
        End Try

    End Function
    Public Function CancelarTimbrado(ByVal pRFC As String, ByVal pUUID As String) As Integer
        Try
            Dim Comando As String
            Dim StrCancelacion As String
            Dim en As New Encriptador
            IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
            If IO.File.Exists(Application.StartupPath + "\temp\cancelacion.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\cancelacion.txt")
            End If
            If pRFC = "SUL010720JN8" Then
                Comando = Application.StartupPath + "\conector2012\CLIENTE_TIMBRADOR222.exe CANCELAR*" + pRFC + "*" + pUUID + "*" + Application.StartupPath + "\temp\cancelacion.txt"
            Else
                Comando = Application.StartupPath + "\conector2012\CLIENTE_TIMBRADOR.exe CANCELAR*" + pRFC + "*" + pUUID + "*" + Application.StartupPath + "\temp\cancelacion.txt"
            End If
            Shell(Comando, AppWinStyle.Hide, True)
            StrCancelacion = en.LeeArchivoTexto(Application.StartupPath + "\temp\cancelacion.txt")
            StrCancelacion = StrCancelacion.ToUpper
            If StrCancelacion.Contains("HAS BEEN CANCELED") Then
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Return 0
        End Try
    End Function
    Public Function Timbrar3(ByVal pRFC As String, ByVal pXML As String, ByVal pRutaSalida As String, ByVal pAPIKEY As String, ByVal PSerie As String, ByVal pFolio As Integer) As String
        'Dim Cadena As String
        'Dim XmlTimbrado As String
        'Cadena = pRFC + "~" + pAPIKEY + "~" + "SI" + "~" + "Factura" + "~" + pXML
        'Dim FF As New facturafiel.server()()
        'XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
        'Return XmlTimbrado
        Dim Pruebas As String = "NO"
        Comm.CommandText = "select codigopostal from tblopciones limit 1"
        Pruebas = Comm.ExecuteScalar

        Dim Alterno As String = "0"
        Comm.CommandText = "select nombreempresalocal from tblopciones limit 1"
        Alterno = Comm.ExecuteScalar
        If Alterno = "1" Then
            Return Timbrar3alt(pRFC, pXML, pRutaSalida, pAPIKEY, PSerie, pFolio)
        End If

        Dim Cadena As String
        Dim XmlTimbrado As String = ""
        Try
            Cadena = pRFC + "~" + pAPIKEY + "~" + Pruebas + "~" + "Factura" + "~" + pXML
            Dim FF As New facturafiel.server()
            FF.Url = "http://www.facturafiel.com/websrv/servicio_timbrado_xml.php?wsdl"
            XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
            FF.Dispose()
            'Return XmlTimbrado
        Catch ex As Exception
            'If ex.Message.Contains("Response is not well-formed XML") Then
            '    'If pConMsgbox Then
            '    MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            '    'Else
            '    '   MensajeError = ex.Message
            '    'End If
            '    'Response is not well-formed XML.
            '    NoCertificadoSAT = "Error"
            '    XmlTimbrado = "ERROR"
            '    'Return "ERROR"
            'Else
            Dim en As New Encriptador
            IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
            If IO.File.Exists(Application.StartupPath + "\temp\nomerror.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\nomerror.txt")
            End If
            en.GuardaArchivoTexto(Application.StartupPath + "\temp\nomerror.txt", ex.Message, System.Text.Encoding.Default)



            XmlTimbrado = "Recuperar"
            'End If
        End Try
        'recuperacion
        Try
            If XmlTimbrado = "Recuperar" Then
                Dim R As New facturafielrecuperacion.server()
                R.Url = "http://www.facturafiel.com/websrv/servicio_recuperacion.php?wsdl"
                If PSerie <> "" Then
                    Cadena = pRFC + "~" + pAPIKEY + "~" + PSerie + "+" + pFolio.ToString
                Else
                    Cadena = pRFC + "~" + pAPIKEY + "~" + pFolio.ToString
                End If
                XmlTimbrado = R.servicio_recuperacion(Cadena)
                XmlTimbrado = XmlTimbrado.Substring(1, XmlTimbrado.Length - 1)
                R.Dispose()
            End If
        Catch ex As Exception
            'If pConMsgbox Then
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            'Else
            'MensajeError = ex.Message
            'End If
            XmlTimbrado = "ERROR"
            NoCertificadoSAT = "Error"
        End Try
        Return XmlTimbrado



    End Function

    Public Function Timbrar3alt(ByVal pRFC As String, ByVal pXML As String, ByVal pRutaSalida As String, ByVal pAPIKEY As String, ByVal PSerie As String, ByVal pFolio As Integer) As String
        'Dim Cadena As String
        'Dim XmlTimbrado As String
        'Cadena = pRFC + "~" + pAPIKEY + "~" + "SI" + "~" + "Factura" + "~" + pXML
        'Dim FF As New facturafiel.server()()
        'XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
        'Return XmlTimbrado
        Dim Pruebas As String = "NO"
        Comm.CommandText = "select codigopostal from tblopciones limit 1"
        Pruebas = Comm.ExecuteScalar
        Dim Cadena As String
        Dim XmlTimbrado As String = ""
        Try
            Cadena = pRFC + "~" + pAPIKEY + "~" + Pruebas + "~" + "Factura" + "~" + pXML
            Dim FF As New facturafiel.server()
            FF.Url = "http://www.facturafiel.com/websrv/servicio_timbrado_xml.php?wsdl"
            'FF.UnsafeAuthenticatedConnectionSharing = False
            XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
            FF.Dispose()
            'Return XmlTimbrado
        Catch ex As Exception
            'If ex.Message.Contains("Response is not well-formed XML") Then
            '    'If pConMsgbox Then
            '    MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            '    'Else
            '    '   MensajeError = ex.Message
            '    'End If
            '    'Response is not well-formed XML.
            '    NoCertificadoSAT = "Error"
            '    XmlTimbrado = "ERROR"
            '    'Return "ERROR"
            'Else
            Dim en As New Encriptador
            IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
            If IO.File.Exists(Application.StartupPath + "\temp\nomerror.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\nomerror.txt")
            End If
            en.GuardaArchivoTexto(Application.StartupPath + "\temp\nomerror.txt", ex.Message, System.Text.Encoding.Default)


            XmlTimbrado = "Recuperar"
            'End If
        End Try
        'recuperacion
        Try
            If XmlTimbrado = "Recuperar" Then
                Dim R As New facturafielrecuperacion.server()
                R.Url = "http://www.facturafiel.com/websrv/servicio_recuperacion.php?wsdl"
                If PSerie <> "" Then
                    Cadena = pRFC + "~" + pAPIKEY + "~" + PSerie + "+" + pFolio.ToString
                Else
                    Cadena = pRFC + "~" + pAPIKEY + "~" + pFolio.ToString
                End If
                XmlTimbrado = R.servicio_recuperacion(Cadena)
                XmlTimbrado = XmlTimbrado.Substring(1, XmlTimbrado.Length - 1)
                R.Dispose()
            End If
        Catch ex As Exception
            'If pConMsgbox Then
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            'Else
            'MensajeError = ex.Message
            'End If
            XmlTimbrado = "ERROR"
            NoCertificadoSAT = "Error"
        End Try
        Return XmlTimbrado
    End Function

    Public Function CancelarTimbrado2(ByVal pRFC As String, ByVal pUUID As String, ByVal pAPIKey As String) As Integer
        Dim Cadena As String = ""
        If pUUID.Contains("Ambiente") Then Return 1
        Dim Cancela As New facturafielcancelacion.server
        Cancela.Url = "http://www.facturafiel.com/websrv/servicio_cancelacion.php?wsdl"
        Dim en As New Encriptador
        IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
        If IO.File.Exists(Application.StartupPath + "\temp\cancelacion.txt") Then
            IO.File.Delete(Application.StartupPath + "\temp\cancelacion.txt")
        End If
        Cadena = pRFC + "~" + pAPIKey + "~" + pUUID
        Cadena = Cancela.servicio_cancelacion(Cadena)
        If Cadena.Contains("EXITOSAMENTE") Then
            Return 1
        Else
            en.GuardaArchivoTexto(Application.StartupPath + "\temp\cancelacion.txt", Cadena, System.Text.Encoding.Default)
            Return 0
        End If
    End Function
    Public Sub DaIdsPorTimbrar()
        IdsPorTimbrar = New Collection
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select idnomina from tblnominas where estado=2 and importado=1"
        DReader = Comm.ExecuteReader
        While DReader.Read
            IdsPorTimbrar.Add(DReader("idnomina"))
        End While
        DReader.Close()

    End Sub

    Public Sub AgregarDetallesReferencia(ByVal pIdNomina As Integer, ByVal pIdNominaOrigen As Integer)

        Comm.CommandText = "insert into tblnominadetalles(idnomina,tipo,tipodetalle,clave,concepto,importegravado,importeexento,valormercado,precioalotorgarse) select " + pIdNomina.ToString + ",tipo,tipodetalle,clave,concepto,importegravado,importeexento,valormercado,precioalotorgarse from tblnominadetalles where idnomina=" + pIdNominaOrigen.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblnominaextra(idnomina,totalpagado,anhosservicio,sueldomensual,acumulable1,noacumulable1,totalunaexhibicion,totalparcialidad,montodiario,acumulable2,noacumulable2) select " + pIdNomina.ToString + ",totalpagado,anhosservicio,sueldomensual,acumulable1,noacumulable1,totalunaexhibicion,totalparcialidad,montodiario,acumulable2,noacumulable2 from tblnominaextra where idnomina=" + pIdNominaOrigen.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblnominahorasextra(idnomina,dias,tipohoras,horasextra,importepagado) select " + pIdNomina.ToString + ",dias,tipohoras,horasextra,importepagado from tblnominahorasextra where idnomina=" + pIdNominaOrigen.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblnominaincapacidades(idnomina,tipoincapacidad,dias,descuento) select " + pIdNomina.ToString + ",tipoincapacidad,dias,descuento from tblnominaincapacidades where idnomina=" + pIdNominaOrigen.ToString
        Comm.ExecuteNonQuery()
        Dim IddetalleHE As Integer
        Comm.CommandText = "select ifnull((select iddetalle from tblnominadetalles inner join tblpercepciones on tblnominadetalles.tipo=tblpercepciones.idpercepcion where tipodetalle=0 and tblpercepciones.clave='019' and idnomina=" + pIdNomina.ToString + "),0)"
        IddetalleHE = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblnominahorasextrac(iddetalle,dias,tipohoras,horasextra,importepagado) select " + IddetalleHE.ToString + ",he.dias,he.tipohoras,he.horasextra,he.importepagado from tblnominahorasextrac he inner join tblnominadetalles on he.iddetalle=tblnominadetalles.iddetalle where tblnominadetalles.idnomina=" + pIdNominaOrigen.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblnominaotrospagos(tipopago,clave,concepto,importe,idnomina,subsidio,saldoafavor,anhos,remanente) select tipopago,clave,concepto,importe," + pIdNomina.ToString + ",subsidio,saldoafavor,anhos,remanente from tblnominaotrospagos where idnomina=" + pIdNominaOrigen.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Recuperar(ByVal PRFC As String, ByVal pAPIKEY As String, ByVal PSerie As String, ByVal pFolio As Integer, ByVal pConMsgBox As Boolean) As String
        Dim XMLTimbrado As String
        Dim Cadena As String
        Try
            'If XMLTimbrado = "Recuperar" Then
            Dim R As New facturafielrecuperacion.server()
            R.Url = "http://www.facturafiel.com/websrv/servicio_recuperacion.php?wsdl"
            If PSerie <> "" Then
                Cadena = PRFC + "~" + pAPIKEY + "~" + PSerie + "+" + pFolio.ToString
            Else
                Cadena = PRFC + "~" + pAPIKEY + "~+" + pFolio.ToString
            End If
            XMLTimbrado = R.servicio_recuperacion(Cadena)
            If XMLTimbrado.StartsWith("Error") = False Then
                XMLTimbrado = XMLTimbrado.Substring(1, XMLTimbrado.Length - 1)
            End If
            R.Dispose()
            'End If
        Catch ex As Exception
            'If pConMsgBox Then
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            'Else
            '   MensajeError = ex.Message
            'End If
            XMLTimbrado = "ERROR"
            NoCertificadoSAT = "Error"
        End Try
        Return XMLTimbrado
    End Function
    Public Function DaNombreBanco(ByVal Clave As Integer) As String
        Comm.CommandText = "select ifnull((select nombre from tblbancoscatalogo where clave=" + Clave.ToString + "),'')"
        Return Comm.ExecuteScalar
    End Function

    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdsucursal As Integer, ByVal pIdTrabajador As Integer, ByVal pEstado As Boolean, ByVal fechapago As Boolean, ByVal periodo As Boolean, ByVal rango As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idnomina,serie,folio,fecha,fechapago,numeroempleado,nombre,if(totalapagar<>0,totalapagar,ifnull((select sum(importegravado+importeexento) from tblnominadetalles where tblnominadetalles.idnomina=tblnominas.idnomina and tipodetalle=0),0)+ifnull((select sum(importepagado) from tblnominahorasextra where tblnominahorasextra.idnomina=tblnominas.idnomina),0)-ifnull((select sum(descuento) from tblnominaincapacidades where tblnominaincapacidades.idnomina=tblnominas.idnomina),0)-ifnull((select sum(importegravado+importeexento) from tblnominadetalles where tblnominadetalles.idnomina=tblnominas.idnomina and tipodetalle=1),0)) " + _
        "importe,tblnominas.idtrabajador,tblnominas.fechainicialpago,tblnominas.fechafinalpago from tblnominas inner join tbltrabajadores on tblnominas.idtrabajador=tbltrabajadores.idtrabajador where "
        If fechapago Then
            Comm.CommandText += " tblnominas.fechapago>='" + pFecha1 + "' and tblnominas.fechapago<='" + pFecha2 + "'"
        End If
        If periodo Then
            Comm.CommandText += " tblnominas.fechainicialpago>='" + pFecha1 + "' and tblnominas.fechafinalpago<='" + pFecha2 + "'"
        End If
        If rango Then
            Comm.CommandText += "tblnominas.fecha>='" + pFecha1 + "' and tblnominas.fecha<='" + pFecha2 + "'"
        End If
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdsucursal.ToString
        End If
        If pIdTrabajador > 0 Then
            Comm.CommandText += " and tblnominas.idtrabajador=" + pIdTrabajador.ToString
        End If
        If pEstado = True Then
            Comm.CommandText += " and tblnominas.estado=4"
        Else
            Comm.CommandText += " and tblnominas.estado=3"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblnominas")
        'DS.WriteXmlSchema("tblnominas.xml")
        Return DS.Tables("tblnominas").DefaultView
    End Function

    Public Function ReportexConcepto(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdsucursal As Integer, ByVal pIdTrabajador As Integer, ByVal pEstado As Boolean, ByVal pTipo As Byte, ByVal pIdConcepto As Integer, ByVal fechapago As Boolean, ByVal periodo As Boolean, ByVal rango As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblnominas.idnomina,tblnominas.serie,tblnominas.folio,tblnominas.fecha,if(tblnominadetalles.tipodetalle=0,(select descripcion from tblpercepciones where idpercepcion=tblnominadetalles.tipo),(select descripcion from tbldeducciones where iddeduccion=tblnominadetalles.tipo)) concepto,tblnominadetalles.importegravado+tblnominadetalles.importeexento importe,tbltrabajadores.nombre,tbltrabajadores.numeroempleado,tblnominadetalles.tipodetalle,tblnominadetalles.tipo,tblnominas.idtrabajador,tblnominadetalles.importegravado,tblnominadetalles.importeexento from tblnominas inner join tblnominadetalles on tblnominadetalles.idnomina=tblnominas.idnomina inner join tbltrabajadores on tblnominas.idtrabajador=tbltrabajadores.idtrabajador where "
        If rango Then
            Comm.CommandText += " tblnominas.fecha>='" + pFecha1 + "' and tblnominas.fecha<='" + pFecha2 + "'"
        End If
        If fechapago Then
            Comm.CommandText += " tblnominas.fechapago>='" + pFecha1 + "' and tblnominas.fechapago<='" + pFecha2 + "'"
        End If
        If periodo Then
            Comm.CommandText += " tblnominas.fechainicialpago>='" + pFecha1 + "' and tblnominas.fechafinalpago<='" + pFecha2 + "'"
        End If
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblnominas.idsucursal=" + pIdsucursal.ToString
        End If
        If pIdTrabajador > 0 Then
            Comm.CommandText += " and tblnominas.idtrabajador=" + pIdTrabajador.ToString
        End If
        If pEstado = True Then
            Comm.CommandText += " and tblnominas.estado=4"
        Else
            Comm.CommandText += " and tblnominas.estado=3"
        End If
        If pTipo > 0 Then
            If pIdConcepto > 0 Then
                Comm.CommandText += " and tblnominadetalles.tipodetalle=" + CStr(pTipo - 1) + " and tblnominadetalles.tipo=" + pIdConcepto.ToString
            Else
                Comm.CommandText += " and tblnominadetalles.tipodetalle=" + CStr(pTipo - 1)
            End If
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblnominascon")
        DS.WriteXmlSchema("tblnominascon.xml")
        Return DS.Tables("tblnominascon").DefaultView
    End Function
    Public Sub ModificaNominaConcepto(pIdConcepto As Integer, pIdCuenta As Integer, pTipo As Byte)
        If pTipo = 0 Then
            Comm.CommandText = "update tblpercepciones set idcuenta=" + pIdCuenta.ToString + " where idpercepcion=" + pIdConcepto.ToString
        Else
            Comm.CommandText = "update tbldeducciones set idcuenta=" + pIdCuenta.ToString + " where iddeduccion=" + pIdConcepto.ToString
        End If
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaIdCuentaConcepto(pIdconcepto As Integer, pTipo As Byte) As Integer
        If pTipo = 0 Then
            Comm.CommandText = "select idcuenta from tblpercepciones where idpercepcion=" + pIdconcepto.ToString
        Else
            Comm.CommandText = "select idcuenta from tbldeducciones where iddeduccion=" + pIdconcepto.ToString
        End If
        Return Comm.ExecuteScalar
    End Function

    Public Function CreaCadenaOriginali33(ByVal pIdNomina As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer, pXMLINE As String, pEsEgreso As Byte, pCadenaOriginalComp As String) As String
        Dim CO As String = "|3.3|"

        Dim en As New Encriptador
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdNomina
        LlenaDatos()

        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdMoneda)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        If Serie <> "" Then CO += Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"

        'If pSelloDigital <> "" Then CO += "Sello=""" + pSelloDigital + """ "

        'Dim strMetodos As String = ""
        'Dim MeP As New dbVentasAddMetodos(Comm.Connection)
        'DR = MeP.ConsultaReader(0, ID)
        'DR.Read()
        'If strMetodos <> "" Then strMetodos += ","
        'If DR("clavesat") < 1000 Then
        'strMetodos += Format(DR("clavesat"), "00")
        'Else
        'strMetodos += "NA"
        'End If
        'DR.Close()
        'CO += strMetodos + "|"
        CO += "99|"
        If NoCertificado <> "" Then CO += NoCertificado + "|"
        'CO += "Certificado=""" + en.Certificado64 + """ "

        'CO+="CondicionesDePago="""""

        CO += Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + "|"
        If TotalExentoD + TotalGravadoD > 0 Then CO += Format(TotalExentoD + TotalGravadoD, "#0.00####") + "|"
        CO += "MXN|"
        CO += Format(TotalExentoP + TotalGravadoP - TotalExentoD - TotalGravadoD + totalOtrosPagos, "#0.00####") + "|"


        'Dim CP As New dbVentasCartaPorte(ID, MySqlcon)

        CO += "N|"


        'If IdFormadePago <> 98 Then
        CO += "PUE|"
        'End If

        If Sucursal.CP2 <> "" Then
            CO += Sucursal.CP2 + "|"
        Else
            CO += Sucursal.CP + "|"
        End If
        'Confirmacion
        'If NoConfirmacion <> "" Then CO += NoConfirmacion + "|"

        'CFDIS relacionados aqui'
        'CO += "01|"
        'DR = DaIvasUUIDS(ID)
        'While DR.Read
        '    CO += DR("uuid") + "|"
        'End While
        'DR.Close()

        CO += Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Sucursal.ClaveRegimen.ToString + "|"


        CO += Replace(Replace(Replace(Replace(Replace(Trabajador.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Replace(Replace(Replace(Replace(Replace(Trabajador.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"

        CO += "P01|"


        CO += "84111505|"
        CO += "1|"
        CO += "ACT|"
        'CO += "ACT|"
        CO += "Pago de nómina|"

        CO += Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + "|"
        CO += Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + "|"
        If TotalExentoD + TotalGravadoD > 0 Then CO += Format(TotalExentoD + TotalGravadoD, "#0.00####") + "|"

        '-----------------Aqui lo de la nomina
        CO += "1.2|"
        CO += tipoNomina + "|"
        CO += Replace(FechaPago, "/", "-") + "|" 'fecha pago
        CO += Replace(FechaInicialPago, "/", "-") + "|" 'fecha inicial
        CO += Replace(FechaFinalPAgo, "/", "-") + "|" ' fecha final
        CO += Format(DiasPagados, "0") + "|" ' numero dias
        If TotalExentoP + TotalGravadoP <> 0 Then CO += Format(TotalExentoP + TotalGravadoP, "#0.00####") + "|"
        If TotalExentoD + TotalGravadoD <> 0 Then CO += Format(TotalExentoD + TotalGravadoD, "#0.00####") + "|"
        If totalOtrosPagos <> 0 Or CantidadOtrosPagos <> 0 Then CO += Format(totalOtrosPagos, "#0.00####") + "|"
        If Sucursal.CURP <> "" Then CO += Sucursal.CURP.Trim + "|"
        If Trabajador.RegistroPatronal <> "" Then CO += Trabajador.RegistroPatronal + "|"
        If Trabajador.RFCPatronOrigen <> "" Then CO += Trabajador.RFCPatronOrigen.Trim + "|"
        If origenRecurso <> "NA" And origenRecurso <> "" Then
            CO += origenRecurso + "|"
            If origenRecurso = "IM" Then CO += Format(montoRecurso, "0.00####") + "|"
        End If

        'If Trabajador.RegistroPatronal
        CO += Trim(Trabajador.Curp) + "|" 'curp
        CO += Trim(Trabajador.NumeroSeguroSocial) + "|" 'num seg social
        If Antiguedad <> 0 And Trabajador.RegistroPatronal <> "" Then
            CO += Replace(Trabajador.FechaInicioLaboral, "/", "-") + "|" 'fechainiciolaboral
            CO += CStr(Antiguedad) + "|" 'anti
        End If
        'CO += "01|"
        CO += Trabajador.TipoContrato.Substring(0, 2) + "|" 'tipocontrato
        If Trabajador.sindicalizado = 1 Then CO += "Sí|"
        CO += Trabajador.TipoJornada.Substring(0, 2) + "|" 'tipo jornada
        'CO += "01|"
        If Trabajador.TipoRegimen < 12 Then
            CO += Trim(Trabajador.TipoRegimen.ToString("00")) + "|" 'tipo regimen
        Else
            CO += "99|"
        End If
        CO += Trim(Trabajador.NumeroEmpleado) + "|" 'num empleado
        CO += Trabajador.Departamento + "|" ' departamento
        CO += Trabajador.Puesto + "|" 'puesto
        If Trabajador.RiesgoPuesto < 6 Then CO += Trabajador.RiesgoPuesto.ToString + "|" 'riesgo puesto
        If Trabajador.Periodicidad <> "" And Trabajador.Periodicidad <> "No aplica" Then CO += Trabajador.Periodicidad.Substring(0, 2) + "|" 'periodicidad pago
        'CO += "04|"
        If Trabajador.Banco > 0 Then
            CO += Format(Trabajador.Banco, "000") + "|" 'banco
            If Trabajador.CLABE <> "" And Trabajador.CLABE.Length <> 18 Then
                CO += Trabajador.CLABE + "|" 'Clabe
            End If
        End If
        If Trabajador.Banco = 0 And Trabajador.CLABE.Length = 18 Then
            CO += Trabajador.CLABE + "|" 'Clabe
        End If
        If Trabajador.SalarioBaseCotApor <> 0 Then CO += Format(Trabajador.SalarioBaseCotApor, "#0.00####") + "|" 'salariobato a cot
        If Trabajador.SalarioDiarioIntegrado <> 0 Then CO += Format(Trabajador.SalarioDiarioIntegrado, "#0.00####") + "|" 'salario diaro
        CO += DaClaveEstadosMexico(Trabajador.EstadoLabora) + "|"
        Dim Subc As New dbnominasubcontratacion(Comm.Connection)
        DR = Subc.ConsultaReader(IdTrabajador)
        While DR.Read
            CO += DR("rfclaboral".Trim) + "|"
            CO += DR("porcentaje").ToString + "|"
        End While
        DR.Close()

        Dim HE As New dbNoominaHorasExtra(Comm.Connection)
        Dim HorasCol As New Collection
        Dim HoraE As HEx
        DR = HE.ConsultaReaderCxml(pIdNomina)
        While DR.Read
            HoraE.IdDetalle = DR("iddetalle")
            HoraE.Dias = DR("dias")
            HoraE.HorasExtra = DR("horasextra")
            HoraE.TipoHoras = DR("tipohoras")
            HoraE.ImportePagado = DR("importepagado")
            HorasCol.Add(HoraE)
        End While
        DR.Close()

        Dim HayJubilacionUnaEx As Boolean = False
        Dim HayJubilacionParcial As Boolean = False
        Dim HaySeparacion As Boolean = False
        Dim VI As New dbNominasDetalles(MySqlcon)
        'Percepciones
        DR = VI.ConsultaReader(ID, 0)
        Dim Hay As Byte = 0
        While DR.Read
            If Hay = 0 Then
                If TotalSueldos <> 0 Then CO += Format(TotalSueldos, "#0.00####") + "|"
                If TotalSeparacion <> 0 Then CO += Format(TotalSeparacion, "#0.00####") + "|"
                If TotalJubilacion <> 0 Then CO += Format(TotalJubilacion, "#0.00####") + "|"
                CO += Format(TotalGravadoP, "#0.00####") + "|" 'totalgrava
                CO += Format(TotalExentoP, "#0.00####") + "|" 'totalexen
                Hay = 1
            End If
            HayJubilacionUnaEx = False
            HayJubilacionParcial = False
            HaySeparacion = False
            'XMLDoc += "<nomina:Percepcion ImporteGravado=""" + +""" ImporteExento=""" + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("concepto"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoPercepcion=""" + +""" />"
            'por cada un
            If DR("tipocl") = "039" Then
                HayJubilacionUnaEx = True
            End If
            If DR("tipocl") = "044" Then
                HayJubilacionParcial = True
            End If
            If DR("tipocl") = "022" Or DR("tipocl") = "023" Or DR("tipocl") = "025" Then
                HaySeparacion = True
            End If
            CO += DR("tipocl") + "|" 'tipo
            CO += Trim(DR("clave")) + "|" 'clave
            CO += Trim(DR("tipode")) + "|" 'concepto
            CO += Format(DR("importegravado"), "#0.00####") + "|" 'importe grava
            CO += Format(DR("importeexento"), "#0.00####") + "|" 'importe exn
            If DR("tipocl") = "045" Then
                CO += Format(DR("valormercado"), "#0.00####") + "|"
                CO += Format(DR("precioalotorgarse"), "#0.00####") + "|"
            End If
            If DR("tipocl") = "019" Then
                For Each horita As HEx In HorasCol
                    If horita.IdDetalle = DR("iddetalle") Then
                        CO += horita.Dias.ToString + "|" 'dias
                        CO += horita.TipoHoras.Substring(0, 2) + "|" 'tipohoras
                        CO += horita.HorasExtra.ToString + "|" 'horas
                        CO += Format(horita.ImportePagado, "#0.00####") + "|" 'importe
                        'XMLDoc += "<nomina12:HorasExtra Dias=""" + horita.Dias.ToString + """ TipoHoras=""" + horita.TipoHoras + """ HorasExtra=""" + horita.HorasExtra.ToString + """ ImportePagado=""" + Format(horita.ImportePagado, "#0.00####") + """ />"
                    End If
                Next
            End If
        End While
        DR.Close()

        Dim NT As New dbNominaTRabajador(pIdNomina, Comm.Connection)
        If NT.HayHatos Then
            If HayJubilacionUnaEx Then
                CO += Format(NT.JtotalUnaExhibicion, "#0.00####") + "|"
                CO += Format(NT.Jacumulable, "#0.00####") + "|"
                CO += Format(NT.JnoAcumulable, "#0.00####") + "|"
            Else
                If HayJubilacionParcial Then
                    CO += Format(NT.JtotalParcialidad, "#0.00####") + "|"
                    CO += Format(NT.JmontoDiario, "#0.00####") + "|"
                    CO += Format(NT.Jacumulable, "#0.00####") + "|"
                    CO += Format(NT.JnoAcumulable, "#0.00####") + "|"
                End If
            End If
            If HaySeparacion Then
                CO += Format(NT.StotalPagado, "#0.00####") + "|"
                CO += NT.SanhosServicio.ToString + "|"
                CO += Format(NT.SsueldoMensual, "#0.00####") + "|"
                CO += Format(NT.Sacumulable, "#0.00####") + "|"
                CO += Format(NT.SnoAcumulable, "#0.00####") + "|"
            End If
        End If
        'Fin Percepciones

        'Deducciones
        If TotalOtrasDeducciones <> 0 Then CO += Format(TotalOtrasDeducciones, "#0.00####") + "|"
        If TotalImpuestosRetenidos <> 0 Then CO += Format(TotalImpuestosRetenidos, "#0.00####") + "|"
        DR = VI.ConsultaReader(ID, 1)
        While DR.Read
            'por cada una
            CO += DR("tipocl") + "|" 'tipo
            CO += Trim(DR("clave")) + "|" 'clave
            CO += Trim(DR("tipode")) + "|" 'concepto
            CO += Format(DR("importegravado") + DR("importeexento"), "#0.00####") + "|" 'importe grava
        End While
        DR.Close()
        'Otros Pagos

        Dim OP As New dbNominaOtrosPagos(Comm.Connection)
        DR = OP.consultaPagos(pIdNomina)
        Dim TipoOP As String
        Dim ConOp As String
        While DR.Read
            If DR("tipopago") < 4 Then
                TipoOP = Format(DR("tipopago") + 1, "000")
            Else
                TipoOP = "999"
            End If
            ConOp = DR("concepto")
            CO += TipoOP + "|"
            CO += DR("clave") + "|"
            CO += ConOp.Substring(4, ConOp.Length - 4) + "|"
            CO += Format(DR("importe"), "0.00####") + "|"
            If TipoOP = "002" Then
                CO += Format(DR("subsidio"), "0.00####") + "|"
            End If
            If TipoOP = "004" Then
                CO += Format(DR("saldoafavor"), "0.00####") + "|"
                CO += Format(DR("anhos"), "0.00####") + "|"
                CO += Format(DR("remanente"), "0.00####") + "|"
            End If
        End While
        DR.Close()


        Dim VIn As New dbNominasIncapacidades(MySqlcon)
        'incapacidades
        DR = VIn.ConsultaReader(ID)
        Hay = 0
        While DR.Read
            CO += DR("dias").ToString + "|" 'dias
            CO += Format(DR("tin") + 1, "00") + "|" 'tipo
            CO += Format(DR("descuento"), "#0.00####") + "|" 'descuento
        End While
        DR.Close()



        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        Replace(CO, "----", "  ")
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        en.GuardaArchivoTexto("co.txt", CO, System.Text.Encoding.Default)
        Return CO
    End Function
    Public Function CreaXMLi33(ByVal pIdNomina As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer, pXMLINE As String, pEsEgreso As Byte) As String
        Dim en As New Encriptador
        Dim XMLDoc As String
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>"
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        XMLDoc += "<cfdi:Comprobante "
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdNomina
        LlenaDatos()
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdMoneda)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        XMLDoc += "Version=""3.3"" "
        If Serie <> "" Then XMLDoc += "Serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        XMLDoc += "Folio=""" + Folio.ToString + """ "
        XMLDoc += "Fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """ "
        If Sucursal.RFC <> "SUL010720JN8" Then
            XMLDoc += "Sello=""" + pSelloDigital + """ "
        Else
            XMLDoc += "Sello="""" "
        End If

        'Dim strMetodos As String = ""
        'Dim MeP As New dbVentasAddMetodos(Comm.Connection)
        'DR = MeP.ConsultaReader(0, ID)
        'DR.Read()
        'While DR.Read()
        '    If strMetodos <> "" Then strMetodos += ","
        '    If DR("clavesat") < 1000 Then
        '        strMetodos += Format(DR("clavesat"), "00")
        '    Else
        '        strMetodos += "NA"
        '    End If
        'End While
        'DR.Close()

        XMLDoc += "FormaPago=""99"" "

        If NoCertificado <> "" Then XMLDoc += "NoCertificado=""" + NoCertificado + """ "
        If Sucursal.RFC <> "SUL010720JN8" Then
            XMLDoc += "Certificado=""" + en.Certificado64 + """ "
        Else
            XMLDoc += "Certificado="""" "
        End If
        'xmldoc+="CondicionesDePago="""""

        XMLDoc += "SubTotal=""" + Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + """ "


        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        'If Descuento + DescuentoG2 > 0 Then
        '    If pEsEgreso = 0 Then
        If TotalExentoD + TotalGravadoD > 0 Then XMLDoc += "Descuento=""" + Format(TotalExentoD + TotalGravadoD, "#0.00####") + """ "
        '    Else
        '        XMLDoc += "Descuento=""" + Format(If(Descuento + DescuentoG2 >= 0, Descuento + DescuentoG2, (Descuento + DescuentoG2) * -1), "#0.00####") + """ "
        '    End If
        'End If

        'Tipo deCambio nuevo
        'If IdMoneda <> 2 Then
        '    Dim Moneda As New dbMonedas(IdMoneda, Comm.Connection)
        '    XMLDoc += "Moneda=""" + Moneda.Abreviatura + """ "
        '    XMLDoc += "TipoCambio=""" + Format(TipodeCambio, "#0.00####") + """ "
        'Else
        XMLDoc += "Moneda=""MXN"" "
        'End If

        XMLDoc += "Total=""" + Format(TotalExentoP + TotalGravadoP - TotalExentoD - TotalGravadoD + totalOtrosPagos, "#0.00####") + """ "
        XMLDoc += "TipoDeComprobante=""N"" "
        XMLDoc += "MetodoPago=""PUE"" "

        If Sucursal.CP2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.CP2 + """ "
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.CP + """ "
        End If
        'Confirmacion
        'If NoConfirmacion <> "" Then XMLDoc += " Confirmacion=""" + NoConfirmacion + """"


        XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:nomina12=""http://www.sat.gob.mx/nomina12"" "
        XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/nomina12 http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina12.xsd"

        XMLDoc += """ "

        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        XMLDoc += ">"

        XMLDoc += "<cfdi:Emisor Rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """"
        XMLDoc += " RegimenFiscal=""" + Sucursal.ClaveRegimen.ToString + """"
        XMLDoc += "/>"


        XMLDoc += "<cfdi:Receptor Rfc=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Nombre=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """"
        XMLDoc += " UsoCFDI=""P01"""
        XMLDoc += "/>"


        XMLDoc += "<cfdi:Conceptos>"

        XMLDoc += "<cfdi:Concepto "
        XMLDoc += "ClaveProdServ=""84111505"" "
        XMLDoc += "Cantidad=""1"" "
        XMLDoc += "ClaveUnidad=""ACT"" "
        'XMLDoc += "Unidad=""ACT"" "
        XMLDoc += "Descripcion=""Pago de nómina"" "
        XMLDoc += "ValorUnitario=""" + Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + """ "
        XMLDoc += "Importe=""" + Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + """ "
        If TotalExentoD + TotalGravadoD > 0 Then XMLDoc += "Descuento=""" + Format(TotalExentoD + TotalGravadoD, "#0.00####") + """ "
        XMLDoc += "/>"

        XMLDoc += "</cfdi:Conceptos>"

        '-----------------Aqui lo de la nomina
        Dim HayJubilacionUnaEx As Boolean = False
        Dim HayJubilacionParcial As Boolean = False
        Dim HaySeparacion As Boolean = False
        XMLDoc += "<cfdi:Complemento>"
        XMLDoc += "<nomina12:Nomina Version=""1.2"" TipoNomina=""" + tipoNomina + """ FechaPago=""" + Replace(FechaPago, "/", "-") + """ FechaInicialPago=""" + Replace(FechaInicialPago, "/", "-") + """ FechaFinalPago=""" + Replace(FechaFinalPAgo, "/", "-") + """ NumDiasPagados=""" + Format(DiasPagados, "0") + """ "
        If TotalExentoP + TotalGravadoP <> 0 Then
            XMLDoc += "TotalPercepciones=""" + Format(TotalExentoP + TotalGravadoP, "#0.00####") + """ "
        End If
        If TotalExentoD + TotalGravadoD <> 0 Then
            XMLDoc += "TotalDeducciones=""" + Format(TotalExentoD + TotalGravadoD, "#0.00####") + """ "
        End If
        If totalOtrosPagos <> 0 Or CantidadOtrosPagos <> 0 Then
            XMLDoc += "TotalOtrosPagos=""" + Format(totalOtrosPagos, "#0.00####") + """ "
        End If
        XMLDoc += ">"
        XMLDoc += "<nomina12:Emisor "
        If Sucursal.CURP <> "" Then
            XMLDoc += "Curp=""" + Sucursal.CURP + """ "
        End If
        If Trabajador.RegistroPatronal <> "" Then
            XMLDoc += "RegistroPatronal=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.RegistroPatronal, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.RFCPatronOrigen <> "" Then
            XMLDoc += "RfcPatronOrigen=""" + Trabajador.RFCPatronOrigen + """"
        End If
        XMLDoc += ">"
        If origenRecurso <> "NA" And origenRecurso <> "" Then
            XMLDoc += "<nomina12:EntidadSNCF OrigenRecurso=""" + origenRecurso + """ "
            If origenRecurso = "IM" Then
                XMLDoc += "MontoRecursoPropio=""" + Format(montoRecurso, "0.00####") + """ "
            End If
            XMLDoc += "/>"
        End If
        XMLDoc += "</nomina12:Emisor>"
        XMLDoc += "<nomina12:Receptor Curp=""" + Trabajador.Curp + """ "
        If Trabajador.NumeroSeguroSocial <> "" Then
            XMLDoc += "NumSeguridadSocial=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.NumeroSeguroSocial, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Antiguedad <> 0 And Trabajador.RegistroPatronal <> "" Then
            XMLDoc += "FechaInicioRelLaboral=""" + Replace(Trabajador.FechaInicioLaboral, "/", "-") + """ "
            XMLDoc += "Antigüedad=""P" + CStr(Antiguedad) + "W"" "
        End If
        'XMLDoc += "TipoContrato=""01"" "
        XMLDoc += "TipoContrato=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.TipoContrato.Substring(0, 2), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.sindicalizado = 1 Then
            XMLDoc += "Sindicalizado=""Sí"" "
        End If
        If Trabajador.TipoJornada <> "No aplica" Then
            'XMLDoc += "TipoJornada=""01"" "
            XMLDoc += "TipoJornada=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.TipoJornada.Substring(0, 2), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.TipoRegimen < 12 Then
            XMLDoc += "TipoRegimen=""" + Trabajador.TipoRegimen.ToString("00") + """ "
        Else
            XMLDoc += "TipoRegimen=""99"" "
        End If
        XMLDoc += "NumEmpleado=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.NumeroEmpleado, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.Departamento <> "" Then
            XMLDoc += "Departamento=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Departamento, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.Puesto <> "" Then
            XMLDoc += "Puesto=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Puesto, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.RiesgoPuesto < 6 Then
            XMLDoc += "RiesgoPuesto=""" + Trabajador.RiesgoPuesto.ToString + """ "
        End If
        If Trabajador.Periodicidad <> "" And Trabajador.Periodicidad <> "No aplica" Then XMLDoc += "PeriodicidadPago=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Periodicidad.Substring(0, 2), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'XMLDoc += "PeriodicidadPago=""04"" "
        If Trabajador.Banco > 0 Then
            XMLDoc += " Banco=""" + Format(Trabajador.Banco, "000") + """ "
            If Clabe <> "" And Clabe.Length <> 18 Then XMLDoc += "CuentaBancaria=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.CLABE, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.Banco = 0 And Trabajador.CLABE.Length = 18 Then
            If Clabe <> "" Then XMLDoc += "CuentaBancaria=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.CLABE, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.SalarioBaseCotApor <> 0 Then
            XMLDoc += "SalarioBaseCotApor=""" + Format(Trabajador.SalarioBaseCotApor, "#0.00####") + """ "
        End If
        If Trabajador.SalarioDiarioIntegrado <> 0 Then
            XMLDoc += "SalarioDiarioIntegrado=""" + Format(Trabajador.SalarioDiarioIntegrado, "#0.00####") + """ "
        End If
        XMLDoc += "ClaveEntFed=""" + DaClaveEstadosMexico(Trabajador.EstadoLabora) + """ "
        XMLDoc += ">"
        Dim Subc As New dbnominasubcontratacion(Comm.Connection)
        DR = Subc.ConsultaReader(IdTrabajador)
        While DR.Read
            XMLDoc += "<nomina12:SubContratacion RfcLabora=""" + DR("rfclaboral") + """ PorcentajeTiempo=""" + DR("porcentaje").ToString + """/>"
        End While
        DR.Close()
        XMLDoc += "</nomina12:Receptor>"
        Dim HE As New dbNoominaHorasExtra(Comm.Connection)
        Dim HorasCol As New Collection
        Dim HoraE As HEx
        DR = HE.ConsultaReaderCxml(pIdNomina)
        While DR.Read
            HoraE.IdDetalle = DR("iddetalle")
            HoraE.Dias = DR("dias")
            HoraE.HorasExtra = DR("horasextra")
            HoraE.TipoHoras = DR("tipohoras")
            HoraE.ImportePagado = DR("importepagado")
            HorasCol.Add(HoraE)
        End While
        DR.Close()
        Dim VI As New dbNominasDetalles(Comm.Connection)
        'Percepciones
        DR = VI.ConsultaReader(ID, 0)
        Dim Hay As Byte = 0
        While DR.Read
            HayJubilacionUnaEx = False
            HayJubilacionParcial = False
            HaySeparacion = False
            If Hay = 0 Then
                XMLDoc += "<nomina12:Percepciones "
                If TotalSueldos <> 0 Then
                    XMLDoc += "TotalSueldos=""" + Format(TotalSueldos, "#0.00####") + """ "
                End If
                If TotalSeparacion <> 0 Then
                    XMLDoc += "TotalSeparacionIndemnizacion=""" + Format(TotalSeparacion, "#0.00####") + """ "
                End If
                If TotalJubilacion <> 0 Then
                    XMLDoc += "TotalJubilacionPensionRetiro=""" + Format(TotalJubilacion, "#0.00####") + """ "
                End If
                XMLDoc += "TotalGravado=""" + Format(TotalGravadoP, "#0.00####") + """ TotalExento=""" + Format(TotalExentoP, "#0.00####") + """>"
                Hay = 1
            End If
            If DR("tipocl") = "039" Then
                HayJubilacionUnaEx = True
            End If
            If DR("tipocl") = "044" Then
                HayJubilacionParcial = True
            End If
            If DR("tipocl") = "022" Or DR("tipocl") = "023" Or DR("tipocl") = "025" Then
                HaySeparacion = True
            End If
            XMLDoc += "<nomina12:Percepcion ImporteGravado=""" + Format(DR("importegravado"), "#0.00####") + """ ImporteExento=""" + Format(DR("importeexento"), "#0.00####") + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("tipode"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoPercepcion=""" + DR("tipocl") + """ "
            If DR("tipocl") = "045" Or DR("tipocl") = "019" Then
                XMLDoc += ">"
            Else
                XMLDoc += "/>"
            End If
            If DR("tipocl") = "045" Then
                XMLDoc += "<nomina12:AccionesOTitulos ValorMercado=""" + Format(DR("valormercado"), "#0.00####") + """ PrecioAlOtorgarse=""" + Format(DR("precioalotorgarse"), "#0.00####") + """ />"
            End If
            If DR("tipocl") = "019" Then
                For Each horita As HEx In HorasCol
                    If horita.IdDetalle = DR("iddetalle") Then
                        XMLDoc += "<nomina12:HorasExtra Dias=""" + horita.Dias.ToString + """ TipoHoras=""" + horita.TipoHoras.Substring(0, 2) + """ HorasExtra=""" + horita.HorasExtra.ToString + """ ImportePagado=""" + Format(horita.ImportePagado, "#0.00####") + """ />"
                    End If
                Next
            End If
            If DR("tipocl") = "045" Or DR("tipocl") = "019" Then
                XMLDoc += "</nomina12:Percepcion>"
            End If
        End While
        DR.Close()
        Dim NT As New dbNominaTRabajador(pIdNomina, Comm.Connection)
        If NT.HayHatos Then
            If HayJubilacionUnaEx Then
                XMLDoc += "<nomina12:JubilacionPensionRetiro TotalUnaExhibicion=""" + Format(NT.JtotalUnaExhibicion, "#0.00####") + """ IngresoAcumulable=""" + Format(NT.Jacumulable, "#0.00####") + """ IngresoNoAcumulable=""" + Format(NT.JnoAcumulable, "#0.00####") + """ />"
            Else
                If HayJubilacionParcial Then
                    XMLDoc += "<nomina12:JubilacionPensionRetiro TotalParcialidad=""" + Format(NT.JtotalParcialidad, "#0.00####") + """ MontoDiario=""" + Format(NT.JmontoDiario, "#0.00####") + """ IngresoAcumulable=""" + Format(NT.Jacumulable, "#0.00####") + """ IngresoNoAcumulable=""" + Format(NT.JnoAcumulable, "#0.00####") + """ />"
                End If
            End If
            If HaySeparacion Then
                XMLDoc += "<nomina12:SeparacionIndemnizacion TotalPagado=""" + Format(NT.StotalPagado, "#0.00####") + """ NumAñosServicio=""" + NT.SanhosServicio.ToString + """ UltimoSueldoMensOrd=""" + Format(NT.SsueldoMensual, "#0.00####") + """ IngresoAcumulable=""" + Format(NT.Sacumulable, "#0.00####") + """ IngresoNoAcumulable=""" + Format(NT.SnoAcumulable, "#0.00####") + """/>"
            End If
        End If
        If Hay = 1 Then
            XMLDoc += "</nomina12:Percepciones>"
        End If
        'Deducciones
        DR = VI.ConsultaReader(ID, 1)
        Hay = 0
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina12:Deducciones "
                If TotalOtrasDeducciones <> 0 Then
                    XMLDoc += " TotalOtrasDeducciones=""" + Format(TotalOtrasDeducciones, "#0.00####") + """ "
                End If
                If TotalImpuestosRetenidos <> 0 Then
                    XMLDoc += " TotalImpuestosRetenidos=""" + Format(TotalImpuestosRetenidos, "#0.00####") + """ "
                End If
                XMLDoc += ">"
                Hay = 1
            End If
            XMLDoc += "<nomina12:Deduccion Importe=""" + Format(DR("importegravado") + DR("importeexento"), "#0.00####") + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("tipode"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoDeduccion=""" + DR("tipocl") + """ />"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina12:Deducciones>"
        End If

        Dim OP As New dbNominaOtrosPagos(Comm.Connection)
        DR = OP.consultaPagos(pIdNomina)
        Hay = 0
        Dim TipoOP As String
        Dim ConOP As String
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina12:OtrosPagos>"
                Hay = 1
            End If
            If DR("tipopago") < 4 Then
                TipoOP = Format(DR("tipopago") + 1, "000")
            Else
                TipoOP = "999"
            End If
            ConOP = DR("concepto")
            XMLDoc += "<nomina12:OtroPago  TipoOtroPago=""" + TipoOP + """ Clave=""" + DR("clave") + """ Concepto=""" + ConOP.Substring(4, ConOP.Length - 4) + """ Importe=""" + Format(DR("importe"), "0.00####") + """>"
            If TipoOP = "002" Then
                XMLDoc += "<nomina12:SubsidioAlEmpleo SubsidioCausado=""" + Format(DR("subsidio"), "0.00####") + """/>"
            End If
            If TipoOP = "004" Then
                XMLDoc += "<nomina12:CompensacionSaldosAFavor SaldoaFavor=""" + Format(DR("saldoafavor"), "0.00####") + """ Año=""" + Format(DR("anhos"), "0.00####") + """ RemanenteSalFav=""" + Format(DR("remanente"), "0.00####") + """ />"
            End If
            XMLDoc += "</nomina12:OtroPago>"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina12:OtrosPagos>"
        End If
        Dim VIn As New dbNominasIncapacidades(MySqlcon)
        'Incapacidades
        DR = VIn.ConsultaReader(ID)
        Hay = 0
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina12:Incapacidades>"
                Hay = 1
            End If
            XMLDoc += "<nomina12:Incapacidad ImporteMonetario=""" + Format(DR("descuento"), "#0.00####") + """ TipoIncapacidad=""" + Format(DR("tin") + 1, "00") + """ DiasIncapacidad=""" + DR("dias").ToString + """ />"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina12:Incapacidades>"
        End If

        XMLDoc += "</nomina12:Nomina>"
        XMLDoc += "</cfdi:Complemento>"


        XMLDoc += "</cfdi:Comprobante>"
        Return XMLDoc

    End Function

End Class
