Imports MySql.Data.MySqlClient
Public Class dbGastos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Dim NombreClasificacion As String 'clasificaciones
    Dim IDClasificacion As String 'clasificaciones
    Public ID As Integer
    Public ID2 As Integer
    Public ID3 As Integer
    'Public IdCliente As Integer
    Public Fecha As String
    'Public Cliente As dbClientes
    Public Folio As Integer
    ' Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Subtotal As Double
    ' Public TotalIva As Double
    ' Public TotalVenta As Double
    Public Hora As String
    Public Estado As Byte
    Public Desglosar As Byte
    Public IdSucursal As Integer
    Public Serie As String
    Public IdCaja As Integer
    Public Usado As Byte
    Public IdVendedor As Integer
    Public Comentario As String
    Public TotalPeso As Double
    Public Tipo As Byte
    Public NombreClas As String
    Public NombreClas2 As String
    Public NombreClas3 As String
    Public ClaveClas As String
    Public ClaveClas2 As String
    Public ClaveClas3 As String

    Public Enum TiposFactura As Byte
        Enproceso = 0
        Facturado = 1
        Cancelado = 2
    End Enum
    Public Function esRepetida(ByVal pNombre As String) As Boolean
        Dim repetida As Boolean = False
        Comm.CommandText = "select count(nombre) from tblgastosclasificacion where nombre='" + pNombre + "'"
        If Comm.ExecuteScalar > 0 Then
            repetida = True
        End If
        Return repetida
    End Function
    Public Sub GuardarClasificacion(ByVal pNombre As String, pClave As String)
        Comm.CommandText = "insert into tblgastosclasificacion (nombre,clave,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(pNombre, "'", "''") + "','" + Replace(pClave, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idclasificacion) from tblgastosclasificacion"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Guardar2(ByVal pNombre As String, ByVal pIdSuperior As Integer, pClave As String)
        Comm.CommandText = "insert into tblgastosclasificacion2(nombre,idclassuperior,clave,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(pNombre, "'", "''") + "'," + pIdSuperior.ToString + ",'" + Replace(pClave, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idclasificacion) from tblgastosclasificacion2"
        ID2 = Comm.ExecuteScalar
    End Sub
    Public Sub Guardar3(ByVal pNombre As String, ByVal pIdSuperior As Integer, pClave As String)
        Comm.CommandText = "insert into tblgastosclasificacion3(nombre,idclassuperior,clave,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(pNombre, "'", "''") + "'," + pIdSuperior.ToString + ",'" + Replace(pClave, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idclasificacion) from tblgastosclasificacion3"
        ID2 = Comm.ExecuteScalar
    End Sub
    Public Sub ModificarClasificacion(ByVal pId As Integer, ByVal pNombre As String, pClave As String)
        'Modifica
        Comm.CommandText = "update tblgastosclasificacion set nombre='" + Replace(pNombre, "'", "''") + "',clave='" + Replace(pClave, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idClasificacion=" + pId.ToString()
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar2(ByVal pId As Integer, ByVal pNombre As String, pClave As String)
        'Modifica
        Comm.CommandText = "update tblgastosclasificacion2 set nombre='" + Replace(pNombre, "'", "''") + "',clave='" + Replace(pClave, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idClasificacion=" + pId.ToString()
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar3(ByVal pId As Integer, ByVal pNombre As String, pClave As String)
        'Modifica
        Comm.CommandText = "update tblgastosclasificacion3 set nombre='" + Replace(pNombre, "'", "''") + "',clave='" + Replace(pClave, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idClasificacion=" + pId.ToString()
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ChecaCodigoRepetido(ByVal pCodigo As String, pIDnivelSuperior As Integer, pTipo As Byte) As Boolean
        Dim Resultado As Integer = 0
        Select Case pTipo
            Case 1
                Comm.CommandText = "select count(clave) from tblgastosclasificacion where clave='" + Replace(pCodigo, "'", "''") + "'"
            Case 2
                Comm.CommandText = "select count(clave) from tblgastosclasificacion2 where clave='" + Replace(pCodigo, "'", "''") + "' and idclassuperior=" + pIDnivelSuperior.ToString
            Case 3
                Comm.CommandText = "select count(clave) from tblgastosclasificacion3 where clave='" + Replace(pCodigo, "'", "''") + "' and idclassuperior=" + pIDnivelSuperior.ToString
        End Select
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaCodigoRepetido = False
        Else
            ChecaCodigoRepetido = True
        End If
    End Function
    Public Function filtroClasificacion(ByVal pNombre As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblgastosclasificacion  where nombre LIKE '%" + pNombre.ToString() + "%' "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblgastosclasificacion ")
        Return DS.Tables("tblgastosclasificacion ")
    End Function
    Public Sub Eliminar(ByVal pId As Integer)
        'Eliminar
        Comm.CommandText = "delete from tblgastosclasificacion where idClasificacion=" + pId.ToString()
        Comm.ExecuteNonQuery()

    End Sub
    Public Sub Eliminar2(ByVal pId As Integer)
        'Eliminar
        Comm.CommandText = "delete from tblgastosclasificacion2 where idClasificacion=" + pId.ToString()
        Comm.ExecuteNonQuery()

    End Sub
    Public Sub Eliminar3(ByVal pId As Integer)
        'Eliminar
        Comm.CommandText = "delete from tblgastosclasificacion3 where idClasificacion=" + pId.ToString()
        Comm.ExecuteNonQuery()

    End Sub
    Public Function idProximo() As Integer
        Dim sig As Integer
        Comm.CommandText = "select count(idClasificacion) from tblgastosclasificacion"
        sig = Comm.ExecuteScalar
        If sig > 0 Then
            Comm.CommandText = "select max(idClasificacion) from tblgastosclasificacion"
            sig = Comm.ExecuteScalar
        End If
       

        Return sig + 1
    End Function
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        'IdCliente = -1
        Fecha = ""
        Hora = ""
        Serie = ""
        Folio = 0
        Desglosar = 0
        'Iva = 0
        TotalaPagar = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        IdCaja = 1
        Usado = 0
        Tipo = 0
        IdVendedor = 0
        Comentario = ""
        Comm.Connection = Conexion
        'Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblgastos where idmovimiento=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            Hora = DReader("hora")
            Estado = DReader("estado")
            Total = DReader("total")
            IdSucursal = DReader("idsucursal")
            Serie = DReader("serie")
            IdCaja = DReader("idcaja")
            IdVendedor = DReader("idvendedor")
            Comentario = DReader("comentario")
            Tipo = DReader("tipo")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pTotal As Double, ByVal pidSucursal As Integer, ByVal pSerie As String, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pTipo As Byte, ByVal pComentario As String)
        Fecha = pFecha
        Folio = pFolio
        Total = pTotal
        IdSucursal = pidSucursal
        Serie = pSerie
        IdCaja = pidCaja
        IdVendedor = pIdVendedor
        Comentario = pComentario
        Tipo = pTipo

        Comm.CommandText = "insert into tblgastos(fecha,folio,hora,estado,total,idsucursal,serie,idcaja,idvendedor,comentario,tipo,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Fecha + "'," + Folio.ToString + ",'" + Format(TimeOfDay, "HH:mm:ss") + "'," + CStr(Estados.SinGuardar) + "," + Total.ToString() + "," + IdSucursal.ToString + ",'" + Replace(Trim(Serie), "'", "''") + "'," + IdCaja.ToString + "," + IdVendedor.ToString + ",'" + Comentario + "'," + Tipo.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH.mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        'Comm.ExecuteNonQuery()
        Comm.CommandText += "select max(idmovimiento) from tblgastos;"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pTotal As Double, ByVal pidSucursal As Integer, ByVal pSerie As String, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pTipo As Byte, ByVal pComentario As String, ByVal pEstado As Integer)
        ID = pID
        Fecha = pFecha '
        Folio = pFolio '
        Total = pTotal
        IdSucursal = pidSucursal
        Serie = pSerie
        IdCaja = pidCaja
        IdVendedor = pIdVendedor
        Comentario = pComentario
        Tipo = pTipo
        Estado = pEstado
        Comm.CommandText = "update tblgastos set fecha='" + Fecha + "',folio=" + Folio.ToString + ",estado=" + Estado.ToString + ",total=" + Total.ToString + ",serie='" + Replace(Trim(Serie), "'", "''") + "',idvendedor=" + IdVendedor.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "',tipo=" + Tipo.ToString + ",idcaja=" + IdCaja.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idmovimiento=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaComentario(ByVal pID As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblgastos set comentario='" + Replace(pTexto, "'", "''") + "' where idMovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarGasto(ByVal pID As Integer)
        Comm.CommandText = "delete from tblgastos where idmovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pEstado As Byte, ByVal pFolio As String, ByVal pidSucursal As Integer, ByVal pidCaja As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblgastos.idmovimiento,tblgastos.fecha,concat(tblgastos.serie,convert(tblgastos.folio using utf8)),round(tblgastos.total) as total,tblcajas.nombre as caja,case tblgastos.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado from tblgastos inner join tblcajas on tblgastos.idcaja=tblcajas.idcaja where tblgastos.fecha>='" + pFecha + "' and tblgastos.fecha<='" + pFecha2 + "'"
        'If pNombreClave <> "" Then
        '    Comm.CommandText += " and concat(tblclientes.clave,tblclientes.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        'End If
        If pEstado <> Estados.Inicio Then
            Comm.CommandText += " and tblgastos.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblgastos.estado<>1"
        End If
        'If pTipo > 0 Then
        '    Comm.CommandText += " and tblgastos.tipo=" + CStr(pTipo - 1)
        'End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblgastos.idsucursal=" + pidSucursal.ToString
            If pidCaja > 0 Then
                Comm.CommandText += " and tblgastos.idcaja=" + pidCaja.ToString
            End If
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblgastos.serie,convert(tblgastos.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If

        Comm.CommandText += " order by tblgastos.fecha desc,tblgastos.serie,tblgastos.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblgastos")
        Return DS.Tables("tblgastos").DefaultView
    End Function
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblgastos where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4)),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblgastos where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado<>1 and estado<>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    Public Function BuscaMovimiento(ByVal pFolio As String) As Integer
        Comm.CommandText = "select ifnull((select idmovimiento from tblgastos where concat(tblgastos.serie,convert(tblgastos.folio using utf8))='" + Replace(pFolio, "'", "''") + "' and estado=3 limit 1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaId(ByVal pFolio As String) As Integer
        Comm.CommandText = "select ifnull((select idmovimiento from tblgastos where tblgastos.estado=3 and usado=0 and concat(tblgastos.serie,convert(tblgastos.folio using utf8))='" + Replace(pFolio, "'", "''") + "'),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function buscaClasificacion(ByVal pidClasificacion As Integer) As String
        Dim Resultado As String
        Comm.CommandText = "select nombre from tblgastosclasificacion where idClasificacion=" + pidClasificacion.ToString
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function
    Public Function filtroTodos(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblgastos.idmovimiento, tblgastos.fecha, CONCAT(tblgastos.serie,' ',tblgastos.folio)as serieFolio, tblgastos.total, tblvendedores.nombre as vendedor from tblgastos inner join  tblvendedores on tblgastos.idvendedor = tblvendedores.idvendedor where tblgastos.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblgastos.fecha>='" + pFechaI + "' and tblgastos.fecha<='" + pFechaF + "' "
        End If
        If pidCaja > 0 Then
            Comm.CommandText += " and tblgastos.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor > 0 Then
            Comm.CommandText += " and tblgastos.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblgastos.idsucursal=" + pidSucursal.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblgastos")
        'DS.WriteXmlSchema("tblgastos.xml")
        Return DS.Tables("tblgastos").DefaultView
    End Function
    Public Function filtroDetalles(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pidSucursal As Integer, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer, ByVal pClasificacion As Integer, pIdClas2 As Integer, pIdClas3 As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblgastos.idmovimiento, tblgastos.fecha, CONCAT(tblgastos.serie,' ',tblgastos.folio)as serieFolio, tblgastos.total, tblvendedores.nombre as vendedor, tblgastosdetalles.descripcion, tblgastosdetalles.precio, tblgastosdetalles.esSalario,ifnull((select nombre from tblgastosclasificacion where tblgastosclasificacion.idclasificacion=tblgastosdetalles.clasificacion),'GENERAL') as clasificacion,tblgastosdetalles.clasificacion as idclasificacion,tblgastosdetalles.idclasificacion2,ifnull((select nombre from tblgastosclasificacion2 where tblgastosclasificacion2.idclasificacion=tblgastosdetalles.idclasificacion2),'GENERAL') as clas2,tblgastosdetalles.idclasificacion3,ifnull((select nombre from tblgastosclasificacion3 where tblgastosclasificacion3.idclasificacion=tblgastosdetalles.idclasificacion3),'GENERAL') as clas3 from tblgastosdetalles  inner join tblgastos on tblgastosdetalles.idmovimiento=tblgastos.idmovimiento inner join  tblvendedores on tblgastos.idvendedor = tblvendedores.idvendedor where tblgastos.estado=3 "

        If pFechaF <> "" Then
            Comm.CommandText += " and tblgastos.fecha>='" + pFechaI + "' and tblgastos.fecha<='" + pFechaF + "' "
        End If
        If pidCaja > 0 Then
            Comm.CommandText += " and tblgastos.idcaja=" + pidCaja.ToString
        End If
        If pIdVendedor > 0 Then
            Comm.CommandText += " and tblgastos.idvendedor=" + pIdVendedor.ToString
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblgastos.idsucursal=" + pidSucursal.ToString
        End If
        If pClasificacion > 0 Then
            Comm.CommandText += " and tblgastosdetalles.clasificacion=" + pClasificacion.ToString
        End If
        If pIdClas2 > 0 Then
            Comm.CommandText += " and tblgastosdetalles.idclasificacion2=" + pIdClas2.ToString
        End If
        If pIdClas3 > 0 Then
            Comm.CommandText += " and tblgastosdetalles.idclasificacion3=" + pIdClas2.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblgastosDetalles")
        'DS.WriteXmlSchema("tblgastosDetalles.xml")
        Return DS.Tables("tblgastosDetalles").DefaultView
    End Function
    Public Function ChecaNombreRepetido(ByVal pNombre As String, ByVal pIDnivelSuperior As Integer, pTipo As Byte) As Boolean
        Dim Resultado As Integer = 0
        Select Case pTipo
            Case 1
                Comm.CommandText = "select count(nombre) from tblgastosclasificacion where nombre='" + Replace(pNombre, "'", "''") + "'"
            Case 2
                Comm.CommandText = "select count(nombre) from tblgastosclasificacion2 where nombre='" + Replace(pNombre, "'", "''") + "' and idclassuperior=" + pIDnivelSuperior.ToString
            Case 3
                Comm.CommandText = "select count(nombre) from tblgastosclasificacion3 where nombre='" + Replace(pNombre, "'", "''") + "' and idclassuperior=" + pIDnivelSuperior.ToString
        End Select
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Sub DaDatosClasificacion(ByVal pId As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblgastosclasificacion where idclasificacion=" + pId.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            ClaveClas = DReader("clave")
            NombreClas = DReader("nombre")
        End If
        DReader.Close()
    End Sub
    Public Sub DaDatosClasificacion2(ByVal pId As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblgastosclasificacion2 where idclasificacion=" + pId.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            ClaveClas2 = DReader("clave")
            NombreClas2 = DReader("nombre")
        End If
        DReader.Close()
    End Sub
    Public Sub DaDatosClasificacion3(ByVal pId As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblgastosclasificacion3 where idclasificacion=" + pId.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            ClaveClas3 = DReader("clave")
            NombreClas3 = DReader("nombre")
        End If
        DReader.Close()
    End Sub
    Public Function DaSiguienteCodigo(ByVal pIDnivelSuperior As Integer, pTipo As Byte) As String
        Dim Resultado As Integer = 0
        Select Case pTipo
            Case 1
                Comm.CommandText = "select count(clave) from tblgastosclasificacion"
            Case 2
                Comm.CommandText = "select count(clave) from tblgastosclasificacion2 where idclassuperior=" + pIDnivelSuperior.ToString
            Case 3
                Comm.CommandText = "select count(clave) from tblgastosclasificacion3 where idclassuperior=" + pIDnivelSuperior.ToString
        End Select
        Resultado = Comm.ExecuteScalar + 1
        Return Format(Resultado, "000")
    End Function
    Public Function DaUltimaFecha() As String
        Dim F As String
        Comm.CommandText = "select ifnull((select max(fecha) from tblgastos),'-')"
        F = Comm.ExecuteScalar
        If F = "-" Then
            Return Format(Date.Now, "yyyy/MM/dd")
        Else
            Return F
        End If
    End Function
End Class
