Imports MySql.Data.MySqlClient
Public Class dbGastosProgramables
    Public tipo As Integer
    Public nombre As String
    Public fecha As String
    Public dias As Integer
    Public frecuencia As String
    Public horas As Integer
    Public minutos As Integer
    Public inicio As String
    Public fin As String
    Public ID As Integer
    Public estado As Boolean

    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        tipo = 0
        nombre = ""
        fecha = ""
        dias = 0
        frecuencia = 0
        inicio = ""
        fin = ""
        estado = True
        Comm.Connection = Conexion

    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos(ID)
    End Sub
    Public Sub LlenaDatos(ByVal pID As Integer)
        ID = pID
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblgastosprogramables where idGasto=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            tipo = DReader("tipo")
            nombre = DReader("nombre")
            fecha = DReader("fecha")
            dias = DReader("dias")
            frecuencia = DReader("frecuencia")
            inicio = DReader("inicio")
            fin = DReader("fin")
            estado = DReader("estado")
        End If
        DReader.Close()
        convertirHora(frecuencia)
    End Sub
    Public Sub Guardar(ByVal pTipo As Integer, ByVal pNombre As String, ByVal pFecha As String, ByVal pDias As Integer, ByVal pFrecuencia As String, ByVal pInicio As String, ByVal pFin As String, ByVal pEstado As Boolean)
        tipo = pTipo
        nombre = pNombre
        fecha = pFecha
        dias = pDias
        frecuencia = pFrecuencia
        inicio = pInicio
        fin = pFin
        estado = pEstado
        Comm.CommandText = "insert into tblgastosprogramables(tipo,nombre,fecha,dias,frecuencia,inicio,fin,estado) values(" + tipo.ToString + ",'" + nombre + "','" + fecha + "'," + dias.ToString + ",'" + frecuencia.ToString + "','" + inicio + "','" + fin + "'," + estado.ToString + ");"
        'Comm.ExecuteNonQuery()
        Comm.CommandText += "select max(idGasto) from tblgastosprogramables;"
        ID = Comm.ExecuteScalar
    End Sub

    Public Sub Modificar(ByVal pID As Integer, ByVal pTipo As Integer, ByVal pNombre As String, ByVal pFecha As String, ByVal pDias As Integer, ByVal pFrecuencia As String, ByVal pInicio As String, ByVal pFin As String, ByVal pEstado As Boolean)
        ID = pID
        tipo = pTipo
        nombre = pNombre
        fecha = pFecha
        dias = pDias
        frecuencia = pFrecuencia
        inicio = pInicio
        fin = pFin
        estado = pEstado
        Comm.CommandText = "update tblgastosprogramables set tipo =" + tipo.ToString + ",nombre='" + nombre + "',fecha='" + fecha + "',dias = " + dias.ToString + ",frecuencia ='" + frecuencia.ToString + "',inicio='" + inicio + "',fin='" + fin + "',estado=" + estado.ToString + " where idGasto=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblgastosprogramables where idGasto=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function busqueda(ByVal pNombre As String, ByVal pTipo As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idGasto,nombre,IF(tipo=0,'Pago constante','Pago único') from tblgastosprogramables where nombre like '%" + pNombre + "%'"
        If pTipo < 2 Then
            Comm.CommandText += "and tipo=" + pTipo.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblgastosprogramables")
        Return DS.Tables("tblgastosprogramables")
    End Function
    Public Sub convertirHora(ByVal horaCom As String)
        Dim aux As String = ""
        For i As Integer = 0 To horaCom.Length - 1
            If horaCom.Chars(i) = ":" Then
                horas = Integer.Parse(aux)
                aux = ""
            Else
                aux = aux + horaCom.Chars(i)
            End If

        Next

        minutos = Integer.Parse(aux)
    End Sub

    'Notificaciones
    Public Function buscarNotificacionesC() As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblgastosprogramables where estado=1 and tipo=0 "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblgastosprogramables")
        Return DS.Tables("tblgastosprogramables")
    End Function
    Public Function buscarNotificacionesU(ByVal pFecha As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblgastosprogramables where estado=1 and tipo=1 and fecha>='" + pFecha.ToString + "' "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblgastosprogramables")
        Return DS.Tables("tblgastosprogramables")
    End Function

    'detalles
    Public Function esPrimerRegistro()
        Comm.CommandText = "select count(ID) from tblgastosprogradetalles where  fecha='" + Date.Now().ToString("dd/MM/yyyy") + "' "
        Return Comm.ExecuteScalar()
    End Function
    Public Sub LimpiarDetalles()
        Comm.CommandText = "delete from tblgastosprogradetalles "
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Guardardetalles(ByVal pidGasto As Integer, ByVal pFecha As String, ByVal pHora As String, ByVal pFrecuencia As String)

        Comm.CommandText = "insert into tblgastosprogradetalles(idGasto,fecha,hora,frecuencia) values(" + pidGasto.ToString + ",'" + pFecha + "','" + pHora + "','" + pFrecuencia.ToString + "');"
        Comm.ExecuteScalar()
    End Sub

    Public Sub EliminarDetallesTemporal(ByVal pidGasto As Integer)
        Comm.CommandText = "delete from tblgastosprogradetalles where idGasto=" + pidGasto.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function bucarTodos() As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblgastosprogradetalles "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblgastosprogramables")
        Return DS.Tables("tblgastosprogramables")
    End Function

    Public Function buscarMensaje(ByVal pidGasto As Integer) As String
        Comm.CommandText = "select nombre from tblgastosprogramables where idGasto=" + pidGasto.ToString
        Return Comm.ExecuteScalar
    End Function
End Class
