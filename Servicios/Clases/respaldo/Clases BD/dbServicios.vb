Public Class dbServicios
    Public ID As Integer
    Public Detalles As String
    Public FechaEntrada As String
    Public FechaSalida As String
    Public HoraEntrada As String
    Public HoraSalida As String
    Public Estado As Byte
    Public IdCliente As Integer
    Public Precio As Double
    Public Folio As Integer
    Public TotalManoObra As Double
    Public TotalEventos As Double
    Public TotalServicio As Double
    Public Cerrado As Byte
    Public Serie As String
    Public idEquipo As Integer
    Public IdTecnico As Integer
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Detalles = ""
        Precio = 0
        FechaEntrada = Format(Date.Now, "yyyy/MM/dd")
        FechaSalida = Format(Date.Now, "yyyy/MM/dd")
        HoraEntrada = Format(Date.Now, "HH:mm")
        HoraSalida = Format(Date.Now, "HH:mm")
        Estado = 0
        IdCliente = 0
        Folio = 0
        idEquipo = 0
        Cerrado = 0
        IdTecnico = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Llenadatos()
    End Sub
    Public Sub Guardar(ByVal pDetalles As String, ByVal pFechaEntrada As String, ByVal pHoraEntrada As String, ByVal pFechaSalida As String, ByVal pHoraSalida As String, ByVal pEstado As Byte, ByVal pIdCliente As Integer, ByVal pPrecio As Double, ByVal pFolio As Integer, ByVal pIdEquipo As Integer, ByVal pidTecnico As Integer)
        Precio = pPrecio
        Detalles = pDetalles
        FechaEntrada = pFechaEntrada
        FechaSalida = pFechaSalida
        HoraEntrada = pHoraEntrada
        HoraSalida = pHoraSalida
        Estado = pEstado
        IdCliente = pIdCliente
        Folio = pFolio
        IdTecnico = pidTecnico
        Cerrado = 0
        Comm.CommandText = "insert into tblservicios(detalles,fechae,horae,fechas,horas,estado,idcliente,precio,folio,cerrado,idEquipo,serie,idtecnico) values('" + Replace(Detalles, "'", "''") + "','" + FechaEntrada + "','" + HoraEntrada + "','" + FechaSalida + "','" + HoraSalida + "'," + Estado.ToString + "," + IdCliente.ToString + "," + Precio.ToString + "," + Folio.ToString + ",0" + "," + pIdEquipo.ToString + ",''," + IdTecnico.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idservicio) from tblservicios"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub GuardarSucursal(ByVal pDetalles As String, ByVal pFechaEntrada As String, ByVal pHoraEntrada As String, ByVal pFechaSalida As String, ByVal pHoraSalida As String, ByVal pEstado As Byte, ByVal pIdCliente As Integer, ByVal pPrecio As Double, ByVal pFolio As Integer, ByVal pIdEquipo As Integer, ByVal pIdTecnico As Integer)
        Precio = pPrecio
        Detalles = pDetalles
        FechaEntrada = pFechaEntrada
        FechaSalida = pFechaSalida
        HoraEntrada = pHoraEntrada
        HoraSalida = pHoraSalida
        Estado = pEstado
        IdCliente = pIdCliente
        Folio = pFolio
        Cerrado = 0
        IdTecnico = pIdTecnico
        Comm.CommandText = "insert into tblserviciossuc(detalles,fechae,horae,fechas,horas,estado,idsucursal,precio,folio,cerrado,idEquipo,serie,idtecnico) values('" + Replace(Detalles, "'", "''") + "','" + FechaEntrada + "','" + HoraEntrada + "','" + FechaSalida + "','" + HoraSalida + "'," + Estado.ToString + "," + IdCliente.ToString + "," + Precio.ToString + "," + Folio.ToString + ",0" + "," + pIdEquipo.ToString + ",''," + IdTecnico.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idservicio) from tblservicios"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pDetalles As String, ByVal pFechaSalida As String, ByVal pHoraSalida As String, ByVal pEstado As Byte, ByVal pPrecio As Double, ByVal pCerrado As Byte, ByVal pSerie As String, ByVal pFolio As Integer, ByVal pIdTecnico As Integer)
        ID = pID
        Precio = pPrecio
        Detalles = pDetalles
        FechaSalida = pFechaSalida
        HoraSalida = pHoraSalida
        Estado = pEstado
        Cerrado = pCerrado
        Folio = pFolio
        Serie = pSerie
        IdTecnico = pIdTecnico
        Comm.CommandText = "update tblservicios set precio=" + Precio.ToString + ",detalles='" + Replace(Detalles, "'", "''") + "',fechas='" + FechaSalida + "',horas='" + HoraSalida + "',estado=" + Estado.ToString + ",cerrado=" + Cerrado.ToString + " ,Serie='" + pSerie + "', Folio=" + Folio.ToString() + ",idtecnico=" + pIdTecnico.ToString + " where idservicio=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarSuc(ByVal pID As Integer, ByVal pDetalles As String, ByVal pFechaSalida As String, ByVal pHoraSalida As String, ByVal pEstado As Byte, ByVal pPrecio As Double, ByVal pCerrado As Byte, ByVal pSerie As String, ByVal pFolio As Integer, ByVal pIdTecnico As Integer)
        ID = pID
        Precio = pPrecio
        Detalles = pDetalles
        FechaSalida = pFechaSalida
        HoraSalida = pHoraSalida
        Estado = pEstado
        Cerrado = pCerrado
        Folio = pFolio
        Serie = pSerie
        IdTecnico = pIdTecnico
        Comm.CommandText = "update tblserviciossuc set precio=" + Precio.ToString + ",detalles='" + Replace(Detalles, "'", "''") + "',fechas='" + FechaSalida + "',horas='" + HoraSalida + "',estado=" + Estado.ToString + ",cerrado=" + Cerrado.ToString + " ,Serie='" + pSerie + "', Folio=" + Folio.ToString() + ",idtecnico=" + IdTecnico.ToString + " where idservicio=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblservicios where idservicio=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal Fecha1 As String, ByVal Fecha2 As String, Optional ByVal FolioCliente As String = "", Optional ByVal pEstado As Integer = 200, Optional ByVal pCerrado As Byte = 200) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idservicio,tblservicios.folio,tblservicios.fechae,tblclientes.nombre,tblservicios.detalles,tblservicios.estado,tblservicios.cerrado from tblservicios inner join tblclientes on tblservicios.idcliente=tblclientes.idcliente where tblservicios.fechae>='" + Fecha1 + "' and tblservicios.fechae<='" + Fecha2 + "' "
        If FolioCliente <> "" Then
            Comm.CommandText += "and concat(convert(tblservicios.folio using utf8),tblclientes.nombre) like '%" + Replace(FolioCliente, "'", "''") + "%' "
        End If
        If pEstado <> 200 Then
            Comm.CommandText += "and tblservicios.estado = " + pEstado.ToString
        End If
        If pCerrado <> 200 Then
            Comm.CommandText += "and tblservicios.cerrado = " + pCerrado.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicios")
        Return DS.Tables("tblservicios").DefaultView
    End Function
    Public Function DaNuevoFolio() As Integer
        Comm.CommandText = "select if(max(folio) is null,0,max(folio)) from tblservicios"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function DaNuevoFolioSucursal() As Integer
        Comm.CommandText = "select if(max(folio) is null,0,max(folio)) from tblserviciossuc"
        DaNuevoFolioSucursal = Comm.ExecuteScalar + 1
    End Function
    Public Sub CalculaTotales(ByVal pIDServicio As Integer)
        Comm.CommandText = "select ifnull(sum(precio),0) from tblservicioseventos where idservicio=" + pIDServicio.ToString
        TotalManoObra = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull(sum(tblserviciosinventario.precio),0) from tblserviciosinventario inner join tblservicioseventos on tblserviciosinventario.idevento=tblservicioseventos.idevento where tblservicioseventos.idservicio=" + pIDServicio.ToString
        TotalEventos = Comm.ExecuteScalar
        TotalServicio = TotalManoObra + TotalEventos
    End Sub
    Private Sub Llenadatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblservicios where idservicio=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Detalles = DReader("detalles")
            Precio = DReader("precio")
            FechaEntrada = DReader("fechae")
            FechaSalida = DReader("fechas")
            HoraEntrada = DReader("horae")
            HoraSalida = DReader("horas")
            Estado = DReader("estado")
            IdCliente = DReader("idcliente")
            Folio = DReader("folio")
            Cerrado = DReader("cerrado")
            If IsDBNull(DReader("Serie")) Then
                Serie = ""
            Else
                Serie = DReader("Serie")
            End If
            IdTecnico = DReader("idtecnico")
        End If
        DReader.Close()
    End Sub
    Public Sub LlenadatosSucursales(ByVal pid As Integer)
        ID = pid
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblserviciossuc where idservicio=" + pid.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Detalles = DReader("detalles")
            Precio = DReader("precio")
            FechaEntrada = DReader("fechae")
            FechaSalida = DReader("fechas")
            HoraEntrada = DReader("horae")
            HoraSalida = DReader("horas")
            Estado = DReader("estado")
            IdCliente = DReader("idsucursal")
            Folio = DReader("folio")
            Cerrado = DReader("cerrado")
            If IsDBNull(DReader("Serie")) Then
                Serie = ""
            Else
                Serie = DReader("Serie")
            End If
            idEquipo = DReader("idEquipo")
            IdTecnico = DReader("idtecnico")
        End If
        DReader.Close()
    End Sub
    Public Function BuscaServicio(ByVal pFolio As String) As Boolean
        Dim Encontro As Integer
        Comm.CommandText = " select if((select idservicio from tblservicios where convert(tblservicios.folio using utf8)='" + Replace(pFolio, "'", "''") + "') is null,0,(select idservicio from tblservicios where  convert(tblservicios.folio using utf8)='" + Replace(pFolio, "'", "''") + "'))"
        Encontro = Comm.ExecuteScalar
        If Encontro = 0 Then
            BuscaServicio = False
        Else
            ID = Encontro
            BuscaServicio = True
            Llenadatos()
        End If
    End Function
    '-------------------------------------------------------------------------
    Public Function ConsultaTodo(ByVal Fecha1 As String, ByVal Fecha2 As String) As DataView
        Dim DS As New DataSet
        'Comm.CommandText = "select idservicio,folio,fechae,detalles,idcliente,estado,cerrado from tblservicios where tblservicios.fechae>='" + Fecha1 + "' and tblservicios.fechae<='" + Fecha2 + "' "
        Comm.CommandText = "select idservicio,tblservicios.folio,tblservicios.fechae,tblclientes.nombre,tblservicios.detalles,tblservicios.estado,tblservicios.cerrado,tblservicios.idEquipo from tblservicios inner join tblclientes on tblservicios.idcliente=tblclientes.idcliente where tblservicios.fechae>='" + Fecha1 + "' and tblservicios.fechae<='" + Fecha2 + "' "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicios")
        Return DS.Tables("tblservicios").DefaultView
    End Function
    Public Function ConsultaCliente(ByVal pIDcliente As Integer) As String
        Dim cliente As String
        Comm.CommandText = "select nombre from tblclientes where idcliente=" + pIDcliente.ToString
        cliente = Comm.ExecuteScalar
        Return cliente
    End Function
    Public Function ConsultaSucursal(ByVal pIDcliente As Integer) As String
        Dim cliente As String
        Comm.CommandText = "select nombre from tblsucursales where idsucursal=" + pIDcliente.ToString
        cliente = Comm.ExecuteScalar
        Return cliente
    End Function
    Public Function ConsultaEspecifica(ByVal Fecha1 As String, ByVal Fecha2 As String, Optional ByVal nombreFolio As String = "", Optional ByVal estado As Integer = 100, Optional ByVal cerrado As Integer = 100) As DataView
        Dim DS As New DataSet
        If nombreFolio = "" And estado = 100 And cerrado = 100 Then
            Comm.CommandText = "select idservicio,tblservicios.folio,tblservicios.fechae,tblclientes.nombre,tblservicios.detalles,tblservicios.estado,tblservicios.cerrado ,tblservicios.idEquipo from tblservicios inner join tblclientes on tblservicios.idcliente=tblclientes.idcliente where tblservicios.fechae>='" + Fecha1 + "' and tblservicios.fechae<='" + Fecha2 + "' "
        Else
            If nombreFolio <> "" And estado = 100 And cerrado = 100 Then
                'filtro solo nombre
                Comm.CommandText = "select idservicio,tblservicios.folio,tblservicios.fechae,tblclientes.nombre,tblservicios.detalles,tblservicios.estado,tblservicios.cerrado ,tblservicios.idEquipo from tblservicios inner join tblclientes on tblservicios.idcliente=tblclientes.idcliente where  tblservicios.fechae>='" + Fecha1 + "' and tblservicios.fechae<='" + Fecha2 + "' and concat(convert(tblservicios.folio using utf8),tblclientes.nombre) like '%" + Replace(nombreFolio.ToString, "'", "''") + "%' "
            Else
                If nombreFolio <> "" And estado <> 100 And cerrado = 100 Then
                    'filtro nombre y estado
                    Comm.CommandText = "select idservicio,tblservicios.folio,tblservicios.fechae,tblclientes.nombre,tblservicios.detalles,tblservicios.estado,tblservicios.cerrado ,tblservicios.idEquipo from tblservicios inner join tblclientes on tblservicios.idcliente=tblclientes.idcliente where  tblservicios.fechae>='" + Fecha1 + "' and tblservicios.fechae<='" + Fecha2 + "' and concat(convert(tblservicios.folio using utf8),tblclientes.nombre) like '%" + Replace(nombreFolio.ToString, "'", "''") + "%' and tblservicios.estado=" + estado.ToString
                Else
                    If nombreFolio <> "" And estado <> 100 And cerrado <> 100 Then
                        'filtro nombre y estado y cerrado
                        Comm.CommandText = "select idservicio,tblservicios.folio,tblservicios.fechae,tblclientes.nombre,tblservicios.detalles,tblservicios.estado,tblservicios.cerrado ,tblservicios.idEquipo from tblservicios inner join tblclientes on tblservicios.idcliente=tblclientes.idcliente where  tblservicios.fechae>='" + Fecha1 + "' and tblservicios.fechae<='" + Fecha2 + "' and concat(convert(tblservicios.folio using utf8),tblclientes.nombre) like '%" + Replace(nombreFolio.ToString, "'", "''") + "%' and tblservicios.estado=" + estado.ToString + " and tblservicios.cerrado=" + cerrado.ToString
                    Else
                        If nombreFolio <> "" And estado = 100 And cerrado <> 100 Then
                            'filtro nombre y cerrado
                            Comm.CommandText = "select idservicio,tblservicios.folio,tblservicios.fechae,tblclientes.nombre,tblservicios.detalles,tblservicios.estado,tblservicios.cerrado ,tblservicios.idEquipo from tblservicios inner join tblclientes on tblservicios.idcliente=tblclientes.idcliente where  tblservicios.fechae>='" + Fecha1 + "' and tblservicios.fechae<='" + Fecha2 + "' and concat(convert(tblservicios.folio using utf8),tblclientes.nombre) like '%" + Replace(nombreFolio.ToString, "'", "''") + "%' and tblservicios.cerrado=" + cerrado.ToString
                        Else
                            If nombreFolio = "" And estado <> 100 And cerrado = 100 Then
                                'filtro estado
                                Comm.CommandText = "select idservicio,tblservicios.folio,tblservicios.fechae,tblclientes.nombre,tblservicios.detalles,tblservicios.estado,tblservicios.cerrado ,tblservicios.idEquipo from tblservicios inner join tblclientes on tblservicios.idcliente=tblclientes.idcliente where  tblservicios.fechae>='" + Fecha1 + "' and tblservicios.fechae<='" + Fecha2 + "' and tblservicios.estado=" + estado.ToString
                            Else
                                If nombreFolio = "" And estado = 100 And cerrado <> 100 Then
                                    'filtro cerrado
                                    Comm.CommandText = "select idservicio,tblservicios.folio,tblservicios.fechae,tblclientes.nombre,tblservicios.detalles,tblservicios.estado,tblservicios.cerrado ,tblservicios.idEquipo from tblservicios inner join tblclientes on tblservicios.idcliente=tblclientes.idcliente where  tblservicios.fechae>='" + Fecha1 + "' and tblservicios.fechae<='" + Fecha2 + "' and tblservicios.cerrado=" + cerrado.ToString
                                Else
                                    If nombreFolio = "" And estado <> 100 And cerrado <> 100 Then
                                        'filtro cerrado y estado
                                        Comm.CommandText = "select idservicio,tblservicios.folio,tblservicios.fechae,tblclientes.nombre,tblservicios.detalles,tblservicios.estado,tblservicios.cerrado ,tblservicios.idEquipo from tblservicios inner join tblclientes on tblservicios.idcliente=tblclientes.idcliente where  tblservicios.fechae>='" + Fecha1 + "' and tblservicios.fechae<='" + Fecha2 + "' and tblservicios.estado=" + estado.ToString + " and tblservicios.cerrado=" + cerrado.ToString
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If



        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicios")
        Return DS.Tables("tblservicios").DefaultView
    End Function
    Public Function ConsultaEspecificaSucursales(ByVal Fecha1 As String, ByVal Fecha2 As String, Optional ByVal nombreFolio As String = "", Optional ByVal estado As Integer = 100, Optional ByVal cerrado As Integer = 100) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblserviciossuc.idservicio,tblserviciossuc.folio,tblserviciossuc.fechae,tblsucursales.nombre,tblserviciossuc.detalles,tblserviciossuc.estado,tblserviciossuc.cerrado ,tblserviciossuc.idEquipo from tblserviciossuc inner join tblsucursales on tblserviciossuc.idsucursal=tblsucursales.idsucursal  where tblserviciossuc.fechae>='" + Fecha1 + "' and tblserviciossuc.fechae<='" + Fecha2 + "' "
        If nombreFolio <> "" Then

            Comm.CommandText += " and concat(convert(tblservicios.folio using utf8),tblclientes.nombre) like '%" + Replace(nombreFolio.ToString, "'", "''") + "%'"
        End If
        If estado <> 100 Then
            Comm.CommandText += " and tblserviciossuc.estado=" + estado.ToString
        End If
        If cerrado <> 100 Then
            Comm.CommandText += " and tblservicios.cerrado=" + cerrado.ToString
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicios")
        Return DS.Tables("tblservicios").DefaultView
    End Function
    Public Function buscarEstado(ByVal pIsEstado As Integer) As String
        Dim Estado As String
        Comm.CommandText = "select estado from tblserviciosestados where idEstado=" + pIsEstado.ToString
        Estado = Comm.ExecuteScalar
        Return Estado
    End Function
    Public Function ConsultaIdEquipo(ByVal pIDcliente As Integer) As Integer
        Dim Equipo As Integer
        Comm.CommandText = "select idEquipo from tblservicios where idservicio=" + pIDcliente.ToString
        Equipo = Comm.ExecuteScalar
        Return Equipo
    End Function
  
    Public Function ConsultaIdEquiposuc(ByVal pIDcliente As Integer) As Integer
        Dim Equipo As Integer
        Comm.CommandText = "select idEquipo from tblserviciossuc where idservicio=" + pIDcliente.ToString
        Equipo = Comm.ExecuteScalar
        Return Equipo
    End Function

    Public Function buscarEquipos(ByVal pidCliente As Integer) As DataTable
        Dim DS As New DataSet
        ' Comm.CommandText = "select Numero,Banco from tblcuentas ORDER BY IdCuenta ASC"
        Comm.CommandText = "SELECT idEquipo, nombre FROM tblclientesequipos WHERE idCliente=" + pidCliente.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesequipos")
        Return DS.Tables("tblclientesequipos")
    End Function
    Public Function buscarEquiposSuc(ByVal pidCliente As Integer) As DataTable
        Dim DS As New DataSet
        ' Comm.CommandText = "select Numero,Banco from tblcuentas ORDER BY IdCuenta ASC"
        Comm.CommandText = "SELECT idEquipo, nombre FROM tblsucequipos WHERE idsucursal=" + pidCliente.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesequipos")
        Return DS.Tables("tblclientesequipos")
    End Function

    Public Function buscarServicios(ByVal pfecha As String, ByVal pfecha2 As String) As DataTable
        Dim DS As New DataSet
        ' Comm.CommandText = "select Numero,Banco from tblcuentas ORDER BY IdCuenta ASC"
        Comm.CommandText = "SELECT * FROM tblservicios WHERE DATE(fechae) between'" + pfecha.ToString + "' AND '" + pfecha2.ToString() + "'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicios")
        Return DS.Tables("tblservicios")
    End Function
    Public Function buscarServiciosSuc(ByVal pfecha As String, ByVal pfecha2 As String) As DataTable
        Dim DS As New DataSet
        ' Comm.CommandText = "select Numero,Banco from tblcuentas ORDER BY IdCuenta ASC"
        Comm.CommandText = "SELECT * FROM tblserviciossuc WHERE DATE(fechae) between'" + pfecha.ToString + "' AND '" + pfecha2.ToString() + "'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicios")
        Return DS.Tables("tblservicios")
    End Function
    Public Function ConsultanombreEquipo(ByVal pIDEquipo As Integer) As String
        Dim Equipo As String
        Comm.CommandText = "select nombre from tblclientesequipos where idEquipo=" + pIDEquipo.ToString
        Equipo = Comm.ExecuteScalar
        Return Equipo
    End Function
    Public Function ConsultanombreEquiposuc(ByVal pIDEquipo As Integer) As String
        Dim Equipo As String
        Comm.CommandText = "select nombre from tblsucequipos where idEquipo=" + pIDEquipo.ToString
        Equipo = Comm.ExecuteScalar
        Return Equipo
    End Function
    Public Function ConsultanombreESTADO(ByVal pIDEquipo As Integer) As String
        Dim Equipo As String = ""
        If pIDEquipo = "0" Then
            Equipo = "Ninguno"
        Else
            Comm.CommandText = "select estado from tblserviciosestados where idEstado=" + pIDEquipo.ToString
            Equipo = Comm.ExecuteScalar
        End If
       
        Return Equipo
    End Function

    Public Function buscarEquioposCliente(ByVal pidCliente As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "SELECT * FROM tblclientesequipos WHERE idcliente=" + pidCliente.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesequipos")
        Return DS.Tables("tblclientesequipos")
    End Function
    Public Function buscarEquioposClientesuc(ByVal pidCliente As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "SELECT * FROM tblsucequipos WHERE idsucursal=" + pidCliente.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesequipos")
        Return DS.Tables("tblclientesequipos")
    End Function


    '00000000
    Public Function nombreCliente(ByVal pidCliente As Integer) As String
        Dim nombre1 As String
        Comm.CommandText = "select nombre from tblclientes where idcliente=" + pidCliente.ToString()
        nombre1 = Comm.ExecuteScalar
        Return nombre1
    End Function
    Public Function nombreClientesuc(ByVal pidCliente As Integer) As String
        Dim nombre1 As String
        Comm.CommandText = "select nombre from tblsucursales where idsucursal=" + pidCliente.ToString()
        nombre1 = Comm.ExecuteScalar
        Return nombre1
    End Function

    Public Function ConsultaRFC(ByVal pidCliente) As String
        Dim rfc As String
        Comm.CommandText = "select rfc from tblclientes where idcliente=" + pidCliente.ToString()
        rfc = Comm.ExecuteScalar
        Return rfc
    End Function
    Public Function ConsultaRFCsuc(ByVal pidCliente) As String
        Dim rfc As String
        Comm.CommandText = "select rfc from tblsucursales where idsucursal=" + pidCliente.ToString()
        rfc = Comm.ExecuteScalar
        Return rfc
    End Function

    Public Function Consultadireccion(ByVal pidCliente As Integer) As String
        Dim calle As String
        Dim num As String
        Dim colonia As String
        Dim ciudad As String
        'Dim municipio As String
        Dim estado As String
        Dim pais As String
        Dim total As String

        Comm.CommandText = "select direccion from tblclientes where idcliente=" + pidCliente.ToString()
        calle = Comm.ExecuteScalar
        Comm.CommandText = "select noexterior from tblclientes where idcliente=" + pidCliente.ToString()
        num = Comm.ExecuteScalar
        Comm.CommandText = "select colonia from tblclientes where idcliente=" + pidCliente.ToString()
        colonia = Comm.ExecuteScalar
        Comm.CommandText = "select ciudad from tblclientes where idcliente=" + pidCliente.ToString()
        ciudad = Comm.ExecuteScalar
        Comm.CommandText = "select estado from tblclientes where idcliente=" + pidCliente.ToString()
        estado = Comm.ExecuteScalar
        Comm.CommandText = "select pais from tblclientes where idcliente=" + pidCliente.ToString()
        pais = Comm.ExecuteScalar
        total = calle + " " + num

        Return total
    End Function
    Public Function Consultadireccionsuc(ByVal pidCliente As Integer) As String
        Dim calle As String
        Dim num As String
        Dim colonia As String
        Dim ciudad As String
        'Dim municipio As String
        Dim estado As String
        Dim pais As String
        Dim total As String

        Comm.CommandText = "select direccion from tblsucursales where idsucursal=" + pidCliente.ToString()
        calle = Comm.ExecuteScalar
        Comm.CommandText = "select noexterior from tblsucursales where idsucursal=" + pidCliente.ToString()
        num = Comm.ExecuteScalar
        Comm.CommandText = "select colonia from tblsucursales where idsucursal=" + pidCliente.ToString()
        colonia = Comm.ExecuteScalar
        Comm.CommandText = "select ciudad from tblsucursales where idsucursal=" + pidCliente.ToString()
        ciudad = Comm.ExecuteScalar
        Comm.CommandText = "select estado from tblsucursales where idsucursal=" + pidCliente.ToString()
        estado = Comm.ExecuteScalar
        Comm.CommandText = "select pais from tblsucursales where idsucursal=" + pidCliente.ToString()
        pais = Comm.ExecuteScalar
        total = calle + " " + num

        Return total
    End Function

    Public Function direccion2(ByVal pidcliente As Integer) As String

        Dim ciudad As String
        Dim estado As String
        Dim pais As String
        Dim total As String

        Comm.CommandText = "select ciudad from tblclientes where idcliente=" + pidcliente.ToString()
        ciudad = Comm.ExecuteScalar
        Comm.CommandText = "select estado from tblclientes where idcliente=" + pidcliente.ToString()
        estado = Comm.ExecuteScalar
        Comm.CommandText = "select pais from tblclientes where idcliente=" + pidcliente.ToString()
        pais = Comm.ExecuteScalar
        total = ciudad + ", " + estado + ", " + pais
        Return total
    End Function
    Public Function direccion2suc(ByVal pidcliente As Integer) As String

        Dim ciudad As String
        Dim estado As String
        Dim pais As String
        Dim total As String

        Comm.CommandText = "select ciudad from tblsucursales where idsucursal=" + pidcliente.ToString()
        ciudad = Comm.ExecuteScalar
        Comm.CommandText = "select estado from tblsucursales where idsucursal=" + pidcliente.ToString()
        estado = Comm.ExecuteScalar
        Comm.CommandText = "select pais from tblsucursales where idsucursal=" + pidcliente.ToString()
        pais = Comm.ExecuteScalar
        total = ciudad + ", " + estado + ", " + pais
        Return total
    End Function
    Public Function Existe(ByVal pNombre As String) As Integer
        Dim Resultado As Integer
        Comm.CommandText = "select * from tblclientes where nombre='" + pNombre.ToString() + "'"
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function
    Public Function Existesuc(ByVal pNombre As String) As Integer
        Dim Resultado As Integer
        Comm.CommandText = "select * from tblsucursales where nombre='" + pNombre.ToString() + "'"
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function
    Public Function obtID(ByVal pNombre As String) As Integer
        Dim Resultado As Integer
        Comm.CommandText = "select idcliente from tblclientes where nombre='" + pNombre.ToString() + "'"
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function
    Public Function obtIDsuc(ByVal pNombre As String) As Integer
        Dim Resultado As Integer
        Comm.CommandText = "select idsucursal from tblsucursales where nombre='" + pNombre.ToString() + "'"
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function

    Public Function buscarEquioposClienteTodos() As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "SELECT * FROM tblclientesequipos  order by idcliente asc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesequipos")
        Return DS.Tables("tblclientesequipos")
    End Function
    Public Function buscarEquioposSucTodos() As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "SELECT * FROM tblsucequipos  order by idsucursal asc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesequipos")
        Return DS.Tables("tblclientesequipos")
    End Function

    Public Function buscarTecnicos() As DataTable
        Dim DS As New DataSet
        ' Comm.CommandText = "select Numero,Banco from tblcuentas ORDER BY IdCuenta ASC"
        Comm.CommandText = "SELECT idtecnico,nombre FROM tbltecnicos "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbltecnicos")
        Return DS.Tables("tbltecnicos")
    End Function

    Public Function buscarClasificaciones() As DataTable
        Dim DS As New DataSet
        ' Comm.CommandText = "select Numero,Banco from tblcuentas ORDER BY IdCuenta ASC"
        Comm.CommandText = "SELECT idclasificacion,nombre FROM tblserviciosclasificaciones "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosclasificaciones")
        Return DS.Tables("tblserviciosclasificaciones")
    End Function

    Public Function buscarSubClasificaciones() As DataTable
        Dim DS As New DataSet
        ' Comm.CommandText = "select Numero,Banco from tblcuentas ORDER BY IdCuenta ASC"
        Comm.CommandText = "SELECT idclasificacion2,nombre FROM tblserviciosclasificaciones2 "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosclasificaciones2")
        Return DS.Tables("tblserviciosclasificaciones2")
    End Function
End Class
