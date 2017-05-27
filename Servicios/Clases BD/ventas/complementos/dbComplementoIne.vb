Imports MySql.Data.MySqlClient
Public Class dbComplementoIne
    Private comm As New MySqlCommand
    Public id As Integer
    Public comite As String
    Public proceso As String
    Public idContabilidad As Integer
    Public idFactura As Integer
    Public entidad As String
    Public ambito As String
    Public version As String
    Public idContabilidadP As String
    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        id = -1
        comite = ""
        proceso = ""
        idContabilidad = -1
        idFactura = -1
        entidad = ""
        ambito = ""
        idContabilidadP = ""
    End Sub

    Public Sub New(ByVal idFactura As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.idFactura = idFactura
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblcomplementoine where idFactura=" + idFactura.ToString
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            id = dr("idComplemento")
            comite = dr("comite")
            proceso = dr("proceso")
            idFactura = dr("idfactura")
            idContabilidadP = dr("idcontabilidad")
        End While
        dr.Close()
    End Sub

    Public Function guardar(ByVal comite As String, ByVal proceso As String, ByVal idFactura As Integer, pidContabilidad As String) As Boolean
        comm.CommandText = "insert into tblcomplementoine(comite,proceso,idfactura,idcontabilidad) values('" + comite + "','" + proceso + "'," + idFactura.ToString + ",'" + pidcontabilidad + "');"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function actualizar(ByVal idComplemento As Integer, ByVal comite As String, ByVal proceso As String, ByVal idFactura As Integer, pIdContabilidad As String) As Boolean
        comm.CommandText = "update tblcomplementoine set comite='" + comite + "', proceso='" + proceso + "', idFactura=" + idFactura.ToString + ",idcontabilidad='" + pIdContabilidad + "' where idComplemento=" + idComplemento.ToString
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "delete from tblcomplementoine where idComplemento=" + idComplemento.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function crearXML() As String
        Dim entidad As New dbComplementoIneEntidad(MySqlcon)
        Dim idsConta As New dbComplementoIneContabilidad(MySqlcon)
        Dim dr1 As MySql.Data.MySqlClient.MySqlDataReader
        Dim aux As String()
        Dim ids As New List(Of Integer)
        Dim en As New List(Of String)
        Dim am As New List(Of String)
        dr1 = entidad.buscar(id)
        Dim xml As String = ""
        Dim IdCont As String = ""
        If idContabilidadP <> "" Then
            IdCont = " IdContabilidad=""" + idContabilidadP + """"
        End If
        xml += "<ine:INE xmlns:ine=""http://www.sat.gob.mx/ine"" Version=""1.1"" TipoProceso=""" + proceso + """ TipoComite=""" + comite + """" + IdCont + ">"
        While dr1.Read()
            ids.Add(dr1("idtblcomplementoineentidad"))
            en.Add(dr1("entidad"))
            am.Add(dr1("ambito"))
        End While
        dr1.Close()
        For i As Integer = 0 To ids.ToArray.Length - 1
            aux = idsConta.listaClaves(ids(i))
            If proceso <> "Ordinario" Then
                If comite <> "Ejecutivo Nacional" Then
                    xml += "<ine:Entidad ClaveEntidad=""" + en(i) + """ ambito=""" + am(i) + """>"
                End If
            Else
                If comite <> "Ejecutivo Nacional" Then
                    xml += "<ine:Entidad ClaveEntidad=""" + en(i) + """>"
                End If
            End If
            For Each s As String In aux
                xml += "<ine:Contabilidad IdContabilidad=""" + s + """/>"
            Next
            If comite <> "Ejecutivo Nacional" Then
                xml += "</ine:Entidad>"
            End If
        Next

        xml += "</ine:INE>"

        Return xml
    End Function

    Public Function cadenaOriginal() As String
        Dim c As String = ""
        c += "|" + version + "|" + proceso + "|" + comite + "|" + idContabilidadP + "|"
        Dim entidad As New dbComplementoIneEntidad(MySqlcon)
        Dim idsConta As New dbComplementoIneContabilidad(MySqlcon)
        Dim dr1 As MySql.Data.MySqlClient.MySqlDataReader
        Dim aux As String()
        Dim ids As New List(Of Integer)
        Dim en As New List(Of String)
        Dim am As New List(Of String)
        dr1 = entidad.buscar(id)
        While dr1.Read()
            ids.Add(dr1("idtblcomplementoineentidad"))
            en.Add(dr1("entidad"))
            am.Add(dr1("ambito"))
        End While
        dr1.Close()
        For i As Integer = 0 To ids.ToArray.Length - 1
            c += "|" + en(i).ToString() + "|"
            If am(i) <> "" Then
                c += am(i) + "|"
            End If
            aux = idsConta.listaClaves(ids(i))
            For Each s As String In aux
                c += s + "|"
            Next
        Next
        Return c
    End Function

    Public Function buscar(ByVal idFactura As Integer) As Boolean
        comm.CommandText = "select idcomplemento from tblcomplementoine where idfactura=" + idFactura.ToString()
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.id = i
            Me.idFactura = idFactura
            llenaDatos()
            Return True
        End If
        Return False
    End Function
End Class
