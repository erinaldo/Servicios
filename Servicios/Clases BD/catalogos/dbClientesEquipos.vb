﻿Public Class dbClientesEquipos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Marca As String
    Public Modelo As String
    Public NoSerie As String
    Public NoMotor As String
    Public Matricula As String
    Public Color As String
    Public Kilometraje As Double
    Public IdCliente As Integer
    Public fechaEnvio As Date
    Public estado As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Marca = ""
        Modelo = ""
        NoSerie = ""
        NoMotor = ""
        Matricula = ""
        Color = ""
        Kilometraje = 0
        IdCliente = 0
        fechaEnvio = Today
        estado = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblclientesequipos where idequipo=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Marca = DReader("marca")
            Modelo = DReader("modelo")
            NoSerie = DReader("noserie")
            NoMotor = DReader("nomotor")
            Matricula = DReader("matricula")
            Color = DReader("color")
            Kilometraje = DReader("kilometraje")
            IdCliente = DReader("idcliente")
            fechaEnvio = DReader("fechaEnvio")
            estado = DReader("estado")
        End If
        DReader.Close()
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection, ByVal algo As Integer)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblsucequipos where idequipo=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Marca = DReader("marca")
            Modelo = DReader("modelo")
            NoSerie = DReader("noserie")
            NoMotor = DReader("nomotor")
            Matricula = DReader("matricula")
            Color = DReader("color")
            Kilometraje = DReader("kilometraje")
            IdCliente = DReader("idsucursal")
        End If
        DReader.Close()
    End Sub
    Public Function Guardar(ByVal pNombre As String, ByVal pMarca As String, ByVal pModelo As String, ByVal pNoSerie As String, ByVal pNoMotor As String, ByVal pMatricula As String, ByVal pColor As String, ByVal pKilometraje As Double, ByVal pIdCliente As Integer, ByVal pfechaEnvio As Date, ByVal pEstado As Integer) As Integer
        Nombre = pNombre
        Marca = pMarca
        Modelo = pModelo
        NoSerie = pNoSerie
        NoMotor = pNoMotor
        Matricula = pMatricula
        Color = pColor
        Kilometraje = pKilometraje
        IdCliente = pIdCliente
        fechaEnvio = pfechaEnvio
        estado = pEstado
        'MsgBox(fechaEnvio.ToString("yyyy/MM/dd"))
        'MsgBox(estado)
        Dim id As Integer
        Comm.CommandText = "insert into tblclientesequipos(nombre,marca,modelo,noserie,nomotor,matricula,color,kilometraje,idcliente,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,fechaEnvio,estado) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Marca, "'", "''") + "','" + Replace(Modelo, "'", "''") + "','" + Replace(NoSerie, "'", "''") + "','" + Replace(NoMotor, "'", "''") + "','" + Replace(Matricula, "'", "''") + "','" + Replace(Color, "'", "''") + "'," + Kilometraje.ToString + "," + IdCliente.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "','" + fechaEnvio.ToString("yyyy/MM/dd") + "','" + estado.ToString + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "SELECT MAX(idequipo) from tblclientesequipos"
        id = Comm.ExecuteScalar
        Return id

    End Function
    Public Function GuardarSucursal(ByVal pNombre As String, ByVal pMarca As String, ByVal pModelo As String, ByVal pNoSerie As String, ByVal pNoMotor As String, ByVal pMatricula As String, ByVal pColor As String, ByVal pKilometraje As Double, ByVal pIdCliente As Integer) As Integer
        Nombre = pNombre
        Marca = pMarca
        Modelo = pModelo
        NoSerie = pNoSerie
        NoMotor = pNoMotor
        Matricula = pMatricula
        Color = pColor
        Kilometraje = pKilometraje
        IdCliente = pIdCliente
        Dim id As Integer
        Comm.CommandText = "insert into tblsucequipos(nombre,marca,modelo,noserie,nomotor,matricula,color,kilometraje,idsucursal,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Marca, "'", "''") + "','" + Replace(Modelo, "'", "''") + "','" + Replace(NoSerie, "'", "''") + "','" + Replace(NoMotor, "'", "''") + "','" + Replace(Matricula, "'", "''") + "','" + Replace(Color, "'", "''") + "'," + Kilometraje.ToString + "," + IdCliente.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "SELECT MAX(idequipo) from tblsucequipos"
        id = Comm.ExecuteScalar
        Return id

    End Function
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pMarca As String, ByVal pModelo As String, ByVal pNoSerie As String, ByVal pNoMotor As String, ByVal pMatricula As String, ByVal pColor As String, ByVal pKilometraje As Double, ByVal pfechaEnvio As Date, ByVal pEstado As Integer)
        ID = pID
        Nombre = pNombre
        Marca = pMarca
        Modelo = pModelo
        NoSerie = pNoSerie
        NoMotor = pNoMotor
        Matricula = pMatricula
        Color = pColor
        Kilometraje = pKilometraje
        fechaEnvio = pfechaEnvio
        estado = pEstado
        Comm.CommandText = "update tblclientesequipos set nombre='" + Replace(Nombre, "'", "''") + "',marca='" + Replace(Marca, "'", "''") + "',modelo='" + Replace(Modelo, "'", "''") + "',noserie='" + Replace(NoSerie, "'", "''") + "',nomotor='" + Replace(NoMotor, "'", "''") + "',matricula='" + Replace(Matricula, "'", "''") + "',color='" + Replace(Color, "'", "''") + "',kilometraje=" + Kilometraje.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "', fechaEnvio='" + fechaEnvio.ToString("yyyy/MM/dd") + "', estado='" + estado.ToString + "' where idequipo=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarSucursal(ByVal pID As Integer, ByVal pNombre As String, ByVal pMarca As String, ByVal pModelo As String, ByVal pNoSerie As String, ByVal pNoMotor As String, ByVal pMatricula As String, ByVal pColor As String, ByVal pKilometraje As Double)
        ID = pID
        Nombre = pNombre
        Marca = pMarca
        Modelo = pModelo
        NoSerie = pNoSerie
        NoMotor = pNoMotor
        Matricula = pMatricula
        Color = pColor
        Kilometraje = pKilometraje
        Comm.CommandText = "update tblsucequipos set nombre='" + Replace(Nombre, "'", "''") + "',marca='" + Replace(Marca, "'", "''") + "',modelo='" + Replace(Modelo, "'", "''") + "',noserie='" + Replace(NoSerie, "'", "''") + "',nomotor='" + Replace(NoMotor, "'", "''") + "',matricula='" + Replace(Matricula, "'", "''") + "',color='" + Replace(Color, "'", "''") + "',kilometraje=" + Kilometraje.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idequipo=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblclientesequipos where idequipo=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarSucursal(ByVal pID As Integer)
        Comm.CommandText = "delete from tblsucequipos where idequipo=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function cartasPorEnviar()
        Comm.CommandText = "SELECT count(idequipo) FROM tblclientesequipos where (fechaEnvio <= curdate() AND estado = 1)"
        Comm.ExecuteNonQuery()
        Dim pendientes = Comm.ExecuteScalar
        Return pendientes
    End Function
    Public Function aumentaFecha(ByVal pID As Integer, ByVal pFecha As Date, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Dim fecha As Date = pFecha.AddMonths(6)
        'Dim fechastring As String = fecha.Year.ToString + "/" + fecha.Month.ToString + "/" + fecha.Day.ToString
        'MsgBox("Fecha: " + fecha)
        Comm.Connection = Conexion
        Comm.CommandText = "update tblclientesequipos set fechaEnvio='" + fecha.ToString("yyyy/MM/dd") + "' where idequipo=" + ID.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "SELECT MAX(idequipo) from tblclientesequipos"
        Return 0
    End Function
    Public Function Consulta(ByVal pIdcliente As Integer, Optional ByVal pTextoBusqueda As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idequipo,nombre,matricula,noserie from tblclientesequipos where concat(nombre,matricula) like '%" + Replace(pTextoBusqueda, "'", "''") + "%' and idcliente=" + pIdcliente.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesequipos")
        Return DS.Tables("tblclientesequipos").DefaultView
    End Function
    Public Function ConsultaSuc(ByVal pIdcliente As Integer, Optional ByVal pTextoBusqueda As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idequipo,nombre,matricula,noserie from tblsucequipos where concat(nombre,matricula) like '%" + Replace(pTextoBusqueda, "'", "''") + "%' and idsucursal=" + pIdcliente.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesequipos")
        Return DS.Tables("tblclientesequipos").DefaultView
    End Function
    Public Function ConsultaSucLibres(ByVal pIdSucursal As Integer, ByVal pTextoBusqueda As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idequipo,nombre,matricula,noserie from tblsucequipos where concat(nombre,matricula) like '%" + Replace(pTextoBusqueda, "'", "''") + "%' and idsucursal=" + pIdSucursal.ToString + " and ifnull((select false from tblfertilizantespedidos inner join tblfertilizantesequipos on tblfertilizantespedidos.idpedido=tblfertilizantesequipos.idpedido where tblfertilizantesequipos.idequipo=tblsucequipos.idequipo and tblfertilizantespedidos.estadopedido=0 and tblfertilizantespedidos.estado<>4),true)"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesequipos")
        Return DS.Tables("tblclientesequipos").DefaultView
    End Function
    Public Function Consulta2(ByVal pIdcliente As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idequipo,nombre,matricula,noserie from tblclientesequipos where idcliente=" + pIdcliente.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesequipos")
        Return DS.Tables("tblclientesequipos").DefaultView
    End Function
    Public Function ConsultaSucReporte(ByVal pIdSucursal As Integer, ByVal pTextoBusqueda As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idequipo,nombre,matricula,noserie," + _
        "ifnull((select concat('EN USO EN PEDIDO: ',tblfertilizantespedidos.serie,convert(tblfertilizantespedidos.folio using utf8),' C: ',tblclientes.nombre) from tblfertilizantespedidos inner join tblfertilizantesequipos on tblfertilizantespedidos.idpedido=tblfertilizantesequipos.idpedido inner join tblclientes on tblclientes.idcliente=tblfertilizantespedidos.idcliente where tblfertilizantesequipos.idequipo=tblsucequipos.idequipo and tblfertilizantespedidos.estadopedido=0 and tblfertilizantespedidos.estado<>4),'LIBRE') as estado" + _
            " from tblsucequipos" ' where concat(nombre,matricula) like '%" + Replace(pTextoBusqueda, "'", "''") + "%'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " where tblsucequipos.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " order by nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblequipossuc")
        DS.WriteXmlSchema("repeuipossuc.xml")
        Return DS.Tables("tblequipossuc").DefaultView
    End Function
    Public Function ConsultaCartas() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblclientesequipos.nombre as 'Nombre del carro',tblclientesequipos.fechaEnvio as 'Fecha de envio',tblclientes.nombre as 'Nombre del cliente',tblclientesequipos.idequipo as 'IDEquipo' from tblclientesequipos join tblclientes on tblclientesequipos.idcliente=tblclientes.idcliente where (tblclientesequipos.fechaEnvio <= curdate() AND tblclientesequipos.estado = 1) order by tblclientesequipos.fechaEnvio asc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcorreos")
        Return DS.Tables("tblcorreos").DefaultView
    End Function
    'Public Function HayCartas() As Integer
    '    Comm.CommandText = "select count(tblclientesequipos.idequipo) from tblclientesequipos join tblclientes on tblclientesequipos.idcliente=tblclientes.idcliente where tblclientesequipos.fechaEnvio <= curdate() AND tblclientesequipos.estado = 1"
    '    Return Comm.ExecuteScalar
    'End Function
End Class
