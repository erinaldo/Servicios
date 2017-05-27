Imports MySql.Data.MySqlClient

Public Class dbCuentas
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    '*****Variables******
    Public Folio As Integer
    Public Numero As String
    Public Banco As Integer
    Public Tipo As String
    Public Idcuenta As Integer
    Public EsExtranjero As Byte
    Public NombreEx As String
    Public BancoNombre As String
    Public BancoClave As String
    Public IdCuenta2 As Integer
    Public IdCuenta3 As Integer
    Public IdCuenta4 As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Folio = 0
        Numero = ""
        Banco = 0
        Tipo = ""
        Idcuenta = 0
        IdCuenta2 = 0
        IdCuenta3 = 0
        IdCuenta4 = 0
        EsExtranjero = 0
        NombreEx = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Folio = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcuentas where idCuenta=" + Folio.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then

            Numero = DReader("Numero")
            Banco = DReader("Banco")
            Tipo = DReader("Tipo")
            Idcuenta = DReader("idccontable")
            EsExtranjero = DReader("esextranjero")
            NombreEx = DReader("nombreex")
            IdCuenta2 = DReader("idcuenta2")
            IdCuenta3 = DReader("idcuenta3")
            IdCuenta4 = DReader("idcuenta4")
            ' IdSucursal = DReader("idsucursal")
        End If
        DReader.Close()
        Comm.CommandText = "select * from tblbancoscatalogo where idbanco=" + Banco.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read Then
            BancoNombre = DReader("nombre")
            BancoClave = DReader("clave")
            BancoClave = BancoClave.PadLeft(3, "0")
        End If
        DReader.Close()
    End Sub
    Public Sub Modificar(ByVal pFolio As Integer, ByVal pNumero As String, ByVal pBanco As Integer, ByVal pTipo As String, pIdCuenta As Integer, pEsExt As Byte, pNombreEx As String, pIdCuenta2 As Integer, pIdCuenta3 As Integer, pIdCuenta4 As Integer)
        Folio = pFolio
        Numero = pNumero
        Banco = pBanco
        Tipo = pTipo
        Idcuenta = pIdCuenta
        NombreEx = pNombreEx
        EsExtranjero = pEsExt
        Comm.CommandText = "update tblcuentas set Numero='" + Replace(Numero, "'", "''") + "',Banco=" + Banco.ToString + ",Tipo='" + Replace(Tipo, "'", "''") + "',idccontable=" + Idcuenta.ToString + ",esextranjero=" + EsExtranjero.ToString + ",nombreex='" + NombreEx.Replace("'", "''") +
            "',idcuenta2=" + pIdCuenta2.ToString + ",idcuenta3=" + pIdCuenta3.ToString + ",idcuenta4=" + pIdCuenta4.ToString +
            ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idCuenta=" + Folio.ToString()
        Comm.ExecuteNonQuery()
    End Sub
    Public Function buscarBancos() As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idBanco,Nombre from tblbancos ORDER BY idBanco ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        Return DS.Tables("tblbancos")
    End Function
    Public Function reporte(pNumero As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idcuenta,numero,if(esextranjero=0,tblbancoscatalogo.nombre,nombreex) nombre,tipo from tblcuentas inner join tblbancoscatalogo on tblcuentas.banco=tblbancoscatalogo.idbanco where numero like '%" + Replace(pNumero, "'", "''") + "%';"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcuentas")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblcuentas").DefaultView
    End Function
    Public Function nombreBanco(ByVal id As Integer) As String
        Dim banco As String
        Comm.CommandText = "select nombre from tblBancoscatalago where idBanco=" + id.ToString
        banco = Comm.ExecuteScalar
        Return banco
    End Function
    'Public Function Consultar(Optional ByVal pNombre As String = "") As DataView
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select idCuenta,Numero,Banco,Tipo from tblcuentas where Numero like '%" + Replace(pNombre, "'", "''") + "%' and idCuenta<>0"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblcuentas")
    '    Return DS.Tables("tblcuentas").DefaultView
    'End Function
    Public Function buscarFolio() As Integer

        Dim Resultado As Integer = 0
        Comm.CommandText = "select ifnull((SELECT MAX(idCuenta) AS idCuenta FROM tblcuentas),0)"
        Resultado = Comm.ExecuteScalar
        Resultado = Resultado + 1
        Return Resultado
    End Function
    Public Function ChecaCuentaRepetida(ByVal pNumero As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(Numero) from tblcuentas where Numero='" + Replace(pNumero, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaCuentaRepetida = False
        Else
            ChecaCuentaRepetida = True
        End If
    End Function

    Public Sub Guardar(ByVal pNumero As String, ByVal pBanco As String, ByVal pTipo As String, pIDCuenta As Integer, pEsext As Byte, pNombreEx As String, PIdCuenta2 As Integer, pIdCuenta3 As Integer, pIdCuenta4 As Integer)
        Numero = pNumero
        Banco = pBanco
        Tipo = pTipo
        Idcuenta = pIDCuenta
        EsExtranjero = pEsext
        NombreEx = pNombreEx
        Comm.CommandText = "insert into tblcuentas(Numero, Banco, Tipo,idccontable,esextranjero,nombreex,idcuenta2,idcuenta3,idcuenta4,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Numero, "'", "''") + "','" + Replace(Banco, "'", "''") + "','" + Replace(Tipo, "'", "''") + "'," + Idcuenta.ToString + "," + EsExtranjero.ToString + ",'" + NombreEx.Replace("'", "''") + "'," + PIdCuenta2.ToString + "," + pIdCuenta3.ToString + "," + pIdCuenta4.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        'Comm.CommandText = "insert into tblalmacenes(nombre,direccion,numero,idsucursal) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Direccion, "'", "''") + "','" + Replace(Numero, "'", "''") + "'," + IdSucursal.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcuentas where idCuenta=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Reporte1() As DataView
        Dim DS As New DataSet

        Comm.CommandText = "SELECT p1.idCuenta,p1.Numero,p2.Nombre,p1.Tipo FROM tblCuentas AS p1 inner join tblBancoscatalogo AS p2 on p1.Banco = p2.idBanco order by p2.nombre;"
        '"select * from tblcuentas order by idCuenta"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcuentas")
        'DS.WriteXmlSchema("tblcuentas.xml")
        Return DS.Tables("tblcuentas").DefaultView
    End Function
End Class
