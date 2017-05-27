Public Class dbAltaEmpleados
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public IdEmpleado As Integer
    Public nombre As String
    Public telefono As String
    Public eMail As String
    Public RFC As String
    Public curp As String
    Public salario As Double
    Public calle As String
    Public nExterior As String
    Public nInterior As String
    Public colonia As String
    Public municipio As String
    Public ciudad As String
    Public referencia As String
    Public estado As String
    Public CP As Integer
    Public pais As String

    '*/*****variables Sueldo ****
    Public idSueldo As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        IdEmpleado = -1
        Inicia()
        Comm.Connection = Conexion
    End Sub
    Public Sub Inicia()
        IdEmpleado = 0
        nombre = ""
        telefono = ""
        eMail = ""
        RFC = ""
        curp = ""
        salario = 0
        calle = ""
        nExterior = ""
        nInterior = ""
        colonia = ""
        municipio = ""
        ciudad = ""
        referencia = ""
        estado = ""
        CP = 0
        idSueldo = 0
        pais = ""
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        IdEmpleado = pID
        Comm.Connection = Conexion
        LlenaDatos(IdEmpleado)
    End Sub
    Public Sub LlenaDatos(ByVal pidEmpleado As Integer)
        IdEmpleado = pidEmpleado
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblgastosempleados where idEmpleado=" + IdEmpleado.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            nombre = DReader("nombre")
            telefono = DReader("telefono")
            eMail = DReader("email")
            RFC = DReader("RFC")
            curp = DReader("CURP")
            salario = DReader("salario")
            calle = DReader("calle")
            nExterior = DReader("noExterior")
            nInterior = DReader("noInterior")
            colonia = DReader("colonia")
            municipio = DReader("municipio")
            ciudad = DReader("ciudad")
            referencia = DReader("referencia")
            estado = DReader("estado")
            CP = DReader("CP")
            pais = DReader("pais")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pnombre As String, ByVal ptelefono As String, ByVal peMail As String, ByVal pRFC As String, ByVal pcurp As String, ByVal psalario As Double, ByVal pcalle As String, ByVal pnExterior As String, ByVal pnInterior As String, ByVal pcolonia As String, ByVal pmunicipio As String, ByVal pciudad As String, ByVal preferencia As String, ByVal pestado As String, ByVal pCP As Integer, ByVal ppais As String)
        nombre = pnombre
        telefono = ptelefono
        eMail = peMail
        RFC = pRFC
        curp = pcurp
        salario = psalario
        calle = pcalle
        nExterior = pnExterior
        nInterior = pnInterior
        colonia = pcolonia
        municipio = pmunicipio
        ciudad = pciudad
        referencia = preferencia
        estado = pestado
        CP = pCP
        pais = ppais

        Comm.CommandText = "insert into tblgastosempleados( nombre, telefono , email, RFC , CURP ,salario ,calle ,noExterior ,noInterior, colonia, municipio, ciudad, referencia, estado, CP, pais,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(nombre, "'", "''") + "', '" + Replace(telefono, "'", "''") + "' , '" + Replace(eMail, "'", "''") + "',  '" + Replace(RFC, "'", "''") + "' , '" + Replace(curp, "'", "''") + "' ," + salario.ToString + " ,'" + Replace(calle, "'", "''") + "' ,'" + Replace(nExterior, "'", "''") + "' ,'" + Replace(nInterior, "'", "''") + "', '" + Replace(colonia, "'", "''") + "', '" + Replace(municipio, "'", "''") + "', '" + Replace(ciudad, "'", "''") + "', '" + Replace(referencia, "'", "''") + "', '" + Replace(estado, "'", "''") + "', " + CP.ToString + ", '" + Replace(pais, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idEmpleado) from tblgastosempleados"
        IdEmpleado = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pnombre As String, ByVal ptelefono As String, ByVal peMail As String, ByVal pRFC As String, ByVal pcurp As String, ByVal psalario As Double, ByVal pcalle As String, ByVal pnExterior As String, ByVal pnInterior As String, ByVal pcolonia As String, ByVal pmunicipio As String, ByVal pciudad As String, ByVal preferencia As String, ByVal pestado As String, ByVal pCP As Integer, ByVal ppais As String)

        IdEmpleado = pID
        nombre = pnombre
        telefono = ptelefono
        eMail = peMail
        RFC = pRFC
        curp = pcurp
        salario = psalario
        calle = pcalle
        nExterior = pnExterior
        nInterior = pnInterior
        colonia = pcolonia
        municipio = pmunicipio
        ciudad = pciudad
        referencia = preferencia
        estado = pestado
        CP = pCP
        pais = ppais

        Comm.CommandText = "update tblgastosempleados set nombre='" + Replace(nombre, "'", "''") + "', telefono='" + Replace(telefono, "'", "''") + "' , email='" + Replace(eMail, "'", "''") + "',  RFC='" + Replace(RFC, "'", "''") + "' , CURP='" + Replace(curp, "'", "''") + "' ,salario=" + salario.ToString + " ,calle='" + Replace(calle, "'", "''") + "' ,noExterior='" + Replace(nExterior, "'", "''") + "' ,noInterior='" + Replace(nInterior, "'", "''") + "', colonia='" + Replace(colonia, "'", "''") + "', municipio='" + Replace(municipio, "'", "''") + "', ciudad='" + Replace(ciudad, "'", "''") + "', referencia='" + Replace(referencia, "'", "''") + "', estado='" + Replace(estado, "'", "''") + "', CP=" + CP.ToString + ", pais='" + Replace(pais, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCAmbio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idEmpleado=" + IdEmpleado.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblgastosempleados where idEmpleado=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idEmpleado,nombre,RFC,telefono, salario from tblgastosempleados where concat(RFC,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblEmpleados")
        Return DS.Tables("tblEmpleados")
    End Function
    '**************************************sueldos*****************************************
    Public Function consultaPago() As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select 1 as x,idEmpleado,nombre,salario,1 as dias,salario as salario2 from tblgastosempleados "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblEmpleados")
        Return DS.Tables("tblEmpleados")
    End Function
    Public Sub guardarSueldo(ByVal pIdGasto As Integer, ByVal pIdEmpleado As Integer, ByVal pSueldo As Double, ByVal pDias As Double, ByVal pTotal As Double)
        Comm.CommandText = "insert into tblsueldo(idGasto, idEmpleado ,sueldo,dias , total ) values(" + pIdGasto.ToString + ", " + pIdEmpleado.ToString + " , " + pSueldo.ToString + ",  " + pDias.ToString + " , " + pTotal.ToString + " )"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idEmpleado) from tblsueldo"
        idSueldo = Comm.ExecuteScalar
    End Sub
    Public Sub modificarSueldo(ByVal pIdSueldo As Integer, ByVal pIdGasto As Integer, ByVal pIdEmpleado As Integer, ByVal pSueldo As Double, ByVal pDias As Double, ByVal pTotal As Double)
        Comm.CommandText = "update tblsueldo set idGasto=" + pIdGasto.ToString + ", idEmpleado=" + pIdEmpleado.ToString + " ,sueldo=" + pSueldo.ToString + ",dias=" + pDias.ToString + " , total=" + pTotal.ToString + " where idSueldo=" + pIdSueldo.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarSueldo(ByVal pIDGasto As Integer)
        Comm.CommandText = "delete from tblsueldo where idGasto=" + pIDGasto.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function consultaSueldo(ByVal pIDGasto As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblsueldo where idGasto=" + pIDGasto.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblSueldos")
        Return DS.Tables("tblSueldos")
    End Function
    Public Function buscarSueldos(ByVal pidGasto As Integer) As Integer
        Dim contador As Integer = 0
        Comm.CommandText = "select count(idGasto) from tblsueldo where idGasto=" + pidGasto.ToString
        contador = Comm.ExecuteScalar()
        Return contador
    End Function

    Public Sub GuardarSalario(ByVal pidMovimiento As Integer, ByVal pDescripcion As String, ByVal pPrecio As Double, ByVal pClasificacion As Integer, pidclasificacion2 As Integer, pidClasificacion3 As Integer)
        Comm.CommandText = "insert into tblgastosdetalles(idMovimiento,descripcion, precio,clasificacion,esSalario,idclasificacion2,idclasificacion3) values(" + pidMovimiento.ToString + ",'" + pDescripcion.ToString + "'," + pPrecio.ToString + "," + pClasificacion.ToString() + ",1," + pidclasificacion2.ToString + "," + pidClasificacion3.ToString + ");"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarSalario(ByVal pidMovimiento As Integer)

        Comm.CommandText = "delete from tblgastosdetalles where idMovimiento=" + pidMovimiento.ToString + " and esSalario=1"
        Comm.ExecuteNonQuery()

    End Sub
End Class
