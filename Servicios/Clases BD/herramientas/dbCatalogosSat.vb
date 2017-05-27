Public Class dbCatalogosSat
    Public Con As MySql.Data.MySqlClient.MySqlConnection
    Dim Comm As MySql.Data.MySqlClient.MySqlCommand
    Public Estado As String
    Public Municipio As String
    Public Localidad As String
    Public Colonia As String

    Public cEstado As String
    Public cMunicipio As String
    Public cLocalidad As String
    Public cColonia As String
    Public TextoX As String
    Public txtSource As New AutoCompleteStringCollection()
    Public Function IniciarMySQL(ByVal ServidorAConectarse As String, ByVal DBUsuario As String, ByVal DBPassword As String, ByVal DBPuerto As String) As Integer
        Dim TodoBien As Integer = 1
        Try
            If DBPuerto <> "" Then
                Con = New MySql.Data.MySqlClient.MySqlConnection("Server=" + ServidorAConectarse + ";Database=db_catalogossat;Uid=" + DBUsuario + ";Pwd=" + DBPassword + ";Port=" + DBPuerto)
            Else
                Con = New MySql.Data.MySqlClient.MySqlConnection("Server=" + ServidorAConectarse + ";Database=db_catalogossat;Uid=" + DBUsuario + ";Pwd=" + DBPassword + ";Port=3306")
            End If
            Comm = New MySql.Data.MySqlClient.MySqlCommand
            Con.Open()
            Comm.Connection = Con
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, GlobalNombreApp)
            TodoBien = 0
        End Try
        Return TodoBien
    End Function
    Public Sub LlenaCombos(ByVal Tabla As String, ByRef ComboaLlenar As ComboBox, ByVal CampoN As String, ByVal AliasN As String, ByVal CampoID As String, ByRef ListaId As elemento, Optional ByVal ValorWhere As String = "", Optional ByVal PrimerValor As String = "", Optional ByVal OrderBy As String = "", Optional pComoSimple As Boolean = False)
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        ComboaLlenar.Items.Clear()
        ListaId.Limpiar()
        If PrimerValor <> "" Then
            ComboaLlenar.Items.Add(PrimerValor)
            ListaId.Agregar(-2)
        End If
        If OrderBy = "" Then
            OrderBy = CampoN
        End If
        If ValorWhere = "" Then
            Comm.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " order by " + OrderBy
        Else
            If ValorWhere.Contains("inner join") = False Then
                Comm.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " where " + ValorWhere + " order by " + OrderBy
            Else
                Comm.CommandText = "select " + CampoID + "," + CampoN + " as " + AliasN + " from " + Tabla + " " + ValorWhere + " order by " + OrderBy
            End If
        End If
        DR = Comm.ExecuteReader
        While DR.Read
            ComboaLlenar.Items.Add(DR.Item(AliasN))
            If pComoSimple = False Then
                ListaId.Agregar(DR.Item(CampoID))
            End If
        End While
        DR.Close()
        If ComboaLlenar.DropDownStyle = ComboBoxStyle.DropDown Then
            ComboaLlenar.Text = ""
        Else
            If ComboaLlenar.Items.Count > 0 Then
                ComboaLlenar.SelectedIndex = 0
            End If
        End If
    End Sub
    Public Function ConsultaFracciones(pDescripcion As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select clave,descripcion from tblfraccionarancelaria where concat(clave,descripcion) like '%" + Replace(pDescripcion, "'", "''") + "%' order by clave"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfc")
        Return DS.Tables("tblfc").DefaultView
    End Function
    Public Function ConsultaColonias(pDescripcion As String, pCP As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select clave,nombre from tblcolonias where concat(clave,nombre) like '%" + Replace(pDescripcion, "'", "''") + "%'  and codigopostal like '%" + pCP + "%' order by clave"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfc")
        Return DS.Tables("tblfc").DefaultView
    End Function
    Public Function ConsultaMunicipio(pDescripcion As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select clave,nombre from tblmunicipios where concat(clave,nombre) like '%" + Replace(pDescripcion, "'", "''") + "%' order by clave"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfc")
        Return DS.Tables("tblfc").DefaultView
    End Function
    Public Function ConsultaLocalidad(pDescripcion As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select clave,nombre from tbllocalidades where concat(clave,nombre) like '%" + Replace(pDescripcion, "'", "''") + "%' order by clave"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfc")
        Return DS.Tables("tblfc").DefaultView
    End Function
    Public Function ConsultaEstados(pDescripcion As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select clave,nombre from tblestados where concat(clave,nombre) like '%" + Replace(pDescripcion, "'", "''") + "%' order by clave"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfc")
        Return DS.Tables("tblfc").DefaultView
    End Function
    Public Function ConsultaPais(pDescripcion As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select clave,descripcion as nombre from tblpaises where concat(clave,descripcion) like '%" + Replace(pDescripcion, "'", "''") + "%' order by clave"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfc")
        Return DS.Tables("tblfc").DefaultView
    End Function

    Public Function ConsultaProductoServ(pDescripcion As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select clave,descripcion as nombre from tblclavesprodserv where concat(clave,descripcion) like '%" + Replace(pDescripcion, "'", "''") + "%' order by clave"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfc")
        Return DS.Tables("tblfc").DefaultView
    End Function

    Public Function ConsultaUnidadMedida(pDescripcion As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select clave,nombre from tblunidadesmedida where concat(clave,nombre) like '%" + Replace(pDescripcion, "'", "''") + "%' order by nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfc")
        Return DS.Tables("tblfc").DefaultView
    End Function
    Public Function DaProductoServicio(pClave As String) As String
        Comm.CommandText = "select ifnull((select descripcion from tblclavesprodserv where clave='" + pClave + "'),'')"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaUnidadMedida(pClave As String) As String
        Comm.CommandText = "select ifnull((select nombre from tblunidadesmedida where clave='" + pClave + "' limit 1),'')"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaUsoCFDI(pClave As String) As String
        Comm.CommandText = "select ifnull((select concat(clave,' ',descripcion) from tblusoscfdi where clave='" + pClave + "' limit 1),'')"
        Return Comm.ExecuteScalar
    End Function
    Public Sub DaDatosCP(pCP As String)
        Comm.CommandText = "select * from tblcodigospostales where codigopostal='" + pCP + "'"
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        DR = Comm.ExecuteReader
        If DR.Read() Then
            cEstado = DR("estado")
            cMunicipio = DR("municipio")
            cLocalidad = DR("localidad")
        End If
        DR.Close()
        Comm.CommandText = "select ifnull((select nombre from tblmunicipios where clave='" + cMunicipio + "' and estado='" + cEstado + "' limit 1),'')"
        Municipio = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select nombre from tbllocalidades where clave='" + cLocalidad + "' and estado='" + cEstado + "' limit 1),'')"
        Localidad = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select nombre from tblestados where clave='" + cEstado + "'),'')"
        Estado = Comm.ExecuteScalar
        txtSource.Clear()
        Comm.CommandText = "select nombre from tblcolonias where codigopostal='" + pCP + "'"
        DR = Comm.ExecuteReader
        While DR.Read
            txtSource.Add(DR("nombre"))
        End While
        DR.Close()
    End Sub
End Class
